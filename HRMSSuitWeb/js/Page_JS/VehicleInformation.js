
FillQueue = 0;
var serializedObject;
var VehicleInfoId = null;
isEditManufacture = false;
isEditVehicle = false;
isVariantEdit = false;





function TriggerPageLoads() {
    GetVehicleType(0, HCM_SetupMaster.Vehicle);
    GetFuelTypes();
    $(".btnCancelSearch").click(function () {
        $('.tbodyCarInformation').html('');
        paginateTable('.tableEmployee', 50);
    });


    ///Area For Manufacture Tab
    $(".ddlVehicleCategoryForManufacture").change(function (e) {

        GetVehicle_Manufacturer(e.target.value, 'G', 'divManufacturerListing', 'ManufacturerListing');
    });

    GetVehicle_Manufacturer("0", 'G', 'divManufacturerListing', 'ManufacturerListing');
    //End Area

     ///Area For Vehicle Tab
    $(".ddlVehicleCategoryForVehicle").change(function (e) {

        if (e.target.value == "0") {
            GetVehicle_Manufacturer("-1", 'D', 'ddlManufacturerForVehicle', '');
            GetVehicle_Name("0", 'G', 'divVehicleListing', 'VehicleListing');
        }
        else {
            GetVehicle_Manufacturer(e.target.value, 'D', 'ddlManufacturerForVehicle', '');
            GetVehicle_Name("-1", 'G', 'divVehicleListing', 'VehicleListing');
        }



    });
    $(".ddlManufacturerForVehicle").change(function (e) {


        if (e.target.value == "0") {

            GetVehicle_Name("-1", 'G', 'divVehicleListing', 'VehicleListing');
        }
        else {
            GetVehicle_Name(e.target.value, 'G', 'divVehicleListing', 'VehicleListing');
        }

    });


    BindOnLoadVehicleTab();
    //End Area
    
    ///Area For Variant Tab
    $(".ddlVehicleCategoryForVariant").change(function (e) {
        
        if (e.target.value == "0") {
            GetVehicle_Manufacturer("-1", 'D', 'ddlManufacturerForVariant', '');
            GetVehicle_Name("-1", 'D', 'ddlVehicleForVariant', '');
            GetVehicle_Variant("0", 'G', 'divVariantListing', 'VariantListing');
        }
        else {
            GetVehicle_Manufacturer(e.target.value, 'D', 'ddlManufacturerForVariant', '');
            GetVehicle_Name("-1", 'D', 'ddlVehicleForVariant', '');
            GetVehicle_Variant("-1", 'G', 'divVariantListing', 'VariantListing');
        }
    });




    $(".ddlManufacturerForVariant").change(function (e) {


        if (e.target.value == "0") {

            GetVehicle_Name("-1", 'D', 'ddlVehicleForVariant', '');

        }
        else {
            GetVehicle_Name(e.target.value, 'D', 'ddlVehicleForVariant', '');
        }
        GetVehicle_Variant("-1", 'G', 'divVariantListing', 'VariantListing');
    });

    $(".ddlVehicleForVariant").change(function (e) {


        if (e.target.value == "0") {


            GetVehicle_Variant("-1", 'G', 'divVariantListing', 'VariantListing');

        }
        else {
            GetVehicle_Variant(e.target.value, 'G', 'divVariantListing', 'VariantListing');

        }

    });
    BindOnLoadVehicleVariantTab();
    //End Area

   
    


    ///Area For Vehicle Information Tab
    $(".ddlVehicleCategoryForVehicleInformation").change(function (e) {

        
        if (e.target.value == "0") {
            GetVehicle_Manufacturer("-1", 'D', 'ddlManufacturerForVehicleInformation', '');
            GetVehicle_Name("-1", 'D', 'ddlVehicleForVehicleInformation', '');
            GetVehicle_Variant("-1", 'D', 'ddlVariantForVehicleInformation', '');
        }
        else {
            GetVehicle_Manufacturer(e.target.value, 'D', 'ddlManufacturerForVehicleInformation', '');
            GetVehicle_Name("-1", 'D', 'ddlVehicleForVehicleInformation', '');
            GetVehicle_Variant("-1", 'D', 'divVariantListing', '');
        }
        GetVehicleDetailListing(true);
        
    });


    $(".ddlManufacturerForVehicleInformation").change(function (e) {


        if (e.target.value == "0") {

            GetVehicle_Name("-1", 'D', 'ddlVehicleForVehicleInformation', '');
            GetVehicle_Variant("-1", 'D', 'ddlVariantForVehicleInformation', '');
        }
        else {
            GetVehicle_Name(e.target.value, 'D', 'ddlVehicleForVehicleInformation', '');
            GetVehicle_Variant("-1", 'D', 'ddlVariantForVehicleInformation', '');
        }
        GetVehicleDetailListing(true);
    });


    $(".ddlVehicleForVehicleInformation").change(function (e) {


        if (e.target.value == "0") {


            GetVehicle_Variant("-1", 'D', 'ddlVariantForVehicleInformation', '');

        }
        else {
            GetVehicle_Variant(e.target.value, 'D', 'ddlVariantForVehicleInformation', '');

        }
        GetVehicleDetailListing(true);
    });


    $(".ddlVariantForVehicleInformation").change(function (e) {


        GetVehicleDetailListing(true);
    });

    BindOnLoadVehicleInformationTab();
    //End Area
}

function BindOnLoadVehicleTab() {
    GetVehicle_Manufacturer("-1", 'D', 'ddlManufacturerForVehicle', '');

    GetVehicle_Name("0", 'G', 'divVehicleListing', 'VehicleListing');
}


function BindOnLoadVehicleVariantTab() {
    GetVehicle_Manufacturer("-1", 'D', 'ddlManufacturerForVariant', '');
    GetVehicle_Name("-1", 'D', 'ddlVehicleForVariant', '');
    GetVehicle_Variant("0", 'G', 'divVariantListing', 'VariantListing');
    
}

function BindOnLoadVehicleInformationTab() {
    GetVehicle_Manufacturer("-1", 'D', 'ddlManufacturerForVehicleInformation', '');
    GetVehicle_Name("-1", 'D', 'ddlVehicleForVehicleInformation', '');
    GetVehicle_Variant("-1", 'D', 'ddlVariantForVehicleInformation', '');
    GetVehicleDetailListing(true);
}


