

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
    service.report_EobiEmpList(EmployeeCode, GroupId, CompanyId, LocationId, BusinessUnitId, DepartmentId, CostCenterId,
        CategoryId, DesignationId, Firstname, Lastname, PayrollMonth,  onGetReportEOBIEmp, null, null);
}

function onGetReportEOBIEmp(result) {
    var GroupByValue = $('.ddlGroupBy').val();

    var res = jQuery.parseJSON(result);

    if (GroupByValue != 0) {
        res = res.sort(sortByProperty(GroupByValue));
    }
    var divTbodyGoalFund = $('.tbodyEOBIEmpListing').html('');
    $('#EOBIEmpListing').tmpl(res).appendTo(divTbodyGoalFund);

   

    //SetReportHeader('EOBI Employee Report', 7, Para2);

    ProgressBarHide();
}

function TriggerPageLoads() {
    
    BindGroupByDDL();
   
   // $('.ddlGroupBy option').filter('[value="clsCompany"],[value="clsDesignation"],[value="clsDepartment"]').remove();
}



//36210