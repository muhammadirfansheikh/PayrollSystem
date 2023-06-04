


function TriggerPageLoads() {
    $('.btnSearch').hide();
    $('.btnSaveChanges').hide();
    $('.divInc').hide();
    HideAllDivs();
    IsAbsent = false;
    holdClicked = false;
    HoldAll = false;
    ResumeAll = false;
    $('.panelProcessOptions').hide();
}


function GetEmployee(selector) {
    holdClicked = false;
    toManage = $(selector).val();
    switchColor(selector);

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
    Categoryid = $(".ddlCategory").val();
    var service = new HrmsSuiteHcmService.HcmService();

    if (toManage == 'IncTaxForcast') {


        service.getEmployeeForIncTaxForcast(Group, Company, Location, BU, CostCenter, Designation, Department, Firstname, Lastname, EmpCode, Categoryid, onGetEmployee, null, null);

    }
    else if (toManage == 'Wppf') {
        service.getWppf(Company, onGetEmployee, null, null);
    }
    else {
        service.getEmployee(Group, Company, Location, BU, CostCenter, Designation, Department, Firstname, Lastname, EmpCode, Categoryid, onGetEmployee, null, null);
    }
}

function switchColor(selector) {
    $('.btnFeature').each(function () {
        $(this).removeClass("btn-warning")
        $(this).addClass("btn-info");
    });
    $(selector).removeClass("btn-info")
    $(selector).addClass("btn-warning");
}


function onGetEmployee(result) {
    var res = jQuery.parseJSON(result);
    var divTbodyGoalFund = $('.tbodyEmployeeListing').html('');
    $('#EmployeeListing').tmpl(res).appendTo(divTbodyGoalFund);
    paginateTable('.tableEmployee', 50);
    $('.panelProcessOptions').show();
    ManageSetups();
    ProgressBarHide();
}


function PopulateSalaryAmount(selector) {
    var Percentage = $(selector).val();
    if (Percentage == '' || Percentage == undefined)
        Percentage = 0;
    var objTR = $(selector).closest('tr');
    var EmpSalary = objTR.find('.EmpSalary').val();
    Percentage = Percentage / 100;
    objTR.find('.txtSalary').val(parseFloat(EmpSalary) + (EmpSalary * Percentage));
}


function PopulateSalaryPercent(selector) {
    var Salary = $(selector).val();
    if (Salary == '' || Salary == undefined)
        Salary = 0;
    var objTR = $(selector).closest('tr');
    var EmpSalary = objTR.find('.EmpSalary').val();
    Salary = (Salary - EmpSalary) / EmpSalary;
    objTR.find('.txtPercentage').val((Salary * 100).toFixed(2));
}


function GlobalPercentageEffect(selector) {
    var effectVal = $(selector).val();
    if (effectVal == '')
        effectVal = 0;

    $('.txtPercentage').each(function () {
        var EmpSalary = $(this).closest('tr').find('.EmpSalary').val();
        if (!(isNaN(EmpSalary) || EmpSalary == undefined || EmpSalary == ''))
            $(this).val(effectVal).keyup();
    });
}

function GlobalPercentageIncTaxForcastEffect(selector) {
    var effectVal = $(selector).val();
    if (effectVal == '')
        effectVal = 0;

    $('.txtIncTaxForcastPercentage').each(function () {
        var EmpSalary = $(this).closest('tr').find('.EmpSalary').val();
        //if (!(isNaN(EmpSalary) || EmpSalary == undefined || EmpSalary == ''))
        $(this).val(effectVal).keyup();
    });
}

function GlobalAmountEffect(selector) {
    var effectVal = $(selector).val();
    if (effectVal == '')
        effectVal = 0;
    $('.txtSalary').each(function () {
        var EmpSalary = $(this).closest('tr').find('.EmpSalary').val();
        if (isNaN(EmpSalary) || EmpSalary == undefined || EmpSalary == '')
            EmpSalary = 0;
        $(this).val(parseFloat(EmpSalary) + parseFloat(effectVal)).keyup();
    });
}

function ClearInput(selector) {
    $(selector).closest('div').find('input').val('').keyup();
}

