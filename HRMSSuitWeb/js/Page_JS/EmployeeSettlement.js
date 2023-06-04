

var Response = [];

function TriggerPageLoads() {

    //toggleDiv('.divInstallment');
    //toggleDiv('.divCheque');
    //toggleDiv('.divIncrementMonth');
    //toggleDiv('.divVehicleDifference');
    //toggleDiv('.divIncrementPercentage');

    //TriggerPageLoadsLoan();

    GetNoticePeriod();
    GetGroupInsuranceType();
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

    var res = jQuery.parseJSON(result);

    ResponseForm = [];

    $(res).each(function (k, v) {
        //if (v.EmployeeIdSettlement != null)
        //{
        //    if ($('.chkIsSettled').is(':checked')) {
        //        if (v.IsSettled == 2) {
        //            ResponseForm.push(v);
        //        }
        //    }
        //    else {
        //        if (v.IsSettled == 1) {
        //            ResponseForm.push(v);
        //        }
        //    }
        //}
        ResponseForm.push(v);
        //if (v.ResignedDate == null)
        //{
        //   // $(`.${v.EmployeeId}`).prop('checked', true);
        //    $('.finalset').prop('checked', true);
        //}
        //if (v.IsFinalSettlement == 1) {  }
       // $(".finalset").prop("disabled", true);
    });

    var divTbodyGoalFund = $('.tbodyEmployeeListing').html('');
    $('#EmployeeListing').tmpl(ResponseForm).appendTo(divTbodyGoalFund);
    paginateTable('.tableEmployee', 50);
    ProgressBarHide();

}

function changefinalsettlement(EmployeeId, CompanyId) {
    debugger
    var service = new HrmsSuiteHcmService.HcmService(); 
    service.isfinalsettlement(EmployeeId, CompanyId, onchangefinalsettlement, null, null);
}

function onchangefinalsettlement(result) {
    debugger
    ProgressBarHide();
    if (result == "1") {
        showSuccess("Saved Successfully");
        GetEmployee();
    }
    else {
        showError("Error");
    }
}

function GetFinalSettlement(_EmployeeId, _IsSettled, _CompanyId) {
    
    EmployeeId = _EmployeeId;
    CompanyId = _CompanyId;
   

    var service = new HrmsSuiteHcmService.HcmService();

    if (_IsSettled == 2) {
        //$(".btnManage").val('View');
        $(".btnReset").hide();
        $(".btnSave").hide();
        $(".btnGetData").hide();
        service.getFinalSettlement(_EmployeeId, onViewEmployeeSettlement, null, null);
    }
    else {
        //$(".btnManage").val('Manage');
        $(".btnReset").show();
        $(".btnSave").show();
        service.getFinalSettlement(_EmployeeId,  onGetEmployeeSettlementSaved, null, null);
    }
}

function onViewEmployeeSettlement(result) {

    var res = jQuery.parseJSON(result);

    $(res).each(function (k, v) {

        Response.push(v);

        $('.txtNetAmountPaid').val(v.FinalAmount);
        $('.txtStopSalaryDate').val(v.StopSalaryDate);
        $('.txtResignedDate').val(v.ResignedDate);
        $('.txtLastWorkingDate').val(v.LastDate);
        $('.ddlNoticePeriodType').val(v.NoticePeriodTypeId);
        $('.txtCarMarketValue').val(v.CarMarketValue);
        $('.txtPfAmount').val(v.PfAmount);
        $('.txtLoanBalanceAmount').val(v.LoanBalanceAmount);
        $('.txtArrearAmount').val(v.TotalArrearAmount);

        $('.hfBasicSalary').val(v.BasicSalary);
        $('.hfSettlementId').val(v.SettlementId);
        $('.txtTotalLeavesRemaining').val(v.TotalLeaves);
        $('.txtTotalLeaveEncashment').val(v.LeaveEncashment);
        $('.txtCarTotalPaid').val(v.CarTotalPaid);
        $('.txtCarTotalPayable').val(v.CarTotalPayable);
        $('.txtOtherPayable').val(v.OtherPayable);
        $('.txtOtherDesc').val(v.OtherDesc);

        $('.ddlGroupInsType').val(v.GroupInsTypeId);
        $('.txtGroupInsAmount').val(v.GroupInsAmount);
        $('.txtBonusRecovery').val(v.BonusRecovery);
        $('.txtPfChequeNumber').val(v.PfChequeNumber);
        $('.txtPfChequeDate').val(v.PfChequeDate);
        $('.txtSettlementChequeNumber').val(v.SettlementChequeNumber);
        $('.txtSettlementChequeDate').val(v.SettlementChequeDate);
        $('.txtEmployeePayableAmount').val(v.EmployeePayableAmount);

        $('.txtEmployeeChequeNumber').val(v.EmployeePayableChequeNumber);
        $('.txtEmployeeChequeDate').val(v.EmployeePayableChequeDate);

        $('.chkIsTakeHome').prop("checked", v.CarTakeHome);
        $('.chkIsSettledPopup').prop("checked", v.IsSettled);
        $('.txtDeductedAmount').val(v.DeductedAmount);

        //$('.txtBonusRecovery').val(v.Arrears);
        //$('.txtPfChequeNumber').val(v.VehiclePaid);
        //$('.txtPfChequeDate').val(v.Vehicle_Payable);
        //$('.txtSettlementChequeNumber').val(v.BonusRecovery);
        //$('.txtSettlementChequeDate').val(v.Basic_Sal);
        //$('.txtEmployeePayableAmount').val(v.SettlementId);

    });

    CalculateTextBoxOnChange();

    $("#Div3 :input").prop("disabled", true);
    $("#Div4 :input").prop("disabled", true);
    $("#Div5 :input").prop("disabled", true);
    $("#Div6 :input").prop("disabled", true);

}

