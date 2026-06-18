Imports System.Configuration
Imports System.Collections.Specialized
Imports System.Deployment.Application
Imports System.Web
Imports System.Windows.Forms
Imports System.Text

Public Class FmPrint
    Private PrintUrl As String = ConfigurationManager.AppSettings("PrintUrl").ToString()
    Private conn As String = ConfigurationManager.AppSettings("connectionString").ToString()
    Public SqlHelperNew As New SqlHelperNew
    Private kbn As String

    Private Sub WebBrowser1_DocumentCompleted(sender As Object, e As Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted
        Try
            If kbn = 0 Then
                WebBrowser1.ShowPrintPreviewDialog()
                Sleep(1000000)
                Me.Close()
            Else
                WebBrowser1.Print()
                Sleep(10000)
                Me.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Me.Close()
        End Try
    End Sub

    Private Sub FmPrint_Load(sender As Object, e As EventArgs) Handles Me.Load

        'GetPrintHave("20191206091111309")

        Try
            Me.TopLevel = False
            Me.Hide()
            Dim nameValueTable As NameValueCollection = GetQueryStringParameters()

            If Not GetPrintHave(nameValueTable.Item("chk_no").Trim()) Then
                Me.TopLevel = True
                Me.Show()
                MessageBox.Show("没有可打印的数据")
                Me.Close()
                Exit Sub
            End If

            'MsgBox(nameValueTable.Item("chk_id"))
            'MsgBox(nameValueTable.Item("kbn"))
            kbn = nameValueTable.Item("kbn")
            Dim index As String = nameValueTable.Item("index")
            If kbn = "" Then
                kbn = "1"
            End If
            Dim url As String = PrintUrl & "?chk_no=" & nameValueTable.Item("chk_no") & "&CD=" & nameValueTable.Item("CD") & "&index=" & nameValueTable.Item("index")
            WebBrowser1.Url = New Uri(url)
            WebBrowser1.Show()

            'Me.Show()
            Me.Hide()

        Catch ex As Exception
            MsgBox(ex.Message)
            Me.Close()

        End Try


    End Sub


    Private Function GetQueryStringParameters() As NameValueCollection
        Dim nameValueTable As NameValueCollection = New NameValueCollection()
        If ApplicationDeployment.IsNetworkDeployed Then
            Dim queryString As String = ApplicationDeployment.CurrentDeployment.ActivationUri.Query
            nameValueTable = HttpUtility.ParseQueryString(queryString)
        End If
        Return (nameValueTable)
    End Function
    Public Shared Sub Sleep(ByVal Interval)
        Dim __time As DateTime = DateTime.Now
        Dim __Span As Int64 = Interval * 10000 '因为时间是以100纳秒为单位。
        While (DateTime.Now.Ticks - __time.Ticks < __Span)
            Application.DoEvents()
        End While
    End Sub

    Public Function GetPrintHave(ByVal chk_no As String) As Boolean

        '--**テーブル： : m_picture
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("SELECT ")
        sb.AppendLine("      a.[make_no]")
        sb.AppendLine("  FROM [t_check_result] a")
        sb.AppendLine("  INNER JOIN [m_print_lines_code_relation] b")
        sb.AppendLine("  ON a.code = b.J_CD")
        sb.AppendLine("  WHERE a.chk_no='" & chk_no & "'")

        Dim ds As New DataSet

        SqlHelperNew.FillDataset(conn, CommandType.Text, sb.ToString, ds, "temp")

        Return ds.Tables(0).Rows.Count > 0

    End Function

End Class