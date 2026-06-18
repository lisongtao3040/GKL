Imports Microsoft.VisualBasic
Imports System.IO


Partial Class AJAX
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim kbn As String = Request.QueryString("kbn")

        If kbn = "pic" Then 'Picture
            Dim BC As New MPictureBC
            Dim dt As Data.DataTable = BC.SelMPicture(Request.QueryString("pic_id"), Request.QueryString("line_id"))
            If dt.Rows.Count > 0 Then
                Response.Write(dt.Rows(0).Item("pic_name"))
            End If
        ElseIf kbn = "chk_method" Then '检查方法
            Dim BC As New MCheckMethodBC
            Dim dt As Data.DataTable = BC.SelMCheckMethod(Request.QueryString("chk_id"), "")
            If dt.Rows.Count > 0 Then
                Response.Write(dt.Rows(0).Item("chk_name"))
            End If
        ElseIf kbn = "tool" Then '治具
            Dim BC As New MToolsBC
            Dim dt As Data.DataTable = BC.SelMTools(Request.QueryString("tool_id"), Request.QueryString("line_id"))
            If dt.Rows.Count > 0 Then
                Response.Write(dt.Rows(0).Item("tool_name"))
            End If

        ElseIf kbn = "scmx" Then '治具

            Dim BaoGongDA As New BaoGongDA
            Dim dtBG As Data.DataTable = BaoGongDA.SelBgListByCd(Request.QueryString("cd"), Request.QueryString("no"), Request.QueryString("line_id"))
            Dim mxs As String = dtBG.Rows(0).Item("ProductCodeSap") & "/" & dtBG.Rows(0).Item("ProductCode") & "/" & dtBG.Rows(0).Item("suu") & "/" & CInt(Math.Ceiling(dtBG.Rows(0).Item("suu") / CInt(dtBG.Rows(0).Item("Package")))) & "/" & dtBG.Rows(0).Item("localStorage") & "/" & dtBG.Rows(0).Item("DestinationCode") & "/" & (1) & "/" & dtBG.Rows(0).Item("ZuoFan")
            Response.Write(mxs)
            Response.End()

        ElseIf kbn = "chk_tmp" Then '检查模板

            Dim BC As New MTempBC
            Dim dt As Data.DataTable = BC.SelMTemp(Request.QueryString("line_id"), Request.QueryString("temp_id"), "")
            If dt.Rows.Count <= 0 Then
                Response.Write("1")
                Response.End()
            End If
            dt = BC.SelMTemp(Request.QueryString("line_id"), Request.QueryString("temp_id_new"), "")
            If dt.Rows.Count > 0 Then
                Response.Write("2")
                Response.End()
            End If

            Dim BC2 As New MTempNameBC
            Dim dt2 As Data.DataTable = BC2.SelMTempName(Request.QueryString("line_id"), Request.QueryString("temp_id_new"))
            If dt2.Rows.Count <= 0 Then
                Response.Write("3")
                Response.End()
            End If
        ElseIf kbn = "user" Then '用户

            Dim user_cd As String = Request.QueryString("user_cd")
            Dim password As String = Request.QueryString("user_cd")

            Dim BC As New MUserBC
            Dim dt As Data.DataTable = BC.SelMUser("", Request.QueryString("user_cd"), "ajax")

            If dt.Rows.Count > 0 Then
                Response.Write(dt.Rows(0).Item("user_name") & "," & dt.Rows(0).Item("line_id"))
                Response.End()
            End If
        ElseIf kbn = "chk_ms_upd" Then '检查明显更新

            Dim chkNo_key As String = Request.Form("chkNo_key")
            Dim in1 As String = Request.Form("in1")
            Dim chkResult As String = Request.Form("chkResult")
            Dim mark As String = Request.Form("mark")
            Dim kj0 As String = Request.Form("kj0")
            Dim kj1 As String = Request.Form("kj1")
            Dim kj2 As String = Request.Form("kj2")
            Dim insUser As String = Request.Form("insUser")
            Dim line_id As String = Request.Form("line_id")
            Dim chk_method_id As String = Request.Form("chk_method_id")
            Dim BC As New TCheckMsBC
            BC.UpdTCheckMs(chkNo_key,
                               in1,
                               chkResult,
                               mark,
                               kj0,
                               kj1,
                               kj2,
                               insUser,
                               line_id, chk_method_id)
        ElseIf kbn = "lines" Then '生产线
            Response.Write(Common.LineIds)

        ElseIf kbn = "tempsIds" Then '模板ID s
            Dim line_id As String = Request.QueryString("line_id")
            Response.Write(Common.TempIds(line_id))

        ElseIf kbn = "userlist" Then '用户list
            Dim line_id As String = Request.QueryString("line_id")
            Response.Write(Common.SelUserlist)

        ElseIf kbn = "chk_color" Then
            Dim goodcd As String = Request.Form("goodcd")
            Dim toolcd As String = Request.Form("toolcd")
            Dim linecd As String = Request.Form("linecd")
            Response.Write(Common.SelColor(goodcd, toolcd, linecd))
        ElseIf kbn = "chk_color_copy_last" Then
            Dim make_no As String = Request.Form("make_no").Trim
            Dim goodcd As String = Request.Form("goodcd").Trim
            Dim linecd As String = Request.Form("linecd").Trim
            Dim user As String = Request.Form("user")
            'Response.Write(Common.SelColor(goodcd, toolcd, linecd))
            '            Select Case top 1 b.tool_id from t_cd_temp_relation a inner join m_tools b On a.color_nm = b.tool_name And a.line_id = b.line_id
            'inner Join(select * from t_colorcheck_result where line_id='')
            'WHERE
            '                a.line_id =''
            'And a.code =''
            'And a.temp_id =''
            Response.Write(Common.AutoSetColor(linecd, make_no, goodcd， user))


        ElseIf kbn = "upd_img" Then '拍照上传
            'Dim chkNo_key As String = Request.Form("chkNo_key")
            'Dim img As String = Request.Form("img")
            'Dim line_id As String = Request.Form("line_id")
            'Dim chk_method_id As String = Request.Form("chk_method_id")


            ''img = img.Replace("data:image/png;base64,", "").Replace(" ", "+")
            'img = img.Replace("data:image/jpeg;base64,", "")

            'Dim img_save_path As String = ConfigurationManager.AppSettings("img_save_path").ToString()
            'If Not System.IO.Directory.Exists(img_save_path) Then
            '    System.IO.Directory.CreateDirectory(img_save_path)
            'End If
            'img_save_path = img_save_path & line_id & "\"
            'If Not System.IO.Directory.Exists(img_save_path) Then
            '    System.IO.Directory.CreateDirectory(img_save_path)
            'End If
            'img_save_path = img_save_path & chkNo_key & "\"
            'If Not System.IO.Directory.Exists(img_save_path) Then
            '    System.IO.Directory.CreateDirectory(img_save_path)
            'End If

            'Dim img_path As String = img_save_path & chk_method_id & "_" & Now.ToString("yyyyMMddHHmmssfff") & ".jpg"
            'Dim signedFromUmt As String = System.Text.Encoding.GetEncoding("utf-8").GetString(Convert.FromBase64String(img))

            'Using fs As New FileStream(img_path, FileMode.Create)
            '    Using bw As New BinaryWriter(fs)
            '        Dim data() As Byte = Convert.FromBase64String(img)
            '        bw.Write(data)
            '        bw.Close()
            '    End Using
            'End Using


        ElseIf kbn = "get_chk_imgs" Then
            Dim chkNo_key As String = Request.Form("chkNo_key")
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

        ElseIf kbn = "show_chk_img" Then
            Dim chkNo_key As String = Request.QueryString("chkNo_key")
            Dim img_name As String = Request.QueryString("img_name")

            Dim BC As New TCheckMsBC
            Dim dt As Data.DataTable = BC.SelImgInfo(chkNo_key)
            Dim rtv As String = ""
            For i As Integer = 0 To dt.Rows.Count - 1

                If dt.Rows(i).Item("picPath").ToString.Contains(img_name) Then
                    If System.IO.File.Exists(dt.Rows(i).Item("picPath").ToString) Then
                        Response.WriteFile(dt.Rows(i).Item("picPath").ToString)
                        Response.End()
                    Else
                        Response.End()
                    End If

                End If

            Next

        ElseIf kbn = "DeleteImg" Then
            '
            Dim img_name As String = Request.Form("img_name")
            Dim chkNo_key As String = Request.Form("chkNo_key")
            Try
                File.Delete(img_name)
                Dim BC As New TCheckMsBC
                Dim dt As Data.DataTable = BC.DelImgInfo(img_name, chkNo_key)


            Catch ex As Exception

            End Try


        End If

        Response.End()

    End Sub


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
