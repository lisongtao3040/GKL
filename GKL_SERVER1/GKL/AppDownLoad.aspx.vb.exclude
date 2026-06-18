
Partial Class AppDownLoad
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.LinkButton1.Text = ConfigurationManager.AppSettings("PicPath").ToString()
        LinkButton1.Attributes.Item("href") = ConfigurationManager.AppSettings("PicPath").ToString()
    End Sub
End Class