function GetEmployee() {
    GroupId = $('.ddlGroup').val();
    CompanyId = $('.ddlCompany').val();
    if (CompanyId > 0) {
        CategoryId = $('.ddlCategoryC').val();

        //DesignationName = $('.txtDesignationName').val();
        ProgressBarShow();
        var service = new HrmsSuiteHcmService.HcmService();
        service.getCategoryWiseVehicleList(GroupId, CompanyId, CategoryId, null, onGetDesignationList, null, null);
    }
}

function onGetDesignationList(result) {
    
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.tbodyCarInformation').html('');
    $('#CarInformation').tmpl(res).appendTo(divTbodyGoalFund);
    paginateTable('.tableEmployee', 50);
    ProgressBarHide();
}


//function GetEmployee() {
//    GroupId = $('.ddlGroup').val();
//    CompanyId = $('.ddlCompany').val();
//    if (CompanyId > 0) {
//        CategoryId = $('.ddlCategoryC').val();
//        DesignationId = $('.ddlDesignation').val();
//        //DesignationName = $('.txtDesignationName').val();
//        ProgressBarShow();
//        var service = new HrmsSuiteHcmService.HcmService();
//        service.getDesignationList(GroupId, CompanyId, CategoryId, DesignationId, null, onGetDesignationList, null, null);
//    }
//}

//function onGetDesignationList(result) {
//    
//    var res = jQuery.parseJSON(result);
//    var divTbodyGoalFund = $('.tbodyCarInformation').html('');
//    $('#CarInformation').tmpl(res).appendTo(divTbodyGoalFund);
//    paginateTable('.tableEmployee', 50);
//    ProgressBarHide();
//}

function GetFromSetupDetail(ParentId, MasterId, _cssClass) {
    if (ParentId != 0) {
        ParentId = $(ParentId).val();
    }
    var service = new HrmsSuiteHcmService.HcmService();

    service.getFromSetupDetail(0, ParentId, MasterId, onGetFromSetupDetail, null, null);
    cssClass = _cssClass;
}

function onGetFromSetupDetail(result) {
    
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(cssClass, res);
    $(cssClass).val(res[0].Id).change();
    if (FillQueue == 0) {
        GetFromSetupDetail(0, HCM_SetupMaster.Vehicle, '.ddlVehicleTypeX');
    }
    FillQueue += 1;
}

function mangeVehicles(_CategoryId, selector) {
    
    FillQueue = 0;
    GetFromSetupDetail(0, HCM_SetupMaster.Vehicle, '.ddlVehicleType');
    CategoryId = _CategoryId;
    GetCategoryVehicleMappingNew();
    GetCategoryVehicleMappingUpgradeNew();

    var CategoryName = $(selector).closest('td').closest('tr').find('.tdCategoryName').html();

    $('.titleModalMapping').html('');
    $('.titleModalMapping').append(CategoryName);
    //resetControls('.formVehicleDetail');

}

function GetDesignationVehicleMapping() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getDesignationVehicleMapping(DesignationId, false, onGetDesignationVehicleMapping, null, null);

}

function onGetDesignationVehicleMapping(result) {
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.SelectedEligCarList').html('');
    $('#VehicleDesignationMappingElig').tmpl(res).appendTo(divTbodyGoalFund);
}


function GetDesignationVehicleMappingUpgrade() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getDesignationVehicleMapping(DesignationId, true, onGetDesignationVehicleMappingUpgrade, null, null);

}

function onGetDesignationVehicleMappingUpgrade(result) {
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.SelectedUpgCarList').html('');
    $('#VehicleDesignationMappingUpg').tmpl(res).appendTo(divTbodyGoalFund);
}


function getManufacturers(select) {
    if ($(select).hasClass('ddlVehicleType'))
        FillManufacturerDDL('.ddlVehicleType', HCM_SetupMaster.Manufacturer, '.ddlManufacturer')
    else
        FillManufacturerDDL('.ddlVehicleTypeX', HCM_SetupMaster.Manufacturer, '.ddlManufacturerX')
}




function FillManufacturerDDL(ParentId, MasterId, _cssClass) {
    if ($(ParentId).val() != 0) {
        ParentId = $(ParentId).val();
        var service = new HrmsSuiteHcmService.HcmService();
        service.getFromSetupDetail(CompanyId, ParentId, MasterId, onFillManufacturerDDL, null, null);
        cssClass = _cssClass;
    }
}

function onFillManufacturerDDL(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(cssClass, res);
}



function GetVehListFromSetupDetail() {
    var _Vehicle = $('.ddlVehicle').val();
    var service = new HrmsSuiteHcmService.HcmService();
    service.getFromSetupDetail(CompanyId, _Vehicle, HCM_SetupMaster.VehicleVariant, onGetVehListFromSetupDetail, null, null);
}

function onGetVehListFromSetupDetail(result) {
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.divListVehicle').html('');
    $('#VehicleList').tmpl(res).appendTo(divTbodyGoalFund);

}

function GetVehListFromSetupDetailX() {
    var _Vehicle = $('.ddlVehicleX').val();
    var service = new HrmsSuiteHcmService.HcmService();
    service.getFromSetupDetail(CompanyId, _Vehicle, HCM_SetupMaster.VehicleVariant, onGetVehListFromSetupDetailX, null, null);
}

function onGetVehListFromSetupDetailX(result) {
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.divListVehicleX').html('');
    $('#VehicleListX').tmpl(res).appendTo(divTbodyGoalFund);
}


function saveVehicleDesignationMapping() {
    var ResponseForm = [];

    $('.SelectedEligCarList').find('tr').each(function () {
        var Response = new Object();
        var VehicleId = $(this).first('td').find('input').val();
        Response.VehicleId = VehicleId;
        Response.DesignationId = DesignationId;
        Response.IsUpgradeVehicle = 0;
        ResponseForm.push(Response);
    });

    $('.SelectedUpgCarList').find('tr').each(function () {
        var Response = new Object();
        var VehicleId = $(this).first('td').find('input').val();
        if (VehicleId == '' || VehicleId == undefined)
            return;
        Response.VehicleId = VehicleId;
        Response.DesignationId = DesignationId;
        Response.IsUpgradeVehicle = 1;
        ResponseForm.push(Response);
    });
    if (ResponseForm.length == 0) {
        var ResponseZero = new Object();
        ResponseZero.VehicleId = 0;
        ResponseZero.DesignationId = DesignationId;
        ResponseZero.IsUpgradeVehicle = 1;
        ResponseForm.push(ResponseZero);

        //alert(JSON.stringify(ResponseForm));
    }

    ProgressBarShow();
    var JSONResponse = JSON.stringify(ResponseForm);
    var service = new HrmsSuiteHcmService.HcmService();
    service.saveUpdatedDesignationVehiclesMapping(JSONResponse, onsaveVehicleDesignationMapping, null, null);
}


