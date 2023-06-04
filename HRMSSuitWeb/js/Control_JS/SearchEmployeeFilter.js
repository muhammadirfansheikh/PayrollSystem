
var HasEmployeeCode = $('.HasEmployeeCode').val();
var HasEmployeeType = $('.HasEmployeeType').val();
var HasLocation = $('.HasLocation').val();
var HasBusinessUnit = $('.HasBusinessUnit').val();
var HasDepartment = $('.HasDepartment').val();
var HasCostCenter = $('.HasCostCenter').val();
var HasSapCostCenter = $('.HasSapCostCenter').val();
var HasJobCategory = $('.HasJobCategory').val();
var HasDesignation = $('.HasDesignation').val();
var HasFirstName = $('.HasFirstName').val();
var HasLastName = $('.HasLastName').val(); 

function TriggerLoad() { 
    if (HasEmployeeCode == "1") {
        $('.EmployeeCodeDiv').show();
        $('.EmployeeNamediv').show();
    }
    if (HasEmployeeType == "1") {
        $('.EmployeeTypeDiv').show();
    }
    if (HasLocation == "1") {
        $('.LocationDiv').show();
    }
    if (HasBusinessUnit == "1") {
        $('.BusinessUnitDiv').show();
    }
    if (HasDepartment == "1") {
        $('.DepartmentDiv').show();
    }
    if (HasCostCenter == "1") {
        $('.CostCenterDiv').show();
    }
    if (HasSapCostCenter == "1") {
        $('.SapCostCenterDiv').show();
    }
    if (HasJobCategory == "1") {
        $('.CategoryDiv').show();
    }
    if (HasDesignation == "1") {
        $('.DesignationDiv').show();
    }
    if (HasFirstName == "1") {
        $('.FirstNameDiv').show();
    }
    if (HasLastName == "1") {
        $('.LastNameDiv').show();
    }
    Get_Control_Data_EmployeeSearchFilter("Onload");
    $('.btnSearch').click(function () {
        return false;
    });
    $(window).keydown(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            if ($('.btnSearch').is(":visible"))
                $('.btnSearch').click();
            return false;
        }
    });
}

function Get_Control_Data_EmployeeSearchFilter(Type) {
   
    var hit_Service = 0;
    var GroupId = 0;
    var CompanyId = 0;
    var BusinessUnitId = 0;
    var JobCategoryId = 0;
    if (Type == "Onload") {
        FillDropDownByReference(".ddlGroup", null);
        FillDropDownByReference(".ddlCompany", null);
        FillDropDownByReference(".ddlEmployeeType", null);
        FillDropDownByReference(".ddlLocation", null);
        FillDropDownByReference(".ddlCostCenter", null);
        FillDropDownByReference(".ddlBU", null);
        FillDropDownByReference(".ddlDepartment", null);
        FillDropDownByReference(".ddlCategoryC", null);
        FillDropDownByReference(".ddlDesignation", null);
        $('.txtEmployeeCode').val('');
        $('.txtEmployeeName').val('');
        $('.txtFirstName').val('');
        $('.txtLastName').val('');
        hit_Service = 1;
    }
    else if (Type == "OnChangeGroup") {
        FillDropDownByReference(".ddlCompany", null);
        FillDropDownByReference(".ddlLocation", null);
        FillDropDownByReference(".ddlCostCenter", null);
        FillDropDownByReference(".ddlBU", null);
        FillDropDownByReference(".ddlDepartment", null);
        FillDropDownByReference(".ddlCategoryC", null);
        FillDropDownByReference(".ddlDesignation", null);
        GroupId = ($('.ddlGroup').val() == "" || $('.ddlGroup').val() == null) ? 0 : $('.ddlGroup').val();
        if (GroupId > 0) {
            hit_Service = 1;
        }
    }
    else if (Type == "OnChangeCompany") {
        FillDropDownByReference(".ddlLocation", null);
        FillDropDownByReference(".ddlCostCenter", null);
        FillDropDownByReference(".ddlBU", null);
        FillDropDownByReference(".ddlDepartment", null);
        FillDropDownByReference(".ddlCategoryC", null);
        FillDropDownByReference(".ddlDesignation", null);
        GroupId = ($('.ddlGroup').val() == "" || $('.ddlGroup').val() == null) ? 0 : $('.ddlGroup').val();
        CompanyId = ($('.ddlCompany').val() == "" || $('.ddlCompany').val() == null) ? 0 : $('.ddlCompany').val();
        if (GroupId > 0 && CompanyId > 0 && (HasLocation == 1 || HasBusinessUnit == 1 || HasDepartment == 1 || HasCostCenter == 1 || HasSapCostCenter == 1 || HasJobCategory == 1 || HasDesignation == 1)) {
            hit_Service = 1;
        }
    }
    else if (Type == "OnChangeBusinessUnit") {
        FillDropDownByReference(".ddlDepartment", null);
        GroupId = ($('.ddlGroup').val() == "" || $('.ddlGroup').val() == null) ? 0 : $('.ddlGroup').val();
        CompanyId = ($('.ddlCompany').val() == "" || $('.ddlCompany').val() == null) ? 0 : $('.ddlCompany').val();
        BusinessUnitId = ($('.ddlBU').val() == "" || $('.ddlBU').val() == null) ? 0 : $('.ddlBU').val();
        if (GroupId > 0 && CompanyId > 0 && HasDepartment == 1) {
            hit_Service = 1;
        }
    }
    else if (Type == "OnChangeJobCategory") {
        FillDropDownByReference(".ddlDesignation", null);
        GroupId = ($('.ddlGroup').val() == "" || $('.ddlGroup').val() == null) ? 0 : $('.ddlGroup').val();
        CompanyId = ($('.ddlCompany').val() == "" || $('.ddlCompany').val() == null) ? 0 : $('.ddlCompany').val();
        JobCategoryId = ($('.ddlCategoryC').val() == "" || $('.ddlCategoryC').val() == null) ? 0 : $('.ddlCategoryC').val();
        if (GroupId > 0 && CompanyId > 0 && HasDesignation == 1) {
            hit_Service = 1;
        }
    }
    if (hit_Service == 1) {
        ProgressBarShow();
        var service = new HrmsSuiteHcmService.HcmService();
        service.Get_Control_Data_EmployeeSearchFilter(Type, GroupId, CompanyId, BusinessUnitId, JobCategoryId, HasEmployeeType, HasLocation, HasBusinessUnit, HasDepartment, HasCostCenter, HasSapCostCenter, HasJobCategory, HasDesignation, onGet_Control_Data_EmployeeSearchFilter, null, Type);
    }
}

