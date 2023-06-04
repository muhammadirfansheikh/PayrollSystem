
function TriggerLoad() {
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


function VehicleDetailReport() {
    ProgressBarShow();
    var service = new HrmsSuiteHcmService.HcmService();
    service.report_VehicleDetailReport($(".ddlCompany").val(), onVehicleDetailReport, null, null);
}

function onVehicleDetailReport(result) {
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.VehicleDetailReport').html('');
    $('#VehicleDetailReport').tmpl(res).appendTo(divTbodyGoalFund);
    ProgressBarHide();
}