<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MENU.aspx.vb" Inherits="MENU" %>

<%@ Register Src="UserCtrl/Links.ascx" TagName="Links" TagPrefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>MENU</title>
    <link rel="stylesheet" type="text/css" href="css/default.css" />

    <!--CSS-->
    <link href="tmp.css" rel="stylesheet" type="text/css" />

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
/*
        function getQueryParam(param) {
            const params = window.location.search.substring(1).split('&');
            for (let i = 0; i < params.length; i++) {
                const pair = params[i].split('=');
                // 这里使用 decodeURIComponent 解码参数
                if (pair[0] === param) {
                    return decodeURIComponent(pair[1]);
                }
            }
            return null;
        }

        // 设置 cookie
        function setCookie(name, value, days) {
            var expires = "";
            if (days) {
                var date = new Date();
                date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
                expires = "; expires=" + date.toUTCString();
            }
            document.cookie = name + "=" + (value || "") + expires + "; path=/";
        }
*/
        //alert(getQueryParam('login_user_cd'));

        //setCookie('login_user_cd','" & ViewState("menu_user_cd") & "');



    </script>
</head>

<body>
    <form id="form1" runat="server">
        <div>

            <uc1:Links ID="Links" runat="server" Visible="false" />
            <div class='title_div' style="width: 100%; height: 50px; line-height: 50px; vertical-align: middle;">

                <div style="float: left; border-radius: 25px; margin: 5px 0px; width: 50px; height: 40px; background-color: #fff; text-align: center; color: #000; font-size: 42px;">
                    🍄
                </div>
                <div style="float: left; padding-left: 4px;">
                    <%Response.Write(Common.SetTitle("MENU"))%>
                </div>
            </div>
            <div class="" style="">

                <table style="width: 100%;">
                    <tr>
                        <td>
                            <div class="menu_title">Master</div>
                            <div class="menu_contant">
                                <asp:LinkButton ID="lbLogout" runat="server">Logout</asp:LinkButton><br />
                                <asp:LinkButton ID="lbUser" runat="server">用户 MS</asp:LinkButton><br />
                                <asp:LinkButton ID="lbProject" runat="server">工程 MS</asp:LinkButton><br />
                                <asp:LinkButton ID="lbTools" runat="server">治具 MS</asp:LinkButton><br />
           
                        
                                <a href="#" onclick="window.open('APP/PictureImport/PictureImport.application')" class="link link--kukuri">图片 MS</a>
                                <br />
                                    <iframe id="ifm1" runat="server" frameborder="0" width="300px" height="100px" 
                                        style="margin:0px auto 0px auto;">

                                    </iframe>
                                   <br />
                  
                                <%--<asp:LinkButton ID="lbPic" runat="server">图片MS</asp:LinkButton>--%>
                                <%--<a href="APP/PictureImport.zip">图片MS</a>--%>
                                <%--<a href="APP/PictureImport/PictureImport.application" class="link link--kukuri">图片MS</a>--%>



                            </div>
                        </td>
                        <td>
                            <div class="menu_title">生产计划作成</div>
                            <div class="menu_contant">
                                <asp:LinkButton ID="lbTemp" runat="server">模板 MS</asp:LinkButton><br />
                                <asp:LinkButton ID="lbRelation" runat="server">关联商品与模板</asp:LinkButton><br />

                                <%--           <iframe id="ifm" runat="server" width="360px" height="300px"
                                           src="http://10.160.192.114/DWP20/GKL_CHK_MENU2.aspx?user_cd=<%%>"></iframe>--%>


                                <asp:LinkButton ID="lbPlan" runat="server">检查计划</asp:LinkButton><br />
                                <asp:LinkButton ID="lbCheckMethod" runat="server">检查方法MS</asp:LinkButton><br />
                            </div>
                        </td>
                        <td>
                            <div class="menu_title">检查</div>
                            <div class="menu_contant">
                                <asp:LinkButton ID="LinkButton1" runat="server" Font-Size="40px">检查一览</asp:LinkButton>
                                <br />
                                <asp:LinkButton ID="lbtnColorChkList" runat="server" Font-Size="40px">色检查一览</asp:LinkButton>
                                <br />
                                <asp:LinkButton ID="lbtnBaogong" runat="server" Font-Size="40px">报工</asp:LinkButton>
                                <br />
                                <img alt="" src="IMG/check.jpg" width="120" />
                            </div>
                        </td>
                        <td>
                            <div class="menu_title">帐票</div>
                            <div class="menu_contant">
                                <%-- <asp:LinkButton ID="LinkButton2" runat="server">帐票出力</asp:LinkButton>--%>
                                <%--<a href="APP/Tyouhyou.xlsm">帐票出力</a><br /></td>--%>
                                <%--             <a href="APP/tyouhyou/Tyouhyou.application">帐票出力</a>--%>

                                <iframe id="ifm2" runat="server" frameborder="0" width="280px" height="300px"></iframe>
                                <br />
                                <a href="#" style="text-decoration: line-through; color: #ccc;" onclick="window.open('APP/tyouhyou/Tyouhyou.application')">帐票出力 预定废弃</a><br />

                            </div>
                        </td>
                        <td>
                            <div class="menu_title">其他</div>
                            <div class="menu_contant">
                                <a href="APP/FirstInstall.zip" class="link link--kukuri">安装说明</a>
                                <br />
                                <a href="APP/操作手顺.xlsx" class="link link--kukuri">操作手顺</a>
                                <br />
                                <a href="#" onclick="window.open('HELP.html')" class="link link--kukuri">操作例</a>
                                <br />
                                <a href="APP/机能详细.xlsx" class="link link--kukuri">机能详细</a>
                                <br />
                                <asp:Button ID="btnBaogongSysIsOpen" runat="server" Text="Button" Font-Size="25px" Width="200" Height="60" OnClientClick="return ChkIptAdmin();" />
                            </div>
                        </td>
                    </tr>
                </table>




            </div>

        </div>
    </form>
</body>
</html>
