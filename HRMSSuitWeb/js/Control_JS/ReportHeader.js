
$('.clsReportH').hide();



function SetReportHeader(ReportName, ColSpan, Para2) {
    var CompanyId = $('.ddlCompany').val();
    if (CompanyId != 0) {
        $('.clsColSpan').attr('colspan', ColSpan);
        $('.clsReportH').show();
        $('.clsCompanyH').text($('.ddlCompany option:selected').text());
        $('.clsReportNameH').text(ReportName);
        $('.clsDateH').text('Date : ' + formatDate($('.txtMonthOfPayroll').val()));
        $('.clsPara2H').text(Para2);
        GetCurrentDate('.clsPrintDate', 'Print Date : ');
        if (Para2 == '') {
            $('.clsPara2H').hide();
        } else {
            $('.clsPara2H').show();
        }
    }
    else {
        $('.clsReportH').hide();
    }
}