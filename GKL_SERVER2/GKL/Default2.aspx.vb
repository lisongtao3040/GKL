
Partial Class Default2
    Inherits System.Web.UI.Page

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        ' URL编码的字符串
        Dim encodedString = "%e6%8b%9c%e6%89%98%e6%a0%87%e7%ad%be%2c1%2c%e5%8f%96%e4%bb%98%e5%ad%94%2c1%2c%e6%88%b7%e5%bd%93%2c0%2c%e5%b7%a5%e4%bb%b6%e9%a2%9c%e8%89%b2%2c1"

        ' 使用HttpUtility.UrlDecode来解码
        Dim decodedString = HttpUtility.UrlDecode(encodedString, System.Text.Encoding.UTF8)

        Response.Write(decodedString)

    End Sub
End Class
