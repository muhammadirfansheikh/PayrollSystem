function TriggerPageLoads() {
    $(".btnCancelSearch").click(function () {
        $('.txtMonthOfPayroll').datepicker('setDate', null);
        $('.ddlGroupBy').val('0');
        ClearReport();
    });

    //BindBank();
    BindGroupByDDL();
    $('.ddlGroupBy option').filter('[value="clsCompany"],[value="clsDesignation"],[value="clsBankName"]').remove();
}

function GetEmployee() {
    var CompanyId = $('.ddlCompany').val();
    if (CompanyId > 0) {
        if (!validateForm('.divMonthPayroll'))
            return;

        var EmployeeCode = $('.txtEmployeeCode').val() == '' ? null : $('.txtEmployeeCode').val();
        var PayrollMonth = formatDate($('.txtMonthOfPayroll').val());


        ProgressBarShow();
        ClearReport();
        var service = new HrmsSuiteHcmService.HcmService();
        service.report_OverTime(EmployeeCode, CompanyId,
            PayrollMonth, onGetReportOverTime, null, null);
    } else {
        showError('Please select Company');
    }
}

function onGetReportOverTime(result) {

    var GroupByValue = $('.ddlGroupBy').val();

    var res = JSON.parse(result);

    if (GroupByValue != 0) {
        res = res.sort(sortByProperty(GroupByValue));
    }
    var divTbodyGoalFund = $('.tbodyOverTimeListing').html('');
    $('#OverTimeListing').tmpl(res).appendTo(divTbodyGoalFund);

    var TotalOverTimeBasicCLA = 0;
    var TotalOverTimeRate = 0;
    var TotalOverTimeDays = 0;
    var TotalOverTimeAmount = 0;


    $('.clsBasic_CLA').each(function () {
        TotalOverTimeBasicCLA += parseFloat($(this).text().trim());
    });

    $('.clsRate').each(function () {
        TotalOverTimeRate += parseFloat($(this).text().trim());
    });


    $('.clsOvertimeDays').each(function () {
        TotalOverTimeDays += parseFloat($(this).text().trim());
    });

    $('.clsAllowanceAmount').each(function () {
        TotalOverTimeAmount += parseFloat($(this).text().trim());
    });

    $('.tdTotalOverTimeBasic_CLA').text(TotalOverTimeBasicCLA);
    $('.tdTotalOverTimeRate').text(TotalOverTimeRate);

    $('.tdTotalOverTimeDays').text(TotalOverTimeDays);
    $('.tdTotalOverTimeAmount').text(TotalOverTimeAmount);

    var Prev = 0;
    var i = 0;
    var _Sum1 = 0, _Sum2 = 0, _Sum3 = 0, _Sum4 = 0;

    var GroupBy = '.' + $('.ddlGroupBy').val();

    var ColSpan = $('.clsBasic_CLA').index() - 0;

    var GroupByName = $(".ddlGroupBy option:selected").text();

    var GroupByValue = $('.ddlGroupBy').val();
    if (GroupByValue != 0) {
        $('.trList').each(function () {


            var CurrLocId = $(this).find('.ABC').find(GroupBy).val();

            if ($(this).is(':first-child')) {
                _Sum1 += parseFloat($(this).find('.clsBasic_CLA').text());
                _Sum2 += parseFloat($(this).find('.clsRate').text());
                _Sum3 += parseFloat($(this).find('.clsOvertimeDays').text());
                _Sum4 += parseFloat($(this).find('.clsAllowanceAmount').text());

            }
            else {

                if (Prev == CurrLocId) {
                    _Sum1 += parseFloat($(this).find('.clsBasic_CLA').text());
                    _Sum2 += parseFloat($(this).find('.clsRate').text());
                    _Sum3 += parseFloat($(this).find('.clsOvertimeDays').text());
                    _Sum4 += parseFloat($(this).find('.clsAllowanceAmount').text());

                }
                else {

                    var GroupByItem = $(this).prev().find('.ABC').find(GroupBy).val();

                    $("<tr class='success'><th colspan=" + ColSpan + " style='text-align:left'>" + GroupByName + " : " + GroupByItem + "</th><th style='text-align:right'>" + _Sum1 + "</th><th style='text-align:right'>" + _Sum2 + "</th><th style='text-align:right'>" + _Sum3 + "</th><th style='text-align:right'>" + _Sum4 + "</th></tr>").insertBefore($(this));
                    i = -1;
                    _Sum1 = parseFloat($(this).find('.clsBasic_CLA').text());
                    _Sum2 = parseFloat($(this).find('.clsRate').text());
                    _Sum3 = parseFloat($(this).find('.clsOvertimeDays').text());
                    _Sum4 = parseFloat($(this).find('.clsAllowanceAmount').text());
                }

                if ($(this).is(':last-child')) {
                    var GroupByItem = $(this).find('.ABC').find(GroupBy).val();
                    $("<tr class='success'><th colspan=" + ColSpan + " style='text-align:left'>" + GroupByName + " : " + GroupByItem + "</th><th style='text-align:right'>" + _Sum1 + "</th><th style='text-align:right'>" + _Sum2 + "</th><th style='text-align:right'>" + _Sum3 + "</th><th style='text-align:right'>" + _Sum4 + "</th></tr>").insertAfter($(this));
                    i = -1;
                    _Sum1 = 0;
                    _Sum2 = 0;
                    _Sum3 = 0;
                    _Sum4 = 0;

                }
            }

            Prev = CurrLocId;
            i++;

        });
    }
    else {
        var divTbodyGoalFund = $('.tbodyOverTimeListing').html('');
        $('#OverTimeListing').tmpl(res).appendTo(divTbodyGoalFund);

    }


    //$('.trList').each(function () {

    //    var GroupByValue = $('.ddlGroupBy').val();

    //    if (GroupByValue != 0) {

    //        var CurrLocId = $(this).find('.ABC').find(GroupBy).val();

    //        if ($(this).is(':first-child')) {
    //            _Sum1 += parseFloat($(this).find('.clsOvertimeDays').text());
    //            _Sum2 += parseFloat($(this).find('.clsAllowanceAmount').text());

    //        }
    //        else {

    //            if (Prev == CurrLocId) {
    //                _Sum1 += parseFloat($(this).find('.clsOvertimeDays').text());
    //                _Sum2 += parseFloat($(this).find('.clsAllowanceAmount').text());

    //            }
    //            else {

    //                var GroupByItem = $(this).prev().find('.ABC').find(GroupBy).val();

    //                $("<tr class='success'><th colspan=" + ColSpan + ">" + GroupByName + " : " + GroupByItem + "</th><th>" + _Sum1 + "</th><th>" + _Sum2 + "</th><th></th></tr>").insertBefore($(this));
    //                i = -1;
    //                _Sum1 = parseFloat($(this).find('.clsOvertimeDays').text());
    //                _Sum2 = parseFloat($(this).find('.clsAllowanceAmount').text());

    //            }

    //            if ($(this).is(':last-child')) {
    //                var GroupByItem = $(this).find('.ABC').find(GroupBy).val();
    //                $("<tr class='success'><th colspan=" + ColSpan + ">" + GroupByName + " : " + GroupByItem + "</th><th>" + _Sum1 + "</th><th>" + _Sum2 + "</th><th></th></tr>").insertAfter($(this));
    //                i = -1;
    //                _Sum1 = 0;
    //                _Sum2 = 0;

    //            }
    //        }

    //        Prev = CurrLocId;
    //        i++;
    //    }
    //    else {
    //        var divTbodyGoalFund = $('.tbodyOverTimeListing').html('');
    //        $('#OverTimeListing').tmpl(res).appendTo(divTbodyGoalFund);

    //    }
    //});


    SetReportHeader('Over Time Report', 10, '');
    $('.div_reportbutton').show();
    addSerialNumber();
    ProgressBarHide();
}

