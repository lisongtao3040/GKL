Imports System.Data
Imports System.Text
Imports System.IO

Partial Class m_check_method
    Inherits System.Web.UI.Page

   Public BC AS NEW MCheckMethodBC
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
            End If
            Me.Links.InitViewstate(ViewState("menu_line_id"), ViewState("menu_user_cd"), ViewState("menu_user_name"))


            '固定項目設定
            KoteiInit()

            '明細項目設定
            MsInit()
        End If

    End Sub
    ''' <summary>
    ''' 固定項目設定
    ''' </summary>
    public Sub KoteiInit()
      'EMAB　ＥＲＲ
       
       
       Me.tbxChkId.Attributes.Item("itType") = "varchar"
       Me.tbxChkId.Attributes.Item("itLength") = "10"
       Me.tbxChkId.Attributes.Item("itName") = "检查方法ID"
       Me.tbxChkName.Attributes.Item("itType") = "nvarchar"
       Me.tbxChkName.Attributes.Item("itLength") = "20"
       Me.tbxChkName.Attributes.Item("itName") = "检查方法名"
       Me.tbxChkMethod.Attributes.Item("itType") = "nvarchar"
       Me.tbxChkMethod.Attributes.Item("itLength") = "1"
       Me.tbxChkMethod.Attributes.Item("itName") = "检查方法"
       Me.tbxChkFormula.Attributes.Item("itType") = "nvarchar"
       Me.tbxChkFormula.Attributes.Item("itLength") = "80"
       Me.tbxChkFormula.Attributes.Item("itName") = "检查方公式"
       Me.tbxVerifyMethodExplain.Attributes.Item("itType") = "nvarchar"
       Me.tbxVerifyMethodExplain.Attributes.Item("itLength") = "200"
       Me.tbxVerifyMethodExplain.Attributes.Item("itName") = "核对方法说明"

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

        MsInit()
    End Sub

    ''' <summary>
    ''' 明細データ取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetMsData() As Data.DataTable

      'EMAB　ＥＲＲ
       
       
       Return BC.SelMCheckMethod(tbxChkId_key.Text, tbxChkName_key.Text)
    End Function

    ''' <summary>
    ''' データ存在チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsHaveData() As Boolean

      'EMAB　ＥＲＲ
       
       
       Return BC.SelMCheckMethod(tbxChkId.Text, tbxChkName.Text).Rows.Count > 0
    End Function

    ''' <summary>
    ''' 更新
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnUpdate_Click(sender As Object, e As System.EventArgs) Handles btnUpdate.Click


            Try
       BC.UpdMCheckMethod(hidchkId.Text, hidchkName.Text,tbxchkId.Text, tbxchkName.Text, tbxchkMethod.Text, tbxchkFormula.Text, tbxverifyMethodExplain.Text)
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
            BC.InsMCheckMethod(tbxchkId.Text, tbxchkName.Text, tbxchkMethod.Text, tbxchkFormula.Text, tbxverifyMethodExplain.Text)
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
       BC.DelMCheckMethod(hidchkId.Text, hidchkName.Text)
        MsInit()
            Catch ex As Exception
                Common.ShowMsg(Me.Page, ex.Message)
                Exit Sub
            End Try
Me.hidOldRowIdx.Text = ""
    End Sub


End Class
