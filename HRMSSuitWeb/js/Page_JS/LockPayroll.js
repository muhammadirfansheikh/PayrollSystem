
TriggerPageLoads();

function TriggerPageLoads() {
    GetGroup();
   
}

function GetGroup() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getGroup(onGetGroup, null, null);
}

function onGetGroup(result) {

    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlGroup", res);
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

function GetPayrollLogData() {
    var service = new HrmsSuiteHcmService.HcmService();

    var Company = $('.ddlCompany').val();
    service.PayrollTransForSeprateLock(Company, onGetPayrollLogData, null, null);
}

function onGetPayrollLogData(result) {
    var res = jQuery.parseJSON(result);
    
    var divTbodyGoalFund = $('.tbodyPayroll').html('');
    $('#Payroll').tmpl(res).appendTo(divTbodyGoalFund);
}

function PayrollLockSeparate(PayrollLogId) {
    var CompanyId = $('.ddlCompany').val();

    if (CompanyId > 0) {
        var service = new HrmsSuiteHcmService.HcmService();
        service.PayrollLockSeparate(PayrollLogId/*, CompanyId*/, onPayrollLockSeparate, null, null);
    }
    else {
        showError('Please Select Company');
    }
}

function onPayrollLockSeparate(result) {
    
    if (result == 1) {
        showSuccess('Payroll Locked Successfully');
        GetPayrollLogData();
    }
    else {
        showError('Unable to Lock Payroll');
    }
}


