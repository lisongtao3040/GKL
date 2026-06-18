Imports System.Web
Public Class IISHandler1
    Implements IHttpHandler

    ''' <summary>
    '''  ハンドラーを使用するには、Web の Web.config ファイルでこの 
    '''  ハンドラーを設定し、IIS に登録する必要があります。詳細については、
    '''  次のリンクを参照してください: http://go.microsoft.com/?linkid=8101007
    ''' </summary>
#Region "IHttpHandler Members"

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            ' マネージ ハンドラーが別の要求に再利用できない場合は、false を返します。
            ' 要求ごとに保存された状態情報がある場合、通常、これは false になります。
            Return True
        End Get
    End Property

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        ' ハンドラーの実装をここに記述します。

    End Sub

#End Region

End Class
