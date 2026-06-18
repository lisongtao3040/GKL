<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Bg_ms.aspx.vb" Inherits="Bg_ms" %>

<%@ Register Src="~/UserCtrl/Links.ascx" TagPrefix="uc1" TagName="Links" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>报工一览</title>

    <!--JS-->
    <script type="text/javascript" src="./jquery/jquery-3.6.0.min.js"></script>
    <script type="text/javascript" src="./jquery-ui-1.12.1/jquery-ui.min.js"></script>
    <script type="text/javascript" src="./jquery/jquery.cookie.js"></script>
    <link rel="stylesheet" href="./jquery-ui-1.12.1/jquery-ui.css" />
    <!--CSS-->
    <link href="tmp.css" rel="stylesheet" type="text/css" />
    <style>
        div.ui-datepicker {
            font-size: 30px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class='title_div' style="width: 99%; height: 50px; line-height: 50px; vertical-align: middle; ">

            <div style="float: left; border-radius: 25px; margin: 5px 0px; width: 50px; height: 40px; background-color: #fff; text-align: center; color: #000; font-size: 42px;">
                🍄
            </div>
            <div style="float: left; padding-left: 4px;">
                <% Response.Write(Common.SetTitle("报工记录"))%>
            </div>
        </div>
        <div class="links_div">
            <uc1:Links runat="server" ID="Links" />
        </div>
        <div style="font-size: 30px;">
            工单号：<asp:Label ID="lblNo" runat="server" Text="Label"></asp:Label>&nbsp;&nbsp;
            CODE：<asp:Label ID="lblCd" runat="server" Text="Label"></asp:Label>&nbsp;&nbsp;
            检查OK数：<asp:Label ID="lblOkSuu" runat="server" Text="Label"></asp:Label>
            <br />
            计划数量：<asp:Label ID="lblSuu" runat="server" Text="Label"></asp:Label>&nbsp;&nbsp;
            捆包数：<asp:Label ID="kbsuu" runat="server" Text="Label"></asp:Label>
            <br />
            托盘入数：<asp:Label ID="tpNyuSuu" runat="server" Text="Label"></asp:Label>&nbsp;&nbsp;
            托盘数：<asp:Label ID="tpSuu" runat="server" Text="Label"></asp:Label>
            <asp:Button ID="btnBack" runat="server" Text="返回"  Height="40px" Width="120"/>

                        <asp:Button ID="btnBgAll" runat="server" Text="全部报工"  Height="40px" Width="160"/>
            <hr />

            <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <Columns>

                    <asp:BoundField DataField="tp_no" HeaderText="托盘序号" />
                    <asp:BoundField DataField="bg_suu" HeaderText="数量" />
                    <asp:BoundField DataField="bg_result" HeaderText="结果" />
                    <asp:BoundField DataField="bg_txt" HeaderText="消息文本" />
                    <asp:BoundField DataField="updateDate" HeaderText="时间" />
                    <asp:BoundField DataField="bg_user" HeaderText="操作担当" />
                    <asp:BoundField DataField="bg_type" HeaderText="操作方式" />
                    <asp:TemplateField HeaderText="手动报工">
                        <ItemTemplate>
                            <asp:Button ID="btnBG" runat="server" Text="报工" Height="40px" />
                        </ItemTemplate>
                    </asp:TemplateField>
                        <asp:BoundField DataField="bg_bar_data" HeaderText="报工发送数据" />
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
        </div>
        <asp:HiddenField ID="hidCd" runat="server" />
        <asp:HiddenField ID="hidNo" runat="server" />
        <asp:HiddenField ID="hidBGNo" runat="server" />

        <asp:Button ID="btnBG" runat="server" Text="Button" style="display:none" />

        <script>

            function GoBG(cd, no, BGNo) {
                $("#hidCd").val(cd);
                $("#hidNo").val(no);
                $("#hidBGNo").val(BGNo);
                $("#btnBG").click();
            }

            $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
            $(".jqTxtDate").datepicker({
                dateFormat: "yy-mm-dd"
            });


        </script>
    </form>
</body>
</html>
