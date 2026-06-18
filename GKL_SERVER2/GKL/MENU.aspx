<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MENU.aspx.vb" Inherits="MENU" %>

<%@ Register Src="UserCtrl/Links.ascx" TagName="Links" TagPrefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>MENU</title>
    <link href="Common.css" rel="stylesheet" type="text/css" />
    <link href="MENU.aspx.css" rel="stylesheet" type="text/css" />
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
</head>

<body>
    <form id="form1" runat="server">
        <div>

            <uc1:Links ID="Links" runat="server" Visible="false" />

            <div class='title_div'>
                <%Response.Write(Common.SetTitle("MENU"))%>
            </div>

            <div class="jq_ms_div">
                <div class="menu_container">
                    <div class="menu_header">
                        <div class="menu_column">
                            <div class="column_title">Ｍａｓｔｅｒ</div>
                        </div>
                        <div class="menu_column">
                            <div class="column_title">生产计划作成</div>
                        </div>
                        <div class="menu_column">
                            <div class="column_title">检查</div>
                        </div>
                        <div class="menu_column">
                            <div class="column_title">帐票</div>
                        </div>
                        <div class="menu_column">
                            <div class="column_title">其他</div>
                        </div>
                    </div>

                    <div class="menu_content">
                        <div class="menu_column">
                            <div class="menu_links">
                                <asp:LinkButton ID="lbLogout" runat="server">Logout</asp:LinkButton>
                                <asp:LinkButton ID="lbUser" runat="server">用户MS</asp:LinkButton>
                                <asp:LinkButton ID="lbProject" runat="server">工程MS</asp:LinkButton>
                                <asp:LinkButton ID="lbTools" runat="server">治具MS</asp:LinkButton>
                                <a href="#" onclick="window.open('APP/PictureImport/PictureImport.application')">图片MS</a>
                                <asp:LinkButton ID="lbCheckMethod" runat="server">检查方法MS</asp:LinkButton>
                            </div>
                        </div>

                        <div class="menu_column">
                            <div class="menu_links">
                                <asp:LinkButton ID="lbTemp" runat="server">模板MS</asp:LinkButton>
                                <asp:LinkButton ID="lbRelation" runat="server" Visible="false">关联商品与模板</asp:LinkButton>
                                <a href="APP/关联模板与商品CD.xlsm">关联商品与模板</a>
                                <asp:LinkButton ID="lbPlan" runat="server">检查计划</asp:LinkButton>
                            </div>
                        </div>

                        <div class="menu_column">
                            <div class="menu_links">
                                <asp:LinkButton ID="LinkButton1" runat="server">检查一览</asp:LinkButton>
                                <div class="hidden_content">
                                    <asp:LinkButton ID="lbtnBaogong" runat="server" Visible="false">报工</asp:LinkButton>
                                </div>
                                <div class="check_image">
                                    <img alt="" src="IMG/check.jpg" />
                                </div>
                            </div>
                        </div>

                        <div class="menu_column">
                            <div class="menu_links">
              <%--                  <a href="#" onclick="window.open('APP/tyouhyou/Tyouhyou.application')" style="display:none;">帐票出力</a>--%>
                                <iframe id="ifm2" runat="server" frameborder="0" width="210px" height="50px" style="background-color:transparent;overflow:hidden"></iframe>
                                <br />
                            </div>
                        </div>

                        <div class="menu_column">
                            <div class="menu_links">
                                <a href="APP/FirstInstall.zip">安装说明</a>
                                <a href="APP/操作手顺.xlsx">操作手顺</a>
                                <a href="#" onclick="window.open('HELP.html')">操作例</a>
                                <a href="APP/机能详细.xlsx">机能详细</a>
                                <asp:Button ID="btnBaogongSysIsOpen" runat="server" Text="Button" CssClass="admin_button" Visible="false" OnClientClick="return ChkIptAdmin();" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </form>
</body>
</html>
