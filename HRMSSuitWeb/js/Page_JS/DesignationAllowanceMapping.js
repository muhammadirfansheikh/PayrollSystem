
EmployeeIdSingle = 0;
PopupOpen = false;

SrcOptions_Value = [];
DestOptions_Value = [];
SrcOptions_Text = [];
DestOptions_Text = [];

function TriggerPageLoads() {
   
    $('.EmployeeCodeDiv').hide();
    $('.LocationDiv').hide();
    $('.BusinessUnitDiv').hide();
    $('.DepartmentDiv').hide();
    $('.CostCenterDiv').hide();
    $('.FirstNameDiv').hide();
    $('.LastNameDiv').hide();
    //$('.EmployeeCodeDiv').hide();
    //$('.EmployeeCodeDiv').hide();
    //$('.EmployeeCodeDiv').hide();
    //$('.EmployeeCodeDiv').hide();


}

function ChkAll() {

    if ($('.chkAll').is(':checked')) {

        $('.chkSelect').each(function () {

            $(this).prop('checked', true);
        });
    }
    else {
        $('.chkSelect').each(function () {

            $(this).prop('checked', false);
        });
    }
}

function ChkSelect(cls) {


    var Total = $('.chkSelect').size();
    var Chk = 0;

    $('.chkSelect').each(function () {
        if ($(this).is(':checked')) {
            Chk = Chk + 1;


            //$(this).closest('.tdMajor').find('.txtMajor').prop('disabled',false);
        }
    });

    if (Total == Chk) {
        $('.chkAll').prop('checked', true);
    }
    else {
        $('.chkAll').prop('checked', false);
    }


    //var s = $(cls).next('.txtMajor').val();
    ////var s = ("input[type=checkbox]").next.next("td").child("input:text").val();
    //// var s = ("input[type=checkbox]").next("input:text").val();
    ////alert($(this).next());
}

function Validate() {

    var IsFlag = 0;
    $('.trInputs').each(function () {
        if ($(this).find('.chkSelect').is(':checked')) {

            var FormulaExist = $(this).find('.hfIsFormulaExist').val();

            if (FormulaExist == 'false') {
                var txtMeasure = $(this).find('.txtMajor').val();

                if (txtMeasure == '') {
                    showError('Please Enter the Required Fields');

                    if (IsFlag == 0) {
                        IsFlag = 1;
                    }
                }
            }
        }
    });

    if (IsFlag == 0) {
        return true;
    }
    else {
        return false;
    }

}

function GetDesignationSearch() {

    Company = $(".ddlCompany").val();
    Designation = $(".txtDesignation").val();

    var service = new HrmsSuiteHcmService.HcmService();
    service.getDesignationSearch(Company, Designation, onGetDesignationSearch, null, null);

}

function onGetDesignationSearch(result) {
    var res = jQuery.parseJSON(result);

    var divTbodyGoalFund = $('.tbodyDesignationListing').html('');
    $('#DesignationListing').tmpl(res).appendTo(divTbodyGoalFund);
}

function GetAllowance(Company) {

    if (PopupOpen == true) {
        //Company = $(".ddlCompany").val();
        //if (Company == 0) {
        //    Company = 2000;
        //}

        Company = $('.clsPopupSearchFilter').find('.ddlCompany').val();
    }

    var service = new HrmsSuiteHcmService.HcmService();
    service.getAllowances(Company, "0", onGetAllowance, null, null);

}

function onGetAllowance(result) {
    //alert(result);

    var res = jQuery.parseJSON(result);

    var divTbodyGoalFund = $('.tbodyAllowanceListing').html('');
    $('#AllowanceMapListing').tmpl(res).appendTo(divTbodyGoalFund);

    GetMappedAllowanceByEmployeeId(EmployeeIdSingle);
}


function GetMappedAllowanceByEmployeeId(_EmployeeId) {


    var service = new HrmsSuiteHcmService.HcmService();
    service.GetAllowancesByEmployeeId(_EmployeeId, onGetMappedAllowanceByEmployeeId, null, null);

}

