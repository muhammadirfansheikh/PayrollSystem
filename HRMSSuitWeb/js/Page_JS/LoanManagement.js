EmployeeId = '';
CompanyId = '';
LoanMasterId = '0';
cssClass = '';
InstallmentAmountEdit = '0';

TriggerPageLoadsLoan();

function TriggerPageLoadsLoan() {

    $(".btnSave").click(function () {
        var insufficientPFAmount = false;
        if ($('.ddlLoanType').val() == HCM_SetupDetail.PFLoanTypeId) {
            if (parseFloat(EmployeePFBalance) < parseFloat($('.txtLoanAmount').val())) {
                showError("Provident Fund amount is Insufficient");
                insufficientPFAmount = true;
            }
        }

        if (insufficientPFAmount)
            return;

        if (!validateForm('.dvEntry')) {
            return;
        }


        if (LoanMasterId > 0) {
            var _InstallmentAmount = $('.txtInstallmentAmount').val();

            if (parseFloat(InstallmentAmountEdit) > parseFloat(_InstallmentAmount)) {
                showError("Installment amount should not be less than previous installment amount");
                return;
            }
        }
        var TotalInstallments = $('.txtTotalInstallment').val();
        var ParentId = LoanMasterId;
        var LoanTypeId = $('.ddlLoanType').val();
        var LoanAmount = $('.txtLoanAmount').val();
        var InstallmentAmount = $('.txtInstallmentAmount').val();
        var SanctionDate = $('.txtSanctionDate').val();
        var LoanGivenDate = $('.txtLoanGivenDate').val();
        var InstallmentDate = $('.txtSettlementDate').val();
        var InterestAmount = $('.txtInterestAmount').val();
        var InterestId = $('.hfInterestId').val() == '' ? 0 : $('.hfInterestId').val();
        var LoanAmount = $('.txtLoanAmount').val() == '' ? 0 : $('.txtLoanAmount').val();
        var RemainderValue = parseFloat(LoanAmount) % parseFloat(InstallmentAmount);

        if (SanctionDate == InstallmentDate) {
            showError("Sanction and Settlement should not be same");
        }
        else {
            if ((TotalInstallments - Math.floor(TotalInstallments)) == 0) {

                if (parseFloat(TotalInstallments) <= 500) {
                    var ResponseForm = [];
                    $('.dvEntryDetail').find('.form-control').each(function () {

                        var AttId = $(this).attr('title');
                        var ControlValue = $(this).val();

                        var Response = new Object();
                        Response.AttributeId = AttId;
                        Response.AttributeValue = ControlValue;

                        ResponseForm.push(Response);
                    });
                    var JSONResponse = JSON.stringify(ResponseForm);

                    SaveLoanMaster(LoanTypeId, LoanAmount, InstallmentAmount, formatDate(SanctionDate), formatDate(LoanGivenDate), formatDate(InstallmentDate), ParentId,/* JSONResponse,*/ InterestAmount, InterestId);

                    GetLoanMaster();
                }
                else {
                    showError("Total number of installments should not be more than 500");
                }
            }
            else {
                showError("Total number of installments should not be in decimal");
            }
        }

    });

}

function ClearFields() {

    $('.txtReason').val("");
    $('.txtComments').val("");

    $('.ddlLoanType').val(0);
    //$('.ddlLoanType').removeAttr('disabled');

    $('.txtLoanAmount').val("");
    //$('.txtLoanAmount').removeAttr('disabled', 'disabled');

    $('.txtInstallmentAmount').val("");
    $('.txtTotalInstallment').val("");

    $('.txtSanctionDate').val("");
    $('.txtLoanGivenDate').val("");
    //$('.txtSanctionDate').removeAttr('disabled', 'disabled');

    $('.txtSettlementDate').val("");

    $('.txtTotalBalance').val("0");

    $('.txtInterestRate').val("0");
    $('.txtInterestAmount').val("0");
    $('.hfInterestId').val("0");

    $('.dvEntryDetail').html('');
    InstallmentAmountEdit = 0;
    LoanMasterId = 0;
    GetLoanAllow();
}