function onsaveVehicleDesignationMapping(result) {
    if (result == 1)
        showSuccess('Vehicles Successfully Mapped');
    GetEmployee();
    ProgressBarHide();
}


function bindtoTable(chkLst, table) {
    var tdlist = '';
    $(chkLst).each(function () {
        if ($(this).is(':checked')) {
            if (!checkValueExistinRow($(this).val(), table))
                return false;

            tdlist += '<tr><td><input type="hidden" value="' + $(this).val() + '"/>';
            tdlist += $(this).next('label').html() + '</td>';
            tdlist += '<td><button onclick="removeRow(this)"  class="btn btn-danger btn-sm" type="button"><i class="fa fa-times"></i></button></td></tr>';
        }
    });

    $(table).append(tdlist);

}



function checkValueExistinRow(val, selector) {
    myBool = true;
    $(selector).find('tr').each(function () {
        var a = $(this).first('td').find('input').val();
        if (a == val)
            myBool = false;
    });

    return myBool;
}

function removeRow(selector) {
    $(selector).closest('tr').remove();
}







///////////////////////////////////////////////////////////////////////////////////////////////////////////



function GetVehicleType(ParentId, MasterId) {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getFromSetupDetail(0, ParentId, MasterId, onGetVehicleType, null, null);
}

function onGetVehicleType(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference('.ddlVehicleCategoryForManufacture', res);
    FillDropDownByReference('.ddlVehicleCategoryForVehicle', res);
    FillDropDownByReference('.ddlVehicleCategoryForVariant', res);
    FillDropDownByReference('.ddlVehicleCategoryForVehicleInformation', res);
    $('.ddlVehicleCategory').val(res[0].Id).change();
}


function GetManufacturer(VehicleType) {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getFromSetupDetail(CompanyId, $(VehicleType).val(), HCM_SetupMaster.Manufacturer, onGetManufacturer, null, null);
}

function onGetManufacturer(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference('.ddlManufacturer', res);
}


function SaveVehicleInformation() {
    
    flag = true;

    if (serializedObject == serializeData('.formVehicleDetail')) {
        showError('No Values Has Been Changed');
        return false;
    }

    VehicleTypeId = $('.formVehicleDetail').find('.ddlVehicleCategoryForVehicleInformation').val();
    ManufacturerId = $('.formVehicleDetail').find('.ddlManufacturerForVehicleInformation').val();
    VehicleNameId = $('.formVehicleDetail').find('.ddlVehicleForVehicleInformation').val();
    VariantId = $('.formVehicleDetail').find('.ddlVariantForVehicleInformation').val();
    ModelYear = $('.formVehicleDetail').find('.ModelYear').val();
    HP = $('.formVehicleDetail').find('.HP').val();
    //PurchaseAmount = $('.txtPurchaseAmnt').val();
    //BookValue = $('.txtBookValue').val();
    FuelType = $('.formVehicleDetail').find('.ddlFuelType').val();

    if (VehicleInfoId == null) {
        $('.VariantId').each(function () {
            if ($(this).val() == VariantId) {
                objTR = $(this).closest('tr');

                var TypeFuelId = objTR.find('.FuelTypeId').val();
                var YearModel = objTR.find('.Model').html();

                if (YearModel == ModelYear && TypeFuelId == FuelType)
                    flag = false;
            }
        });


        if (!flag) {



            showError('Information Already Exists');
            return;
        }
    }


    //if (parseFloat(PurchaseAmount) < parseFloat(BookValue)) {
    //    showError('Book Value Cannot Be Greater Than Purchase Amount');
    //    return;
    //}

    if (ModelYear.length > 4 || HP.length > 4) {
        showError('Horse Power and Model Year Cannot Be Greater Than Four Digits');
        return;
    }
    ProgressBarShow();
    var service = new HrmsSuiteHcmService.HcmService();


    service.SaveVehicleInformation(VariantId, VehicleNameId, ManufacturerId, VehicleTypeId, FuelType, ModelYear, HP, VehicleInfoId, onSaveVehicleInformation, null, VehicleInfoId == null ? false : true);
}


function onSaveVehicleInformation(result,isEdit) {

    if (result == 1) {
        if (isEdit)
            showSuccess('Successfully Updated!');
        else
            showSuccess('Successfully Inserted!');
    }
        

    VehicleInfoId = null;
    Clear();
    

    ProgressBarHide();
}

function Clear() {
    VehicleInfoId = null;
    GetVehicle_Manufacturer("-1", 'D', 'ddlManufacturerForVehicleInformation', '');
    GetVehicle_Name("-1", 'D', 'ddlVehicleForVehicleInformation', '');
    GetVehicle_Variant("-1", 'D', 'ddlVariantForVehicleInformation', '');
    resetControlsWithoutChangeEvent('.formVehicleDetail');
    GetVehicleDetailListing(true);
}



function GetVehicleDetailListing(RefreshBit) {
    
    //if (!validateForm('.divGroupAndCompany'))
    //    return;

    if (!RefreshBit)
        $('.modalOpenManageVehicle').click();
    
    //CompanyId = $('.ddlCompany').val();

    const vehicleTypeid = $(".ddlVehicleCategoryForVehicleInformation").val() == null ? "0" : $(".ddlVehicleCategoryForVehicleInformation").val();
    const manufactureId = $(".ddlManufacturerForVehicleInformation").val() == null ? "0" : $(".ddlManufacturerForVehicleInformation").val();
    const vehicleId = $(".ddlVehicleForVehicleInformation").val() == null ? "0" : $(".ddlVehicleForVehicleInformation").val();
    const variantId = $(".ddlVariantForVehicleInformation").val() == null ? "0" : $(".ddlVariantForVehicleInformation").val();



    var service = new HrmsSuiteHcmService.HcmService();
    ProgressBarShow();
    service.GetVehicleInformationListing(vehicleTypeid, manufactureId, vehicleId,variantId,onGetVehicleDetailListing, null, null);
}