function onGetMappedAllowanceByEmployeeId(result) {
    // alert(result);

    var res = jQuery.parseJSON(result);

    $('.trInputs').each(function () {
        var that = this;
        var AllowanceId = $(that).find('.hfAllowanceId').val();
        $(res).each(function (k, v) {

            if (AllowanceId == v.Id) {

                $(that).find('.chkSelect').prop('checked', true);

                //alert($(that).find('.hfIsFormulaExist').val());
                if ($(that).find('.hfIsFormulaExist').val()) {
                    $(that).find('.txtMajor').val(v.Measure);
                }
            }

        });
        //if ($(this).find('.chkSelect').is(':checked')) {

        //    var Response = new Object();
        //    Response.EmployeeId = EmployeeId;
        //    Response.AllowanceId = $(this).find('.hfAllowanceId').val();
        //    Response.Measure = $(this).find('.hfIsFormulaExist').val() == "false" ? $(this).find('.txtMajor').val() : null;
        //    Response.IsSpecialAllowance = false;
        //    ResponseForm.push(Response);
        //}
    });


    //var divTbodyGoalFund = $('.tbodyAllowanceListing').html('');
    //$('#AllowanceMapListing').tmpl(res).appendTo(divTbodyGoalFund);
}

function setDesignationId(_DesignationId) {
    DesignationId = _DesignationId;
    GetAllowance();
}

function GetFromSetupDetail(ParentId, MasterId, _cssClass) {
    if (ParentId != 0) {
        ParentId = $(ParentId).val();
    }
    var service = new HrmsSuiteHcmService.HcmService();
    service.getFromSetupDetail(ParentId, MasterId, onGetFromSetupDetail, null, null);
    cssClass = _cssClass;
}

function onGetFromSetupDetail(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(cssClass, res);
}

function showError(message) {
    AlertBox('Error!', message, 'error');
}

function showSuccess(message) {
    AlertBox('Success!', message, 'success');
}

function AlertBox(title, Message, type) {
    swal(title, Message, type);
}

function SaveMultipleAllowances() {

    if (!Validate()) {
        return;
    }
    ProgressBarShow();
    var selected_dest = $("#multiddlDest option").map(function () { return this.value }).get();

    if (selected_dest.length > 0) {
        var ResponseForm = [];
        for (var i = 0; i < selected_dest.length; i++) {

            var EmployeeId = selected_dest[i];

            $('.trInputs').each(function () {

                if ($(this).find('.chkSelect').is(':checked')) {

                    var Response = new Object();
                    Response.EmployeeId = EmployeeId;
                    Response.AllowanceId = $(this).find('.hfAllowanceId').val();
                    Response.Measure = $(this).find('.hfIsFormulaExist').val() == "false" ? $(this).find('.txtMajor').val() : null;
                    Response.IsSpecialAllowance = false;
                    ResponseForm.push(Response);
                }
            });
        }
        var JSONResponse = JSON.stringify(ResponseForm);
        var service = new HrmsSuiteHcmService.HcmService();
        service.saveMultipleAllowanceMapping(JSONResponse, PopupOpen, onSaveByDesignation, null, null);
    }
    else {
        showError("No Employee Found");
    }
}

function SaveByDesignation() {

    var ResponseForm = [];
    $(EmployeeId).each(function (k, v) {

        $('.trInputs').each(function () {

            if ($(this).find('.chkSelect').is(':checked')) {
                var Response = new Object();

                Response.EmployeeId = v;
                Response.AllowanceId = $(this).find('.hfAllowanceId').val();
                Response.Measure = $(this).find('.hfIsFormulaExist').val() == "false" ? $(this).find('.txtMajor').val() : null;
                Response.IsSpecialAllowance = false;
                ResponseForm.push(Response);


            }
        });

    });

    var JSONResponse = JSON.stringify(ResponseForm);
    var service = new HrmsSuiteHcmService.HcmService();
    service.saveAllowanceMapping(JSONResponse, onSaveByDesignation, null, null);

    //alert(JSONResponse);
}

function onSaveByDesignation(result) {

    if (result == 1) {
        ClearFields();
        showSuccess("Data saved successfully");

    }
    else {
        showError("No Data Found");
    }
    ProgressBarHide();
}

