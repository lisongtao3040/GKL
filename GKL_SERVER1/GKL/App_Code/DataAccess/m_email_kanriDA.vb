Imports Microsoft.VisualBasic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Transactions
Imports System.Configuration.ConfigurationSettings
Imports System.Collections.Generic

Public Class m_email_kanriDA

    Public SqlHelperNew As New SqlHelperNew

    ''' <summary>
    ''' 邮箱信息
    ''' </summary>
    ''' <returns></returns>
    Public Function Selm_email_kanri() As Data.DataTable
        'SQLコメント
        '--**テーブル：m_email_kanri
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("SELECT")
        sb.AppendLine(" * ")                                                   '用户CD
        sb.AppendLine("FROM m_email_kanri")
        sb.AppendLine("WHERE 1=1")
        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "m_email_kanri")
        Return dsInfo.Tables("m_email_kanri")

    End Function

    ''' <summary>
    ''' m_email_kanri一览数据取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMailData(ByVal line_id As String) As DataTable
        Dim sb As New StringBuilder
        Dim paramList As New List(Of SqlParameter)

        sb.AppendLine("select")
        sb.AppendLine("    xi")
        sb.AppendLine("  , line_id")
        sb.AppendLine("  , to_email")
        sb.AppendLine("  , cc_email")
        sb.AppendLine("  , send_email_time")
        sb.AppendLine("  , qidong")
        sb.AppendLine("from [m_email_kanri]")

        If line_id <> "" Then
            sb.AppendLine("where line_id = '" & line_id & "'")
        End If

        sb.AppendLine("order by line_id")

        Dim ds As New DataSet

        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), ds, "ComRes", paramList.ToArray)
        Return ds.Tables(0)

    End Function


    ''' <summary>
    '''  m_email_kanri数据删除
    ''' </summary>
    ''' <param name="line_id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DelMailData(ByVal line_id As String) As Integer
        Dim sb As New StringBuilder
        Dim paramList As New List(Of SqlParameter)

        sb.AppendLine("delete from [m_email_kanri]")
        sb.AppendLine("where line_id = '" & line_id & "'")

        '删除の実行
        Return SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)

    End Function

    ''' <summary>
    ''' m_email_kanri数据更新
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdMailData(ByVal xi As String, ByVal line_id As String, ByVal to_email As String, ByVal cc_email As String) As Integer
        Dim sb As New StringBuilder
        Dim paramList As New List(Of SqlParameter)

        sb.AppendLine("update [m_email_kanri]")
        sb.AppendLine("set xi = N'" & xi & "'")
        sb.AppendLine("  , to_email = '" & to_email & "'")
        sb.AppendLine("  , cc_email = '" & cc_email & "'")
        'sb.AppendLine("  , send_email_time = '" & send_email_time & "'")
        'sb.AppendLine("  , qidong = '" & qidong & "'")
        sb.AppendLine("where line_id = '" & line_id & "'")

        '更新の実行
        Return SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)
    End Function


    ''' <summary>
    ''' m_email_kanri数据登录
    ''' </summary>
    Public Function InsMailData2(ByVal xi As String, ByVal line_id As String, ByVal to_email As String, ByVal cc_email As String, ByVal send_email_time As String, ByVal qidong As String) As Integer
        Dim sb As New StringBuilder

        sb.AppendLine("insert into [m_email_kanri]")
        sb.AppendLine("(")
        sb.AppendLine("    xi")
        sb.AppendLine("  , line_id")
        sb.AppendLine("  , to_email")
        sb.AppendLine("  , cc_email")
        sb.AppendLine("  , send_email_time")
        sb.AppendLine("  , qidong")
        sb.AppendLine(")")
        sb.AppendLine("values")
        sb.AppendLine("(")
        sb.AppendLine("    N'" & xi & "'")
        sb.AppendLine("  , '" & line_id & "'")
        sb.AppendLine("  , '" & to_email & "'")
        sb.AppendLine("  , '" & cc_email & "'")
        sb.AppendLine("  , '" & send_email_time & "'")
        sb.AppendLine("  , '" & qidong & "'")
        sb.AppendLine(")")

        Return SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString())

    End Function


    Public Function InsMailData(ByVal xi As String, ByVal line_id As String, ByVal to_email As String, ByVal cc_email As String) As Integer
        Dim sb As New StringBuilder

        sb.AppendLine("insert into [m_email_kanri]")
        sb.AppendLine("(")
        sb.AppendLine("    xi")
        sb.AppendLine("  , line_id")
        sb.AppendLine("  , to_email")
        sb.AppendLine("  , cc_email")
        'sb.AppendLine("  , send_email_time")
        'sb.AppendLine("  , qidong")
        sb.AppendLine(")")
        sb.AppendLine("values")
        sb.AppendLine("(")
        sb.AppendLine("    N'" & xi & "'")
        sb.AppendLine("  , '" & line_id & "'")
        sb.AppendLine("  , '" & to_email & "'")
        sb.AppendLine("  , '" & cc_email & "'")
        'sb.AppendLine("  , '" & send_email_time & "'")
        'sb.AppendLine("  , '" & qidong & "'")
        sb.AppendLine(")")

        Return SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString())

    End Function
End Class
