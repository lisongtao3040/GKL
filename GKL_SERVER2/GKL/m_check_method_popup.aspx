<%@ Page Language="VB" AutoEventWireup="false" CodeFile="m_check_method_popup.aspx.vb" Inherits="m_check_method" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1"  />
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>检查方法MS</title>

    <!--JS-->
    <script language="javascript" type="text/javascript" src="./js/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="./JidouTemp.js"></script>
    <script language="javascript" type="text/javascript" src="./m_check_method.aspx.js"></script>

    <!--CSS-->
    <link href="tmp.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
    <div>


 
<div style="margin-left:5px;">

</div>
<!--明細Title部-->
<div id="divGvwTitle" class='jq_title_div' runat ="server" style="overflow:hidden ;margin-left:0px; width:1004px; margin-top :0px; border-collapse :collapse ;">
             <table class='ms_title' style="width:850px" cellpadding="0" cellspacing="0">
          <tr>
              <td style="width:170px;">
                  检查方法ID 
              </td>
              <td style="width:210px;">
                  检查方法名 
              </td>
              <td style="width:30px;" title="0:input
1:scan
2:固定（合/否）*没有公式">
                  检查方法 
               </td>
              <td style="width:210px;">
                  检查方公式 
              </td>
              <td style="">
                  核对方法说明 
              </td>
          </tr>
      </table>
</div>

<!--明細Body部-->
<div id="divGvw" class='jq_ms_div' runat ="server" style="overflow:scroll ; height:494px;margin-left:0px; width:870px; margin-top :0px; border-collapse :collapse ;">

   <asp:GridView CssClass ="jq_ms" Width="850px"  runat="server" ID="gvMs" EnableTheming="True" ShowHeader="False" AutoGenerateColumns="False" BorderColor="black" style=" margin-top :-1px; " TabIndex="-1" >
      <Columns>
          <asp:TemplateField><ItemTemplate ><%#Eval("chk_id")%></ItemTemplate><ItemStyle Width="170px" HorizontalAlign="Left" CssClass="jq_chk_id" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("chk_name")%></ItemTemplate><ItemStyle Width="210px" HorizontalAlign="Left" CssClass="jq_chk_name" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("chk_method")%></ItemTemplate><ItemStyle Width="30px" HorizontalAlign="Left" CssClass="jq_chk_method" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("chk_formula")%></ItemTemplate><ItemStyle Width="210px" HorizontalAlign="Left" CssClass="jq_chk_formula" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("verify_method_explain")%></ItemTemplate><ItemStyle  HorizontalAlign="Left" CssClass="jq_verify_method_explain" /></asp:TemplateField>
      </Columns>
   </asp:GridView>
</div>

        <asp:TextBox ID="hidChkId" runat="server" class="jq_chk_id_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidChkName" runat="server" class="jq_chk_name_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidChkMethod" runat="server" class="jq_chk_method_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidChkFormula" runat="server" class="jq_chk_formula_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidVerifyMethodExplain" runat="server" class="jq_verify_method_explain_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidOldRowIdx" runat="server" class="jq_hidOldRowIdx" style=" visibility:hidden;"></asp:TextBox>

         <asp:TextBox ID="method_id" runat="server" class="" style=" visibility:hidden;"></asp:TextBox>
         <asp:TextBox ID="method_name_id" runat="server" class="" style=" visibility:hidden;"></asp:TextBox>
    </div>
    </form>

    <script language="javascript">
        $(".jq_ms tr").dblclick(function () {
            window.opener.document.getElementById($("#method_id").val()).value = $(this).find("td")[0].innerText;
            window.opener.document.getElementById($("#method_name_id").val()).value = $(this).find("td")[1].innerText
            window.close();
        })
    </script>
</body>
</html>