function setFilter() {
    //Filter = _Filter;

    //Group = $(".ddlGroup").val();
    //Company = $(".ddlCompany").val();
    //Location = $(".ddlLocation").val();

    //Dcepartment = $(".ddlDepartment").val();
    //CostCenter = $(".ddlCostCenter").val();
    //Designation = $(".ddlDesignation").val();
    //EmpCode = $(".txtEmployeeCode").val();

    if ($(".ddlGroup").val() > 0) {
        FilterValue = $(".ddlGroup").val();
        Filter = "Group";
        FilterText = $(".ddlGroup option:selected").html();
    }
    if ($(".ddlCompany").val() > 0) {
        FilterValue = $(".ddlCompany").val();
        Filter = "Company";
        FilterText = $(".ddlCompany option:selected").html();
    }
    if ($(".ddlLocation").val() > 0) {
        FilterValue = $(".ddlLocation").val();
        Filter = "Location";
        FilterText = $(".ddlLocation option:selected").html();
    }
    if ($(".ddlBU").val() > 0) {
        FilterValue = $(".ddlBU").val();
        Filter = "Business Unit";
        FilterText = $(".ddlBU option:selected").html();
    }
    if ($(".ddlDepartment").val() > 0) {
        FilterValue = $(".ddlDepartment").val();
        Filter = "Department";
        FilterText = $(".ddlDepartment option:selected").html();
    }
    if ($(".ddlCostCenter").val() > 0) {
        FilterValue = $(".ddlCostCenter").val();
        Filter = "Cost Center";
        FilterText = $(".ddlCostCenter option:selected").html();
    }
    if ($(".ddlDesignation").val() > 0) {
        FilterValue = $(".ddlDesignation").val();
        Filter = "Designation";
        FilterText = $(".ddlDesignation option:selected").html();
    }


    //alert(FilterText);
    //alert(Filter);
    $(".lblFilter").html(Filter);
    $(".txtFilterText").val(FilterText);

    EmployeeId = [];
    $('.hfEmployeeId').each(function () {
        EmployeeId.push($(this).val());
    });

    //var JSONResponse = JSON.stringify(EmployeeId);
    //alert(JSONResponse);
    //
    GetAllowance();
}

function GetEmployee() {
    //$('.abcdid').show();



    ProgressBarShow();

    if (PopupOpen == true) {

        Group = $('.clsModal').find('.ddlGroup').val();
        Company = $('.clsModal').find(".ddlCompany").val();
        Location = $('.clsModal').find(".ddlLocation").val();
        BU = $('.clsModal').find(".ddlBU").val();
        Department = $('.clsModal').find(".ddlDepartment").val();
        CostCenter = $('.clsModal').find(".ddlCostCenter").val();
        Designation = $('.clsModal').find(".ddlDesignation").val();
        Firstname = $('.clsModal').find(".txtFirstName").val();
        Lastname = $('.clsModal').find(".txtLastName").val();
        EmpCode = $('.clsModal').find(".txtEmployeeCode").val();
        Category = $('.clsModal').find(".ddlCategory").val();

        var service = new HrmsSuiteHcmService.HcmService();
        //service.getEmployee(Group, Company, Location, BU, CostCenter, Designation, Department, Firstname, Lastname, EmpCode, Category, onGetEmployee, null, null);
        servive.getDesignationForMultiAllowance(Company, Category, Designation, onGetEmployee, null, null);
    }
    else {

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
        Category = $(".ddlCategory").val();

        var service = new HrmsSuiteHcmService.HcmService();
        service.getEmployee(Group, Company, Location, BU, CostCenter, Designation, Department, Firstname, Lastname, EmpCode, Category, onGetEmployeeSearch, null, null);
    }
}

function onGetEmployee(result) {

    res_multi_search = jQuery.parseJSON(result);

    BindSrcOptions(res_multi_search);
    GetAllowance(0);

    ProgressBarHide();
}

