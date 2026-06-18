function trim(str) {
    if (isEmpty(str)) return '';
    if (str == null) return '';
    return (str + '').replace(/^\s+|\s+$/g, '');
}

// 改进：统一的用户反馈函数
function showMessage(message, type) {
    // type: 'info', 'warning', 'error'
    var prefix = '';
    switch (type) {
        case 'warning':
            prefix = '警告: ';
            break;
        case 'error':
            prefix = '错误: ';
            break;
    }
    alert(prefix + message);
}

//共通AJAX 呼出函数
function WebMethod(asmxName, functionName, param) {

    // 改进：添加参数验证
    if (isEmpty(asmxName) || isEmpty(functionName)) {
        console.error('WebMethod调用参数不完整');
        showMessage('请检查asmxName和functionName参数', 'warning');
        return "";
    }

    var rtv = "";
    $.ajaxSetup({ cache: false });

    $.ajax({
        type: "POST",
        url: asmxName + ".asmx/" + functionName,
        contentType: "application/json;charset=utf-8",
        async: false,//使用同步的方式,true为异步方式
        data: param,
        dataType: "json",
        success: function (data) {
            if (typeof data.d == "object") {
                rtv = data.d;
            }
            if (isJSON(data.d)) {//如果是JSON类型 返回JSON类型值
                rtv = $.parseJSON(data.d);
            } else {
                rtv = data.d;//如果不是JSON类型 返回就这么返回 ，也就是可能是字符串，也有可能是 Object，之后利用时，有Object.result 利用的时候
            }
        },
        error: function (message) {
            alert("提交失败:" + asmxName + "." + functionName + $.parseJSON(param) + message.responseText);
        }
    });

    return rtv;
}

function WebMethodToStr(asmxName, functionName, param) {

    // 改进：添加参数验证
    if (isEmpty(asmxName) || isEmpty(functionName)) {
        console.error('WebMethodToStr调用参数不完整');
        showMessage('请检查asmxName和functionName参数', 'warning');
        return "";
    }

    var rtv = "";
    $.ajaxSetup({ cache: false });

    $.ajax({
        type: "POST",
        url: asmxName + ".asmx/" + functionName,
        contentType: "application/json;charset=utf-8",
        async: false,//使用同步的方式,true为异步方式
        data: param,
        dataType: "json",
        success: function (data) {
            rtv = data.d + '';
        },
        error: function (message) {
            alert("提交失败:" + asmxName + "." + functionName + $.parseJSON(param) + message.responseText);
        }
    });

    return rtv;
}


function WebMethodLog(asmxName, functionName, param) {

    // 改进：添加参数验证
    if (isEmpty(asmxName) || isEmpty(functionName)) {
        console.error('WebMethodToStr调用参数不完整');
        showMessage('请检查asmxName和functionName参数', 'warning');
        return "";
    }

    var rtv = "";
    $.ajaxSetup({ cache: true });

    $.ajax({
        type: "POST",
        url: asmxName + ".asmx/" + functionName,
        contentType: "application/json;charset=utf-8",
        async: false,//使用同步的方式,true为异步方式
        data: param,
        dataType: "json",
        success: function (data) {
            rtv = data.d + '';
        },
        error: function (message) {
            alert("提交失败:" + asmxName + "." + functionName + $.parseJSON(param) + message.responseText);
        }
    });

    return rtv;
}

function isJSON(str) {
    try {
        JSON.parse(str);
        return true;
    } catch (e) {
        return false;
    }
}

// 获取 cookie
function getCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) === ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) === 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

// 设置一个有效期为 7 天的 cookie
function setCookie(name, value, days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value || "") + expires + "; path=/";
}


function isString(value) {
    return typeof value === 'string';
}

function isObject(value) {
    return value !== null && typeof value === 'object';
}

function isArray(value) {
    return Object.prototype.toString.call(value) === '[object Array]';
}


function isEmpty(value) {
    if (value == null || value === undefined) return true;
    /* 
    if (isString(value)) return trim(value) === '';
   if (isArray(value)) return value.length === 0;
    if (isObject(value)) {
        for (var key in value) {
            if (value.hasOwnProperty(key)) return false;
        }
        return true;
    }
    */
    if (typeof value === 'number') return isNaN(value);
    return false;
}

