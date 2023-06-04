



function GetEmployee() {
    if (!validateForm('.divMonthPayroll'))
        return;

    var CompanyId = $('.ddlCompany').val();
   
    ProgressBarShow();

    var service = new HrmsSuiteHcmService.HcmService();

    service.report_HajList(parseInt(CompanyId), onGetHajList, null, null);

}

function onGetHajList(result) {

    var res = jQuery.parseJSON(result);

    bindReport(res);

    ProgressBarHide();
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

    $('.theadHR').children().not('.clsReportH').hide();
    $('.tbodyHR').html('');

    $('.theadHR').append(headers);
    $('.tbodyHR').append(data);


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

    BindGroupByDDL();

    $('.EmployeeCodeDiv').hide();

}