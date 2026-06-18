
Partial Class Test_GetTpXiangxian
    Inherits System.Web.UI.Page

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        Dim bg As New Bg

        bg.GetTpXiangxian("340043,370003,,,,340108")
    End Sub
End Class
