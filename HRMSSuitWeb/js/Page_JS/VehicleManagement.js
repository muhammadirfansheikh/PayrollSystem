
TriggerVehiclePageLoad();


var cssClas = '';
var EmployeeId = 0;
var origForm;
var CarResult;
var IsAdjusted = false;
_VMsId = 0;

function setEmployeeId(_EmployeeId, _CompanyId, selector) {

    EmployeeId = _EmployeeId;
    CompanyId = _CompanyId;
    //EmployeePFBalance = $(selector).closest('tr').find('.hdnPFBalance').val();

    /////////////////////////Vehicle//////////////////
    ResetControls();
    EmployeeName = $(selector).closest('tr').find('.tdEmployeeName').html();
    DesignationName = $(selector).closest('tr').find('.tdDesignation').html();
    InterestStandardId = $(selector).closest('tr').find('.hdnInterestStandard').val();
    EmployeeCode = $(selector).closest('tr').find('.tdEmployeeCode').html();
    $('.panelHeading').append(EmployeeName);
    $('.panelHeading1').append(EmployeeCode);
    $('.txtzakatempcode').val(EmployeeCode);
    $('.panelSubHeading').append(DesignationName);

    GetVehicleLastRecord();
    GetVehicleTransactionalHistory();
    $('.ddlCategory').val(0).change();

    //////////////////////////////////////////////////


    ///////////////////Arrear//////////////////
    GetArrearType();
    removeCloneDivs('.divArrearInputs');
    resetControls('.divArrearInputs');
    GetArrearHistory();
    ///////////////////////////////////////////


    ///////////////Allowances//////////////////
    GetMappedAllowances();
    //////////////////////////////////////////


    ///////////////Salary/////////////////////
    Get_Payroll_Lock_Count();
    GetSalaryStandard();
    GetEmployeeSalary();
    GetEmployeeSalaryHistory();

    GetSalaryChangeHistory();
    //////////////////////////////////////////



    ///////////PROVIDENT FUND/////////////////
    GetProvidentHistory();
    GetProvidentOpening();
    GetWithdrawHistory();
    /////////////////////////////////////////


    ///////////////LOAN/////////////////////
    GetInterestList();
    GetLoanMaster();
    //GetFromSetupDetail(0, HCM_SetupMaster.LoanType, '.ddlLoanType');
    GetLoanType();
    ////////////////////////////////////////


    ///////////////INCREMENT/////////////////////
    //GetIncrementType();
    ////////////////////////////////////////


    ////////////TAX////////////////////////
    GetTaxLawComputation();
    GetTaxDetail();
    GetTaxYear();
    //////////////////////////////////////
}

function ResetControls() {
    $('.Tab_').removeClass("active");
    $('.Tab_Salary').addClass("active");
    $('.panelHeading').html('');
    $('.panelHeading1').html('');
    $('.panelSubHeading').html('');
    $('.Payroll_Lock_Count').val('0');
    $(".txtSalary").prop('disabled', true);
    $(".btn_SaveSalary").hide();
    $(".txtSalaryInc").prop('disabled', true);
    $(".txtPercentage").prop('disabled', true);
    $(".ddlIncrementType").prop('disabled', true);
    $(".IncrementSalaryMonth").prop('disabled', true);
    $(".btn_SaveIncrementSalary").hide();
    $(".txtSalary").val('');
    $(".txtSalaryInc").val('');
    $(".txtPercentage").val('');
    $(".ddlIncrementType").val('0');
    $(".IncrementSalaryMonth").val('');
    $(".hdnStandard").val('');
    $(".txtSalaryStandard").val('');


    $(".UpgradeAmount").prop("disabled", true);
    $(".txtCarAllowance").prop("disabled", true);
    $(".txtInstAmt").prop("disabled", true);
    $(".txtCurrMonthInstallment").prop("disabled", true);
    $(".txtInsAdjTillDate").prop("disabled", true);
    _VMsId = 0;
    onRecordEdit = false;
    resetControls('.divControlsVehicle');
    $('#chkOwnerShip').attr('checked', false);
    $('.divInstallment').hide();
}

function onViewHistory(val) {

    window.open("LoanDetailHistory.aspx?LoanMasterID=" + val + "", "_blank");
}

