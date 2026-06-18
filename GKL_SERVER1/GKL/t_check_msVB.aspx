<%@ Page Language="VB" AutoEventWireup="false" CodeFile="t_check_msVB.aspx.vb" Inherits="t_check_ms" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1"  />
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>检查明細</title>

    <!--JS-->
    <script language="javascript" type="text/javascript" src="./js/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="./JidouTemp.js"></script>
    <script language="javascript" type="text/javascript" src="./t_check_ms.aspx.js"></script>

    <!--CSS-->
    <link href="tmp.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
    <div>
        <div class='title_div'>检查明細</div>
        <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
        
<!--条件部-->
        <table class='jyouken_panel' cellpadding="0" cellspacing="0">
            <tr>
            <td>检查No : &nbsp;</td>
            <td>
              <asp:TextBox ID="tbxChkNo_key" class="jq_chk_no_key" runat="server" style="width:200px;background-color: #FFAA00;"></asp:TextBox>
            </td>
            <td></td>
            </tr>
        </table>
        <br /> 

<!--Button部-->
        <asp:Button ID="btnSelect" runat="server" Text="検索" CssClass="jq_sel" />
 <br /> 

<!--明細Title部-->
<div id="divGvwTitle" class='jq_title_div' runat ="server" style="overflow:hidden ;margin-left:0px; width:1004px; margin-top :0px; border-collapse :collapse ;">
      <table class='ms_title' style="width:2418px" cellpadding="0" cellspacing="0">
          <tr>
              <td style="width:210px;">
                  工程名
              </td>
              <td style="width:70px;">
                  检查项目
              </td>
              <td style="width:30px;">
                  图片标记
              </td>
              <td style="width:210px;">
                  检查方法
              </td>
              <td style="width:210px;">
                  基准説明
              </td>
              <td style="width:210px;">
                  输入值
              </td>
              <td style="width:210px;">
                  结果
              </td>
              <td style="width:210px;">
                  备考
              </td>
          </tr>
      </table>
</div>

<!--明細Body部-->
<div id="divGvw" class='jq_ms_div' runat ="server" style="overflow:scroll ; height:1200px;margin-left:0px; width:1020px; margin-top :0px; border-collapse :collapse ;">
     
   <asp:GridView CssClass ="jq_ms" Width="2418px"  runat="server" ID="gvMs" EnableTheming="True" ShowHeader="False" AutoGenerateColumns="False" BorderColor="black" style=" margin-top :-1px; " TabIndex="-1" >
      <Columns>
          <asp:TemplateField><ItemTemplate ><%#Eval("project_name")%></ItemTemplate><ItemStyle Width="210px" HorizontalAlign="Left" CssClass="jq_chk_no" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("chk_km_name")%></ItemTemplate><ItemStyle Width="70px" HorizontalAlign="Left" CssClass="jq_chk_method_id" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("chk_flg")%></ItemTemplate><ItemStyle Width="30px" HorizontalAlign="Left" CssClass="jq_chk_flg" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("pic_sign")%></ItemTemplate><ItemStyle Width="210px" HorizontalAlign="Left" CssClass="jq_in_1" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("chk_name")%></ItemTemplate><ItemStyle Width="210px" HorizontalAlign="Left" CssClass="jq_in_2" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("in_1")%></ItemTemplate><ItemStyle Width="210px" HorizontalAlign="Left" CssClass="jq_chk_result" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("chk_result")%></ItemTemplate><ItemStyle Width="210px" HorizontalAlign="Left" CssClass="jq_mark" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("mark")%></ItemTemplate><ItemStyle Width="210px" HorizontalAlign="Left" CssClass="jq_kj_0" /></asp:TemplateField>
      </Columns>
   </asp:GridView>
</div>

        <asp:TextBox ID="hidChkNo" runat="server" class="jq_chk_no_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidChkMethodId" runat="server" class="jq_chk_method_id_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidChkFlg" runat="server" class="jq_chk_flg_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidIn1" runat="server" class="jq_in_1_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidIn2" runat="server" class="jq_in_2_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidChkResult" runat="server" class="jq_chk_result_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidMark" runat="server" class="jq_mark_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidKj0" runat="server" class="jq_kj_0_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidKj1" runat="server" class="jq_kj_1_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidKj2" runat="server" class="jq_kj_2_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidKjExplain" runat="server" class="jq_kj_explain_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidInsUser" runat="server" class="jq_ins_user_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidInsDate" runat="server" class="jq_ins_date_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidOldRowIdx" runat="server" class="jq_hidOldRowIdx" style=" visibility:hidden;"></asp:TextBox>
    </div>
    </form>
</body>
</html>
