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

    if (CompanyId > 0) {

        if (FromDate != null) {
            if (ToDate != null) {

                if (ToDate >= FromDate) {
                    if (!validateForm('.divMonthPayroll'))
                        return;
                    ProgressBarShow();
                    ClearReport();
                    var service = new HrmsSuiteHcmService.HcmService();
                    service.Report_EOBI_No(EmployeeCode, CompanyId, FromDate, ToDate, onReport_EOBI_No, null, null);
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

function onReport_EOBI_No(result) {
   
    /*ReqGrp var GroupByValue = $('.ddlGroupBy').val();*/

    var res = JSON.parse(result);


    var divTbodyGoalFund = $('.tbodyEOBIEmployeeListing').html('');
    $('#EOBIEmployeeListing').tmpl(res).appendTo(divTbodyGoalFund);

    var TotalDays = 0;
   


    $('.clsno_of_days').each(function () {
        TotalDays += parseFloat($(this).text().trim());
    });

   
    $('.tdno_of_days').text(TotalDays);
    

    var Para2 = '';

    SetReportHeader('EOBI  No List Report', 10, Para2);
    $('.clsDateH').html('From Date : ' + $('.txtFromDate').val() + ' To Date : ' + $('.txtFromDate').val());
    $('.div_reportbutton').show();
    ProgressBarHide();
}

function ClearReport() {
    $('.div_reportbutton').hide();
    $('.clsReportH').hide();
    $('.tbodyEOBIEmployeeListing').html('');
    $('.tdno_of_days').text('');
   
}
