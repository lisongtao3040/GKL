Imports System.Data
Imports System.Text
Imports System.IO
Imports System.Data.SqlClient

Partial Class t_cd_temp_relation
    Inherits System.Web.UI.Page

    Public BC As New TCdTempRelationBC

    ''' <summary>
    ''' PAGE LOAD
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '加载参数
        Cparam.SetCommonViewState(IsPostBack, Me.ViewState, Me.Context)

        Me.lblMsg.Text = ""
        If Not IsPostBack Then


            ViewState("menu_line_id") = Common.NullToEmpty(Context.Items("menu_line_id"))
            ViewState("menu_user_cd") = Common.NullToEmpty(Context.Items("menu_user_cd"))
            ViewState("menu_user_name") = Common.NullToEmpty(Context.Items("menu_user_name"))

            If ViewState("menu_user_cd") = "admin" Then
            Else
                tbxLineId.Enabled = False
                tbxLineId.Text = ViewState("menu_line_id")
                tbxLineId_key.Enabled = False
                tbxLineId_key.Text = ViewState("menu_line_id")

            End If
            Me.Links.InitViewstate(ViewState("menu_line_id"), ViewState("menu_user_cd"), ViewState("menu_user_name"))


            '固定項目設定
            KoteiInit()

        End If

    End Sub
    ''' <summary>
    ''' 固定項目設定
    ''' </summary>
    Public Sub KoteiInit()

        Me.tbxLineId.Attributes.Item("itType") = "varchar"
        Me.tbxLineId.Attributes.Item("itLength") = "10"
        Me.tbxLineId.Attributes.Item("itName") = "line_id"
        Me.tbxCode.Attributes.Item("itType") = "varchar"
        Me.tbxCode.Attributes.Item("itLength") = "20"
        Me.tbxCode.Attributes.Item("itName") = "code"
        Me.tbxTempId.Attributes.Item("itType") = "varchar"
        Me.tbxTempId.Attributes.Item("itLength") = "10"
        Me.tbxTempId.Attributes.Item("itName") = "temp_id"

    End Sub

    ''' <summary>
    ''' 明細項目設定
    ''' </summary>
    Public Sub MsInit(Optional ByVal pageIdx As Integer = 1)

        '明細設定

        Dim dtMs As New DataTable
        Dim dtPageIdx As New DataTable
        Common.GetPageData(GetMsData(), pageIdx, dtMs, dtPageIdx)

        Me.gvMs.DataSource = dtMs
        Me.gvMs.DataBind()

        ddlPageIdx.DataValueField = "idx"
        ddlPageIdx.DataTextField = "idx"
        Me.ddlPageIdx.DataSource = dtPageIdx
        Me.ddlPageIdx.DataBind()

        lblAllPageText.Text = ddlPageIdx.Items.Count

        If ddlPageIdx.Items.Count > pageIdx Then
            ddlPageIdx.SelectedIndex = pageIdx - 1
        End If
    End Sub

    ''' <summary>
    ''' 検索
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSelect_Click(sender As Object, e As System.EventArgs) Handles btnSelect.Click

        If tbxLineId_key.Text.Trim = "" AndAlso tbxTempId_key.Text.Trim = "" Then
            Common.ShowMsg(Me.Page, "生产线/模板ID 至少输入一个")
            Exit Sub
        End If

        'If tbxTempId_key.Text.Trim = "" Then
        '    Common.ShowMsg(Me.Page, "请输入模板ID")
        '    Exit Sub
        'End If

        MsInit()
    End Sub

    ''' <summary>
    ''' 明細データ取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetMsData() As Data.DataTable


        Return BC.SelTCdTempRelation(tbxLineId_key.Text, tbxCode_key.Text, tbxTempId_key.Text)
    End Function

    ''' <summary>
    ''' データ存在チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsHaveData() As Boolean


        Return BC.SelTCdTempRelation(tbxLineId.Text, tbxCode.Text, tbxTempId.Text).Rows.Count > 0
    End Function

    ''' <summary>
    ''' 更新
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnUpdate_Click(sender As Object, e As System.EventArgs) Handles btnUpdate.Click


        Try
            BC.UpdTCdTempRelation(hidLineId.Text, hidCode.Text, hidTempId.Text, tbxLineId.Text, tbxCode.Text, tbxTempId.Text, Me.tbxColor.Text, ViewState("menu_user_cd"))
            MsInit()
        Catch ex As Exception
            Common.ShowMsg(Me.Page, ex.Message)
            Exit Sub
        End Try
        Me.hidOldRowIdx.Text = ""
    End Sub
    ''' <summary>
    ''' 登録
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnInsert_Click(sender As Object, e As System.EventArgs) Handles btnInsert.Click

        'データ存在チェック
        If IsHaveData() Then
            Common.ShowMsg(Me.Page, "数据已经存在")
            Exit Sub
        End If
        Try
            BC.InsTCdTempRelation(tbxLineId.Text, tbxCode.Text, tbxTempId.Text, Me.tbxColor.Text, ViewState("menu_user_cd"))
            MsInit()
        Catch ex As Exception
            Common.ShowMsg(Me.Page, ex.Message)
            Exit Sub
        End Try
        Me.hidOldRowIdx.Text = ""
    End Sub

    ''' <summary>
    ''' 削除
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnDelete_Click(sender As Object, e As System.EventArgs) Handles btnDelete.Click


        Try
            BC.DelTCdTempRelation(hidLineId.Text, hidCode.Text, hidTempId.Text)
            MsInit()
        Catch ex As Exception
            Common.ShowMsg(Me.Page, ex.Message)
            Exit Sub
        End Try
        Me.hidOldRowIdx.Text = ""
    End Sub



    Protected Sub Upload()
        If GetUploadFileContent.PostedFile.InputStream.Length < 1 Then
            'Msg.Text = "请选择文件"

            Common.ShowMsg(Me.Page, "请选择文件")
            Exit Sub

            Return
        End If

        Dim FileName As String = GetUploadFileContent.FileName
        Dim FilePath As String = GetUploadFileContent.PostedFile.FileName

        'If FileName.ToLower().IndexOf(".htm") = -1 Then
        '    'Msg.Text = "请选择要求的类型的文件"
        '    Return
        'End If

        Dim dt As New DataTable
        dt.Columns.Add("line_id")
        dt.Columns.Add("code")
        dt.Columns.Add("temp_id")
        dt.Columns.Add("color_nm")

        dt.Columns.Add("ins_user")
        dt.Columns.Add("ins_date")
        dt.Columns.Add("upd_user")
        dt.Columns.Add("upd_date")

        dt.Columns.Item("ins_date").DataType = GetType(Date)
        dt.Columns.Item("upd_date").DataType = GetType(Date)

        Dim arr() As String

        Dim FileLen As Integer = GetUploadFileContent.PostedFile.ContentLength
        Dim input As Byte() = New Byte(FileLen - 1) {}
        Dim UpLoadStream As System.IO.Stream = GetUploadFileContent.PostedFile.InputStream
        UpLoadStream.Read(input, 0, FileLen)
        UpLoadStream.Position = 0
        Dim sr As System.IO.StreamReader = New System.IO.StreamReader(UpLoadStream, System.Text.Encoding.UTF8)
        'Msg.Text = "您上传的文件内容是：<br/><br/>" & sr.ReadToEnd()

        arr = sr.ReadToEnd.Split(vbNewLine)


        sr.Close()
        UpLoadStream.Close()
        UpLoadStream = Nothing
        sr = Nothing

        Dim str As String

        For Each str In arr
            If str.Trim <> "" Then

                Dim line_id As String = str.Split(",")(0).Replace(vbCr, "").Replace(vbLf, "")

                If ViewState("menu_line_id") = "" OrElse ViewState("menu_line_id") = line_id Then


                    Dim dr As DataRow
                    dr = dt.NewRow

                    dr.Item(0) = str.Split(",")(0).Replace(vbCr, "").Replace(vbLf, "")
                    dr.Item(1) = str.Split(",")(1).Replace(vbCr, "").Replace(vbLf, "")
                    dr.Item(2) = str.Split(",")(2).Replace(vbCr, "").Replace(vbLf, "")
                    dr.Item(3) = str.Split(",")(3).Replace(vbCr, "").Replace(vbLf, "")
                    dr.Item(4) = ViewState("menu_user_cd")
                    dr.Item(5) = Now
                    dr.Item(6) = ViewState("menu_user_cd")
                    dr.Item(7) = Now

                    dt.Rows.Add(dr)
                End If

            End If
        Next

        Try



            If dt.Rows.Count > 0 Then

                '戻りデータセット
                Dim dsInfo As New Data.DataSet

                Using conn As New SqlClient.SqlConnection(DataAccessManager.Connection)

                    conn.Open()

                    'Dim sqlTransaction As SqlTransaction = conn.BeginTransaction()

                    Dim sql As String
                    'sql = "TRUNCATE TABLE #t_cd_temp_relation_junbi"
                    sql = "SELECT * INTO #t_cd_temp_relation_junbi FROM t_cd_temp_relation WHERE 1<>1"
                    Dim SqlCommand As System.Data.SqlClient.SqlCommand = conn.CreateCommand()
                    SqlCommand.CommandText = sql
                    SqlCommand.CommandTimeout = 0
                    'SqlCommand.Transaction = sqlTransaction
                    SqlCommand.ExecuteNonQuery()



                    Dim bCopy As New SqlClient.SqlBulkCopy(conn)
                    bCopy.BulkCopyTimeout = 30
                    bCopy.DestinationTableName = "#t_cd_temp_relation_junbi"
                    bCopy.WriteToServer(dt)

                    Dim sb2 As New StringBuilder
                    With sb2
                        .AppendLine("SELECT Distinct temp_id")
                        .AppendLine("FROM #t_cd_temp_relation_junbi ")
                        .AppendLine("WHERE temp_id not in (select temp_id from m_temp_name)")
                    End With

                    Dim dataAdatpter As System.Data.SqlClient.SqlDataAdapter = Nothing
                    Dim command As System.Data.SqlClient.SqlCommand = conn.CreateCommand()
                    command = New System.Data.SqlClient.SqlCommand(sb2.ToString(), conn)
                    command.CommandTimeout = 320
                    '実行
                    dataAdatpter = New System.Data.SqlClient.SqlDataAdapter(command)
                    dataAdatpter.Fill(dsInfo)
                    If dsInfo.Tables(0).Rows.Count > 0 Then
                    Else
                        Dim sb As New StringBuilder
                        With sb
                            .AppendLine("DELETE")
                            .AppendLine("FROM t_cd_temp_relation ")
                            .AppendLine("WHERE  ")
                            .AppendLine("EXISTS (")
                            .AppendLine("               SELECT 1")
                            .AppendLine("               FROM   #t_cd_temp_relation_junbi t")
                            .AppendLine("               WHERE  t_cd_temp_relation.line_id = t.line_id")
                            .AppendLine("                      AND t_cd_temp_relation.code = t.code")
                            '.AppendLine("                      AND t_cd_temp_relation.temp_id = t.temp_id")
                            .AppendLine("           )")
                            .AppendLine("")
                            .AppendLine("INSERT INTO t_cd_temp_relation SELECT * FROM #t_cd_temp_relation_junbi")
                        End With
                        SqlCommand.Dispose()
                        Dim SqlCommand2 As System.Data.SqlClient.SqlCommand = conn.CreateCommand()
                        SqlCommand2.CommandText = sb.ToString
                        'SqlCommand2.Transaction = sqlTransaction
                        SqlCommand2.CommandTimeout = 0
                        SqlCommand2.ExecuteNonQuery()
                        SqlCommand2.Dispose()
                    End If

                    'sqlTransaction.Commit()

                End Using

                If dsInfo.Tables(0).Rows.Count > 0 Then

                    Dim sbMsg As New StringBuilder

                    For i As Integer = 0 To dsInfo.Tables(0).Rows.Count - 1
                        If i > 0 Then
                            sbMsg.Append(",")
                        End If
                        sbMsg.Append(dsInfo.Tables(0).Rows(i).Item(0).ToString)
                    Next

                    Common.ShowMsg(Me.Page, "模板不存在" & sbMsg.ToString)
                    Exit Sub
                Else
                    Common.ShowMsg(Me.Page, "导入的数据" & dt.Rows.Count & "件")
                End If

            Else
                Common.ShowMsg(Me.Page, "没有导入的数据")
                Exit Sub
            End If
        Catch ex As Exception
            Me.lblMsg.Text = ex.Message
            Common.ShowMsg(Me.Page, ex.Message)
        End Try


    End Sub


    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Upload()
    End Sub

    Protected Sub ddlPageIdx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPageIdx.SelectedIndexChanged
        MsInit(Me.ddlPageIdx.SelectedValue)
    End Sub

End Class
