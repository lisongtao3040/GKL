
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Transactions
Imports System.Configuration.ConfigurationSettings
Imports System.Collections.Generic

Public Class MTempNameBC
Public DA AS NEW MTempNameDA

    ''' <summary>
    ''' 
    ''' 模板名称MSInfoを検索する
    ''' </summary>
    '''<param name="lineId_key">生产线</param>
    '''<param name="tempId_key">检查模板编号</param>
    ''' <returns>模板名称MSInfo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/09  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function SelMTempName(ByVal lineId_key As String, _
               ByVal tempId_key As String) As Data.DataTable
   
        'SQLコメント
        Return DA.SelMTempName( _
               lineId_key, _
               tempId_key)
    End Function

    ''' <summary>
    ''' 
    ''' 模板名称MSInfoを更新する
    ''' </summary>
    '''<param name="lineId_key">生产线</param>
    '''<param name="tempId_key">检查模板编号</param>
    '''<param name="lineId">生产线</param>
    '''<param name="tempId">检查模板编号</param>
    '''<param name="tempName">模板名称</param>
    ''' <returns>模板名称MSInfo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/09  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function UpdMTempName(ByVal lineId_key As String, _
               ByVal tempId_key As String, _
               ByVal lineId As String, _
               ByVal tempId As String, _
               ByVal tempName As String, ByVal menu_user As String) As Boolean

        'SQLコメント
        '--**テーブル：模板名称MS : m_temp_name
        Return DA.UpdMTempName( _
               lineId_key, _
               tempId_key, _
               lineId, _
               tempId, _
               tempName, menu_user)

    End Function

    ''' <summary>
    ''' 
    ''' 模板名称MSInfoを登録する
    ''' </summary>
    '''<param name="lineId">生产线</param>
    '''<param name="tempId">检查模板编号</param>
    '''<param name="tempName">模板名称</param>
    ''' <returns>模板名称MSInfo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/09  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function InsMTempName(ByVal lineId As String, _
               ByVal tempId As String, _
               ByVal tempName As String, ByVal menu_user As String) As Boolean

        'SQLコメント
        '--**テーブル：模板名称MS : m_temp_name
        Return DA.InsMTempName( _
               lineId, _
               tempId, _
               tempName, menu_user)

    End Function

    ''' <summary>
    ''' 
    ''' 模板名称MSInfoを削除する
    ''' </summary>
    '''<param name="lineId_key">生产线</param>
    '''<param name="tempId_key">检查模板编号</param>
    ''' <returns>模板名称MSInfo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/09  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function DelMTempName(ByVal lineId_key As String, _
               ByVal tempId_key As String) As Boolean

        'SQLコメント
        '--**テーブル：模板名称MS : m_temp_name
        Return DA.DelMTempName( _
               lineId_key, _
               tempId_key)


    End Function

End Class