function ManageSetups() {
    HideAllDivs();
    $('.divIncForcast').hide();
    $('.mainTitle').html('');
    if (toManage == 'Inc') {
        $('.mainTitle').append('Manage Increment');
        $('.divInc').show();
    }
    else if (toManage == 'PF') {
        $('.mainTitle').append('Manage Provident Fund');
        $('.divPFOpening').show();
        $('.txtPFOpening').each(function () {
            if ($(this).val() > 0) {
                $(this).prop("disabled", true);
                $(this).closest('tr').addClass("warning");
            }
        });
    }
    else if (toManage == 'Flex') {
        $('.mainTitle').append('Manage Increment');
        $('.divFlex').show();
    }
    else if (toManage == 'Sal') {
        $('.mainTitle').append('Manage Salary');
        $('.divSal').show();
    }
    else if (toManage == 'Loan') {
        $('.mainTitle').append('Manage Loan');
        $('.divLoan').show();
        $('#chk').prop('checked', false);
        GetLoan();
    }
    else if (toManage == 'HoldPF') {
        $('.mainTitle').append('Manage Provident Fund');
        $('.divHoldPF').show();
        GetPFHoldRecord();
    }
    else if (toManage == 'InterestInc') {
        $('.mainTitle').append('Manage Interest Income');
        $('.divInterestInc').show();
        //$('.YearOf').change();
    }
    else if (toManage == 'SESSILim') {
        $('.mainTitle').append('Manage SESSI Limit');
        $('.divSESSILimit').show();
    }
    else if (toManage == 'OvertimeHours') {
        $('.mainTitle').append('Manage Overtime Hours');
        $('.divOvertime').show();
    }
    else if (toManage == 'LeaveEncashments') {
        $('.mainTitle').append('Manage Leave Encashments');
        $('.divLeaveEncashment').show();
    }
    else if (toManage == 'IncTaxForcast') {
        $('.mainTitle').append('Increment Tax Forcast');
        $('.divIncForcast').show();
        $('.divIncForcastChkAll').show();

        GetIncTaxForcastRecords();
    }
    else if (toManage == 'Wppf') {
        $('.mainTitle').append('Manage WPPF');
        $('.divWppf').show();

        GetYearWppf();
    }
    else if (toManage == 'TaxRefund') {
        $('.mainTitle').append('Manage Tax Refund');
        $('.divTaxRefund').show();

        GetYearTaxRefund();
    }

    $('.btnSaveChanges').show();
}

