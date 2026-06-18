<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MailWebForm.aspx.vb" Inherits="MailData.MailWebForm"  enableEventValidation="true"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Text="Email管理" ></asp:Label></div>
    <div>
    <div>
        <asp:Label ID="lbl_line_id" runat="server" Text="线号：" Width ="100px"></asp:Label>
        <asp:TextBox ID="tbx_line_id" runat="server"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" Text="検索" />
        <asp:Button ID="btnInsert" runat="server" Text="追加" />
    </div>
    <div>
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
    </div>
    <div>
        <asp:GridView ID="gvMainData" runat="server" DataKeyNames="line_id" onRowDeleting = "gvMainData_RowDeleting">
            <Columns>
                <asp:CommandField ButtonType="Button" ShowCancelButton="True" ShowEditButton="True" CancelText="取消" />
                <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>
    </div>
        
    <div>
        <asp:Panel ID="pnlUpd" runat="server">
        <table>
        <tr>
            <td>系：</td>
            <td><asp:TextBox ID="tbx_xi_upd" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>线号：</td>
            <td><asp:Label ID="lbl_line_id_upd" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td>送信先：</td>
            <td><asp:TextBox ID="tbx_to_email_upd" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>CC：</td>
            <td><asp:TextBox ID="tbx_cc_email_upd" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>送信时间：</td>
            <td><asp:DropDownList ID="ddl_send_email_time_upd" runat="server"></asp:DropDownList></td>
        </tr>
        <tr>
            <td>启动：</td>
            <td><asp:CheckBox ID="cb_qidong_upd" runat="server" Checked="True" />
        </tr>
    </table>
    <asp:Button ID="btnUpdate" runat="server" Text="更新" /> 
    </asp:Panel>
    </div>
    </form>
</body>
</html>
