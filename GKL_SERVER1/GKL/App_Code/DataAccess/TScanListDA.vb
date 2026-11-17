Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class TScanListDA
    Public SqlHelperNew As New SqlHelperNew

    ''' <summary>
    ''' 扫描列表Infoを検索する
    ''' </summary>
    ''' <param name="chkNo_key">检查No</param>
    ''' <param name="lineId_key">生产线</param>
    ''' <param name="makeNo_key">作番</param>
    ''' <returns>扫描列表Info</returns>
    Public Function SelTScanList(ByVal chkNo_key As String,
                                 ByVal lineId_key As String,
                                 ByVal makeNo_key As String) As Data.DataTable

        'SQLコメント
        '--**テーブル：扫描列表 : t_scan_list
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("SELECT")
        sb.AppendLine("a.scan_id")                                                   '扫描ID
        sb.AppendLine(", a.scan_txt")                                                '扫码值
        sb.AppendLine(", a.result")                                                  '结果值
        sb.AppendLine(", a.upd_user+'-'+b.user_name as upd_user")                                                '更新者
        sb.AppendLine(", a.upd_date")                                                '更新日
        sb.AppendLine("FROM t_scan_list a")
        sb.AppendLine("LEFT JOIN m_user b")
        sb.AppendLine("ON a.upd_user = b.user_cd")
        sb.AppendLine("WHERE 1=1")
        sb.AppendLine("AND a.chk_no=@chk_no_key")                                    '检查No
        sb.AppendLine("AND a.line_id=@line_id_key")                                  '生产线
        sb.AppendLine("AND a.make_no=@make_no_key")                                  '作番
        sb.AppendLine("ORDER BY a.scan_id DESC")

        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@chk_no_key", SqlDbType.VarChar, 20, chkNo_key))
        paramList.Add(SqlHelperNew.MakeParam("@line_id_key", SqlDbType.VarChar, 10, lineId_key))
        paramList.Add(SqlHelperNew.MakeParam("@make_no_key", SqlDbType.VarChar, 20, makeNo_key))

        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "t_scan_list", paramList.ToArray)

        Return dsInfo.Tables("t_scan_list")

    End Function

    Public Function IsHaveNg(ByVal chkNo_key As String,
                                 ByVal lineId_key As String,
                                 ByVal makeNo_key As String) As Boolean


        'SQLコメント
        '--**テーブル：扫描列表 : t_scan_list
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("SELECT")
        sb.AppendLine("a.scan_id")                                                   '扫描ID
        '更新日
        sb.AppendLine("FROM t_scan_list a")

        sb.AppendLine("WHERE 1=1")
        sb.AppendLine("AND a.chk_no=@chk_no_key")                                    '检查No
        sb.AppendLine("AND a.line_id=@line_id_key")                                  '生产线
        sb.AppendLine("AND a.result='NG'")

        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@chk_no_key", SqlDbType.VarChar, 20, chkNo_key))
        paramList.Add(SqlHelperNew.MakeParam("@line_id_key", SqlDbType.VarChar, 10, lineId_key))

        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "t_scan_list", paramList.ToArray)
        If dsInfo.Tables("t_scan_list").Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function




    ''' <summary>
    ''' 扫描列表Infoを登録する
    ''' </summary>
    ''' <param name="chkNo">检查No</param>
    ''' <param name="lineId">生产线</param>
    ''' <param name="makeNo">作番</param>
    ''' <param name="code">コード（URLパラメータ）</param>
    ''' <param name="scanTxt">扫码值</param>
    ''' <param name="result">结果值</param>
    ''' <param name="insUser">登録者</param>
    ''' <returns>True:成功</returns>
    Public Function InsTScanList(ByVal chkNo As String, _
                                 ByVal lineId As String, _
                                 ByVal makeNo As String, _
                                 ByVal code As String, _
                                 ByVal scanTxt As String, _
                                 ByVal result As String, _
                                 ByVal insUser As String) As Boolean

        'SQLコメント
        '--**テーブル：扫描列表 : t_scan_list
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("INSERT INTO t_scan_list")
        sb.AppendLine("(")
        sb.AppendLine("chk_no")                                                    '检查No
        sb.AppendLine(", line_id")                                                 '生产线
        sb.AppendLine(", make_no")                                                 '作番
        sb.AppendLine(", code")                                                    'コード
        sb.AppendLine(", scan_txt")                                                '扫码值
        sb.AppendLine(", result")                                                  '结果值
        sb.AppendLine(", upd_user")                                                '更新者
        sb.AppendLine(", upd_date")                                                '更新日
        sb.AppendLine(", ins_user")                                                '登録者
        sb.AppendLine(", ins_date")                                                '登録日
        sb.AppendLine(")")
        sb.AppendLine("VALUES(")
        sb.AppendLine("@chk_no")                                                   '检查No
        sb.AppendLine(", @line_id")                                                '生产线
        sb.AppendLine(", @make_no")                                                '作番
        sb.AppendLine(", @code")                                                   'コード
        sb.AppendLine(", @scan_txt")                                               '扫码值
        sb.AppendLine(", @result")                                                 '结果值
        sb.AppendLine(", @upd_user")                                               '更新者
        sb.AppendLine(", getdate()")                                               '更新日
        sb.AppendLine(", @ins_user")                                               '登録者
        sb.AppendLine(", getdate()")                                               '登録日
        sb.AppendLine(")")

        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@chk_no", SqlDbType.VarChar, 20, chkNo))
        paramList.Add(SqlHelperNew.MakeParam("@line_id", SqlDbType.VarChar, 10, lineId))
        paramList.Add(SqlHelperNew.MakeParam("@make_no", SqlDbType.VarChar, 20, makeNo))
        paramList.Add(SqlHelperNew.MakeParam("@code", SqlDbType.VarChar, 30, code))
        paramList.Add(SqlHelperNew.MakeParam("@scan_txt", SqlDbType.VarChar, 200, scanTxt))
        paramList.Add(SqlHelperNew.MakeParam("@result", SqlDbType.VarChar, 30, result))
        paramList.Add(SqlHelperNew.MakeParam("@upd_user", SqlDbType.VarChar, 20, insUser))
        paramList.Add(SqlHelperNew.MakeParam("@ins_user", SqlDbType.VarChar, 20, insUser))

        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)

        Return True

    End Function

    ''' <summary>
    ''' 扫描列表Infoの結果値を更新する
    ''' </summary>
    ''' <param name="scanId">扫描ID</param>
    ''' <param name="chkNo">检查No</param>
    ''' <param name="lineId">生产线</param>
    ''' <param name="makeNo">作番</param>
    ''' <param name="result">结果值</param>
    ''' <param name="updUser">更新者</param>
    ''' <returns>True:更新成功</returns>
    Public Function UpdTScanListResult(ByVal scanId As Integer, _
                                       ByVal chkNo As String, _
                                       ByVal lineId As String, _
                                       ByVal makeNo As String, _
                                       ByVal result As String, _
                                       ByVal updUser As String) As Boolean

        'SQLコメント
        '--**テーブル：扫描列表 : t_scan_list
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("UPDATE t_scan_list")
        sb.AppendLine("SET")
        sb.AppendLine("result=@result")                                            '结果值
        sb.AppendLine(", upd_user=@upd_user")                                      '更新者
        sb.AppendLine(", upd_date=getdate()")                                      '更新日
        sb.AppendLine("WHERE 1=1")
        sb.AppendLine("AND scan_id=@scan_id")                                      '扫描ID
        sb.AppendLine("AND chk_no=@chk_no")                                        '检查No
        sb.AppendLine("AND line_id=@line_id")                                      '生产线
        sb.AppendLine("AND make_no=@make_no")                                      '作番

        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@scan_id", SqlDbType.Int, 4, scanId))
        paramList.Add(SqlHelperNew.MakeParam("@chk_no", SqlDbType.VarChar, 20, chkNo))
        paramList.Add(SqlHelperNew.MakeParam("@line_id", SqlDbType.VarChar, 10, lineId))
        paramList.Add(SqlHelperNew.MakeParam("@make_no", SqlDbType.VarChar, 20, makeNo))
        paramList.Add(SqlHelperNew.MakeParam("@result", SqlDbType.VarChar, 30, result))
        paramList.Add(SqlHelperNew.MakeParam("@upd_user", SqlDbType.VarChar, 20, updUser))

        Dim rtv As Integer = SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)

        Return (rtv > 0)

    End Function

End Class
