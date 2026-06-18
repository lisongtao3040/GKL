<%@ Page Language="VB" AutoEventWireup="false" CodeFile="m_tools_popup.aspx.vb" Inherits="m_tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1"  />
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>治具MS</title>

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
     

<!--明細Title部-->
<div id="divGvwTitle" class='jq_title_div' runat ="server" style="overflow:hidden ;margin-left:0px; width:1004px; margin-top :0px; border-collapse :collapse ;">
      <table class='ms_title' style="width:815px" cellpadding="0" cellspacing="0">
          <tr>
              <td style="width:210px;">
                  治具ID
              </td>
              <td style="width:170px;">
                  生产线
              </td>
              <td style="width:210px;">
                  工程
              </td>
              <td style="">
                  治具显示文本
              </td>
          </tr>
      </table>
</div>

<!--明細Body部-->
<div id="divGvw" class='jq_ms_div' runat ="server" style="overflow:scroll ; height:1200px;margin-left:0px; width:835px; margin-top :0px; border-collapse :collapse ;">

   <asp:GridView CssClass ="jq_ms" Width="815px"  runat="server" ID="gvMs" EnableTheming="True" ShowHeader="False" AutoGenerateColumns="False" BorderColor="black" style=" margin-top :-1px; " TabIndex="-1" >
      <Columns>
          <asp:TemplateField><ItemTemplate ><%#Eval("tool_id")%></ItemTemplate><ItemStyle Width="210px" HorizontalAlign="Left" CssClass="jq_tool_id" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("line_id")%></ItemTemplate><ItemStyle Width="170px" HorizontalAlign="Left" CssClass="jq_line_id" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("project_name")%></ItemTemplate><ItemStyle Width="210px" HorizontalAlign="Left" CssClass="jq_project_name" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("tool_name")%></ItemTemplate><ItemStyle  HorizontalAlign="Left" CssClass="jq_tool_name" /></asp:TemplateField>
      </Columns>
   </asp:GridView>
</div>


<script language="javascript">
    $(".jq_ms tr").dblclick(function () {
        window.opener.document.getElementById($("#tool_id").val()).value = $(this).find("td")[0].innerText;
        window.opener.document.getElementById($("#tool_name_id").val()).value = $(this).find("td")[3].innerText
        window.close();
    })
</script>


        <asp:TextBox ID="hidToolId" runat="server" class="jq_tool_id_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidLineId" runat="server" class="jq_line_id_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidProjectName" runat="server" class="jq_project_name_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidToolName" runat="server" class="jq_tool_name_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidOldRowIdx" runat="server" class="jq_hidOldRowIdx" style=" visibility:hidden;"></asp:TextBox>

        <asp:TextBox ID="tool_id" runat="server" class="" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="tool_name_id" runat="server" class="" style=" visibility:hidden;"></asp:TextBox>
    </div>
    </form>
</body>
</html>
