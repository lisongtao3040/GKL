Imports System.Data
Imports System.Text
Imports System.IO

Partial Class m_temp
    Inherits System.Web.UI.Page

    Public BC As New MTempBC


    Public Function GetUserCd() As String
        Return ViewState("menu_user_cd")
    End Function


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

            newTemp.Attributes.Item("onclick") = "window.open('m_temp_name.aspx?line_id='+$('#tbxLineId_key').val()+'&menu_user_cd=" & ViewState("menu_user_cd") & "'); return false;"
            Me.Links.InitViewstate(ViewState("menu_line_id"), ViewState("menu_user_cd"), ViewState("menu_user_name"))


            '固定項目設定
            KoteiInit()

            'line_id_list.InnerHtml = Common.LineIds
      

            '明細項目設定
            'MsInit()

        End If

    End Sub
    ''' <summary>
    ''' 固定項目設定
    ''' </summary>
    public Sub KoteiInit()
      'EMAB　ＥＲＲ
       
       
       Me.tbxLineId.Attributes.Item("itType") = "varchar"
       Me.tbxLineId.Attributes.Item("itLength") = "10"
       Me.tbxLineId.Attributes.Item("itName") = "生产线"
       Me.tbxTempId.Attributes.Item("itType") = "nvarchar"
       Me.tbxTempId.Attributes.Item("itLength") = "10"
       Me.tbxTempId.Attributes.Item("itName") = "检查模板编号"
       Me.tbxChkMethodId.Attributes.Item("itType") = "varchar"
       Me.tbxChkMethodId.Attributes.Item("itLength") = "10"
       Me.tbxChkMethodId.Attributes.Item("itName") = "检查项目ID"
       Me.tbxProjectId.Attributes.Item("itType") = "varchar"
       Me.tbxProjectId.Attributes.Item("itLength") = "10"
       Me.tbxProjectId.Attributes.Item("itName") = "工程ID"
       Me.tbxProjectName.Attributes.Item("itType") = "varchar"
       Me.tbxProjectName.Attributes.Item("itLength") = "40"
       Me.tbxProjectName.Attributes.Item("itName") = "工程名"
       Me.tbxPicId.Attributes.Item("itType") = "varchar"
       Me.tbxPicId.Attributes.Item("itLength") = "10"
       Me.tbxPicId.Attributes.Item("itName") = "图片ID"
       Me.tbxPicName.Attributes.Item("itType") = "varchar"
       Me.tbxPicName.Attributes.Item("itLength") = "200"
       Me.tbxPicName.Attributes.Item("itName") = "图片名称"
       Me.tbxChkKmName.Attributes.Item("itType") = "varchar"
       Me.tbxChkKmName.Attributes.Item("itLength") = "200"
       Me.tbxChkKmName.Attributes.Item("itName") = "检查项目"
       Me.tbxPicSign.Attributes.Item("itType") = "varchar"
       Me.tbxPicSign.Attributes.Item("itLength") = "10"
       Me.tbxPicSign.Attributes.Item("itName") = "图片标记"
       Me.tbxChkId.Attributes.Item("itType") = "varchar"
       Me.tbxChkId.Attributes.Item("itLength") = "10"
       Me.tbxChkId.Attributes.Item("itName") = "检查方法ID"
       Me.tbxChkName.Attributes.Item("itType") = "varchar"
       Me.tbxChkName.Attributes.Item("itLength") = "20"
       Me.tbxChkName.Attributes.Item("itName") = "检查方法名"
       Me.tbxToolId.Attributes.Item("itType") = "varchar"
       Me.tbxToolId.Attributes.Item("itLength") = "40"
       Me.tbxToolId.Attributes.Item("itName") = "治具ID"
       Me.tbxKj0.Attributes.Item("itType") = "varchar"
       Me.tbxKj0.Attributes.Item("itLength") = "100"
       Me.tbxKj0.Attributes.Item("itName") = "基准"
       Me.tbxKj1.Attributes.Item("itType") = "varchar"
       Me.tbxKj1.Attributes.Item("itLength") = "20"
       Me.tbxKj1.Attributes.Item("itName") = "工差1"
       Me.tbxKj2.Attributes.Item("itType") = "varchar"
       Me.tbxKj2.Attributes.Item("itLength") = "20"
       Me.tbxKj2.Attributes.Item("itName") = "工差2"
       Me.tbxKjExplain.Attributes.Item("itType") = "nvarchar"
       Me.tbxKjExplain.Attributes.Item("itLength") = "200"
       Me.tbxKjExplain.Attributes.Item("itName") = "基准説明"

    End Sub

    ''' <summary>
    ''' 明細項目設定
    ''' </summary>
    public Sub MsInit()
      'EMAB　ＥＲＲ
       
       
            '明細設定
            Dim dt As DataTable = GetMsData()
            Me.gvMs.DataSource = dt
            Me.gvMs.DataBind()



    End Sub

    ''' <summary>
    ''' 検索
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSelect_Click(sender As Object, e As System.EventArgs) Handles btnSelect.Click

        If Me.tbxLineId_key.Text = "" Then
            Common.ShowMsg(Me.Page, "生产线必须输入")
            Exit Sub

        End If


        MsInit()
    End Sub

    ''' <summary>
    ''' 明細データ取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetMsData() As Data.DataTable

      'EMAB　ＥＲＲ
       
       
       Return BC.SelMTemp(tbxLineId_key.Text, tbxTempId_key.Text, tbxChkMethodId_key.Text)
    End Function

    ''' <summary>
    ''' データ存在チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsHaveData() As Boolean

      'EMAB　ＥＲＲ
       
       
       Return BC.SelMTemp(tbxLineId.Text, tbxTempId.Text, tbxChkMethodId.Text).Rows.Count > 0
    End Function

    ''' <summary>
    ''' 更新
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnUpdate_Click(sender As Object, e As System.EventArgs) Handles btnUpdate.Click


            Try
            BC.UpdMTemp(hidLineId.Text, hidTempId.Text, hidChkMethodId.Text, tbxLineId.Text, tbxTempId.Text, tbxChkMethodId.Text, tbxProjectId.Text, tbxProjectName.Text, tbxPicId.Text, tbxPicName.Text, tbxChkKmName.Text, tbxPicSign.Text, tbxChkId.Text, tbxChkName.Text, tbxToolId.Text, tbxKj0.Text, tbxKj1.Text, tbxKj2.Text, tbxKjExplain.Text, ViewState("menu_user_cd"))
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
        If tbxLineId.Text = "" Then
            Common.ShowMsg(Me.Page, "数据没有输入")
            Exit Sub

        End If
        'データ存在チェック
        If IsHaveData() Then
            Common.ShowMsg(Me.Page, "数据已经存在")
            Exit Sub
        End If


        Try
            BC.InsMTemp(tbxLineId.Text, tbxTempId.Text, tbxChkMethodId.Text, tbxProjectId.Text, tbxProjectName.Text, tbxPicId.Text, tbxPicName.Text, tbxChkKmName.Text, tbxPicSign.Text, tbxChkId.Text, tbxChkName.Text, tbxToolId.Text, tbxKj0.Text, tbxKj1.Text, tbxKj2.Text, tbxKjExplain.Text, ViewState("menu_user_cd"))
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
            BC.DelMTemp(hidLineId.Text, hidTempId.Text, hidChkMethodId.Text)
            MsInit()
        Catch ex As Exception
            Common.ShowMsg(Me.Page, ex.Message)
            Exit Sub
        End Try
        Me.hidOldRowIdx.Text = ""
    End Sub

    Protected Sub btnCopy_Click(sender As Object, e As EventArgs) Handles btnCopy.Click

        If Me.tbxLineId_key.Text.Trim = "" Then
            Common.ShowMsg(Me.Page, "请输入生产线")
            Exit Sub
        End If
        If Me.tbxTempId_key.Text.Trim = "" Then
            Common.ShowMsg(Me.Page, "请输模板编号")
            Exit Sub
        End If
        If Me.tbxTempId_new.Text.Trim = "" Then
            Common.ShowMsg(Me.Page, "请输新模板编号")
            Exit Sub
        End If

        Try
            BC.CopyTemp(tbxLineId_key.Text.Trim, Me.tbxTempId_key.Text.Trim, Me.tbxTempId_new.Text.Trim, ViewState("menu_user_cd"))


            Try
                Dim MTempNameBC As New MTempNameBC
                MTempNameBC.InsMTempName(tbxLineId_key.Text, tbxTempId_new.Text, tbxTempName_new.Text, ViewState("menu_user_cd"))
            Catch ex As Exception

            End Try

            Common.ShowMsg(Me.Page, "复制完了")
            tbxTempId_key.Text = Me.tbxTempId_new.Text.Trim
            Me.tbxTempId_new.Text = ""
            tbxTempName_new.Text = ""




            MsInit()

        Catch ex As Exception
            Common.ShowMsg(Me.Page, ex.Message)
            Exit Sub
        End Try


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
        dt.Columns.Add("temp_id")
        dt.Columns.Add("chk_method_id")
        dt.Columns.Add("project_id")
        dt.Columns.Add("project_name")
        dt.Columns.Add("pic_id")
        dt.Columns.Add("pic_name")
        dt.Columns.Add("chk_km_name")
        dt.Columns.Add("pic_sign")
        dt.Columns.Add("chk_id")
        dt.Columns.Add("chk_name")
        dt.Columns.Add("tool_id")
        dt.Columns.Add("kj_0")
        dt.Columns.Add("kj_1")
        dt.Columns.Add("kj_2")
        dt.Columns.Add("kj_explain")
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

        Dim idx As Integer = 0

        For Each str In arr

            If idx = 0 Then
                idx = idx + 1
                Continue For
            End If




            If str.Trim <> "" Then

                Dim line_id As String = str.Split(",")(0).Replace(vbCr, "").Replace(vbLf, "")

                If ViewState("menu_line_id") = "" OrElse ViewState("menu_line_id") = line_id Then

                    Dim dr As DataRow
                    dr = dt.NewRow

                    dr.Item(0) = str.Split(",")(0).Replace(vbCr, "").Replace(vbLf, "")
                    dr.Item(1) = str.Split(",")(1).Replace(vbCr, "").Replace(vbLf, "")
                    dr.Item(2) = str.Split(",")(2).Replace(vbCr, "").Replace(vbLf, "")
                    dr.Item(3) = str.Split(",")(3).Replace(vbCr, "").Replace(vbLf, "")
                    dr.Item(4) = str.Split(",")(4).Replace(vbCr, "").Replace(vbLf, "")
                    dr.Item(5) = str.Split(",")(5).Replace(vbCr, "").Replace(vbLf, "")
                    dr.Item(6) = str.Split(",")(6).Replace(vbCr, "").Replace(vbLf, "")
                    dr.Item(7) = str.Split(",")(7).Replace(vbCr, "").Replace(vbLf, "")
                    dr.Item(8) = str.Split(",")(8).Replace(vbCr, "").Replace(vbLf, "")
                    dr.Item(9) = str.Split(",")(9).Replace(vbCr, "").Replace(vbLf, "")
                    dr.Item(10) = str.Split(",")(10).Replace(vbCr, "").Replace(vbLf, "")
                    dr.Item(11) = str.Split(",")(11).Replace(vbCr, "").Replace(vbLf, "")
                    dr.Item(12) = str.Split(",")(12).Replace(vbCr, "").Replace(vbLf, "")
                    dr.Item(13) = str.Split(",")(13).Replace(vbCr, "").Replace(vbLf, "")
                    dr.Item(14) = str.Split(",")(14).Replace(vbCr, "").Replace(vbLf, "")
                    dr.Item(15) = str.Split(",")(15).Replace(vbCr, "").Replace(vbLf, "")


                    dr.Item("ins_user") = ViewState("menu_user_cd")
                    dr.Item("ins_date") = Now
                    dr.Item("upd_user") = ViewState("menu_user_cd")
                    dr.Item("upd_date") = Now


                    dt.Rows.Add(dr)

                End If



            End If
        Next

        Try



            If dt.Rows.Count > 0 Then

                '戻りデータセット
                'Dim dsInfo As New Data.DataSet

                Using conn As New SqlClient.SqlConnection(DataAccessManager.Connection)

                    conn.Open()

                    'Dim sqlTransaction As SqlTransaction = conn.BeginTransaction()

                    Dim sql As String
                    'sql = "TRUNCATE TABLE #t_cd_temp_relation_junbi"
                    sql = "SELECT * INTO #t_temp_junbi FROM m_temp WHERE 1<>1"
                    Dim SqlCommand As System.Data.SqlClient.SqlCommand = conn.CreateCommand()
                    SqlCommand.CommandText = sql
                    SqlCommand.CommandTimeout = 0
                    'SqlCommand.Transaction = sqlTransaction
                    SqlCommand.ExecuteNonQuery()



                    Dim bCopy As New SqlClient.SqlBulkCopy(conn)
                    bCopy.BulkCopyTimeout = 30
                    bCopy.DestinationTableName = "#t_temp_junbi"
                    bCopy.WriteToServer(dt)

                    'Dim sb2 As New StringBuilder
                    'With sb2
                    '    .AppendLine("SELECT Distinct temp_id")
                    '    .AppendLine("FROM #t_temp_junbi ")
                    '    .AppendLine("WHERE temp_id not in (select temp_id from m_temp_name)")
                    'End With

                    'Dim dataAdatpter As System.Data.SqlClient.SqlDataAdapter = Nothing
                    'Dim command As System.Data.SqlClient.SqlCommand = conn.CreateCommand()
                    'command = New System.Data.SqlClient.SqlCommand(sb2.ToString(), conn)
                    'command.CommandTimeout = 320
                    ''実行
                    'dataAdatpter = New System.Data.SqlClient.SqlDataAdapter(command)
                    'dataAdatpter.Fill(dsInfo)
                    'If dsInfo.Tables(0).Rows.Count > 0 Then
                    'Else
                    Dim sb As New StringBuilder
                    With sb
                        .AppendLine("DELETE")
                        .AppendLine("FROM m_temp ")
                        .AppendLine("WHERE  ")
                        .AppendLine("EXISTS (")
                        .AppendLine("               SELECT 1")
                        .AppendLine("               FROM   #t_temp_junbi t")
                        .AppendLine("               WHERE  m_temp.line_id = t.line_id")
                        .AppendLine("                      AND m_temp.temp_id = t.temp_id")
                        .AppendLine("           )")
                        .AppendLine("")
                        .AppendLine("INSERT INTO m_temp SELECT * FROM #t_temp_junbi")
                    End With
                    SqlCommand.Dispose()
                    Dim SqlCommand2 As System.Data.SqlClient.SqlCommand = conn.CreateCommand()
                    SqlCommand2.CommandText = sb.ToString
                    'SqlCommand2.Transaction = sqlTransaction
                    SqlCommand2.CommandTimeout = 0
                    SqlCommand2.ExecuteNonQuery()
                    SqlCommand2.Dispose()
                    'End If

                    'sqlTransaction.Commit()

                End Using

                'If dsInfo.Tables(0).Rows.Count > 0 Then

                '    Dim sbMsg As New StringBuilder

                '    For i As Integer = 0 To dsInfo.Tables(0).Rows.Count - 1
                '        If i > 0 Then
                '            sbMsg.Append(",")
                '        End If
                '        sbMsg.Append(dsInfo.Tables(0).Rows(i).Item(0).ToString)
                '    Next

                '    Common.ShowMsg(Me.Page, "模板不存在" & sbMsg.ToString)
                '    Exit Sub
                'Else
                '    Common.ShowMsg(Me.Page, "导入的数据" & dt.Rows.Count & "件")
                'End If

            Else
                Common.ShowMsg(Me.Page, "没有导入的数据")
                Exit Sub
            End If
        Catch ex As Exception
            Me.lblMsg.Text = ex.Message
            Common.ShowMsg(Me.Page, ex.Message)
        End Try

        Common.ShowMsg(Me.Page, "导入完了")

    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Upload()
    End Sub
End Class
