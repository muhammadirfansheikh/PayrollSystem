



function GetEmployee() {
    if (!validateForm('.divMonthPayroll'))
        return;

    var PayrollMonth = formatDate($('.txtMonthOfPayroll').val());

    var EmployeeCode = $('.txtEmployeeCode').val() == '' ? 0 : $('.txtEmployeeCode').val();
    var GroupId = $('.ddlGroup').val();
    var CompanyId = $('.ddlCompany').val();
    var LocationId = $('.ddlLocation').val();
    var BusinessUnitId = $('.ddlBU').val();
    var DepartmentId = $('.ddlDepartment').val();
    var CostCenterId = $('.ddlCostCenter').val();
    var CategoryId = $('.ddlCategoryC').val();
    var DesignationId = $('.ddlDesignation').val();
    var Firstname = $('.txtFirstName').val();
    var Lastname = $('.txtLastName').val();


    ProgressBarShow();
    var service = new HrmsSuiteHcmService.HcmService();
    service.report_Arrear(EmployeeCode, GroupId, CompanyId, LocationId, BusinessUnitId, DepartmentId, CostCenterId, CategoryId, DesignationId, Firstname, Lastname, PayrollMonth, onGetArrear, null, null);

}

function onGetArrear(result) {

    if (result != "") {

        var res = jQuery.parseJSON(result);
        bindReport(res);
    }
    else {
        ProgressBarHide();
        $('.theadArr').html('');
        $('.tbodyArr').html('');
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
    $('.theadArr').children().not('.clsReportH').hide();
    $('.tbodyArr').html('');
    SetReportHeader('Arrear Report', 15, '');
    $('.theadArr').append(headers);
    $('.tbodyArr').append(data);

    //if ($("#chkSummary").is(':checked')) {

    //}
    //else {
    //    $('.clsReportH').hide();
    //}

    ProgressBarHide();
}

function replaceZeroAndNull(val) {


    if (val == "0" || val == null)
        return "-";
    else {
        if ($.isNumeric(val))
            return parseFloat(val);
        else
            return val;
    }
}

function TriggerPageLoads() { }