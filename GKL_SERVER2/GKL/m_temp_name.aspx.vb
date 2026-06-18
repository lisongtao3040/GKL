Imports System.Data
Imports System.Text
Imports System.IO

Partial Class m_temp_name
    Inherits System.Web.UI.Page

   Public BC AS NEW MTempNameBC
    ''' <summary>
    ''' PAGE LOAD
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Me.lblMsg.Text = ""
        If Not IsPostBack Then

            If Request.QueryString("line_id") <> "" Then

                Me.tbxLineId.Text = Request.QueryString("line_id")
                Me.tbxLineId_key.Text = Request.QueryString("line_id")
                tbxLineId.Enabled = False
                tbxLineId_key.Enabled = False
            End If


            ViewState("menu_user_cd") = Request.QueryString("menu_user_cd")


            '固定項目設定
            KoteiInit()

            '明細項目設定
            'MsInit()
        End If

    End Sub
    ''' <summary>
    ''' 固定項目設定
    ''' </summary>
    public Sub KoteiInit()

        Me.tbxLineId.Attributes.Item("itType") = "varchar"
        Me.tbxLineId.Attributes.Item("itLength") = "10"
        Me.tbxLineId.Attributes.Item("itName") = "生产线"
        Me.tbxTempId.Attributes.Item("itType") = "varchar"
        Me.tbxTempId.Attributes.Item("itLength") = "10"
        Me.tbxTempId.Attributes.Item("itName") = "检查模板编号"
        Me.tbxTempName.Attributes.Item("itType") = "nvarchar"
        Me.tbxTempName.Attributes.Item("itLength") = "200"
        Me.tbxTempName.Attributes.Item("itName") = "模板名称"

    End Sub

    ''' <summary>
    ''' 明細項目設定
    ''' </summary>
    Public Sub MsInit()

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

        MsInit()
    End Sub

    ''' <summary>
    ''' 明細データ取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetMsData() As Data.DataTable


        Return BC.SelMTempName(tbxLineId_key.Text, tbxTempId_key.Text)
    End Function

    ''' <summary>
    ''' データ存在チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsHaveData() As Boolean

        Return BC.SelMTempName(tbxLineId.Text, tbxTempId.Text).Rows.Count > 0
    End Function

    ''' <summary>
    ''' 更新
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnUpdate_Click(sender As Object, e As System.EventArgs) Handles btnUpdate.Click


        Try
            BC.UpdMTempName(hidLineId.Text, hidTempId.Text, tbxLineId.Text, tbxTempId.Text, tbxTempName.Text, ViewState("menu_user_cd"))
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
            Common.ShowMsg(Me.Page, "データ存在しました。")
            Exit Sub
        End If
        Try
            BC.InsMTempName(tbxLineId.Text, tbxTempId.Text, tbxTempName.Text, ViewState("menu_user_cd"))
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
            BC.DelMTempName(hidlineId.Text, hidtempId.Text)
            MsInit()
        Catch ex As Exception
            Common.ShowMsg(Me.Page, ex.Message)
            Exit Sub
        End Try
        Me.hidOldRowIdx.Text = ""
    End Sub

End Class
