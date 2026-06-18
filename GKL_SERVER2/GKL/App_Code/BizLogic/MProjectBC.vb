
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Transactions
Imports System.Configuration.ConfigurationSettings
Imports System.Collections.Generic

Public Class MProjectBC
Public DA AS NEW MProjectDA

    ''' <summary>
    ''' 
    ''' 工程MSInfoを検索する
    ''' </summary>
    '''<param name="projectId_key">工程ID</param>
'''<param name="lineId_key">生产线</param>
    ''' <returns>工程MSInfo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

Public Function SelMProject(Byval projectId_key AS String, _
           Byval lineId_key AS String) As Data.DataTable

    'SQLコメント
    Return DA.SelMProject( _
           projectId_key, _
           lineId_key)
End Function

    ''' <summary>
    ''' 
    ''' 工程MSInfoを更新する
    ''' </summary>
    '''<param name="projectId_key">工程ID</param>
'''<param name="lineId_key">生产线</param>
'''<param name="projectId">工程ID</param>
'''<param name="lineId">生产线</param>
'''<param name="projectName">工程名</param>
    ''' <returns>工程MSInfo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function UpdMProject(ByVal projectId_key As String, _
               ByVal lineId_key As String, _
               ByVal projectId As String, _
               ByVal lineId As String, _
               ByVal projectName As String, ByVal menu_user As String) As Boolean

        'SQLコメント
        '--**テーブル：工程MS : m_project
        Return DA.UpdMProject( _
               projectId_key, _
               lineId_key, _
               projectId, _
               lineId, _
               projectName, menu_user)

    End Function

    ''' <summary>
    ''' 
    ''' 工程MSInfoを登録する
    ''' </summary>
    '''<param name="projectId">工程ID</param>
'''<param name="lineId">生产线</param>
'''<param name="projectName">工程名</param>
    ''' <returns>工程MSInfo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function InsMProject(ByVal projectId As String, _
               ByVal lineId As String, _
               ByVal projectName As String, ByVal menu_user As String) As Boolean

        'SQLコメント
        '--**テーブル：工程MS : m_project
        Return DA.InsMProject( _
               projectId, _
               lineId, _
               projectName, menu_user)

    End Function

    ''' <summary>
    ''' 
    ''' 工程MSInfoを削除する
    ''' </summary>
    '''<param name="projectId_key">工程ID</param>
'''<param name="lineId_key">生产线</param>
    ''' <returns>工程MSInfo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

Public Function DelMProject(Byval projectId_key AS String, _
           Byval lineId_key AS String) As Boolean

    '--**テーブル：工程MS : m_project
    Return DA.DelMProject( _
           projectId_key, _
           lineId_key)


End Function

End Class