function localStorageGetItem(key) {

    var lastRtv = "";
    var rtvCookie = null;
    rtvCookie = getCookie(key);
    if (rtvCookie != null) {
        lastRtv = rtvCookie;
    } else {
        try {
            var rtv = WebMethodToStr("api", "GetServerStorage",
                JSON.stringify({
                    login_user_cd: getCookie('login_user_cd'),
                    key: key
                }
            ));
            lastRtv = rtv;
        } catch (e) {
            lastRtv = "";
        }
    }

    if (isEmpty(lastRtv)) {
        return "";
    } else {
        return lastRtv;
    }
}

function localStorageSetItem(key, value) {
    setCookie(key, value, 1);
    setTimeout(function () {
        WebMethodToStr("api", "SetServerStorage",
                JSON.stringify({
                    login_user_cd: getCookie('login_user_cd'),
                    key: key,
                    value: value
                }
            ));
    }, 1);
}


$(document).ready(function () {

    //============================================================================================================
    //关联托盘
    $("#joinTp_scmx").click(function (e) {
        $("#NEW_TP_DIV").show();

        var logs = [];
        logs.push("・[画面.关联托盘 Click]　" + getCookie('login_user_cd'));

        try {


            var all_tps = localStorageGetItem("all_tps");
            var ac_tp = localStorageGetItem("Active_TpBarcode");

            logs.push("    所有托盘:" + all_tps);
            logs.push("    默认托盘:" + ac_tp);

            //托盘加载
            logs.push("    托盘加载开始");
            if (all_tps) {
                var tpNos = all_tps.split(",");
                var i;
                for (i = 0; i <= tpNos.length - 1; i++) {
                    $("#iptTpBarcode" + (i + 1)).val(tpNos[i]);
                    if (ac_tp) {
                        if (ac_tp == tpNos[i]) {
                            $("#iptTpBarcode" + (i + 1)).focus();
                            $("#iptTpBarcode" + (i + 1)).select();
                            $(".defaultTpTd").text('');
                            $("#defaultTpTd" + (i + 1)).text('●');
                            logs.push("    默认托盘加载成功：" + ac_tp);
                        }
                    }
                }
            }
            logs.push("    托盘加载成功");


            logs.push("    向先加载开始");
            var all_tps_xiangxian = localStorageGetItem("all_tps_xiangxian");
            logs.push("    所有向先:" + all_tps_xiangxian);

            if (all_tps_xiangxian) {
                var tpXiangxians = all_tps_xiangxian.split(",");
                var i;
                for (i = 0; i <= tpXiangxians.length - 1; i++) {
                    $("#xiangxian" + (i + 1)).text(tpXiangxians[i]);
                }
            }
            logs.push("    向先加载成功");

        } catch (error) {
            logs.push("    失败：" + error.message);
        }

        setTimeout(function () {
            WebMethodLog("api", "WriteSerLog",
                    JSON.stringify({
                        user_cd: getCookie('login_user_cd'),
                        txt: logs.join('\n')
                    }));
        }, 1);

    });

    //设置当前托盘文字
    $("#acTp").text(localStorageGetItem("Active_TpBarcode"));

    //设置托盘码
    $(".tp_barcode").keydown(function (e) {
        if (event.keyCode == 13) {
            var cd = $(this).val();
            if (cd.split("/").length == 3) {
                $(this).val(cd.split("/")[2]);
            }
            e.preventDefault();
        }
    });

    $(".tp_barcode").focus(function () {

        //$(this).focus();
        $(this).select();
    });


    //修正托盘
    //取得订单下所有托盘的报工数据，表示到画面上
    $("#editTp").click(function (e) {

        var logs = [];
        logs.push("・[画面.修正托盘 Click]　" + getCookie('login_user_cd'));




        var no = trim($("#lblMake_no").text());
        var cd = trim($("#lblCode").text());

        logs.push("    [画面.修正托盘]作番：　" + no);
        logs.push("    [画面.修正托盘]CD：　" + cd);


        var rtv = WebMethod("Bg", "GetBgPanel",
                JSON.stringify({
                    no: no,
                    cd: cd,
                    IsBaogong: ($("#hidBaogong").val() == "1"),
                    IsGuanlianTuopan: ($("#hidTuopanLines").val() == "1")
                }
            ));

        logs.push("    [画面.修正托盘.子画面]：打开　");
        TP_PANEL_INIT(rtv);

        var i;
        for (i = 1; i <= 6; i++) {

            logs.push("    [画面.修正托盘.子画面.上部按钮（" + i + "）Barcode+xiangxian]：　iptTpBarcode：" + $("#iptTpBarcode" + (i)).val());
            logs.push("    [画面.修正托盘.子画面.上部按钮（" + i + "）Barcode+xiangxian]：　xiangxian：" + $("#xiangxian" + (i)).text());

            $("#btnTp" + i).val($("#iptTpBarcode" + (i)).val() + '\n' + $("#xiangxian" + (i)).text());
            $("#btnTp" + i).attr("tp_no", $("#iptTpBarcode" + (i)).val());
        }

        //xiangxian5


        var all_tps = localStorageGetItem("all_tps");

        if (all_tps) {
            var tpNos = all_tps.split(",");
            var i;
            for (i = 0; i <= tpNos.length - 1; i++) {
                // $("#iptTpBarcode" + (i + 1)).val(tpNos[i]);
                $("#btnTp" + (i + 1)).val(tpNos[i]);
                $("#btnTp" + (i + 1)).val(tpNos[i] + '\n' + $("#xiangxian" + (i + 1)).text());
                $("#btnTp" + (i + 1)).attr("tp_no", tpNos[i]);
            }
        }
        logs.push("    [画面.修正托盘.子画面.所有托盘]：" + all_tps);

        try {
            $(document).find(".msBgTPNO")[0].focus();
        } catch (e1) {

        }

        setTimeout(function () {
            WebMethodLog("api", "WriteSerLog",
                    JSON.stringify({
                        user_cd: getCookie('login_user_cd'),
                        txt: logs.join('\n')
                    }));
        }, 1);



    });

    $(".tp_button").mousedown(function (e) {


        //typ="scan"

        var obj = document.activeElement;

        //alert($(obj).attr("typ"));

        if ($(obj).attr("typ") == "scan") {
            $(obj).val($(this).attr("tp_no"));
        }


        e.preventDefault();
    });


    //如果报工没打开
    // $("#hidBaogong").val() == "1" ||
    if ($("#hidTuopanLines").val() == "1") {
        //加载报工list
        $("#joinTp_scmx").show();
        $("#editTp").show();
        $("#acTp").show();
    }


    //报工面板-【关闭】
    $("#btnCloseBaogong").click(function () {
        $("#baogong_panel").hide();
    });



    //完了按钮按下时
    $("#btnHtmlComplete").click(function () {
        //if ($('#tbx_scan_scmx').attr("scmx") == "1") {
        //    if ($('#tbx_scan_scmx').val() != "OK") {
        //        alert("捆包标签确认结果不正确，不能完了");
        //        return false;
        //    }
        //}


        $('#btnHtmlComplete').attr("disabled", true);
        setTimeout(function () { $('#btnHtmlComplete').removeAttr("disabled"); }, 5300);

        if (camera_flg == "1") {
            if ($(".item_1").length == 0) {
                alert("请注意没有拍照，不能设置成OK！！！");
            }
        }

        $("#div_imgs").html("");

        //如果报工已经打开 而且 关联托盘也打开了
        if ($("#hidBaogong").val() == "1" && $("#hidTuopanLines").val() == "1") {
            //加载报工list
            if (!JoinTP_InsertTrayData()) {
                return false;
            }
        } else if ($("#hidBaogong").val() == "1" && $("#hidTuopanLines").val() != "1") {
            //调用后台报工
        } else if ($("#hidBaogong").val() != "1" && $("#hidTuopanLines").val() == "1") {
            //
            if (!JoinTP_InsertTrayData()) {
                return false;
            }
        } else {

        }

        setTimeout(function () { $('#btnComplete').click() }, 300);
        return true;
    });




});




