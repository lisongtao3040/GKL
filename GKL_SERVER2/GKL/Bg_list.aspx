<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Bg_list.aspx.vb" Inherits="Bg_list" %>

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
        <div class='title_div' style="width: 99%; height: 50px; line-height: 50px; vertical-align: middle; background: url(IMG/banner2.jpg)">

            <div style="float: left; border-radius: 25px; margin: 5px 0px; width: 50px; height: 40px; background-color: #fff; text-align: center; color: #000; font-size: 42px;">
                🍄
            </div>
            <div style="float: left; padding-left: 4px;">
                <% Response.Write(Common.SetTitle("报工一览"))%>
            </div>
        </div>
        <div class="links_div">
            <uc1:Links runat="server" ID="Links" />
        </div>
        <div style="font-size: 30px;">
            &nbsp;&nbsp;&nbsp;
            生产线：
            <asp:DropDownList ID="ddlLine" runat="server" Width="200" Height="40px" Font-Size="30px">
            </asp:DropDownList>
            &nbsp;&nbsp;
            计划日期：
            <asp:TextBox ID="tbxYmd" runat="server" Width="200" Height="40px" Font-Size="30px" CssClass="jqTxtDate"></asp:TextBox>
            &nbsp;&nbsp;
            <asp:Button ID="btnSel" runat="server" Height="40px" Text="检索" Width="120px" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnBack" runat="server" Text="返回" Width="120px" Height="40px" />
            <hr />


            <asp:Label ID="lblTxt" runat="server" Text=""></asp:Label>
            <hr />
            <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="ZuoFan" HeaderText="作番" />
                    <asp:BoundField DataField="ProductCode" HeaderText="CODE" />
                    <asp:BoundField DataField="suu" HeaderText="计划数量" />
                    <asp:BoundField HeaderText="托盘数" />
                    <asp:BoundField DataField="ok_suu" HeaderText="检查OK次数" />
                    <asp:BoundField DataField="complete_date" HeaderText="报工完了时间" />
                    <asp:BoundField HeaderText="报工结果" DataField="bg_result" />
                    <asp:BoundField HeaderText="向先" DataField="DestinationCode" />
                    <asp:TemplateField HeaderText="查看详细">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnLink" runat="server"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
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
        <asp:Button ID="btnGoMs" runat="server" Text="Button" Style="display: none" />
    </form>
    <script>

        function GoToMs(cd, no) {
            $("#hidCd").val(cd);
            $("#hidNo").val(no);
            $("#btnGoMs").click();
        }

        $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
        $(".jqTxtDate").datepicker({
            dateFormat: "yy-mm-dd"
        });


    </script>
</body>
</html>
