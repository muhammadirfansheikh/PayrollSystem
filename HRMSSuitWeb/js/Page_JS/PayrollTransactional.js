
CompanyId = '';
EmployeeId = "";
SalaryId = "";
carDeductionBlnc = 0;
carInsDeduction = 0;
OverTimeRateId = "";
oSerialized = "";
Lock = false;
var _resPayroll;
var noRecord = false;

TriggerPageLoads();
strDivAddnew = '.divNewAllowanceAdd';

var divToAdd = "<tr class='divNewAllowanceAdd'><td class='project-title'><select class='form-control ddlNewAllowance'></select></td><td class='project-title'><input class='form-control txtAllowanceAmount numericOnly' type='text' /></td><td><button onclick='removeSelectedDiv(this,strDivAddnew)' class='btn btn-danger btn-circle' type='button'><i class='fa fa-times'></i></button><td/></tr>";


function initDefaults(_EmployeeId, _SalaryId) {
    EmployeeId = _EmployeeId;
    SalaryId = _SalaryId;
}

function TriggerPageLoads() {
    GetGroup();
    hideLock();
    $('.alertCount').hide();



}


function ClosePopup(popUpId) {
    $("#" + popUpId).hide();
}

function hideLock() {
    $('.btnLock').hide();
}

function AddNewAllowance() {
    cloneDiv(divToAdd, '.divExtraAllowance');
    GetAllowances();
}

function GetGroup() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getGroup(onGetGroup, null, null);
}

function onGetGroup(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlGroup", res);
    $('.ddlGroup').val(res[0].Id)
    $(".ddlGroup").prop("disabled", true);
    $(".ddlGroup").change();
}

function GetCompany(Group) {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getCompanyByGroupId($(Group).val(), onGetCompany, null, null);
}

function onGetCompany(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlCompany", res);
    $(".ddlCompany").change();
}

function GetAllowances() {
    var str = getCommaSeparatedValues(".hdAllowanceID") + getCommaSeparatedValues(".ddlNewAllowance");
    var service = new HrmsSuiteHcmService.HcmService();
    service.getAllowances(CompanyId, str, onGetAllowances, null, null);
}

function onGetAllowances(result) {
    var res = jQuery.parseJSON(result);
    var div = $(".ddlNewAllowance").last();
    FillDropDownByReference(div, res);
    $(div).change();
    $(div).focus();
    validateNumeric();
}

function GetPayroll() {

    if (!validateForm('.divPayrollForm'))
        return;
    ResSal = $("#chkResSalary").is(':checked');
    ResInc = $('#chkResInc').is(':checked');
    ResArr = $('#chkResArrear').is(':checked');
    ResBonus = $('#chkResBonus').is(':checked');
    Company = $(".ddlCompany").val();
    DateOfPayroll = formatDate($('.txtMonth').val());
    CompanyId = Company;
    var ListIncIds = getCommaSeparatedValues_CheckedBoxes('.chkIncrementList');

    //if (ResSal && ResArr) {
    //    ProgressBarShow();
    //    var service = new HrmsSuiteHcmService.HcmService();
    //    service.payrollTransactions(Company, DateOfPayroll, true, ResInc, ListIncIds, onGetPayroll, onPayrollError, null);
    //}
    //else
    if (ResSal) {
        if (!Lock) {
            ProgressBarShow();
            var service = new HrmsSuiteHcmService.HcmService();
            service.payrollTransactions(Company, DateOfPayroll, false, ResInc, ListIncIds, onGetPayroll, onPayrollError, null);
        } else {
            showError("Payroll alread locked of this month!");
        }
    }
        //else if (ResArr) {
        //    onContinueArrearsOnly();
        //}
        //else if (ResBonus) {
        //    ProgressBarShow();
        //    var service = new HrmsSuiteHcmService.HcmService();
        //    service.payrollBonusRelease(Company, DateOfPayroll, onGetBonusRelease, onPayrollError, null);
        //}
    else {
        showError('Please select salary checkbox');
    }
}


