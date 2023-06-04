function TriggerPageLoads() {
    $(".btnCancelSearch").click(function () {
        ClearReport();
    });


    BindGroupByDDL();
    $('.ddlGroupBy option').filter('[value="clsCompany"],[value="clsDesignation"]').remove();
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
            service.report_Emp_List(EmployeeCode, CompanyId, fromDate, onreport_Emp_List, null, null);
        }

    } else {
        showError('Please select Company');
    }
}


function onreport_Emp_List(result) {
    ;
    var GroupByValue = $('.ddlGroupBy').val();

    var res = JSON.parse(result);

    if (GroupByValue != 0) {
        res = res.sort(sortByProperty(GroupByValue));
    }
    var divTbodyGoalFund = $('.tbodyEMPListListing').html('');
    $('#EMPListListing').tmpl(res).appendTo(divTbodyGoalFund);

    var Basic = 0;
    var HouseRent = 0;
    var Cola = 0;
    var MedicalAllownce = 0;
    var SpecialAlownce = 0;
    var Gross = 0;
    var OtherAllownce = 0;
    var Bonus = 0;
    var PF = 0;
    var PerformanceBonus = 0;
    var EOBI = 0;
    var Sessi = 0;
    var CarAllownce = 0;
    var ConveyanceReImburstment = 0;
    var CellAllownce = 0;
    var HardShip = 0;
    var CordinationAllowance = 0;
    var Fuel = 0;
    var RepairMaintainance = 0;
    var CellLimit = 0;
    var FuelInLitres = 0;
    var InstallmentAmount = 0;
    var OutStandingAllownce = 0;




    $('.clsBasic').each(function () {
        Basic += parseFloat($(this).text().trim());
    });
    $('.clsHouseRent').each(function () {
        HouseRent += parseFloat($(this).text().trim());
    });
    $('.clsCola').each(function () {
        Cola += parseFloat($(this).text().trim());
    });
    $('.clsMedicalAllowance').each(function () {
        MedicalAllownce += parseFloat($(this).text().trim());
    });
    $('.clsSpecialAllowance').each(function () {
        SpecialAlownce += parseFloat($(this).text().trim());
    });
    $('.clsGross').each(function () {
        Gross += parseFloat($(this).text().trim());
    });
    $('.clsOtherAllownce').each(function () {
        OtherAllownce += parseFloat($(this).text().trim());
    });
    $('.clsBonus').each(function () {
        Bonus += parseFloat($(this).text().trim());
    });
    $('.clsPerformanceBonus').each(function () {
        PerformanceBonus += parseFloat($(this).text().trim());
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
   
    $('.clsCarAllowance').each(function () {
        CarAllownce += parseFloat($(this).text().trim());
    });

    $('.clsCellAllowance').each(function () {
        CellAllownce += parseFloat($(this).text().trim());
    });
    $('.clsConveyanceReImburstment').each(function () {
        ConveyanceReImburstment += parseFloat($(this).text().trim());
    });
    $('.clsHardship').each(function () {
        HardShip += parseFloat($(this).text().trim());
    });
    $('.clsCordinationAllowance').each(function () {
        CordinationAllowance += parseFloat($(this).text().trim());
    });
    $('.clsFuel').each(function () {
        Fuel += parseFloat($(this).text().trim());
    });
    $('.clsRepairMaintenance').each(function () {
        RepairMaintainance += parseFloat($(this).text().trim());
    });
    $('.clsCellLimit').each(function () {
        CellLimit += parseFloat($(this).text().trim());
    });
    $('.clsFuelInLitres').each(function () {
        FuelInLitres += parseFloat($(this).text().trim());
    });

    $('.clsInstallmentAmount').each(function () {
        InstallmentAmount += parseFloat($(this).text().trim());
    });

    $('.clsDis_LocationAllowance').each(function () {
        OutStandingAllownce += parseFloat($(this).text().trim());
    });



    $('.tdTotalBasic').text(Basic);
    $('.tdHouseRent').text(HouseRent);
    $('.tdCola').text(Cola);
    $('.tdMedicalAlownce').text(MedicalAllownce);
    $('.tdSpecialAlownce').text(SpecialAlownce);
    $('.tdTotalGross').text(Gross);
    $('.tdTotalBonus').text(Bonus);
    $('.tdTotalPerformanceBonus').text(PerformanceBonus);
    $('.tdTotalPF').text(PF);
    $('.tdTotalEOBI').text(EOBI);
    $('.tdTotalSessi').text(Sessi);
    $('.tdTotalCarAllownce').text(CarAllownce);
    $('.tdTotalConveyanceReImburstment').text(ConveyanceReImburstment);
    $('.tdTotalCellAllownces').text(CellAllownce);
    $('.tdTotalHardShip').text(HardShip);
    $('.tdTotalCordinationAllowance').text(CordinationAllowance);
    $('.tdTotalFuel').text(Fuel);
    $('.tdTotalRepairMaintainance').text(RepairMaintainance);
    $('.tdTotalCellLimit').text(CellLimit);
    $('.tdTotalFuelInLitres').text(FuelInLitres);
    $('.tdInstallmentAllownce').text(InstallmentAmount);
    $('.tdOutStandingAllownce').text(OutStandingAllownce);




    var Prev = 0;
    var i = 0;
    var _Sum1 = 0, _Sum2 = 0, _Sum3 = 0, _Sum4 = 0, _Sum5 = 0, _Sum6 = 0, _Sum7 = 0, _Sum8 = 0, _Sum9 = 0, _Sum10 = 0, _Sum11 = 0, _Sum12 = 0, _Sum13 = 0, _Sum14 = 0, _Sum15 = 0, _Sum16 = 0, _Sum17 = 0, _Sum18 = 0, _Sum19 = 0, _Sum20 = 0, _Sum21 = 0, _Sum22 = 0, _Sum23 = 0;
    var GroupBy = '.' + $('.ddlGroupBy').val();

    var ColSpan = $('.clsBasic').index() - 0;

    var GroupByName = $(".ddlGroupBy option:selected").text();

    var GroupByValue = $('.ddlGroupBy').val();

    if (GroupByValue != 0) {

        $('.trList').each(function () {


            var CurrLocId = $(this).find('.ABC').find(GroupBy).val();

            if ($(this).is(':first-child')) {
                _Sum1 += parseFloat($(this).find('.clsBasic').text());
                _Sum2 += parseFloat($(this).find('.clsHouseRent').text());
                _Sum3 += parseFloat($(this).find('.clsCola').text());
                _Sum4 += parseFloat($(this).find('.clsMedicalAllowance').text());
                _Sum5 += parseFloat($(this).find('.clsSpecialAllowance').text());
                _Sum6 += parseFloat($(this).find('.clsGross').text());
                _Sum7 += parseFloat($(this).find('.clsOtherAllownces').text());
                _Sum8 += parseFloat($(this).find('.clsBonus').text());
                _Sum9 += parseFloat($(this).find('.clsPerformanceBonus').text());
                _Sum10 += parseFloat($(this).find('.clsPF').text());
                _Sum11 += parseFloat($(this).find('.clsEOBI').text());
                _Sum12 += parseFloat($(this).find('.clsSESSI').text());
                _Sum13 += parseFloat($(this).find('.clsCarAllowance').text());
                _Sum14 += parseFloat($(this).find('.clsConveyanceReImburstment').text());
                _Sum15 += parseFloat($(this).find('.clsCellAllowance').text());
                _Sum16 += parseFloat($(this).find('.clsHardship').text());
                _Sum17 += parseFloat($(this).find('.clsCordinationAllowance').text());
                _Sum18 += parseFloat($(this).find('.clsFuel').text());
                _Sum19 += parseFloat($(this).find('.clsRepairMaintenance').text());
                _Sum20 += parseFloat($(this).find('.clsCellLimit').text());
                _Sum21 += parseFloat($(this).find('.clsFuelInLitres').text());
                _Sum22 += parseFloat($(this).find('.clsInstallmentAmount').text());
                _Sum23 += parseFloat($(this).find('.clsDis_LocationAllowance').text());




            }
            else {

                if (Prev == CurrLocId) {
                   _Sum1 += parseFloat($(this).find('.clsBasic').text());
                   _Sum2 += parseFloat($(this).find('.clsHouseRent').text());
                   _Sum3 += parseFloat($(this).find('.clsCola').text());
                   _Sum4 += parseFloat($(this).find('.clsMedicalAllowance').text());
                   _Sum5 += parseFloat($(this).find('.clsSpecialAllowance').text());
                   _Sum6 += parseFloat($(this).find('.clsGross').text());
                   _Sum7 += parseFloat($(this).find('.clsOtherAllownces').text());
                   _Sum8 += parseFloat($(this).find('.clsBonus').text());
                   _Sum9 += parseFloat($(this).find('.clsPerformanceBonus').text());
                   _Sum10 += parseFloat($(this).find('.clsPF').text());
                   _Sum11 += parseFloat($(this).find('.clsEOBI').text());
                   _Sum12 += parseFloat($(this).find('.clsSESSI').text());
                   _Sum13 += parseFloat($(this).find('.clsCarAllowance').text());
                   _Sum14 += parseFloat($(this).find('.clsConveyanceReImburstment').text());
                   _Sum15 += parseFloat($(this).find('.clsCellAllowance').text());
                   _Sum16 += parseFloat($(this).find('.clsHardship').text());
                   _Sum17 += parseFloat($(this).find('.clsCordinationAllowance').text());
                   _Sum18 += parseFloat($(this).find('.clsFuel').text());
                   _Sum19 += parseFloat($(this).find('.clsRepairMaintenance').text());
                    _Sum20 += parseFloat($(this).find('.clsCellLimit').text());
                   _Sum21 += parseFloat($(this).find('.clsFuelInLitres').text());
                   _Sum22 += parseFloat($(this).find('.clsInstallmentAmount').text());
                   _Sum23 += parseFloat($(this).find('.clsDis_LocationAllowance').text()); //_Sum1 += parseFloat($(this).find('.clsBasic').text());
                    //_Sum2 += parseFloat($(this).find('.clsAllowances').text());
                    //_Sum3 += parseFloat($(this).find('.clsGross').text());
                    //_Sum4 += parseFloat($(this).find('.clsBonus').text());
                    //_Sum5 += parseFloat($(this).find('.clsPF').text());
                    //_Sum6 += parseFloat($(this).find('.clsEOBI').text());
                    //_Sum7 += parseFloat($(this).find('.clsSESSI').text());
                    //_Sum8 += parseFloat($(this).find('.clsCarAllowance').text());
                    //_Sum9 += parseFloat($(this).find('.clsCellAllowance').text());
                    //_Sum10 += parseFloat($(this).find('.clsHardship').text());
                    //_Sum11 += parseFloat($(this).find('.clsFuel').text());
                    //_Sum12 += parseFloat($(this).find('.clsRepairMaintenance').text());
                    //_Sum13 += parseFloat($(this).find('.clsFuelInLitres').text());
                    //_Sum14 += parseFloat($(this).find('.clsInstallmentAmount').text());
                    //_Sum15 += parseFloat($(this).find('.clsDis_LocationAllowance').text());

                }
                else {

                    var GroupByItem = $(this).prev().find('.ABC').find(GroupBy).val();

                    $("<tr class='success'><th colspan=" + ColSpan + " style='text-align:left'>" + GroupByName + " : " + GroupByItem + "</th><th style='text-align:right'>" + _Sum1 + "</th><th style='text-align:right'>" + _Sum2 + "</th style='text-align:right'><th>" + _Sum3 + "</th><th style='text-align:right'>" + _Sum4 + "</th><th style='text-align:right'>" + _Sum5 + "</th><th style='text-align:right'>" + _Sum6 + "</th><th style='text-align:right'>" + _Sum7 + "</th><th style='text-align:right'>" + _Sum8 + "</th><th style='text-align:right'>" + _Sum9 + "</th><th style='text-align:right'>" + _Sum10 + "</th><th style='text-align:right'>" + _Sum11 + "</th><th style='text-align:right'>" + _Sum12 + "</th>><th style='text-align:right'>" + _Sum13 + "</th><th style='text-align:right'>" + _Sum14 + "</th><th style='text-align:right'>" + _Sum15 + "</th><th style='text-align:right'>" + _Sum16 + "</th><th style='text-align:right'>" + _Sum17 + "</th><th style='text-align:right'>" + _Sum18 + "</th><th style='text-align:right'>" + _Sum19 + "</th><th style='text-align:right'>" + _Sum20 + "</th><th style='text-align:right'>" + _Sum21 + "</th><th style='text-align:right'>" + _Sum22 + "</th><th style='text-align:right'>" + _Sum23 + "</th></tr>").insertBefore($(this));
                    i = -1;
                    _Sum1 = parseFloat($(this).find('.clsBasic').text());
                    _Sum2 = parseFloat($(this).find('.clsHouseRent').text());
                    _Sum3 = parseFloat($(this).find('.clsCola').text());
                    _Sum4 = parseFloat($(this).find('.clsMedicalAllowance').text());
                    _Sum5 = parseFloat($(this).find('.clsSpecialAllowance').text());
                    _Sum6 = parseFloat($(this).find('.clsGross').text());
                    _Sum7 = parseFloat($(this).find('.clsOtherAllownces').text());
                    _Sum8 = parseFloat($(this).find('.clsBonus').text());
                    _Sum9 = parseFloat($(this).find('.clsPerformanceBonus').text());
                    _Sum10 = parseFloat($(this).find('.clsPF').text());
                    _Sum11 = parseFloat($(this).find('.clsEOBI').text());
                    _Sum12 = parseFloat($(this).find('.clsSESSI').text());
                    _Sum13 = parseFloat($(this).find('.clsCarAllowance').text());
                    _Sum14 = parseFloat($(this).find('.clsConveyanceReImburstment').text());
                    _Sum15 = parseFloat($(this).find('.clsCellAllowance').text());
                    _Sum16 = parseFloat($(this).find('.clsHardship').text());
                    _Sum17 = parseFloat($(this).find('.clsCordinationAllowance').text());
                    _Sum18 = parseFloat($(this).find('.clsFuel').text());
                    _Sum19 = parseFloat($(this).find('.clsRepairMaintenance').text());
                    _Sum20 = parseFloat($(this).find('.clsCellLimit').text());
                    _Sum21 = parseFloat($(this).find('.clsFuelInLitres').text());//_Sum1 = parseFloat($(this).find('.clsBasic').text());
                    _Sum22 = parseFloat($(this).find('.clsInstallmentAmount').text());//_Sum2 = parseFloat($(this).find('.clsAllowances').text());
                    _Sum23 = parseFloat($(this).find('.clsDis_LocationAllowance').text());//_Sum3 = parseFloat($(this).find('.clsGross').text());
                    //_Sum4 = parseFloat($(this).find('.clsBonus').text());
                    //_Sum5 = parseFloat($(this).find('.clsPF').text());
                    //_Sum6 = parseFloat($(this).find('.clsEOBI').text());
                    //_Sum7 = parseFloat($(this).find('.clsSESSI').text());
                    //_Sum8 = parseFloat($(this).find('.clsCarAllowance').text());
                    //_Sum9 = parseFloat($(this).find('.clsCellAllowance').text());
                    //_Sum10 = parseFloat($(this).find('.clsHardship').text());
                    //_Sum11 = parseFloat($(this).find('.clsFuel').text());
                    //_Sum12 = parseFloat($(this).find('.clsRepairMaintenance').text());
                    //_Sum13 = parseFloat($(this).find('.clsFuelInLitres').text());
                    //_Sum14 = parseFloat($(this).find('.clsInstallmentAmount').text());
                    //_Sum15 = parseFloat($(this).find('.clsDis_LocationAllowance').text());

                }

                if ($(this).is(':last-child')) {
                    var GroupByItem = $(this).find('.ABC').find(GroupBy).val();
                    $("<tr class='success'><th colspan=" + ColSpan + " style='text-align:left'>" + GroupByName + " : " + GroupByItem + "</th><th style='text-align:right'>" + _Sum1 + "</th><th style='text-align:right'>" + _Sum2 + "</th style='text-align:right'><th>" + _Sum3 + "</th><th style='text-align:right'>" + _Sum4 + "</th><th style='text-align:right'>" + _Sum5 + "</th><th style='text-align:right'>" + _Sum6 + "</th><th style='text-align:right'>" + _Sum7 + "</th><th style='text-align:right'>" + _Sum8 + "</th><th style='text-align:right'>" + _Sum9 + "</th><th style='text-align:right'>" + _Sum10 + "</th><th style='text-align:right'>" + _Sum11 + "</th><th style='text-align:right'>" + _Sum12 + "</th>><th style='text-align:right'>" + _Sum13 + "</th><th style='text-align:right'>" + _Sum14 + "</th><th style='text-align:right'>" + _Sum15 + "</th><th style='text-align:right'>" + _Sum16 + "</th><th style='text-align:right'>" + _Sum17 + "</th><th style='text-align:right'>" + _Sum18 + "</th><th style='text-align:right'>" + _Sum19 + "</th><th style='text-align:right'>" + _Sum20 + "</th><th style='text-align:right'>" + _Sum21 + "</th><th style='text-align:right'>" + _Sum22 + "</th><th style='text-align:right'>" + _Sum23 + "</th></tr>").insertAfter($(this));
                    /*$("<tr class='success'><th colspan=" + ColSpan + ">" + GroupByName + " : " + GroupByItem + "</th><th>" + _Sum1 + "</th><th>" + _Sum2 + "</th><th></th></tr>").insertAfter($(this));*/
                    i = -1;
                    _Sum1 = 0;
                    _Sum2 = 0;
                    _Sum3 = 0;
                    _Sum4 = 0;
                    _Sum5 = 0;
                    _Sum6 = 0;
                    _Sum7 = 0;
                    _Sum8 = 0;
                    _Sum9 = 0;
                    _Sum10 = 0;
                    _Sum11 = 0;
                    _Sum12 = 0;
                    _Sum13 = 0;
                    _Sum14 = 0;
                    _Sum15 = 0;
                    _Sum16 = 0;
                    _Sum17 = 0;
                    _Sum18 = 0;
                    _Sum19 = 0;
                    _Sum20 = 0;
                    _Sum21 = 0;
                    _Sum22 = 0;
                    _Sum23 = 0;
                }
            }

            Prev = CurrLocId;
            i++;

        });

    }
    else {
        var divTbodyGoalFund = $('.tbodyEMPListListing').html('');
        $('#EMPListListing').tmpl(res).appendTo(divTbodyGoalFund);

    }
    var Para2 = '';
    

    SetReportHeader('EMP List Report', 10, Para2);
    $('.div_reportbutton').show();
    addSerialNumber();
    ProgressBarHide();
}

function ClearReport() {
    $('.div_reportbutton').hide();
    $('.clsReportH').hide();
    $('.tbodyEMPListListing').html('');
    $('.tdTotalBasic').text('');
    $('.tdTotalAllownces').text('');
    $('.tdTotalGross').text('');
    $('.tdTotalBonus').text('');
    $('.tdTotalPerformanceBonus').text('');
    $('.tdTotalPF').text('');
    $('.tdTotalEOBI').text('');
    $('.tdTotalSessi').text('');
    $('.tdTotalCellAllownces').text('');
    $('.tdTotalFuel').text('');
    $('.tdTotalRepairMaintainance').text('');
    $('.tdTotalAllownces').text('');
    $('.tdTotalCarAllownces').text('');
    $('.tdTotalHardShip').text('');
    $('.tdTotalFuelInLitres').text('');
    $('.tdInstallmentAllownce').text('');
    $('.tdOutStandingAllownce').text('');
}

