
Partial Class YXLDUPD
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim code As String = Request.QueryString("code")
        Dim BC As New TCheckResultBC

        Try
            BC.InsYXLDLog(code, "2")
        Catch ex As Exception

        End Try

        Dim no As String = Left(code, 10)
        Dim cnt As String = code.Substring(10, 2)
        Dim result As String = code.Substring(12)
        BC.UpdYXLD(no, cnt, result)
        Response.Write("OK")
        Response.End()

    End Sub
End Class