function onGetVehicleDetailListing(result) {

    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.VehicleListingTBody').html('');
    $('#VehicleDetailListing').tmpl(res).appendTo(divTbodyGoalFund);
    ProgressBarHide();
}



function onEdit(selector) {
   
    ProgressBarShow();

    
    var obj = $(selector).closest('tr').first('td');
    var row = $(selector).closest('tr');
    var objTab = row.closest('.tab-pane');



    VehicleInfoId = obj.find('.VehicleInfoId').val();

    $('.txtVehicle').val(row.find('.Vehicle').text());
    $('.ModelYear').val(row.find('.Model').text());
    $('.HP').val(row.find('.HorsePower').text());
    //$('.txtPurchaseAmnt').val(row.find('.PurchaseAmount').text());
    //$('.txtBookValue').val(row.find('.BookValue').text());

    $('.ddlFuelType').val(obj.find('.FuelTypeId').val());


    GetVehicle_Manufacturer(obj.find('.CategoryId').val(), 'D', 'ddlManufacturerForVehicleInformation', '', obj.find('.ManufacturerId').val());
    GetVehicle_Name(obj.find('.ManufacturerId').val(), 'D', 'ddlVehicleForVehicleInformation', '', obj.find('.VehicleId').val());
    GetVehicle_Variant(obj.find('.VehicleId').val(), 'D', 'ddlVariantForVehicleInformation', '', obj.find('.VariantId').val());

 
    objTab.find('.ddlVehicleCategoryForVehicleInformation').val(obj.find('.CategoryId').val());

}



//////////////////////////////////////////////////////////////////////////////////////////

function GetManufacturerListing(selector) {

    var VehicleCategoryId = $(selector).val();

    if ($(selector).val() == 0)
        return false;
    var service = new HrmsSuiteHcmService.HcmService();
    service.getFromSetupDetail(CompanyId, VehicleCategoryId, HCM_SetupMaster.Manufacturer, onGetManufacturerListing, null, null);
}

function onGetManufacturerListing(result) {
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.divManufacturerListing').html('');
    $('#ManufacturerListing').tmpl(res).appendTo(divTbodyGoalFund);
}



function onDeleteManufacturer(selector) {
    var SetupDetailid = $(selector).closest('tr').find('.Manufacturer_SetupDetailId').val();
    var service = new HrmsSuiteHcmService.HcmService();
    service.deleteFromSetupDetail(SetupDetailid, OnDeleteManufacturerRecord, null, null);
}


function OnDeleteManufacturerRecord(result) {
    if (result == 1)
        showSuccess('Vehicle Has Been Deleted Successfully');
    /* GetManufacturerListing('.ddlVehicleCategory');*/

    clearManufacturerFields();
}


function onEditManufacturer(selector) {
    

    Manufacture_SetupDetailid = $(selector).closest('tr').find('.Manufacturer_SetupDetailId').val();
    var val = $(selector).closest('tr').find('.tdManufacturerName').html();
    var parentid = $(selector).closest('tr').find('.Manufacturer_ParentId').val();
    $('.ddlVehicleCategoryForManufacture').val(parentid);
    $('.txtManufacturerName').val(val);
    isEditManufacture = true;
}


function onSaveManufacturer() {
    
    var _CompanyId = 0;

    if (!validateForm('.divManufacturer'))
        return;

    var flag = true;
    var ManufacturerName = $('.txtManufacturerName').val();

    if (isEditManufacture) {
        ProgressBarShow();
        var service = new HrmsSuiteHcmService.HcmService();
        service.saveToSetupDetail(_CompanyId, HCM_SetupMaster.Manufacturer, ManufacturerName, Manufacture_SetupDetailid, $('.ddlVehicleCategoryForManufacture').val(), onSaveManufacturerRecord, null, true);
    }
    else {

        $('.tdManufacturerName').each(function () {
            var val = $(this).html();
            if (ManufacturerName.trim().toLowerCase() == val.trim().toLowerCase())
                flag = false;
        });

        if (flag) {
            ProgressBarShow();
            var service = new HrmsSuiteHcmService.HcmService();
            service.saveToSetupDetail(_CompanyId, HCM_SetupMaster.Manufacturer, ManufacturerName, null, $('.ddlVehicleCategoryForManufacture').val(), onSaveManufacturerRecord, null, false);
        }
        else
            showError('Manufacturer Already Been Exists');
    }

}

function onSaveManufacturerRecord(result,isEdit) {
    if (result == 1) {
        if (isEdit)
            showSuccess('Manufacturer Has Been Updated');
        else {
            showSuccess('Manufacturer Has Been Inserted');
        }
    }
       
    //GetManufacturerListing('.ddlVehicleCategory');
    clearManufacturerFields();

    $('.txtManufacturerName').val('');
    GetVehicle_Manufacturer("0", 'G', 'divManufacturerListing', 'ManufacturerListing');

    ProgressBarHide();
}


function clearManufacturerFields() {

    isEditManufacture = false;
    resetControlsWithoutChangeEvent('.divManufacturer');
    GetVehicle_Manufacturer("0", 'G', 'divManufacturerListing', 'ManufacturerListing');
}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////


function onChangeCategoryVehicle(selector) {
    GetManufacturer(selector);
    GetVehicleInfoListing(selector);
}


function GetVehicleInfoListing(selector) {

    if ($(selector).val() == 0)
        return false;
    var service = new HrmsSuiteHcmService.HcmService();
    service.getVehicleInfoListing($(selector).val(), onGetVehicleInfoListing, null, null);
}

function onGetVehicleInfoListing(result) {
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.divVehicleListing').html('');
    $('#VehicleListing').tmpl(res).appendTo(divTbodyGoalFund);
}


