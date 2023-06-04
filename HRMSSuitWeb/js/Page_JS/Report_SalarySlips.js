
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
    service.report_SalarySlips(EmployeeCode, GroupId, CompanyId, LocationId, BusinessUnitId, DepartmentId, CostCenterId, CategoryId, DesignationId, Firstname, Lastname, PayrollMonth, onGetTaxForecast, null, null);

}


function onGetTaxForecast(result) {
   
    var res = JSON.parse(result);
        var divTbodyGoalFund = $('.SalarySlips').html('');
        $('#SalarySlips').tmpl(res).appendTo(divTbodyGoalFund);

        $('.tableSalarySlip').each(function () {

            var TotalAllowances = 0;
            var TotalDeductions = 0;
            var OtherDetails = 0;

            $(this).find('.tdAllowances').each(function () {
                TotalAllowances += parseFloat($(this).text().trim());

                if (parseFloat($(this).text().trim()) < 0) {
                    $(this).text('( ' + parseFloat($(this).text().trim()) * (-1) + ' )');
                }
            });

            $(this).find('.tdDeduction').each(function () {
                TotalDeductions += parseFloat($(this).text().trim());
                if ($(this).text().trim() == '0')
                    $(this).text('');
            });

            $(this).find('.tdOtherDetails').each(function () {
                if ($(this).text().trim() == '0')
                    $(this).text('');
            });


            $(this).find('.tdTotalAllowances').text(TotalAllowances);
            $(this).find('.tdTotalDeduction').text(TotalDeductions);
            $(this).find('.tdNetSalary').text(TotalAllowances - TotalDeductions);

        });

    
    

   

    ProgressBarHide();
}


function TriggerPageLoads() {

}

