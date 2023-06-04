function TriggerPageLoads() {
    $(".btnCancelSearch").click(function () {
        $('.txtMonthOfPayroll').datepicker('setDate', null);

        ClearReport();
    });


}

function exportTableToExcel(tableID, filename = '') {
    var downloadLink;
    var dataType = 'application/vnd.ms-excel';
    var tableSelect = document.getElementById(tableID);
    var tableHTML = tableSelect.outerHTML.replace(/ /g, '%20');

    // Specify file name
    filename = filename ? filename + '.xls' : 'common_wealth_tax_report.xls';

    // Create download link element
    downloadLink = document.createElement("a");

    document.body.appendChild(downloadLink);

    if (navigator.msSaveOrOpenBlob) {
        var blob = new Blob(['\ufeff', tableHTML], {
            type: dataType
        });
        navigator.msSaveOrOpenBlob(blob, filename);
    } else {
        // Create a link to the file
        downloadLink.href = 'data:' + dataType + ', ' + tableHTML;

        // Setting the file name
        downloadLink.download = filename;

        //triggering the function
        downloadLink.click();
    }
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
                        service.Report_Common_Wealth_Tax(EmployeeCode, CompanyId, FromDate, ToDate, onreport_Common_Wealth_Tax, null, null);
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

function onreport_Common_Wealth_Tax(result) {
    ;
    var res = JSON.parse(result);




    var divTbodyGoalFund = $('.tbodycwtListing').html('');
    $('#cwtListing').tmpl(res.Table).appendTo(divTbodyGoalFund);

    var divTbodyGoalFund = $('.tbodycwtListListing').html('');
    $('#cwtListing').tmpl(res.Table1).appendTo(divTbodyGoalFund);

    var TotalTaxableGross = 0;
    var TotalTaxableIncome = 0;
    var TotalTaxDeduction = 0;
    var TotalPFLoan = 0;
    var TotalCompanyLoan = 0;
    var TotalLoanMarkup = 0;
    var TotalCarInstallmentDeduction = 0;
    var TotalZakat = 0;
    var TotalInvestment = 0;
    var TotalDonation61 = 0;
    var TotalDonation = 0;
    var TotalRebate = 0;
    var TotalAdvanceTax = 0;
    var TotalMotorTax = 0;
    var TotalMobileTax = 0;
    var TotalPropertyTax = 0;
    var TotalPFWithdrawl = 0;
   


    $('.clsTaxableGross').each(function () {
        TotalTaxableGross += parseFloat($(this).text().trim());
    });

    $('.clsTaxableIncome').each(function () {
        TotalTaxableIncome += parseFloat($(this).text().trim());
    });


    $('.clsTaxDeduction').each(function () {
        TotalTaxDeduction += parseFloat($(this).text().trim());
    });

    $('.clsPFLoan').each(function () {
        TotalPFLoan += parseFloat($(this).text().trim());
    });
    $('.clsCompanyLoan').each(function () {
        TotalCompanyLoan += parseFloat($(this).text().trim());
    });





    $('.clsLoanMarkup').each(function () {
        TotalLoanMarkup += parseFloat($(this).text().trim());
    });
    $('.clsCarInstallmentDeduction').each(function () {
        TotalCarInstallmentDeduction += parseFloat($(this).text().trim());
    });
    $('.clsZakat').each(function () {
        TotalZakat += parseFloat($(this).text().trim());
    });
    $('.clsInvestment').each(function () {
        TotalInvestment += parseFloat($(this).text().trim());
    });
    $('.clsDonation61').each(function () {
        TotalDonation61 += parseFloat($(this).text().trim());
    });
    $('.clsDonation').each(function () {
        TotalDonation += parseFloat($(this).text().trim());
    });
    $('.clsRebate').each(function () {
        TotalRebate += parseFloat($(this).text().trim());
    });


    $('.clsAdvanceTax').each(function () {
        TotalAdvanceTax += parseFloat($(this).text().trim());
    });
    $('.clsMotorTax').each(function () {
        TotalMotorTax += parseFloat($(this).text().trim());
    });
    $('.clsMobileTax').each(function () {
        TotalMobileTax += parseFloat($(this).text().trim());
    });
    $('.clsPropertyTax').each(function () {
        TotalPropertyTax += parseFloat($(this).text().trim());
    });
    $('.clsPFWithdrawl').each(function () {
        TotalPFWithdrawl += parseFloat($(this).text().trim());
    });
    
    $('.tdTaxableGross').text(TotalTaxableGross);
    $('.tdTaxableIncome').text(TotalTaxableIncome);
    $('.tdTaxDeduction').text(TotalTaxDeduction);
    $('.tdPFLoan').text(TotalPFLoan);
    $('.tdCompanyLoan').text(TotalCompanyLoan);
    $('.tdLoanMarkup').text(TotalLoanMarkup);
    $('.tdCarInstallmentDeduction').text(TotalCarInstallmentDeduction);
    $('.tdZakat').text(TotalZakat);
    $('.tdInvestment').text(TotalInvestment);
    $('.tdDonation61').text(TotalDonation61);
    $('.tdDonation').text(TotalDonation);
    $('.tdRebate').text(TotalRebate);
    $('.tdAdvanceTax').text(TotalAdvanceTax);
    $('.tdMotorTax').text(TotalMotorTax);
    $('.tdMobileTax').text(TotalMobileTax);
    $('.tdPropertyTax').text(TotalPropertyTax);
    $('.tdPFWithdrawl').text(TotalPFWithdrawl);
   


    SetReportHeader('Common Wealth Tax Report', 10, "Employee Code : " + ($(".txtEmployeeCode").val() == "" ? "All Employees" : $(".txtEmployeeCode").val() ) + "");


    $('.div_reportbutton').show();

    $('.clsDateH').html('From Date : ' + $('.txtFromDate').val() + ' To Date : ' + $('.txtToDate').val());
    ProgressBarHide();
}

function ClearReport() {
    $('.div_reportbutton').hide();
    $('.clsReportH').hide();
    $('.tbodycwtListing').html('');
    $('.tbodycwtListing').html('');


    $('.tdTotalTaxableGross').text("");
    $('.tdTotalTaxableIncome').text("");
    $('.tdTotalTaxDeduction').text("");
    $('.tdTotalPFLoan').text("");
    $('.tdTotalCompanyLoan').text("");
    $('.tdTotalLoanMarkup').text("");
    $('.tdTotalCarInstallmentDeduction').text("");
    $('.tdTotalZakat').text("");
    $('.tdTotalInvestment').text("");
    $('.tdTotalDonation61').text("");
    $('.tdTotalDonation').text("");
    $('.tdTotalRebate').text("");
    $('.tdTotalAdvanceTax').text("");
    $('.tdTotalMotorTax').text("");
    $('.tdTotalMobileTax').text("");
    $('.tdTotalPropertyTax').text("");
    $('.tdTotalPFWithdrawl').text("");
}

