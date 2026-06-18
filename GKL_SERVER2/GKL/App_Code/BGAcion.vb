Imports Microsoft.VisualBasic
Imports System.Data

Public Class BGAcion
    Implements IDisposable

    Public Pub_cd As String
    Public Pub_no As String
    Public Pub_User As String
    Public Pub_Line As String

    Private BaoGongDA2 As New BaoGongDA2

    Public Function GetKijunListData(ByVal cd As String, ByVal no As String, ByVal lineid As String) As Data.DataTable
        Return BaoGongDA2.SelBgListByCd(cd, no, lineid)
    End Function


    Public Function BaoGongAll(ByVal cd As String, ByVal no As String, ByVal user As String, ByVal lineid As String
) As String

        '获得计划的数据
        Dim dt As Data.DataTable = GetKijunListData(cd, no, lineid)
        If dt.Rows.Count <= 0 Then
            Return "计划中没有要报工的数据"
        End If

        Dim msg As String
        '登录报工基础数据
        msg = InsBGLstAndMs(cd, no, user, lineid)
        If msg <> "" Then
            Return msg
        End If

        Return msg

    End Function


    Public Function InsBGLstAndMs(ByVal cd As String, ByVal no As String, ByVal user As String, ByVal lineid As String) As String

        '获得计划的数据 v_bg_list_new
        Dim dt As Data.DataTable = GetKijunListData(cd, no, lineid)
        If dt.Rows.Count <= 0 Then
            Return "计划中没有要报工的数据"
        End If

        '登录初期一览 m_baogong_list_new
        If (BaoGongDA2.SelListData(cd, no).Rows.Count = 0) Then
            Try
                BaoGongDA2.InsListData(cd, no, user, CInt(dt.Rows(0).Item("suu")), CInt(dt.Rows(0).Item("tuopan_syu_suu")), lineid)
                BaoGongDA2.InsMSData(cd, no, user, CInt(dt.Rows(0).Item("suu")), CInt(dt.Rows(0).Item("tuopan_syu_suu")), lineid)
                Return ""
            Catch ex As Exception
                BaoGongDA2.DelAllData(cd, no)
                Return ex.Message
            End Try
        Else
            Return "已经登录了报工基础数据"
        End If

    End Function

    Public pubBarCode As String = ""
    Public inProcessing As Boolean = False
    Public inProcessingOrderNo As String = ""
    Public userID As String
    Public userPass As String
    Private msg As String 'As MsgWindow = 'New MsgWindow()
    'Private socC As BGsocketConnect = Nothing
    Public socC As BGsocketConnect

    Public result As Boolean


    Function MessShow(ByVal msg As String, ByVal kbn As Boolean) As Object
        result = kbn
        Me.msg = msg
        Return Nothing
    End Function



    Private pvCd As String, pvNo As String, pvBgno As String, pvUser As String, pvbg_type As String, pvbg_user As String, pvBarScanNo As String
    Public OutMsg As String
    Public Sub RunBGAll()

        '如果报工系统关闭
        If BaoGongDA2.IsBaogongSysOn() = False Then
            Return
        End If

        Dim cd As String = Pub_cd
        Dim no As String = Pub_no
        Dim user As String = Pub_User

        '获得计划的数据
        Dim dt As Data.DataTable = GetKijunListData(cd, no, Pub_Line)
        If dt.Rows.Count <= 0 Then
            OutMsg = "计划中没有要报工的数据"
            Exit Sub
        End If

        '登录初期一览
        If (BaoGongDA2.SelListData(cd, no).Rows.Count = 0) Then
            Try
                BaoGongDA2.InsListData(cd, no, user, CInt(dt.Rows(0).Item("suu")), CInt(dt.Rows(0).Item("tuopan_syu_suu")), Pub_Line)
                BaoGongDA2.InsMSData(cd, no, user, CInt(dt.Rows(0).Item("suu")), CInt(dt.Rows(0).Item("tuopan_syu_suu")), Pub_Line)
            Catch ex As Exception
                BaoGongDA2.DelAllData(cd, no)
                OutMsg = ex.Message
                Exit Sub
            End Try
        Else
        End If

        If dt.Rows.Count >= 0 Then
            Dim suu As Integer, tp_nyu_suu As Integer
            suu = CInt(dt.Rows(0).Item("suu"))
            tp_nyu_suu = CInt(dt.Rows(0).Item("tuopan_syu_suu"))
            Dim lsttpNo As New List(Of String)
            Dim lstBarScanNo As New List(Of String)

            Dim lstBgSendStr As New List(Of String)
            For i As Integer = 0 To Math.Ceiling(suu / tp_nyu_suu) - 1
                lsttpNo.Add((i + 1).ToString)
                lstBarScanNo.Add((i + 1).ToString)
                'lstBgSendStr.Add("" & dt.Rows(0).Item("ProductCodeSap") & "/" & dt.Rows(0).Item("ProductCode") & "/" & dt.Rows(0).Item("suu") & "/" & dt.Rows(0).Item("Package") & "/" & dt.Rows(0).Item("localStorage") & "/" & dt.Rows(0).Item("DeliveryToName") & "/" & (i + 1) & "/" & dt.Rows(0).Item("ZuoFan") & ")
            Next
            RunBG(cd, no, lsttpNo, lstBarScanNo, user, "AUTO", "自动")
        End If

    End Sub

    Public Shared ReadOnly Padlock As Object = New Object()

    Public Function RunBG(ByVal cd As String, ByVal no As String, ByVal lsttpNo As List(Of String), ByVal lsttp_bar_cd As List(Of String), ByVal user As String, ByVal bg_user As String, ByVal bg_type As String) As String

        'Return "报工系统权限关闭"

        SyncLock Padlock

            pvCd = cd
            pvNo = no
            pvUser = user
            pvbg_user = bg_user
            pvbg_type = bg_type

            Dim BaoGongDA2 As New BaoGongDA2
            '如果报工系统关闭
            If BaoGongDA2.IsBaogongSysOn() = False Then
                Return "报工系统权限关闭"
            End If

            'BGsocketConnect.DataReceive += New socketConnect.DataHandler(AddressOf _dataRecive)
            AddHandler BGsocketConnect.DataReceive, AddressOf _dataRecive

            Dim wIdx As Integer = 0
            ConnectToServer()
            inProcessing = True
            socC.sendMessage("ZX" + ("3999998").PadRight(15, " ") + ("1").PadRight(15, " "))
            While inProcessing And wIdx < 300
                Threading.Thread.Sleep(100)
                wIdx = wIdx + 1
            End While

            If inProcessing Then
                BaoGongDA2.UpdMSData(pvCd, pvNo, lsttpNo(0), lsttp_bar_cd(0), "NG", "报工超时", pvUser, pvbg_user, pvbg_type)
                BaoGongDA2 = Nothing
                disConnectToServer()
                'InitMs()
                Return "报工超时(用户登录)"
            End If

            Threading.Thread.Sleep(100)

            Dim dt As Data.DataTable = BaoGongDA2.SelMSData(cd, no)

            For i As Integer = 0 To lsttpNo.Count - 1

                '如果登录成功
                If IsLoginRtv Then

                    pvBgno = lsttpNo(i)
                    pvBarScanNo = lsttp_bar_cd(i)

                    Dim drs() As DataRow = dt.Select("tp_no=" & pvBgno & "")

                    If drs.Length > 0 Then
                        inProcessing = True
                        'socC.sendMessage("XX" + "WNWAP890/NWZA890/40/10/4102//1/9999988885")

                        socC.sendMessage("XX" + drs(0).Item("bg_bar_data"))
                        'socC.sendMessage("XX" + lstBgSendStr(i))
                        wIdx = 0
                        ' 正在执行 ， 并且小于30秒
                        While inProcessing AndAlso wIdx < 300
                            Threading.Thread.Sleep(100)
                            wIdx = wIdx + 1
                        End While

                        '如果已经30秒了 还没报工完
                        If inProcessing Then
                            BaoGongDA2.UpdMSData(pvCd, pvNo, pvBgno, pvBarScanNo, "NG", "报工超时", pvUser, pvbg_user, pvbg_type)
                            BaoGongDA2 = Nothing

                            '回收
                            Try
                                RemoveHandler BGsocketConnect.DataReceive, AddressOf _dataRecive
                            Catch ex As Exception : End Try
                            Try
                                disConnectToServer()
                            Catch ex As Exception : End Try

                            Return "报工超时:" & (i + 1)

                        End If
                    End If
                End If
            Next

            '回收
            Try
                RemoveHandler BGsocketConnect.DataReceive, AddressOf _dataRecive
            Catch ex As Exception : End Try
            Try
                disConnectToServer()
            Catch ex As Exception : End Try



            Dim dtms As DataTable = BaoGongDA2.SelMSData(cd, no)
            For i As Integer = 0 To lsttpNo.Count - 1
                Dim drs() As DataRow = dtms.Select("tp_no=" & pvBgno & "")
                If drs.Length = 0 Then
                    Return "未找到报工明细:" & lsttpNo(i)
                Else
                    If Common.NullToEmpty(drs(0).Item("bg_result")) = "" Then
                        Return "报工中:" & lsttpNo(i)
                    ElseIf Common.NullToEmpty(drs(0).Item("bg_result")) = "NG" Then
                        Return "报出错:NG" & lsttpNo(i) & Common.NullToEmpty(drs(0).Item("bg_txt"))
                    End If
                End If
            Next
        End SyncLock

        BaoGongDA2 = Nothing

        Return ""

    End Function

    Public IsLoginRtv As Boolean = False
    Private disposedValue As Boolean

    Private Sub _dataRecive(ByVal strData As String)

        'inProcessing = False

        Dim strDataFlag As String

        If strData.Length < 2 Then
            MessShow("接收到不可识别数据", False)
            Return
        End If

        strDataFlag = strData.Substring(0, 2)

        If strDataFlag.ToUpper() = "XY" Then
            MessShow(inProcessingOrderNo & " 报工成功", True)

            Dim BaoGongDA2 As New BaoGongDA2
            BaoGongDA2.UpdMSData(pvCd, pvNo, pvBgno, pvBarScanNo, "OK", Right(strData, Len(strData) - 2), pvUser, pvbg_user, pvbg_type)

            inProcessing = False

            Return
        ElseIf strDataFlag.ToUpper() = "XZ" Then

            Dim BaoGongDA2 As New BaoGongDA2
            Dim result As String
            If strData.Contains("入库成功") Then
                result = "OK"
            Else
                result = "NG"
            End If
            BaoGongDA2.UpdMSData(pvCd, pvNo, pvBgno, pvBarScanNo, result, strData, pvUser, pvbg_user, pvbg_type)
            '报工失败
            MessShow(inProcessingOrderNo & strData.Substring(2), False)

            inProcessing = False

            Return

        ElseIf strDataFlag.ToUpper() = "ZY" Then
            MessShow("登录成功", True)
            IsLoginRtv = True
            inProcessing = False

            'Me.Invoke(New Action(Sub()
            '                         Me.pnl_Login.Visible = False
            '                         Me.txt_UserID.Enabled = False
            '                         Me.txt_UserPass.Enabled = False
            '                         Me.btn_Login.Enabled = False
            '                         Me.pnl_Login.Enabled = False
            '                     End Sub))
            'Bt.ScanLib.Control.btScanEnable()
            Return
        ElseIf strDataFlag.ToUpper() = "ZZ" Then
            IsLoginRtv = False
            MessShow(strData.Substring(2), False)
            inProcessing = False
            Return
        ElseIf strDataFlag.ToUpper() = "YZ" Then
            'isUpdate(strData)
            inProcessing = False
        End If


    End Sub

    Private Sub ConnectToServer()
        Try
            If socC Is Nothing Then socC = New BGsocketConnect()

            If socC.isConnect = True Then
                'Me.pnl_Login.Visible = True
            Else
                socC = Nothing
            End If

        Catch
        End Try
    End Sub

    Private Sub disConnectToServer()
        Try

            If socC IsNot Nothing Then
                socC.DisConnectServer()
                socC = Nothing
                'showConnectStatus(False)
            End If

        Catch
        Finally
        End Try
    End Sub

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: 释放托管状态(托管对象)
            End If

            ' TODO: 释放未托管的资源(未托管的对象)并重写终结器
            ' TODO: 将大型字段设置为 null
            disposedValue = True
        End If
    End Sub

    ' ' TODO: 仅当“Dispose(disposing As Boolean)”拥有用于释放未托管资源的代码时才替代终结器
    ' Protected Overrides Sub Finalize()
    '     ' 不要更改此代码。请将清理代码放入“Dispose(disposing As Boolean)”方法中
    '     Dispose(disposing:=False)
    '     MyBase.Finalize()
    ' End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' 不要更改此代码。请将清理代码放入“Dispose(disposing As Boolean)”方法中
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub
End Class


