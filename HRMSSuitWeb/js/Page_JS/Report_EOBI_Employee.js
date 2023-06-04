function TriggerPageLoads() {
    $(".btnCancelSearch").click(function () {
        $('.txtMonthOfPayroll').datepicker('setDate', null);
        //ReqGrp  /*$('.ddlGroupBy').val('0');*/
        ClearReport();
    });

    //BindBank();
    BindGroupByDDL();
    $('.ddlGroupBy option').filter('[value="clsCompany"],[value="clsDesignation"],[value="clsBankName"]').remove();
}

function GetEmployee() {

    var CompanyId = $('.ddlCompany').val();
    var EmployeeCode = $('.txtEmployeeCode').val() == "" ? null : $('.txtEmployeeCode').val();
    var FromDate = $('.txtMonthOfPayroll').val() == "" ? null : formatDate($('.txtMonthOfPayroll').val());
    //var ToDate = $('.txtToDate').val() == "" ? null : formatDate($('.txtToDate').val());

    if (CompanyId > 0) {

        if (FromDate != null) {
            if (!validateForm('.divMonthPayroll'))
                return;
            ProgressBarShow();
            ClearReport();
            var service = new HrmsSuiteHcmService.HcmService();
            service.Report_EOBI_Employee(EmployeeCode, CompanyId, FromDate, onreport_eobi_employee, null, null);
        }
        else {
            showError('Please select From Date');
        }

    } else {
        showError('Please select Company');
    }
}

