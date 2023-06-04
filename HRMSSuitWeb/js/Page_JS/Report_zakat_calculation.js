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
            service.report_Zakat_Calculation(EmployeeCode, CompanyId, fromDate, onreport_zakat_calculation_List, null, null);
        }

    } else {
        showError('Please select Company');
    }
}


function onreport_zakat_calculation_List(result) {

    // var GroupByValue = $('.ddlGroupBy').val();

    var res = JSON.parse(result);

    //if (GroupByValue != 0) {
    //    res = res.sort(sortByProperty(GroupByValue));
    //}
    var divTbodyGoalFund = $('.tbodyzakatcalculationListing').html('');
    $('#zakatcalculationListing').tmpl(res).appendTo(divTbodyGoalFund);

    var EmployeeBalance = 0;
    var CompanyBalance = 0;
    var InterestIncome = 0;
    var Total = 0;
    var Zakat = 0;
   
    $('.clsEmployeeBalance').each(function () {
        EmployeeBalance += parseFloat($(this).text().trim());
    });
    $('.clsCompanyBalance').each(function () {
        CompanyBalance += parseFloat($(this).text().trim());
    });
    $('.clsInterestIncome').each(function () {
        InterestIncome += parseFloat($(this).text().trim());
    });
    $('.clsTotal').each(function () {
        Total += parseFloat($(this).text().trim());
    });
    $('.clsZakat').each(function () {
        Zakat += parseFloat($(this).text().trim());
    });
    


    $('.tdEmployeeBalance').text(EmployeeBalance);
    $('.tdCompanyBalance').text(CompanyBalance);
    $('.tdInterestIncome').text(InterestIncome);
    $('.tdTotal').text(Total);
    $('.tdZakat').text(Zakat);
    
    var Para2 = '';


    SetReportHeader('Zakat Calculation Report', 10, Para2);
    $('.div_reportbutton').show();
    addSerialNumber();
    ProgressBarHide();
}

function ClearReport() {
    $('.div_reportbutton').hide();
    $('.clsReportH').hide();
    $('.tbodyzakatcalculationListing').html('');
    $('.tdEmployeeBalance').text('');
    $('.tdCompanyBalance').text('');
    $('.tdInterestIncome').text('');
    $('.tdTotal').text('');
    $('.tdZakat').text('');
}

