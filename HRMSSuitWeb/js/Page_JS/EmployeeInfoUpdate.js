var IsAdmin = 0;

function TriggerPageLoads() {
    IsAdmin = GetURLVars()['IsAdmin'];
    if (IsAdmin == 1)
    {
        $('.lblHeading').text('Employee Change Request');
    }
    GetAllDepartments();
    GetAllDesignations();
    GetAllLocations();
    GetAllGenders();
    GetAllReligions();
    GetAllMaritalStatus();
    $('.btnRequestChanges').hide();
    $('.btnApproveRequest').hide();
    $('.btnRejectRequest').hide();
}

function GetAllDepartments() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getDepartment(0, onGetAllDepartments, null, null);
}

function onGetAllDepartments(result) {
    AllDepartments = jQuery.parseJSON(result);
}

function GetAllCostCenter() {
    //var service = new HrmsSuiteHcmService.HcmService();
    //service.getCostCenter($('.ddlCompany').val(), onGetAllCostCenter, null, null);

    var $options = $(".ddlCostCenter > option").clone();
    $('.ddlChCostCentre').append($options);
}

function onGetAllCostCenter(result) {

    AllCostCenter = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlChCostCentre", AllCostCenter);

}

function GetAllDesignations() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getDesignation(0, onGetAllDesignations, null, null);
}

function onGetAllDesignations(result) {
    AllDesignations = jQuery.parseJSON(result);
}

function GetAllLocations() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getLocation(0, onGetAllLocations, null, null);
}

function onGetAllLocations(result) {
    AllLocations = jQuery.parseJSON(result);
}

function GetAllGenders() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getGender(onGetAllGenders, null, null);
}

function onGetAllGenders(result) {
    AllGenders = jQuery.parseJSON(result);
}

function GetAllReligions() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getReligion(onGetAllReligions, null, null);
}

function onGetAllReligions(result) {
    AllReligion = jQuery.parseJSON(result);
}

function GetAllMaritalStatus() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getMaritalStatus(onGetAllMaritalStatus, null, null);
}

function onGetAllMaritalStatus(result) {
    AllMaritalStatus = jQuery.parseJSON(result);
}

function GetEmployee() {
  
    Group = $(".ddlGroup").val();
    Company = $(".ddlCompany").val();
    Location = $(".ddlLocation").val();
    BU = $(".ddlBU").val();
    Department = $(".ddlDepartment").val();
    CostCenter = $(".ddlCostCenter").val();
    Designation = $(".ddlDesignation").val();
    Firstname = $(".txtFirstName").val();
    Lastname = $(".txtLastName").val();
    EmpCode = $(".txtEmployeeCode").val();
    Categoryid = $(".ddlCategoryC").val();
    if (Company > 0) {
        var service = new HrmsSuiteHcmService.HcmService();
        if (IsAdmin == 1) {
            ProgressBarShow();
            service.getChangeRequestEmployees(Group, Company, Location, BU, CostCenter, Designation, Department, Firstname, Lastname, EmpCode, Categoryid, onGetEmployee, null, null);
        }
        else {
            ProgressBarShow();
            service.getEmployee(Group, Company, Location, BU, CostCenter, Designation, Department, Firstname, Lastname, EmpCode, Categoryid, onGetEmployee, null, null);
        }
    }
}

function onGetEmployee(result) {

    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.EmployeeInfo').html('');
    $('#EmployeeInfo').tmpl(res).appendTo(divTbodyGoalFund);
    SetEmployeeValues(res);

    if (IsAdmin != 1) {
        $('.btnRequestChanges').show();
        $('.AdminControl').remove();
        $('.btnApproveRequest').hide();
        $('.btnRejectRequest').hide();
    }
    else {
        if (res.length > 0) {
            $('.btnApproveRequest').show();
            $('.btnRejectRequest').show();
        }
        else {
            $('.btnApproveRequest').hide();
            $('.btnRejectRequest').hide();
        }

    }
    ProgressBarHide();
}

function SetEmployeeValues(res) {

    FillDropDownByReference(".ddlChGender", AllGenders);
    FillDropDownByReference(".ddlChDepartment", AllDepartments);
    FillDropDownByReference(".ddlChDesignation", AllDesignations);
    FillDropDownByReference(".ddlChLocation", AllLocations);
    FillDropDownByReference(".ddlChReligion", AllReligion);
    FillDropDownByReference(".ddlChMaritalStatus", AllMaritalStatus);

    GetAllBusinessUnit();
    GetAllCostCenter();

    //FillDropDownByReference(".ddlChBU", AllBusinessUnit);
    //FillDropDownByReference(".ddlChCostCentre", AllCostCenter);

    $(res).each(function (k, v) {
        var objTr = $('input:hidden[value=\'' + v.EmployeeId + '\']').closest('tr');
        objTr.find('.ddlChLocation').val(v.LocationId);
        objTr.find('.ddlChDesignation').val(v.DesignationId);
        objTr.find('.ddlChDepartment').val(v.DepartmentId);
        objTr.find('.ddlChCostCentre').val(v.CostCenterId);
        objTr.find('.ddlChGender').val(v.GenderId);
        objTr.find('.ddlChMaritalStatus').val(v.MaritalStatusId);
        objTr.find('.ddlChBU').val(v.BuisnessUnitId);
        objTr.find('.ddlChReligion').val(v.ReligionId);
    });
}

