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
        //var BankId = $('.ddlBranch').val();

        ProgressBarShow();
        ClearReport();
        var service = new HrmsSuiteHcmService.HcmService();
        service.report_Sessi(EmployeeCode, GroupId, CompanyId, LocationId, BusinessUnitId, DepartmentId, CostCenterId,
            CategoryId, DesignationId, Firstname, Lastname, PayrollMonth, onGetReportSessiList, null, null);
    } else {
        showError('Please select Company');

    }
}

function onGetReportSessiList(result) {
    var res = jQuery.parseJSON(result);
    if (res.length > 0) {
        var GroupByValue = $('.ddlGroupBy').val();
        if (GroupByValue != 0) {
            res = res.sort(sortByProperty(GroupByValue));
        }
        var divTbodyGoalFund = $('.tbodySessiListing').html('');
        $('#SessiListing').tmpl(res).appendTo(divTbodyGoalFund);

        var WagesTotal = 0;
        var SessiTotal = 0;
        var EmpContTotal = 0;
        var SesaContTotal = 0;

        $('.trList').each(function () {
            WagesTotal += ($(this).find('.clsWagesPaid').text().trim() == '' ? 0 : (parseFloat($(this).find('.clsWagesPaid').text().trim().toString().replace('Above Rs.',''))));
            EmpContTotal += $(this).find('.clsEmpCont').text().trim() == '' ? 0 : parseFloat($(this).find('.clsEmpCont').text().trim());
            SesaContTotal += $(this).find('.clsSesaCont').text().trim() == '' ? 0 : parseFloat($(this).find('.clsSesaCont').text().trim());
            SessiTotal += $(this).find('.clsWagesCont').text().trim() == '' ? 0 : parseFloat($(this).find('.clsWagesCont').text().trim());

        });
        $('.tdTotalWages').text(WagesTotal);
        $('.tdTotalEmpCont').text(EmpContTotal);
        $('.tdTotalSesaCont').text(SesaContTotal);
        $('.tdTotalWagesCont').text(SessiTotal);

        //;
        //$('.clsWages').each(function () {
        //    WagesTotal += parseFloat($(this).val().trim());
        //});
        //$('.tdTotalWages').text(WagesTotal);


        //$('.clsEmpCont').each(function () {
        //    EmpContTotal += parseFloat($(this).text().trim());
        //});
        //$('.tdTotalEmpCont').text(EmpContTotal);


        //$('.clsSesaCont').each(function () {
        //    SesaContTotal += parseFloat($(this).text().trim());
        //});
        //$('.tdTotalSesaCont').text(SesaContTotal);

        //$('.clsWagesCont').each(function () {
        //    SessiTotal += parseFloat($(this).text().trim());
        //});
        //$('.tdTotalWagesCont').text(SessiTotal);


        //alert(WagesTotal);
        //alert(SessiTotal);
        //alert(EmpContTotal);
        //alert(SesaContTotal);
         ColSpan = 13;
        if (GroupByValue != 0) {
            var Sno = 0;
            var Prev = 0;
            var i = 0;
            var _SesaCont = 0;
            var _EmpCont = 0;
            var _WagesCont = 0;
            var _Wages = 0;
            var GroupBy = '.' + $('.ddlGroupBy').val();
            ColSpan = 8;//$('.clsEmpCont').index() - 1;
            var GroupByName = $(".ddlGroupBy option:selected").text();
            $('.trList').each(function ()
            {
                Sno = parseInt(Sno) + 1;
                $(this).closest('td').find('.clsSNo').text(parseInt(Sno));
                var GroupByValue = $('.ddlGroupBy').val();
                if (GroupByValue != 0) {
                    var CurrLocId = $(this).find('.ABC').find(GroupBy).val();
                    if ($(this).is(':first-child')) {
                        ;
                        _EmpCont += parseFloat($(this).find('.clsEmpCont').text());
                        _WagesCont += parseFloat($(this).find('.clsWagesCont').text());
                        _SesaCont += parseFloat($(this).find('.clsSesaCont').text());
                        _Wages += parseFloat(($(this).find('.clsWagesPaid').text().toString().replace('Above Rs.', '')));
                    }
                    else {

                        if (Prev == CurrLocId) {
                            _EmpCont += parseFloat($(this).find('.clsEmpCont').text());
                            _WagesCont += parseFloat($(this).find('.clsWagesCont').text());
                            _SesaCont += parseFloat($(this).find('.clsSesaCont').text());
                            _Wages += parseFloat(($(this).find('.clsWagesPaid').text().toString().replace('Above Rs.', '')));
                        }
                        else {

                            var GroupByItem = $(this).prev().find('.ABC').find(GroupBy).val();

                            $("<tr class='success'><th colspan=" + ColSpan + " style='text-align:left'>" + GroupByName + " : " + GroupByItem + "</th><th style='text-align: right;'>" + _Wages + "</th><th style='text-align: right;'>" + _EmpCont + "</th><th style='text-align: right;'>" + _SesaCont + "</th><th style='text-align: right;'>" + _WagesCont + "</th></tr>").insertBefore($(this));
                            i = -1;
                            _EmpCont = parseFloat($(this).find('.clsEmpCont').text());
                            _WagesCont = parseFloat($(this).find('.clsWagesCont').text());
                            _SesaCont = parseFloat($(this).find('.clsSesaCont').text());
                            _Wages = parseFloat(($(this).find('.clsWagesPaid').text().toString().replace('Above Rs.', '')));
                        }

                        if ($(this).is(':last-child')) {

                            var GroupByItem = $(this).find('.ABC').find(GroupBy).val();

                            $("<tr class='success'><th colspan=" + ColSpan + ">" + GroupByName + " : " + GroupByItem + "</th><th style='text-align: right;'>" + _Wages + "</th><th style='text-align: right;'>" + _EmpCont + "</th><th style='text-align: right;'>" + _SesaCont + "</th><th style='text-align: right;'>" + _WagesCont + "</th></tr>").insertAfter($(this));
                            i = -1;
                            _EmpCont = 0;
                            _WagesCont = 0;
                            _SesaCont = 0;
                            _Wages = 0;
                        }
                    }

                    Prev = CurrLocId;
                    i++;
                }
                else {
                    var divTbodyGoalFund = $('.tbodySessiListing').html('');
                    $('#SessiListing').tmpl(res).appendTo(divTbodyGoalFund);
                }
            });
        }
        //alert(ColSpan);
        var _Counter = 0;
        $('.clsSNo').each(function (_Counter) {
            _Counter = _Counter + 1;
            $(this).text(_Counter);
        });

        SetReportHeader('Sessi Report', 13, '');
        $('.div_reportbutton').show();
    }
    ProgressBarHide();
}

function ClearReport() {
    $('.clsReportH').hide();
    $('.tbodySessiListing').html('');

    $('.tdTotalWages').text('');
    $('.tdTotalWagesCont').text('');
    $('.tdTotalEmpCont').text('');
    $('.tdTotalSesaCont').text('');
    $('.div_reportbutton').hide();
}