function TriggerVehiclePageLoad() {

    $(".PurchaseAmountUpgraded").keyup(function () {

        UpgradeDiffrenceAmount();

    });

    $(".PurchaseAmount").keyup(function () {


        UpgradeDiffrenceAmount();



    });
}

function UpgradeDiffrenceAmount() {
    if ($('.ddlCategory').val() == '2') {
        let _PurValue = $('.PurchaseAmountUpgraded').val() == "" ? 0 : parseFloat($('.PurchaseAmountUpgraded').val());
        let _ElgPurValue = $('.PurchaseAmount').val() == "" ? 0 : parseFloat($('.PurchaseAmount').val());
        let _UpgDiffAmount = _PurValue - _ElgPurValue;


        $(".UpgradeAmount").val(_UpgDiffAmount);
    }

}

function toggle() {
    toggleDiv('.divInstallment')
}

function GetVehicleLastRecord() {

    ProgressBarShow();
    var service = new HrmsSuiteHcmService.HcmService();
    service.GetVehicleHistory(EmployeeId, onGetVehicleLastRecord, null, null);
}

function onGetVehicleLastRecord(result) {
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.tbodyVehicleHistory').html('');
    $('#VehicleHistory').tmpl(res).appendTo(divTbodyGoalFund);
    if (res.length > 0) {
        try {
            IsAdjusted = res[0].IsCompleted;
        }
        catch (err) {
            IsAdjusted = true;
        }

        if (res[0].IsCarAllowanceExist == 0) {
            $('.txtCarAllowance').val('NO');
        }
        else if (res[0].IsCarAllowanceExist == 1) {
            $('.txtCarAllowance').val('YES');
        }
    }

    ProgressBarHide();
}

function ClearAllFields(selector) {
    $(selector).find('input, select, textarea').each(function () {
        $(this).val('');
    });
}

function GetVehicleChangeHistory() {

    var service = new HrmsSuiteHcmService.HcmService();
    service.getVehicleChangeHistory(EmployeeId, onGetVehicleChangeHistory, null, null);
}

function onGetVehicleChangeHistory(result) {

    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.tbodyVehicleHistory').html('');
    $('#VehicleHistory').tmpl(res).appendTo(divTbodyGoalFund);
}

function GetVehicleTransactionalHistory() {
    ProgressBarShow();
    var service = new HrmsSuiteHcmService.HcmService();
    service.GetVehicleTransactionalHistory(EmployeeId, onGetVehicleTransactionalHistory, null, null);
}

function onGetVehicleTransactionalHistory(result) {

    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.tbodyVehicleTransactionalHistory').html('');
    $('#VehicleHistoryTransactional').tmpl(res).appendTo(divTbodyGoalFund);
    ProgressBarHide();
    //var i = 0;
    //$(res).each(function () {

    //    if (res[i].IsLock == 1) {

    //        $('.btnVehicleEdit').hide();
    //        $('.btnVehicleDelete').hide();

    //        //break;
    //    }
    //    i++;
    //});
}

function Open_Modal_VehicleChangeInstallment(VehicleMasterId) {


    if (VehicleMasterId > 0) {
        $('.txtVehicleCurrentMonthInstallmentAmount').val('');
        $('.txtVehicleCurrentMonthInstallmentTillDate').val('');
        $('.hf_ChangeVehicleMasterId').val('0');

        $('.hf_ChangeVehicleMasterId').val(VehicleMasterId);

        $('.ChangeVehicleInstallmentModal').click();
    }
}

function Change_VehicleInstallment_Amount() {
    /* if (parseInt(Math.round($(".txtVehicleCurrentMonthInstallmentAmount").val())) > 0 && $(".txtVehicleCurrentMonthInstallmentAmount").val() != "" && $(".txtVehicleCurrentMonthInstallmentAmount").val() != undefined) {*/
    var service = new HrmsSuiteHcmService.HcmService();
    const vehicleMasterId = parseInt($(".hf_ChangeVehicleMasterId").val());
    const changedInstallmentAmount = Math.round(parseFloat($('.txtVehicleCurrentMonthInstallmentAmount').val()));
    const tillDate = $('.txtVehicleCurrentMonthInstallmentTillDate').val();
    service.ChangeVehicleInstallmentAmount(vehicleMasterId, changedInstallmentAmount, tillDate, onChange_VehicleInstallment_Amount, null, null);
    //}
    //else {
    //    showError("Change Installment Amount Must Be Greater Than Zero OR Not Null.");
    //} 
}

