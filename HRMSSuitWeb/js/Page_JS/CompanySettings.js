
//alert(formatDate(''))

function TriggerPageLoads() {
    //GetGroup();
  
    $(".btnCancelSearch").click(function () {
        $('.Div_SaveButton').hide();
        $('.divSettingControls').html('');
    });
    $('.divBasicInterestManupulation').hide();
    isEdit = false;
}


//function GetGroup() {
//    var service = new HrmsSuiteHcmService.HcmService();
//    service.getGroup(onGetGroup, null, null);
//}

//function onGetGroup(result) {
//    var res = jQuery.parseJSON(result);
//    FillDropDownByReference(".ddlGroup", res);
//    $(".ddlGroup").change();

//}



//function GetCompany(Group) {

//    var service = new HrmsSuiteHcmService.HcmService();
//    service.getCompanyByGroupId($(Group).val(), onGetCompany, null, null);
//}

//function onGetCompany(result) {
//    var res = jQuery.parseJSON(result);
//    FillDropDownByReference(".ddlCompany", res);
//    $(".ddlCompany").change();

//}

function GetEmployee() {
    var Id = $('.ddlCompany').val();
    if (Id > 0) {
        GetSlabListing();
        var service = new HrmsSuiteHcmService.HcmService();
        service.getCompanySettingsList($('.ddlCompany').val(), onGetCompanySettings, null, null);
    }
}

function onGetCompanySettings(result) {
    setTimeout(function () { }, 1000);
    var res = jQuery.parseJSON(result);
    if (res.length > 0) {
        var divTbodyGoalFund = $('.divSettingControls').html('');
        $('#SettingControls').tmpl(res).appendTo(divTbodyGoalFund);
        validateNumeric();
        ProgressBarShow();
        FillandBindDropDowns(res);

        $('.Div_SaveButton').show();
        $('.tooltipInfo').hide();
        FormToolTip();
    }
}

function FillandBindDropDowns(res) {
    i = 0;
    var interval = null;
    interval = setInterval(function () {
        if (i == res.length - 1) {
            ProgressBarHide();
            clearInterval(interval);
        }
        if (!res[i].IsDisplayInMenu) {
            var SetupName = 'ddl' + res[i].SetupName.replace(' ', '');
            $('.divSettingControls').append('<div class="col-lg-3"><input type="hidden" class="IsRelational" value="true"/><input type="hidden" class="SetupMasterId" value="' + res[i].SetupMasterID + '"/><div  style="top:-50%;" class="tooltip fade top in tooltipInfo"><div class="tooltip-arrow" style="top:80%;"></div><div class="tooltip-inner">' + res[i].SetupName + '</div></div><label class="lblTooltip" for="exampleIsnputEmail2">' + res[i].SetupName + '</label><select class="form-control txtValues ' + SetupName + ' ' + res[i].SetupClass + ' "></select></div>');
            GetDropDownList(res[i].SetupMasterID, res[i].Value, '.' + SetupName);
            $('.tooltipInfo').hide();
            FormToolTip()
        }
        i++;
    }, 200);

}

function GetDropDownList(SetupMasterId, _ValueChange, _selector) {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getFromSetupDetail(0, 0, SetupMasterId, onGetDropDownList, null, null);
    cssClass = _selector;
    ValueChange = _ValueChange;
}

function onGetDropDownList(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(cssClass, res);

    if (ValueChange != null)
        $(cssClass).val(ValueChange).change();
    onChangeBasicInterest();
}

function onSaveChanges() {
    var ResponseForm = [];
    $('.divSettingControls').children().each(function () {
        var Response = new Object();

        Response.Value = $(this).children('.txtValues').val();
        Response.SetupID = $(this).children('.SetupMasterId').val();
        Response.CompanyId = $('.ddlCompany').val();
        Response.IsRelationalValue = $(this).children('.IsRelational').val();

        ResponseForm.push(Response);
    });
    var JSONResponse = JSON.stringify(ResponseForm);
    saveCompanySettings(JSONResponse);
}