function HCM_Validate_PayrollMonth() {
    var CompanyId_ = $(".ddlCompany").val();
    if (CompanyId_ > 0) {
        var DateOfPayroll_ = formatDate($('.txtMonth').val().trim());
        if (DateOfPayroll_ != "") {
            if (!validateForm('.divPayrollForm'))
                return;
            var ResSal_ = $("#chkResSalary").is(':checked');
            if (ResSal_) {
                $('.payrollHead').html('');
                $('.payrollBody').html('');
                $('.alertCount').hide();
                $('.spanCount').html('');
                PayrollLogId = 0;
                ProgressBarShow();
                var service = new HrmsSuiteHcmService.HcmService();
                service.HCM_Validate_PayrollMonth(CompanyId_, DateOfPayroll_, onHCM_Validate_PayrollMonth, onHCM_Validate_PayrollMonthError, null);
            }
            else {
                showError('Please select salary checkbox');
            }
        }
        else {
            showError('Please select payroll month');
        }
    }
    else {
        showError('Please select company');
    }
}
function onHCM_Validate_PayrollMonth(result) {
    ;
    try {
        var res = jQuery.parseJSON(result);
        if (res.length > 0) {
            if (res[0].Execute_Status == "1") {
                GetPayroll();
            }
            else {
                showError(res[0].msg);
                ProgressBarHide();
            }
        } else {
            showError("Data not found!");
            ProgressBarHide();
        }
    }
    catch (e) {
        showError("Payroll Informations are not yet updated!");
        ProgressBarHide();
    }
}
function onHCM_Validate_PayrollMonthError(request, status, error) {
    ProgressBarHide();
}


function onGetPayroll(result) {
    ;
    try {
        
        var res = jQuery.parseJSON(result);
        if (res.length > 0) {
            bindTableDynamic(res, '.payrollHead', '.payrollBody');
            GetPayrollCount('.txtMonth');
            //GetPayrollLogID();
        } else {
            showError("Data not found!");
        }
    }
    catch (e) {
        showError("Payroll Informations are not yet updated!");
    }
    ProgressBarHide();
}

function onGetBonusRelease(result) {
    ;
    try {

        var res = jQuery.parseJSON(result);
        bindTableDynamicBonus(res, '.payrollHead', '.payrollBody');
        GetPayrollCount('.txtMonth');
        GetPayrollLogID();
        ProgressBarHide();
    }
    catch (e) {
        showError("Payroll Informations are not yet updated!");
        ProgressBarHide();
    }
}

function onPayrollError(request, status, error) {
    ProgressBarHide();
}

function GetPayrollLogID() {
    ProgressBarShow();
    var DateOfPayroll = formatDate($('.txtMonth').val());
    var CompanyId = $(".ddlCompany").val();
    var service = new HrmsSuiteHcmService.HcmService();
    service.getPayrollLogId(DateOfPayroll, CompanyId, onGetPayrollLogID, onGetPayrollLogIDError, null);
}

function onGetPayrollLogID(result) {
    PayrollLogId = result;
    ProgressBarHide();

}

function onGetPayrollLogIDError(result) {
    ProgressBarHide();
}

function GetEmployeeSalaryRefresh() {
    ProgressBarShow();
    var service = new HrmsSuiteHcmService.HcmService();
    service.getEmployeeSalaryRefresh(PayrollLogId, onGetEmployeeSalaryRefresh, null, null);
}

function onGetEmployeeSalaryRefresh(result) {
    var res = jQuery.parseJSON(result);
    bindTableDynamic(res, '.payrollHead', '.payrollBody');
    ProgressBarHide();
}


function GetArrearNonDispersed() {
    ProgressBarShow();
    var CompanyId = $(".ddlCompany").val();
    var service = new HrmsSuiteHcmService.HcmService();
    service.getArrearListNonDispersed(CompanyId, onGetArrearNonDispersed, null, null);
}

function onGetArrearNonDispersed(result) {
    var res = jQuery.parseJSON(result);

    var divTbodyGoalFund = $('.divArrears').html('');
    $('#NonDispersedArrearList').tmpl(res).appendTo(divTbodyGoalFund);

    if (res.length == 0 || res.length == undefined) {
        noRecord = true;
    }
    ProgressBarHide();
}