function saveChanges() {
    ResAll = $("#chkAll").is(':checked');
    var ValuesUpdates = 0;
    if (toManage == 'Inc') {
        $('.txtSalary').each(function () {
            var IncSal = $(this).val();
            var EmpSalary = $(this).closest('tr').find('.EmpSalary').val();
            var EmployeeId = $(this).closest('tr').find('.EmployeeId').val();
            if (isNaN(EmpSalary) || EmpSalary == undefined || EmpSalary == '')
                EmpSalary = 0;
            if (IncSal > EmpSalary && EmpSalary != 0 && IncSal != 0) {
                ValuesUpdates += 1;
            }
        });

        if (ValuesUpdates == 0) {
            showError('Value Has Not Been Updated');
        }

    }
    else if (toManage == 'PF') {
        var JSONFORM = [];
        $('.txtPFOpening').each(function () {
            if (!$(this).prop('disabled') && $(this).val() > 0) {
                var Response = new Object();
                var EmployeeId = $(this).closest('tr').find('.EmployeeId').val();
                Response.EmployeeId = EmployeeId;
                Response.TotalBalance = $(this).val();
                JSONFORM.push(Response);
            }
        });
        var JSONResponse = JSON.stringify(JSONFORM);
        var service = new HrmsSuiteHcmService.HcmService();
        service.multimanage_SavePFOpening(JSONResponse, onMultiManageSavePF, null, null);
    }
    else if (toManage == 'Flex') {
        if ($('.dtMonthOfAbsents').val() == '' || $('.dtMonthOfAbsents').val() == undefined) {
            showError('Please Select Month');
            return;
        }
        var JSONFORM = [];
        $('.txtAbsentFlexi').each(function () {
            if (!$(this).prop('disabled') /*&& $(this).val() > 0*/) {
                var value = 0;
                if ($(this).val().trim() != '')
                    value = $(this).val();
                var Response = new Object();
                var EmployeeId = $(this).closest('tr').find('.EmployeeId').val();
                Response.EmployeeId = EmployeeId;
                Response.AbsentFlexiCount = value;
                Response.AbsentFlexMonth = formatDate($('.dtMonthOfAbsents').val());
                Response.IsAbsent = IsAbsent;
                JSONFORM.push(Response);
            }
        });
        var JSONResponse = JSON.stringify(JSONFORM);
        var service = new HrmsSuiteHcmService.HcmService();
        service.multimanage_SaveFlexiAbsents(JSONResponse, onMultiManageSaveFlexiAbsents, null, null);
        ProgressBarShow();
    }
    else if (toManage == 'Sal') {
        var JSONFORM = [];
        $('.txtSalaryOP').each(function () {
            var newSalary = $(this).val();
            var EmpSalary = $(this).closest('tr').find('.EmpSalary').val();
            var EmployeeId = $(this).closest('tr').find('.EmployeeId').val();
            if (isNaN(EmpSalary) || EmpSalary == undefined || EmpSalary == '')
                EmpSalary = 0;
            if (newSalary != EmpSalary && newSalary != 0) {
                var Response = new Object();
                Response.EmployeeID = EmployeeId;
                Response.GrossSalary = newSalary;
                Response.BasicSalary = 0;
                Response.IsIncrement = false;
                JSONFORM.push(Response);
            }
        });
        var JSONResponse = JSON.stringify(JSONFORM);
        var service = new HrmsSuiteHcmService.HcmService();
        service.multimanage_SaveSalaries(JSONResponse, onMultiManageSaveSalaries, null, null);
        ProgressBarShow();
    }
    else if (toManage == 'InterestInc') {

        //IntrestIncome_AddToBalance();

        var JSONFORM = [];
        $('.recordEmp').each(function () {

            var PrevIntIncome = $(this).closest('tr').find('.txtPrevIntIncome').val();
            var IntRate = $(this).closest('tr').find('.txtIntRate').val();
            var PFBalance = $(this).closest('tr').find('.txtPFBalance').val();
            var IntIncome = $(this).closest('tr').find('.txtIntIncome').val();
            var EmployeeId = $(this).closest('tr').find('.EmployeeId').val();

            var Response = new Object();
            Response.EmployeeId = EmployeeId;
            Response.YearOf = $('.YearOf').val();
            Response.PrevYearIntrestIncome = PrevIntIncome;
            Response.CurrentPFBalance = PFBalance;
            Response.IncomeInterest_Rate = IntRate;
            Response.InterestIncome = IntIncome;
            Response.IsActive = true;
            JSONFORM.push(Response);

        });
        var JSONResponse = JSON.stringify(JSONFORM);
        var service = new HrmsSuiteHcmService.HcmService();
        service.multimanage_SaveInterestIncome(JSONResponse, onMultiManageSaveInterestIncome, null, null);
        ProgressBarShow();
    }
    else if (toManage == 'SESSILim') {

        var JSONFORM = [];
        $('.txtSessiLim').each(function () {
            if (!$(this).prop('disabled')) {
                var value = 0;
                if ($(this).val().trim() != '')
                    value = $(this).val();
                var Response = new Object();
                var EmployeeId = $(this).closest('tr').find('.EmployeeId').val();
                Response.EmployeeId = EmployeeId;
                Response.SESSILimit = value;
                JSONFORM.push(Response);
            }
        });
        var JSONResponse = JSON.stringify(JSONFORM);
        var service = new HrmsSuiteHcmService.HcmService();
        service.multimanage_SaveSESSILimit(JSONResponse, onMultiManageSaveSESSI, null, null);

    }
    else if (toManage == 'OvertimeHours') {
        var OverTimeMonth = formatDate($('.dtMonthOfOvertime').val());

        var JSONFORM = [];
        $('.txtOverTimeHours').each(function () {
            if (!$(this).prop('disabled')) {
                var value = 0;
                if ($(this).val().trim() != '')
                    value = $(this).val();
                var Response = new Object();
                var EmployeeId = $(this).closest('tr').find('.EmployeeId').val();
                Response.EmployeeId = EmployeeId;
                Response.OvertimeHours = value;
                Response.Month = OverTimeMonth;
                JSONFORM.push(Response);
            }
        });
        var JSONResponse = JSON.stringify(JSONFORM);
        var service = new HrmsSuiteHcmService.HcmService();
        service.multimanage_SaveOvertimeHours(JSONResponse, onMultiManageSaveOverTime, null, null);
    }
    else if (toManage == 'LeaveEncashments') {

        if (!validateForm('.divLeaveEncashValidate')) {

            showError('Please Enter the Year');
            return;
        }

        var LeaveEncashmentYear = $('.dtYearOfLeaveEncashment').val();

        var JSONFORM = [];
        $('.txtLeaveEncashments').each(function () {
            if (!$(this).prop('disabled')) {
                var value = 0;
                if ($(this).val().trim() != '')
                    value = $(this).val();
                var Response = new Object();
                var EmployeeId = $(this).closest('tr').find('.EmployeeId').val();
                Response.EmployeeId = EmployeeId;
                Response.TotalLeaves = value;
                Response.EncashYear = LeaveEncashmentYear;
                JSONFORM.push(Response);
            }
        });
        var JSONResponse = JSON.stringify(JSONFORM);
        var service = new HrmsSuiteHcmService.HcmService();
        service.multimanage_LeaveEncashment(JSONResponse, onMultiManageSaveLeaveEncashment, null, null);
    }
    else if (toManage == 'IncTaxForcast') {
       
        if (ResAll) {
            
            var CompanyId = $('.ddlCompany').val();
            var PercentGlobal = $('.txtGlobalPercentEffect').val();

            var service = new HrmsSuiteHcmService.HcmService();
            service.multimanage_IncTaxForcastAll(CompanyId, PercentGlobal, onMultiManageSaveIncTaxForcast, null, null);
        }
        else {

            var JSONFORM = [];
            $('.txtIncTaxForcastPercentage').each(function () {
                if (!$(this).prop('disabled') /*&& $(this).val() > 0*/) {
                    var value = 0;
                    if ($(this).val().trim() != '')
                        value = $(this).val();
                    var Response = new Object();
                    var EmployeeId = $(this).closest('tr').find('.EmployeeId').val();
                    Response.EmployeeId = EmployeeId;
                    Response.IncPercent = value;

                    JSONFORM.push(Response);
                }
            });
            var JSONResponse = JSON.stringify(JSONFORM);
            var service = new HrmsSuiteHcmService.HcmService();
            service.multimanage_IncTaxForcast(JSONResponse, onMultiManageSaveIncTaxForcast, null, null);
        }
    }
    else if (toManage == 'Wppf') {

        if ($('.ddlYearWppf').val() == '0') {
            showError('Please Select Year');
            return;
        }

        var JSONFORM = [];

        $('.txtTotalWppf').each(function () {

            var Response = new Object();

            var EmployeeId = $(this).closest('tr').find('.EmployeeId').val();
            var CompanyId = $('.ddlCompany').val();
            var YearId = $('.ddlYearWppf').val();
            var SlabId = $(this).closest('tr').find('.hfSlabIdWppf').val();
            var UnitRate = $(this).closest('tr').find('.txtUnitRateWppf').val();
            var MaxUnitRate = $('.txtMaxUnitRateWppf').val();
            var InterestRate = $(this).closest('tr').find('.txtInterestRateWppf').val();
            var MaxInterestRate = $('.txtMaxInterestRateWppf').val();
            var TotalUnitRate = $(this).closest('tr').find('.txtTotalUnitRateWppf').val();
            var TotalInterestRate = $(this).closest('tr').find('.txtTotalInterestRateWppf').val();
            var TotalWppf = $(this).closest('tr').find('.txtTotalWppf').val();

            Response.EmployeeId = EmployeeId;
            Response.CompanyId = CompanyId;
            Response.YearId = YearId;
            Response.Slab_Id = SlabId;
            Response.UnitRate = UnitRate;
            Response.MaxUnitRate = MaxUnitRate;

            Response.InterestRate = InterestRate;
            Response.MaxInterestRate = MaxInterestRate;

            Response.UnitRateAmount = TotalUnitRate;
            Response.InterestRateAmount = TotalInterestRate;

            Response.Total_WPPF = TotalWppf;

            //Response.UnitRateAmount = UnitRateAmount;
            //Response.InterestRateAmount = InterestRateAmount;

            JSONFORM.push(Response);

        });

        var JSONResponse = JSON.stringify(JSONFORM);
        var service = new HrmsSuiteHcmService.HcmService();
        service.multimanage_Wppf(JSONResponse, onMultiManageSaveIncTaxForcast, null, null);
    }
    else if (toManage == 'TaxRefund') {
        
        var YearId = $('.ddlYearTaxRefund').val();

        if (parseInt(YearId) > 0) {
            var JSONFORM = [];
            $('.txtTaxRefund').each(function () {
                if (!$(this).prop('disabled') /*&& $(this).val() > 0*/) {
                    var value = 0;
                    if ($(this).val().trim() != '' && $(this).val().trim() != '0')
                        value = $(this).val();

                    if (parseFloat(value) > 0) {
                        var Response = new Object();
                        var EmployeeId = $(this).closest('tr').find('.EmployeeId').val();
                        Response.EmployeeId = EmployeeId;
                        Response.RefundAmount = value;
                        Response.CompanyId = $('.ddlCompany').val();

                        JSONFORM.push(Response);
                    }
                }
            });


            var JSONResponse = JSON.stringify(JSONFORM);
            var service = new HrmsSuiteHcmService.HcmService();
            service.multimanage_TaxRefund(JSONResponse, YearId, onMultiManageSaveIncTaxForcast, null, null);
        }
    }
}