function SaveVehicleTabInfo(selector) {


    var _CompanyId = 0;

    flag = true;
    if (!validateForm('.divVehicle'))
        return;

    objTab = $(selector).closest('.tab-pane');
    var ManufacturerId = objTab.find('.ddlManufacturerForVehicle').val();
    var VehicleName = objTab.find('.txtVehicleName').val();

    if (isEditVehicle) {
        ProgressBarShow();
        var service = new HrmsSuiteHcmService.HcmService();
        service.saveToSetupDetail(_CompanyId, HCM_SetupMaster.VehicleName, VehicleName, TabVehicleId, ManufacturerId, onSaveVehicleTabInfo, null, true);
    }
    else {

        $('.tdVehicleName').each(function () {
            var val = $(this).html();
            if (val.trim().toLowerCase() == $('.txtVehicleName').val())
                flag = false;
        });

        if (flag) {
            ProgressBarShow();
            var service = new HrmsSuiteHcmService.HcmService();
            service.saveToSetupDetail(_CompanyId, HCM_SetupMaster.VehicleName, VehicleName, null, ManufacturerId, onSaveVehicleTabInfo, null, false);
        }
        else
            showError('Vehicle Name Already Exists');
    }
}



function onSaveVehicleTabInfo(result,isEdit) {
    if (result == 1) {

        if (isEdit) {
            showSuccess('Vehicle Updated Successfully');
        }
        else {
            showSuccess('Vehicle Inserted Successfully');
        }
    }
        
    //var objCat = $('#VehicleTab').find('.ddlVehicleCategory')
    //GetVehicleInfoListing(objCat);
    clearVehicleTabFields();

    $('.txtVehicleName').val('');
    //GetVehicle_Name("0", 'G', 'divVehicleListing', 'VehicleListing');


}


function EditVehicleTabInfo(selector) {
    var objRow = $(selector).closest('tr');
    var objTab = objRow.closest('.tab-pane');

    var VehicleName = objRow.find('.tdVehicleName').html();
    TabVehicleId = objRow.find('.Vehicle_SetupDetailId').val();
    var ManufacturerId = objRow.find('.Vehicle_ParentId').val();
    var CategotyText = objRow.find('.categorytext').html();
    var value = $(".ddlVehicleCategoryForVehicle option:contains('" + CategotyText + "')").val();


    objTab.find('.ddlVehicleCategoryForVehicle').val(value);
    GetVehicle_Manufacturer(value, 'D', 'ddlManufacturerForVehicle', '', ManufacturerId);



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
    //var objCat = $('#VehicleTab').find('.ddlVehicleCategory')
    //GetVehicleInfoListing(objCat);

    clearVehicleTabFields();
}


function clearVehicleTabFields() {
    GetVehicle_Manufacturer("-1", 'D', 'ddlManufacturerForVehicle', '');
    isEditVehicle = false;
    resetControlsWithoutChangeEvent('.divVehicle');

    GetVehicle_Name("0", 'G', 'divVehicleListing', 'VehicleListing');
}


function GetVehicleListingX(selector) {
    if ($(selector).val() == 0)
        return false;
    var service = new HrmsSuiteHcmService.HcmService();
    service.getFromSetupDetail(CompanyId, $(selector).val(), HCM_SetupMaster.Manufacturer, onGetVehicleListingX, null, null);
}

function onGetVehicleListingX(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference('.ddlVehicleX', res);
}


////////////////////////////////////////////////////////

function GetVehicleListing(selector) {

    if ($(selector).val() == 0)
        return false;
    var service = new HrmsSuiteHcmService.HcmService();
    service.getFromSetupDetail(CompanyId, $(selector).val(), HCM_SetupMaster.Manufacturer, onGetVehicleListing, null, null);
}

function onGetVehicleListing(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference('.ddlVehicle', res);
}


function SaveVariantTabInfo(selector) {

    var _CompanyId = 0;

    flag = true;
    if (!validateForm('.divVariant'))
        return;

    objTab = $(selector).closest('.tab-pane');
    var VehicleId = objTab.find('.ddlVehicleForVariant').val();
    var VariantName = objTab.find('.txtVariant').val();

    if (isVariantEdit) {
        ProgressBarShow();
        var service = new HrmsSuiteHcmService.HcmService();
        service.saveToSetupDetail(_CompanyId, HCM_SetupMaster.VehicleVariant, VariantName, TabVariantId, VehicleId, onSaveVariantTabInfo, null, true);
    }
    else {

        $('.tdVariantName').each(function () {
            var val = $(this).html();
            if (val.trim().toLowerCase() == VariantName.trim().toLowerCase())
                flag = false;
        });

        if (flag) {
            ProgressBarShow();
            var service = new HrmsSuiteHcmService.HcmService();
            service.saveToSetupDetail(_CompanyId, HCM_SetupMaster.VehicleVariant, VariantName, null, VehicleId, onSaveVariantTabInfo, null, false);
        }
        else
            showError('Variant Name Already Exists');
    }


}


function onSaveVariantTabInfo(result,isEdit) {
    if (result == 1) {
        if (isEdit)
            showSuccess('Variant Updated Successfully');
        else
            showSuccess('Variant Inserted Successfully');

    }
        
    //var objVeh = $('#VehicleVariants').find('.ddlVehicle')
    //GetVariantListing(objVeh);
    clearVariantTabFields();

    $('.txtVariant').val('');

    GetVehicle_Variant("0", 'G', 'divVariantListing', 'VariantListing');
}






function onEditVariantInfo(selector) {
    var objRow = $(selector).closest('tr');
   

    var VariantName = objRow.find('.tdVariantName').html();
    var ManufacturerName = objRow.find('.tdlevel1').html();
    var VehicleTypeName = objRow.find('.tdlevel3').html();
    TabVariantId = objRow.find('.Variant_SetupDetailId').val();
    var VehicleId = objRow.find('.Variant_ParentId').val();
    
    var valueVehicleType = $(".ddlVehicleCategoryForVariant option:contains('" + VehicleTypeName + "')").val();
    $('.ddlVehicleCategoryForVariant').val(valueVehicleType);


    GetVehicle_Manufacturer(valueVehicleType, 'D', 'ddlManufacturerForVariant', '');
    
    var valueManufacturer;
    setTimeout(function () {
         valueManufacturer = $(".ddlManufacturerForVariant option:contains('" + ManufacturerName + "')").val();
        GetVehicle_Name(valueManufacturer, 'D', 'ddlVehicleForVariant', '', VehicleId);
    }, 200)
    ////var CategoryId = objRow.find('.Vehicle_CategoryId').val();


    setTimeout(function () {  $('.ddlManufacturerForVariant').val(valueManufacturer); }, 300);
    

    $('.txtVariant').val(VariantName);
    //$('.divVariantCategory').hide();
    //$('.divVariantManufacture').hide();

    isVariantEdit = true;
}



