Imports HashidsNet
Public Class Form1
    Dim strall As String

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim hs As Hashids
        hs = New Hashids("koreha watashino lixil 2018", 27)
        strall = ""
        TextBox4.Text = intFun(TextBox1.Text)
        TextBox2.Text = hs.EncodeHex(TextBox4.Text)
        TextBox3.Text = hs.DecodeHex(TextBox2.Text)





    End Sub

    Private Function intFun(ByVal Str As String) As String
        Dim n1 As String
        For i As Integer = 0 To Str.Length - 1
            Dim SubStr As String = Str.Substring(i, 1)
            n1 = Asc(SubStr)
            n1 = Hex(n1)
            strall = strall & n1
        Next
        intFun = strall
    End Function




    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub


    Private Function deintFun(ByVal str As String) As String
        Dim re As String = ""
        For i As Integer = 0 To str.Length - 1 Step 2
            Dim substring As String = str.Substring(i, 2)
            Dim n1 As Integer = HEX_to_DEC(substring)
            Dim n2 = Chr(n1)
            re = re & n2
        Next

        deintFun = re

    End Function

    Public Function HEX_to_DEC(ByVal Hex As String) As Long
        Dim i As Long
        Dim B As Long

        Hex = UCase(Hex)
        For i = 1 To Len(Hex)
            Select Case Mid(Hex, Len(Hex) - i + 1, 1)
                Case "0" : B = B + 16 ^ (i - 1) * 0
                Case "1" : B = B + 16 ^ (i - 1) * 1
                Case "2" : B = B + 16 ^ (i - 1) * 2
                Case "3" : B = B + 16 ^ (i - 1) * 3
                Case "4" : B = B + 16 ^ (i - 1) * 4
                Case "5" : B = B + 16 ^ (i - 1) * 5
                Case "6" : B = B + 16 ^ (i - 1) * 6
                Case "7" : B = B + 16 ^ (i - 1) * 7
                Case "8" : B = B + 16 ^ (i - 1) * 8
                Case "9" : B = B + 16 ^ (i - 1) * 9
                Case "A" : B = B + 16 ^ (i - 1) * 10
                Case "B" : B = B + 16 ^ (i - 1) * 11
                Case "C" : B = B + 16 ^ (i - 1) * 12
                Case "D" : B = B + 16 ^ (i - 1) * 13
                Case "E" : B = B + 16 ^ (i - 1) * 14
                Case "F" : B = B + 16 ^ (i - 1) * 15
            End Select
        Next i
        HEX_to_DEC = B
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles de.Click
        Dim hs As Hashids
        hs = New Hashids("koreha watashino lixil 2018", 27)

        TextBox3.Text = hs.DecodeHex(TextBox2.Text)

        TextBox5.Text = deintFun(TextBox3.Text) '解密后字符串
        TextBox6.Text = TextBox5.Text.Substring(0, 2) '
        TextBox7.Text = TextBox5.Text.Substring(2, 8) 'lot
        TextBox8.Text = TextBox5.Text.Substring(10, 9) '工单号
        TextBox9.Text = TextBox5.Text.Substring(19, 3) '序号

    End Sub
End Class
