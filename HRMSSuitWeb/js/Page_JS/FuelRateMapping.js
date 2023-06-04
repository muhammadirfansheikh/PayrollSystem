
var DesignationId = 0;
var FuelProvided = 0;


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
    ProgressBarShow();
}

function onGetCompany(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlCompany", res);
    $(".ddlCompany").change();
    ProgressBarHide();
}



function GetCategory(Company) {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getCategory($(Company).val(), onGetCategory, null, null);
    ProgressBarShow();
}

function onGetCategory(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlCategory", res);
    $(".ddlCategory").change();
    ProgressBarHide();
}



function GetDesignation(Company) {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getDesignationByCategoryId($(Company).val(), onGetDesignation, null, null);

    ProgressBarShow();
}

function onGetDesignation(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlDesignation", res);
    $(".ddlDesignation").change();
    ProgressBarHide();
}


function GetDesignationList() {
    GroupId = $('.ddlGroup').val();
    CompanyId = $('.ddlCompany').val();
    CategoryId = $('.ddlCategory').val();
    DesignationId = $('.ddlDesignation').val();
    DesignationName = $('.txtDesignationName').val();
    var service = new HrmsSuiteHcmService.HcmService();
    service.getDesignationFuelMapping(GroupId, CompanyId, CategoryId, DesignationId, DesignationName, onGetDesignationList, null, null);

    ProgressBarShow();
}

function onGetDesignationList(result) {
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.tbodyFuelInformation').html('');
    $('#FuelInformation').tmpl(res).appendTo(divTbodyGoalFund);
    paginateTable('.tableEmployee', 50);

    ProgressBarHide();
}


function manageFuelMapping(selector) {
    var objRow = $(selector).closest('tr');
    DesignationId = objRow.find('.hdnDesignationId').val();
    FuelProvided = objRow.find('.FuelProvidedInLitres').text().trim();
    $('.txtFuel').val(FuelProvided);
}


function saveFuelChanges() {
    if (!validateForm('.divFuel'))
        return;

    var Fuel = $('.txtFuel').val();
    var FirstYear = $('.txtFirstYear').val();
    var SecondYear = $('.txtSecondYear').val();
    var ThirdYear = $('.txtThirdYear').val();
    var FourthYear = $('.txtFourthYear').val();
    var FifthYear = $('.txtFifthYear').val();
    var IsActual = false;
    if ($(".chkbxOnActual").prop('checked') == true) {
        IsActual = true;
    }

    var service = new HrmsSuiteHcmService.HcmService();
    service.saveFuelMapping(DesignationId, parseFloat(Fuel), parseFloat(FirstYear), parseFloat(SecondYear), parseFloat(ThirdYear), parseFloat(FourthYear), parseFloat(FifthYear),
        IsActual, onsaveFuelChanges, null, null);

    ProgressBarShow();
}

function onsaveFuelChanges(result) {
    if (result == 1)
        showSuccess('Successfully Updated');
    else
        showError('Operation Failed');
    GetDesignationList();

    ProgressBarHide();
}

function SetSavedValues(selector, desig) {

    $("#lblDesignation").text(desig);
    GroupId = $('.ddlGroup').val();
    CompanyId = $('.ddlCompany').val();
    CategoryId = $('.ddlCategory').val();
    hfDesignationId = $(selector).closest('tr').find('.hdnDesignationId').val();
    DesignationName = $('.txtDesignationName').val();
    var service = new HrmsSuiteHcmService.HcmService();
    service.getDesignationFuelMapping(GroupId, CompanyId, CategoryId, hfDesignationId, DesignationName, onSetSavedValues, null, null);

    ProgressBarShow();
}

function onSetSavedValues(result) {
    var res = jQuery.parseJSON(result);

    $(res).each(function (k, v) {

        $('.txtFuel').val(v.FuelInLitres);
        $('.txtFirstYear').val(v.First);
        $('.txtSecondYear').val(v.Second);
        $('.txtThirdYear').val(v.Third);
        $('.txtFourthYear').val(v.Fourth);
        $('.txtFifthYear').val(v.Fifth);
        $('.chkbxOnActual').prop('checked', v.IsOnActual);

        $('.hfFuel').val(v.FuelInLitres);
        $('.hfFirstYear').val(v.First);
        $('.hfSecondYear').val(v.Second);
        $('.hfThirdYear').val(v.Third);
        $('.hfFourthYear').val(v.Fourth);
        $('.hfFifthYear').val(v.Fifth);
    });
    ProgressBarHide();
}

function SetZeroForActual() {
    if ($(".chkbxOnActual").prop('checked') == true) {

        $('.txtFirstYear').val(0);
        $('.txtSecondYear').val(0);
        $('.txtThirdYear').val(0);
        $('.txtFourthYear').val(0);
        $('.txtFifthYear').val(0);
    }
    else {
        $('.txtFirstYear').val($('.hfFirstYear').val());
        $('.txtSecondYear').val($('.hfSecondYear').val());
        $('.txtThirdYear').val($('.hfThirdYear').val());
        $('.txtFourthYear').val($('.hfFourthYear').val());
        $('.txtFifthYear').val($('.hfFifthYear').val());

        $('.txtFuel').val($('.hfFuel').val());
    }
}

function IncreasePercentage() {
    if (!validateForm('.txtPercentage'))
        return;

    var Percent = $('.txtPercentage').val();

    var service = new HrmsSuiteHcmService.HcmService();
    service.IncreaseRMPercentage(Percent, onIncreaeseRMPercentage, null, null);

    ProgressBarShow();
}

function onIncreaeseRMPercentage(result) {
    if (result == 1) {
        showSuccess('Successfully Updated');
        $('.txtPercentage').val('');
    }
    else {
        showError('Operation Failed');
    }

    ProgressBarHide();
}