
TypeId = 0;

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

    service.report_HRList(EmployeeCode, parseInt(GroupId), parseInt(CompanyId), parseInt(LocationId), parseInt(BusinessUnitId), parseInt(DepartmentId), parseInt(CostCenterId),
        parseInt(CategoryId), parseInt(DesignationId), Firstname, Lastname, PayrollMonth, parseInt(TypeId), onGetHRList, null, null);

}

function onGetHRList(result) {

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

    if (TypeId == 1) {
        SetReportHeader('EMP List', 15, '');
    }
    else if (TypeId == 2) {
        SetReportHeader('HR HOD List', 15, '');
    }
    else if (TypeId == 3) {
        SetReportHeader('HR List', 15, '');
    }
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

    var urlParams = new URLSearchParams(location.search);

    if (urlParams.has('TypeId')) {

        //urlParams.has('TypeId');  // true
        //urlParams.get('TypeId');    // 1234

        TypeId = parseInt(urlParams.get('TypeId'));
    }
}