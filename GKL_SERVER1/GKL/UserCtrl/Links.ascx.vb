
Partial Class UserCtrl_Links
    Inherits System.Web.UI.UserControl


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        '加载参数
        Cparam.SetCommonViewState(IsPostBack, Me.ViewState, Me.Context)

        'If Not IsPostBack Then
        '    ViewState("menu_line_id") = Context.Items("menu_line_id")
        '    ViewState("menu_user_cd") = Context.Items("menu_user_cd")
        '    ViewState("menu_user_name") = Context.Items("menu_user_name")

        'Else
        '    Context.Items("menu_line_id") = ViewState("menu_line_id")
        '    Context.Items("menu_user_cd") = ViewState("menu_user_cd")
        '    Context.Items("menu_user_name") = ViewState("menu_user_name")

        'End If

    End Sub

    Public Sub InitViewstate(ByVal line_id As String, ByVal user_cd As String, ByVal user_name As String)
        ViewState("menu_line_id") = line_id
        ViewState("menu_user_cd") = user_cd
        ViewState("menu_user_name") = user_name
    End Sub

    Protected Sub lbUser_Click(sender As Object, e As EventArgs) Handles lbUser.Click
        Server.Transfer("m_user.aspx")
    End Sub


    Protected Sub lbProject_Click(sender As Object, e As EventArgs) Handles lbProject.Click
        Server.Transfer("m_project.aspx")
    End Sub

    Protected Sub lbTools_Click(sender As Object, e As EventArgs) Handles lbTools.Click
        Server.Transfer("m_tools.aspx")
    End Sub

    Protected Sub lbCheckMethod_Click(sender As Object, e As EventArgs) Handles lbCheckMethod.Click
        Server.Transfer("m_check_method.aspx")
    End Sub

    Protected Sub lbRelation_Click(sender As Object, e As EventArgs) Handles lbRelation.Click
        Server.Transfer("t_cd_temp_relation.aspx")
    End Sub

    Protected Sub lbPlan_Click(sender As Object, e As EventArgs) Handles lbPlan.Click
        Server.Transfer("t_check_plan.aspx")
    End Sub

    Protected Sub lbTemp_Click(sender As Object, e As EventArgs) Handles lbTemp.Click
        Server.Transfer("m_temp.aspx")
    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Server.Transfer("CheckItiran.aspx")
    End Sub

    Protected Sub lbMenu_Click(sender As Object, e As EventArgs) Handles lbMenu.Click
        Server.Transfer("Menu.aspx")
    End Sub


End Class
