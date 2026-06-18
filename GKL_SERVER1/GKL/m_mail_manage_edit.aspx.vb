Imports System.Data
Imports System.Text
Imports System.IO

Partial Class m_mail_manage_edit
    Inherits System.Web.UI.Page

    Public BC As New MToolsBC
    Public updDA As New m_email_kanriDA

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
                'tbxLineId.Enabled = False
                tbxLineId.Text = ViewState("menu_line_id")
                'tbxLineId_key.Enabled = False
                tbxLineId_key.Text = ViewState("menu_line_id")

            End If
            Me.Links.InitViewstate(ViewState("menu_line_id"), ViewState("menu_user_cd"), ViewState("menu_user_name"))


            '固定項目設定
            KoteiInit()

            '明細項目設定
            MsInit()
            tbxLineId_key.Attributes.Item("onkeydown") = "if(event.keyCode==13){event.keyCode=10;return false}"
        End If

    End Sub
    ''' <summary>
    ''' 固定項目設定
    ''' </summary>
    Public Sub KoteiInit()

        Me.tbxXi.Attributes.Item("itType") = "varchar"
        Me.tbxXi.Attributes.Item("itLength") = "20"
        Me.tbxXi.Attributes.Item("itName") = "系"

        Me.tbxLineId.Attributes.Item("itType") = "varchar"
        Me.tbxLineId.Attributes.Item("itLength") = "10"
        Me.tbxLineId.Attributes.Item("itName") = "生产线"

        Me.tbxToMail.Attributes.Item("itType") = "nvarchar"
        Me.tbxToMail.Attributes.Item("itLength") = "800"
        Me.tbxToMail.Attributes.Item("itName") = "To 邮箱地址"

        Me.tbxCCMail.Attributes.Item("itType") = "nvarchar"
        Me.tbxCCMail.Attributes.Item("itLength") = "800"
        Me.tbxCCMail.Attributes.Item("itName") = "CC 邮箱地址"

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
        Return updDA.GetMailData(tbxLineId_key.Text)
    End Function

    ''' <summary>
    ''' データ存在チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsHaveData() As Boolean
        Return updDA.GetMailData(Me.tbxLineId.Text).Rows.Count > 0
    End Function

    ''' <summary>
    ''' 更新
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnUpdate_Click(sender As Object, e As System.EventArgs) Handles btnUpdate.Click

        Dim msg As String = ChkMail()
        If msg <> "" Then
            Common.ShowMsg(Me.Page, msg)
            Exit Sub
        End If

        Try
            updDA.UpdMailData(Me.tbxXi.Text, hidLineId.Text, Me.tbxToMail.Text, Me.tbxCCMail.Text)
            'BC.UpdMTools(hidToolId.Text, hidLineId.Text, tbxToolId.Text, tbxLineId.Text, tbxProjectName.Text, tbxToolName.Text, ViewState("menu_user_cd"))
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

        Dim msg As String = ChkMail()
        If msg <> "" Then
            Common.ShowMsg(Me.Page, msg)
            Exit Sub
        End If

        Try

            updDA.InsMailData(Me.tbxXi.Text, tbxLineId.Text, Me.tbxToMail.Text, Me.tbxCCMail.Text)
            'BC.InsMTools(tbxToolId.Text, tbxLineId.Text, tbxProjectName.Text, tbxToolName.Text, ViewState("menu_user_cd"))
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
            updDA.DelMailData(hidLineId.Text)
            MsInit()
        Catch ex As Exception
            Common.ShowMsg(Me.Page, ex.Message)
            Exit Sub
        End Try
        Me.hidOldRowIdx.Text = ""
    End Sub

    Function ChkMail() As String

        Dim arr()
        arr = Me.tbxToMail.Text.Split(";"c)

        For i As Integer = 0 To arr.Length - 1
            If (arr(i).ToString.Trim() <> "") Then
                If Not EmailAddressChecker(arr(i).ToString.Trim()) Then
                    Return arr(i).ToString.Trim() & " （非法邮件地址）"
                End If
            End If
        Next
        arr = Me.tbxCCMail.Text.Split(";"c)
        For i As Integer = 0 To arr.Length - 1
            If (arr(i).ToString.Trim() <> "") Then
                If Not EmailAddressChecker(arr(i).ToString.Trim()) Then
                    Return arr(i).ToString.Trim() & " （非法邮件地址）"
                End If
            End If
        Next

        Return ""

    End Function


    Function EmailAddressChecker(ByVal emailAddress As String) As Boolean
        Dim regExPattern As String = "\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}\b"
        Dim emailAddressMatch As Match = Regex.Match(emailAddress, regExPattern)
        If emailAddressMatch.Success Then
            Return True
        Else
            Return False
        End If
    End Function
End Class
