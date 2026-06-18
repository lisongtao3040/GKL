Imports System.Data
Imports System.Text
Imports Newtonsoft.Json
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Drawing.Drawing2D

Partial Class printLinesCodeRelationOne
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' 设置响应编码为 UTF-8
        Response.ContentEncoding = System.Text.Encoding.UTF8
        Response.Charset = "utf-8"

        ' 从 URL 参数获取数据
        Dim chk_no As String = Request.QueryString("chk_no")
        Dim CD As String = Request.QueryString("CD")
        Dim index As String = Request.QueryString("index")

        ' 设置默认值
        If String.IsNullOrEmpty(chk_no) Then
            chk_no = "260430_T020670643_1"
        End If
        If String.IsNullOrEmpty(CD) Then
            CD = "CHBAC14XKABYXXR"
        End If

        ' 加载标签
        LoadLabel(chk_no, CD, index)
    End Sub

    Private Sub LoadLabel(chk_no As String, CD As String, index As String)
        Try
            ' 调用 API 获取数据
            Dim api As New api()
            Dim result As String = api.GetPrintLinesCodeRelationByChkNo(chk_no)

            ' 解析 JSON
            Dim jsonResult = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(result)

            If jsonResult("success").ToString() = "True" Then
                Dim data As List(Of Dictionary(Of String, Object)) = JsonConvert.DeserializeObject(Of List(Of Dictionary(Of String, Object)))(jsonResult("data").ToString())

                ' 查找匹配的 CD
                Dim matchedRow As Dictionary(Of String, Object) = Nothing
                For Each row In data
                    If row("CD").ToString() = CD Then
                        matchedRow = row
                        Exit For
                    End If
                Next

                If matchedRow Is Nothing Then
                    ' 未找到数据，显示空白
                    Return
                End If

                ' 生成标签 HTML
                GenerateLabel(matchedRow, index)

                ' 检查是否已打印
                CheckIfPrinted(chk_no, CD)
            End If

        Catch ex As Exception
            ' 忽略错误，显示空白页面
        End Try
    End Sub

    Private Sub GenerateLabel(row As Dictionary(Of String, Object), index As String)
        Dim sb As New StringBuilder()

        ' 标签容器
        sb.AppendLine("<div class='label-wrapper'>")
        sb.AppendLine("<div class='qr-row'>")
        sb.AppendLine("<div class='label-content'>")
        sb.AppendLine("<table class='label-table' border='0'>")

        ' 头部信息
        sb.AppendLine("<tr><td colspan='2'>")
        sb.AppendLine("<table>")
        sb.AppendLine("<tr><td class='header-info'>作番:" & GetFieldValue(row, "make_no") & "</td></tr>")
        sb.AppendLine("<tr><td class='header-info'>CD:" & GetFieldValue(row, "CD") & "</td></tr>")
        sb.AppendLine("<tr><td style='font-size:11px;'>ID:" & GetFieldValue(row, "chk_no") & "-" & index & "</td></tr>")
        sb.AppendLine("</table>")
        sb.AppendLine("</td></tr>")

        ' 内容行
        sb.AppendLine("<tr>")

        ' 二维码单元格
        sb.AppendLine("<td class='barcode-cell' style='text-align:left;vertical-align:top;'>")

        ' 生成二维码图片并转换为 Base64
        Dim qrContent As String = BuildQRCodeContent(row, index)
        Dim qrImageBase64 As String = GenerateQRCodeImage(qrContent)
        sb.AppendLine("<img src='" & qrImageBase64 & "' style='width:94px;height:94px;display:block;' />")

        sb.AppendLine("</td>")

        ' 尺寸信息单元格
        sb.AppendLine("<td>")
        sb.AppendLine("<table class='dimensions-table'>")
        sb.AppendLine("<tr><td>&nbsp;W:" & FormatDecimal(GetFieldValue(row, "W"), "0000.0") & "</td><td>&nbsp;H:" & FormatDecimal(GetFieldValue(row, "H"), "0000.0") & "</td></tr>")
        sb.AppendLine("<tr><td>DW:" & FormatDecimal(GetFieldValue(row, "DW"), "0000.0") & "</td><td>DH:" & FormatDecimal(GetFieldValue(row, "DH"), "0000.0") & "</td></tr>")
        sb.AppendLine("<tr><td>SW:" & FormatDecimal(GetFieldValue(row, "SW"), "0000.0") & "</td><td>KW:" & FormatDecimal(GetFieldValue(row, "KW"), "0000.0") & "</td></tr>")
        sb.AppendLine("<tr><td colspan='2'></td></tr>")
        'sb.AppendLine("<tr><td colspan='2' class='seq-info'>顺位:" & GetFieldValue(row, "shunwei") & "</td></tr>")
        sb.AppendLine("<tr><td colspan='2'>" & GetFieldValue(row, "J_CD") & "</td></tr>")

        sb.AppendLine("</table>")
        sb.AppendLine("</td>")

        sb.AppendLine("</tr>")
        sb.AppendLine("</table>")

        ' 底部文字
        sb.AppendLine("<div class='footer-text'>此生产标签生产完成后请取下！</div>")

        sb.AppendLine("</div>") ' label-content
        sb.AppendLine("</div>") ' qr-row
        sb.AppendLine("</div>") ' label-wrapper

        labelContainer.InnerHtml = sb.ToString()
    End Sub

    Private Function GetFieldValue(row As Dictionary(Of String, Object), fieldName As String) As String
        If row.ContainsKey(fieldName) AndAlso row(fieldName) IsNot Nothing Then
            Dim value As String = row(fieldName).ToString()
            If Not String.IsNullOrEmpty(value) Then
                Return value
            End If
        End If
        Return "-"
    End Function

    Private Function FormatDecimal(value As String, format As String) As String
        If String.IsNullOrEmpty(value) OrElse value = "-" Then
            Return format.Replace("0"c, "0"c)
        End If

        Dim num As Double
        If Double.TryParse(value, num) Then
            Dim decimalPlaces As Integer = 0
            Dim dotIndex As Integer = format.IndexOf("."c)
            If dotIndex <> -1 Then
                decimalPlaces = format.Length - dotIndex - 1
            End If

            Dim formatted As String = num.ToString("F" & decimalPlaces)

            Dim intLength As Integer = If(dotIndex <> -1, dotIndex, format.Length)
            Dim parts() As String = formatted.Split("."c)
            Dim intPart As String = parts(0)

            While intPart.Length < intLength
                intPart = "0" & intPart
            End While

            If parts.Length > 1 Then
                Return intPart & "." & parts(1)
            Else
                Return intPart
            End If
        End If

        Return value
    End Function

    Private Function BuildQRCodeContent(row As Dictionary(Of String, Object), index As String) As String
        Dim qrFM As New StringBuilder()

        ' 工单号 make_no（10位）
        Dim makeNo As String = GetFieldValue(row, "make_no")
        qrFM.Append(PadRight(makeNo, 10, " "c) & "/")

        ' code CD（20位）
        Dim cd As String = GetFieldValue(row, "CD")
        qrFM.Append(PadRight(cd, 20, " "c) & "/")

        ' 数量 suu（4位）
        Dim suu As String = GetFieldValue(row, "suu")
        qrFM.Append(PadLeft(suu, 4, "0"c) & "/")

        ' SAP订单号 sapOderNo（15位）
        Dim sapOderNo As String = GetFieldValue(row, "sapOderNo")
        If sapOderNo.Length > 15 Then
            sapOderNo = sapOderNo.Substring(0, 15)
        End If
        qrFM.Append(PadRight(sapOderNo, 15, " "c) & "/")

        ' SAP订单序号 sapIndexNo（10位）- 取后2位
        Dim sapIndexNo As String = GetFieldValue(row, "sapIndexNo")
        Dim last2Chars As String = If(sapIndexNo.Length >= 2, sapIndexNo.Substring(sapIndexNo.Length - 2), sapIndexNo)
        qrFM.Append(PadRight(last2Chars, 10, " "c) & "/")

        ' 打印时间（17位）
        Dim timeStr As String = DateTime.Now.ToString("yyyyMMddHHmmssfff")
        qrFM.Append(PadRight(timeStr, 17, " "c) & "/")

        ' H（6位，格式 0000.0）
        qrFM.Append(FormatDecimal(GetFieldValue(row, "H"), "0000.0") & "/")

        ' W（6位，格式 0000.0）
        qrFM.Append(FormatDecimal(GetFieldValue(row, "W"), "0000.0") & "/")

        ' DH（6位，格式 0000.0）
        qrFM.Append(FormatDecimal(GetFieldValue(row, "DH"), "0000.0") & "/")

        ' DW（6位，格式 0000.0）
        qrFM.Append(FormatDecimal(GetFieldValue(row, "DW"), "0000.0") & "/")

        ' SW（6位，格式 0000.0）
        qrFM.Append(FormatDecimal(GetFieldValue(row, "SW"), "0000.0") & "/")

        ' KW（6位，格式 0000.0）
        qrFM.Append(FormatDecimal(GetFieldValue(row, "KW"), "0000.0") & "/")

        ' lot_no（8位）
        Dim lotNo As String = GetFieldValue(row, "lot_no")
        qrFM.Append(PadRight(lotNo, 8, " "c) & "/")

        ' 末尾补充空格
        qrFM.Append("       ")

        Return qrFM.ToString()
    End Function

    ''' <summary>
    ''' 使用 DotNetBarcode 生成二维码图片并转换为 Base64（备用）
    ''' </summary>
    Private Function GenerateQRCodeImage_Old(qrContent As String) As String
        Try
            Dim qrBar As New DotNetBarcode()
            qrBar.Type = DotNetBarcode.Types.QRCode
            qrBar.FontSize = 24
            qrBar.SaveFileType = DotNetBarcode.SaveFileTypes.Jpeg
            qrBar.PrintChar = False

            ' 增加空白边距，提高识别率
            qrBar.QRQuitZone = 2
            qrBar.QRColorQuitZone = Color.White

            Dim qRCodeBitMap As New Bitmap(110, 110)
            Dim qRCodeGraphics As Graphics = Graphics.FromImage(qRCodeBitMap)

            ' 使用白色背景填充
            qRCodeGraphics.Clear(Color.White)

            ' 绘制二维码，使用较小的尺寸以减少复杂度
            qrBar.WriteBar(qrContent, 5, 5, 100, 100, qRCodeGraphics)

            Dim ms As New MemoryStream()
            qRCodeBitMap.Save(ms, ImageFormat.Jpeg)
            Dim bytes() As Byte = ms.ToArray()

            ' 转换为 Base64 字符串
            Dim base64String As String = Convert.ToBase64String(bytes)
            Return "data:image/jpeg;base64," & base64String

        Catch ex As Exception
            ' 如果生成失败，返回空字符串
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' 使用 QRCoder 生成二维码图片并转换为 Base64
    ''' </summary>
    Private Function GenerateQRCodeImage(qrContent As String) As String
        Try
            ' 使用 QRCoder 库生成二维码
            Dim qrGenerator As New QRCoder.QRCodeGenerator()

            ' 使用最低纠错级别 L，生成最简单的二维码
            Dim qrCodeData As QRCoder.QRCodeData = qrGenerator.CreateQrCode(qrContent, QRCoder.QRCodeGenerator.ECCLevel.M)
            Dim qrCode As New QRCoder.QRCode(qrCodeData)

            ' 生成二维码图片
            ' 参数说明：pixelsPerModule=3（每个模块3像素，越小越简单）
            ' 最后一个参数 True 表示添加 Quiet Zone（白色边框）
            Dim qrBitmap As Bitmap = qrCode.GetGraphic(1, Color.Black, Color.White, True)

            ' 缩放到目标尺寸（99 * 0.95 = 94）
            Dim scaledBitmap As New Bitmap(94, 94)
            Using g As Graphics = Graphics.FromImage(scaledBitmap)
                g.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
                g.DrawImage(qrBitmap, 0, 0, 94, 94)
            End Using

            ' 转换为 JPEG
            Dim ms As New MemoryStream()
            scaledBitmap.Save(ms, ImageFormat.Jpeg)
            Dim bytes() As Byte = ms.ToArray()

            ' 转换为 Base64
            Dim base64String As String = Convert.ToBase64String(bytes)
            Return "data:image/jpeg;base64," & base64String

        Catch ex As Exception
            ' 如果生成失败，返回空字符串
            Return ""
        End Try
    End Function

    Private Function PadLeft(str As String, len As Integer, padChar As Char) As String
        Return str.PadLeft(len, padChar)
    End Function

    Private Function PadRight(str As String, len As Integer, padChar As Char) As String
        Return str.PadRight(len, padChar)
    End Function

    Private Sub CheckIfPrinted(chk_no As String, CD As String)
        Try
            Dim api As New api()
            Dim result As String = api.GetPrintedLabels(chk_no)

            Dim jsonResult = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(result)

            If jsonResult("success").ToString() = "True" Then
                Dim printedCDs As List(Of String) = JsonConvert.DeserializeObject(Of List(Of String))(jsonResult("data").ToString())

                If printedCDs.Contains(CD) Then
                    ' 添加 printed 类
                    Dim script As String = "var qrRow = document.querySelector('.qr-row'); if (qrRow) { qrRow.classList.add('printed'); }"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "MarkPrinted", script, True)
                End If
            End If
        Catch ex As Exception
            ' 忽略错误
        End Try
    End Sub
End Class
