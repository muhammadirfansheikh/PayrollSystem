

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
    var BankId = $('.ddlBranch').val();

    ProgressBarShow();

    var service = new HrmsSuiteHcmService.HcmService();
    service.report_LoanJV( CompanyId, PayrollMonth,  onGetReportStaffLoan, null, null);
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

    $('.clsCompanyLoan').each(function () {
        InstallmentTotal += parseFloat($(this).text().trim());
    });

    $('.clsOtherLoan').each(function () {
        BalanceTotal += parseFloat($(this).text().trim());
    });

    $('.tdAmount').text(InstallmentTotal);
    $('.tdAdvance').text(BalanceTotal);

    var Prev = 0;
    var i = 0;
    var _Sum1 = 0, _Sum2 = 0, _Sum3 = 0;
    var GroupBy = '.' + $('.ddlGroupBy').val();
    var GroupByName = $(".ddlGroupBy option:selected").text();
    var ColSpan =  0;

    $('.trList').each(function () {
        
        var GroupByValue = $('.ddlGroupBy').val();

        if (GroupByValue != 0) {

            var CurrLocId = $(this).find('.ABC').find(GroupBy).val();

            if ($(this).is(':first-child')) {
                _Sum1 += parseFloat($(this).find('.clsCompanyLoan').text());
                _Sum2 += parseFloat($(this).find('.clsOtherLoan').text());
              
            }
            else {

                if (Prev == CurrLocId) {
                    _Sum1 += parseFloat($(this).find('.clsCompanyLoan').text());
                    _Sum2 += parseFloat($(this).find('.clsOtherLoan').text());
                }
                else {

                    var GroupByItem = $(this).prev().find('.ABC').find(GroupBy).val();

                    //_Sum3 = isNaN(_Sum3) ? '' : _Sum3;

                    $("<tr class='success'><th colspan=" + ColSpan + ">" + GroupByName + " : " + GroupByItem + "</th><th>" + _Sum1 + "</th><th>" + _Sum2 + "</th></tr>").insertBefore($(this));
                    i = -1;
                    _Sum1 = parseFloat($(this).find('.clsCompanyLoan').text());
                    _Sum2 = parseFloat($(this).find('.clsOtherLoan').text());
                    
                }

                if ($(this).is(':last-child')) {

                    var GroupByItem = $(this).find('.ABC').find(GroupBy).val();

                    //_Sum3 = isNaN(_Sum3) ? '' : _Sum3;

                    $("<tr class='success'><th colspan=" + ColSpan + ">" + GroupByName + " : " + GroupByItem + "</th><th>" + _Sum1 + "</th><th>" + _Sum2 + "</th></tr>").insertAfter($(this));
                    i = -1;
                    _Sum1 = 0;
                    _Sum2 = 0;
                   
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

    SetReportHeader('JV Loan Report', 7, Para2);

    ProgressBarHide();
}


function TriggerPageLoads() {
    //BindBank();
    BindGroupByDDL();

    //$('.ddlGroupBy').append($("<option></option>").val('clsPaymentType').html('Payment Type'));
    $('.ddlGroupBy option').filter('[value="clsCompany"],[value="clsDesignation"],[value="clsDepartment"]').remove();
}


