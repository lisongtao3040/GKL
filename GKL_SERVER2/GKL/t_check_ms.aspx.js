

var pub_chk_no;
var pub_user;
var pub_line_id;
var sanp_chk_method_id;

//返回字符串  
function returnString() {
    return sanp_chk_method_id;
}


function isJSON(str) {
    try {
        JSON.parse(str);
        return true;
    } catch (e) {
        return false;
    }
}

//共通AJAX 呼出函数
function WebMethod(asmxName, functionName, param) {

    //$("#cover").show();
    var rtv;
    $.ajaxSetup({ cache: false });
    $.ajax({
        type: "POST",
        url: asmxName + ".asmx/" + functionName,
        contentType: "application/json;charset=utf-8",
        async: false,//使用同步的方式,true为异步方式
        data: param,
        dataType: "json",
        success: function (data) {
            //$("#cover").hide();
            if (isJSON(data.d)) {
                rtv = $.parseJSON(data.d);
            } else {
                rtv = data.d;
            }
        },
        error: function (message) {
            //$("#cover").hide();
            alert("提交失败:" + asmxName + "." + functionName + $.parseJSON(param) + message.responseText);
        }
    });
    return rtv;
}

//// Function to get the value of a cookie
//function getCookie(cookieName) {
//    const name = cookieName + "=";
//    const decodedCookies = decodeURIComponent(document.cookie);
//    const cookiesArray = decodedCookies.split(";");

//    for (let i = 0; i < cookiesArray.length; i++) {
//        let cookie = cookiesArray[i];
//        while (cookie.charAt(0) === " ") {
//            cookie = cookie.substring(1);
//        }
//        if (cookie.indexOf(name) === 0) {
//            return cookie.substring(name.length, cookie.length);
//        }
//    }
//    return "";
//}

//function SetYXLDCookie(value) {
//    var expires = new Date();
//    expires.setDate(expires.getDate() + 7); // 过期时间为7天后
//    document.cookie = "yxld_key=" + value + "; expires=" + expires.toUTCString();
//}

//function IsYXLDAutoRun(cd, no, line) {
//    var yxld_key;
//    yxld_key = getCookie("yxld_key");
//    return (cd + '|' + no + '|' + line) == yxld_key;
//    //SetYXLDCookie(cd + '|' + no + '|' + line);
//}
// Usage
//const cookieValue = getCookie("cookieName");
//console.log(cookieValue);