function saveCompanySettings(Json) {
    var service = new HrmsSuiteHcmService.HcmService();
    service.saveCompanySettings(Json, onsaveCompanySettings, null, null);
}

function onsaveCompanySettings(result) {
    if (result == "1")
        showSuccess('Successfully Updated All The Settings');
    GetEmployee();
}

function FormToolTip() {
    $('.lblTooltip').mouseenter(function () {
        $(this).prev('.tooltipInfo').show();
    });

    $('.lblTooltip').mouseout(function () {
        $(this).prev('.tooltipInfo').hide();
    });
}

function GetSlabListing() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getFromSetupDetail(0, 0, HCM_SetupMaster.YearSlabs, onGetSlabListing, null, null);
}

function onGetSlabListing(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference('.ddlYearSlab', res);
}

function saveInterestInfo() {
    var flag = true;
    if (!validateForm('.divInterestInfo'))
        return;

    
    var InterestRate = $('.txtInterestRate').val();
    var SlabYearId = $('.ddlYearSlab').val();
    var CompanyId = $(".ddlCompany").val();
    var SlabYearText = parseInt($('.ddlYearSlab').find('option:selected').text());
    var LastSlabYear = parseInt($('.tbodyInterestSlabListing').find('tr').last().find('.hdSlabYear').val()) + 1;

    if (SlabYearText > LastSlabYear) {
        showError('Year Slabs must be in incremental order');
        return;
    }


    $('.hdSlabYearId').each(function () {
        if ($(this).val() == SlabYearId)
            flag = false;
    });

    if (!flag && !isEdit) {
        showError('The Interest Rate for the Year Slab is already added.')
        return;
    }


    var service = new HrmsSuiteHcmService.HcmService();
    service.companysetting_SaveInterestSlab(CompanyId, SlabYearId, InterestRate, OnsaveInterestInfo, null, null);
}

function OnsaveInterestInfo(result) {
    if (result == 1)
        showSuccess('Succefully Updated');
    GetInterestSlab();
    isEdit = false;
}

function onChangeBasicInterest() {
    $('.manupulateBasicInterest').change(function () {
        if ($(this).val() == HCM_SetupDetail.BasicInterest) {
            $('.divBasicInterestManupulation').slideDown(500);
            GetInterestSlab();
        }
        else {
            $('.divBasicInterestManupulation').hide();
        }

    });
    $('.manupulateBasicInterest').change();
}

function GetInterestSlab() {
    var CompanyId = $(".ddlCompany").val();
    var service = new HrmsSuiteHcmService.HcmService();
    service.companysetting_GetInterestSlab(CompanyId, onGetInterestSlab, null, null);
}

function onGetInterestSlab(result) {
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.tbodyInterestSlabListing').html('');
    $('#InterestSlabListing').tmpl(res).appendTo(divTbodyGoalFund);
    $('.tbodyInterestSlabListing').find('tr').last().find('td').eq(0).append(' Or Greater');
}



function DeleteInterestSlabRate(selector) {
    var InterestId = $(selector).closest('tr').find('.hdInterestId').val();

    var SlabYearText = $(selector).closest('tr').find('.hdSlabYear').val();
    var LastSlabYear = parseInt($('.tbodyInterestSlabListing').find('tr').last().find('.hdSlabYear').val());

    if (SlabYearText != LastSlabYear) {
        showError('This Slab cannot be deleted!');
        return;
    }

    var service = new HrmsSuiteHcmService.HcmService();
    service.companysetting_DeleteInterestSlab(InterestId, onDeleteInterestSlabRate, null, null); s
}

function onDeleteInterestSlabRate(result) {
    if (result == 1)
        showSuccess('Succefully Deleted');
    GetInterestSlab();
}


function EditInterestSlabRate(selector) {
    isEdit = true;
    var InterestRate = $(selector).closest('tr').find('.hdInterestRate').val();
    var SlabYearId = $(selector).closest('tr').find('.hdSlabYearId').val();
    $('.txtInterestRate').val(InterestRate);
    $('.ddlYearSlab').val(SlabYearId);
}


function ResetFieldsInterest() {
    resetControls('.divInterestInfo');
}

