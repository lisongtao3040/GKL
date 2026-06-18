Imports Microsoft.VisualBasic
Imports System.Text
Imports System.Data.SqlClient
Imports System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports System.Collections.Generic
Imports System.Data

Public Class UpdDA
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
        sb.AppendLine("from [LIS_DB].[dbo].[m_email_kanri]")

        If line_id <> "" Then
            sb.AppendLine("where line_id = '" & line_id & "'")
        End If

        sb.AppendLine("order by line_id")

        Dim ds As New DataSet

        FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), ds, "ComRes", paramList.ToArray)
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

        sb.AppendLine("delete from [LIS_DB].[dbo].[m_email_kanri]")
        sb.AppendLine("where line_id = '" & line_id & "'")

        '删除の実行
        Return ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)

    End Function

    ''' <summary>
    ''' m_email_kanri数据更新
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdMailData(ByVal xi As String, ByVal line_id As String, ByVal to_email As String, ByVal cc_email As String, ByVal send_email_time As String, ByVal qidong As String) As Integer
        Dim sb As New StringBuilder
        Dim paramList As New List(Of SqlParameter)

        sb.AppendLine("update [LIS_DB].[dbo].[m_email_kanri]")
        sb.AppendLine("set xi = '" & xi & "'")
        sb.AppendLine("  , to_email = '" & to_email & "'")
        sb.AppendLine("  , cc_email = '" & cc_email & "'")
        sb.AppendLine("  , send_email_time = '" & send_email_time & "'")
        sb.AppendLine("  , qidong = '" & qidong & "'")
        sb.AppendLine("where line_id = '" & line_id & "'")

        '更新の実行
        Return ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)
    End Function


    ''' <summary>
    ''' m_email_kanri数据登录
    ''' </summary>
    Public Function InsMailData(ByVal xi As String, ByVal line_id As String, ByVal to_email As String, ByVal cc_email As String, ByVal send_email_time As String, ByVal qidong As String) As Integer
        Dim sb As New StringBuilder

        sb.AppendLine("insert into [LIS_DB].[dbo].[m_email_kanri]")
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
        sb.AppendLine("    '" & xi & "'")
        sb.AppendLine("  , '" & line_id & "'")
        sb.AppendLine("  , '" & to_email & "'")
        sb.AppendLine("  , '" & cc_email & "'")
        sb.AppendLine("  , '" & send_email_time & "'")
        sb.AppendLine("  , '" & qidong & "'")
        sb.AppendLine(")")

        Return ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString())

    End Function


End Class
