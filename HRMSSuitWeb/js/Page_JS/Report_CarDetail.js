function TriggerPageLoads() {
    $(".btnCancelSearch").click(function () {
        ClearReport();
    });


    BindGroupByDDL();
    $('.ddlGroupBy option').filter('[value="clsCompany"],[value="clsDesignation"],[value="clsDepartment"]').remove();
}

function GetEmployee() {

    var CompanyId = $('.ddlCompany').val();
    if (CompanyId > 0) {
        if (!validateForm('.divMonthPayroll'))
            return;

        if ($(".txtFromDate").val() == '') {

            showError('Please Select Month.');
        }
        else {
            var EmployeeCode = $('.txtEmployeeCode').val() == '' ? 0 : $('.txtEmployeeCode').val();
            var fromDate = $(".txtFromDate").val();


            ProgressBarShow();
            ClearReport();
            var service = new HrmsSuiteHcmService.HcmService();
            service.report_Car_Details(EmployeeCode, CompanyId, fromDate, onreport_Emp_List, null, null);
        }

    } else {
        showError('Please select Company');
    }
}


function onreport_Emp_List(result) {

    // var GroupByValue = $('.ddlGroupBy').val();

    var res = JSON.parse(result);

    //if (GroupByValue != 0) {
    //    res = res.sort(sortByProperty(GroupByValue));
    //}
    var divTbodyGoalFund = $('.tbodyCarDetailListListing').html('');
    $('#CarDetailListListing').tmpl(res).appendTo(divTbodyGoalFund);

    var EntitledCost = 0;
    var Diffrence = 0;
    var UpgradedCost = 0;
    var WrittenDownValue = 0;
    var NoOfInstallments = 0;
    var InstallmentAmount = 0;
    var InstallmentReceived = 0;
    var InstallmentBlnc = 0;
    var FuelLtrs = 0;
    var FuelAmount = 0;
    var RepairAndMaintenance = 0;

    $('.clsEntitled').each(function () {
        EntitledCost += parseFloat($(this).text().trim());
    });
    $('.clsDifference').each(function () {
        Diffrence += parseFloat($(this).text().trim());
    });
    $('.clsUpgradedCost').each(function () {
        UpgradedCost += parseFloat($(this).text().trim());
    });
    $('.clsWrittenDownValue').each(function () {
        WrittenDownValue += parseFloat($(this).text().trim());
    });
    $('.clsInstallmentAmount').each(function () {
        InstallmentAmount += parseFloat($(this).text().trim());
    });
    $('.clsInstallmentReceived').each(function () {
        InstallmentReceived += parseFloat($(this).text().trim());
    });
    $('.clsInstallmentBalance').each(function () {
        InstallmentBlnc += parseFloat($(this).text().trim());
    });
    $('.clsFuelInLitres').each(function () {
        FuelLtrs += parseFloat($(this).text().trim());
    });
    $('.clsFuelAmount').each(function () {
        FuelAmount += parseFloat($(this).text().trim());
    });


    $('.clsRepairMaintenance').each(function () {
        RepairAndMaintenance += parseFloat($(this).text().trim());
    });


    $('.clsNoOfInstallments').text(NoOfInstallments);
    $('.tdEntitledCost').text(EntitledCost);
    $('.tdDiffrence').text(Diffrence);
    $('.tdUpgradedCost').text(UpgradedCost);
    $('.tdWrittenDownValue').text(WrittenDownValue);
    $('.tdInstallmentAmount').text(InstallmentAmount);
    $('.tdInstallmentReceived').text(InstallmentReceived);
    $('.tdInstallmentBalnc').text(InstallmentBlnc);
    $('.tdFuelLtrs').text(FuelLtrs);
    $('.tdFuelAmount').text(FuelAmount);
    $('.tdRepairAndMaintenance').text(RepairAndMaintenance);
    var Para2 = '';


    SetReportHeader('Car Detail List Report', 10, Para2);
    $('.div_reportbutton').show();
    addSerialNumber();
    ProgressBarHide();
}

function ClearReport() {
    $('.div_reportbutton').hide();
    $('.clsReportH').hide();
    $('.tbodyCarDetailListListing').html('');
    $('.tdEntitledCost').text('');
    $('.tdDiffrence').text('');
    $('.tdUpgradedCost').text('');
    $('.tdWrittenDownValue').text('');
    $('.tdInstallmentAmount').text('');
    $('.tdInstallmentReceived').text('');
    $('.tdInstallmentBalnc').text('');
    $('.tdFuelLtrs').text('');
    $('.tdFuelAmount').text('');
    $('.tdRepairAndMaintenance').text('');
    $('.clsNoOfInstallments').text('');
}