function onGetEmployeeSettlementSaved(result) {

    var res = jQuery.parseJSON(result);

    $(res).each(function (k, v) {

        Response.push(v);

        $('.txtNetAmountPaid').val(v.FinalAmount);
        $('.txtStopSalaryDate').val(v.StopSalaryDate);
        $('.txtResignedDate').val(v.ResignedDate);
        $('.txtLastWorkingDate').val(v.LastDate);
        $('.ddlNoticePeriodType').val(v.NoticePeriodTypeId);
        $('.txtCarMarketValue').val(v.CarMarketValue);
        $('.txtPfAmount').val(v.PfAmount);
        $('.txtLoanBalanceAmount').val(v.LoanBalanceAmount);
        $('.txtArrearAmount').val(v.TotalArrearAmount);

        $('.hfBasicSalary').val(v.BasicSalary);
        $('.hfSettlementId').val(v.SettlementId);
        $('.txtTotalLeavesRemaining').val(v.TotalLeaves);
        $('.txtTotalLeaveEncashment').val(v.LeaveEncashment);
        $('.txtCarTotalPaid').val(v.CarTotalPaid);
        $('.txtCarTotalPayable').val(v.CarTotalPayable);
        $('.txtOtherPayable').val(v.OtherPayable);
        $('.txtOtherDesc').val(v.OtherDesc);

        $('.ddlGroupInsType').val(v.GroupInsTypeId);
        $('.txtGroupInsAmount').val(v.GroupInsAmount);
        $('.txtBonusRecovery').val(v.BonusRecovery);
        $('.txtPfChequeNumber').val(v.PfChequeNumber);
        $('.txtPfChequeDate').val(v.PfChequeDate);
        $('.txtSettlementChequeNumber').val(v.SettlementChequeNumber);
        $('.txtSettlementChequeDate').val(v.SettlementChequeDate);
        $('.txtEmployeePayableAmount').val(v.EmployeePayableAmount);

        $('.txtEmployeeChequeNumber').val(v.EmployeePayableChequeNumber);
        $('.txtEmployeeChequeDate').val(v.EmployeePayableChequeDate);

        $('.chkIsTakeHome').prop("checked", v.IsCarTakeHome);
        $('.chkIsSettledPopup').prop("checked", v.IsSettled);
        $('.txtDeductedAmount').val(v.DeductedAmount);

        //$('.txtBonusRecovery').val(v.Arrears);
        //$('.txtPfChequeNumber').val(v.VehiclePaid);
        //$('.txtPfChequeDate').val(v.Vehicle_Payable);
        //$('.txtSettlementChequeNumber').val(v.BonusRecovery);
        //$('.txtSettlementChequeDate').val(v.Basic_Sal);
        //$('.txtEmployeePayableAmount').val(v.SettlementId);

    });

    CalculateTextBoxOnChange();
}

function onGetEmployeeSettlement(result) {

    var res = jQuery.parseJSON(result);

    $(res).each(function (k, v) {

        Response.push(v);

        $('.txtPfAmount').val(v.PF_Balance);
        $('.txtLoanBalanceAmount').val(v.LoanPayable);
        $('.txtArrearAmount').val(v.Arrears);
        $('.txtCarTotalPaid').val(v.VehiclePaid);
        $('.txtCarTotalPayable').val(v.Vehicle_Payable);
        $('.txtBonusRecovery').val(v.BonusRecovery);
        $('.hfBasicSalary').val(v.Basic_Sal);
        $('.hfSettlementId').val(v.SettlementId);
        $('.txtGratuity').val(v.Gratuity);
        $('.txtResignedDate').val(v.ResignDate);
        $('.txtLastWorkingDate').val(v.LastWorking);

        $('.txtTotalLeavesRemaining').val(0);
        CalculateLeaveEncashment('.txtTotalLeavesRemaining');

        $('.txtGroupInsAmount').val(0);

        $('.txtOtherPayable').val(0);
        CalculateTextBoxOnChange();

    });
    
    CalculateTextBoxOnChange();
}