$(document).ready(function () {

    try {
        window.external.SetSnapTrue();
    } catch (e) {

    }

    //获得影像结果
    var putYXLD_CNT = 0;
    var GetYXLD_RLT = function (cd, no, line) {
        var rtv = WebMethod("api", "GetYXLD_RLT",
            JSON.stringify({
                no: no,
                cd: cd,
                line: line,
                start_time: $("#hidYXLD_START_TIME").val()
            }
            ));

        if (putYXLD_CNT > 8000) {
            $("#btnZB")[0].disabled = false;
            $("#btnZB").css('background-color', '');
            $("#rtv0").text("80分钟未获得结果，终止影像检查结果获取");
            $("#btnZB").val("1.准备影像检");
        }

        if (rtv == "NEXT") {
            putYXLD_CNT++;
            $("#btnZB").val("1.准备影像检查开始接收!!!" + putYXLD_CNT);
            setTimeout(function () {
                GetYXLD_RLT(cd, no, line);
            }, 4000);

        } else if (rtv == "NG") {
            $("#btnZB")[0].disabled = false;
            $("#btnZB").css('background-color', '');
            $("#rtv0").text("影像检查结果获取失败");
            $("#btnZB").val("1.准备影像检");

        } else {
            $("#btnZB")[0].disabled = false;
            $("#btnZB").css('background-color', '');
            $("#rtv0").text("影像检查结果获取成功：" + rtv);
            $("#btnZB").val("1.准备影像检");


            var i;
            var arr = rtv.split(",");

            for (i = 0; i <= arr.length - 1; i = i + 2) {
                var km = arr[i];
                var yx_rlt = arr[i + 1];

                $("#gvMs").find("tr").each(function (index, element) {
                    var tr = $(element);
                    var kmTd = tr.find("td").eq(1);
                    var kmTxt = tr.attr("chk_km_name");
                    if (kmTxt.indexOf(km) >= 0 && km != '' && kmTxt != '') {
                        var jqIn1Obj = tr.find(".jq_in1");
                        jqIn1Obj[0].click();
                        if (yx_rlt == "0") {
                            SetResult(false, jqIn1Obj);
                        } else {
                            SetResult(true, jqIn1Obj);
                        }

                    }
                });
            }
        }
    }

    var yxld_cd = $("#lblCode").text();
    var yxld_no = $("#lblMake_no").text();
    var yxld_line = $("#hidPlanLineId").val();
    if ($("#hidYXLD_START_TIME").val() != '') {
        //if (IsYXLDAutoRun(yxld_cd, yxld_no, yxld_line)) {
            $("#btnZB").val("1.准备影像检查开始接收!!!");
            $("#rtv0").text("等待影像结果");
            GetYXLD_RLT(yxld_cd, yxld_no, yxld_line);
        //}
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



    var chk_id;
    var kj_0;
    var kj_1;
    var kj_2;
    var chk_method_id;
    var chk_method;
    var chk_formula;
    var pic_old_id;
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

        if (ky == "OK" || ky == "NG") {
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

        var allRlt = true;
        $(".jq_result").each(function (index, element) {
            if ($(element).val() != "OK") {
                allRlt = false;
                return false;
            }
        });

        //allRlt = true;

        if (allRlt) {
            if ($(".sel_link_btn").next()) {
                //$(".sel_link_btn").next().removeAttr("disabled");
                //$(".sel_link_btn").next()[0].disabled = false;

                $(".sel_link_btn").next().unbind("click");
                $(".sel_link_btn").next().removeAttr("onclick");
            }
        } else {
            if ($(".sel_link_btn").next()) {
                try{
                    $(".sel_link_btn").next()[0].disabled = true;
                } catch (e) {

                }

            }
        }

        //if ($(".sel_link_btn").next()) {
        //    $(".sel_link_btn").next().removeAttr("disabled");
        //} else {
        //    $(".sel_link_btn")[0].disabled = true;
        //}




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



            //1.4捆包票签验证
            //扫描捆包标签QR码，按规则取出制品CODE，与现品票CODE一致
            //MM-AA06H-MBN9       Y7181111 PPP/C
            if (tmp_chk_id == '312D00007') {
                //].trim()
				if (value.slice(-2) != "/C") {
                    jq_e.val(value);
                    return false;
				}
                
                var cd = value.split("Y7")[0].trim();
                if ($("#lblCode").text() == cd) {
                    jq_e.val(value);
                    return true;
                } else {
                    jq_e.val(value);
                    return false;
                }

            } else if (tmp_chk_id == '312D00006') {
                //1.3日期标签验证
                //解密后，取得工单号与现品票工单号一致
                //https://cs.lixil.co.jp/D86beweG74FjKnpgqneLtl9j5yaWWaT4b5lGo
                //var no = value.split("/")[value.split("/").length - 1].trim();

                var rtvTxt = AjaxPostChkDateBar(value);


                if ($("#lblMake_no").text().trim().substring(1, 10) == rtvTxt) {
                    jq_e.val(rtvTxt);
                    return true;
                } else {
                    jq_e.val(rtvTxt);
                    return false;
                }
            } else if (tmp_chk_id == '312D00008') {//中间材生产明细书
                //|| tmp_chk_id == '312D00001'
                //WMJBMM9Z80062283/MJBMM9Z80062283/40/40/2134//5/9007790405
                //wMJBMM9Z80062283/MJBMM9Z80062283/40/40/2134//5/9007790405
                //9007615034/CHFDPDMABAAXXJX /0001/1202356717 /20 /20211116090509067/2090.0/0795.0/2055.0/0738.0/0000.0/0000.0/Y7211116/15208
                //9007808011/MM-AB065H-MBN9 /0001/1202356717 /20 /20211116090509067/2090.0/0795.0/2055.0/0738.0/0000.0/0000.0/Y7211116/15208
                var arr = [];
                arr = value.split("/");
                var zjcCd = arr[0];

                jq_e.val(zjcCd);

                var arrZjc = zjcCd.split("");
                var i;
                var strZjcCd = "";
                var strColor = "";
                for (i = 0; i <= arrZjc.length - 1; i++) {
                    if (i == 4 || i == 5) {
                        strColor = strColor + arrZjc[i];
                    } else {
                        strZjcCd = strZjcCd + arrZjc[i];
                    }
                }

                //if (strZjcCd != tmp_thisRow.attr("midcode")) {
                //    return false;
                //}

                //中建材CD改成相等了（所以不用去掉4,5位了）
                if (zjcCd != tmp_thisRow.attr("midcode")) {
                    return false;
                }

                var color_nm = tmp_thisRow.attr("color_nm");
                if (color_nm.length == 1) {
                    color_nm = color_nm + '' + color_nm;
                } else if (color_nm.length == 3) {
                    color_nm = color_nm.substring(0, 2);
                }

                if (strColor != color_nm) {
                    return false;
                }


                return true;


            } else if (tmp_chk_id == '312D00009') {//制品生产明细书
                //9007808011/MM-AB08K-MEZ9 /60
                //WMM-AB08K-MEZ9/MM-AB08K-MEZ9/20/20/4102/AX36/1/9007808011

                var arr = [];
                arr = value.split("/");

                var tmp_make_no;
                tmp_make_no = "";

                if (arr.length == 3) {
                    tmp_make_no = arr[0];
                } else if (arr.length == 8) {
                    tmp_make_no = arr[7];
                }

                jq_e.val(tmp_make_no);

                if (tmp_make_no == $("#lblMake_no").text().trim()) {
                    return true;
                } else {
                    return false;
                }
            } else if (tmp_chk_id == '312D00010') {//中间材生产明细书
                //22589/9011809825/WMKTPP530L2/2023-05-19
                var arr = [];
                arr = value.split("/");
                var zjcCd = arr[2];

                //中建材CD改成相等了（所以不用去掉4,5位了）
                if (zjcCd != tmp_thisRow.attr("midcode")) {
                    return false;
                }

                return true;
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
    function AjaxPostChkDateBar(url) {
        $("#btnComplete").attr("disabled", true);
        $("#btnModoru").attr("disabled", true);
        var rtv;
        rtv = "";
        $.ajax({
            type: 'POST',
            url: 'AJAX.aspx?kbn=DateBar',
            async: false, //true:yibu
            data: {
                url: url,
                make_no: $("#lblMake_no").text(),
            },
            datatype: 'html', //'xml', 'html', 'script', 'json', 'jsonp', 'text'.
            beforeSend: function () { },
            //when success
            success: function (data) {
                $("#btnComplete").removeAttr("disabled");
                $("#btnModoru").removeAttr("disabled");

            },
            //when complete
            complete: function (XMLHttpRequest, textStatus) {

                rtv = XMLHttpRequest.responseText;
                //return rtvTxt;




                //if (XMLHttpRequest.responseText == "true") {
                //    rtv = true;
                //    return true;
                //}
                //if (XMLHttpRequest.responseText == "true" || XMLHttpRequest.responseText == "false") {

                //} else {
                //    alert(XMLHttpRequest.responseText);
                //}

            },
            //when error
            error: function () { alert('日期标签检查错误'); }
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
        var rtv = WebMethod("api", "SetJunbiYXLD",
            JSON.stringify({
                no: no,
                cd: cd,
                line: line,
                user: user
            }
            ));

        $("#rtv0").text(rtv);

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
    //$(".keyboard").width( 750);
});