function GetAllBusinessUnit() {
    //var service = new HrmsSuiteHcmService.HcmService();
    //service.getBusinessUnit($(".ddlCompany").val(), onGetAllBusinessUnit, null, null);

    var $options = $(".ddlBU > option").clone();
    $('.ddlChBU').append($options);
}

function onGetAllBusinessUnit(result) {
    AllBusinessUnit = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlChBU", AllBusinessUnit);
}

function EnableCurrentRow(selector) {

    if (IsAdmin != 1) {
        if ($(selector).is(':checked')) {
            $(selector).closest('tr').find('input, select').not(selector).each(function () {
                $(this).prop("disabled", false);
            });
        }
        else {
            $(selector).closest('tr').find('input, select').not(selector).each(function () {
                $(this).prop("disabled", true);
            })
        }
    }
}

function RequestChanges() {
    var JSONFORM = [];
    $('.chkEditInfo:checked').each(function () {
        var objTr = $(this).closest('tr');

        var RequestData = new Object();

        RequestData.EmployeeId = objTr.find('.EmployeeId').val();
        RequestData.EmployeeCode = objTr.find('.EmployeeCode').val().trim();
        RequestData.FirstName = objTr.find('.FirstName').val().trim();
        RequestData.LastName = objTr.find('.LastName').val().trim();
        RequestData.BusinessUnitID = objTr.find('.ddlChBU').val();
        RequestData.CostCenterId = objTr.find('.ddlChCostCentre').val();
        RequestData.DepartmentId = objTr.find('.ddlChDepartment').val();
        RequestData.DesignationId = objTr.find('.ddlChDesignation').val();
        RequestData.LocationId = objTr.find('.ddlChLocation').val();
        RequestData.GenderId = objTr.find('.ddlChGender').val();
        RequestData.ReligionId = objTr.find('.ddlChReligion').val();
        RequestData.MatrialStatusId = objTr.find('.ddlChMaritalStatus').val();
        RequestData.Phone = objTr.find('.PhoneNumber').val().trim();
        RequestData.OfficialMobileNumber = objTr.find('.MobileNumber').val().trim();
        RequestData.Salary = objTr.find('.Salary').val().trim();
        RequestData.CompanyId = objTr.find('.CompanyId').val().trim();

        JSONFORM.push(RequestData);
    });
    if (JSONFORM.length == 0) {
        showError('Request Cannot Proceed!');
        return;
    }

    var JSONResponse = JSON.stringify(JSONFORM);

    var service = new HrmsSuiteHcmService.HcmService();
    service.RequestEmployeeInfoUpdate(JSONResponse, onRequestChanges, null, null);

}

function onRequestChanges(result) {
    if (result == 1)
        showSuccess('Successfully Requested!');
    else
        showError(result);
}

function RejectRequest(selector) {
    if (confirm("Are you sure want to Reject the Request")) {

        var EmployeeChangeRequestId = $(selector).closest('tr').find('.EmployeeChangeRequestId').val();

        var service = new HrmsSuiteHcmService.HcmService();
        service.RejectRequest(EmployeeChangeRequestId, onRejectRequest, null, null);
    }
}

function onRejectRequest(result) {
    if (result == 1) {
        showSuccess('Successfully Rejected the Request!');
        GetEmployee();
    }
    else {
        showError(result);
    }
}

function RejectAllRequest() {
    if (confirm("Are you sure want to Reject All Requests")) {
        var MultiReqIds = '';
        $('.chkEditInfo:checked').each(function () {

            var objTr = $(this).closest('tr');

            MultiReqIds = MultiReqIds + objTr.find('.EmployeeChangeRequestId').val() + ',';
        });

        var service = new HrmsSuiteHcmService.HcmService();
        service.RejectMultiRequest(MultiReqIds, onRejectAllRequest, null, null);
    }
}

function onRejectAllRequest(result) {
    if (result == 1) {
        showSuccess('Successfully Rejected the Selected Requests!');
        GetEmployee();
    }
    else {
        showError(result);
    }
}

function EnableAllRows(chk) {
    $('.chkEditInfo').prop('checked', $(chk).is(':checked'));
    $('.chkEditInfo').each(function () {
        EnableCurrentRow($(this));
    });
}

function ApproveRequest(selector) {



    if (confirm("Are you sure want to Approve the Request")) {

        var EmployeeChangeRequestId = $(selector).closest('tr').find('.EmployeeChangeRequestId').val();

        var service = new HrmsSuiteHcmService.HcmService();
        service.ApproveRequest(EmployeeChangeRequestId, onApproveRequest, null, null);
    }
}

function onApproveRequest(result) {
    if (result == 1) {
        showSuccess('Successfully Approved the Request!');
        GetEmployee();
    }
    else {
        showError(result);
    }
}

function ApproveAllRequest() {



    if (confirm("Are you sure want to Approve All Requests")) {
        var MultiReqIds = '';
        $('.chkEditInfo:checked').each(function () {

            var objTr = $(this).closest('tr');

            MultiReqIds = MultiReqIds + objTr.find('.EmployeeChangeRequestId').val() + ',';
        });

        var service = new HrmsSuiteHcmService.HcmService();
        service.ApproveMultiRequest(MultiReqIds, onApproveAllRequest, null, null);
    }
}

function onApproveAllRequest(result) {
    if (result == 1) {
        showSuccess('Successfully Approve the Selected Requests!');
        GetEmployee();
    }
    else {
        showError(result);
    }
}

