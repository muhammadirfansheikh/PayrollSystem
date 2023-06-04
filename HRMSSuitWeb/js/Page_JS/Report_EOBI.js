function TriggerPageLoads() {
    $(".btnCancelSearch").click(function () {
        $('.txtMonthOfPayroll').datepicker('setDate', null);
        $('.ddlGroupBy').val('0');
        ClearReport();
    });

    BindGroupByDDL();
    $('.ddlGroupBy option').filter('[value="clsCompany"],[value="clsDesignation"],[value="clsDepartment"],[value="clsBankName"]').remove();
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
        ClearReport()
        var service = new HrmsSuiteHcmService.HcmService();
        service.report_EOBIList(EmployeeCode, GroupId, CompanyId, LocationId, BusinessUnitId, DepartmentId, CostCenterId, CategoryId, DesignationId, Firstname, Lastname, PayrollMonth, onGetReportEOBI, null, null);
    } else {
        showError('Please select Company');
    }
}

function onGetReportEOBI(result) {
    var res = jQuery.parseJSON(result);
    if (res.length > 0) {
        var GroupByValue = $('.ddlGroupBy').val();

        if (GroupByValue != 0) {
            res = res.sort(sortByProperty(GroupByValue));
        }
        var divTbodyGoalFund = $('.tbodyEOBIListing').html('');
        $('#EOBIListing').tmpl(res).appendTo(divTbodyGoalFund);

        var EmpCont = 0;
        var CompanyCont = 0;
        var Arrear = 0;
        var Total = 0;
        var WagesPaid = 0;


        //$('.trList').each(function () {

        //    EmpCont += parseFloat($(this).find('.clsEmployeeCont').text().trim());
        //    CompanyCont += parseFloat($(this).find('.clsCompanyCont').text().trim());
        //    Arrear += parseFloat($(this).find('.clsArrear').text().trim());
        //    Total += parseFloat($(this).find('.clsTotal').text().trim());
        //});

        //$('.clsEmployeeCont').each(function () {
        //    EmpCont += parseFloat($(this).text().trim());
        //});

        //$('.clsCompanyCont').each(function () {
        //    CompanyCont += parseFloat($(this).text().trim());
        //});

        //$('.clsArrear').each(function () {
        //    Arrear += parseFloat($(this).text().trim());
        //});

        //$('.clsTotal').each(function () {
        //    Total += parseFloat($(this).text().trim());
        //});

        //$('.tdEmployeeContribution').text(EmpCont);
        //$('.tdCompanyContribution').text(CompanyCont);
        //$('.tdArrear').text(Arrear);
        //$('.tdTotal').text(Total);

        var _Counter = 0;
        var Prev = 0;
        var i = 0;
        var _Sum1 = 0, _Sum2 = 0, _Sum3 = 0, _Sum4 = 0, _Sum5 = 0;
        var GroupBy = '.' + $('.ddlGroupBy').val();

        //var ColSpan = $('.clsMonthlyCont').index();
        var ColSpan = 12;
        var GroupByName = $(".ddlGroupBy option:selected").text();
        var GroupByValue = $('.ddlGroupBy').val();

        $('.trList').each(function () {

            if (GroupByValue != 0) {
                //var CurrLocId = $(this).find(GroupBy).text();
                var CurrLocId = $(this).find('.ABC').find(GroupBy).val();

                if ($(this).is(':first-child')) {
                    _Sum1 += parseFloat($(this).find('.clsEmployeeCont').text());
                    _Sum2 += parseFloat($(this).find('.clsCompanyCont').text());
                    _Sum3 += parseFloat($(this).find('.clsArrear').text());
                    _Sum4 += parseFloat($(this).find('.clsTotal').text());
                    //_Sum5 += parseFloat($(this).find('.clsWagesPaid').val());
                }
                else {

                    if (Prev == CurrLocId) {
                        _Sum1 += parseFloat($(this).find('.clsEmployeeCont').text());
                        _Sum2 += parseFloat($(this).find('.clsCompanyCont').text());
                        _Sum3 += parseFloat($(this).find('.clsArrear').text());
                        _Sum4 += parseFloat($(this).find('.clsTotal').text());
                        //_Sum5 += parseFloat($(this).find('.clsWagesPaid').val());
                    }
                    else {

                        var GroupByItem = $(this).prev().find('.ABC').find(GroupBy).val();

                        $("<tr class='success'><th colspan=" + ColSpan + " style='text-align:left'>" + GroupByName + " : " + GroupByItem + "</th><th style='text-align:right'>" + _Sum1 + "</th><th style='text-align:right'>" + _Sum2 + "</th><th style='text-align:right'>" + _Sum3 + "</th><th style='text-align:right'>" + _Sum4 + "</th></tr>").insertBefore($(this));
                        i = -1;
                        _Sum1 = parseFloat($(this).find('.clsEmployeeCont').text());
                        _Sum2 = parseFloat($(this).find('.clsCompanyCont').text());
                        _Sum3 = parseFloat($(this).find('.clsArrear').text());
                        _Sum4 = parseFloat($(this).find('.clsTotal').text());
                        //_Sum5 = parseFloat($(this).find('.clsWagesPaid').val());
                    }

                    if ($(this).is(':last-child')) {

                        var GroupByItem = $(this).find('.ABC').find(GroupBy).val();

                        $("<tr class='success'><th colspan=" + ColSpan + " style='text-align:left'>" + GroupByName + " : " + GroupByItem + "</th><th style='text-align:right'>" + _Sum1 + "</th><th style='text-align:right'>" + _Sum2 + "</th><th style='text-align:right'>" + _Sum3 + "</th><th style='text-align:right'>" + _Sum4 + "</th></tr>").insertAfter($(this));
                        i = -1;
                        _Sum1 = 0;
                    }
                }

                Prev = CurrLocId;
                i++;
            }
            //else
            {
                //var divTbodyGoalFund = $('.tbodyEOBIListing').html('');
                //$('#EOBIListing').tmpl(res).appendTo(divTbodyGoalFund);
                //;
                EmpCont += parseFloat($(this).find('.clsEmployeeCont').text().trim());
                CompanyCont += parseFloat($(this).find('.clsCompanyCont').text().trim());
                Arrear += parseFloat($(this).find('.clsArrear').text().trim());
                Total += parseFloat($(this).find('.clsTotal').text().trim());
                //var WagesPaid_val = $(this).find('.clsWagesPaid').val();
                //if (WagesPaid_val.includes('Above Limit') == true) {
                //    var WagesPaid_ = WagesPaid_val.split('Above Limit');
                //    WagesPaid += parseFloat(WagesPaid_[1]);
                //} else {
                //    WagesPaid += parseFloat($(this).find('.clsWagesPaid').val());
                //}
            }

        });

        $('.tdEmployeeContribution').text(EmpCont);
        $('.tdCompanyContribution').text(CompanyCont);
        $('.tdArrear').text(Arrear);
        $('.tdTotal').text(Total);
        //$('.tdWagesPaid').text(WagesPaid);

        $('.clsSNo').each(function (_Counter) {

            _Counter = _Counter + 1;
            $(this).text(_Counter);

        });

        SetReportHeader('EOBI Report', 17, '');
        $('.div_reportbutton').show();
    }
    ProgressBarHide();
}

function ClearReport() {
    $('.clsReportH').hide();
    $('.tbodyEOBIListing').html('');
    $('.div_reportbutton').hide();
    $('.tdEmployeeContribution').text('0');
    $('.tdCompanyContribution').text('0');
    $('.tdArrear').text('0');
    $('.tdTotal').text('0');
    //$('.tdWagesPaid').text('0');
}