function onChange_VehicleInstallment_Amount(result) {

    var result = jQuery.parseJSON(result);
    if (result.ResponseMessageType == 1) {
        $('.txtVehicleCurrentMonthInstallmentAmount').val('');
        $('.txtVehicleCurrentMonthInstallmentTillDate').val('');
        $('.hf_ChangeVehicleMasterId').val('0');
        $('.btnCancelVehicleChange').click();
        GetVehicleLastRecord();
        GetVehicleTransactionalHistory();

        origForm = serializeData('#panelVehicleInformation');
        showSuccess(result.ResponseMessage);
    }
    else {
        showError(result.ResponseMessage);
    }

}

function InsertVehicleInformation() {

    //if (!onRecordEdit) {
    //    if (!IsAdjusted) {
    //        showError('New vehicle cannot be added until current is adjusted');
    //        return;
    //    }
    //}

    //alert($('.UpgradeAmount').val());

    if (origForm == serializeData('#panelVehicleInformation')) {
        showError('No Changes Has Been Made');
        return;
    }

    if (!validateForm('.divControlsMandotory'))
        return;


    //ParentId = $(".parentId").first().val();

    InstallmentAmount = null;
    ChequeNo = null;

    IsUpgraded = ($('.UpgradeAmount').val() == '' || $('.UpgradeAmount').val() == '0') ? false : true;

    //if (IsUpgraded)
    //    $(resDifference).each(function (a, b) { $(b).each(function (k, v) { if (v.Id == $('.ddlVehicleDifference').val()) { PurchaseAmount = v.PurchaseAmount; BookValue = v.BookValue } }) })
    //else
    //    $(Vehicleres).each(function (a, b) { $(b).each(function (k, v) { if (v.Id == $('.ddlVehicle').val()) { PurchaseAmount = v.PurchaseAmount; BookValue = v.BookValue } }) })


    IsOwnershipDeduction = $("#chkOwnerShip").is(':checked');

    if (IsOwnershipDeduction && _VMsId == 0) {
        //InstallmentAmount = BookValue / $('.InstallmentAmount').val(); 
        InstallmentAmount = Math.round($('.BookValue').val() / $('.InstallmentAmount').val());
        ChequeNo = $('.ChqNo').val();
    }
    else {
        $('.InstallmentAmount').val('');
        InstallmentAmount = $('.txtInstAmt').val() == 0 ? null : $('.txtInstAmt').val();
    }

    DiffVehicleId = $('.ddlVehicleDifference').val() == 0 ? null : $('.ddlVehicleDifference').val();

    IsVehiclePayment = $("#chkVehiclePayment").is(':checked');
    debugger

    var ResponseForm = [];
    var Response = new Object();

    //Response.CarSettlementDate = $('.CarSettlementDate').val();
    //Response.VehicleId = $('.ddlVehicle').val();
    //Response.DifferenceVehicleInformationId = DiffVehicleId;
    //Response.PurchaseAmount = PurchaseAmount;

    Response.EmployeeId = EmployeeId;
    Response.VehicleInformationId = $('.ddlVehicle').val();
    Response.PurchaseDate = $('.PurchaseDate').val();
    Response.AlowanceDate = $('.AllowanceDate').val();
    Response.EngineNumber = $('.EngineNo').val();
    Response.RegistrationNumber = $('.RegNo').val();
    Response.ChasisNumber = $('.ChasisNo').val();
    Response.IsOwnerShipDeduction = IsOwnershipDeduction;
    Response.IsUpgraded = IsUpgraded;
    Response.UpgradedAmount = $('.UpgradeAmount').val();
    Response.PurchaseAmount = $('.PurchaseAmount').val();
    Response.WrittenDownValue = $('.BookValue').val();
    Response.Balance = $('.Balance').val();

    Response.ChequeNumber = $('.ChqNo').val();
    Response.InstallmentAmount = InstallmentAmount;
    Response.IsVehiclePayment = IsVehiclePayment;
    Response.CurrentMonthInstallment = $('.txtCurrMonthInstallment').val() == '' ? 0 : $('.txtCurrMonthInstallment').val();
    Response.UpgradedPurchaseAmount = $('.PurchaseAmountUpgraded').val();
    Response.CarSettlementDate = $('.CarSettlementDate').val();

    //if (ParentId == undefined)
    //    Response.ParentId = null;
    //else
    //    Response.ParentId = ParentId

    if (_VMsId == 0) {
        Response.ParentId = null;
        Response.Balance = $('.BookValue').val();
        Response.CurrentMonthInstallment = InstallmentAmount;
    }
    else
        Response.ParentId = _VMsId

    ResponseForm.push(Response);
    var JSONResponse = JSON.stringify(ResponseForm);
    var service = new HrmsSuiteHcmService.HcmService();
    ProgressBarShow();
    service.insertVehicleMasterInformation(JSONResponse, onInsertVehicleInformation, null, null);
}