function GetLockStatus(selector) {
    ResBonus = $('#chkResBonus').is(':checked');
    if ($(selector).val() != '') {
        ProgressBarShow();
        var dateofpayroll = formatDate($(selector).val());
        var service = new HrmsSuiteHcmService.HcmService();

        if (!ResBonus) {
            service.getLockStatus(dateofpayroll, parseInt($('.ddlCompany').val()), onGetLockStatus, null, null);
        }
        else {
            service.getLockStatusBonus(dateofpayroll, parseInt($('.ddlCompany').val()), onGetLockStatus, null, null);
        }
    }
}

function GetLockStatusBonus() {
    //if ($(selector).val() != '') {
    ProgressBarShow();
    var dateofpayroll = formatDate($('.txtMonth').val());
    var CompanyId = $('.ddlCompany').val();
    var service = new HrmsSuiteHcmService.HcmService();
    service.getLockStatusBonus(dateofpayroll, CompanyId, onGetLockStatus, null, null);
    //}
}

function onGetLockStatus(result) {
    if (result == true) {
        Lock = true;
        $('.btnLock').hide()
    }
    else {
        Lock = false;
        $('.btnLock').show();
    }

    ProgressBarHide();

}

function bindTableDynamic(res, HeaderSelector, BodySelector) {
    
    if (!Lock) {
        //alert(Lock);
        $(HeaderSelector).html('');
        $(BodySelector).html('');
        var headers = '<tr class="info"><th style="text-align:center;">S.R.</th>';
        var data = '';
        PayrollLogId = res[0].PayrollMasterId_ColumnHide;
        //alert(PayrollLogId);
        $(res).each(function (i, val) {
            var holdEmployeeId = val.EmployeeId_ColumnHide;
            var holdSalaryId = val.PayrollMasterId_ColumnHide;
            data += '<tr employeeId=' + holdEmployeeId+'>';
            $.each(val, function (k, v) {
                if (k.includes('_ColumnHide') == false) {
                    if (i == 0) {
                        headers += '<th>' + k + '</th>';
                    }
                    if (k == 'Emp #') {
                        if (Lock == false) {
                            //data += '<td style="text-align:center;"> <input checked="checked" value="' + holdSalaryId + '" class="checkDisperse" type="checkbox"> </td>' +
                            data += '<td style="text-align:center;"> ' + (i + 1) + ' </td>' +
                                    '<td style="text-align:center;"> <input type="hidden" class="empId" value="' + holdEmployeeId + '" /><a><span data-toggle="modal" data-target="#CreateProjectModal" onclick="initDefaults(' + holdEmployeeId + ',' + holdSalaryId + ')" class="label label-info">' + v + '</span></a> </td>';
                        }
                        else {
                            data +=
                                //'<td style="text-align:center;"> <input checked="checked" disabled="disabled" value="' + holdSalaryId + '" class="checkDisperse" type="checkbox"></td>' +
                                    '<td style="text-align:center;"> ' + (i + 1) + ' </td>' +
                                    '<td style="text-align:center;"> <span class="label label-info">' + v + '</span></td>';
                        }
                    }
                    else {
                        data += '<td columnAttribute=' + k.replaceAll(' ', '') + '>' + replaceZeroAndNull(v) + '</td>';
                    }
                }
            });
            data += '</tr>';
        });
        headers += '</tr>';

        //var headers = '';
        //var data = '';
        //$(res[0]).each(function (i, val) {
        //    headers += '<tr class="info"><th>Desperse<th/>';
        //    $.each(val, function (k, v) {
        //        if (i > 1)
        //            headers += '<th>' + k + '</th>';
        //        i++;
        //    });
        //    headers += '</tr>';
        //});

        //$(res).each(function (i, val) {
        //    ;
        //    //alert(val.EmployeeCode);
        //    i = 0;
        //    data += '<tr>';
        //    holdEmployeeId = val.EmployeeId_ColumnHide;
        //    holdSalaryId = val.EmployeeId_ColumnHide;
        //    $.each(val, function (k, v) {

        //        //if (i == 0) {
        //        //    ;
        //        //    holdEmployeeId = v;
        //        //}
        //        //else if (i == 1)
        //        //    holdSalaryId = v;
        //        //else
        //        //    if (i == 2) {
        //        //    if (Lock == false)
        //        //        data += '<td><input checked="checked" value="' + holdSalaryId + '" class="checkDisperse" type="checkbox"><td><td><input type="hidden" class="empId" value="' + holdEmployeeId + '" /><a><span data-toggle="modal" data-target="#CreateProjectModal" onclick="initDefaults(' + holdEmployeeId + ',' + holdSalaryId + ')" class="label label-info">' + v + '<span/></a></td>';
        //        //    else
        //        //        data += '<td><input checked="checked" disabled="disabled" value="' + holdSalaryId + '" class="checkDisperse" type="checkbox"><td><td><span class="label label-info">' + v + '<span/></td>';
        //        //}
        //        //else
        //        //        data += '<td>' + replaceZeroAndNull(v) + '</td>';
        //        if (k == 'EmployeeCode') {
        //            if (Lock == false)
        //                data += '<td><input checked="checked" value="' + holdSalaryId + '" class="checkDisperse" type="checkbox"></td>' +
        //                '<td><input type="hidden" class="empId" value="' + holdEmployeeId + '" /><a><span data-toggle="modal" data-target="#CreateProjectModal" onclick="initDefaults(' + holdEmployeeId + ',' + holdSalaryId + ')" class="label label-info">' + v + '<span/></a></td>';
        //            else
        //                data += '<td><input checked="checked" disabled="disabled" value="' + holdSalaryId + '" class="checkDisperse" type="checkbox"><td><td><span class="label label-info">' + v + '<span/></td>';
        //        }
        //        else {
        //            data += '<td>' + replaceZeroAndNull(v) + '</td>';
        //        }


        //        i++;
        //    });

        //    data += '</tr>';
        //});

        $(HeaderSelector).append(headers);
        $(BodySelector).append(data);
    }
    else {
        $('.divPayroll').html('');
        $('.divPayroll').append(res[0].PayrollHtml);
        $('.payrollBody').find('tr').each(function () {
            $(this).find('td:first').find('input').prop('disabled', true);
            var objTD = $(this).find('a').closest('td');
            var tdTxt = $(this).find('a').html();
            tdTxt = tdTxt.replace('#CreateProjectModal', '');
            $(objTD).html('');
            $(objTD).append(tdTxt);
        });
    }

}

