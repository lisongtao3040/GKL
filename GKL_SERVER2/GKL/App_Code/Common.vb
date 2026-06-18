Imports Microsoft.VisualBasic

Public Class Common

    Public Shared YXLD_INFOS As New System.Collections.Generic.Dictionary(Of String, String)

    ''' <summary>
    ''' Message
    ''' </summary>
    ''' <param name="page"></param>
    ''' <param name="msg"></param>
    ''' <remarks></remarks>
    Public Shared Sub ShowMsg(ByVal page As Page, ByVal msg As String)

        Dim csScript As New StringBuilder

        With csScript
            .AppendLine("alert('" & msg & "');")
        End With

        'ページ応答で、クライアント側のスクリプト ブロックを出力します
        page.ClientScript.RegisterStartupScript(page.GetType(), "ShowMessage", csScript.ToString, True)

    End Sub

    ''' <summary>
    ''' 1 Page data
    ''' </summary>
    ''' <param name="inDt"></param>
    ''' <param name="pageIdx"></param>
    ''' <param name="outDt"></param>
    ''' <param name="pageListDt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetPageData(ByVal inDt As Data.DataTable, ByVal pageIdx As Integer, ByRef outDt As Data.DataTable, ByRef pageListDt As Data.DataTable) As Data.DataTable

        Dim onePageRowCnt As Integer = 100
        Dim mxPageIdx As Integer = Math.Ceiling(inDt.Rows.Count / onePageRowCnt)

        Dim dt As Data.DataTable = inDt.Clone
        For i As Integer = (pageIdx - 1) * onePageRowCnt To (pageIdx) * onePageRowCnt - 1
            If i < inDt.Rows.Count Then
                dt.Rows.Add(inDt.Rows(i).ItemArray)
            End If

        Next
        outDt = dt

        Dim dt2 As New Data.DataTable
        dt2.Columns.Add("idx")
        For i As Integer = 1 To mxPageIdx
            Dim dr As Data.DataRow = dt2.NewRow
            dr.Item(0) = i.ToString
            dt2.Rows.Add(dr)
        Next
        pageListDt = dt2
        Return Nothing
    End Function

    ''' <summary>
    ''' Set title text
    ''' </summary>
    ''' <param name="txt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function SetTitle(ByVal txt As String) As String

        Dim str As String
        str = "检查系统 【" & txt & "】"
        Return str

    End Function

    ''' <summary>
    ''' list
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function LineIds() As String
        Dim dt As Data.DataTable = (New MUserBC).SelLineIds()
        Dim sb As New StringBuilder
        For i As Integer = 0 To dt.Rows.Count - 1
            sb.AppendLine("<option value=""" & dt.Rows(i).Item(0).ToString & """></option>")
        Next
        Return sb.ToString
    End Function

    ''' <summary>
    ''' user list
    ''' </summary>
    ''' <param name="line_id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function SelUserlist(Optional ByVal line_id As String = "") As String
        Dim dt As Data.DataTable = (New MUserBC).SelUserlist(line_id)
        Dim sb As New StringBuilder
        For i As Integer = 0 To dt.Rows.Count - 1
            sb.AppendLine("<option value=""" & dt.Rows(i).Item(0).ToString & """></option>")
        Next
        Return sb.ToString
    End Function

    Public Shared Function TempIds(ByVal line_id As String) As String
        Dim dt As Data.DataTable = (New MTempBC).SelTempIds(line_id)
        Dim sb As New StringBuilder
        For i As Integer = 0 To dt.Rows.Count - 1
            sb.AppendLine("<option value=""" & dt.Rows(i).Item(0).ToString & """></option>")
        Next
        Return sb.ToString
    End Function


    Public Shared Function SelColor(ByVal goodcd As String, ByVal toolcd As String, ByVal linecd As String) As String
        'Dim dt As Data.DataTable = (New MUserBC).SelUserlist(line_id)
        'Dim sb As New StringBuilder
        'For i As Integer = 0 To dt.Rows.Count - 1
        '    sb.AppendLine("<option value=""" & dt.Rows(i).Item(0).ToString & """></option>")
        'Next
        'Return sb.ToString

        Dim dt As Data.DataTable = (New TCdTempRelationBC).SelTCdTempRelation(linecd.Trim, goodcd.Trim, "")

        Dim dt2 As Data.DataTable = (New MToolsBC).SelMTools(toolcd.Trim, linecd.Trim)

        Dim toolTxt As String = ""
        Dim toolms_txt As String = ""

        If dt.Rows.Count > 0 Then
            toolTxt = NullToEmpty(dt.Rows(0).Item("color_nm"))
        End If
        If dt2.Rows.Count > 0 Then
            toolms_txt = NullToEmpty(dt2.Rows(0).Item("tool_name"))
        End If

        If toolTxt.Trim = "" Then
            Return "没有设置商品关联的颜色"
        End If

        If toolms_txt.Trim = "" Then
            Return "没有设置治具关联的颜色"
        End If

        If toolTxt.Trim.ToLower = toolms_txt.Trim.ToLower Then
            Return "true"
        Else
            Return "false"
        End If

    End Function


    ''检查中间材
    'Public Shared Function ChkZhongjiancai(ByVal linecd As String, ByVal goodcd As String, ByVal zjcCd As String) As String
    '    '关联情报取得
    '    Dim dt As Data.DataTable = (New TCdTempRelationBC).SelTCdTempRelation(linecd.Trim, goodcd.Trim, "")

    '    Dim 
    '    If dt.Rows.Count > 0 Then
    '        toolTxt = NullToEmpty(dt.Rows(0).Item("color_nm"))
    '    End If


    '    'Dim dt2 As Data.DataTable = (New MToolsBC).SelMTools(toolcd.Trim, linecd.Trim)

    '    Dim toolTxt As String = ""
    '    Dim toolms_txt As String = ""

    '    If dt.Rows.Count > 0 Then
    '        toolTxt = NullToEmpty(dt.Rows(0).Item("color_nm"))
    '    End If
    '    If dt2.Rows.Count > 0 Then
    '        toolms_txt = NullToEmpty(dt2.Rows(0).Item("tool_name"))
    '    End If

    '    If toolTxt.Trim = "" Then
    '        Return "没有设置商品关联的颜色"
    '    End If

    '    If toolms_txt.Trim = "" Then
    '        Return "没有设置治具关联的颜色"
    '    End If

    '    If toolTxt.Trim.ToLower = toolms_txt.Trim.ToLower Then
    '        Return "true"
    '    Else
    '        Return "false"
    '    End If

    'End Function
    Public Shared Function AutoSetColor(ByVal line_cd As String, ByVal make_no As String, ByVal goodcd As String, ByVal user As String) As String

        Dim color As String = ""
        Dim dt As Data.DataTable = (New TCdTempRelationBC).SelTCdTempRelation(line_cd.Trim, goodcd.Trim, "")
        If dt.Rows.Count > 0 Then
            color = NullToEmpty(dt.Rows(0).Item("color_nm"))
        Else
            Return "没有设置商品关联的颜色"
        End If


        Return (New TCdTempRelationBC).Inst_colorcheck_resultLastCopy(line_cd.Trim, make_no.Trim, color.Trim, user)
        'Dim dt As Data.DataTable = (New MUserBC).SelUserlist(line_id)
        'Dim sb As New StringBuilder
        'For i As Integer = 0 To dt.Rows.Count - 1
        '    sb.AppendLine("<option value=""" & dt.Rows(i).Item(0).ToString & """></option>")
        'Next
        'Return sb.ToString

        'Dim dt As Data.DataTable = (New TCdTempRelationBC).SelTCdTempRelation(linecd.Trim, goodcd.Trim, "")

        'Dim dt2 As Data.DataTable = (New MToolsBC).SelMTools(toolcd.Trim, linecd.Trim)

        'Dim toolTxt As String = ""
        'Dim toolms_txt As String = ""

        'If dt.Rows.Count > 0 Then
        '    toolTxt = NullToEmpty(dt.Rows(0).Item("color_nm"))
        'End If
        'If dt2.Rows.Count > 0 Then
        '    toolms_txt = NullToEmpty(dt2.Rows(0).Item("tool_name"))
        'End If

        'If toolTxt.Trim = "" Then
        '    Return "没有设置商品关联的颜色"
        'End If

        'If toolms_txt.Trim = "" Then
        '    Return "没有设置治具关联的颜色"
        'End If

        'If toolTxt.Trim.ToLower = toolms_txt.Trim.ToLower Then
        '    Return "true"
        'Else
        '    Return "false"
        'End If

    End Function


    ''' <summary>
    ''' NullToEmpty
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function NullToEmpty(ByVal obj As Object) As String
        If obj Is DBNull.Value Then
            Return ""
        ElseIf obj Is Nothing Then
            Return ""
        Else
            Return obj.ToString
        End If
    End Function

End Class
