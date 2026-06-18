$(document).ready(function () {

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


    var arrStation = $("#hidStationNo").val().split("|");

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
    //SetStation("asdfasdf", "这是一个动态创建的 DIV");
});
