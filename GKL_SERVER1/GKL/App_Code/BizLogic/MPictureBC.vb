
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Transactions
Imports System.Configuration.ConfigurationSettings
Imports System.Collections.Generic

Public Class MPictureBC
Public DA AS NEW MPictureDA

    ''' <summary>
    ''' 
    ''' Infoを検索する
    ''' </summary>
    '''<param name="picId_key">pic_id</param>
'''<param name="lineId_key">line_id</param>
    ''' <returns>Info</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

Public Function SelMPicture(Byval picId_key AS String, _
           Byval lineId_key AS String) As Data.DataTable

    'SQLコメント
    Return DA.SelMPicture( _
           picId_key, _
           lineId_key)
    End Function

    Public Function GetLineListPic(ByVal picId_key As String, _
           ByVal lineId_key As String) As Object
  
        Return DA.GetLineListPic( _
               picId_key, _
               lineId_key)
    End Function


    ''' <summary>
    ''' 
    ''' Infoを更新する
    ''' </summary>
    '''<param name="picId_key">pic_id</param>
'''<param name="lineId_key">line_id</param>
'''<param name="picId">pic_id</param>
'''<param name="lineId">line_id</param>
'''<param name="picName">pic_name</param>
    ''' <returns>Info</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

Public Function UpdMPicture(Byval picId_key AS String, _
           Byval lineId_key AS String, _
           Byval picId AS String, _
           Byval lineId AS String, _
           Byval picName AS String) As Boolean

    'SQLコメント
    '--**テーブル： : m_picture
    Return DA.UpdMPicture( _
           picId_key, _
           lineId_key, _
           picId, _
           lineId, _
           picName)

End Function

    ''' <summary>
    ''' 
    ''' Infoを登録する
    ''' </summary>
    '''<param name="picId">pic_id</param>
'''<param name="lineId">line_id</param>
'''<param name="picName">pic_name</param>
    ''' <returns>Info</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

Public Function InsMPicture(Byval picId AS String, _
           Byval lineId AS String, _
           Byval picName AS String) As Boolean

    'SQLコメント
    '--**テーブル： : m_picture
    Return DA.InsMPicture( _
           picId, _
           lineId, _
           picName)

End Function

    ''' <summary>
    ''' 
    ''' Infoを削除する
    ''' </summary>
    '''<param name="picId_key">pic_id</param>
'''<param name="lineId_key">line_id</param>
    ''' <returns>Info</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

Public Function DelMPicture(Byval picId_key AS String, _
           Byval lineId_key AS String) As Boolean

    'SQLコメント
    '--**テーブル： : m_picture
    Return DA.DelMPicture( _
           picId_key, _
           lineId_key)


End Function

End Class
