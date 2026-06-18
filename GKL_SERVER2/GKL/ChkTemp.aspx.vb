
Partial Class ChkTemp
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim lineId_key As String = Request.QueryString("line_id")
        Dim tempId_key As String = Request.QueryString("temp_id")

        Dim bc As New MTempBC

        Dim dt As Data.DataTable = bc.SelMTempChk(lineId_key, tempId_key)
        gv.DataSource = dt
        gv.DataBind()

        If dt.Rows.Count = 0 Then
            Response.Write("没有错误！")
        End If

    End Sub
End Class
