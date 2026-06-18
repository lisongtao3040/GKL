Imports Microsoft.VisualBasic

Public Class Conn

    Private Const connStrHome As String = "Data Source=WIN7U-20150705K\R2; Initial Catalog=auto_savedata;Persist Security Info=True;User ID=sa;Password=1983313a"
    Private Const connStrCompaney As String = "Data Source=10.160.200.39; Initial Catalog=auto_savedata;Persist Security Info=True;User ID=sa;Password=lixil@2014"
    Private Const connStrDell As String = "Data Source=ADP1QD9478YL0O2\ILIKE; Initial Catalog=auto_savedata;Persist Security Info=True;User ID=sa;Password=19833130"
    Private Const connStrWanguo As String = "Data Source=AGOBW-707150707\SQLEXPRESS2008; Initial Catalog=auto_savedata;Persist Security Info=True;User ID=sa;Password=19833130"

    Public Shared Function ConnStr() As String
        If System.Net.Dns.GetHostName = "WIN7U-20150705K" Then
            Return connStrHome
        ElseIf System.Net.Dns.GetHostName = "ADP1QD9478YL0O2" Then
            Return connStrDell
        ElseIf System.Net.Dns.GetHostName = "AGOBW-707150707" Then
            Return connStrWanguo
        Else
            Return connStrCompaney
        End If
    End Function

End Class
