Imports System.Configuration
Imports System.Threading
Imports System.Text

Public Class Form1


    Private conn As String = "Data Source=10.160.192.114; Initial Catalog=GKL;Persist Security Info=True;User ID=sa;Password=ying+xian_2019"
    Public SqlHelperNew As New SqlHelperNew

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ChkMail()
    End Sub


    Private Function ChkMail()

        'SQLコメント
        '--**テーブル：用户MS : m_user
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM [m_email_kanri]")
        sb.AppendLine("WHERE 1=1")

        Dim ds As New DataSet
        SqlHelperNew.FillDataset(conn, CommandType.Text, sb.ToString, ds, "temp")

        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

        Next


        Return ""

    End Function
End Class
