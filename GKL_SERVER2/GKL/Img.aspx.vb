Imports System.Data
Imports System.IO

Partial Class Img
    Inherits System.Web.UI.Page

    Private MPictureBC As New MPictureBC

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Request.QueryString("imgKbn") Is Nothing Then
            Try
                Dim bt As Byte()

                bt = DirectCast(MPictureBC.GetLineListPic(Request.QueryString("pic_id"), Request.QueryString("line_id")), Byte())
                Response.BinaryWrite(bt)

                Response.End()
            Catch ex As Exception
            End Try

        ElseIf Request.QueryString("imgKbn") = 1 Then


            Dim chkNo_key As String = Request.QueryString("chkNo_key")

            Dim BC As New TCheckMsBC
            Dim dt As Data.DataTable = BC.SelImgInfo(chkNo_key)
            Dim rtv As String = ""
            For i As Integer = 0 To dt.Rows.Count - 1
                If rtv = "" Then
                    rtv = dt.Rows(i).Item("picPath").ToString
                Else
                    rtv = rtv & "," & dt.Rows(i).Item("picPath").ToString
                End If
            Next
            Response.Write(rtv)
            Response.End()

        ElseIf Request.QueryString("imgKbn") = 2 Then

            Dim bt As Byte()

            FileToBinary(bt, Request.QueryString("imgpath"))

            Response.BinaryWrite(bt)

            Response.End()

        End If



    End Sub

    '文件转二进制流,读取文件二进制流
    Public Function FileToBinary(ByRef byTmp As Byte(), ByVal path As String) As Boolean
        Dim fs As FileStream
        Dim br As BinaryReader

        fs = New FileStream(path, FileMode.Open, FileAccess.Read)
        br = New BinaryReader(fs)

        byTmp = br.ReadBytes(fs.Length)

        br.Close()
        fs.Close()
        fs.Dispose()

        Return True
    End Function


    Private Function GetAllFile(ByVal path As String) As String

        If Not System.IO.Directory.Exists(path) Then
            Return ""
        End If

        Dim strFile As String() = System.IO.Directory.GetFiles(path, "*.jpg")
        Dim rtv As String = ""
        Dim i As Integer

        If strFile.Length > 0 Then


            For i = 0 To strFile.Length - 1
                If rtv = "" Then
                    rtv = strFile(i)
                Else
                    rtv = rtv & "," & strFile(i)
                End If
            Next
        End If

        Return rtv

    End Function





End Class