function bindTableDynamicBonus(res, HeaderSelector, BodySelector) {

    if (!Lock) {
        $(HeaderSelector).html('');
        $(BodySelector).html('');

        var headers = '';
        var data = '';


        $(res[0]).each(function (i, val) {
            headers += '<tr class="info"><th>Desperse<th/>';
            $.each(val, function (k, v) {
                if (i > 1)
                    headers += '<th>' + k + '</th>';
                i++;
            });
            headers += '</tr>';
        });

        $(res).each(function (i, val) {
            i = 0;
            data += '<tr>';
            holdEmployeeId = "";
            holdSalaryId = "";
            $.each(val, function (k, v) {

                if (i == 0)
                    holdEmployeeId = v;
                else if (i == 1)
                    holdSalaryId = v;
                else if (i == 2) {
                    if (Lock == false)
                        data += '<td><input checked="checked" value="' + holdSalaryId + '" class="checkDisperse" type="checkbox"><td><td><input type="hidden" class="empId" value="' + holdEmployeeId + '" /><span class="label label-info">' + v + '<span/></td>';
                    else
                        data += '<td><input checked="checked" disabled="disabled" value="' + holdSalaryId + '" class="checkDisperse" type="checkbox"><td><td><span class="label label-info">' + v + '<span/></td>';
                }
                else
                    data += '<td>' + replaceZeroAndNull(v) + '</td>';
                i++;
            });

            data += '</tr>';
        });

        $(HeaderSelector).append(headers);
        $(BodySelector).append(data);
    }
    else {
        $('.divPayroll').html('');
        $('.divPayroll').append(res[0].PayrollHtml);
        $('.checkDisperse').prop('disabled', true);
        //$('.payrollBody').find('tr').each(function () {
        //    $(this).find('td:first').find('input').prop('disabled', true);
        //    var objTD = $(this).find('a').closest('td');
        //    var tdTxt = $(this).find('a').html();
        //    tdTxt = tdTxt.replace('#CreateProjectModal', '');
        //    $(objTD).html('');
        //    $(objTD).append(tdTxt);
        //});
    }

}