function DeleteVariantInfo(selector) {
    var SetupDetailid = $(selector).closest('tr').find('.Variant_SetupDetailId').val();
    var service = new HrmsSuiteHcmService.HcmService();
    service.deleteFromSetupDetail(SetupDetailid, onDeleteVariantInfo, null, null);
}



function onDeleteVariantInfo(result) {
    if (result == 1)
        showSuccess('Successsfully Deleted');
    var objVeh = $('#VehicleVariants').find('.ddlVehicle')
    //GetVariantListing(objVeh);
    clearVariantTabFields();
}





function GetVariantListing(selector) {
    if ($(selector).val() == 0)
        return false;
    var service = new HrmsSuiteHcmService.HcmService();
    service.getFromSetupDetail(CompanyId, $(selector).val(), HCM_SetupMaster.VehicleVariant, onGetVariantListing, null, null);
}

function onGetVariantListing(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference('.ddlVariant', res);
    var divTbodyGoalFund = $('.divVariantListing').html('');
    $('#VariantListing').tmpl(res).appendTo(divTbodyGoalFund);
}




function clearVariantTabFields() {
    GetVehicle_Manufacturer("-1", 'D', 'ddlManufacturerForVariant', '');
    GetVehicle_Name("-1", 'D', 'ddlVehicleForVariant', '');
    isVariantEdit = false;
    resetControlsWithoutChangeEvent('.divVariant');
    GetVehicle_Variant("0", 'G', 'divVariantListing', 'VariantListing');
   
}

function GetFuelTypes() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getFromSetupDetail(0, 0, HCM_SetupMaster.FuelType, onGetFuelTypes, null, null);
}

function onGetFuelTypes(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference('.ddlFuelType', res);
    //$('.ddlFuelType').val(res[0].Id);
}



function onDeleteVehicle(selector) {
    var obj = $(selector).closest('tr').first('td');
    VehicleInfoId = obj.find('.VehicleInfoId').val();
    var service = new HrmsSuiteHcmService.HcmService();
    service.deleteVehicleInfo(VehicleInfoId, DeleteCompletition, null, null);
}

function DeleteCompletition(result) {
    if (result == 1)
        showSuccess('Successfully Deleted!');
    Clear();    
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
    //$('.ddlVehicleCategory').val(res[0].Id);
    $('.ddlVehicleCategory').change();
}

function GetVehicle_Manufacturer(VehicleTypeId, ControlType, AssignedClass, AssignedelementId,SelectedValue = "0") {
    
    var VehicleTypeId = VehicleTypeId;
    var service = new HrmsSuiteHcmService.HcmService();

    if (ControlType == 'G') {
        ProgressBarShow();
        service.GetManufacturer(VehicleTypeId, OnGetVehicle_ManufacturerGrid, null, [AssignedClass, AssignedelementId, SelectedValue]);
    }
    else if (ControlType == 'D') {
        
        service.GetManufacturer(VehicleTypeId, OnGetVehicle_ManufacturerDropDown, null, [AssignedClass, AssignedelementId, SelectedValue]);
    }
}

function OnGetVehicle_ManufacturerGrid(result, AssignedArray) {
    
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.' + AssignedArray[0]+'').html('');
    $('#' + AssignedArray[1] +'').tmpl(res).appendTo(divTbodyGoalFund);
    ProgressBarHide();
}

function OnGetVehicle_ManufacturerDropDown(result, AssignedArray) {
    
    var res = jQuery.parseJSON(result);
    FillDropDownByReference('.' + AssignedArray[0]+'', res);
    $('.' + AssignedArray[0] + '').val(AssignedArray[2]);
    //$('.' + AssignedArray[0] + '').change();

   /// GetVehicle_Name('.'+ AssignedClass + '', 'G');
    ProgressBarHide();
}

function GetVehicle_Name(ManufacturerId, ControlType, AssignedClass, AssignedelementId,SelectedValue = "0") {
    
    var ManufacturerId = ManufacturerId ;
    var service = new HrmsSuiteHcmService.HcmService();

    if (ControlType == 'G') {
        ProgressBarShow();
        service.GetVehicleName(ManufacturerId, OnGetVehicle_NameGrid, null, [AssignedClass, AssignedelementId, SelectedValue]);
    }
    else if (ControlType == 'D') {
        
        service.GetVehicleName(ManufacturerId, OnGetVehicle_NameDropDown, null, [AssignedClass, AssignedelementId, SelectedValue]);
    }
}

function OnGetVehicle_NameGrid(result, AssignedArray) {
    
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.' + AssignedArray[0] + '').html('');
    $('#' + AssignedArray[1] + '').tmpl(res).appendTo(divTbodyGoalFund);
    ProgressBarHide();
}

function OnGetVehicle_NameDropDown(result, AssignedArray) {
    
    var res = jQuery.parseJSON(result);
    FillDropDownByReference('.' + AssignedArray[0] + '', res);
    $('.' + AssignedArray[0] + '').val(AssignedArray[2]);
    //$('.ddlVehicle').change();

    /*GetVehicle_Variant('.ddlVehicle', 'G');*/

    //setTimeout(
    //    function () {
    //        GetVehicle_Variant('.ddlVehicle', 'D');
    //    }, 1000);
    ProgressBarHide();
}

function GetVehicle_Variant(VehicleId, ControlType, AssignedClass, AssignedelementId,SelectedValue = "0") {
    
    var VehicleId = VehicleId;
    var service = new HrmsSuiteHcmService.HcmService();

    if (ControlType == 'G') {
        ProgressBarShow();
        service.GetVehicleVariant(VehicleId, OnGetVehicle_VariantGrid, null, [AssignedClass, AssignedelementId, SelectedValue]);
    }
    else if (ControlType == 'D') {
        
        service.GetVehicleVariant(VehicleId, OnGetVehicle_VariantDropDown, null, [AssignedClass, AssignedelementId, SelectedValue]);
    }
    else if (ControlType == 'CHK') {
        ProgressBarShow();
        service.GetVehicleVariant(VehicleId, OnGetVehicle_VariantChkBox, null, null);
    }
}

function OnGetVehicle_VariantGrid(result, AssignedArray) {
    
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.' + AssignedArray[0] + '').html('');
    $('#' + AssignedArray[1] + '').tmpl(res).appendTo(divTbodyGoalFund);
    ProgressBarHide();
}