function onGet_Control_Data_EmployeeSearchFilter(result, Type) {
    if (result != "") {
        if (Type == "Onload") {
            var _result = result.split('#SPLIT#');
            var Group = jQuery.parseJSON(_result[0]);
            var Company = jQuery.parseJSON(_result[1]);
            var EmployeeType = jQuery.parseJSON(_result[2]);
            FillDropDownByReference(".ddlGroup", Group);
            if (Group.length == 1) {
                $('.ddlGroup').val(Group[0].Id)
                $(".ddlGroup").prop("disabled", true);
            }
            FillDropDownByReference(".ddlCompany", Company);
            FillDropDownByReference(".ddlEmployeeType", EmployeeType);
        }
        else if (Type == "OnChangeGroup") {
            var Company = jQuery.parseJSON(result);
            FillDropDownByReference(".ddlCompany", Company);
        }
        else if (Type == "OnChangeCompany") {
          
            var _result = result.split('#SPLIT#');
            var Location = jQuery.parseJSON(_result[0]);
            var BusinessUnit = jQuery.parseJSON(_result[1]);
            var Department = jQuery.parseJSON(_result[2]);
            var CostCenter = jQuery.parseJSON(_result[3]);
            var SapCostCenter = jQuery.parseJSON(_result[4]);
            var JobCategory = jQuery.parseJSON(_result[5]);
            var Designation = jQuery.parseJSON(_result[6]);

            FillDropDownByReference(".ddlLocation", Location);
            FillDropDownByReference(".ddlBU", BusinessUnit);
            FillDropDownByReference(".ddlDepartment", Department);
            FillDropDownByReference(".ddlCostCenter", CostCenter);
            FillDropDownByReference(".ddlSapCostCenter", SapCostCenter);
            FillDropDownByReference(".ddlCategoryC", JobCategory);
            FillDropDownByReference(".ddlDesignation", Designation);

        }
        else if (Type == "OnChangeBusinessUnit") {
            var Department = jQuery.parseJSON(result);
            FillDropDownByReference(".ddlDepartment", Department);
        }
        else if (Type == "OnChangeJobCategory") {
            var Designation = jQuery.parseJSON(result);
            FillDropDownByReference(".ddlDesignation", Designation);
        }
    }
    ProgressBarHide();
}

function FillDropDownByReference(DropDownReference, res) {
    $(DropDownReference).empty().append('<option selected="selected" value="0">--Select--</option>');
    $(res).each(function () {
        $(DropDownReference).append($("<option></option>").val(this.Id).html(this.Value));
    });
} 

function GetEmployeeType() {
    if (HasEmployeeType == "1") {
        var service = new HrmsSuiteHcmService.HcmService();
        service.getEmployeeType(0, onGetEmployeeType, null, null);
    }
}

function onGetEmployeeType(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlEmployeeType", res);
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

function GetLocationANDBU(Company) {

    GetLocation(Company);
    GetBU(Company);
    GetCostCenter(Company);
    GetDesignation(Company);
    GetCategory(Company);
}

function GetLocation(Company) {

    var service = new HrmsSuiteHcmService.HcmService();
    service.getLocation($(Company).val(), onGetLocation, null, null);

}

function onGetLocation(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlLocation", res);
}

function GetBU(Company) {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getBusinessUnit($(Company).val(), onGetBU, null, null);
}

function onGetBU(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlBU", res);
    $(".ddlBU").change();
}

function GetDepartment(BU) {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getDepartment($(BU).val(), onGetDepartment, null, null);
}

function onGetDepartment(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlDepartment", res);
    $(".ddlDepartment").change();
}

function GetCostCenter(Company) {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getCostCenter($(Company).val(), onGetCostCenter, null, null);
}

function onGetCostCenter(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlCostCenter", res);
    $(".ddlCostCenter").change();
}

function GetDesignation(Company) {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getDesignation($(Company).val(), onGetDesignation, null, null);
}

function onGetDesignation(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlDesignation", res);
    $(".ddlDesignation").change();
}

function GetCategory(Company) {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getCategory($(Company).val(), onGetCategory, null, null);
}

function onGetCategory(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlCategoryC", res);
    $(".ddlCategoryC").change();
}





