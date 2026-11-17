Imports System.Data

Public Class TScanListBC
    Public DA As New TScanListDA

    ''' <summary>
    ''' 扫描列表Infoを検索する
    ''' </summary>
    ''' <param name="chkNo_key">检查No</param>
    ''' <param name="lineId_key">生产线</param>
    ''' <param name="makeNo_key">作番</param>
    ''' <returns>扫描列表Info</returns>
    Public Function SelTScanList(ByVal chkNo_key As String,
                                 ByVal lineId_key As String,
                                 ByVal makeNo_key As String) As Data.DataTable

        Return DA.SelTScanList(chkNo_key,
                               lineId_key,
                               makeNo_key)

    End Function

    Public Function IsHaveNg(ByVal chkNo_key As String,
                                 ByVal lineId_key As String,
                                 ByVal makeNo_key As String) As Boolean

        Return DA.IsHaveNg(chkNo_key,
                               lineId_key,
                               makeNo_key)

    End Function



    ''' <summary>
    ''' 扫描列表Infoを登録する
    ''' </summary>
    ''' <param name="chkNo">检查No</param>
    ''' <param name="lineId">生产线</param>
    ''' <param name="makeNo">作番</param>
    ''' <param name="code">コード（URLパラメータ）</param>
    ''' <param name="scanTxt">扫码值</param>
    ''' <param name="result">结果值</param>
    ''' <param name="insUser">登録者</param>
    ''' <returns>True:成功</returns>
    Public Function InsTScanList(ByVal chkNo As String, _
                                 ByVal lineId As String, _
                                 ByVal makeNo As String, _
                                 ByVal code As String, _
                                 ByVal scanTxt As String, _
                                 ByVal result As String, _
                                 ByVal insUser As String) As Boolean

        Return DA.InsTScanList(chkNo, _
                               lineId, _
                               makeNo, _
                               code, _
                               scanTxt, _
                               result, _
                               insUser)

    End Function

    ''' <summary>
    ''' 扫描列表Infoの結果値を更新する
    ''' </summary>
    ''' <param name="scanId">扫描ID</param>
    ''' <param name="chkNo">检查No</param>
    ''' <param name="lineId">生产线</param>
    ''' <param name="makeNo">作番</param>
    ''' <param name="result">结果值</param>
    ''' <param name="updUser">更新者</param>
    ''' <returns>True:更新成功</returns>
    Public Function UpdTScanListResult(ByVal scanId As Integer, _
                                       ByVal chkNo As String, _
                                       ByVal lineId As String, _
                                       ByVal makeNo As String, _
                                       ByVal result As String, _
                                       ByVal updUser As String) As Boolean

        Return DA.UpdTScanListResult(scanId, _
                                     chkNo, _
                                     lineId, _
                                     makeNo, _
                                     result, _
                                     updUser)

    End Function

End Class