function clearVehicleTabsFields() {

    ResetControls();
    resetControls('.panelVehicleInformation');

}

function onInsertVehicleInformation(result) {
    var res = jQuery.parseJSON(result);

    if (res.ResponseMessageType == 1) {
        clearVehicleTabsFields();
        GetVehicleLastRecord();
        GetVehicleTransactionalHistory();
        origForm = serializeData('#panelVehicleInformation');
        showSuccess(res.ResponseMessage);
    }
    else {
        showError(res.ResponseMessage);
    }


    ProgressBarHide();
}

function onVehicleInstallment(_selector) {


    $(".ddlCategory").prop("disabled", true);

    FillControlsOnEdit(_selector);

}

function onVehicleEdit(_selector) {

    FillControlsOnEdit(_selector);

    jQuery('#CreateProjectModal').animate({ scrollTop: 0 }, 2000);

}

function calculateInstallmentAmount() {
    if ($("#chkOwnerShip").is(":checked")) {
        const txtInstAmnt = Math.ceil(parseFloat($('.BookValue').val()) / parseFloat($('.InstallmentAmount').val()));
        $('.txtInstAmt').val(isNaN(txtInstAmnt) === true ? 0 : txtInstAmnt);
    }
    else {
        $('.txtInstAmt').val(0);
    }

}

