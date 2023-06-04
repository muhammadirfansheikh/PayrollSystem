function TriggerPageLoads() {
    $(".btnCancelSearch").click(function () {
        $('.txtMonthOfPayroll').datepicker('setDate', null);
        $('.ddlGroupBy').val('0');
        ClearReport();
    });

    //BindBank();
    BindGroupByDDL();
    $('.ddlGroupBy option').filter('[value="clsCompany"],[value="clsDesignation"],[value="clsDepartment"],[value="clsBankName"]').remove();
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
        service.report_IncomeTax(EmployeeCode, GroupId, CompanyId, LocationId, BusinessUnitId, DepartmentId, CostCenterId,
            CategoryId, DesignationId, Firstname, Lastname, PayrollMonth, BankId, onGetReportIncomeTax, null, null);
    } else {
        showError('Please select Company');
    }
}

function onGetReportIncomeTax(result) {
    var GroupByValue = $('.ddlGroupBy').val();

    var res = jQuery.parseJSON(result);

    if (GroupByValue != 0) {
        res = res.sort(sortByProperty(GroupByValue));
    }
    var divTbodyGoalFund = $('.tbodyIncomeTaxListing').html('');
    $('#IncomeTaxListing').tmpl(res).appendTo(divTbodyGoalFund);

  
    var TotalTaxPaid = 0;
    var TotalTaxableIncome = 0;
    var TotalAccTax = 0;

    

    $('.clsTotalTaxableIncome').each(function () {
        TotalTaxableIncome += parseFloat($(this).text().trim());
    });

    $('.clsTotalTaxPaid').each(function () {
        TotalTaxPaid += parseFloat($(this).text().trim());
    });

    $('.clsTotalAccumulatedTax').each(function () {
        TotalAccTax += parseFloat($(this).text().trim());
    });

    
    $('.tdTotalTaxableIncome').text(TotalTaxableIncome);
    $('.tdTotalTaxPaid').text(TotalTaxPaid);
    $('.tdTotalAccTax').text(TotalAccTax);

    var Prev = 0;
    var i = 0;
    var  _Sum2 = 0, _Sum3 = 0, _Sum4 = 0;
    var GroupBy = '.' + $('.ddlGroupBy').val();

    var ColSpan = $('.clsTotalTaxableIncome').index() - 0;

    var GroupByName = $(".ddlGroupBy option:selected").text();

    $('.trList').each(function () {

        var GroupByValue = $('.ddlGroupBy').val();

        if (GroupByValue != 0) {

            var CurrLocId = $(this).find('.ABC').find(GroupBy).val();

            if ($(this).is(':first-child')) {
                
                _Sum2 += parseFloat($(this).find('.clsTotalTaxableIncome').text());
                _Sum3 += parseFloat($(this).find('.clsTotalTaxPaid').text());
                _Sum4 += parseFloat($(this).find('.clsTotalAccumulatedTax').text());
            }
            else {

                if (Prev == CurrLocId) {
                    
                    _Sum2 += parseFloat($(this).find('.clsTotalTaxableIncome').text());
                    _Sum3 += parseFloat($(this).find('.clsTotalTaxPaid').text());
                    _Sum4 += parseFloat($(this).find('.clsTotalAccumulatedTax').text());
                }
                else {

                    var GroupByItem = $(this).prev().find('.ABC').find(GroupBy).val();

                    $("<tr class='success'><th colspan=" + ColSpan + " style='text-align:left'>" + GroupByName + " : " + GroupByItem + "</th><th style='text-align:right'>" + _Sum2 + "</th><th style='text-align:right'>" + _Sum3 + "</th><th style='text-align:right'>" + _Sum4 + "</th></tr>").insertBefore($(this));
                    i = -1;
                    _
                    _Sum2 = parseFloat($(this).find('.clsTotalTaxableIncome').text());
                    _Sum3 = parseFloat($(this).find('.clsTotalTaxPaid').text());
                    _Sum4 = parseFloat($(this).find('.clsTotalAccumulatedTax').text());
                }

                if ($(this).is(':last-child')) {
                    var GroupByItem = $(this).find('.ABC').find(GroupBy).val();
                    $("<tr class='success'><th colspan=" + ColSpan + " style='text-align:left'>" + GroupByName + " : " + GroupByItem + "</th><th style='text-align:right'>" + _Sum2 + "</th><th style='text-align:right'>" + _Sum3 + "</th><th style='text-align:right'>" + _Sum4 + "</th></tr>").insertAfter($(this));
                    i = -1;
                    
                    _Sum2 = 0;
                    _Sum3 = 0;
                    _Sum4 = 0;
                }
            }

            Prev = CurrLocId;
            i++;
        }
        else {
            var divTbodyGoalFund = $('.tbodyIncomeTaxListing').html('');
            $('#IncomeTaxListing').tmpl(res).appendTo(divTbodyGoalFund);

        }
    });

    var Para2 = '';
    var BranchId = $('.ddlBranch').val();
    if (BranchId != 0) {

        Para2 = $('.ddlBank option:selected').text() + ' - ' + $('.ddlBranch option:selected').text();
    }

    SetReportHeader('Income Tax Report', 10, Para2);
    $('.div_reportbutton').show();
    ProgressBarHide();
}

