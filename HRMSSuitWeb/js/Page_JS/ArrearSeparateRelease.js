
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

function GetSalaryMonth() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.GetSalaryMonth($('.ddlCompany').val(), onGetSalaryMonth, null, null);
}

function onGetSalaryMonth(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlSalaryMonth", res);

}

function CalculateArrear() {
    if (!validateForm('.divPayrollForm'))
        return;

    var Company = $(".ddlCompany").val();
    var DateOfPayroll = formatDate($('.txtMonth').val());

    ProgressBarShow();
    var service = new HrmsSuiteHcmService.HcmService();
    service.payrollBonusRelease(Company, DateOfPayroll, onGetBonusRelease, null, null);
}

function onCalculateArrear(result) {

    try {

        if (result == '0') {
            showError("Arrears are not yet updated!");
            ProgressBarHide();
            return;
        }


        var res = jQuery.parseJSON(result);

        var divTbodyGoalFund = $('.tbodyArrearListing').html('');
        $('#ArrearListing').tmpl(res).appendTo(divTbodyGoalFund);

        showSuccess('Arrear Release Successfully!');
        GetBonus();
        ProgressBarHide();
    }
    catch (e) {
        showError("Arrears are not yet updated!");
        ProgressBarHide();
    }
}

//function GetBonus() {
//    var service = new HrmsSuiteHcmService.HcmService();
//    service.GetBonusTransactionMaster($('.ddlCompany').val(), onGetBonus, null, null);
//}

//function onGetBonus(result) {
//    var res = jQuery.parseJSON(result);
//    FillDropDownByReference(".ddlSeparateBonus", res);
//    //$(".ddlCompany").change();
//}

//function ReleaseBonus() {
//    if (!validateForm('.divPayrollForm'))
//        return;

//    var Company = $(".ddlCompany").val();
//    var DateOfPayroll = formatDate($('.txtMonth').val());

//    ProgressBarShow();
//    var service = new HrmsSuiteHcmService.HcmService();
//    service.payrollBonusRelease(Company, DateOfPayroll, onGetBonusRelease, null, null);
//}

//function onGetBonusRelease(result) {

//    try {

//        if (result == '0') {
//            showError("Bonus Informations are not yet updated!");
//            ProgressBarHide();
//            return;
//        }


//        var res = jQuery.parseJSON(result);

//        var divTbodyGoalFund = $('.tbodyBonusListing').html('');
//        $('#BonusListing').tmpl(res).appendTo(divTbodyGoalFund);

//        showSuccess('Bonus Release Successfully!');
//        GetBonus();
//        ProgressBarHide();
//    }
//    catch (e) {
//        showError("Bonus Informations are not yet updated!");
//        ProgressBarHide();
//    }
//}

//function MergeSalaryBonus() {

//    if (!validateForm('.divPayrollForm'))
//        return;

//    if (!validateForm('.divBonus'))
//        return;

//    var Company = $(".ddlCompany").val();
//    var DateOfPayroll = formatDate($('.txtMonth').val());

//    var service = new HrmsSuiteHcmService.HcmService();
//    service.MergeSalaryBonus($('.ddlCompany').val(), DateOfPayroll, onMergeSalaryBonus, null, null);
//}

//function onMergeSalaryBonus(result) {
//    try {

//    }
//    catch (e) {
//        showError("Bonus Informations are not yet merge!");
//        ProgressBarHide();
//    }
//}
