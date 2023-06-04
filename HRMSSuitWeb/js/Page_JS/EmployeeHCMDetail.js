
init();

function init() {

    var fitInt = 0;
    var isAdjusted = false;
    IsIncrement = 0;
    isEditIncrement = false;
}

function TriggerPageLoads() {

    $(".btnCancelSearch").click(function () {
        $('.tbodyEmployeeListing').html('');
        paginateTable('.tableEmployee', 50);
    });
    //GetSalaryChangeHistory();
    GetIncrementType();
    init();
    toggleDiv('.divInstallment');
    toggleDiv('.divCheque');
    toggleDiv('.divVehicleDifference');
    TriggerPageLoadsLoan();
}

function GetEmployee() {
    
    Group = $(".ddlGroup").val();
    Company = $(".ddlCompany").val();
    var CompanyId = $('.ddlCompany').val();
    if (CompanyId > 0) {
        Location = $(".ddlLocation").val();
        BU = $(".ddlBU").val();
        Department = $(".ddlDepartment").val();
        CostCenter = $(".ddlCostCenter").val();
        Designation = $(".ddlDesignation").val();
        Firstname = $(".txtFirstName").val();
        Lastname = $(".txtLastName").val();
        EmpCode = $(".txtEmployeeCode").val();
        Firstname = $(".txtEmployeeName").val();
        Categoryid = $(".ddlCategoryC").val();
        ProgressBarShow();
        var service = new HrmsSuiteHcmService.HcmService();
        service.getEmployee(Group, Company, Location, BU, CostCenter, Designation, Department, Firstname, Lastname, EmpCode, Categoryid, onGetEmployee, null, null);
    } else {
        showError('Please select Company');
    }
}

function onGetEmployee(result) {
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.tbodyEmployeeListing').html('');
    $('#EmployeeListing').tmpl(res).appendTo(divTbodyGoalFund);
    paginateTable('.tableEmployee', 50);
    ProgressBarHide();
}

function GetFromSetupDetail(ParentId, MasterId, _cssClass) {
    if (ParentId != 0) {
        ParentId = $(ParentId).val();
    }
    var service = new HrmsSuiteHcmService.HcmService();

    service.getFromSetupDetail(ParentId, MasterId, onGetFromSetupDetail, null, null);
    cssClass = _cssClass;
}

function onGetFromSetupDetail(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(cssClass, res);

}


function GetArrearType(ParentId, MasterId, _cssClass) {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getFromSetupDetail(CompanyId, 0, HCM_SetupMaster.ArrearType, onGetArrearType, null, null);
}

function onGetArrearType(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference('.ddlArrearType', res);
}

function Get_Payroll_Lock_Count() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.Get_Payroll_Lock_Count(EmployeeId, onGet_Payroll_Lock_Count, null, null);
}

function onGet_Payroll_Lock_Count(result) { 
    $('.Payroll_Lock_Count').val(result);
    if (result > 0) {
        $(".txtSalaryInc").prop('disabled', false);
        $(".txtPercentage").prop('disabled', false);
        $(".ddlIncrementType").prop('disabled', false);
        $(".IncrementSalaryMonth").prop('disabled', false);
        $(".btn_SaveIncrementSalary").show();
    }
    else {
        $(".txtSalary").prop('disabled', false);
        $(".btn_SaveSalary").show();
    }
}

function GetEmployeeSalaryHistory() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getEmployeeSalaryHistory(EmployeeId, onGetEmployeeSalaryHistory, null, null);
}

function onGetEmployeeSalaryHistory(result) { 
    var res = jQuery.parseJSON(result);
    bindTabledynamicHistory(res, '.theadSalaryHistory', '.tbodySalaryHistory');
}


function GetSalaryChangeHistory() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getSalaryChangeHistory(EmployeeId, onGetSalaryChangeHistory, null, null);
}

function onGetSalaryChangeHistory(result) {
    ;
    var res = jQuery.parseJSON(result);
    var Increment = res[1];
    var General = res[0];

    //$(res).each(function (k, v) {
    //    if (v.IsIncrement)
    //        Increment.push(v);
    //    else
    //        General.push(v);
    //});
    var divTbodyGoalFund = $('.tbodySalaryChangeHistory').html('');
    $('#SalaryChangeHistory').tmpl(General).appendTo(divTbodyGoalFund);

    var divTbodyGoalFund = $('.tbodyIncrementChangeHistory').html('');
    $('#IncrementChangeHistory').tmpl(Increment).appendTo(divTbodyGoalFund);
}

