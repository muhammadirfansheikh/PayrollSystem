function TriggerPageLoads() {
    $(".btnCancelSearch").click(function () {
        $('.txtMonthOfPayroll').datepicker('setDate', null);
        $('.ddlGroupBy').val('0');
        $('.ddlBank').val('0').change();
        ClearReport();
    });

    //BindBank();
    BindGroupByDDL();
    $('.ddlGroupBy option').filter('[value="clsCompany"],[value="clsDesignation"],[value="clsDepartment"],[value="clsBankName"]').remove();
}

function BindBank() {
    var service = new HrmsSuiteHcmService.HcmService();
    ProgressBarShow();
    service.getBank(onGetBank, null, null);
}

function onGetBank(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference('.ddlBank', res);
    BindBankBranch();
    ProgressBarHide();
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
        ProgressBarShow();
        ClearReport();
        var service = new HrmsSuiteHcmService.HcmService();
        service.report_StaffLoan(EmployeeCode, GroupId, CompanyId, LocationId, BusinessUnitId, DepartmentId, CostCenterId, CategoryId, DesignationId, Firstname, Lastname, PayrollMonth, BankId, onGetReportStaffLoan, null, null);
    } else {
        showError('Please select Company');
    }
}

function onGetReportStaffLoan(result) {
    var GroupByValue = $('.ddlGroupBy').val();

    var res = jQuery.parseJSON(result);

    if (GroupByValue != 0) {
        res = res.sort(sortByProperty(GroupByValue));
    }
    var divTbodyGoalFund = $('.tbodyStaffLoanListing').html('');
    $('#StaffLoanListing').tmpl(res).appendTo(divTbodyGoalFund);
    var InstallmentTotal = 0;
    var BalanceTotal = 0;
    $('.clsInstallmentAmount').each(function () {
        InstallmentTotal += parseFloat($(this).text().trim());
    });
    $('.clsBalance').each(function () {
        BalanceTotal += parseFloat($(this).text().trim());
    });
    $('.tdTotalInstallment').text(InstallmentTotal);
    $('.tdTotalBalance').text(BalanceTotal);

    var Prev = 0;
    var i = 0;
    var _Sum1 = 0, _Sum2 = 0, _Sum3 = 0;
    var GroupBy = '.' + $('.ddlGroupBy').val();
    var GroupByName = $(".ddlGroupBy option:selected").text();
    var ColSpan = $('.clsInstallmentAmount').index() - 0;

    $('.trList').each(function () {
        var GroupByValue = $('.ddlGroupBy').val();

        if (GroupByValue != 0) {

            var CurrLocId = $(this).find('.ABC').find(GroupBy).val();

            if ($(this).is(':first-child')) {
                _Sum1 += parseFloat($(this).find('.clsInstallmentAmount').text());
                _Sum2 += parseFloat($(this).find('.clsBalance').text());
                _Sum3 += parseFloat($(this).find('.clsInterestAmount').text());
            }
            else {

                if (Prev == CurrLocId) {
                    _Sum1 += parseFloat($(this).find('.clsInstallmentAmount').text());
                    _Sum2 += parseFloat($(this).find('.clsBalance').text());
                    _Sum3 += parseFloat($(this).find('.clsInterestAmount').text());
                }
                else {

                    var GroupByItem = $(this).prev().find('.ABC').find(GroupBy).val();

                    _Sum3 = isNaN(_Sum3) ? '' : _Sum3;

                    $("<tr class='success'><th colspan=" + ColSpan + " style='text-align:left'>" + GroupByName + " : " + GroupByItem + "</th><th style='text-align:right'>" + _Sum1 + "</th><th style='text-align:right'>" + _Sum2 + "</th></tr>").insertBefore($(this));
                    i = -1;
                    _Sum1 = parseFloat($(this).find('.clsInstallmentAmount').text());
                    _Sum2 = parseFloat($(this).find('.clsBalance').text());
                    _Sum3 = parseFloat($(this).find('.clsInterestAmount').text());
                }

                if ($(this).is(':last-child')) {

                    var GroupByItem = $(this).find('.ABC').find(GroupBy).val();

                    _Sum3 = isNaN(_Sum3) ? '' : _Sum3;

                    $("<tr class='success'><th colspan=" + ColSpan + " style='text-align:left'>" + GroupByName + " : " + GroupByItem + "</th><th style='text-align:right'>" + _Sum1 + "</th><th style='text-align:right'>" + _Sum2 + "</th></tr>").insertAfter($(this));
                    i = -1;
                    _Sum1 = 0;
                    _Sum2 = 0;
                    _Sum3 = 0;
                }
            }

            //if (i == 0) {
            //    $("<tr class='success'><th colspan=" + ColSpan + ">Total</th><th>" + _Sum1 + "</th><th>" + _Sum2 + "</th><th>" + _Sum3 + "</th><th></th></tr>").insertAfter($(this));
            //    i = -1;
            //    _Sum1 = 0;
            //    _Sum2 = 0;
            //    _Sum3 = 0;
            //}

            Prev = CurrLocId;
            i++;
        }
        else {
            var divTbodyGoalFund = $('.tbodyStaffLoanListing').html('');
            $('#StaffLoanListing').tmpl(res).appendTo(divTbodyGoalFund);
        }
    });

    var Para2 = '';
    var BranchId = $('.ddlBranch').val();
    if (BranchId != 0) {

        Para2 = $('.ddlBank option:selected').text() + ' - ' + $('.ddlBranch option:selected').text();
    }

    SetReportHeader('Staff Loan Report', 8, Para2);
    $('.div_reportbutton').show();
    ProgressBarHide();
}

function ClearReport() {
    $('.div_reportbutton').hide();
    $('.clsReportH').hide();
    $('.tbodyStaffLoanListing').html('');
    $('.tdTotalInstallment').text('');
    $('.tdTotalBalance').text('');
   
}