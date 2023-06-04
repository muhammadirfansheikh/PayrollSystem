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
            service.Rpt_report_Pessi(EmployeeCode, CompanyId, fromDate, onreport_Emp_List, null, null);
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
    var divTbodyGoalFund = $('.tbodyPessiReportListing').html('');
    $('#PessiReportListing').tmpl(res).appendTo(divTbodyGoalFund);

    var Days = 0;
    var Hours = 0;
    var MonthWages = 0;
    var WagesPaid = 0;
    var PessiCont = 0;



    $('.clsDays').each(function () {
        Days += parseFloat($(this).text().trim());
    });
    $('.clsHours').each(function () {
        Hours += parseFloat($(this).text().trim());
    });
    $('.clsMonthWages').each(function () {
        MonthWages += parseFloat($(this).text().trim());
    });
    $('.clsWagesPaid').each(function () {
        WagesPaid += parseFloat($(this).text().trim());
    });
    $('.clsPESSICont').each(function () {
        PessiCont += parseFloat($(this).text().trim());
    });

    $('.tdDays').text(Days);
    $('.tdHours').text(Hours);
    $('.tdMonthWedges').text(MonthWages);
    $('.tdWagesPaid').text(WagesPaid);
    $('.tdPessiCont').text(PessiCont);


      
    var Para2 = '';


    SetReportHeader('Pessi Report', 10, Para2);
    $('.div_reportbutton').show();
    addSerialNumber();
    ProgressBarHide();
}

function ClearReport() {
    $('.div_reportbutton').hide();
    $('.clsReportH').hide();
    $('.tbodyCarDetailListListing').html('');

    $('.tdDays').text('');
    $('.tdHours').text('');
    $('.tdMonthWedges').text('');
    $('.tdWagesPaid').text('');
    $('.tdPessiCont').text('');

}

