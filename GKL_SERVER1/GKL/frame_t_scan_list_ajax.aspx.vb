Imports System.Text
Imports System.Web

Partial Class frame_t_scan_list_ajax
    Inherits System.Web.UI.Page

    '結果値
    Private Const RESULT_OK As String = "OK"
    Private Const RESULT_NG As String = "NG"
    Private Const RESULT_RN As String = "RN"

    '扫码値の最大長（t_scan_list.scan_txt）
    Private Const LEN_SCAN_TXT As Integer = 200

    'JSON用ダブルクォーテーション
    Private Const DQ As String = """"

    'JSON用バックスラッシュ
    Private Const BACKSLASH As String = "\"

    ''' <summary>
    ''' PAGE LOAD
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'レスポンス（UTF-8のテキスト）
        Response.ContentType = "text/plain"
        Response.Charset = "utf-8"
        Response.ContentEncoding = System.Text.Encoding.UTF8
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetNoStore()

        Dim kbn As String = GetFormValue("kbn")

        Try
            Select Case kbn
                Case "list"
                    '一覧取得
                    Response.Write(SelTScanListJson())
                Case "ins"
                    '扫码値登録
                    Response.Write(InsTScanList())
                Case "upd"
                    '結果値更新（NG → RN）
                    Response.Write(UpdTScanListResult())
                Case Else
                    Response.Write("ERR:参数 kbn 不正确。")
            End Select
        Catch ex As Exception
            Response.Write("ERR:" & ex.Message)
        End Try

        Response.End()

    End Sub

    ''' <summary>
    ''' 扫描列表InfoをJSON形式で取得する
    ''' </summary>
    ''' <returns>JSON文字列（エラー時は"ERR:メッセージ"）</returns>
    Private Function SelTScanListJson() As String

        Dim chkNo As String = GetFormValue("chk_no")
        Dim lineId As String = GetFormValue("line_id")
        Dim makeNo As String = GetFormValue("make_no")

        '必須パラメータチェック
        Dim errorMsg As String = CheckKeyParams(chkNo, lineId, makeNo)
        If errorMsg <> "" Then
            Return "ERR:" & errorMsg
        End If

        Dim bc As New TScanListBC
        Dim dt As Data.DataTable = bc.SelTScanList(chkNo, lineId, makeNo)

        Dim sb As New StringBuilder
        sb.Append("{" & DQ & "rows" & DQ & ":[")

        For i As Integer = 0 To dt.Rows.Count - 1

            If i > 0 Then
                sb.Append(",")
            End If

            Dim rowJson As New StringBuilder
            rowJson.Append("{")
            rowJson.Append(DQ & "scan_id" & DQ & ":" & GetText(dt.Rows(i).Item("scan_id")) & ",")
            rowJson.Append(DQ & "scan_txt" & DQ & ":" & DQ & JsonEscape(GetText(dt.Rows(i).Item("scan_txt"))) & DQ & ",")
            rowJson.Append(DQ & "result" & DQ & ":" & DQ & JsonEscape(GetText(dt.Rows(i).Item("result"))) & DQ & ",")
            rowJson.Append(DQ & "upd_user" & DQ & ":" & DQ & JsonEscape(GetText(dt.Rows(i).Item("upd_user"))) & DQ & ",")
            rowJson.Append(DQ & "upd_date" & DQ & ":" & DQ & JsonEscape(GetDateText(dt.Rows(i).Item("upd_date"))) & DQ)
            rowJson.Append("}")

            sb.Append(rowJson.ToString())

        Next

        sb.Append("]}")

        Return sb.ToString()

    End Function

    ''' <summary>
    ''' 扫码値をt_scan_listへ登録する
    ''' </summary>
    ''' <returns>"OK"/"NG"（エラー時は"ERR:メッセージ"）</returns>
    Private Function InsTScanList() As String

        Dim chkNo As String = GetFormValue("chk_no")
        Dim lineId As String = GetFormValue("line_id")
        Dim makeNo As String = GetFormValue("make_no")
        Dim code As String = GetFormValue("code")
        Dim scanTxt As String = GetFormValue("scan_txt")
        Dim user As String = GetFormValue("user")

        '必須パラメータチェック
        Dim errorMsg As String = CheckKeyParams(chkNo, lineId, makeNo)
        If errorMsg <> "" Then
            Return "ERR:" & errorMsg
        End If
        If code = "" Then
            Return "ERR:URL参数 code 未指定。"
        End If
        If scanTxt = "" Then
            Return "ERR:扫码值为空。"
        End If
        If scanTxt.Length > LEN_SCAN_TXT Then
            Return "ERR:扫码值超过" & LEN_SCAN_TXT.ToString() & "字符。"
        End If

        '扫码値からコードを取り出す（例：YY-5053-MBJB Y7261118 /C → YY-5053-MBJB Y7261118）
        Dim scanCode As String = GetScanCode(scanTxt)

        'コードがURLパラメータのcodeと一致すればOK、それ以外はNG
        Dim resultValue As String = RESULT_NG
        If IsCodeMatch(scanCode, code) Then
            resultValue = RESULT_OK
        End If

        'code列にはURLパラメータのcodeを登録する（chk_no/line_id/make_noと同じ扱い）
        Dim bc As New TScanListBC
        If Not bc.InsTScanList(chkNo, lineId, makeNo, code, scanTxt, resultValue, user) Then
            Return "ERR:登录失败。"
        End If

        Return resultValue

    End Function

    ''' <summary>
    ''' 扫描列表Infoの結果値をRNへ更新する
    ''' </summary>
    ''' <returns>"true"/"false"（エラー時は"ERR:メッセージ"）</returns>
    Private Function UpdTScanListResult() As String

        Dim chkNo As String = GetFormValue("chk_no")
        Dim lineId As String = GetFormValue("line_id")
        Dim makeNo As String = GetFormValue("make_no")
        Dim scanIdText As String = GetFormValue("scan_id")
        Dim user As String = GetFormValue("user")

        '必須パラメータチェック
        Dim errorMsg As String = CheckKeyParams(chkNo, lineId, makeNo)
        If errorMsg <> "" Then
            Return "ERR:" & errorMsg
        End If

        Dim scanId As Integer = 0
        If Not Integer.TryParse(scanIdText, scanId) Then
            Return "ERR:扫描ID不正确。"
        End If

        Dim bc As New TScanListBC
        If bc.UpdTScanListResult(scanId, chkNo, lineId, makeNo, RESULT_RN, user) Then
            Return "true"
        End If

        Return "false"

    End Function

    ''' <summary>
    ''' 必須パラメータをチェックする
    ''' </summary>
    Private Function CheckKeyParams(ByVal chkNo As String, ByVal lineId As String, ByVal makeNo As String) As String
        If chkNo = "" OrElse lineId = "" OrElse makeNo = "" Then
            Return "URL参数不足（chk_no / line_id / make_no）。"
        End If
        Return ""
    End Function

    ''' <summary>
    ''' POSTパラメータを取得する
    ''' </summary>
    Private Function GetFormValue(ByVal key As String) As String
        Dim value As String = Request.Form(key)
        If value Is Nothing Then
            Return ""
        End If
        Return value.Trim()
    End Function

    ''' <summary>
    ''' 扫码値からコードを取り出す
    ''' 扫码値フォーマット：コード 製造番号 /枝番（例：YY-5053-MBJB Y7261118 /C）
    ''' →「/」で区切った1項目目がコード
    ''' </summary>
    ''' <param name="scanTxt">扫码値</param>
    ''' <returns>コード（解析できない場合は空文字）</returns>
    Private Function GetScanCode(ByVal scanTxt As String) As String

        If scanTxt Is Nothing OrElse scanTxt = "" Then
            Return ""
        End If

        Dim items() As String = scanTxt.Split("/"c)

        Return items(0).Trim()

    End Function

    ''' <summary>
    ''' コードがURLパラメータのcodeと一致するか判定する
    ''' （比較時は「-」と空白を無視し、大文字小文字も区別しない）
    ''' </summary>
    ''' <param name="scanCode">扫码値のコード</param>
    ''' <param name="urlCode">URLパラメータのcode</param>
    ''' <returns>True:一致</returns>
    Private Function IsCodeMatch(ByVal scanCode As String, ByVal urlCode As String) As Boolean

        Dim scanValue As String = NormalizeCode(scanCode)
        Dim urlValue As String = NormalizeCode(urlCode)

        If scanValue = "" OrElse urlValue = "" Then
            Return False
        End If

        '完全一致
        If scanValue = urlValue Then
            Return True
        End If

        '扫码値のコードがURLのコードで始まる場合も一致とみなす
        '（例：URLのcode=YY-5053-MBJB、扫码値のコード=YY-5053-MBJB Y7261118）
        If scanValue.Length > urlValue.Length Then
            If scanValue.Substring(0, urlValue.Length) = urlValue Then
                Return True
            End If
        End If

        Return False

    End Function

    ''' <summary>
    ''' 比較用にコードを正規化する（「-」と空白を除去し、大文字に変換する）
    ''' </summary>
    ''' <param name="value">コード</param>
    ''' <returns>正規化したコード</returns>
    Private Function NormalizeCode(ByVal value As String) As String

        If value Is Nothing OrElse value = "" Then
            Return ""
        End If

        Dim sb As New StringBuilder

        For i As Integer = 0 To value.Length - 1

            Dim c As Char = value.Chars(i)
            Dim charCode As Integer = AscW(c) And &HFFFF

            If charCode = 45 Then
                'ハイフン（-）は比較対象外
            ElseIf charCode = 65293 Then
                '全角ハイフン（－）は比較対象外
            ElseIf charCode = 8208 OrElse charCode = 8209 Then
                'ハイフン（‐ ‑）は比較対象外
            ElseIf charCode = 8210 OrElse charCode = 8211 OrElse charCode = 8212 Then
                'ダッシュ（‒ – —）は比較対象外
            ElseIf charCode = 8722 Then
                '数学記号マイナス（−）は比較対象外
            ElseIf charCode = 32 OrElse charCode = 9 OrElse charCode = 12288 Then
                '半角スペース・タブ・全角スペースは比較対象外
            Else
                sb.Append(c)
            End If

        Next

        Return sb.ToString().ToUpper()

    End Function

    ''' <summary>
    ''' 値を文字列で取得する
    ''' </summary>
    Private Function GetText(ByVal value As Object) As String
        If value Is Nothing OrElse IsDBNull(value) Then
            Return ""
        End If
        Return value.ToString()
    End Function

    ''' <summary>
    ''' 日付値を文字列で取得する
    ''' </summary>
    Private Function GetDateText(ByVal value As Object) As String
        If value Is Nothing OrElse IsDBNull(value) Then
            Return ""
        End If
        Return Convert.ToDateTime(value).ToString("yyyy/MM/dd HH:mm:ss")
    End Function

    ''' <summary>
    ''' JSON文字列をエスケープする
    ''' </summary>
    Private Function JsonEscape(ByVal value As String) As String
        If value Is Nothing OrElse value = "" Then
            Return ""
        End If

        Dim sb As New StringBuilder

        For i As Integer = 0 To value.Length - 1

            Dim c As Char = value.Chars(i)
            Dim charCode As Integer = AscW(c) And &HFFFF

            If c = CChar(DQ) Then
                'ダブルクォーテーション
                sb.Append(BACKSLASH & DQ)
            ElseIf c = "\"c Then
                'バックスラッシュ
                sb.Append(BACKSLASH & BACKSLASH)
            ElseIf charCode = 8 Then
                sb.Append(BACKSLASH & "b")
            ElseIf charCode = 9 Then
                sb.Append(BACKSLASH & "t")
            ElseIf charCode = 10 Then
                sb.Append(BACKSLASH & "n")
            ElseIf charCode = 12 Then
                sb.Append(BACKSLASH & "f")
            ElseIf charCode = 13 Then
                sb.Append(BACKSLASH & "r")
            ElseIf charCode < 32 OrElse charCode = 8232 OrElse charCode = 8233 Then
                '制御コード・改行コード
                sb.Append(BACKSLASH & "u" & charCode.ToString("x4"))
            Else
                sb.Append(c)
            End If

        Next

        Return sb.ToString()

    End Function

End Class
