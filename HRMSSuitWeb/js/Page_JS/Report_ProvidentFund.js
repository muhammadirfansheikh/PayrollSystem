function TriggerPageLoads() {
    $(".btnCancelSearch").click(function () {
        $('.txtMonthOfPayroll').datepicker('setDate', null);
        $('.ddlGroupBy').val('0');
        ClearReport();
    });

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
        ProgressBarShow();
        ClearReport();
        var service = new HrmsSuiteHcmService.HcmService();
        service.report_ProvidentFund(EmployeeCode, GroupId, CompanyId, LocationId, BusinessUnitId, DepartmentId, CostCenterId, CategoryId, DesignationId, Firstname, Lastname, PayrollMonth, onProvidentFundReport, null, null);
    } else {
        showError('Please select Company');
    }
}

function onProvidentFundReport(result) {
    var res = jQuery.parseJSON(result);
    if (res.length > 0) {

        var InterestIncome = 0;
        var MonthlyCont = 0;
        var EmpBalance = 0;
        var LoanInstallment = 0;
        var Balance = 0;
        var InstrestAmount = 0;
        var LoanInstAmount = 0;
        var GroupByValue = $('.ddlGroupBy').val();
        var Prev = 0;
        var i = 0;
        var _Sum1 = 0, _Sum2 = 0, _Sum3 = 0, _Sum4 = 0, _Sum5 = 0, _Sum6 = 0, _Sum7 = 0;
        var GroupBy = '.' + $('.ddlGroupBy').val();
        var GroupByName = $(".ddlGroupBy option:selected").text();
        var ColSpan = 4;//$('.clsMonthlyCont').index();
        
        if (GroupByValue != 0) {
            res = res.sort(sortByProperty(GroupByValue));
        }
        var divTbodyGoalFund = $('.ProvidentFundSummary').html('');
        $('#ProvidentFundSummary').tmpl(res).appendTo(divTbodyGoalFund);

        $('.trList').each(function () {
            InterestIncome += parseFloat($(this).find(".clsInterestIncome").text().trim());
            MonthlyCont += parseFloat($(this).find(".clsMonthlyCont").text().trim());
            EmpBalance += parseFloat($(this).find(".clsEmpBalance").text().trim());
            LoanInstallment += parseFloat($(this).find(".clsLoanInst").text().trim());
            Balance += parseFloat($(this).find(".clsBalance").text().trim());
            InstrestAmount += parseFloat($(this).find(".clsIntrestAmount").text().trim());
            LoanInstAmount += parseFloat($(this).find(".clsLoanInstAmount").text().trim());

            if (!$(this).hasClass("success")) {
                if (GroupByValue != 0) {
                    var CurrLocId = $(this).find('.ABC').find(GroupBy).val();

                    if ($(this).is(':first-child')) {
                        _Sum1 += parseFloat($(this).find('.clsMonthlyCont').text());
                        _Sum2 += parseFloat($(this).find('.clsEmpBalance').text());
                        _Sum3 += parseFloat($(this).find('.clsInterestIncome').text());
                        _Sum4 += parseFloat($(this).find('.clsLoanInst').text());
                        _Sum5 += parseFloat($(this).find('.clsBalance').text());
                        _Sum6 += parseFloat($(this).find('.clsIntrestAmount').text());
                        _Sum7 += parseFloat($(this).find('.clsLoanInstAmount').text());
                       
                    }
                    else {

                        if (Prev == CurrLocId) {
                            _Sum1 += parseFloat($(this).find('.clsMonthlyCont').text());
                            _Sum2 += parseFloat($(this).find('.clsEmpBalance').text());
                            _Sum3 += parseFloat($(this).find('.clsInterestIncome').text());
                            _Sum4 += parseFloat($(this).find('.clsLoanInst').text());
                            _Sum5 += parseFloat($(this).find('.clsBalance').text());
                            _Sum6 += parseFloat($(this).find('.clsIntrestAmount').text());
                            _Sum7 += parseFloat($(this).find('.clsLoanInstAmount').text());
                        }
                        else {

                            var GroupByItem = $(this).prev().find('.ABC').find(GroupBy).val();
                            $("<tr class='success XYZ'><th colspan=" + ColSpan + " style='text-align:left'>" + GroupByName + " : " + GroupByItem + "</th><th style='text-align:right'>" + _Sum1 + "</th><th style='text-align:right'>" + _Sum2 + "</th><th style='text-align:right'>" + _Sum3 + "</th><th></th><th style='text-align:right'>" + _Sum4 + "</th><th style='text-align:right'>" + _Sum5 + "</th><th style='text-align:right'>" + _Sum7 + "</th></tr>").insertBefore($(this));
                            i = -1;
                            _Sum1 = parseFloat($(this).find('.clsMonthlyCont').text());
                            _Sum2 = parseFloat($(this).find('.clsEmpBalance').text());
                            _Sum3 = parseFloat($(this).find('.clsInterestIncome').text());
                            _Sum4 = parseFloat($(this).find('.clsLoanInst').text());
                            _Sum5 = parseFloat($(this).find('.clsBalance').text());
                            _Sum6 = parseFloat($(this).find('.clsIntrestAmount').text());
                            _Sum7 = parseFloat($(this).find('.clsLoanInstAmount').text());
                        }

                        if ($(this).is(':last-child')) {

                            var GroupByItem = $(this).find('.ABC').find(GroupBy).val();
                            $("<tr class='success XYZ'><th colspan=" + ColSpan + " style='text-align:left'>" + GroupByName + " : " + GroupByItem + "</th><th style='text-align:right'>" + _Sum1 + "</th><th style='text-align:right'>" + _Sum2 + "</th><th style='text-align:right'>" + _Sum3 + "</th><th></th><th  style='text-align:right'>" + _Sum4 + "</th><th  style='text-align:right'>" + _Sum5 + "</th><th  style='text-align:right'>" + _Sum7 + "</th></tr>").insertAfter($(this));
                            i = -1;
                            _Sum1 = 0;
                        } 
                    }

                    Prev = CurrLocId;
                    i++;
                }
                //else {
                //    var divTbodyGoalFund = $('.ProvidentFundSummary').html('');
                //    $('#ProvidentFundSummary').tmpl(res).appendTo(divTbodyGoalFund);
                //}
            }
        });

        $('.tdMonthlyCont').text(MonthlyCont);
        $('.tdEmpBalance').text(EmpBalance);
        $('.tdLoanInst').text(LoanInstallment);
        $('.tdBalance').text(Balance);
        /*$('.tdIntrestAmount').text(InstrestAmount);*/
        $('.tdLoanInstAmount').text(LoanInstAmount);
        $('.tdInterestIncome').text(InterestIncome);


        SetReportHeader('Provident Fund Report', 12, '');
        $('.div_reportbutton').show();
    }
    ProgressBarHide();
}

function ClearReport() {
    $('.clsReportH').hide();
    //$('.HeaderColoumn').remove();
    $('.ProvidentFundSummary').html('');
    $('.div_reportbutton').hide();


    $('.tdMonthlyCont').text('');
    $('.tdEmpBalance').text('');
    $('.tdLoanInst').text('');
    $('.tdBalance').text('');
    $('.tdIntrestAmount').text('');
    $('.tdLoanInstAmount').text('');
    $('.tdInterestIncome').text('');

}

