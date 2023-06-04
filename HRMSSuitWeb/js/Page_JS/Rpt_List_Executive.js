function TriggerPageLoads() {
    $(".btnCancelSearch").click(function () {
        ClearReport();
    });


    BindGroupByDDL();
    $('.ddlGroupBy option').filter('[value="clsCompany"],[value="clsDesignation"],[value="clsDepartment"]').remove();
}

function GetEmployee() {
    var CompanyId = $('.ddlCompany').val();
    if (CompanyId > 0) {
        if (!validateForm('.divMonthPayroll'))
            return;
        var EmployeeCode = $('.txtEmployeeCode').val() == '' ? 0 : $('.txtEmployeeCode').val();

        ProgressBarShow();
        ClearReport();
        var service = new HrmsSuiteHcmService.HcmService();
        service.report_ExecutiveLis(EmployeeCode, CompanyId, onreport_ExecutiveLis, null, null);
    } else {
        showError('Please select Company');
    }
}

function onreport_ExecutiveLis(result) {
  
    var res = JSON.parse(result);


    var divTbodyGoalFund = $('.tbodyExecutiveListing').html('');
    $('#ExecutiveListing').tmpl(res).appendTo(divTbodyGoalFund);

    var Para2 = '';
    SetReportHeader('Executive List', 10, Para2);
    $('.div_reportbutton').show();
    addSerialNumber();
    ProgressBarHide();
}

function ClearReport() {
    $('.div_reportbutton').hide();
    $('.clsReportH').hide();
    $('.tbodyExecutiveListing').html('');
 
}

