


FillQueue = 0;
var serializedObject;
var VehicleInfoId = null;
isEditManufacture = false;
isEditVehicle = false;
isVariantEdit = false;


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



function GetCategory(Company) {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getCategory($(Company).val(), onGetCategory, null, null);
}

function onGetCategory(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlCategory", res);
    $(".ddlCategory").change();

}



function GetDesignation(Company) {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getDesignationByCategoryId($(Company).val(), onGetDesignation, null, null);
}

function onGetDesignation(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlDesignation", res);
    $(".ddlDesignation").change();
}


function GetDesignationList() {
    GroupId = $('.ddlGroup').val();
    CompanyId = $('.ddlCompany').val();
    CategoryId = $('.ddlCategory').val();
    DesignationId = $('.ddlDesignation').val();
    DesignationName = $('.txtDesignationName').val();
    var service = new HrmsSuiteHcmService.HcmService();
    service.getDesignationList(GroupId, CompanyId, CategoryId, DesignationId, DesignationName, onGetDesignationList, null, null);
}

function onGetDesignationList(result) {
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.tbodyCarInformation').html('');
    $('#CarInformation').tmpl(res).appendTo(divTbodyGoalFund);
    paginateTable('.tableEmployee', 50);
    GetVehicleType(0, HCM_SetupMaster.Vehicle);
}





/********** VEHICLE NEW WORK AFTER STRUCTURE CHANGE **********/

function VehiclePopup() {
    GetVehicle_Type();
}

function GetVehicle_Type() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.GetVehicleType(OnGetVehicle_Type, null, null);
}

function OnGetVehicle_Type(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference('.ddlVehicleCategory', res);
    $('.ddlVehicleCategory').val(res[0].Id);
    $('.ddlVehicleCategory').change();
}

function GetVehicle_Manufacturer(selector, ControlType) {

    var VehicleTypeId = $(selector).val();
    var service = new HrmsSuiteHcmService.HcmService();

    if (ControlType == 'G') {
        service.GetManufacturer(VehicleTypeId, OnGetVehicle_ManufacturerGrid, null, null);
    }
    else if (ControlType == 'D') {
        service.GetManufacturer(VehicleTypeId, OnGetVehicle_ManufacturerDropDown, null, null);
    }
}

function OnGetVehicle_ManufacturerGrid(result) {
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.divManufacturerListing').html('');
    $('#ManufacturerListing').tmpl(res).appendTo(divTbodyGoalFund);
}

function OnGetVehicle_ManufacturerDropDown(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference('.ddlManufacturer', res);
    $('.ddlManufacturer').val(res[0].Id);
    $('.ddlManufacturer').change();
}

function GetVehicle_Name(selector, ControlType) {

    var ManufacturerId = $(selector).val();
    var service = new HrmsSuiteHcmService.HcmService();

    if (ControlType == 'G') {
        service.GetVehicleName(ManufacturerId, OnGetVehicle_NameGrid, null, null);
    }
    else if (ControlType == 'D') {
        service.GetVehicleName(ManufacturerId, OnGetVehicle_NameDropDown, null, null);
    }
}

function OnGetVehicle_NameGrid(result) {
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.divVehicleListing').html('');
    $('#VehicleListing').tmpl(res).appendTo(divTbodyGoalFund);
}

function OnGetVehicle_NameDropDown(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference('.ddlVehicle', res);
    $('.ddlVehicle').val(res[0].Id);
    $('.ddlVehicle').change();
}

function clearManufacturerFields() {

    isEditManufacture = false;
    resetControls('.divManufacturer');
}


function onSaveManufacturer() {

    var _CompanyId = 0;

    if (!validateForm('.divManufacturer'))
        return;

    var flag = true;
    var ManufacturerName = $('.txtManufacturerName').val();

    if (isEditManufacture) {
        var service = new HrmsSuiteHcmService.HcmService();
        service.saveToSetupDetail(_CompanyId, HCM_SetupMaster.Manufacturer, ManufacturerName, Manufacture_SetupDetailid, null, onSaveManufacturerRecord, null, null);
    }
    else {

        $('.tdManufacturerName').each(function () {
            var val = $(this).html();
            if (ManufacturerName.trim().toLowerCase() == val.trim().toLowerCase())
                flag = false;
        });

        if (flag) {
            var service = new HrmsSuiteHcmService.HcmService();
            service.saveToSetupDetail(_CompanyId, HCM_SetupMaster.Manufacturer, ManufacturerName, null, $('.ddlVehicleCategory').val(), onSaveManufacturerRecord, null, null);
        }
        else
            showError('Manufacturer Already Been Exists');
    }

}

