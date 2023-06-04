$('.divAllowance').hide();
GetGroup();
GetUploadType();

function GetGroup() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getGroup(onGetGroup, null, null);
}

function onGetGroup(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlGroup", res);
    $('.ddlGroup').val(res[0].Id)
    $(".ddlGroup").prop("disabled", true);
    $(".ddlGroup").change();
}

function GetCompany(Group) {

    var service = new HrmsSuiteHcmService.HcmService();
    ProgressBarShow();
    service.getCompanyByGroupId($(Group).val(), onGetCompany, null, null);
}

function onGetCompany(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlCompany", res);
    $(".ddlCompany").change();
    ProgressBarHide();
}

function Medical_GetReInbursment() {
    if (!validateForm('.viewReinbursement'))
        return;

    ProgressBarShow();
    var CompanyId = $(".ddlCompany").val();
    var dtMonth = formatDate($('.dtMonthOf').val());
    var service = new HrmsSuiteHcmService.HcmService();
    service.medical_GetReInbursment(CompanyId, dtMonth, onMedical_GetReInbursment, null, null);
}

function onMedical_GetReInbursment(result) {
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.tbodyMedicalReinbursement').html('');
    $('#MedicalReinbursement').tmpl(res).appendTo(divTbodyGoalFund);
    ProgressBarHide();
}

function UploadFile() { 
    if (validateForm('.viewReinbursement') == true) {
        var Is_Validate = false;
        var AllowanceId = 0;
        var UploadType = $('.ddlUploadType').val();
        if (UploadType == "Allowance") {
            AllowanceId = $('.ddlAllowance').val();
            if (AllowanceId > 0) {
                Is_Validate = true;
            }
        }
        else if (UploadType == "Overtime") {
            AllowanceId = $('.ddlAllowance').val();
            if (AllowanceId > 0) {
                Is_Validate = true;
            }
        }
        else {
            Is_Validate = true;
        }
        if (Is_Validate == true) {

            var filename = FileUploadHandler('#fileMedicalReinbursement');
            if (filename != '') {
                SaveUploadFile(filename);
                ;
            } else {
                showError('Please select file');
            }
        } else {
            showError('Please select allowance');
        }
    }
}

function Medical_UploadFile(filename) {
    ProgressBarShow();
    var CompanyId = $(".ddlCompany").val();
    var dtMonth = formatDate($('.dtMonthOf').val());
    var service = new HrmsSuiteHcmService.HcmService();
    service.medical_UploadFile(CompanyId, dtMonth, filename, onMedical_UploadFile, null, null);
}

function onMedical_UploadFile(result) {
    if (result == 1) {
        showSuccess('Success');
        Medical_GetReInbursment();
    }
    else {
        showError('Error In Completing Process');
    }
    ProgressBarHide();
}

function GetUploadType() {
    FillDropDownByReference(".ddlUploadType", HCM_UploadType);
}

function SaveUploadFile(filename) {

    var AllowanceId = 0;
    var YearId = 0;
    var TypeId = 0;


    var id = $('.ddlUploadType').val();
    var UploadType = $('.ddlUploadType').val();

    if (UploadType == "Allowance") {
        AllowanceId = $('.ddlAllowance').val();
    }
    else if (UploadType == "Overtime") {
        AllowanceId = $('.ddlAllowance').val();
    }
    else if (UploadType == "LeaveEncashment") {
        YearId = $('.ddlYear').val();
    }
    else if (UploadType == "GeneralData") {
        YearId = $('.ddlYear').val();
        TypeId = $('.ddlgeneraldata').val();
    }

    ProgressBarShow();
    var CompanyId = $(".ddlCompany").val();
    var dtMonth = formatDate($('.dtMonthOf').val());
    var service = new HrmsSuiteHcmService.HcmService();
    service.save_UploadFile(CompanyId, dtMonth, filename, id, AllowanceId, YearId, TypeId, onSaveUploadFile, null, null);

}

