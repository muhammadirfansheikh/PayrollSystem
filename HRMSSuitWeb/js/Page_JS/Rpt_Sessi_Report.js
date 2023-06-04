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
            service.Rpt_report_Sessi(EmployeeCode, CompanyId, fromDate, onreport_Emp_List, null, null);
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
    var divTbodyGoalFund = $('.tbodySessiReportListing').html('');
    $('#SessiReportListing').tmpl(res).appendTo(divTbodyGoalFund);

    var Gross = 0;
    var WorkedDays = 0;
    var MonthGross = 0;


    $('.clsGross').each(function () {
        Gross += parseFloat($(this).text().trim());
    });
    $('.clsWorkedDays').each(function () {
        WorkedDays += parseFloat($(this).text().trim());
    });
    $('.clsMonthGross').each(function () {
        MonthGross += parseFloat($(this).text().trim());
    });

    $('.tdGross').text(Gross);
    $('.tdWorkedDays').text(WorkedDays);
    $('.tdMonthGross').text(MonthGross);

    var Para2 = '';


    SetReportHeader('Sessi Report', 10, Para2);
    $('.div_reportbutton').show();
    addSerialNumber();
    ProgressBarHide();
}

function ClearReport() {
    $('.div_reportbutton').hide();
    $('.clsReportH').hide();
    $('.tbodyCarDetailListListing').html('');
    $('.tdGross').text('');
    $('.tdWorkedDays').text('');
    $('.tdMonthGross').text('');

}

