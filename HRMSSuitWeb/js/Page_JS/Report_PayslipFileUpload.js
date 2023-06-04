function TriggerPageLoads() {
    $(".btnCancelSearch").click(function () {
        $('.txtMonthOfPayroll').datepicker('setDate', null);
        ClearReport();
    });
}

function ClearReport() {
    $('.theadPayData').html('');
    $('.tbodyPayData').html('');
    $('.div_reportbutton').hide();
}

function GetEmployee() {
    var CompanyId = $('.ddlCompany').val();
    if (CompanyId > 0) {
        if (!validateForm('.divMonthPayroll'))
            return;
        var PayrollMonth = formatDate($('.txtMonthOfPayroll').val());
        ProgressBarShow();
        ClearReport();
        var service = new HrmsSuiteHcmService.HcmService();
        service.RPT_Payslip_Upload(PayrollMonth, CompanyId, onGetData, null, null);
    }
    else {
        showError("Please select company");
    }
}

function onGetData(result) {
    if (result != "") {
        var res = jQuery.parseJSON(result);
        bindReport(res);
    }
    else {
        ProgressBarHide();
        $('.theadPayData').html('');
        $('.tbodyPayData').html('');
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
            data += '<td>' + v + '</td>';
        });

        data += '</tr>';
    });
    $('.theadPayData').html('');
    $('.tbodyPayData').html('');
    $('.theadPayData').append(headers);
    $('.tbodyPayData').append(data);
    $('.div_reportbutton').show();
    ProgressBarHide();
}

function replaceZeroAndNull(val) {
    //if (val == "0" || val == null)
    //    return "-";
    //else {
    //    if ($.isNumeric(val))
    //        return parseFloat(val);
    //    else
    //        return val;
    //}
    return val;
}