function onMultiManageSaveIncTaxForcast(result) {
    if (result == 1)
        showSuccess('Successfully Updated');
}


function onMultiManageSaveLeaveEncashment(result) {
    if (result == 1)
        showSuccess('Successfully Updated');
}

function GetGlobalSESSIEffect(selector) {
    var SESSIEffect = $(selector).val();
    $('.txtSessiLim').each(function () {
        $(this).val(SESSIEffect);
    });
}


function HideAllDivs() {
    $('.divInc').hide();
    $('.divPFOpening').hide();
    $('.divFlex').hide();
    $('.divSal').hide();
    $('.divLoan').hide();
    $('.divHoldPF').hide();
    $('.divInterestInc').hide();
    $('.divSESSILimit').hide();
    $('.divOvertime').hide();
    $('.divLeaveEncashment').hide();
    $('.divIncTaxForcast').hide();
    $('.divWppf').hide();
    $('.divTaxRefund').hide();
    $('.divIncForcastChkAll').hide();
}

function onMultiManageSaveSESSI(result) {
    if (result == 1)
        showSuccess('Successfully Updated');
}

function onMultiManageSaveOverTime(result) {
    if (result == 1)
        showSuccess('Successfully Updated');
}

function GetPFHoldRecord() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.multimanage_GetPF(onMultiManage_GetPFHoldRecord, null, null);
}

