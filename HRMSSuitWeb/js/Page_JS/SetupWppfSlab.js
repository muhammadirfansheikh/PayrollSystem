


//TaxSlabId = 0;

function pageLoad() {
    //alert();
    TriggerPageLoadsLoan();
}


function TriggerPageLoadsLoan() {


    GetCompany();
    //GetTaxSlabList();
    GetWppfSlabList();
}

//function GetTaxes() {

//    var service = new HrmsSuiteHcmService.HcmService();
//    service.getTax(onGetTaxes, null, null);
//}

//function onGetTaxes(result) {
//    //alert(result);
//    resTax = jQuery.parseJSON(result);
//    FillDropDownByReference('.ddlTax', resTax);
//}

function GetCompany() {

    var service = new HrmsSuiteHcmService.HcmService();
    service.getCompany(onGetCompany, null, null);
}

function onGetCompany(result) {
    //alert(result);
    res = jQuery.parseJSON(result);
    FillDropDownByReference('.ddlCompany', res);
}

function Save() {
    if (!validateForm('.dvEntry')) {
        return;
    }
    //;
    var SalaryFrom = $('.txtSalaryRangeFrom').val();
    var SalaryTo = $('.txtSalaryRangeTo').val();

    if (parseFloat(SalaryFrom) < parseFloat(SalaryTo)) {

        var CompanyId = $('.ddlCompany').val();
        var WppfSlabId = $('.hfWppfSlabId').val();
        //var TaxId = $('.ddlTax').val();
        var WppfSlab = $('.txtWppfSlab').val();
        var UnitValue = $('.txtUnitValue').val();
        //var TaxPercent = $('.txtTaxPercent').val();
        //var YearFrom = $('.txtYearFrom').val();
        //var YearTo = $('.txtYearTo').val();
        var YearId = $('.ddlYear').val();

        SaveWppfSlab(parseInt(CompanyId), WppfSlab, parseFloat(SalaryFrom), parseFloat(SalaryTo), parseFloat(UnitValue), YearId, parseInt(WppfSlabId));
    }
    else {

        showError("Salary Range From should not be greater than Salary Range To");
    }
}

function showError(message) {
    AlertBox('Error!', message, 'error');
}

function showSuccess(message) {
    AlertBox('Success!', message, 'success');
}

function AlertBox(title, Message, type) {
    swal(title, Message, type);
}

function GetWppfSlabList() {

    var CompanyId = $('.ddlCompany').val();

    ClearFields();

    $('.ddlCompany').val(CompanyId);

    //LoanMasterId = 0;
    var service = new HrmsSuiteHcmService.HcmService();
    service.getWppfSlabListing(CompanyId, onGetWppfSlabList, null, null);


}

function onGetWppfSlabList(result) {

    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.tbodyTaxSlabListing').html('');
    $('#TaxSlabListing').tmpl(res).appendTo(divTbodyGoalFund);
}

function SaveWppfSlab(CompanyId,  WppfSlab, SalaryRangeFrom, SalaryRangeTo, UnitValue, YearId, WppfSlabId) {

    var service = new HrmsSuiteHcmService.HcmService();
    service.saveWppfSlab(CompanyId, WppfSlab, SalaryRangeFrom, SalaryRangeTo, UnitValue, YearId, WppfSlabId, onSaveWppfSlab, null, null);
}

function onSaveWppfSlab(result) {
    if (result == 1)
        showSuccess('Save Successfully!');

    ClearFields();
    GetWppfSlabList();
}

function setWppfSlabFieldsOnEdit(WppfSlabId, CompanyId, WppfSlab, SalaryRangeStart, SalaryRangeEnd, UnitValue, YearId) {

    $('.ddlCompany').val(CompanyId);
    $('.hfWppfSlabId').val(WppfSlabId);
    //$('.ddlTax').val(TaxId);
    $('.txtWppfSlab').val(WppfSlab);
    $('.txtUnitValue').val(UnitValue);
    //$('.txtTaxPercent').val(TaxPercent);
    $('.ddlYear').val(YearId);
    $('.txtSalaryRangeFrom').val(SalaryRangeStart);
    $('.txtSalaryRangeTo').val(SalaryRangeEnd);

}

function ClearFields() {
    $('.ddlCompany').val(0);
    $('.hfWppfSlabId').val(0);
    $('.ddlTax').val(0);
    $('.txtWppfSlab').val("");
    $('.txtUnitValue').val(0);
    $('.txtTaxPercent').val("");
    $('.txtSalaryRangeFrom').val(0);
    $('.txtSalaryRangeTo').val(0);
    $('.ddlYear').val(0);
}


function GetYear() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getTaxYear($('.ddlCompany').val(), onGetYear, null, null);
}

function onGetYear(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlYear", res);
}

function deleteWppfSlab(WppfSlabId) {
    var service = new HrmsSuiteHcmService.HcmService();
    service.DeleteWppfSlabById(WppfSlabId, onDeleteWppfSlabById, null, null);
}

function onDeleteWppfSlabById(result) {
    if (result == 1)
        showSuccess("Successfully Deleted!");

    GetWppfSlabList();
}