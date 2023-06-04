function TriggerPageLoads() {
    $(".btnCancelSearch").click(function () {
        $('.txtMonthOfPayroll').datepicker('setDate', null);
        ClearReport();
    });
}

function GetEmployee() {
    var CompanyId = $('.ddlCompany').val();
    if (CompanyId > 0) {
        if (!validateForm('.divMonthPayroll'))
            return;
        var PayrollMonth = formatDate($('.txtMonthOfPayroll').val());
        var EmployeeCode = $('.txtEmployeeCode').val() == '' ? 0 : $('.txtEmployeeCode').val();
        var GroupId = $('.ddlGroup').val();
        var LocationId = $('.ddlLocation').val();
        var BusinessUnitId = $('.ddlBU').val();
        var DepartmentId = $('.ddlDepartment').val();
        var CostCenterId = $('.ddlCostCenter').val();
        var CategoryId = $('.ddlCategoryC').val();
        var DesignationId = $('.ddlDesignation').val();
        var Firstname = $('.txtFirstName').val();
        var Lastname = $('.txtLastName').val();
        ProgressBarShow();
        ClearReport();
        var service = new HrmsSuiteHcmService.HcmService();
        service.report_ProportionateList(EmployeeCode, GroupId, CompanyId, LocationId, BusinessUnitId, DepartmentId, CostCenterId, CategoryId, DesignationId, Firstname, Lastname, PayrollMonth, onGetProportionateList, null, null);
    } else {
        showError('Please select Company');
    }
}

function onGetProportionateList(result) {
    var res = jQuery.parseJSON(result);
    if (res.length > 0) {
        var divTbodyGoalFund = $('.tbodypropotionatelisting').html('');
        $('#ProportionateListing').tmpl(res).appendTo(divTbodyGoalFund);
        $('.tdGrossSalary').text(res[0].TotalGrossSalaries);
        $('.tdProportionateRecovery').text(res[0].ProportionateRecovery);
        $('.tdNetPayable').text(res[0].NetPayable);
        $('.tdTotalNumEmployees').text(res[0].TotalEmployees);
        SetReportHeader('Propotionate Report', 8, '');
        $('.div_reportbutton').show();
    }
    ProgressBarHide();
}

function ClearReport() {
    $('.clsReportH').hide();
    $('.HeaderColoumn').remove();
    $('.tbodypropotionatelisting').html('');
    $('.div_reportbutton').hide();
    $('.tdGrossSalary').text('0.0');
    $('.tdProportionateRecovery').text('0.0');
    $('.tdNetPayable').text('0.0');
    $('.tdTotalNumEmployees').text(0);
}