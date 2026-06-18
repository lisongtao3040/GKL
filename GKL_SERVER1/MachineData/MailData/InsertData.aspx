<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="InsertData.aspx.vb" Inherits="MailData.InsertData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 19px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Text="Email情报登录" ></asp:Label>
    </div>
    <table>
        <tr>
            <td>系：</td>
            <td><asp:TextBox ID="tbx_xi" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>线号：</td>
            <td><asp:TextBox ID="tbx_line_id" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>送信先：</td>
            <td><asp:TextBox ID="tbx_to_email" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>CC：</td>
            <td><asp:TextBox ID="tbx_cc_email" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>送信时间：</td>
            <td><asp:DropDownList ID="ddl_send_email_time" runat="server"></asp:DropDownList></td>
        </tr>
        <tr>
            <td>启动：</td>
            <td><asp:CheckBox ID="cb_qidong" runat="server" Checked="True" />
        </tr>
    </table>
    <asp:Button ID="btnInsert" runat="server" Text="追加" /> 
    </form>
</body>
</html>
