Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Newtonsoft.Json
Imports System.Data

' この Web サービスを、スクリプトから ASP.NET AJAX を使用して呼び出せるようにするには、次の行のコメントを解除します。
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class Bg
    Inherits System.Web.Services.WebService

    ''' <summary>
    ''' 登录按钮按下时
    '''     单个托盘时：  
    '''             直接报工
    '''             关联托盘    
    '''     复数个托盘时：
    '''             登录 LIST 和 MS
    ''' </summary>
    ''' <param name="line_cd"></param>
    ''' <param name="no"></param>
    ''' <param name="cd"></param>
    ''' <param name="insUser"></param>
    ''' <param name="insUserName"></param>
    ''' <param name="chk_id"></param>
    ''' <param name="tp_bar_cd"></param>
    ''' <returns></returns>
    <WebMethod()>
    Public Function JoinTP_InsertTrayData(ByVal line_cd As String, ByVal no As String, ByVal cd As String,
                                          ByVal insUser As String, ByVal insUserName As String,
                                          ByVal chk_id As String, ByVal tp_bar_cd As String) As String

        Dim BaoGongDA As New BaoGongDA
        Dim BC As New TCheckResultBC
        Dim OKNG As String = "OK"
        Dim sbRtv As New StringBuilder

        '如果没报工，那么更新检查结果
        If (BaoGongDA.SelListData(cd, no).Rows.Count = 0) Then
            BC.UpdTCheck(chk_id, line_cd, insUser)
        End If

        '需要报工
        Dim IsBaogong As Boolean = System.Configuration.ConfigurationManager.AppSettings.[Get]("baogong_lines").ToString().IndexOf(line_cd) >= 0 And (New BaoGongDA).IsBaogongSysOn
        '需要关联托盘
        Dim IsGuanlianTuopan As Boolean = System.Configuration.ConfigurationManager.AppSettings.[Get]("tuopan_lines").ToString().IndexOf(line_cd)

        Dim dt As DataTable
        Dim dtRlt As DataTable


        If IsBaogong Or IsGuanlianTuopan Then
            'Dim BC_CK As TCheckResultBC = New TCheckResultBC()
            'v_bg_list  获得报工list
            dt = (New GKL_BgDA).SelTCheckResultOkSuu(no, cd, line_cd)
            If dt.Rows.Count <= 0 Then
                Return JsonConvert.SerializeObject(New With {
                    .result = "NG",
                    .msg = "没有初始数据（报工、托盘)"
                })
            End If

            '获得检查结果  chk_result
            dtRlt = BC.GetResultByChkNo(chk_id)

            '初始化报工记录
            Dim bgMsg As String = ""
            bgMsg = InitBaogongData(dt, cd, no, line_cd, insUser)
            If bgMsg <> "" Then Return bgMsg

            If CInt(dt.Rows(0).Item("suu")) <= CInt(CInt(dt.Rows(0).Item("tuopan_syu_suu"))) AndAlso Not line_cd.Contains("983") Then

                Dim msg As String = ""

                Dim jsonRtvTP As Object = (New With {.result = "", .msg = ""})
                '==============================================================================
                '1.单纯关联托盘
                '==============================================================================
                If IsGuanlianTuopan Then
                    If CInt(dt.Rows(0).Item("suu")) <= CInt(CInt(dt.Rows(0).Item("tuopan_syu_suu"))) AndAlso Not line_cd.Contains("983") Then
                        '关联托盘时
                        jsonRtvTP = Run_InsertTrayData(cd, no, line_cd, dt.Rows(0)("suu").ToString(), "1", tp_bar_cd, insUser, insUserName)
                        If (jsonRtvTP.result <> "OK") Then
                            msg = msg & jsonRtvTP.msg
                        End If
                    End If
                End If
                '==============================================================================
                '2.单纯报工
                '==============================================================================
                Dim jsonRtvBG As Object = (New With {.result = "", .msg = ""})
                If IsBaogong Then
                    If CInt(dt.Rows(0).Item("suu")) <= CInt(CInt(dt.Rows(0).Item("tuopan_syu_suu"))) AndAlso Not line_cd.Contains("983") Then
                        '单纯报工
                        jsonRtvBG = BaoGongFnc(line_cd, cd, no, insUser, insUserName, chk_id, tp_bar_cd, dt, dtRlt, "1")
                        If (jsonRtvBG.result <> "OK") Then
                            msg = msg & jsonRtvBG.msg
                        End If
                    End If
                End If

                '既报工 又关联托盘
                If IsBaogong And IsGuanlianTuopan Then
                    If (jsonRtvTP.result = "OK") AndAlso (jsonRtvBG.result = "OK") Then
                        Return JsonConvert.SerializeObject(New With {
                            .result = "OK",
                            .msg = "已经关联托盘，已经报工！"
                        })
                    Else
                        Return JsonConvert.SerializeObject(New With {
                            .result = "WA",
                            .msg = msg
                        })
                    End If
                ElseIf IsGuanlianTuopan Then
                    If (jsonRtvTP.result = "OK") Then
                        Return JsonConvert.SerializeObject(New With {
                            .result = "OK",
                            .msg = "已经关联托盘，已经报工！"
                        })
                    Else
                        Return JsonConvert.SerializeObject(New With {
                            .result = "WA",
                            .msg = msg
                        })
                    End If
                ElseIf IsBaogong Then
                    If (jsonRtvBG.result = "OK") Then
                        Return JsonConvert.SerializeObject(New With {
                            .result = "OK",
                            .msg = "已经关联托盘，已经报工！"
                        })
                    Else
                        Return JsonConvert.SerializeObject(New With {
                            .result = "WA",
                            .msg = msg
                        })
                    End If
                End If
            Else
                '做成报工的list ms 表数据
                'Dim rtv As Object = InsBgListAndMsData(line_cd, cd, no, insUser, insUserName, chk_id, tp_bar_cd, dt, dtRlt)
                'If rtv.result <> "OK" Then
                '    Return JsonConvert.SerializeObject(rtv)
                'End If
                '报工Panel
                Dim rtvBgPanel As Object = GetBgPanel(cd, no, IsBaogong, IsGuanlianTuopan)
                Return JsonConvert.SerializeObject(rtvBgPanel)

            End If

        Else
            Return JsonConvert.SerializeObject(New With {
                .result = "OK",
                .msg = ""
            })

        End If

    End Function


    Function InitBaogongData(ByVal dt As DataTable, ByVal cd As String, ByVal no As String, ByVal line_cd As String, ByVal insUser As String) As String

        Dim BaoGongDA As New BaoGongDA

        If CInt(dt.Rows(0).Item("suu")) <= CInt(CInt(dt.Rows(0).Item("tuopan_syu_suu"))) AndAlso Not line_cd.Contains("983") Then
            '登录报工初期一览（也是给关联托盘用的）
            If (BaoGongDA.SelListData(cd, no).Rows.Count = 0) Then
                Try
                    '只添加LIST
                    BaoGongDA.InsListData(cd, no, insUser, CInt(dt.Rows(0).Item("suu")), CInt(dt.Rows(0).Item("tuopan_syu_suu")), line_cd)
                    '只添加MS
                    BaoGongDA.InsMSOneRowData(cd, no, insUser, CInt(dt.Rows(0).Item("suu")), CInt(dt.Rows(0).Item("tuopan_syu_suu")), line_cd, "1", CInt(dt.Rows(0).Item("suu")), "")
                Catch ex As Exception
                    BaoGongDA.DelAllData(cd, no)
                    Return JsonConvert.SerializeObject(New With {
                        .result = "NG",
                        .msg = "初期化报工list与Ms数据失败！"
                    })
                End Try
            End If
        Else
            If (BaoGongDA.SelListData(cd, no).Rows.Count = 0) Then
                Try

                    BaoGongDA.InsListData(cd, no, insUser, CInt(dt.Rows(0).Item("suu")), CInt(dt.Rows(0).Item("tuopan_syu_suu")), line_cd)
                    BaoGongDA.InsMSData(cd, no, insUser, CInt(dt.Rows(0).Item("suu")), CInt(dt.Rows(0).Item("tuopan_syu_suu")), line_cd, "")
                    ''只添加一览数据
                    ''suu：计划.生产数量
                    'If Not line_cd.Contains("983") Then
                    '    BaoGongDA.InsListData(cd, no, insUser, CInt(dt.Rows(0).Item("suu")), CInt(dt.Rows(0).Item("tuopan_syu_suu")), line_cd)
                    '    BaoGongDA.InsMSData(cd, no, insUser, CInt(dt.Rows(0).Item("suu")), CInt(dt.Rows(0).Item("tuopan_syu_suu")), line_cd)

                    'Else
                    '    BaoGongDA.InsListData983(cd, no, insUser, CInt(dt.Rows(0).Item("suu")), CInt(dt.Rows(0).Item("tuopan_syu_suu")), line_cd)
                    '    BaoGongDA.InsMSData983(cd, no, insUser, CInt(dt.Rows(0).Item("suu")), CInt(dt.Rows(0).Item("tuopan_syu_suu")), line_cd)

                    'End If
                Catch ex As Exception
                    BaoGongDA.DelAllData(cd, no)
                    Return JsonConvert.SerializeObject(New With {
                        .result = "NG",
                        .msg = "初期化报工list与Ms数据失败！"
                    })
                End Try
            End If
        End If

        Return ""
    End Function



    '报工Panel
    <WebMethod()>
    Public Function GetBgPanel(ByVal cd As String, ByVal no As String, ByVal IsBaogong As Boolean, ByVal IsGuanlianTuopan As Boolean) As Object

        Dim BaoGongDA As New BaoGongDA
        Dim sbRtv As New StringBuilder
        Dim dtBgMs As DataTable = (New GKL_BgDA).GetBgMSData(cd, no)

        sbRtv.AppendLine("<table width='1280px' class='bg_pl'>") 'style='width:1000px;
        sbRtv.AppendLine("<tr>")
        sbRtv.AppendLine("<th width='180px'>工单号</th>")
        sbRtv.AppendLine("<th width='100px'>序号</th>")
        sbRtv.AppendLine("<th width='100px'>捆包数</th>")
        sbRtv.AppendLine("<th width='100px' style=''>报工</th>")
        sbRtv.AppendLine("<th width='100px'>托盘号</th>")
        sbRtv.AppendLine("<th>新托盘号</th>")
        sbRtv.AppendLine("<th width='100px'>关联</th>")
        sbRtv.AppendLine("</tr>")
        '取得已经装载的数量

        For i As Integer = 0 To dtBgMs.Rows.Count - 1
            sbRtv.AppendLine("<tr")
            sbRtv.AppendLine(" tp_no='" & dtBgMs.Rows(i).Item("tp_no").ToString() & "'")
            sbRtv.AppendLine(" bg_suu='" & dtBgMs.Rows(i).Item("bg_suu").ToString() & "'")
            sbRtv.AppendLine(" tp_bar_cd='" & dtBgMs.Rows(i).Item("tp_bar_cd").ToString() & "'")
            sbRtv.AppendLine(">")
            sbRtv.AppendLine("<td>" & no & "</td>")                                         '工单号
            sbRtv.AppendLine("<td>" & dtBgMs.Rows(i).Item("tp_no").ToString() & "</td>")    '序号
            sbRtv.AppendLine("<td>" & dtBgMs.Rows(i).Item("bg_suu").ToString() & "</td>")   '捆包数

            VStr(dtBgMs.Rows(i)("bg_result"))
            '报工

            If VStr(dtBgMs.Rows(i)("bg_result")) = "OK" Then
                sbRtv.AppendLine("<td class='greenBg'>")
                sbRtv.AppendLine("OK")
            ElseIf VStr(dtBgMs.Rows(i)("bg_result")) = "NG" Then
                sbRtv.AppendLine("<td class='redBg'>")
                sbRtv.AppendLine(VStr(dtBgMs.Rows(i)("bg_result")) & "  ")
                If IsBaogong Then
                    sbRtv.AppendLine("<input type='button' class='msBgBtn' value='报工' />")
                End If
            Else
                sbRtv.AppendLine("<td class=''>")
                sbRtv.AppendLine(VStr(dtBgMs.Rows(i)("bg_result")) & "  ")
                If IsBaogong Then
                    sbRtv.AppendLine("<input type='button' class='msBgBtn' value='报工' />")
                End If
            End If
            sbRtv.AppendLine("</td>")

            '托盘号tp_bar_cd


            If VStr(dtBgMs.Rows(i)("tp_bar_cd")) <> "" Then
                sbRtv.AppendLine("<td class='greenBg'>")
                sbRtv.AppendLine(VStr(dtBgMs.Rows(i)("tp_bar_cd")))

            Else
                sbRtv.AppendLine("<td class=''>")
                sbRtv.AppendLine(" <input type='text'  typ='scan' class='msBgTPNO old_tp_ipt' value='" & VStr(dtBgMs.Rows(i)("tp_bar_cd")) & "' />")
            End If
            sbRtv.AppendLine("</td>")
            If VStr(dtBgMs.Rows(i)("tp_bar_cd")) <> "" Then
                sbRtv.AppendLine("<td>")
                sbRtv.AppendLine(" ⇛ " & "<input type='text'  typ='scan' class='msBgTPNO' value='' />")
                sbRtv.AppendLine("</td>")
            Else
                sbRtv.AppendLine("<td>")
                sbRtv.AppendLine("</td>")
            End If

            sbRtv.AppendLine("<td>")
            If VStr(dtBgMs.Rows(i)("tp_bar_cd")) = "" Then
                sbRtv.AppendLine("<input type='button' class='msBgTPNO_GL' value='关联' />")
            Else
                sbRtv.AppendLine("<input type='button' class='msBgTPNO_EDIT' value='修改' />")
            End If
            sbRtv.AppendLine("</td>")
            sbRtv.AppendLine("</tr>")
        Next
        sbRtv.AppendLine("</table>") 'style='width:1000px;

        Return (New With {
        .result = "OKPANEL",
        .panelHTML = sbRtv.ToString
        })
    End Function

    '点击【关联托盘】时，调用接口关联托盘
    <WebMethod()>
    Public Function WB_InsertTrayData(ByVal line_cd As String, ByVal no As String, ByVal cd As String,
                                          ByVal insUser As String, ByVal insUserName As String,
                                          ByVal chk_id As String, ByVal tp_bar_cd As String, ByVal tp_no As String) As String
        Dim BaoGongDA As New BaoGongDA
        Dim BC As New TCheckResultBC
        Dim OKNG As String = "OK"
        Dim sbRtv As New StringBuilder

        'v_bg_list  获得报工list
        Dim dt As DataTable = (New GKL_BgDA).SelTCheckResultOkSuu(no, cd, line_cd)

        '获得检查结果  chk_result
        Dim dtRlt As DataTable = BC.GetResultByChkNo(chk_id)

        '关联托盘时
        Dim jsonRtvTP As Object =
                Run_InsertTrayData(cd, no, line_cd, dt.Rows(0)("suu").ToString(), tp_no, tp_bar_cd, insUser, insUserName)

        If (jsonRtvTP.result = "OK") Then
            Return JsonConvert.SerializeObject(New With {
                .result = "OK",
                .msg = ""
            })

        Else
            Return JsonConvert.SerializeObject(New With {
                .result = "WA",
                .msg = jsonRtvTP.msg
            })

        End If

    End Function

    '点击【修正托盘】时，调用接口关联托盘
    <WebMethod()>
    Public Function Edit_UpdateTrayData(ByVal line_cd As String, ByVal no As String, ByVal cd As String, ByVal insUser As String,
                                        ByVal insUserName As String, ByVal chk_id As String, ByVal tp_bar_cd1 As String, ByVal tp_bar_cd2 As String,
                                        ByVal tp_no As String) As String

        Dim BaoGongDA As New BaoGongDA
        Dim BC As New TCheckResultBC
        Dim dt As DataTable = (New GKL_BgDA).SelTCheckResultOkSuu(no, cd, line_cd)
        'Dim tp_no As String = BaoGongDA.NewTpNo(cd, no)

        Dim UpdTrayDataRtv As String = UpdateTrayData(cd, no, line_cd, dt.Rows(0)("suu").ToString(), tp_no, tp_bar_cd1, tp_bar_cd2, insUser, insUserName)



        If (UpdTrayDataRtv Is Nothing) OrElse UpdTrayDataRtv = "" Then

            'JsonConvert.SerializeObject({ff:  "aa"})
            Return JsonConvert.SerializeObject(New With {
                    .result = "NG",
                    .msg = "修改托盘、生产明细书对应关系时，调用接口出错！"
                })
        Else
            Try
                Dim jsonRtv As Object = JsonConvert.DeserializeObject(UpdTrayDataRtv)
                '         //dynamic jsonObj = JsonConvert.DeserializeObject(resMsgStr);
                If (jsonRtv("isSuccess")) Then
                    BaoGongDA.UpdMSOneRowData(cd, no, tp_no, tp_bar_cd2)
                Else
                    Return JsonConvert.SerializeObject(New With {
                            .result = "NG",
                            .msg = "修改托盘、生产明细书对应关系时：" & jsonRtv("message")
                        })
                End If
            Catch ex As Exception
                Return JsonConvert.SerializeObject(New With {
                        .result = "NG",
                        .msg = "修改托盘、生产明细书对应关系时：" & ex.Message
                    })
            End Try

        End If

        Return JsonConvert.SerializeObject(New With {
            .result = "OK",
            .msg = "OK:修改托盘、生产明细书对应关系完成"
        })

    End Function


    '调用报工接口
    <WebMethod()>
    Public Function RunBgOnlyService(ByVal no As String,
                                    ByVal cd As String,
                                    ByVal insUser As String,
                                    ByVal tp_bar_cd As String,
                                    ByVal tp_no As String) As Object

        Try
            Dim BGAcion2 As New BGAcion
            Dim lstBgno As New List(Of String)
            Dim lstBarScanNo As New List(Of String)
            lstBgno.Add(tp_no)
            lstBarScanNo.Add(tp_bar_cd)

            Dim rtv As String = BGAcion2.RunBG(cd, no, lstBgno, lstBarScanNo, insUser, insUser, "手动")

            If rtv = "" Then
                Return (New With {
                                    .result = "OK",
                                    .msg = "报工完了！"
                                 })
            Else
                Return (New With {
                                    .result = "NG",
                                    .msg = rtv
                                 })
            End If
        Catch ex As Exception
            Return (New With {
                                .result = "WA",
                                .msg = "报工出错：" + ex.Message
                             })
        End Try

    End Function


























































    Public Function Run_InsertTrayData(cd As String, no As String, line_cd As String,
                                       suu As String, tp_no As String, tp_bar_cd As String,
                                       insUser As String, insUserName As String) As Object


        Dim BaoGongDA As New BaoGongDA

        Dim InsertTrayDataRtv As String = InsertTrayData(cd, no, line_cd, suu, tp_no, tp_bar_cd, insUser, insUserName)

        If (InsertTrayDataRtv Is Nothing) OrElse InsertTrayDataRtv = "" Then
            'JsonConvert.SerializeObject
            Return (New With {
                .result = "WA",
                .msg = "关联托盘时 返回对象为空"
            })
        Else
            Try
                Dim jsonRtv As Object = JsonConvert.DeserializeObject(InsertTrayDataRtv)
                '//dynamic jsonObj = JsonConvert.DeserializeObject(resMsgStr);
                If (jsonRtv("isSuccess").value) Then
                    '只添加明细
                    BaoGongDA.UpdMSOneRowData(cd, no, tp_no, tp_bar_cd)
                Else
                    Return (New With {
                        .result = "WA",
                        .msg = "关联托盘时出错：" & jsonRtv("message").value
                    }) 'jsonRtv.message
                End If

            Catch ex As Exception
                Return (New With {
                    .result = "WA",
                    .msg = "关联托盘时出错：" & ex.Message
                })
            End Try

        End If

        Return (New With {
            .result = "OK",
            .msg = "OK 关联托盘时 成功"
        })

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Function InsertTrayData(ByVal cd As String,
                                    ByVal no As String,
                                    ByVal lineid As String,
                                    ByVal bg_suu As String,
                                    ByVal tp_no As String,
                                    ByVal no1 As String,
                                    ByVal user_id As String,
                                    ByVal user_name As String) As String

        Dim da As New BaoGongDA
        Dim dt As DataTable = da.SelBgListByCd(cd, no, lineid)

        Dim dtBgMs As Data.DataTable = （New BaoGongDA2）.SelMSData(cd, no)
        Dim drs() As DataRow = dtBgMs.Select("tp_no=" & tp_no & "")
        Dim qr As String = drs(0).Item("bg_bar_data")

        'Dim qr As String = dt.Rows(0).Item("ProductCodeSap") & "/" & dt.Rows(0).Item("ProductCode") & "/" & bg_suu & "/" &
        '     CInt(Math.Ceiling(bg_suu / CInt(dt.Rows(0).Item("Package")))) & "/" & dt.Rows(0).Item("localStorage") & "/" & dt.Rows(0).Item("DestinationCode") &
        '              "/" & tp_no.ToString & "/" & dt.Rows(0).Item("ZuoFan")


        Dim formid As String = "01"

        Dim TP As New TPVB

        Dim rtv As String = TP.InsertTrayData(no, formid, qr, no1, user_id, user_name)

        Return rtv

    End Function

    Public Function UpdateTrayData(ByVal cd As String,
                                    ByVal no As String,
                                    ByVal lineid As String,
                                    ByVal bg_suu As String,
                                    ByVal tp_no As String,
                                    ByVal no1 As String,
                                    ByVal no2 As String,
                                    ByVal user_id As String,
                                    ByVal user_name As String) As String
        Dim da As New BaoGongDA
        Dim dt As DataTable = da.SelBgListByCd(cd, no, lineid)

        Dim dtBgMs As Data.DataTable = （New BaoGongDA2）.SelMSData(cd, no)
        Dim drs() As DataRow = dtBgMs.Select("tp_no=" & tp_no & "")
        Dim qr As String = drs(0).Item("bg_bar_data")

        'Dim qr As String = dt.Rows(0).Item("ProductCodeSap") & "/" & dt.Rows(0).Item("ProductCode") & "/" & bg_suu & "/" &
        '     CInt(Math.Ceiling(bg_suu / CInt(dt.Rows(0).Item("Package")))) & "/" & dt.Rows(0).Item("localStorage") & "/" & dt.Rows(0).Item("DestinationCode") &
        '              "/" & tp_no.ToString & "/" & dt.Rows(0).Item("ZuoFan")

        Dim formid As String = "01"
        Dim TP As New TPVB

        Dim rtv As String = TP.UpdateTrayData(no, formid, qr, no1, no2, user_id, user_name)

        Return rtv

    End Function



    Public Function BaoGongFnc(ByVal line_cd As String, ByVal cd As String, ByVal no As String,
                                ByVal insUser As String, ByVal insUserName As String,
                                ByVal chk_id As String, ByVal tp_bar_cd As String,
                                ByVal dt As DataTable, ByVal dtRlt As DataTable, ByVal tp_no As String) As Object


        Dim BaoGongDA As New BaoGongDA
        Dim BC As New TCheckResultBC

        Dim txt As String = ""

        Try
            txt = "1.准备报工,v_bg_list_new:" & dt.Rows.Count.ToString() & ",t_check:" + dtRlt.Rows.Count.ToString()
            If dt.Rows.Count > 0 AndAlso dtRlt.Rows.Count > 0 Then
                txt = txt & "--ok_suu:" & dt.Rows(0)("ok_suu").ToString()
                txt = txt & "--bg_result:" & dt.Rows(0)("bg_result").ToString()
            End If
        Catch e3 As Exception
            txt = "文本准备出错" & e3.Message.Replace(ChrW(13), ChrW(0)).Replace(ChrW(10), ChrW(0)).Replace(",", "").Substring(0, 450)
            Return (New With {
                        .result = "WA",
                        .msg = "文本准备出错：" + e3.Message
                    })
        End Try

        Try
            BC.InsBaogongRireki(chk_id, no, cd, line_cd, txt)
        Catch e2 As Exception

        End Try

        If dt.Rows.Count > 0 AndAlso dtRlt.Rows.Count > 0 Then
            'If Convert.ToInt32(dt.Rows(0)("ok_suu").ToString()) >= 2 AndAlso (dt.Rows(0)("bg_result") = "" OrElse dt.Rows(0)("bg_result") = "NG") Then

            If (dt.Rows(0)("bg_result") = "" OrElse dt.Rows(0)("bg_result") = "NG") Then

                If Common.NullToEmpty(dtRlt.Rows(0)("result").ToString()) = "OK" Then

                    '登录初期一览
                    If (BaoGongDA.SelListData(cd, no).Rows.Count = 0) Then
                        Try

                            If CInt(dt.Rows(0).Item("suu")) <= CInt(CInt(dt.Rows(0).Item("tuopan_syu_suu"))) Then
                                '只添加明细
                                BaoGongDA.InsListData(cd, no, insUser, CInt(dt.Rows(0).Item("suu")), CInt(dt.Rows(0).Item("tuopan_syu_suu")), line_cd)
                                '只添加明细
                                BaoGongDA.InsMSOneRowData(cd, no, insUser, CInt(dt.Rows(0).Item("suu")), CInt(dt.Rows(0).Item("tuopan_syu_suu")), line_cd, tp_no, CInt(dt.Rows(0).Item("suu")), tp_bar_cd)

                            End If


                        Catch ex As Exception
                            BaoGongDA.DelAllData(cd, no)
                            Return (New With {
                                        .result = "WA",
                                        .msg = "报工出错：" + ex.Message
                                    })

                        End Try
                    End If

                Else
                    Return (New With {
                                .result = "WA",
                                .msg = "检查结果没有全部OK，不能报工"
                            })
                End If

            Else
                'Return "检查结果没有全部OK，不能报工"
                'sbRtv.AppendLine(" <a class='greenMsg'>报工状态已经是OK了，不能再报工</a>")
                'OKNG = "IR"
                Return (New With {
                                .result = "WA",
                                .msg = "报工状态已经是OK了，不能再报工"
                            })
            End If

        Else
            'Return "检查结果没有全部OK，不能报工"
            'sbRtv.AppendLine(" <a class='errorMsg'>检查结果没有全部OK，不能报工</a>")
            'OKNG = "IR"
            Return (New With {
                                .result = "WA",
                                .msg = "检查结果没有全部OK，不能报工"
                            })
        End If


        '调用报工接口
        Return RunBgService(no, cd, insUser, tp_bar_cd, tp_no)

    End Function

    '调用报工接口
    <WebMethod()>
    Public Function RunBgService(ByVal no As String,
                                    ByVal cd As String,
                                    ByVal insUser As String,
                                    ByVal tp_bar_cd As String,
                                    ByVal tp_no As String) As Object

        Try
            Dim BGAcion2 As New BGAcion
            Dim lstBgno As New List(Of String)
            Dim lstBarScanNo As New List(Of String)
            lstBgno.Add(tp_no)
            lstBarScanNo.Add(tp_bar_cd)

            Dim rtv As String = BGAcion2.RunBG(cd, no, lstBgno, lstBarScanNo, insUser, insUser, "手动")

            If rtv = "" Then
                Return (New With {
                                    .result = "OK",
                                    .msg = "托盘呼出完了，报工完了！"
                                 })
            Else
                Return (New With {
                                    .result = "NG",
                                    .msg = rtv
                                 })
            End If



        Catch ex As Exception

            Return (New With {
                                .result = "WA",
                                .msg = "报工出错：" + ex.Message
                             })
        End Try

    End Function











    '做成报工的list ms 表数据
    Public Function InsBgListAndMsData(line_cd As String, cd As String, no As String, insUser As String _
            , insUserName As String, chk_id As String, tp_bar_cd As String, dt As DataTable, dtRlt As DataTable) As Object

        Dim BaoGongDA As New BaoGongDA
        Dim BC As New TCheckResultBC

        '****1.登录报工履历
        Dim txt As String = ""
        Try
            txt = "1.准备报工,v_bg_list_new:" & dt.Rows.Count.ToString() & ",t_check:" + dtRlt.Rows.Count.ToString()
            If dt.Rows.Count > 0 AndAlso dtRlt.Rows.Count > 0 Then
                txt = txt & "--ok_suu:" & dt.Rows(0)("ok_suu").ToString()
                txt = txt & "--bg_result:" & dt.Rows(0)("bg_result").ToString()
            End If

        Catch e3 As Exception
            txt = "文本准备出错" & e3.Message.Replace(ChrW(13), ChrW(0)).Replace(ChrW(10), ChrW(0)).Replace(",", "").Substring(0, 450)
        End Try

        Try
            BC.InsBaogongRireki(chk_id, no, cd, line_cd, txt)
        Catch e2 As Exception

        End Try

        '****2.登录报工一览与明细
        'dt   ：v_bg_list  m_plan_gnfm，[t_check]，[m_baogong_ms_new]，[m_baogong_list_new]
        'dtRlt：获得检查结果  chk_result
        If dt.Rows.Count > 0 AndAlso dtRlt.Rows.Count > 0 Then
            '报工一览的 result
            If (dt.Rows(0)("bg_result") = "" OrElse dt.Rows(0)("bg_result") = "NG") Then
                '检查结果是OK
                If Common.NullToEmpty(dtRlt.Rows(0)("result").ToString()) = "OK" Then

                    '登录初期一览
                    If (BaoGongDA.SelListData(cd, no).Rows.Count = 0) Then
                        Try
                            '只添加一览数据
                            'suu：计划.生产数量
                            BaoGongDA.InsListData(cd, no, insUser, CInt(dt.Rows(0).Item("suu")), CInt(dt.Rows(0).Item("tuopan_syu_suu")), line_cd)
                            BaoGongDA.InsMSData(cd, no, insUser, CInt(dt.Rows(0).Item("suu")), CInt(dt.Rows(0).Item("tuopan_syu_suu")), line_cd, tp_bar_cd)

                            'If line_cd.Contains("983") Then
                            '    BaoGongDA.InsListData(cd, no, insUser, CInt(dt.Rows(0).Item("suu")), CInt(dt.Rows(0).Item("tuopan_syu_suu")), line_cd)
                            '    BaoGongDA.InsMSData(cd, no, insUser, CInt(dt.Rows(0).Item("suu")), CInt(dt.Rows(0).Item("tuopan_syu_suu")), line_cd)

                            'Else
                            '    BaoGongDA.InsListData983(cd, no, insUser, CInt(dt.Rows(0).Item("suu")), CInt(dt.Rows(0).Item("tuopan_syu_suu")), line_cd)
                            '    BaoGongDA.InsMSData983(cd, no, insUser, CInt(dt.Rows(0).Item("suu")), CInt(dt.Rows(0).Item("tuopan_syu_suu")), line_cd)

                            'End If

                        Catch ex As Exception
                            BaoGongDA.DelAllData(cd, no)
                            Return (New With {
                                        .result = "WA",
                                        .msg = "报工出错：" + ex.Message
                                    })
                        End Try
                    End If

                Else
                    Return (New With {
                                .result = "WA",
                                .msg = "检查结果没有全部OK，不能报工"
                            })
                End If
            End If

        Else
            Return (New With {
                    .result = "WA",
                    .msg = "没有报工数据，不能报工"
                })
        End If

        Return (New With {
            .result = "OK",
            .msg = ""
        })

    End Function



    Public Function VStr(ByVal obj As Object) As String
        If obj Is DBNull.Value Then
            Return ""
        End If

        If (String.IsNullOrEmpty(obj)) Then
            Return ""
        Else
            Return obj.ToString
        End If
    End Function






    <WebMethod()>
    Public Function RunBg(ByVal line_cd As String, ByVal no As String, ByVal cd As String, ByVal insUser As String, ByVal tp_bar_cd As String _
    , ByVal bg_suu As String _
    , ByVal suu As String _
    , ByVal tuopan_syu_suu As String) As String
        Dim BC As New TCheckResultBC
        'Dim line_cd As String = Request.Form("line_cd")
        'Dim no As String = Request.Form("no")
        'Dim cd As String = Request.Form("cd")
        'Dim insUser As String = Request.Form("insUser")
        'Dim tp_no As String = Request.Form("tp_no")
        'Dim tp_bar_cd As String = Request.Form("tp_bar_cd")

        'line_cd: line_cd,
        '            no: no,
        '            cd: cd,
        '            insUser: insUser,
        '            tp_bar_cd:  $("#tp_bar_scan").val(),
        '            suu: $("#suu").text(),
        '            tuopan_syu_suu: $("#tuopan_syu_suu").text()


        'BaoGongDA.InsMSData(cd, no, insUser, CInt(dt.Rows(0).Item("suu")), CInt(dt.Rows(0).Item("tuopan_syu_suu")), line_cd)
        Dim BaoGongDA As New BaoGongDA2

        Dim tp_no As Integer = BaoGongDA.NewTpNo(cd, no)

        BaoGongDA.InsMSOneRowData(cd, no, insUser, CInt(suu), CInt(tuopan_syu_suu), line_cd, tp_no, bg_suu, tp_bar_cd)


        Return "OK"

        Dim BGAcion2 As New BGAcion

        Dim lstBgno As New List(Of String)
        Dim lstBarScanNo As New List(Of String)

        'Dim lstBgSendStr As New List(Of String)

        lstBgno.Add(tp_no)
        lstBarScanNo.Add(tp_bar_cd)
        'lstBgSendStr.Add("WNWAP890/NWZA890/40/10/4102//1/9999988885")

        BGAcion2.RunBG(cd, no, lstBgno, lstBarScanNo, insUser, insUser, "手动")

        lstBgno.Clear()
        lstBgno = Nothing

        BGAcion2.Dispose()
        BGAcion2 = Nothing

        'InitMs()

        GC.Collect()
        GC.WaitForPendingFinalizers()

        'Response.Write("OK")
        'Response.End()

        Return "OK"

    End Function



End Class