<%@ Page Language="VB" AutoEventWireup="false" CodeFile="t_check_plan.aspx.vb" Inherits="t_check_plan" %>
<%@ Register Src="~/UserCtrl/Links.ascx" TagPrefix="uc1" TagName="Links" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1"  />
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>检查计划</title>

    <!--JS-->
    <script language="javascript" type="text/javascript" src="./js/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="./JidouTemp.js"></script>
    <script language="javascript" type="text/javascript" src="./t_check_plan.aspx.js"></script>

    <!--CSS-->
    <link href="tmp.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
    <div>
        <div class='title_div'><%Response.Write(Common.SetTitle("检查计划"))%>
        </div>
        <div  class="links_div">
            <uc1:Links runat="server" ID="Links" />
        </div>


        <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
        
<!--条件部-->
        <table class='jyouken_panel' cellpadding="0" cellspacing="0">

            <tr>
            <td>生产线 : &nbsp;&nbsp;</td>
            <td>
              <asp:TextBox ID="tbxLineId_key" class="jq_line_id_key" runat="server" style="width:160px;background-color: #FFAA00;" list="line_id_list"></asp:TextBox>
                </td>
            <td>预订检查日 : &nbsp;</td>
            <td>
              <asp:TextBox ID="tbxCheckDate_key" class="jq_CheckDate_key" runat="server" style="width:200px;background-color: #FFAA00;"></asp:TextBox>
            </td>
            <td>计划No : &nbsp;</td>
            <td>
              <asp:TextBox ID="tbxPlanNo_key" class="jq_plan_no_key" runat="server" style="width:200px;background-color: #FFAA00;"></asp:TextBox>
                </td>
            </tr>

            <tr>
            <td>检查No : </td>
            <td>
              <asp:TextBox ID="tbxChkNo_key" class="jq_chk_no_key" runat="server" style="width:200px;background-color: #FFAA00;"></asp:TextBox>
                </td>
            <td>作番 : &nbsp;</td>
            <td>
              <asp:TextBox ID="tbxMakeNo_key" class="jq_make_no_key" runat="server" style="width:200px;background-color: #FFAA00;"></asp:TextBox>
            </td>
            <td>コード : &nbsp;</td>
            <td>
              <asp:TextBox ID="tbxCode_key" class="jq_code_key" runat="server" style="width:200px;background-color: #FFAA00;"></asp:TextBox>
                </td>
            </tr>
            <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
            <asp:Button ID="btnSelect" runat="server" Text="検索" CssClass="jq_sel" Height="30"/>
                </td>
            </tr>
        </table>
        <br /> 

<!--Button部-->
        <div style="width:560px; text-align:right;">

    <!--Button部-->
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
 <br /> 

