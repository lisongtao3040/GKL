// Do Ajax function
function AjaxPost(ajaxActionType){
    $.ajax({
        type: 'POST',
        url: 'SaveDataAjax.aspx',
        data: {
            ajaxActionType : ajaxActionType
            ,tbxLineId_key:$('#tbxLineId_key').val()
            ,tbxTempId_key:$('#tbxTempId_key').val()
            ,tbxLineId:$('#tbxLineId').val()
            ,tbxTempId:$('#tbxTempId').val()
            ,tbxTempName:$('#tbxTempName').val()
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
// Ajax page use
    //Dim tbxLineId_key As String = Request.Form("tbxLineId_key")
    //Dim tbxTempId_key As String = Request.Form("tbxTempId_key")
    //Dim tbxLineId As String = Request.Form("tbxLineId")
    //Dim tbxTempId As String = Request.Form("tbxTempId")
    //Dim tbxTempName As String = Request.Form("tbxTempName")

//専嶕
//       Return BC.SelMTempName(tbxLineId_key ,tbxTempId_key)
//搊榐
//       Return BC.InsMTempName(tbxLineId ,tbxTempId ,tbxTempName)
//嶍彍
//       Return BC.DelMTempName(tbxLineId_key ,tbxTempId_key)
//峏怴
//       Return BC.UpdMTempName(tbxLineId_key ,tbxTempId_key ,tbxLineId ,tbxTempId ,tbxTempName)