function GetNoticePeriod() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getNoticePeriodType(onGetNoticePeriodType, null, null);
}

function onGetNoticePeriodType(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlNoticePeriodType", res);

}

function GetGroupInsuranceType() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getGroupInsuranceType(onGetGroupInsuranceType, null, null);
}

function onGetGroupInsuranceType(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlGroupInsType", res);

}

function GetData() {

    $(".btnManage").val('Manage');
    $(".btnReset").show();
    $(".btnSave").show();

    var service = new HrmsSuiteHcmService.HcmService();
    service.getEmployeeSettlement(EmployeeId, CompanyId, onGetEmployeeSettlement, null, null);
}

function CalculateLeaveEncashment(selector)
{
    var BasicSalary = $('.hfBasicSalary').val();
    var TotalLeaves = $(selector).val();

    $('.txtTotalLeaveEncashment').val(TotalLeaves * (BasicSalary / 30));

    CalculateTextBoxOnChange();
}

function Save()
{

    var SettlementId = $('.hfSettlementId').val();
    var PFAmount = $('.txtPfAmount').val();
    var LoanBalanceAmount = $('.txtLoanBalanceAmount').val();
    var ArrearAmount = $('.txtArrearAmount').val();

    var LeavesRemaining = $('.txtTotalLeavesRemaining').val();
    var LeaveEncashment = $('.txtTotalLeaveEncashment').val();
    var CarMktValue = $('.txtCarMarketValue').val();

    var CarTotalValue = $('.txtCarTotalPayable').val();
    var CarTotalPaid = $('.txtCarTotalPaid').val();
    var OtherPayable = $('.txtOtherPayable').val();

    var OtherDesc = $('.txtOtherDesc').val();
    var GroupInsTypeId = $('.ddlGroupInsType').val();
    var GroupInsAmount = $('.txtGroupInsAmount').val();

    var BonusRecovery = $('.txtBonusRecovery').val();
    var PfChequeNumber = $('.txtPfChequeNumber').val();
    var PfChequeDate = $('.txtPfChequeDate').val();

    var SettlementChequeNumber = $('.txtSettlementChequeNumber').val();
    var SettlementChequeDate = $('.txtSettlementChequeDate').val();
    var EmployeePayableAmount = $('.txtEmployeePayableAmount').val();

    var EmployeePayableChequeNumber = $('.txtEmployeeChequeNumber').val();
    var EmployeePayableChequeDate = $('.txtEmployeeChequeDate').val();
    //var Final_Amount = $('.txtArrearAmount').val();

    var ResignedDate = $('.txtResignedDate').val();
    var LastWorkingDate = $('.txtLastWorkingDate').val();
    var StopSalaryDate = $('.txtStopSalaryDate').val();
    var NoticePeriodType = $('.ddlNoticePeriodType').val();

    var _EmployeeId = EmployeeId;

    var IsCarTakeHome = $(".chkIsTakeHome").prop('checked');
    var IsSettled = $(".chkIsSettledPopup").prop('checked');

    var BasicSalary = $('.hfBasicSalary').val();
    var FinalAmount = $('.txtNetAmountPaid').val();

    var DeductedAmount = $('.txtDeductedAmount').val();
    
    IsSettledGlobal = IsSettled;

    var service = new HrmsSuiteHcmService.HcmService();

    service.saveEmployeeFinalSettlement(SettlementId, PFAmount, LoanBalanceAmount, ArrearAmount, LeavesRemaining,LeaveEncashment, CarMktValue, CarTotalValue, CarTotalPaid,
        OtherPayable, OtherDesc, GroupInsTypeId, GroupInsAmount, BonusRecovery, PfChequeNumber, PfChequeDate, SettlementChequeNumber, SettlementChequeDate, EmployeePayableAmount,
        EmployeePayableChequeNumber, EmployeePayableChequeNumber, FinalAmount, ResignedDate, LastWorkingDate, NoticePeriodType, _EmployeeId, IsCarTakeHome, IsSettled, BasicSalary,
        StopSalaryDate, DeductedAmount, onSaveEmployeeFinalSettlement, null, null);
}

