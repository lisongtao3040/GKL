
Partial Class Default3
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        SelTCheckResult("922", "2026/01/15", "2026/01/15", "", "")


    End Sub






    Public Function SelTCheckResult(ByVal lineId_key As String, ByVal startDate As String, ByVal endDate As String, ByVal make_no As String, ByVal code As String) As Data.DataTable

        Dim isAllLine As Boolean = True

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
        If isAllLine Then
            sb.AppendLine("  FROM v_plan_code_grp t_check_plan")
        Else
            sb.AppendLine("  FROM t_check_plan")
        End If
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



        sb.AppendLine("UNION ")
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
        If isAllLine Then
            sb.AppendLine("  LEFT JOIN v_plan_code_grp t_check_plan")
        Else
            sb.AppendLine("  LEFT JOIN t_check_plan")
        End If
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

        Response.Write(sb.ToString
                 )

    End Function
End Class