function GetTotalNoInstallments() {

    var IntrestAmount = $('.txtInterestAmount').val();

    var LoanAmount = $('.txtLoanAmount').val() == '' ? 0 : $('.txtLoanAmount').val();

    var TotalBalance = LoanMasterId == 0 ? LoanAmount : $('.txtTotalBalance').val();
    $('.txtTotalBalance').val(TotalBalance);

    var InstallmentAmount = Math.round($('.txtInstallmentAmount').val() == "" ? "0" : $('.txtInstallmentAmount').val());
    var LoanAmount2 = TotalBalance;

    $('.txtTotalInstallment').val(LoanMasterId == 0 ? LoanAmount / InstallmentAmount : LoanAmount2 / InstallmentAmount);

    $('.txtTotalInstallment').val(parseFloat($('.txtTotalInstallment').val()).toFixed(2));

    if ($('.txtTotalInstallment').val() == "Infinity") {
        $('.txtTotalInstallment').val('');
    }

    if ($('.txtTotalInstallment').val() == "NaN") {
        $('.txtTotalInstallment').val('');
    }

    GetSettlementMonth();
}

function setLoanMasterId(_LoanMasterId, _LoanTypeId, _LoanAmount, _InstallmentAmount, _SanctionDate,_LoanGivenDate, _SettlementDate, _Balance, _Reason, _Comments) {
    if (confirm("Are you sure you want to Edit?") == true) {

        ProgressBarShow();

        LoanMasterId = _LoanMasterId;
        InstallmentAmountEdit = Math.round(_InstallmentAmount);

        $('.ddlLoanType').val(_LoanTypeId);
        //$('.ddlLoanType').attr('disabled', 'disabled');

        $('.txtLoanAmount').val(_LoanAmount);
        //$('.txtLoanAmount').attr('disabled', 'disabled');

        $('.txtTotalBalance').val(_Balance);

        $('.txtInstallmentAmount').val(_InstallmentAmount);
        //$('.txtTotalInstallment').val(_LoanTypeId);
        $('.txtSanctionDate').val(_GetDate(_SanctionDate));
        $('.txtLoanGivenDate').val(_GetDate(_LoanGivenDate));
        //$('.txtSanctionDate').attr('disabled', 'disabled');

        $('.txtSettlementDate').val(_GetDate(_SettlementDate));

        $('.btnSaveLoan').show();

        $('.txtReason').val(_Reason);
        $('.txtComments').val(_Comments);

        //GenerateDynamicControls();
        GetTotalNoInstallments();

        LoanPaymentLoad(_LoanMasterId);
    }
}

function SaveLoanMaster(LoanTypeId, LoanAmount, InstallmentAmount, SanctionDate,LoanGivenDate, InstallmentDate, ParentId, /*Json,*/ InterestAmount, InterestId) {


    var Reason = $('.txtReason').val();
    var Comments = $('.txtComments').val();

    var service = new HrmsSuiteHcmService.HcmService();
    service.saveLoanMaster(LoanTypeId, EmployeeId, LoanAmount, InstallmentAmount, SanctionDate, LoanGivenDate, InstallmentDate, ParentId, /*Json,*/ InterestAmount, InterestId, Reason, Comments,
        onSaveLoanMaster, null, null);
}

function onSaveLoanMaster(result) {

    GetLoanMaster();
    if (result == 1) {

        showSuccess("Loan Saved Successfully");
    }
    else {
        showError(result);
    }
}

function GetLoanMaster() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getLoanMaster(EmployeeId, onGetLoanMaster, null, null);
    ClearFields();
}

function onGetLoanMaster(result) {

    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.tbodyLoanListing').html('');
    $('#LoanListing').tmpl(res).appendTo(divTbodyGoalFund);

    $('.tr_LoanListing').each(function () {
        var Has_IsLocked_Transection = $(this).find(".hf_Has_IsLocked_Transection").val();
        if (Has_IsLocked_Transection > 0) {
            $(this).find('.btn_Loan_Edit').hide();
            $(this).find('.btn_Loan_Delete').hide();
        }
        var IsHold = $(this).find(".hf_IsHold").val();
        if (IsHold == "true") {
            $(this).find('.btn_Loan_Hold').prop("value", "Unhold");
        }
        var Balance = $(this).find(".hf_Balance").val();
        if (Balance > 0) {
            $(this).find('.btn_Loan_Change_Installment').show();
        }
    });
}

