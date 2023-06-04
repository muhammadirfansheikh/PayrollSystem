function GetEmployee() {
    if (!validateForm('.divMonthPayroll'))
        return;

    var PayrollMonth = formatDate($('.txtMonthOfPayroll').val());

    var EmployeeCode = $('.txtEmployeeCode').val() == '' ? 0 : $('.txtEmployeeCode').val();
    var GroupId = $('.ddlGroup').val();
    var CompanyId = $('.ddlCompany').val();
    var LocationId = $('.ddlLocation').val();
    var BusinessUnitId = $('.ddlBU').val();
    var DepartmentId = $('.ddlDepartment').val();
    var CostCenterId = $('.ddlCostCenter').val();
    var CategoryId = $('.ddlCategoryC').val();
    var DesignationId = $('.ddlDesignation').val();
    var Firstname = $('.txtFirstName').val();
    var Lastname = $('.txtLastName').val();

    ProgressBarShow();

    var service = new HrmsSuiteHcmService.HcmService();
    service.report_LoanSummary(EmployeeCode, GroupId, CompanyId, LocationId, BusinessUnitId, DepartmentId, CostCenterId, CategoryId, DesignationId, Firstname, Lastname, PayrollMonth, onGetLoanSummary, null, null);
}


function onGetLoanSummary(result) {
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.tbodyLoanSummary').html('');
    $('#LoanSummary').tmpl(res).appendTo(divTbodyGoalFund);

    var InstallmentTotal = 0,BalanceTotal = 0,InterestAmountTotal = 0,InterestInstAmountTotal = 0;

    $('.clsInstallmentAmount').each(function () {
        InstallmentTotal += parseFloat($(this).text().trim());
    });

    $('.clsBalance').each(function () {
        BalanceTotal += parseFloat($(this).text().trim());
    });

    $('.clsInterestAmount').each(function () {
        InterestAmountTotal += parseFloat($(this).text().trim());
    });

    $('.clsInterestInstallmentAmount').each(function () {
        InterestInstAmountTotal += parseFloat($(this).text().trim());
    });

    $('.tdInstallmentAmount').text(InstallmentTotal);
    $('.tdBalance').text(BalanceTotal);
    $('.tdInterestAmount').text(InterestAmountTotal);
    $('.tdInterestInstallmentAmount').text(InterestInstAmountTotal);

    var Prev = 0;
    var i = 0;
    var _Sum1 = 0, _Sum2 = 0, _Sum3 = 0, _Sum4 = 0;
    var GroupBy = '.' + $('.ddlGroupBy').val();

    var ColSpan = $('.clsInstallmentAmount').index() - 1;

    $('.trList').each(function () {

        var GroupByValue = $('.ddlGroupBy').val();

        if (GroupByValue != 0) {

            var CurrLocId = $(this).find(GroupBy).text();

            if ($(this).is(':first-child')) {
                _Sum1 += parseFloat($(this).find('.clsInstallmentAmount').text());
                _Sum2 += parseFloat($(this).find('.clsBalance').text());
                _Sum3 += parseFloat($(this).find('.clsInterestAmount').text());
                _Sum4 += parseFloat($(this).find('.clsInterestInstallmentAmount').text());
            }
            else {

                if (Prev == CurrLocId) {
                    _Sum1 += parseFloat($(this).find('.clsInstallmentAmount').text());
                    _Sum2 += parseFloat($(this).find('.clsBalance').text());
                    _Sum3 += parseFloat($(this).find('.clsInterestAmount').text());
                    _Sum4 += parseFloat($(this).find('.clsInterestInstallmentAmount').text());
                }
                else {

                    $("<tr class='success'><th colspan=" + ColSpan + ">Total</th><th>" + _Sum1 + "</th><th>" + _Sum2 + "</th><th>" + _Sum3 + "</th><th>" + _Sum4 + "</th></tr>").insertBefore($(this));
                    i = -1;
                    _Sum1 = parseFloat($(this).find('.clsInstallmentAmount').text());
                    _Sum2 = parseFloat($(this).find('.clsBalance').text());
                    _Sum3 = parseFloat($(this).find('.clsInterestAmount').text());
                    _Sum4 = parseFloat($(this).find('.clsInterestInstallmentAmount').text());
                }

                if ($(this).is(':last-child')) {
                    $("<tr class='success'><th colspan=" + ColSpan + ">Total</th><th>" + _Sum1 + "</th><th>" + _Sum2 + "</th><th>" + _Sum3 + "</th><th>" + _Sum4 + "</th></tr>").insertAfter($(this));
                    i = -1;
                    _Sum1 = 0;
                }
            }

            Prev = CurrLocId;
            i++;
        }
        else {
            var divTbodyGoalFund = $('.tbodyLoanSummary').html('');
            $('#LoanSummary').tmpl(res).appendTo(divTbodyGoalFund);
        }
    });

    ProgressBarHide();

    SetReportHeader('Loan Summary Report', 6, '');
}


function TriggerPageLoads() {

    BindGroupByDDL();
    $('.ddlGroupBy option').filter('[value="clsCompany"],[value="clsLocation"],[value="clsDepartment"],[value="clsCostCenter"],[value="clsCostCenter"]').remove();
    $('.ddlGroupBy').append($("<option></option>").val('clsLoanType').html('Loan Type'));
}