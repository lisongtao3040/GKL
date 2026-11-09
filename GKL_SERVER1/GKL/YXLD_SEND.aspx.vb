Imports System.Net
Imports System.IO

Partial Class YXLD_SEND
    Inherits System.Web.UI.Page

    Private Sub YXLD_SEND_Load(sender As Object, e As EventArgs) Handles Me.Load



        Dim ajaxActionType As String = Request.QueryString("ajaxActionType")

        'SEND
        If ajaxActionType = "1" Then


            'Dim MyClient As Net.WebClient = New Net.WebClient
            'Dim MyReader As New System.IO.StreamReader(MyClient.OpenRead("http://wap.baidu.com"), System.Text.Encoding.Default)
            'Dim MyWebCode As String = MyReader.ReadToEnd
            'MyReader.Close()
            Dim kbn As String = Request.Form("kbn")
            Dim no As String = Request.Form("no")
            Dim cnt As String = Request.Form("cnt")
            Dim cd As String = Request.Form("cd")
            Dim lr As String = Request.Form("lr")

            Dim dw As String = ""
            Dim dh As String = ""

            If Request.Form("dw") IsNot Nothing Then
                dw = Request.Form("dw")
            End If

            If Request.Form("dh") IsNot Nothing Then
                dh = Request.Form("dh")
            End If

            'Dim rtv As String = PostData("http://192.168.1.80:5001/api/code?=" & kbn & no & cnt & cd & lr, "")
            'Dim rtv As String
            'rtv = GetData("http://192.168.1.80:5001/api/code", "code=" & kbn & no & cnt & cd & lr)
            'rtv = GetData("http://10.160.192.114/GN_CHK/Default.aspx", "code=" & kbn & no & cnt & cd & lr)

            Dim BC As New TCheckResultBC

            Try
                BC.InsYXLDLog(no & "_" & cd & "_" & kbn & "_" & cnt & "_" & lr & "_" & dw & "_" & dh, "1")
            Catch ex As Exception

            End Try

            If BC.SendYXLD(kbn, no, cnt, cd, lr, dw, dh) Then
                Response.Write("OK")
            Else
                Response.Write("NG")
            End If
            Response.End()



        ElseIf ajaxActionType = "2" Then
            Dim no As String = Request.Form("no")
            Dim cnt As String = Request.Form("cnt")
            Dim BC As New TCheckResultBC
            Dim dt As Data.DataTable = BC.GetYXLD(no, cnt)

            If dt.Rows.Count = 0 Then
                Response.Write("Waiting....")
            Else
                Response.Write("OK:" & dt.Rows(0).Item("result"))

            End If


            Response.End()


        End If

    End Sub



    Public Shared Function GetData(ByVal url As String, ByVal data As String) As String

        Dim request As HttpWebRequest = WebRequest.Create(url + "?" + data)
        request.Method = "GET"
        Dim sr As StreamReader = New StreamReader(request.GetResponse().GetResponseStream)
        Return sr.ReadToEnd
    End Function

    Public Function PostData(ByVal url As String, ByVal data As String) As String

        ServicePointManager.Expect100Continue = False
        Dim request As HttpWebRequest = WebRequest.Create(url)
        '//Post请求方式
        request.Method = "POST"

        '内容类型
        request.ContentType = "application/x-www-form-urlencoded"
        '将URL编码后的字符串转化为字节
        Dim encoding As New UTF8Encoding()
        Dim bys As Byte() = encoding.GetBytes(data)
        '设置请求的 ContentLength 
        request.ContentLength = bys.Length
        '获得请 求流
        Dim newStream As Stream = request.GetRequestStream()
        'newStream.Write(bys, bys.Length)
        newStream.Write(bys, 0, bys.Length)
        newStream.Close()
        '获得响应流
        Dim sr As StreamReader = New StreamReader(request.GetResponse().GetResponseStream)
        Return sr.ReadToEnd
    End Function
End Class
