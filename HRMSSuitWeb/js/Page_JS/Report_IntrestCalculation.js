
function GetEmployee() {
    if (!validateForm('.divMonthPayroll'))
        return;

    var Year = $('.txtYear').val();

    var GroupId = $('.ddlGroup').val();
    var CompanyId = $('.ddlCompany').val();

    ProgressBarShow();
    var service = new HrmsSuiteHcmService.HcmService();
    service.report_IntrestCalculation(CompanyId, Year, onGetIntrestCalculation, null, null);

}

function onGetIntrestCalculation(result) {

    if (result != "") {

        var res = jQuery.parseJSON(result);

        bindReport(res);
  
    }
    else {
        ProgressBarHide();
        $('.thead').html('');
        $('.tbody').html('');
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
    $('.thead').children().not('.clsReportH').hide();
    $('.tbody').html('');
    SetReportHeader('Intrest Calculation Report', 15, '');
    $('.thead').append(headers);
    $('.tbody').append(data);
    $('.clsDateH').html('Year : ' + $('.txtYear').val());

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
