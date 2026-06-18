
Partial Class apexcharts_Default
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not IsPostBack Then

            'tbxDate_key.Text = System.DateTime.Now.ToString("yyyy/MM/dd")
            'tbxDate_key.Attributes("itType") = "date"
            'tbxDate_key.Attributes("itLength") = "20"
            'tbxDate_key.Attributes("itName") = "登録日"

            Dim line_id As String
            Dim chk_date As String
            If Request.QueryString("line_id") IsNot Nothing Then

                line_id = Request.QueryString("line_id")
                chk_date = Request.QueryString("chk_date")

                Me.tbxLineId_key.Text = line_id

                'If chk_date.Trim <> "" Then
                '    chk_date = DateAdd(DateInterval.Month, -1, CDate(chk_date)).ToString("yyyy/MM/dd")
                'Else
                '    chk_date = DateAdd(DateInterval.Month, -1, Now).ToString("yyyy/MM/dd")
                'End If

                If chk_date.Trim <> "" Then
                    chk_date = CDate(chk_date).ToString("yyyy/MM/dd")
                Else
                    chk_date = Now.ToString("yyyy/MM/dd")
                End If

                Me.Calendar1.SelectedDate = chk_date
                Me.Calendar1.TodaysDate = chk_date

                'Me.tbxDate_key.Text = chk_date

            End If


        End If


    End Sub




    Public Function GetGraData() As String


        Dim startEnd As Date = CDate(Me.Calendar1.SelectedDate)

        Dim startDate As Date = DateAdd(DateInterval.Month, -1, startEnd)

        Dim sb0 As New StringBuilder

        With sb0
            .AppendLine("    var myDate;")
            .AppendLine("    var tm;")
            .AppendLine("    var data = [];")
            .AppendLine("    var data2 = [];")
            .AppendLine("    myDate = new Date(" & startDate.Year & ", " & startDate.Month - 1 & ", " & startDate.Day & ");")
            .AppendLine("    tm = myDate.getTime();")
        End With

        Dim sb As New StringBuilder
        Dim BC As New TCheckResultBC



        Dim dt As Data.DataTable = BC.SelTCheckResult(tbxLineId_key.Text.Trim, startDate.ToString("yyyy/MM/dd"), startEnd.ToString("yyyy/MM/dd"), "", "")

        Dim iDate As Date = startDate

        While iDate <= startEnd

            Dim drs() As Data.DataRow = dt.Select("yotei_chk_date='" & iDate.ToString("yyyy/MM/dd") & "'")


            '加一天
            If sb.Length > 0 Then
                sb.AppendLine("tm = tm + 86400000;")

            End If


            '计划
            sb.AppendLine("data.push([tm, " & dt.Select("yotei_chk_date='" & iDate.ToString("yyyy/MM/dd") & "'").Length & "]);")
            '实际
            sb.AppendLine("data2.push([tm, " & dt.Select("yotei_chk_date='" & iDate.ToString("yyyy/MM/dd") & "' AND chk_times>=1").Length & "]);")

            iDate = DateAdd(DateInterval.Day, 1, iDate)
        End While

        Return sb0.ToString & vbNewLine & sb.ToString


    End Function

    Protected Sub Calendar1_SelectionChanged(sender As Object, e As EventArgs) Handles Calendar1.SelectionChanged
        GetGraData()
    End Sub
End Class
