function TriggerPageLoads() {
    $(".btnCancelSearch").click(function () {
        ClearReport();
    });


    BindGroupByDDL();
    $('.ddlGroupBy option').filter('[value="clsCompany"],[value="clsDesignation"],[value="clsDepartment"]').remove();
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
            service.report_Provident_Fund_Resigned_Employee(EmployeeCode, CompanyId, fromDate, onreport_Provident_Fund_Resigned_Employee, null, null);
        }

    } else {
        showError('Please select Company');
    }
}


function onreport_Provident_Fund_Resigned_Employee(result) {

    // var GroupByValue = $('.ddlGroupBy').val();

    var res = JSON.parse(result);

    //if (GroupByValue != 0) {
    //    res = res.sort(sortByProperty(GroupByValue));
    //}
    var divTbodyGoalFund = $('.tbodyProvidentFundResignedListListing').html('');
    $('#ProvidentFundResignedListListing').tmpl(res).appendTo(divTbodyGoalFund);

    var PFOpening = 0;
    var MonthlyContribution = 0;
    var PFArrear = 0;
    var PFClosing = 0;
    var CurrentPFInterest = 0;
    var CummulativePFInterest = 0;
    var PFLoanBalance = 0;
    var PFLoanInterest = 0;
    var CompanyLoanBalance = 0;
    var CompanyLoanInterest = 0;
    var OtherLoanBalance = 0;
    var OtherLoanInterest = 0;
 

    $('.clsPFOpening').each(function () {
        PFOpening += parseFloat($(this).text().trim());
    });


    $('.clsMonthlyContribution').each(function () {
        MonthlyContribution += parseFloat($(this).text().trim());
    });

    $('.clsPFArrear').each(function () {
        PFArrear += parseFloat($(this).text().trim());
    });

    $('.clsPFClosing').each(function () {
        PFClosing += parseFloat($(this).text().trim());
    });

    $('.clsCurrentPFInterest').each(function () {
        CurrentPFInterest += parseFloat($(this).text().trim());
    });

    $('.clsCummulativePFInterest').each(function () {
        CummulativePFInterest += parseFloat($(this).text().trim());
    });

    $('.clsPFLoanBalance').each(function () {
        PFLoanBalance += parseFloat($(this).text().trim());
    });

    $('.clsPFLoanInterest').each(function () {
        PFLoanInterest += parseFloat($(this).text().trim());
    });

    $('.clsCompanyLoanBalance').each(function () {
        CompanyLoanBalance += parseFloat($(this).text().trim());
    });

    $('.clsCompanyLoanInterest').each(function () {
        CompanyLoanInterest += parseFloat($(this).text().trim());
    });

    $('.clsOtherLoanBalance').each(function () {
        OtherLoanBalance += parseFloat($(this).text().trim());
    });

    $('.clsOtherLoanInterest').each(function () {
        OtherLoanInterest += parseFloat($(this).text().trim());
    });

   



    $('.tdPFOpening').text(PFOpening);
    $('.tdMonthlyContribution').text(MonthlyContribution);
    $('.tdPFArrear').text(PFArrear);
    $('.tdPFClosing').text(PFClosing);
    $('.tdCurrentPFInterest').text(CurrentPFInterest);
    $('.tdCummulativePFInterest').text(CummulativePFInterest);
    $('.tdPFLoanBalance').text(PFLoanBalance);
    $('.tdPFLoanInterest').text(PFLoanInterest);
    $('.tdCompanyLoanBalance').text(CompanyLoanBalance);
    $('.tdCompanyLoanInterest').text(CompanyLoanInterest);
    $('.tdOtherLoanBalance').text(OtherLoanBalance);
    $('.tdOtherLoanInterest').text(OtherLoanInterest);
    


    var Para2 = '';


    SetReportHeader('Provident Fund Resigned Employee List Report', 10, Para2);
    $('.div_reportbutton').show();
    addSerialNumber();
    ProgressBarHide();
}

function ClearReport() {
    $('.div_reportbutton').hide();
    $('.clsReportH').hide();
    $('.tbodyProvidentFundResignedListListing').html('');
    $('.tdPFOpening').text('');
    $('.tdMonthlyContribution').text('');
    $('.tdPFArrear').text('');
    $('.tdPFClosing').text('');
    $('.tdCurrentPFInterest').text('');
    $('.tdCummulativePFInterest').text('');
    $('.tdPFLoanBalance').text('');
    $('.tdPFLoanInterest').text('');
    $('.tdCompanyLoanBalance').text('');
    $('.tdCompanyLoanInterest').text('');
    $('.tdOtherLoanBalance').text('');
    $('.tdOtherLoanInterest').text('');
}

