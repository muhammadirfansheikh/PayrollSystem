function TriggerPageLoads() {
    $(".btnCancelSearch").click(function () {
        $('.txtMonthOfPayroll').datepicker('setDate', null);
        $('.ddlGroupBy').val('0');
        $('.txtPremiumRate').val('0');
        $('.txtPremiumRateGPA').val('0');
        ClearReport();
    });

    //BindBank();
    BindGroupByDDL();
    $('.ddlGroupBy option').filter('[value="clsCompany"],[value="clsBankName"],[value="clsDesignation"]').remove();
}
function GetEmployee() {
    var CompanyId = $('.ddlCompany').val();
    if (CompanyId > 0) {
        if (!validateForm('.divMonthPayroll'))
            return;
        var EmployeeCode = $('.txtEmployeeCode').val() == '' ? null : $('.txtEmployeeCode').val();
        var PayrollMonth = formatDate($('.txtMonthOfPayroll').val());
        var PremiumRate = parseFloat($('.txtPremiumRate').val());
        var PremiumRateGPA = parseFloat($('.txtPremiumRateGPA').val());

        if (PremiumRate != null && PremiumRate > 0) {
            if (PremiumRateGPA != null && PremiumRateGPA > 0) {
                ProgressBarShow();
                ClearReport();
                var service = new HrmsSuiteHcmService.HcmService();
                service.report_GetBudgetDetailReportActual(EmployeeCode, CompanyId, PremiumRate, PremiumRateGPA,
                    PayrollMonth, onGetBudgetDetailReportActual, null, null);
            }
            else {
                showError('Please enter premium rate GPA.');
            }
        }
        else {
            showError('Please enter premium rate GLI.');
        }
        //var LocationId = $('.ddlLocation').val();
        //var DepartmentId = $('.ddlDepartment').val();
        //var CostCenterId = $('.ddlCostCenter').val();
        //var SapCostCenterId = $('.ddlSapCostCenter').val();


    } else {
        showError('Please select Company');
    }
}

