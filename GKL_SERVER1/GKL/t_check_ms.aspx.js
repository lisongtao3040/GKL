
//chk_name 左右 ，要包含左右的文字
//chk_method l,r

String.prototype.PadLeft = function (len, charStr) {
    var s = this + '';
    return new Array(len - s.length + 1).join(charStr || '') + s;
}
String.prototype.PadRight = function (len, charStr) {
    var s = this + '';
    return s + new Array(len - s.length + 1).join(charStr || '');
}

// 使用方式
//loadCrossDomainUrl('fraYXLD', 'http://10.160.204.135:5001/api/code?code=00901907570602WA-5010-MBJB');
// 最终使用函数
function loadUrlSafe(frameId, url) {
    var rtv = WebMethodToStr("api", "PostURL",
        JSON.stringify({
            url: url
        }
    ));
}



var pub_chk_no;
var pub_user;
var pub_line_id;
var sanp_chk_method_id;


var chk_id;
var kj_0;
var kj_1;
var kj_2;
var chk_method_id;
var chk_method;
var chk_formula;
var pic_old_id;

//返回字符串  
function returnString() {
    return sanp_chk_method_id;
}
$(document).ready(function () {

    PUB_YXLD_URL = GetYXLD_URL($("#lblLine_id").text().trim());

    try {
        window.external.SetSnapTrue();
    } catch (e) {

    }

    pub_chk_no = $("#hidChkNo").val();
    pub_user = $("#hidInsUser").val();
    pub_line_id = $("#hidLineId").val();


    document.getElementById("barcodeSCMX").innerHTML = "";

    try {
        //scmx
        var scmxTxt = $.ajax({
            url: "AJAX.aspx?a=" + new Date()
                + "&kbn=scmx"
                + "&cd=" + $("#lblCode").text()
                + "&no=" + $("#lblMake_no").text()
                + "&line_id=" + $("#lblLine_id").text()
                ,
            async: false
        });



        //alert(scmxTxt);

        var qrcode = new QRCode(document.getElementById("barcodeSCMX"), {
            text: scmxTxt.responseText,
            width: 128,
            height: 128,
            colorDark: "#000000",
            colorLight: "#ffffff",
            correctLevel: QRCode.CorrectLevel.H
        });
    } catch (e1) { }







    pic_old_id = "";

    var pic_id;
    var acText;

    var resultTxt;
    var resultCell;
    var thisRow;

    var SC;
    SC = document.getElementById("SC");

    var pub_picture_jq;
    pub_picture_jq = $(".JQ_IMG");

    var pub_select_row;
    var acIn1;
    var acResult;
    var acMark;

    var pub_jq_all_value1 = $(".jq_in1");
    var DW, DH;

    //Init select row's value
    function InitRowValue(inputObj) {

        SetAllSelectValueBlur();
        //TextBlurStyle();

        thisRow = $(inputObj).parent().parent();
        acText = $(inputObj);
        chk_id = thisRow.attr("chk_id");
        kj_0 = thisRow.attr("kj_0");
        kj_1 = thisRow.attr("kj_1");
        kj_2 = thisRow.attr("kj_2");
        chk_method_id = thisRow.attr("chk_method_id");
        sanp_chk_method_id = chk_method_id;
        chk_method = thisRow.attr("chk_method");
        chk_formula = thisRow.attr("chk_formula");
        DW = thisRow.attr("DW");
        DH = thisRow.attr("DH");
        pic_id = thisRow.attr("pic_id");
        acIn1 = thisRow.find(".jq_in1");
        acResult = thisRow.find(".jq_result");
        acMark = thisRow.find(".jq_mark");

        $("#hidChkMethodId").val(chk_method_id);

        //alert(kj_0);
        //if (pic_old_id != pic_id) {
        if (pic_id == "") {
            pub_picture_jq.hide();
            pic_old_id = pic_id;
        } else {
            if (pic_old_id != pic_id) {
                pub_picture_jq.show(300);
                pub_picture_jq.attr("src", "Img.aspx?pic_id=" + pic_id + "&line_id=" + $("#hidLineIdKey").val());
                pic_old_id = pic_id;
            }
        }

        //}

    }

    function SetAllSelectValueBlur() {
        pub_jq_all_value1.css('border-color', '#000');
        pub_jq_all_value1.css('border-width', '1px');
    }

    //统计检查明细结果
    check_setume_txt();


    //行设置
    $(".jq_result").attr("readonly", "readonly");
    $(".jq_ms tr").each(function () {

        chk_method = $(this).attr("chk_method");

        if (chk_method == "1") {        //SCAN
            $(this).find(".jq_in1").css('background-color', '#ffff66');
            $(this).find(".jq_in1").attr("readonly", "readonly");
        } else if (chk_method == "2") { //固定
            $(this).find(".jq_in1").css('background-color', '#CCC');
            $(this).find(".jq_in1").attr("readonly", "readonly");
            $(this)[0].readOnly = true;
        }

        resultCell = $(this).find(".jq_result");
        resultTxt = resultCell.val();

        if (resultTxt == "OK") {
            resultCell.css('background-color', 'green');
        } else if (resultTxt == "NG") {
            resultCell.css('background-color', 'red');
        }

    });

    //图片操作
    $(".JQ_IMG").mousedown(function () {
        $(this).css('width', '100%');
    });

    $(".JQ_IMG").mouseup(function () {
        $(this).css('width', '');
    });

    var kbChkKbn;
    kbChkKbn = false;
    var oldIn1Value;
    oldIn1Value = "";

    var autoColor = false;

    //入力値１
    $(".jq_in1").focus(function () {

        InitRowValue(this);
        TextFocusStyle($(this));
        oldIn1Value = $(this).val();
        autoColor = false;

        $(".autocolor").hide();
        var tmp_thisRow = $(this).parent().parent();

        //影像面板隐藏
        $(".divYXLD").hide();
        //颜色检查 不扫码
        if (tmp_thisRow.attr("chk_formula") == "{color}" && $("#autoColor_flg").val() == "1") {
            $(".kbsuu").hide();
            $(".jq_hantei_btn").show();
            $(".autocolor").show();
            autoColor = true;
        } else if (chk_method == "2") { // OK/NG button 可用 不可用
            //$(".jq_hantei_btn").removeAttr('disabled');
            $(".kbsuu").hide();
            $(".jq_hantei_btn").show();
        } else if (chk_method == "l" || chk_method == "r") {//影像联动
            $(".kbsuu").hide();
            $(".jq_hantei_btn").hide();


            if (chk_method == 'r') {
                //如果前一行也就是左  是OK，那么打开影像面板，  否则不打开
                if ($(this).parent().parent().prev().find(".jq_result").val() == "OK") {
                    $(".divYXLD").show();
                } else {
                    $(".divYXLD").hide();
                }
            } else {
                $(".divYXLD").show();
            }





        } else {
            //$(".jq_hantei_btn").attr('disabled', ' true');
            $(".kbsuu").show();
            $(".jq_hantei_btn").hide();
        }


    });

    var pub_old_blur_obj;
    var keyUpFlg = false;

    $(".jq_in1").blur(function () {

        keyUpFlg = false;

        pub_old_blur_obj = $(this);

        var tmp_thisRow = $(this).parent().parent();
        var tmp_chk_method = tmp_thisRow.attr("chk_method");

        if (tmp_chk_method == "0") {
            setTimeout(function () {
                if (kbChkKbn == false) {
                    if (GetChkMethodStr(pub_old_blur_obj)) {
                        SetResult(true, pub_old_blur_obj);
                    } else {
                        SetResult(false, pub_old_blur_obj);
                    }
                }
                kbChkKbn = false;
            }, 200);
        }
    });




    $(".jq_in1").keydown(function (e) {

        if (chk_method == "0") { //INPUT
            if (event.keyCode == 13) {

                if (GetChkMethodStr($(this))) {
                    SetResult(true, $(this));
                    SetNextFocus(acIn1);
                } else {
                    SetResult(false, $(this));
                    SetNextFocus(acIn1);
                }

            }
        } else if (chk_method == "1") { //SCAN
            //if (event.keyCode == 119) { //F8
            //    SC.value = "";
            //    SC.focus();
            //    acText = $(this);

            //    e.preventDefault();
            //    return false;
            //}
            if (event.keyCode == 119) { //F8
                $(this).removeAttr('readonly');
            }

            if (!keyUpFlg) {
                $(this).removeAttr('readonly');
                $(this)[0].select();
                keyUpFlg = true;
            }

            if (event.keyCode == 13) {  //Enter

                $(this).attr("readonly", true);
                keyUpFlg = false;

                //颜色检查 不扫码
                //if (tmp_thisRow.attr("chk_formula") == "{color}" && $("#autoColor_flg").val() == "1") {
                //    SetResult(true, $(this));
                //    SetNextFocus(acIn1);
                //} else
                if (GetChkMethodStr($(this))) {
                    SetResult(true, $(this));
                    SetNextFocus(acIn1);
                } else {
                    SetResult(false, $(this));
                    SetNextFocus(acIn1);
                }

                e.preventDefault();
                return false;
            }

        }
    });


    $(SC).keydown(function (e) {

        if (event.keyCode == 13) {
            try {
                acText.val(SC.value);
                if (GetChkMethodStr($(acIn1))) {
                    SetResult(true, acIn1);
                    SetNextFocus(acIn1);
                } else {
                    SetResult(false, acIn1);
                    SetNextFocus(acIn1);
                }
                SC.value = "";
            } catch (e) {

            }

            e.preventDefault();
            return false;
            //event.keyCode = 0;
            //return false;
        }
    });



    //备考
    $(".jq_mark").focus(function () {
        InitRowValue(this);
        TextFocusStyle($(this));
    });
    $(".jq_mark").blur(function () {
        TextBlurStyle($(this));



        //AjaxPostMsUpd(thisRow.find(".jq_in1").val()
        //    , thisRow.find(".jq_result").val()
        //    , thisRow.find(".jq_mark").val());

        var tmp_thisRow = $(this).parent().parent();
        var tmp_acIn1 = tmp_thisRow.find(".jq_in1");
        var tmp_acResult = tmp_thisRow.find(".jq_result");

        AjaxPostMsUpd(tmp_acIn1, $(tmp_acResult).val());
    });

    function TextBlurStyle(jq_e) {
        jq_e.css('border-color', '#333');
        jq_e.css('border-width', '1px');
    }

    //小键盘
    $(".keyboard").find("input").click(function (e) {

        kbChkKbn = true;

        var ky = $(this).val();
        var that = this;

        if (ky == "OK" || ky == "NG") {
            //if (autoColor == true) {

            //    var rtv = AjaxPostColorCopyLast($("#lblMake_no").text(), $("#lblUser").text());
            //    if (rtv) {
            //        ky = "OK";
            //    } else {
            //        ky = "NG";
            //    }
            //    if (ky == "OK") {
            //        acResult.css('background-color', 'green');
            //    } else {
            //        acResult.css('background-color', 'red');
            //    }
            //    acResult.val(ky);
            //    //AjaxPostMsUpd(acIn1.val(), ky, acMark.val());
            //    AjaxPostMsUpd(acIn1, ky);
            //    SetNextFocus(acIn1);

            //} else {
            //    if (ky == "OK") {
            //        acResult.css('background-color', 'green');
            //    } else {
            //        acResult.css('background-color', 'red');
            //    }
            //    acResult.val(ky);
            //    //AjaxPostMsUpd(acIn1.val(), ky, acMark.val());
            //    AjaxPostMsUpd(acIn1, ky);
            //    SetNextFocus(acIn1);
            //}
            if (ky == "OK") {
                acResult.css('background-color', 'green');
            } else {
                acResult.css('background-color', 'red');
            }
            acResult.val(ky);
            //AjaxPostMsUpd(acIn1.val(), ky, acMark.val());
            AjaxPostMsUpd(acIn1, ky);
            SetNextFocus(acIn1);


        } else if (ky == "回车") {
            if (chk_method != "2") {
                if (GetChkMethodStr(acIn1)) {
                    SetResult(true, acIn1);
                    SetNextFocus(acIn1);
                } else {
                    SetResult(false, acIn1);
                    SetNextFocus(acIn1);
                }
            }

        } else if (ky == "删除") {
            $(acText).val("");
            //4、输入错误删除时全部删除，是否可以改成删除一位；
            ////扫描 固定 全删
            //if (chk_method == "1" || chk_method == "2") {
            //    $(acText).val("");
            //} else {
            //    //可输入时删最后一个
            //    if ($(acText).val().length >= 1) {
            //        $(acText).val($(acText).val().substring(0, $(acText).val().length - 1));
            //    }
            //}
            // }
            $(acText).focus();
        } else if (ky == "影像联动检查") {
            $("#cover").height($(window).height());
            $("#cover").text("接收中......");
            $("#cover").show();
            $(this).val("影像联动检查中...");
            setTimeout(function () { YXLD(acIn1, that, DH, DW); }, 100);

        } else if (ky == "影像联动检查中...") {
            return;




        } else if (ky == "照片") {


        } else if (ky == "自动颜色") {
            var rtv = AjaxPostColorCopyLast($("#lblMake_no").text(), $("#lblUser").text());
            if (rtv) {
                ky = "OK";
            } else {
                ky = "NG";
            }
            if (ky == "OK") {
                acResult.css('background-color', 'green');
            } else {
                acResult.css('background-color', 'red');
            }
            acResult.val(ky);
            //AjaxPostMsUpd(acIn1.val(), ky, acMark.val());
            AjaxPostMsUpd(acIn1, ky);
            SetNextFocus(acIn1);

        }
        else {
            if (chk_method == "0") {
                $(acText).val($(acText).val() + ky + '');
            }
            $(acText).focus();

        }
    });


    function SetResult(rlt, jq_e) {

        var tmp_thisRow = $(jq_e).parent().parent();
        var tmp_acResult = tmp_thisRow.find(".jq_result");
        //acIn1 = thisRow.find(".jq_in1");
        //acResult = thisRow.find(".jq_result");
        //acMark = thisRow.find(".jq_mark");

        if (rlt) {
            tmp_acResult.css('background-color', 'green');
            tmp_acResult.val("OK");
            //AjaxPostMsUpd(jq_e.val(), "OK", acMark.val());

            AjaxPostMsUpd(jq_e, "OK");

        } else {
            tmp_acResult.css('background-color', 'red');
            tmp_acResult.val("NG");
            //AjaxPostMsUpd(jq_e.val(), "NG", acMark.val());
            AjaxPostMsUpd(jq_e, "NG");

        }


        //alert();
    }

    $("#btnNext").mouseup(function () {
        SetNextFocus(acIn1);
    });
    $("#btnPre").mouseup(function () {
        SetPreFocus(acIn1);
    });

    //选择下一行
    function SetNextFocus(jq_e) {
        if (thisRow.next().length > 0) {
            thisRow = thisRow.next();
            InitRowValue(thisRow.find(".jq_in1")[0]);
        }

        try {
            acIn1.focus();
        } catch (e) {
        }
    }
    function SetPreFocus(jq_e) {
        if (thisRow.prev().length > 0) {
            thisRow = thisRow.prev();
            InitRowValue(thisRow.find(".jq_in1")[0]);
        }

        try {
            acIn1.focus();
        } catch (e) {
        }
    }

    function GetChkMethodStr(jq_e) {

        var tmp_thisRow = $(jq_e).parent().parent();

        var tmp_chk_id = tmp_thisRow.attr("chk_id");
        var tmp_kj_0 = tmp_thisRow.attr("kj_0");
        var tmp_kj_1 = tmp_thisRow.attr("kj_1");
        var tmp_kj_2 = tmp_thisRow.attr("kj_2");
        var tmp_chk_method_id = tmp_thisRow.attr("chk_method_id");
        var tmp_chk_method = tmp_thisRow.attr("chk_method");
        var tmp_chk_formula = tmp_thisRow.attr("chk_formula");
        var tmp_pic_id = tmp_thisRow.attr("pic_id");
        var tmp_acIn1 = tmp_thisRow.find(".jq_in1");
        var tmp_acResult = tmp_thisRow.find(".jq_result");
        var tmp_acMark = tmp_thisRow.find(".jq_mark");

        var str;
        var value;
        value = jq_e.val();
        str = tmp_chk_formula;
        str = str.replace("{in1}", value).replace("{in1}", value).replace("{in1}", value).replace("{in1}", value).replace("{in1}", value);
        str = str.replace("{kj0}", tmp_kj_0).replace("{kj0}", tmp_kj_0).replace("{kj0}", tmp_kj_0).replace("{kj0}", tmp_kj_0).replace("{kj0}", tmp_kj_0);
        str = str.replace("{kj1}", tmp_kj_1).replace("{kj1}", tmp_kj_1).replace("{kj1}", tmp_kj_1).replace("{kj1}", tmp_kj_1).replace("{kj1}", tmp_kj_1);
        str = str.replace("{kj2}", tmp_kj_2).replace("{kj2}", tmp_kj_2).replace("{kj2}", tmp_kj_2).replace("{kj2}", tmp_kj_2).replace("{kj2}", tmp_kj_2);
		str = str.replace("{code}", $("#lblCode").text());

        try {

            if (tmp_chk_formula == "{color}") {

                var code;
                code = $("#lblCode").text();

                return AjaxPostChkColor(code, value, $("#lblLine_id").text());

                //return true;
            } else if (tmp_chk_formula == "{biaoqianyizhi}") {
                jq_e.val(value.substring(0, 16).trim());
                return (value.substring(0, 16).trim() == $("#lblCode").text());

            }

            if (eval(str)) {
                return true;
            } else {
                return false;
            }
        } catch (e) {
            //alert(e.message);
            return false;
        }
    }


    function ChkInput(jq_e) {
        var value;
        value = jq_e.val();


    }

    function TextFocusStyle(jq_e) {
        jq_e.css('border-color', 'red');
        jq_e.css('border-width', '2px');
    }



    //统计检查明细结果
    function check_setume_txt() {
        var ngSuu = $(".jq_result:[value='NG']").length;
        var okSuu = $(".jq_result:[value='OK']").length;
        var nullSuu = $(".jq_result:[value='']").length;

        $("#lblSou").text("【NG】:" + ngSuu + " --【OK】:" + okSuu + " --【未チェック】:" + nullSuu + " --【全部】:" + $(".jq_result").length);

        if (okSuu == $(".jq_result").length) {
            $("#lblSou").css('color', 'green');
        } else {
            $("#lblSou").css('color', 'red');
        }
    }

    //更新检查明细
    function AjaxPostMsUpd(inObj, rlt_txt) {

        var tmp_thisRow = $(inObj).parent().parent();
        var tmp_chk_id = tmp_thisRow.attr("chk_id");
        var tmp_kj_0 = tmp_thisRow.attr("kj_0");
        var tmp_kj_1 = tmp_thisRow.attr("kj_1");
        var tmp_kj_2 = tmp_thisRow.attr("kj_2");
        var tmp_chk_method_id = tmp_thisRow.attr("chk_method_id");
        var tmp_chk_method = tmp_thisRow.attr("chk_method");
        var tmp_chk_formula = tmp_thisRow.attr("chk_formula");
        var tmp_pic_id = tmp_thisRow.attr("pic_id");
        var tmp_acIn1 = tmp_thisRow.find(".jq_in1");
        var tmp_acResult = tmp_thisRow.find(".jq_result");
        var tmp_acMark = tmp_thisRow.find(".jq_mark");



        $("#btnComplete").attr("disabled", true);
        $("#btnModoru").attr("disabled", true);
        $.ajax({
            type: 'POST',
            url: 'AJAX.aspx?kbn=chk_ms_upd',
            async: true, //true:yibu
            data: {
                chkNo_key: pub_chk_no,
                in1: $(tmp_acIn1).val(),
                chkResult: rlt_txt,
                mark: $(tmp_acMark).val(),
                kj0: tmp_kj_0,
                kj1: tmp_kj_1,
                kj2: tmp_kj_2,
                insUser: pub_user,
                line_id: pub_line_id,
                chk_method_id: tmp_chk_method_id
            },
            datatype: 'html', //'xml', 'html', 'script', 'json', 'jsonp', 'text'.
            beforeSend: function () { },
            //when success
            success: function (data) {
                check_setume_txt();
                $("#btnComplete").removeAttr("disabled");
                $("#btnModoru").removeAttr("disabled");
            },
            //when complete
            complete: function (XMLHttpRequest, textStatus) {
            },
            //when error
            error: function () { alert('明细更新错误'); }
        });
    }


    //COLOR检查明细
    function AjaxPostChkColor(goodcd, toolcd, linecd) {
        $("#btnComplete").attr("disabled", true);
        $("#btnModoru").attr("disabled", true);
        var rtv;
        rtv = false;
        $.ajax({
            type: 'POST',
            url: 'AJAX.aspx?kbn=chk_color',
            async: false, //true:yibu
            data: {
                goodcd: goodcd,
                toolcd: toolcd,
                linecd: linecd
            },
            datatype: 'html', //'xml', 'html', 'script', 'json', 'jsonp', 'text'.
            beforeSend: function () { },
            //when success
            success: function (data) {
                check_setume_txt();
                $("#btnComplete").removeAttr("disabled");
                $("#btnModoru").removeAttr("disabled");

            },
            //when complete
            complete: function (XMLHttpRequest, textStatus) {
                if (XMLHttpRequest.responseText == "true") {
                    rtv = true;
                    return true;
                }
                if (XMLHttpRequest.responseText == "true" || XMLHttpRequest.responseText == "false") {

                } else {
                    alert(XMLHttpRequest.responseText);
                }

            },
            //when error
            error: function () { alert('颜色检查错误'); }
        });

        return rtv;
    }


    //COLOR检查明细
    function AjaxPostColorCopyLast(make_no, user) {
        $("#btnComplete").attr("disabled", true);
        $("#btnModoru").attr("disabled", true);
        var rtv;
        rtv = false;
        $.ajax({
            type: 'POST',
            url: 'AJAX.aspx?kbn=chk_color_copy_last',
            async: false, //true:yibu
            data: {
                make_no: make_no,
                goodcd: $("#lblCode").text(),
                linecd: $("#lblLine_id").text(),
                user: user
            },
            datatype: 'html', //'xml', 'html', 'script', 'json', 'jsonp', 'text'.
            beforeSend: function () { },
            //when success
            success: function (data) {

                if (data == "OK") {
                    rtv = true;
                } else {
                    alert(data);
                }

                check_setume_txt();
                $("#btnComplete").removeAttr("disabled");
                $("#btnModoru").removeAttr("disabled");

            },
            //when complete
            complete: function (XMLHttpRequest, textStatus) {
                if (XMLHttpRequest.responseText == "true") {
                    rtv = true;
                    return true;
                }
                if (XMLHttpRequest.responseText == "true" || XMLHttpRequest.responseText == "false") {

                } else {
                    alert(XMLHttpRequest.responseText);
                }

            },
            //when error
            error: function () { alert('颜色检查错误'); }
        });

        return rtv;
    }


    $(document).keydown(function () {
        if (event.keyCode == 13) {
            event.keyCode = 10;
            return false;
        }
    });


    $(".jq_in1").each(function () {
        $(this)[0].focus();
        return false;
    });

    $("#tbxScanTxt").focus(function (e) {
        $(this).val("");
    });

    $("#tbxScanTxt").keydown(function (e) {
        if (e.keyCode == 13) {
            ScanBarCode(this);
            e.preventDefault();
            return false;
        }
    });


    //扫描现品票
    function ScanBarCode(e) {

        var cd = $(e).val();
        var make_no;
        var good_cd;
        if (cd.split("/").length == 8) {
            make_no = cd.split("/")[7].trim();//.replace(/-/g, "");
            good_cd = cd.split("/")[1].trim();//.replace(/-/g, "");
            $("#tbxCode_key").val(good_cd);
            $("#tbxMakeNo_key").val(make_no);
            $("#btnSinki")[0].click();
        }
    }

    $("#btnSinki").hide();


    var bdWidth = $(document.body).width();


    $(".jq_kbtbl").width(bdWidth);
    $(".jq_suubtn").width((bdWidth - 750) / 4);
    $(".jq_suubtn0").width((bdWidth - 750) / 2);
    $(".jq_suubtncz").width((bdWidth - 750) / 4);
    $(".jq_okng").width((bdWidth - 750) / 2);


    $("#gvMs_ctl02_imgLook").width(750);
    $(".title_div").width(window.screen.availWidth - 20);



    //$(".keyboard").width( 750);





    function sleep(delay) {
        for (var t = Date.now() ; Date.now() - t <= delay;);
    }


    //影像联动
    $("#btnYXLD").click(function () {
        //var kbn = "11";
        var kbn = "00";
        var cd = $("#lblCode").text();
        var no = $("#lblMake_no").text().PadLeft(10, "0");
        var cnt;//= $("#tbxCnt").val().PadLeft(2, "0");
        var lr;

        if (chk_method == "l") {
            lr = "1";
            cnt = "01";
        } else {
            lr = "2";
            cnt = "02";
        }

        var line_cd = $("#lblLine_id").text().trim();
        if (line_cd.indexOf("983") != -1) {
            lr = "";
        } else {

        }

        var url;
        if (line_cd.indexOf("983") != -1) {
            url = PUB_YXLD_URL + "?code=" + kbn + no + cnt + cd + lr + "&dw=" + dw + "&dh=" + dh;
        } else {
            //url = "http://10.162.201.93:5001/api/code?code=" + kbn + no + cnt + cd + lr;
            //url = PUB_YXLD_URL + "?code=" + kbn + no + cnt + cd + lr;
            url = PUB_YXLD_URL + "?code=" + kbn + no + cnt + cd;
        }

        // 调用方式
        loadUrlSafe('fraYXLD', url);
        //$('#fraYXLD').attr('src', url);


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
                dw: "",
                dh: ""
            },
            datatype: 'html', //'xml', 'html', 'script', 'json', 'jsonp', 'text'.
            beforeSend: function () { },
            //when success
            success: function (data) {
                if (data) {
                    //tms = 30;
                    //AjaxGet(kbn, no, cnt);
                    alert("影像联动OK");

                    $("#btnYXLD").css("background-color", "LightGreen");

                } else {
                    alert("请求失败");
                    //$("#cover").remove();
                    $("#cover").hide();
                }
            },
            //when complete
            complete: function (XMLHttpRequest, textStatus) {
            },
            //when error
            error: function () { }
        });

        //阻止冒泡
        event.preventDefault();
        event.stopPropagation();
        return false;

    });

    //影像联动
    /**
    
    */
    function YXLD(inObj, btn, dh, dw) {

        var arr = [];
        var tmp_thisRow = $(inObj).parent().parent();
        //var param;
        //param = ParamRowInfo(tmp_thisRow);

        var kbn = "11";
        var cd = $("#lblCode").text();
        var no = $("#lblMake_no").text().PadLeft(10, "0");
        var cnt;//= $("#tbxCnt").val().PadLeft(2, "0");
        var lr;

        if (chk_method == "l") {
            lr = "1";
            cnt = "01";
        } else {
            lr = "2";
            cnt = "02";
        }

        var line_cd = $("#lblLine_id").text().trim();
        if (line_cd.indexOf("983") != -1) {
            lr = "";
        } else {

        }

        //*** 特注品 注意 ， 取的字段不同*******
        //http://10.162.201.93:5001/api/code?code=11900687307601WA-AC00HR-MHFT1
        var url;
        if (line_cd.indexOf("983") != -1) {
            url = PUB_YXLD_URL + "?code=" + kbn + no + cnt + cd + lr + "&dw=" + dw + "&dh=" + dh;
        } else {
            //url = "http://10.162.201.93:5001/api/code?code=" + kbn + no + cnt + cd + lr;
            url = PUB_YXLD_URL + "?code=" + kbn + no + cnt + cd + lr;
        }

        //$('#fraYXLD').attr('src', url);
        loadUrlSafe('fraYXLD', url);

        if (dw == undefined) dw = "null";
        if (dh == undefined) dh = "null";

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
                dw: dw,
                dh: dh
            },
            datatype: 'html', //'xml', 'html', 'script', 'json', 'jsonp', 'text'.
            beforeSend: function () { },
            //when success
            success: function (data) {
                if (data) {
                    //tms = 30;
                    //AjaxGet(kbn, no, cnt);

                } else {
                    alert("请求失败");
                    //$("#cover").remove();
                    $("#cover").hide();
                }
            },
            //when complete
            complete: function (XMLHttpRequest, textStatus) {
            },
            //when error
            error: function () { }
        });

        var i;
        i = 0;
        var myVar = setInterval(function () {

            var ajRtv = 0;

            //sleep(1000);
            $.ajax({
                type: 'POST',
                url: 'YXLD_SEND.aspx?ajaxActionType=2&k' + new Date().getTime(),
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

                    if (data.substring(0, 2) == "OK") {

                        var rlt;
                        //0, NG
                        //1, OK
                        //2, 只拍照
                        rlt = data.substring(data.length - 1, data.length)
                        $("#cover").text(i + "回：接收完了" + data);
                        //$("#cover").remove();
                        $("#cover").hide();

                        if (rlt == "0") {
                            SetResult(false, acResult);
                            SetNextFocus(acIn1);
                        } else if (rlt == "1") {
                            SetResult(true, acResult);
                            SetNextFocus(acIn1);
                        } else if (rlt == "2") {
                            $(acIn1).val("只拍照");
                            SetResult(true, acResult);
                            SetNextFocus(acIn1);
                        }
                        $(btn).val("影像联动检查");
                        ajRtv = 1;
                    } else {
                        $("#cover").text(i + "回：接收中......" + data);
                        $(btn).val("影像联动检查中...");
                    }
                },
                //when complete
                complete: function (XMLHttpRequest, textStatus) {
                },
                //when error
                error: function () { }
            });


            if (ajRtv == 1) {
                clearInterval(myVar);
                return;
            }

            i++;
            if (i == 30) {
                alert("没有接受到影像结果，请手动判断！！");
                $(btn).val("影像联动检查");
                //$("#cover").remove();
                $("#cover").hide();
                $(".kb_okng").show();
                clearInterval(myVar);
            }

        }, 1000);


    }





    //----------------------------
    // 准备按钮按下 设置【准备完了FLG】cookkie？
    //    1.把CD，NO内容登录到 m_yxld 【0.准备】
    //    2.现场开始检查就自动 GetYXLD_RLT 准备开始接收
    //    3.业者影像系统 从 m_yxld 获得 CD和No内容 接口1    （GetYXLD）
    //    4.业者影像系统 从 m_yxld 根据 CD和No内容 设置 接口2（SetYXLD）
    //    5.把业者设置的结果反应到画面上
    //----------------------------
    $("#btnZB").click(function () {
        var cd = $("#lblCode").text();
        var no = $("#lblMake_no").text();
        var line = $("#hidPlanLineId").val();
        putYXLD_CNT = "0";
        $("#rtv1").text("");
        $("#rtv2").text("");
        $("#btnZB")[0].disabled = true;
        $("#btnZB").css('background-color', 'green');
        var user = $("#hidInsUser").val();

        //登录数据到 m_yxld 获得返回值 getdate():yyyy-MM-dd HH:mm:sss.fff
        var rtv = WebMethod("api", "SetJunbiYXLD",
            JSON.stringify({
                no: no,
                cd: cd,
                line: line,
                user: user
            }
            ));

        $("#rtv0").text(rtv);

        //
        //if (rtv == "OK") {
        if (rtv.length == 23) {
            $("#hidYXLD_START_TIME").val(rtv);
            $("#btnZB").val("1.准备影像检查开始接收!!!");
            //SetYXLDCookie(cd + '|' + no + '|' + line);
            GetYXLD_RLT(cd, no, line);
            $("#rtv0").text(rtv + "：等待影像结果");

        } else {
            $("#btnZB").val("1.准备影像检");
            $("#btnZB")[0].disabled = false;
            $("#btnZB").css('background-color', '');
            alert(rtv);
        }

        return false;

    });

    //接口1
    $("#btnSv1").click(function () {
        var line = $("#hidPlanLineId").val();

        var rtv = WebMethod("api", "GetYXLD",
            JSON.stringify({
                line: line
            }
            ));

        $("#rtv1").text("接口1结果：" + rtv);

    });

    //接口2
    $("#btnSv2").click(function () {
        var cd = $("#lblCode").text();
        var no = $("#lblMake_no").text();
        var line = $("#hidPlanLineId").val();
        var txt = $("#tbxTxt").text();

        var rtv = WebMethod("api", "SetYXLD",
            JSON.stringify({
                no: no,
                cd: cd,
                line: line,
                txt: txt
            }
            ));

        $("#rtv2").text("接口2结果：" + rtv);

    });


    $("#btnHujiao").click(function () {
        $("#hujiao").show();

        var rtv = WebMethodToStr("api", "GetHujiao",
            JSON.stringify({
                line_id: pub_line_id
            }
        ));



        var tdStation = $("#tdStation");
        var tdTaiChe = $("#tdTaiChe");

        tdStation.html("");
        tdTaiChe.html("");
        var SetStation = function (qr, txt) {
            var div = $('<div class="dynamic-div"></div>');
            div.appendTo(tdStation);
            $("<p>" + txt + "</p><hr>").appendTo(tdStation);
            var qrcode = new QRCode(div[0], {
                text: qr,
                width: 128,
                height: 128,
                colorDark: "#000000",
                colorLight: "#ffffff",
                correctLevel: QRCode.CorrectLevel.H
            });
        }

        var SetTaiChe = function (qr, txt) {
            var div = $('<div class="dynamic-div"></div>');
            div.appendTo(tdTaiChe);
            $("<p>" + txt + "</p><hr>").appendTo(tdTaiChe);
            var qrcode = new QRCode(div[0], {
                text: qr,
                width: 128,
                height: 128,
                colorDark: "#000000",
                colorLight: "#ffffff",
                correctLevel: QRCode.CorrectLevel.H
            });
        }


        var arrStation = rtv.split("|");

        for (i = 0; i <= arrStation.length - 1; i++) {
            SetStation(arrStation[i], arrStation[i].split("/")[1]);
        }


        var all_tps = localStorageGetItem("all_tps");
        var ac_tp = localStorageGetItem("Active_TpBarcode");

        if (all_tps) {
            var tpNos = all_tps.split(",");
            var i;
            for (i = 0; i <= tpNos.length - 1; i++) {

                var txt;
                var value;
                txt = tpNos[i];

                if (txt != "") {

                    value = "XTRO/" + tpNos[i] + "/" + tpNos[i];
                    if (ac_tp) {
                        if (ac_tp == tpNos[i]) {
                            txt = "●" + txt;
                        }
                    }

                    SetTaiChe(value, txt);
                }
            }
        }


        //window.open("HuJiaoPop.aspx?line_id=" + pub_line_id);
        //阻止冒泡
        event.preventDefault();
        event.stopPropagation();
        return false;
    });









});






