
TriggerPageLoads();

function TriggerPageLoads() {
    GetGroup();
    //hideLock();
    //$('.alertCount').hide();
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

function GetYear() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getTaxYear($('.ddlCompany').val(), onGetYear, null, null);
}

function onGetYear(result) {
    var res = jQuery.parseJSON(result);

    FillDropDownByReference(".ddlYear", res);
    $(".ddlYear").val(res[0].Id);
}