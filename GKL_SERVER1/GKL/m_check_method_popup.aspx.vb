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



        If Not IsPostBack Then

            'ViewState("method_id") = Request.QueryString("method_id")
            'ViewState("method_name_id") = Request.QueryString("method_name_id")

            Me.method_id.Text = Request.QueryString("method_id")
            Me.method_name_id.Text = Request.QueryString("method_name_id")

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
    ''' 明細データ取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetMsData() As Data.DataTable

      'EMAB　ＥＲＲ
       
       
        Return BC.SelMCheckMethod("", "")
    End Function

    ''' <summary>
    ''' データ存在チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsHaveData() As Boolean

      'EMAB　ＥＲＲ
       
       
        'Return BC.SelMCheckMethod(tbxChkId.Text, tbxChkName.Text).Rows.Count > 0
    End Function

End Class