<!--明細Title部-->
<div id="divGvwTitle" class='jq_title_div' runat ="server" style="overflow:hidden ;margin-left:0px; width:1004px; margin-top :0px; border-collapse :collapse ;">
      <table class='ms_title' style="width:1000px" cellpadding="0" cellspacing="0">
          <tr>
              <td style="width:50px;">
                  计划No</td>
              <td style="width:110px;">
                  检查No
              </td>
              <td style="width:90px;">
                  作番
              </td>
              <td style="width:90px;">
                  コード
              </td>
              <td style="width:90px;">
                  生产线
              </td>
              <td style="width:90px;">
                  数量
              </td>
              <td style="width:68px;">
                  预订检查日
              </td>
              <td style="width:30px;">
                  状态
              </td>
              <td style="width:60px;">
                  向先
              </td>
              <td style="">
                  备注
              </td>
          </tr>
      </table>
      <table class='ms_input' style="width:1000px" cellpadding="0" cellspacing="0">
          <tr>
              <td style="width:50px;">
              <asp:TextBox ID="tbxPlanNo" class="jq_plan_no_ipt" runat="server" maxLength="20" style="width:44px;background-color: #FFAA00;"></asp:TextBox>
          </td>
              <td style="width:110px;">
              <asp:TextBox ID="tbxChkNo" class="jq_chk_no_ipt" runat="server" maxLength="20" style="width:104px;background-color: #FFAA00;"></asp:TextBox>
          </td>
              <td style="width:90px;">
              <asp:TextBox ID="tbxMakeNo" class="jq_make_no_ipt" runat="server" maxLength="20" style="width:84px;background-color: #FFAA00;"></asp:TextBox>
          </td>
              <td style="width:90px;">
              <asp:TextBox ID="tbxCode" class="jq_code_ipt" runat="server" maxLength="20" style="width:84px;background-color: #FFAA00;"></asp:TextBox>
          </td>
              <td style="width:90px;">
              <asp:TextBox ID="tbxLineId" class="jq_line_id_ipt" runat="server" maxLength="10" style="width:84px;background-color: #FFAA00;"></asp:TextBox>
          </td>
              <td style="width:90px;">
              <asp:TextBox ID="tbxSuu" class="jq_suu_ipt" runat="server" maxLength="10" style="width:84px;"></asp:TextBox>
          </td>
              <td style="width:68px;">
              <asp:TextBox ID="tbxYoteiChkDate" class="jq_yotei_chk_date_ipt" runat="server" maxLength="10" style="width:62px;"></asp:TextBox>
          </td>
              <td style="width:30px;">
              <asp:TextBox ID="tbxStatus" class="jq_status_ipt" runat="server" maxLength="1" style="width:24px;" ToolTip="默认0， 请不要随意修改"></asp:TextBox>
          </td>
              <td style="width:60px;">
              <asp:TextBox ID="tbxInsUser" class="jq_ins_user_ipt" runat="server" maxLength="20" style="width:54px;"></asp:TextBox>
          </td>
              <td style="">
              <asp:TextBox ID="tbxInsDate" class="jq_ins_date_ipt" runat="server" maxLength="200" style="width:248px;"></asp:TextBox>
          </td>
          </tr>
      </table>
</div>

<!--明細Body部-->
<div id="divGvw" class='jq_ms_div' runat ="server" style="overflow:scroll ; height:300px;margin-left:0px; width:1020px; margin-top :0px; border-collapse :collapse ;">

   <asp:GridView CssClass ="jq_ms" Width="1000px"  runat="server" ID="gvMs" EnableTheming="True" ShowHeader="False" AutoGenerateColumns="False" BorderColor="black" style=" margin-top :-1px; " TabIndex="-1" >
      <Columns>
          <asp:TemplateField><ItemTemplate ><%#Eval("plan_no")%></ItemTemplate><ItemStyle Width="50px" HorizontalAlign="Left" CssClass="jq_plan_no" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("chk_no")%></ItemTemplate><ItemStyle Width="110px" HorizontalAlign="Left" CssClass="jq_chk_no" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("make_no")%></ItemTemplate><ItemStyle Width="90px" HorizontalAlign="Left" CssClass="jq_make_no" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("code")%></ItemTemplate><ItemStyle Width="90px" HorizontalAlign="Left" CssClass="jq_code" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("line_id")%></ItemTemplate><ItemStyle Width="90px" HorizontalAlign="Left" CssClass="jq_line_id" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("suu")%></ItemTemplate><ItemStyle Width="90px" HorizontalAlign="Left" CssClass="jq_suu" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("yotei_chk_date")%></ItemTemplate><ItemStyle Width="68px" HorizontalAlign="Left" CssClass="jq_yotei_chk_date" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("status")%></ItemTemplate><ItemStyle Width="30px" HorizontalAlign="Left" CssClass="jq_status" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("xiangxian")%></ItemTemplate><ItemStyle Width="60px" HorizontalAlign="Left" CssClass="jq_ins_user" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("mark")%></ItemTemplate><ItemStyle  HorizontalAlign="Left" CssClass="jq_ins_date" /></asp:TemplateField>
      </Columns>
   </asp:GridView>
