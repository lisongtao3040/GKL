<%@ Page Language="VB" AutoEventWireup="false" CodeFile="m_check_method.aspx.vb" Inherits="m_check_method" %>
<%@ Register Src="~/UserCtrl/Links.ascx" TagPrefix="uc1" TagName="Links" %>

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

        <div class='title_div'> <%Response.Write(Common.SetTitle("检查方法MS"))%>
        </div>
        <div  class="links_div">
            <uc1:Links runat="server" ID="Links" />
        </div>
        <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
        
<!--条件部-->
        <table class='jyouken_panel' cellpadding="0" cellspacing="0">
            <tr>
            <td>检查方法ID : &nbsp;</td>
            <td>
              <asp:TextBox ID="tbxChkId_key" class="jq_chk_id_key" runat="server" style="width:160px;background-color: #FFAA00;"></asp:TextBox>
            </td>
            <td></td>
            </tr>
            <tr>
            <td>检查方法名 : &nbsp;</td>
            <td>
              <asp:TextBox ID="tbxChkName_key" class="jq_chk_name_key" runat="server" style="width:200px;background-color: #FFAA00;"></asp:TextBox>
            </td>
            <td>
        <asp:Button ID="btnSelect" runat="server" Text="検索" CssClass="jq_sel"  Height="30" Width="50" />
                </td>
            </tr>
        </table>
        <br /> 

<!--Button部-->
        <div style="width:820px; text-align:right;">

            <asp:Button ID="btnUpdate" runat="server" Text="更新" CssClass="jq_upd" />
            <asp:Button ID="btnInsert" runat="server" Text="登録" CssClass="jq_ins" />
            <asp:Button ID="btnDelete" runat="server" Text="削除" CssClass="jq_del" />
            <br />
            如果没有特殊情况，请不要变更已经存在的检测方法。（影响已经存在的检测记录）
            追加可以。
             
        </div>

 
<div style="margin-left:390px;">
检查方法<br />( 0:input
1:scan
2:固定（合/否）*没有公式)
</div>
<!--明細Title部-->
<div id="divGvwTitle" class='jq_title_div' runat ="server" style="overflow:hidden ;margin-left:0px; width:1004px; margin-top :0px; border-collapse :collapse ;">
      <table class='ms_title' style="width:950px" cellpadding="0" cellspacing="0">
          <tr>
              <td style="width:100px;">
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
              <td style="width:300px;">
                  检查方公式 
              </td>
              <td style="">
                  核对方法说明 
              </td>
          </tr>
      </table>
      <table class='ms_input' style="width:950px" cellpadding="0" cellspacing="0">
          <tr>
              <td style="width:100px;">
              <asp:TextBox ID="tbxChkId" class="jq_chk_id_ipt" runat="server" maxLength="10" style="width:94px;background-color: #FFAA00;"></asp:TextBox>
          </td>
              <td style="width:210px;">
              <asp:TextBox ID="tbxChkName" class="jq_chk_name_ipt" runat="server" maxLength="20" style="width:204px;background-color: #FFAA00;"></asp:TextBox>
          </td>
              <td style="width:30px;">
              <asp:TextBox ID="tbxChkMethod" class="jq_chk_method_ipt" runat="server" maxLength="1" style="width:24px;"></asp:TextBox>
          </td>
              <td style="width:210px;">
              <asp:TextBox ID="tbxChkFormula" class="jq_chk_formula_ipt" runat="server" maxLength="200" style="width:294px;"></asp:TextBox>
          </td>
              <td style="">
              <asp:TextBox ID="tbxVerifyMethodExplain" class="jq_verify_method_explain_ipt" runat="server" maxLength="200" style="width:250px;"></asp:TextBox>
          </td>
          </tr>
      </table>

</div>

<!--明細Body部-->
<div id="divGvw" class='jq_ms_div' runat ="server" style="overflow:scroll ; height:450px;margin-left:0px; width:970px; margin-top :0px; border-collapse :collapse ;">

   <asp:GridView CssClass ="jq_ms" Width="950px"  runat="server" ID="gvMs" EnableTheming="True" ShowHeader="False" AutoGenerateColumns="False" BorderColor="black" style=" margin-top :-1px; " TabIndex="-1" >
      <Columns>
          <asp:TemplateField><ItemTemplate ><%#Eval("chk_id")%></ItemTemplate><ItemStyle Width="100px" HorizontalAlign="Left" CssClass="jq_chk_id" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("chk_name")%></ItemTemplate><ItemStyle Width="210px" HorizontalAlign="Left" CssClass="jq_chk_name" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("chk_method")%></ItemTemplate><ItemStyle Width="30px" HorizontalAlign="Left" CssClass="jq_chk_method" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("chk_formula")%></ItemTemplate><ItemStyle Width="300px" HorizontalAlign="Left" CssClass="jq_chk_formula" /></asp:TemplateField>
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
    </div>
    </form>
</body>
</html>
