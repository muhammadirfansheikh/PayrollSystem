function TriggerPageLoads() {
    $(".btnCancelSearch").click(function () {
        $('.txtMonthOfPayroll').datepicker('setDate', null);

        ClearReport();
    });


}

function GetEmployee() {
    
    var CompanyId = $('.ddlCompany').val();
    if (CompanyId > 0) {
        if ($('.txtEmployeeCode').val()  != "") {
            if (!validateForm('.divMonthPayroll'))
                return;

            var EmployeeCode = $('.txtEmployeeCode').val() == '' ? null : $('.txtEmployeeCode').val();
            var Year = formatDate($('.txtMonthOfPayroll').val());


            ProgressBarShow();
            ClearReport();
            var service = new HrmsSuiteHcmService.HcmService();
            service.report_PF_Statement(EmployeeCode, CompanyId,
                Year, onreport_PF_Statement, null, null);
        } else {
            showError('Please Enter Employee Code');
        }
        
    } else {
        showError('Please select Company');
    }
}

function onreport_PF_Statement(result) {
    ;
    /* var GroupByValue = $('.ddlGroupBy').val();*/

    var res = JSON.parse(result);

    //if (GroupByValue != 0) {
    //    res = res.sort(sortByProperty(GroupByValue));
    //}


    var divTbodyGoalFund = $('.tbodyPFStatementEmployeeListing').html('');
    $('#PFStatementEmployeeListing').tmpl(res.Table).appendTo(divTbodyGoalFund);

    var divTbodyGoalFund = $('.tbodyPFStatementListing').html('');
    $('#PFStatementListing').tmpl(res.Table1).appendTo(divTbodyGoalFund);

    var TotalEMPLOYEES_CONTRIBUTION = 0;
    var TotalEMPLOYERS_CONTRIBUTION = 0;
    var TotalTOTAL_CONTRIBUTION = 0;
    


    $('.clsEMPLOYEESCONTRIBUTION').each(function () {
        TotalEMPLOYEES_CONTRIBUTION += parseFloat(($(this).text().trim() == '' ? 0 : $(this).text().trim() ));
    });

    $('.clsEMPLOYERSCONTRIBUTION').each(function () {
        TotalEMPLOYERS_CONTRIBUTION += parseFloat(($(this).text().trim() == '' ? 0 : $(this).text().trim()));
    });


    $('.clsTOTALCONTRIBUTION').each(function () {
        TotalTOTAL_CONTRIBUTION += parseFloat(($(this).text().trim() == '' ? 0 : $(this).text().trim()));
    });

  

    $('.tdEMPLOYEESCONTRIBUTION').text(TotalEMPLOYEES_CONTRIBUTION);
    $('.tdEMPLOYERSCONTRIBUTION').text(TotalEMPLOYERS_CONTRIBUTION);
    $('.tdTOTALCONTRIBUTION').text(TotalTOTAL_CONTRIBUTION);
   
    //var Para2 = '';
    //var BranchId = $('.ddlBranch').val();
    //if (BranchId != 0) {

    //    Para2 = "";
    //}

    SetReportHeader('PF Statement', 10, "Employee Code : " + $(".txtEmployeeCode").val()+"");


    $('.div_reportbutton').show();
    ProgressBarHide();
}

function ClearReport() {
    $('.div_reportbutton').hide();
    $('.clsReportH').hide();
    $('.tbodyPFStatementListing').html('');
    $('.tbodyPFStatementEmployeeListing').html('');

    $('.tdEMPLOYEESCONTRIBUTION').text('');
    $('.tdEMPLOYERSCONTRIBUTION').text('');
    $('.tdTOTALCONTRIBUTION').text('');
}

