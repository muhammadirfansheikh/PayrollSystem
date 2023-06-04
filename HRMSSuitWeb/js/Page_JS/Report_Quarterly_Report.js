function TriggerPageLoads() {
    $(".btnCancelSearch").click(function () {
        $('.txtMonthOfPayroll').datepicker('setDate', null);
        $('.ddlGroupBy').val('0');
        ClearReport();
    });

    BindGroupByDDL();
    $('.ddlGroupBy option').filter('[value="clsCompany"],[value="clsDesignation"],[value="clsBankName"]').remove();
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
                    service.Report_Quarterly_Report(EmployeeCode, CompanyId, FromDate, ToDate, onReportQuarterly, null, null);
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


    var divTbodyGoalFund = $('.tbodyQuarterlyReport').html('');
    $('#QuarterlyReportListing').tmpl(res).appendTo(divTbodyGoalFund);

    var Totalxlmastbasi = 0;
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
    var TotalEOBICompany = 0;
    var TotalMedicalAllowance = 0;
    var TotalTotal2 = 0;
    var TotalGratuity = 0;
    var TotalTotal3 = 0;



    $('.clsxlmastbasi').each(function () {
        Totalxlmastbasi += parseFloat($(this).text().trim());
    });

    $('.clsBasic').each(function () {
        TotalBasic += parseFloat($(this).text().trim());
    });

    $('.clsHouseRent').each(function () {
        TotalHouseRent+= parseFloat($(this).text().trim());
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

    $('.clsUtilityAllowance').each(function () {
        TotalUtilityAllowance += parseFloat($(this).text().trim());
    });

    $('.clsDis_LocationAllowance').each(function () {
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
    $('.clsEOBICompany').each(function () {
        TotalEOBICompany += parseFloat($(this).text().trim());
    });
    $('.clsSpecialAllowance').each(function () {
        TotalSpecialAllowance += parseFloat($(this).text().trim());
    });
    $('.clsMedicalAllowance').each(function () {
        TotalMedicalAllowance += parseFloat($(this).text().trim());
    });
    $('.clsTotal2').each(function () {
        TotalTotal2 += parseFloat($(this).text().trim());
    });
    $('.clsGratuity').each(function () {
        TotalGratuity += parseFloat($(this).text().trim());
    });
    $('.clsTotal3').each(function () {
        TotalTotal3 += parseFloat($(this).text().trim());
    });




    $('.tdxlmastbasi').text(Totalxlmastbasi);
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
    $('.tdEOBICompany').text(TotalEOBICompany);
    $('.tdMedicalAllowance').text(TotalMedicalAllowance);
    $('.tdTotal2').text(TotalTotal2);
    $('.tdGratuity').text(TotalGratuity);
    $('.tdTotal3').text(TotalTotal3);


    var Prev = 0;
    var i = 0;
    var _Sum1 = 0;
    var _Sum2 = 0;
    var _Sum3 = 0;
    var _Sum4 = 0;
    var _Sum5 = 0;
    var _Sum6 = 0;
    var _Sum7 = 0;
    var _Sum8 = 0;
    var _Sum9 = 0;
    var _Sum10 = 0;
    var _Sum11 = 0;
    var _Sum12 = 0;
    var _Sum13 = 0;
    var _Sum14 = 0;
    var _Sum15 = 0;
    var _Sum16 = 0;
    var _Sum17 = 0;
    var _Sum18 = 0;
   

    var GroupBy = '.' + $('.ddlGroupBy').val();

    var ColSpan = $('.clsincr').index() - 0;

    var GroupByName = $(".ddlGroupBy option:selected").text();

    var GroupByValue = $('.ddlGroupBy').val();
    if (GroupByValue != 0) {
        $('.trList').each(function () {


            var CurrLocId = $(this).find('.ABC').find(GroupBy).val();

            if ($(this).is(':first-child')) {

                _Sum1 += parseFloat($(this).find('.clsxlmastbasi').text());
                _Sum2 += parseFloat($(this).find('.clsBasic').text());
                _Sum3 += parseFloat($(this).find('.clsHouseRent').text());
                _Sum4 += parseFloat($(this).find('.clsBonus').text());
                _Sum5 += parseFloat($(this).find('.clsPF').text());
                _Sum6 += parseFloat($(this).find('.clsxlprcarp').text());
                _Sum7 += parseFloat($(this).find('.clsUtilityAllowance').text());
                _Sum8 += parseFloat($(this).find('.clsDis_LocationAllowance').text());
                _Sum9 += parseFloat($(this).find('.clsCarAllowance').text());
                _Sum10 += parseFloat($(this).find('.clsCellAllowance').text());
                _Sum11 += parseFloat($(this).find('.clsIncentive').text());
                _Sum12 += parseFloat($(this).find('.clsCOLA').text());
                _Sum13 += parseFloat($(this).find('.clsSpecialAllowance').text());
                _Sum14 += parseFloat($(this).find('.clsEOBICompany').text());
                _Sum15 += parseFloat($(this).find('.clsMedicalAllowance').text());
                _Sum16 += parseFloat($(this).find('.clsTotal2').text());
                _Sum17 += parseFloat($(this).find('.clsGratuity').text());
                _Sum18 += parseFloat($(this).find('.clsTotal3').text());
              



            }
            else {

                if (Prev == CurrLocId) {
                    _Sum1 += parseFloat($(this).find('.clsxlmastbasi').text());
                    _Sum2 += parseFloat($(this).find('.clsBasic').text());
                    _Sum3 += parseFloat($(this).find('.clsHouseRent').text());
                    _Sum4 += parseFloat($(this).find('.clsBonus').text());
                    _Sum5 += parseFloat($(this).find('.clsPF').text());
                    _Sum6 += parseFloat($(this).find('.clsxlprcarp').text());
                    _Sum7 += parseFloat($(this).find('.clsUtilityAllowance').text());
                    _Sum8 += parseFloat($(this).find('.clsDis_LocationAllowance').text());
                    _Sum9 += parseFloat($(this).find('.clsCarAllowance').text());
                    _Sum10 += parseFloat($(this).find('.clsCellAllowance').text());
                    _Sum11 += parseFloat($(this).find('.clsIncentive').text());
                    _Sum12 += parseFloat($(this).find('.clsCOLA').text());
                    _Sum13 += parseFloat($(this).find('.clsSpecialAllowance').text());
                    _Sum14 += parseFloat($(this).find('.clsEOBICompany').text());
                    _Sum15 += parseFloat($(this).find('.clsMedicalAllowance').text());
                    _Sum16 += parseFloat($(this).find('.clsTotal2').text());
                    _Sum17 += parseFloat($(this).find('.clsGratuity').text());
                    _Sum18 += parseFloat($(this).find('.clsTotal3').text());

                }
                else {

                    var GroupByItem = $(this).prev().find('.ABC').find(GroupBy).val();

                    $("<tr class='success'><th colspan=" + ColSpan + " style='text-align:left'>" + GroupByName + " : " + GroupByItem + "</th><th style='text-align:right'>" + _Sum1 + "</th><th></th><th></th><th></th><th style='text-align:right'>" + _Sum2 + "</th><th style='text-align:right'>" + _Sum3 + "</th><th style='text-align:right'>" + _Sum4 + "</th><th style='text-align:right'>" + _Sum5 + "</th><th style='text-align:right'>" + _Sum6 + "</th><th style='text-align:right'>" + _Sum7 + "</th><th style='text-align:right'>" + _Sum8 + "</th><th style='text-align:right'>" + _Sum9 + "</th><th style='text-align:right'>" + _Sum10 + "</th><th style='text-align:right'>" + _Sum11 + "</th><th style='text-align:right'>" + _Sum12 + "</th><th style='text-align:right'>" + _Sum13 + "</th><th style='text-align:right'>" + _Sum14 + "</th><th style='text-align:right'>" + _Sum15 + "</th><th style='text-align:right'>" + _Sum16 + "</th><th style='text-align:right'>" + _Sum17 + "</th><th style='text-align:right'>" + _Sum18 + "</th></tr>").insertBefore($(this));
                    i = -1;
                    _Sum1 = parseFloat($(this).find('.clsxlmastbasi').text());
                    _Sum2 = parseFloat($(this).find('.clsBasic').text());
                    _Sum3 = parseFloat($(this).find('.clsHouseRent').text());
                    _Sum4 = parseFloat($(this).find('.clsBonus').text());
                    _Sum5 = parseFloat($(this).find('.clsPF').text());
                    _Sum6 = parseFloat($(this).find('.clsxlprcarp').text());
                    _Sum7 = parseFloat($(this).find('.clsUtilityAllowance').text());
                    _Sum8 = parseFloat($(this).find('.clsDis_LocationAllowance').text());
                    _Sum9 = parseFloat($(this).find('.clsCarAllowance').text());
                    _Sum10 = parseFloat($(this).find('.clsCellAllowance').text());
                    _Sum11 = parseFloat($(this).find('.clsIncentive').text());
                    _Sum12 = parseFloat($(this).find('.clsCOLA').text());
                    _Sum13 = parseFloat($(this).find('.clsSpecialAllowance').text());
                    _Sum14 = parseFloat($(this).find('.clsEOBICompany').text());
                    _Sum15 = parseFloat($(this).find('.clsMedicalAllowance').text());
                    _Sum16 = parseFloat($(this).find('.clsTotal2').text());
                    _Sum17 = parseFloat($(this).find('.clsGratuity').text());
                    _Sum18 = parseFloat($(this).find('.clsTotal3').text());
                }

                if ($(this).is(':last-child')) {
                    var GroupByItem = $(this).find('.ABC').find(GroupBy).val();
                    $("<tr class='success'><th colspan=" + ColSpan + " style='text-align:left'>" + GroupByName + " : " + GroupByItem + "</th><th style='text-align:right'>" + _Sum1 + "</th><th></th><th></th><th></th><th style='text-align:right'>" + _Sum2 + "</th><th style='text-align:right'>" + _Sum3 + "</th><th style='text-align:right'>" + _Sum4 + "</th><th style='text-align:right'>" + _Sum5 + "</th><th style='text-align:right'>" + _Sum6 + "</th><th style='text-align:right'>" + _Sum7 + "</th><th style='text-align:right'>" + _Sum8 + "</th><th style='text-align:right'>" + _Sum9 + "</th><th style='text-align:right'>" + _Sum10 + "</th><th style='text-align:right'>" + _Sum11 + "</th><th style='text-align:right'>" + _Sum12 + "</th><th style='text-align:right'>" + _Sum13 + "</th><th style='text-align:right'>" + _Sum14 + "</th><th style='text-align:right'>" + _Sum15 + "</th><th style='text-align:right'>" + _Sum16 + "</th><th style='text-align:right'>" + _Sum17 + "</th><th style='text-align:right'>" + _Sum18 + "</th></tr>").insertAfter($(this));
                    i = -1;
                    _Sum1 = 0;
                    _Sum2 = 0;
                    _Sum3 = 0;
                    _Sum4 = 0;
                    _Sum5 = 0;
                    _Sum6 = 0;
                    _Sum7 = 0;
                    _Sum8 = 0;
                    _Sum9 = 0;
                    _Sum10 = 0;
                    _Sum11 = 0;
                    _Sum12 = 0;
                    _Sum13 = 0;
                    _Sum14 = 0;
                    _Sum15 = 0;
                    _Sum16 = 0;
                    _Sum17 = 0;
                    _Sum18 = 0;
                 

                }
            }

            Prev = CurrLocId;
            i++;

        });
    }
    else {
        var divTbodyGoalFund = $('.tbodyQuarterlyReport').html('');
        $('#QuarterlyReportListing').tmpl(res).appendTo(divTbodyGoalFund);

    }


    var Para2 = '';
    var _Counter = 0;
    $('.clsSNo').each(function (_Counter) {
        _Counter = _Counter + 1;
        $(this).text(_Counter);
    });
    SetReportHeader('Quarterly Report', 10, Para2);
    $('.clsDateH').html('From Date : ' + $('.txtFromDate').val() + ' To Date : ' + $('.txtToDate').val());
    $('.div_reportbutton').show();
    ProgressBarHide();
}

function ClearReport() {
    $('.tdxlmastbasi').text('');
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
    $('.tdEOBICompany').text('');
    $('.tdMedicalAllowance').text('');
    $('.tdTotal2').text('');
    $('.tdGratuity').text('');
    $('.tdTotal3').text('');
}
