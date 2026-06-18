Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Transactions
Imports System.Configuration.ConfigurationSettings
Imports System.Collections.Generic

Public Class t_colorcheck_resultDA
    Public SqlHelperNew As New SqlHelperNew

    Public Function Sel_t_colorcheck_result(ByVal lineId_key As String,
           ByVal ymd As String) As Data.DataTable

        'SQLコメント
        '--**テーブル：检查结果 : t_check_result
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("SELECT")
        sb.AppendLine("a.*")                                                    '检查No
        sb.AppendLine("FROM t_colorcheck_result a")
        sb.AppendLine("LEFT JOIN m_Line_List b")
        sb.AppendLine("ON isnull(a.linecode,'') = isnull(b.zhipinLine,'')")
        sb.AppendLine("WHERE 1=1")
        sb.AppendLine("AND  CONVERT(varchar(100), a.updatedate, 23) = '" & ymd & "'")
        If lineId_key <> "" Then
            sb.AppendLine("AND isnull(a.linecode,'') in (select zhongjiancaiLine from m_Line_List WHERE zhipinLine = '" & lineId_key & "')")
        End If

        sb.AppendLine("ORDER BY linecode,checkid desc")
        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "ms")
        Return dsInfo.Tables(0)
    End Function
End Class
