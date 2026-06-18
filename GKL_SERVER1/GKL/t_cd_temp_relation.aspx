<%@ Page Language="VB" AutoEventWireup="false" CodeFile="t_cd_temp_relation.aspx.vb" Inherits="t_cd_temp_relation" %>
<%@ Register Src="~/UserCtrl/Links.ascx" TagPrefix="uc1" TagName="Links" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1"  />
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>t_cd_temp_relation</title>

    <!--JS-->
    <script language="javascript" type="text/javascript" src="./js/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="./JidouTemp.js"></script>
    <script language="javascript" type="text/javascript" src="./t_cd_temp_relation.aspx.js"></script>

    <!--CSS-->
    <link href="tmp.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
    <div>
        <div class='title_div'><%Response.Write(Common.SetTitle("关联商品与模板"))%>
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
            <td></td>
            </tr>
            <tr>
            <td>模板CD : &nbsp;</td>
            <td>
                <asp:TextBox ID="tbxTempId_key" class="jq_temp_id_key" runat="server" style="width:160px;background-color: #FFAA00;" list="temp_ids"></asp:TextBox>
              <datalist id="temp_ids" runat="server"></datalist>
                         </td>
            <td></td>
            </tr>
            <tr>
            <td>商品CD : &nbsp;</td>
            <td>
                 <asp:TextBox ID="tbxCode_key" class="jq_code_key" runat="server" style="width:200px;background-color: #FFAA00;"></asp:TextBox>

                          </td>
            <td>
            <asp:Button ID="btnSelect" runat="server" Text="検索" CssClass="jq_sel" Height="24px" />
                </td>
            </tr>
        </table>
        <br /> 
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


<!--明細Title部-->
<div id="divGvwTitle" class='jq_title_div' runat ="server" style="overflow:hidden ;margin-left:0px; width:1004px; margin-top :0px; border-collapse :collapse ;">
      <table class='ms_title' style="width:560px" cellpadding="0" cellspacing="0">
          <tr>
              <td style="width:70px;">
                  生产线
              </td>
              <td style="width:210px;">
                  商品CD
              </td>
              <td style="width:150px;">
                  模板CD
              </td>
              <td style="">
                  色</td>
          </tr>
      </table>
      <table class='ms_input' style="width:560px" cellpadding="0" cellspacing="0">
          <tr>
              <td style="width:70px;">
              <asp:TextBox ID="tbxLineId" class="jq_line_id_ipt" runat="server" maxLength="10" style="width:64px;background-color: #FFAA00;"></asp:TextBox>
          </td>
              <td style="width:210px;">
              <asp:TextBox ID="tbxCode" class="jq_code_ipt" runat="server" maxLength="20" style="width:204px;background-color: #FFAA00;"></asp:TextBox>
          </td>
              <td style="width:150px;">
              <asp:TextBox ID="tbxTempId" class="jq_temp_id_ipt" runat="server" maxLength="10" style="width:95%;background-color: #FFAA00;"></asp:TextBox>
          </td>
              <td style="">
                 <asp:TextBox ID="tbxColor" class="jq_color_nm_ipt" runat="server" maxLength="50" style="width:95%;background-color: #FFAA00;"></asp:TextBox></td>
          </tr>
      </table>

</div>

<!--明細Body部-->
<div id="divGvw" class='jq_ms_div' runat ="server" style="overflow:scroll ; height:440px;margin-left:0px; width:580px; margin-top :0px; border-collapse :collapse ;">

   <asp:GridView CssClass ="jq_ms" Width="560px"  runat="server" ID="gvMs" EnableTheming="True" ShowHeader="False" AutoGenerateColumns="False" BorderColor="black" style=" margin-top :-1px; " TabIndex="-1" >
      <Columns>
          <asp:TemplateField><ItemTemplate ><%#Eval("line_id")%></ItemTemplate><ItemStyle Width="70px" HorizontalAlign="Left" CssClass="jq_line_id" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("code")%></ItemTemplate><ItemStyle Width="210px" HorizontalAlign="Left" CssClass="jq_code" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("temp_id")%></ItemTemplate><ItemStyle Width="150px" HorizontalAlign="Left" CssClass="jq_temp_id" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("color_nm")%></ItemTemplate><ItemStyle  HorizontalAlign="Left" CssClass="jq_color_nm_id" /></asp:TemplateField>
      </Columns>
   </asp:GridView>
      <br />

</div>
        <br />

        <div style="width:1000px; text-align:left;">
            <asp:FileUpload ID="GetUploadFileContent" runat="server" Width="500" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <a href="APP/关联商品与模板.csv">文件例</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnUpload" runat="server" Text="导入" />

        </div>

        <asp:TextBox ID="hidLineId" runat="server" class="jq_line_id_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidCode" runat="server" class="jq_code_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidTempId" runat="server" class="jq_temp_id_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidColorNm" runat="server" class="jq_color_nm_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidOldRowIdx" runat="server" class="jq_hidOldRowIdx" style=" visibility:hidden;"></asp:TextBox>
    </div>
    </form>
</body>
</html>
