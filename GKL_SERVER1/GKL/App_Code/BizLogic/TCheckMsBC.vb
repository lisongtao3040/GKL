
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Transactions
Imports System.Configuration.ConfigurationSettings
Imports System.Collections.Generic

Public Class TCheckMsBC
Public DA AS NEW TCheckMsDA

    ''' <summary>
    ''' 
    ''' 检查结果Infoを検索する
    ''' </summary>
    '''<param name="chkNo_key">检查No</param>
    ''' <returns>检查结果Info</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

Public Function SelTCheckMs(Byval chkNo_key AS String) As Data.DataTable

    'SQLコメント
    Return DA.SelTCheckMs( _
           chkNo_key)
End Function

    ''' <summary>
    ''' 
    ''' 检查结果Infoを更新する
    ''' </summary>
    '''<param name="chkNo_key">检查No</param>
'''<param name="chkNo">检查No</param>
'''<param name="chkMethodId">检查项目ID</param>
'''<param name="chkFlg">检查flg</param>
'''<param name="in1">入力値1</param>
'''<param name="in2">入力値2</param>
'''<param name="chkResult">检查结果</param>
'''<param name="mark">备考</param>
'''<param name="kj0">基准</param>
'''<param name="kj1">工差1</param>
'''<param name="kj2">工差2</param>
'''<param name="kjExplain">基准説明</param>
'''<param name="insUser">登録者</param>
'''<param name="insDate">登録日</param>
    ''' <returns>检查结果Info</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

Public Function UpdTCheckMs(Byval chkNo_key AS String, _
           Byval chkNo AS String, _
           Byval chkMethodId AS String, _
           Byval chkFlg AS String, _
           Byval in1 AS String, _
           Byval in2 AS String, _
           Byval chkResult AS String, _
           Byval mark AS String, _
           Byval kj0 AS String, _
           Byval kj1 AS String, _
           Byval kj2 AS String, _
           Byval kjExplain AS String, _
           Byval insUser AS String, _
           Byval insDate AS String) As Boolean
   
    'SQLコメント
    '--**テーブル：检查结果 : t_check_ms
    Return DA.UpdTCheckMs( _
           chkNo_key, _
           chkNo, _
           chkMethodId, _
           chkFlg, _
           in1, _
           in2, _
           chkResult, _
           mark, _
           kj0, _
           kj1, _
           kj2, _
           kjExplain, _
           insUser, _
           insDate)

End Function

    ''' <summary>
    ''' 
    ''' 检查结果Infoを登録する
    ''' </summary>
    '''<param name="chkNo">检查No</param>
'''<param name="chkMethodId">检查项目ID</param>
'''<param name="chkFlg">检查flg</param>
'''<param name="in1">入力値1</param>
'''<param name="in2">入力値2</param>
'''<param name="chkResult">检查结果</param>
'''<param name="mark">备考</param>
'''<param name="kj0">基准</param>
'''<param name="kj1">工差1</param>
'''<param name="kj2">工差2</param>
'''<param name="kjExplain">基准説明</param>
'''<param name="insUser">登録者</param>
'''<param name="insDate">登録日</param>
    ''' <returns>检查结果Info</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

Public Function InsTCheckMs(Byval chkNo AS String, _
           Byval chkMethodId AS String, _
           Byval chkFlg AS String, _
           Byval in1 AS String, _
           Byval in2 AS String, _
           Byval chkResult AS String, _
           Byval mark AS String, _
           Byval kj0 AS String, _
           Byval kj1 AS String, _
           Byval kj2 AS String, _
           Byval kjExplain AS String, _
           Byval insUser AS String, _
           Byval insDate AS String) As Boolean
    
    'SQLコメント
    '--**テーブル：检查结果 : t_check_ms
    Return DA.InsTCheckMs( _
           chkNo, _
           chkMethodId, _
           chkFlg, _
           in1, _
           in2, _
           chkResult, _
           mark, _
           kj0, _
           kj1, _
           kj2, _
           kjExplain, _
           insUser, _
           insDate)

End Function

    ''' <summary>
    ''' 
    ''' 检查结果Infoを削除する
    ''' </summary>
    '''<param name="chkNo_key">检查No</param>
    ''' <returns>检查结果Info</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function DelTCheckMs(ByVal chkNo_key As String) As Boolean
       
        'SQLコメント
        '--**テーブル：检查结果 : t_check_ms
        Return DA.DelTCheckMs( _
               chkNo_key)


    End Function

    Public Function SelTCheckMs(ByVal chkNo_key As String, ByVal line_id As String) As Data.DataTable
        Return DA.SelTCheckMs(chkNo_key, line_id)
    End Function


    Public Function UpdTCheckMs(ByVal chkNo_key As String, _
               ByVal in1 As String, _
               ByVal chkResult As String, _
               ByVal mark As String, _
               ByVal kj0 As String, _
               ByVal kj1 As String, _
               ByVal kj2 As String, _
               ByVal insUser As String, _
               ByVal line_id As String, ByVal chk_method_id As String) As Boolean
        Return DA.UpdTCheckMs(chkNo_key, _
                               in1, _
                               chkResult, _
                               mark, _
                               kj0, _
                               kj1, _
                               kj2, _
                               insUser, _
                               line_id, chk_method_id)
    End Function


    Public Function UpdTCheckResultMS(ByVal chkNo_key As String, _
           ByVal line_id As String, ByVal insUser As String) As Boolean

        DA.UpdTCheckResultMS(chkNo_key, line_id, insUser)

        Return True

    End Function

    Public Function UpdTCheckResultOK(ByVal chkNo_key As String,
           ByVal line_id As String, ByVal insUser As String) As Boolean

        DA.UpdTCheckResultOK(chkNo_key, line_id, insUser)

        Return True

    End Function

    'Public Function UpdTCheckResultMSWanliao(ByVal chkNo_key As String, _
    '           ByVal line_id As String) As Boolean

    '    DA.UpdTCheckResultMSWanliao(chkNo_key, line_id)

    '    Return True

    'End Function

    Public Function InsImgInfo(ByVal chkNo As String, _
           ByVal picPath As String) As Object
        DA.InsImgInfo(chkNo, picPath)

        Return True
    End Function

    Public Function SelImgInfo(ByVal chkNo_key As String) As Data.DataTable
        Return DA.SelImgInfo(chkNo_key)
    End Function

    Public Function DelImgInfo(ByVal picPath As String, ByVal chkNo_key As String) As Object

        DA.DelImgInfo(picPath, chkNo_key)

        Return True
    End Function

    Public Function GetZuofanAndLine(ByVal chkNo_key As String) As DataTable
        Return DA.GetZuofanAndLine(chkNo_key)
    End Function


    Public Function GetResultByChkNo(ByVal chkNo_key As String) As DataTable
        Return DA.GetResultByChkNo(chkNo_key)
    End Function

    Public Function GetResultByChkNo(ByVal chkNo_key As String, ByVal line_id As String) As DataTable
        Return DA.GetResultByChkNo(chkNo_key, line_id)
    End Function

    Public Function GetDbDate() As String
        Return DA.GetDbDate()
    End Function
End Class
