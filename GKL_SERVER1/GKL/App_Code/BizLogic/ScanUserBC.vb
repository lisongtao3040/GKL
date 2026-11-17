Imports System.Data

Public Class ScanUserBC
    Public DA As New ScanUserDA

    ''' <summary>
    ''' 用户名を取得する
    ''' </summary>
    ''' <param name="userCd">用户CD</param>
    ''' <returns>用户名（存在しない場合は空文字）</returns>
    Public Function SelUserName(ByVal userCd As String) As String

        Return DA.SelUserName(userCd)

    End Function

End Class