</div>
        <br />

        <div style="width:1000px; text-align:left;">
            <asp:FileUpload ID="GetUploadFileContent" runat="server" Width="500" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <a href="APP/YOJITSU.txt" target="_blank">文件例</a> 请注意文件的编码是Shift-jis&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnUpload" runat="server" Text="导入" />
            <br />
        </div>
        <hr />
        <div style="width:1000px; text-align:left;">
            年：
            <asp:DropDownList ID="ddlY" runat="server" AutoPostBack="true" Width="90px">
                <asp:ListItem Text="2020" Value="2020"></asp:ListItem>
                <asp:ListItem Text="2021" Value="2021"></asp:ListItem>
                <asp:ListItem Text="2022" Value="2022"></asp:ListItem>
                <asp:ListItem Text="2023" Value="2023"></asp:ListItem>
                <asp:ListItem Text="2024" Value="2024"></asp:ListItem>
                <asp:ListItem Text="2025" Value="2025"></asp:ListItem>
                <asp:ListItem Text="2026" Value="2026"></asp:ListItem>
                <asp:ListItem Text="2027" Value="2027"></asp:ListItem>
                <asp:ListItem Text="2028" Value="2028"></asp:ListItem>
                <asp:ListItem Text="2029" Value="2029"></asp:ListItem>
                <asp:ListItem Text="2030" Value="2030"></asp:ListItem>
            </asp:DropDownList>
            月：
            <asp:DropDownList ID="ddlM" runat="server" AutoPostBack="true" Width="90px">
                <asp:ListItem Text="01" Value="01"></asp:ListItem>
                <asp:ListItem Text="02" Value="02"></asp:ListItem>
                <asp:ListItem Text="03" Value="03"></asp:ListItem>
                <asp:ListItem Text="04" Value="04"></asp:ListItem>
                <asp:ListItem Text="05" Value="05"></asp:ListItem>
                <asp:ListItem Text="06" Value="06"></asp:ListItem>
                <asp:ListItem Text="07" Value="07"></asp:ListItem>
                <asp:ListItem Text="08" Value="08"></asp:ListItem>
                <asp:ListItem Text="09" Value="09"></asp:ListItem>
                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                <asp:ListItem Text="11" Value="11"></asp:ListItem>
                <asp:ListItem Text="12" Value="12"></asp:ListItem>
            </asp:DropDownList>

            <asp:Button ID="btnImportFromSap" runat="server" Text="SAP取入导入" Width="200" />
        </div>
        <asp:TextBox ID="hidPlanNo" runat="server" class="jq_plan_no_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidChkNo" runat="server" class="jq_chk_no_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidMakeNo" runat="server" class="jq_make_no_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidCode" runat="server" class="jq_code_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidLineId" runat="server" class="jq_line_id_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidSuu" runat="server" class="jq_suu_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidYoteiChkDate" runat="server" class="jq_yotei_chk_date_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidStatus" runat="server" class="jq_status_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidInsUser" runat="server" class="jq_ins_user_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidInsDate" runat="server" class="jq_ins_date_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidOldRowIdx" runat="server" class="jq_hidOldRowIdx" style=" visibility:hidden;"></asp:TextBox>
    </div>
    </form>

        <script language="javascript">
            $(".jq_upd,.jq_ins").click(function (e) {
                if (!GetDateFormat($("#tbxYoteiChkDate")[0])) {
                    alert("日期形式错误！");

                    e.stopPropagation()
                    e.preventDefault();
                    this.focus();
                    return false;
                }
                if (!chkHankakuSuuji($("#tbxSuu")[0].value)) {
                    alert("请输入数字！");

                    e.stopPropagation()
                    e.preventDefault();
                    this.focus();
                    return false;
                }
                

                
            });
            $("#btnSelect").click(function () {

                if ($("#tbxLineId_key").val() == "") {
                    alert("请输入生产线");
                    $("#tbxLineId_key").focus();
                    return false;
                }


            });


    </script>
</body>
</html>