function onGetEmployeeSearch(result) {


    $('.tbodyEmployeeListing').html('');
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.tbodyEmployeeListing').html('');
    $('#EmployeeListing').tmpl(res).appendTo(divTbodyGoalFund);
    paginateTable('.tableEmployee', 50);
    ProgressBarHide();

}


function BindSrcOptions(res) {

    $('.multiddlSearch').html('');
    var option = '';
    var selected_dest = $("#multiddlDest option").map(function () { return this.value }).get();

    if (selected_dest.length > 0) {

        $(res).each(function (k, v) {

            var found = false;
            for (var i = 0; i < selected_dest.length; i++) {

                var EmployeeId = selected_dest[i];

                if (v.EmployeeId == EmployeeId) {
                    found = true;
                    break;
                }

            }

            if (found == false) {
                option += '<option value="' + v.EmployeeId + '">' + v.FirstName + ' ' + v.LastName + '</option>';
            }
        });


    }
    else {
        $(res).each(function (k, v) {

            option += '<option value="' + v.EmployeeId + '">' + v.FirstName + ' ' + v.LastName + '</option>';
        });
    }

    $('.multiddlSearch').append(option);
}

function SrcToDest() {

    selected = $('select#multiddlSearch').val();
    selected_text = [];

    $('select#multiddlSearch :selected').each(function (i, _selected) {

        selected_text[i] = $(_selected).text();
    });

    BindDestOptions();

    BindSrcOptions(res_multi_search);

}

function DestToSrc() {


    var option = '';
    var option_dest = '';
    var dest = $('select#multiddlDest').val();
    var dest_text = [];

    $('select#multiddlDest :selected').each(function (i, _dest) {

        dest_text[i] = $(_dest).text();
    });

    $(res_multi_search).each(function (k, v) {

        var found = false;
        for (var i = 0; i < dest.length; i++) {

            var EmployeeId = dest[i];

            if (v.EmployeeId == EmployeeId) {
                found = true;
                break;
            }
        }

        if (found == true) {

            option += '<option value="' + v.EmployeeId + '">' + v.FirstName + ' ' + v.LastName + '</option>';

            $(".multiddlDest option:selected").each(function () {
                $(this).remove(); //or whatever else

            });
        }
    });

    $('.multiddlSearch').append(option);
    $('.multiddlDest').append(option_dest);
}

function BindDestOptions() {
    //alert(selected);
    //$('.multiddlDest').html('');
    var option = '';

    for (var i = 0; i < selected.length; i++) {

        option += '<option value="' + selected[i] + '">' + selected_text[i] + '</option>';
    }

    $('.multiddlDest').append(option);

    selected = [];
    selected_text = [];
}

function ClearFields() {
    ClearSearchFields();

    $('.multiddlSearch').html('');
    $('.multiddlDest').html('');
    $('.tbodyAllowanceListing').html('');
}

function ClearSearchFields() {
    $(".ddlGroup").val(0);
    $(".ddlCompany").val(0);
    $(".ddlLocation").val(0);
    $(".ddlBU").val(0);
    $(".ddlDepartment").val(0);
    $(".ddlCostCenter").val(0);
    $(".ddlDesignation").val(0);
    $(".txtFirstName").val('');
    $(".txtLastName").val('');
    $(".txtEmployeeCode").val('');
    $(".ddlCategory").val(0);
}

function OpenPopupMulti() {

    //PopupOpen = true;
    SetPopupOpen(true);
    $('.clsPopupSearchFilter').show();
    $('.dvSelect').show();

    ClearFields();
}

function OpenPopupSingle(_EmployeeId, _FirstName, _LastName, _CompanyId) {

    EmployeeIdSingle = _EmployeeId;
    //PopupOpen = false;
    SetPopupOpen(false);
    $('.clsPopupSearchFilter').hide();
    $('.dvSelect').hide();

    var option = '';
    $('.multiddlDest').html('');
    option = '<option value="' + _EmployeeId + '">' + _FirstName + ' ' + _LastName + '</option>';
    $('.multiddlDest').append(option);

    GetAllowance(_CompanyId);
}

function SetPopupOpen(IsOpen) {
    PopupOpen = IsOpen;
}

