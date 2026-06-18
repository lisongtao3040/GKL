
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Transactions
Imports System.Configuration.ConfigurationSettings
Imports System.Collections.Generic

Public Class TCheckPlanBC
Public DA AS NEW TCheckPlanDA

    ''' <summary>
    ''' 
    ''' 检查计划Infoを検索する
    ''' </summary>
    '''<param name="planNo_key">计划No</param>
'''<param name="chkNo_key">检查No</param>
'''<param name="makeNo_key">作番</param>
'''<param name="code_key">コード</param>
'''<param name="lineId_key">生产线</param>
    ''' <returns>检查计划Info</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function SelTCheckPlan(ByVal planNo_key As String, _
               ByVal chkNo_key As String, _
               ByVal makeNo_key As String, _
               ByVal code_key As String, _
               ByVal lineId_key As String, _
               ByVal tbxCheckDate_key As String) As Data.DataTable
       
        'SQLコメント
        Return DA.SelTCheckPlan( _
               planNo_key, _
               chkNo_key, _
               makeNo_key, _
               code_key, _
               lineId_key, _
               tbxCheckDate_key)
    End Function



    Public Function SelTPlanFromSap(ByVal YM As String, _
      ByVal user As String, ByVal lineid As String) As Data.DataTable

        'SQLコメント
        Return DA.SelTPlanFromSap( _
               YM, _
               user, lineid)
    End Function


    ''' <summary>
    ''' 
    ''' 检查计划Infoを更新する
    ''' </summary>
    '''<param name="planNo_key">计划No</param>
    '''<param name="chkNo_key">检查No</param>
    '''<param name="makeNo_key">作番</param>
    '''<param name="code_key">コード</param>
    '''<param name="lineId_key">生产线</param>
    '''<param name="planNo">计划No</param>
    '''<param name="chkNo">检查No</param>
    '''<param name="makeNo">作番</param>
    '''<param name="code">コード</param>
    '''<param name="lineId">生产线</param>
    '''<param name="suu">数量</param>
    '''<param name="yoteiChkDate">预订检查日</param>
    '''<param name="status">状态</param>
    '''<param name="insUser">登録者</param>
    '''<param name="insDate">登録日</param>
    ''' <returns>检查计划Info</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function UpdTCheckPlan(ByVal planNo_key As String, _
               ByVal chkNo_key As String, _
               ByVal makeNo_key As String, _
               ByVal code_key As String, _
               ByVal lineId_key As String, _
               ByVal planNo As String, _
               ByVal chkNo As String, _
               ByVal makeNo As String, _
               ByVal code As String, _
               ByVal lineId As String, _
               ByVal suu As String, _
               ByVal yoteiChkDate As String, _
               ByVal status As String, _
               ByVal insUser As String, _
               ByVal insDate As String, ByVal menu_user As String) As Boolean

        'SQLコメント
        '--**テーブル：检查计划 : t_check_plan
        Return DA.UpdTCheckPlan( _
               planNo_key, _
               chkNo_key, _
               makeNo_key, _
               code_key, _
               lineId_key, _
               planNo, _
               chkNo, _
               makeNo, _
               code, _
               lineId, _
               suu, _
               yoteiChkDate, _
               status, _
               insUser, _
               insDate, menu_user)

    End Function

    ''' <summary>
    ''' 
    ''' 检查计划Infoを登録する
    ''' </summary>
    '''<param name="planNo">计划No</param>
    '''<param name="chkNo">检查No</param>
    '''<param name="makeNo">作番</param>
    '''<param name="code">コード</param>
    '''<param name="lineId">生产线</param>
    '''<param name="suu">数量</param>
    '''<param name="yoteiChkDate">预订检查日</param>
    '''<param name="status">状态</param>
    '''<param name="insUser">登録者</param>
    '''<param name="insDate">登録日</param>
    ''' <returns>检查计划Info</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function InsTCheckPlan(ByVal planNo As String, _
               ByVal chkNo As String, _
               ByVal makeNo As String, _
               ByVal code As String, _
               ByVal lineId As String, _
               ByVal suu As String, _
               ByVal yoteiChkDate As String, _
               ByVal status As String, _
               ByVal insUser As String, _
               ByVal insDate As String, ByVal menu_user As String) As Boolean

        'SQLコメント
        '--**テーブル：检查计划 : t_check_plan
        Return DA.InsTCheckPlan( _
               planNo, _
               chkNo, _
               makeNo, _
               code, _
               lineId, _
               suu, _
               yoteiChkDate, _
               status, _
               insUser, _
               insDate, menu_user)

    End Function

    ''' <summary>
    ''' 
    ''' 检查计划Infoを削除する
    ''' </summary>
    '''<param name="planNo_key">计划No</param>
    '''<param name="chkNo_key">检查No</param>
    '''<param name="makeNo_key">作番</param>
    '''<param name="code_key">コード</param>
    '''<param name="lineId_key">生产线</param>
    ''' <returns>检查计划Info</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function DelTCheckPlan(ByVal planNo_key As String, _
               ByVal chkNo_key As String, _
               ByVal makeNo_key As String, _
               ByVal code_key As String, _
               ByVal lineId_key As String) As Boolean

        'SQLコメント
        '--**テーブル：检查计划 : t_check_plan
        Return DA.DelTCheckPlan( _
               planNo_key, _
               chkNo_key, _
               makeNo_key, _
               code_key, _
               lineId_key)


    End Function

End Class
