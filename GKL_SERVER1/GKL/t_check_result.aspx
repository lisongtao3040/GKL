<%@ Page Language="VB" AutoEventWireup="false" CodeFile="t_check_result.aspx.vb" Inherits="t_check_result" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1"  />
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>检查结果</title>

    <!--JS-->
    <script language="javascript" type="text/javascript" src="./js/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="./JidouTemp.js"></script>
    <script language="javascript" type="text/javascript" src="./t_check_result.aspx.js"></script>

    <!--CSS-->
    <link href="tmp.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
    <div>
        <div class='title_div'>检查结果</div>
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
            <tr>
            <td>年 : &nbsp;</td>
            <td>
              <asp:TextBox ID="tbxNen_key" class="jq_nen_key" runat="server" style="width:64px;background-color: #FFAA00;"></asp:TextBox>
            </td>
            <td></td>
            </tr>
            <tr>
            <td>生产线 : &nbsp;</td>
            <td>
              <asp:TextBox ID="tbxLineId_key" class="jq_line_id_key" runat="server" style="width:160px;background-color: #FFAA00;"></asp:TextBox>
            </td>
            <td></td>
            </tr>
            <tr>
            <td>作番 : &nbsp;</td>
            <td>
              <asp:TextBox ID="tbxMakeNo_key" class="jq_make_no_key" runat="server" style="width:200px;background-color: #FFAA00;"></asp:TextBox>
            </td>
            <td></td>
            </tr>
        </table>
        <br /> 

<!--Button部-->
        <asp:Button ID="btnSelect" runat="server" Text="検索" CssClass="jq_sel" />
        <asp:Button ID="btnUpdate" runat="server" Text="更新" CssClass="jq_upd" />
        <asp:Button ID="btnInsert" runat="server" Text="登録" CssClass="jq_ins" />
        <asp:Button ID="btnDelete" runat="server" Text="削除" CssClass="jq_del" />
 <br /> 

<!--明細Title部-->
<div id="divGvwTitle" class='jq_title_div' runat ="server" style="overflow:hidden ;margin-left:0px; width:1004px; margin-top :0px; border-collapse :collapse ;">
      <table class='ms_title' style="width:2363px" cellpadding="0" cellspacing="0">
          <tr>
              <td style="width:210px;">
                  检查No 20
              </td>
              <td style="width:74px;">
                  年 4
              </td>
              <td style="width:210px;">
                  计划No 20
              </td>
              <td style="width:170px;">
                  生产线 10
              </td>
              <td style="width:210px;">
                  作番 20
              </td>
              <td style="width:210px;">
                  コード 20
              </td>
              <td style="width:170px;">
                  数量 10
              </td>
              <td style="width:170px;">
                  检查模板编号 10
              </td>
              <td style="width:30px;">
                  检查结果 1
              </td>
              <td style="width:210px;">
                  检查者 20
              </td>
              <td style="width:58px;">
                  检查開始日 3
              </td>
              <td style="width:58px;">
                  检查完了日 3
              </td>
              <td style="width:210px;">
                  父检查No 20
              </td>
              <td style="width:30px;">
                  状态 1
              </td>
              <td style="width:210px;">
                  登録者 20
              </td>
              <td style="">
                  登録日 3
              </td>
          </tr>
      </table>
</div>

