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
        //var GroupId = $('.ddlGroup').val();
        //var LocationId = $('.ddlLocation').val();
        //var BusinessUnitId = $('.ddlBU').val();
        //var DepartmentId = $('.ddlDepartment').val();
        //var CostCenterId = $('.ddlCostCenter').val();
        //var CategoryId = $('.ddlCategoryC').val();
        //var DesignationId = $('.ddlDesignation').val();
        //var Firstname = $('.txtFirstName').val();
        //var Lastname = $('.txtLastName').val();
        ProgressBarShow();
        ClearReport();
        var service = new HrmsSuiteHcmService.HcmService();
        service.getReportPfLoanClosingBalance(CompanyId, PayrollMonth, EmployeeCode, onGetPfLoanClosingBalanceList, null, null);
    } else {
        showError('Please select Company');
    }
}

function onGetPfLoanClosingBalanceList(result) {
    var res = jQuery.parseJSON(result);
    if (res.length > 0) {
        var TotalMaster = 0;
        var divTbodyGoalFund = $('.tbodyPfLoanClosingBalancelisting').html('');
        $('#PfLoanClosingBalanceListing').tmpl(res).appendTo(divTbodyGoalFund);
        $('.tdMasterSalary').each(function () {
            TotalMaster += parseFloat($(this).text().trim());
        });
        $('.tdTotal').text(TotalMaster);
        SetReportHeader('PF Loan Closing Report', 4, '');
        $('.div_reportbutton').show();
    }
    ProgressBarHide();

}

function ClearReport() {
    $('.clsReportH').hide();
    $('.tbodyPfLoanClosingBalancelisting').html('');
    $('.tdTotal').text('');
    $('.div_reportbutton').hide();
}