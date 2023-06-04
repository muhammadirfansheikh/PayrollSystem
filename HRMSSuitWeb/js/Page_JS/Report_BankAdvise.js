function TriggerPageLoads() {
    $(".btnCancelSearch").click(function () {
        $('.txtMonthOfPayroll').datepicker('setDate', null);
        $('.ddlBank').val('0');
        $('.ddlBank').change();
        $("#chkSepBonus").prop("checked", false);
        ClearReport();
    });

    BindBank();
    BindGroupByDDL();
    $('.ddlGroupBy option').filter('[value="clsCompany"],[value="clsDesignation"],[value="clsDepartment"],[value="clsLocation"],[value="clsCostCenter"],[value="clsSapCostCenter"]').remove();
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
        var BankId = $('.ddlBranch').val();
        var B_Master = $('.ddlBank').val();
        var IsSepBonus = false;
        if ($("#chkSepBonus").is(':checked')) {
            IsSepBonus = true;
        }
        ProgressBarShow();
        ClearReport();
        var service = new HrmsSuiteHcmService.HcmService();
        service.report_BankAdvise(EmployeeCode, GroupId, CompanyId, LocationId, BusinessUnitId, DepartmentId, CostCenterId,
            CategoryId, DesignationId, Firstname, Lastname, PayrollMonth, BankId, B_Master, IsSepBonus, onGetReportBankAdvise, null, null);
    } else {
        showError('Please select Company');
    }
}
function exportBankAdhvise(selector, fileName) {
    ;

    ;
    var tab_text = "<table border='2px'><tr>";
    var textRange; var j = 0;
    tab = document.getElementById(selector); // id of table

    if ($(".ddlBank").val() != "38" && $(".ddlBank").val() != "43") {
        $(tab).find('td:nth-child(6),th:nth-child(6)').remove();
    }

    for (j = 0; j < tab.rows.length; j++) {

        var _val = $(tab.rows[j]).find('.ABC').text().toString();

        tab_text = tab_text + tab.rows[j].innerHTML + "</tr>";
        //tab_text=tab_text+"</tr>";
    }

    tab_text = tab_text + "</table>";
    tab_text = tab_text.replace(/<A[^>]*>|<\/A>/g, "");//remove if u want links in your table
    tab_text = tab_text.replace(/<img[^>]*>/gi, ""); // remove if u want images in your table
    tab_text = tab_text.replace(/<input[^>]*>|<\/input>/gi, ""); // reomves input params

    var Qlik = new Blob([tab_text], {
        type: "application/vnd.ms-excel;charset=utf-8"
    });

    saveAs(Qlik, fileName + ".xls");


    //var copyTable = $(selector).clone(false).attr('id', '_copy_dailySales');
    //copyTable.insertAfter($(selector))
    //copyTable.find('td input[type=hidden]').remove();



    //var tab_text = "<table border='2px'><tr>";
    //var textRange; var j = 0;
    /////tab = document.getElementById(selector); // id of table
    //tab = $(copyTable).context.getElementById("_copy_dailySales"); // id of table


    //if ($(".ddlBank").val() != "38" && $(".ddlBank").val() != "43") {
    //    $(tab).find('td:nth-child(6),th:nth-child(6)').remove();
    //}
   
    //for (j = 0; j < tab.rows.length; j++) {

    //    var _val = $(tab.rows[j]).find('.ABC').text().toString();

    //    tab_text = tab_text + tab.rows[j].innerHTML + "</tr>";
    //    //tab_text=tab_text+"</tr>";
    //}

    //tab_text = tab_text + "</table>";
    //tab_text = tab_text.replace(/<A[^>]*>|<\/A>/g, "");//remove if u want links in your table
    //tab_text = tab_text.replace(/<img[^>]*>/gi, ""); // remove if u want images in your table
    //tab_text = tab_text.replace(/<input[^>]*>|<\/input>/gi, ""); // reomves input params

    //var Qlik = new Blob([tab_text], {
    //    type: "application/vnd.ms-excel;charset=utf-8"
    //});

    //saveAs(Qlik, fileName + ".xls");

}
//function onGetReportBankAdvise(result) {
//    ;
//    var res = jQuery.parseJSON(result);
//    if (res.length > 0) {
//        var GroupByValue = $('.ddlGroupBy').val();
//        if (GroupByValue != 0) {
//            res = res.sort(sortByProperty(GroupByValue));
//        }
//        var divTbodyGoalFund = $('.tbodyBankAdviseListing').html('');
//        $('#BankAdviseListing').tmpl(res).appendTo(divTbodyGoalFund);
//        var TotalPay = 0;
//        $('.clsTotalPay').each(function () {
//            TotalPay += parseFloat($(this).text().trim());
//        });
//        $('.tdTotalPay').text(TotalPay);

