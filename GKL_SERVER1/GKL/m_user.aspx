<%@ Page Language="VB" AutoEventWireup="false" CodeFile="m_user.aspx.vb" Inherits="m_user" %>

<%@ OutputCache Location="None" %>
<%@ Register Src="~/UserCtrl/Links.ascx" TagPrefix="uc1" TagName="Links" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>用户MS</title>

    <!--JS-->
    <script language="javascript" type="text/javascript" src="./js/jquery-1.4.1.min.js"></script>
    <link href="jquery-ui.css" rel="stylesheet" />
    <script src="external/jquery/jquery.js"></script>
    <script src="jquery-ui.js"></script>


    <script language="javascript" type="text/javascript" src="./JidouTemp.js"></script>
    <script language="javascript" type="text/javascript" src="./m_user.aspx.js"></script>




    <!--CSS-->
    <link href="tmp.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class='title_div'>
                <%Response.Write(Common.SetTitle("用户MS"))%>
                <asp:Button ID="btnBack" runat="server" Text="返回" CssClass="jq_back" Visible="false" />
            </div>
            <div class="links_div">
                <uc1:Links runat="server" ID="Links" />
            </div>
            <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
            <!--条件部-->
            <table class='jyouken_panel' cellpadding="0" cellspacing="0">
                <tr>
                    <td>用户CD : &nbsp;</td>
                    <td>
                        <asp:TextBox ID="tbxUserCd_key" placeholder="请输入员工编号" class="jq_user_cd_key" runat="server" Style="width: 160px; background-color: #FFAA00;"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnSelect" runat="server" Text="検索" CssClass="jq_sel" />
                    </td>
                </tr>
            </table>

            <!--Button部-->
            <div style="width: 520px; text-align: right; margin-bottom: 14px;">
                <asp:Button ID="btnUpdate" runat="server" Text="更新" CssClass="jq_upd" />
                <asp:Button ID="btnInsert" runat="server" Text="登録" CssClass="jq_ins" />
                <asp:Button ID="btnDelete" runat="server" Text="削除" CssClass="jq_del" />

                &nbsp;&nbsp;&nbsp;&nbsp;
            第
            <asp:DropDownList ID="ddlPageIdx" runat="server" AutoPostBack="true" Width="60px">
            </asp:DropDownList>
                /
            <asp:Label ID="lblAllPageText" runat="server" Text="0"></asp:Label>
                页
            </div>

            <!--明細Title部-->
            <div id="divGvwTitle" class='jq_title_div' runat="server" style="overflow: hidden; margin-left: 0px; width: 1004px; margin-top: 0px; border-collapse: collapse;">
                <table class='ms_title' style="width: 900px" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 170px;">用户CD</td>
                        <td style="width: 170px;">生产线
                        </td>
                        <td style="width: 170px;">用户名
                        </td>
                        <td style="width: 170px;">密码
                        </td>
                        <td style="">权限 0：普通 1：管理
                        </td>
                    </tr>
                </table>
                <table class='ms_input' style="width: 900px" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 170px;">
                            <asp:TextBox ID="tbxUserCd" placeholder="请输入员工编号" class="jq_user_cd_ipt" runat="server" MaxLength="10" Style="width: 164px; background-color: #FFAA00;"></asp:TextBox>
                        </td>
                        <td style="width: 170px;">
                            <asp:TextBox ID="tbxLineId" placeholder="生产线编号" class="jq_line_id_ipt" runat="server" MaxLength="10" Style="width: 164px;"></asp:TextBox>
                        </td>
                        <td style="width: 170px;">
                            <asp:TextBox ID="tbxUserName" placeholder="用户名汉字" class="jq_user_name_ipt" runat="server" MaxLength="10" Style="width: 164px;"></asp:TextBox>
                        </td>
                        <td style="width: 170px;">
                            <asp:TextBox ID="tbxUserPassword" placeholder="密码" class="jq_user_password_ipt" runat="server" MaxLength="10" Style="width: 164px;"></asp:TextBox>
                        </td>
                        <td style="">
                            <asp:TextBox ID="tbxKengen" placeholder="权限" class="jq_kengen_ipt" runat="server" MaxLength="1" Style="width: 96%;"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>

            <!--明細Body部-->
            <div id="divGvw" class='jq_ms_div' runat="server" style="overflow: scroll; height: 500px; margin-left: 0px; width: 920px; margin-top: 0px; border-collapse: collapse;">

                <asp:GridView CssClass="jq_ms" Width="900px" runat="server" ID="gvMs" EnableTheming="True" ShowHeader="False" AutoGenerateColumns="False" BorderColor="black" Style="margin-top: -1px;" TabIndex="-1">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate><%#Eval("user_cd")%></ItemTemplate>
                            <ItemStyle Width="170px" HorizontalAlign="Left" CssClass="jq_user_cd" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate><%#Eval("line_id")%></ItemTemplate>
                            <ItemStyle Width="170px" HorizontalAlign="Left" CssClass="jq_line_id" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate><%#Eval("user_name")%></ItemTemplate>
                            <ItemStyle Width="170px" HorizontalAlign="Left" CssClass="jq_user_name" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate><%#Eval("user_password")%></ItemTemplate>
                            <ItemStyle  Width="170px" HorizontalAlign="Left" CssClass="jq_user_password" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate><%#Eval("kengen")%></ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" CssClass="jq_kengen" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <asp:TextBox ID="hidUserCd" runat="server" class="jq_user_cd_ipt" Style="visibility: hidden;"></asp:TextBox>
            <asp:TextBox ID="hidLineId" runat="server" class="jq_line_id_ipt" Style="visibility: hidden;"></asp:TextBox>
            <asp:TextBox ID="hidUserName" runat="server" class="jq_user_name_ipt" Style="visibility: hidden;"></asp:TextBox>
            <asp:TextBox ID="hidUserPassword" runat="server" class="jq_user_password_ipt" Style="visibility: hidden;"></asp:TextBox>
            <asp:TextBox ID="hidKengen" runat="server" class="jq_kengen_ipt" Style="visibility: hidden;"></asp:TextBox>

            <asp:TextBox ID="hidOldRowIdx" runat="server" class="jq_hidOldRowIdx" Style="visibility: hidden;"></asp:TextBox>
        </div>
    </form>
</body>
</html>
