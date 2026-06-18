
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Transactions
Imports System.Configuration.ConfigurationSettings
Imports System.Collections.Generic

Public Class MUserBC

    Public DA As New MUserDA

    ''' <summary>
    ''' 
    ''' 用户MSInfoを検索する
    ''' </summary>
    '''<param name="userCd_key">用户CD</param>
    ''' <returns>用户MSInfo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function SelMUser(ByVal lineId_key As String, ByVal userCd_key As String, Optional ByVal flg As String = "") As Data.DataTable
        'SQLコメント
        Return DA.SelMUser(lineId_key, userCd_key, flg)

    End Function

    Public Function SelLineIds() As Data.DataTable
        Return DA.SelLineIds(）
    End Function

    Public Function SelUserlist(Optional ByVal line_id As String = "") As Data.DataTable
        Return DA.SelUserlist(line_id)
    End Function
    ''' <summary>
    ''' 
    ''' 用户MSInfoを更新する
    ''' </summary>
    '''<param name="userCd_key">用户CD</param>
    '''<param name="userCd">用户CD</param>
    '''<param name="lineId">生产线</param>
    '''<param name="userName">用户名</param>
    ''' <returns>用户MSInfo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function UpdMUser(ByVal userCd_key As String,
            ByVal userCd As String,
            ByVal lineId As String,
            ByVal userName As String, ByVal user_password As String, ByVal kengen As String, ByVal menu_user As String) As Boolean
        'SQLコメント
        '--**テーブル：用户MS : m_user
        Return DA.UpdMUser(
            userCd_key,
            userCd,
            lineId,
            userName, user_password, kengen, menu_user)

    End Function

    ''' <summary>
    ''' 
    ''' 用户MSInfoを登録する
    ''' </summary>
    '''<param name="userCd">用户CD</param>
    '''<param name="lineId">生产线</param>
    '''<param name="userName">用户名</param>
    ''' <returns>用户MSInfo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>
    Public Function InsMUser(ByVal userCd As String,
               ByVal lineId As String,
               ByVal userName As String, ByVal user_password As String, ByVal kengen As String, ByVal menu_user As String) As Boolean
        'SQLコメント
        '--**テーブル：用户MS : m_user
        Return DA.InsMUser(
               userCd,
               lineId,
               userName, user_password, kengen, menu_user)
    End Function

    ''' <summary>
    ''' 
    ''' 用户MSInfoを削除する
    ''' </summary>
    '''<param name="userCd_key">用户CD</param>
    ''' <returns>用户MSInfo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>
    Public Function DelMUser(ByVal userCd_key As String) As Boolean
        'SQLコメント
        '--**テーブル：用户MS : m_user
        Return DA.DelMUser( _
               userCd_key)
    End Function


    Public Function ChkUser(ByVal userCd_key As String, ByVal user_password As String) As Data.DataTable
        Return DA.ChkUser(userCd_key, user_password)
    End Function
End Class
