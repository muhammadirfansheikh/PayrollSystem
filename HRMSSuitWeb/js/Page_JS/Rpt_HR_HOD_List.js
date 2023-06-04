function TriggerPageLoads() {
    $(".btnCancelSearch").click(function () {
        $('.txtMonthOfPayroll').datepicker('setDate', null);
        $('.ddlGroupBy').val('0');
        ClearReport();
    });
    
    //BindBank();
    BindGroupByDDL();
    $('.ddlGroupBy option').filter('[value="clsCompany"],[value="clsDesignation"]').remove();
}

function GetEmployee() {
    var CompanyId = $('.ddlCompany').val();
    if (CompanyId > 0) {
        if (!validateForm('.divMonthPayroll'))
            return;
        var EmployeeCode = $('.txtEmployeeCode').val() == '' ? 0 : $('.txtEmployeeCode').val();
        var PayrollMonth = formatDate($('.txtMonthOfPayroll').val());
        //var LocationId = $('.ddlLocation').val();
        //var DepartmentId = $('.ddlDepartment').val();
        //var CostCenterId = $('.ddlCostCenter').val();
        //var SapCostCenterId = $('.ddlSapCostCenter').val();


        ProgressBarShow();
        ClearReport();
        var service = new HrmsSuiteHcmService.HcmService();
        service.report_HOD_HR_LIST(EmployeeCode, CompanyId,
            PayrollMonth, onreport_HOD_HR_LIST, null, null);
    } else {
        showError('Please select Company');
    }
}

