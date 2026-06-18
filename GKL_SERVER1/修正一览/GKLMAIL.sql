/****** SSMS の SelectTopNRows コマンドのスクリプト  ******/
SELECT TOP 1000 [xi]
      ,[line_id]
      ,[to_email]
      ,[cc_email]
      ,[send_email_time]
      ,[qidong]
  FROM [GKL].[dbo].[m_email_kanri];

SELECT 
--count(*)
a.[make_no],a.[code],a.[yotei_chk_date],
--ISNULL(b_6.chk_times,ISNULL(b_5.chk_times,ISNULL(b_4.chk_times,ISNULL(b_3.chk_times,ISNULL(b_2.chk_times,ISNULL(b_1.chk_times,0)))))),
CASE WHEN b_1.plan_no is     null                                                THEN N'一次检查也没有'
	 WHEN b_1.plan_no is not null AND b_2.plan_no is null AND b_1.[status]<>'2'  THEN N'只有一次检查且是检查中'
	 --WHEN (b_1.chk_result = '9' OR b_2.chk_result = '9' OR b_3.chk_result = '9' OR b_4.chk_result = '9' OR b_5.chk_result = '9' OR b_6.chk_result = '9')
	 --																		 THEN N'检查结果NG'
ELSE
''
END

--b_1.plan_no,
--b_2.plan_no,
--b_1.[status]

FROM       t_check_plan   a
INNER JOIN [t_cd_temp_relation] t_cd
	ON  a.code     =   t_cd.code
	AND a.line_id like '%'+t_cd.line_id 
LEFT JOIN t_check_result b_1
	ON  a.plan_no       = b_1.plan_no
	AND a.[chk_no]+'_1' = b_1.[chk_no]
LEFT JOIN t_check_result b_2
	ON  a.plan_no       = b_2.plan_no
	AND a.[chk_no]+'_2' = b_2.[chk_no]
/*
LEFT JOIN t_check_result b_3
	ON  a.plan_no       = b_3.plan_no
	AND a.[chk_no]+'_3' = b_3.[chk_no]
LEFT JOIN t_check_result b_4
	ON  a.plan_no       = b_4.plan_no
	AND a.[chk_no]+'_4' = b_4.[chk_no]
LEFT JOIN t_check_result b_5
	ON  a.plan_no       = b_5.plan_no
	AND a.[chk_no]+'_5' = b_5.[chk_no]
LEFT JOIN t_check_result b_6
	ON  a.plan_no       = b_6.plan_no
	AND a.[chk_no]+'_6' = b_6.[chk_no]
	*/
WHERE
	a.yotei_chk_date between dateadd(day,-11,getdate()) and dateadd(day,-1,getdate())
	AND ISNULL(t_cd.temp_id,'')<>''
	AND (
		    (b_1.plan_no is null)
		 OR (b_1.plan_no is not null AND b_2.plan_no is null AND b_1.[status]<>'2')
		 --OR (b_1.chk_result = '9' OR b_2.chk_result = '9' OR b_3.chk_result = '9' OR b_4.chk_result = '9' OR b_5.chk_result = '9' OR b_6.chk_result = '9')

	)

UNION

SELECT
	a.[make_no],a.[code],a.[yotei_chk_date],N'检查结果NG'
FROM       t_check_result   a
INNER JOIN [t_cd_temp_relation] t_cd
	ON  a.code     =   t_cd.code
	AND a.line_id like '%'+t_cd.line_id 
WHERE a.chk_result = '9'
AND
	a.yotei_chk_date between dateadd(day,-11,getdate()) and dateadd(day,-1,getdate())
	OR 
	a.chk_end_date between CAST(CAST(dateadd(day,-1,GETDATE()) AS DATE) AS DATETIME) + CAST('08:00:00' AS DATETIME) and CAST(CAST(GETDATE() AS DATE) AS DATETIME) + CAST('08:00:00' AS DATETIME)

--220329_9008600976_1