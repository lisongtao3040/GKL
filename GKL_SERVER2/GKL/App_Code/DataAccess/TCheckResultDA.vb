



Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Transactions
Imports System.Configuration.ConfigurationSettings
Imports System.Collections.Generic

Public Class TCheckResultDA
    Public SqlHelperNew As New SqlHelperNew

    ''' <summary>
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
    ''' 检查一览取得
    ''' </summary>
    ''' <param name="lineId_key"></param>
    ''' <param name="startDate"></param>
    ''' <param name="endDate"></param>
    ''' <param name="make_no"></param>
    ''' <param name="code"></param>
    ''' <param name="isAllLine"></param>
    ''' <returns></returns>
    Public Function SelTCheckResult(ByVal lineId_key As String, ByVal startDate As String, ByVal endDate As String, ByVal make_no As String, ByVal code As String, ByVal isAllLine As Boolean) As Data.DataTable


        'SQLコメント
        '--**テーブル：检查结果 : t_check_result
        Dim sb As New StringBuilder
        'SQL文
        'sb.AppendLine("SELECT a.*,b.user_name,c.ProductCodeSap,c.Package,c.localStorage,c.DestinationCode FROM (")
        sb.AppendLine("SELECT a.*,b.user_name,isnull(c.qianpin_suu,0) qianpin_suu FROM (")

        sb.AppendLine("     SELECT ")
        sb.AppendLine("            t_check_plan.[chk_no]+'_1' as chk_no")
        sb.AppendLine("           ,substring( t_check_plan.[yotei_chk_date],1,4) as nen")
        sb.AppendLine("           ,ISNULL(t_check_result.[chk_times],0) [chk_times]")
        sb.AppendLine("           ,t_check_plan.[plan_no]")
        sb.AppendLine("           ,t_check_plan.[line_id] as [line_id]")
        sb.AppendLine("           ,t_check_plan.[make_no]")
        sb.AppendLine("           ,t_check_plan.[code] as [code]")
        sb.AppendLine("           ,t_check_plan.[suu]")
        sb.AppendLine("           ,t_cd_temp_relation.temp_id ")
        sb.AppendLine("           ,ISNULL(t_check_result.chk_result,'') chk_result")
        sb.AppendLine("           ,ISNULL(t_check_result.chk_user,'') chk_user")
        sb.AppendLine("           ,ISNULL(t_check_result.chk_start_date,NULL) chk_start_date")
        sb.AppendLine("           ,ISNULL(t_check_result.chk_end_date,NULL) chk_end_date")
        sb.AppendLine("           ,ISNULL(t_check_result.parent_chk_no,'') parent_chk_no")
        sb.AppendLine("           ,CASE WHEN ISNULL(t_check_result.[status],'0') = '0' THEN N'待检查'")
        sb.AppendLine("          		WHEN ISNULL(t_check_result.[status],'1') = '1' THEN N'临时保存'")
        sb.AppendLine("          		WHEN ISNULL(t_check_result.[status],'1') = '2' THEN N'完了'   ")
        sb.AppendLine("          		WHEN ISNULL(t_check_result.[status],'1') = '9' THEN N'删除'")
        sb.AppendLine("          	   ELSE")
        sb.AppendLine("          		''")
        sb.AppendLine("          	   END  [status]")
        sb.AppendLine("           ,ISNULL(t_check_result.ins_user,'') ins_user")
        sb.AppendLine("           ,ISNULL(t_check_result.ins_date,NULL) AS ins_date")
        sb.AppendLine("           ,ISNULL(t_check_plan.yotei_chk_date,NULL) AS yotei_chk_date")
        sb.AppendLine("          ,t_check_plan.[xiangxian]")
        If isAllLine Then
            sb.AppendLine("  FROM v_plan_code_grp t_check_plan")
        Else
            sb.AppendLine("  FROM t_check_plan")
        End If

        If isAllLine Then
            'sb.AppendLine("     LEFT JOIN (SELECT [code],max([temp_id]) as temp_id FROM [GKL].[dbo].[t_cd_temp_relation] group by [code]) t_cd_temp_relation")
            'sb.AppendLine("	    ON  ")
            'sb.AppendLine("         t_check_plan.code = t_cd_temp_relation.code")


            sb.AppendLine("     LEFT JOIN t_cd_temp_relation ")
            sb.AppendLine("	    ON  ")
            sb.AppendLine("         t_check_plan.code = t_cd_temp_relation.code")
            sb.AppendLine("     AND t_cd_temp_relation.line_id  =  '" & lineId_key & "' ")
        Else
            sb.AppendLine("     LEFT JOIN t_cd_temp_relation ")
            sb.AppendLine("	    ON  ")
            sb.AppendLine("         t_check_plan.code = t_cd_temp_relation.code")
            sb.AppendLine("     AND t_cd_temp_relation.line_id  =  '" & lineId_key & "' ")
        End If

        sb.AppendLine("       LEFT JOIN t_check_result")
        sb.AppendLine("	     	ON t_check_plan.plan_no = t_check_result.plan_no ")
        sb.AppendLine("	     	AND (t_check_result.line_id= '" & lineId_key & "') ")
        sb.AppendLine("	     	AND t_check_plan.[chk_no]+'_1' = t_check_result.[chk_no] ")
        sb.AppendLine("       WHERE t_check_result.chk_no is null")

        If isAllLine Then
            sb.AppendLine("         AND (t_check_plan.line_id = 'alllines')")
        Else
            sb.AppendLine("         AND (t_check_plan.line_id = '" & lineId_key & "' OR t_check_plan.line_id in(select line_id_saki from m_line_sansyou where line_id_gen = '" & lineId_key & "'))")
        End If

        If startDate <> "" Then
            sb.AppendLine("         AND t_check_plan.yotei_chk_date >= '" & startDate & "'")
            sb.AppendLine("         AND t_check_plan.yotei_chk_date <= '" & endDate & "'")
        End If

        If make_no <> "" Then
            sb.AppendLine("	        AND t_check_plan.make_no = '" & make_no & "' ")
        End If

        If code <> "" Then
            sb.AppendLine("	        AND t_check_plan.code = '" & code & "' ")
        End If

        '计划 不在 有检查的计划中
        sb.AppendLine("             AND (t_check_plan.[chk_no]+'_1') not in (")
        sb.AppendLine("                  select [chk_no]+'_1' from t_check_plan a  ")
        sb.AppendLine("                  where a.line_id= '" & lineId_key & "'")
        If startDate <> "" Then
            sb.AppendLine("                 AND a.yotei_chk_date >= '" & startDate & "'")
            sb.AppendLine("                 AND a.yotei_chk_date <= '" & endDate & "'")
        End If

        sb.AppendLine("                     and EXISTS (select 1 from t_check_result b  ")
        sb.AppendLine("                                 where a.plan_no=b.plan_no and a.make_no=b.make_no and a.code = b.code ")
        sb.AppendLine("                                     AND b.line_id= '" & lineId_key & "'")
        If startDate <> "" Then
            sb.AppendLine("                                 AND b.yotei_chk_date >= '" & startDate & "'")
            sb.AppendLine("                                 AND b.yotei_chk_date <= '" & endDate & "'")
        End If
        sb.AppendLine("                                 )")
        sb.AppendLine(")")

        sb.AppendLine("     UNION ")
        sb.AppendLine("  SELECT ")
        sb.AppendLine("	    	  t_check_result.[chk_no]")
        sb.AppendLine("          ,t_check_result.[nen]")
        sb.AppendLine("          ,t_check_result.[chk_times]")
        sb.AppendLine("          ,t_check_result.[plan_no]")
        sb.AppendLine("          ,t_check_result.[line_id]")
        sb.AppendLine("          ,t_check_result.[make_no]")
        sb.AppendLine("          ,t_check_result.[code]")
        sb.AppendLine("          ,t_check_result.[suu]")
        sb.AppendLine("          ,t_check_result.[temp_id]")
        sb.AppendLine("          ,CASE WHEN t_check_result.[chk_result] = '1' THEN 'OK' WHEN t_check_result.[chk_result] = '9' THEN 'NG' ELSE '' END chk_result")
        sb.AppendLine("          ,t_check_result.[chk_user]")
        sb.AppendLine("          ,t_check_result.[chk_start_date]")
        sb.AppendLine("          ,t_check_result.[chk_end_date]")
        sb.AppendLine("          ,t_check_result.[parent_chk_no]")
        sb.AppendLine("          ,CASE WHEN ISNULL(t_check_result.[status],'0') = '0' THEN N'检查中'")
        sb.AppendLine("	    	 WHEN ISNULL(t_check_result.[status],'1') = '1' THEN N'临时保存'")
        sb.AppendLine("	    	 WHEN ISNULL(t_check_result.[status],'1') = '2' THEN N'完了'   ")
        sb.AppendLine("	    	 WHEN ISNULL(t_check_result.[status],'1') = '9' THEN N'删除'")
        sb.AppendLine("	            ELSE")
        sb.AppendLine("	    	        ''")
        sb.AppendLine("	            END  [status]")
        sb.AppendLine("          ,t_check_result.[ins_user]")
        sb.AppendLine("          ,t_check_result.[ins_date]")
        sb.AppendLine("          ,t_check_result.yotei_chk_date")
        sb.AppendLine("          ,t_check_plan.[xiangxian]")
        sb.AppendLine("   FROM t_check_result")
        If isAllLine Then
            sb.AppendLine("  LEFT JOIN v_plan_code_grp t_check_plan")
        Else
            sb.AppendLine("  LEFT JOIN t_check_plan")
        End If
        sb.AppendLine("		ON t_check_plan.plan_no = t_check_result.plan_no ")
        sb.AppendLine("		AND t_check_plan.[chk_no] +'_1'= t_check_result.[chk_no] ")
        sb.AppendLine("  WHERE 1=1")
        sb.AppendLine("        AND t_check_result.line_id= '" & lineId_key & "'")
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

        sb.AppendLine("left join t_qianpin c on c.chk_no = a.chk_no")

        sb.AppendLine("  ORDER BY yotei_chk_date DESC, chk_no,chk_times")

        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "t_check_result")

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
               ByVal insDate As String, ByVal isAllLine As Boolean) As Boolean

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

        If isAllLine Then
        Else
            sb.AppendLine("AND  m_temp.line_id='" & loginlineId & "'")
        End If


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



    Public Function GetQianpinCnt(ByVal chkNo_key As String) As Integer
        'SQLコメント
        '--**テーブル：检查结果 : t_check_result
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("SELECT")
        sb.AppendLine("qianpin_suu")                                                    '检查No
        sb.AppendLine("FROM t_qianpin")
        sb.AppendLine("WHERE 1=1")
        sb.AppendLine("AND chk_no='" & chkNo_key & "'")
        Dim paramList As New List(Of SqlParameter)
        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "t_qianpin", paramList.ToArray)
        Try
            If dsInfo.Tables(0).Rows.Count > 0 Then
                Return CInt(dsInfo.Tables(0).Rows(0).Item(0).ToString)
            Else
                Return 0
            End If
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Function SetQianpinCnt(ByVal chkNo_key As String, ByVal suu As String) As Integer
        'SQLコメント
        '--**テーブル：检查结果 : t_check_result
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("DELETE")
        sb.AppendLine("FROM t_qianpin")
        sb.AppendLine("WHERE 1=1")
        sb.AppendLine("AND chk_no='" & chkNo_key & "'")
        sb.AppendLine("INSERT INTO t_qianpin SELECT '" & chkNo_key & "'," & suu & "")
        Dim paramList As New List(Of SqlParameter)
        Dim dsInfo As New Data.DataSet
        Return SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)


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



End Class