//        var Prev = 0;
//        var i = 0;
//        var _Sum1 = 0;
//        var GroupBy = '.' + $('.ddlGroupBy').val();
//        var ColSpan = $('.clsTotalPay').index() - 0;
//        var GroupByName = $(".ddlGroupBy option:selected").text();
//        if (GroupByValue != 0) {
//            $('.trList').each(function () {

//                var GroupByValue = $('.ddlGroupBy').val();

//                if (GroupByValue != 0) {
//                    var CurrLocId = $(this).find('.ABC').find(GroupBy).val();

//                    if ($(this).is(':first-child')) {
//                        _Sum1 += parseFloat($(this).find('.clsTotalPay').text());

//                    }
//                    else {

//                        if (Prev == CurrLocId) {
//                            _Sum1 += parseFloat($(this).find('.clsTotalPay').text());

//                        }
//                        else {

//                            var GroupByItem = $(this).prev().find('.ABC').find(GroupBy).val();

//                            $("<tr class='success'><th colspan=" + ColSpan + " style='text-align:left'>" + GroupByName + " : " + GroupByItem + "</th><th style='text-align:right'>" + _Sum1 + "</th></tr>").insertBefore($(this));
//                            i = -1;
//                            _Sum1 = parseFloat($(this).find('.clsTotalPay').text());

//                        }

//                        if ($(this).is(':last-child')) {

//                            var GroupByItem = $(this).find('.ABC').find(GroupBy).val();

//                            $("<tr class='success'><th colspan=" + ColSpan + "   style='text-align:left'>" + GroupByName + " : " + GroupByItem + "</th><th style='text-align:right'>" + _Sum1 + "</th></tr>").insertAfter($(this));
//                            i = -1;
//                            _Sum1 = 0;

//                        }
//                    }

//                    //if (i == 0) {
//                    //    $("<tr class='success'><th colspan=" + ColSpan + ">Total</th><th>" + _Sum1 + "</th></tr>").insertAfter($(this));
//                    //    i = -1;
//                    //    _Sum1 = 0;

//                    //}

//                    Prev = CurrLocId;
//                    i++;
//                }
//                else {
//                    var divTbodyGoalFund = $('.tbodyBankAdviseListing').html('');
//                    $('#BankAdviseListing').tmpl(res).appendTo(divTbodyGoalFund);
//                }
//            });
//        }
//        var Para2 = '';
//        var BranchId = $('.ddlBranch').val();
//        if (BranchId != 0) {
//            Para2 = $('.ddlBank option:selected').text() + ' - ' + $('.ddlBranch option:selected').text();
//        }

//        var _Counter = 0;
//        $('.clsSNo').each(function (_Counter) {
//            _Counter = _Counter + 1;
//            $(this).text(_Counter);
//        });
//        SetReportHeader('Bank Advise Report', 6, Para2);
//        $('.div_reportbutton').show();


//        if ($(".ddlBank").val() == "38" || $(".ddlBank").val() =="43") {
//            $('td:nth-child(6),th:nth-child(6)').show();
//            $('tfoot .tfootColsSpan').attr('colspan', 7);
//        }
//        else {
//            $('td:nth-child(6),th:nth-child(6)').hide();
//            $('tfoot .tfootColsSpan').attr('colspan', 6);
//        }

//    }
//    ProgressBarHide();
//}



