Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Transactions
Imports System.Configuration.ConfigurationSettings
Imports System.Collections.Generic

Public Class GKL_BgDA

    Public SqlHelperNew As New SqlHelperNew

    ''' <summary>
    ''' 获得订单的所有报工数据
    ''' </summary>
    ''' <param name="cd"></param>
    ''' <param name="no"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBgMSData(ByVal cd As String, ByVal no As String) As Data.DataTable

        'SQLコメント
        '--**テーブル：检查计划 : t_check_plan
        Dim sb As New StringBuilder
        'SQL文

        sb.AppendLine("SELECT ")
        sb.AppendLine("*")
        sb.AppendLine("FROM [m_baogong_ms_new]")
        sb.AppendLine("WHERE [cd] = '" & cd & "'")
        sb.AppendLine("AND [make_no] = '" & no & "'")

        'PARAM
        Dim paramList As New List(Of SqlParameter)

        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "SelBgList", paramList.ToArray)

        Return dsInfo.Tables(0)

    End Function


    Public Function SelTCheckResultOkSuu(ByVal make_no As String, ByVal code As String, ByVal lineid As String) As DataTable

        'SQLコメント
        '--**テーブル：检查结果 : t_check_result
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("SELECT")
        sb.AppendLine("*")                                                    '检查No
        sb.AppendLine("FROM v_bg_list_new")
        sb.AppendLine("WHERE 1=1")
        sb.AppendLine("AND [ZuoFan]='" & make_no & "'")   '检查No
        sb.AppendLine("AND [ProductCode]='" & code & "'")   '年

        'sb.AppendLine("AND [line_cd]='" & lineid & "'")   '计划No


        If (Right(lineid, 1) = "A") Then
            sb.AppendLine("AND 'SRM1'+[line_cd]+'A'='" & lineid & "'")   '计划No
        ElseIf (Right(lineid, 1) = "B") Then
            sb.AppendLine("AND 'SRM1'+[line_cd]+'B'='" & lineid & "'")   '计划No
        End If

        'PARAM
        Dim paramList As New List(Of SqlParameter)
        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "SelTCheckResultOkSuu", paramList.ToArray)
        Return dsInfo.Tables("SelTCheckResultOkSuu")
    End Function




End Class