function FillControlsOnEdit(selector) {

    ProgressBarShow();
    onRecordEdit = true;
    var objRow = $(selector).closest('tr');


    _VMsId = objRow.find('.parentId').val();
    VehicleId = objRow.find('.VehicleId').val();
    const categoryIsupgrade = objRow.find('.IsUpgraded').val();
    const currentMonthInstallmentAmount = objRow.find('.tdCurrentMonthInstallment').html();
    const insAdjTillDate = objRow.find('.currentTillDateInsAdj').val();
    //DiffVehicleId = objRow.find('.DiffVehicleId').val;

    var Upgd = objRow.find('.UpgradedAmount').html();
    var Installment = objRow.find('.tdInstallmentAmount').html();


    //if (categoryIsupgrade === "1") {
    //    $(".UpgradeAmount").prop("disabled", true);
    //}
    //else {
    //    $(".UpgradeAmount").prop("disabled", false);
    //}
    //$(".txtCarAllowance").prop("disabled", true);
    //$(".txtInstAmt").prop("disabled", true);
    //$(".txtCurrMonthInstallment").prop("disabled", true);
    //$(".txtInsAdjTillDate").prop("disabled", true);



    //$(".UpgradeAmount").prop("disabled", false);
    $(".Balance").prop("disabled", false);
    //$(".txtCarAllowance").prop("disabled", true);
    $(".txtInstAmt").prop("disabled", false);
    $(".txtCurrMonthInstallment").prop("disabled", false);
    $(".txtInsAdjTillDate").prop("disabled", false);
    $("#divCarSettlementDate").css('display', 'block');

    debugger
    //if (Upgd != '') {

    if (categoryIsupgrade === "1") {
        $('.ddlCategory').val(1).change();
    }
    else {
        $('.ddlCategory').val(2).change();
        
        // $('.ddlVehicle').val(Vehicleres[0].Id).change();
    }
    setTimeout(
        function () {
            //  $('.ddlVehicle').val(VehicleId);
            //  $('.ddlVehicleDifference').val(DiffVehicleId);
            ProgressBarHide();
        }, 1000);
    //} 

    //var BookValue = objRow.find('.tdBookValue').val();

    var BookValue = objRow.find('.tdBookValue').html();

    if (Installment != '') {
        valInstallment = Math.ceil(BookValue / Installment);
        if (!$('#chkOwnerShip').is(':checked')) {
            $('#chkOwnerShip').click();
            $('.InstallmentAmount').val(isNaN(valInstallment) === true ? 0 : valInstallment);
            $('.divInstallment').show();
        }

        if (objRow.find('.IsOwnerShipDeduction').html() == '1') {
            $('#chkOwnerShip').attr('checked', true);

            $('.InstallmentAmount').val(isNaN(valInstallment) === true ? 0 : valInstallment);
        }
    }

    $('.UpgradeAmount').val(Upgd);
    $('.ChqNo').val(objRow.find('.ChequeNumber').html());
    $('.ChasisNo').val(objRow.find('.ChasisNumber').html());
    $('.EngineNo').val(objRow.find('.EngineNumber').html());
    $('.RegNo').val(objRow.find('.RegistrationNumber').html());
    $('.PurchaseDate').val(formatDate(objRow.find('.PurchaseDate').html()));
    $('.PurchaseAmount').val(objRow.find('.PurchaseAmount').html());
    $('.BookValue').val(objRow.find('.tdBookValue').html());
    $('.Balance').val(objRow.find('.tdBalance').html());
    $('.PurchaseAmountUpgraded').val(objRow.find('.PurchaseAmountUpgraded').html());
    $('.CarSettlementDate').val(formatDate(objRow.find('.tdCarSettlementDate').html()));

    if ($('.CarSettlementDate').val() == 'NaN-NaN-NaN') { $('.CarSettlementDate').val(''); }
    if (objRow.find('.AlowanceDate').html() == 'undefined') {
        $('.AllowanceDate').val('');
    }
    else {
        $('.AllowanceDate').val(formatDate(objRow.find('.AlowanceDate').html()));
    }

    //if (objRow.find('.IsVehiclePayment').html() == '1') {
    //    $('#chkVehiclePayment').attr('checked', true);

    //}
    const txtInstAmnt = Math.round(parseFloat($('.BookValue').val()) / parseFloat($('.InstallmentAmount').val()));
    $('.txtInstAmt').val(isNaN(txtInstAmnt) === true ? 0 : txtInstAmnt);

    $('.txtCurrMonthInstallment').val(currentMonthInstallmentAmount);
    $('.txtInsAdjTillDate').val(insAdjTillDate.replace("T00:00:00", ""));

    //alert(objRow.find('.AlowanceDate').html()); 

    setTimeout(
        function () {
            //$('.ddlVehicle').val(VehicleId);
            $('.ddlVehicle').val(VehicleId).change();
            ProgressBarHide();
        }, 1000);

    origForm = serializeData('#panelVehicleInformation');
    ProgressBarHide();
}

function DisableAllControls() {

    $('#divControlsVehicle').find('input, textarea, button, select').prop('disabled', true);
}

function EnableAllControls() {

    $('#divControlsVehicle').find('input, textarea, button, select').prop('disabled', false);
}

function GetVehicleList() {

    isUpgrade = null;

    if ($('.ddlCategory').val() == 0)
        isUpgrade = null;
    else if ($('.ddlCategory').val() == 1)
        isUpgrade = 0;
    else if ($('.ddlCategory').val() == 2)
        isUpgrade = 1;
    else
        isUpgrade = null;

    if (isUpgrade) {
        //GetDifferenceVehicles();
        $('.divCheque').show();
        //$('.divVehicleDifference').show();

        $('.UpgradeAmount').prop('disabled', false);
    }
    else {
        //$('.ddlVehicleDifference').val(0).change();
        $('.UpgradeAmount').val('0');
        $('.divCheque').hide();
        //$('.divVehicleDifference').hide();

        $('.UpgradeAmount').prop('disabled', true);
    }


    var service = new HrmsSuiteHcmService.HcmService();
    service.getVehicleList(EmployeeId, isUpgrade, onGetVehicleList, null, null);
}

