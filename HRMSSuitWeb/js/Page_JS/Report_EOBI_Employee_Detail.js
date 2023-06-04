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
    if (CompanyId > 0) {
       
            if (!validateForm('.divMonthPayroll'))
                return;

            var EmployeeCode = $('.txtEmployeeCode').val() == '' ? null : $('.txtEmployeeCode').val();
            var Year = formatDate($('.txtMonthOfPayroll').val());


            ProgressBarShow();
            ClearReport();
            var service = new HrmsSuiteHcmService.HcmService();
            service.Report_EOBI_Employee_Detail(EmployeeCode, CompanyId,
                Year, onReport_EOBI_Employee_Detail, null, null);
       

    } else {
        showError('Please select Company');
    }
}

function onReport_EOBI_Employee_Detail(result) {
   
    /*ReqGrp var GroupByValue = $('.ddlGroupBy').val();*/

    var res = JSON.parse(result);


    var divTbodyGoalFund = $('.tbodyEOBIEmployeeListing').html('');
    $('#EOBIEmployeeListing').tmpl(res).appendTo(divTbodyGoalFund);

    var TotalDays = 0;
   
    var TotalEmployerCount = 0;
    var TotalEmployeeCount = 0;
    var TotalTotalCount = 0;


    $('.clsno_of_days').each(function () {
        TotalDays += (parseFloat(($(this).text().trim() == '' ? 0 : $(this).text().trim() )));
    });

 
    $('.clsemployer').each(function () {
        TotalEmployerCount += (parseFloat(($(this).text().trim() == '' ? 0 : $(this).text().trim())));
    });


    $('.clsemployee').each(function () {
        TotalEmployeeCount += (parseFloat(($(this).text().trim() == '' ? 0 : $(this).text().trim())));
    });

    $('.clstotal_cont').each(function () {
        TotalTotalCount += (parseFloat(($(this).text().trim() == '' ? 0 : $(this).text().trim())));
    });

    $('.tdno_of_days').text(TotalDays);
   
    $('.tdemployer').text(TotalEmployerCount);
    $('.tdemployee').text(TotalEmployeeCount);
    $('.tdtotal_cont').text(TotalTotalCount);
     

    var Para2 = '';

    SetReportHeader('EOBI Employee Detail List Report', 10, Para2);
    $('.div_reportbutton').show();
    ProgressBarHide();
}

function ClearReport() {
    $('.div_reportbutton').hide();
    $('.clsReportH').hide();
    $('.tbodyEOBIEmployeeListing').html('');
    $('.tdno_of_days').text('');

    $('.tdemployer').text('');
    $('.tdemployee').text('');
    $('.tdtotal_cont').text('');


}
