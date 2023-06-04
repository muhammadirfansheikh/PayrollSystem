function TriggerPageLoads() {
    $(".btnCancelSearch").click(function () {
        $('.txtMonthOfPayroll').datepicker('setDate', null);
        //ReqGrp  /*$('.ddlGroupBy').val('0');*/
        ClearReport();
    });

    //BindBank();
    //BindGroupByDDL();ReqGrp
    //$('.ddlGroupBy option').filter('[value="clsCompany"],[value="clsDesignation"],[value="clsDepartment"]').remove();ReqGrp
}

function GetEmployee() {
    var CompanyId = $('.ddlCompany').val();
    var EmployeeCode = $('.txtEmployeeCode').val() == "" ? null : $('.txtEmployeeCode').val();
    var FromDate = $('.txtFromDate').val() == "" ? null : formatDate($('.txtFromDate').val());
    var ToDate = $('.txtToDate').val() == "" ? null : formatDate($('.txtToDate').val());
    var EmployeeType = $('#ddlEmployeeType').val();

    if (CompanyId > 0) {

        if (FromDate != null) {
            if (ToDate != null) {

                if (ToDate >= FromDate) {
                    if (parseInt(EmployeeType) != -1) {
                        if (!validateForm('.divMonthPayroll'))
                            return;
                        ProgressBarShow();
                        ClearReport();
                        var service = new HrmsSuiteHcmService.HcmService();
                        service.Report_WPPF_Salary_History(EmployeeCode, CompanyId, FromDate, ToDate, EmployeeType, onReport_WPPF_Salary_History, null, null);
                    }
                    else {
                        showError('Please Select Employee Type.');
                    }
                    
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

function onReport_WPPF_Salary_History(result) {
    ;
    /*ReqGrp var GroupByValue = $('.ddlGroupBy').val();*/

    var res = JSON.parse(result);


    var divTbodyGoalFund = $('.tbodyEOBIEmployeeListing').html('');
    $('#EOBIEmployeeListing').tmpl(res.Table).appendTo(divTbodyGoalFund);

    var TotalJuly = 0;
    var TotalAugust = 0;
    var TotalSeptember = 0;
    var TotalOctober = 0;
    var TotalNovember = 0;
    var TotalDecember = 0;
    var TotalJanuary = 0;
    var TotalFebruary = 0;
    var TotalMarch = 0;
    var TotalApril = 0;
    var TotalMay= 0;
    var TotalJune = 0;
    var Totaltotal= 0;
    var TotalPeriods= 0;
    /*var TotalWorkingDays= 0;*/
    var TotalPM= 0;


    $('.clsJuly').each(function () {
        TotalJuly += parseFloat($(this).text().trim());
    });

    $('.clsAugust').each(function () {
        TotalAugust += parseFloat($(this).text().trim());
    });

    $('.clsSeptember').each(function () {
        TotalSeptember += parseFloat($(this).text().trim());
    });


    $('.clsOctober').each(function () {
        TotalOctober += parseFloat($(this).text().trim());
    });

    $('.clsNovember').each(function () {
        TotalNovember += parseFloat($(this).text().trim());
    });

    $('.clsDecember').each(function () {
        TotalDecember += parseFloat($(this).text().trim());
    });
    $('.clsJanuary').each(function () {
        TotalJanuary += parseFloat($(this).text().trim());
    });
    $('.clsFebruary').each(function () {
        TotalFebruary += parseFloat($(this).text().trim());
    });
    $('.clsMarch').each(function () {
        TotalMarch += parseFloat($(this).text().trim());
    });
    $('.clsApril').each(function () {
        TotalApril += parseFloat($(this).text().trim());
    });
    $('.clsMay').each(function () {
        TotalMay += parseFloat($(this).text().trim());
    });
    $('.clsJune').each(function () {
        TotalJune += parseFloat($(this).text().trim());
    });
    $('.clsTotal').each(function () {
        Totaltotal += parseFloat($(this).text().trim());
    });
    $('.clsPeriod').each(function () {
        TotalPeriods += parseFloat($(this).text().trim());
    });
    //$('.clsWorkingDays').each(function () {
    //    TotalWorkingDays += parseFloat($(this).text().trim());
    //});


    $('.clsPM').each(function () {
        TotalPM += parseFloat($(this).text().trim());
    });


    $('.tdJuly').text(TotalJuly);
    $('.tdAugust').text(TotalAugust);
    $('.tdSeptember').text(TotalSeptember);
    $('.tdOctober').text(TotalOctober);
    $('.tdNovember').text(TotalNovember);
    $('.tdDecember').text(TotalDecember);
    $('.tdJanuary').text(TotalJanuary);
    $('.tdFebruary').text(TotalFebruary);
    $('.tdMarch').text(TotalMarch);
    $('.tdApril').text(TotalApril);
    $('.tdMay').text(TotalMay);
    $('.tdJune').text(TotalJune);
    $('.tdTotal').text(Totaltotal);
    $('.tdPeriod').text(TotalPeriods);
    //$('.tdWorkingDays').text(TotalWorkingDays);
    $('.tdPM').text(TotalPM);
   

    var Para2 = '';

    SetReportHeader('WPPF Salary History List Report', 10, Para2);
    $('.div_reportbutton').show();
    $('.clsDateH').html('From Date : ' + $('.txtFromDate').val() + ' To Date : ' + $('.txtToDate').val());
    addSerialNumber();


    var divTbodySummary= $('.tbodySummary').html('');
    $('#DataSummary').tmpl(res.Table2).appendTo(divTbodySummary);

   
    $('.divSummary').show();

   
    $('.divSummary').find('.clsDateH').html('From Date : ' + $('.txtFromDate').val() + ' To Date : ' + $('.txtToDate').val());
    $('.divSummary').find('.clsPrintDate').html(res.Table1[0].ColumnName1);
    ProgressBarHide();
}

function ClearReport() {
    $('.div_reportbutton').hide();
    $('.clsReportH').hide();
    $('.divSummary').hide();
    $('.tbodyEOBIEmployeeListing').html('');
    $('.tdJuly').text('');
    $('.tdAugust').text('');
    $('.tdSeptember').text('');
    $('.tdOctober').text('');
    $('.tdNovember').text('');
    $('.tdDecember').text('');
    $('.tdJanuary').text('');
    $('.tdFebruary').text('');
    $('.tdMarch').text('');
    $('.tdApril').text('');
    $('.tdMay').text('');
    $('.tdJune').text('');
    $('.tdTotal').text('');
    $('.tdPeriod').text('');
    $('.tdWorkingDays').text('');
    $('.tdPM').text('');
}
