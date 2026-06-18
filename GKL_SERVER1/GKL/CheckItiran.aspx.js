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

// 全局状态锁，双重保险
var isProcessing = false;

// 显示遮罩
var showOverlay = function () {
    var overlay = document.getElementById('loadingOverlay');
    if (!overlay) return;

    // 兼容 IE6：计算页面的总宽高（包括滚动部分）
    var doc = document.documentElement;
    var body = document.body;
    var w = Math.max(doc.scrollWidth, body.scrollWidth, doc.clientWidth);
    var h = Math.max(doc.scrollHeight, body.scrollHeight, doc.clientHeight);

    overlay.style.width = w + "px";
    overlay.style.height = h + "px";
    overlay.style.display = 'block';
};

// 隐藏遮罩
var hideOverlay = function () {
    var overlay = document.getElementById('loadingOverlay');
    if (overlay) {
        overlay.style.display = 'none';
    }
};

// 业务逻辑
var submitData = function (btn) {
    if (isProcessing) return; // 逻辑锁：防止狂点

    isProcessing = true;
    showOverlay(); // UI 锁：覆盖屏幕
};

$(document).ready(function () {

    try {
        window.external.SetSnapFalse();
    } catch (e) {

    }


    var okSuu = 0;
    var ngSuu = 0;
    var allSuu = 0;
    var completeSuu = 0;

    var hidOldChkNo = $("#hidOldChkNo").val();
    //默认行状态
    $(".jq_ms tr").each(function () {
        chk_method = $(this).attr("chk_method");
        if ($(this).find("td")[6].innerText == "NG") {
            $($(this).find("td")[6]).css('background-color', 'red');
        } else if ($(this).find("td")[6].innerText == "OK") {
            $($(this).find("td")[6]).css('background-color', 'green');
        }



    });

    if (hidOldChkNo != "") {
        $(".jq_ms tr").each(function () {
            //if ($(this).attr("chk_no") == hidOldChkNo) {
if ($(this).attr("chk_no") == hidOldChkNo) {
                var that = this;
                setTimeout(function () { $(that).focus(); $(that).click(); }, 300);
                return false;
            }
        });
    }


    //合计
    check_setume_txt();

    //统计检查明细结果
    function check_setume_txt() {
        //var okSuu = 0;
        //var ngSuu = 0;
        //var allSuu = 0;
        //var completeSuu = 0;

        $(".jq_ms tr").each(function () {
            if ($(this).find("td")[6].innerText == "NG") {
                ngSuu++;
            } else if ($(this).find("td")[6].innerText == "OK") {
                okSuu++;
            }
            allSuu++;
            if ($(this).find("td")[9].innerText == "完了") {
                completeSuu++;
            }
        });

        $("#lblSou").text("【NG】:" + ngSuu + "--【OK】:" + okSuu + "--【完了】:" + completeSuu + "--【全部】:" + allSuu);

        if (okSuu == allSuu) {
            $("#lblSou").css('color', 'green');
        } else {
            $("#lblSou").css('color', 'red');
        }
    }

    //如果扫描生产明细书，那么直接进入画面
    if (allSuu == 1 && $("#hidScanFlg").val() == "1") {
        $(".jq_ms tr")[0].click();
        if ($(".jq_ms tr")[0].innerText.indexOf("待检查") >= 0) {
            $('#btnInsert')[0].click();
        } else {
            $('.jq_upd')[0].click();
        }
    }




    //默认按钮状态
    DisabledIt($('.jq_upd')[0]);
    DisabledIt($('.jq_del')[0]);
    DisabledIt($('#btnComlete')[0]);

    //行选择
    $(".jq_ms tr").click(function () {
        //
        if ($(this)[0].innerText.indexOf("待检查") == -1) {
            UndisabledIt($('.jq_upd')[0]);
            UndisabledIt($('.jq_del')[0]);
            UndisabledIt($('#btnComlete')[0]);
        } else {
            DisabledIt($('.jq_upd')[0]);
            DisabledIt($('.jq_del')[0]);
            DisabledIt($('#btnComlete')[0]);
        }

        if ($("#hidIsQiangzhi_baogong_lines").val() == "1") {
            UndisabledIt($('#btnComlete')[0]);
        }

        if ($("#hidIsMyLine").val() != '1') {
            UndisabledIt($('.jq_del')[0]);
            UndisabledIt($('#btnComlete')[0]);
            //DisabledIt($('#btnInsert')[0]);
        }

        // 获取要生成一维码的数据
        var barcodeData = $(this).find(".jq_make_no").text();

        // 获取SVG元素
        var barcodeElement = document.getElementById("barcodeNo");

        // 使用JsBarcode生成一维码
        JsBarcode(barcodeElement, barcodeData, {
            format: "CODE128",  // 选择一维码类型
            width: 2,           // 条形码宽度
            height: 60,         // 条形码高度
        });


        // 获取要生成一维码的数据
        barcodeData = $(this).find(".jq_code").text();

        // 获取SVG元素
        barcodeElement = document.getElementById("barcodeCD");

        // 使用JsBarcode生成一维码
        JsBarcode(barcodeElement, barcodeData, {
            format: "CODE128",  // 选择一维码类型
            width: 2,           // 条形码宽度
            height: 60,         // 条形码高度
        });

        // 获取SVG元素
        //barcodeElement = document.getElementById("barcodeSCMX");

        //// 使用JsBarcode生成一维码
        //JsBarcode(barcodeElement, barcodeData, {
        //    format: "qrcode",  // 选择一维码类型
        //    width: 60,           // 条形码宽度
        //    height: 60,         // 条形码高度
        //});
        document.getElementById("barcodeSCMX").innerHTML = "";

        try {


            //scmx
            var scmxTxt = $.ajax({
                url: "AJAX.aspx?a=" + new Date()
                    + "&kbn=scmx"
                    + "&cd=" + $(this).find(".jq_code").text()
                    + "&no=" + $(this).find(".jq_make_no").text()
                    + "&line_id=" + $("#tbxLineId").val()
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



    });

    //图片Disable
    DisabledIt($("#lblUserName"));
    $("#lblUserName").attr("tabindex", "-1");

    ////User名
    //document.onsubmit = function () {
    //    UndisabledIt($("#lblUserName"));
    //}

    /* 得到日期年月日等加数字后的日期 */
    Date.prototype.dateAdd = function (interval, number) {
        var d = this;
        var k = { 'y': 'FullYear', 'q': 'Month', 'm': 'Month', 'w': 'Date', 'd': 'Date', 'h': 'Hours', 'n': 'Minutes', 's': 'Seconds', 'ms': 'MilliSeconds' };
        var n = { 'q': 3, 'w': 7 };
        eval('d.set' + k[interval] + '(d.get' + k[interval] + '()+' + ((n[interval] || 1) * number) + ')');
        return d;
    }

    /* 计算两日期相差的日期年月日等 */
    Date.prototype.dateDiff = function (interval, objDate2) {
        var d = this, i = {}, t = d.getTime(), t2 = objDate2.getTime();
        i['y'] = objDate2.getFullYear() - d.getFullYear();
        i['q'] = i['y'] * 4 + Math.floor(objDate2.getMonth() / 4) - Math.floor(d.getMonth() / 4);
        i['m'] = i['y'] * 12 + objDate2.getMonth() - d.getMonth();
        i['ms'] = objDate2.getTime() - d.getTime();
        i['w'] = Math.floor((t2 + 345600000) / (604800000)) - Math.floor((t + 345600000) / (604800000));
        i['d'] = Math.floor(t2 / 86400000) - Math.floor(t / 86400000);
        i['h'] = Math.floor(t2 / 3600000) - Math.floor(t / 3600000);
        i['n'] = Math.floor(t2 / 60000) - Math.floor(t / 60000);
        i['s'] = Math.floor(t2 / 1000) - Math.floor(t / 1000);
        return i[interval];
    }

    //日期格式
    Date.prototype.FormatDateYMD = function () {
        var date = this;
        var seperator1 = "/";
        var year = date.getFullYear();
        var month = date.getMonth() + 1;
        var strDate = date.getDate();
        if (month >= 1 && month <= 9) {
            month = "0" + month;
        }
        if (strDate >= 0 && strDate <= 9) {
            strDate = "0" + strDate;
        }
        var currentdate = year + seperator1 + month + seperator1 + strDate;
        return currentdate;
    }


    //更新 登录
    $("#btnUpdate,#btnInsert").click(function () {

        //逻辑锁：防止狂点
        submitData();

        var obj;
        obj = $("#tbxCheckUser");

        if ($(obj).val() == "") {
            alert("用户不存在");
            setTimeout(function () { obj.focus(); }, 100);
            hideOverlay();
            return false;
        }

        if ($("#tbxMakeNo_key").val() == "" || $("#tbxCode_key").val() == "") {
            alert("作番/CODE不存在");
            setTimeout(function () { $("#tbxMakeNo_key").focus(); }, 100);
            hideOverlay();
            return false;
        }

        htmlobj = $.ajax({ url: "AJAX.aspx?a=" + new Date() + "&kbn=user&user_cd=" + $(obj).val() + "&line_id=" + $("#tbxLineId").val(), async: false });
        if (htmlobj.responseText == "") {
            alert("用户不存在");
            setTimeout(function () { obj.focus(); }, 100);
            hideOverlay();
            return false;
        } else {
            //$("#lblUserName").val(htmlobj.responseText.split(",")[0]);
            //$("#tbxLineId_key").val(htmlobj.responseText.split(",")[1]);
        }

        setTimeout(function () { hideOverlay(); }, 2000);
        return true;

    });

    $("#btnDelete,#btnComlete").click(function () {

        //逻辑锁：防止狂点
        submitData();
        setTimeout(function () { hideOverlay(); }, 2000);
        return true;

    });




    $("#btnSelect").click(function () {
        var obj;
        obj = $("#tbxCheckUser");

        if ($(obj).val() == "") {
            alert("用户不存在");
            setTimeout(function () { obj.focus(); }, 100);
            return false;
        }

        htmlobj = $.ajax({ url: "AJAX.aspx?a=" + new Date() + "&kbn=user&user_cd=" + obj.val() + "&line_id=" + $("#tbxLineId_key").val(), async: false });
        if (htmlobj.responseText == "") {
            alert("用户不存在");
            return false;
        } else {

            $("#tbxMakeNo_key")[0].select();
        }


    });


    //纵览
    $("#zongl").click(function (e) {
        window.open("apexcharts/Default.aspx?line_id=" + $("#tbxLineId_key").val() + "&chk_date=" + $("#tbxDate_key").val())
    });

    $("#tbxCheckUser,#tbxMakeNo_key,#tbxCode_key,#tbxDate_key").focus(function (e) {
        $(this)[0].select();
    });

    //作番 : 
    $("#tbxMakeNo_key").keydown(function (e) {
        if (e.keyCode == 13) {
            $("#tbxCode_key")[0].select();
            ScanBarCode(this);
            e.preventDefault();
            return false;
        }
    });
    $("#tbxCode_key").keydown(function (e) {
        if (e.keyCode == 13) {
            $("#btnInsert")[0].focus();
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
            $("#btnSelect2")[0].click();
        }



    }


    $.ajax({
        type: 'POST',
        url: './AJAX.aspx?kbn=userlist&line_id=' + $("#tbxLineId_key").val(),
        async: true, //true:yibu
        datatype: 'html',//'xml', 'html', 'script', 'json', 'jsonp', 'text'.
        //when complete
        complete: function (XMLHttpRequest, textStatus) {
            $("#userlist")[0].innerHTML = XMLHttpRequest.responseText;
        }
    });

});

