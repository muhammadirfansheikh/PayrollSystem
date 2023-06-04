function TriggerPageLoads() {
    $(".btnCancelSearch").click(function () {
        $('.txtMonthOfPayroll').datepicker('setDate', null);

        ClearReport();
    });


}

function GetEmployee() {

    var CompanyId = $('.ddlCompany').val();
    var EmployeeCode = $('.txtEmployeeCode').val() == "" ? null : $('.txtEmployeeCode').val();
    var FromDate = $('.txtFromDate').val() == "" ? null : formatDate($('.txtFromDate').val());
    var ToDate = $('.txtToDate').val() == "" ? null : formatDate($('.txtToDate').val());

    if (CompanyId > 0) {

        if (FromDate != null) {
            if (ToDate != null) {

                if (ToDate >= FromDate) {
                    if (!validateForm('.divMonthPayroll'))
                        return;
                    ProgressBarShow();
                    ClearReport();
                    var service = new HrmsSuiteHcmService.HcmService();
                    service.Report_Gratuity(EmployeeCode, CompanyId, FromDate, ToDate, 1, onReportGratuity, null, null);
                }
                else {
                    showError('To Date Must Be Greater Than From Date.');
                }

            }
            else {
                showError('Please select To Date');
            }

        }
        else {
            showError('Please select From Date');
        }

    } else {
        showError('Please select Company');
    }
}

function onReportGratuity(result) {

    /*ReqGrp var GroupByValue = $('.ddlGroupBy').val();*/

    var res = JSON.parse(result);


    var divTbodyGoalFund = $('.tbodyGPListing').html('');
    $('#GPListing').tmpl(res).appendTo(divTbodyGoalFund);

    var TotalBasicSalary = 0;
    var TotalYear = 0;
    var TotalMonth = 0;
    var TotalDay = 0;
    var TotalPerAnum = 0;
    var TotalTotalGratuity = 0;
    


    $('.clsBasicSalary').each(function () {
        TotalBasicSalary += parseFloat($(this).text().trim());
    });

    $('.clsYear').each(function () {
        TotalYear += parseFloat($(this).text().trim());
    });

    $('.clsMonth').each(function () {
        TotalMonth += parseFloat($(this).text().trim());
    });

    $('.clsDay').each(function () {
        TotalDay += parseFloat($(this).text().trim());
    });

    $('.clsPerAnum').each(function () {
        TotalPerAnum += parseFloat($(this).text().trim());
    });

    $('.clsTotalGratuity').each(function () {
        TotalTotalGratuity += parseFloat($(this).text().trim());
    });

   



    $('.tdBasicSalary').text(TotalBasicSalary);
    $('.tdYear').text(TotalYear);
    $('.tdMonth').text(TotalMonth);
    $('.tdDay').text(TotalDay);
    $('.tdPerAnum').text(TotalPerAnum);
    $('.tdTotalGratuity').text(TotalTotalGratuity);
    

    var Para2 = '';
    var _Counter = 0;
    $('.clsSNo').each(function (_Counter) {
        _Counter = _Counter + 1;
        $(this).text(_Counter);
    });
    SetReportHeader('Gratuity Payment Report', 10, Para2);
    $('.clsDateH').html('From Date : ' + $('.txtFromDate').val() + ' To Date : ' + $('.txtFromDate').val());
    $('.div_reportbutton').show();
    ProgressBarHide();
}

function ClearReport() {
    $('.div_reportbutton').hide();
    $('.clsReportH').hide();
    $('.tbodyGPListing').html('');
    $('.tdBasicSalary').text('');
    $('.tdYear').text('');
    $('.tdMonth').text('');
    $('.tdDay').text('');
    $('.tdPerAnum').text('');
    $('.tdTotalGratuity').text('');


}
