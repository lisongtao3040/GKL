
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Transactions
Imports System.Configuration.ConfigurationSettings
Imports System.Collections.Generic

Public Class MToolsBC
Public DA AS NEW MToolsDA

    ''' <summary>
    ''' 
    ''' 治具MSInfoを検索する
    ''' </summary>
    '''<param name="toolId_key">治具ID</param>
'''<param name="lineId_key">生产线</param>
    ''' <returns>治具MSInfo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

Public Function SelMTools(Byval toolId_key AS String, _
           Byval lineId_key AS String) As Data.DataTable

    'SQLコメント
    Return DA.SelMTools( _
           toolId_key, _
           lineId_key)
End Function

    ''' <summary>
    ''' 
    ''' 治具MSInfoを更新する
    ''' </summary>
    '''<param name="toolId_key">治具ID</param>
'''<param name="lineId_key">生产线</param>
'''<param name="toolId">治具ID</param>
'''<param name="lineId">生产线</param>
'''<param name="projectName">工程</param>
'''<param name="toolName">治具显示文本</param>
    ''' <returns>治具MSInfo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function UpdMTools(ByVal toolId_key As String, _
               ByVal lineId_key As String, _
               ByVal toolId As String, _
               ByVal lineId As String, _
               ByVal projectName As String, _
               ByVal toolName As String, ByVal menu_user As String) As Boolean

        'SQLコメント
        '--**テーブル：治具MS : m_tools
        Return DA.UpdMTools( _
               toolId_key, _
               lineId_key, _
               toolId, _
               lineId, _
               projectName, _
               toolName, menu_user)

    End Function

    ''' <summary>
    ''' 
    ''' 治具MSInfoを登録する
    ''' </summary>
    '''<param name="toolId">治具ID</param>
'''<param name="lineId">生产线</param>
'''<param name="projectName">工程</param>
'''<param name="toolName">治具显示文本</param>
    ''' <returns>治具MSInfo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function InsMTools(ByVal toolId As String, _
               ByVal lineId As String, _
               ByVal projectName As String, _
               ByVal toolName As String, ByVal menu_user As String) As Boolean

        'SQLコメント
        '--**テーブル：治具MS : m_tools
        Return DA.InsMTools( _
               toolId, _
               lineId, _
               projectName, _
               toolName, menu_user)

    End Function

    ''' <summary>
    ''' 
    ''' 治具MSInfoを削除する
    ''' </summary>
    '''<param name="toolId_key">治具ID</param>
'''<param name="lineId_key">生产线</param>
    ''' <returns>治具MSInfo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

Public Function DelMTools(Byval toolId_key AS String, _
           Byval lineId_key AS String) As Boolean

    'SQLコメント
    '--**テーブル：治具MS : m_tools
    Return DA.DelMTools( _
           toolId_key, _
           lineId_key)


End Function

End Class
