function TriggerPageLoads() {
    $(".btnCancelSearch").click(function () {
       /* $('.txtMonthOfPayroll').datepicker('setDate', null);*/
      
        ClearReport();
    });


}

function GetEmployee() {
    var CompanyId = $('.ddlCompany').val();
    var EmployeeCode = $('.txtEmployeeCode').val() == "" ? null : $('.txtEmployeeCode').val();
    var FromDate = $('.txtFromDate').val() == "" ? null : formatDate($('.txtFromDate').val());
    var ToDate = $('.txtToDate').val() == "" ? null : formatDate($('.txtToDate').val());

    if (CompanyId > 0) {
        if (EmployeeCode != null) {
            if (FromDate != null) {
                if (ToDate != null) {

                    if (ToDate >= FromDate) {
                        if (!validateForm('.divMonthPayroll'))
                            return;




                        ProgressBarShow();
                        ClearReport();
                        var service = new HrmsSuiteHcmService.HcmService();
                        service.report_Tax_Certificate(FromDate, ToDate, CompanyId, EmployeeCode,
                            onreport_Tax_Certificate, null, null);
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
        }
        else {
            showError('Please Enter Employee Code');
        }
    } else {
        showError('Please select Company');
    }
}

function onreport_Tax_Certificate(result) {

    if (result == "1") {

        window.open("TaxCertificate.aspx","_blank");

        showSuccess("Certificate Generated Successfully.");

    }
    else {
        showError("Data Not Found");
    }
   


    ProgressBarHide();
}

function ClearReport() {
    $('.div_reportbutton').hide();
    $('.clsReportH').hide();
    

}