function SaveOverTime() {
    var service = new HrmsSuiteHcmService.HcmService();
    OverTimeHours = $('.txtHours').val();
    OverTimeAmount = $('.txtAmountExpected').val();
    service.SaveOverTime(OverTimeAmount, SalaryId, OverTimeHours, OverTimeRateId, onSaveOverTime, null, null);
}

function onSaveOverTime(result) {
    var res = jQuery.parseJSON(result);

    if (res == "1") {
        var overtimeAmount = $('.txtAmountExpected').val();
        var objTotalAllowance = $('.payrollHead').find('tr').find('th:nth-last-child(3)');
        $('<th>Overtime</th>').insertBefore(objTotalAllowance);
        $('.payrollBody').find('.empId').each(function () {
            var empId = $(this).val();
            if (EmployeeId == empId) {
                var objTotalAllowance = $(this).closest('tr').find('td:nth-last-child(3)');
                var objNetAmount = $(this).closest('tr').find('td:last');

                $('<td>' + overtimeAmount + '</td>').insertBefore(objTotalAllowance);

                var TotalAllowance = objTotalAllowance.text() == '-' ? '0' : objTotalAllowance.text();
                TotalAllowance = parseFloat(TotalAllowance) + parseFloat(overtimeAmount);

                var NetAmount = objNetAmount.text() == '-' ? '0' : objNetAmount.text();
                NetAmount = parseFloat(NetAmount) + parseFloat(overtimeAmount);

                objTotalAllowance.text(TotalAllowance);
                objNetAmount.text(NetAmount);
            }
        });
        showSuccess("Overtime Inserted Successfully!");
    }
    else if (res == "0")
        showSuccess("Overtime Updated Successfully!");
    else
        showError(res);

    resetControls('.divOverTime');

    GetCompanySettings();
}

function manageOvertime() {
    $('.close').click();
    resetControls('.divOverTime');
    GetCompanySettings();
}

function GetCompanySettings() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getCompanySettings(HCM_SetupMaster.OverTimeRate, CompanyId, onGetCompanySettings, null, null);

}

function onGetCompanySettings(result) {
    var res = jQuery.parseJSON(result);
    $('.txtOverTimeRate').val(res[0].Value);
    OverTimeRateId = res[0].ID;
}

function calculateOverTimeAmount() {
    OverTimeRate = $('.txtOverTimeRate').val();
    Hours = $('.txtHours').val();
    $('.txtAmountExpected').val(OverTimeRate * Hours);
    $('.txtAmountExpected').val(OverTimeRate * Hours);
}

function GetEmployeeSalaryAllowances() {
   
    var service = new HrmsSuiteHcmService.HcmService();
    service.getEmployeeSalaryAllowances(EmployeeId,SalaryId, onGetEmployeeSalaryAllowances, null, null);
}

function onGetEmployeeSalaryAllowances(result) {

    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.divExtraAllowance').html('');
    $('#SalaryEmployeeAllowances').tmpl(res).appendTo(divTbodyGoalFund);
    validateNumeric();
    oSerialized = serializeData('.divExtraAllowance');
    $("#ExtraAllowance").show();
}

function enableFieldAllowanceAmount(selector) {
    if ($(selector).is(':checked'))
        $(selector).closest('tr').find('.txtAllowanceAmount').removeAttr('disabled');
    else
        $(selector).closest('tr').find('.txtAllowanceAmount').prop('disabled', true);

}

