Imports System.Web

Partial Class frame_t_scan_list
    Inherits System.Web.UI.Page

    'ユーザ表示名（用户名（用户CD））のキャッシュ
    Private _userDisplay As String = Nothing

    ''' <summary>
    ''' 检查No（URLパラメータ）
    ''' </summary>
    Protected ReadOnly Property ChkNo() As String
        Get
            Return GetQueryValue("chk_no")
        End Get
    End Property

    ''' <summary>
    ''' 生产线（URLパラメータ）
    ''' </summary>
    Protected ReadOnly Property LineId() As String
        Get
            Return GetQueryValue("line_id")
        End Get
    End Property

    ''' <summary>
    ''' 作番（URLパラメータ）
    ''' </summary>
    Protected ReadOnly Property MakeNo() As String
        Get
            Return GetQueryValue("make_no")
        End Get
    End Property

    ''' <summary>
    ''' コード（URLパラメータ）
    ''' </summary>
    Protected ReadOnly Property Code() As String
        Get
            Return GetQueryValue("code")
        End Get
    End Property

    ''' <summary>
    ''' ユーザ（URLパラメータ）
    ''' </summary>
    Protected ReadOnly Property ScanUser() As String
        Get
            Return GetQueryValue("user")
        End Get
    End Property

    Protected ReadOnly Property HtmlChkNo() As String
        Get
            Return Server.HtmlEncode(ChkNo)
        End Get
    End Property

    Protected ReadOnly Property HtmlLineId() As String
        Get
            Return Server.HtmlEncode(LineId)
        End Get
    End Property

    Protected ReadOnly Property HtmlMakeNo() As String
        Get
            Return Server.HtmlEncode(MakeNo)
        End Get
    End Property

    Protected ReadOnly Property HtmlCode() As String
        Get
            Return Server.HtmlEncode(Code)
        End Get
    End Property

    Protected ReadOnly Property HtmlUser() As String
        Get
            Return Server.HtmlEncode(ScanUser)
        End Get
    End Property

    ''' <summary>
    ''' ユーザ表示名（用户名（用户CD））
    ''' </summary>
    Protected ReadOnly Property UserDisplay() As String
        Get
            If _userDisplay IsNot Nothing Then
                Return _userDisplay
            End If

            Dim userCd As String = ScanUser
            If userCd = "" Then
                _userDisplay = ""
                Return _userDisplay
            End If

            Dim userName As String = ""
            Try
                Dim bc As New ScanUserBC
                userName = bc.SelUserName(userCd)
            Catch ex As Exception
                'ユーザ名が取得できない場合は用户CDのみ表示する
                userName = ""
            End Try

            If userName = "" Then
                _userDisplay = userCd
            Else
                _userDisplay = userName & "（" & userCd & "）"
            End If

            Return _userDisplay
        End Get
    End Property

    Protected ReadOnly Property HtmlUserDisplay() As String
        Get
            Return Server.HtmlEncode(UserDisplay)
        End Get
    End Property

    ''' <summary>
    ''' URLパラメータを取得する
    ''' </summary>
    ''' <param name="key">パラメータ名</param>
    ''' <returns>パラメータ値</returns>
    Private Function GetQueryValue(ByVal key As String) As String
        Dim value As String = Me.Request.QueryString(key)
        If value Is Nothing Then
            Return ""
        End If
        Return value.Trim()
    End Function

End Class
