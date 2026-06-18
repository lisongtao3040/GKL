
Partial Class _Default
    Inherits System.Web.UI.Page


    'Protected Sub lbPic_Click(sender As Object, e As EventArgs) Handles lbPic.Click

    'End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not IsPostBack Then

            'If Context.Items("menu_line_id") IsNot Nothing Then
            '    ViewState("menu_line_id") = Context.Items("menu_line_id")
            '    ViewState("menu_user_cd") = Context.Items("menu_user_cd")
            '    ViewState("menu_user_name") = Context.Items("menu_user_name")
            '    Me.Panel1.Visible = False
            '    lblMsg.Text = ""
            'End If

            tbx_user_cd.Focus()

        End If

        'Context.Items("menu_line_id") = ViewState("menu_line_id")
        'Context.Items("menu_user_cd") = ViewState("menu_user_cd")
        'Context.Items("menu_user_name") = ViewState("menu_user_name")

    End Sub



    Protected Sub btnLoginIn_Click(sender As Object, e As EventArgs) Handles btnLoginIn.Click

        Dim user_cd As String = Me.tbx_user_cd.Text
        Dim user_password As String = Me.tbx_user_password.Text

        Dim BC As New MUserBC
        Dim dt As Data.DataTable = BC.ChkUser(user_cd, user_password)

        If dt.Rows.Count > 0 Then
            lblMsg.Text = ""
            Context.Items("menu_line_id") = Common.NullToEmpty(dt.Rows(0).Item("line_id"))
            Context.Items("menu_user_cd") = Common.NullToEmpty(dt.Rows(0).Item("user_cd"))
            Context.Items("menu_user_name") = Common.NullToEmpty(dt.Rows(0).Item("user_name"))
            Context.Items("menu_kengen") = Common.NullToEmpty(dt.Rows(0).Item("kengen"))

            'Response.Cookies("user_cd").Value = Common.NullToEmpty(dt.Rows(0).Item("user_cd"))
            'Response.Cookies("line_id").Value = Common.NullToEmpty(dt.Rows(0).Item("line_id"))
            'Response.Cookies("user_name").Value = Common.NullToEmpty(dt.Rows(0).Item("user_name"))
            'Response.Cookies("kengen").Value = Common.NullToEmpty(dt.Rows(0).Item("kengen"))

            Server.Transfer("Menu.aspx?login_user_cd=" & Context.Items("menu_user_cd"))

        Else

            lblMsg.Text = "用户名密码不匹配"

        End If


    End Sub


End Class