function onEditManufacturer(selector) {


    Manufacture_SetupDetailid = $(selector).closest('tr').find('.Manufacturer_SetupDetailId').val();
    var val = $(selector).closest('tr').find('.tdManufacturerName').html();
    //var parentid = $(selector).closest('tr').find('.Manufacturer_ParentId').val();
    //$('.ddlVehicleCategory').val(parentid);
    $('.txtManufacturerName').val(val);
    isEditManufacture = true;
}

function onDeleteManufacturer(selector) {
    var SetupDetailid = $(selector).closest('tr').find('.Manufacturer_SetupDetailId').val();
    var service = new HrmsSuiteHcmService.HcmService();
    service.deleteFromSetupDetail(SetupDetailid, OnDeleteManufacturerRecord, null, null);
}

function onSaveManufacturerRecord(result) {
    if (result == 1)
        showSuccess('Manufacturer Has Been Updated');
    //GetManufacturerListing('.ddlVehicleCategory');
    //clearManufacturerFields();

    $('.txtManufacturerName').val('');
    $('.ddlVehicleCategory').change();
}


function EditVehicleTabInfo(selector) {
    var objRow = $(selector).closest('tr');
    var objTab = objRow.closest('.tab-pane');

    var VehicleName = objRow.find('.tdVehicleName').html();
    TabVehicleId = objRow.find('.Vehicle_SetupDetailId').val();
    //var ManufacturerId = objRow.find('.Vehicle_ManufacturerId').val();
    //var CategoryId = objRow.find('.Vehicle_CategoryId').val();

    //objTab.find('.ddlVehicleCategory').val(CategoryId);
    //objTab.find('.ddlManufacturer').val(ManufacturerId);
    objTab.find('.txtVehicleName').val(VehicleName);

    isEditVehicle = true;
}

function DeleteVehicleTabInfo(selector) {
    var SetupDetailid = $(selector).closest('tr').find('.Vehicle_SetupDetailId').val();
    var service = new HrmsSuiteHcmService.HcmService();
    service.deleteFromSetupDetail(SetupDetailid, onDeleteVehicleTabInfo, null, null);
}

function onDeleteVehicleTabInfo(result) {
    if (result == 1)
        showSuccess('Successsfully Deleted');
    var objCat = $('#VehicleTab').find('.ddlVehicleCategory')
    GetVehicleInfoListing(objCat);
}

function clearVehicleTabFields() {
    isEditVehicle = false;
    resetControls('.divVehicle');
}


function SaveVehicleTabInfo(selector) {


    var _CompanyId = 0;

    flag = true;
    if (!validateForm('.divVehicle'))
        return;

    objTab = $(selector).closest('.tab-pane');
    var ManufacturerId = objTab.find('.ddlManufacturer').val();
    var VehicleName = objTab.find('.txtVehicleName').val();

    if (isEditVehicle) {
        var service = new HrmsSuiteHcmService.HcmService();
        service.saveToSetupDetail(_CompanyId, HCM_SetupMaster.VehicleName, VehicleName, TabVehicleId, null, onSaveVehicleTabInfo, null, null);
    }
    else {

        $('.tdVehicleName').each(function () {
            var val = $(this).html();
            if (val.trim().toLowerCase() == $('.txtVehicleName').val())
                flag = false;
        });

        if (flag) {
            var service = new HrmsSuiteHcmService.HcmService();
            service.saveToSetupDetail(_CompanyId, HCM_SetupMaster.VehicleName, VehicleName, null, ManufacturerId, onSaveVehicleTabInfo, null, null);
        }
        else
            showError('Vehicle Name Already Exists');
    }
}



function onSaveVehicleTabInfo(result) {
    if (result == 1)
        showSuccess('Vehicle Updated Successfully');
    //var objCat = $('#VehicleTab').find('.ddlVehicleCategory')
    //GetVehicleInfoListing(objCat);
    //clearVehicleTabFields();

    $('.txtVehicleName').val('');

    GetVehicle_Name('.ddlManufacturer', 'G');

}

