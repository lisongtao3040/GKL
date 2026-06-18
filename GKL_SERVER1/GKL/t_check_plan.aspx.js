// Do Ajax function
function AjaxPost(ajaxActionType){
    $.ajax({
        type: 'POST',
        url: 'SaveDataAjax.aspx',
        data: {
            ajaxActionType : ajaxActionType
            ,tbxPlanNo_key:$('#tbxPlanNo_key').val()
            ,tbxChkNo_key:$('#tbxChkNo_key').val()
            ,tbxMakeNo_key:$('#tbxMakeNo_key').val()
            ,tbxCode_key:$('#tbxCode_key').val()
            ,tbxLineId_key:$('#tbxLineId_key').val()
            ,tbxPlanNo:$('#tbxPlanNo').val()
            ,tbxChkNo:$('#tbxChkNo').val()
            ,tbxMakeNo:$('#tbxMakeNo').val()
            ,tbxCode:$('#tbxCode').val()
            ,tbxLineId:$('#tbxLineId').val()
            ,tbxSuu:$('#tbxSuu').val()
            ,tbxYoteiChkDate:$('#tbxYoteiChkDate').val()
            ,tbxStatus:$('#tbxStatus').val()
            ,tbxInsUser:$('#tbxInsUser').val()
            ,tbxInsDate:$('#tbxInsDate').val()
        },
        datatype: 'html',//'xml', 'html', 'script', 'json', 'jsonp', 'text'.
        beforeSend: function () {
        },
        //when success
        success: function (data) {
        },
        //when complete
        complete: function (XMLHttpRequest, textStatus) {
            //alert(XMLHttpRequest.responseText);
            alert(textStatus);
        },
        //when error
        error: function () {
        }
    });
}
// 峏怴
function ajax_update(){
    AjaxPost('update');
}
// 嶍彍
function ajax_delete(){
    AjaxPost('delete');
}
// 搊榐
function ajax_insert(){
    AjaxPost('insert');
}
// 専嶕
function ajax_select(){
    AjaxPost('select');
}


