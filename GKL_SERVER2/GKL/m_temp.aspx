<%@ Page Language="VB" AutoEventWireup="false" CodeFile="m_temp.aspx.vb" Inherits="m_temp" %>
<%@ Register Src="~/UserCtrl/Links.ascx" TagPrefix="uc1" TagName="Links" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1"  />
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>模板MS</title>

    <!--JS-->
    <script language="javascript" type="text/javascript" src="./js/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="./JidouTemp.js"></script>
    <script language="javascript" type="text/javascript" src="./m_temp.aspx.js"></script>

    <!--CSS-->
    <link href="tmp.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
    <div>
        <div class='title_div'><%Response.Write(Common.SetTitle("模板MS"))%>
            
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
            <td>
                <input id="newTemp" runat ="server" type ="button" value="新规模板" style="height:30px;width:80px;"  /></td>
            <td>&nbsp;</td>
            <td></td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            </tr>
            <tr>
            <td>检查模板编号 : &nbsp;</td>
            <td>
              <asp:TextBox ID="tbxTempId_key" class="jq_temp_id_key" runat="server" style="width:160px;background-color: #FFAA00;" list="temp_ids"></asp:TextBox>
              <datalist id="temp_ids" runat="server"></datalist>
            </td>
            <td>
                <asp:Button ID="btnCopy" runat="server" Text="复制到" CssClass="jq_sel"  style="height:30px;width:80px;"/>
            </td>
            <td>
                &nbsp;<%--  <asp:Button ID="btnNewTelement" runat="server" Text="新规模板" OnClick="window.open('m_temp_name.aspx'); return false;" CssClass="jq_sel" />--%></td>
            <td>
                &nbsp;</td>
            <td>
                新模板ID/名：
              <asp:TextBox ID="tbxTempId_new" class="jq_temp_id_key" runat="server" style="width:160px;background-color: #FFAA00;"></asp:TextBox>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                <asp:TextBox ID="tbxTempName_new" class="jq_temp_id_key" runat="server" style="width:160px;background-color: #FFAA00;"></asp:TextBox>

            </td>
            </tr>
            <tr>
            <td>检查项目ID : &nbsp;</td>
            <td>
              <asp:TextBox ID="tbxChkMethodId_key" class="jq_chk_method_id_key" runat="server" style="width:160px;background-color: #FFAA00;"></asp:TextBox>
            </td>
            <td>
        <asp:Button ID="btnSelect" runat="server" Text="検索" style="height:30px;width:80px;" CssClass="jq_sel" />
                </td>
            <td>
                &nbsp;</td>
            <td></td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            </tr>
        </table>
<table style=" width: 1904px;">

    <tr>

        <td style="width:1020px;border:none;">
<!--Button部-->
        <asp:Button ID="btnUpdate" runat="server" Text="更新" CssClass="jq_upd" />
        <asp:Button ID="btnInsert" runat="server" Text="登録" CssClass="jq_ins" />
        <asp:Button ID="btnDelete" runat="server" Text="削除" CssClass="jq_del" />
        <input type ="button" id="btnChkTemp" value="检查模板" OnClick="CheckTemp(); return false;" style="width:80px;" />
        </td>
        <td style="border:none;">
        .{1} 匹配任意字符 1次<br />
        .{2} 匹配任意字符 2次<br />
        .{0,}匹配任意字符 0到多次<br />
        .{1,}匹配任意字符 0到多次<br />
        .{0,1}匹配任意字符 0到1次<br />

        </td>
    </tr>
</table>

 <br /> 

