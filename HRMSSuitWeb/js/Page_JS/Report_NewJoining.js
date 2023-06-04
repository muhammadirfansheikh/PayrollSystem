function TriggerPageLoads() {
    $(".btnCancelSearch").click(function () {
        $('.txtMonthOfPayroll').datepicker('setDate', null);
        $('.ddlGroupBy').val('0');
        ClearReport();
    });

    //BindBank();
    BindGroupByDDL();
    $('.ddlGroupBy option').filter('[value="clsCompany"],[value="clsDesignation"]').remove();
}

function GetEmployee() {
    var CompanyId = $('.ddlCompany').val();
    if (CompanyId > 0) {
        if (!validateForm('.divFromDate'))
            return;

        if (!validateForm('.divToDate'))
            return;

        var EmployeeCode = $('.txtEmployeeCode').val() == '' ? 0 : $('.txtEmployeeCode').val();
       
        //var LocationId = $('.ddlLocation').val();
        //var DepartmentId = $('.ddlDepartment').val();
        //var CostCenterId = $('.ddlCostCenter').val();
        //var SapCostCenterId = $('.ddlSapCostCenter').val();
        var fromDate = $('.txtFromDate').val();
        var toDate = $('.txtToDate').val();

        ProgressBarShow();
        ClearReport();
        var service = new HrmsSuiteHcmService.HcmService();
        service.report_NewJoining(EmployeeCode, CompanyId,
            fromDate, toDate, onGetReportResignedStaff, null, null);
    } else {
        showError('Please select Company');
    }
}

function onGetReportResignedStaff(result) {
    
    var GroupByValue = $('.ddlGroupBy').val();

    var res = JSON.parse(result);

    if (GroupByValue != 0) {
        res = res.sort(sortByProperty(GroupByValue));
    }
    var divTbodyGoalFund = $('.tbodyNewJoiningListing').html('');
    $('#NewJoiningListing').tmpl(res).appendTo(divTbodyGoalFund);

    var TotalGrossSalary = 0;
    var TotalOtherBenefits = 0;


    $('.clsGrossSalary').each(function () {
        TotalGrossSalary += parseFloat($(this).text().trim());
    });

    $('.clsOtherBenefits').each(function () {
        TotalOtherBenefits += parseFloat($(this).text().trim());
    });



    $('.tdTotalGrossSalary').text(TotalGrossSalary);
    $('.tdTotalBenefits').text(TotalOtherBenefits);

    var Prev = 0;
    var i = 0;
    var _Sum1 = 0, _Sum2 = 0;
    var GroupBy = '.' + $('.ddlGroupBy').val();

    var ColSpan = $('.clsGrossSalary').index() - 0;

    var GroupByName = $(".ddlGroupBy option:selected").text();

    var GroupByValue = $('.ddlGroupBy').val();

    if (GroupByValue != 0) {

        $('.trList').each(function () {


            var CurrLocId = $(this).find('.ABC').find(GroupBy).val();

            if ($(this).is(':first-child')) {
                _Sum1 += parseFloat($(this).find('.clsGrossSalary').text());
                _Sum2 += parseFloat($(this).find('.clsOtherBenefits').text());

            }
            else {

                if (Prev == CurrLocId) {
                    _Sum1 += parseFloat($(this).find('.clsGrossSalary').text());
                    _Sum2 += parseFloat($(this).find('.clsOtherBenefits').text());

                }
                else {

                    var GroupByItem = $(this).prev().find('.ABC').find(GroupBy).val();

                    $("<tr class='success'><th colspan=" + ColSpan + " style='text-align:left'>" + GroupByName + " : " + GroupByItem + "</th><th style='text-align:right'>" + _Sum1 + "</th><th style='text-align:right'>" + _Sum2 + "</th></tr>").insertBefore($(this));
                    i = -1;
                    _Sum1 = parseFloat($(this).find('.clsGrossSalary').text());
                    _Sum2 = parseFloat($(this).find('.clsOtherBenefits').text());

                }

                if ($(this).is(':last-child')) {
                    var GroupByItem = $(this).find('.ABC').find(GroupBy).val();
                    $("<tr class='success'><th colspan=" + ColSpan + " style='text-align:left'>" + GroupByName + " : " + GroupByItem + "</th><th style='text-align:right'>" + _Sum1 + "</th><th style='text-align:right'>" + _Sum2 + "</th></tr>").insertAfter($(this));
                    i = -1;
                    _Sum1 = 0;
                    _Sum2 = 0;

                }
            }

            Prev = CurrLocId;
            i++;

        });

    }
    else {
        var divTbodyGoalFund = $('.tbodyNewJoiningListing').html('');
        $('#NewJoiningListing').tmpl(res).appendTo(divTbodyGoalFund);

    }
    

    SetReportHeader('New Joining Report', 10, '');
    $('.div_reportbutton').show();

    addSerialNumber();
    $('.clsDateH').html('From Date : ' + $('.txtFromDate').val() + ' To Date : ' + $('.txtToDate').val());
    ProgressBarHide();
}