//绑定PANEL
function TP_PANEL_INIT(rtv) {


    var line_cd = trim($("#lblLine_id").text());
    var insUser = trim($("#lblUser").text());
    var no = trim($("#lblMake_no").text());
    var cd = trim($("#lblCode").text());
    var chk_id = $("#hidChkNo").val();




    $("#gongdan").text(no);

    $("#baogong_panel").show();
    $("#baogong_panel_data").html(rtv.panelHTML);

    //自动设置第一个托盘


    $(".old_tp_ipt").val(localStorageGetItem("Active_TpBarcode"));

    //msBgTPNO_EDIT
    //-----------------------------------------------------------
    //bg_result报工结果
    //空的/NG时候【报工】表示
    //
    //tp_bar_cd:关联托盘CD
    //空的时候【关联】表示
    //   以外 【修改】表示
    //-----------------------------------------------------------
    //点击 明细-【报工】
    $(".msBgBtn").click(function (e) {


        var logs = [];
        logs.push("    [画面.修正托盘.子画面.报工 Click]：");


        var no = trim($("#lblMake_no").text());
        var cd = trim($("#lblCode").text());
        var tp_bar_cd = trim($(this).parent().parent().find(".msBgTPNO").val());
        var tp_no = trim($(this).parent().parent().attr("tp_no"));

        logs.push("        no：　" + no);
        logs.push("        cd：　" + cd);
        logs.push("        tp_bar_cd：　" + tp_bar_cd);
        logs.push("        tp_no 行号：　" + tp_no);


        if (tp_no == "") {
            alert("托盘No为空");
            return false;
        }

        localStorageSetItem('Active_TpBarcode', tp_bar_cd);

        $("#acTp").text(tp_bar_cd);

        var rtvTP = WebMethod("Bg", "RunBgOnlyService",
            JSON.stringify({
                no: no,
                cd: cd,
                insUser: insUser,
                tp_bar_cd: $(this).parent().parent().attr("tp_bar_cd"),
                tp_no: tp_no
            }
            ));

        logs.push("        RunBgOnlyService：　" + JSON.stringify({
                                        no: no,
                                        cd: cd,
                                        insUser: insUser,
                                        tp_bar_cd: $(this).parent().parent().attr("tp_bar_cd"),
                                        tp_no: tp_no
                                    }));

        if (rtvTP.result == "OK") {

            logs.push("        RunBgOnlyService：　OK:" + rtvTP.msg);


            alert("OK：" + rtvTP.msg);
            var rtv = WebMethod("Bg", "GetBgPanel",
                JSON.stringify({
                    no: no,
                    cd: cd,
                    IsBaogong: ($("#hidBaogong").val() == "1"),
                    IsGuanlianTuopan: ($("#hidTuopanLines").val() == "1")
                }
                ));
            TP_PANEL_INIT(rtv);



            logs.push("        GetBgPanel（刷新子画面）：" + JSON.stringify({
                no: no,
                cd: cd,
                IsBaogong: ($("#hidBaogong").val() == "1"),
                IsGuanlianTuopan: ($("#hidTuopanLines").val() == "1")
            }
                ));

            setTimeout(function () {
                WebMethodLog("api", "WriteSerLog",
                        JSON.stringify({
                            user_cd: getCookie('login_user_cd'),
                            txt: logs.join('\n')
                        }));
            }, 1);

            return true;

        } else {

            logs.push("        RunBgOnlyService：　NG" + rtvTP.msg);
            alert("NG1：" + rtvTP.msg);

            setTimeout(function () {
                WebMethodLog("api", "WriteSerLog",
                        JSON.stringify({
                            user_cd: getCookie('login_user_cd'),
                            txt: logs.join('\n')
                        }));
            }, 1);

            return true;
        }
    });

    //点击 明细-【关联】
    $(".msBgTPNO_GL").click(function (e) {


        var logs = [];
        logs.push("・[画面.修正托盘.关联 Click]　" + getCookie('login_user_cd'));

        var tp_bar_cd = trim($(this).parent().parent().find(".msBgTPNO").val());
        var tp_no = trim($(this).parent().parent().attr("tp_no"));
        logs.push("    tp_bar_cd托盘号：" + tp_bar_cd);
        logs.push("    tp_no行号：" + tp_no);

        if (tp_bar_cd == "") {
            alert("请扫描托盘码");
            return false;
        }
        localStorageSetItem('Active_TpBarcode', tp_bar_cd);
        $("#acTp").text(tp_bar_cd);
        var rtvTP = WebMethod("Bg", "WB_InsertTrayData",
            JSON.stringify({
                line_cd: line_cd,
                no: no,
                cd: cd,
                insUser: insUser,
                insUserName: $("#lblUserName").text(),
                chk_id: chk_id,
                tp_bar_cd: tp_bar_cd,
                tp_no: tp_no

            }
            ));

        logs.push("    WB_InsertTrayData 关联托盘：" + JSON.stringify({
            line_cd: line_cd,
            no: no,
            cd: cd,
            insUser: insUser,
            insUserName: $("#lblUserName").text(),
            chk_id: chk_id,
            tp_bar_cd: tp_bar_cd,
            tp_no: tp_no

        }
            ));


        if (rtvTP.result == "OK") {

            logs.push("    WB_InsertTrayData 关联托盘：OK" + rtvTP.msg);

            alert("OK：" + rtvTP.msg);
            var rtv = WebMethod("Bg", "GetBgPanel",
                JSON.stringify({
                    no: no,
                    cd: cd,
                    IsBaogong: ($("#hidBaogong").val() == "1"),
                    IsGuanlianTuopan: ($("#hidTuopanLines").val() == "1")
                }
                ));
            TP_PANEL_INIT(rtv);

            logs.push("        GetBgPanel（刷新子画面）：" + JSON.stringify({
                no: no,
                cd: cd,
                IsBaogong: ($("#hidBaogong").val() == "1"),
                IsGuanlianTuopan: ($("#hidTuopanLines").val() == "1")
            }
    ));

            setTimeout(function () {
                WebMethodLog("api", "WriteSerLog",
                        JSON.stringify({
                            user_cd: getCookie('login_user_cd'),
                            txt: logs.join('\n')
                        }));
            }, 1);


            return true;
        } else {

            logs.push("    WB_InsertTrayData 关联托盘：NG" + rtvTP.msg);
            alert("NG2：" + rtvTP.msg);
            var rtv = WebMethod("Bg", "GetBgPanel",
                JSON.stringify({
                    no: no,
                    cd: cd,
                    IsBaogong: ($("#hidBaogong").val() == "1"),
                    IsGuanlianTuopan: ($("#hidTuopanLines").val() == "1")
                }
                ));
            TP_PANEL_INIT(rtv);

            logs.push("        GetBgPanel（刷新子画面）：" + JSON.stringify({
                no: no,
                cd: cd,
                IsBaogong: ($("#hidBaogong").val() == "1"),
                IsGuanlianTuopan: ($("#hidTuopanLines").val() == "1")
            }


    ));
            setTimeout(function () {
                WebMethodLog("api", "WriteSerLog",
                        JSON.stringify({
                            user_cd: getCookie('login_user_cd'),
                            txt: logs.join('\n')
                        }));
            }, 1);

            return true;
        }



    });

    //点击 明细-【修改】
    $(".msBgTPNO_EDIT").click(function (e) {

        var logs = [];
        logs.push("・[画面.修正托盘.修改 Click]　" + getCookie('login_user_cd'));

        var tp_bar_cd = trim($(this).parent().parent().find(".msBgTPNO").val());
        var tp_no = trim($(this).parent().parent().attr("tp_no"));


        logs.push("    tp_bar_cd托盘号：" + tp_bar_cd);
        logs.push("    tp_no行号：" + tp_no);



        if (tp_bar_cd == "") {
            alert("请扫描托盘码");
            return false;
        }





        localStorageSetItem('Active_TpBarcode', tp_bar_cd);

        $("#acTp").text(tp_bar_cd);

        var rtvTP = WebMethod("Bg", "Edit_UpdateTrayData",
            JSON.stringify({
                line_cd: line_cd,
                no: no,
                cd: cd,
                insUser: insUser,
                insUserName: $("#lblUserName").text(),
                chk_id: chk_id,
                tp_bar_cd1: $(this).parent().parent().attr("tp_bar_cd"),
                tp_bar_cd2: tp_bar_cd,
                tp_no: tp_no
            }
            ));

        logs.push("    Edit_UpdateTrayData（点击【修正托盘】时，调用接口关联托盘）：" + JSON.stringify({
            line_cd: line_cd,
            no: no,
            cd: cd,
            insUser: insUser,
            insUserName: $("#lblUserName").text(),
            chk_id: chk_id,
            tp_bar_cd1: $(this).parent().parent().attr("tp_bar_cd"),
            tp_bar_cd2: tp_bar_cd,
            tp_no: tp_no
        }
            ));



        if (rtvTP.result == "OK") {

            logs.push("    Edit_UpdateTrayData（点击【修正托盘】时，调用接口关联托盘）：结果 OK");

            alert("OK：" + rtvTP.msg);
            var rtv = WebMethod("Bg", "GetBgPanel",
                JSON.stringify({
                    no: no,
                    cd: cd,
                    IsBaogong: ($("#hidBaogong").val() == "1"),
                    IsGuanlianTuopan: ($("#hidTuopanLines").val() == "1")
                }
                ));
            TP_PANEL_INIT(rtv);

            logs.push("    GetBgPanel（刷新子画面）：" + JSON.stringify({
                no: no,
                cd: cd,
                IsBaogong: ($("#hidBaogong").val() == "1"),
                IsGuanlianTuopan: ($("#hidTuopanLines").val() == "1")
            }
                ));


            setTimeout(function () {
                WebMethodLog("api", "WriteSerLog",
                        JSON.stringify({
                            user_cd: getCookie('login_user_cd'),
                            txt: logs.join('\n')
                        }));
            }, 1);


            return true;
        } else {

            logs.push("    Edit_UpdateTrayData（点击【修正托盘】时，调用接口关联托盘）：结果 NG");
            logs.push("    Edit_UpdateTrayData（点击【修正托盘】时，调用接口关联托盘）：结果 NG" + rtvTP.msg);

            alert("NG3：" + rtvTP.msg);
            var rtv = WebMethod("Bg", "GetBgPanel",
                JSON.stringify({
                    no: no,
                    cd: cd,
                    IsBaogong: ($("#hidBaogong").val() == "1"),
                    IsGuanlianTuopan: ($("#hidTuopanLines").val() == "1")
                }
                ));
            TP_PANEL_INIT(rtv);
            logs.push("    GetBgPanel（刷新子画面）：" + JSON.stringify({
                no: no,
                cd: cd,
                IsBaogong: ($("#hidBaogong").val() == "1"),
                IsGuanlianTuopan: ($("#hidTuopanLines").val() == "1")
            }
    ));

            setTimeout(function () {
                WebMethodLog("api", "WriteSerLog",
                        JSON.stringify({
                            user_cd: getCookie('login_user_cd'),
                            txt: logs.join('\n')
                        }));
            }, 1);
            return true;
        }
    });


    $(".msBgTPNO").keydown(function (e) {
        if (event.keyCode == 13) {
            var cd = $(this).val();
            if (cd.split("/").length == 3) {
                $(this).val(cd.split("/")[2]);
            }
            e.preventDefault();
        }
    });


}




