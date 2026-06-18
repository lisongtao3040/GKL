
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Transactions
Imports System.Configuration.ConfigurationSettings
Imports System.Collections.Generic

Public Class MTempBC
Public DA AS NEW MTempDA

    ''' <summary>
    ''' 
    ''' 模板MSInfoを検索する
    ''' </summary>
    '''<param name="lineId_key">生产线</param>
    '''<param name="tempId_key">检查模板编号</param>
    '''<param name="chkMethodId_key">检查项目ID</param>
    ''' <returns>模板MSInfo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function SelMTemp(ByVal lineId_key As String, _
               ByVal tempId_key As String, _
               ByVal chkMethodId_key As String) As Data.DataTable

        'SQLコメント
        Return DA.SelMTemp( _
               lineId_key, _
               tempId_key, _
               chkMethodId_key)
    End Function

    Public Function SelTempIds(ByVal lineId_key As String) As Data.DataTable
        Return DA.SelTempIds(lineId_key)

    End Function

    ''' <summary>
    ''' 
    ''' 模板MSInfoを更新する
    ''' </summary>
    '''<param name="lineId_key">生产线</param>
    '''<param name="tempId_key">检查模板编号</param>
    '''<param name="chkMethodId_key">检查项目ID</param>
    '''<param name="lineId">生产线</param>
    '''<param name="tempId">检查模板编号</param>
    '''<param name="chkMethodId">检查项目ID</param>
    '''<param name="projectId">工程ID</param>
    '''<param name="projectName">工程名</param>
    '''<param name="picId">图片ID</param>
    '''<param name="picName">图片名称</param>
    '''<param name="chkKmName">检查项目</param>
    '''<param name="picSign">图片标记</param>
    '''<param name="chkId">检查方法ID</param>
    '''<param name="chkName">检查方法名</param>
    '''<param name="toolId">治具ID</param>
    '''<param name="kj0">基准</param>
    '''<param name="kj1">工差1</param>
    '''<param name="kj2">工差2</param>
    '''<param name="kjExplain">基准説明</param>
    ''' <returns>模板MSInfo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function UpdMTemp(ByVal lineId_key As String, _
               ByVal tempId_key As String, _
               ByVal chkMethodId_key As String, _
               ByVal lineId As String, _
               ByVal tempId As String, _
               ByVal chkMethodId As String, _
               ByVal projectId As String, _
               ByVal projectName As String, _
               ByVal picId As String, _
               ByVal picName As String, _
               ByVal chkKmName As String, _
               ByVal picSign As String, _
               ByVal chkId As String, _
               ByVal chkName As String, _
               ByVal toolId As String, _
               ByVal kj0 As String, _
               ByVal kj1 As String, _
               ByVal kj2 As String, _
               ByVal kjExplain As String, ByVal menu_user As String) As Boolean
        'EMAB　ＥＲＲ

        'SQLコメント
        '--**テーブル：模板MS : m_temp
        Return DA.UpdMTemp( _
               lineId_key, _
               tempId_key, _
               chkMethodId_key, _
               lineId, _
               tempId, _
               chkMethodId, _
               projectId, _
               projectName, _
               picId, _
               picName, _
               chkKmName, _
               picSign, _
               chkId, _
               chkName, _
               toolId, _
               kj0, _
               kj1, _
               kj2, _
               kjExplain, menu_user)

    End Function

    ''' <summary>
    ''' 
    ''' 模板MSInfoを登録する
    ''' </summary>
    '''<param name="lineId">生产线</param>
    '''<param name="tempId">检查模板编号</param>
    '''<param name="chkMethodId">检查项目ID</param>
    '''<param name="projectId">工程ID</param>
    '''<param name="projectName">工程名</param>
    '''<param name="picId">图片ID</param>
    '''<param name="picName">图片名称</param>
    '''<param name="chkKmName">检查项目</param>
    '''<param name="picSign">图片标记</param>
    '''<param name="chkId">检查方法ID</param>
    '''<param name="chkName">检查方法名</param>
    '''<param name="toolId">治具ID</param>
    '''<param name="kj0">基准</param>
    '''<param name="kj1">工差1</param>
    '''<param name="kj2">工差2</param>
    '''<param name="kjExplain">基准説明</param>
    ''' <returns>模板MSInfo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function InsMTemp(ByVal lineId As String, _
               ByVal tempId As String, _
               ByVal chkMethodId As String, _
               ByVal projectId As String, _
               ByVal projectName As String, _
               ByVal picId As String, _
               ByVal picName As String, _
               ByVal chkKmName As String, _
               ByVal picSign As String, _
               ByVal chkId As String, _
               ByVal chkName As String, _
               ByVal toolId As String, _
               ByVal kj0 As String, _
               ByVal kj1 As String, _
               ByVal kj2 As String, _
               ByVal kjExplain As String, ByVal menu_user As String) As Boolean

        'SQLコメント
        '--**テーブル：模板MS : m_temp
        Return DA.InsMTemp( _
               lineId, _
               tempId, _
               chkMethodId, _
               projectId, _
               projectName, _
               picId, _
               picName, _
               chkKmName, _
               picSign, _
               chkId, _
               chkName, _
               toolId, _
               kj0, _
               kj1, _
               kj2, _
               kjExplain, menu_user)

    End Function

    ''' <summary>
    ''' 
    ''' 模板MSInfoを削除する
    ''' </summary>
    '''<param name="lineId_key">生产线</param>
    '''<param name="tempId_key">检查模板编号</param>
    '''<param name="chkMethodId_key">检查项目ID</param>
    ''' <returns>模板MSInfo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function DelMTemp(ByVal lineId_key As String, _
               ByVal tempId_key As String, _
               ByVal chkMethodId_key As String) As Boolean
     
        'SQLコメント
        '--**テーブル：模板MS : m_temp
        Return DA.DelMTemp( _
               lineId_key, _
               tempId_key, _
               chkMethodId_key)


    End Function


    Public Function CopyTemp(ByVal lineId_key As String, _
                        ByVal tempId_key As String, _
                        ByVal tempId_key_new As String, ByVal menu_user As String) As Boolean

        Return DA.CopyTemp( _
                          lineId_key, _
                          tempId_key, _
                          tempId_key_new, menu_user)

    End Function

    Public Function SelMTempChk(ByVal lineId_key As String, _
       ByVal tempId_key As String) As Data.DataTable

        Return DA.SelMTempChk( _
                      lineId_key, _
                      tempId_key)

    End Function

End Class