Public Class BGRsv2

    Public pvCd As String, pvNo As String, pvBgno As String, pvUser As String, pvbg_type As String, pvbg_user As String, pvBarScanNo As String


    Private msg As String  'As MsgWindow = 'New MsgWindow()
    'Private socC As BGsocketConnect = Nothing
    Public socC As BGsocketConnect
    Public result As Boolean
    Public inProcessingOrderNo As String = ""
    Public IsLoginRtv As Boolean = False
    Public inProcessing As Boolean


    Private Sub _dataRecive(ByVal strData As String)

        'inProcessing = False

        Dim strDataFlag As String

        If strData.Length < 2 Then
            MessShow("接收到不可识别数据", False)
            Return
        End If

        strDataFlag = strData.Substring(0, 2)

        If strDataFlag.ToUpper() = "XY" Then
            MessShow(inProcessingOrderNo & " 报工成功", True)

            Dim BaoGongDA2 As New BaoGongDA2
            BaoGongDA2.UpdMSData(pvCd, pvNo, pvBgno, pvBarScanNo, "OK", Right(strData, Len(strData) - 2), pvUser, pvbg_user, pvbg_type)

            inProcessing = False
            Return
        ElseIf strDataFlag.ToUpper() = "XZ" Then
            Dim BaoGongDA2 As New BaoGongDA2
            Dim result As String
            If strData.Contains("入库成功") Then
                result = "OK"
            Else
                result = "NG"
            End If
            BaoGongDA2.UpdMSData(pvCd, pvNo, pvBgno, pvBarScanNo, result, strData, pvUser, pvbg_user, pvbg_type)
            '报工失败
            MessShow(inProcessingOrderNo & strData.Substring(2), False)
            inProcessing = False
            Return
        ElseIf strDataFlag.ToUpper() = "ZY" Then
            MessShow("登录成功", True)
            IsLoginRtv = True
            inProcessing = False
            'Me.Invoke(New Action(Sub()
            '                         Me.pnl_Login.Visible = False
            '                         Me.txt_UserID.Enabled = False
            '                         Me.txt_UserPass.Enabled = False
            '                         Me.btn_Login.Enabled = False
            '                         Me.pnl_Login.Enabled = False
            '                     End Sub))
            'Bt.ScanLib.Control.btScanEnable()
            Return
        ElseIf strDataFlag.ToUpper() = "ZZ" Then
            IsLoginRtv = False
            MessShow(strData.Substring(2), False)
            inProcessing = False
            Return
        ElseIf strDataFlag.ToUpper() = "YZ" Then
            'isUpdate(strData)
            inProcessing = False
        End If


    End Sub

    Function MessShow(ByVal msg As String, ByVal kbn As Boolean) As Object
        result = kbn
        Me.msg = msg
        Return Nothing
    End Function

End Class