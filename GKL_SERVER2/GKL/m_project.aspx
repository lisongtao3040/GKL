<%@ Page Language="VB" AutoEventWireup="false" CodeFile="m_project.aspx.vb" Inherits="m_project" %>
<%@ Register Src="~/UserCtrl/Links.ascx" TagPrefix="uc1" TagName="Links" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1"  />
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>工程MS</title>

    <!--JS-->
    <script language="javascript" type="text/javascript" src="./js/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="./JidouTemp.js"></script>
    <script language="javascript" type="text/javascript" src="./m_project.aspx.js"></script>

    <!--CSS-->
    <link href="tmp.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
    <div>
        <div class='title_div'> <%Response.Write(Common.SetTitle("工程MS"))%>
   
        </div>
        <div  class="links_div">
            <uc1:Links runat="server" ID="Links" />
        </div>
        <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
        
<!--条件部-->
        <table class='jyouken_panel' cellpadding="0" cellspacing="0">
            <tr style="display:none;">
            <td>工程ID : &nbsp;</td>
            <td>
              <asp:TextBox ID="tbxProjectId_key" class="jq_project_id_key" runat="server" style="width:160px;background-color: #FFAA00;"></asp:TextBox>
           
                 </td>
            <td>

            </td>
            </tr>
            <tr>
            <td>生产线 : &nbsp;</td>
            <td>
             
              <asp:TextBox ID="tbxLineId_key" class="jq_line_id_key" runat="server" style="width:160px;background-color: #FFAA00;" list="line_id_list"></asp:TextBox>

            </td>
            <td>
        <asp:Button ID="btnSelect" runat="server" Text="検索" CssClass="jq_sel" Height="30" Width="50" />
                </td>
            </tr>
        </table>
        <br /> 

<!--Button部-->
        <div style="width:520px; text-align:right;">

        <asp:Button ID="btnUpdate" runat="server" Text="更新" CssClass="jq_upd" />
        <asp:Button ID="btnInsert" runat="server" Text="登録" CssClass="jq_ins" />
        <asp:Button ID="btnDelete" runat="server" Text="削除" CssClass="jq_del" />
        </div>
 <br /> 

<!--明細Title部-->
<div id="divGvwTitle" class='jq_title_div' runat ="server" style="overflow:hidden ;margin-left:0px; width:1004px; margin-top :0px; border-collapse :collapse ;">
      <table class='ms_title' style="width:560px" cellpadding="0" cellspacing="0">
          <tr>
              <td style="width:170px;">
                  工程ID
              </td>
              <td style="width:170px;">
                  生产线
              </td>
              <td style="">
                  工程名
              </td>
          </tr>
      </table>
      <table class='ms_input' style="width:560px" cellpadding="0" cellspacing="0">
          <tr>
              <td style="width:170px;">
              <asp:TextBox ID="tbxProjectId" class="jq_project_id_ipt" runat="server" maxLength="10" style="width:164px;background-color: #FFAA00;"></asp:TextBox>
          </td>
              <td style="width:170px;">
              <asp:TextBox ID="tbxLineId" class="jq_line_id_ipt" runat="server" maxLength="10" style="width:164px;background-color: #FFAA00;"></asp:TextBox>
          </td>
              <td style="">
              <asp:TextBox ID="tbxProjectName" class="jq_project_name_ipt" runat="server" maxLength="40" style="width:200px;"></asp:TextBox>
          </td>
          </tr>
      </table>
</div>

<!--明細Body部-->
<div id="divGvw" class='jq_ms_div' runat ="server" style="overflow:scroll ; height:500px;margin-left:0px; width:580px; margin-top :0px; border-collapse :collapse ;">

   <asp:GridView CssClass ="jq_ms" Width="560px"  runat="server" ID="gvMs" EnableTheming="True" ShowHeader="False" AutoGenerateColumns="False" BorderColor="black" style=" margin-top :-1px; " TabIndex="-1" >
      <Columns>
          <asp:TemplateField><ItemTemplate ><%#Eval("project_id")%></ItemTemplate><ItemStyle Width="170px" HorizontalAlign="Left" CssClass="jq_project_id" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("line_id")%></ItemTemplate><ItemStyle Width="170px" HorizontalAlign="Left" CssClass="jq_line_id" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("project_name")%></ItemTemplate><ItemStyle  HorizontalAlign="Left" CssClass="jq_project_name" /></asp:TemplateField>
      </Columns>
   </asp:GridView>
</div>

        <asp:TextBox ID="hidProjectId" runat="server" class="jq_project_id_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidLineId" runat="server" class="jq_line_id_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidProjectName" runat="server" class="jq_project_name_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidOldRowIdx" runat="server" class="jq_hidOldRowIdx" style=" visibility:hidden;"></asp:TextBox>
    </div>
    </form>
</body>
</html>
