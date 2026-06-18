
Partial Class MENU
    Inherits System.Web.UI.Page



    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        '加载参数
        Cparam.SetCommonViewState(IsPostBack, Me.ViewState, Me.Context)

        If Not IsPostBack Then
            ViewState("menu_line_id") = Common.NullToEmpty(Context.Items("menu_line_id"))
            ViewState("menu_user_cd") = Common.NullToEmpty(Context.Items("menu_user_cd"))
            ViewState("menu_user_name") = Common.NullToEmpty(Context.Items("menu_user_name"))
            If ViewState("menu_user_cd") IsNot Nothing AndAlso ViewState("menu_user_cd") = "admin" Then
                lbCheckMethod.Visible = True
            Else
                lbCheckMethod.Visible = False
            End If


            ' ifm2.Attributes.Item("src") = "http://localhost:21948/GKL2_CHK_MENU2.aspx?user_id=" + ViewState("menu_user_cd") + "&user_name=" + ViewState("menu_user_name") + "&line_cd=" + ViewState("menu_line_id")


            ifm2.Attributes.Item("src") = "http://10.160.192.114/DWP20/GKL2_CHK_MENU2.aspx?user_id=" + ViewState("menu_user_cd") + "&user_name=" + ViewState("menu_user_name") + "&line_cd=" + ViewState("menu_line_id")

            Dim csScript As New StringBuilder
            With csScript
                .AppendLine("localStorage.setItem('login_user_cd','" & ViewState("menu_user_cd") & "');")
            End With

            'ページ応答で、クライアント側のスクリプト ブロックを出力します
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "localStorage", csScript.ToString, True)

            '报工系统是否打开
            Dim BaoGongDA As New BaoGongDA

            '如果报工系统打开
            If BaoGongDA.IsBaogongSysOn() Then
                btnBaogongSysIsOpen.Text = "报工已打开--关闭"
                btnBaogongSysIsOpen.BackColor = Drawing.Color.Green
            Else
                btnBaogongSysIsOpen.Text = "报工已关闭--打开"
                btnBaogongSysIsOpen.BackColor = Drawing.Color.Red
            End If


            If (System.Configuration.ConfigurationManager.AppSettings.Get("baogong_lines").ToString().IndexOf(ViewState("menu_line_id").ToString()) >= 0) Then

            Else
                btnBaogongSysIsOpen.Visible = False
                lbtnBaogong.Visible = False
            End If

        End If

        Me.Links.InitViewstate(ViewState("menu_line_id"), ViewState("menu_user_cd"), ViewState("menu_user_name"))

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

    Protected Sub lbLogout_Click(sender As Object, e As EventArgs) Handles lbLogout.Click
        Context.Items("menu_line_id") = ""
        Context.Items("menu_user_cd") = ""
        Context.Items("menu_user_name") = ""
        Server.Transfer("Default.aspx")
    End Sub

    Protected Sub lbtnBaogong_Click(sender As Object, e As EventArgs) Handles lbtnBaogong.Click
        Server.Transfer("Bg_list.aspx")
    End Sub
    
        Protected Sub btnBaogongSysIsOpen_Click(sender As Object, e As EventArgs) Handles btnBaogongSysIsOpen.Click

        '报工系统是否打开
        Dim BaoGongDA As New BaoGongDA

        If btnBaogongSysIsOpen.Text = "报工已打开--关闭" Then
            BaoGongDA.BaogongOnOff("0")
            btnBaogongSysIsOpen.Text = "报工已关闭--打开"
            btnBaogongSysIsOpen.BackColor = Drawing.Color.Red
        Else
            BaoGongDA.BaogongOnOff("1")
            btnBaogongSysIsOpen.Text = "报工已打开--关闭"
            btnBaogongSysIsOpen.BackColor = Drawing.Color.Green
        End If

        'If BaoGongDA.IsBaogongSysOn() Then
        '    btnBaogongSysIsOpen.Text = "报工已打开--关闭"
        '    btnBaogongSysIsOpen.BackColor = Drawing.Color.Green
        'Else
        '    btnBaogongSysIsOpen.Text = "报工已关闭--打开"
        '    btnBaogongSysIsOpen.BackColor = Drawing.Color.Red
        'End If

        ''如果报工系统打开
        'If BaoGongDA.IsBaogongSysOn() Then
        '    btnBaogongSysIsOpen.Text = "报工已打开--关闭"
        'Else
        '    btnBaogongSysIsOpen.Text = "报工已关闭--打开"
        'End If


    End Sub
End Class

