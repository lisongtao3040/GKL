
Partial Class t_colorcheck_result
    Inherits System.Web.UI.Page
    Private DA As New t_colorcheck_resultDA()
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim dtLines As Data.DataTable = (New MUserBC()).SelLineIds()
            Dim line_cd As String, line_cd_3keta As String
            ddl_lines.Items.Clear()
            ddl_lines.Items.Add("")

            For i As Integer = 0 To dtLines.Rows.Count - 1
                line_cd = dtLines.Rows(i).Item(0).ToString().Trim
                If line_cd.Length = 8 Then
                    line_cd_3keta = Left(Right(line_cd, 4), 3)
                ElseIf line_cd.Length = 4 Then
                    line_cd_3keta = Left(line_cd, 3)
                Else
                    line_cd_3keta = line_cd
                End If

                ddl_lines.Items.Add(line_cd_3keta)
            Next



            tbxYmd.Text = Now.ToString("yyyy-MM-dd")

            InitMs()

        End If


    End Sub

    Sub InitMs()

        Dim dt As Data.DataTable = DA.Sel_t_colorcheck_result(Me.ddl_lines.SelectedValue, Me.tbxYmd.Text)
        gv.DataSource = dt
        gv.DataBind()

    End Sub
    Protected Sub btnSel_Click(sender As Object, e As EventArgs) Handles btnSel.Click
        InitMs()
    End Sub
    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Server.Transfer("MENU.aspx")
    End Sub
End Class