function onGetBudgetDetailReportActual(result) {
   
    var GroupByValue = $('.ddlGroupBy').val();

    var res = JSON.parse(result);

    if (GroupByValue != 0) {
        res = res.sort(sortByProperty(GroupByValue));
    }
    var divTbodyGoalFund = $('.tbodybudgetactualListing').html('');
    $('#budgetactualListing').tmpl(res).appendTo(divTbodyGoalFund);

    var TotalGrossAndOtherBenefits = 0;
    var TotalPFCount= 0;
    var TotalBonus10C = 0;
    var TotalSPBonus = 0;
    var TotalGratuity = 0;
    var TotalEOBICount= 0;
    var TotalSessiCount = 0;
    var TotalVehFuel = 0;
    var TotalRAndM = 0;
    var TotalGLI = 0;
    var TotalGPA= 0;
    var TotalMedicalInsurance= 0;


    $('.ClsGrossSalaryANDOtherBenefits').each(function () {
        TotalGrossAndOtherBenefits += parseFloat(($(this).text().trim() == '' ? 0 : $(this).text().trim() ));
    });

    $('.ClsPFCont').each(function () {
        TotalPFCount += parseFloat(($(this).text().trim() == '' ? 0 : $(this).text().trim()));
    });

    $('.ClsBonus10C').each(function () {
        TotalBonus10C += parseFloat(($(this).text().trim() == '' ? 0 : $(this).text().trim()));
    });

    $('.ClsSPBonus').each(function () {
        TotalSPBonus += parseFloat(($(this).text().trim() == '' ? 0 : $(this).text().trim()));
    });
    $('.ClsGratuity').each(function () {
        TotalGratuity += parseFloat(($(this).text().trim() == '' ? 0 : $(this).text().trim()));
    });
    $('.ClsEOBICont').each(function () {
        TotalEOBICount += parseFloat(($(this).text().trim() == '' ? 0 : $(this).text().trim()));
    });
    $('.ClsSESSICont').each(function () {
        TotalSessiCount += parseFloat(($(this).text().trim() == '' ? 0 : $(this).text().trim()));
    });
    $('.ClsVehFuel').each(function () {
        TotalVehFuel += parseFloat(($(this).text().trim() == '' ? 0 : $(this).text().trim()));
    });
    $('.ClsVehRANDM').each(function () {
        TotalRAndM += parseFloat(($(this).text().trim() == '' ? 0 : $(this).text().trim()));
    });
    $('.ClsGLI').each(function () {
        TotalGLI += parseFloat(($(this).text().trim() == '' ? 0 : $(this).text().trim()));
    });
    $('.ClsGPA').each(function () {
        TotalGPA += parseFloat(($(this).text().trim() == '' ? 0 : $(this).text().trim()));
    });

    $('.ClsMedicalInsurance').each(function () {
        TotalMedicalInsurance += parseFloat(($(this).text().trim() == '' ? 0 : $(this).text().trim()));
    });



    $('.tdGSAndOtherBenefit').text(TotalGrossAndOtherBenefits);
    $('.tdPFCount').text(TotalPFCount);
    $('.tdBonus10C').text(TotalBonus10C);
    $('.tdSPBonus').text(TotalSPBonus);
    $('.tdGratuity').text(TotalGratuity);
    $('.tdEOBICount').text(TotalEOBICount);
    $('.tdSessiCount').text(TotalSessiCount);
    $('.tdVehFuel').text(TotalVehFuel);
    $('.tdVehRANDM').text(TotalRAndM);
    $('.tdGLI').text(TotalGLI);
    $('.tdGPA').text(TotalGPA);
    $('.tdMedicalInsurance').text(TotalMedicalInsurance);

    var Prev = 0;
    var i = 0;
    var _Sum1 = 0, _Sum2 = 0; _Sum3 = 0; _Sum4 = 0; _Sum5 = 0; _Sum6 = 0; _Sum7 = 0; _Sum8 = 0; _Sum9 = 0; _Sum10 = 0; _Sum11 = 0; _Sum12 = 0;
    var GroupBy = '.' + $('.ddlGroupBy').val();

    var ColSpan = $('.ClsGrossSalaryANDOtherBenefits').index() - 0;

    var GroupByName = $(".ddlGroupBy option:selected").text();

    var GroupByValue = $('.ddlGroupBy').val();

    if (GroupByValue != 0) {

        $('.trList').each(function () {


            var CurrLocId = $(this).find('.ABC').find(GroupBy).val();

            if ($(this).is(':first-child')) {
                _Sum1 += parseFloat($(this).find('.ClsGrossSalaryANDOtherBenefits').text());
                _Sum2 += parseFloat($(this).find('.ClsPFCont').text());
                _Sum3 += parseFloat($(this).find('.ClsBonus10C').text());
                _Sum4 += parseFloat($(this).find('.ClsSPBonus').text());
                _Sum5 += parseFloat($(this).find('.ClsGratuity').text());
                _Sum6 += parseFloat($(this).find('.ClsEOBICont').text());
                _Sum7 += parseFloat($(this).find('.ClsSESSICont').text());
                _Sum8 += parseFloat($(this).find('.ClsVehFuel').text());
                _Sum9 += parseFloat($(this).find('.ClsVehRANDM').text());
                _Sum10 += parseFloat($(this).find('.ClsGLI').text());
                _Sum11 += parseFloat($(this).find('.ClsGPA').text());
                _Sum12 += parseFloat($(this).find('.ClsMedicalInsurance').text());

            }
            else {

                if (Prev == CurrLocId) {
                    _Sum1 += parseFloat($(this).find('.ClsGrossSalaryANDOtherBenefits').text());
                    _Sum2 += parseFloat($(this).find('.ClsPFCont').text());
                    _Sum3 += parseFloat($(this).find('.ClsBonus10C').text());
                    _Sum4 += parseFloat($(this).find('.ClsSPBonus').text());
                    _Sum5 += parseFloat($(this).find('.ClsGratuity').text());
                    _Sum6 += parseFloat($(this).find('.ClsEOBICont').text());
                    _Sum7 += parseFloat($(this).find('.ClsSESSICont').text());
                    _Sum8 += parseFloat($(this).find('.ClsVehFuel').text());
                    _Sum9 += parseFloat($(this).find('.ClsVehRANDM').text());
                    _Sum10 += parseFloat($(this).find('.ClsGLI').text());
                    _Sum11 += parseFloat($(this).find('.ClsGPA').text());
                    _Sum12 += parseFloat($(this).find('.ClsMedicalInsurance').text());

                }
                else {

                    var GroupByItem = $(this).prev().find('.ABC').find(GroupBy).val();

                    $("<tr class='success'><th colspan=" + ColSpan + " style='text-align:left'>" + GroupByName + " : " + GroupByItem + "</th><th style='text-align:right'>" + _Sum1 + "</th><th style='text-align:right'>" + _Sum2 + "</th><th style='text-align:right'>" + _Sum3 + "</th><th style='text-align:right'>" + _Sum4 + "</th><th style='text-align:right'>" + _Sum5 + "</th><th style='text-align:right'>" + _Sum6 + "</th><th style='text-align:right'>" + _Sum7 + "</th><th style='text-align:right'>" + _Sum8 + "</th><th style='text-align:right'>" + _Sum9 + "</th><th style='text-align:right'>" + _Sum10 + "</th><th style='text-align:right'>" + _Sum11 + "</th><th style='text-align:right'>" + _Sum12 + "</th></tr>").insertBefore($(this));
                    i = -1;
                    _Sum1 = parseFloat($(this).find('.ClsGrossSalaryANDOtherBenefits').text());
                    _Sum2 = parseFloat($(this).find('.ClsPFCont').text());
                    _Sum3 = parseFloat($(this).find('.ClsBonus10C').text());
                    _Sum4 = parseFloat($(this).find('.ClsSPBonus').text());
                    _Sum5 = parseFloat($(this).find('.ClsGratuity').text());
                    _Sum6 = parseFloat($(this).find('.ClsEOBICont').text());
                    _Sum7 = parseFloat($(this).find('.ClsSESSICont').text());
                    _Sum8 = parseFloat($(this).find('.ClsVehFuel').text());
                    _Sum9 = parseFloat($(this).find('.ClsVehRANDM').text());
                    _Sum10 = parseFloat($(this).find('.ClsGLI').text());
                    _Sum11 = parseFloat($(this).find('.ClsGPA').text());
                    _Sum12 = parseFloat($(this).find('.ClsMedicalInsurance').text());

                }

                if ($(this).is(':last-child')) {
                    var GroupByItem = $(this).find('.ABC').find(GroupBy).val();
                    $("<tr class='success'><th colspan=" + ColSpan + " style='text-align:left'>" + GroupByName + " : " + GroupByItem + "</th><th style='text-align:right'>" + _Sum1 + "</th><th style='text-align:right'>" + _Sum2 + "</th><th style='text-align:right'>" + _Sum3 + "</th><th style='text-align:right'>" + _Sum4 + "</th><th style='text-align:right'>" + _Sum5 + "</th><th style='text-align:right'>" + _Sum6 + "</th><th style='text-align:right'>" + _Sum7 + "</th><th style='text-align:right'>" + _Sum8 + "</th><th style='text-align:right'>" + _Sum9 + "</th><th style='text-align:right'>" + _Sum10 + "</th><th style='text-align:right'>" + _Sum11 + "</th><th style='text-align:right'>" + _Sum12 + "</th></tr>").insertAfter($(this));
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
                    _Sum11= 0;
                    _Sum12= 0;

                }
            }

            Prev = CurrLocId;
            i++;

        });
    }
    else {
        var divTbodyGoalFund = $('.tbodybudgetactualListing').html('');
        $('#budgetactualListing').tmpl(res).appendTo(divTbodyGoalFund);

    }
   
    SetReportHeader('Budget Detail Report Actual', 10, '');
    $('.div_reportbutton').show();
    addSerialNumber();
    
    ProgressBarHide();
}

function ClearReport() {
    $('.div_reportbutton').hide();
    $('.clsReportH').hide();
    $('.tbodybudgetactualListing').html('');
    $('.tdGSAndOtherBenefit').text('');
    $('.tdPFCount').text('');
    $('.tdBonus10C').text('');
    $('.tdSPBonus').text('');
    $('.tdGratuity').text('');
    $('.tdEOBICount').text('');
    $('.tdSessiCount').text('');
    $('.tdVehFuel').text('');
    $('.tdVehRANDM').text('');
    $('.tdGLI').text('');
    $('.tdGPA').text('');
    $('.tdMedicalInsurance').text('');
}