function getUpdatedAllowance() {
   
    var ResponseForm = [];
    $('.trUpdatedAllowances').each(function () {
        if ($(this).find('.checkAllowanceAmount').is(':checked')) {
            var Response = new Object();
            Response.PayrollMasterId = SalaryId;
            Response.AllowanceID = $(this).find('.hdAllowanceID').val();
            Response.AllowanceAmount = $(this).find('.txtAllowanceAmount').val();
            Response.Amount = $(this).find('.txtAllowanceAmount').val();
            Response.AllownceName = $(this).find('td.AllownceName').html();
            Response.EmployeeId = EmployeeId;
            Response.Month = formatDate($('.txtMonth').val());

            ResponseForm.push(Response);
        }
    });

    $('.divNewAllowanceAdd').each(function () {
        var Response = new Object();
        Response.PayrollMasterId = SalaryId;
        Response.AllowanceID = $(this).find('.ddlNewAllowance').val();
        Response.AllowanceAmount = $(this).find('.txtAllowanceAmount').val();
        Response.Amount = $(this).find('.txtAllowanceAmount').val();
        Response.AllownceName = $(this).find('.ddlNewAllowance option:selected').text();
        Response.EmployeeId = EmployeeId;
        Response.Month = formatDate($('.txtMonth').val());

        ResponseForm.push(Response);
    });
    var JSONResponse = JSON.stringify(ResponseForm);
    return JSONResponse;
}

function SaveUpdatedAllowance() {
    
    if (oSerialized == serializeData('.divExtraAllowance')) {
        showError('No Value Has Been Updated!');
        return;
    }
    ProgressBarShow();
    var JSON = getUpdatedAllowance();
    var service = new HrmsSuiteHcmService.HcmService();
    service.saveUpdatedAllowances(JSON, onSaveUpdatedAllowance, null, null);
}

function onSaveUpdatedAllowance(result) {
    var res = jQuery.parseJSON(result);
    if (res == "1")
        showSuccess('Salary Updated Successfully!');
    ProgressBarHide();
    UpdateGeneratePayRollGridAfterAllownceAdd();
    //GetEmployeeSalaryRefresh();
}

function UpdateGeneratePayRollGridAfterAllownceAdd() {

    let _rowGetAgainstEmployeeId = $("#tblPayrollGenerate").find('tr[employeeid=' + EmployeeId + ']');
    let _generatedObjectOfAllownce = JSON.parse(getUpdatedAllowance());

    $(_generatedObjectOfAllownce).each(function (i, v) {
      
        $(_rowGetAgainstEmployeeId).find('td[columnattribute=' + v.AllownceName.replaceAll(' ', '') + ']').html(v.AllowanceAmount);

    })




}

function getNonDespersedSalaries() {
    str = '';
    $('.checkDisperse').each(function () {
        if (!$(this).is(':checked'))
            str += $(this).val() + ',';
    });
    str += '0';
    return str;
}

function onLock() {


    var htmlPayroll = $('.divPayroll').html();
    var service = new HrmsSuiteHcmService.HcmService();

    if (!ResBonus) {
        //service.getLockStatus(htmlPayroll, onGetLockStatus, null, null);
        service.getLockStatus(formatDate($('.txtMonth ').val()), parseInt($('.ddlCompany').val()), onGetLockStatus, null, null);
    }
    else {
        service.getLockStatusBonus(formatDate($('.txtMonth ').val()), parseInt($('.ddlCompany').val()), onGetLockStatus, null, null);

    }

    IDs = getNonDespersedSalaries();
    _SalaryId = $('.checkDisperse').first().val();
    var service = new HrmsSuiteHcmService.HcmService();
    service.executePayroll(IDs, _SalaryId, htmlPayroll, formatDate($('.txtMonth ').val()), onLockExecute, null, null);

}

function onLockExecute(result) {
    if (result == 1)
        showSuccess('Payroll Excecuted and Locked Successfully');
    else
        showError('Payroll Failed to Lock');
    $('.btnLock').hide();
    Lock = 1;
    GetPayroll();

}

