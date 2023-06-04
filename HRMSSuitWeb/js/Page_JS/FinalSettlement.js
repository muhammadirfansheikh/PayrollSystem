

var Response = [];

function TriggerPageLoads() {

    //toggleDiv('.divInstallment');
    //toggleDiv('.divCheque');
    //toggleDiv('.divIncrementMonth');
    //toggleDiv('.divVehicleDifference');
    //toggleDiv('.divIncrementPercentage');



    //TriggerPageLoadsLoan();
}

function GetEmployee() {
    ProgressBarShow();
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
    Categoryid = $(".ddlCategoryC").val();



    var service = new HrmsSuiteHcmService.HcmService();
    service.getEmployee(Group, Company, Location, BU, CostCenter, Designation, Department, Firstname, Lastname, EmpCode, Categoryid, onGetEmployee, null, null);

}

function onGetEmployee(result) {
    debugger
    var res = jQuery.parseJSON(result);
    ResponseForm = [];

    $(res).each(function (k, v) {
        if (v.EmployeeIdSettlement != null) {

            if ($('.chkIsSettled').is(':checked')) {
                if (v.IsSettled == 2) {
                    ResponseForm.push(v);
                }
            }
            else {
                if (v.IsSettled == 1) {
                    ResponseForm.push(v);
                }
            }
        }
        $(".finalset").prop("disabled", true);
    });

    var divTbodyGoalFund = $('.tbodyEmployeeListing').html('');
    $('#EmployeeListing').tmpl(ResponseForm).appendTo(divTbodyGoalFund);
    paginateTable('.tableEmployee', 50);
    ProgressBarHide();


}


function formatDate(datetime) {
    var dateObj = new Date(datetime);
    var dateStr = (dateObj.getMonth() + 1) + "/" + dateObj.getDate() + "/" + dateObj.getFullYear();
    return dateStr; // will return mm/dd/yyyy
}
function GetFinalSettlement(_EmployeeId, _IsSettled, _CompanyId) {

   
    EmployeeId = _EmployeeId;
    CompanyId = _CompanyId;

 
    GetAllowanceToCalculateLeaveEncashment();

    var service = new HrmsSuiteHcmService.HcmService();

    if (_IsSettled == 2) {
        $(".btnManage").val('View');
        $(".btnReset").hide();
        $(".btnSave").hide();
        service.viewEmployeeSettlement(_EmployeeId, onViewEmployeeSettlement, null, null);
    }
    else {
        $(".btnManage").val('Manage');
        $(".btnReset").show();
        $(".btnSave").show();
        service.getFinalSettlementAmount(_EmployeeId, onGetFinalSettlement, null, null);
    }
}

function onViewEmployeeSettlement(result) {

    var res = jQuery.parseJSON(result);

    $(res).each(function (k, v) {
       
        $('.chkIsTakeHome').prop("checked", v.IsVehicleTakeHome);

        //$('.chkIsOwnershipDeduction').prop("checked", v.IsOwnerShipDeduction);
        //$('.chkIsUpgraded').prop("checked", v.IsUpgraded);
        $('.txtPurchaseAmount').val(v.VehiclePurhaseAmont);
        $('.txtUpgradedAmount').val(v.VehicleUpgradedAmount);
        $('.txtBalanceAmount').val(v.BalanceAmount);
        $('.txtRecvDeductedAmount').val(v.ReceiveableDeductedAmount);
        $('.txtTotalAmountVehicle').val(v.TotalVehicleAmount);

        $('.txtPfAmount').val(v.PFAmount);
        $('.txtLoanBalanceAmount').val(v.LoanBalanceAmount);
        $('.txtArrearAmount').val(v.ArrearAmount);
        $('.txtTotalAllowanceAmount').val(v.BasicSalary);
        $('.txtTotalLeavesRemaining').val(v.LeavesRemaining);
        $('.txtTotalLeaveEncashment').val(v.LeaveEncashment);
        $('.txtTotal').val(v.TotalAmount);
        //$('.txtChequeNumber').val(v.ArrearAmount);

        $('.ddlLeaveEncashmentAllowance').prop("disabled", true);
        $('.ddlLeaveEncashmentAllowance').val(v.AllowanceId);

        $('.chkIsTakeHome').prop("disabled", true);
        $('.txtTotalLeavesRemaining').prop("disabled", true);
        $('.txtChequeNumber').prop("disabled", true);


    });
}

function onGetFinalSettlement(result) {

    var res = jQuery.parseJSON(result);

    $(res).each(function (k, v) {

        Response.push(v);

        //CompanyId = v.CompanyId;
        SettlementId = v.SettlementId;

        ClearFields();

        $('.chkIsOwnershipDeduction').prop("checked", v.IsOwnerShipDeduction);
        $('.chkIsUpgraded').prop("checked", v.IsUpgraded);
        $('.txtUpgradedAmount').val(v.UpgradedAmount);
        $('.txtBalanceAmount').val(v.VehicleBalance);
        $('.txtPurchaseAmount').val(v.PurchaseAmount);
        $('.hfBalanceAmount').val(v.VehicleBalance);
        $('.txtPfAmount').val(v.PfBalance);
        $('.txtLoanBalanceAmount').val(v.LoanBalance);
        $('.txtArrearAmount').val(v.TotalArrearAmount);
        $('.txtTotalLeavesRemaining').val(v.AnnualLeavesRemaining);

        if (v.IsOwnerShipDeduction == true) {

            $('.chkIsTakeHome').prop("checked", true);
            $('.txtRecvDeductedAmount').val(0);
        }
        GetTotalVehicleAmount();
        GetAllowanceToCalculateLeaveEncashment();
        //GetTotalAmountFinalSettlement();


        GetTotalLeaveEncashment();
        GetTotalAmountFinalSettlement();
    });
}