function onGetReportBankAdvise(result) {
    ;
    var res = jQuery.parseJSON(result);
   
    if (res.length > 0) {
        var GroupByValue = $('.ddlGroupBy').val();

        if (GroupByValue != 0) {
            res = res.sort(sortByProperty(GroupByValue));
        }
        var divTbodyGoalFund = $('.tbodyBankAdviseListing').html('');
        $('#BankAdviseListing').tmpl(res).appendTo(divTbodyGoalFund);

        var Total = 0;

        $('.clsTotalPay').each(function () {
            Total += parseFloat($(this).text().trim());
            console.log($(this).text().trim());
        });

        $('.tdTotalPay').text(Total);
        var _Counter = 0;
        var Prev = 0;
        var i = 0;
        var _Sum1 = 0;
        var GroupBy = '.' + $('.ddlGroupBy').val();

        //var ColSpan = $('.clsMonthlyCont').index();
        var ColSpan = $('.clsTotalPay').index() - 0;
        var GroupByName = $(".ddlGroupBy option:selected").text();
        var GroupByValue = $('.ddlGroupBy').val();

        if (GroupByValue != 0) {
            $('.trList').each(function () {

                if (GroupByValue != 0) {
                    //var CurrLocId = $(this).find(GroupBy).text();
                    var CurrLocId = $(this).find('.ABC').find(GroupBy).val();

                    if ($(this).is(':first-child')) {

                        _Sum1 += parseFloat($(this).find('.clsTotalPay').text());
                        //_Sum5 += parseFloat($(this).find('.clsWagesPaid').val());
                    }
                    else {

                        if (Prev == CurrLocId) {

                            _Sum1 += parseFloat($(this).find('.clsTotalPay').text());
                            //_Sum5 += parseFloat($(this).find('.clsWagesPaid').val());
                        }
                        else {

                            var GroupByItem = $(this).prev().find('.ABC').find(GroupBy).val();

                            if ($(".ddlBank").val() == "38" || $(".ddlBank").val() == "43")
                                $("<tr class='success'><th class='grfooter' colspan='7' style='text-align:left'>" + GroupByName + " : " + GroupByItem + "</th><th style='text-align:right'>" + _Sum1 + "</th></tr>").insertBefore($(this));
                            else
                                $("<tr class='success'><th class='grfooter' colspan='6' style='text-align:left'>" + GroupByName + " : " + GroupByItem + "</th><th style='text-align:right'>" + _Sum1 + "</th></tr>").insertBefore($(this));
                            i = -1;

                            _Sum1 = parseFloat($(this).find('.clsTotalPay').text());
                            //_Sum5 = parseFloat($(this).find('.clsWagesPaid').val());
                        }

                        if ($(this).is(':last-child')) {

                            var GroupByItem = $(this).find('.ABC').find(GroupBy).val();

                            if ($(".ddlBank").val() == "38" || $(".ddlBank").val() == "43")
                                $("<tr class='success'><th class='grfooter' colspan='7' style='text-align:left'>" + GroupByName + " : " + GroupByItem + "</th><th style='text-align:right'>" + _Sum1 + "</th></tr>").insertAfter($(this));
                            else
                                $("<tr class='success'><th class='grfooter' colspan='6' style='text-align:left'>" + GroupByName + " : " + GroupByItem + "</th><th style='text-align:right'>" + _Sum1 + "</th></tr>").insertAfter($(this));

                            i = -1;
                            _Sum1 = 0;
                        }
                    }

                    Prev = CurrLocId;
                    i++;
                }
                else {
                    //var divTbodyGoalFund = $('.tbodyEOBIListing').html('');
                    //$('#EOBIListing').tmpl(res).appendTo(divTbodyGoalFund);
                    //;

                    ///Total += parseFloat($(this).find('.clsTotalPay').text().trim());
                    //var WagesPaid_val = $(this).find('.clsWagesPaid').val();
                    //if (WagesPaid_val.includes('Above Limit') == true) {
                    //    var WagesPaid_ = WagesPaid_val.split('Above Limit');
                    //    WagesPaid += parseFloat(WagesPaid_[1]);
                    //} else {
                    //    WagesPaid += parseFloat($(this).find('.clsWagesPaid').val());
                    //}
                }

            });
        }
        else {
            var divTbodyGoalFund = $('.tbodyBankAdviseListing').html('');
            $('#BankAdviseListing').tmpl(res).appendTo(divTbodyGoalFund);

        }
       

        ;
        
        
        //$('.tdWagesPaid').text(WagesPaid);

        var Para2 = '';
        var BranchId = $('.ddlBranch').val();
        if (BranchId != 0) {
            Para2 = $('.ddlBank option:selected').text() + ' - ' + $('.ddlBranch option:selected').text();
        }

        var _Counter = 0;
        $('.clsSNo').each(function (_Counter) {
            _Counter = _Counter + 1;
            $(this).text(_Counter);
        });
        SetReportHeader('Bank Advise Report', 6, Para2);
        $('.div_reportbutton').show();


        if ($(".ddlBank").val() == "38" || $(".ddlBank").val() == "43") {
            $('td:nth-child(6),th:nth-child(6)').show();
            $('tfoot .tfootColsSpan').attr('colspan', 7);

        }
        else {
            $('td:nth-child(6),th:nth-child(6)').hide();
            $('tfoot .tfootColsSpan').attr('colspan', 6);

        }
    }
    ProgressBarHide();
}
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
    ProgressBarShow();
    var service = new HrmsSuiteHcmService.HcmService();
    service.getBankBranch(BankBranchId, onGetBankBranch, null, null);
}

function onGetBankBranch(result) {
    var res = jQuery.parseJSON(result);

    FillDropDownByReference('.ddlBranch', res);
    ProgressBarHide();
}

function ClearReport() {
    $('.clsReportH').hide();
    $('.tbodyBankAdviseListing').html('');
    $('.tdTotalPay').text('');
    $('.div_reportbutton').hide();
}