<!--明細Body部-->
<div id="divGvw" class='jq_ms_div' runat ="server" style="overflow:scroll ; height:1200px;margin-left:0px; width:1020px; margin-top :0px; border-collapse :collapse ;">
      <table class='ms_input' style="width:2363px" cellpadding="0" cellspacing="0">
          <tr>
              <td style="width:210px;">
              <asp:TextBox ID="tbxChkNo" class="jq_chk_no_ipt" runat="server" maxLength="20" style="width:204px;background-color: #FFAA00;"></asp:TextBox>
          </td>
              <td style="width:74px;">
              <asp:TextBox ID="tbxNen" class="jq_nen_ipt" runat="server" maxLength="4" style="width:68px;background-color: #FFAA00;"></asp:TextBox>
          </td>
              <td style="width:210px;">
              <asp:TextBox ID="tbxPlanNo" class="jq_plan_no_ipt" runat="server" maxLength="20" style="width:204px;"></asp:TextBox>
          </td>
              <td style="width:170px;">
              <asp:TextBox ID="tbxLineId" class="jq_line_id_ipt" runat="server" maxLength="10" style="width:164px;background-color: #FFAA00;"></asp:TextBox>
          </td>
              <td style="width:210px;">
              <asp:TextBox ID="tbxMakeNo" class="jq_make_no_ipt" runat="server" maxLength="20" style="width:204px;background-color: #FFAA00;"></asp:TextBox>
          </td>
              <td style="width:210px;">
              <asp:TextBox ID="tbxCode" class="jq_code_ipt" runat="server" maxLength="20" style="width:204px;"></asp:TextBox>
          </td>
              <td style="width:170px;">
              <asp:TextBox ID="tbxSuu" class="jq_suu_ipt" runat="server" maxLength="10" style="width:164px;"></asp:TextBox>
          </td>
              <td style="width:170px;">
              <asp:TextBox ID="tbxTempId" class="jq_temp_id_ipt" runat="server" maxLength="10" style="width:164px;"></asp:TextBox>
          </td>
              <td style="width:30px;">
              <asp:TextBox ID="tbxChkResult" class="jq_chk_result_ipt" runat="server" maxLength="1" style="width:24px;"></asp:TextBox>
          </td>
              <td style="width:210px;">
              <asp:TextBox ID="tbxChkUser" class="jq_chk_user_ipt" runat="server" maxLength="20" style="width:204px;"></asp:TextBox>
          </td>
              <td style="width:58px;">
              <asp:TextBox ID="tbxChkStartDate" class="jq_chk_start_date_ipt" runat="server" maxLength="3" style="width:52px;"></asp:TextBox>
          </td>
              <td style="width:58px;">
              <asp:TextBox ID="tbxChkEndDate" class="jq_chk_end_date_ipt" runat="server" maxLength="3" style="width:52px;"></asp:TextBox>
          </td>
              <td style="width:210px;">
              <asp:TextBox ID="tbxParentChkNo" class="jq_parent_chk_no_ipt" runat="server" maxLength="20" style="width:204px;"></asp:TextBox>
          </td>
              <td style="width:30px;">
              <asp:TextBox ID="tbxStatus" class="jq_status_ipt" runat="server" maxLength="1" style="width:24px;"></asp:TextBox>
          </td>
              <td style="width:210px;">
              <asp:TextBox ID="tbxInsUser" class="jq_ins_user_ipt" runat="server" maxLength="20" style="width:204px;"></asp:TextBox>
          </td>
              <td style="">
              <asp:TextBox ID="tbxInsDate" class="jq_ins_date_ipt" runat="server" maxLength="3" style="width:48px;"></asp:TextBox>
          </td>
          </tr>
      </table>
   <asp:GridView CssClass ="jq_ms" Width="2363px"  runat="server" ID="gvMs" EnableTheming="True" ShowHeader="False" AutoGenerateColumns="False" BorderColor="black" style=" margin-top :-1px; " TabIndex="-1" >
      <Columns>
          <asp:TemplateField><ItemTemplate ><%#Eval("chk_no")%></ItemTemplate><ItemStyle Width="210px" HorizontalAlign="Left" CssClass="jq_chk_no" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("nen")%></ItemTemplate><ItemStyle Width="74px" HorizontalAlign="Left" CssClass="jq_nen" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("plan_no")%></ItemTemplate><ItemStyle Width="210px" HorizontalAlign="Left" CssClass="jq_plan_no" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("line_id")%></ItemTemplate><ItemStyle Width="170px" HorizontalAlign="Left" CssClass="jq_line_id" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("make_no")%></ItemTemplate><ItemStyle Width="210px" HorizontalAlign="Left" CssClass="jq_make_no" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("code")%></ItemTemplate><ItemStyle Width="210px" HorizontalAlign="Left" CssClass="jq_code" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("suu")%></ItemTemplate><ItemStyle Width="170px" HorizontalAlign="Left" CssClass="jq_suu" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("temp_id")%></ItemTemplate><ItemStyle Width="170px" HorizontalAlign="Left" CssClass="jq_temp_id" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("chk_result")%></ItemTemplate><ItemStyle Width="30px" HorizontalAlign="Left" CssClass="jq_chk_result" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("chk_user")%></ItemTemplate><ItemStyle Width="210px" HorizontalAlign="Left" CssClass="jq_chk_user" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("chk_start_date")%></ItemTemplate><ItemStyle Width="58px" HorizontalAlign="Left" CssClass="jq_chk_start_date" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("chk_end_date")%></ItemTemplate><ItemStyle Width="58px" HorizontalAlign="Left" CssClass="jq_chk_end_date" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("parent_chk_no")%></ItemTemplate><ItemStyle Width="210px" HorizontalAlign="Left" CssClass="jq_parent_chk_no" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("status")%></ItemTemplate><ItemStyle Width="30px" HorizontalAlign="Left" CssClass="jq_status" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("ins_user")%></ItemTemplate><ItemStyle Width="210px" HorizontalAlign="Left" CssClass="jq_ins_user" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("ins_date")%></ItemTemplate><ItemStyle  HorizontalAlign="Left" CssClass="jq_ins_date" /></asp:TemplateField>
      </Columns>
   </asp:GridView>
</div>

<asp:TextBox ID="hidChkNo" runat="server" class="jq_chk_no_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hidNen" runat="server" class="jq_nen_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hidPlanNo" runat="server" class="jq_plan_no_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hidLineId" runat="server" class="jq_line_id_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hidMakeNo" runat="server" class="jq_make_no_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hidCode" runat="server" class="jq_code_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hidSuu" runat="server" class="jq_suu_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hidTempId" runat="server" class="jq_temp_id_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hidChkResult" runat="server" class="jq_chk_result_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hidChkUser" runat="server" class="jq_chk_user_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hidChkStartDate" runat="server" class="jq_chk_start_date_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hidChkEndDate" runat="server" class="jq_chk_end_date_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hidParentChkNo" runat="server" class="jq_parent_chk_no_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hidStatus" runat="server" class="jq_status_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hidInsUser" runat="server" class="jq_ins_user_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hidInsDate" runat="server" class="jq_ins_date_ipt" style=" visibility:hidden;"></asp:TextBox>
<asp:TextBox ID="hidOldRowIdx" runat="server" class="jq_hidOldRowIdx" style=" visibility:hidden;"></asp:TextBox>
    </div>
    </form>
</body>
</html>
