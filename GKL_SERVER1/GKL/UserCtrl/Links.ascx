<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Links.ascx.vb" Inherits="UserCtrl_Links" %>
<link rel="stylesheet" type="text/css" href="css/normalize.css" />
<link rel="stylesheet" type="text/css" href="css/demo.css" />
<link rel="stylesheet" type="text/css" href="css/linkstyles.css" />
<link rel="stylesheet" type="text/css" href="css/default.css" />

<table cellpadding="0" cellspacing="0" style="margin:0px 0px 0px auto;" class="barLink">
<tr> 
<td><asp:LinkButton ID="lbMenu" runat="server" CssClass="link--kukuri">MENU</asp:LinkButton> </td>       
<td><asp:LinkButton ID="lbUser" runat="server" CssClass="link--kukuri">用户MS</asp:LinkButton> </td>
<td><asp:LinkButton ID="lbProject" runat="server" CssClass="link--kukuri">工程MS</asp:LinkButton> </td>
<td><asp:LinkButton ID="lbTools" runat="server" CssClass="link--kukuri">治具MS</asp:LinkButton> </td>
<td><a href="APP/PictureImport/PictureImport.application" Class="link--kukuri">图片MS</a> </td>
<td><asp:LinkButton ID="lbCheckMethod" runat="server" CssClass="link--kukuri">检查方法MS</asp:LinkButton> </td>
<td><asp:LinkButton ID="lbTemp" runat="server" CssClass="link--kukuri">模板MS</asp:LinkButton> </td>
<td><asp:LinkButton ID="lbRelation" runat="server" CssClass="link--kukuri">关联商品与模板</asp:LinkButton> </td>
<td><asp:LinkButton ID="lbPlan" runat="server" CssClass="link--kukuri">检查计划</asp:LinkButton> </td>
<td><asp:LinkButton ID="LinkButton1" runat="server" CssClass="link--kukuri">检查一览</asp:LinkButton> </td>
<%--<td><a href="APP/tyouhyou/Tyouhyou.application" Class="link--kukuri">帐票出力</a>
</td>--%>
</tr>
</table>
<datalist id="line_id_list"></datalist>