// Do Ajax function
function AjaxPost(ajaxActionType){
    $.ajax({
        type: 'POST',
        url: 'SaveDataAjax.aspx',
        data: {
            ajaxActionType : ajaxActionType
            ,tbxChkId_key:$('#tbxChkId_key').val()
            ,tbxChkName_key:$('#tbxChkName_key').val()
            ,tbxChkId:$('#tbxChkId').val()
            ,tbxChkName:$('#tbxChkName').val()
            ,tbxChkMethod:$('#tbxChkMethod').val()
            ,tbxChkFormula:$('#tbxChkFormula').val()
            ,tbxVerifyMethodExplain:$('#tbxVerifyMethodExplain').val()
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
    //Dim tbxChkId_key As String = Request.Form("tbxChkId_key")
    //Dim tbxChkName_key As String = Request.Form("tbxChkName_key")
    //Dim tbxChkId As String = Request.Form("tbxChkId")
    //Dim tbxChkName As String = Request.Form("tbxChkName")
    //Dim tbxChkMethod As String = Request.Form("tbxChkMethod")
    //Dim tbxChkFormula As String = Request.Form("tbxChkFormula")
    //Dim tbxVerifyMethodExplain As String = Request.Form("tbxVerifyMethodExplain")

//専嶕
//       Return BC.SelMCheckMethod(tbxChkId_key ,tbxChkName_key)
//搊榐
//       Return BC.InsMCheckMethod(tbxChkId ,tbxChkName ,tbxChkMethod ,tbxChkFormula ,tbxVerifyMethodExplain)
//嶍彍
//       Return BC.DelMCheckMethod(tbxChkId_key ,tbxChkName_key)
//峏怴
//       Return BC.UpdMCheckMethod(tbxChkId_key ,tbxChkName_key ,tbxChkId ,tbxChkName ,tbxChkMethod ,tbxChkFormula ,tbxVerifyMethodExplain)