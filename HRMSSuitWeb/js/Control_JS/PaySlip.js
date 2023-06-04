
var arrMaster;
var arrAllowance;
var arrDeduction;
var arrTax;
var COUNTER = 0;
var arrLoan;
var arrPf;

function TriggerPageLoads() {
    $(".btnCancelSearch").click(function () {
        $('.txtMonthOfPayroll').datepicker('setDate', null);
        $('.txtSelectedEmployeeCode').val('');
        ClearReport();
    });

    //$('.dataTables').DataTable({
    //    dom: '<"html5buttons"B>lTfgitp',
    //    buttons: [
    //        { extend: 'copy' },
    //        { extend: 'csv' },
    //        { extend: 'excel', title: 'Access Control' },
    //        { extend: 'pdf', title: 'Access Control' },

    //        {
    //            extend: 'print',
    //            customize: function (win) {
    //                $(win.document.body).addClass('white-bg');
    //                $(win.document.body).css('font-size', '10px');

    //                $(win.document.body).find('table')
    //                        .addClass('compact')
    //                        .css('font-size', 'inherit');
    //            }
    //        }
    //    ]

    //});
}

function GetEmployee() {
    var CompanyId = $('.ddlCompany').val();
    if (CompanyId > 0) {
        if (!validateForm('.divMonthPayroll'))
            return;

        var PayrollMonth = formatDate($('.txtMonthOfPayroll').val());

        var EmployeeCode = $('.txtEmployeeCode').val() == '' ? 0 : $('.txtEmployeeCode').val();
        var GroupId = $('.ddlGroup').val();

        var LocationId = $('.ddlLocation').val();
        var BusinessUnitId = $('.ddlBU').val();
        var DepartmentId = $('.ddlDepartment').val();
        var CostCenterId = $('.ddlCostCenter').val();
        var CategoryId = $('.ddlCategoryC').val();
        var DesignationId = $('.ddlDesignation').val();
        var Firstname = $('.txtFirstName').val();
        var Lastname = $('.txtLastName').val();
        ClearReport();
        ProgressBarShow();
        var service = new HrmsSuiteHcmService.HcmService();
        service.report_SalarySlips(EmployeeCode, GroupId, CompanyId, LocationId, BusinessUnitId, DepartmentId, CostCenterId, CategoryId, DesignationId, Firstname, Lastname, PayrollMonth, onGetTaxForecast, null, null);
    } else {
        showError('Please select Company');
    }
}

function onGetTaxForecast(result) {
  
    var result = result.split("#SPLIT#");
    RES_MASTER = jQuery.parseJSON(result[0]);
    RES_ALLOWANCE = jQuery.parseJSON(result[1]);
    RES_DEDUCTION = jQuery.parseJSON(result[2]);
    RES_LOAN = jQuery.parseJSON(result[3]);
    RES_TAX = jQuery.parseJSON(result[4]);
    RES_PF = jQuery.parseJSON(result[5]);
    RES_VEHICLE = jQuery.parseJSON(result[6]);
    GeneratePayslip(RES_MASTER[COUNTER].EmployeeCode);
    $('.div_reportbutton').show();
    ProgressBarHide();
}

function GeneratePayslip(_EmpCode) {

    //RES_MASTER = res;
    arrMaster = new Array();
    arrAllowance = new Array();
    arrDeduction = new Array();
    arrTax = new Array();
    arrLoan = new Array();
    arrPf = new Array();
    arrVehicle = new Array();

    var EMP_CODE;

    for (var i = 0; i < RES_MASTER.length; i++) {

        EMP_CODE = RES_MASTER[i].EmployeeCode;

        if (_EmpCode == EMP_CODE) {

            arrMaster.push(RES_MASTER[i]);
        }
    }

    for (var j = 0; j < RES_ALLOWANCE.length; j++) {

        if (_EmpCode == RES_ALLOWANCE[j].EmployeeCode) {

            arrAllowance.push(RES_ALLOWANCE[j]);
        }
    }

    for (var k = 0; k < RES_DEDUCTION.length; k++) {

        if (_EmpCode == RES_DEDUCTION[k].EmployeeCode) {
            arrDeduction.push(RES_DEDUCTION[k]);
        }
    }

    for (var m = 0; m < RES_TAX.length; m++) {

        if (_EmpCode == RES_TAX[m].EmployeeCode) {
            arrTax.push(RES_TAX[m]);
        }
    }

    for (var n = 0; n < RES_LOAN.length; n++) {

        if (_EmpCode == RES_LOAN[n].EmployeeCode) {
            arrLoan.push(RES_LOAN[n]);
        }
    }

    for (var p = 0; p < RES_PF.length; p++) {

        if (_EmpCode == RES_PF[p].EmployeeCode) {
            arrPf.push(RES_PF[p]);
        }
    }

    for (var p = 0; p < RES_VEHICLE.length; p++) {

        if (_EmpCode == RES_VEHICLE[p].EmployeeCode) {
            arrVehicle.push(RES_VEHICLE[p]);
        }
    }

    var i = 0;
    $(arrMaster).each(function (k, v) {

        var Emp_Code = v.EmployeeCode;

        if (_EmpCode == Emp_Code) {

            BindPaySlipData(Emp_Code);
        }
        i++;

    });
}