function BindSrc(res) {
    $('.multiddlSearch').html('');
    var option = '';
    var selected_dest = $("#multiddlDest option").map(function () { return this.value }).get();

    if (selected_dest.length > 0) {
        SrcOptions_Value = [];
        SrcOptions_Text = [];

        $(res).each(function (k, v) {

            var found = false;
            for (var i = 0; i < selected_dest.length; i++) {

                var EmployeeId = selected_dest[i];

                if (v.EmployeeId == EmployeeId) {
                    found = true;
                    break;
                }

            }

            if (found == false) {

                SrcOptions_Value.push(v.EmployeeId);
                SrcOptions_Text.push(v.FirstName + ' ' + v.LastName);

                //option += '<option value="' + v.EmployeeId + '">' + v.FirstName + ' ' + v.LastName + '</option>';
            }
        });


    }
    else {
        $(res).each(function (k, v) {

            //option += '<option value="' + v.EmployeeId + '">' + v.FirstName + ' ' + v.LastName + '</option>';

            SrcOptions_Value.push(v.EmployeeId);
            SrcOptions_Text.push(v.FirstName + ' ' + v.LastName);
        });
    }

    for (var i = 0; i < SrcOptions_Value.length; i++) {
        option += '<option value="' + SrcOptions_Value[i] + '">' + SrcOptions_Text[i] + '</option>';
    }

    $('.multiddlSearch').append(option);
}

function BindDest() {
    $('.multiddlDest').html('');
    var option = '';

    DestOptions_Value = $('select#multiddlSearch').val();

    $('select#multiddlSearch :selected').each(function (i, selector) {

        DestOptions_Text[i] = $(selector).text();
    });

    for (var i = 0; i < DestOptions_Value.length; i++) {
        option += '<option value="' + DestOptions_Value[i] + '">' + DestOptions_Text[i] + '</option>';
    }

    $('.multiddlDest').append(option);

    $(".multiddlSearch option:selected").each(function () {
        $(this).remove(); //or whatever else

    });

}

function RemoveMultipleAllowances() {

    if (ValidateRemove()) {
        showError('Please Select Allowance');
        return;
    }
    ProgressBarShow();
    var selected_dest = $("#multiddlDest option").map(function () { return this.value }).get();

    if (selected_dest.length > 0) {
        var ResponseForm = [];
        for (var i = 0; i < selected_dest.length; i++) {

            var EmployeeId = selected_dest[i];

            $('.trInputs').each(function () {

                if ($(this).find('.chkSelect').is(':checked')) {

                    var Response = new Object();
                    Response.EmployeeId = EmployeeId;
                    Response.AllowanceId = $(this).find('.hfAllowanceId').val();
                    //Response.Measure = $(this).find('.hfIsFormulaExist').val() == "false" ? $(this).find('.txtMajor').val() : null;
                    //Response.IsSpecialAllowance = false;
                    ResponseForm.push(Response);
                }
            });
        }
        var JSONResponse = JSON.stringify(ResponseForm);
        var service = new HrmsSuiteHcmService.HcmService();
        service.removeMultipleAllowanceMapping(JSONResponse, onRemoveMultipleAllowances, null, null);
    }
    else {
        showError("No Employee Found");
    }
}

function onRemoveMultipleAllowances(result) {

    if (result == 1) {
        ClearFields();
        showSuccess("Allowances removed successfully");

    }
    else {
        showError("No Data Found");
    }
    ProgressBarHide();
}

function ValidateRemove() {

    var IsFlag = 0;
    $('.trInputs').each(function () {
        if ($(this).find('.chkSelect').is(':checked')) {

            //var FormulaExist = $(this).find('.hfIsFormulaExist').val();

            //if (FormulaExist == 'false') {
            //var txtMeasure = $(this).find('.txtMajor').val();

            //if (txtMeasure == '') {
            //showError('Please Select Allowance');

            if (IsFlag == 0) {
                IsFlag = 1;
            }
            //}
            //}
        }
    });

    if (IsFlag == 0) {
        return true;
    }
    else {
        return false;
    }

}