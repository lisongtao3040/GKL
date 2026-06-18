// Do Ajax function
function AjaxPost(ajaxActionType){
    $.ajax({
        type: 'POST',
        url: 'SaveDataAjax.aspx',
        data: {
            ajaxActionType : ajaxActionType
            ,tbxChkNo_key:$('#tbxChkNo_key').val()
            ,tbxNen_key:$('#tbxNen_key').val()
            ,tbxLineId_key:$('#tbxLineId_key').val()
            ,tbxMakeNo_key:$('#tbxMakeNo_key').val()
            ,tbxChkNo:$('#tbxChkNo').val()
            ,tbxNen:$('#tbxNen').val()
            ,tbxPlanNo:$('#tbxPlanNo').val()
            ,tbxLineId:$('#tbxLineId').val()
            ,tbxMakeNo:$('#tbxMakeNo').val()
            ,tbxCode:$('#tbxCode').val()
            ,tbxSuu:$('#tbxSuu').val()
            ,tbxTempId:$('#tbxTempId').val()
            ,tbxChkResult:$('#tbxChkResult').val()
            ,tbxChkUser:$('#tbxChkUser').val()
            ,tbxChkStartDate:$('#tbxChkStartDate').val()
            ,tbxChkEndDate:$('#tbxChkEndDate').val()
            ,tbxParentChkNo:$('#tbxParentChkNo').val()
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