function bindTabledynamicHistory(res, HeaderSelector, BodySelector) {
    
    $(HeaderSelector).html('');
    $(BodySelector).html('');

    var headers = '';
    var data = '';

    $(res[0]).each(function (i, val) { 
        headers += '<tr class="info">';
        $.each(val, function (k, v) { 
            if (k != 'EmployeeId_ColumnHide' && k != 'PayrollId' && k != 'PayrollDate' && k != 'EmployeeId_ColumnHide1' && k != 'PayrollLogId')
            { headers += '<th>' + k + '</th>'; } 
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
            

            if (k != 'EmployeeId_ColumnHide' && k != 'PayrollId' && k != 'PayrollDate' && k != 'EmployeeId_ColumnHide1' && k != 'PayrollLogId')
            {
                if (k != 'PayrollDate') {
                    if (parseFloat(v)) {

                        if (k == 'TotalAllowance')
                        {
                            data += '<td>' + '<span class="label label-info">' + v.toFixed(2) + '</span>' + '</td>';
                        }
                        else { data += '<td>' + v.toFixed(2) + '</td>'; }
                    }
                    else {
                        data += '<td>' + v + '</td>';
                    }
                }
                else
                {
                    data += '<td>' + v + '</td>';
                }    
            }
            i++;
        });

        data += '</tr>';
    });

    $(HeaderSelector).append(headers);
    $(BodySelector).append(data);
    prompNetSalary(BodySelector); 
}


function prompNetSalary(BodySelector) {
    
    $(BodySelector).find('tr').each(function () {

        

        objSalary = $(this).find('td').last();
        var Salary = objSalary.html();
        objSalary.html('');
        objSalary.append('<span class="label label-warning">' + Salary + '</span>');

        objDeductions = $(this).find('td:nth-last-child(2)');
        var Deduction = objDeductions.html();
        objDeductions.html('');
        objDeductions.append('<span class="label label-danger">' + Deduction + '</span>');

        //objAllowances = $(this).find('td:nth-last-child(3)');
        //var Allowances = objAllowances.html();
        //objAllowances.html('');
        //objAllowances.append('<span class="label label-info">' + Allowances + '</span>');
    });
}


function GetEmployeeSalary() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getEmployeeSalary(EmployeeId, onGetEmployeeSalary, null, null);
}

function onGetEmployeeSalary(result) {
    DefaultSalary = result;
    $('.txtSalary').val(result);
    $('.txtSalaryInc').val(result);
}

function GetSalaryStandard() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getSalaryStandard(CompanyId, onGetSalaryStandard, null, null);
}

function onGetSalaryStandard(result) {
    var res = jQuery.parseJSON(result);
    $('.txtSalaryStandard').val(res[0].Standard);
    $('.hdnStandard').val(res[0].SetupDetailId);
}


function SalaryRadioClick(selector) {

    if ($(selector).val() == 1) {
        $('.divIncrementMonth').show();
        $('.divIncrementPercentage').show();
        $('.txtPercentage').val(0);
        $('.txtPercentage').prop('disabled', false);

    }
    else {
        $('.divIncrementMonth').hide();
        $('.IncrementSalaryMonth').val('');
        $('.divIncrementPercentage').hide();
        $('.txtPercentage').val(1);
        $('.txtPercentage').prop('disabled', true);
    }
    IsIncrement = $(selector).val();
}


