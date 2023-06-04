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

        var EmployeeCode = $('.txtEmployeeCode').val() == '' ? null : $('.txtEmployeeCode').val();
        var PayrollMonth = formatDate($('.txtMonthOfPayroll').val());


        ProgressBarShow();
        ClearReport();
        var service = new HrmsSuiteHcmService.HcmService();
        service.report_Tax_Return(PayrollMonth,CompanyId,EmployeeCode,
            onreport_Tax_Return, null, null);
    } else {
        showError('Please select Company');
    }
}

function onreport_Tax_Return(result) {

    var res = JSON.parse(result);

   
    var divTbodyGoalFund = $('.tbodyTaxReturnListing').html('');
    $('#TaxReturnListing').tmpl(res).appendTo(divTbodyGoalFund);

    var TotalChargedSalary = 0;
    var TotalTaxDeposit = 0;
    


    $('.clsChargedSalary').each(function () {
        TotalChargedSalary += parseFloat($(this).text().trim());
    });

    $('.clsTaxDeposit').each(function () {
        TotalTaxDeposit += parseFloat($(this).text().trim());
    });


    $('.tdChargedSalary').text(TotalChargedSalary);
    $('.tdTaxDeposit').text(TotalTaxDeposit);

    SetReportHeader('Tax Return', 10, '');
    $('.div_reportbutton').show();
    addSerialNumber();
    ProgressBarHide();
}

function ClearReport() {
    $('.div_reportbutton').hide();
    $('.clsReportH').hide();
    $('.tbodyOverTimeListing').html('');
    $('.tdChargedSalary').text('');
    $('.tdTaxDeposit').text('');

}
