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

        
        var PayrollMonth = formatDate($('.txtMonthOfPayroll').val());


        ProgressBarShow();
        ClearReport();
        var service = new HrmsSuiteHcmService.HcmService();
        service.CPPL_Count(PayrollMonth, CompanyId,
            onCPPL_COUTN_REPORT, null, null);
    } else {
        showError('Please select Company');
    }
}

function onCPPL_COUTN_REPORT(result) {
   
    var res = JSON.parse(result);

    $('#trCompanyTitle').html('<h2>'+$('.ddlCompany option:selected').text()+'</h2>');
    $('#trHeadCountTitleWithDate').html('<h3 style="text-decoration: underline;">Head Count Report – '+$('.txtMonthOfPayroll ').val()+'</h3>');
    $('#trHeadOfficTitle').html('<h4>HUMAN RESOURCE DEPARTMENT - HEAD OFFICE, KARACHI</h4></br>');
    $('#MonthOf').html($('.txtMonthOfPayroll').val().toString().split('-')[0]);

    var divTbodyGoalFund = $('.tbodyReportFirstListing').html('');
    $('#ReportFirstListing').tmpl(res.Table).appendTo(divTbodyGoalFund);

    var divTbodySecondReport = $('.tbodyReportSecondListing').html('');
    $('#ReportSecondListing').tmpl(res.Table1).appendTo(divTbodySecondReport);


    var divTbodyThirdReport = $('.tbodyReportThirdListing').html('');
    $('#ReportThirdListing').tmpl(res.Table2).appendTo(divTbodyThirdReport);


    var divTbodyFourthReport = $('.tbodyReportFourthListing').html('');
    $('#ReportFourthListing').tmpl(res.Table3).appendTo(divTbodyFourthReport);

    
    $('.div_reportbutton').show();
    ProgressBarHide();
}

function ClearReport() {
    $('.div_reportbutton').hide();
    $('.clsReportH').hide();
    $('#trCompanyTitle').html('');
    $('#trHeadCountTitleWithDate').html('');
    $('#trHeadOfficTitle').html('');
    $('.tbodyReportFirstListing').html('');
    $('.tbodyReportSecondListing').html('');
    $('.tbodyReportFourthListing').html('');
    $('.tbodyReportThirdListing').html('');
    $('#MonthOf').html('');
    

}