function GetFromSetupDetail(ParentId, MasterId, _cssClass) {
    if (ParentId != 0) {
        ParentId = $(ParentId).val();
    }
    var service = new HrmsSuiteHcmService.HcmService();
    service.getFromSetupDetail(CompanyId, ParentId, MasterId, onGetFromSetupDetail, null, null);
    cssClass = _cssClass;
}

function onGetFromSetupDetail(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(cssClass, res);
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

function GenerateDynamicControls() {
    var LoanTypeId = $('.ddlLoanType').val();
    $('.dvEntry').html();
    if (LoanTypeId == 0)
        return;
    ProgressBarShow();
    var service = new HrmsSuiteHcmService.HcmService();
    service.getAttributes(LoanTypeId, CompanyId, onGenerateDynamicControls, null, null);

}

function onGenerateDynamicControls(result) {
    var res = jQuery.parseJSON(result);
    $(".dvEntryDetail").html('');
    if (res.length > 0) {
        CreateControls(res, ".dvEntryDetail");
    } else {
        ProgressBarHide();
    }
    res = null;

}

function CreateControls(res, divToAppendId) {

    var html = ''; i = 0;

    var Interval = null;

    Interval = setInterval(function () {

        if (i == res.length - 1) {
            //alert();
            ProgressBarHide();
            clearInterval(Interval);
        }

        innerDivClass = res[i].ParentClass;
        control = '';



        if (res[i].ControlType == "select") {
            control = '<select class="' + res[i].CssClass + ' ' + res[i].FillByRefrenceClass + '" title="' + res[i].AttributeId + '" onchange=GetAttributeDependent("' + res[i].AttributeId + '"); data-columnname = "' + res[i].WhereColumnName + '"></select>';
            html = '<div class="clsInput cls_' + i + ' ' + innerDivClass + '"><label for="exampleIsnputEmail2">' + res[i].AttributeName + '</label>' + control + '</div>';
            $(divToAppendId).append(html);
            ;
            if (res[i].ParentId == null) {
                FillDropdown(res[i].AttributeId, '', '.' + res[i].FillByRefrenceClass);
            }
            else {
                FillDropdown(res[i].AttributeId, res[i].WhereColumnName, '.' + res[i].FillByRefrenceClass);
            }
        }
        else if (res[i].ControlType == "textarea") {
            control = '<textarea cols="40" rows="5" class="' + res[i].CssClass + '" title="' + res[i].AttributeId + '"></textarea>';
            html = '<div class="clsInput cls_' + i + ' ' + innerDivClass + '"><label for="exampleIsnputEmail2">' + res[i].AttributeName + '</label>' + control + '</div>';
            $(divToAppendId).append(html);
        }
        else {
            control = '<input type="' + res[i].ControlType + '" class="' + res[i].CssClass + '" title="' + res[i].AttributeId + '"/>';
            html = '<div class="clsInput cls_' + i + ' ' + innerDivClass + '"><label for="exampleIsnputEmail2">' + res[i].AttributeName + '</label>' + control + '</div>';
            $(divToAppendId).append(html);
        }

        i++;
    }, 200);


    if (LoanMasterId > 0) {
        GetLoanAttributeDetail(LoanMasterId);
    }
}

function FillDropdown(ControlId, Value, _cssClass) {
    var service = new HrmsSuiteHcmService.HcmService();
    service.DynamicDdlFill(ControlId, Value, onFillDropdown, null, null);
    cssClass = _cssClass;
}

function onFillDropdown(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(cssClass, res);

}

function CreateControlsByLoanType() {
    var ResponseForm = [];
    $('.dvEntryDetail').find('.form-control').each(function () {
        var AttId = $(this).attr('title');
        var ControlValue = $(this).val();
        var Response = new Object();
        Response.AttributeId = AttId;
        Response.Value = ControlValue;
        ResponseForm.push(Response);
    });
}

function GetLoanAttributeDetail(LoanMasterId) {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getLoanAttributeDetail(LoanMasterId, onGetLoanAttributeDetail, null, null);
}

function onGetLoanAttributeDetail(result) {

    var res = jQuery.parseJSON(result);

    setTimeout(function () {
        $('.dvEntryDetail').find('.form-control').each(function () {
            var AttId = $(this).attr('title');
            var ControlValue = $(this).val();
            var Control = $(this);

            $(res).each(function (k, v) {
                if (AttId == v.AttributeId) {

                    Control.val(v.AttributeValue);
                }
            });
        });
    }, 1000);
}

function GetSettlementMonth() {
    ;
    var SanctionDate = $('.txtSanctionDate').val();
    var TotalInstallments = $('.txtTotalInstallment').val();

    var d = new Date(SanctionDate);
    var month = (d.getMonth() + 1 + parseInt(TotalInstallments));
    var date = d.getDate();
    var year = d.getFullYear();

    if (date >= 1 && date <= 9)
        date = '0' + date;
    if (month > 12) {
        var x = month % 12;
        var y = month / 12;
        month = parseInt(x);
        year = year + parseInt(y);
    }

    if (month >= 1 && month <= 9)
        month = '0' + month;

    if (month == '0')
        month = '01';

    if (isNaN(month) || isNaN(date) || isNaN(year)) {
        $('.txtSettlementDate').val("");
    }
    else {
        var SettlementDate = month + '/' + date + '/' + year;
        SettlementDate = _GetDate(SettlementDate);
        $('.txtSettlementDate').val(SettlementDate);
    }
}

function GetInterestRate() {

    if (InterestStandardId != HCM_SetupDetail.BasicInterest) {
        GetLoanAllow();
        //return;
    }


    var closest = null;
    var SanctionDate = $('.txtSanctionDate').val();
    var d = new Date(SanctionDate);
    var SanctionYear = d.getFullYear();
    var SettlementDate = $('.txtSettlementDate').val();
    var ds = new Date(SettlementDate);
    var SettlementYear = ds.getFullYear();
    var LoanAmount = $('.txtLoanAmount').val();
    var LoanTypeId = $('.ddlLoanType').val();

    var diffYear = parseInt(SettlementYear) - parseInt(SanctionYear);

    if (InterestList == '0') {
        closest = 0;
    }
    else {
        $(InterestList).each(function (k, v) {
            if (closest == null || Math.abs(v.Slab - diffYear) < Math.abs(closest - diffYear)) {
                closest = v.Slab;
            }
        });
    }



    if (SettlementYear != '') {
        var InterestRate = InterestList.filter(x => x.Slab == closest).map(x => x.InterestRate) / 100;
        var InterestId = InterestList.filter(x => x.Slab == closest).map(x => x.InterestId);

        if (LoanTypeId != 97) {
            InterestRate = 0;
            InterestId = '';
        }
        var InterestRateNew = InterestRate / 12;

        //var TotalLoanWithInterestAmount = parseFloat(InterestRate) * parseFloat(LoanAmount);
        var TotalInstallments = $('.txtTotalInstallment').val();
        var i;
        var ClosingBalanceRun = 0;
        var IntrestAmountRun = 0;
        var IntrestAmountRunTotal = 0;

        for (i = 0; i < parseInt(TotalInstallments) ; i++) {

            if (i == 0) {
                IntrestAmountRun = parseFloat(LoanAmount) * InterestRateNew;
                ClosingBalanceRun = parseFloat(LoanAmount) - parseFloat($('.txtInstallmentAmount').val());
            }
            else {
                IntrestAmountRun = parseFloat(ClosingBalanceRun) * InterestRateNew;
                ClosingBalanceRun = ClosingBalanceRun - parseFloat($('.txtInstallmentAmount').val());
            }

            IntrestAmountRunTotal = IntrestAmountRunTotal + IntrestAmountRun;
        }

        var TotalLoanWithInterestAmount = parseFloat(IntrestAmountRunTotal);



        $('.hfInterestId').val(InterestId);
        $('.txtInterestRate').val(InterestRate * 100);
        $('.txtInterestAmount').val(TotalLoanWithInterestAmount);

        //var TotalBalanceWithIntrestAmount = parseFloat(LoanAmount) + parseFloat(TotalLoanWithInterestAmount);
        //$('.txtTotalBalance').val(TotalBalanceWithIntrestAmount);

        GetInstallmentAmountByTotalInstallment();
        //var TotalInstallment = $('.txtTotalInstallment').val();
        //var InterestAmountPerInstallment = parseFloat(TotalLoanWithInterestAmount) / parseFloat(TotalInstallment);
        //var InstallmentAmountWithInterest = parseFloat(InterestAmountPerInstallment) + parseFloat(InstallmentAmount);
        //$('.txtInstallmentAmount').val(Math.ceil(InstallmentAmountWithInterest));
    }


}

function CalculateInterest() {

}

function GetAttributeDependent(ParentAttributeId) {
    var value = '';
    $('.dvEntryDetail').find("select").each(function () {
        AttributeId = $(this).attr("title");
        if (AttributeId == ParentAttributeId) {
            value = $(this).val();
        }
        if (AttributeId != ParentAttributeId) {
            var column = $(this).attr('data-columnname');
            var myClass = $(this).attr("class");
            FillDropdown(AttributeId, value, myClass);
        }
    });

}

function onLoanDelete(LoanMasterId) {
    if (confirm("Are you sure you want to delete?") == true) {
        ProgressBarShow();
        var service = new HrmsSuiteHcmService.HcmService();
        service.deleteLoanById(LoanMasterId, onLoanDeleteById, null, null);
    }
}

function onLoanDeleteById(result) {
    if (result == 0) {
        showError('Loan With Transactions Cannot Be Deleted');
    }
    else {
        showSuccess('Successfully Deleted');
    }
    GetLoanMaster();
    ProgressBarHide();
}



function HoldLoan(LoanMasterId, isHold) {
    var msg = "Are you sure ?";
    if (isHold == 'true') {
        flag = false;
        msg = "Are you sure you want to Unhold?";
    }
    else {
        flag = true;
        msg = "Are you sure you want to Hold?";
    }

    if (confirm(msg) == true) {
        ProgressBarShow();
        var service = new HrmsSuiteHcmService.HcmService();
        service.holdLoanById(LoanMasterId, flag, onHoldLoan, null, null);
    }
}

function onHoldLoan(result) {
    if (result == 1) {
        if (flag == true) {
            showSuccess('Loan hold Successfully.');
        }
        else if (flag == false) {
            showSuccess('Loan Unhold Successfully.');
        } else {
            showSuccess('Successfully Updated!');
        }
        GetLoanMaster();
    }
    ProgressBarHide();
}




function GetInstallmentAmountByTotalInstallment() {
    var TotalBalance = $('.txtTotalBalance').val();
    var TotalInstallment = $('.txtTotalInstallment').val();
    var InstallmentAmount = parseFloat(TotalBalance) / parseFloat(TotalInstallment);
    InstallmentAmount = Math.ceil(InstallmentAmount)
    var IntrestAmount = $('.txtInterestAmount').val();
    $('.txtInstallmentAmount').val(InstallmentAmount.toFixed(2));


    //var InterestAmountPerInstallment = parseFloat(IntrestAmount) / parseFloat(TotalInstallment);
    //var InstallmentAmountWithInterest = parseFloat(InterestAmountPerInstallment) + parseFloat(InstallmentAmount);  
    //$('.txtInstallmentAmount').val(Math.ceil(InstallmentAmountWithInterest));

    GetSettlementMonth();
}



function GetLoanAllow() {

    if (CompanyId != '' && EmployeeId != '') {
        var service = new HrmsSuiteHcmService.HcmService();
        service.getLoanAllow(CompanyId, EmployeeId, onGetLoanAllow, null, null);
    }
}

function onGetLoanAllow(result) {

    var res = result;

    if (res == 1) {
        $('.btnSaveLoan').show();
    }
    else {
        $('.btnSaveLoan').hide();
    }
}

function GetInterestList() {
    ProgressBarShow();
    InterestList = "0";
    var service = new HrmsSuiteHcmService.HcmService();
    service.getInterestList(CompanyId, EmployeeId, onGetInterestList, null, null);
}

function onGetInterestList(result) {
    InterestList = jQuery.parseJSON(result);
    ProgressBarHide();
}

function GetLoanType() {

    var service = new HrmsSuiteHcmService.HcmService();
    service.getLoanType(onGetLoanType, null, null);
}

function onGetLoanType(result) {

    var res = jQuery.parseJSON(result);
    FillDropDownByReference('.ddlLoanType', res);
}

function GetSettelmentType() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getSettelmentType(CompanyId, onGetSettelmentType, null, null);
}

