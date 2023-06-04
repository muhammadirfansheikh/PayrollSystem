function TriggerPageLoads() {
    $(".btnCancelSearch").click(function () {

        $('.ddlGroupBy').val('0');
        $('.txtUnitRate').val('0');
        $('.txtInterestRate').val('0');
        $('.txtMinimumWage').val('0');


        $('.txtFromDate').val('');
        $('.txtToDate').val('');
        $('.ddlIsResigned').val('1');
        ClearReport();
    });

    //BindBank();
    BindGroupByDDL();
    $('.ddlGroupBy option').filter('[value="clsCompany"],[value="clsDesignation"],[value="clsBankName"]').remove();
}
function GetEmployee() {
    var CompanyId = $('.ddlCompany').val();
    if (CompanyId > 0) {
        if ($('.txtFromDate').val() != '') {
            if ($('.txtToDate').val() != '') {
                if ($('.txtUnitRate').val() != '') {
                    if ($('.txtInterestRate').val() != '') {

                        if ($('.txtMinimumWage').val() != '') {

                            var EmployeeCode = $('.txtEmployeeCode').val() == '' ? null : $('.txtEmployeeCode').val();
                            var CompanyId = $('.ddlCompany').val() == '' ? null : $('.ddlCompany').val();
                            var fromMonth = $('.txtFromDate').val();
                            var toMonth = $('.txtToDate').val();
                            var UnitRate = parseFloat($('.txtUnitRate').val());
                            var InterestRate = parseFloat($('.txtInterestRate').val());
                            var MinimumWage = parseFloat($('.txtMinimumWage').val());
                            var IsResigned = $('.ddlIsResigned').val();



                            ProgressBarShow();
                            ClearReport();
                            var service = new HrmsSuiteHcmService.HcmService();
                            service.report_WPPF_Payments(CompanyId, EmployeeCode, fromMonth, toMonth, UnitRate, InterestRate, MinimumWage, IsResigned
                                , onGetEmployee, null, null);


                            //var LocationId = $('.ddlLocation').val();
                            //var DepartmentId = $('.ddlDepartment').val();
                            //var CostCenterId = $('.ddlCostCenter').val();
                            //var SapCostCenterId = $('.ddlSapCostCenter').val();
                        }
                        else {
                            showError("Enter Minimum Wage");
                        }



                    }
                    else {
                        showError("Enter Interest Rate");
                    }

                }
                else {
                    showError("Enter Unit Rate");
                }

            }
            else {
                showError("Please Select To Date");
            }

        }
        else {
            showError('Please select From Date');
        }




    } else {
        showError('Please select Company');
    }
}

function onGetEmployee(result) {

    var GroupByValue = $('.ddlGroupBy').val();

    var res = JSON.parse(result);

    if (GroupByValue != 0) {
        res = res.sort(sortByProperty(GroupByValue));
    }
    var divTbodyGoalFund = $('.tbodywppfpaymentListing').html('');
    $('#budgetwppfpaymentListing').tmpl(res).appendTo(divTbodyGoalFund);

    var TotalUnitRate = 0;
    var TotalInterestRate = 0;
    var TotalMinimumWage = 0;
    


    $('.clsUnitRate').each(function () {
        TotalUnitRate += parseFloat(($(this).text().trim() == '' ? 0 : $(this).text().trim()));
    });

    $('.clsInterestRate').each(function () {
        TotalInterestRate += parseFloat(($(this).text().trim() == '' ? 0 : $(this).text().trim()));
    });

    $('.clsTotalPay').each(function () {
        TotalMinimumWage += parseFloat(($(this).text().trim() == '' ? 0 : $(this).text().trim()));
    });

   


    $('.tdUnitRate').text(TotalUnitRate);
    $('.tdInterestRate').text(TotalInterestRate);
    $('.tdTotalPay').text(TotalMinimumWage);
    
    var Prev = 0;
    var i = 0;
    var _Sum1 = 0, _Sum2 = 0, _Sum3 = 0;
    var GroupBy = '.' + $('.ddlGroupBy').val();

    var ColSpan = $('.clsUnitRate').index() - 0;

    var GroupByName = $(".ddlGroupBy option:selected").text();

    var GroupByValue = $('.ddlGroupBy').val();



    if (GroupByValue != 0) {

        $('.trList').each(function () {


            var CurrLocId = $(this).find('.ABC').find(GroupBy).val();

            if ($(this).is(':first-child')) {
                _Sum1 += parseFloat($(this).find('.clsUnitRate').text());
                _Sum2 += parseFloat($(this).find('.clsInterestRate').text());
                _Sum3 += parseFloat($(this).find('.clsTotalPay').text());
               


            }
            else {

                if (Prev == CurrLocId) {
                    Sum1 += parseFloat($(this).find('.clsUnitRate').text());
                    _Sum2 += parseFloat($(this).find('.clsInterestRate').text());
                    _Sum3 += parseFloat($(this).find('.clsTotalPay').text());

                }
                else {
                 

                    var GroupByItem = $(this).prev().find('.ABC').find(GroupBy).val();

                    $("<tr style='height: 60px' class='success'><th colspan=" + ColSpan + " style='text-align:left;height:60px'>" + GroupByName + " : " + GroupByItem + "</th><th style='text-align:right;height:60px'>" + _Sum1 + "</th><th style='text-align:right;height:60px'>" + _Sum2 + "</th><th style='text-align:right;height:60px'>" + _Sum3 + "</th><th style='width: 140px;height:60px'></th></tr>").insertBefore($(this));
                    i = -1;
                    Sum1 = parseFloat($(this).find('.clsUnitRate').text());
                    _Sum2 = parseFloat($(this).find('.clsInterestRate').text());
                    _Sum3 = parseFloat($(this).find('.clsTotalPay').text());
                    

                }

                if ($(this).is(':last-child')) {
                    var GroupByItem = $(this).find('.ABC').find(GroupBy).val();
                    $("<tr style='height: 60px' class='success'><th colspan=" + ColSpan + " style='text-align:left;height:60px'>" + GroupByName + " : " + GroupByItem + "</th><th style='text-align:right;height:60px'>" + _Sum1 + "</th><th style='text-align:right;height:60px'>" + _Sum2 + "</th><th style='text-align:right;height:60px'>" + _Sum3 + "</th><th style='width: 140px;height:60px'></th></tr>").insertAfter($(this));
                    /*$("<tr class='success'><th colspan=" + ColSpan + ">" + GroupByName + " : " + GroupByItem + "</th><th>" + _Sum1 + "</th><th>" + _Sum2 + "</th><th></th></tr>").insertAfter($(this));*/
                    i = -1;
                    _Sum1 = 0;
                    _Sum2 = 0;
                    _Sum3 = 0;
                   

                }
            }

            Prev = CurrLocId;
            i++;

        });

    }
    else {
        var divTbodyGoalFund = $('.tbodywppfpaymentListing').html('');
        $('#budgetwppfpaymentListing').tmpl(res).appendTo(divTbodyGoalFund);

    }


    SetReportHeader('WPPF Payment Report', 10, '');
    $('.div_reportbutton').show();
    addSerialNumber();

    ProgressBarHide();
}

function ClearReport() {
    $('.div_reportbutton').hide();
    $('.clsReportH').hide();
    $('.tbodywppfpaymentListing').html('');
    $('.tdUnitRate').text('');
    $('.tdInterestRate').text('');
    $('.tdTotalPay').text('');
}
