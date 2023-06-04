function TriggerPageLoads() {
    $(".btnCancelSearch").click(function () {
        $('.txtMonthOfPayroll').datepicker('setDate', null);

        $('.txtPremiumRate').val('0');
        ClearReport();
    });


}

function GetEmployee() {
    var CompanyId = $('.ddlCompany').val();
    if (CompanyId > 0) {
        if (!validateForm('.divMonthPayroll'))
            return;
        var EmployeeCode = $('.txtEmployeeCode').val() == '' ? null : $('.txtEmployeeCode').val();
        var Year = formatDate($('.txtMonthOfPayroll').val());
        var PremiumRate = parseFloat($('.txtPremiumRate').val());

        if (PremiumRate != null && PremiumRate > 0) {

            ProgressBarShow();
            ClearReport();
            var service = new HrmsSuiteHcmService.HcmService();
            service.report_PF_Interest_Allocation_Sheet(CompanyId, EmployeeCode, Year, PremiumRate,
                onreport_PF_Interest_Allocation_Sheet, null, null);
        }
        else {
            showError('Please enter rate.');
        }
        //var LocationId = $('.ddlLocation').val();
        //var DepartmentId = $('.ddlDepartment').val();
        //var CostCenterId = $('.ddlCostCenter').val();
        //var SapCostCenterId = $('.ddlSapCostCenter').val();


    } else {
        showError('Please select Company');
    }
}

function onreport_PF_Interest_Allocation_Sheet(result) {
    
    var res = JSON.parse(result);

  

    var divTbodyGoalFund = $('.tbodyPFIASListing').html('');
    $('#PFIASListing').tmpl(res).appendTo(divTbodyGoalFund);

    var TotalLocation = 0;
    var TotalDepartment = 0;
    var TotalCostCenter = 0;
    var TotalStaffBalance = 0;
    var TotalCompanyBalance = 0;
    var TotalPreviousInterest = 0;
    var TotalTotal = 0;
    var TotalCurrentInterest = 0;
    var TotalCumInterest = 0;
   
   


    $('.clsLocation').each(function () {
        TotalLocation += parseFloat($(this).text().trim());
    });
    $('.clsDepartment').each(function () {
        TotalDepartment += parseFloat($(this).text().trim());
    });

    $('.clsCostCenter').each(function () {
        TotalCostCenter += parseFloat($(this).text().trim());
    });

    $('.clsStaffBalance').each(function () {
        TotalStaffBalance += parseFloat($(this).text().trim());
    });

    $('.clsCompanyBalance').each(function () {
        TotalCompanyBalance += parseFloat($(this).text().trim());
    });

    $('.clsPreviousInterest').each(function () {
        TotalPreviousInterest += parseFloat($(this).text().trim());
    });

    $('.clsTotal').each(function () {
        TotalTotal += parseFloat($(this).text().trim());
    });

    $('.clsCurrentInterest').each(function () {
        TotalCurrentInterest += parseFloat($(this).text().trim());
    });
    $('.clsCumInterest').each(function () {
        TotalCumInterest += parseFloat($(this).text().trim());
    });
   





   

    $('.tdLocation').text(TotalLocation);
    $('.tdDepartment').text(TotalDepartment);
    $('.tdCostCenter').text(TotalCostCenter);
    $('.tdStaffBalance').text(TotalStaffBalance);
    $('.tdCompanyBalance').text(TotalCompanyBalance);
    $('.tdPreviousInterest').text(TotalPreviousInterest);
    $('.tdTotal').text(TotalTotal);
    $('.tdCurrentInterest').text(TotalCurrentInterest);
    $('.tdCumInterest').text(TotalCumInterest);

   


    SetReportHeader('PF Interest Allocation Sheet', 10, '');
    $('.div_reportbutton').show();
    addSerialNumber();
    ProgressBarHide();
}

function ClearReport() {
    $('.div_reportbutton').hide();
    $('.clsReportH').hide();
    $('.tbodybudgetactualListing').html('');
    $('.tdLocation').text('');
    $('.tdDepartment').text('');
    $('.tdCostCenter').text('');
    $('.tdStaffBalance').text('');
    $('.tdCompanyBalance').text('');
    $('.tdPreviousInterest').text('');
    $('.tdTotal').text('');
    $('.tdCurrentInterest').text('');
    $('.tdCumInterest').text('');
}