//登录的时候调用
//单个托盘直接报工
//加载报工list
function JoinTP_InsertTrayData() {

    var line_cd = trim($("#lblLine_id").text());
    var insUser = trim($("#lblUser").text());
    var no = trim($("#lblMake_no").text());
    var cd = trim($("#lblCode").text());
    var chk_id = $("#hidChkNo").val();
    //var rtv = false;

    // $("#hidBaogong").val() == "1" ||
    //if ( $("#hidTuopanLines").val() == "1") {

    if ($("#hidTuopanLines").val() == "1" && trim(localStorageGetItem("Active_TpBarcode")) == "") {
        alert("请扫描托盘码");
        return false;
    } else {
        //localStorageSetItem('Active_TpBarcode', newTpBarcode);
    }

    var rtv = WebMethod("Bg", "JoinTP_InsertTrayData",
        JSON.stringify({
            line_cd: line_cd,
            no: no,
            cd: cd,
            insUser: insUser,
            insUserName: $("#lblUserName").text(),
            chk_id: chk_id,
            tp_bar_cd: localStorageGetItem("Active_TpBarcode")
        }
        ));

    if (rtv.result == "OK") {
        alert("OK：" + rtv.msg);
        return true;
    } else if (rtv.result == "WA") {
        alert("警告" + rtv.msg);
        return true;
    } else if (rtv.result == "OKPANEL") {
        TP_PANEL_INIT(rtv);
    } else {
        alert(rtv.msg);
        return false;
    }

}


