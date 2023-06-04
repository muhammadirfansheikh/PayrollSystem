


function GetEmployee() {

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
    service.getGratuityData(EmployeeCode, GroupId, CompanyId, LocationId, BusinessUnitId, DepartmentId, CostCenterId, CategoryId, DesignationId, Firstname, Lastname, onGetGratuityData, null, null);
}

function onGetGratuityData(result) {
    var res = jQuery.parseJSON(result);
    bindReport(res);
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

    $('.theadGratuity').children().not('.clsReportH').hide();
    //$('.theadMasterSalary').html('');
    $('.tbodyGratuity').html('');

    $('.theadGratuity').append(headers);
    $('.tbodyGratuity').append(data);

    ProgressBarHide();

    SetReportHeader('Gratuity', 15, '');
    $('.clsDateH').hide();
    $('.clsDateH').remove();
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

}