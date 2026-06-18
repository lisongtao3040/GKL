Imports Microsoft.VisualBasic
Imports System.Web
Imports System.Web.HttpContext


Public Class Cparam
    Public Shared Sub SetCommonViewState(ByVal IsPostBack As Boolean, ByRef viewstate As System.Web.UI.StateBag, ByRef context As System.Web.HttpContext)
        If Not IsPostBack Then
            viewstate("menu_line_id") = Common.NullToEmpty(context.Items("menu_line_id"))
            viewstate("menu_user_cd") = Common.NullToEmpty(context.Items("menu_user_cd"))
            viewstate("menu_user_name") = Common.NullToEmpty(context.Items("menu_user_name"))
            viewstate("menu_kengen") = Common.NullToEmpty(context.Items("menu_kengen"))
        Else
            context.Items("menu_line_id") = viewstate("menu_line_id")
            context.Items("menu_user_cd") = viewstate("menu_user_cd")
            context.Items("menu_user_name") = viewstate("menu_user_name")
            context.Items("menu_kengen") = viewstate("menu_kengen")
        End If
    End Sub


    'Public Shared Sub SetCommonViewState(ByRef viewstate As System.Web.UI.StateBag, ByRef context As System.Web.HttpContext, ByVal IsPostBack As Boolean)
    '    If Not IsPostBack Then
    '        viewstate("menu_line_id") = Common.NullToEmpty(context.Items("menu_line_id"))
    '        viewstate("menu_user_cd") = Common.NullToEmpty(context.Items("menu_user_cd"))
    '        viewstate("menu_user_name") = Common.NullToEmpty(context.Items("menu_user_name"))
    '        viewstate("menu_kengen") = Common.NullToEmpty(context.Items("menu_kengen"))
    '    Else
    '        context.Items("menu_line_id") = viewstate("menu_line_id")
    '        context.Items("menu_user_cd") = viewstate("menu_user_cd")
    '        context.Items("menu_user_name") = viewstate("menu_user_name")
    '        context.Items("menu_kengen") = viewstate("menu_kengen")
    '    End If
    'End Sub

End Class
