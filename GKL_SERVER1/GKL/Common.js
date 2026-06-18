
String.prototype.Trim = function () {
    var value = this.replace(/^\s+|\s+$/g, "");
    return value.replace(/(^　*)|(　*$)/g, "");
}
String.prototype.Right = function (lngLen) {
    if (this.length - lngLen >= 0 && this.length >= 0 && this.length - lngLen <= this.length) {
        return this.substring(this.length - lngLen, this.length)
    } else {
        return null
    }
}
String.prototype.Left = function (lngLen) {
    return this.substring(0, lngLen);
}


function right(mainStr, lngLen) {
    if (mainStr.length - lngLen >= 0 && mainStr.length >= 0 && mainStr.length - lngLen <= mainStr.length) {
        return mainStr.substring(mainStr.length - lngLen, mainStr.length)
    } else {
        return null
    }
}

function m_chkFoundSymbol(strInput) {
    var char1;
    for (i = 0; i < strInput.length; i++) {
        char1 = strInput.charAt(i);
        if (char1 == "'" || char1 == '"' || char1 == "&" || char1 == '<' || char1 == '>' || char1 == ',') {
            return true;
            break;
        }
    }
    return false;
}

function fncGetLengthB(data, keta) {
    var ch1;
    var ch2;
    var intLen = 0;
    var wkstr = data;
    for (i = 0; i < wkstr.length; i++) {
        ch1 = wkstr.charCodeAt(i);
        if (ch1 >= 0 && ch1 <= 255) {
            intLen = intLen + 1;
        } else {
            intLen = intLen + 2;
        }
    }
    if (parseInt(intLen, 10) > parseInt(keta, 10)) {
        return false;
    } else {
        return true
    }
}


/* ****************************************************************************
	関数名 isHalfNumber ()

		作成日 : 1998/3/25 (S.Yano)
		引数   : 1番目   -> チェックする文字列
		戻り値 : true    -> 1番目の引数がすべて半角数値の時
				 false   -> true以外の時

**************************************************************************** */
function isHalfNumber() {
    var i;
    var sBuffer
    if (isHalfNumber.arguments.length != 1) {
        alert("isHalfNumberへの引数の数が間違っています");
        return false;
    }
    for (i = 0; i < isHalfNumber.arguments[0].length; i++) {
        sBuffer = isHalfNumber.arguments[0].substring(i, i + 1);
        if (!(sBuffer >= "0" && sBuffer <= "9")) {
            return false;
        }
    }
    return true;
}


/**
 * マイナス符号付小数点チェックを行います。
 * ・パターンマッチ 0～9、"-"、"."
 * ・パターンマッチ "-","."のみはＮＧ
 * ・マイナス符号の数 0 or 1
 * ・小数点の数 0 or 1
 * ・整数部の桁数チェック
 * ・小数部の桁数チェック
 *
 * 例）整数部 3, 小数部 2の場合
 * -123 --> OK
 * 123 --> OK
 * 123.45 --> OK
 * -123.45 --> OK
 * 1.2 --> OK
 * - --> NG
 * 123- --> NG
 * --123 --> NG
 * 1234.56 --> NG
 * 123.456 --> NG
 * . --> NG
 * .123 --> NG
 * 123. --> NG
 * -. --> NG
 *
 * @param argValue チェック対象文字列
 * @param argIntKetasu 整数部の入力可能桁数
 * @param argDecimalKetasu 小数部の入力可能桁数
 * @return チェックＯＫの場合はtrue、
 * チェックＮＧの場合はfalse
 */
function isMinusDecimal(argValue, argIntKetasu, argDecimalKetasu) {
    var minusFlg = false;
    var decFlg = false;
    if (argValue.match(/[^0-9|^\-|^.]/g)) {
        // パターンマッチ 0～9,"-","."以外はＮＧ
        return false;
    }
    if (argValue.match(/[^\-|^.]/g)) {
    }
    else {
        // パターンマッチ "-","."のみはＮＧ
        return false;
    }

    // 小数点の数を取得する
    var count = 0;
    for (var i = 0; i < argValue.length; i++) {
        if (argValue.charAt(i) == ".") {
            decFlg = true; count++;
        }
    }
    if (2 <= count) {
        // "."が２つ以上入力されている場合はＮＧ
        return false;
    }

    // "-"の入力個数を取得する
    count = 0;
    for (i = 0; i < argValue.length; i++) {
        if (argValue.charAt(i) == "-") {
            minusFlg = true;
            count++;
        }
    }
    if (2 <= count || (count == 1 && argValue.charAt(0) != "-")) {
        // "-"が２つ以上入力されている場合はＮＧ
        // "-"が入力されていて、かつ先頭に"-"がない場合はＮＧ
        return false;
    }

    // 小数点以下のチェック
    if (decFlg) {
        // 小数点以下の桁数チェック
        var idx = argValue.lastIndexOf(".");
        var decimalPart = argValue.substring(idx);

        // 小数点以下の桁数を取得する
        var length = decimalPart.length - 1;
        if (length == 0) {
            // 小数点以下の入力がない場合はＮＧ
            return false;
        }
        if (argDecimalKetasu < length) {
            // 小数点以下の桁数がオーバーしている場合はＮＧ
            return false;
        }
    }

    // 整数部の桁数チェック
    var intPart = "";
    length = 0;
    if (decFlg) {
        // 小数点が入力された場合
        intPart = argValue.substring(0, argValue.indexOf("."));
    }
    else {
        intPart = argValue;
    }
    length = intPart.length;
    if (minusFlg) {
        // マイナスが入力された場合は-1
        length--;
    }
    if (argIntKetasu < length) {
        // 整数の桁数がオーバーしている場合はＮＧ
        return false;
    }
    return true;
}
