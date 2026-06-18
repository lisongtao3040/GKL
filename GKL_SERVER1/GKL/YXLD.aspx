<%@ Page Language="VB" AutoEventWireup="false" CodeFile="YXLD.aspx.vb" Inherits="YXLD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript" src="./config.js?ver=1.7"></script>
    <script language="javascript" type="text/javascript" src="./js/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="./JidouTemp.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>区分 
                    </td>
                    <td>工单号 
                    </td>
                    <td>次数 
                    </td>
                    <td>制品CODE 
                    </td>
                    <td>左右 
                    </td>
                </tr>

                <tr>
                    <td>
                        <input type="text" id="tbxKbn" value="11" />
                    </td>
                    <td>
                        <input type="text" id="tbxNo" value="9006873076" />
                    </td>
                    <td>
                        <input type="text" id="tbxCnt" value="01" />
                    </td>
                    <td>
                        <input type="text" id="tbxCd" value="WA-AC00HR-MHFT" />
                    </td>
                    <td>
                        <input type="text" id="tbxLR" value="1" />
                    </td>
                </tr>

            </table>

            <input type="button" value="发生" onclick="AjaxPost()" />
            <div id="divRlt"></div>
            <iframe id="myframe" width="100%" frameBorder="0" src="" scrolling="no"></iframe>
            <script>
                String.prototype.PadLeft = function (len, charStr) {
                    var s = this + '';
                    return new Array(len - s.length + 1).join(charStr || '') + s;
                }
                String.prototype.PadRight = function (len, charStr) {
                    var s = this + '';
                    return s + new Array(len - s.length + 1).join(charStr || '');
                }

                var tms = 30;
                //只是个测试页面，所以不用改

                function AjaxPost() {

                    var kbn = $("#tbxKbn").val().PadLeft(2,"0");
                    var no = $("#tbxNo").val().PadLeft(10, "0");
                    var cnt = $("#tbxCnt").val().PadLeft(2, "0");
                    var cd = $("#tbxCd").val();
                    var lr = $("#tbxLR").val();
                    var url;
                    //url = "http://10.162.201.93:5001/api/code?code=" + kbn + no + cnt + cd + lr;
                    url = PUB_YXLD_URL+"?code=" + kbn + no + cnt + cd + lr;
                    $('#myframe').attr('src', url);

                    $.ajax({
                        type: 'POST',
                        url: 'YXLD_SEND.aspx?ajaxActionType=1',
                        async: false, //true:yibu
                        data: {
                            kbn: kbn,
                            no: no,
                            cnt: cnt,
                            cd: cd,
                            lr: lr,
                        },
                        datatype: 'html', //'xml', 'html', 'script', 'json', 'jsonp', 'text'.
                        beforeSend: function () { },
                        //when success
                        success: function (data) {
                            if (data) {
                                tms = 30;
                                AjaxGet();
                            } else {
                                alert("请求失败");
                            }
                        },
                        //when complete
                        complete: function (XMLHttpRequest, textStatus) {
                        },
                        //when error
                        error: function () { }
                    });
                }



                function AjaxGet() {

                    var kbn = $("#tbxKbn").val().PadLeft(2, "0");
                    var no = $("#tbxNo").val().PadLeft(10, "0");
                    var cnt = $("#tbxCnt").val().PadLeft(2, "0");

                    $.ajax({
                        type: 'POST',
                        url: 'YXLD_SEND.aspx?ajaxActionType=2',
                        async: false, //true:yibu
                        data: {
                            kbn: kbn,
                            no: no,
                            cnt: cnt
                        },
                        datatype: 'html', //'xml', 'html', 'script', 'json', 'jsonp', 'text'.
                        beforeSend: function () { },
                        //when success
                        success: function (data) {

                            if (data.substring(0,2) == "OK") {
                                tms = 0;
                                $("#divRlt").text(tms + "接收完了" + data);
                            } else {
                                $("#divRlt").text(tms + "接收中......" + data);
                            }
                        },
                        //when complete
                        complete: function (XMLHttpRequest, textStatus) {
                        },
                        //when error
                        error: function () { }
                    });

                    tms = tms - 1;
                    if (tms > 0) {
                        setTimeout(function () { AjaxGet() }, 1000);
                    }
                }

            </script>
        </div>
    </form>
</body>
</html>
