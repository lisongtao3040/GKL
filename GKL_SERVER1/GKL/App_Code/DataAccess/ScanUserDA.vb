Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class ScanUserDA
    Public SqlHelperNew As New SqlHelperNew

    ''' <summary>
    ''' 用户名を取得する
    ''' </summary>
    ''' <param name="userCd_key">用户CD</param>
    ''' <returns>用户名（存在しない場合は空文字）</returns>
    Public Function SelUserName(ByVal userCd_key As String) As String

        'SQLコメント
        '--**テーブル：用户MS : m_user
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("SELECT")
        sb.AppendLine("user_cd")                                                   '用户CD
        sb.AppendLine(", user_name")                                               '用户名
        sb.AppendLine("FROM m_user")
        sb.AppendLine("WHERE 1=1")
        sb.AppendLine("AND user_cd=@user_cd_key")                                  '用户CD

        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@user_cd_key", SqlDbType.VarChar, 10, userCd_key))

        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "m_user", paramList.ToArray)

        If dsInfo.Tables("m_user").Rows.Count = 0 Then
            Return ""
        End If

        Dim nameValue As Object = dsInfo.Tables("m_user").Rows(0).Item("user_name")
        If nameValue Is Nothing OrElse IsDBNull(nameValue) Then
            Return ""
        End If

        Return nameValue.ToString().Trim()

    End Function

End Class