//加载报工list
function GetBgList() {

    var line_cd = trim($("#lblLine_id").text());
    var insUser = trim($("#lblUser").text());
    var no = trim($("#lblMake_no").text());
    var cd = trim($("#lblCode").text());
    var chk_id = $("#hidChkNo").val();
    //var rtv = false;



    if ($("#hidTuopanLines").val() == "1" && trim(localStorageGetItem("Active_TpBarcode")) == "") {
        alert("请扫描托盘码");
        return false;
    } else {
        //localStorageSetItem('Active_TpBarcode', newTpBarcode);
    }


    var rtv = WebMethod("Bg", "JoinTP_InsertTrayData",
        JSON.stringify({
            line_cd: line_cd,
            no: no,
            cd: cd,
            insUser: insUser,
            insUserName: $("#lblUserName").text(),
            chk_id: chk_id,
            tp_bar_cd: localStorageGetItem("Active_TpBarcode")
        }
        ));

    if (rtv.result == "OK") {
        alert("OK：" + rtv.msg);
        return true;
    } else if (rtv.result == "WA") {
        alert("警告" + rtv.msg);
        return true;
    } else if (rtv.result == "SS") {
        alert("警告" + rtv.msg);
        return true;
    } else {
        alert(rtv.msg);
        return false;
    }
}


