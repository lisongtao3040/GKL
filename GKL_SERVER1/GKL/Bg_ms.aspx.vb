
Partial Class Bg_ms
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        '加载参数
        Cparam.SetCommonViewState(IsPostBack, Me.ViewState, Me.Context)

        If Not IsPostBack Then

            ViewState("menu_line_id") = Common.NullToEmpty(Context.Items("menu_line_id"))
            ViewState("menu_user_cd") = Common.NullToEmpty(Context.Items("menu_user_cd"))
            ViewState("menu_user_name") = Common.NullToEmpty(Context.Items("menu_user_name"))

            'Context.Items("cd") = hidCd.Value
            'Context.Items("no") = hidNo.Value
            ViewState("cd") = Context.Items("cd")
            ViewState("no") = Context.Items("no")
            ViewState("line") = Context.Items("line")
            ViewState("planymd") = Context.Items("planymd")

            Dim BGAcion As New BGAcion
            Dim dt As Data.DataTable = BGAcion.GetKijunListData(ViewState("cd"), ViewState("no"), ViewState("line"))
            If dt.Rows.Count > 0 Then
                Me.lblNo.Text = dt.Rows(0).Item("ZuoFan")
                Me.lblCd.Text = dt.Rows(0).Item("ProductCode")
                Me.lblOkSuu.Text = dt.Rows(0).Item("ok_suu")
                Me.lblSuu.Text = dt.Rows(0).Item("suu")
                Try
                    'Me.kbsuu.Text = dt.Rows(0).Item("Package")
                    Me.kbsuu.Text = CInt(Math.Ceiling(dt.Rows(0).Item("suu") / CInt(dt.Rows(0).Item("Package"))))
                Catch ex As Exception

                End Try
                Me.tpNyuSuu.Text = dt.Rows(0).Item("tuopan_syu_suu")
                If Me.tpNyuSuu.Text = "0" Then
                    Me.tpSuu.Text = "1"
                Else
                    Me.tpSuu.Text = Math.Ceiling(CInt(dt.Rows(0).Item("suu")) / CInt(dt.Rows(0).Item("tuopan_syu_suu")))
                End If

            End If

            BGAcion.InsBGLstAndMs(ViewState("cd"), ViewState("no"), ViewState("menu_user_cd"), ViewState("line"))


            'Dim user As String = "test"

            'Dim BaoGongDA As New BaoGongDA
            'Dim dt As Data.DataTable = BaoGongDA.SelBgListByCd(ViewState("cd"), ViewState("no"))



            'If (BaoGongDA.SelListData(ViewState("cd"), ViewState("no")).Rows.Count = 0) Then
            '    Try
            '        BaoGongDA.InsListData(ViewState("cd"), ViewState("no"), user, CInt(dt.Rows(0).Item("suu")), CInt(dt.Rows(0).Item("tuopan_syu_suu")))
            '        BaoGongDA.InsMSData(ViewState("cd"), ViewState("no"), user, CInt(dt.Rows(0).Item("suu")), CInt(dt.Rows(0).Item("tuopan_syu_suu")))
            '    Catch ex As Exception

            '        BaoGongDA.DelAllData(ViewState("cd"), ViewState("no"))
            '    End Try

            'End If


            InitMs()
        End If


    End Sub


    Sub InitMs()

        Dim BaoGongDA As New BaoGongDA
        'm_baogong_ms_new
        Dim dt As Data.DataTable = (New GKL_BgDA).GetBgMSData(ViewState("cd"), ViewState("no"))
        gv.DataSource = dt
        gv.DataBind()

        Dim IsBaogongSysOn As Boolean = BaoGongDA.IsBaogongSysOn()

        For i As Integer = 0 To dt.Rows.Count - 1
            CType(gv.Rows(i).FindControl("btnBG"), Button).OnClientClick = "GoBG('" & dt.Rows(i).Item("cd") & "','" & dt.Rows(i).Item("make_no") & "'," & dt.Rows(i).Item("tp_no") & ");return false;"
            If dt.Rows(i).Item("bg_result") = "OK" Then
                gv.Rows(i).Cells(2).BackColor = Drawing.Color.LightGreen

                '如果已经有报工OK 的  ，不允许 全部报工 以及个别的手动报工
                CType(gv.Rows(i).FindControl("btnBG"), Button).Enabled = False
                btnBgAll.Enabled = False

            ElseIf dt.Rows(i).Item("bg_result") = "NG" Then
                gv.Rows(i).Cells(2).BackColor = Drawing.Color.Red
            Else
                gv.Rows(i).Cells(2).BackColor = Drawing.Color.White
            End If

            If IsBaogongSysOn = False Then
                CType(gv.Rows(i).FindControl("btnBG"), Button).Enabled = False
            End If

        Next

        If IsBaogongSysOn = False Then
            btnBgAll.Enabled = False
        End If

    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Context.Items("line") = ViewState("line")
        Context.Items("planymd") = ViewState("planymd")
        Server.Transfer("Bg_list.aspx")
    End Sub


    'Public socC As BGsocketConnect

    Protected Sub btnBgAll_Click(sender As Object, e As EventArgs) Handles btnBgAll.Click
        Dim BGAcion As New BGAcion
        BGAcion.Pub_cd = ViewState("cd")
        BGAcion.Pub_no = ViewState("no")
        BGAcion.Pub_User = ViewState("menu_user_cd")
        BGAcion.Pub_Line = ViewState("line")


        BGAcion.RunBGAll()

        BGAcion.Dispose()
        BGAcion = Nothing

        InitMs()
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub
    '报工
    Protected Sub btnBG_Click(sender As Object, e As EventArgs) Handles btnBG.Click

        Dim BGAcion As New BGAcion

        Dim lstBgno As New List(Of String)
        'Dim lstBgSendStr As New List(Of String)

        lstBgno.Add(Me.hidBGNo.Value)
        'lstBgSendStr.Add("WNWAP890/NWZA890/40/10/4102//1/9999988885")

        BGAcion.RunBG(Me.hidCd.Value, Me.hidNo.Value, lstBgno, lstBgno, ViewState("menu_user_cd"), ViewState("menu_user_cd"), "手动")

        'BGAcion.RunBG(Me.hidCd.Value, Me.hidNo.Value, lstBgno, lstBgno, ViewState("menu_user_cd"), ViewState("menu_user_cd"), "手动")
        lstBgno.Clear()
        lstBgno = Nothing

        BGAcion.Dispose()
        BGAcion = Nothing

        InitMs()

        GC.Collect()
        GC.WaitForPendingFinalizers()
        ''BGsocketConnect.DataReceive += New socketConnect.DataHandler(AddressOf _dataRecive)
        'AddHandler BGsocketConnect.DataReceive, AddressOf _dataRecive

        'Dim wIdx As Integer = 0
        'ConnectToServer()

        'inProcessing = True
        'socC.sendMessage("ZX" + ("3999998").PadRight(15, " ") + ("1").PadRight(15, " "))
        'While inProcessing And wIdx < 300
        '    Threading.Thread.Sleep(100)
        '    wIdx = wIdx + 1
        'End While

        'If inProcessing Then
        '    Dim BaoGongDA As New BaoGongDA
        '    BaoGongDA.UpdMSData(Me.hidCd.Value, Me.hidNo.Value, Me.hidBGNo.Value, "NG", "报工超时", "testuser")
        '    disConnectToServer()
        '    InitMs()
        '    Exit Sub
        'End If

        ''如果登录成功
        'If IsLoginRtv Then
        '    inProcessing = True
        '    socC.sendMessage("XX" + "WNWAP890/NWZA890/40/10/4102//1/9999988885")
        '    wIdx = 0
        '    While inProcessing And wIdx < 300
        '        Threading.Thread.Sleep(100)
        '        wIdx = wIdx + 1
        '    End While

        '    If inProcessing Then
        '        Dim BaoGongDA As New BaoGongDA
        '        BaoGongDA.UpdMSData(Me.hidCd.Value, Me.hidNo.Value, Me.hidBGNo.Value, "NG", "报工超时", "testuser")
        '        disConnectToServer()
        '        InitMs()
        '        Exit Sub
        '    End If
        'End If
        'disConnectToServer()
        'InitMs()

    End Sub


    Public pubBarCode As String = ""
    Public inProcessing As Boolean = False
    Public inProcessingOrderNo As String = ""
    Public userID As String
    Public userPass As String
    Private msg 'As MsgWindow = 'New MsgWindow()
    'Private socC As BGsocketConnect = Nothing

    Public result


    Function MessShow(ByVal msg As String, ByVal kbn As Boolean)
        result = kbn
        Me.msg = msg
    End Function

    Public IsLoginRtv As Boolean = False

    'Private Sub _dataRecive(ByVal strData As String)

    '    Dim strDataFlag As String

    '    If strData.Length < 2 Then
    '        MessShow("接收到不可识别数据", False)
    '        Return
    '    End If

    '    strDataFlag = strData.Substring(0, 2)

    '    If strDataFlag.ToUpper() = "XY" Then
    '        MessShow(inProcessingOrderNo & " 报工成功", True)

    '        Dim BaoGongDA As New BaoGongDA
    '        BaoGongDA.UpdMSData(Me.hidCd.Value, Me.hidNo.Value, Me.hidBGNo.Value, "OK", Right(strData, Len(strData) - 2), "testuser")

    '        inProcessing = False
    '        Return
    '    ElseIf strDataFlag.ToUpper() = "XZ" Then
    '        Dim BaoGongDA As New BaoGongDA
    '        BaoGongDA.UpdMSData(Me.hidCd.Value, Me.hidNo.Value, Me.hidBGNo.Value, "NG", strData, "testuser")
    '        '报工失败
    '        MessShow(inProcessingOrderNo & strData.Substring(2), False)
    '        inProcessing = False
    '        Return
    '    ElseIf strDataFlag.ToUpper() = "ZY" Then
    '        MessShow("登录成功", True)
    '        IsLoginRtv = True
    '        inProcessing = False
    '        'Me.Invoke(New Action(Sub()
    '        '                         Me.pnl_Login.Visible = False
    '        '                         Me.txt_UserID.Enabled = False
    '        '                         Me.txt_UserPass.Enabled = False
    '        '                         Me.btn_Login.Enabled = False
    '        '                         Me.pnl_Login.Enabled = False
    '        '                     End Sub))
    '        'Bt.ScanLib.Control.btScanEnable()
    '        Return
    '    ElseIf strDataFlag.ToUpper() = "ZZ" Then
    '        IsLoginRtv = False
    '        MessShow(strData.Substring(2), False)
    '        inProcessing = False
    '        Return
    '    ElseIf strDataFlag.ToUpper() = "YZ" Then
    '        'isUpdate(strData)
    '        inProcessing = False
    '    End If


    'End Sub

    'Private Sub ConnectToServer()
    '    Try
    '        If socC Is Nothing Then socC = New BGsocketConnect()

    '        If socC.isConnect = True Then
    '            'Me.pnl_Login.Visible = True
    '        Else
    '            socC = Nothing
    '        End If

    '    Catch
    '    End Try
    'End Sub

    'Private Sub disConnectToServer()
    '    Try

    '        If socC IsNot Nothing Then
    '            socC.DisConnectServer()
    '            socC = Nothing
    '            'showConnectStatus(False)
    '        End If

    '    Catch
    '    Finally
    '    End Try
    'End Sub

 
End Class
