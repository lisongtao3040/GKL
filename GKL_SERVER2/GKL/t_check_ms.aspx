<%@ Page Language="C#" AutoEventWireup="true" CodeFile="t_check_ms.aspx.cs" Inherits="t_check_ms" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>检查明細</title>

    <script>
        var camera_flg;
        camera_flg = <%=ViewState["camera_flg"].ToString()%>;

    </script>
    <!--JS-->
    <script language="javascript" type="text/javascript" src="./js/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="./Common.js"></script>
    <%--    <script src="js/jquery-2.1.1.js"></script>--%>
    <script language="javascript" type="text/javascript" src="./t_check_ms.aspx.js?version=55336"></script>
    <script language="javascript" type="text/javascript" src="./t_check_msForTpAndBg.js?version=5355"></script>

    <script language="javascript" type="text/javascript" src="./JsBarcode.js"></script>
    <script language="javascript" type="text/javascript" src="./Qrcode.js"></script>

    <!--CSS-->
    <link href="tmp_chk.css?version=201903260500" rel="stylesheet" type="text/css" />
    <link href="t_check_ms.aspx.css?version=201903260500" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
        <div class='title_div' style="width: 99%; height: 50px; line-height: 50px; vertical-align: middle; background: url(IMG/banner2.jpg)">

            <div style="float: left; border-radius: 25px; margin: 5px 0px; width: 50px; height: 40px; background-color: #fff; text-align: center; color: #000; font-size: 42px;">
                🍄
            </div>
            <div style="float: left; padding-left: 4px;">
                <% Response.Write(Common.SetTitle("检查明細")); %>
                <asp:Button ID="btnZB" runat="server" Text=" 1.准备影像检查" Width="400" />
                <input type="button" value="关联托盘" id="joinTp_scmx" style="width: 180px; height: 60px; margin-left: 30px;" />
                <input type="button" value="修正托盘" id="editTp" style="width: 180px; height: 60px; margin-left: 30px;" />
            </div>
        </div>
        <div class="NEW_TP_DIV" id="NEW_TP_DIV">
            <b class="title_txt">关联托盘
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <button class="close_btn" onclick="$('#txtNewTpBarcode').val('');return false">清空</button>
                <button class="close_btn" onclick="CloseDivPanel('NEW_TP_DIV');return false">关闭</button>
            </b>
            <hr />
            托盘码：<input type='text' typ='scan' class='tp_barcode' id='txtNewTpBarcode' value='' />
            <button class="close_btn" onclick="SetNewTpBarcode();return false">设定</button>
        </div>

        <div class="EDIT_TP_DIV" id="EDIT_TP_DIV">
            <b class="title_txt">修正托盘
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <button class="close_btn" onclick="CloseDivPanel('EDIT_TP_DIV');return false">关闭</button>
            </b>
            <hr />
            元托盘码：
            <b id="GetOldTps"></b>
            <hr />
            托盘码1：<input type='text' typ='scan' class='tp_barcode' id='txtNewTpBarcode1' value='' />
            托盘码2：<input type='text' typ='scan' class='tp_barcode' id='txtNewTpBarcode2' value='' />
            <button class="close_btn" onclick="UpdTpBarcode();return false">修改</button>
        </div>

        <div class="baogong_panel" id="baogong_panel">
            <div class="baogong_panel_title" id="baogong_panel_title">
                工单：<a id="gongdan"></a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    数量：<asp:Label ID="lblSuu2" runat="server" Text="Label"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="button" value="关闭" id="btnCloseBaogong" />
                <input type="button" value="强制完了" id="btnQZComplete" onclick="$('#btnComplete').click()" />

                <hr />

                <%--                入数：
                <input type='text' class='tp_in_suu' id='tp_in_suu' value='' />
                托盘码：
                <input type='text' class='tp_bar_scan' id='tp_bar_scan' value='' readonly="true" />
                <input type="button" value="报工" id="btnBaogong" />
                --%>
            </div>
            <div class="baogong_panel_data" id="baogong_panel_data">
            </div>
        </div>

        <div style="display: none;">

            <hr />
            <div id="rtv0"></div>
            <hr />
            <br />
            <input type="button" value="2.业者模拟调用接口1" id="btnSv1" />

            <div id="rtv1"></div>
            <hr />

            以下与设定影像返回结果
            <br />

            <textarea id="tbxTxt" style="width: 800px; height: 200px;">捆包标签,1,有部品17MC,0,胶带数量,1</textarea>
            <br />
            <input type="button" value="3.业者模拟调用接口2" id="btnSv2" />
            <div id="rtv2"></div>
        </div>


        <div style="width: 1040px;" class="">

            <%--            <div class='title_div' style="height: 80px;">
                <table style="border: none; width: 1000px;">
                    <tr>
                        <td style="border: none; width: 250px; font-size: 50px;">检查明細</td>
                        <td style="border: none;"></td>
                    </tr>
                </table>


            </div>--%>

            <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>



            <!--条件部-->
            <table class='jyouken_panel' style="width: 1040px" cellpadding="0" cellspacing="0">
                <tr>
                                        <td rowspan="2">
                        <div id="barcodeSCMX"></div>
                    </td>
                    <td style="text-align: left;">
                        <div style="border: none; font-size: 32px;">
                            &nbsp;作番：<asp:Label ID="lblMake_no" runat="server" Text="001"></asp:Label>
                            &nbsp;CODE：<asp:Label ID="lblCode" runat="server" Text="001"></asp:Label>
                            &nbsp;数量：<asp:Label ID="lblSuu" runat="server" Text=""></asp:Label>
                            <br />
                            &nbsp;检查者：<asp:Label ID="lblUser" runat="server" Text="001"></asp:Label>
                            <asp:Label ID="lblUserName" runat="server" Text="001"></asp:Label>
                            &nbsp;（<asp:Label ID="lblLine_id" runat="server" Text="001"></asp:Label>）
                             &nbsp; 扫描:<asp:TextBox ID="tbxScanTxt" runat="server" typ="scan"></asp:TextBox>
                            <br />
                        </div>

                    </td>

                </tr>
                <tr>

                    <td style="text-align: right;">

                        <asp:Label ID="lblSou" runat="server" Text="" Font-Size="32px"></asp:Label></td>
                </tr>
            </table>
            <%--   <input type="text" id="SC" style="height:4px;width:4px; font-size:1px;" />--%>


            <asp:Panel ID="PanelLinks" runat="server" CssClass="chk_div_links"></asp:Panel>

            <!--明細Title部-->
            <div id="divGvwTitle" class='jq_title_div' runat="server" style="overflow: hidden; margin-left: 0px; width: 1040px; margin-top: 0px; border-collapse: collapse;">
                <table class='ms_title' style="width: 1040px" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 80px;">工程名
                        </td>
                        <td style="width: 100px;">检查项目
                        </td>
                        <td style="width: 30px;" title="图片标记">记
                        </td>
                        <td style="width: 160px;">检查方法
                        </td>
                        <td style="width: 180px;">基准説明
                        </td>
                        <td style="width: 200px;">输入值
                        </td>
                        <td style="width: 50px;">结果
                        </td>
                        <td style="">备考<input type="tel" id="SC" style="height: 1px; width: 1px; font-size: 1px; ime-mode: disabled;" tabindex="-1" />
                        </td>
                    </tr>
                </table>
            </div>

            <!--明細Body部-->
            <div id="divGvw" class='jq_ms_div' runat="server" style="overflow: auto; height: 830px; margin-left: 0px; width: 1064px; margin-top: 0px; border-collapse: collapse;">

                <asp:GridView CssClass="jq_ms" Width="1040px" runat="server" ID="gvMs" EnableTheming="True" ShowHeader="False" AutoGenerateColumns="False" BorderColor="black" Style="margin-top: -1px;" TabIndex="-1">
                    <Columns>
                        <asp:BoundField DataField="project_name" HeaderText="" ControlStyle-Width="80px" ItemStyle-Width="80px" />
                        <asp:BoundField DataField="chk_km_name" HeaderText="" ItemStyle-Width="100px" />
                        <asp:TemplateField>
                            <ItemTemplate><%#Eval("pic_sign")%></ItemTemplate>
                            <ItemStyle Width="30px" HorizontalAlign="Left" CssClass="jq_chk_flg" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate><%#Eval("chk_name")%></ItemTemplate>
                            <ItemStyle Width="160px" HorizontalAlign="Left" CssClass="jq_in_1" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate><%#Eval("kj_explain_Expr")%></ItemTemplate>
                            <ItemStyle Width="180px" HorizontalAlign="Left" CssClass="jq_in_2" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:TextBox ID="tbxIn1" runat="server" Text='<%#Eval("in_1")%>' CssClass="jq_in1" Width="98%" AutoCompleteType="Disabled"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle Width="200px" HorizontalAlign="Left" CssClass="jq_chk_result" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:TextBox ID="tbxresult" runat="server" Text='<%#Eval("chk_result")%>' CssClass="jq_result" BorderStyle="None" TabIndex="-1" Width="92%"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle Width="50px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:TextBox ID="tbxMark" runat="server" Text='<%#Eval("mark")%>' CssClass="jq_mark" Width="98%" AutoCompleteType="Disabled"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" CssClass="jq_kj_0" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div style="height: 1px;">


                <asp:TextBox ID="hidChkNo" runat="server" class="jq_chk_no_ipt" Style="visibility: hidden;"></asp:TextBox>
                <asp:TextBox ID="hidChkMethodId" runat="server" class="jq_chk_method_id_ipt" Style="visibility: hidden;"></asp:TextBox>
                <asp:TextBox ID="hidChkFlg" runat="server" class="jq_chk_flg_ipt" Style="visibility: hidden;"></asp:TextBox>
                <asp:TextBox ID="hidIn1" runat="server" class="jq_in_1_ipt" Style="visibility: hidden;"></asp:TextBox>
                <asp:TextBox ID="hidIn2" runat="server" class="jq_in_2_ipt" Style="visibility: hidden;"></asp:TextBox>
                <asp:TextBox ID="hidChkResult" runat="server" class="jq_chk_result_ipt" Style="visibility: hidden;"></asp:TextBox>
                <asp:TextBox ID="hidMark" runat="server" class="jq_mark_ipt" Style="visibility: hidden;"></asp:TextBox>
                <asp:TextBox ID="hidKj0" runat="server" class="jq_kj_0_ipt" Style="visibility: hidden;"></asp:TextBox>
                <asp:TextBox ID="hidKj1" runat="server" class="jq_kj_1_ipt" Style="visibility: hidden;"></asp:TextBox>
                <asp:TextBox ID="hidKj2" runat="server" class="jq_kj_2_ipt" Style="visibility: hidden;"></asp:TextBox>
                <asp:TextBox ID="hidKjExplain" runat="server" class="jq_kj_explain_ipt" Style="visibility: hidden;"></asp:TextBox>
                <asp:TextBox ID="hidInsDate" runat="server" class="jq_ins_date_ipt" Style="visibility: hidden;"></asp:TextBox>
                <asp:TextBox ID="hidOldRowIdx" runat="server" class="jq_hidOldRowIdx" Style="visibility: hidden;"></asp:TextBox>

                <asp:TextBox ID="hidLineId" runat="server" class="jq_chk_no_ipt" Style="visibility: hidden;"></asp:TextBox>
                <asp:TextBox ID="hidInsUser" runat="server" class="jq_ins_user_ipt" Style="visibility: hidden;"></asp:TextBox>
                <asp:TextBox ID="hidLineIdKey" runat="server" class="jq_ins_user_ipt" Style="visibility: hidden;"></asp:TextBox>
                <asp:TextBox ID="hidPlanLineId" runat="server" class="jq_chk_no_ipt" Text="SRM1532B" Style="visibility: hidden;"></asp:TextBox>
                <asp:TextBox ID="hidBaogong" runat="server" class="jq_chk_id_ipt" Style="visibility: hidden;"></asp:TextBox>
                <asp:TextBox ID="hidTuopanLines" runat="server" class="jq_chk_id_ipt" Style="visibility: hidden;"></asp:TextBox>
            </div>
            <div class="">


                <table class="jq_kbtbl" style="">
                    <tr>
                        <td style="width: 752px">
                            <img id="gvMs_ctl02_imgLook" class="JQ_IMG" src="" style="border-width: 0px; width: 750px;" />
                        </td>
                        <td class="jq_kbtd" style="width: 250px; vertical-align: top; text-align: center;">


                            <table class="keyboard">
                                <tr class="kbsuu">
                                    <td style="width: 60px;">
                                        <input type="button" class="jq_suubtn" value="7" style="width: 60px; height: 65px;" /></td>
                                    <td style="width: 60px;">
                                        <input type="button" class="jq_suubtn" value="8" style="width: 60px; height: 65px;" /></td>
                                    <td style="width: 60px;">
                                        <input type="button" class="jq_suubtn" value="9" style="width: 60px; height: 65px;" /></td>
                                    <td rowspan="2">
                                        <input type="button" class="jq_suubtncz" value="删除" style="width: 60px; height: 144px" /></td>
                                </tr>
                                <tr class="kbsuu">
                                    <td>
                                        <input type="button" class="jq_suubtn" value="4" style="width: 60px; height: 65px;" /></td>
                                    <td>
                                        <input type="button" class="jq_suubtn" value="5" style="width: 60px; height: 65px;" /></td>
                                    <td>
                                        <input type="button" class="jq_suubtn" value="6" style="width: 60px; height: 65px;" /></td>
                                </tr>
                                <tr class="kbsuu">
                                    <td>
                                        <input type="button" class="jq_suubtn" value="1" style="width: 60px; height: 65px;" /></td>
                                    <td>
                                        <input type="button" class="jq_suubtn" value="2" style="width: 60px; height: 65px;" /></td>
                                    <td>
                                        <input type="button" class="jq_suubtn" value="3" style="width: 60px; height: 65px;" /></td>
                                    <td rowspan="2">
                                        <input type="button" class="jq_suubtncz" value="回车" style="width: 60px; height: 134px;" /></td>
                                </tr>
                                <tr class="kbsuu">
                                    <td colspan="2">
                                        <input type="button" class="jq_suubtn0" value="0" style="width: 120px; height: 65px;" /></td>
                                    <td>
                                        <input type="button" class="jq_suubtn" value="." style="width: 60px; height: 65px;" /></td>
                                </tr>
                                <tr class="jq_hantei_btn">
                                    <td colspan="2">
                                        <input type="button" class="jq_okng" value="NG" style="width: 120px; height: 240px;" /></td>
                                    <td colspan="2">
                                        <input type="button" class="jq_okng" value="OK" style="width: 120px; height: 240px;" /></td>
                                </tr>
                                <tr class="autocolor">
                                    <td colspan="4">
                                        <input type="button" class="jq_color" value="自动颜色" style="width: 240px; height: 240px;" />

                                    </td>
                                </tr>
                                <tr class="kamera">
                                    <td colspan="4">
                                        <%--                         <input type="button"  style=" width:246px; height:120px;"  value="照片" onclick="$('#kameira').show(); InitKamera()" />--%>

                                    </td>
                                </tr>
                            </table>


                        </td>
                    </tr>
                </table>


                <div class="bottom_div">
                    <asp:Button ID="btnComplete" runat="server" Text="完了" Style="display: none;" OnClick="btnComplete_Click" />
                    <input type="button" id="btnHtmlComplete" value="完了" />
                    <asp:Button ID="btnModoru" runat="server" Text="返回" Style="display: none;" OnClick="btnModoru_Click" />
                    <input type="button" value="返回" onclick="BackPage();" />
                    <input id="btnNext" type="button" value="下一个" />
                    <input id="btnPre" type="button" value="上一个" />
                </div>
                <br />
                <br />
                <div id="kameira" style="width: 1010px; z-index: 10000; top: 1px; left: 1px;">
                    <%--            <input type="button" value="关闭" onclick="$('#kameira').hide()" />
            
                    <button id="snap" onclick="snap()">拍照</button>
                    --%>

                    <%-- <a href="#" onclick="$('#kameira').hide()"  style="font-size:35px;">关闭</a>--%>


                    <a style="font-size: 35px;">照片一览 <input type="button" value="刷新" onclick="ImgsInit()" style="width:200px" /></a>

                    <table style="display: none;">
                        <tr>
                            <td>
                                <video id="video" width="10" height="10" autoplay></video>
                            </td>

                            <td style="width: 180px;">
                                <%--<a href="#" onclick="snap()" style="font-size:35px;">拍照</a>--%>
                            </td>
                            <td>

                                <canvas id="canvas" width="800" height="600"></canvas>
                            </td>
                        </tr>
                    </table>
                    <hr />

                    <div id="div_imgs" style="padding: 4px 4px 4px 4px;">
                    </div>

                </div>



                <script type="text/javascript">

                    /*
                                        function Complete(){
                    
                                            if (camera_flg == "1") {
                                                if($(".item_1").length==0){
                                                    alert("请注意没有拍照，不能设置成OK！！！");
                                                }
                                            }
                                            $("#div_imgs").html("");
                                            setTimeout(function () { $('#btnComplete').click() }, 300);
                        
                                        }
                    */
                    function BackPage(){
                        $("#div_imgs").html("");
                        setTimeout(function () { $('#btnModoru').click() }, 300);

                    }

                    var canvas = document.getElementById("canvas");
                    var context = canvas.getContext("2d");
                    var video = document.getElementById("video");

                    var kameraFlg;
                    kameraFlg = false;

                    function InitKamera() {
                        if (kameraFlg == false) {
                            videoObj = {
                                "video": true
                            };
                            var errBack = function (error) {
                                console.log("Video capture error: ", error.code);
                            };
                            if (navigator.getUserMedia) { // Standard
                                navigator.getUserMedia(videoObj, function (stream) {
                                    video.srcObject = stream;
                                    video.play();
                                }, errBack);
                            } else if (navigator.webkitGetUserMedia) { // WebKit-prefixed
                                navigator.webkitGetUserMedia(videoObj, function (stream) {
                                    video.src = window.webkitURL.createObjectURL(stream);
                                    video.play();
                                }, errBack);
                            } else if (navigator.mozGetUserMedia) { // Firefox-prefixed
                                navigator.mozGetUserMedia(videoObj, function (stream) {
                                    video.src = window.URL.createObjectURL(stream);
                                    video.play();
                                }, errBack);
                            };

                    
                            kameraFlg = true;
                        }
                        ImgsInit();

                    }

                    $(document).ready(function () {



                        if (camera_flg == "0") {
                            $(".kamera").hide();
                        } else {
                            $(".kamera").show();
                            InitKamera();
                        }

                    });

                    function ImgsInit() {
                        $.ajax({
                            type: 'POST',               
                            url: 'AJAX.aspx?kbn=get_chk_imgs',
                            async: true, //true:yibu                    
                            data: {
                                chkNo_key: $("#hidChkNo").val(),
                                line_id: $("#hidLineId").val(),
                                chk_method_id: $("#hidChkMethodId").val()
                            },
                            datatype: 'html', //'xml', 'html', 'script', 'json', 'jsonp', 'text'.
                            beforeSend: function () { },
                            //when success
                            success: function (data) {
                                //alert(data);
                                var shtml = "";
                                var arr;
                                arr = data.split(",");
                                var i;
                                if (data != '') {
                                    for (i = 0; i <= arr.length - 1; i++) {
                                        shtml += '<li class="item_1"><img src="AJAX.aspx?kbn=show_chk_img&line_id=' + $("#hidLineId").val() + '&chkNo_key=' + $("#hidChkNo").val() + '&img_name=' + arr[i] + '" alt="">';
                                        shtml += ' <a href="#" onclick="DeleteImg(\'' + arr[i].replace(/\\/g, "\\\\") + '\')"  style="font-size:35px">↑删除↑</a> </li><hr><br>';
                                    }
                                }
                                $("#div_imgs").html(shtml);

                            },
                            //when complete
                            complete: function (XMLHttpRequest, textStatus) {
                            },
                            //when error
                            error: function (e) { 
                                alert('图片取得错误'+e.responseText); 
                
                            }
                        });
                    }
                    function DeleteImg(img_name) {
                        $.ajax({
                            type: 'POST',
                            url: 'AJAX.aspx?kbn=DeleteImg',
                            async: true, //true:yibu
                            data: {
                                chkNo_key: $("#hidChkNo").val(),
                                line_id: $("#hidLineId").val(),                        
                                chk_method_id: $("#hidChkMethodId").val(),
                                img_name: img_name

                            },
                            datatype: 'html', //'xml', 'html', 'script', 'json', 'jsonp', 'text'.
                            beforeSend: function () { },
                            //when success
                            success: function (data) {
                                ImgsInit();
                            },
                            //when complete
                            complete: function (XMLHttpRequest, textStatus) {
                            },
                            //when error
                            error: function () { alert('图片删除错误'); }
                        });
                    }


                    function snap() {
                        context.drawImage(video, 0, 0, 800, 600);
                        var img = canvas.toDataURL("image/jpeg", 0.92);
                        $.ajax({
                            type: 'POST',
                            url: 'AJAX.aspx?kbn=upd_img',
                            async: true, //true:yibu
                            data: {
                                chkNo_key: $("#hidChkNo").val(),
                                line_id: $("#hidLineId").val(),
                                chk_method_id: $("#hidChkMethodId").val(),
                                img: img.replace(/^data:image\/(png|jpg|jpeg);base64,/, "")
                        
                            },
                            datatype: 'html', //'xml', 'html', 'script', 'json', 'jsonp', 'text'.
                            beforeSend: function () { },
                            //when success
                            success: function (data) {
                                ImgsInit();
                            },
                            //when complete
                            complete: function (XMLHttpRequest, textStatus) {
                            },
                            //when error
                            error: function () { alert('图片上传错误'); }
                        });
                        return false;
                    }




                </script>



                <asp:HiddenField ID="tbxCode_key" runat="server" />
                <asp:HiddenField ID="tbxMakeNo_key" runat="server" />
                <asp:Button ID="btnSinki" runat="server" Text="Button" OnClick="btnSinki_Click" />
                <asp:HiddenField ID="camera_flg" runat="server" />
                <asp:HiddenField ID="autoColor_flg" runat="server" />
                <asp:HiddenField ID="hidYXLD_START_TIME" runat="server" />

            </div>
        </div>
    </form>
</body>
</html>
