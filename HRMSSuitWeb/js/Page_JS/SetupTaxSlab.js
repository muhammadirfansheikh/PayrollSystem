
//TaxSlabId = 0;

function pageLoad() {
    //alert();
    TriggerPageLoadsLoan();
}

function TriggerPageLoadsLoan() {
    GetTaxes();
    GetGroup();
}

function GetGroup() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getGroup(onGetGroup, null, null);
}

function onGetGroup(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlGroup", res);
    if (res.length == 1) {
        $('.ddlGroup').val(res[0].Id)
        $(".ddlGroup").prop("disabled", true);
    }
    $(".ddlGroup").change();
}

function GetCompany(Group) {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getCompanyByGroupId($(Group).val(), onGetCompany, null, null);
}

function onGetCompany(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlCompany", res);
    $(".ddlCompany").change();
}

function GetTaxYear() {
    var service = new HrmsSuiteHcmService.HcmService();
    ProgressBarShow();
    service.getTaxYear($('.ddlCompany').val(), onGetTaxYear, null, null);
}

function onGetTaxYear(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlTaxYear", res);
    ProgressBarHide();
}

function GetTaxes() {

    var service = new HrmsSuiteHcmService.HcmService();
    service.getTax(onGetTaxes, null, null);
}

function onGetTaxes(result) {
    //alert(result);
    resTax = jQuery.parseJSON(result);
    FillDropDownByReference('.ddlTax', resTax);
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
        var TaxSlabId = $('.hfTaxSlabId').val();
        var TaxId = $('.ddlTax').val();
        var TaxSlab = $('.txtTaxSlab').val();
        var FixedValue = $('.txtFixedValue').val();
        var TaxPercent = $('.txtTaxPercent').val();
        //var YearFrom = $('.txtYearFrom').val();
        //var YearTo = $('.txtYearTo').val();
        var TaxYearId = $('.ddlTaxYear').val();

        SaveTaxSlab(parseInt(CompanyId), parseInt(TaxId), TaxSlab, parseFloat(SalaryFrom), parseFloat(SalaryTo), parseFloat(FixedValue), parseFloat(TaxPercent),
            TaxYearId, parseInt(TaxSlabId));
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

function GetTaxSlabList() {

    var CompanyId = $('.ddlCompany').val();
     $('.ddlCompany').val(CompanyId);
     ProgressBarShow();
    var service = new HrmsSuiteHcmService.HcmService();
    service.getTaxSlabListing(CompanyId,onGetTaxSlabList, null, null);
}

function onGetTaxSlabList(result) {

    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.tbodyTaxSlabListing').html('');
    $('#TaxSlabListing').tmpl(res).appendTo(divTbodyGoalFund);
    ProgressBarHide();
}

function SaveTaxSlab(CompanyId, TaxId, TaxSlab, SalaryRangeFrom, SalaryRangeTo, FixedValue, TaxPercent, TaxYearId, TaxSlabId) {

    var service = new HrmsSuiteHcmService.HcmService();
    service.saveTaxSlab(CompanyId, TaxId, TaxSlab, SalaryRangeFrom, SalaryRangeTo, FixedValue, TaxPercent, TaxYearId, TaxSlabId, onSaveTaxSlab, null, null);
}

function onSaveTaxSlab(result) {
    if (result == 1)
        showSuccess('Save Successfully!');

    ClearFields();
    GetTaxSlabList();
}

function setTaxSlabFieldsOnEdit(TaxSlabId, CompanyId, TaxId, TaxSlab, SalaryRangeStart, SalaryRangeEnd, FixedValue, TaxPercent, TaxYearId) {

    $('.ddlCompany').val(CompanyId);
    $('.ddlCompany').change();
    $('.ddlTax').val(TaxId);
    $('.txtTaxSlab').val(TaxSlab);
    $('.txtFixedValue').val(FixedValue);
    $('.txtTaxPercent').val(TaxPercent);
    $('.ddlTaxYear').val(TaxYearId);
    $('.txtSalaryRangeFrom').val(SalaryRangeStart);
    $('.txtSalaryRangeTo').val(SalaryRangeEnd);
    $('.hfTaxSlabId').val(TaxSlabId);
}

function ClearFields() {
    $('.tbodyTaxSlabListing').html('');
    $('.ddlCompany').val(0);
    $('.ddlCompany').change();
    $('.hfTaxSlabId').val(0);
    $('.ddlTax').val(0);
    $('.txtTaxSlab').val("");
    $('.txtFixedValue').val(0);
    $('.txtTaxPercent').val("");
    $('.txtSalaryRangeFrom').val(0);
    $('.txtSalaryRangeTo').val(0);
    $('.ddlTaxYear').val(0);
}

function deleteTaxSlab(TaxSlabId) {
    var service = new HrmsSuiteHcmService.HcmService();
    service.DeleteTaxSlabById(TaxSlabId, onDeleteTaxSlabById, null, null);
}

function onDeleteTaxSlabById(result) {
    if (result == 1)
        showSuccess("Successfully Deleted!");
}