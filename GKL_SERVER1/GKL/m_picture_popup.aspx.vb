Imports System.Data
Imports System.Text
Imports System.IO
Partial Class m_picture
    Inherits System.Web.UI.Page

    Public BC As New MPictureBC
    ''' <summary>
    ''' PAGE LOAD
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        ViewState("pic_id") = Request.QueryString("pic_id")
        ViewState("pic_name_id") = Request.QueryString("pic_name_id")
        ViewState("line") = Request.QueryString("line_id")

        Me.hidPrePicId.Text = Request.QueryString("pic_id")
        Me.hidPrePicName.Text = Request.QueryString("pic_name_id")

        If Not IsPostBack Then

            '固定項目設定
            KoteiInit()

            '明細項目設定
            MsInit()
        End If

    End Sub
    ''' <summary>
    ''' 固定項目設定
    ''' </summary>
    Public Sub KoteiInit()
        'EMAB　ＥＲＲ



    End Sub

    ''' <summary>
    ''' 明細項目設定
    ''' </summary>
    Public Sub MsInit()
        'EMAB　ＥＲＲ


        '明細設定
        Dim dt As DataTable = GetMsData()
        Me.gvMs.DataSource = dt
        Me.gvMs.DataBind()

        For i As Integer = 0 To dt.Rows.Count - 1

            Dim pic_id As String = dt.Rows(i).Item("pic_id").ToString
            Dim line_id As String = dt.Rows(i).Item("line_id").ToString


            Dim imgLook As System.Web.UI.WebControls.Image
            imgLook = CType(gvMs.Rows(i).FindControl("imgLook"), System.Web.UI.WebControls.Image)
            imgLook.ImageUrl = "Img.aspx?pic_id=" & pic_id & "&line_id=" & line_id

        Next


    End Sub

    ''' <summary>
    ''' 明細データ取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetMsData() As Data.DataTable

        'EMAB　ＥＲＲ


        Return BC.SelMPicture("", ViewState("line").ToString)
    End Function



End Class