function onGetSettelmentType(result) {

    var res = jQuery.parseJSON(result);
    FillDropDownByReference('.ddlSettlementType', res);
}

function GetPaymentType() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getPaymentType(CompanyId, onGetPaymentType, null, null);
}

function onGetPaymentType(result) {

    var res = jQuery.parseJSON(result);
    FillDropDownByReference('.ddlPaymentType', res);
}

function LoanPaymentLoad(SelectedLoanMasterId) {

    GetSettelmentType();
    GetPaymentType();
    LoanSettlementList(SelectedLoanMasterId);
    $('.hfSelectedLoanMasterId').val(SelectedLoanMasterId);
}

function SaveLoanPayment() {
    var PaymentType = $('.ddlPaymentType').text();
    var SettlementTypeId = $('.ddlSettlementType').val();
    var PaymentTypeId = $('.ddlPaymentType').val();
    var SettlementAmount = $('.txtSettlementAmount').val();
    var ChequeDate = $('.txtChequeDate').val();
    var ChequeNo = $('.txtChequeNo').val();
    var Bank = $('.txtBankDetail').val();
    var SelectedLoanMasterId = $('.hfSelectedLoanMasterId').val();
    var SelectedSettlementDetailId = $('.hfSettlementDetailId').val();

    if (PaymentType == 'Cash') {

    }
    else {

        if (!validateForm('.dvEntryLoanPayment'))
            return;

        ProgressBarShow();
        var service = new HrmsSuiteHcmService.HcmService();
        service.SaveLoanPayment(SettlementTypeId, PaymentTypeId, EmployeeId, SettlementAmount, ChequeDate, ChequeNo, Bank, SelectedLoanMasterId, SelectedSettlementDetailId, onSaveLoanPayment, null, null);
    }
}

