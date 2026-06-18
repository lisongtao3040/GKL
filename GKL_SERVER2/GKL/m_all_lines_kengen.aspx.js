$(document).ready(function () {
    $(".jq_upd,.jq_del,.jq_ins").click(function (e) {
        if ($("#tbxLineId").val().Trim() == '') {
            alert("请输入生产线ID");
            return false;
        }
    });
    
    // 表格行点击事件
    $(".jq_ms tr").click(function () {
        var lineId = $(this).find(".jq_line_id_gen").text().Trim();
        $("#tbxLineId").val(lineId);
        $("#hidLineIdGen").val(lineId);
        
        // 清除旧的选择
        $(".jq_ms tr").css("background-color", "");
        // 设置当前选择行的背景色
        $(this).css("background-color", "#FFAA00");
    });
});

// String扩展方法：Trim
String.prototype.Trim = function () {
    return this.replace(/^\s+|\s+$/g, "");
};