function onMultiManage_GetPFHoldRecord(result) {
    var res = jQuery.parseJSON(result);
    $(res).each(function (k, v) {
        nEmpId = v.EmployeeId;
        $('.EmployeeId').each(function () {
            oEmpId = $(this).val();
            if (nEmpId == oEmpId) {
                var obj = $(this).closest('tr');
                obj.find('.lblStatusPfHold').html('');
                obj.find('.hdnStatus').val(v.OnHold);
                obj.find('.hdnPFLogId').val(v.PFLogId);
                obj.find('.lblStatusPfHold').append(v.OnHold == true ? 'On Hold' : 'In Process');
            }
        });
    });
}

function GetOvertimeDetailRecords() {
    if ($('.dtMonthOfOvertime').val() != '') {
        var OverTimeMonth = formatDate($('.dtMonthOfOvertime').val());
        var service = new HrmsSuiteHcmService.HcmService();
        service.multimanage_GetOvertimeDetailRecords(OverTimeMonth, onGetOvertimeDetailRecords, null, null);
    }
}

function onGetOvertimeDetailRecords(result) {
    var res = jQuery.parseJSON(result);
    $(res).each(function (k, v) {
        nEmpId = v.EmployeeId;
        $('.EmployeeId').each(function () {
            oEmpId = $(this).val();
            if (nEmpId == oEmpId) {
                var obj = $(this).closest('tr');
                obj.find('.txtOverTimeHours').val(v.OvertimeHours);
            }
        });
    });
}


function GetLeaveEncashmentRecords() {
    if ($('.dtYearOfLeaveEncashment').val() != '') {
        var YearLeaveEncashment = $('.dtYearOfLeaveEncashment').val();
        var service = new HrmsSuiteHcmService.HcmService();
        service.multimanage_GetLeaveEncashment(parseInt(YearLeaveEncashment), onGetLeaveEncashmentRecords, null, null);
    }
}

function onGetLeaveEncashmentRecords(result) {
    var res = jQuery.parseJSON(result);
    $(res).each(function (k, v) {
        nEmpId = v.EmployeeId;
        $('.EmployeeId').each(function () {
            oEmpId = $(this).val();
            if (nEmpId == oEmpId) {
                var obj = $(this).closest('tr');
                obj.find('.txtLeaveEncashments').val(v.TotalLeaves);
            }
        });
    });
}

function onMultiManageSaveInterestIncome(result) {
    if (result == 1) {
        showSuccess('Successfully Updated!');
    }
}





//multimanage_GetOvertimeDetailRecords


function GetLoan() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.multimanage_GetLoan(onMultiManage_GetLoan, null, null);
}

function onMultiManage_GetLoan(result) {
    var res = jQuery.parseJSON(result);

    $('.EmployeeId').each(function () {
        oEmpId = $(this).val();
        divE = $(this).closest('tr').find('.divLoantd');
        divE.html('');
        var col = '';
        col = '<table class="table table-hover"><tr class="info"><th>Loan Name</th><th>Balance</th><th>Status</th><th>Action <span onclick="showSummary(this)" class="badge badge-info badgeAction"> <i class="fa fa-plus"></i> </span> </th></tr><tbody class="tbodyLoanSummary">';
        var records = 0;
        $(res).each(function (k, v) {
            nEmpId = v.EmployeeId;
            if (oEmpId == nEmpId) {
                records += 1;
                var status = 'In Process';
                if (v.IsHold)
                    status = 'On Hold';
                if (v.Balance == -1)
                    bal = v.Amount

                col += '<tr><td class="project-title">' + v.LoanName + '</td><td>' + bal + '</td><td>' + status + '</td><td><input onclick="HoldLoan(' + v.LoanMasterID + ', ' + v.IsHold + ')" type="button" class="btn btn-info btn-xs" value="Hold"></td></tr>';
            }
        });
        if (records == 0)
            col = '';
        else
            col += '</tbody></table>';
        divE.append(col);
    });

    if (!holdClicked) {
        $('.tbodyLoanSummary').hide();
        $('.badgeAction').closest('th').siblings('th').toggle();
    }

}

function showSummary(selector) {
    $(selector).closest('table').find('.tbodyLoanSummary').slideToggle();
    $(selector).closest('th').siblings('th').slideToggle();
}


function showAllSummary(selector) {
    $('.tbodyLoanSummary').slideToggle();
    $('.badgeAction').closest('th').siblings('th').slideToggle();
}

function onMultiManageSavePF(result) {
    if (result == 1)
        showSuccess('Successfully Updated!');
    GetEmployee('.btnPF');
    ProgressBarHide();
}

function onMultiManageSaveFlexiAbsents(result) {
    if (result == 1)
        showSuccess('Successfully Updated!');
    // GetEmployee('.btnFlex');
    ProgressBarHide();

}

function onMultiManageSaveSalaries(result) {
    if (result == 1)
        showSuccess('Successfully Updated!');
    GetEmployee('.btnSal');
    ProgressBarHide();
}