<!--明細Title部-->
<%--<div id="divGvwTitle" class='jq_title_div' runat ="server" style="overflow:hidden ;margin-left:0px; width:1004px; margin-top :0px; border-collapse :collapse ;">--%>
      <table class='ms_title' style="width:1505px" cellpadding="0" cellspacing="0">
          <tr>
              <td style="width:70px;">
                  生产线
              </td>
              <td style="width:70px;">
                  检查模板编号
              </td>
              <td style="width:80px;">
                  检查项目ID 
              </td>
              <td style="width:70px;">
                  工程ID 
              </td>
              <td style="width:110px;">
                  工程名 
              </td>
              <td style="width:80px;">
                  图片ID
              </td>
              <td style="width:110px;">
                  图片名称
              </td>
              <td style="width:110px;">
                  检查项目
              </td>
              <td style="width:20px;">
                  图片标记
              </td>
              <td style="width:70px;">
                  检查方法ID
              </td>
              <td style="width:110px;">
                  检查方法名
              </td>
              <td style="width:80px;">
                  治具ID
              </td>
              <td style="width:80px;">
                  基准
              </td>
              <td style="width:80px;">
                  工差1
              </td>
              <td style="width:80px;">
                  工差2 
              </td>
              <td style="">
                  基准説明 
              </td>
          </tr>
      </table>
      <table class='ms_input' style="width:1505px" cellpadding="0" cellspacing="0">
          <tr>
              <td style="width:70px;">
                            <asp:TextBox ID="tbxLineId" class="jq_line_id_ipt" runat="server" maxLength="10" style="width:64px;background-color: #FFAA00;"></asp:TextBox>

                      </td>
              <td style="width:70px;">
              <asp:TextBox ID="tbxTempId" class="jq_temp_id_ipt" runat="server" maxLength="10" style="width:64px;background-color: #FFAA00;"></asp:TextBox>
          </td>
              <td style="width:80px;">
              <asp:TextBox ID="tbxChkMethodId" class="jq_chk_method_id_ipt" runat="server" maxLength="10" style="width:74px;background-color: #FFAA00;"></asp:TextBox>
          </td>
              <td style="width:70px;">
              <asp:TextBox ID="tbxProjectId" class="jq_project_id_ipt" runat="server" maxLength="10" style="width:64px;"></asp:TextBox>
          </td>
              <td style="width:110px;">
              <asp:TextBox ID="tbxProjectName" class="jq_project_name_ipt" runat="server" maxLength="40" style="width:104px;"></asp:TextBox>
          </td>
              <td style="width:80px;">
              <asp:TextBox ID="tbxPicId" class="jq_pic_id_ipt" runat="server" maxLength="10" style="width:74px;"></asp:TextBox>
          </td>
              <td style="width:110px;">
              <asp:TextBox ID="tbxPicName" class="jq_pic_name_ipt" runat="server" maxLength="200" style="width:104px; "></asp:TextBox>
          </td>
              <td style="width:110px;">
              <asp:TextBox ID="tbxChkKmName" class="jq_chk_km_name_ipt" runat="server" maxLength="200" style="width:104px;"></asp:TextBox>
          </td>
              <td style="width:20px;">
              <asp:TextBox ID="tbxPicSign" class="jq_pic_sign_ipt" runat="server" maxLength="10" style="width:14px;"></asp:TextBox>
          </td>
              <td style="width:70px;">
              <asp:TextBox ID="tbxChkId" class="jq_chk_id_ipt" runat="server" maxLength="10" style="width:64px;"></asp:TextBox>
          </td>
              <td style="width:110px;">
              <asp:TextBox ID="tbxChkName" class="jq_chk_name_ipt" runat="server" maxLength="20" style="width:104px;"></asp:TextBox>
          </td>
              <td style="width:80px;">
              <asp:TextBox ID="tbxToolId" class="jq_tool_id_ipt" runat="server" maxLength="40" style="width:74px;"></asp:TextBox>
          </td>
              <td style="width:80px;">
              <asp:TextBox ID="tbxKj0" class="jq_kj_0_ipt" runat="server" maxLength="100" style="width:74px;"></asp:TextBox>
          </td>
              <td style="width:80px;">
              <asp:TextBox ID="tbxKj1" class="jq_kj_1_ipt" runat="server" maxLength="20" style="width:74px;"></asp:TextBox>
          </td>
              <td style="width:80px;">
              <asp:TextBox ID="tbxKj2" class="jq_kj_2_ipt" runat="server" maxLength="20" style="width:74px;"></asp:TextBox>
          </td>
              <td style="">
              <asp:TextBox ID="tbxKjExplain" class="jq_kj_explain_ipt" runat="server" maxLength="200" style="width:200px;"></asp:TextBox>
          </td>
          </tr>
      </table>

<%--</div>--%>