function onSaveLoanPayment(result) {

    var res = result;
    ProgressBarHide();
    if (res == 1) {
        showSuccess('Saved Successfully!');
        ResetControlsForLoanPayment();
        var SelectedLoanMasterId = $('.hfSelectedLoanMasterId').val();
        LoanSettlementList(SelectedLoanMasterId);
    }
    else {
        showError(res);
    }

    
}

function LoanSettlementList(SelectedLoanMasterId) {


    //var SettlementDetailId = $('.hfSettlementDetailId').val();

    var service = new HrmsSuiteHcmService.HcmService();
    service.getLoanSettlementList(SelectedLoanMasterId, onLoanSettlementList, null, null);
}

function onLoanSettlementList(result) {

    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.tbodyLoanPaymentDetail').html('');
    $('#LoanPayment').tmpl(res).appendTo(divTbodyGoalFund);
    $('.trLoanPayment').each(function () {
        var Locked = $(this).find("._Islock").val();
        if (Locked == 1) {
            $(this).find('.td_Action').hide();
        }
    })
    ProgressBarHide();
}

function EditSettlement(ref) {

    if (confirm("Are you sure?") == true) {
        var SettlementDetailId = $(ref).closest('tr').find('._hfSettlementDetailId').val();
        var SettlementTypeId = $(ref).closest('tr').find('._hfSettlementTypeId').val();
        var PaymentTypeId = $(ref).closest('tr').find('._hfPaymentTypeId').val();
        //var LoanMasterId = $(ref).closest('tr').find('._hfLoanMasterId').val(); 

        var SettlementType = $(ref).closest('tr').find('.tdSettlementType').html();
        var PaymentType = $(ref).closest('tr').find('.tdPaymentType').html();
        var SettlementAmount = $(ref).closest('tr').find('.tdSettlementAmount').html();
        var Bank = $(ref).closest('tr').find('.tdBank').html();
        var ChequeNo = $(ref).closest('tr').find('.tdChequeNo').html();
        var ChequeDate = $(ref).closest('tr').find('.tdChequeDate').html();

        $('.hfSettlementDetailId').val(SettlementDetailId);
        //$('.hfSelectedLoanMasterId').val(LoanMasterId);

        $('.ddlSettlementType').val(SettlementTypeId);
        $('.ddlPaymentType').val(PaymentTypeId);
        $('.txtSettlementAmount').val(SettlementAmount);
        $('.txtChequeDate').val(ChequeDate);
        $('.txtChequeNo').val(ChequeNo);
        $('.txtBankDetail').val(Bank);
    }
}