function CloseDivPanel(id) {
    $('#' + id).hide();
    return false;
}


function SetXiangxian() {

    var tps = [];
    for (i = 1; i <= 6; i++) {
        tps.push($("#iptTpBarcode" + (i)).val());
    }

    var rtv = WebMethod("Bg", "GetTpXiangxian",
        JSON.stringify({
            tps: tps.join(',')
        }));
    if (rtv.result == "OK") {
        var tpsRtv = rtv.txt.split(",");
        for (j = 1; j <= 6; j++) {
            $("#xiangxian" + j).text(tpsRtv[j - 1]);
        }
        localStorageSetItem('all_tps_xiangxian', rtv.txt);
    }

}

//设定新的托盘码
function SetNewTpBarcode(obj) {

    var logs = [];
    logs.push("・[画面.关联托盘.设定 Click]　" + getCookie('login_user_cd'));

    var idx = $(obj).attr("idx");
    var newTpBarcode = $("#iptTpBarcode" + idx).val();
    var i;

    logs.push("    设定托盘码(" + idx + ")" + newTpBarcode);



    if ($("#hidTuopanLines").val() == "1" && trim(newTpBarcode) == "") {
        logs.push("    画面显示消息：请扫描托盘码");
        alert("请扫描托盘码");
        $("#iptTpBarcode" + idx).focus();
        $("#iptTpBarcode" + idx).select();
        return false;
    } else {

        var tps = [];
        for (i = 1; i <= 6; i++) {
            tps.push($("#iptTpBarcode" + (i)).val());
            $("#btnTp" + i).val($("#iptTpBarcode" + (i)).val() + '\n' + $("#xiangxian" + (i)).text());
            $("#btnTp" + i).attr("tp_no", $("#iptTpBarcode" + (i)).val());
        }
        localStorageSetItem('all_tps', tps.join(','));

        logs.push("    设定所有托盘：" + tps.join(','));

        if ($("#defaultTpTd" + idx).text() == '●') {
            localStorageSetItem('Active_TpBarcode', trim(newTpBarcode));
        }

        logs.push("    设定默认托盘：" + newTpBarcode);

        try {
            //刷新向先
            logs.push("    刷新向先");
            SetXiangxian();
        } catch (error) {
            logs.push("    刷新向先失败");
            logs.push("    失败：" + error.message);
        }

        setTimeout(function () {
            WebMethodLog("api", "WriteSerLog",
                    JSON.stringify({
                        user_cd: getCookie('login_user_cd'),
                        txt: logs.join('\n')
                    }));
        }, 1);

        alert("托盘码设已经置成：" + newTpBarcode);
        //$('#NEW_TP_DIV').hide();
    }
}
//设定默认盘码
function SetTpDefault(obj) {

    var logs = [];
    logs.push("・[画面.关联托盘.默认 Click]　" + getCookie('login_user_cd'));

    var idx = $(obj).attr("idx");
    var newTpBarcode = $("#iptTpBarcode" + idx).val();
    var i;

    if ($("#hidTuopanLines").val() == "1" && trim(newTpBarcode) == "") {
        alert("请扫描托盘码");
        logs.push("    画面显示消息：请扫描托盘码");
        $("#iptTpBarcode" + idx).focus();
        $("#iptTpBarcode" + idx).select();
        return false;
    } else {
        var tps = [];
        for (i = 1; i <= 6; i++) {
            tps.push($("#iptTpBarcode" + (i)).val());
        }

        localStorageSetItem('all_tps', tps.join(','));
        localStorageSetItem('Active_TpBarcode', trim(newTpBarcode));

        $("#acTp").text(newTpBarcode);

        logs.push("    设定所有托盘：" + tps.join(','));
        logs.push("    设定默认托盘：" + newTpBarcode);


        $(".defaultTpTd").text('');
        $("#defaultTpTd" + idx).text('●');

        setTimeout(function () {
            WebMethodLog("api", "WriteSerLog",
                    JSON.stringify({
                        user_cd: getCookie('login_user_cd'),
                        txt: logs.join('\n')
                    }));
        }, 1);

        //alert("托盘码设已经置成：" + newTpBarcode);
        //$('#NEW_TP_DIV').hide();
    }
}

