
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

$(document).ready(function () {

    var putYXLD_CNT = 0;
    var GetYXLD_RLT = function (cd, no, line) {
        var rtv = WebMethod("api", "GetYXLD_RLT",
            JSON.stringify({
                no: no,
                cd: cd,
                line: line
            }
            ));

        if (putYXLD_CNT > 1000) {
            $("#btnZB")[0].disabled = false;
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
            $("#rtv0").text("影像检查结果获取失败");
            $("#btnZB").val("1.准备影像检");

        } else {
            $("#btnZB")[0].disabled = false;
            $("#rtv0").text("影像检查结果获取成功：" + rtv);
            $("#btnZB").val("1.准备影像检");
        }
    }


    //准备按钮按下
    $("#btnZB").click(function () {
        var cd = $("#tbxCode_key").val();
        var no = $("#tbxMakeNo_key").val();
        var line = $("#hidPlanLineId").val();
        putYXLD_CNT = "0";
        $("#rtv1").text("");
        $("#rtv2").text("");
        $("#btnZB")[0].disabled = true;
        var rtv = WebMethod("api", "SetJunbiYXLD",
            JSON.stringify({
                no: no,
                cd: cd,
                line: line
            }
            ));

        $("#rtv0").text(rtv);

        if (rtv == "OK") {
            $("#btnZB").val("1.准备影像检查开始接收!!!");
            GetYXLD_RLT(cd, no, line);
            $("#rtv0").text(rtv + "：等待影像结果");

        } else {
            $("#btnZB").val("1.准备影像检");
            $("#btnZB")[0].disabled = false;
            alert(rtv);
        }

    });

    //准备按钮按下
    $("#btnSv1").click(function () {
        var line = $("#hidPlanLineId").val();

        var rtv = WebMethod("api", "GetYXLD",
            JSON.stringify({
                line: line
            }
            ));


        $("#rtv1").text("接口1结果：" + rtv);

    });

    //准备按钮按下
    $("#btnSv2").click(function () {
        var cd = $("#tbxCode_key").val();
        var no = $("#tbxMakeNo_key").val();
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

        $("#rtv2").text("接口2结果："+rtv);

    });

    //


});