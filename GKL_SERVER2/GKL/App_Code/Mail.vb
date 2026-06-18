Imports Microsoft.VisualBasic
Imports System.Net
Imports System.Net.Mail

Public Class Mail

    Public Shared Sub SenMail(ByVal proxy_server_address As String, ByVal port As String)
        Try
            ' 设置全局的默认代理
            'Dim proxy As New WebProxy(proxy_server_address, CInt(port))
            'proxy.Credentials = New NetworkCredential("songtao.li@lixil.com", "lengqing%%%555")
            'WebRequest.DefaultWebProxy = proxy

            ' 创建 SmtpClient 对象
            'Dim smtpClient As New SmtpClient("smtp.gmail.com", 587)
            'smtpClient.UseDefaultCredentials = False
            'https://learn.microsoft.com/en-us/answers/questions/1167393/send-email-form-gmail-account-using-c
            'vxyy uzzd lubr iacs.
            'ekvf urid vjhk ykxt
            Dim smtpClient As New SmtpClient("smtp.gmail.com", 587)
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network

            smtpClient.EnableSsl = True
            smtpClient.UseDefaultCredentials = False




            smtpClient.Credentials = New NetworkCredential("songtao.li@lixil.com", "ekvfuridvjhkykxt")

            ' 设置发件人地址、收件人地址、主题和正文内容
            Dim mailMessage As New MailMessage("songtao.li@lixil.com", "songtao.li@lixil.com", "这是一封测试邮件", "这是邮件的内容")

            ' 发送邮件
            smtpClient.Send(mailMessage)

            Console.WriteLine("邮件发送成功！")
        Catch ex As Exception
            Console.WriteLine("发送邮件时出现错误： " & ex.Message)
        End Try
    End Sub

    Public Shared Sub SendMail()
        Try
            '' 设置全局的默认代理
            'Dim proxy As New WebProxy("10.88.102.54", 8080)
            'proxy.Credentials = New NetworkCredential("songtao.li@lixil.com", "lengqing%%%555")
            'WebRequest.DefaultWebProxy = proxy
            'ektb qfkd ibco sxkj
            Dim SmtpServer As New SmtpClient()
            Dim mail As New MailMessage()
            SmtpServer.Credentials = New Net.NetworkCredential("lisongtao2016@gmail.com", "ektbqfkdibcosxkj")
            SmtpServer.Port = 587
            SmtpServer.Host = "smtp.gmail.com"
            'SmtpServer.EnableSsl = False

            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network

            SmtpServer.EnableSsl = True
            SmtpServer.UseDefaultCredentials = False

            mail = New MailMessage()
            mail.From = New MailAddress("lisongtao2016@gmail.com")
            mail.To.Add("songtao.li@lixil.com")
            mail.Subject = "Test Mail"
            mail.Body = "This is for testing SMTP mail from GMAIL"
            SmtpServer.Send(mail)
            MsgBox("mail send")
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub


    Public Shared Function SendMail2() As String

        Try

            'JJFSOWEGKRCYAUSA

            '' 设置全局的默认代理
            'Dim proxy As New WebProxy("10.88.102.54", 8080)
            'Dim proxy As New WebProxy("10.88.42.18", 8080)
            Dim proxy As New WebProxy("10.160.219.1", 8080)
            proxy.Credentials = New NetworkCredential("songtao.li@lixil.com", "lengqing%%%555")
            WebRequest.DefaultWebProxy = proxy
            'ektb qfkd ibco sxkj
            Dim SmtpServer As New SmtpClient()
            Dim mail As New MailMessage()
            SmtpServer.Credentials = New Net.NetworkCredential("lengqing2010@163.com", "JJFSOWEGKRCYAUSA")
            'SmtpServer.Port = 587
            SmtpServer.Host = "smtp.163.com"
            'SmtpServer.EnableSsl = False

            'SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network

            'SmtpServer.EnableSsl = False
            'SmtpServer.UseDefaultCredentials = True

            mail = New MailMessage()
            mail.From = New MailAddress("lengqing2010@163.com")
            mail.To.Add("songtao.li@lixil.com")
            mail.To.Add("lengqing2010@163.com")
            mail.Subject = "Test Mail"
            mail.Body = "This is for testing SMTP mail from GMAIL"
            SmtpServer.Send(mail)
            Return ("mail send")
        Catch ex As Exception
            Return (ex.ToString)
        End Try
    End Function
End Class