function onreport_HOD_HR_LIST(result) {
   
    var GroupByValue = $('.ddlGroupBy').val();

    var res = JSON.parse(result);

    if (GroupByValue != 0) {
        res = res.sort(sortByProperty(GroupByValue));
    }
    var divTbodyGoalFund = $('.tbodyHRHODListing').html('');
    $('#HRHODListing').tmpl(res).appendTo(divTbodyGoalFund);

    var Basic = 0;
    var Allownces = 0;
    var Gross = 0;
    var Bonus = 0;
    var PF = 0;
    var EOBI = 0;
    var Sessi = 0;
    var CellAllownce = 0;
    var Fuel = 0;
    var RepairMaintainance = 0;
    var CarAllownce = 0;
    var HardShip = 0;
    var OutstandingAllownce = 0;
    var FuelInLiters = 0;


    $('.clsBasic').each(function () {
        Basic += parseFloat($(this).text().trim());
    });
    $('.clsAllowances').each(function () {
        Allownces += parseFloat($(this).text().trim());
    });
    $('.clsGross').each(function () {
        Gross += parseFloat($(this).text().trim());
    });
    $('.clsBonus').each(function () {
        Bonus += parseFloat($(this).text().trim());
    });
    $('.clsPF').each(function () {
        PF += parseFloat($(this).text().trim());
    });
    $('.clsEOBI').each(function () {
        EOBI += parseFloat($(this).text().trim());
    });
    $('.clsSESSI').each(function () {
        Sessi += parseFloat($(this).text().trim());
    });
    $('.clsCellAllowance').each(function () {
        CellAllownce += parseFloat($(this).text().trim());
    });
    $('.clsFuel').each(function () {
        Fuel += parseFloat($(this).text().trim());
    });
    $('.clsRepairMaintenance').each(function () {
        RepairMaintainance += parseFloat($(this).text().trim());
    });
    $('.clsCarAllowance').each(function () {
        CarAllownce += parseFloat($(this).text().trim());
    });
    $('.clsHardship').each(function () {
        HardShip += parseFloat($(this).text().trim());
    });
    $('.clsDisLocationAllowance').each(function () {
        OutstandingAllownce += parseFloat($(this).text().trim());
    });
    $('.clsFuelInLitres').each(function () {
        FuelInLiters += parseFloat($(this).text().trim());
    });
    $('.tdTotalBasic').text(Basic);
    $('.tdTotalAllownces').text(Allownces);
    $('.tdTotalGross').text(Gross.toFixed(0));
    $('.tdTotalBonus').text(Bonus);
    $('.tdTotalPF').text(PF);
    $('.tdTotalEOBI').text(EOBI);
    $('.tdTotalSessi').text(Sessi);
    $('.tdTotalCellAllownces').text(CellAllownce);
    $('.tdTotalFuel').text(Fuel);
    $('.tdTotalRepairMaintainance').text(RepairMaintainance);
   
    $('.tdTotalCarAllownces').text(CarAllownce);
    $('.tdTotalHardShip').text(HardShip);
    $('.tdDisLocationAllowance').text(OutstandingAllownce);
    $('.tdFuelInLitres').text(FuelInLiters);

    var Prev = 0;
    var i = 0;
    var _Sum1 = 0, _Sum2 = 0, _Sum3 = 0, _Sum4 = 0, _Sum5 = 0, _Sum6 = 0, _Sum7 = 0, _Sum8 = 0, _Sum9 = 0, _Sum10 = 0, _Sum11 = 0, _Sum12 = 0, _Sum13 = 0, _Sum14 = 0;
    var GroupBy = '.' + $('.ddlGroupBy').val();

    var ColSpan = $('.clsBasic').index() - 0;

    var GroupByName = $(".ddlGroupBy option:selected").text();

    var GroupByValue = $('.ddlGroupBy').val();

    if (GroupByValue != 0) {

        $('.trList').each(function () {


            var CurrLocId = $(this).find('.ABC').find(GroupBy).val();

            if ($(this).is(':first-child')) {
                _Sum1 += parseFloat($(this).find('.clsBasic').text());
                _Sum2 += parseFloat($(this).find('.clsAllowances').text());
                _Sum3 += parseFloat($(this).find('.clsGross').text());
                _Sum4 += parseFloat($(this).find('.clsBonus').text());
                _Sum5 += parseFloat($(this).find('.clsPF').text());
                _Sum6 += parseFloat($(this).find('.clsEOBI').text());
                _Sum7 += parseFloat($(this).find('.clsSESSI').text());
                _Sum8 += parseFloat($(this).find('.clsCellAllowance').text());
                _Sum9 += parseFloat($(this).find('.clsFuel').text());
                _Sum10 += parseFloat($(this).find('.clsRepairMaintenance').text());
                _Sum11 += parseFloat($(this).find('.clsCarAllowance').text());
                _Sum12 += parseFloat($(this).find('.clsHardship').text());
                _Sum13 += parseFloat($(this).find('.clsDisLocationAllowance').text());
                _Sum14 += parseFloat($(this).find('.clsFuelInLitres').text());
             

            }
            else {

                if (Prev == CurrLocId) {
                    _Sum1 += parseFloat($(this).find('.clsBasic').text());
                    _Sum2 += parseFloat($(this).find('.clsAllowances').text());
                    _Sum3 += parseFloat($(this).find('.clsGross').text());
                    _Sum4 += parseFloat($(this).find('.clsBonus').text());
                    _Sum5 += parseFloat($(this).find('.clsPF').text());
                    _Sum6 += parseFloat($(this).find('.clsEOBI').text());
                    _Sum7 += parseFloat($(this).find('.clsSESSI').text());
                    _Sum8 += parseFloat($(this).find('.clsCellAllowance').text());
                    _Sum9 += parseFloat($(this).find('.clsFuel').text());
                    _Sum10 += parseFloat($(this).find('.clsRepairMaintenance').text());
                    _Sum11 += parseFloat($(this).find('.clsCarAllowance').text());
                    _Sum12 += parseFloat($(this).find('.clsHardship').text());
                    _Sum13 += parseFloat($(this).find('.clsDisLocationAllowance').text());
                    _Sum14 += parseFloat($(this).find('.clsFuelInLitres').text());

                }
                else {

                    var GroupByItem = $(this).prev().find('.ABC').find(GroupBy).val();

                    $("<tr class='success'><th colspan=" + ColSpan + " style='text-align:left'>" + GroupByName + " : " + GroupByItem + "</th><th style='text-align:right'>" + _Sum1 + "</th><th style='text-align:right'>" + _Sum2 + "</th><th style='text-align:right'>" + _Sum3 + "</th><th style='text-align:right'>" + _Sum4 + "</th><th style='text-align:right'>" + _Sum5 + "</th><th style='text-align:right'>" + _Sum6 + "</th><th style='text-align:right'>" + _Sum7 + "</th><th style='text-align:right'>" + _Sum8 + "</th><th style='text-align:right'>" + _Sum9 + "</th><th style='text-align:right'>" + _Sum10 + "</th><th style='text-align:right'>" + _Sum11 + "</th><th style='text-align:right'>" + _Sum12 + "</th><th style='text-align:right'>" + _Sum13 + "</th><th style='text-align:right'>" + _Sum14 + "</th></tr>").insertBefore($(this));
                    i = -1;
                    _Sum1 = parseFloat($(this).find('.clsBasic').text());
                    _Sum2 = parseFloat($(this).find('.clsAllowances').text());
                    _Sum3 = parseFloat($(this).find('.clsGross').text());
                    _Sum4 = parseFloat($(this).find('.clsBonus').text());
                    _Sum5 = parseFloat($(this).find('.clsPF').text());
                    _Sum6 = parseFloat($(this).find('.clsEOBI').text());
                    _Sum7 = parseFloat($(this).find('.clsSESSI').text());
                    _Sum8 = parseFloat($(this).find('.clsCellAllowance').text());
                    _Sum9 = parseFloat($(this).find('.clsFuel').text());
                    _Sum10 = parseFloat($(this).find('.clsRepairMaintenance').text());
                    _Sum11 = parseFloat($(this).find('.clsCarAllowance').text());
                    _Sum12 = parseFloat($(this).find('.clsHardship').text());
                    _Sum13 = parseFloat($(this).find('.clsDisLocationAllowance').text());
                    _Sum14 = parseFloat($(this).find('.clsFuelInLitres').text());

                }

                if ($(this).is(':last-child')) {
                    var GroupByItem = $(this).find('.ABC').find(GroupBy).val();
                    $("<tr class='success'><th colspan=" + ColSpan + " style='text-align:left'>" + GroupByName + " : " + GroupByItem + "</th><th style='text-align:right'>" + _Sum1 + "</th><th style='text-align:right'>" + _Sum2 + "</th><th style='text-align:right'>" + _Sum3 + "</th><th style='text-align:right'>" + _Sum4 + "</th><th style='text-align:right'>" + _Sum5 + "</th><th style='text-align:right'>" + _Sum6 + "</th><th style='text-align:right'>" + _Sum7 + "</th><th style='text-align:right'>" + _Sum8 + "</th><th style='text-align:right'>" + _Sum9 + "</th><th style='text-align:right'>" + _Sum10 + "</th><th style='text-align:right'>" + _Sum11 + "</th><th style='text-align:right'>" + _Sum12 + "</th><th style='text-align:right'>" + _Sum13 + "</th><th style='text-align:right'>" + _Sum14 + "</th></tr>").insertAfter($(this));
                    /*$("<tr class='success'><th colspan=" + ColSpan + ">" + GroupByName + " : " + GroupByItem + "</th><th>" + _Sum1 + "</th><th>" + _Sum2 + "</th><th></th></tr>").insertAfter($(this));*/
                    i = -1;
                    _Sum1 =0;
                    _Sum2 =0;
                    _Sum3 =0;
                    _Sum4 = 0;
                    _Sum5 = 0;
                    _Sum6 = 0;
                    _Sum7 =0;
                    _Sum8 = 0;
                    _Sum9 = 0;
                    _Sum10 =0;
                    _Sum11 = 0;
                    _Sum12 = 0;
                    _Sum13 = 0;
                    _Sum14 = 0;

                }
            }

            Prev = CurrLocId;
            i++;

        });

    }
    else {
        var divTbodyGoalFund = $('.tbodyHRHODListing').html('');
        $('#HRHODListing').tmpl(res).appendTo(divTbodyGoalFund);

    }
    var Para2 = '';
   

    SetReportHeader('HR HOD List Report', 10, Para2);
    $('.div_reportbutton').show();
    addSerialNumber(); 
    ProgressBarHide();
}

function ClearReport() {
    $('.div_reportbutton').hide();
    $('.clsReportH').hide();
    $('.tbodyHRHODListing').html('');
    $('.tdTotalBasic').text('');
    $('.tdTotalAllownces').text('');
    $('.tdTotalGross').text('');
    $('.tdTotalBonus').text('');
    $('.tdTotalPF').text('');
    $('.tdTotalEOBI').text('');
    $('.tdTotalSessi').text('');
    $('.tdTotalCellAllownces').text('');
    $('.tdTotalFuel').text('');
    $('.tdTotalRepairMaintainance').text('');
    $('.tdTotalAllownces').text('');
    $('.tdTotalCarAllownces').text('');
    $('.tdTotalHardShip').text('');
}