function GetTotalVehicleAmount() {
    var TotalVehicleAmount = parseFloat($('.txtUpgradedAmount').val()) - parseFloat($('.txtBalanceAmount').val()) + parseFloat($('.txtRecvDeductedAmount').val());
    $('.txtTotalAmountVehicle').val(TotalVehicleAmount);
}

function CheckIsTakeHome() {

    if ($('.chkIsTakeHome').is(':checked')) {

        $(Response).each(function (k, v) {
            // alert(v.VehicleBalance);
            $('.txtBalanceAmount').val(v.VehicleBalance);
        });
        $('.txtRecvDeductedAmount').val(0);


    }
    else {
        $('.txtBalanceAmount').val(0);

        var RecvDeductAmount = parseFloat($('.txtPurchaseAmount').val()) - parseFloat($('.hfBalanceAmount').val());
        $('.txtRecvDeductedAmount').val(RecvDeductAmount);
    }

    GetTotalVehicleAmount();
}

function GetAllowanceToCalculateLeaveEncashment() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getAllowances(CompanyId, "0", onGetAllowanceToCalculateLeaveEncashment, null, null);
}

function onGetAllowanceToCalculateLeaveEncashment(result) {

    var res = jQuery.parseJSON(result);

    FillDropDownByReference('.ddlLeaveEncashmentAllowance', res);
}

function GetAllowanceAmount() {
    var AllowanceId = $('.ddlLeaveEncashmentAllowance').val();
    var service = new HrmsSuiteHcmService.HcmService();
    service.GetAllowanceAmount(EmployeeId, AllowanceId, onGetAllowanceAmount, null, null);
}

function onGetAllowanceAmount(result) {

    $('.txtTotalAllowanceAmount').val(result);

    GetTotalLeaveEncashment();
    GetTotalAmountFinalSettlement();

}

function GetTotalLeaveEncashment() {
    var TotalLeavesRemaining = $('.txtTotalLeavesRemaining').val();
    var TotalBasicSalaryAmount = $('.txtTotalAllowanceAmount').val();
    var PerDayBasicSalary = parseFloat(TotalBasicSalaryAmount) / 30;

    var TotalLeaveEncashment = parseFloat(PerDayBasicSalary) * parseFloat(TotalLeavesRemaining);

    $('.txtTotalLeaveEncashment').val(TotalLeaveEncashment);
}

function GetTotalAmountFinalSettlement() {
    var TotalAmount = parseFloat($('.txtTotalAmountVehicle').val()) + parseFloat($('.txtPfAmount').val()) -
        parseFloat($('.txtLoanBalanceAmount').val()) + parseFloat($('.txtArrearAmount').val()) + parseFloat($('.txtTotalLeaveEncashment').val());
    $('.txtTotal').val(TotalAmount);
}

function SaveFinalSettlement() {

    if (!validateForm('.dvEntry'))
        return;

    IsVehicleTakeHome = $(".chkIsTakeHome").prop('checked');
    VehiclePurchaseAmount = $(".txtPurchaseAmount").val();
    VehicleUpgradedAmount = $(".txtUpgradedAmount").val();
    VehicleBalanceAmount = $(".txtBalanceAmount").val();
    RecvDeducAmount = $(".txtRecvDeductedAmount").val();
    TotalVehicleAmount = $(".txtTotalAmountVehicle").val();
    PFAmount = $(".txtPfAmount").val();
    LoanBalanceAmount = $(".txtLoanBalanceAmount").val();
    ArrearAmount = $(".txtArrearAmount").val();
    BasicSalary = $(".txtTotalAllowanceAmount").val();
    LeavesRemaining = $(".txtTotalLeavesRemaining").val();
    LeaveEncashment = $(".txtTotalLeaveEncashment").val();
    TotalAmount = $(".txtTotal").val();

    var service = new HrmsSuiteHcmService.HcmService();
    service.saveFinalSettlement(parseInt(SettlementId), IsVehicleTakeHome, parseFloat(VehiclePurchaseAmount), parseFloat(VehicleUpgradedAmount), parseFloat(VehicleBalanceAmount),
         parseFloat(RecvDeducAmount), parseFloat(TotalVehicleAmount), parseFloat(PFAmount), parseFloat(LoanBalanceAmount), parseFloat(ArrearAmount), parseFloat(BasicSalary),
        parseInt(LeavesRemaining), parseFloat(LeaveEncashment), parseFloat(TotalAmount), onSaveFinalSettlement, null, null);
}

function onSaveFinalSettlement(result) {

    if (result == 1) {
        showSuccess('Settlement Saved Successfully');
    }
    else {

    }

}

function ClearFields() {

    $('.txtChequeNumber').removeAttr('disabled');
    $('.chkIsTakeHome').removeAttr('disabled');
    $('.ddlLeaveEncashmentAllowance').removeAttr('disabled');
    $('.txtTotalLeavesRemaining').removeAttr('disabled');

    $('.chkIsOwnershipDeduction').prop("checked", false);
    $('.chkIsUpgraded').prop("checked", false);
    $('.txtUpgradedAmount').val(0);
    $('.txtBalanceAmount').val(0);
    $('.txtPurchaseAmount').val(0);
    $('.hfBalanceAmount').val(0);
    $('.txtPfAmount').val(0);
    $('.txtLoanBalanceAmount').val(0);
    $('.txtArrearAmount').val(0);
    $('.chkIsTakeHome').prop("checked", false);
    $('.txtRecvDeductedAmount').val(0);

}