function ClearReport() {
    $('.div_reportbutton').hide();
    $('.clsReportH').hide();
    $('.tbodyNewJoiningListing').html('');
    $('.tdTotalGrossSalary').text('');
    $('.tdTotalBenefits').text('');
}

function onGetReportIncomeTax1(result) {
    var GroupByValue = $('.ddlGroupBy').val();

    var res = jQuery.parseJSON(result);

    if (GroupByValue != 0) {
        res = res.sort(sortByProperty(GroupByValue));
    }
    var divTbodyGoalFund = $('.tbodyNewJoiningListing').html('');
    $('#NewJoiningListing').tmpl(res).appendTo(divTbodyGoalFund);

    var TotalGrossSalaryAmount = 0;
    var TotalOthersBenefit = 0;


    $('.clsGrossSalary').each(function () {
        TotalGrossSalaryAmount += parseFloat($(this).text().trim());
    });

    $('.clsOtherBenefits').each(function () {
        TotalOthersBenefit += parseFloat($(this).text().trim());
    });



    $('.tdTotalGrossSalary').text(TotalGrossSalaryAmount);
    $('.tdTotalBenefits').text(TotalOthersBenefit);


    var Prev = 0;
    var i = 0;
    var _Sum1 = 0, _Sum2 = 0;
    var GroupBy = '.' + $('.ddlGroupBy').val();

    var ColSpan = $('.clsGrossSalary').index() - 1;

    var GroupByName = $(".ddlGroupBy option:selected").text();

    $('.trList').each(function () {

        var GroupByValue = $('.ddlGroupBy').val();

        if (GroupByValue != 0) {

            var CurrLocId = $(this).find('.ABC').find(GroupBy).val();

            if ($(this).is(':first-child')) {
                _Sum1 += parseFloat($(this).find('.clsGrossSalary').text());
                _Sum2 += parseFloat($(this).find('.clsOtherBenefits').text());

            }
            else {

                if (Prev == CurrLocId) {
                    _Sum1 += parseFloat($(this).find('.clsGrossSalary').text());
                    _Sum2 += parseFloat($(this).find('.clsOtherBenefits').text());
                }
                else {

                    var GroupByItem = $(this).prev().find('.ABC').find(GroupBy).val();

                    $("<tr class='success'><th colspan=" + ColSpan + ">" + GroupByName + " : " + GroupByItem + "</th><th>" + _Sum1 + "</th><th>" + _Sum2 + "</th><th></th></tr>").insertBefore($(this));
                    i = -1;
                    _Sum1 = parseFloat($(this).find('.clsGrossSalary').text());
                    _Sum2 = parseFloat($(this).find('.clsOtherBenefits').text());

                }

                if ($(this).is(':last-child')) {
                    var GroupByItem = $(this).find('.ABC').find(GroupBy).val();
                    $("<tr class='success'><th colspan=" + ColSpan + ">" + GroupByName + " : " + GroupByItem + "</th><th>" + _Sum1 + "</th><th>" + _Sum2 + "</th><th></th></tr>").insertAfter($(this));
                    i = -1;
                    _Sum1 = 0;
                    _Sum2 = 0;

                }
            }

            Prev = CurrLocId;
            i++;
        }
        else {
            var divTbodyGoalFund = $('.tbodyNewJoiningListing').html('');
            $('#NewJoiningListing').tmpl(res).appendTo(divTbodyGoalFund);

        }
    });

    var Para2 = '';
    var BranchId = $('.ddlBranch').val();
    if (BranchId != 0) {

        Para2 = $('.ddlBank option:selected').text() + ' - ' + $('.ddlBranch option:selected').text();
    }

    SetReportHeader('New Joining Report', 9, Para2);
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