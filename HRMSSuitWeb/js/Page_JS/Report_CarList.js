function TriggerPageLoads() {
    $(".btnCancelSearch").click(function () {
        $('.txtMonthOfPayroll').datepicker('setDate', null);
        $('.ddlGroupBy').val('0');
        ClearReport();
    });

    BindGroupByDDL();
    $('.ddlGroupBy option').filter('[value="clsCompany"],[value="clsDesignation"],[value="clsBankName"]').remove();
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
        ProgressBarShow();
        ClearReport();
        var service = new HrmsSuiteHcmService.HcmService();
        service.report_CarList(EmployeeCode, GroupId, CompanyId, LocationId, BusinessUnitId, DepartmentId, CostCenterId,
            CategoryId, DesignationId, Firstname, Lastname, PayrollMonth, 0, onGetReportCarList, null, null);
    } else {
        showError('Please select Company');
    }

}

function onGetReportCarList(result) {
    var res = jQuery.parseJSON(result);
    if (res.length > 0) {
        var GroupByValue = $('.ddlGroupBy').val();
        if (GroupByValue != 0) {
            res = res.sort(sortByProperty(GroupByValue));
        }
        var divTbodyGoalFund = $('.tbodyCarListing').html('');
        $('#CarListing').tmpl(res).appendTo(divTbodyGoalFund);
        SetReportHeader('Vehicle List', 12, '');
        
        var InstallmentTotal = 0;
        var BalanceTotal = 0;
        var PetrolAmountTotal = 0;
        var MaintananceAmountTotal = 0;
        var RecoveryTotal = 0;
    
        $('.clsInstallmentAmount').each(function () {
            InstallmentTotal += parseFloat($(this).text().trim());
        });
    
        $('.clsBalance').each(function () {
            BalanceTotal += parseFloat($(this).text().trim());
        });
    
        $('.clsPetrolAmount').each(function () {
            PetrolAmountTotal += parseFloat($(this).text().trim());
        });
    
        $('.clsMaintanenceAmount').each(function () {
            MaintananceAmountTotal += parseFloat($(this).text().trim());
        });
    
        $('.clsTotalRecovery').each(function () {
            RecoveryTotal += parseFloat($(this).text().trim());
        });
    
        $('.tdTotalInstallment').text(InstallmentTotal);
        $('.tdTotalBalance').text(BalanceTotal);
        $('.tdPetrolAmount').text(PetrolAmountTotal);
        $('.tdMaintainanceAmount').text(MaintananceAmountTotal);
        $('.tdTotalRecovery').text(RecoveryTotal);
    
        var Prev = 0;
        var i = 0;
        var _Sum1 = 0, _Sum2 = 0, _Sum3 = 0, _Sum4 = 0, _Sum5 = 0;
        var GroupBy = '.' + $('.ddlGroupBy').val();
    
    
        var ColSpan = $('.clsPetrolAmount').index();
    
        var GroupByName = $(".ddlGroupBy option:selected").text();
    
        $('.trList').each(function () {
    
            var GroupByValue = $('.ddlGroupBy').val();
    
            if (GroupByValue != 0) {
                var CurrLocId = $(this).find('.ABC').find(GroupBy).val();
    
                if ($(this).is(':first-child')) {
                    _Sum1 += parseFloat($(this).find('.clsPetrolAmount').text());
                    _Sum2 += parseFloat($(this).find('.clsMaintanenceAmount').text());
                    _Sum3 += parseFloat($(this).find('.clsInstallmentAmount').text());
                    _Sum4 += parseFloat($(this).find('.clsTotalRecovery').text());
                    _Sum5 += parseFloat($(this).find('.clsBalance').text());
                }
                else {
    
                    if (Prev == CurrLocId) {
                        _Sum1 += parseFloat($(this).find('.clsPetrolAmount').text());
                        _Sum2 += parseFloat($(this).find('.clsMaintanenceAmount').text());
                        _Sum3 += parseFloat($(this).find('.clsInstallmentAmount').text());
                        _Sum4 += parseFloat($(this).find('.clsTotalRecovery').text());
                        _Sum5 += parseFloat($(this).find('.clsBalance').text());
                    }
                    else {
    
                        var GroupByItem = $(this).prev().find('.ABC').find(GroupBy).val();
    
                        $("<tr class='success'><th colspan=" + ColSpan + " style='text-align:left'>" + GroupByName + " : " + GroupByItem + "</th><th style='text-align:right'>" + _Sum1 + "</th><th style='text-align:right'>" + _Sum2 + "</th><th style='text-align:right'>" + _Sum3 + "</th><th style='text-align:right'>" + _Sum4 + "</th><th style='text-align:right'>" + _Sum5 + "</th></tr>").insertBefore($(this));
                        i = -1;
                        _Sum1 = parseFloat($(this).find('.clsPetrolAmount').text());
                        _Sum2 = parseFloat($(this).find('.clsMaintanenceAmount').text());
                        _Sum3 = parseFloat($(this).find('.clsInstallmentAmount').text());
                        _Sum4 = parseFloat($(this).find('.clsTotalRecovery').text());
                        _Sum5 = parseFloat($(this).find('.clsBalance').text());
                    }
    
                    if ($(this).is(':last-child')) {
    
                        var GroupByItem = $(this).find('.ABC').find(GroupBy).val();
    
                        $("<tr class='success'><th colspan=" + ColSpan + " style='text-align:left'>" + GroupByName + " : " + GroupByItem + "</th><th style='text-align:right'>" + _Sum1 + "</th><th style='text-align:right'>" + _Sum2 + "</th><th style='text-align:right'>" + _Sum3 + "</th><th style='text-align:right'>" + _Sum4 + "</th><th style='text-align:right'>" + _Sum5 + "</th></tr>").insertAfter($(this));
                        i = -1;
                        _Sum1 = 0;
                        _Sum2 = 0;
                        _Sum3 = 0;
                        _Sum4 = 0;
                        _Sum5 = 0;
                    }
                }
    
                //if (i == 0) {
                //    $("<tr class='success'><th colspan=" + ColSpan + ">Total</th><th>" + _Sum1 + "</th><th>" + _Sum2 + "</th><th>" + _Sum3 + "</th><th>" + _Sum4 + "</th><th>" + _Sum5 + "</th></tr>").insertAfter($(this));
                //    i = -1;
                //    _Sum1 = 0;
                //    _Sum2 = 0;
                //    _Sum3 = 0;
                //    _Sum4 = 0;
                //    _Sum5 = 0;
                //}
    
                Prev = CurrLocId;
                i++;
            }
            else {
                var divTbodyGoalFund = $('.tbodyCarListing').html('');
                $('#CarListing').tmpl(res).appendTo(divTbodyGoalFund);
            }
        });
    
        var _Counter = 0;
        $('.clsSNo').each(function (_Counter) {
    
            _Counter = _Counter + 1;
            $(this).text(_Counter);
    
        });
    
        SetReportHeader('Vehicle Report', 11, '');
        

        $('.div_reportbutton').show();
    }
    ProgressBarHide();
}

function ClearReport() {
    $('.clsReportH').hide();
    $('.tbodyCarListing').html('');
    $('.div_reportbutton').hide();
}