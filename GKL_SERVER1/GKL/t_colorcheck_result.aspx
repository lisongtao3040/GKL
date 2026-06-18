<%@ Page Language="VB" AutoEventWireup="false" CodeFile="t_colorcheck_result.aspx.vb" Inherits="t_colorcheck_result" %>

<%@ Register Src="~/UserCtrl/Links.ascx" TagPrefix="uc1" TagName="Links" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" /> 
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <!--JS-->
    <script type="text/javascript" src="./jquery/jquery-3.6.0.min.js"></script>
<%--       <script language="javascript" type="text/javascript" src="./js/jquery-1.4.1.min.js"></script>--%>
    <script type="text/javascript" src="./jquery-ui-1.12.1/jquery-ui.min.js"></script>
    <script type="text/javascript" src="./jquery/jquery.cookie.js"></script>
    <link rel="stylesheet" href="./jquery-ui-1.12.1/jquery-ui.css" />
    <!--CSS-->
        <link href="tmp.css" rel="stylesheet" type="text/css" />

    <style>
        div.ui-datepicker {
            font-size: 30px;
        }

        td {
            border: 1px solid #efefef;
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
                <% Response.Write(Common.SetTitle("色检查一览"))%>
            </div>
        </div>
        <div class="links_div">
            <uc1:links runat="server" id="Links" />
        </div>

        <div>
            &nbsp;&nbsp;生产线：
            <asp:DropDownList ID="ddl_lines" runat="server" Font-Size="30" Width="140"></asp:DropDownList>
            &nbsp;&nbsp;日期：
            &nbsp;&nbsp;<asp:TextBox ID="tbxYmd" runat="server" Width="200" Height="50px" Font-Size="35px" CssClass="jqTxtDate"></asp:TextBox>
            &nbsp;&nbsp;<asp:Button ID="btnSel" runat="server" Height="40px" Text="检索" Width="120px" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnBack" runat="server" Text="返回" Width="120px" Height="40px" />
        </div>

                <hr />
        <div style="height: 1100px; width: 1060px; overflow: auto;">


            <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="checkid" HeaderText="作番" />
                    <asp:BoundField DataField="linecode" HeaderText="生产线" />
                    <asp:BoundField DataField="make_no" HeaderText="作番" />
                    <asp:BoundField DataField="code" HeaderText="CD" />
                    <asp:BoundField DataField="colorTxt" HeaderText="色" />
                    <asp:BoundField DataField="checkResult" HeaderText="检查结果" />
                    <asp:BoundField DataField="enable" HeaderText="enable" />
                    <asp:BoundField DataField="remark" HeaderText="备考" />
                    <asp:BoundField DataField="insertuser" HeaderText="登录者" />
                    <asp:BoundField DataField="insertdate" HeaderText="登录日" />
                    <asp:BoundField DataField="updateuser" HeaderText="更新者" />
                    <asp:BoundField DataField="updatedate" HeaderText="更新日" />
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
    </form>

    <script>

        $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
        $(".jqTxtDate").datepicker({
            dateFormat: "yy-mm-dd"
        });


    </script>
</body>
</html>