function onreport_eobi_employee(result) {


    ;
    var GroupByValue = $('.ddlGroupBy').val();

    var res = JSON.parse(result);

    if (GroupByValue != 0) {
        res = res.sort(sortByProperty(GroupByValue));
    }
    var divTbodyGoalFund = $('.tbodyEOBIEmployeeListing').html('');
    $('#EOBIEmployeeListing').tmpl(res).appendTo(divTbodyGoalFund);

    var TotalDays = 0;
    var TotalWadges = 0;
    var TotalEmployerCount = 0;
    var TotalEmployeeCount = 0;
    var TotalTotalCount = 0;



    $('.clsDays').each(function () {
        TotalDays += parseFloat($(this).text().trim());
    });
    $('.clswages').each(function () {
        TotalWadges += parseFloat($(this).text().trim());
    });
    $('.clsemplyr_con').each(function () {
        TotalEmployerCount += parseFloat($(this).text().trim());
    });
    $('.clsemp_cont').each(function () {
        TotalEmployeeCount += parseFloat($(this).text().trim());
    });
    $('.clstotal_cont').each(function () {
        TotalTotalCount += parseFloat($(this).text().trim());
    });
  



    $('.tdTotalDays').text(TotalDays);
    $('.tdTotalWadges').text(TotalWadges);
    $('.tdTotalEmployerCount').text(TotalEmployerCount);
    $('.tdEmployeeCount').text(TotalEmployeeCount);
    $('.tdTotalCount').text(TotalTotalCount);
  

    var Prev = 0;
    var i = 0;
    var _Sum1 = 0, _Sum2 = 0, _Sum3 = 0, _Sum4 = 0, _Sum5 = 0;
    var GroupBy = '.' + $('.ddlGroupBy').val();

    var ColSpan = $('.clsDays').index() - 0;

    var GroupByName = $(".ddlGroupBy option:selected").text();

    var GroupByValue = $('.ddlGroupBy').val();

    if (GroupByValue != 0) {

        $('.trList').each(function () {


            var CurrLocId = $(this).find('.ABC').find(GroupBy).val();

            if ($(this).is(':first-child')) {
                _Sum1 += parseFloat($(this).find('.clsDays').text());
                _Sum2 += parseFloat($(this).find('.clswages').text());
                _Sum3 += parseFloat($(this).find('.clsemplyr_con').text());
                _Sum4 += parseFloat($(this).find('.clsemp_cont').text());
                _Sum5 += parseFloat($(this).find('.clstotal_cont').text());
            }
            else {

                if (Prev == CurrLocId) {
                    _Sum1 += parseFloat($(this).find('.clsDays').text());
                    _Sum2 += parseFloat($(this).find('.clswages').text());
                    _Sum3 += parseFloat($(this).find('.clsemplyr_con').text());
                    _Sum4 += parseFloat($(this).find('.clsemp_cont').text());
                    _Sum5 += parseFloat($(this).find('.clstotal_cont').text());

                }
                else {

                    var GroupByItem = $(this).prev().find('.ABC').find(GroupBy).val();

                    $("<tr class='success'><th colspan=" + ColSpan + " style='text-align:left'>" + GroupByName + " : " + GroupByItem + "</th><th style='text-align:right'>" + _Sum1 + "</th><th style='text-align:right'>" + _Sum2 + "</th style='text-align:right'><th>" + _Sum3 + "</th><th style='text-align:right'>" + _Sum4 + "</th><th style='text-align:right'>" + _Sum5 + "</th></tr>").insertBefore($(this));
                    i = -1;
                    _Sum1 = parseFloat($(this).find('.clsDays').text());
                    _Sum2 = parseFloat($(this).find('.clswages').text());
                    _Sum3 = parseFloat($(this).find('.clsemplyr_con').text());
                    _Sum4 = parseFloat($(this).find('.clsemp_cont').text());
                    _Sum5 = parseFloat($(this).find('.clstotal_cont').text());

                }

                if ($(this).is(':last-child')) {
                    var GroupByItem = $(this).find('.ABC').find(GroupBy).val();
                    $("<tr class='success'><th colspan=" + ColSpan + " style='text-align:left'>" + GroupByName + " : " + GroupByItem + "</th><th style='text-align:right'>" + _Sum1 + "</th><th style='text-align:right'>" + _Sum2 + "</th style='text-align:right'><th>" + _Sum3 + "</th><th style='text-align:right'>" + _Sum4 + "</th><th style='text-align:right'>" + _Sum5 + "</th></tr>").insertAfter($(this));
                    /*$("<tr class='success'><th colspan=" + ColSpan + ">" + GroupByName + " : " + GroupByItem + "</th><th>" + _Sum1 + "</th><th>" + _Sum2 + "</th><th></th></tr>").insertAfter($(this));*/
                    i = -1;
                    _Sum1 = 0;
                    _Sum2 = 0;
                    _Sum3 = 0;
                    _Sum4 = 0;
                    _Sum5 = 0;
                   
                }
            }

            Prev = CurrLocId;
            i++;

        });

    }
    else {
        var divTbodyGoalFund = $('.tbodyEOBIEmployeeListing').html('');
        $('#EOBIEmployeeListing').tmpl(res).appendTo(divTbodyGoalFund);

    }
    var Para2 = '';


    SetReportHeader('EOBI Employee List Report', 10, Para2);
    $('.div_reportbutton').show();
    addSerialNumber();
    ProgressBarHide();


    ///*ReqGrp var GroupByValue = $('.ddlGroupBy').val();*/

    //var res = JSON.parse(result);


    //var divTbodyGoalFund = $('.tbodyEOBIEmployeeListing').html('');
    //$('#EOBIEmployeeListing').tmpl(res).appendTo(divTbodyGoalFund);

    //var TotalDays = 0;
    //var TotalWadges = 0;
    //var TotalEmployerCount = 0;
    //var TotalEmployeeCount = 0;
    //var TotalTotalCount = 0;


    //$('.clsDays').each(function () {
    //    TotalDays += parseFloat($(this).text().trim());
    //});

    //$('.clswages').each(function () {
    //    TotalWadges += parseFloat($(this).text().trim());
    //});

    //$('.clsemplyr_con').each(function () {
    //    TotalEmployerCount += parseFloat($(this).text().trim());
    //});


    //$('.clsemp_cont').each(function () {
    //    TotalEmployeeCount += parseFloat($(this).text().trim());
    //});

    //$('.clstotal_cont').each(function () {
    //    TotalTotalCount += parseFloat($(this).text().trim());
    //});

    //$('.tdTotalDays').text(TotalDays);
    //$('.tdTotalWadges').text(TotalWadges);
    //$('.tdTotalEmployerCount').text(TotalEmployerCount);
    //$('.tdEmployeeCount').text(TotalEmployeeCount);
    //$('.tdTotalCount').text(TotalTotalCount);

    //var divTbodyGoalFund = $('.tbodyEOBIEmployeeListing').html('');
    //$('#EOBIEmployeeListing').tmpl(res).appendTo(divTbodyGoalFund);



    //var Para2 = '';

    //SetReportHeader('EOBI Employee List Report', 10, Para2);
    //$('.div_reportbutton').show();
    //addSerialNumber();
    //ProgressBarHide();
}

function ClearReport() {
    $('.div_reportbutton').hide();
    $('.clsReportH').hide();
    $('.tbodyEOBIEmployeeListing').html('');
    $('.tdTotalDays').text('');
    $('.tdTotalWadges').text('');
    $('.tdTotalEmployerCount').text('');
    $('.tdEmployeeCount').text('');
    $('.tdTotalCount').text('');
}