function replaceZeroAndNull(val) {
    if (val == "0" || val == null)
        return "-";
    else
        return val;
}

function onChangeAllCheckBoxArrears() {

    Checked = $("#chkArrearsList").is(':checked');


    if (Checked) {
        $('.chkNonDispersedArrears').each(function () {
            $(this).prop('checked', true);
        });

    }
    else {
        $('.chkNonDispersedArrears').each(function () {
            $(this).prop('checked', false);
        });
    }

}

function onContinueArrearsOnly() {
    ArrearTypeIds = getCommaSeparatedValues_CheckedBoxes('.ArrearTypeId');
    DateOfPayroll = formatDate($('.txtMonth').val());
    var service = new HrmsSuiteHcmService.HcmService();
    service.payrollArrearRelease(CompanyId, DateOfPayroll, ArrearTypeIds, onBindArrearResult, onPayrollError, null);
}

function onBindArrearResult(result) {
    try {
        res = jQuery.parseJSON(result);
        bindTableDynamic(res, '.payrollHead', '.payrollBody');
        GetPayrollLogID();
        ProgressBarHide();
    }
    catch (e) {
        showError("Payroll information is not yet completed!");
        ProgressBarHide();
    }
}

function GetPayrollCount(selector) {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getPayrollCount(formatDate($(selector).val()), CompanyId, onGetPayrollCount, null, null);
}

function onGetPayrollCount(result) {

    if (parseFloat(result) >= 0)
        $('.alertCount').show();
    else
        $('.alertCount').hide();

    $('.spanCount').html('');
    $('.spanCount').append(parseFloat(result));
}

function CheckSettings(selector) {
    var valSelector = $(selector).val();
    var checkStatus = $(selector).prop('checked');
    if (valSelector == 'Inc') {
        $('.chkbxBonus').prop('checked', false);

        if (checkStatus) {
            $('.divInc').show();
            GetIncrementType();
        }
        else
            $('.divInc').hide();
    }

    if (valSelector == 'Arr') {
        $('.chkbxBonus').prop('checked', false);

        if (checkStatus) {
            $('.divArrears').show();
            GetArrearNonDispersed();
        }
        else {
            $('.divArrears').hide();
        }

    }

    if (valSelector == 'Bonus') {

        $('.resSalary').prop('checked', false);
        $('.ChkLstUpgd').prop('checked', false);
        $('.resArrear').prop('checked', false);
        $('.divInc').hide();

        //GetLockStatusBonus();
    }

    if (valSelector == 'Sal') {

        $('.chkbxBonus').prop('checked', false);

    }
}

function GetIncrementType() {
    ProgressBarShow();
    var service = new HrmsSuiteHcmService.HcmService();
    service.getFromSetupDetail(0, 0, HCM_SetupMaster.IncrementType, onGetIncrementType, null, null);
}

function onGetIncrementType(result) {
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.divIncList').html('');
    $('#CheckListIncrement').tmpl(res).appendTo(divTbodyGoalFund);
    ProgressBarHide();
}

function ManageTaxDeduction() {
    //$('.close').click();
    //resetControls('.divOverTime');
    //GetCompanySettings();
    ProgressBarShow();
    $('.txtTaxBalance').val('');
    $('.txtTaxDeduction').val('');
    $('.txtTaxDeductionNew').val('');
    var service = new HrmsSuiteHcmService.HcmService();
    service.GetTaxDetailsForTaxUpdate(CompanyId, EmployeeId, PayrollLogId, onGetManageTaxDeduction, null, null);
}

function onGetManageTaxDeduction(result) {
    var res = jQuery.parseJSON(result);
    if (res.length > 0) {
        $('.txtTaxDeduction').val(res[0].TaxPaid);
        $('.txtTaxBalance').val(res[0].TaxBalance);
        EmployeeTaxId = res[0].EmployeeTaxId;

        $("#TaxDeductionManagement").show();
    }
    ProgressBarHide();
}

