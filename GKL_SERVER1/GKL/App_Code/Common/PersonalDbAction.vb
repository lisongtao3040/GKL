Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration  '必须要在管理器中添加引用  

Public Class PersonalDbTransactionScopeAction

    '获得数据库的连接字符串    
    Private ReadOnly strConnection As String = DataAccessManager.Connection
    '设置连接    
    Public conn As SqlConnection = New SqlConnection(strConnection)
    '定义cmd命令    
    Public cmd As New SqlCommand

    Public trans As System.Data.SqlClient.SqlTransaction

    Public Result As Boolean = True


    Sub New(Optional ByVal beginTran As Boolean = True)

        conn.Open()

        If beginTran Then
            trans = conn.BeginTransaction()
        End If

        cmd.Connection = conn                '设置连接,全局变量    
        cmd.CommandTimeout = 10000000

        If beginTran Then
            cmd.Transaction = trans
        End If


    End Sub



    Public Function ExecuteNonQuery(ByVal connectDonNotuse As String, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal paras As SqlParameter()) As Integer

        '将传入的值,分别为cmd的属性赋值    
        cmd.Parameters.AddRange(paras)   '将参数传入    
        cmd.CommandType = cmdType            '设置一个值,解释cmdText    

        cmd.CommandText = cmdText            '设置查询的语句  

        Try
            ExecuteNonQuery = cmd.ExecuteNonQuery()     '执行增删改操作    
            cmd.Parameters.Clear()           '清除参数    
        Catch ex As Exception
            Result = False
            ExecuteNonQuery = 0
        Finally

        End Try
        Result = True



    End Function



    Public Function FillDataset(ByVal cmdType As CommandType, ByVal cmdText As String, ByRef ds As DataSet, ByVal dtName As String, ByVal paras As SqlParameter()) As Integer

        Dim dataAdatpter As System.Data.SqlClient.SqlDataAdapter = Nothing
        Dim command As New System.Data.SqlClient.SqlCommand
        command = New System.Data.SqlClient.SqlCommand(cmdText.ToString(), conn)
        command.Parameters.AddRange(paras)
        command.CommandTimeout = 320

        '実行
        '戻りデータセット
        Dim dsInfo As New Data.DataSet
        dataAdatpter = New System.Data.SqlClient.SqlDataAdapter(command)
        dataAdatpter.Fill(dsInfo)
        ds = dsInfo

        Return 0

    End Function




    Public Sub CloseCommit()
        If trans IsNot Nothing Then
            trans.Commit()
        End If

        Call CloseConn(conn)
        Call CloseCmd(cmd)
    End Sub

    Public Sub CloseRollback()
        If trans IsNot Nothing Then
            trans.Rollback()
        End If

        Call CloseConn(conn)
        Call CloseCmd(cmd)
    End Sub



    ''' <summary>    
    ''' 关闭连接    
    ''' </summary>    
    ''' <param name="conn">需要关闭的连接</param>    
    ''' <remarks></remarks>    
    Public Sub CloseConn(ByVal conn As SqlConnection)
        If (conn.State <> ConnectionState.Closed) Then  '如果没有关闭    
            conn.Close()                    '关闭连接    
            conn = Nothing                  '不指向原对象    
        End If

    End Sub
    ''' <summary>    
    ''' 关闭命令    
    ''' </summary>    
    ''' <param name="cmd">需要关闭的命令</param>    
    ''' <remarks></remarks>    
    Public Sub CloseCmd(ByVal cmd As SqlCommand)

        If Not IsNothing(cmd) Then          '如果cmd命令存在    
            cmd.Dispose()                   '销毁    
            cmd = Nothing
        End If
    End Sub









End Class

Public Class PersonFillDataset


    '获得数据库的连接字符串    
    Private ReadOnly strConnection As String = System.Configuration.ConfigurationManager.ConnectionStrings("connectionString").ConnectionString
    '设置连接    
    Public MyCon As SqlConnection = New SqlConnection(strConnection)
    '定义cmd命令    
    Public cmd As New SqlCommand



    Public Function FillDataset(ByVal cmdType As CommandType, ByVal cmdText As String, ByRef ds As DataSet, ByVal dtName As String, ByVal paras As SqlParameter()) As Integer
        'MyCon.Open()
        'Dim Cmd As New SqlCommand
        'Dim MyDa As New SqlDataAdapter
        'Cmd.CommandType = CommandType.Text
        'Cmd.Connection = MyCon
        'Cmd.CommandTimeout = 300000
        'Cmd.Parameters.AddRange(paras)   '将参数传入    
        'Cmd.CommandText = cmdText

        'Dim dt As New DataTable
        'dt.TableName = dtName

        'MyDa.SelectCommand = Cmd

        'MyDa.Fill(dt)

        'ds.Tables.Add(dt)

        ' MyCon.Close()


        Dim dataAdatpter As System.Data.SqlClient.SqlDataAdapter = Nothing
        Dim conn As System.Data.SqlClient.SqlConnection = Nothing
        conn = New System.Data.SqlClient.SqlConnection(strConnection)
        conn.Open()
        Dim command As New System.Data.SqlClient.SqlCommand
        command = New System.Data.SqlClient.SqlCommand(cmdText.ToString(), conn)
        command.Parameters.AddRange(paras)
        command.CommandTimeout = 320
        'パラメタの設定

        '実行
        '戻りデータセット
        Dim dsInfo As New Data.DataSet
        dataAdatpter = New System.Data.SqlClient.SqlDataAdapter(command)
        dataAdatpter.Fill(dsInfo)
        ds = dsInfo

        Return 0

    End Function




End Class



