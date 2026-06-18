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
    Public Function GetMailData(ByVal lineId As String) As DataTable
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

        If lineId <> "" Then
            sb.AppendLine("where line_id = '" & lineId & "'")
        End If

        sb.AppendLine("order by line_id")

        Dim ds As New DataSet

        FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), ds, "ComRes", paramList.ToArray)
        Return ds.Tables(0)

    End Function

    ''' <summary>
    ''' 一览数据取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMainData() As DataTable
        Dim sb As New StringBuilder
        Dim paramList As New List(Of SqlParameter)

        sb.AppendLine("select top 20")
        sb.AppendLine("    IDX")
        sb.AppendLine("  , COMPUTER")
        sb.AppendLine("  , F_SIGN")
        sb.AppendLine("  , F_VALUE")
        sb.AppendLine("  , INS_DATE")
        sb.AppendLine("  , UPD_DATE")
        sb.AppendLine("  , W_IS_CHOOSE")
        sb.AppendLine("from [LIS_DB].[dbo].[t_com_res]")
        sb.AppendLine("order by IDX desc")

        Dim ds As New DataSet

        FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), ds, "ComRes", paramList.ToArray)
        Return ds.Tables(0)


    End Function


    ''' <summary>
    ''' IS_CHOOSE和UPD_DATE更新
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdWinFormData(ByVal id As Integer) As Integer
        Dim sb As New StringBuilder
        Dim paramList As New List(Of SqlParameter)

        sb.AppendLine("update [LIS_DB].[dbo].[t_com_res]")
        sb.AppendLine("set IS_CHOOSE = '1'")
        sb.AppendLine("  , UPD_DATE = getdate()")
        sb.AppendLine("where IDX = " & id)

        '更新の実行
        Return ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)

    End Function

    ''' <summary>
    ''' W_IS_CHOOSE,W_UPD_DATE和W_UPD_USER更新
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="updUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdWebData(ByVal id As Integer, ByVal updUser As String) As Integer
        Dim sb As New StringBuilder
        Dim paramList As New List(Of SqlParameter)

        sb.AppendLine("update [LIS_DB].[dbo].[t_com_res]")
        sb.AppendLine("set W_IS_CHOOSE = '1'")
        sb.AppendLine("  , W_UPD_DATE = getdate()")
        sb.AppendLine("  , W_UPD_USER = '" & updUser & "'")
        sb.AppendLine("where IDX = " & id)

        '更新の実行
        Return ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)
    End Function


    ''' <summary>
    ''' 数据登录
    ''' </summary>
    ''' <param name="comName"></param>
    ''' <param name="fSign"></param>
    ''' <param name="fValue"></param>
    ''' <param name="computer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsMainData(ByVal comName As String, ByVal fSign As String, ByVal fValue As String, ByVal computer As String) As Integer
        Dim sb As New StringBuilder

        sb.AppendLine("insert into [LIS_DB].[dbo].[t_com_res]")
        sb.AppendLine("(")
        sb.AppendLine("    COM_NAME")
        sb.AppendLine("  , F_SIGN")
        sb.AppendLine("  , F_VALUE")
        sb.AppendLine("  , COMPUTER")
        sb.AppendLine("  , IS_CHOOSE")
        sb.AppendLine("  , INS_DATE")
        sb.AppendLine("  , UPD_DATE")
        sb.AppendLine(")")
        sb.AppendLine("values")
        sb.AppendLine("(")
        sb.AppendLine("    '" & comName & "'")
        sb.AppendLine("  , '" & fSign & "'")
        sb.AppendLine("  , '" & fValue & "'")
        sb.AppendLine("  , '" & computer & "'")
        sb.AppendLine("  , '0'")
        sb.AppendLine("  , getdate()")
        sb.AppendLine("  , getdate()")
        sb.AppendLine(")")

        Return ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString())

    End Function


End Class
