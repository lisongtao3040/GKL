Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration  '必须要在管理器中添加引用  

Public Class CMsSql

    '获得数据库的连接字符串    
    Public strConnection As String '= "Data Source=10.160.200.39; Initial Catalog=LIS_DB;Persist Security Info=True;User ID=sa;Password=lixil@2014"
    '设置连接    
    Public conn As SqlConnection '= New SqlConnection(strConnection)
    '定义cmd命令    
    Public cmd As New SqlCommand

    Public trans As System.Data.SqlClient.SqlTransaction

    Public Result As Boolean = True

    Public errMsg As String


    Sub New(Optional ByVal connStr As String = "")

        If connStr = "" Then
            connStr = ConfigurationManager.AppSettings("connectionString").ToString()
        End If

        conn = New SqlConnection(connStr)
        conn.Open()
        trans = conn.BeginTransaction()
        cmd.Connection = conn                '设置连接,全局变量    
        cmd.CommandTimeout = 10000000
        cmd.Transaction = trans

    End Sub



    Public Function ExecuteNonQuery(ByVal connectDonNotuse As String, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal paras As SqlParameter()) As Integer

        '将传入的值,分别为cmd的属性赋值    
        cmd.Parameters.AddRange(paras)   '将参数传入    
        cmd.CommandType = cmdType            '设置一个值,解释cmdText   
        cmd.CommandText = cmdText            '设置查询的语句  

        Try
            ExecuteNonQuery = cmd.ExecuteNonQuery()     '执行增删改操作    
            cmd.Parameters.Clear()           '清除参数    
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            errMsg = ex.Message
            Result = False
            ExecuteNonQuery = 0
        Finally

        End Try
        Result = True

    End Function

    ''' <summary>    
    ''' 执行查询的操作,(有参),参数不限    
    ''' </summary>    
    ''' <param name="cmdText">需要执行语句,一般是Sql语句,也有存储过程</param>    
    ''' <param name="cmdType">判断Sql语句的类型,一般都不是存储过程</param>    
    ''' <param name="paras">传入的参数</param>    
    ''' <returns></returns>    
    ''' <remarks></remarks>    
    Public Function ExecSelect(ByVal cmdText As String, ByVal cmdType As CommandType, ByVal paras As SqlParameter()) As DataTable

        Dim sqlAdapter As SqlDataAdapter
        Dim dt As New DataTable
        Dim ds As New DataSet
        '还是给cmd赋值    
        cmd.CommandText = cmdText
        cmd.CommandType = cmdType
        cmd.Connection = conn
        cmd.Parameters.AddRange(paras)  '参数添加    
        sqlAdapter = New SqlDataAdapter(cmd)  '实例化adapter    
        Try
            sqlAdapter.Fill(ds)           '用adapter将dataSet填充     
            dt = ds.Tables(0)             'datatable为dataSet的第一个表    
            cmd.Parameters.Clear()        '清除参数    
        Catch ex As Exception
            MsgBox("查询失败", CType(vbOKOnly + MsgBoxStyle.Exclamation, MsgBoxStyle), "警告")
        Finally                            '最后一定要销毁cmd    
            Call CloseCmd(cmd)
        End Try
        Return dt
    End Function


    ''' <summary>    
    ''' 执行查询的操作,(无参)    
    ''' </summary>    
    ''' <param name="cmdText">需要执行语句,一般是Sql语句,也有存储过程</param>      
    ''' <returns>dataTable,查询到的表格</returns>    
    ''' <remarks></remarks>    
    Public Function ExecSelect(ByVal cmdText As String) As DataTable
        Dim sqlAdapter As SqlDataAdapter
        Dim ds As New DataSet
        '还是给cmd赋值    
        cmd.CommandText = cmdText
        cmd.CommandType = CommandType.Text
        cmd.Connection = conn
        sqlAdapter = New SqlDataAdapter(cmd)  '实例化adapter    
        Try
            sqlAdapter.Fill(ds)           '用adapter将dataSet填充     
            Return ds.Tables(0)             'datatable为dataSet的第一个表    
        Catch ex As Exception
            Return Nothing
        Finally                            '最后一定要销毁cmd    
            Call CloseCmd(cmd)
        End Try
    End Function

    Public Function ExecuteNonQuery(ByVal cmdText As String) As Integer

        '将传入的值,分别为cmd的属性赋值    

        cmd.CommandType = CommandType.Text             '设置一个值,解释cmdText    

        cmd.CommandText = cmdText            '设置查询的语句  

        Try
            ExecuteNonQuery = cmd.ExecuteNonQuery()     '执行增删改操作    
            cmd.Parameters.Clear()           '清除参数    
        Catch ex As Exception
            errMsg = ex.Message
            Result = False
            ExecuteNonQuery = 0
        Finally

        End Try
        Result = True

    End Function


    Public Sub CloseCommit()
        trans.Commit()
        Call CloseConn(conn)
        Call CloseCmd(cmd)
    End Sub

    Public Sub CloseRollback()
        trans.Rollback()
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