function ClearReport() {
    $('.div_reportbutton').hide();
    $('.clsReportH').hide();
    $('.tbodyIncomeTaxListing').html('');
    $('.tdTotalAmount').text('');
    $('.tdTotalTaxableIncome').text('');
    $('.tdTotalTaxPaid').text('');
    $('.tdTotalAccTax').text('');

}

function onGetReportIncomeTax1(result) {
    var GroupByValue = $('.ddlGroupBy').val();

    var res = jQuery.parseJSON(result);

    if (GroupByValue != 0) {
        res = res.sort(sortByProperty(GroupByValue));
    }
    var divTbodyGoalFund = $('.tbodyIncomeTaxListing').html('');
    $('#IncomeTaxListing').tmpl(res).appendTo(divTbodyGoalFund);

    var TotalAmount = 0;
    var TotalTaxPaid = 0;
    var TotalTaxableIncome = 0;
    var TotalAccTax = 0;

    $('.clsTotalAmount').each(function () {
        TotalAmount += parseFloat($(this).text().trim());
    });

    $('.clsTotalTaxableIncome').each(function () {
        TotalTaxableIncome += parseFloat($(this).text().trim());
    });

    $('.clsTotalTaxPaid').each(function () {
        TotalTaxPaid += parseFloat($(this).text().trim());
    });

    $('.clsTotalAccumulatedTax').each(function () {
        TotalAccTax += parseFloat($(this).text().trim());
    });

    $('.tdTotalAmount').text(TotalAmount);
    $('.tdTotalTaxableIncome').text(TotalTaxableIncome);
    $('.tdTotalTaxPaid').text(TotalTaxPaid);
    $('.tdTotalAccTax').text(TotalAccTax);

    var Prev = 0;
    var i = 0;
    var _Sum1 = 0, _Sum2 = 0, _Sum3 = 0, _Sum4 = 0;
    var GroupBy = '.' + $('.ddlGroupBy').val();

    var ColSpan = $('.clsTotalAmount').index() - 1;

    var GroupByName = $(".ddlGroupBy option:selected").text();

    $('.trList').each(function () {

        var GroupByValue = $('.ddlGroupBy').val();

        if (GroupByValue != 0) {

            var CurrLocId = $(this).find('.ABC').find(GroupBy).val();

            if ($(this).is(':first-child')) {
                _Sum1 += parseFloat($(this).find('.clsTotalAmount').text());
                _Sum2 += parseFloat($(this).find('.clsTotalTaxableIncome').text());
                _Sum3 += parseFloat($(this).find('.clsTotalTaxPaid').text());
                _Sum4 += parseFloat($(this).find('.clsTotalAccumulatedTax').text());
            }
            else {

                if (Prev == CurrLocId) {
                    _Sum1 += parseFloat($(this).find('.clsTotalAmount').text());
                    _Sum2 += parseFloat($(this).find('.clsTotalTaxableIncome').text());
                    _Sum3 += parseFloat($(this).find('.clsTotalTaxPaid').text());
                    _Sum4 += parseFloat($(this).find('.clsTotalAccumulatedTax').text());
                }
                else {

                    var GroupByItem = $(this).prev().find('.ABC').find(GroupBy).val();

                    $("<tr class='success'><th colspan=" + ColSpan + ">" + GroupByName + " : " + GroupByItem + "</th><th>" + _Sum1 + "</th><th>" + _Sum2 + "</th><th>" + _Sum3 + "</th><th>" + _Sum4 + "</th><th></th></tr>").insertBefore($(this));
                    i = -1;
                    _Sum1 = parseFloat($(this).find('.clsTotalAmount').text());
                    _Sum2 = parseFloat($(this).find('.clsTotalTaxableIncome').text());
                    _Sum3 = parseFloat($(this).find('.clsTotalTaxPaid').text());
                    _Sum4 = parseFloat($(this).find('.clsTotalAccumulatedTax').text());
                }

                if ($(this).is(':last-child')) {
                    var GroupByItem = $(this).find('.ABC').find(GroupBy).val();
                    $("<tr class='success'><th colspan=" + ColSpan + ">" + GroupByName + " : " + GroupByItem + "</th><th>" + _Sum1 + "</th><th>" + _Sum2 + "</th><th>" + _Sum3 + "</th><th>" + _Sum4 + "</th><th></th></tr>").insertAfter($(this));
                    i = -1;
                    _Sum1 = 0;
                    _Sum2 = 0;
                    _Sum3 = 0;
                    _Sum4 = 0;
                }
            }

            Prev = CurrLocId;
            i++;
        }
        else {
            var divTbodyGoalFund = $('.tbodyIncomeTaxListing').html('');
            $('#IncomeTaxListing').tmpl(res).appendTo(divTbodyGoalFund);

        }
    });

    var Para2 = '';
    var BranchId = $('.ddlBranch').val();
    if (BranchId != 0) {

        Para2 = $('.ddlBank option:selected').text() + ' - ' + $('.ddlBranch option:selected').text();
    }

    SetReportHeader('Income Tax Report', 9, Para2);
}


/*
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

    var service = new HrmsSuiteHcmService.HcmService();
    service.getBankBranch(BankBranchId, onGetBankBranch, null, null);
}

function onGetBankBranch(result) {
    var res = jQuery.parseJSON(result);

    FillDropDownByReference('.ddlBranch', res);
}

*/