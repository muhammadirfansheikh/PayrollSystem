function TriggerPageLoads() {
    $(".btnCancelSearch").click(function () {
        $('.txtMonthOfPayroll').datepicker('setDate', null);
        $('.ddlBank').val('0');
        $('.ddlBank').change();
        ClearReport();
    });
    BindBank();
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
        var BankId = $('.ddlBranch').val();
        var BankMaterId = $('.ddlBank').val();

        ProgressBarShow();
        ClearReport();
        var service = new HrmsSuiteHcmService.HcmService();
        service.report_PayOrder(EmployeeCode, GroupId, CompanyId, LocationId, BusinessUnitId, DepartmentId, CostCenterId,
            CategoryId, DesignationId, Firstname, Lastname, PayrollMonth, BankId, BankMaterId, onGetReportPayOrder, null, null);
    } else {
        showError('Please select Company');
    }
}

function onGetReportPayOrder(result) {
    var res = jQuery.parseJSON(result);
    if (res.length > 0) {
        var divTbodyGoalFund = $('.tbodyPayOrderListing').html('');
        $('#PayOrderListing').tmpl(res).appendTo(divTbodyGoalFund);


        var Total = 0;



        $('.clsTotalPay').each(function () {
            Total += parseFloat($(this).text().trim());
        });


        $('.tdTotal').text(Total);



        var Para2 = '';
        var BranchId = $('.ddlBranch').val();
        if (BranchId != 0) {
            Para2 = $('.ddlBank option:selected').text() + ' - ' + $('.ddlBranch option:selected').text();
        }
        SetReportHeader('Pay Order Report', 5, Para2);
        $('.div_reportbutton').show();
    }
    ProgressBarHide();
}

function BindBank() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getBank(onGetBank, null, null);
}

function onGetBank(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference('.ddlBank', res);
    BindBankBranch();
}

function BindBankBranch() {
    var BankBranchId = $('.ddlBank').val();
    ProgressBarShow();
    var service = new HrmsSuiteHcmService.HcmService();
    service.getBankBranch(BankBranchId, onGetBankBranch, null, null);
}

function onGetBankBranch(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference('.ddlBranch', res);
    ProgressBarHide();
}

function ClearReport() {
    $('.clsReportH').hide();
    $('.tbodyPayOrderListing').html('');
    $('.div_reportbutton').hide();
}