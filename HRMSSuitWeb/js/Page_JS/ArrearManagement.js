

function GetArrearInformation() {
    if (!validateForm('.divArrearInputs'))
        return;

    var ResponseForm = [];
    $('.divArrearInputs').each(function () {
        var Response = new Object();
        Response.EmployeeId = EmployeeId;
        Response.ArrearTypeId = $(this).find('.ddlArrearType').val();
        Response.ArrearAmount = $(this).find('.txtAmount').val();
        Response.DispersedDate = $(this).find('.dateDispersedDate').val();
        ResponseForm.push(Response);
    });
    ProgressBarShow();
    var JSONResponse = JSON.stringify(ResponseForm);
    var service = new HrmsSuiteHcmService.HcmService();
    service.saveArrearHistory(JSONResponse, onSaveArrearInformation, null, null);

}

function onSaveArrearInformation(result) {

    var res = jQuery.parseJSON(result);
    ProgressBarHide()
    if (res == "1") {
        showSuccess('Arrears Updated Successfully');
        removeCloneDivs('.divArrearInputs');
        resetControls('.divArrearInputs');
    }
    else
        showError('Operation Failed');

    GetArrearHistory();
}



function GetArrearHistory() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getArrearHistory(EmployeeId, onGetArrearHistory, null, null);
}

function onGetArrearHistory(result) {
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.tbodyArrearHistory').html('');
    $('#ArrearHistory').tmpl(res).appendTo(divTbodyGoalFund);
}

function onRemoveArrear(selector) {
    var Status = $(selector).closest('tr').find('.IsDispersed').val();

    ArrearId = $(selector).closest('td').find('.ArrearId').val();

    if (Status == 'false')
        RemoveArrearById(ArrearId);
    else
        showError('Disbursed Record Cannot Be Deleted');
}


function RemoveArrearById(Arrear) {
    var service = new HrmsSuiteHcmService.HcmService();
    service.removeArrear(ArrearId, onRemoveArrearById, null, null);
}

function onRemoveArrearById(result) {
    if (result == 1)
        showSuccess('Successfully Deleted!');
    GetArrearHistory();
}
