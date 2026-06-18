Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Transactions
Imports System.Configuration.ConfigurationSettings
Imports System.Collections.Generic

Public Class TCheckResultBC
    Public DA As New TCheckResultDA

    Public Function InsBaogongRireki(ByVal chkNo As String,
           ByVal makeNo As String,
           ByVal cd As String,
           ByVal line As String,
           ByVal txt As String) As Boolean

        Return DA.InsBaogongRireki(chkNo, makeNo, cd, line, txt)

    End Function

    ''' <summary>
    ''' 
    ''' 检查结果Infoを検索する
    ''' </summary>
    '''<param name="chkNo_key">检查No</param>
    '''<param name="nen_key">年</param>
    '''<param name="lineId_key">生产线</param>
    '''<param name="makeNo_key">作番</param>
    ''' <returns>检查结果Info</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function SelTCheckResult(ByVal chkNo_key As String,
           ByVal nen_key As String,
           ByVal lineId_key As String,
           ByVal makeNo_key As String) As Data.DataTable

        'SQLコメント
        Return DA.SelTCheckResult(
           chkNo_key,
           nen_key,
           lineId_key,
           makeNo_key)
    End Function

    ''' <summary>
    ''' 
    ''' 检查结果Infoを更新する
    ''' </summary>
    '''<param name="chkNo_key">检查No</param>
    '''<param name="nen_key">年</param>
    '''<param name="lineId_key">生产线</param>
    '''<param name="makeNo_key">作番</param>
    '''<param name="chkNo">检查No</param>
    '''<param name="nen">年</param>
    '''<param name="planNo">计划No</param>
    '''<param name="lineId">生产线</param>
    '''<param name="makeNo">作番</param>
    '''<param name="code">コード</param>
    '''<param name="suu">数量</param>
    '''<param name="tempId">检查模板编号</param>
    '''<param name="chkResult">检查结果</param>
    '''<param name="chkUser">检查者</param>
    '''<param name="chkStartDate">检查開始日</param>
    '''<param name="chkEndDate">检查完了日</param>
    '''<param name="parentChkNo">父检查No</param>
    '''<param name="status">状态</param>
    '''<param name="insUser">登録者</param>
    '''<param name="insDate">登録日</param>
    ''' <returns>检查结果Info</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function UpdTCheckResult(ByVal chkNo_key As String,
           ByVal nen_key As String,
           ByVal lineId_key As String,
           ByVal makeNo_key As String,
           ByVal chkNo As String,
           ByVal nen As String,
           ByVal planNo As String,
           ByVal lineId As String,
           ByVal makeNo As String,
           ByVal code As String,
           ByVal suu As String,
           ByVal tempId As String,
           ByVal chkResult As String,
           ByVal chkUser As String,
           ByVal chkStartDate As String,
           ByVal chkEndDate As String,
           ByVal parentChkNo As String,
           ByVal status As String,
           ByVal insUser As String,
           ByVal insDate As String) As Boolean

        'SQLコメント
        '--**テーブル：检查结果 : t_check_result
        Return DA.UpdTCheckResult(
               chkNo_key,
               nen_key,
               lineId_key,
               makeNo_key,
               chkNo,
               nen,
               planNo,
               lineId,
               makeNo,
               code,
               suu,
               tempId,
               chkResult,
               chkUser,
               chkStartDate,
               chkEndDate,
               parentChkNo,
               status,
               insUser,
               insDate)

    End Function

    ''' <summary>
    ''' 
    ''' 检查结果Infoを登録する
    ''' </summary>
    '''<param name="chkNo">检查No</param>
    '''<param name="nen">年</param>
    '''<param name="planNo">计划No</param>
    '''<param name="lineId">生产线</param>
    '''<param name="makeNo">作番</param>
    '''<param name="code">コード</param>
    '''<param name="suu">数量</param>
    '''<param name="tempId">检查模板编号</param>
    '''<param name="chkResult">检查结果</param>
    '''<param name="chkUser">检查者</param>
    '''<param name="chkStartDate">检查開始日</param>
    '''<param name="chkEndDate">检查完了日</param>
    '''<param name="parentChkNo">父检查No</param>
    '''<param name="status">状态</param>
    '''<param name="insUser">登録者</param>
    '''<param name="insDate">登録日</param>
    ''' <returns>检查结果Info</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function InsTCheckResult(ByVal chkNo As String,
               ByVal nen As String,
               ByVal chk_times As String,
               ByVal planNo As String,
               ByVal lineId As String,
               ByVal loginlineId As String,
               ByVal makeNo As String,
               ByVal code As String,
               ByVal suu As String,
               ByVal tempId As String,
               ByVal chkResult As String,
               ByVal chkUser As String,
               ByVal chkYoteiDate As String,
               ByVal chkStartDate As String,
               ByVal chkEndDate As String,
               ByVal parentChkNo As String,
               ByVal status As String,
               ByVal insUser As String,
               ByVal insDate As String, ByVal isAllLine As Boolean) As Boolean

        '--**テーブル：检查结果 : t_check_result
        Return DA.InsTCheckResult(
               chkNo,
               nen,
               chk_times,
               planNo,
               lineId,
               loginlineId,
               makeNo,
               code,
               suu,
               tempId,
               chkResult,
               chkUser,
               chkYoteiDate,
               chkStartDate,
               chkEndDate,
               parentChkNo,
               status,
               insUser,
               insDate, isAllLine)

    End Function

    ''' <summary>
    ''' 
    ''' 检查结果Infoを削除する
    ''' </summary>
    '''<param name="chkNo_key">检查No</param>
    '''<param name="nen_key">年</param>
    '''<param name="lineId_key">生产线</param>
    '''<param name="makeNo_key">作番</param>
    ''' <returns>检查结果Info</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function DelTCheckResult(ByVal chkNo_key As String,
           ByVal nen_key As String,
           ByVal lineId_key As String,
           ByVal makeNo_key As String) As Boolean

        'SQLコメント
        '--**テーブル：检查结果 : t_check_result
        Return DA.DelTCheckResult(
               chkNo_key,
               nen_key,
               lineId_key,
               makeNo_key)


    End Function

    Public Function SelTCheckResult(ByVal lineId_key As String, ByVal startDate As String, ByVal endDate As String, ByVal make_no As String, ByVal code As String, ByVal isAllLine As Boolean) As Data.DataTable
        Return DA.SelTCheckResult(
                                   lineId_key,
                                   startDate,
                                   endDate, make_no, code, isAllLine)

    End Function

    Public Function DeleteCheckResult(ByVal chkNo_key As String,
       ByVal lineId_key As String,
       ByVal insUser As String) As Boolean

        Return DA.DeleteCheckResult(chkNo_key,
                                    lineId_key,
                                    insUser)
    End Function

    Public Function GetQianpinCnt(ByVal chkNo_key As String) As Integer
        Return DA.GetQianpinCnt(chkNo_key)
    End Function

    Public Function SetQianpinCnt(ByVal chkNo_key As String, ByVal suu As String) As Integer
        Return DA.SetQianpinCnt(chkNo_key, suu)
    End Function

    Public Function GetResultByChkNo(ByVal chkNo_key As String) As DataTable
        Return DA.GetResultByChkNo(chkNo_key)
    End Function

    ''' <summary>
    ''' 检查T 结果更新
    ''' </summary>
    ''' <param name="chk_id_key"></param>
    ''' <param name="insUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdTCheck(ByVal chk_id_key As String, ByVal line_id As String, ByVal insUser As String) As Boolean
        Dim TCheckMsDA As New TCheckMsDA
        Return TCheckMsDA.UpdTCheckResultMS(chk_id_key, line_id, insUser)
    End Function

End Class
