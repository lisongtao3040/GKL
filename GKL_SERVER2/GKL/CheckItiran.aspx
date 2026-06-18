<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckItiran.aspx.cs" Inherits="CheckItiran" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>检查一览</title>

    <!--JS-->
    <script language="javascript" type="text/javascript" src="./js/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="./JidouTemp.js?version=20190716"></script>
    <script language="javascript" type="text/javascript" src="./CheckItiran.aspx.js?version=20260810"></script>
    <script language="javascript" type="text/javascript" src="./JsBarcode.js"></script>
    <script language="javascript" type="text/javascript" src="./Qrcode.js"></script>
    <!--CSS-->
    <link href="tmp_chk.css" rel="stylesheet" type="text/css" />
    <link href="CheckItiran.css" rel="stylesheet" type="text/css" />

    <style>
        #loadingOverlay {
            display: none;
            position: absolute; /* IE6 模拟全屏 */
            top: 0;
            left: 0;
            z-index: 9999;
            background-color: #000;
            /* 现代浏览器透明度 */
            opacity: 0.5;
            /* IE6-8 滤镜透明度 */
            filter: alpha(opacity=50);
        }

        /* 关键：让 iframe 撑满父容器并完全透明 */
        .ie6-iframe-shim {
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            z-index: -1;
            filter: alpha(opacity=0);
            opacity: 0;
        }

        .loading-content {
            position: absolute;
            top: 45%;
            left: 0;
            width: 100%;
            text-align: center;
            color: #fff;
            font-weight: bold;
            font-size: 120px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <div id="loadingOverlay" style="display: none;">
            <iframe class="ie6-iframe-shim" frameborder="0" src="about:blank"></iframe>
            <div class="loading-content">正在处理，请稍候...</div>
        </div>
        <div class='title_div' style="width: 99%; height: 50px; line-height: 50px; vertical-align: middle; background: url(IMG/banner2.jpg)">

            <div style="float: left; border-radius: 25px; margin: 5px 0px; width: 50px; height: 40px; background-color: #fff; text-align: center; color: #000; font-size: 42px;">
                🍄
            </div>
            <div style="float: left; padding-left: 4px;">
                <% Response.Write(Common.SetTitle("检查一览")); %>
            </div>
        </div>
        <div style="width: 1040px;">

            <%--            <div class='title_div' style="width: 1000px; height: 80px; font-size: 50px;">
                <%Response.Write(Common.SetTitle("检查一览"));%>
            </div>--%>
            <!--Button部-->


            <!--明細Title部-->
            <div id="div1" class='jq_title_div' runat="server" style="overflow: hidden; margin-left: 0px; width: 1044px; margin-top: 0px; border-collapse: collapse;">
                <table class="ms_title" style="width: 1040px;" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 50px;">No
                        </td>
                        <td style="width: 130px;">作番
                        </td>
                        <td style="width: 210px;">コード
                        </td>
                        <td style="width: 40px;">数量
                        </td>
                        <td style="width: 40px;">次数 
                        </td>
                        <td style="width: 60px;">检查模板编号
                        </td>
                        <td style="width: 60px;">检查结果
                        </td>
                        <td style="width: 110px;">检查者
                        </td>
                        <td style="width: 118px;">预定检查日
                        </td>
                        <td style="width: 60px;">欠品
                        </td>
                        <td style="">状态
                        </td>
                    </tr>
                </table>

            </div>
            <!--明細Body部-->
            <div id="divGvw" class='jq_ms_div' runat="server" style="overflow: scroll; height: 1230px; margin-left: 0px; width: 1060px; margin-top: 0px; border-collapse: collapse; font-family: Consolas; font-size: 28px;">

                <asp:GridView CssClass="jq_ms" Width="1040px" runat="server" ID="gvMs" EnableTheming="True" ShowHeader="False" AutoGenerateColumns="False" BorderColor="black" Style="margin-top: -1px;" TabIndex="-1">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate><%#Eval("No")%></ItemTemplate>
                            <ItemStyle Width="50px" Height="60px" HorizontalAlign="Left" Font-Size="24px" CssClass="jq_chk_no" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate><%#Eval("make_no")%></ItemTemplate>
                            <ItemStyle Width="130px" HorizontalAlign="Left" CssClass="jq_make_no" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate><%#Eval("code")%></ItemTemplate>
                            <ItemStyle Width="210px" HorizontalAlign="Left" CssClass="jq_code" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate><%#Eval("suu")%></ItemTemplate>
                            <ItemStyle Width="40px" HorizontalAlign="Center" CssClass="jq_suu" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate><%#Eval("chk_times")%></ItemTemplate>
                            <ItemStyle Width="40px" HorizontalAlign="Center" CssClass="jq_chk_times" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate><%#Eval("temp_id")%></ItemTemplate>
                            <ItemStyle Width="60px" HorizontalAlign="Center" CssClass="jq_temp_id" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate><%#Eval("chk_result")%></ItemTemplate>
                            <ItemStyle Width="60px" HorizontalAlign="Center" CssClass="jq_chk_result" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <%#Eval("chk_user")%>
                                <br />
                                <%#Eval("user_name")%>
                            </ItemTemplate>
                            <ItemStyle Width="110px" HorizontalAlign="Center" CssClass="jq_chk_user" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate><%#Eval("yotei_chk_date")%></ItemTemplate>
                            <ItemStyle Width="118px" Font-Size="20px" HorizontalAlign="Center" CssClass="jq_chk_start_date" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <input id="Button1" type="button" value="<%#Eval("qianpin_suu")%>" onclick="OpenQianpin(this,'Qianpin.aspx?chk_no=<%#Eval("chk_no")%>    ')" style='visibility: <%#  IsVis(Eval("qianpin_suu").ToString())%>' />
                            </ItemTemplate>
                            <ItemStyle Width="60px" Font-Size="20px" HorizontalAlign="Center" CssClass="jq_chk_start_date" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate><%#Eval("status")%></ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" CssClass="jq_status" />
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
            </div>
            <div style="height: 1px;">
                <asp:TextBox ID="hidChkNo" runat="server" CssClass="jq_chk_no_ipt" Style="visibility: hidden;"></asp:TextBox>
                <asp:TextBox ID="hidNen" runat="server" CssClass="jq_nen_ipt" Style="visibility: hidden;"></asp:TextBox>
                <asp:TextBox ID="hidPlanNo" runat="server" CssClass="jq_plan_no_ipt" Style="visibility: hidden;"></asp:TextBox>
                <asp:TextBox ID="hidLineId" runat="server" CssClass="jq_line_id_ipt" Style="visibility: hidden;"></asp:TextBox>
                <asp:TextBox ID="hidMakeNo" runat="server" CssClass="jq_make_no_ipt" Style="visibility: hidden;"></asp:TextBox>
                <asp:TextBox ID="hidCode" runat="server" CssClass="jq_code_ipt" Style="visibility: hidden;"></asp:TextBox>
                <asp:TextBox ID="hidSuu" runat="server" CssClass="jq_suu_ipt" Style="visibility: hidden;"></asp:TextBox>
                <asp:TextBox ID="hidTempId" runat="server" CssClass="jq_temp_id_ipt" Style="visibility: hidden;"></asp:TextBox>
                <asp:TextBox ID="hidChkResult" runat="server" CssClass="jq_chk_result_ipt" Style="visibility: hidden;"></asp:TextBox>
                <asp:TextBox ID="hidChkUser" runat="server" CssClass="jq_chk_user_ipt" Style="visibility: hidden;"></asp:TextBox>
                <asp:TextBox ID="hidChkStartDate" runat="server" CssClass="jq_chk_start_date_ipt" Style="visibility: hidden;"></asp:TextBox>
                <asp:TextBox ID="hidChkEndDate" runat="server" CssClass="jq_chk_end_date_ipt" Style="visibility: hidden;"></asp:TextBox>
                <asp:TextBox ID="hidParentChkNo" runat="server" CssClass="jq_parent_chk_no_ipt" Style="visibility: hidden;"></asp:TextBox>
                <asp:TextBox ID="hidStatus" runat="server" CssClass="jq_status_ipt" Style="visibility: hidden;"></asp:TextBox>
                <asp:TextBox ID="hidInsUser" runat="server" CssClass="jq_ins_user_ipt" Style="visibility: hidden;"></asp:TextBox>
                <asp:TextBox ID="hidInsDate" runat="server" CssClass="jq_ins_date_ipt" Style="visibility: hidden;"></asp:TextBox>
                <asp:TextBox ID="hidOldRowIdx" runat="server" CssClass="jq_hidOldRowIdx" Style="visibility: hidden;"></asp:TextBox>
                <asp:TextBox ID="hidchk_times" runat="server" CssClass="jq_chk_times_ipt" Style="visibility: hidden;"></asp:TextBox>
                <asp:TextBox ID="hidScanFlg" runat="server" CssClass="jq_chk_times_ipt" Style="visibility: hidden;"></asp:TextBox>

                <asp:TextBox ID="hidScroll" runat="server" class="jq_chk_times_ipt" Style="visibility: hidden;"></asp:TextBox>
                <asp:TextBox ID="hidOldChkNo" runat="server" Style="visibility: hidden;"></asp:TextBox>

                <asp:TextBox ID="hidIsQiangzhi_baogong_lines" runat="server" CssClass="" Style="visibility: hidden;"></asp:TextBox>

                <asp:TextBox ID="hidIsMyLine" runat="server" Style="visibility: hidden;"></asp:TextBox>
            </div>
            <hr />
            <!--条件部-->
            <table class='jyouken_panel' cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 100px;">检查者</td>
                    <td style="width: 200px;">
                        <asp:TextBox ID="tbxCheckUser" CssClass="jq_make_no_key" runat="server" Style="background-color: #FFAA00; width: 98%;" Enabled="false"></asp:TextBox>
                        <datalist id="userlist"></datalist>
                    </td>
                    <td style="width: 250px;">
                        <asp:TextBox ID="lblUserName" CssClass="" runat="server" Style="width: 150px; background-color: #fff; color: #000; border: none;" TabIndex="-1" Enabled="false"></asp:TextBox>
                    </td>
                    <td style="width: 200px;">生产线 :
                    </td>
                    <td style="width: 200px;" colspan="2">

                        <asp:TextBox ID="tbxLineId_key" CssClass="jq_line_id_key" runat="server" Style="background-color: #fff; display: none;" Text="" Enabled="false" Width="172px"></asp:TextBox>
                        <datalist id="line_id_list"></datalist>
                        <asp:DropDownList ID="ddl_lines" runat="server" Font-Size="30" AutoPostBack="True" OnSelectedIndexChanged="ddl_lines_SelectedIndexChanged"></asp:DropDownList>
                    </td>


                    <td>&nbsp;</td>

                </tr>
                <tr>
                    <td colspan="1">作番 :</td>
                    <td>
                        <asp:TextBox ID="tbxMakeNo_key" CssClass="jq_make_no_ipt" runat="server" typ="scan" Style="width: 98%; background-color: #FFAA00; ime-mode: disabled;"></asp:TextBox>
                    </td>
                    <td colspan="1">CODE:
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="tbxCode_key" CssClass="jq_code_ipt" runat="server" MaxLength="220" typ="scan" Style="width: 98%; background-color: #FFAA00; ime-mode: disabled;"></asp:TextBox></td>
                    <td>

                        <asp:Button ID="btnInsert" runat="server" Text="新规检查" CssClass="jq_ins" Style="height: 40px;" OnClick="btnInsert_Click" />

                    </td>
                    <td></td>


                </tr>
                <tr>
                    <td colspan="7">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td>检查日</td>
                    <td colspan="3">
                        <asp:Button ID="btnPreDay" runat="server" Text="<" CssClass="jq_sel" Style="width: 50px;" OnClick="btnPreDay_Click" />
                        <asp:TextBox ID="tbxDate_key" CssClass="" runat="server" MaxLength="20" Style="background-color: #FFAA00; ime-mode: disabled;"
                            onkeydown="if(event.keyCode==13){GetDateFormat(this)}" placeholder="7日内"></asp:TextBox>
                        <asp:Button ID="btnNextDay" runat="server" Text=">" CssClass="jq_sel" Style="width: 50px;" OnClick="btnNextDay_Click" />

                        <asp:Button ID="btnSelect" runat="server" Text="検索" CssClass="jq_sel" OnClick="btnSelect_Click" />

                        <asp:Button ID="btnSelect2" runat="server" Text="検索2" CssClass="jq_sel" OnClick="btnSelect2_Click" Style="width: 115px; display: none;" />
                        &nbsp;
            <input type="button" value="总览" id="zongl" style="width: 115px; display: none;" /></td>
                    <td colspan="3">

                        <%--<a href="APP/scanner/scanner.application" style="height:40px;">扫描App</a>--%>


                    </td>


                </tr>

                <tr>
                    <td colspan="7">
                        <asp:Label ID="lblSou" runat="server" Width="913px"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td colspan="7">

                        <div style="width: 1000px; text-align: right;">

                            <!--Button部-->
                            <asp:Button ID="btnUpdate" runat="server" Text="继续检查" CssClass="jq_upd" OnClick="btnUpdate_Click" />
                            &nbsp;<asp:Button ID="btnComlete" runat="server" Text="强制完了" CssClass="" OnClick="btnComlete_Click" />
                            &nbsp;
        <asp:Button ID="btnDelete" runat="server" Text="削除" CssClass="jq_del" OnClick="btnDelete_Click" />
                            &nbsp;
        第
        <asp:DropDownList ID="ddlPageIdx" runat="server" AutoPostBack="true"
            Style="font-size: 22px;"
            Width="60px" Height="42px" OnSelectedIndexChanged="ddlPageIdx_SelectedIndexChanged">
        </asp:DropDownList>
                            /
        <asp:Label ID="lblAllPageText" runat="server" Text="0"></asp:Label>
                            页
        &nbsp;
        <asp:Button ID="btnBack" runat="server" Text="返回" CssClass="jq_back" Style="font-size: 22px;" OnClick="btnBack_Click" Width="120px" Height="42px" />
                        </div>
                    </td>
                </tr>
            </table>

            <table style="width: 100%" class="btmBar">
                <tr>
                    <td style="width: 33%">
                        <svg id="barcodeNo"></svg></td>
                    <td style="">
                        <svg id="barcodeCD"></svg></td>
                    <td style="width: 140px">
                        <div id="barcodeSCMX"></div>
                    </td>
                </tr>
            </table>
            <br />
        </div>
    </form>
    <script language="javascript" type="text/javascript">
        try {
            window.scrollTo(0, document.body.scrollHeight);
            //$(".jq_ms_div")[0].scrollTo(0, 10000);
            $(".jq_ms_div").scrollTop($(".jq_ms").height() - 21);

            if ($("#hidScroll").val() != "") {
                $(".jq_ms_div").scrollTop($("#hidScroll").val());
            }


        } catch (e) {
            alert(e.message)
        }

    </script>
</body>
</html>