<!--明細Body部-->
<%--<div id="divGvw" class='jq_ms_div' runat ="server" style="overflow:scroll ; height:1200px;margin-left:0px; width:1020px; margin-top :0px; border-collapse :collapse ;">--%>

   <asp:GridView CssClass ="jq_ms" Width="1505px" Height="200px"  runat="server" ID="gvMs" EnableTheming="True" ShowHeader="False" AutoGenerateColumns="False" BorderColor="black" style=" margin-top :-1px; " TabIndex="-1" >
      <Columns>
          <asp:TemplateField><ItemTemplate ><%#Eval("line_id")%></ItemTemplate><ItemStyle Width="70px" HorizontalAlign="Left" CssClass="jq_line_id" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("temp_id")%></ItemTemplate><ItemStyle Width="70px" HorizontalAlign="Left" CssClass="jq_temp_id" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("chk_method_id")%></ItemTemplate><ItemStyle Width="80px" HorizontalAlign="Left" CssClass="jq_chk_method_id" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("project_id")%></ItemTemplate><ItemStyle Width="70px" HorizontalAlign="Left" CssClass="jq_project_id" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("project_name")%></ItemTemplate><ItemStyle Width="110px" HorizontalAlign="Left" CssClass="jq_project_name" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("pic_id")%></ItemTemplate><ItemStyle Width="80px" HorizontalAlign="Left" CssClass="jq_pic_id" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("pic_name")%></ItemTemplate><ItemStyle Width="110px" HorizontalAlign="Left" CssClass="jq_pic_name" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("chk_km_name")%></ItemTemplate><ItemStyle Width="110px" HorizontalAlign="Left" CssClass="jq_chk_km_name" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("pic_sign")%></ItemTemplate><ItemStyle Width="20px" HorizontalAlign="Left" CssClass="jq_pic_sign" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("chk_id")%></ItemTemplate><ItemStyle Width="70px" HorizontalAlign="Left" CssClass="jq_chk_id" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("chk_name")%></ItemTemplate><ItemStyle Width="110px" HorizontalAlign="Left" CssClass="jq_chk_name" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("tool_id")%></ItemTemplate><ItemStyle Width="80px" HorizontalAlign="Left" CssClass="jq_tool_id" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("kj_0")%></ItemTemplate><ItemStyle Width="80px" HorizontalAlign="Left" CssClass="jq_kj_0" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("kj_1")%></ItemTemplate><ItemStyle Width="80px" HorizontalAlign="Left" CssClass="jq_kj_1" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("kj_2")%></ItemTemplate><ItemStyle Width="80px" HorizontalAlign="Left" CssClass="jq_kj_2" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("kj_explain")%></ItemTemplate><ItemStyle  HorizontalAlign="Left" CssClass="jq_kj_explain" /></asp:TemplateField>
      </Columns>
   </asp:GridView>
<%--</div>--%>
        <br />
        <hr />
        <div style="width:1200px; text-align:left; ">
            <asp:FileUpload ID="GetUploadFileContent" runat="server" Width="500px" />
            <asp:Button ID="btnUpload" runat="server" Text="导入" />
            <a href="APP/模板MS_CSV.xlsm">CSV作成用 参照</a>
        </div>

        <asp:TextBox ID="hidLineId" runat="server" class="jq_line_id_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidTempId" runat="server" class="jq_temp_id_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidChkMethodId" runat="server" class="jq_chk_method_id_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidProjectId" runat="server" class="jq_project_id_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidProjectName" runat="server" class="jq_project_name_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidPicId" runat="server" class="jq_pic_id_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidPicName" runat="server" class="jq_pic_name_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidChkKmName" runat="server" class="jq_chk_km_name_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidPicSign" runat="server" class="jq_pic_sign_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidChkId" runat="server" class="jq_chk_id_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidChkName" runat="server" class="jq_chk_name_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidToolId" runat="server" class="jq_tool_id_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidKj0" runat="server" class="jq_kj_0_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidKj1" runat="server" class="jq_kj_1_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidKj2" runat="server" class="jq_kj_2_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidKjExplain" runat="server" class="jq_kj_explain_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidOldRowIdx" runat="server" class="jq_hidOldRowIdx" style=" visibility:hidden;"></asp:TextBox>
    </div>


    </form>


</body>
</html>