function onMultiManageSaveInterestIncome(result) {
    if (result == 1)
        showSuccess('Successfully Updated!');

    ProgressBarHide();
}

function CheckPayrollLock() {
    $('.txtAbsentFlexi').each(function () {
        $(this).val(0);
    });
    var service = new HrmsSuiteHcmService.HcmService();
    service.multimanage_LockStatus(formatDate($('.dtMonthOfAbsents').val()), onCheckPayrollLock, null, null);
}

function onCheckPayrollLock(result) {
    var dateofpayroll = formatDate($('.dtMonthOfAbsents').val());
    if (result == 0) {
        $('.txtAbsentFlexi').each(function () {
            $(this).prop('disabled', true);
        });
        $('.globalFlexi').prop('disabled', true);
        $('.btnSaveChanges').hide();
        showError('Payroll Process Already Completed For The Selected Month Contact Administrator');
    }
    else {
        $('.txtAbsentFlexi').each(function () {
            $(this).prop('disabled', false);
        });
        $('.globalFlexi').val('');
        $('.globalFlexi').prop('disabled', false);
        $('.btnSaveChanges').show();
        multimanage_Flexible(dateofpayroll);
    }
}

function GlobalAbsentFlexis(selector) {
    $('.txtAbsentFlexi').each(function () {
        $(this).val($(selector).val());
    })
}

function GlobalLeaveEncashment(selector) {
    $('.txtLeaveEncashments').each(function () {
        $(this).val($(selector).val());
    })
}

function SetIsAbsent(val) {
    IsAbsent = val;
    $('.dtMonthOfAbsents').change();
}

function multimanage_Flexible(dateofpayroll) {
    var service = new HrmsSuiteHcmService.HcmService();
    service.multimanage_Flexible(dateofpayroll, IsAbsent, onmultimanage_Flexible, null, null);
}

function onmultimanage_Flexible(result) {
    var res = jQuery.parseJSON(result);

    $(res).each(function (k, v) {
        var oEmpId = v.EmployeeId
        var val = v.AbsentFlexiCount;
        $('.recordEmp').each(function () {
            var iEmpId = $(this).find('.EmployeeId').val();
            if (oEmpId == iEmpId) {
                $(this).find('.txtAbsentFlexi').val(val);
                return;
            }
        });
    })
}

function HoldLoan(LoanMasterId, isHold) {

    holdClicked = true;
    if (isHold)
        flag = false;
    else
        flag = true;
    var service = new HrmsSuiteHcmService.HcmService();
    service.holdLoanById(LoanMasterId, flag, onHoldLoan, null, null);
}


function onHoldLoan(result) {
    GetLoan();
}


function ToggleLoanRecords() {
    if ($('#chk').prop('checked')) {
        $('.divLoantd').each(function () {
            if ($(this).html() == '') {
                $(this).closest('tr').hide();
            }
            else {
                $(this).closest('tr').show();
            }
        });
    }
    else {
        $('.divLoantd').each(function () {
            if ($(this).html() == '') {
                $(this).closest('tr').show();
            }
        });
        paginateTable('.tableEmployee', 50);
    }

}


function HoldPF(selector) {
    var PFLogId = $(selector).closest('div').find('.hdnPFLogId').val();
    var status = $(selector).closest('div').find('.hdnStatus').val();
    status = status == 'true' ? false : true;

    if (!(PFLogId == '' || PFLogId == undefined)) {
        if (HoldAll) {
            var service = new HrmsSuiteHcmService.HcmService();
            service.holdProvidentFund(PFLogId, true, onHoldPF, null, null);
        }
        else if (ResumeAll) {
            var service = new HrmsSuiteHcmService.HcmService();
            service.holdProvidentFund(PFLogId, false, onHoldPF, null, null);
        }
        else {
            var service = new HrmsSuiteHcmService.HcmService();
            service.holdProvidentFund(PFLogId, status, onHoldPF, null, null);
        }
    }
}


function onHoldPF(result) {
    if (result == 1) {
        GetPFHoldRecord();
    }
}

function HoldAllPF() {
    ProgressBarShow();
    HoldAll = true;
    $('.btnHoldPf').click();
    HoldAll = false;
    ProgressBarHide();
}

function ResumeAllPF() {
    ProgressBarShow();
    ResumeAll = true;
    $('.btnHoldPf').click();
    ResumeAll = false;
    ProgressBarHide();
}


function GenInterestIncome(selector) {

    var Rate = $(selector).val() / 100.0;
    $('.hdnOpening').each(function () {

        var Val = $(this).val();
        var obj = $(this).closest('div').find('.lblGeneratedIncome');
        var hdnIncome = $(this).closest('div').find('.hdnGenereatedIncome');
        var AllowInterest = $(this).closest('div').find('.hdnAllowInterest').val();
        var IsProvided = $(this).closest('div').find('.hdnIsProvided').val();
        IsProvided = IsProvided == '' ? 'false' : IsProvided;
        if (AllowInterest && IsProvided == 'false') {
            obj.html('');
            hdnIncome.val((Val * Rate));
            obj.append((Val * Rate));
        }

    });
}