function DeleteSettlement(ref) {
    if (confirm("Are you sure you want to delete?") == true) {
        var SettlementDetailId = $(ref).closest('tr').find('._hfSettlementDetailId').val();
        ProgressBarShow();
        var service = new HrmsSuiteHcmService.HcmService();
        service.DeleteSettlement(SettlementDetailId, onDeleteSettlement, null, null);
    }
}

function onDeleteSettlement(result) {
    var res = result;
    if (res == 1) {
        showSuccess('Deleted Successfully!');
        ResetControlsForLoanPayment();
        var SelectedLoanMasterId = $('.hfSelectedLoanMasterId').val();
        LoanSettlementList(SelectedLoanMasterId);
    }
    else {
        showError(res);
    }
    ProgressBarHide();
}

function ResetControlsForLoanPayment() {
    $('.ddlSettlementType').val(0);
    $('.ddlPaymentType').val(0);
    $('.txtSettlementAmount').val("");
    $('.txtChequeDate').val("");
    $('.txtChequeNo').val("");
    $('.txtBankDetail').val("");
    $('.hfSettlementDetailId').val(0);
}

function CancelLoanPayment() {
    ResetControlsForLoanPayment();

    var SelectedLoanMasterId = $('.hfSelectedLoanMasterId').val();
    LoanSettlementList(SelectedLoanMasterId);
}

