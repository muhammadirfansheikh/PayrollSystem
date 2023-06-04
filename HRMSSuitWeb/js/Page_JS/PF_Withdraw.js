

var employeeId;

function BindWithdrawHistory(employee) {
    employeeId = employee;
    GetWithdrawHistory(employee);
}

function shoutEmployeeId() {
    alert(employeeId);
}


function TriggerLoad() {

   
    GetGroup();

    $('.btnSearch').click(function () {
        return false;
    });

}


function GetEmployee() {

   

    Group = $(".ddlGroup").val();
    Company = $(".ddlCompany").val();
    Location = $(".ddlLocation").val();
    BU = $(".ddlBU").val();
    Department = $(".ddlDepartment").val();
    CostCenter = $(".ddlCostCenter").val();
    Designation = $(".ddlDesignation").val();
    Firstname = $(".txtFirstName").val();
    Lastname = $(".txtLastName").val();
    EmpCode = $(".txtEmployeeCode").val();
    var service = new HrmsSuiteHcmService.HcmService();
    service.getEmployee(Group, Company, Location, BU, CostCenter, Designation, Department, Firstname, Lastname, EmpCode, onGetEmployeePfWd, null, null);

}

function onGetEmployeePfWd(result) { 
    var res = jQuery.parseJSON(result);
    alert(result);
    var divTbodyGoalFund = $('.tBodyDetail').html('');
    $('#wfForm').tmpl(res).appendTo(divTbodyGoalFund);
}


function GetWithdrawHistory(EmployeeId) {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getWithdrawHistory(EmployeeId, onGetWithdrawHistory, null, null);
}

function onGetWithdrawHistory(result) {
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.tablewithdrawHistory').html('');
    $('#bindWithdrawHistory').tmpl(res).appendTo(divTbodyGoalFund);
    $('.historyTotal').text(historyAmountTotal());

}

function historyAmountTotal()
{
    var val = 0;
    $('.historyAmount').each(function () {
        val = val + parseFloat($(this).text());
    }
    );
    return val;
}

function withdraw() {
    var txtAmt = $('.txtAmountWithdraw').val();
    SaveWithdraw(employeeId, txtAmt);
    $('.txtAmountWithdraw').val('');
}


function SaveWithdraw() { 

    var service = new HrmsSuiteHcmService.HcmService();
    service.insertWithdraw(EmployeeId, onSaveWithdraw, onFailure, null);
}

function onSaveWithdraw(result) {
    var res = jQuery.parseJSON(result);
    if (res == '1') {
        showSuccess('Provident Fund Withdrawl Success');
        GetWithdrawHistory(employeeId);
    }
}


function onFailure()
{
    showError('Please Check All the Required Fields');
}



function showError(message) {
    AlertBox('Error!', message, 'error');
}

function showSuccess(message) {
    AlertBox('Success!', message, 'success');
}

function AlertBox(title, Message, type) {
    swal(title, Message, type);
}

