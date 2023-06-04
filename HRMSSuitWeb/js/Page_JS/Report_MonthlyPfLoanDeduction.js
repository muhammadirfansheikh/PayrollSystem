


function GetEmployee() {
    if (!validateForm('.divMonthPayroll'))
        return;

    var Year = $('.txtYear').val();

    var GroupId = $('.ddlGroup').val();
    var CompanyId = $('.ddlCompany').val();

    ProgressBarShow();
    var service = new HrmsSuiteHcmService.HcmService();
    service.report_MonthlyPfLoanDeduction(CompanyId, Year, onGetMonthlyPfDeduction, null, null);

}

function onGetMonthlyPfDeduction(result) {

    if (result != "") {

        var result = result.split("#SPLIT#");
        var res = jQuery.parseJSON(result[0]);
        var res2 = jQuery.parseJSON(result[1]);

        bindReport(res);
        bindReportSummary(res2);
    }
    else {
        ProgressBarHide();
        $('.theadMonthlyPfDeduction').html('');
        $('.tbodyMonthlyPfDeduction').html('');
        showError("No Record Found");
    }
}

function bindReport(res) {

    var headers = '';
    var data = '';
    $(res[0]).each(function (i, val) {
        headers += '<tr class="info">';
        $.each(val, function (k, v) {
            headers += '<th>' + k + '</th>';
        });
        headers += '</tr>';
    });

    $(res).each(function (i, val) {

        data += '<tr>';

        $.each(val, function (k, v) {
            data += '<td>' + replaceZeroAndNull(v) + '</td>';
        });

        data += '</tr>';
    });
    $('.theadMonthlyPfDeduction').children().not('.clsReportH').hide();
    $('.tbodyMonthlyPfDeduction').html('');
    SetReportHeader('Monthly PF Deduction Loan Report', 15, '');
    $('.theadMonthlyPfDeduction').append(headers);
    $('.tbodyMonthlyPfDeduction').append(data);
    $('.clsDateH').html('Year : ' + $('.txtYear').val());

    ProgressBarHide();
}

function bindReportSummary(res) {

    var headers = '';
    var data = '';
    //$(res[0]).each(function (i, val) {
    //    headers += '<tr class="info">';
    //    $.each(val, function (k, v) {
    //        headers += '<th>' + k + '</th>';
    //    });
    //    headers += '</tr>';
    //});

    $(res).each(function (i, val) {

        data += '<tr>';

        $.each(val, function (k, v) {
            data += '<td>' + replaceZeroAndNull(v) + '</td>';
        });

        data += '</tr>';
    });
    $('.theadMonthlyPfDeductionSumm').children().not('.clsReportH').hide();
    $('.tbodyMonthlyPfDeductionSumm').html('');
    SetReportHeader('Monthly PF Deduction Loan Report ', 15, '');
    $('.theadMonthlyPfDeductionSumm').append(headers);
    $('.tbodyMonthlyPfDeductionSumm').append(data);
    $('.clsDateH').html('Year : ' + $('.txtYear').val());
    $('.tableMonthlyPfDeductionSumm theadMonthlyPfDeductionSumm ').find('.clsReportNameH').html('Monthly PF Deduction Loan Report');

    ProgressBarHide();
}

function replaceZeroAndNull(val) {
    if (val == "0" || val == null)
        return "-";
    else {
        if ($.isNumeric(val))
            return parseInt(val);
        else
            return val;
    }
}

function TriggerPageLoads() {
    $('.EmployeeCodeDiv').hide();
}
