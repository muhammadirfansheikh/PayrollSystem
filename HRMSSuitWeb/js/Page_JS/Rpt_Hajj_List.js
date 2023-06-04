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

        if ($(".txtFromDate").val() == '' ) {

            showError('Please Select Date.');
        }
        else {
            var EmployeeCode = $('.txtEmployeeCode').val() == '' ? 0 : $('.txtEmployeeCode').val();
            var fromDate = $(".txtFromDate").val();
            

            ProgressBarShow();
            ClearReport();
            var service = new HrmsSuiteHcmService.HcmService();
            service.report_Hajj_List(EmployeeCode, CompanyId, fromDate, onreport_HajjLis, null, null);
        }
      
    } else {
        showError('Please select Company');
    }
}

function onreport_HajjLis(result) {

    var res = JSON.parse(result);


    var divTbodyGoalFund = $('.tbodyHajjListing').html('');
    $('#HajjListing').tmpl(res).appendTo(divTbodyGoalFund);

    var Para2 = '';
    SetReportHeader('Hajj List', 10, Para2);
    $('.div_reportbutton').show();
    addSerialNumber();
    ProgressBarHide();
}

function ClearReport() {
    $('.div_reportbutton').hide();
    $('.clsReportH').hide();
    $('.tbodyHajjListing').html('');

}