//清空托盘
function SetTpClear(obj) {

    var logs = [];
    logs.push("・[画面.关联托盘.清空 Click]　" + getCookie('login_user_cd'));

    var idx = $(obj).attr("idx");
    var newTpBarcode = '';

    //清空托盘行
    $("#iptTpBarcode" + idx).val('');

    $("#xiangxian" + idx).text('');

    //托盘按钮
    $("#btnTp" + idx).val('');
    $("#btnTp" + idx).attr("tp_no", '');


    logs.push("    清空：托盘行 向先，托盘No");
    var i;


    var tps = [];
    for (i = 1; i <= 6; i++) {
        tps.push($("#iptTpBarcode" + (i)).val());
    }
    localStorageSetItem('all_tps', tps.join(','));
    localStorageSetItem('Active_TpBarcode', newTpBarcode);

    $("#acTp").text(newTpBarcode);


    //如果是默认托盘 那么清空
    if ($("#defaultTpTd" + idx).text() == '●') {
        //默认列清空
        $(".defaultTpTd").text('');
        alert("请注意默认托盘为空！！！");
    }
    logs.push("    清空：成功");

    setTimeout(function () {
        WebMethodLog("api", "WriteSerLog",
                JSON.stringify({
                    user_cd: getCookie('login_user_cd'),
                    txt: logs.join('\n')
                }));
    }, 1);

    $("#iptTpBarcode" + idx).focus();
    //$('#NEW_TP_DIV').hide();

}




