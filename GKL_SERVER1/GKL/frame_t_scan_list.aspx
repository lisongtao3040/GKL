<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frame_t_scan_list.aspx.vb" Inherits="frame_t_scan_list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>t_scan_list</title>

    <!--JS-->
    <script language="javascript" type="text/javascript" src="./frame_t_scan_list.aspx.js"></script>

    <!--CSS-->
    <link href="frame_t_scan_list.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div class="scan-wrap">

        <!-- 上部表示部 make_no / code / user -->
        <div class="scan-header">
            <table class="scan-info" cellpadding="0" cellspacing="0">
                <tr>
                    <th>作番</th>
                    <td id="lblMakeNo" class="scan-info-value"><%= Me.HtmlMakeNo %></td>
                    <th>CD</th>
                    <td id="lblCode" class="scan-info-value"><%= Me.HtmlCode %></td>
                    <th>ユーザ</th>
                    <td id="lblUser" class="scan-info-value"><%= Me.HtmlUserDisplay %></td>
                    <td class="scan-close-cell">
                        <%--<input type="button" id="btnClose" class="scan-close-button" value="关闭" />--%>
                    </td>
                </tr>
            </table>
        </div>

        <!-- 扫码入力部 -->
        <div class="scan-input-panel">
            <label for="tbxScanTxt" class="scan-input-label">扫码值</label>
            <input type="text" id="tbxScanTxt" class="scan-text" maxlength="200" autocomplete="off" />
        </div>

        <!-- 消息表示部 -->
        <div class="scan-message-panel">
            <span id="lblMsg" class="scan-message"></span>
        </div>

        <!-- 一览部 -->
        <div class="scan-list-panel">
            <table class="scan-list" cellpadding="0" cellspacing="0">
                <colgroup>
                    <col class="scan-col-scan-txt" />
                    <col width="56" />
                    <col width="90" />
                    <col width="128" />
                    <col width="64" />
                </colgroup>
                <thead>
                    <tr>
                        <th>扫码值</th>
                        <th>结果</th>
                        <th>更新者</th>
                        <th>更新日</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody id="tbodyScanList">
                </tbody>
            </table>
        </div>

        <div class="scan-note">
            扫码后自动登录（扫码值输入框一直保持焦点）。扫码值的第一部分（code）与画面的 CD 一致时结果为 OK（比较时忽略「-」和空格），否则为 NG。结果为 NG 时，点击「维护」按钮可把结果变更为 RN。
        </div>

        <!-- 画面参数（URL 参数） -->
        <input type="hidden" id="hidChkNo" value="<%= Me.HtmlChkNo %>" />
        <input type="hidden" id="hidLineId" value="<%= Me.HtmlLineId %>" />
        <input type="hidden" id="hidMakeNo" value="<%= Me.HtmlMakeNo %>" />
        <input type="hidden" id="hidCode" value="<%= Me.HtmlCode %>" />
        <input type="hidden" id="hidUser" value="<%= Me.HtmlUser %>" />

    </div>
</body>
</html>
