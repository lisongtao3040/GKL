
//共通AJAX 呼出函数
function WebMethod(asmxName, functionName, param) {
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
            if (isJSON(data.d)) {
                rtv = $.parseJSON(data.d);
            } else {
                rtv = data.d;
            }
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


function $$SET_G(key, value) {
    localStorage.setItem(key, value);
}
//(function () {
//    // 保存原始的 localStorage.setItem 方法
//    var originalSetItem = localStorage.setItem;

//    // 重写 localStorage.setItem 方法
//    localStorage.setItem = function (key, value) {
//        // 调用原始的 setItem 方法
//        originalSetItem.call(this, key, value);

//        //ByVal login_user_cd As String, ByVal key As String, ByVal value As String
//        var rtv = WebMethod("api", "SetServerStorage",
//                JSON.stringify({
//                    login_user_cd: localStorage.getItem('login_user_cd'),
//                    key: key,
//                    value: value
//                }
//            ));
//    };    
//})();

function $$LG(keyName) {
    //return localStorage.getItem(keyName) == null ? '' : localStorage.getItem(keyName);

    if (localStorage.getItem(keyName) == null) {
            var rtv = WebMethod("api", "GetServerStorage",
            JSON.stringify({
                login_user_cd: localStorage.getItem('login_user_cd'),
                key: keyName
            }    
        ));
        return rtv;

    } else {
        return localStorage.getItem(keyName);
    }
}


$(document).ready(function () {

	//localStorage.setItem("ffff","123");
	//var aaa =  $$LG("ffff");
	//alert(aaa);

    //============================================================================================================
    //关联托盘
    $("#joinTp_scmx").click(function (e) {
        $("#NEW_TP_DIV").show();

        var all_tps = $$LG("all_tps");
        var ac_tp = $$LG("Active_TpBarcode");

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
                    }
                }
            }
        }

        var all_tps_xiangxian = $$LG("all_tps_xiangxian");

        if (all_tps_xiangxian) {
            var tpXiangxians = all_tps_xiangxian.split(",");

            var i;
            for (i = 0; i <= tpXiangxians.length - 1; i++) {
                $("#xiangxian" + (i + 1)).text(tpXiangxians[i]);

            }
        }



    });

    //设置当前托盘文字
    $("#acTp").text($$LG("Active_TpBarcode"));

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
        var no = $("#lblMake_no").text().Trim();
        var cd = $("#lblCode").text().Trim();
        var rtv = WebMethod("Bg", "GetBgPanel",
                JSON.stringify({
                    no: no,
                    cd: cd,
                    IsBaogong: ($("#hidBaogong").val() == "1"),
                    IsGuanlianTuopan: ($("#hidTuopanLines").val() == "1")
                }
            ));

        TP_PANEL_INIT(rtv);

        var i;
        for (i = 1; i <= 6; i++) {
            $("#btnTp" + i).val($("#iptTpBarcode" + (i)).val() + '\n' + $("#xiangxian" + (i)).text());
            $("#btnTp" + i).attr("tp_no", $("#iptTpBarcode" + (i)).val());
        }

        //xiangxian5


        var all_tps = $$LG("all_tps");

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
        try{
            $(document).find(".msBgTPNO")[0].focus();
        } catch (e1) {

        }
        
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

        //setTimeout(function () { $('#btnComplete').click() }, 300);
        return true;
    });




});




