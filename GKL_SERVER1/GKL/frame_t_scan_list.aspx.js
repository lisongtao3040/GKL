/*
 * frame_t_scan_list.aspx.js
 * t_scan_list 扫描一览画面
 * IE6/7 でも動作するように XMLHttpRequest → ActiveXObject の順で生成する
 */

//AJAX通信先
var SCAN_LIST_AJAX_URL = './frame_t_scan_list_ajax.aspx';

//二重送信防止フラグ
var scanSubmitting = false;

/* XMLHttpRequestを生成する（IE6/7はActiveXObjectを使用する） */
function createXmlHttpRequest() {
    var xmlHttp = null;

    if (window.XMLHttpRequest) {
        try {
            xmlHttp = new XMLHttpRequest();
        } catch (e1) {
            xmlHttp = null;
        }
    }

    if (xmlHttp == null && window.ActiveXObject) {
        try {
            xmlHttp = new ActiveXObject('Msxml2.XMLHTTP');
        } catch (e2) {
            try {
                xmlHttp = new ActiveXObject('Microsoft.XMLHTTP');
            } catch (e3) {
                xmlHttp = null;
            }
        }
    }

    return xmlHttp;
}

/* 前後の空白を除去する */
function trimValue(value) {
    if (value == null) {
        return '';
    }
    return String(value).replace(/^[\s\u3000]+/, '').replace(/[\s\u3000]+$/, '');
}

/* 要素を取得する */
function getElement(elementId) {
    return document.getElementById(elementId);
}

/* 文字列を設定する（IE8以下はinnerTextを使用する） */
function setElementText(element, value) {
    var text = (value == null) ? '' : String(value);
    if (typeof element.textContent != 'undefined') {
        element.textContent = text;
    } else {
        element.innerText = text;
    }
}

/* メッセージを表示する */
function showMessage(message, isError) {
    var msgElement = getElement('lblMsg');
    if (msgElement == null) {
        return;
    }
    setElementText(msgElement, message);
    msgElement.className = (isError == true) ? 'scan-message scan-message-error' : 'scan-message scan-message-info';
}

/* 画面のURLパラメータを取得する */
function getScreenParams() {
    return {
        chk_no: getElement('hidChkNo').value,
        line_id: getElement('hidLineId').value,
        make_no: getElement('hidMakeNo').value,
        code: getElement('hidCode').value,
        user: getElement('hidUser').value
    };
}

/* クエリ文字列を生成する */
function buildQueryString(params) {
    var queryList = [];
    var key = '';
    var value = '';

    for (key in params) {
        if (params.hasOwnProperty(key)) {
            value = (params[key] == null) ? '' : String(params[key]);
            queryList[queryList.length] = encodeURIComponent(key) + '=' + encodeURIComponent(value);
        }
    }

    return queryList.join('&');
}

/* JSON文字列をオブジェクトに変換する */
function parseJsonText(jsonText) {
    if (window.JSON && typeof window.JSON.parse == 'function') {
        return window.JSON.parse(jsonText);
    }
    return eval('(' + jsonText + ')');
}

/* AJAX(POST)通信を行う */
function ajaxPost(params, onSuccess, onError) {
    var xmlHttp = createXmlHttpRequest();

    if (xmlHttp == null) {
        showMessage('AJAXの初期化に失敗しました。', true);
        return;
    }

    //IEのキャッシュ対策
    params._ts = new Date().getTime();

    xmlHttp.open('POST', SCAN_LIST_AJAX_URL, true);
    xmlHttp.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState != 4) {
            return;
        }
        if (xmlHttp.status == 200) {
            if (onSuccess) {
                onSuccess(xmlHttp.responseText);
            }
        } else {
            if (onError) {
                onError(xmlHttp.status);
            } else {
                showMessage('通信エラーが発生しました。(' + xmlHttp.status + ')', true);
            }
        }
    };

    try {
        xmlHttp.send(buildQueryString(params));
    } catch (e) {
        showMessage('通信エラーが発生しました。', true);
    }
}

/* エラーメッセージを取り出す（"ERR:xxx" → "xxx"） */
function getErrorMessage(responseText) {
    var text = trimValue(responseText);
    if (text.indexOf('ERR:') == 0) {
        return text.substring(4);
    }
    return '';
}

/* 一覧を描画する */
function renderScanList(jsonText) {
    var responseData = null;

    try {
        responseData = parseJsonText(jsonText);
    } catch (e) {
        showMessage('一覧の解析に失敗しました。', true);
        return;
    }

    var tbody = getElement('tbodyScanList');
    var rows = (responseData != null && responseData.rows != null) ? responseData.rows : [];

    //既存行を削除する
    while (tbody.rows.length > 0) {
        tbody.deleteRow(0);
    }

    //データなし
    if (rows.length == 0) {
        var emptyRow = tbody.insertRow(-1);
        var emptyCell = emptyRow.insertCell(-1);
        emptyCell.colSpan = 5;
        emptyCell.className = 'scan-empty';
        setElementText(emptyCell, '数据不存在。');
        return;
    }

    for (var rowIndex = 0; rowIndex < rows.length; rowIndex++) {
        appendScanRow(tbody, rows[rowIndex]);
    }
}