function BindPaySlipData(EmpCode) {

    $('.clsCompanyName').text($('.ddlCompany option:selected').text());
    $('.clsMonth').text($('.txtMonthOfPayroll').val());
    $('.clsEmployeeCode').text(EmpCode);
    $('.txtSelectedEmployeeCode').val(EmpCode);

    $(arrMaster).each(function (k, v) {

        var Emp_Code = v.EmployeeCode;

        if (EmpCode == Emp_Code) {
            $('.clsEmployeeName').text(v.FirstName + ' ' + v.LastName);
            $('.clsDesignation').text(v.DesignationName);
            $('.clsDepartment').text(v.DepartmentName);

            $('.clsBank').text(v.BankName);
            $('.clsBranch').text(v.BankDescription);
            $('.clsAccountNumber').text(v.AccountNumber);
        }
    });

    var Diff = 0;
    var CountAllowance = arrAllowance.length;
    var CountDeduction = arrDeduction.length;

    if (CountAllowance > CountDeduction) {
        Diff = CountAllowance - CountDeduction;

        for (var i = 0; i <= Diff; i++) {
            arrDeduction.push(i);
        }
    }
    else if (CountAllowance < CountDeduction) {
        Diff = -CountAllowance + CountDeduction;

        for (var i = 0; i <= Diff; i++) {
            arrAllowance.push(i);
        }
    }

    var AllowanceListing = $('.tbodyAllowanceListing').html('');
    $('#AllowanceListing').tmpl(arrAllowance).appendTo(AllowanceListing);


    var TotalAllowances = 0;
    $('.clsAllowanceAmount').each(function () {
        if ($(this).text().trim() != "") {
            TotalAllowances += parseFloat($(this).text().trim());
        }
    });
    $('.tdAllowanceAmount').text(TotalAllowances);

    var DeductionListing = $('.tbodyDeductionListing').html('');
    $('#DeductionListing').tmpl(arrDeduction).appendTo(DeductionListing);

    var TotalDeductions = 0;
    $('.clsDeductionAmount').each(function () {
        if ($(this).text().trim() != "") {
            TotalDeductions += parseFloat($(this).text().trim());
        }
    });
    $('.tdDeductionAmount').text(TotalDeductions);

    var TaxListing = $('.tbodyTaxListing').html('');
    $('#TaxListing').tmpl(arrTax).appendTo(TaxListing);

    
    var LoanListing = $('.tbodyLoanListing').html('');
    $('#LoanListing').tmpl(arrLoan).appendTo(LoanListing);

    if (arrLoan.length == 0) {
        $('.divLoan').hide();
    }
    else {
        $('.divLoan').show();
    }

    var PfListing = $('.tbodyPfListing').html('');
    $('#PfListing').tmpl(arrPf).appendTo(PfListing);

    if (arrPf.length == 0) {
        $('.divPF').hide();
    }
    else {
        $('.divPF').show();
    }

    var VehicleListing = $('.tbodyVehicleDeductionListing').html('');
    $('#VehicleListing').tmpl(arrVehicle).appendTo(VehicleListing);

    if (arrVehicle.length == 0) {
        $('.divVehicleDeduction').hide();
    }
    else {
        $('.divVehicleDeduction').show();
    }

    $('.clsDaysWork').text(arrAllowance[0].DaysWorked);

    $('.clsNetSalary').text(parseFloat(TotalAllowances) - parseFloat(TotalDeductions));
}

function Next() {

    COUNTER++;

    if (COUNTER < 0) {
        COUNTER = 0;
    }
    else {
        if (COUNTER > RES_MASTER.length) {
            COUNTER = RES_MASTER.length - 1;
        }
    }
    GeneratePayslip(RES_MASTER[COUNTER].EmployeeCode);
}

function Previous() {

    COUNTER--;

    if (COUNTER < 0) {
        COUNTER = 0;
    }
    GeneratePayslip(RES_MASTER[COUNTER].EmployeeCode);
}

function GetPaySlip() {

    GeneratePayslip($('.txtSelectedEmployeeCode ').val());
}

function ClearReport() {
    //$('.clsReportH').hide();
    //$('.HeaderColoumn').remove();
    $('.tbodyAllowanceListing').html('');
    $('.tbodyDeductionListing').html('');
    $('.tbodyTaxListing').html('');
    $('.tbodyLoanListing').html('');
    $('.tbodyPfListing').html('');
    $('.tbodyVehicleDeductionListing').html('');
    $('.border').text('');
    $('.clsDaysWork').text('');
    $('.clsNetSalary').text('0');
    $('.tdAllowanceAmount').text('0');
    $('.divVehicleDeduction').hide();
    $('.div_reportbutton').hide();
    
}