function onSaveSalary(selector, isIncrement) {
    if (!isIncrement) {
        Salary = $('.txtSalary').val();
        if (!validateForm('.divNormalSalaryForm'))
            return;
        if (DefaultSalary == Salary) {
            showError('Values Has Not Been Updated!');
            return;
        }
        else if (Salary <= 0) {
            showError('Please Enter The Employee Salary!');
            return;
        }


        var objDiv = $(selector).closest('div');
        SetupDetailId = objDiv.find('.hdnStandard').val();


        IncrementDate = '';
        var service = new HrmsSuiteHcmService.HcmService();
        service.saveSalaryInformation(EmployeeId, SetupDetailId, Salary, false, IncrementDate, null, onSaveSalaryInformation, null, null);
    }
    else {

        if (!validateForm('.divIncrementSalaryForm'))
            return;
        var stopInc = false;
        var objDiv = $(selector).closest('div').closest('div');
        var SetupDetailId = objDiv.find('.hdnStandard').val();
        var SalaryInc = $('.txtSalaryInc').val();
        var IncrementDate = $('.IncrementSalaryMonth').val();
        var IncrementTypeId = $('.ddlIncrementType').val();
        if (SalaryInc < DefaultSalary) {
            if (confirm("Salary Cannot Be Demoted.Do you want to save demoted salary?") == true) {

            }
            else {
                return;
            }
        }
        else {
            if (DefaultSalary == SalaryInc) {
                showError('Values Has Not Been Updated!');
                return;
            } else if (SalaryInc <= 0) {
                showError('Please Enter The Employee Salary!');
                return;
            }
        }
     

        $('.trIncRecord').each(function () {
            var IsGranted = $(this).find('.hdIsGranted').val();
            if (IsGranted == 'false') {
                var Salary = $(this).find('.hdnSalaryInc').val();
                var hdnIncTypeId = $(this).find('.hdnIncrementTypeId').val();
                if (!isEditIncrement) {
                    if (hdnIncTypeId == IncrementTypeId) {
                        showError('You already have pending increments for this Increment type');
                        stopInc = true;
                    }
                }
                if (Salary > SalaryInc) {
                    showError('You already have pending increments with Greater values');
                    stopInc = true;
                }
            }
        });

        if (stopInc)
            return;

        if (confirm("Are you sure?") == true) {
            ProgressBarShow();
            var service = new HrmsSuiteHcmService.HcmService();
            service.saveSalaryInformation(EmployeeId, SetupDetailId, SalaryInc, true, IncrementDate, IncrementTypeId, onSaveSalaryInformation, null, null);
        }
    }
}

function onSaveSalaryInformation(result) {
    if (result > 0) {
        showSuccess('Salary Has Been Updated');
        $('.txtPercentage').val('');
        $('.IncrementSalaryMonth').val('');
        $('.ddlIncrementType').val('0');

        GetEmployeeSalary();
        GetSalaryChangeHistory();
        GetMappedAllowances();
    }
    ProgressBarHide();
}

function GeneratePercentage() {
    var NewSalary = $('.txtSalaryInc').val();
    var change = (NewSalary - DefaultSalary) / DefaultSalary;
    var change = change * 100;
    $('.txtPercentage').val(change.toFixed(2));
}

function GenerateSalaryChange() {
    var NewPercentage = $('.txtPercentage').val();
    var change = NewPercentage / 100;
    var change = parseFloat(DefaultSalary) + (change * DefaultSalary);
    $('.txtSalaryInc').val(change);
}

function GetIncrementType() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getFromSetupDetail(0, 0, HCM_SetupMaster.IncrementType, onGetIncrementType, null, null);
}

function onGetIncrementType(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference('.ddlIncrementType', res);
}



function DeleteIncrement(selector) {
    if (confirm("Are you sure you want to delete?") == true) {
        objTr = $(selector).closest('tr');
        var SalaryId = objTr.find('.hdnEmpSalaryId').val();
        var IsGranted = objTr.find('.hdIsGranted').val();

        if (IsGranted == 'true') {
            showError('Granted increment cannot be deleted.');
            return;
        }

        var service = new HrmsSuiteHcmService.HcmService();
        service.increment_DeleteBySalaryId(SalaryId, onDeleteIncrement, null, null);
    }
}


function onDeleteIncrement(result) {
    if (result == 1) {
        $('.txtPercentage').val('');
        $('.IncrementSalaryMonth').val('');
        $('.ddlIncrementType').val('0');

        showSuccess('Successfully Deleted');
        GetEmployeeSalary();
        GetSalaryChangeHistory();
    }
}



function EditIncrement(selector) {
    $('.txtPercentage').val('');
    $('.IncrementSalaryMonth').val('');
    $('.ddlIncrementType').val('0');


    objTr = $(selector).closest('tr');

    var Salary = objTr.find('.hdnSalaryInc').val();
    var IsGranted = objTr.find('.hdIsGranted').val();
    var IncTypeId = objTr.find('.hdnIncrementTypeId').val();
    var EmpSalaryId = objTr.find('.hdnEmpSalaryId').val();
    var InitiatedDate = objTr.find('.tdIncInitiatedDate').html();
    var IncPercent = ((Salary - DefaultSalary) / DefaultSalary) * 100.0;
    IncPercent = parseInt(IncPercent);
    if (IsGranted == 'true') {
        showError('Granted increment cannot be edited.');
        return
    }
    else {
        $('.txtSalaryInc').val(Salary);
        $('.ddlIncrementType').val(IncTypeId);
        $('.IncrementSalaryMonth').val(_GetDate(InitiatedDate));
        $('.txtPercentage').val(IncPercent);
        isEditIncrement = true;
    }

}


