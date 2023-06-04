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
    service.report_SalaryRegister(EmployeeCode, GroupId, CompanyId, LocationId, BusinessUnitId, DepartmentId, CostCenterId, CategoryId, DesignationId, Firstname, Lastname, PayrollMonth,0, onGetReportSalaryRegister, null, null);
}


function onGetReportSalaryRegister(result) {

    var result = result.split("#SPLIT#");
    var res = jQuery.parseJSON(result[0]);
    var res2 = jQuery.parseJSON(result[1]);
    var res3 = jQuery.parseJSON(result[2]);
    var resMaster = jQuery.parseJSON(result[3]);

    var divTbodyGoalFund = $('.tbodyAllowances').html('');
    $('#Allowances').tmpl(res2).appendTo(divTbodyGoalFund);

    var divTbodyGoalFund = $('.tbodyDeduction').html('');
    $('#Deduction').tmpl(res3).appendTo(divTbodyGoalFund);

    var AllowanceTotal = 0;
    var DeductionTotal = 0;
    var MasterTotal = 0;

    $('.AllowanceAmount').each(function () {
        AllowanceTotal += parseFloat($(this).text().trim());
    });

    $('.DeductionAmount').each(function () {
        DeductionTotal += parseFloat($(this).text().trim());
    });

    var PropotionateCount = 0;
    $(resMaster).each(function (k, v) {
        MasterTotal += (v.DifferenceAmount);

        PropotionateCount++;
    });

    $('.tdTotal').text(AllowanceTotal);
    $('.tdTotalDeduction').text(DeductionTotal);
    //$('.tdPropRecovery').text(MasterTotal + ' For Employees ( ' + PropotionateCount + ' )');

    if (PropotionateCount > 0) {
        $('.tdPropRecovery').text((parseFloat(MasterTotal) - parseFloat(res2[0].FlexiSum)) + ' For Employees ( ' + PropotionateCount + ' )');
    }

    $('.tdTotalMaster').text(res2[0].MasterGross);
    $('.tdNetSalary').text(AllowanceTotal - DeductionTotal);

    if (parseFloat(res2[0].FlexiEmpCount) > 0) {
        $('.tdFlexiSum').text(res2[0].FlexiSum + ' For Employees ( ' + res2[0].FlexiEmpCount + ' )')
    }
    else {

    }

    $('.tdEOBI').text(res2[0].EOBI + ' For Employees ( ' + res2[0].EOBIEmployee + ' )');
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

    $('.theadSalaryRegister').children().not('.clsReportH').hide();
    //$('.theadSalaryRegister').html('');
    $('.tbodySalaryRegister').html('');

    $('.theadSalaryRegister').append(headers);
    $('.tbodySalaryRegister').append(data);

    SetReportHeader('Salary Register  Report', 15, '');
    SetReportHeader('Salary Register Report', 15, '');

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

}