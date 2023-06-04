
function GetProvidentHistory() {
    ProgressBarShow();
    var service = new HrmsSuiteHcmService.HcmService();
    service.getProvidentHistory(EmployeeId, onGetProvidentHistory, null, null);
}
function getzakatcalculation() {

    var CompanyId = $('.ddlCompany').val();
    if (CompanyId > 0) {
        if (!validateForm('.divMonthPayroll'))
            return;

        if ($(".txtzakat").val() == '') {

            showError('Please Select Month.');
        }
        else if ($(".txtzakatempcode").val() == '') {

            showError('Please Enter Employee Code.');
        }
        else {
            var EmployeeCode = $('.txtzakatempcode').val() == '' ? 0 : $('.txtzakatempcode').val();
            var fromDate = $(".txtzakat").val();


            ProgressBarShow();
           
            var service = new HrmsSuiteHcmService.HcmService();
            service.report_Zakat_Calculation(EmployeeCode, CompanyId, fromDate, onreport_zakat_calculation_List, null, null);
        }

    } else {
        showError('Please select Company');
    }
}


function onreport_zakat_calculation_List(result) {

    // var GroupByValue = $('.ddlGroupBy').val();

    var res = JSON.parse(result);

    $(".txtCalculatedZakat").val(res[0].Zakat);
    //if (GroupByValue != 0) {
    //    res = res.sort(sortByProperty(GroupByValue));
    //}
    

    ProgressBarHide();
}
function onGetProvidentHistory(result) {
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.tbodyProvidentHistory').html('');
    $('#ProvidentHistory').tmpl(res).appendTo(divTbodyGoalFund);

    if (res.length == 1)
        $('.btnEditPFOpening').show();
    else
        $('.btnEditPFOpening').hide();

    if (res.length == 0 || res[0].TotalBalance == 0) {
        $('.txtWithdrawAmount').hide();
        $('.btnWithdrawPF').hide();
    }

    else {
        $('.txtWithdrawAmount').show();
        $('.btnWithdrawPF').show();
    }

    $('.btnEditPFOpening').hide();

    ProgressBarHide();
}


function SaveProvidentHistory() {
    PFOpening = $('.txtPFOpening').val();
    var service = new HrmsSuiteHcmService.HcmService();
    service.savePFOpening(EmployeeId, PFOpening, onSaveProvidentHistory, null, null);
}


function onSaveProvidentHistory(result) {
    if (result == "1")
        showSuccess('Opening Updated Sucessfully');

    GetProvidentHistory();
    GetProvidentOpening();
}


function GetProvidentOpening() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getProvidentOpening(EmployeeId, onGetProvidentOpening, null, null);
}

function onGetProvidentOpening(result) {
    var res = jQuery.parseJSON(result);
    try {
        EmployeePFBalance = res[0].TotalBalance;
        $('.txtPFOpening').val(res[0].TotalBalance);
        $('.txtPFOpening').prop('disabled', true);
        $('.btnSavePFOpening').hide();
    }
    catch (e) {
        $('.txtPFOpening').val('');
        $('.txtPFOpening').prop('disabled', false);
        $('.btnSavePFOpening').show();
    }
}


function onEditPFOpening() {
    $('.txtPFOpening').prop('disabled', false);
    $('.btnSavePFOpening').show();
}



////////////////////////////////////WITHDRAW WORK//////////////////////////////////////////////////////////


