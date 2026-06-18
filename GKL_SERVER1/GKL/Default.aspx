<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title></title>

    <%--	<link rel="stylesheet" type="text/css" href="css/normalize.css" />--%>
    <%--	<link rel="stylesheet" type="text/css" href="css/demo.css" />--%>
    <link rel="stylesheet" type="text/css" href="css/linkstyles.css" />
    <link rel="stylesheet" type="text/css" href="css/default.css" />

    <!--CSS-->
    <link href="tmp.css" rel="stylesheet" type="text/css" />

    <script>
        //无参数调用  
        function alertNull() {
            alert("WebBrowser call!");
        }
        //有参数调用  
        function callWithPar(name, address) {
            alert("Name is " + name + "; address is " + address);
        }

        //返回字符串  
        function returnString() {
            return ("This is a test.");
        }

        //返回对象  
        function returnScriptObject() {
            return (new (MyObject));
        }

        function MyObject() {
            this.Data = "Data for my private object.";
        }
    </script>

</head>
<body style="background-color: #efefef;">
    <form id="form1" runat="server">
        <div class='title_div' style="width: 100%; height: 50px; line-height: 50px; vertical-align: middle; ">

            <div style="float: left; border-radius: 25px; margin: 5px 0px; width: 50px; height: 40px; background-color: #fff; text-align: center; color: #000; font-size: 42px;">
                🍄
            </div>
            <div style="float: left; padding-left: 4px;">
                <%Response.Write(Common.SetTitle("Login　in"))%>
            </div>


        </div>
        <br />

        <asp:Panel ID="Panel1" runat="server" CssClass="jyouken_panel" Style="background-color: #fff;">
            <br />
            <br />
            <br />
            <table style="width: 700px; margin: 0 auto 0 auto; font-size: 40px;">
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 250px;">用户CD：</td>
                    <td>
                        <asp:TextBox ID="tbx_user_cd" runat="server" Font-Size="50px" Height="50px" Style="ime-mode: disabled;" Width="100%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>密码：</td>
                    <td>
                        <asp:TextBox ID="tbx_user_password" runat="server" TextMode="Password" Style="ime-mode: disabled;" Font-Size="50px" Height="50px" Width="100%"></asp:TextBox></td>

                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td style="text-align:right;">
                        <asp:Button ID="btnLoginIn" runat="server" Text="Login In" Font-Size="30px" Height="60px" Width="300px" /></td>
                </tr>
            </table>
            <br />
            <br />
            <br />
        </asp:Panel>

<%--        <a href="http://10.160.192.114/AppDownload/default.aspx" target="_blank">
            软件安装 测试用
        </a>--%>
        <br />
        <a href="\\10.160.192.114\gkl_ie_json_show\" target="_blank">
            \\10.160.192.114\gkl_ie_json_show\
        </a>

        
    </form>
</body>
</html>