//绑定PANEL
function TP_PANEL_INIT(rtv) {


    var line_cd = $("#lblLine_id").text().Trim();
    var insUser = $("#lblUser").text().Trim();
    var no = $("#lblMake_no").text().Trim();
    var cd = $("#lblCode").text().Trim();
    var chk_id = $("#hidChkNo").val();




    $("#gongdan").text(no);

    $("#baogong_panel").show();
    $("#baogong_panel_data").html(rtv.panelHTML);

    //自动设置第一个托盘


    $(".old_tp_ipt").val($$LG("Active_TpBarcode"));

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

        var no = $("#lblMake_no").text().Trim();
        var cd = $("#lblCode").text().Trim();
        var tp_bar_cd = $(this).parent().parent().find(".msBgTPNO").val().Trim();
        var tp_no = $(this).parent().parent().attr("tp_no").Trim();
        if (tp_no == "") {
            alert("托盘No为空");
            return false;
        }

        $$SET_G('Active_TpBarcode', tp_bar_cd);
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

        if (rtvTP.result == "OK") {
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
            return true;

        } else {
            alert("NG：" + rtvTP.msg);
            return true;
        }
    });

    //点击 明细-【关联】
    $(".msBgTPNO_GL").click(function (e) {
        var tp_bar_cd = $(this).parent().parent().find(".msBgTPNO").val().Trim();
        var tp_no = $(this).parent().parent().attr("tp_no").Trim();
        if (tp_bar_cd == "") {
            alert("请扫描托盘码");
            return false;
        }
        $$SET_G('Active_TpBarcode', tp_bar_cd);
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

        if (rtvTP.result == "OK") {
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
            return true;
        } else {
            alert("NG：" + rtvTP.msg);
            var rtv = WebMethod("Bg", "GetBgPanel",
                JSON.stringify({
                    no: no,
                    cd: cd,
                    IsBaogong: ($("#hidBaogong").val() == "1"),
                    IsGuanlianTuopan: ($("#hidTuopanLines").val() == "1")
                }
                ));
            TP_PANEL_INIT(rtv);
            return true;
        }



    });

    //点击 明细-【修改】
    $(".msBgTPNO_EDIT").click(function (e) {
        var tp_bar_cd = $(this).parent().parent().find(".msBgTPNO").val().Trim();
        var tp_no = $(this).parent().parent().attr("tp_no").Trim();
        if (tp_bar_cd == "") {
            alert("请扫描托盘码");
            return false;
        }

        $$SET_G('Active_TpBarcode', tp_bar_cd);
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

        if (rtvTP.result == "OK") {
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
            return true;
        } else {
            alert("NG：" + rtvTP.msg);
            var rtv = WebMethod("Bg", "GetBgPanel",
                JSON.stringify({
                    no: no,
                    cd: cd,
                    IsBaogong: ($("#hidBaogong").val() == "1"),
                    IsGuanlianTuopan: ($("#hidTuopanLines").val() == "1")
                }
                ));
            TP_PANEL_INIT(rtv);
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

    var line_cd = $("#lblLine_id").text().Trim();
    var insUser = $("#lblUser").text().Trim();
    var no = $("#lblMake_no").text().Trim();
    var cd = $("#lblCode").text().Trim();
    var chk_id = $("#hidChkNo").val();
    //var rtv = false;

    // $("#hidBaogong").val() == "1" ||
    //if ( $("#hidTuopanLines").val() == "1") {

    if ($("#hidTuopanLines").val() == "1" && $$LG("Active_TpBarcode").Trim() == "") {
        alert("请扫描托盘码");
        return false;
    } else {
        //$$SET_G('Active_TpBarcode', newTpBarcode);
    }

    var rtv = WebMethod("Bg", "JoinTP_InsertTrayData",
        JSON.stringify({
            line_cd: line_cd,
            no: no,
            cd: cd,
            insUser: insUser,
            insUserName: $("#lblUserName").text(),
            chk_id: chk_id,
            tp_bar_cd: $$LG("Active_TpBarcode")
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

    var line_cd = $("#lblLine_id").text().Trim();
    var insUser = $("#lblUser").text().Trim();
    var no = $("#lblMake_no").text().Trim();
    var cd = $("#lblCode").text().Trim();
    var chk_id = $("#hidChkNo").val();
    //var rtv = false;



    if ($("#hidTuopanLines").val() == "1" && $$LG("Active_TpBarcode").Trim() == "") {
        alert("请扫描托盘码");
        return false;
    } else {
        //$$SET_G('Active_TpBarcode', newTpBarcode);
    }


    var rtv = WebMethod("Bg", "JoinTP_InsertTrayData",
        JSON.stringify({
            line_cd: line_cd,
            no: no,
            cd: cd,
            insUser: insUser,
            insUserName: $("#lblUserName").text(),
            chk_id: chk_id,
            tp_bar_cd: $$LG("Active_TpBarcode")
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
        $$SET_G('all_tps_xiangxian', rtv.txt);
    }

}

//设定新的托盘码
function SetNewTpBarcode(obj) {
    var idx = $(obj).attr("idx");
    var newTpBarcode = $("#iptTpBarcode" + idx).val();
    var i;

    if ($("#hidTuopanLines").val() == "1" && newTpBarcode.Trim() == "") {
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
        $$SET_G('all_tps', tps.join(','));

        if ($("#defaultTpTd" + idx).text() == '●') {
            $$SET_G('Active_TpBarcode', newTpBarcode.Trim());
        }

        //刷新向先
        SetXiangxian();

        alert("托盘码设已经置成：" + newTpBarcode);
        //$('#NEW_TP_DIV').hide();
    }
}
//设定默认盘码
function SetTpDefault(obj) {

    var idx = $(obj).attr("idx");
    var newTpBarcode = $("#iptTpBarcode" + idx).val();
    var i;

    if ($("#hidTuopanLines").val() == "1" && newTpBarcode.Trim() == "") {
        alert("请扫描托盘码");
        $("#iptTpBarcode" + idx).focus();
        $("#iptTpBarcode" + idx).select();
        return false;
    } else {
        var tps = [];
        for (i = 1; i <= 6; i++) {
            tps.push($("#iptTpBarcode" + (i)).val());
        }
        $$SET_G('all_tps', tps.join(','));
        $$SET_G('Active_TpBarcode', newTpBarcode.Trim());
        $("#acTp").text(newTpBarcode);

        $(".defaultTpTd").text('');
        $("#defaultTpTd" + idx).text('●');

        //alert("托盘码设已经置成：" + newTpBarcode);
        //$('#NEW_TP_DIV').hide();
    }
}

//清空托盘
function SetTpClear(obj) {

    var idx = $(obj).attr("idx");
    var newTpBarcode = '';

    //清空托盘行
    $("#iptTpBarcode" + idx).val('');

    $("#xiangxian" + idx).text('');

    //托盘按钮
    $("#btnTp" + idx).val('');
    $("#btnTp" + idx).attr("tp_no", '');
    var i;


    var tps = [];
    for (i = 1; i <= 6; i++) {
        tps.push($("#iptTpBarcode" + (i)).val());
    }
    $$SET_G('all_tps', tps.join(','));
    $$SET_G('Active_TpBarcode', newTpBarcode);
    $("#acTp").text(newTpBarcode);


    //如果是默认托盘 那么清空
    if ($("#defaultTpTd" + idx).text() == '●') {
        //默认列清空
        $(".defaultTpTd").text('');
        alert("请注意默认托盘为空！！！");
    }

    $("#iptTpBarcode" + idx).focus();
    //$('#NEW_TP_DIV').hide();

}




//修改托盘、生产明细书对应关系
function UpdTpBarcode() {
    var line_cd = $("#lblLine_id").text().Trim();
    var insUser = $("#lblUser").text().Trim();
    var no = $("#lblMake_no").text().Trim();
    var cd = $("#lblCode").text().Trim();
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