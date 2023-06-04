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
    filename = filename ? filename + '.xls' : 'excel_data.xls';

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
        if ($('.txtEmployeeCode').val() != "") {
            if (FromDate != null) {
                if (ToDate != null) {

                    if (ToDate >= FromDate) {
                        if (!validateForm('.divMonthPayroll'))
                            return;
                        ProgressBarShow();
                        ClearReport();
                        var service = new HrmsSuiteHcmService.HcmService();
                        service.EOBI_Employee_List_Individual(EmployeeCode, CompanyId, FromDate, ToDate, onreport_EOBI_Employee_List_Individual, null, null);
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

function onreport_EOBI_Employee_List_Individual(result) {
    
    var res = JSON.parse(result);

  


    var divTbodyGoalFund = $('.tbodyEOBIEmployeeIndividualListing').html('');
    $('#EOBIEmployeeIndividualListing').tmpl(res.Table).appendTo(divTbodyGoalFund);

    var divTbodyGoalFund = $('.tbodyEOBIEmployeeIndividualListListing').html('');
    $('#EOBIEmployeeIndividualListListing').tmpl(res.Table1).appendTo(divTbodyGoalFund);

    var TotalDays = 0;
    var Totalwages = 0;
    var Totalemplyr_con= 0;
    var Totalemp_cont= 0;
    var Totaltotal_cont= 0;



    $('.clsDays').each(function () {
        TotalDays += parseFloat($(this).text().trim());
    });

    $('.clswages').each(function () {
        Totalwages += parseFloat($(this).text().trim());
    });


    $('.clsemplyr_con').each(function () {
        Totalemplyr_con += parseFloat($(this).text().trim());
    });

    $('.clsemp_cont').each(function () {
        Totalemp_cont += parseFloat($(this).text().trim());
    });
    $('.clstotal_cont').each(function () {
        Totaltotal_cont += parseFloat($(this).text().trim());
    });

    $('.tdDays').text(TotalDays);
    $('.tdwages').text(Totalwages);
    $('.tdemplyr_con').text(Totalemplyr_con);
    $('.tdemp_cont').text(Totalemp_cont);
    $('.tdtotal_cont').text(Totaltotal_cont);

   
    SetReportHeader('EOBI Employee List Individual', 10, "Employee Code : " + $(".txtEmployeeCode").val() + "");


    $('.div_reportbutton').show();
    ProgressBarHide();
}

function ClearReport() {
    $('.div_reportbutton').hide();
    $('.clsReportH').hide();
    $('.tbodyEOBIEmployeeIndividualListing').html('');
    $('.tbodyEOBIEmployeeIndividualListListing').html('');


    $('.tdDays').text('');
    $('.tdwages').text('');
    $('.tdemplyr_con').text('');
    $('.tdemp_cont').text('');
    $('.tdtotal_cont').text('');
}