function onSaveUploadFile(result) {

    var result = jQuery.parseJSON(result);
    if (result.ResponseMessageType == 1) {
        $("#fileMedicalReinbursement").val(null);
        showSuccess(result.ResponseMessage);
    }
    else {
        showError(result.ResponseMessage);
    }
    ProgressBarHide();
}

function GetAllowances() {
    debugger
    var CompanyId = $(".ddlCompany").val();
    getyear();
    var UploadType = $('.ddlUploadType').val();

    if (UploadType == "Allowance") {
        ListOfIds = getCommaSeparatedValues('.SetupAllowanceID');
        var service = new HrmsSuiteHcmService.HcmService();
        ProgressBarShow();
        service.getAllowances(CompanyId, ListOfIds, onGetAllowances, null, null);
    }
    else if (UploadType == "Overtime") {
        var service = new HrmsSuiteHcmService.HcmService();
        ProgressBarShow();
        service.getOvertimeAllowances(CompanyId, onGetAllowances, null, null);
    }
}

function onGetAllowances(result) { 
    resAllowances = jQuery.parseJSON(result);
    FillDropDownByReference('.ddlAllowance', resAllowances);
    ProgressBarHide();

}



function GetUploadSampleFormatFile() {
    var CompanyId = $(".ddlCompany").val();
    var UploadType = $('.ddlUploadType').val();


    var service = new HrmsSuiteHcmService.HcmService();
    ProgressBarShow();
    service.getSampleFileFormatUploadType(UploadType, CompanyId, onGetUploadSampleFormatFile, null, null);

}

function onGetUploadSampleFormatFile(result) {

    const _data = jQuery.parseJSON(result);

    if (_data.ResponseMessageType === 1) {
        $("#divDownloadSamplFileFormat").css("display", "block");
        $("#anchorfiledownload").attr('href', _data.ResponseData)
    }
    else {
        $("#anchorfiledownload").attr('href', '');
        $("#divDownloadSamplFileFormat").css("display", "none");
        showError(_data.ResponseMessage);
    }

    ProgressBarHide();

}


function UploadType() { 
    var UploadType = $('.ddlUploadType').val();

    if (UploadType == "Allowance") { 
        $('.divAllowance').show();
        $('.divddlyear').hide();
        $('.divddlgeneraldata').hide();
        GetAllowances();
    }
    else if (UploadType == "Overtime") { 
        $('.divAllowance').show();
        $('.divddlyear').hide();
        $('.divddlgeneraldata').hide();
        GetAllowances();
    }
    else if (UploadType == "LeaveEncashment") {
        $('.divddlyear').show();
        $('.divddlgeneraldata').hide();
        getyear();
        $('.divAllowance').hide();
    }
    else if (UploadType == "GeneralData") {
        $('.divddlyear').show();
        $('.divddlgeneraldata').show();
        getyear();
        getGeneralData();
        $('.divAllowance').hide();
    }
    else {
        $('.divAllowance').hide();
        $('.divddlyear').hide();
        $('.divddlgeneraldata').hide();
    } 
    GetUploadSampleFormatFile();
}


function Cancel() {
    $('.ddlAllowance').val('0');
    $('.dtMonthOf').val('');
    $("#fileMedicalReinbursement").val(null);
    $('.ddlUploadType').val('0');
    $('.ddlUploadType').change();
    $('.ddlCompany').val('0');
    $('.ddlCompany').change();


}

function getyear() {

    var CompanyId = $(".ddlCompany").val(); 
    var service = new HrmsSuiteHcmService.HcmService();
    ProgressBarShow();
    service.getYear(CompanyId, onGetYear, null, null);
}

function onGetYear(result) {
    resAllowances = jQuery.parseJSON(result);
    FillDropDownByReference('.ddlYear', resAllowances);
    ProgressBarHide();
}

function getGeneralData() { 
    var UploadType = $('.ddlUploadType').val();
    
    var service = new HrmsSuiteHcmService.HcmService();
    ProgressBarShow();
    service.getgeneraldata(45, ongetGeneralData, null, null);
}

function ongetGeneralData(result) {
    resAllowances = jQuery.parseJSON(result);
    FillDropDownByReference('.ddlgeneraldata', resAllowances);
    ProgressBarHide();
}
