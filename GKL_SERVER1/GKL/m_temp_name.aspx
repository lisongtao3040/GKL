<%@ Page Language="VB" AutoEventWireup="false" CodeFile="m_temp_name.aspx.vb" Inherits="m_temp_name" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1"  />
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>模板名称MS</title>

    <!--JS-->
    <script language="javascript" type="text/javascript" src="./js/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="./JidouTemp.js"></script>
    <script language="javascript" type="text/javascript" src="./m_temp_name.aspx.js"></script>

    <!--CSS-->
    <link href="tmp.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
    <div>
        <div class='title_div'>模板名称MS</div>
        <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
        
<!--条件部-->
        <table class='jyouken_panel' cellpadding="0" cellspacing="0">
            <tr>
            <td>生产线 : &nbsp;</td>
            <td>
              <asp:TextBox ID="tbxLineId_key" class="jq_line_id_key" runat="server" style="width:160px;background-color: #FFAA00;" list="line_id_list"></asp:TextBox>
                <datalist id="line_id_list"></datalist>
              </td>
            <td></td>
            </tr>
            <tr>
            <td>检查模板编号 : &nbsp;</td>
            <td>
              <asp:TextBox ID="tbxTempId_key" class="jq_temp_id_key" runat="server" style="width:160px;background-color: #FFAA00;"></asp:TextBox>
            </td>
            <td></td>
            </tr>
        </table>
        <br /> 

<!--Button部-->
        <asp:Button ID="btnSelect" runat="server" Text="検索" CssClass="jq_sel" style="margin-left:10px;" />
        <asp:Button ID="btnUpdate" runat="server" Text="更新" CssClass="jq_upd" />
        <asp:Button ID="btnInsert" runat="server" Text="登録" CssClass="jq_ins" />
        <asp:Button ID="btnDelete" runat="server" Text="削除" CssClass="jq_del" />
 <br /> 

<!--明細Title部-->
<div id="divGvwTitle" class='jq_title_div' runat ="server" style="overflow:hidden ;margin-left:0px; width:1004px; margin-top :0px; border-collapse :collapse ;">
      <table class='ms_title' style="width:560px" cellpadding="0" cellspacing="0">
          <tr>
              <td style="width:170px;">
                  生产线 
              </td>
              <td style="width:170px;">
                  检查模板编号 
              </td>
              <td style="">
                  模板名称 
              </td>
          </tr>
      </table>
</div>

<!--明細Body部-->
<div id="divGvw" class='jq_ms_div' runat ="server" style="overflow:scroll ; height:1200px;margin-left:0px; width:580px; margin-top :0px; border-collapse :collapse ;">
      <table class='ms_input' style="width:560px" cellpadding="0" cellspacing="0">
          <tr>
              <td style="width:170px;">
              <asp:TextBox ID="tbxLineId" class="jq_line_id_ipt" runat="server" maxLength="10" style="width:164px;background-color: #FFAA00;"></asp:TextBox>
          </td>
              <td style="width:170px;">
              <asp:TextBox ID="tbxTempId" class="jq_temp_id_ipt" runat="server" maxLength="10" style="width:164px;background-color: #FFAA00;"></asp:TextBox>
          </td>
              <td style="">
              <asp:TextBox ID="tbxTempName" class="jq_temp_name_ipt" runat="server" maxLength="200" style="width:200px;"></asp:TextBox>
          </td>
          </tr>
      </table>
   <asp:GridView CssClass ="jq_ms" Width="560px"  runat="server" ID="gvMs" EnableTheming="True" ShowHeader="False" AutoGenerateColumns="False" BorderColor="black" style=" margin-top :-1px; " TabIndex="-1" >
      <Columns>
          <asp:TemplateField><ItemTemplate ><%#Eval("line_id")%></ItemTemplate><ItemStyle Width="170px" HorizontalAlign="Left" CssClass="jq_line_id" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("temp_id")%></ItemTemplate><ItemStyle Width="170px" HorizontalAlign="Left" CssClass="jq_temp_id" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("temp_name")%></ItemTemplate><ItemStyle  HorizontalAlign="Left" CssClass="jq_temp_name" /></asp:TemplateField>
      </Columns>
   </asp:GridView>
</div>
    
        <asp:TextBox ID="hidLineId" runat="server" class="jq_line_id_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidTempId" runat="server" class="jq_temp_id_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidTempName" runat="server" class="jq_temp_name_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidOldRowIdx" runat="server" class="jq_hidOldRowIdx" style=" visibility:hidden;"></asp:TextBox>
    </div>
    </form>
</body>
</html>