function OnGetVehicle_VariantDropDown(result, AssignedArray) {
    
    var res = jQuery.parseJSON(result);
    FillDropDownByReference('.' + AssignedArray[0] + '', res);
    $('.' + AssignedArray[0] + '').val(AssignedArray[2]);
    //$('.ddlVariant').change();
    ProgressBarHide();
}

function OnGetVehicle_VariantChkBox(result) {


    var res = jQuery.parseJSON(result);
    //var divTbodyGoalFund = $('.divVariantListing').html('');
    //$('#VariantListing').tmpl(res).appendTo(divTbodyGoalFund);

    var divTbodyGoalFund = $('.divListVehicle').html('');
    $('#VehicleList').tmpl(res).appendTo(divTbodyGoalFund);
    ProgressBarHide();
}



function GetVehicle_ManufacturerX(selector, ControlType) {

    var VehicleTypeId = $(selector).val();
    var service = new HrmsSuiteHcmService.HcmService();

    if (ControlType == 'G') {
        service.GetManufacturer(VehicleTypeId, OnGetVehicle_ManufacturerGrid, null, null);
    }
    else if (ControlType == 'D') {
        service.GetManufacturer(VehicleTypeId, OnGetVehicle_ManufacturerDropDownX, null, null);
    }
}

function OnGetVehicle_ManufacturerDropDownX(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference('.ddlManufacturerX', res);

}


function GetVehicle_NameX(selector, ControlType) {

    var ManufacturerId = $(selector).val();
    var service = new HrmsSuiteHcmService.HcmService();

    if (ControlType == 'G') {
        service.GetVehicleName(ManufacturerId, OnGetVehicle_NameGrid, null, null);
    }
    else if (ControlType == 'D') {
        service.GetVehicleName(ManufacturerId, OnGetVehicle_NameDropDownX, null, null);
    }
}

function OnGetVehicle_NameDropDownX(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference('.ddlVehicleX', res);

}


function GetVehicle_VariantX(selector, ControlType) {

    var VehicleId = $(selector).val();
    var service = new HrmsSuiteHcmService.HcmService();

    if (ControlType == 'G') {
        service.GetVehicleVariant(VehicleId, OnGetVehicle_VariantGrid, null, null);
    }
    else if (ControlType == 'D') {
        service.GetVehicleVariant(VehicleId, OnGetVehicle_VariantDropDown, null, null);
    }
    else if (ControlType == 'CHK') {
        service.GetVehicleVariant(VehicleId, OnGetVehicle_VariantChkBoxX, null, null);
    }
}

function OnGetVehicle_VariantChkBoxX(result) {


    var res = jQuery.parseJSON(result);
    //var divTbodyGoalFund = $('.divVariantListing').html('');
    //$('#VariantListing').tmpl(res).appendTo(divTbodyGoalFund);

    var divTbodyGoalFund = $('.divListVehicleX').html('');
    $('#VehicleListX').tmpl(res).appendTo(divTbodyGoalFund);
}

function BindVehicleInGrid(ddlvehicle, table) {
    //var tdlist = '';
    //$(chkLst).each(function () {
    //    if ($(this).is(':checked')) {
    //        if (!checkValueExistinRow($(this).val(), table))
    //            return false;

    //        tdlist += '<tr><td><input type="hidden" value="' + $(this).val() + '"/>';
    //        tdlist += $(this).next('label').html() + '</td>';
    //        tdlist += '<td><button onclick="removeRow(this)"  class="btn btn-danger btn-sm" type="button"><i class="fa fa-times"></i></button></td></tr>';
    //    }
    //});

    //$(table).append(tdlist);


    var tdlist = '';

    var VehicleInfoId = $(ddlvehicle).val();

    if (VehicleInfoId > 0) {
        if (!checkValueExistinRowNew(VehicleInfoId, table))
            return false;

        tdlist += '<tr><td><input type="hidden" value="' + VehicleInfoId + '"/>';
        //tdlist += $(this).next('label').html() + '</td>';
        //tdlist += $(ddlvehicle).text() + '</td>';


        tdlist += $(ddlvehicle + " option:selected").text();


        tdlist += '<td><button onclick="removeRow(this)"  class="btn btn-danger btn-sm" type="button"><i class="fa fa-times"></i></button></td></tr>';

        $(table).append(tdlist);
    }
}

function GetVehicleInfoByVehicleType(ddlClass) {
    var VehicleTypeId = $('.' + ddlClass).val();
    if (VehicleTypeId == 0 || VehicleTypeId > 0) {
        var service = new HrmsSuiteHcmService.HcmService();
        if (ddlClass == 'ddlVehicleType_') {
            ProgressBarShow();
            service.GetVehicleInfoByVehicleType(VehicleTypeId, onGetVehicleInfoByVehicleType, null, null);
        }
        else if (ddlClass == 'ddlVehicleTypeX_') {
            ProgressBarShow();
            service.GetVehicleInfoByVehicleType(VehicleTypeId, onGetVehicleInfoByVehicleTypeX, null, null);
        }
    }
}

function onGetVehicleInfoByVehicleType(result) {

    var res = jQuery.parseJSON(result);
    FillDropDownByReference('.ddlVehicleInfo', res);
    ProgressBarHide();
}

function onGetVehicleInfoByVehicleTypeX(result) {

    var res = jQuery.parseJSON(result);
    FillDropDownByReference('.ddlVehicleInfoX', res);
    ProgressBarHide();
}

function mangeVehiclesNew(_CategoryID, selector) {
    
    FillQueue = 0;
    GetFromSetupDetail(0, HCM_SetupMaster.Vehicle, '.ddlVehicleType');

    CategoryId = _CategoryID;

    var CategoryName = $(selector).closest('td').closest('tr').find('.tdCategoryName').html();

    $('.titleModalMapping').html('');
    $('.titleModalMapping').append(CategoryName);
    GetCategoryVehicleMappingNew();
    GetCategoryVehicleMappingUpgradeNew();
    //resetControls('.formVehicleDetail');

}

//function mangeVehiclesNew(_DesignationId, selector) {
//    FillQueue = 0;
//    GetFromSetupDetail(0, HCM_SetupMaster.Vehicle, '.ddlVehicleType');
//    DesignationId = _DesignationId;
//    GetDesignationVehicleMappingNew();
//    GetDesignationVehicleMappingUpgradeNew();

