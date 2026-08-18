



Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Transactions
Imports System.Configuration.ConfigurationSettings
Imports System.Collections.Generic

Public Class TCheckResultDA
    Public SqlHelperNew As New SqlHelperNew
    ''' <summary>
    ''' 
    ''' 检查结果Infoを検索する
    ''' </summary>
    '''<param name="chkNo_key">检查No</param>
    '''<param name="nen_key">年</param>
    '''<param name="lineId_key">生产线</param>
    '''<param name="makeNo_key">作番</param>
    ''' <returns>检查结果Info</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function SelTCheckResult(ByVal chkNo_key As String,
           ByVal nen_key As String,
           ByVal lineId_key As String,
           ByVal makeNo_key As String) As Data.DataTable

        'SQLコメント
        '--**テーブル：检查结果 : t_check_result
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("SELECT")
        sb.AppendLine("chk_no")                                                    '检查No
        sb.AppendLine(", nen")                                                     '年
        sb.AppendLine(", plan_no")                                                 '计划No
        sb.AppendLine(", line_id")                                                 '生产线
        sb.AppendLine(", make_no")                                                 '作番
        sb.AppendLine(", code")                                                    'コード
        sb.AppendLine(", suu")                                                     '数量
        sb.AppendLine(", temp_id")                                                 '检查模板编号
        sb.AppendLine(", chk_result")                                              '检查结果
        sb.AppendLine(", chk_user")                                                '检查者
        sb.AppendLine(", chk_start_date")   '检查開始日
        sb.AppendLine(", chk_end_date")                                            '检查完了日
        sb.AppendLine(", parent_chk_no")   '父检查No
        sb.AppendLine(", status")                                                  '状态
        sb.AppendLine(", ins_user")                                                '登録者
        sb.AppendLine(", ins_date")                                                '登録日

        sb.AppendLine("FROM t_check_result")
        sb.AppendLine("WHERE 1=1")
        If chkNo_key <> "" Then
            sb.AppendLine("AND chk_no=@chk_no_key")   '检查No
        End If
        If nen_key <> "" Then
            sb.AppendLine("AND nen=@nen_key")   '年
        End If
        If lineId_key <> "" Then
            'sb.AppendLine("AND line_id=@line_id_key")   '生产线

            sb.AppendLine("AND line_id=@line_id_key")   '生产线
        End If
        If makeNo_key <> "" Then
            sb.AppendLine("AND make_no=@make_no_key")   '作番
        End If

        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@chk_no_key", SqlDbType.VarChar, 20, chkNo_key))
        paramList.Add(SqlHelperNew.MakeParam("@nen_key", SqlDbType.VarChar, 4, nen_key))
        paramList.Add(SqlHelperNew.MakeParam("@line_id_key", SqlDbType.VarChar, 10, lineId_key))
        paramList.Add(SqlHelperNew.MakeParam("@make_no_key", SqlDbType.VarChar, 20, makeNo_key))

        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "t_check_result", paramList.ToArray)

        Return dsInfo.Tables("t_check_result")

    End Function

    ''' <summary>
    ''' 
    ''' 检查结果Infoを更新する
    ''' </summary>
    '''<param name="chkNo_key">检查No</param>
    '''<param name="nen_key">年</param>
    '''<param name="lineId_key">生产线</param>
    '''<param name="makeNo_key">作番</param>
    '''<param name="chkNo">检查No</param>
    '''<param name="nen">年</param>
    '''<param name="planNo">计划No</param>
    '''<param name="lineId">生产线</param>
    '''<param name="makeNo">作番</param>
    '''<param name="code">コード</param>
    '''<param name="suu">数量</param>
    '''<param name="tempId">检查模板编号</param>
    '''<param name="chkResult">检查结果</param>
    '''<param name="chkUser">检查者</param>
    '''<param name="chkStartDate">检查開始日</param>
    '''<param name="chkEndDate">检查完了日</param>
    '''<param name="parentChkNo">父检查No</param>
    '''<param name="status">状态</param>
    '''<param name="insUser">登録者</param>
    '''<param name="insDate">登録日</param>
    ''' <returns>检查结果Info</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function UpdTCheckResult(ByVal chkNo_key As String,
               ByVal nen_key As String,
               ByVal lineId_key As String,
               ByVal makeNo_key As String,
               ByVal chkNo As String,
               ByVal nen As String,
               ByVal planNo As String,
               ByVal lineId As String,
               ByVal makeNo As String,
               ByVal code As String,
               ByVal suu As String,
               ByVal tempId As String,
               ByVal chkResult As String,
               ByVal chkUser As String,
               ByVal chkStartDate As String,
               ByVal chkEndDate As String,
               ByVal parentChkNo As String,
               ByVal status As String,
               ByVal insUser As String,
               ByVal insDate As String) As Boolean

        'SQLコメント
        '--**テーブル：检查结果 : t_check_result
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("UPDATE t_check_result")
        sb.AppendLine("SET")
        sb.AppendLine("chk_no=@chk_no")                                                '检查No
        sb.AppendLine(", nen=@nen")                                                    '年
        sb.AppendLine(", plan_no=@plan_no")                                            '计划No
        sb.AppendLine(", line_id=@line_id")                                            '生产线
        sb.AppendLine(", make_no=@make_no")                                            '作番
        sb.AppendLine(", code=@code")                                                  'コード
        sb.AppendLine(", suu=@suu")                                                    '数量
        sb.AppendLine(", temp_id=@temp_id")                                            '检查模板编号
        sb.AppendLine(", chk_result=@chk_result")   '检查结果
        sb.AppendLine(", chk_user=@chk_user")   '检查者
        sb.AppendLine(", chk_start_date=@chk_start_date")   '检查開始日
        sb.AppendLine(", chk_end_date=@chk_end_date")   '检查完了日
        sb.AppendLine(", parent_chk_no=@parent_chk_no")   '父检查No
        sb.AppendLine(", status=@status")                                              '状态
        sb.AppendLine(", ins_user=@ins_user")   '登録者
        sb.AppendLine(", ins_date=@ins_date")   '登録日

        sb.AppendLine("FROM t_check_result")
        sb.AppendLine("WHERE 1=1")

        sb.AppendLine("AND chk_no=@chk_no_key")   '检查No

        'If nen_key <> "" Then
        '    sb.AppendLine("AND nen=@nen_key")   '年
        'End If
        If lineId_key <> "" Then
            sb.AppendLine("AND line_id=@line_id_key")   '生产线
        End If
        'If makeNo_key <> "" Then
        '    sb.AppendLine("AND make_no=@make_no_key")   '作番
        'End If

        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@chk_no_key", SqlDbType.VarChar, 20, chkNo_key))
        paramList.Add(SqlHelperNew.MakeParam("@nen_key", SqlDbType.VarChar, 4, nen_key))
        paramList.Add(SqlHelperNew.MakeParam("@line_id_key", SqlDbType.VarChar, 10, lineId_key))
        paramList.Add(SqlHelperNew.MakeParam("@make_no_key", SqlDbType.VarChar, 20, makeNo_key))

        paramList.Add(SqlHelperNew.MakeParam("@chk_no", SqlDbType.VarChar, 20, chkNo))
        paramList.Add(SqlHelperNew.MakeParam("@nen", SqlDbType.VarChar, 4, nen))
        paramList.Add(SqlHelperNew.MakeParam("@plan_no", SqlDbType.VarChar, 20, planNo))
        paramList.Add(SqlHelperNew.MakeParam("@line_id", SqlDbType.VarChar, 10, lineId))
        paramList.Add(SqlHelperNew.MakeParam("@make_no", SqlDbType.VarChar, 20, makeNo))
        paramList.Add(SqlHelperNew.MakeParam("@code", SqlDbType.VarChar, 20, code))
        paramList.Add(SqlHelperNew.MakeParam("@suu", SqlDbType.VarChar, 10, suu))
        paramList.Add(SqlHelperNew.MakeParam("@temp_id", SqlDbType.VarChar, 10, tempId))
        paramList.Add(SqlHelperNew.MakeParam("@chk_result", SqlDbType.VarChar, 1, chkResult))
        paramList.Add(SqlHelperNew.MakeParam("@chk_user", SqlDbType.NVarChar, 20, chkUser))
        paramList.Add(SqlHelperNew.MakeParam("@chk_start_date", SqlDbType.DateTime, 3, IIf(chkStartDate = "", DBNull.Value, chkStartDate)))
        paramList.Add(SqlHelperNew.MakeParam("@chk_end_date", SqlDbType.DateTime, 3, IIf(chkEndDate = "", DBNull.Value, chkEndDate)))
        paramList.Add(SqlHelperNew.MakeParam("@parent_chk_no", SqlDbType.VarChar, 20, parentChkNo))
        paramList.Add(SqlHelperNew.MakeParam("@status", SqlDbType.VarChar, 1, status))
        paramList.Add(SqlHelperNew.MakeParam("@ins_user", SqlDbType.NVarChar, 20, insUser))
        paramList.Add(SqlHelperNew.MakeParam("@ins_date", SqlDbType.DateTime, 3, IIf(insDate = "", DBNull.Value, insDate)))


        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)

        Return True

    End Function

    Public Function DeleteCheckResult(ByVal chkNo_key As String,
           ByVal lineId_key As String,
           ByVal insUser As String) As Boolean

        'SQLコメント
        '--**テーブル：检查结果 : t_check_result
        Dim sb As New StringBuilder
        sb.AppendLine("INSERT INTO t_check_result_rireki")
        sb.AppendLine("SELECT * , '削除','" & insUser & "',getdate() FROM t_check_result")
        sb.AppendLine("WHERE 1=1")
        sb.AppendLine("AND chk_no=@chk_no_key")   '检查No
        sb.AppendLine("AND line_id=@line_id_key")   '生产线

        'SQL文
        sb.AppendLine("DELETE ")
        sb.AppendLine("FROM t_check_result")
        sb.AppendLine("WHERE 1=1")
        sb.AppendLine("AND chk_no=@chk_no_key")   '检查No
        sb.AppendLine("AND line_id=@line_id_key")   '生产线

        'SQL文
        sb.AppendLine("DELETE ")
        sb.AppendLine("FROM t_check_ms")
        sb.AppendLine("WHERE 1=1")
        sb.AppendLine("AND chk_no=@chk_no_key")   '检查No
        sb.AppendLine("AND line_id=@line_id_key")   '生产线

        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@chk_no_key", SqlDbType.VarChar, 20, chkNo_key))
        paramList.Add(SqlHelperNew.MakeParam("@line_id_key", SqlDbType.VarChar, 10, lineId_key))

        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)

        Return True

    End Function

    ''' <summary>
    ''' 
    ''' 检查结果Infoを登録する
    ''' </summary>
    '''<param name="chkNo">检查No</param>
    '''<param name="nen">年</param>
    '''<param name="planNo">计划No</param>
    '''<param name="lineId">生产线</param>
    '''<param name="makeNo">作番</param>
    '''<param name="code">コード</param>
    '''<param name="suu">数量</param>
    '''<param name="tempId">检查模板编号</param>
    '''<param name="chkResult">检查结果</param>
    '''<param name="chkUser">检查者</param>
    '''<param name="chkStartDate">检查開始日</param>
    '''<param name="chkEndDate">检查完了日</param>
    '''<param name="parentChkNo">父检查No</param>
    '''<param name="status">状态</param>
    '''<param name="insUser">登録者</param>
    '''<param name="insDate">登録日</param>
    ''' <returns>检查结果Info</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function InsTCheckResult(ByVal chkNo As String,
               ByVal nen As String,
               ByVal chk_times As String,
               ByVal planNo As String,
               ByVal lineId As String,
               ByVal loginlineId As String,
               ByVal makeNo As String,
               ByVal code As String,
               ByVal suu As String,
               ByVal tempId As String,
               ByVal chkResult As String,
               ByVal chkUser As String,
               ByVal chkYoteiDate As String,
               ByVal chkStartDate As String,
               ByVal chkEndDate As String,
               ByVal parentChkNo As String,
               ByVal status As String,
               ByVal insUser As String,
               ByVal insDate As String) As Boolean

        'SQLコメント
        '--**テーブル：检查结果 : t_check_result
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("INSERT INTO  t_check_result")
        sb.AppendLine("(")
        sb.AppendLine("chk_no")                                                    '检查No
        sb.AppendLine(", nen")                                                     '年
        sb.AppendLine(", chk_times")                                                     'chk_times
        sb.AppendLine(", plan_no")                                                 '计划No
        sb.AppendLine(", line_id")                                                 '生产线
        sb.AppendLine(", make_no")                                                 '作番
        sb.AppendLine(", code")                                                    'コード
        sb.AppendLine(", suu")                                                     '数量
        sb.AppendLine(", temp_id")                                                 '检查模板编号
        sb.AppendLine(", chk_result")                                              '检查结果
        sb.AppendLine(", chk_user")                                                '检查者
        sb.AppendLine(", yotei_chk_date")
        sb.AppendLine(", chk_start_date")   '检查開始日
        sb.AppendLine(", chk_end_date")                                            '检查完了日
        sb.AppendLine(", parent_chk_no")   '父检查No
        sb.AppendLine(", status")                                                  '状态
        sb.AppendLine(", ins_user")                                                '登録者
        sb.AppendLine(", ins_date")                                                '登録日

        sb.AppendLine(")")
        sb.AppendLine("VALUES(")
        sb.AppendLine("@chk_no")                                                       '检查No
        sb.AppendLine(", @nen")                                                        '年
        sb.AppendLine(", " & chk_times & "")                                                        'chk_times
        sb.AppendLine(", @plan_no")                                                    '计划No
        sb.AppendLine(", '" & loginlineId & "'")                                                    '生产线
        sb.AppendLine(", @make_no")                                                    '作番
        sb.AppendLine(", @code")                                                       'コード
        sb.AppendLine(", @suu")                                                        '数量
        sb.AppendLine(", @temp_id")                                                    '检查模板编号
        sb.AppendLine(", @chk_result")                                                 '检查结果
        sb.AppendLine(", @chk_user")                                                   '检查者
        sb.AppendLine(", '" & chkYoteiDate & "'")
        sb.AppendLine(", @chk_start_date")                                             '检查開始日
        sb.AppendLine(", @chk_end_date")                                               '检查完了日
        sb.AppendLine(", @parent_chk_no")                                              '父检查No
        sb.AppendLine(", @status")                                                     '状态
        sb.AppendLine(", @ins_user")                                                   '登録者
        sb.AppendLine(", @ins_date")                                                   '登録日

        sb.AppendLine(")")

        sb.AppendLine("INSERT INTO t_check_result_rireki")
        sb.AppendLine("SELECT * , N'新规检查','" & insUser & "',getdate() FROM t_check_result")
        sb.AppendLine("WHERE 1=1")
        sb.AppendLine("AND chk_no=@chk_no")   '检查No
        sb.AppendLine("AND line_id='" & loginlineId & "'")   '生产线


        'sb.AppendLine("Declare @chk_no varchar(20)")
        'sb.AppendLine("Declare @temp_id varchar(10)")
        'sb.AppendLine("Declare @ins_user varchar(20)")
        'sb.AppendLine("Declare @line_id varchar(20)")
        'sb.AppendLine("set @chk_no = ''")
        'sb.AppendLine("set @temp_id = '312M0001'")
        'sb.AppendLine("set @ins_user = '3003017'")
        'sb.AppendLine("set @line_id = '" & lineId & "'")
        sb.AppendLine("INSERT INTO t_check_ms")
        sb.AppendLine("Select ")
        sb.AppendLine("	@chk_no chk_no")
        sb.AppendLine("	,m_temp.chk_method_id")
        sb.AppendLine("	,'" & loginlineId & "'")
        sb.AppendLine("	,'0' chk_flg")
        sb.AppendLine("	,'' in_1")
        sb.AppendLine("	,'' in_2")
        sb.AppendLine("	,'' chk_result")
        sb.AppendLine("	,'' mark")
        sb.AppendLine("	,m_temp.kj_0")
        sb.AppendLine("	,m_temp.kj_1")
        sb.AppendLine("	,m_temp.kj_2")
        sb.AppendLine("	,m_temp.kj_explain")
        sb.AppendLine("	,@ins_user ins_user")
        sb.AppendLine("	,GETDATE() ins_date")


        sb.AppendLine("	,m_temp.[project_name] ")
        sb.AppendLine("	,m_temp.[pic_id] ")
        sb.AppendLine("	,m_temp.[pic_name] ")
        sb.AppendLine("	,m_temp.[chk_km_name] ")
        sb.AppendLine("	,m_temp.[chk_name] ")
        sb.AppendLine("	,m_temp.[tool_id] ")
        sb.AppendLine("	,m_temp.[pic_sign] ")
        sb.AppendLine("	,m_temp.[chk_id] ")



        sb.AppendLine(",m_check_method.chk_method")
        sb.AppendLine(",m_check_method.chk_formula")
        sb.AppendLine(",ISNULL(m_tools.tool_name,m_check_method.verify_method_explain )")

        sb.AppendLine("FROM m_temp")

        sb.AppendLine("LEFT JOIN m_check_method")
        sb.AppendLine("ON m_temp.chk_id = m_check_method.chk_id")

        sb.AppendLine("LEFT JOIN m_tools")
        sb.AppendLine(" ON m_temp.tool_id = m_tools.tool_id ")
        sb.AppendLine(" AND m_temp.line_id = m_tools.line_id ")


        sb.AppendLine("WHERE m_temp.temp_id = @temp_id")
        sb.AppendLine("AND  m_temp.line_id='" & loginlineId & "'")

        If System.Configuration.ConfigurationManager.AppSettings.Get("biaoqianyizhi").ToString().IndexOf(lineId) >= 0 Then
            sb.AppendLine("INSERT INTO t_check_ms")
            sb.AppendLine("Select ")
            sb.AppendLine("	@chk_no chk_no")
            sb.AppendLine("	,'0000000000'")
            sb.AppendLine("	,'" & loginlineId & "'")
            sb.AppendLine("	,'0' chk_flg")
            sb.AppendLine("	,'' in_1")
            sb.AppendLine("	,'' in_2")
            sb.AppendLine("	,'' chk_result")
            sb.AppendLine("	,'' mark")
            sb.AppendLine("	,N'CD与标签一致'")
            sb.AppendLine("	,''")
            sb.AppendLine("	,''")
            sb.AppendLine("	,N'CD与标签一致'")
            sb.AppendLine("	,@ins_user ins_user")
            sb.AppendLine("	,GETDATE() ins_date")
            sb.AppendLine("	,''")
            sb.AppendLine("	,''")
            sb.AppendLine("	,''")
            sb.AppendLine("	,''")
            sb.AppendLine("	,''")
            sb.AppendLine("	,''")
            sb.AppendLine("	,''")
            sb.AppendLine("	,'0000000000' chk_id ")
            sb.AppendLine(",'1' chk_method")
            sb.AppendLine(",'{biaoqianyizhi}' chk_formula")
            sb.AppendLine("	,N'CD与标签一致'")
        End If


        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@chk_no", SqlDbType.VarChar, 20, chkNo))
        paramList.Add(SqlHelperNew.MakeParam("@nen", SqlDbType.VarChar, 4, nen))
        paramList.Add(SqlHelperNew.MakeParam("@plan_no", SqlDbType.VarChar, 20, planNo))
        paramList.Add(SqlHelperNew.MakeParam("@line_id", SqlDbType.VarChar, 10, lineId))
        paramList.Add(SqlHelperNew.MakeParam("@make_no", SqlDbType.VarChar, 20, makeNo))
        paramList.Add(SqlHelperNew.MakeParam("@code", SqlDbType.VarChar, 20, code))
        paramList.Add(SqlHelperNew.MakeParam("@suu", SqlDbType.VarChar, 10, suu))
        paramList.Add(SqlHelperNew.MakeParam("@temp_id", SqlDbType.VarChar, 10, tempId))
        paramList.Add(SqlHelperNew.MakeParam("@chk_result", SqlDbType.VarChar, 1, chkResult))
        paramList.Add(SqlHelperNew.MakeParam("@chk_user", SqlDbType.NVarChar, 20, chkUser))
        paramList.Add(SqlHelperNew.MakeParam("@chk_start_date", SqlDbType.DateTime, 3, IIf(chkStartDate = "", DBNull.Value, chkStartDate)))
        paramList.Add(SqlHelperNew.MakeParam("@chk_end_date", SqlDbType.DateTime, 3, IIf(chkEndDate = "", DBNull.Value, chkEndDate)))
        paramList.Add(SqlHelperNew.MakeParam("@parent_chk_no", SqlDbType.VarChar, 20, parentChkNo))
        paramList.Add(SqlHelperNew.MakeParam("@status", SqlDbType.VarChar, 1, status))
        paramList.Add(SqlHelperNew.MakeParam("@ins_user", SqlDbType.NVarChar, 20, insUser))
        paramList.Add(SqlHelperNew.MakeParam("@ins_date", SqlDbType.DateTime, 3, IIf(insDate = "", DBNull.Value, insDate)))


        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)

        Return True

    End Function

    ''' <summary>
    ''' 
    ''' 检查结果Infoを削除する
    ''' </summary>
    '''<param name="chkNo_key">检查No</param>
    '''<param name="nen_key">年</param>
    '''<param name="lineId_key">生产线</param>
    '''<param name="makeNo_key">作番</param>
    ''' <returns>检查结果Info</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function DelTCheckResult(ByVal chkNo_key As String,
               ByVal nen_key As String,
               ByVal lineId_key As String,
               ByVal makeNo_key As String) As Boolean

        'SQLコメント
        '--**テーブル：检查结果 : t_check_result
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("DELETE FROM t_check_result")
        sb.AppendLine("WHERE 1=1")
        If chkNo_key <> "" Then
            sb.AppendLine("AND chk_no=@chk_no_key")   '检查No
        End If
        If nen_key <> "" Then
            sb.AppendLine("AND nen=@nen_key")   '年
        End If
        If lineId_key <> "" Then
            sb.AppendLine("AND line_id=@line_id_key")   '生产线
        End If
        If makeNo_key <> "" Then
            sb.AppendLine("AND make_no=@make_no_key")   '作番
        End If

        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@chk_no_key", SqlDbType.VarChar, 20, chkNo_key))
        paramList.Add(SqlHelperNew.MakeParam("@nen_key", SqlDbType.VarChar, 4, nen_key))
        paramList.Add(SqlHelperNew.MakeParam("@line_id_key", SqlDbType.VarChar, 10, lineId_key))
        paramList.Add(SqlHelperNew.MakeParam("@make_no_key", SqlDbType.VarChar, 20, makeNo_key))


        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)

        Return True

    End Function

    ''' <summary>
    ''' GetIntValue
    ''' </summary>
    ''' <param name="v"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetIntValue(ByVal v As Object) As Object

        If v Is DBNull.Value Or v.ToString = "" Then
            Return DBNull.Value

        Else
            Return Convert.ToInt32(v)
        End If

    End Function

    Public Function TestAndShowData(ByVal lineId As String, ByVal startDate As String, ByVal endDate As String, ByVal makeNo As String, ByVal code As String) As String
        Dim sb As New StringBuilder()
        Dim sw As New System.Diagnostics.Stopwatch()

        Try
            ' 1. 执行旧逻辑并计时
            sw.Start()
            Dim dtOld As Data.DataTable = SelTCheckResultBK(lineId, startDate, endDate, makeNo, code)
            sw.Stop()
            Dim oldTime As Long = sw.ElapsedMilliseconds

            ' 2. 执行新逻辑并计时
            sw.Restart()
            Dim dtNew As Data.DataTable = SelTCheckResult(lineId, startDate, endDate, makeNo, code)
            sw.Stop()
            Dim newTime As Long = sw.ElapsedMilliseconds

            ' 3. 构造报告头部
            sb.Append("<h3>对比报告</h3>")
            sb.AppendFormat("<p>旧版耗时：{0} ms | 行数：{1}</p>", oldTime, dtOld.Rows.Count)
            sb.AppendFormat("<p>新版耗时：{0} ms | 行数：{1}</p>", newTime, dtNew.Rows.Count)
            sb.Append("<hr/>")

            ' 4. 生成数据对比表格 (HTML)
            sb.Append("<table border='1' style='border-collapse:collapse; font-size:12px;'>")
            sb.Append("<tr style='background-color:#eee;'><th>来源</th><th>chk_no</th><th>plan_no</th><th>make_no</th><th>status</th><th>chk_start_date</th><th>user_name</th></tr>")

            ' 展现旧版前5条记录 (示例)
            AppendRows(sb, dtOld, "旧版", "#fff4f4")
            ' 分隔线
            sb.Append("<tr style='background-color:#000;'><td colspan='7' style='height:2px;'></td></tr>")
            ' 展现新版前5条记录 (示例)
            AppendRows(sb, dtNew, "新版", "#f4fff4")

            sb.Append("</table>")

            Return sb.ToString()
        Catch ex As Exception
            Return "测试出错：" & ex.Message
        End Try
    End Function

    ' 辅助方法：将 DataTable 的行转为 HTML 表格行
    Private Sub AppendRows(ByRef sb As StringBuilder, ByVal dt As Data.DataTable, ByVal label As String, ByVal bgColor As String)
        ' 为了方便人工对比，我们只取前10条，或者全部取（如果数据量不大的话）
        Dim maxRows As Integer = Math.Min(dt.Rows.Count, 20)
        For i As Integer = 0 To maxRows - 1
            Dim dr As DataRow = dt.Rows(i)
            sb.AppendFormat("<tr style='background-color:{0};'>", bgColor)
            sb.AppendFormat("<td>{0}</td>", label)
            sb.AppendFormat("<td>{0}</td>", dr("chk_no"))
            sb.AppendFormat("<td>{0}</td>", dr("plan_no"))
            sb.AppendFormat("<td>{0}</td>", dr("make_no"))
            sb.AppendFormat("<td>{0}</td>", dr("status"))
            sb.AppendFormat("<td>{0}</td>", dr("chk_start_date"))
            sb.AppendFormat("<td>{0}</td>", dr("user_name"))
            sb.Append("</tr>")
        Next
    End Sub

    Public Function SelTCheckResultBK(ByVal lineId_key As String, ByVal startDate As String, ByVal endDate As String, ByVal make_no As String, ByVal code As String) As Data.DataTable
        'SQLコメント
        '--**テーブル：检查结果 : t_check_result
        Dim sb As New StringBuilder
        'SQL文
        'sb.AppendLine("SELECT a.*,b.user_name,c.ProductCodeSap,c.Package,c.localStorage,c.DestinationCode FROM (")
        sb.AppendLine("SELECT a.*,b.user_name FROM (")

        sb.AppendLine("SELECT ")
        sb.AppendLine("	 t_check_plan.[chk_no]+'_1' as chk_no")
        sb.AppendLine("	,substring( t_check_plan.[yotei_chk_date],1,4) as nen")
        sb.AppendLine("	,ISNULL(t_check_result.[chk_times],0) [chk_times]")
        sb.AppendLine("	,t_check_plan.[plan_no]")
        sb.AppendLine(" ,t_check_plan.[line_id] as [line_id]")
        sb.AppendLine(" ,t_check_plan.[make_no]")
        sb.AppendLine(" ,t_check_plan.[code] as [code]")
        sb.AppendLine(" ,t_check_plan.[suu]")
        sb.AppendLine(" ,t_cd_temp_relation.temp_id ")
        sb.AppendLine(" ,ISNULL(t_check_result.chk_result,'') chk_result")
        sb.AppendLine(" ,ISNULL(t_check_result.chk_user,'') chk_user")
        sb.AppendLine(" ,ISNULL(t_check_result.chk_start_date,NULL) chk_start_date")
        sb.AppendLine(" ,ISNULL(t_check_result.chk_end_date,NULL) chk_end_date")
        sb.AppendLine(" ,ISNULL(t_check_result.parent_chk_no,'') parent_chk_no")
        'sb.AppendLine(" ,ISNULL(t_check_result.[status],'') [status]")
        sb.AppendLine(" ,CASE WHEN ISNULL(t_check_result.[status],'0') = '0' THEN N'待检查'")
        sb.AppendLine("		WHEN ISNULL(t_check_result.[status],'1') = '1' THEN N'临时保存'")
        sb.AppendLine("		WHEN ISNULL(t_check_result.[status],'1') = '2' THEN N'完了'   ")
        sb.AppendLine("		WHEN ISNULL(t_check_result.[status],'1') = '9' THEN N'删除'")
        sb.AppendLine("	   ELSE")
        sb.AppendLine("		''")
        sb.AppendLine("	   END  [status]")

        sb.AppendLine("      ,ISNULL(t_check_result.ins_user,'') ins_user")
        sb.AppendLine("      ,ISNULL(t_check_result.ins_date,NULL) AS ins_date")
        sb.AppendLine("      ,ISNULL(t_check_plan.yotei_chk_date,NULL) AS yotei_chk_date")

        sb.AppendLine("     ,t_check_plan.[xiangxian]")
        sb.AppendLine(" FROM t_check_plan")
        sb.AppendLine(" LEFT JOIN t_cd_temp_relation ")
        sb.AppendLine("	 ON  ")
        sb.AppendLine("      t_check_plan.code = t_cd_temp_relation.code")
        sb.AppendLine("     AND t_cd_temp_relation.line_id  =  '" & lineId_key & "' ")
        sb.AppendLine("  LEFT JOIN t_check_result")
        sb.AppendLine("		ON t_check_plan.plan_no = t_check_result.plan_no ")
        'sb.AppendLine("		AND t_check_plan.line_id  = t_check_result.line_id ")
        'sb.AppendLine("		AND (t_check_result.line_id= '" & lineId_key & "' OR t_check_result.line_id  in(select line_id_saki from m_line_sansyou where line_id_gen = '" & lineId_key & "')) ")
        sb.AppendLine("		AND (t_check_result.line_id= '" & lineId_key & "') ")
        sb.AppendLine("		AND t_check_plan.[chk_no]+'_1' = t_check_result.[chk_no] ")
        'sb.AppendLine("		AND t_check_plan.line_id  =  '" & lineId_key & "' ")
        'sb.AppendLine("		AND (t_check_plan.line_id= '" & lineId_key & "' OR t_check_plan.line_id in(select line_id_saki from m_line_sansyou where line_id_gen = '" & lineId_key & "')) ")

        sb.AppendLine("  WHERE t_check_result.chk_no is null")
        'sb.AppendLine("  AND t_check_plan.line_id= '" & lineId_key & "'")

        sb.AppendLine("  AND (t_check_plan.line_id= '" & lineId_key & "' OR t_check_plan.line_id in(select line_id_saki from m_line_sansyou where line_id_gen = '" & lineId_key & "'))")

        If startDate <> "" Then
            sb.AppendLine("  AND t_check_plan.yotei_chk_date >= '" & startDate & "'")
            sb.AppendLine("  AND t_check_plan.yotei_chk_date <= '" & endDate & "'")
        End If


        If make_no <> "" Then
            sb.AppendLine("	  AND t_check_plan.make_no = '" & make_no & "' ")
        End If

        If code <> "" Then
            sb.AppendLine("	  AND t_check_plan.code = '" & code & "' ")
        End If

        '计划 不在 有检查的计划中
        sb.AppendLine(" AND (t_check_plan.[chk_no]+'_1') not in (")
        sb.AppendLine("     select [chk_no]+'_1' from t_check_plan a  ")
        sb.AppendLine("     where a.line_id= '" & lineId_key & "'")
        sb.AppendLine("")

        If startDate <> "" Then
            sb.AppendLine("     AND a.yotei_chk_date >= '" & startDate & "'")
            sb.AppendLine("     AND a.yotei_chk_date <= '" & endDate & "'")
        End If

        sb.AppendLine("     and EXISTS (select 1 from t_check_result b  ")
        sb.AppendLine("                 where a.plan_no=b.plan_no and a.make_no=b.make_no and a.code = b.code ")
        sb.AppendLine("                         AND b.line_id= '" & lineId_key & "'")
        sb.AppendLine("")
        If startDate <> "" Then
            sb.AppendLine("                     AND b.yotei_chk_date >= '" & startDate & "'")
            sb.AppendLine("                     AND b.yotei_chk_date <= '" & endDate & "'")
        End If
        sb.AppendLine("                 )")

        sb.AppendLine(")")


        'sb.AppendLine(" AND t_check_plan.plan_no not in (select plan_no from t_check_plan a where EXISTS (select 1 from t_check_result b where a.plan_no=b.plan_no ))")



        sb.AppendLine("UNION all ")
        sb.AppendLine("  SELECT ")
        sb.AppendLine("		t_check_result.[chk_no]")
        sb.AppendLine("      ,t_check_result.[nen]")
        sb.AppendLine("      ,t_check_result.[chk_times]")
        sb.AppendLine("      ,t_check_result.[plan_no]")
        sb.AppendLine("      ,t_check_result.[line_id]")
        sb.AppendLine("      ,t_check_result.[make_no]")
        sb.AppendLine("      ,t_check_result.[code]")
        sb.AppendLine("      ,t_check_result.[suu]")
        sb.AppendLine("      ,t_check_result.[temp_id]")
        sb.AppendLine("      ,CASE WHEN t_check_result.[chk_result] = '1' THEN 'OK' WHEN t_check_result.[chk_result] = '9' THEN 'NG' ELSE '' END chk_result")




        sb.AppendLine("      ,t_check_result.[chk_user]")
        sb.AppendLine("      ,t_check_result.[chk_start_date]")
        sb.AppendLine("      ,t_check_result.[chk_end_date]")
        sb.AppendLine("      ,t_check_result.[parent_chk_no]")
        'sb.AppendLine("      ,t_check_result.[status]")
        sb.AppendLine("      ,CASE WHEN ISNULL(t_check_result.[status],'0') = '0' THEN N'检查中'")
        sb.AppendLine("		WHEN ISNULL(t_check_result.[status],'1') = '1' THEN N'临时保存'")
        sb.AppendLine("		WHEN ISNULL(t_check_result.[status],'1') = '2' THEN N'完了'   ")
        sb.AppendLine("		WHEN ISNULL(t_check_result.[status],'1') = '9' THEN N'删除'")
        sb.AppendLine("	   ELSE")
        sb.AppendLine("		''")
        sb.AppendLine("	   END  [status]")

        sb.AppendLine("      ,t_check_result.[ins_user]")
        sb.AppendLine("      ,t_check_result.[ins_date]")
        sb.AppendLine("      ,t_check_result.yotei_chk_date")
        sb.AppendLine("     ,t_check_plan.[xiangxian]")
        sb.AppendLine("   FROM t_check_result")
        sb.AppendLine("  LEFT JOIN t_check_plan")
        sb.AppendLine("		ON t_check_plan.plan_no = t_check_result.plan_no ")
        sb.AppendLine("		AND t_check_plan.[chk_no] +'_1'= t_check_result.[chk_no] ")
        sb.AppendLine("  WHERE 1=1")

        sb.AppendLine("  AND t_check_result.line_id= '" & lineId_key & "'")
        If startDate <> "" Then
            sb.AppendLine("  AND t_check_result.yotei_chk_date >= '" & startDate & "'")
            sb.AppendLine("  AND t_check_result.yotei_chk_date <= '" & endDate & "'")
        End If

        If make_no <> "" Then
            sb.AppendLine("	  AND t_check_result.make_no = '" & make_no & "' ")
        End If
        If code <> "" Then
            sb.AppendLine("	  AND t_check_result.code = '" & code & "' ")
        End If

        sb.AppendLine(") a")
        sb.AppendLine("left join m_user b on a.chk_user = b.user_cd")
        'sb.AppendLine("left join TCM_BianPlan c on c.ZuoFan = a.make_no COLLATE japanese_xjis_100_bin2")

        sb.AppendLine("  ORDER BY yotei_chk_date DESC, chk_no,chk_times")

        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "t_check_result")

        Return dsInfo.Tables("t_check_result")

    End Function


    Public Function SelTCheckResult(ByVal lineId_key As String, ByVal startDate As String, ByVal endDate As String, ByVal make_no As String, ByVal code As String) As Data.DataTable
        ' 1. 获取待检查的计划
        Dim dtPlans As Data.DataTable = GetPendingPlans(lineId_key, startDate, endDate, make_no, code)

        ' 2. 获取已有的检查结果
        Dim dtResults As Data.DataTable = GetCheckResults(lineId_key, startDate, endDate, make_no, code)

        ' 3. 合并 DataTable
        ' 这样会将 dtResults 的行导入到 dtPlans 中
        dtPlans.Merge(dtResults)

        ' 4. 排序 (DataTable 级别排序)
        ' 修改主函数最后的返回逻辑
        Dim dv As DataView = dtPlans.DefaultView
        dv.Sort = "yotei_chk_date DESC, chk_no ASC, chk_times ASC"

        ' ToTable 的第一个参数设为 True，表示 DISTINCT（去重）
        ' 第二个参数传入所有列名，表示整行对比去重
        Return dv.ToTable(True, "chk_no", "nen", "chk_times", "plan_no", "line_id", "make_no", "code", "suu", "temp_id", "chk_result", "chk_user", "chk_start_date", "chk_end_date", "parent_chk_no", "status", "ins_user", "ins_date", "yotei_chk_date", "xiangxian", "user_name")

        'Return dv.ToTable()
    End Function

    Private Function GetSansyouLines(ByVal lineId As String) As String

        Dim sb1 As New StringBuilder
        sb1.AppendLine("SELECT line_id_saki FROM m_line_sansyou WHERE line_id_gen = '" & lineId & "'")
        Dim ds As New DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb1.ToString(), ds, "t_check_result")

        If ds.Tables(0).Rows.Count = 0 Then
            Return ""
        Else
            ' 2. 遍历 DataTable 拼接字符串
            Dim lineList As New List(Of String)
            For Each row As DataRow In ds.Tables(0).Rows
                ' 加上单引号，并处理空值情况
                Dim sakiId As String = row("line_id_saki").ToString().Replace("'", "''") ' 简单的转义处理
                lineList.Add("'" & sakiId & "'")
            Next

            ' 3. 使用 Join 函数用逗号连接
            Return String.Join(",", lineList.ToArray())

        End If

    End Function


    Private Function GetPendingPlans(ByVal lineId As String, ByVal sDate As String, ByVal eDate As String, ByVal makeNo As String, ByVal code As String) As Data.DataTable


        Dim SansyouLinesStr As String = GetSansyouLines(lineId)


        Dim sb As New StringBuilder

        sb.AppendLine("SELECT ")
        ' 基础信息
        sb.AppendLine("    p.[chk_no] + '_1' AS chk_no, ")
        sb.AppendLine("    SUBSTRING(p.[yotei_chk_date], 1, 4) AS nen, ")
        sb.AppendLine("    0 AS chk_times, ")
        sb.AppendLine("    p.[plan_no], p.[line_id], p.[make_no], p.[code], p.[suu], ")
        sb.AppendLine("    r_rel.temp_id, ")

        ' --- 关键修正：显式转换数据类型以匹配 t_check_result 的定义 ---
        sb.AppendLine("    CAST('' AS NVARCHAR(50)) AS chk_result, ")
        sb.AppendLine("    CAST('' AS NVARCHAR(50)) AS chk_user, ")
        sb.AppendLine("    CAST(NULL AS DATETIME) AS chk_start_date, ") ' 必须是 DATETIME
        sb.AppendLine("    CAST(NULL AS DATETIME) AS chk_end_date, ")   ' 必须是 DATETIME
        sb.AppendLine("    CAST('' AS NVARCHAR(50)) AS parent_chk_no, ")
        sb.AppendLine("    N'待检查' AS [status], ")
        sb.AppendLine("    CAST('' AS NVARCHAR(50)) AS ins_user, ")
        sb.AppendLine("    CAST(NULL AS DATETIME) AS ins_date, ")       ' 必须是 DATETIME
        ' ---------------------------------------------------------

        sb.AppendLine("    p.yotei_chk_date, ")
        sb.AppendLine("    p.[xiangxian], ")
        sb.AppendLine("    CAST('' AS NVARCHAR(50)) AS user_name ") ' 对应 m_user.user_name

        sb.AppendLine("FROM t_check_plan p")
        sb.AppendLine("LEFT JOIN t_cd_temp_relation r_rel ON p.code = r_rel.code AND r_rel.line_id = '" & lineId & "'")
        sb.AppendLine("LEFT JOIN t_check_result res ON p.plan_no = res.plan_no AND p.[chk_no] + '_1' = res.[chk_no] AND res.line_id = '" & lineId & "'")
        sb.AppendLine("WHERE res.chk_no IS NULL")
        'sb.AppendLine("  AND (p.line_id = '" & lineId & "' OR p.line_id IN (SELECT line_id_saki FROM m_line_sansyou WHERE line_id_gen = '" & lineId & "'))")
        If SansyouLinesStr = "" Then
            sb.AppendLine("  AND p.line_id = '" & lineId & "'")
        Else
            sb.AppendLine("  AND p.line_id in( '" & lineId & "'," & SansyouLinesStr & " )")
        End If

        ' 动态过滤条件
        If Not String.IsNullOrEmpty(sDate) Then
            sb.AppendLine(String.Format(" AND p.yotei_chk_date >= '{0}' AND p.yotei_chk_date <= '{1}' ", sDate, eDate))
        End If
        If Not String.IsNullOrEmpty(makeNo) Then
            sb.AppendLine(String.Format(" AND p.make_no = '{0}' ", makeNo))
        End If
        If Not String.IsNullOrEmpty(code) Then
            sb.AppendLine(String.Format(" AND p.code = '{0}' ", code))
        End If


        sb.AppendLine("  AND NOT EXISTS (")
        sb.AppendLine("      SELECT 1 FROM t_check_result b ")
        sb.AppendLine("      WHERE b.plan_no = p.plan_no ")
        sb.AppendLine("        AND b.make_no = p.make_no ")
        sb.AppendLine("        AND b.code = p.code ")
        sb.AppendLine("        AND b.line_id = '" & lineId & "'")
        ' 如果需要支持周期性重复，才加这一句：
        'If Not String.IsNullOrEmpty(sDate) Then
        '    sb.AppendLine(String.Format(" AND b.chk_date >= '{0}' AND b.chk_date <= '{1}' ", sDate, eDate))
        'End If

        ' 
        sb.AppendLine("  )")




        '' 排除逻辑 (逻辑同原 SQL)
        'sb.AppendLine(" AND (p.[chk_no] + '_1') NOT IN (")
        'sb.AppendLine("     SELECT a.[chk_no] + '_1' FROM t_check_plan a ")
        'sb.AppendLine("     WHERE a.line_id = '" & lineId & "'")
        'If Not String.IsNullOrEmpty(sDate) Then
        '    sb.AppendLine(String.Format(" AND a.yotei_chk_date >= '{0}' AND a.yotei_chk_date <= '{1}' ", sDate, eDate))
        'End If
        'sb.AppendLine("     AND EXISTS (SELECT 1 FROM t_check_result b WHERE a.plan_no = b.plan_no AND a.make_no = b.make_no AND a.code = b.code AND b.line_id = '" & lineId & "')")
        'sb.AppendLine(" )")

        Dim ds As New DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), ds, "t_check_result")
        Return ds.Tables(0)
    End Function


    Private Function GetCheckResults(lineId As String, sDate As String, eDate As String, makeNo As String, code As String) As Data.DataTable
        Dim sb As New StringBuilder
        sb.AppendLine("SELECT ")
        sb.AppendLine("    r.[chk_no], r.[nen], r.[chk_times], r.[plan_no], r.[line_id], r.[make_no], r.[code], r.[suu], r.[temp_id],")
        sb.AppendLine("    CASE r.[chk_result] WHEN '1' THEN 'OK' WHEN '9' THEN 'NG' ELSE '' END AS chk_result,")
        sb.AppendLine("    r.[chk_user], r.[chk_start_date], r.[chk_end_date], r.[parent_chk_no],")
        sb.AppendLine("    CASE ISNULL(r.[status],'0') WHEN '0' THEN N'检查中' WHEN '1' THEN N'临时保存' WHEN '2' THEN N'完了' WHEN '9' THEN N'删除' ELSE '' END AS [status],")
        sb.AppendLine("    r.[ins_user], r.[ins_date], r.yotei_chk_date, p.[xiangxian], b.user_name")
        sb.AppendLine("FROM t_check_result r")
        sb.AppendLine("LEFT JOIN t_check_plan p ON p.plan_no = r.plan_no AND p.[chk_no] + '_1' = r.[chk_no]")
        sb.AppendLine("LEFT JOIN m_user b ON r.chk_user = b.user_cd")
        sb.AppendLine("WHERE r.line_id = '" & lineId & "'")

        If sDate <> "" Then
            sb.AppendLine(String.Format(" AND r.yotei_chk_date >= '{0}' AND r.yotei_chk_date <= '{1}' ", sDate, eDate))
        End If
        If makeNo <> "" Then
            sb.AppendLine(String.Format(" AND r.make_no = '{0}' ", makeNo))
        End If
        If code <> "" Then
            sb.AppendLine(String.Format(" AND r.code = '{0}' ", code))
        End If

        Dim ds As New DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), ds, "t_check_result")
        Return ds.Tables(0)
    End Function




    Public Function InsBaogongRireki(ByVal chkNo As String,
               ByVal makeNo As String,
               ByVal cd As String,
               ByVal line As String,
               ByVal txt As String) As Boolean

        'SQLコメント
        '--**テーブル：检查结果 : t_check_result
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("INSERT INTO [m_baogong_rireki]([chk_no] ,[make_no],[cd],[line],[txt],upd_date)VALUES")
        sb.AppendLine("(")

        sb.AppendLine("'" & chkNo & "'")
        sb.AppendLine(",'" & makeNo & "'")
        sb.AppendLine(",'" & cd & "'")
        sb.AppendLine(",'" & line & "'")
        sb.AppendLine(",N'" & txt & "'")
        sb.AppendLine(",getdate()")
        sb.AppendLine(")")
        'PARAM
        Dim paramList As New List(Of SqlParameter)

        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)

        Return True

    End Function


    Public Function GetResultByChkNo(ByVal chk_id As String) As DataTable

        'SQLコメント
        '--**テーブル：检查结果 : t_check_ms
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("SELECT top 1 case when chk_result='1' then 'OK' when chk_result='9' then 'NG' ELSE chk_result END  as result ")
        sb.AppendLine("FROM t_check_result")
        sb.AppendLine("WHERE")
        sb.AppendLine("chk_no='" & chk_id & "'")   '检查No

        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "GetResultByChkNo")

        Return dsInfo.Tables("GetResultByChkNo")

    End Function


    ''' <summary>
    ''' Send 影像联动
    ''' </summary>
    Public Function SendYXLD(ByVal kbn As String, ByVal no As String, ByVal cnt As String, ByVal cd As String, ByVal lr As String, ByVal dw As String, ByVal dh As String) As Boolean
        Dim paramList As New List(Of SqlParameter)
        Dim result As String = ""
        'SQLコメント
        '--**テーブル：检查结果 : t_check_ms
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("DELETE FROM m_yxld WHERE")
        sb.AppendLine("[kbn]='" & kbn & "'")
        sb.AppendLine("AND [make_no]='" & no & "'")
        sb.AppendLine("AND [cnt]='" & cnt & "'")

        sb.AppendLine("INSERT INTO m_yxld")
        sb.AppendLine("SELECT ")
        sb.AppendLine("'" & kbn & "'")
        sb.AppendLine(",'" & no & "'")
        sb.AppendLine(",'" & cnt & "'")
        sb.AppendLine(",'" & cd & "'")
        sb.AppendLine(",'" & lr & "'")
        sb.AppendLine(",'" & dw & "'")
        sb.AppendLine(",'" & dh & "'")
        sb.AppendLine(",''")
        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)
        Return True

    End Function

    Public Function GetYXLD(ByVal no As String, ByVal cnt As String) As DataTable

        Dim result As String = ""
        'SQLコメント
        '--**テーブル：检查结果 : t_check_ms
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("SELECT * FROM m_yxld WHERE")
        'sb.AppendLine("[kbn]='" & kbn & "'")
        sb.AppendLine("[make_no]='" & no & "'")
        sb.AppendLine("AND [cnt]='" & cnt & "'")
        sb.AppendLine("AND isnull([result],'')<>''")

        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "GetYXLD")
        Return dsInfo.Tables(0)

    End Function


    Public Function GetYXLDResult(ByVal no As String) As Boolean


        Dim result As String = ""
        'SQLコメント
        '--**テーブル：检查结果 : t_check_ms
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("SELECT * FROM m_yxld WHERE")
        'sb.AppendLine("[kbn]='" & kbn & "'")
        sb.AppendLine("[make_no]='" & no & "'")


        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "GetYXLD")
        Return dsInfo.Tables(0).Rows.Count > 0

    End Function


    Public Function UpdYXLD(ByVal no As String, ByVal cnt As String, ByVal result As String) As Boolean
        Dim paramList As New List(Of SqlParameter)

        'SQLコメント
        '--**テーブル：检查结果 : t_check_ms
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("UPDATE m_yxld SET result = '" & result & "' WHERE ")
        sb.AppendLine("[make_no]='" & no & "'")
        sb.AppendLine("AND [cnt]='" & cnt & "'")

        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)
        Return True

    End Function

    Public Function InsYXLDLog(ByVal code As String, ByVal kbn As String) As Boolean
        Dim paramList As New List(Of SqlParameter)
        Dim result As String = ""
        'SQLコメント
        '--**テーブル：检查结果 : t_check_ms
        Dim sb As New StringBuilder

        sb.AppendLine("INSERT INTO m_yxld_log")
        sb.AppendLine("SELECT ")
        sb.AppendLine("getdate()")
        sb.AppendLine(",'" & code & "'")
        sb.AppendLine(",'" & kbn & "'")
        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)
        Return True

    End Function


    Public Function GetCdColor(ByVal cd As String) As String
        Dim result As String = ""
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("SELECT * FROM m_code_color WHERE")
        sb.AppendLine("[code]='" & cd & "'")


        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "GetYXLD")
        If dsInfo.Tables(0).Rows.Count > 0 Then
            Return dsInfo.Tables(0).Rows(0).Item("color").ToString & "|" & dsInfo.Tables(0).Rows(0).Item("mark").ToString
        Else
            Return ""
        End If

    End Function



End Class
