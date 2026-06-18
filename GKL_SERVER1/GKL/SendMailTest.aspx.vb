
Partial Class SendMailTest
    Inherits System.Web.UI.Page

    Private Sub SendMailTest_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Mail.SenMail("10.160.219.1", "8080")
        Response.Write(Mail.SendMail2())
    End Sub
End Class
