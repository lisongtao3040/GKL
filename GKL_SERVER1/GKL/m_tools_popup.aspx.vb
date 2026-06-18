Imports System.Data
Imports System.Text
Imports System.IO

Partial Class m_tools
    Inherits System.Web.UI.Page

   Public BC AS NEW MToolsBC
    ''' <summary>
    ''' PAGE LOAD
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not IsPostBack Then

            ViewState("line_id") = Request.QueryString("line_id")

            Me.tool_id.Text = Request.QueryString("tool_id")
            Me.tool_name_id.Text = Request.QueryString("tool_name_id")

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


    End Sub

    ''' <summary>
    ''' 明細項目設定
    ''' </summary>
    public Sub MsInit()

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


        Return BC.SelMTools("", ViewState("line_id").ToString)
    End Function

    ''' <summary>
    ''' データ存在チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsHaveData() As Boolean
        Return True
    End Function

End Class
