
var EmployeeId = '';

function TriggerPageLoads() {


}

function GetIncrementedEmployees() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.IncrementedEmployeesSalaryForcast($('.ddlCompany').val(), onGetIncrementedEmployees, null, null);
}

function onGetIncrementedEmployees(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlEmployee", res);
    //$(".ddlTaxYear").val(res[0].Id);
}

function SaveSalaryForcast() {

    if (!validateForm('.dvEntry'))
        return;

    var CompanyId = $('.ddlCompany').val();
    var EmpId = $('.ddlEmployee').val();

    var service = new HrmsSuiteHcmService.HcmService();
    service.SalaryForcasterIncrement(CompanyId, EmpId, UserId/*, UserIP,*/, onSaveSalaryForcast, null, null);
}

function onSaveSalaryForcast(result) {

    //var res = jQuery.parseJSON(result);

    if (result.length > 0) {
        //showSuccess('Save Successfully!');

        if (result == "1") {
            showSuccess('Save Successfully!');
        }
        else {
            //showSuccess('Save Successfully!');
        }
    }

    ProgressBarHide();
}

function ClearFields() {
    $('.ddlGroup').val(0);
    $('.ddlCompany').val(0);
    $('.ddlEmployee').val(0);
}