function onGetVehicleList(result) {
    debugger
    Vehicleres = '';
    Vehicleres = jQuery.parseJSON(result);
    FillDropDownByReference('.ddlVehicle', Vehicleres);
    if (Vehicleres.length > 0) {
        $('.ddlVehicle').val(Vehicleres[0].Id).change();
    }
}

function GetDifferenceVehicles() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getVehicleList(EmployeeId, false, onGetDifferenceVehicles, null, null);
}

function onGetDifferenceVehicles(result) {
    resDifference = jQuery.parseJSON(result);
    FillDropDownByReference('.ddlVehicleDifference', resDifference);
    $('.ddlVehicleDifference').val(resDifference[0].Id).change();
}

//function GetUpgradeDifference() {
//    try {
//        $(Vehicleres).each(function (a, b) { $(b).each(function (k, v) { if (v.Id == $('.ddlVehicle').val()) { PurchaseAmount = v.PurchaseAmount } }) })
//        $(resDifference).each(function (a, b) { $(b).each(function (k, v) { if (v.Id == $('.ddlVehicleDifference').val()) { DifferenceAmount = v.PurchaseAmount } }) })
//        PurchaseAmount = (PurchaseAmount - DifferenceAmount) < 0 ? null : (PurchaseAmount - DifferenceAmount);
//        $('.UpgradeAmount').val(PurchaseAmount);
//    }
//    catch (e) {
//        console.log("The System is not finding Exception " + e);
//    }
//}

function VehicleEligbleVehicleDiffChange() {
    GetUpgradeDifference();
}

function onVehicleDelete(selector) {

    if (confirm("Are you sure you want to delete?") == true) {
        var objRow = $(selector).closest('tr');
        //VehicleMasterId = objRow.find('.parentId').val();

        VehicleMasterId = $('.hf_ChangeVehicleMasterId').val();
        var comments = $('.txtVehicleDeleteRecordComments').val();
        var service = new HrmsSuiteHcmService.HcmService();
        service.deleteVehicleById(VehicleMasterId, comments, onDeleteVehicleById, null, null);
    }
}

function onDeleteVehicleById(result) {
    if (result == 0)
        showError('Vehicle With Transactions Cannot Be Deleted');
    else
        showSuccess('Successfully Deleted');

    $('.txtVehicleDeleteRecordComments').val('');
    $('.hf_ChangeVehicleMasterId').val('0');
    $('.btnCancelVehicleDelete').click();
    GetVehicleLastRecord();
    GetVehicleTransactionalHistory();
    origForm = serializeData('#panelVehicleInformation');
}

function onVehicleHold(selector) {

    var objRow = $(selector).closest('tr');
    VehicleMasterId = objRow.find('.parentId').val();
    isVehicleHold = objRow.find('.IsHold').val();

    if (isVehicleHold == '1')
        vehicleflag = 0;
    else
        vehicleflag = 1;
    var service = new HrmsSuiteHcmService.HcmService();
    service.holdVehicleDeductionByVehicleMasterId(parseInt(VehicleMasterId), parseInt(vehicleflag), onHoldVehicleDeduct, null, null);

}

function onHoldVehicleDeduct(result) {

    if (result == 1) {
        showSuccess('Hold Successfully!');
    }
    else if (result == 0) {
        showSuccess('UnHold Successfully!');
    }
    else {
        showError(result);
    }

    GetVehicleLastRecord();

    setTimeout(
        function () {


            GetVehicleTransactionalHistory();

        }, 1000);
}

function Open_Modal_VehicleDeleteRecord(VehicleMasterId) {

    if (VehicleMasterId > 0) {
        $('.txtVehicleDeleteRecordComments').val('');
        $('.hf_ChangeVehicleMasterId').val('0');

        $('.hf_ChangeVehicleMasterId').val(VehicleMasterId);

        $('.ChangeVehicleDeleteRecordModal').click();
    }
}