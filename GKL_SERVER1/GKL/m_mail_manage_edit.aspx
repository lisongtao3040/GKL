<%@ Page Language="VB" AutoEventWireup="false" CodeFile="m_mail_manage_edit.aspx.vb" Inherits="m_mail_manage_edit" %>
<%@ Register Src="~/UserCtrl/Links.ascx" TagPrefix="uc1" TagName="Links" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1"  />
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>邮件地址管理</title>

    <!--JS-->
    <script language="javascript" type="text/javascript" src="./js/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="./JidouTemp.js"></script>
    <script language="javascript" type="text/javascript" src="./m_tools.aspx.js"></script>

    <!--CSS-->
    <link href="tmp.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
    <div>
        <div class='title_div'> <%Response.Write(Common.SetTitle("邮件地址管理"))%>
        </div>
        <div  class="links_div">
            <uc1:Links runat="server" ID="Links" />
        </div>
        <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
        
<!--条件部-->
        <table class='jyouken_panel' cellpadding="0" cellspacing="0">
            <tr>
            <td>生产线 : &nbsp;</td>
            <td>
                <asp:TextBox ID="tbxLineId_key" class="jq_line_id_key" runat="server" style="width:160px;background-color: #FFAA00;" list="line_id_list"></asp:TextBox>
            </td>
       <td><asp:Button ID="btnSelect" runat="server" Text="検索" CssClass="jq_sel"  Height="30" Width="50"/></td>
            </tr>
          </table>
        <br /> 

<!--Button部-->
        <div style="width:820px; text-align:right;">

            <asp:Button ID="btnUpdate" runat="server" Text="更新" CssClass="jq_upd" />
            <asp:Button ID="btnInsert" runat="server" Text="登録" CssClass="jq_ins" />
            <asp:Button ID="btnDelete" runat="server" Text="削除" CssClass="jq_del" />
        </div>
 <br /> 

<!--明細Title部-->
<div id="divGvwTitle" class='jq_title_div' runat ="server" style="overflow:hidden ;margin-left:0px; width:1014px; margin-top :0px; border-collapse :collapse ;">
      <table class='ms_title' style="width:1000px" cellpadding="0" cellspacing="0">
          <tr>
              <td style="width:90px;">
                  系
              </td>
              <td style="width:90px;">
                  生产线
              </td>
              <td style="width:400px;">
                  To 邮箱地址
              </td>
              <td style="">
                  CC 邮箱地址
              </td>
          </tr>
      </table>
      <table class='ms_input' style="width:1210px" cellpadding="0" cellspacing="0">
          <tr>
              <td style="width:90px;">
              <asp:TextBox ID="tbxXi" class="jq_xi_ipt" runat="server" maxLength="10" style="width:84px;"></asp:TextBox>
          </td>
              <td style="width:90px;">
              <asp:TextBox ID="tbxLineId" class="jq_line_id_ipt" runat="server" list="line_id_list" maxLength="20" style="width:84px;background-color: #FFAA00;"></asp:TextBox>
          </td>
              <td style="width:400px;">
              <asp:TextBox ID="tbxToMail" class="jq_to_email_ipt" runat="server" maxLength="800" style="width:390px;"></asp:TextBox>
          </td>
              <td style="">
              <asp:TextBox ID="tbxCCMail" class="jq_cc_email_ipt" runat="server" maxLength="800" style="width:400px;"></asp:TextBox>
          </td>
          </tr>
      </table>
</div>

<!--明細Body部-->
<div id="divGvw" class='jq_ms_div' runat ="server" style="overflow:scroll ; height:450px;margin-left:0px; width:1020px; margin-top :0px; border-collapse :collapse ;">

   <asp:GridView CssClass ="jq_ms" Width="1000px"  runat="server" ID="gvMs" EnableTheming="True" ShowHeader="False" AutoGenerateColumns="False" BorderColor="black" style=" margin-top :-1px; " TabIndex="-1" >
      <Columns>
          <asp:TemplateField><ItemTemplate ><%#Eval("xi")%></ItemTemplate><ItemStyle Width="90px" HorizontalAlign="Left" CssClass="jq_xi" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("line_id")%></ItemTemplate><ItemStyle Width="90px" HorizontalAlign="Left" CssClass="jq_line_id" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("to_email")%></ItemTemplate><ItemStyle Width="400px" HorizontalAlign="Left" CssClass="jq_to_email" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("cc_email")%></ItemTemplate><ItemStyle  HorizontalAlign="Left" CssClass="jq_cc_email" /></asp:TemplateField>
      </Columns>
   </asp:GridView>
</div>

        <asp:TextBox ID="hidXi" runat="server" class="jq_xi_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidLineId" runat="server" class="jq_line_id_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidToMail" runat="server" class="jq_to_email_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidCCMail" runat="server" class="jq_cc_email_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidOldRowIdx" runat="server" class="jq_hidOldRowIdx" style=" visibility:hidden;"></asp:TextBox>
    </div>
    </form>
</body>
</html>
