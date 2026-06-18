<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="UTF-8" />
    <%-- <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />--%>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title></title>

    <link href="Common.css" rel="stylesheet" type="text/css" />
    <link href="Default.aspx.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
        <div class='title_div'>
            <%Response.Write(Common.SetTitle("登录GKL2"))%>
        </div>
        <asp:Panel ID="Panel1" runat="server" CssClass="jyouken_panel">
            <div class="login-form">
                <div class="form-row">
                    <div class="form-label">用户CD：</div>
                    <div class="form-input">
                        <asp:TextBox ID="tbx_user_cd" runat="server" CssClass="login-input ime-disabled"></asp:TextBox>
                    </div>
                </div>

                <div class="form-row">
                    <div class="form-label">密码：</div>
                    <div class="form-input">
                        <asp:TextBox ID="tbx_user_password" runat="server" CssClass="login-input ime-disabled" TextMode="Password"></asp:TextBox>
                    </div>
                </div>

                <div class="form-button-row">
                    <asp:Button ID="btnLoginIn" runat="server" CssClass="login-button" Text="登录" />
                </div>
                <br />
                <asp:Label ID="lblMsg" runat="server" CssClass="error-message"></asp:Label>
            </div>
        </asp:Panel>
    </form>
</body>
</html>
