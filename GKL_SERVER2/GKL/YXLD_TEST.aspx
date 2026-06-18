<%@ Page Language="VB" AutoEventWireup="false" CodeFile="YXLD_TEST.aspx.vb" Inherits="YXLD_TEST" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <!--JS-->
    <script language="javascript" type="text/javascript" src="./js/jquery-1.4.1.min.js"></script>
    <!--JS-->
    <script language="javascript" type="text/javascript" src="YXLD_TEST.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            CD:<asp:TextBox ID="tbxCode_key" runat="server" Text="PP-AA08H-MEZ9" />
            作番：<asp:TextBox ID="tbxMakeNo_key" runat="server" Text="9008004043" />
            生产线:<asp:TextBox ID="hidPlanLineId" runat="server" class="jq_chk_no_ipt" Text="SRM1532B"></asp:TextBox>

            <hr />
            <input type="button" value="1.准备影像检查" id="btnZB" />
            <div id="rtv0"></div>
            <hr />
            <br />
            <input type="button" value="2.业者模拟调用接口1" id="btnSv1" />

            <div id="rtv1"></div>
            <hr />
            
            以下与设定影像返回结果
            <br />

            <textarea id="tbxTxt" style="width: 800px; height: 200px;">捆包标签对错:OK,说明书:OK,保护条:OK</textarea>
            <br />
            <input type="button" value="3.业者模拟调用接口2" id="btnSv2" />
            <div id="rtv2"></div>
        </div>
    </form>
</body>
</html>
