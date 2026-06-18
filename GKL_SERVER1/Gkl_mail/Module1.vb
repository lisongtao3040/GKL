Imports System.Configuration
Imports System.Threading
Imports System.Text
Imports Microsoft.VisualBasic
Imports System.Net
Imports System.Net.Mail
Imports System.IO
Imports System.Deployment

Module Module1

    Private conn As String = ConfigurationManager.AppSettings("connectionString").ToString()

    Private logPath As String = ConfigurationManager.AppSettings("logPath").ToString()

    Public SqlHelperNew As New SqlHelperNew

    Sub Main()
        ChkMail()
    End Sub

    Sub WriteLog(filePath As String, message As String)
        Using writer As New StreamWriter(filePath, True)
            writer.WriteLine($"{DateTime.Now.ToString()} - {message}")
        End Using
    End Sub


    Sub WriteLog(message As String)
        Try
            Dim filePath As String = logPath & DateTime.Now.ToString("yyyyMMdd") & "_log.log"

            'Dim exeFolderPath As String = Path.GetDirectoryName(Application.ExecutablePath)

            Using writer As New StreamWriter(filePath, True)
                writer.WriteLine($"{DateTime.Now.ToString()} - {message}")
            End Using
        Catch ex As Exception

        End Try

    End Sub


    Private Function ChkMail()

        'SQLコメント
        '--**テーブル：用户MS : m_user
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM [m_email_kanri]")
        sb.AppendLine("WHERE 1=1 and isnull(xi,'')<>''")
        sb.AppendLine("ORDER BY xi")

        Dim ds As New DataSet
        SqlHelperNew.FillDataset(conn, CommandType.Text, sb.ToString, ds, "temp")
        WriteLog("Start　SendMail:" & Now.ToString("yyyy-MM-dd HH:mm"))

        Dim old_xi As String = ""
        Dim mail_to As String = ""
        Dim mail_cc As String = ""

        If ds.Tables(0).Rows.Count > 0 Then
            old_xi = NullToEmpty(ds.Tables(0).Rows(0).Item("xi"))
            mail_to = NullToEmpty(ds.Tables(0).Rows(0).Item("to_email"))
            mail_cc = NullToEmpty(ds.Tables(0).Rows(0).Item("cc_email"))
        Else
            Return ""
        End If



        For i As Integer = 1 To ds.Tables(0).Rows.Count - 1

            'If ds.Tables(0).Rows(i).Item("xi") IsNot DBNull.Value AndAlso ds.Tables(0).Rows(i).Item("xi").ToString.Trim <> "" Then
            'Or i = ds.Tables(0).Rows.Count - 1

            If old_xi <> NullToEmpty(ds.Tables(0).Rows(i).Item("xi")) Then
                'send mail
                Try
                    WriteLog("SendMail:" & ds.Tables(0).Rows(i).Item("line_id"))
                    SendMail(old_xi, mail_to, mail_cc)
                Catch ex As Exception
                    WriteLog(ex.Message)
                End Try

                old_xi = NullToEmpty(ds.Tables(0).Rows(i).Item("xi"))
                mail_to = NullToEmpty(ds.Tables(0).Rows(i).Item("to_email"))
                mail_cc = NullToEmpty(ds.Tables(0).Rows(i).Item("cc_email"))

            Else
                mail_to = mail_to & ";" & NullToEmpty(ds.Tables(0).Rows(i).Item("to_email"))
                mail_cc = mail_cc & ";" & NullToEmpty(ds.Tables(0).Rows(i).Item("cc_email"))

            End If


            If i = ds.Tables(0).Rows.Count - 1 Then
                'send mail
                Try
                    WriteLog("SendMail:" & ds.Tables(0).Rows(i).Item("line_id"))
                    SendMail(old_xi, mail_to, mail_cc)
                Catch ex As Exception
                    WriteLog(ex.Message)
                End Try

            End If



            'Try
            '    WriteLog("SendMail:" & ds.Tables(0).Rows(i).Item("line_id"))
            '    SendMail(NullToEmpty(ds.Tables(0).Rows(i).Item("xi")), NullToEmpty(ds.Tables(0).Rows(i).Item("line_id")), mail_to, mail_cc)
            'Catch ex As Exception
            '    WriteLog(ex.Message)
            'End Try

            'End If
        Next


        Return ""

    End Function

    Function GetTDHtml(ByVal v As Object, Optional ByVal redfont As Boolean = False) As String
        Dim sb As New StringBuilder

        If redfont Then
            If NullToEmpty(v) > "0" Then
                sb.AppendLine("<td style=""border: 1px solid #111;padding-left: 2px;padding-right: 2px;color:red;"">")
            Else
                sb.AppendLine("<td style=""border: 1px solid #111;padding-left: 2px;padding-right: 2px;"">")
            End If
        Else
            sb.AppendLine("<td style=""border: 1px solid #111;padding-left: 2px;padding-right: 2px;"">")
        End If


        sb.AppendLine(NullToEmpty(v))
        sb.AppendLine("</td>")

        Return sb.ToString
    End Function


    Private Function SendMail(ByVal xi As String, ByVal mail_to As String, ByVal mail_cc As String) As String
        Dim sb As New StringBuilder

        Dim dsLine As New DataSet
        sb.Length = 0
        sb.AppendLine("select distinct line_id from m_email_kanri where xi = N'" & xi & "'")
        SqlHelperNew.FillDataset(conn, CommandType.Text, sb.ToString, dsLine, "dsLine")

        Dim msg As New StringBuilder
        msg.AppendLine("<body>")

        msg.AppendLine("<table cellpadding=""0"" cellspacing=""0""  style=""border-collapse: collapse;border-spacing: 0;"">")
        msg.AppendLine("<tr style=""border: 1px solid #111;padding-left: 2px;padding-right: 2px;background-color: #9cf;"">")

        msg.AppendLine("<th style=""border: 1px solid #111;padding-left: 2px;padding-right: 2px;"">")
        msg.AppendLine("线")
        msg.AppendLine("</th>")

        msg.AppendLine("<th style=""border: 1px solid #111;padding-left: 2px;padding-right: 2px;"">")
        msg.AppendLine("总 数")
        msg.AppendLine("</th>")

        msg.AppendLine("<th style=""border: 1px solid #111;padding-left: 2px;padding-right: 2px;"">")
        msg.AppendLine("NG 数")
        msg.AppendLine("</th>")

        msg.AppendLine("<th style=""border: 1px solid #111;padding-left: 2px;padding-right: 2px;"">")
        msg.AppendLine("检查中")
        msg.AppendLine("</th>")

        msg.AppendLine("<th style=""border: 1px solid #111;padding-left: 2px;padding-right: 2px;"">")
        msg.AppendLine("OK数")
        msg.AppendLine("</th>")

        msg.AppendLine("<th style=""border: 1px solid #111;padding-left: 2px;padding-right: 2px;"">")
        msg.AppendLine(“未检查”)
        msg.AppendLine("</th>")

        For i As Integer = 0 To dsLine.Tables(0).Rows.Count - 1

            Dim tmp_line_id As String = dsLine.Tables(0).Rows(i).Item(0)
            sb.Length = 0
            sb.AppendLine("SELECT ")
            sb.AppendLine("	count(*) all_cnt,")
            sb.AppendLine("	isnull(sum(case when exists_ng = 1 then 1 else 0 end) ,0) ng_suu,")
            sb.AppendLine("	isnull(sum(case when exists_ng = 0 and exists_chking = 1 then 1 else 0 end) ,0) chking_suu,")
            sb.AppendLine("	isnull(sum(case when exists_ng = 0 and exists_chking = 0 and exists_ok = 1 then 1 else 0 end) ,0) ok_suu,")
            sb.AppendLine("	isnull(sum(case when not_exists_checkresult = 1 then 1 else 0 end) ,0) mi_chk_suu")
            sb.AppendLine("FROM (")
            sb.AppendLine("")
            sb.AppendLine("	SELECT ")
            sb.AppendLine("	 case when not exists(select 1 from t_check_result tcr ")
            sb.AppendLine("					  where tcr.plan_no = a.plan_no and tcr.chk_no like a.chk_no+'_%')")
            sb.AppendLine("	 then 1 else 0 end not_exists_checkresult,")
            sb.AppendLine("")
            sb.AppendLine("	 case when exists(select 1 from t_check_result tcr ")
            sb.AppendLine("					  where tcr.plan_no = a.plan_no and tcr.chk_no like a.chk_no+'_%')")
            sb.AppendLine("	 then 1 else 0 end exists_checkresult,")
            sb.AppendLine("")
            sb.AppendLine("	 case when exists(select 1 from t_check_result tcr ")
            sb.AppendLine("					  where tcr.plan_no = a.plan_no and tcr.chk_no like a.chk_no+'_%' and tcr.chk_result=9)")
            sb.AppendLine("	 then 1 else 0 end exists_ng,")
            sb.AppendLine("")
            sb.AppendLine("	  case when exists(select 1 from t_check_result tcr ")
            sb.AppendLine("					  where tcr.plan_no = a.plan_no and tcr.chk_no like a.chk_no+'_%' and tcr.status<>2)")
            sb.AppendLine("	 then 1 else 0 end exists_chking,")
            sb.AppendLine("")
            sb.AppendLine("	  case when exists(select 1 from t_check_result tcr ")
            sb.AppendLine("					  where tcr.plan_no = a.plan_no and tcr.chk_no like a.chk_no+'_%' and tcr.chk_result=1)")
            sb.AppendLine("	 then 1 else 0 end exists_ok")
            sb.AppendLine("")
            sb.AppendLine("	FROM       t_check_plan   a")
            sb.AppendLine("	INNER JOIN [t_cd_temp_relation] t_cd")
            sb.AppendLine("		ON  a.code     =   t_cd.code")
            sb.AppendLine("		AND a.line_id like '%'+t_cd.line_id ")
            sb.AppendLine("	WHERE")
            sb.AppendLine("		CONVERT(varchar(8), cast(a.yotei_chk_date as date), 112) between CONVERT(varchar(8), dateadd(day,-1,getdate()), 112) and CONVERT(varchar(8), dateadd(day,-1,getdate()), 112)")
            sb.AppendLine("		AND ISNULL(t_cd.temp_id,'')<>''")
            sb.AppendLine("		AND ISNULL(a.line_id,'') ='" & tmp_line_id & "'")
            sb.AppendLine(") as aa")
            Dim tmp As New DataSet
            SqlHelperNew.FillDataset(conn, CommandType.Text, sb.ToString, tmp, "temp")


            msg.AppendLine("</tr>")

            For j As Integer = 0 To tmp.Tables(0).Rows.Count - 1
                msg.AppendLine("<tr>")

                msg.AppendLine(GetTDHtml(tmp_line_id)）

                msg.AppendLine(GetTDHtml(tmp.Tables(0).Rows(j).Item("all_cnt")))

                msg.AppendLine(GetTDHtml(tmp.Tables(0).Rows(j).Item("ng_suu"), True)）

                msg.AppendLine(GetTDHtml(tmp.Tables(0).Rows(j).Item("chking_suu"), True)）

                msg.AppendLine(GetTDHtml(tmp.Tables(0).Rows(j).Item("ok_suu"), False)）

                msg.AppendLine(GetTDHtml(tmp.Tables(0).Rows(j).Item("mi_chk_suu"), True)）

                msg.AppendLine("</tr>")
                'End If
            Next

        Next
        msg.AppendLine("</table>")





        'sb.Length = 0
        'sb.AppendLine("SELECT count(*)")
        'sb.AppendLine("FROM       t_check_plan   a")
        'sb.AppendLine("INNER JOIN [t_cd_temp_relation] t_cd")
        'sb.AppendLine("	ON  a.code     =   t_cd.code")
        'sb.AppendLine("	AND a.line_id like '%'+t_cd.line_id ")
        'sb.AppendLine("WHERE")
        'sb.AppendLine("	CONVERT(varchar(8), cast(a.yotei_chk_date as date), 112) between CONVERT(varchar(8), dateadd(day,-1,getdate()), 112) and CONVERT(varchar(8), dateadd(day,-1,getdate()), 112)")
        'sb.AppendLine("	AND ISNULL(t_cd.temp_id,'')<>''")
        'sb.AppendLine("	AND ISNULL(a.line_id,'') in (select line_id from m_email_kanri where xi = N'" & xi & "')")
        'Dim ds0 As New DataSet
        'SqlHelperNew.FillDataset(conn, CommandType.Text, sb.ToString, ds0, "temp")


        'sb.Length = 0
        'sb.AppendLine("SELECT count(*)")
        'sb.AppendLine("FROM       t_check_plan   a")
        'sb.AppendLine("INNER JOIN [t_cd_temp_relation] t_cd")
        'sb.AppendLine("	ON  a.code     =   t_cd.code")
        'sb.AppendLine("	AND a.line_id like '%'+t_cd.line_id ")
        'sb.AppendLine("LEFT JOIN t_check_result b_1")
        'sb.AppendLine("	ON  a.plan_no       = b_1.plan_no")
        'sb.AppendLine("	AND a.[chk_no]+'_1' = b_1.[chk_no]")
        'sb.AppendLine("LEFT JOIN t_check_result b_2")
        'sb.AppendLine("	ON  a.plan_no       = b_2.plan_no")
        'sb.AppendLine("	AND a.[chk_no]+'_2' = b_2.[chk_no]")
        'sb.AppendLine("WHERE")
        'sb.AppendLine("	CONVERT(varchar(8), cast(a.yotei_chk_date as date), 112) between CONVERT(varchar(8), dateadd(day,-1,getdate()), 112) and CONVERT(varchar(8), dateadd(day,-1,getdate()), 112)")
        'sb.AppendLine("	AND ISNULL(t_cd.temp_id,'')<>''")
        'sb.AppendLine("	AND ISNULL(a.line_id,'') in (select line_id from m_email_kanri where xi = N'" & xi & "')")

        'Dim ds1 As New DataSet
        'SqlHelperNew.FillDataset(conn, CommandType.Text, sb.ToString, ds1, "temp")


        'sb.Length = 0
        'sb.AppendLine("SELECT a.*")
        'sb.AppendLine("FROM       t_check_result   a")
        'sb.AppendLine("WHERE")
        'sb.AppendLine("	CONVERT(varchar(8), cast(a.yotei_chk_date as date), 112) between CONVERT(varchar(8), dateadd(day,-1,getdate()), 112) and CONVERT(varchar(8), dateadd(day,-1,getdate()), 112)")
        ''sb.AppendLine("	AND ISNULL(t_cd.temp_id,'')<>''")
        'sb.AppendLine("	AND ISNULL(a.line_id,'') in (select line_id from m_email_kanri where xi = N'" & xi & "')")
        ''sb.AppendLine("	AND ISNULL(a.[status],'')='2' ")
        'Dim ds2 As New DataSet
        'SqlHelperNew.FillDataset(conn, CommandType.Text, sb.ToString, ds2, "temp")


        sb.Length = 0
        sb.AppendLine("SELECT ")
        sb.AppendLine("ISNULL(a.line_id,'') line_id,a.[make_no],a.[code],a.[yotei_chk_date],")
        sb.AppendLine("CASE WHEN b_1.plan_no is     null                                                THEN N'一次检查也没有'")
        sb.AppendLine("	 WHEN b_1.plan_no is not null AND b_2.plan_no is null AND b_1.[status]<>'2'  THEN N'只有一次检查且是检查中'")
        sb.AppendLine("ELSE")
        sb.AppendLine("''")
        sb.AppendLine("END as rlt")
        sb.AppendLine("")
        sb.AppendLine("FROM       t_check_plan   a")
        sb.AppendLine("INNER JOIN [t_cd_temp_relation] t_cd")
        sb.AppendLine("	ON  a.code     =   t_cd.code")
        sb.AppendLine("	AND a.line_id like '%'+t_cd.line_id ")
        sb.AppendLine("LEFT JOIN t_check_result b_1")
        sb.AppendLine("	ON  a.plan_no       = b_1.plan_no")
        sb.AppendLine("	AND a.[chk_no]+'_1' = b_1.[chk_no]")
        sb.AppendLine("LEFT JOIN t_check_result b_2")
        sb.AppendLine("	ON  a.plan_no       = b_2.plan_no")
        sb.AppendLine("	AND a.[chk_no]+'_2' = b_2.[chk_no]")
        sb.AppendLine("WHERE")
        sb.AppendLine("	CONVERT(varchar(8), cast(a.yotei_chk_date as date), 112) between CONVERT(varchar(8), dateadd(day,-1,getdate()), 112) and CONVERT(varchar(8), dateadd(day,-1,getdate()), 112)")
        sb.AppendLine("	AND ISNULL(t_cd.temp_id,'')<>''")
        sb.AppendLine("	AND (")
        sb.AppendLine("		    (b_1.plan_no is null)")
        sb.AppendLine("		 OR (b_1.plan_no is not null AND b_2.plan_no is null AND b_1.[status]<>'2')")
        sb.AppendLine("	)")
        'sb.AppendLine("	AND ISNULL(a.line_id,'')='" & line_id & "'")
        sb.AppendLine("	AND ISNULL(a.line_id,'') in (select line_id from m_email_kanri where xi = N'" & xi & "')")
        sb.AppendLine("UNION")
        sb.AppendLine("")
        sb.AppendLine("SELECT")
        sb.AppendLine("	ISNULL(a.line_id,'') line_id,a.[make_no],a.[code],a.[yotei_chk_date],N'检查结果NG' as rlt")
        sb.AppendLine("FROM       t_check_result   a")
        sb.AppendLine("INNER JOIN [t_cd_temp_relation] t_cd")
        sb.AppendLine("	ON  a.code     =   t_cd.code")
        sb.AppendLine("	AND a.line_id like '%'+t_cd.line_id ")
        sb.AppendLine("WHERE a.chk_result = '9'")
        sb.AppendLine("AND")
        sb.AppendLine("	(")
        sb.AppendLine("	CONVERT(varchar(8), cast(a.yotei_chk_date as date), 112) between CONVERT(varchar(8), dateadd(day,-1,getdate()), 112) and CONVERT(varchar(8), dateadd(day,-1,getdate()), 112)")
        sb.AppendLine("	    OR ")
        sb.AppendLine("	    a.chk_end_date between CAST(CAST(dateadd(day,-1,GETDATE()) AS DATE) AS DATETIME) + CAST('08:00:00' AS DATETIME) and CAST(CAST(GETDATE() AS DATE) AS DATETIME) + CAST('08:00:00' AS DATETIME)")
        sb.AppendLine("	)")
        'sb.AppendLine("	AND ISNULL(a.line_id,'')='" & line_id & "'")
        sb.AppendLine("	AND ISNULL(a.line_id,'') in (select line_id from m_email_kanri where xi = N'" & xi & "')")

        Dim ds As New DataSet
        SqlHelperNew.FillDataset(conn, CommandType.Text, sb.ToString, ds, "temp")


        'msg.AppendLine("<style>")
        'msg.AppendLine("td{border: 1px solid #000;}")
        'msg.AppendLine("</style>")
        'msg.AppendLine("<p>")
        'msg.AppendLine("计划：" & ds0.Tables(0).Rows(0).Item(0))


        'msg.AppendLine("检查记录：" & ds1.Tables(0).Rows.Count)
        'msg.AppendLine("检查记录-OK：" & ds2.Tables(0).Select("chk_result=1").Length)
        'msg.AppendLine("检查记录-NG：" & ds2.Tables(0).Select("chk_result=9").Length)
        'msg.AppendLine("检查记录-检查中：" & ds2.Tables(0).Select("status<>2").Length)


        '' msg.AppendLine("计划：" & ds0.Tables(0).Rows(0).Item(0) & "，检查记录：" & ds1.Tables(0).Rows(0).Item(0) & "条，NG几条，未检几条")

        'msg.AppendLine("</p>")

        msg.AppendLine("<hr>以下NG明细")
        msg.AppendLine("<table cellpadding=""0"" cellspacing=""0""  style=""border-collapse: collapse;border-spacing: 0;"">")
        msg.AppendLine("<tr style=""border: 1px solid #111;padding-left: 2px;padding-right: 2px;background-color: #9cf;"">")

        msg.AppendLine("<th style=""border: 1px solid #111;padding-left: 2px;padding-right: 2px;"">")
        msg.AppendLine("线")
        msg.AppendLine("</th>")

        msg.AppendLine("<th style=""border: 1px solid #111;padding-left: 2px;padding-right: 2px;"">")
        msg.AppendLine("作番")
        msg.AppendLine("</th>")

        msg.AppendLine("<th style=""border: 1px solid #111;padding-left: 2px;padding-right: 2px;"">")
        msg.AppendLine("CD")
        msg.AppendLine("</th>")

        msg.AppendLine("<th style=""border: 1px solid #111;padding-left: 2px;padding-right: 2px;"">")
        msg.AppendLine("检查预定日")
        msg.AppendLine("</th>")

        msg.AppendLine("<th style=""border: 1px solid #111;padding-left: 2px;padding-right: 2px;"">")
        msg.AppendLine(“结果”)
        msg.AppendLine("</th>")
        msg.AppendLine("</tr>")
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            'If ds.Tables(0).Rows(i).Item("xi") IsNot DBNull.Value AndAlso ds.Tables(0).Rows(i).Item("xi").ToString.Trim <> "" Then
            'Dim mail_to As String = NullToEmpty(ds.Tables(0).Rows(i).Item("to_email"))
            'Dim mail_cc As String = NullToEmpty(ds.Tables(0).Rows(i).Item("cc_email"))
            msg.AppendLine("<tr>")

            msg.AppendLine("<td style=""border: 1px solid #111;padding-left: 2px;padding-right: 2px;"">")
            msg.AppendLine(NullToEmpty(ds.Tables(0).Rows(i).Item("line_id")))
            msg.AppendLine("</td>")

            msg.AppendLine("<td style=""border: 1px solid #111;padding-left: 2px;padding-right: 2px;"">")
            msg.AppendLine(NullToEmpty(ds.Tables(0).Rows(i).Item("make_no")))
            msg.AppendLine("</td>")

            msg.AppendLine("<td style=""border: 1px solid #111;padding-left: 2px;padding-right: 2px;"">")
            msg.AppendLine(NullToEmpty(ds.Tables(0).Rows(i).Item("code")))
            msg.AppendLine("</td>")

            msg.AppendLine("<td style=""border: 1px solid #111;padding-left: 2px;padding-right: 2px;"">")
            msg.AppendLine(NullToEmpty(ds.Tables(0).Rows(i).Item("yotei_chk_date")))
            msg.AppendLine("</td>")

            msg.AppendLine("<td style=""border: 1px solid #111;padding-left: 2px;padding-right: 2px;"">")
            msg.AppendLine(NullToEmpty(ds.Tables(0).Rows(i).Item("rlt")))
            msg.AppendLine("</td>")
            msg.AppendLine("</tr>")
            'End If
        Next
        msg.AppendLine("</table>")
        msg.AppendLine("</body>")
        WriteLog("SendMail:" & ds.Tables(0).Rows.Count & "件")

        If msg.ToString <> "" Then
            SendMail2(mail_to, mail_cc, msg.ToString, xi)
        End If


        'Return msg.ToString

    End Function


    Public Function SendMail2(ByVal tomail As String, ByVal ccmail As String, ByVal txt As String, ByVal xi As String) As String

        'NullToEmpty(ds.Tables(0).Rows(i).Item("line_id")),

        Try
            Dim SmtpServer As New SmtpClient("10.160.192.62")
            Dim mail As New MailMessage()
            mail = New MailMessage()
            mail.IsBodyHtml = True
            mail.From = New MailAddress("chinahelpdesk@lixil.com")


            Dim arr()
            arr = tomail.Trim.Split(";"c)

            For i As Integer = 0 To arr.Length - 1
                If (arr(i).ToString.Trim() <> "") Then
                    mail.To.Add(arr(i).ToString.Trim())
                End If
            Next

            arr = ccmail.Split(";"c)
            For i As Integer = 0 To arr.Length - 1
                If (arr(i).ToString.Trim() <> "") Then
                    If (arr(i).ToString.Trim() <> "") Then
                        mail.CC.Add(arr(i).ToString.Trim())
                    End If
                End If
            Next

            mail.Bcc.Add("songtao.li@lixil.com")
            mail.Bcc.Add("lengqing2010@163.com")

            mail.Subject = "[" & xi & "] GKL NG DATA " & Now.ToString("yyyy-MM-dd")
            mail.Body = txt
            SmtpServer.Send(mail)
            WriteLog("mail send:" & txt)
            Return ("mail send")
        Catch ex As Exception
            WriteLog(ex.Message)
            Return (ex.ToString)
        End Try
    End Function


    Public Function NullToEmpty(ByVal obj As Object) As String
        If obj Is DBNull.Value Then
            Return ""
        ElseIf obj Is Nothing Then
            Return ""
        Else
            Return obj.ToString.Trim
        End If
    End Function

End Module