function SaveWithdraw() { 
    var employeeWithdraw = parseInt($('.txtEmpBalanceWithdraw').val());
    var companyWithdraw = parseInt($('.txtCompBalanceWithdraw').val());
    var profitWithdraw = parseInt($('.txtProfitWithdraw').val());
    var withdrawdate = $('.txtwithdrawdate').val();
    if (withdrawdate == '') {
        showError('Select Withdraw Date');
        return;
    }

    var employeebalance = parseInt($('.tdEmployeeBalance').first().text().trim());
    var companybalance = parseInt($('.tdCompanyBalance').first().text().trim());
    var interestbalance = parseInt($('.tdInterestIncome').first().text().trim());


    var totalbalance = employeebalance + companybalance + interestbalance;
    var totalwithdraw = employeeWithdraw + companyWithdraw + profitWithdraw;
    if (totalwithdraw > 0) {
        if (employeeWithdraw > employeebalance && employeeWithdraw > 0) {
            showError('Employee Withdraw Amount Cannot Be Greater Than Employee Balance');
            return;
        }

        if (companyWithdraw > companybalance && companyWithdraw > 0) {
            showError('Company Withdraw Amount Cannot Be Greater Than Company Balance');
            return;
        }

        if (profitWithdraw > interestbalance && profitWithdraw > 0) {
            showError('Profit Withdraw Amount Cannot Be Greater Than Profit Balance');
            return;
        }


        //if (totalwithdraw > totalbalance) {
        //    showError('Withdrawl not possible!');
        //    return;
        //}

        var service = new HrmsSuiteHcmService.HcmService();
        service.insertWithdraw(EmployeeId, employeeWithdraw, companyWithdraw, profitWithdraw, withdrawdate, onSaveWithdraw, null, null);
    }
    else {
        showError('Please enter the required fields.');
    }
}

function onSaveWithdraw(result) {
    var res = jQuery.parseJSON(result);
    if (res == '1') {
        //resetControls('.ProvidentFund');

        $('.txtTotalBalanceWithdraw').val(0);
        $('.txtProfitWithdraw').val(0);
        $('.txtEmpBalanceWithdraw').val('0');
        $('.txtCompBalanceWithdraw').val('0');
        $('.txtTotalWithdraw').val('0');
        $('.txtwithdrawdate').val('');

        showSuccess('Provident Fund Withdrawl Success');
        GetWithdrawHistory();
        GetProvidentHistory();
    }
}


function GetWithdrawHistory() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getWithdrawHistory(EmployeeId, onGetWithdrawHistory, null, null);
}

function onGetWithdrawHistory(result) {
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.tablewithdrawHistory').html('');
    $('#bindWithdrawHistory').tmpl(res).appendTo(divTbodyGoalFund);
    $('.historyTotal').text(historyAmountTotal());

}



function historyAmountTotal() {
    var val = 0;
    $('.historyAmount').each(function () {
        val = val + parseFloat($(this).text());
    }
    );
    return val;
}

function onChangePFBalance(selector) {
    var Total = $(selector).val() == "" ? 0 : $(selector).val();
    var PfIntrest = $('.txtProfitWithdraw').val();
    var EmpBalance = parseInt(parseFloat(Total) / 2);

    $('.txtEmpBalanceWithdraw').val(EmpBalance);
    $('.txtCompBalanceWithdraw').val(EmpBalance);

    $('.txtTotalWithdraw').val(parseInt(Total) + parseInt(PfIntrest == '' ? 0 : PfIntrest));

}

function onChangePFIntrest(selector) {
    var Total = $('.txtTotalBalanceWithdraw').val();
    var PfIntrest = $(selector).val() == "" ? 0 : $(selector).val();

    $('.txtTotalWithdraw').val(parseInt(Total) + parseInt(PfIntrest == '' ? 0 : PfIntrest));
}

function PfWithdrawDelete(FundWithdrawId) {
    if (confirm("Are you sure you want to delete?") == true) {
        //var FundWithdrawId = $(selector).closest('td').find('.hfFundWithdrawId').val();

        var service = new HrmsSuiteHcmService.HcmService();
        service.DeleteWithdraw(FundWithdrawId, onPfWithdrawDelete, null, null);
    }
}

function onPfWithdrawDelete(result) {

}


function ReversePF(FundWithdrawId) {
    ProgressBarShow();
    var service = new HrmsSuiteHcmService.HcmService();
    service.ReversePF_Fund(FundWithdrawId, onReversePF, null, null);

}

function onReversePF(result) { 
    if (result.ResponseMessageType == 1) {

        showSuccess(result.ResponseMessage);
        GetWithdrawHistory();
        GetProvidentHistory();
    }
    else {
        showError(result.ResponseMessage);
    }

    ProgressBarHide();
   
}
