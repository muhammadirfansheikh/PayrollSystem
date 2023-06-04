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
                    
                        if (!validateForm('.divMonthPayroll'))
                            return;
                        ProgressBarShow();
                        ClearReport();
                        var service = new HrmsSuiteHcmService.HcmService();
                        service.Report_Leave_Encashment(EmployeeCode, CompanyId, FromDate, ToDate, EmployeeType, onReport_Leave_Enchashment, null, null);
                   

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

function onReport_Leave_Enchashment(result) {
    ;
    /*ReqGrp var GroupByValue = $('.ddlGroupBy').val();*/

    var res = JSON.parse(result);


    var divTbodyGoalFund = $('.tbodyLEListing').html('');
    $('#LEListing').tmpl(res.LeaveEncashmentData).appendTo(divTbodyGoalFund);

    var TotalBasic = 0;
    var TotalDays = 0;
    var TotalHours = 0;
    var TotalAmount = 0;
   


    $('.clsBasic').each(function () {
        TotalBasic += parseFloat($(this).text().trim());
    });

    $('.clsDays').each(function () {
        TotalDays += parseFloat($(this).text().trim());
    });

    $('.clsHours').each(function () {
        TotalHours += parseFloat($(this).text().trim());
    });


    $('.clsTotalAmount').each(function () {
        TotalAmount += parseFloat($(this).text().trim());
    });

   


    $('.tdBasic').text(TotalBasic);
    $('.tdDays').text(TotalDays);
    $('.tdHours').text(TotalHours);
    $('.tdTotalAmount').text(TotalAmount);
   


    var Para2 = '';

    SetReportHeader('Leave Encashment Report', 10, Para2);
    $('.div_reportbutton').show();
    $('.clsDateH').html('From Date : ' + $('.txtFromDate').val() + ' To Date : ' + $('.txtToDate').val());
    addSerialNumber();


    var divTbodySummary = $('.tbodySummary').html('');
    $('#DataSummary').tmpl(res.LeaveEncashmentDataSummary).appendTo(divTbodySummary);


    var TotalSapAmount = 0;
    $('.clsAmount').each(function () {
        TotalSapAmount += parseFloat($(this).text().trim());
    });

    $('.tdAmount').text(TotalSapAmount);

    $('.divSummary').show();


    $('.divSummary').find('.clsDateH').html('From Date : ' + $('.txtFromDate').val() + ' To Date : ' + $('.txtToDate').val());
    $('.divSummary').find('.clsPrintDate').html("");
    ProgressBarHide();
}

function ClearReport() {
    $('.div_reportbutton').hide();
    $('.clsReportH').hide();
    $('.divSummary').hide();
    $('.tbodyLEListing').html('');


    $('.tdBasic').text("");
    $('.tdDays').text("");
    $('.tdHours').text("");
    $('.tdAmount').text("");
    $('.tdAmount').text("");
}
