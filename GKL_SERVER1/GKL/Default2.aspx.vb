
Partial Class Default2
    Inherits System.Web.UI.Page

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        Dim da As New TCheckResultDA
        Response.Write(da.TestAndShowData("SRM1321A", "", "", "9020458522", "YY-AB00HR-MHFT"))

        'Response.Write(da.TestAndShowData("SRM1334A", "", "", "9020512637", "YY-AZ12K-MWC6"))
    End Sub
End Class
