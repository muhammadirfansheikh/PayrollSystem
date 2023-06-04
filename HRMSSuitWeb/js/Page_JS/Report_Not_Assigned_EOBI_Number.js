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
                    service.Report_EOBI_Not_Assigned_Number(EmployeeCode, CompanyId, FromDate, ToDate, onReport_EOBI_Not_Assigned_Number, null, null);
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

function SplitTimeFromDate(val) {

    return val.split('T')[0];
}


function onReport_EOBI_Not_Assigned_Number(result) {
    
    /*ReqGrp var GroupByValue = $('.ddlGroupBy').val();*/

    var res = JSON.parse(result);


    var divTbodyGoalFund = $('.tbodyEOBIEmployeeListing').html('');
    $('#EOBIEmployeeListing').tmpl(res).appendTo(divTbodyGoalFund);

   


    
  

    var Para2 = '';

    SetReportHeader('Not Assigned EOBI Number Employee List Report', 10, Para2);

    $('.clsDateH').html('From Date : ' + $('.txtFromDate').val() + ' To Date : ' + $('.txtFromDate').val());
    $('.div_reportbutton').show();
    ProgressBarHide();
}

function ClearReport() {
    $('.div_reportbutton').hide();
    $('.clsReportH').hide();
    $('.tbodyEOBIEmployeeListing').html('');
    
}
