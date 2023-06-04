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
        var Year = formatDate($('.txtMonthOfPayroll').val());


        ProgressBarShow();
        ClearReport();
        var service = new HrmsSuiteHcmService.HcmService();
        service.report_Contribution_Sheet(EmployeeCode, CompanyId,
            Year, onreport_Contribution_Sheet, null, null);
    } else {
        showError('Please select Company');
    }
}

function onreport_Contribution_Sheet(result) {
  
   /* var GroupByValue = $('.ddlGroupBy').val();*/

    var res = JSON.parse(result);

    //if (GroupByValue != 0) {
    //    res = res.sort(sortByProperty(GroupByValue));
    //}

    var divTbodyGoalFund = $('.tbodyContributionSheetListing').html('');
    $('#ContributionSheetListing').tmpl(res).appendTo(divTbodyGoalFund);

    var TotalOpeningBlnc = 0;
    var TotalJanuary = 0;
    var TotalFebruary = 0;
    var TotalMarch = 0;
    var TotalApril = 0;
    var TotalMay = 0;
    var TotalJune = 0;
    var TotalJuly = 0;
    var TotalAugust = 0;
    var TotalSeptember = 0;
    var TotalOctober = 0;
    var TotalNovember = 0;
    var TotalDecember = 0;
    var TotalArrear = 0;
    var TotalWithdrawal = 0;
    var TotalClosingBalance = 0;
   
    var TotalCommulIncome = 0;
  


    $('.clsOpeningBalance').each(function () {
        TotalOpeningBlnc  += parseFloat($(this).text().trim());
    });

    $('.clsJanuary').each(function () {
        TotalJanuary  += parseFloat($(this).text().trim());
    });


    $('.clsFebruary').each(function () {
        TotalFebruary  += parseFloat($(this).text().trim());
    });

    $('.clsMarch').each(function () {
        TotalMarch  += parseFloat($(this).text().trim());
    });


    $('.clsApril').each(function () {
        TotalApril  += parseFloat($(this).text().trim());
    });

    $('.clsMay').each(function () {
        TotalMay  += parseFloat($(this).text().trim());
    });

    $('.clsJune').each(function () {
        TotalJune  += parseFloat($(this).text().trim());
    });

    $('.clsJuly').each(function () {
        TotalJuly  += parseFloat($(this).text().trim());
    });

    $('.clsAugust').each(function () {
        TotalAugust  += parseFloat($(this).text().trim());
    });

    $('.clsSeptember').each(function () {
        TotalSeptember  += parseFloat($(this).text().trim());
    });

    $('.clsOctober').each(function () {
        TotalOctober  += parseFloat($(this).text().trim());
    });

    $('.clsNovember').each(function () {
        TotalNovember  += parseFloat($(this).text().trim());
    });

    $('.clsDecember').each(function () {
        TotalDecember  += parseFloat($(this).text().trim());
    });

    $('.clsArrear').each(function () {
        TotalArrear  += parseFloat($(this).text().trim());
    });

    $('.clsWithdrawal').each(function () {
        TotalWithdrawal  += parseFloat($(this).text().trim());
    });

    $('.clsClosingBalance').each(function () {
        TotalClosingBalance  += parseFloat($(this).text().trim());
    });
   
    $('.clsCommulIncome').each(function () {
        TotalCommulIncome  += parseFloat($(this).text().trim());
    });
   





    $('.tdTotalOpeningBalance').text(TotalOpeningBlnc);
    $('.tdTotalJanuary').text(TotalJanuary);
    $('.tdTotalFebruary').text(TotalFebruary);
    $('.tdTotalMarch').text(TotalMarch);
    $('.tdTotalApril').text(TotalApril);
    $('.tdTotalMay').text(TotalMay);
    $('.tdTotalJune').text(TotalJune);
    $('.tdTotalJuly').text(TotalJuly);
    $('.tdTotalAugust').text(TotalAugust);
    $('.tdTotalSeptember').text(TotalSeptember);
    $('.tdTotalOctober').text(TotalOctober);
    $('.tdTotalNovember').text(TotalNovember);
    $('.tdTotalDecember').text(TotalDecember);
    $('.tdTotalArrear').text(TotalArrear);
    $('.tdTotalWithdrawal').text(TotalWithdrawal);
    $('.tdTotalClosingBalance').text(TotalClosingBalance);
    
    $('.tdTotalCommulIncome').text(TotalCommulIncome);
   

   

  

    //var Para2 = '';
    //var BranchId = $('.ddlBranch').val();
    //if (BranchId != 0) {

    //    Para2 = "";
    //}

    SetReportHeader('Over Time Report', 10, '');


    $('.div_reportbutton').show();
    addSerialNumber();
    ProgressBarHide();
}

function ClearReport() {
    $('.div_reportbutton').hide();
    $('.clsReportH').hide();
    $('.tbodyContributionSheetListing').html('');


    $('.tdTotalOpeningBalance').text('');
    $('.tdTotalJanuary').text('');
    $('.tdTotalFebruary').text('');
    $('.tdTotalMarch').text('');
    $('.tdTotalApril').text('');
    $('.tdTotalMay').text('');
    $('.tdTotalJune').text('');
    $('.tdTotalJuly').text('');
    $('.tdTotalAugust').text('');
    $('.tdTotalSeptember').text('');
    $('.tdTotalOctober').text('');
    $('.tdTotalNovember').text('');
    $('.tdTotalDecember').text('');
    $('.tdTotalArrear').text('');
    $('.tdTotalWithdrawal').text('');
    $('.tdTotalClosingBalance').text('');
  
    $('.tdTotalCommulIncome').text('');
}

