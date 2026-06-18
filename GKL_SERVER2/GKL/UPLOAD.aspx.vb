
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Partial Class UPLOAD
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If GetUploadFileContent.PostedFile.InputStream.Length < 1 Then
            'Msg.Text = "请选择文件"
            Return
        End If

        Dim FileName As String = GetUploadFileContent.FileName
        Dim FilePath As String = GetUploadFileContent.PostedFile.FileName

        If FileName.ToLower().IndexOf(".htm") = -1 Then
            'Msg.Text = "请选择要求的类型的文件"
            Return
        End If

        Dim FileLen As Integer = GetUploadFileContent.PostedFile.ContentLength
        Dim input As Byte() = New Byte(FileLen - 1) {}
        Dim UpLoadStream As System.IO.Stream = GetUploadFileContent.PostedFile.InputStream
        UpLoadStream.Read(input, 0, FileLen)
        UpLoadStream.Position = 0
        Dim sr As System.IO.StreamReader = New System.IO.StreamReader(UpLoadStream, System.Text.Encoding.[Default])
        'Msg.Text = "您上传的文件内容是：<br/><br/>" & sr.ReadToEnd()
        sr.Close()
        UpLoadStream.Close()
        UpLoadStream = Nothing
        sr = Nothing
    End Sub

End Class


