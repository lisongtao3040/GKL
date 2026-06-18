Imports System
Imports System.Linq
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports System.Threading
Imports System.Net
Imports System.Net.Sockets
Imports System.IO
Imports System.Diagnostics
Imports System.Reflection

Namespace OderConfirmForSap
    Partial Public Class Form1
        'Inherits Form

        Public pubBarCode As String = ""
        Public inProcessing As Boolean = False
        Public inProcessingOrderNo As String = ""
        Public userID As String
        Public userPass As String
        Private msg 'As MsgWindow = 'New MsgWindow()
        Private socC As BGsocketConnect = Nothing

        'Public Sub New()
        '    InitializeComponent()
        'End Sub

        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs)
            inProcessing = False
            'msg.OnGetBarcode += New GetBarcodeEventHandler(AddressOf getBarCode)
            'socketConnect.ConnectedChanged += New socketConnect.ConnectedHandler(AddressOf showConnectStatus)
            'socketConnect.showMessage += New socketConnect.MessageShow(AddressOf MessShow)
            'socketConnect.DataReceive += New socketConnect.DataHandler(AddressOf _dataRecive)
            ControlFormat()
            ConnectToServer()
            CheckUpdate()
        End Sub

        Public Sub MessShow(ByVal mess As String, ByVal bl As Boolean)
            'Me.Invoke(New Action(Sub()
            '                         Me.lbl_Message.Text = mess
            '                         Me.lbl_Message.BackColor = If(bl, Color.Lime, Color.Red)
            '                     End Sub))
        End Sub

        Public Sub getBarCode(ByVal strBarCode As String)
            strBarCode = strBarCode.Replace(vbLf, "").Replace(vbCr, "").Replace(" ", "")
            Dim ss As String() = strBarCode.Split("/"c)

            If ss.Length <> 8 Then
                MessShow("不是合法的生产明细书QR码", False)
                Return
            End If

            'Me.lbl_OrderNo.Text = ss(7).Trim()
            'Me.lbl_MaterialCode.Text = ss(1).Trim()
            'Me.lbl_Amount.Text = ss(2).Trim()
            'Me.lbl_TrolleyNo.Text = ss(6).Trim()
            'Me.txt_ConfirmAmount.Text = ss(2).Trim()
            pubBarCode = strBarCode
            MessShow("读取生产明细书成功", True)
        End Sub

        Private Sub btn_Cancel_Click(ByVal sender As Object, ByVal e As EventArgs)
            'Me.Close()
        End Sub

        Private Sub ControlFormat()
            'Me.lbl_Amount.Text = ""
            'Me.lbl_MaterialCode.Text = ""
            'Me.lbl_OrderNo.Text = ""
            'Me.lbl_TrolleyNo.Text = ""
            'Me.txt_ConfirmAmount.Text = ""
            'Me.txt_ConfirmAmount.Enabled = False
            pubBarCode = ""
            inProcessing = False
        End Sub

        Private Sub Form1_Closing(ByVal sender As Object, ByVal e As CancelEventArgs)
            'Bt.ScanLib.Control.btScanDisable()
            'msg.OnGetBarcode -= New GetBarcodeEventHandler(AddressOf getBarCode)
            'socketConnect.ConnectedChanged -= New socketConnect.ConnectedHandler(AddressOf showConnectStatus)
            'socketConnect.showMessage -= New socketConnect.MessageShow(AddressOf MessShow)
            'socketConnect.DataReceive -= New socketConnect.DataHandler(AddressOf _dataRecive)
        End Sub

        Private Sub ConnectToServer()
            Try
                If socC Is Nothing Then socC = New BGsocketConnect()

                If socC.isConnect = True Then
                    'Me.pnl_Login.Visible = True
                Else
                    socC = Nothing
                End If

            Catch
            End Try
        End Sub

        Private Sub disConnectToServer()
            Try

                If socC IsNot Nothing Then
                    socC.DisConnectServer()
                    socC = Nothing
                    showConnectStatus(False)
                End If

            Catch
            Finally
            End Try
        End Sub

        Private Sub showConnectStatus(ByVal bl As Boolean)
            'Me.Invoke(New Action(Sub()
            '                         Me.lbl_ServerStatus.BackColor = If(bl, Color.Lime, Color.Red)
            '                         If bl = False Then MessShow("服务器连接中断", False)
            '                     End Sub))
        End Sub

        Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs)
            dataSend()
        End Sub

        Private Sub dataSend()
            If inProcessing Then
                MessShow("请等待上一条数据处理成功", False)
                Return
            End If

            ' Me.txt_ConfirmAmount.Enabled = False

            If String.IsNullOrEmpty(pubBarCode) Then
                MessShow("没有可发送的数据", False)
                Return
            End If

            Dim ss As String() = pubBarCode.Split("/"c)
            inProcessingOrderNo = ss(7)
            'ss(2) = Me.txt_ConfirmAmount.Text
            Dim sd As String = String.Join("/", ss.ToArray())

            If socC Is Nothing Then
                MessShow("服务器未连接，不可报工", False)
                Return
            End If

            socC.sendMessage("XX" & sd)
            pubBarCode = ""
            inProcessing = True
        End Sub

        Private Sub btn_AmountR_Click(ByVal sender As Object, ByVal e As EventArgs)
            'Me.txt_ConfirmAmount.Enabled = True
            'Me.txt_ConfirmAmount.Focus()
            'Me.txt_ConfirmAmount.[Select](0, txt_ConfirmAmount.Text.Length)
        End Sub

        Private Sub _dataRecive(ByVal strData As String)
            inProcessing = False
            Dim strDataFlag As String

            If strData.Length < 2 Then
                MessShow("接收到不可识别数据", False)
                Return
            End If

            strDataFlag = strData.Substring(0, 2)

            If strDataFlag.ToUpper() = "XY" Then
                MessShow(inProcessingOrderNo & " 报工成功", True)
                Return
            ElseIf strDataFlag.ToUpper() = "XZ" Then
                MessShow(inProcessingOrderNo & strData.Substring(2), False)
                Return
            ElseIf strDataFlag.ToUpper() = "ZY" Then
                MessShow("登录成功", True)
                'Me.Invoke(New Action(Sub()
                '                         Me.pnl_Login.Visible = False
                '                         Me.txt_UserID.Enabled = False
                '                         Me.txt_UserPass.Enabled = False
                '                         Me.btn_Login.Enabled = False
                '                         Me.pnl_Login.Enabled = False
                '                     End Sub))
                'Bt.ScanLib.Control.btScanEnable()
                Return
            ElseIf strDataFlag.ToUpper() = "ZZ" Then
                MessShow(strData.Substring(2), False)
                Return
            ElseIf strDataFlag.ToUpper() = "YZ" Then
                isUpdate(strData)
            End If
        End Sub

        Private Sub btn_Login_Click(ByVal sender As Object, ByVal e As EventArgs)
            'socC.sendMessage("ZX" & Me.txt_UserID.Text.PadRight(15, " "c) + Me.txt_UserPass.Text.PadRight(15, " "c))
            Return
        End Sub

        Private Sub CheckUpdate()
            If Not socC.isConnected OrElse socC Is Nothing Then
                Return
            Else
                Dim appVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString()
                socC.sendMessage("YX" & appVersion.ToString())
            End If
        End Sub

        Private Sub isUpdate(ByVal strData As String)
            'If MessageBox.Show("服务器有更新程序，是否进行自动更新？" & "/r/n" & "当前版本:" & Assembly.GetExecutingAssembly().GetName().Version.ToString(), "更新提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) <> DialogResult.Yes Then Return
            Dim path As String = "" ' Me.[GetType]().Assembly.GetModules()(0).FullyQualifiedName
            Dim en As Int32 = path.LastIndexOf("\")
            Dim AppPath As String = path.Substring(0, en)
            Dim fs As FileStream = New FileStream(AppPath & "\serverurl.txt", FileMode.Create, FileAccess.Write)
            Dim sw As StreamWriter = New StreamWriter(fs)
            sw.WriteLine(strData.Substring(2))
            sw.Close()
            fs.Close()
            Dim process As System.Diagnostics.Process = New System.Diagnostics.Process()
            process.StartInfo.FileName = AppPath & "\update.exe"
            process.Start()
            'Me.Close()
        End Sub

        Private Sub testRead(ByVal data As String)
            Dim path As String = "" 'Me.[GetType]().Assembly.GetModules()(0).FullyQualifiedName
            Dim en As Int32 = path.LastIndexOf("\")
            Dim AppPath As String = path.Substring(0, en)
            Dim fs As FileStream = New FileStream(AppPath & "\tmpdata.txt", FileMode.Append, FileAccess.Write)
            Dim sw As StreamWriter = New StreamWriter(fs)
            sw.WriteLine(data)
            sw.Close()
            fs.Close()
        End Sub

        'Private Sub Form1_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        '    If e.KeyChar = 13 Then
        '        dataSend()
        '    End If
        'End Sub
    End Class
End Namespace
