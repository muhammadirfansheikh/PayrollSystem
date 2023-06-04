
var EmployeeId = '';
function TriggerPageLoads() {
    FillDropDownByReference(".ddlTaxYear", null);
    
}


function openModal(selector) {
    $('.hdnOpenModal').click();
    var EmpId = $(selector).find('.hdnEmployeeId').val();
    EmployeeId = EmpId;
    GetTaxableTransactions();
}

function GetTaxableTransactions() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getTaxableTransactions(EmployeeId, onGetTaxableTransactions, null, null);
}

function onGetTaxableTransactions(result) {
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.tbodyTaxableAllowances').html('');
    $('#TaxableAllowances').tmpl(res).appendTo(divTbodyGoalFund);

    var yearTotal = 0;
    var monthTotal = 0;

    $('.tdYearlyAmount').each(function () { yearTotal += parseFloat($(this).text()); });
    $('.tdMonthlyAmount').each(function () { monthTotal += parseFloat($(this).text()); });


    $('.tdMonthlyTotal').text(monthTotal);
    $('.tdYearlyTotal').text(yearTotal);

    GetTaxForecastedDetailsByEmployeeId();
}


function GetTaxForecastedDetailsByEmployeeId() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getTaxForecastedDetailsByEmployeeId(EmployeeId, onGetTaxForecastedDetailsByEmployeeId, null, null);
}

function onGetTaxForecastedDetailsByEmployeeId(result) {
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.ForecastedDetails').html('');
    $('#ForecastedDetails').tmpl(res).appendTo(divTbodyGoalFund);
}

function GetTaxForcasting() {

    var CompanyId = $('.ddlCompany').val();
    var YearId = $('.ddlTaxYear').val();

    if (CompanyId > 0 && YearId > 0) {
        ProgressBarShow();
        var service = new HrmsSuiteHcmService.HcmService();
        service.getTaxForcast(CompanyId, YearId, onGetTaxForcasting, null, null);
    }
}

function onGetTaxForcasting(result) {
    var res = jQuery.parseJSON(result);
    ResponseForm = [];

    $(res).each(function (k, v) {

        ResponseForm.push(v);

    });

    var divTbodyGoalFund = $('.tbodyTaxForcastListing').html('');
    $('#TaxForcastListing').tmpl(ResponseForm).appendTo(divTbodyGoalFund);
    paginateTable('.tableEmployee', 50);
    ProgressBarHide();


}

function GetTaxYear() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getTaxYear($('.ddlCompany').val(), onGetTaxYear, null, null);
}

function onGetTaxYear(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlTaxYear", res);
    $(".ddlTaxYear").val(res[0].Id);
}

function SaveTaxForcast() {
    debugger
    if (!validateForm('.dvEntry'))
        return;

    var CompanyId = $('.ddlCompany').val();
    var YearId = $('.ddlTaxYear').val();
    var IsAll = $(".chkAll").prop('checked');
    let _advanceTaxPerc = $('.txtAdvanceTaxc').val() == '' ? "0" : $('.txtAdvanceTaxc').val() ;

    var service = new HrmsSuiteHcmService.HcmService();
    service.saveTaxForcastMain(YearId, /*UserId, UserIP,*/ CompanyId, IsAll, _advanceTaxPerc, onSaveTaxForcast, null, null);
}

function onSaveTaxForcast(result) {

    var res = jQuery.parseJSON(result);

    if (result.length > 0) {
        showSuccess('Save Successfully!');
    }

    //ResponseForm = [];

    //$(res).each(function (k, v) {

    //    ResponseForm.push(v);

    //});

    //var divTbodyGoalFund = $('.tbodyTaxForcastListing').html('');
    //$('#TaxForcastListing').tmpl(ResponseForm).appendTo(divTbodyGoalFund);
    //paginateTable('.tableEmployee', 50);

    GetTaxForcasting();

    ProgressBarHide();

    //ClearFields();

}


function ClearFields() {
    $('.ddlCompany').val(0);
    $('.ddlTaxYear').val(0);

    var divTbodyGoalFund = $('.tbodyTaxForcastListing').html('');
    $('#TaxForcastListing').tmpl('').appendTo(divTbodyGoalFund);
}
