// iframe 全屏弹出层
function openInFrame(url) {
    // 如果已存在则先移除
    $('#iframeOverlay').remove();

    // 注意：这里增加了 style 内联样式，确保它在页面中真正全屏显示
    var overlay = $(
        '<div id="iframeOverlay" style="position:fixed; top:0; left:0; width:100%; height:100%; background-color:rgba(0,0,0,0.6); z-index:9999;">' +
            '<button id="iframeClose" title="关闭" style="position:absolute; right:20px; top:20px; font-size:30px; font-weight:bold; color:white; background:none; border:none; cursor:pointer; z-index:10000;">×</button>' +
            '<iframe src="' + url + '" frameborder="0" style="width:100%; height:100%; display:block;"></iframe>' +
        '</div>'
    );

    // 【修改点】：将 .on('click', ...) 替换为了老版本兼容的 .click(...)
    // 并且使用 overlay.find() 确保在 jQuery 1.4 中准确找到未插入 DOM 的内部元素
    overlay.find('#iframeClose').click(function () {
        overlay.remove();
    });

    $('body').append(overlay);
}

function login(table) {
    var user = $("#lblUserCd").text();
    var name = $("#lblUserName").text();
    var db = "GKL2_本番";

    var params = 'user=' + encodeURIComponent(user) + '&name=' + encodeURIComponent(name);
    if (db && table) {
        params += '&db=' + btoa(encodeURIComponent(db)) + '&table=' + btoa(encodeURIComponent(table));
    }
    openInFrame('http://10.160.192.131/DBTableKanri/TableMaint.aspx?' + params);
}

function loginView(view) {
    var user = $("#lblUserCd").text();
    var name = $("#lblUserName").text();
    var db = "GKL2_本番";

    var params = 'user=' + encodeURIComponent(user) + '&name=' + encodeURIComponent(name);
    if (db && view) {
        params += '&db=' + btoa(encodeURIComponent(db)) + '&view=' + btoa(encodeURIComponent(view));

        // 默认条件：按「检查日期=今天」过滤
        if (view === 'v_A01_result_list_2026') {
            var t = new Date();
            var y = t.getFullYear();
            var m = ('0' + (t.getMonth() + 1)).slice(-2);
            var d = ('0' + t.getDate()).slice(-2);
            params += '&filter=' + btoa(encodeURIComponent("检查日期=" + y + '-' + m + '-' + d));
        } else if (view === 'v_A02_check_result_ms_2026') {
            var t = new Date();
            var y = t.getFullYear();
            var m = ('0' + (t.getMonth() + 1)).slice(-2);
            var d = ('0' + t.getDate()).slice(-2);
            params += '&filter=' + btoa(encodeURIComponent("检查日期YMD=" + y + '-' + m + '-' + d));
        } else if (view === 'v_A03_mi_check_result_2026') {
            var t = new Date();
            var y = t.getFullYear();
            var m = ('0' + (t.getMonth() + 1)).slice(-2);
            var d = ('0' + t.getDate()).slice(-2);
            params += '&filter=' + btoa(encodeURIComponent("生成实际日YMD=" + y + '-' + m + '-' + d));
        } else if (view === 'v_A04_result_list_2026') {
            var t = new Date();
            var y = t.getFullYear();
            var m = ('0' + (t.getMonth() + 1)).slice(-2);
            var d = ('0' + t.getDate()).slice(-2);
            params += '&filter=' + btoa(encodeURIComponent("生成实际日=" + y + '-' + m + '-' + d));
        } else if (view === 'v_A05_check_scx_one') {
            var t = new Date();
            var y = t.getFullYear();
            var m = ('0' + (t.getMonth() + 1)).slice(-2);
            var d = ('0' + t.getDate()).slice(-2);
            params += '&filter=' + btoa(encodeURIComponent("检查日期=" + y + '-' + m + '-' + d));
        }
    }

    openInFrame('http://10.160.192.131/DBTableKanri/ViewDisplay.aspx?' + params);
}