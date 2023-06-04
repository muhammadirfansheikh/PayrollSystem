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
    var fromDate = $(".txtFromDate").val();
    if (CompanyId > 0) {
        if ($('.txtEmployeeCode').val() != "") {
            if (!validateForm('.divMonthPayroll'))
                return;

            if (fromDate != '') {
                ProgressBarShow();
                ClearReport();
                var service = new HrmsSuiteHcmService.HcmService();
                service.Increment_Lettet(EmployeeCode, CompanyId, fromDate, onreport_Increment_Letter, null, null);
            }
            else {
                showError('Please Select Month');
            }
        }
        else {
            showError('Please Enter Employee Code');
        }

    } else {
        showError('Please select Company');
    }
}

function onreport_Increment_Letter(result) {

    var res = JSON.parse(result);


    ;

    var divTbodyGoalFund = $('.tbodyIncrementLetter').html('');
    $('#IncrementLetterEmployeeDetail').tmpl(res.Table).appendTo(divTbodyGoalFund);

    var divTbodyGoalFund = $('.tbodyIncrementLetterAllownces').html('');
    $('#IncrementLetterAllownces').tmpl(res.Table1).appendTo(divTbodyGoalFund);

    
    $('#IncrementLetterAllowncesThree').tmpl(res.Table2).appendTo(divTbodyGoalFund);

    //var NewSalaryRupees = 0;
    //var CurrentSalaryRupees = 0;
    //var ChangeRupees = 0;


  

    //$('.clsNewSalaryRupees').each(function () {
    //    NewSalaryRupees += parseFloat($(this).text().trim());
    //});


    //$('.clsCurrentSalaryRupees').each(function () {
    //    CurrentSalaryRupees += parseFloat($(this).text().trim());
    //});

    //$('.clsChangeRupees').each(function () {
    //    ChangeRupees += parseFloat($(this).text().trim());
    //});
  

    //$('.tdNewSalaryRupees').text(NewSalaryRupees);
    //$('.tdCurrentSalaryRupees').text(CurrentSalaryRupees);
    //$('.tdChangeRupees').text(ChangeRupees);
    
    SetReportHeader('Increment Letter', 10, "Employee Code : " + $(".txtEmployeeCode").val() + "");


    $('.div_reportbutton').show();
    ProgressBarHide();
}

function ClearReport() {
    $('.div_reportbutton').hide();
    $('.clsReportH').hide();
    $('.tbodyIncrementLetter').html('');
    $('.tbodyIncrementLetterAllownces').html('');


    $('.tdNewSalaryRupees').text('');
    $('.tdCurrentSalaryRupees').text('');
    $('.tdChangeRupees').text('');
}

