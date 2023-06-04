

GetGroup();

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

function Medical_UploadReinbursementFile() {
    if (!validateForm('.viewReinbursement'))
        return;
    var filename = FileUploadHandler('#fileMedicalReinbursement');
    if (filename != '')
        Medical_UploadFile(filename);
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
        showError('Error Completing Process');
    }
    ProgressBarHide();
}