function ClearReport() {
    $('.div_reportbutton').hide();
    $('.clsReportH').hide();
    $('.tbodyOverTimeListing').html('');
    $('.tdTotalOverTimeBasic_CLA').text('');
    $('.tdTotalOverTimeRate').text('');
    $('.tdTotalOverTimeDays').text('');
    $('.tdTotalOverTimeAmount').text('');
}

//function onGetReportIncomeTax1(result) {
//    var GroupByValue = $('.ddlGroupBy').val();

//    var res = jQuery.parseJSON(result);

//    if (GroupByValue != 0) {
//        res = res.sort(sortByProperty(GroupByValue));
//    }
//    var divTbodyGoalFund = $('.tbodyOverTimeListing').html('');
//    $('#OverTimeListing').tmpl(res).appendTo(divTbodyGoalFund);


//    var TotalOverTimeBasicCLA = 0;
//    var TotalRate= 0;
//    var TotalOverTime = 0;
//    var TotalAllownceAmount = 0;


//    $('.clsBasic_CLA').each(function () {
//        TotalOverTimeBasicCLA += parseFloat($(this).text().trim());
//    });

//    $('.clsRate').each(function () {
//        TotalRate += parseFloat($(this).text().trim());
//    });

//    $('.clsOvertimeDays').each(function () {
//        TotalOverTime += parseFloat($(this).text().trim());
//    });

