
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not IsPostBack Then
            tbx_user_cd.Focus()
        End If
    End Sub



    Protected Sub btnLoginIn_Click(sender As Object, e As EventArgs) Handles btnLoginIn.Click

        Dim user_cd As String = Me.tbx_user_cd.Text
        Dim user_password As String = Me.tbx_user_password.Text

        Dim BC As New MUserBC

        Try

            Dim dt As Data.DataTable = BC.ChkUser(user_cd, user_password)

            If dt.Rows.Count > 0 Then

                lblMsg.Text = ""

                Context.Items("menu_line_id") = Common.NullToEmpty(dt.Rows(0).Item("line_id"))
                Context.Items("menu_user_cd") = Common.NullToEmpty(dt.Rows(0).Item("user_cd"))
                Context.Items("menu_user_name") = Common.NullToEmpty(dt.Rows(0).Item("user_name"))
                Context.Items("menu_kengen") = Common.NullToEmpty(dt.Rows(0).Item("kengen"))

                Server.Transfer("Menu.aspx")

            Else

                lblMsg.Text = "用户名密码不匹配"

            End If
        Catch ex As Exception
            lblMsg.Text = ex.Message

        End Try



    End Sub


End Class
