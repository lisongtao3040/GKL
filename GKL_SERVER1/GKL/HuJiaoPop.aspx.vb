
Partial Class HuJiaoPop
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim da As New HuJiaoDA
            Dim line_id As String = Request.QueryString("line_id")
            If line_id = "" Then
                line_id = "211"
            End If

            If line_id.Trim.Length = 8 Then
                line_id = line_id.Trim
                line_id = Left(Right(line_id, 4), 3)

            End If

            Dim dt As Data.DataTable = da.Get_m_station_list(line_id)

            Dim lst As List(Of String) = New List(Of String)()



            For i As Integer = 0 To dt.Rows.Count - 1
                lst.Add("STATION/" & dt.Rows(i).Item("stationNo").ToString())
            Next

            hidStationNo.Value = String.Join("|", lst)

        End If
    End Sub
End Class
