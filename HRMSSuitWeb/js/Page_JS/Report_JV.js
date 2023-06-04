
function TriggerPageLoads() {
    $(".btnCancelSearch").click(function () {
        $('.txtMonthOfPayroll').datepicker('setDate', null);
        $(".chkSummary").prop("checked", false);
        ClearReport();
    });
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
        var IsSummary = false;
        if ($("#chkSummary").is(':checked')) {
            IsSummary = true;
        }
        ProgressBarShow();
        ClearReport();
        var service = new HrmsSuiteHcmService.HcmService();
        service.report_JV(EmployeeCode, GroupId, CompanyId, LocationId, BusinessUnitId, DepartmentId, CostCenterId, CategoryId, DesignationId, Firstname, Lastname, PayrollMonth, IsSummary, onGetJV, null, null);
    } else {
        showError('Please select Company');
    }
}

function onGetJV(result) {
    if (result != "") {
        var res = jQuery.parseJSON(result);
        if (res.length > 0) {
            bindReport(res);
        }
    }
    else {
        $('.theadJV').html('');
        $('.tbodyJV').html('');
        showError("No Record Found");
    }
    ProgressBarHide();
}

function bindReport(res) {

    var headers = '';
    var data = '';
    var colpsan = 15;
    $(res[0]).each(function (i, val) {
        headers += '<tr class="info">';
        $.each(val, function (k, v) {
            let _splitHeaderText;

            if (k.includes('_')) {

                _splitHeaderText = k.toString().split('_');

                headers += '<th>' + _splitHeaderText[0] + '<br/>' + _splitHeaderText[1]+' </th>';

            }
            else {
                headers += '<th>' + k + '</th>';
            }

            
            colpsan += 1;
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
    $('.theadJV').children().not('.clsReportH').hide();
    $('.theadJV .info ').remove();
    $('.theadJV').append(headers);
    $('.tbodyJV').html(data);
    var ReportName = "JV Report";
    if ($("#chkSummary").is(':checked')) {
        ReportName =  "JV Summary Report";
    }
    SetReportHeader(ReportName, colpsan, '');
    $('.div_reportbutton').show();
    ProgressBarHide();
}

function replaceZeroAndNull(val) {
    if (val == "0" || val == null)
        return "-";
    else {
        //if ($.isNumeric(val))
        //    return parseInt(val);
        //else
            return val;
    }
}

function ClearReport() {
    $('.clsReportH').hide();
    $('.HeaderColoumn').remove();
    $('.tbodyJV').html('');
    $('.div_reportbutton').hide();
}