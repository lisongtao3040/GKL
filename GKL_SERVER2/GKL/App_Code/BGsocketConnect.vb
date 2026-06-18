Imports System
Imports System.Linq
Imports System.Collections.Generic
Imports System.Text
Imports System.Net
Imports System.Threading
Imports System.Net.Sockets
Imports System.IO


Public Class BGsocketConnect
    Private sokClient As Socket
    Public Delegate Sub DataHandler(ByVal strData As String)
    Public Delegate Sub ConnectedHandler(ByVal blConnected As Boolean)
    Public Delegate Sub MessageShow(ByVal strMessage As String, ByVal bl As Boolean)
    Public Shared Event DataReceive As DataHandler
    'Public Shared Event ConnectedChanged As ConnectedHandler
    'Public Shared Event showMessage As MessageShow
    Public isConnected As Boolean = False
    Private strIp As String = "10.160.192.116"
    Private strPort As String = "13909"
    Private threadClient As Thread
    Private isRec As Boolean = True

    Public result As Boolean
    Public msg As String

    Public Sub New()
        ConnectToServer()
    End Sub

    Private Sub ConnectToServer()
        isRec = True

        If sokClient IsNot Nothing AndAlso sokClient.Connected = True Then
            Return
        End If

        sokClient = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
        Dim address As IPAddress = IPAddress.Parse(strIp)
        Dim endpoint As IPEndPoint = New IPEndPoint(address, Integer.Parse(strPort))

        Try
            sokClient.Connect(endpoint)
            ConnectedChanged(True)
            showMessage("服务器连接成功！", True)
            threadClient = New Thread(AddressOf ReceiveMsg)
            threadClient.IsBackground = True
            threadClient.Start()
            isConnected = True
        Catch e As System.Exception

            If (CType((e.[GetType]()), System.Reflection.MemberInfo)).Name = "SocketException" AndAlso (CType((e), System.Net.Sockets.SocketException)).ErrorCode.ToString() = "10061" Then
                ConnectedChanged(False)
                showMessage("服务器端没有开启", False)
            Else
                ConnectedChanged(False)
                showMessage("服务器连接错误", False)
            End If
        End Try
    End Sub

    Public Sub DisConnectServer()
        Try
            isRec = False
            Thread.Sleep(100)
            ConnectedChanged(False)
            showMessage("服务器连接中止！", False)
            sokClient.Close()
        Catch e As System.Exception
        End Try
    End Sub

    Private Sub ReceiveMsg()
        While isRec
            Dim msgArr As Byte() = New Byte(1048575) {}
            Dim length As Integer = 0

            Try
                length = sokClient.Receive(msgArr)
                Dim lenBytes As Byte() = msgArr.ToList().GetRange(0, 4).ToArray()
                Dim packageLen As Integer = BitConverter.ToInt32(lenBytes, 0)
                RaiseEvent DataReceive(System.Text.Encoding.UTF8.GetString(msgArr, 4, packageLen))
            Catch ex As Exception

                If (CType((ex.[GetType]()), System.Reflection.MemberInfo)).Name = "SocketException" AndAlso (CType((ex), System.Net.Sockets.SocketException)).ErrorCode = 10054 Then
                    ConnectedChanged(False)
                    showMessage("服务器中止了连接", False)
                    Return
                Else
                    ConnectedChanged(False)
                    Return
                End If
            End Try
        End While
    End Sub

    Public Function sendMessage(ByVal strData As String) As Boolean
        Try
            Dim arrMsg As Byte() = System.Text.Encoding.UTF8.GetBytes(strData)
            Dim buff As Byte() = New Byte(arrMsg.Length + 4 - 1) {}
            Array.Copy(BitConverter.GetBytes(arrMsg.Length), buff, 4)
            Array.Copy(arrMsg, 0, buff, 4, arrMsg.Length)
            sokClient.Send(buff)
            showMessage("发送报工数据成功", True)
            Return True
        Catch ex As Exception

            If (CType((ex.[GetType]()), System.Reflection.MemberInfo)).Name = "SocketException" AndAlso (CType((ex), System.Net.Sockets.SocketException)).ErrorCode = 10054 Then
                ConnectedChanged(False)
                showMessage("服务器中断了连接", False)
                Return False
            Else
                ConnectedChanged(False)
                showMessage("数据发送错误", False)
                Return False
            End If
        End Try
    End Function

    Public ReadOnly Property isConnect As Boolean
        Get
            Return isConnected
        End Get
    End Property

    Function ConnectedChanged(ByVal kbn As Boolean)
        isConnected = kbn
    End Function

    Function showMessage(ByVal msg As String, ByVal kbn As Boolean)
        result = kbn
        Me.msg = msg
    End Function

End Class
