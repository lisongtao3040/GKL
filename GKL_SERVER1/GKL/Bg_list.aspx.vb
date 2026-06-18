
Partial Class Bg_list
    Inherits System.Web.UI.Page



    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        '加载参数
        Cparam.SetCommonViewState(IsPostBack, Me.ViewState, Me.Context)

        If Not IsPostBack Then

            ViewState("menu_line_id") = Common.NullToEmpty(Context.Items("menu_line_id"))
            ViewState("menu_user_cd") = Common.NullToEmpty(Context.Items("menu_user_cd"))
            ViewState("menu_user_name") = Common.NullToEmpty(Context.Items("menu_user_name"))


            Dim dt As Data.DataTable = (New MUserBC).SelLineIds()
            ddlLine.Items.Clear()
            For i As Integer = 0 To dt.Rows.Count - 1
                ddlLine.Items.Add(dt.Rows(i).Item("line_id").ToString)
                ddlLine.Items(ddlLine.Items.Count - 1).Value = dt.Rows(i).Item("line_id").ToString
            Next

            If Context.Items("line") Is Nothing Then
                ddlLine.SelectedValue = ViewState("menu_line_id")
                tbxYmd.Text = Now.ToString("yyyy-MM-dd")
            Else
                ddlLine.SelectedValue = Context.Items("line")
                tbxYmd.Text = Context.Items("planymd")
                InitPage()
            End If
        End If
    End Sub

    Protected Sub btnSel_Click(sender As Object, e As EventArgs) Handles btnSel.Click
        InitPage()
    End Sub

    Sub InitPage()

        Dim dt As Data.DataTable = (New BaoGongDA).SelBgList(Me.tbxYmd.Text, Me.ddlLine.SelectedValue)
        gv.DataSource = dt
        gv.DataBind()

        For i As Integer = 0 To dt.Rows.Count - 1

            gv.Rows(i).Cells(3).Text = Math.Ceiling(CInt(dt.Rows(i).Item("suu")) / CInt(dt.Rows(i).Item("tuopan_syu_suu")))
            If Common.NullToEmpty(dt.Rows(i).Item("bg_result")).ToString <> "" Then
                'gv.Rows(i).Cells(4).Text = "OK"
                gv.Rows(i).Cells(5).Text = Common.NullToEmpty(dt.Rows(i).Item("complete_date"))
            Else
                'gv.Rows(i).Cells(4).Text = ""

            End If

            If Common.NullToEmpty(dt.Rows(i).Item("bg_result")).ToString = "OK" Then
                CType(gv.Rows(i).FindControl("lbtnLink"), LinkButton).Text = "查看"
                gv.Rows(i).Cells(6).BackColor = Drawing.Color.Green
            ElseIf Common.NullToEmpty(dt.Rows(i).Item("bg_result")).ToString = "NG" Then
                CType(gv.Rows(i).FindControl("lbtnLink"), LinkButton).Text = "查看"
                gv.Rows(i).Cells(6).BackColor = Drawing.Color.Red
            Else
                CType(gv.Rows(i).FindControl("lbtnLink"), LinkButton).Text = "报工"
            End If
            CType(gv.Rows(i).FindControl("lbtnLink"), LinkButton).OnClientClick = "GoToMs('" & dt.Rows(i).Item("ProductCode") & "','" & dt.Rows(i).Item("ZuoFan") & "');return false;"

        Next

        lblTxt.Text = "共有 " & dt.Rows.Count & " 条工单，报工完了 " & dt.Select("bg_result='OK'").Length & "条，未报工 " & dt.Select("bg_result is null").Length & "条 ,NG" & dt.Select("bg_result='NG'").Length & " 条 "

    End Sub

    Protected Sub btnGoMs_Click(sender As Object, e As EventArgs) Handles btnGoMs.Click
        Context.Items("cd") = hidCd.Value
        Context.Items("no") = hidNo.Value

        Context.Items("line") = Me.ddlLine.SelectedValue
        Context.Items("planymd") = Me.tbxYmd.Text.Trim


        Server.Transfer("Bg_ms.aspx")
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Server.Transfer("MENU.aspx")
    End Sub
End Class