function SaveTaxDeduction() {

    if (!validateForm('.divTaxDeduction'))
        return;
    var Current_Month_Deduction_New = parseFloat($('.txtTaxDeductionNew').val() == "" ? "0" : $('.txtTaxDeductionNew').val());
    var TaxBalance = parseFloat($('.txtTaxBalance').val() == "" ? "0" : $('.txtTaxBalance').val());
    if (Current_Month_Deduction_New <= TaxBalance) {
        var service = new HrmsSuiteHcmService.HcmService();
        service.SaveTaxDeduction(parseInt(EmployeeTaxId), parseFloat($('.txtTaxDeductionNew').val()), onSaveTaxDeduction, null, null);
    } else {
        showError("Current Month Deduction (New) should be less or equal to Tax Balance");
    }
}

function onSaveTaxDeduction(result) {
    if (result == "1") {
        $('.txtTaxBalance').val('');
        $('.txtTaxDeduction').val('');
        $('.txtTaxDeductionNew').val('');
        showSuccess("Tax Deduction Updated Successfully!");
    }
    else {

        showError(result);
    }
}


function ManageCarInstallmentDeduction() {
    //$('.close').click();
    //resetControls('.divOverTime');
    //GetCompanySettings();
    ProgressBarShow();
    var service = new HrmsSuiteHcmService.HcmService();
    service.GetCarDetailsForCarDeductionUpdate(CompanyId, EmployeeId, PayrollLogId, onManageCarInstallmentDeduction, null, null);
}

function onManageCarInstallmentDeduction(result) {


    var res = jQuery.parseJSON(result);

    if (res.length > 0) {
        $('.txtCarInstallmentDeduction').val(res[0].InstallmentAmount);
        $('.txtCarDeductionBalance').val(res[0].Balance);
        carDeductionBlnc = res[0].Balance;
        carInsDeduction = res[0].InstallmentAmount;
        VehicleDetailId = res[0].VehicleDetailId;
    }
    else {
        $('.txtCarInstallmentDeduction').val('');
        $('.txtCarDeductionBalance').val('');
        carDeductionBlnc = 0;
        carInsDeduction = 0;
        //VehicleDetailId = res[0].VehicleDetailId;
    }
    $("#ModalCarDeduction").show();
    ProgressBarHide();
}

function calculationCarDeduction(val) {
   
    let _carInstallmentDeduction = parseFloat(carInsDeduction != "" ? carInsDeduction : 0);
    let _carDeductionBalance = parseFloat(carDeductionBlnc != "" ? carDeductionBlnc : 0);
    let _carNewInstallment = parseFloat($(val).val());
    let _diffrenceBetweenCurrAndNewIns = 0;
    let _newBalance = 0;

    if (_carNewInstallment >= 0 && _carNewInstallment != null && _carNewInstallment != undefined) {

        _diffrenceBetweenCurrAndNewIns = _carNewInstallment - _carInstallmentDeduction;

        if (_carNewInstallment > _carInstallmentDeduction) {
            _newBalance = _carDeductionBalance - _diffrenceBetweenCurrAndNewIns;
        }
        else if (_carNewInstallment < _carInstallmentDeduction) {
            _diffrenceBetweenCurrAndNewIns = _diffrenceBetweenCurrAndNewIns * (-1);

            _newBalance = _carDeductionBalance + _diffrenceBetweenCurrAndNewIns;
        }
        $(".txtCarDeductionBalance").val(Math.abs(_newBalance));
    }
    else {
        $(".txtCarDeductionBalance").val(Math.abs(carDeductionBlnc));
       
    }
 

}


function SaveCarInstallmentDeduction() {

    if (!validateForm('.divCarInstallmentDeduction'))
        return;

    var service = new HrmsSuiteHcmService.HcmService();

    service.SaveInstallmentDeduction(parseInt(VehicleDetailId), parseFloat($('.txtCarInstallmentDeductionNew').val()), onSaveCarInstallmentDeduction, null, null);
}

function onSaveCarInstallmentDeduction(result) {
    var res = jQuery.parseJSON(result);
    if (res == "1") {

        $('.txtCarInstallmentDeductionNew').val('');

        showSuccess("Car Installment Updated Successfully!");
    }
    else
        showError(res);
}