function GenerateInterestIncome(selector) {

    $('.txtIntRate').val($(selector).val());

    var Rate = $(selector).val() / 100.0;
    onChangeIntIncomeRate(Rate);
}

//function GlobalInterestRateWppf(selector) {
//    var effectVal = $(selector).val();
//    if (effectVal == '')
//        effectVal = 0;

//    $('.txtInterestRateWppf').each(function () {

//        $(this).val(effectVal).keyup();
//        onChangeInterestRate(this);
//    });
//}

function onChangeIntIncomeRate(Rate) {
    $('.txtPFBalance').each(function () {


        var PrevIntIncome = $(this).closest('tr').find('.txtPrevIntIncome').val();
        var PfBalance = $(this).closest('tr').find('.txtPFBalance').val();
        var Rate_PfBalance = parseFloat(PfBalance) * parseFloat(Rate);
        var IntIncome = parseFloat(PrevIntIncome) + parseFloat(Rate_PfBalance);

        //alert($(this).closest('div').find('.txtIntIncome'));
        $(this).closest('tr').find('.txtIntIncome').val(IntIncome);

        //var Val = $(this).val();
        //var obj = $(this).closest('div').find('.txtPrevIntIncome');
        //var hdnIncome = $(this).closest('div').find('.hdnGenereatedIncome');
        //var AllowInterest = $(this).closest('div').find('.hdnAllowInterest').val();
        //var IsProvided = $(this).closest('div').find('.hdnIsProvided').val();
        //IsProvided = IsProvided == '' ? 'false' : IsProvided;
        //if (AllowInterest && IsProvided == 'false') {
        //    obj.html('');
        //    hdnIncome.val((Val * Rate));
        //    obj.append((Val * Rate));
        //}

    });

}

function GetIncomeInterestList(selector) {

    var CompanyId = $('.ddlCompany').val();
    var YearOf = $(selector).val();
    //$('.lblGeneratedIncome').html('');
    //$('.lblGeneratedIncome').append('0');
    if (YearOf != '') {
        var service = new HrmsSuiteHcmService.HcmService();
        service.multimanage_GetInterestIncome(YearOf, CompanyId, onGetIncomeInterestList, null, null);
    }
}

function onGetIncomeInterestList(result) {
    var res = jQuery.parseJSON(result);

    //alert(result);
    $(res).each(function (k, v) {

        nEmpId = v.EmployeeId;
        $('.EmployeeId').each(function () {

            oEmpId = $(this).val();
            if (nEmpId == oEmpId) {
                var obj = $(this).closest('tr');
                obj.find('.txtPrevIntIncome').val(v.PrevYearIntrestIncome);
                obj.find('.txtIntRate').val(v.IncomeInterest_Rate);
                obj.find('.txtPFBalance').val(v.CurrentPFBalance);
                obj.find('.txtIntIncome').val(v.InterestIncome);
            }
        });
    });
    //$(res).each(function (k, v) {
    //    var oEmpId = v.EmployeeId
    //    var val = v.InterestIncome;
    //    var isProvided = v.IsProvided;
    //    $('.recordEmp').each(function () {
    //        var iEmpId = $(this).find('.EmployeeId').val();
    //        if (oEmpId == iEmpId) {
    //            if (isProvided == null || isProvided == '' || isProvided == undefined)
    //                isProvided = false;

    //            $(this).find('.lblGeneratedIncome').html('');
    //            $(this).find('.lblGeneratedIncome').append(val);
    //            $(this).find('.lblIsProvided').html('');
    //            $(this).find('.lblIsProvided').append(isProvided ? 'Granted' : 'Pending');
    //            $(this).find('.hdnIsProvided').val(isProvided);
    //            return;
    //        }
    //    });
    //})
    //$('.txtPFIncomeInterestRate').keyup();
}


function InterestIncome_SelectAllCheckBoxes(selector) {
    var isChecked = $(selector).prop('checked');
    $('.chkInterestIncomeEmp').each(function () {
        $(this).prop('checked', isChecked);
    });
}



function IntrestIncome_AddToBalance() {

    if (!validateForm('.divYear'))
        return;

    var EmpIds = '';
    var YearOf = $('.YearOf').val();

    $('.chkInterestIncomeEmp').each(function () {

        var InterestIncome = $(this).closest('tr').find('.lblGeneratedIncome').html();
        InterestIncome = parseInt(InterestIncome);
        var IsProvided = $(this).closest('tr').find('.hdnIsProvided').val();
        IsProvided = IsProvided == '' ? 'false' : IsProvided;
        if ($(this).prop('checked') && InterestIncome != 0 && IsProvided == 'false')
            EmpIds += $(this).val() + ',';
    });

    EmpIds += '0';


    if (EmpIds == '0') {
        showError('No Records Available For Update!');
    }
    else {
        var service = new HrmsSuiteHcmService.HcmService();
        service.multimanage_GrantInterestIncome(YearOf, EmpIds, onIntrestIncome_AddToBalance, null, null);
    }
}


