<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MENU.aspx.vb" Inherits="MENU" %>

<%@ Register Src="UserCtrl/Links.ascx" TagName="Links" TagPrefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>MENU</title>
    <link href="Common.css" rel="stylesheet" type="text/css" />
    <link href="MENU.aspx.css" rel="stylesheet" type="text/css" />
    <%--    <script type="text/javascript" src="/jquery/jquery-3.6.0.min.js"></script>
    <script type="text/javascript" src="/jquery-ui-1.12.1/jquery-ui.min.js"></script>
    <script type="text/javascript" src="/jquery/jquery.cookie.js"></script>--%>

    <script type="text/javascript" src="./js/jquery-1.4.1.min.js"></script>

    <link rel="stylesheet" href="./jquery-ui-1.12.1/jquery-ui.css" />
    <script language="javascript">

        function ChkIptAdmin() {

            var pass = prompt("请输入管理密码！！");

            if (pass == "admin") {
                
                return true;
            } else {
                alert("管理密码不正确");
                return false;
            }

        }

    </script>
    <script type="text/javascript" src="./Menu.js?randomId=1.00"></script>
    <link rel="stylesheet" href="./Menu.css" />
    <style>
        .tbl{

}

.tbl td{
    width:200px;
    border:1px solid #ccc;
    text-align:center;
    line-height:60px;
    vertical-align:top;
}
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <div>

            <uc1:links id="Links" runat="server" visible="false" />

            <div class='title_div'>
                <%Response.Write(Common.SetTitle("MENU"))%>
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Label ID="lblUserCd" runat="server" Text="Label"></asp:Label>
                <asp:Label ID="lblUserName" runat="server" Text="Label"></asp:Label>

            </div>
            <table class="tbl">
                <tr style="height:100px; color:#fff;font-size:30px;background-color:#5063ad;font-weight:bolder">
                    <td>
                        <div class="">Ｍａｓｔｅｒ</div>
                    </td>
                    <td>
                        <div class="">生产计划作成</div>
                    </td>
                    <td>
                        <div class="">检查</div>
                    </td>
                    <td>
                        <div class="">帐票</div>
                    </td>
                    <td>
                        <div class="">其他</div>
                    </td>
                </tr>
               <tr style="height:100px; color:blue;font-size:30px">
                    <td>
                        <asp:LinkButton ID="lbLogout" runat="server">Logout</asp:LinkButton><br />
                        <asp:LinkButton ID="lbUser" runat="server">用户MS</asp:LinkButton><br />
                        <asp:LinkButton ID="lbProject" runat="server">工程MS</asp:LinkButton><br />
                        <asp:LinkButton ID="lbTools" runat="server">治具MS</asp:LinkButton><br />
                        <a href="#" onclick="window.open('APP/PictureImport/PictureImport.application')">图片MS</a><br />
                        <asp:LinkButton ID="lbCheckMethod" runat="server">检查方法MS</asp:LinkButton><br />
                    </td>
                    <td>
                        <asp:LinkButton ID="lbTemp" runat="server">模板MS</asp:LinkButton><br />
                        <asp:LinkButton ID="lbRelation" runat="server" Visible="false">关联商品与模板</asp:LinkButton><br />
                        <div style="border: 1px solid #111; padding: 12px 2px 12px 2px;">
                            <a href="APP/关联模板与商品CD.xlsm" style="background-color: #ccc; font-size: 14px; width: 200px; height: 30px;">关联商品与模板(旧 不使用)</a><br />
                            <input type="button" class="new-badge" value="关联商品与模板 新DWP2.0" onclick="login('t_cd_temp_relation')" />
                        </div>

                        <asp:LinkButton ID="lbPlan" runat="server">检查计划</asp:LinkButton>
                    </td>
                    <td>
                        <asp:LinkButton ID="LinkButton1" runat="server">检查一览</asp:LinkButton><br />
                        <div class="hidden_content">
                            <asp:LinkButton ID="lbtnBaogong" runat="server" Visible="false">报工</asp:LinkButton>
                        </div><br />
                        <div class="check_image">
                            <img alt="" src="IMG/check.jpg" />
                        </div>
                    </td>
                    <td><%--                  <a href="#" onclick="window.open('APP/tyouhyou/Tyouhyou.application')" style="display:none;">帐票出力</a>--%>
                        <iframe id="ifm2" runat="server" frameborder="0" width="210px" height="50px" style="background-color: transparent; overflow: hidden"></iframe>
                        <br />
                    </td>
                    <td><a href="APP/FirstInstall.zip">安装说明</a><br />
                        <a href="APP/操作手顺.xlsx">操作手顺</a><br />
                        <a href="#" onclick="window.open('HELP.html')">操作例</a><br />
                        <a href="APP/机能详细.xlsx">机能详细</a><br />
                        <asp:Button ID="btnBaogongSysIsOpen" runat="server" Text="Button" CssClass="admin_button" Visible="false" OnClientClick="return ChkIptAdmin();" />
                    </td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