function Open_Modal_LoanChangeInstallment(LoanMasterId, Balance) {
    if (LoanMasterId > 0 && Balance > 0) {
        $('.txtCurrentMonthInstallmentAmount').val('');
        $('.txtCurrentMonthInstallmentTillDate').val('');
        $('.hf_ChangeLoanMasterId').val('0');
        $('.hfLoanCurrentBalance').val('0');
        $('.hf_ChangeLoanMasterId').val(LoanMasterId);
        $('.hfLoanCurrentBalance').val(Balance);
        $('.ChangeInstallmentModal').click();
    }
}

function Change_LoanInstallment_Amount() {
   
    var LoanMasterId = $('.hf_ChangeLoanMasterId').val();
    if (LoanMasterId > 0) {
        var InstallmentAmount_ = $('.txtCurrentMonthInstallmentAmount').val();
        InstallmentAmount_ = parseFloat(InstallmentAmount_);
        var TillDate = $('.txtCurrentMonthInstallmentTillDate').val();
        if (InstallmentAmount_ > 0) {
            if (TillDate != "" && TillDate != null) {
                var Balance_ = $('.hfLoanCurrentBalance').val();
                Balance_ = parseFloat(Balance_);
                if (Balance_ > 0) {
                    if (InstallmentAmount_ <= Balance_) {
                        if (confirm("Are you sure?") == true) {
                            ProgressBarShow();
                            var service = new HrmsSuiteHcmService.HcmService();
                            service.Change_LoanInstallment_Amount(LoanMasterId, InstallmentAmount_, TillDate, onChange_LoanInstallment_Amount, null, null);
                        }
                    }
                    else {
                        showError("Installment amount not be greater then balance");
                    }
                } else {
                    showError("Installment amount balance is zero");
                }
            }
            else {
                showError("Please select Till Date");
            }
        } else {
            showError("Please enter Installment Amount");
        }
    }
}

function onChange_LoanInstallment_Amount(result) {
    if (result == "1") {
        showSuccess('Changed Successfully!');
        $('.txtCurrentMonthInstallmentAmount').val('');
        $('.txtCurrentMonthInstallmentTillDate').val('');
        $('.hf_ChangeLoanMasterId').val('0');
        $('.hfLoanCurrentBalance').val('0');
        $('.btnCancelLoanChange').click();
        GetLoanMaster();
    }
    ProgressBarHide();
}