//    $('.clsAllowanceAmount').each(function () {
//        TotalAllownceAmount += parseFloat($(this).text().trim());
//    });



//    $('.tdTotalOverTimeBasic_CLA').text(TotalOverTimeBasicCLA);
//    $('.tdTotalOverTimeRate').text(TotalRate);
//    $('.tdTotalOverTimeDays').text(TotalOverTime);
//    $('.tdTotalOverTimeAmount').text(TotalAllownceAmount);


//    var Prev = 0;
//    var i = 0;
//    var _Sum1 = 0, _Sum2 = 0,_Sum3=0,_Sum4=0;
//    var GroupBy = '.' + $('.ddlGroupBy').val();

//    var ColSpan = $('.clsBasic_CLA').index() - 1;

//    var GroupByName = $(".ddlGroupBy option:selected").text();

//    $('.trList').each(function () {

//        var GroupByValue = $('.ddlGroupBy').val();

//        if (GroupByValue != 0) {

//            var CurrLocId = $(this).find('.ABC').find(GroupBy).val();

//            if ($(this).is(':first-child')) {
//                _Sum1 += parseFloat($(this).find('.clsBasic_CLA').text());
//                _Sum2 += parseFloat($(this).find('.clsRate').text());
//                _Sum3 += parseFloat($(this).find('.clsOvertimeDays').text());
//                _Sum4 += parseFloat($(this).find('.clsAllowanceAmount').text());

//            }
//            else {

//                if (Prev == CurrLocId) {
//                    _Sum1 += parseFloat($(this).find('.clsBasic_CLA').text());
//                    _Sum2 += parseFloat($(this).find('.clsRate').text());
//                    _Sum3 += parseFloat($(this).find('.clsOvertimeDays').text());
//                    _Sum4 += parseFloat($(this).find('.clsAllowanceAmount').text());

//                }
//                else {

//                    var GroupByItem = $(this).prev().find('.ABC').find(GroupBy).val();

//                    $("<tr class='success'><th colspan=" + ColSpan + ">" + GroupByName + " : " + GroupByItem + "</th><th>" + _Sum1 + "</th><th>" + _Sum2 + "</th><th>" + _Sum3 + "</th><th>" + _Sum4 + "</th><th></th></tr>").insertBefore($(this));
//                    i = -1;
//                    _Sum1 = parseFloat($(this).find('.clsBasic_CLA').text());
//                    _Sum2 = parseFloat($(this).find('.clsRate').text());
//                    _Sum3 = parseFloat($(this).find('.clsOvertimeDays').text());
//                    _Sum4 = parseFloat($(this).find('.clsAllowanceAmount').text());

//                }

//                if ($(this).is(':last-child')) {
//                    var GroupByItem = $(this).find('.ABC').find(GroupBy).val();
//                    $("<tr class='success'><th colspan=" + ColSpan + ">" + GroupByName + " : " + GroupByItem + "</th><th>" + _Sum1 + "</th><th>" + _Sum2 + "</th><th>" + _Sum3 + "</th><th>" + _Sum4 + "</th><th></th></tr>").insertAfter($(this));
//                    i = -1;
//                    _Sum1 = 0;
//                    _Sum2 = 0;
//                    _Sum3= 0;
//                    _Sum4 = 0;

//                }
//            }

//            Prev = CurrLocId;
//            i++;
//        }
//        else {
//            var divTbodyGoalFund = $('.tbodyOverTimeListing').html('');
//            $('#OverTimeListing').tmpl(res).appendTo(divTbodyGoalFund);

//        }
//    });

//    var Para2 = '';
//    var BranchId = $('.ddlBranch').val();
//    if (BranchId != 0) {

//        Para2 = $('.ddlBank option:selected').text() + ' - ' + $('.ddlBranch option:selected').text();
//    }

//    SetReportHeader('Over Time Report', 9, Para2);
//}


/*
function BindBank() {

    var service = new HrmsSuiteHcmService.HcmService();
    service.getBank(onGetBank, null, null);
}

function onGetBank(result) {
    var res = jQuery.parseJSON(result);

    FillDropDownByReference('.ddlBank', res);
    BindBankBranch();
}

function BindBankBranch() {

    var BankBranchId = $('.ddlBank').val();

    var service = new HrmsSuiteHcmService.HcmService();
    service.getBankBranch(BankBranchId, onGetBankBranch, null, null);
}

function onGetBankBranch(result) {
    var res = jQuery.parseJSON(result);

    FillDropDownByReference('.ddlBranch', res);
}

*/