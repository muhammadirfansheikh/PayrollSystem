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
                    service.Report_Quarterly(EmployeeCode, CompanyId, FromDate, ToDate, onReportQuarterly, null, null);
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

function onReportQuarterly(result) {

    /*ReqGrp var GroupByValue = $('.ddlGroupBy').val();*/

    var res = JSON.parse(result);


    var divTbodyGoalFund = $('.tbodyQuarterlyListing').html('');
    $('#QuarterlyListing').tmpl(res).appendTo(divTbodyGoalFund);

    var TotalBasic = 0;
    var TotalHouseRent = 0;
    var TotalBonus = 0;
    var TotalPF = 0;
    var Totalxlprcarp = 0;
    var TotalUtilityAllowance = 0;
    var TotalDis_LocationAllowance = 0;
    var TotalCarAllowance = 0;
    var TotalCellAllowance = 0;
    var TotalIncentive = 0;
    var TotalCOLA = 0;
    var TotalSpecialAllowance = 0;
    var TotalMedicalAllowance = 0;



    $('.clsBasic').each(function () {
        TotalBasic += parseFloat($(this).text().trim());
    });

    $('.clsHouseRent').each(function () {
        TotalHouseRent += parseFloat($(this).text().trim());
    });

    $('.clsBonus').each(function () {
        TotalBonus += parseFloat($(this).text().trim());
    });

    $('.clsPF').each(function () {
        TotalPF += parseFloat($(this).text().trim());
    });

    $('.clsxlprcarp').each(function () {
        Totalxlprcarp += parseFloat($(this).text().trim());
    });

    $('.clsfontUtilityAllowance').each(function () {
        TotalUtilityAllowance += parseFloat($(this).text().trim());
    });

    $('.clsfontDis_LocationAllowance').each(function () {
        TotalDis_LocationAllowance += parseFloat($(this).text().trim());
    });

    $('.clsCarAllowance').each(function () {
        TotalCarAllowance += parseFloat($(this).text().trim());
    });

    $('.clsCellAllowance').each(function () {
        TotalCellAllowance += parseFloat($(this).text().trim());
    });

    $('.clsIncentive').each(function () {
        TotalIncentive += parseFloat($(this).text().trim());
    });
    $('.clsCOLA').each(function () {
        TotalCOLA += parseFloat($(this).text().trim());
    });
    $('.clsSpecialAllowance').each(function () {
        TotalSpecialAllowance += parseFloat($(this).text().trim());
    });
    $('.clsMedicalAllowance').each(function () {
        TotalMedicalAllowance += parseFloat($(this).text().trim());
    });
    




    $('.tdBasic').text(TotalBasic);
    $('.tdHouseRent').text(TotalHouseRent);
    $('.tdBonus').text(TotalBonus);
    $('.tdPF').text(TotalPF);
    $('.tdxlprcarp').text(Totalxlprcarp);
    $('.tdUtilityAllowance').text(TotalUtilityAllowance);
    $('.tdDis_LocationAllowance').text(TotalDis_LocationAllowance);
    $('.tdCarAllowance').text(TotalCarAllowance);
    $('.tdCellAllowance').text(TotalCellAllowance);
    $('.tdIncentive').text(TotalIncentive);
    $('.tdCOLA').text(TotalCOLA);
    $('.tdSpecialAllowance').text(TotalSpecialAllowance);
    $('.tdMedicalAllowance').text(TotalMedicalAllowance);
    


    var Para2 = '';
    var _Counter = 0;
    $('.clsSNo').each(function (_Counter) {
        _Counter = _Counter + 1;
        $(this).text(_Counter);
    });
    SetReportHeader('Quarterly Report', 10, Para2);
    $('.clsDateH').html('From Date : ' + $('.txtFromDate').val() + ' To Date : ' + $('.txtFromDate').val());
    $('.div_reportbutton').show();
    ProgressBarHide();
}

function ClearReport() {
    $('.div_reportbutton').hide();
    $('.clsReportH').hide();
    $('.tbodyEOBIEmployeeListing').html('');
    $('.tdBasic').text('');
    $('.tdHouseRent').text('');
    $('.tdBonus').text('');
    $('.tdPF').text('');
    $('.tdxlprcarp').text('');
    $('.tdUtilityAllowance').text('');
    $('.tdDis_LocationAllowance').text('');
    $('.tdCarAllowance').text('');
    $('.tdCellAllowance').text('');
    $('.tdIncentive').text('');
    $('.tdCOLA').text('');
    $('.tdSpecialAllowance').text('');
    $('.tdMedicalAllowance').text('');

}