//修改托盘、生产明细书对应关系
function UpdTpBarcode() {
    var line_cd = trim($("#lblLine_id").text());
    var insUser = trim($("#lblUser").text());
    var no = trim($("#lblMake_no").text());
    var cd = trim($("#lblCode").text());
    var chk_id = $("#hidChkNo").val();

    var newTpBarcode1 = $("#txtNewTpBarcode1").val();
    var newTpBarcode2 = $("#txtNewTpBarcode1").val();


    if (newTpBarcode1 == "" || newTpBarcode2 == "") {
        alert("请扫描托盘码");
        return false;
    } else {


        var rtv = WebMethod("Bg", "Edit_UpdateTrayData",
            JSON.stringify({
                line_cd: line_cd,
                no: no,
                cd: cd,
                insUser: insUser,
                insUserName: $("#lblUserName").text(),
                chk_id: chk_id,
                tp_bar_cd1: newTpBarcode1,
                tp_bar_cd2: newTpBarcode2
            }
            ));

        if (rtv.result == "OK") {
            alert("OK：" + rtv.msg);
            $('#EDIT_TP_DIV').hide();
            return true;
        } else if (rtv.result == "WA") {
            alert("警告" + rtv.msg);
            return true;
        } else {
            alert(rtv.msg);
            return false;
        }

    }
}
