
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Transactions
Imports System.Configuration.ConfigurationSettings
Imports System.Collections.Generic

Public Class TCdTempRelationBC
Public DA AS NEW TCdTempRelationDA

    ''' <summary>
    ''' 
    ''' Infoを検索する
    ''' </summary>
    '''<param name="lineId_key">line_id</param>
'''<param name="code_key">code</param>
'''<param name="tempId_key">temp_id</param>
    ''' <returns>Info</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

Public Function SelTCdTempRelation(Byval lineId_key AS String, _
           Byval code_key AS String, _
           Byval tempId_key AS String) As Data.DataTable

    'SQLコメント
    Return DA.SelTCdTempRelation( _
           lineId_key, _
           code_key, _
           tempId_key)
End Function

    ''' <summary>
    ''' 
    ''' Infoを更新する
    ''' </summary>
    '''<param name="lineId_key">line_id</param>
    '''<param name="code_key">code</param>
    '''<param name="tempId_key">temp_id</param>
    '''<param name="lineId">line_id</param>
    '''<param name="code">code</param>
    '''<param name="tempId">temp_id</param>
    ''' <returns>Info</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function UpdTCdTempRelation(ByVal lineId_key As String,
           ByVal code_key As String,
           ByVal tempId_key As String,
           ByVal lineId As String,
           ByVal code As String,
           ByVal tempId As String, ByVal color_nm As String, ByVal menu_user As String
) As Boolean

        'SQLコメント
        '--**テーブル： : t_cd_temp_relation
        Return DA.UpdTCdTempRelation(
           lineId_key,
           code_key,
           tempId_key,
           lineId,
           code,
           tempId, color_nm, menu_user)

    End Function

    ''' <summary>
    ''' 
    ''' Infoを登録する
    ''' </summary>
    '''<param name="lineId">line_id</param>
    '''<param name="code">code</param>
    '''<param name="tempId">temp_id</param>
    ''' <returns>Info</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function InsTCdTempRelation(ByVal lineId As String,
           ByVal code As String,
           ByVal tempId As String, ByVal color_nm As String, ByVal menu_user As String
) As Boolean

        'SQLコメント
        '--**テーブル： : t_cd_temp_relation
        Return DA.InsTCdTempRelation(
           lineId,
           code,
           tempId, color_nm, menu_user)

    End Function

    ''' <summary>
    ''' 
    ''' Infoを削除する
    ''' </summary>
    '''<param name="lineId_key">line_id</param>
    '''<param name="code_key">code</param>
    '''<param name="tempId_key">temp_id</param>
    ''' <returns>Info</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function DelTCdTempRelation(ByVal lineId_key As String,
           ByVal code_key As String,
           ByVal tempId_key As String) As Boolean

        'SQLコメント
        '--**テーブル： : t_cd_temp_relation
        Return DA.DelTCdTempRelation(
           lineId_key,
           code_key,
           tempId_key)


    End Function

    Public Function Inst_colorcheck_resultLastCopy(ByVal line_cd As String, ByVal make_no As String, ByVal color As String, ByVal user As String) As String
        Return DA.Inst_colorcheck_resultLastCopy(line_cd, make_no, color, user)
    End Function


End Class