function onSaveEmployeeFinalSettlement(result)
{
    if (result == "1")
    {
        if (IsSettledGlobal == "true") {
            $("#Div3 :input").prop("disabled", true);
            $("#Div4 :input").prop("disabled", true);
            $("#Div5 :input").prop("disabled", true);
            $("#Div6 :input").prop("disabled", true);
        }
        ClearFields();
        showSuccess("Saved Successfully");
        //;
        //GetFinalSettlement(EmployeeId, IsSettledGlobal, CompanyId);
    }
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

function TotalAmountPaid()
{

    var Total = parseFloat($('.txtPfAmount').val()) + parseFloat($('.txtGratuity').val()) + parseFloat($('.txtArrearAmount').val()) + parseFloat($('.txtTotalLeaveEncashment').val()) + 
        parseFloat($('.txtGroupInsAmount').val()) - parseFloat($('.txtDeductedAmount').val());

    $('.txtTotalAmountPaid').val(Total);

    FinalAmount();
}

function TotalAmountPayable() {
    var Total = parseFloat($('.txtLoanBalanceAmount').val()) + parseFloat($('.txtCarTotalPayable').val()) + parseFloat($('.txtOtherPayable').val()) + parseFloat($('.txtBonusRecovery').val());

    $('.txtEmployeePayableAmount').val(Total);

    FinalAmount();
}

function FinalAmount()
{
    var Total = parseFloat($('.txtTotalAmountPaid').val()) - parseFloat($('.txtEmployeePayableAmount').val());

    $('.txtNetAmountPaid').val(Total);
}

function CalculateTextBoxOnChange()
{
    TotalAmountPaid();
    //CarTotalPayable();
    TotalAmountPayable();
}

function ClearFields()
{
    $('.txtStopSalaryDate').val('');
    $('.txtResignedDate').val('');
    $('.txtLastWorkingDate').val('');
    $('.ddlNoticePeriodType').val(0);
    $('.txtCarMarketValue').val('0');
    $('.txtPfAmount').val('0');
    $('.txtGratuity').val('0');
    $('.txtArrearAmount').val('0');
    $('.txtTotalLeavesRemaining').val('0');
    $('.txtTotalLeaveEncashment').val('0');
    $('.ddlGroupInsType').val(0);
    $('.txtGroupInsAmount').val('0');
    $('.txtDeductedAmount').val('0');
    $('.txtTotalAmountPaid').val('0');


    $('.txtLoanBalanceAmount').val('0');
    $('.txtCarTotalPaid').val('0');
    $('.txtCarTotalPayable').val('0');
    $('.txtOtherPayable').val('0');
    $('.txtOtherDesc').val('');
    $('.txtBonusRecovery').val('0');
    $('.txtEmployeePayableAmount').val('0');

    $('.txtEmployeeChequeNumber').val('');
    $('.txtEmployeeChequeDate').val('');
    $('.txtPfChequeNumber').val('');
    $('.txtPfChequeDate').val('');
    $('.txtSettlementChequeNumber').val('');
    $('.txtSettlementChequeDate').val('');

    $('.txtNetAmountPaid').val('0');
}

function CarTotalPayable()
{
    var Total = parseFloat($('.txtCarMarketValue').val()) - parseFloat($('.txtCarTotalPaid').val());

    $('.txtCarTotalPayable').val(Total);

    CalculateTextBoxOnChange();
}

function CheckIsTakeHome()
{
    var isDisabled = $('.txtCarMarketValue').prop('disabled');
    
    if (isDisabled == false) {
        
        $('.txtCarMarketValue').prop("disabled", true);
        $('.txtCarMarketValue').val(0);
        CarTotalPayable();
    }
    else {
        $('.txtCarMarketValue').prop("disabled", false);
    }
}

function SetPopupHeading(value, _EmployeeId)
{
    $("#lblResignPop").text(value);

    EmployeeId = _EmployeeId;
}

function SaveDates()
{
    var ResignedDate = $('.txtResignedDatePop').val();
    var LastWorkingDate = $('.txtLastWorkingDatePop').val();
    var StopSalaryDate = $('.txtStopSalaryDatePop').val();

    ProgressBarShow();
    var service = new HrmsSuiteHcmService.HcmService();
    service.SaveResignInfoDates(EmployeeId,StopSalaryDate, ResignedDate, LastWorkingDate, onSaveResignInfoDates, null, null);
}

function onSaveResignInfoDates(result)
{
    ProgressBarHide();
    if (result == "1") {

        showSuccess("Saved Successfully");
        //;
  
        GetEmployee();
    }
}


function MarkRevokeFinalSettlement(_EmployeeId) {
   
    let EmployeeId = _EmployeeId;
    ProgressBarShow();
    var service = new HrmsSuiteHcmService.HcmService();
    service.RevokeResignDates(EmployeeId,onMarkRevokeFinalSettlement, null, null);
}

function onMarkRevokeFinalSettlement(result) {
    
    var res = jQuery.parseJSON(result);
    ProgressBarHide();
    if (res.ResponseMessageType == 1) {
        GetEmployee();
        showSuccess(res.ResponseMessage);
        //;

    }
    else {
        showError(res.ResponseMessage);
    }
}