function onIntrestIncome_AddToBalance(result) {
    if (result == 1) {
        showSuccess('Successfully Granted!');
        $('.btnInterestIncome').click();
    }
}




function methd_GenerateInterestIncome() {
    if (!validateForm('.divInterestInc'))
        return;

    var YearOf = $('.YearOf').val();
    var IncomeInterest_Rate = $('.txtPFIncomeInterestRate').val();

    var JSONFORM = [];
    $('.hdnGenereatedIncome').each(function () {
        var isProvided = $(this).closest('tr').find('.hdnIsProvided').val();
        isProvided = isProvided == '' ? 'false' : isProvided;
        if (isProvided == 'false') {
            if (!($(this).val() == '' || $(this).val() == undefined || $(this).val() == 0)) {
                var Response = new Object();
                Response.EmployeeId = $(this).closest('div').closest('tr').find('.EmployeeId').val();
                Response.InterestIncome = $(this).val();
                Response.YearOf = YearOf;
                Response.IncomeInterest_Rate = IncomeInterest_Rate;
                JSONFORM.push(Response);
            }
        }
    });
    var JSONResponse = JSON.stringify(JSONFORM);
    var service = new HrmsSuiteHcmService.HcmService();
    service.multimanage_SaveInterestIncome(JSONResponse, onMultiManageSaveInterestIncome, null, null);
}

function GetIncTaxForcastRecords() {

    var service = new HrmsSuiteHcmService.HcmService();
    service.multimanage_GetIncTaxForcastRecords(onGetIncTaxForcastRecords, null, null);
}

function onGetIncTaxForcastRecords(result) {
    var res = jQuery.parseJSON(result);
    $(res).each(function (k, v) {

        nEmpId = v.EmployeeID;
        $('.EmployeeId').each(function () {
            oEmpId = $(this).val();
            if (nEmpId == oEmpId) {
                var obj = $(this).closest('tr');
                obj.find('.txtIncTaxForcastPercentage').val(v.AdvanceTaxPercent);
            }
        });
    });
}

function GlobalUnitRateWppf(selector) {
    var effectVal = $(selector).val();
    if (effectVal == '')
        effectVal = 0;

    $('.txtUnitRateWppf').each(function () {

        $(this).val(effectVal).keyup();
        onChangeUnitRate(this);
    });
}

function GlobalInterestRateWppf(selector) {
    var effectVal = $(selector).val();
    if (effectVal == '')
        effectVal = 0;

    $('.txtInterestRateWppf').each(function () {

        $(this).val(effectVal).keyup();
        onChangeInterestRate(this);
    });
}

function onChangeUnitRate(selector) {

    var UnitRate = $(selector).val();
    var InterestRate = $(selector).closest('tr').find('.txtInterestRateWppf').val();
    var UnitValue = $(selector).closest('tr').find('.txtUnitValueWppf').val();

    GetTotalWppf(UnitRate, InterestRate, UnitValue, selector);
}

function onChangeInterestRate(selector) {

    var UnitRate = $(selector).closest('tr').find('.txtUnitRateWppf').val();
    var InterestRate = $(selector).val();
    var UnitValue = $(selector).closest('tr').find('.txtUnitValueWppf').val();

    GetTotalWppf(UnitRate, InterestRate, UnitValue, selector);
}

function GetTotalWppf(UnitRate, InterestRate, UnitValue, selector) {
    var MaxUnitRate = $('.txtMaxUnitRateWppf').val();
    var MaxInterestRate = $('.txtMaxInterestRateWppf').val();

    var TotalUnit = parseFloat(UnitRate) * parseFloat(UnitValue);
    var TotalInterest = parseFloat(InterestRate) * parseFloat(UnitValue);

    if (MaxUnitRate != '') {
        if (parseFloat(TotalUnit) > 0) {
            TotalUnit = MaxUnitRate;
        }
    }

    if (MaxInterestRate != '') {
        if (parseFloat(TotalInterest) > 0) {
            TotalInterest = MaxInterestRate;
        }
    }

    var TotalWppf = parseFloat(TotalInterest) + parseFloat(TotalUnit);

    $(selector).closest('tr').find('.txtTotalUnitRateWppf').val(TotalUnit);
    $(selector).closest('tr').find('.txtTotalInterestRateWppf').val(TotalInterest);
    $(selector).closest('tr').find('.txtTotalWppf').val(TotalWppf);
}

function GetYearWppf() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getTaxYear($('.ddlCompany').val(), onGetYearWppf, null, null);
}

function onGetYearWppf(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlYearWppf", res);
}

function GetYearTaxRefund() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getTaxYear($('.ddlCompany').val(), onGetYearTaxRefund, null, null);
}

function onGetYearTaxRefund(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlYearTaxRefund", res);
}





