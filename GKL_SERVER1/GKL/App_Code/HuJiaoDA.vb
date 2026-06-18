Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Transactions
Imports System.Configuration.ConfigurationSettings
Imports System.Collections.Generic

Public Class HuJiaoDA

    Public Function Get_m_station_list(ByVal line_id As String) As Data.DataTable

        'SQLコメント
        '--**テーブル：检查计划 : t_check_plan
        Dim sb As New StringBuilder
        'SQL文

        sb.AppendLine("SELECT ")
        sb.AppendLine("*")
        sb.AppendLine("FROM [m_station_list]")
        sb.AppendLine("WHERE [line_id] = '" & line_id & "'")

        'PARAM
        Dim paramList As New List(Of SqlParameter)

        Dim dsInfo As New Data.DataSet
        Dim SqlHelperNew As New SqlHelperNew
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "Get_m_station_list", paramList.ToArray)

        Return dsInfo.Tables(0)

    End Function

End Class
