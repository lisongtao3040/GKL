
Partial Class Qianpin
    Inherits System.Web.UI.Page


    Private BC As New TCheckResultBC

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim suu As Integer = BC.GetQianpinCnt(Request.QueryString("chk_no"))
            Me.tbxQianpinSuu.Text = suu

        End If

    End Sub

    'Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

    '    Try
    '        BC.SetQianpinCnt(Request.QueryString("chk_no"), Me.tbxQianpinSuu.Text)
    '        lblMsg.Text = "保持成功"
    '    Catch ex As Exception
    '        lblMsg.Text = ex.Message

    '    End Try


    'End Sub
End Class