/* 一覧の1行を追加する */
function appendScanRow(tbody, rowData) {
    var resultValue = (rowData.result == null) ? '' : String(rowData.result);

    var row = tbody.insertRow(-1);
    if (resultValue == 'NG') {
        row.className = 'scan-row-ng';
    }

    //扫码值
    var scanTxtCell = row.insertCell(-1);
    setElementText(scanTxtCell, rowData.scan_txt);
    scanTxtCell.title = (rowData.scan_txt == null) ? '' : String(rowData.scan_txt);

    //结果
    var resultCell = row.insertCell(-1);
    resultCell.className = 'scan-result scan-result-' + resultValue.toLowerCase();
    setElementText(resultCell, resultValue);

    //更新者
    setElementText(row.insertCell(-1), rowData.upd_user);

    //更新日
    setElementText(row.insertCell(-1), rowData.upd_date);

    //操作（NGのみ「维护」ボタンを表示する）
    var actionCell = row.insertCell(-1);
    actionCell.className = 'scan-col-action';
    if (resultValue == 'NG') {
        actionCell.appendChild(createMaintainButton(rowData.scan_id));
    }
}

/* 「维护」ボタンを生成する */
function createMaintainButton(scanId) {
    var button = document.createElement('input');
    button.type = 'button';
    button.value = '维护';
    button.className = 'scan-maintain-button';
    button.onclick = function () {
        maintainScanResult(scanId);
    };
    return button;
}

/* NGの行をRNへ更新する */
function maintainScanResult(scanId) {
    if (!window.confirm('将结果变更为 RN。可以吗？')) {
        return;
    }

    var params = getScreenParams();
    params.kbn = 'upd';
    params.scan_id = scanId;

    ajaxPost(params, function (responseText) {
        var resultText = trimValue(responseText);
        if (resultText == 'true') {
            showMessage('已把结果变更为 RN。', false);
        } else {
            showMessage('更新失败。' + getErrorMessage(resultText), true);
        }
        loadScanList();
    });
}

/* 一覧を取得して描画する */
function loadScanList() {
    var params = getScreenParams();
    params.kbn = 'list';

    ajaxPost(params, function (responseText) {
        if (trimValue(responseText).indexOf('ERR:') == 0) {
            showMessage('取得失败。' + getErrorMessage(responseText), true);
            return;
        }
        renderScanList(responseText);
    });
}

/* 扫码値を登録する */
function submitScan() {
    if (scanSubmitting) {
        return;
    }

    var scanElement = getElement('tbxScanTxt');
    var scanTxt = trimValue(scanElement.value);
    if (scanTxt == '') {
        scanElement.focus();
        return;
    }

    scanSubmitting = true;
    scanElement.value = '';

    var params = getScreenParams();
    params.kbn = 'ins';
    params.scan_txt = scanTxt;

    ajaxPost(params, function (responseText) {
        scanSubmitting = false;

        var resultText = trimValue(responseText);
        if (resultText == 'OK') {
            showMessage('登录完成：OK', false);
        } else if (resultText == 'NG') {
            showMessage('登录完成：NG', true);
        } else {
            showMessage('登录失败。' + getErrorMessage(resultText), true);
        }

        focusScanBox();
        loadScanList();
    }, function (status) {
        scanSubmitting = false;
        showMessage('通信エラーが発生しました。(' + status + ')', true);
        focusScanBox();
    });
}

/* 画面パラメータのチェック */
function checkScreenParams() {
    var params = getScreenParams();
    var missingList = [];

    if (trimValue(params.chk_no) == '') {
        missingList[missingList.length] = 'chk_no';
    }
    if (trimValue(params.line_id) == '') {
        missingList[missingList.length] = 'line_id';
    }
    if (trimValue(params.make_no) == '') {
        missingList[missingList.length] = 'make_no';
    }
    if (trimValue(params.code) == '') {
        missingList[missingList.length] = 'code';
    }

    if (missingList.length > 0) {
        showMessage('URL参数不足（' + missingList.join(' / ') + '）。', true);
        return false;
    }

    return true;
}

/* 扫码入力欄にフォーカスを戻す */
function focusScanBox() {
    var scanElement = getElement('tbxScanTxt');
    if (scanElement == null) {
        return;
    }
    try {
        scanElement.focus();
    } catch (e) {
        //無視する
    }
}

/* 画面を閉じる（iframe内の場合はiframeを非表示にする） */
function closeScreen() {
    var frameElement = null;

    try {
        frameElement = window.frameElement;
    } catch (e1) {
        frameElement = null;
    }

    if (frameElement != null) {
        //親画面に閉じる処理が用意されている場合はそれを使用する
        try {
            if (window.parent != null && typeof window.parent.onScanListClose == 'function') {
                window.parent.onScanListClose();
                return;
            }
        } catch (e2) {
            //クロスドメインの場合は無視する
        }

        try {
            frameElement.style.display = 'none';
            return;
        } catch (e3) {
            //無視する
        }
    }

    //単独ウィンドウの場合はウィンドウを閉じる
    try {
        window.close();
    } catch (e4) {
        //無視する
    }
}

/* 初期化 */
function initScanList() {
    var scanElement = getElement('tbxScanTxt');

    //Enterキーで扫码値を登録する
    scanElement.onkeydown = function (eventObject) {
        var eventData = eventObject || window.event;
        var keyCode = eventData.keyCode || eventData.which;

        if (keyCode == 13) {
            if (eventData.preventDefault) {
                eventData.preventDefault();
            } else {
                eventData.returnValue = false;
            }
            submitScan();
            return false;
        }
        return true;
    };



    //スキャナがTabで終了する場合にも対応する
    //（フォーカスが外れたら登録し、入力欄へフォーカスを戻す）
    scanElement.onblur = function () {
        submitScan();
        window.setTimeout(focusScanBox, 150);
    };

    focusScanBox();

    if (checkScreenParams()) {
        loadScanList();
    }
}

window.onload = initScanList;
