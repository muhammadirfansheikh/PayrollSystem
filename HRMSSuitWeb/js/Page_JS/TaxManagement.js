TaxComputationId = 0;
ResponseTaxComputation = [];



function TriggerPageLoadsLoan() {
    GetTaxLaw();
    $('.divActivityDate').hide();
}

function GetTaxLaw() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getTaxLaw(onGetTaxLaw, null, null);
}


function activeDate(selector) {
    var id = $(selector).val();
    if (id == HCM_TaxLaw.TaxLaw34)
        $('.divActivityDate').show();
    else {
        $('.divActivityDate').hide();
        $('.txtActivityDate').val('');
    }

}

function onGetTaxLaw(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlTaxLaw", res);
}

function GetTaxYear() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getTaxYear(CompanyId, onGetTaxYear, null, null);
}

function onGetTaxYear(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlTaxYear", res);
    FillDropDownByReference(".ddlTaxYearAllowanceMapping", res);
}

function SaveTaxComputation() {
    var TaxYearId = $('.ddlTaxYear').val();
    var TaxLawId = $('.ddlTaxLaw').val();
    var TaxLawAmount = $('.txtTaxLawAmount').val();
    var LawActivityDate = $('.txtActivityDate').val() == '' ? null : formatDate($('.txtActivityDate').val());
    var service = new HrmsSuiteHcmService.HcmService();
    service.saveTaxLawSettings(TaxComputationId, EmployeeId, TaxLawId, TaxLawAmount, TaxYearId, LawActivityDate, onSaveTaxComputation, null, null);
}

function onSaveTaxComputation(result) {
    if (result == 1)
        showSuccess('Successfully Added');
    else
        showError(result);

    GetTaxLawComputation();
    ClearFieldsTaxComputation();

}

function GetTaxLawComputation() {

    var service = new HrmsSuiteHcmService.HcmService();
    service.getTaxLawComputation(EmployeeId, onGetTaxLawComputation, null, null);
}

function onGetTaxLawComputation(result) {

    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.tbodyTaxLaw').html('');
    $('#TaxManagement').tmpl(res).appendTo(divTbodyGoalFund);
}

function SetEdit(ref) {
    var dtLawActivity = $(ref).closest('.trTaxComputation').find('.hfLawActivityDate').val();

    dtLawActivity = dtLawActivity == '' ? '' : dtLawActivity.substring(0, 10);

    $('.txtActivityDate').val(reformatDate(dtLawActivity)).keyup();

    TaxComputationId = $(ref).closest('.trTaxComputation').find('.hfTaxComputationId').val();
    $('.ddlTaxLaw').val($(ref).closest('.trTaxComputation').find('.hfTaxLawId').val()).change();
    $('.txtTaxLawAmount').val($(ref).closest('.trTaxComputation').find('.hfTaxAmount').val());
    $('.ddlTaxYear').val($(ref).closest('.trTaxComputation').find('.hfTaxYearId').val());
}

function ClearFieldsTaxComputation() {
    TaxComputationId = 0;
    $('.ddlTaxLaw').val(0).change();
    $('.ddlTaxYear').val(0);
    $('.txtTaxLawAmount').val('');
    $('.txtActivityDate').val('');
}

function DeleteTaxComputation(ref) {
    if (confirm("Are you sure you want to delete?") == true) {
        TaxComputationId = $(ref).closest('.trTaxComputation').find('.hfTaxComputationId').val();
        var service = new HrmsSuiteHcmService.HcmService();
        service.deleteTaxLaw(TaxComputationId, onDeleteTaxComputation, null, null);
    }
}

function onDeleteTaxComputation(result) {
    var res = jQuery.parseJSON(result);
    GetTaxLawComputation();
    ClearFieldsTaxComputation();
}

function reformatDate(_date) {
    var year = _date.substring(0, 4);
    var month = _date.substring(5, 7);
    var day = _date.substring(8, 11);
    var retDate = month + '/' + day + '/' + year;
    return retDate;
}

function GetTaxDetail() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getTaxInformation(EmployeeId, onGetTaxDetail, null, null);
}


function onGetTaxDetail(result) {
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.tbodyTaxDetail').html('');
    $('#TaxDetailInformation').tmpl(res).appendTo(divTbodyGoalFund);
}