//    var CategoryName = $(selector).closest('td').closest('tr').find('.tdCategoryName').html();
//    var DesignationName = $(selector).closest('td').closest('tr').find('.tdDesignationName').html();
//    $('.titleModalMapping').html('');
//    $('.titleModalMapping').append(DesignationName + ' - ' + CategoryName);
//    //resetControls('.formVehicleDetail');

//}

function checkValueExistinRowNew(val, selector) {

    myBool = true;
    $(selector).find('tr').each(function () {
        var a = $(this).first('td').find('input').val();
        if (a == val)
            myBool = false;
    });

    return myBool;
}

function GetCategoryVehicleMappingNew() {
    
    var service = new HrmsSuiteHcmService.HcmService();
    service.GetCategoryVehicleMappingNew(CategoryId, false, onGetCategoryVehicleMappingNew, null, null);

}

function onGetCategoryVehicleMappingNew(result) {
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.SelectedEligCarListNew').html('');
    $('#VehicleDesignationMappingEligNew').tmpl(res).appendTo(divTbodyGoalFund);
}

function GetCategoryVehicleMappingUpgradeNew() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.GetCategoryVehicleMappingNew(CategoryId, true, onGetCategoryVehicleMappingUpgradeNew, null, null);

}

function onGetCategoryVehicleMappingUpgradeNew(result) {
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.SelectedUpgCarListNew').html('');
    $('#VehicleDesignationMappingUpgradeNew').tmpl(res).appendTo(divTbodyGoalFund);
}

//function GetDesignationVehicleMappingNew() {
//    var service = new HrmsSuiteHcmService.HcmService();
//    service.GetDesignationVehicleMappingNew(DesignationId, false, onGetDesignationVehicleMappingNew, null, null);

//}

//function onGetDesignationVehicleMappingNew(result) {
//    var res = jQuery.parseJSON(result);
//    var divTbodyGoalFund = $('.SelectedEligCarListNew').html('');
//    $('#VehicleDesignationMappingEligNew').tmpl(res).appendTo(divTbodyGoalFund);
//}

//function GetDesignationVehicleMappingUpgradeNew() {
//    var service = new HrmsSuiteHcmService.HcmService();
//    service.GetDesignationVehicleMappingNew(DesignationId, true, onGetDesignationVehicleMappingUpgradeNew, null, null);

//}

//function onGetDesignationVehicleMappingUpgradeNew(result) {
//    var res = jQuery.parseJSON(result);
//    var divTbodyGoalFund = $('.SelectedUpgCarListNew').html('');
//    $('#VehicleDesignationMappingUpgradeNew').tmpl(res).appendTo(divTbodyGoalFund);
//}
function SaveVehicleCategoryMappingNew() {
    var ResponseForm = [];
    
    $('.SelectedEligCarListNew').find('tr').each(function () {
        var Response = new Object();
        var VehicleInformationId = $(this).first('td').find('input').val();
        Response.VehicleInformationId = VehicleInformationId;
        Response.CategoryId = CategoryId;
        Response.IsUpgradeVehicle = 0;
        ResponseForm.push(Response);
    });

    $('.SelectedUpgCarListNew').find('tr').each(function () {
        var Response = new Object();
        var VehicleInformationId = $(this).first('td').find('input').val();
        if (VehicleInformationId == '' || VehicleInformationId == undefined)
            return;
        Response.VehicleInformationId = VehicleInformationId;
        Response.CategoryId = CategoryId;
        Response.IsUpgradeVehicle = 1;
        ResponseForm.push(Response);
    });

    //if (ResponseForm.length == 0) {
    //    var ResponseZero = new Object();
    //    ResponseZero.VehicleId = 0;
    //    ResponseZero.DesignationId = DesignationId;
    //    ResponseZero.IsUpgradeVehicle = 1;
    //    ResponseForm.push(ResponseZero);

    //    //alert(JSON.stringify(ResponseForm));
    //}

    ProgressBarShow();
    var JSONResponse = JSON.stringify(ResponseForm);
    var service = new HrmsSuiteHcmService.HcmService();
    service.SaveUpdatedCategoryVehiclesMappingNew(JSONResponse, onSaveVehicleCategoryMappingNew, null, null);
}

function onSaveVehicleCategoryMappingNew(result) {
    if (result == 1)
        showSuccess('Vehicles Successfully Mapped');
    GetEmployee();
    ProgressBarHide();
}



//function SaveVehicleDesignationMappingNew() {
//    var ResponseForm = [];

//    $('.SelectedEligCarListNew').find('tr').each(function () {
//        var Response = new Object();
//        var VehicleInformationId = $(this).first('td').find('input').val();
//        Response.VehicleInformationId = VehicleInformationId;
//        Response.DesignationId = DesignationId;
//        Response.IsUpgradeVehicle = 0;
//        ResponseForm.push(Response);
//    });

//    $('.SelectedUpgCarListNew').find('tr').each(function () {
//        var Response = new Object();
//        var VehicleInformationId = $(this).first('td').find('input').val();
//        if (VehicleInformationId == '' || VehicleInformationId == undefined)
//            return;
//        Response.VehicleInformationId = VehicleInformationId;
//        Response.DesignationId = DesignationId;
//        Response.IsUpgradeVehicle = 1;
//        ResponseForm.push(Response);
//    });

//    //if (ResponseForm.length == 0) {
//    //    var ResponseZero = new Object();
//    //    ResponseZero.VehicleId = 0;
//    //    ResponseZero.DesignationId = DesignationId;
//    //    ResponseZero.IsUpgradeVehicle = 1;
//    //    ResponseForm.push(ResponseZero);

//    //    //alert(JSON.stringify(ResponseForm));
//    //}

//    ProgressBarShow();
//    var JSONResponse = JSON.stringify(ResponseForm);
//    var service = new HrmsSuiteHcmService.HcmService();
//    service.SaveUpdatedDesignationVehiclesMappingNew(JSONResponse, onSaveVehicleDesignationMappingNew, null, null);
//}

//function onSaveVehicleDesignationMappingNew(result) {
//    if (result == 1)
//        showSuccess('Vehicles Successfully Mapped');
//    GetEmployee();
//    ProgressBarHide();
//}





