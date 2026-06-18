<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Qianpin.aspx.vb" Inherits="Qianpin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <!--JS-->
    <script language="javascript" type="text/javascript" src="./js/jquery-1.4.1.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center;">
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            <hr />
            <input id="btnJan" type="button" value="-" style="font-size: 40px; width: 100px" onclick="add(-1)" />
            <asp:TextBox ID="tbxQianpinSuu" runat="server" Font-Size="40px" Width="200px"></asp:TextBox>
            <input id="btnAdd" type="button" value="+" style="font-size: 40px; width: 100px" onclick="add(1)" />
            <hr />
            <br />
            <%--            <asp:Button ID="btnSave" runat="server" Text="保存" Font-Size="40px" Width="200px" />--%>
            <input id="Button1" type="button" value="保存" style="font-size: 40px; width: 200px" onclick="SaveIt()" />
            <input id="Button2" type="button" value="关闭" style="font-size: 40px; width: 200px" onclick="CloseIt()" />
        </div>
        <script>
            function add(s) {
                var tmp = document.getElementById('tbxQianpinSuu').value;
                if (!/^\d+$/.test(tmp)) {
                    return false;
                }

                suu = parseInt(tmp);
                suu = suu + s;
                document.getElementById('tbxQianpinSuu').value = suu;
            }


            function CloseIt() {
                var t;
                var divGvw;
                divGvw = window.opener.document.getElementById('divGvw');
                t = window.opener.document.getElementById('divGvw').scrollTop;

                window.close();
            }

            function SaveIt() {

                var suu = document.getElementById('tbxQianpinSuu').value;

                if (!/^\d+$/.test(suu)) {
                    alert("不是整数");
                    return false
                }

                var rtv = false;

                $.ajax({
                    type: 'POST',
                    url: './AJAX.aspx?kbn=SaveQianPin&suu=' + suu + '&chk_no=<%Response.Write(Request.QueryString("chk_no"))%>',
                    async: false, //true:yibu
                    datatype: 'html',//'xml', 'html', 'script', 'json', 'jsonp', 'text'.
                    //when complete
                    complete: function (XMLHttpRequest, textStatus) {
                        //$("#userlist")[0].innerHTML = XMLHttpRequest.responseText;
                        rtv = true;

                    }
                });

                if (rtv == true) {
                    window.opener.qpBtn.value = document.getElementById('tbxQianpinSuu').value;
                }
                window.close();
                return rtv;
            }
        </script>
    </form>
</body>
</html>
