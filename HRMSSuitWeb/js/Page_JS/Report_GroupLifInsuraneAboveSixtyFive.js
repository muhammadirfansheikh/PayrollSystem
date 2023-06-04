function TriggerPageLoads() {
    $(".btnCancelSearch").click(function () {
        $('.txtMonthOfPayroll').datepicker('setDate', null);
        $('.ddlGroupBy').val('0');
        $('.txtPremiumRate').val('0');
        ClearReport();
    });

    //BindBank();
    BindGroupByDDL();
    $('.ddlGroupBy option').filter('[value="clsCompany"],[value="clsDesignation"]').remove();
}

function GetEmployee() {
    var CompanyId = $('.ddlCompany').val();
    if (CompanyId > 0) {
        if (!validateForm('.divMonthPayroll'))
            return;
        var EmployeeCode = $('.txtEmployeeCode').val() == '' ? null : $('.txtEmployeeCode').val();
        var PayrollMonth = formatDate($('.txtMonthOfPayroll').val());
        var PremiumRate = parseFloat($('.txtPremiumRate').val());

        if (PremiumRate != null && PremiumRate > 0) {

            ProgressBarShow();
            ClearReport();
            var service = new HrmsSuiteHcmService.HcmService();
            service.report_GroupLifeInsuranceAboveSixtyFive(EmployeeCode, CompanyId, PremiumRate,
                PayrollMonth, onGetLifeInsuranceAboveSixtyFive, null, null);
        }
        else {
            showError('Please enter premium rate.');
        }
        //var LocationId = $('.ddlLocation').val();
        //var DepartmentId = $('.ddlDepartment').val();
        //var CostCenterId = $('.ddlCostCenter').val();
        //var SapCostCenterId = $('.ddlSapCostCenter').val();


    } else {
        showError('Please select Company');
    }
}

function onGetLifeInsuranceAboveSixtyFive(result) {
    var GroupByValue = $('.ddlGroupBy').val();

    var res = JSON.parse(result);

    if (GroupByValue != 0) {
        res = res.sort(sortByProperty(GroupByValue));
    }
    var divTbodyGoalFund = $('.tbodyGLIAboveListing').html('');
    $('#GLIAboveListing').tmpl(res).appendTo(divTbodyGoalFund);

    var TotalSumInsured = 0;
    var TotalPremiumRate = 0;
    var TotalPremiumAmount = 0;


    $('.clsSumInsured').each(function () {
        TotalSumInsured += parseFloat($(this).text().trim());
    });

    $('.clsPremiumRate').each(function () {
        TotalPremiumRate += parseFloat($(this).text().trim());
    });

    $('.clsPremiumAmount').each(function () {
        TotalPremiumAmount += parseFloat($(this).text().trim());
    });

    $('.tdTotalSumInsured').text(TotalSumInsured);
    $('.tdTotalSumPremiumRate').text(TotalPremiumRate);
    $('.tdTotalSumPremiumAmount').text(TotalPremiumAmount);

    var Prev = 0;
    var i = 0;
    var _Sum1 = 0, _Sum2 = 0; _Sum3 = 0;
    var GroupBy = '.' + $('.ddlGroupBy').val();

    var ColSpan = $('.clsSumInsured').index() - 0;

    var GroupByName = $(".ddlGroupBy option:selected").text();

    var GroupByValue = $('.ddlGroupBy').val();

    if (GroupByValue != 0) {

        $('.trList').each(function () {


            var CurrLocId = $(this).find('.ABC').find(GroupBy).val();

            if ($(this).is(':first-child')) {
                _Sum1 += parseFloat($(this).find('.clsSumInsured').text());
                _Sum2 += parseFloat($(this).find('.clsPremiumRate').text());
                _Sum3 += parseFloat($(this).find('.clsPremiumAmount').text());

            }
            else {

                if (Prev == CurrLocId) {
                    _Sum1 += parseFloat($(this).find('.clsSumInsured').text());
                    _Sum2 += parseFloat($(this).find('.clsPremiumRate').text());
                    _Sum3 += parseFloat($(this).find('.clsPremiumAmount').text());

                }
                else {

                    var GroupByItem = $(this).prev().find('.ABC').find(GroupBy).val();

                    $("<tr class='success'><th colspan=" + ColSpan + " style='text-align:left'>" + GroupByName + " : " + GroupByItem + "</th><th style='text-align:right'>" + _Sum1 + "</th><th style='text-align:right'>" + _Sum2 + "</th><th style='text-align:right'>" + _Sum3 + "</th></tr>").insertBefore($(this));
                    i = -1;
                    _Sum1 = parseFloat($(this).find('.clsSumInsured').text());
                    _Sum2 = parseFloat($(this).find('.clsPremiumRate').text());
                    _Sum3 = parseFloat($(this).find('.clsPremiumAmount').text());

                }

                if ($(this).is(':last-child')) {
                    var GroupByItem = $(this).find('.ABC').find(GroupBy).val();
                    $("<tr class='success'><th colspan=" + ColSpan + " style='text-align:left'>" + GroupByName + " : " + GroupByItem + "</th><th style='text-align:right'>" + _Sum1 + "</th><th style='text-align:right'>" + _Sum2 + "</th><th style='text-align:right'>" + _Sum3 + "</th></tr>").insertAfter($(this));
                    i = -1;
                    _Sum1 = 0;
                    _Sum2 = 0;
                    _Sum3 = 0;

                }
            }

            Prev = CurrLocId;
            i++;

        });
    }
    else {
        var divTbodyGoalFund = $('.tbodyGLIAboveListing').html('');
        $('#GLIAboveListing').tmpl(res).appendTo(divTbodyGoalFund);

    }
   
    SetReportHeader('Group Life Insurance Above Sixty Five Report', 10, '');
    $('.div_reportbutton').show();
    addSerialNumber();
    ProgressBarHide();
}

function ClearReport() {
    $('.div_reportbutton').hide();
    $('.clsReportH').hide();
    $('.tbodyGLIAboveListing').html('');
    $('.tdTotalSumInsured').text('');
    $('.tdTotalSumPremiumRate').text('');
    $('.tdTotalSumPremiumAmount').text('');
}
