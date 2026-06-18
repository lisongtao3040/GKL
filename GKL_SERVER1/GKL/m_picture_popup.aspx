<%@ Page Language="VB" AutoEventWireup="false" CodeFile="m_picture_popup.aspx.vb" Inherits="m_picture" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1"  />
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>m_picture</title>

    <!--JS-->
    <script language="javascript" type="text/javascript" src="./js/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="./JidouTemp.js"></script>
    <script language="javascript" type="text/javascript" src="./m_picture.aspx.js"></script>

    <!--CSS-->
    <link href="tmp.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
    <div>

<!--明細Title部-->
<div id="divGvwTitle" class='jq_title_div' runat ="server" style="overflow:hidden ;margin-left:0px; width:1004px; margin-top :0px; border-collapse :collapse ;">
      <table class='ms_title' style="width:960px" cellpadding="0" cellspacing="0">
          <tr>
              <td style="width:70px;">
                  图片ID
              </td>
              <td style="width:70px;">
                  生产线
              </td>
              <td style="width:140px;">
                  图片名称
              </td>
              <td style="">
                  图片
              </td>
          </tr>
      </table>
</div>
         
<!--明細Body部-->
<div id="divGvw" class='jq_ms_div' runat ="server" style="overflow:scroll ; height:700px;margin-left:0px; width:1020px; margin-top :0px; border-collapse :collapse ;">

   <asp:GridView CssClass ="jq_ms" Width="960px"  runat="server" ID="gvMs" EnableTheming="True" ShowHeader="False" AutoGenerateColumns="False" BorderColor="black" style=" margin-top :-1px; " TabIndex="-1" >
      <Columns>
          <asp:TemplateField><ItemTemplate ><%#Eval("pic_id")%></ItemTemplate><ItemStyle Width="70px" HorizontalAlign="Left" CssClass="jq_pic_id" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("line_id")%></ItemTemplate><ItemStyle Width="70px" HorizontalAlign="Left" CssClass="jq_line_id" /></asp:TemplateField>
          <asp:TemplateField><ItemTemplate ><%#Eval("pic_name")%></ItemTemplate><ItemStyle  Width="140px" HorizontalAlign="Left" CssClass="jq_pic_name" /></asp:TemplateField>
      
            <asp:TemplateField><ItemTemplate ><asp:Image ID="imgLook" runat="server" CssClass="JQ_IMG" /></ItemTemplate><ItemStyle  HorizontalAlign="Left" CssClass="jq_pic_name" /></asp:TemplateField>
      
      
      </Columns>
   </asp:GridView>
</div>

        <asp:TextBox ID="hidPicId" runat="server" class="jq_pic_id_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidLineId" runat="server" class="jq_line_id_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidPicName" runat="server" class="jq_pic_name_ipt" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidOldRowIdx" runat="server" class="jq_hidOldRowIdx" style=" visibility:hidden;"></asp:TextBox>


        <asp:TextBox ID="hidPrePicId" runat="server" class="" style=" visibility:hidden;"></asp:TextBox>
        <asp:TextBox ID="hidPrePicName" runat="server" class="" style=" visibility:hidden;"></asp:TextBox>

    </div>

    <script language="javascript">
        $(".jq_ms tr").dblclick(function () {
            window.opener.document.getElementById($("#hidPrePicId").val()).value = $(this).find("td")[0].innerText;
            window.opener.document.getElementById($("#hidPrePicName").val()).value = $(this).find("td")[2].innerText
            window.close();
        })
    </script>


    </form>
</body>
</html>
