
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

$(document).ready(function () {

    //============================================================================================================
    //关联托盘
    $("#joinTp_scmx").click(function (e) {
        $("#NEW_TP_DIV").show();
        $("#txtNewTpBarcode").val($$LG("Active_TpBarcode"));
        $("#txtNewTpBarcode").focus();
        $("#txtNewTpBarcode").select();
    });

    //设置当前托盘文字
    $("#acTp").text($$LG("Active_TpBarcode"));

    $(".tp_barcode").keydown(function (e) {
        if (event.keyCode == 13) {
            var cd = $(this).val();
            if (cd.split("/").length == 3) {
                $(this).val(cd.split("/")[2]);
            }
            e.preventDefault();
        }
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
                IsBaogong: ($("#hidBaogong").val() == "1")
            }
            ));

        TP_PANEL_INIT(rtv);

    });


    //如果报工没打开
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


    var line_cd = $("#lblLine_id").text().Trim();
    var insUser = $("#lblUser").text().Trim();
    var no = $("#lblMake_no").text().Trim();
    var cd = $("#lblCode").text().Trim();
    var chk_id = $("#hidChkNo").val();




    $("#gongdan").text(no);

    $("#baogong_panel").show();
    $("#baogong_panel_data").html(rtv.panelHTML);

    //自动设置第一个托盘
    //if ($(".msBgTPNO:eq(0)").val() == "" && $(".msBgTPNO:eq(0)").parent().index() == 4) {
    //    $(".msBgTPNO:eq(0)").val($$LG("Active_TpBarcode"));
    //}

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
        var tp_bar_cd = $(this).parent().parent().find(".msBgTPNO").val();
        var tp_no = $(this).parent().parent().attr("tp_no");
        if (tp_no == "") {
            alert("托盘No为空");
            return false;
        }

        localStorage.setItem('Active_TpBarcode', tp_bar_cd);
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
        var tp_bar_cd = $(this).parent().parent().find(".msBgTPNO").val();
        var tp_no = $(this).parent().parent().attr("tp_no");
        if (tp_bar_cd == "") {
            alert("请扫描托盘码");
            return false;
        }
        localStorage.setItem('Active_TpBarcode', tp_bar_cd);
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
        var tp_bar_cd = $(this).parent().parent().find(".msBgTPNO").val();
        var tp_no = $(this).parent().parent().attr("tp_no");
        if (tp_bar_cd == "") {
            alert("请扫描托盘码");
            return false;
        }

        localStorage.setItem('Active_TpBarcode', tp_bar_cd);
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

    if ($$LG("Active_TpBarcode") == "") {
        alert("请扫描托盘码");
        return false;
    } else {
        //localStorage.setItem('Active_TpBarcode', newTpBarcode);
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



    if ($$LG("Active_TpBarcode") == "") {
        alert("请扫描托盘码");
        return false;
    } else {
        //localStorage.setItem('Active_TpBarcode', newTpBarcode);
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

function $$LG(keyName) {
    return localStorage.getItem(keyName) == null ? '' : localStorage.getItem(keyName);
}


//设定新的托盘码
function SetNewTpBarcode() {

    var newTpBarcode = $("#txtNewTpBarcode").val();
    if (newTpBarcode == "") {
        alert("请扫描托盘码");
        return false;
    } else {
        localStorage.setItem('Active_TpBarcode', newTpBarcode);
        $("#acTp").text(newTpBarcode);
        alert("托盘码设已经置成：" + newTpBarcode);
        $('#NEW_TP_DIV').hide();
    }
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