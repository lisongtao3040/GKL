
$(document).ready(function () {
    $(".jq_upd,.jq_del,.jq_ins").click(function (e) {
        if ($("#tbxUserCd").val().Trim() == '') {
            alert("请输入用户CD");
            return false;
        }        
    });
});

