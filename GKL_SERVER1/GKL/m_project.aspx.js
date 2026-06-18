
$(document).ready(function () {
    $(".jq_upd,.jq_del,.jq_ins").click(function (e) {
        if ($("#tbxProjectId").val().Trim() == '') {
            alert("请输入工程ID");
            return false;
        }
    });
});

