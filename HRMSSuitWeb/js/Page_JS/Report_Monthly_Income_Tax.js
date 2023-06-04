function TriggerPageLoads() {
    $(".btnCancelSearch").click(function () {
        ClearReport();
    });


}

function GetEmployee() {

    var CompanyId = $('.ddlCompany').val();
    if (CompanyId > 0) {
        if (!validateForm('.divMonthPayroll'))
            return;

        if ($(".txtFromDate").val() == '') {

            showError('Please Select Month.');
        }
        else {
            var EmployeeCode = $('.txtEmployeeCode').val() == '' ? 0 : $('.txtEmployeeCode').val();
            var fromDate = $(".txtFromDate").val();


            ProgressBarShow();
            ClearReport();
            var service = new HrmsSuiteHcmService.HcmService();
            service.report_Monthly_Income_Tax_Report(EmployeeCode, CompanyId, fromDate, onreport_Emp_List, null, null);
        }

    } else {
        showError('Please select Company');
    }
}


function onreport_Emp_List(result) {

    // var GroupByValue = $('.ddlGroupBy').val();

    var res = JSON.parse(result);

    //if (GroupByValue != 0) {
    //    res = res.sort(sortByProperty(GroupByValue));
    //}
    var divTbodyGoalFund = $('.tbodyMonthlyIncomeTaxListing').html('');
    $('#MonthlyIncomeTaxListing').tmpl(res).appendTo(divTbodyGoalFund);

    var TaxGross = 0;
    var TaxMonth = 0;
   

    $('.clsTaxGross').each(function () {
        TaxGross += parseFloat($(this).text().trim());
    });
    $('.clsTaxMonth').each(function () {
        TaxMonth += parseFloat($(this).text().trim());
    });
   

    $('.tdTaxGross').text(TaxGross);
    $('.tdTaxMonth').text(TaxMonth);
   
    var Para2 = '';


    SetReportHeader('Monthly Income Tax Report', 10, Para2);
    $('.div_reportbutton').show();
    //addSerialNumber();
    ProgressBarHide();
}

function ClearReport() {
    $('.div_reportbutton').hide();
    $('.clsReportH').hide();
    $('.tbodyCarDetailListListing').html('');
    $('.tdTaxGross').text('');
    $('.tdTaxMonth').text('');
   
}

