function TriggerPageLoads() {
    $(".btnCancelSearch").click(function () {
        $('.txtMonthOfPayroll').datepicker('setDate', null);
        ClearReport();
    });
}

function ClearReport() {
    $('.clsReportH').hide();
    $('.HeaderColoumn').remove();
    $('.tbodyPayData').html('');
    $('.div_reportbutton').hide();

}

function GetEmployee() {
    var CompanyId = $('.ddlCompany').val();
    if (CompanyId > 0) {
        if (!validateForm('.divMonthPayroll'))
            return;
        var PayrollMonth = formatDate($('.txtMonthOfPayroll').val());
        var EmployeeCode = $('.txtEmployeeCode').val() == '' ? 0 : $('.txtEmployeeCode').val();
        var GroupId = $('.ddlGroup').val();

        var LocationId = $('.ddlLocation').val();
        var BusinessUnitId = $('.ddlBU').val();
        var DepartmentId = $('.ddlDepartment').val();
        var CostCenterId = $('.ddlCostCenter').val();
        var CategoryId = $('.ddlCategoryC').val();
        var DesignationId = $('.ddlDesignation').val();
        var Firstname = $('.txtFirstName').val();
        var Lastname = $('.txtLastName').val();
        ProgressBarShow();
        ClearReport();
        var service = new HrmsSuiteHcmService.HcmService();
        service.report_PayData(EmployeeCode, GroupId, CompanyId, LocationId, BusinessUnitId, DepartmentId, CostCenterId, CategoryId, DesignationId, Firstname, Lastname, PayrollMonth, onGetPayData, null, null);
    }
    else {
        showError("Please select company");
    }
}

function onGetPayData(result) {
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
            data += '<td>' + replaceZeroAndNull(v) + '</td>';
        });

        data += '</tr>';
    });
    $('.theadPayData').children().not('.clsReportH').hide();
    $('.tbodyPayData').html('');
    SetReportHeader('Pay Data Report', 17, '');
    $('.theadPayData').append(headers);
    $('.tbodyPayData').append(data);
    $('.div_reportbutton').show();
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

