function TriggerPageLoads() {
    $(".btnCancelSearch").click(function () {
        $(".chkSepBonus").prop("checked", false);
        $('.txtMonthOfPayroll').datepicker('setDate', null);
        $('.txtMonthOfPayroll_date').datepicker('setDate', null);
        $('.txtMonthOfPayroll_date').hide();
        $('.txtMonthOfPayroll').show();
        $('.ddlGroupBy').val('0');
        $('.ddlCompany').change();
        ClearReport();
    });


    $('.chkSepBonus').click(function () {
        if (!$(this).is(':checked')) {
            $('.txtMonthOfPayroll').show();
            $('.txtMonthOfPayroll_date').hide();
        } else {
            $('.txtMonthOfPayroll_date').show();
            $('.txtMonthOfPayroll').hide();
        }
    });
    BindGroupByDDL();
    $('.ddlGroupBy option').filter('[value="clsCompany"],[value="clsDesignation"],[value="clsDepartment"]').remove();
    FillDropDownByReference(".ddlBonus", null);
    $(".ddlCompany").change(function () {
        BindBonus();
    });
}

function GetEmployee() {

    var CompanyId = $('.ddlCompany').val();
    if (CompanyId > 0) {
        //validateForm('.divMonthPayroll');
        //return;
        var PayrollMonth = formatDate($('.txtMonthOfPayroll').val());
        if ($("#chkSepBonus").is(':checked')) {
            PayrollMonth = '';
            IsSepBonus = true;
            PayrollMonth = formatDate($('.txtMonthOfPayroll_date').val());
        }
        //if (PayrollMonth != '' && PayrollMonth != null) {
        var EmployeeCode = $('.txtEmployeeCode').val() == '' ? 0 : $('.txtEmployeeCode').val();
        var GroupId = $('.ddlGroup').val();
        var CompanyId = $('.ddlCompany').val();
        var LocationId = $('.ddlLocation').val();
        var BusinessUnitId = $('.ddlBU').val();
        var DepartmentId = $('.ddlDepartment').val();
        var CostCenterId = $('.ddlCostCenter').val();
        var CategoryId = $('.ddlCategoryC').val();
        var DesignationId = $('.ddlDesignation').val();
        var Firstname = $('.txtFirstName').val();
        var Lastname = $('.txtLastName').val();
        var BonusId = $('.ddlBonus').val();
        var IsSepBonus = false;
        ProgressBarShow();
        ClearReport();
        var service = new HrmsSuiteHcmService.HcmService();
        service.report_BonusList(EmployeeCode, GroupId, CompanyId, LocationId, BusinessUnitId, DepartmentId, CostCenterId,
            CategoryId, DesignationId, Firstname, Lastname, PayrollMonth, 0, BonusId, IsSepBonus, onGetReportBonusList, null, null);
        //} else {
        //    showError('Check All Mandotary Fields');
        //}
    } else {
        showError('Please select Company');
    }

}

function onGetReportBonusList(result) {
    var GroupByValue = $('.ddlGroupBy').val();
    var res = jQuery.parseJSON(result);
    if (res.length > 0) {



        if (GroupByValue != 0) {
            res = res.sort(sortByProperty(GroupByValue));
        }
        var divTbodyGoalFund = $('.tbodyBonusListing').html('');
        $('#BonusListing').tmpl(res).appendTo(divTbodyGoalFund);

        var BonusTotal = 0;

        $('.clsBonusAmount').each(function () {
            BonusTotal += parseFloat($(this).text().trim());
        });

        $('.tdBonusAmount').text(BonusTotal);


        var Prev = 0;
        var i = 0;
        var _Sum1 = 0;
        var GroupBy = '.' + $('.ddlGroupBy').val();

        var ColSpan = $('.clsBonusAmount').index() - 0;

        var GroupByName = $(".ddlGroupBy option:selected").text();


        $('.trList').each(function () {

            var GroupByValue = $('.ddlGroupBy').val();

            if (GroupByValue != 0) {

                var CurrLocId = $(this).find('.ABC').find(GroupBy).val();

                if ($(this).is(':first-child')) {
                    _Sum1 += parseFloat($(this).find('.clsBonusAmount').text());

                }
                else {

                    if (Prev == CurrLocId) {
                        _Sum1 += parseFloat($(this).find('.clsBonusAmount').text());
                    }
                    else {

                        var GroupByItem = $(this).prev().find('.ABC').find(GroupBy).val();

                        $("<tr class='success'><th colspan=" + ColSpan + ">" + GroupByName + " : " + GroupByItem + "</th><th>" + _Sum1 + "</th><th></th></tr>").insertBefore($(this));
                        i = -1;
                        _Sum1 = parseFloat($(this).find('.clsBonusAmount').text());
                    }

                    if ($(this).is(':last-child')) {
                        var GroupByItem = $(this).find('.ABC').find(GroupBy).val();

                        $("<tr class='success'><th colspan=" + ColSpan + ">" + GroupByName + " : " + GroupByItem + "</th><th>" + _Sum1 + "</th><th></th></tr>").insertAfter($(this));
                        i = -1;
                        _Sum1 = 0;
                    }
                }

                Prev = CurrLocId;
                i++;
            }
            else {
                var divTbodyGoalFund = $('.tbodyBonusListing').html('');
                $('#BonusListing').tmpl(res).appendTo(divTbodyGoalFund);
            }
        });

        var Para2 = '';
        var BonusId = $('.ddlBonus').val();
        if (BonusId != 0) {
            Para2 = $('.ddlBonus option:selected').text();
        }

        var _Counter = 0;
        $('.clsSNo').each(function (_Counter) {

            _Counter = _Counter + 1;
            $(this).text(_Counter);

        });

        SetReportHeader('Bonus Report', 8, Para2);
        $('.div_reportbutton').show();
    }
    ProgressBarHide();
}


function BindBonus() {
    var CompanyId = $('.ddlCompany').val();
    ProgressBarShow();
    var service = new HrmsSuiteHcmService.HcmService();
    service.getBonus(CompanyId, onGetBonus, null, null);
}

function onGetBonus(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference('.ddlBonus', res);
    ProgressBarHide();

}

function ClearReport() {
    $('.clsReportH').hide();
    $('.tbodyBonusListing').html('');
    $('.tdBonusAmount').text('');
    $('.div_reportbutton').hide();
}
