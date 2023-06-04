function TriggerPageLoads() {
    $(".btnCancelSearch").click(function () {
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

        if ($(".txtFromDate").val() == '') {

            showError('Please Select Month.');
        }
        else {
            var EmployeeCode = $('.txtEmployeeCode').val() == '' ? 0 : $('.txtEmployeeCode').val();
            var fromDate = $(".txtFromDate").val();


            ProgressBarShow();
            ClearReport();
            var service = new HrmsSuiteHcmService.HcmService();
            service.report_Employee_Increment(EmployeeCode, CompanyId, fromDate, onreport_Emp_List, null, null);
        }

    } else {
        showError('Please select Company');
    }
}


function onreport_Emp_List(result) {

    var GroupByValue = $('.ddlGroupBy').val();

    var res = JSON.parse(result);

    if (GroupByValue != 0) {
        res = res.sort(sortByProperty(GroupByValue));
    }
    var divTbodyGoalFund = $('.tbodyEmployeeIncrementListListing').html('');
    $('#EmployeeIncrementListListing').tmpl(res).appendTo(divTbodyGoalFund);

    var incr = 0;
    var nbas = 0;
    var nhrt = 0;
    var ncla1 = 0;
    var nmed = 0;
    var nspl1 = 0;
    var ngros = 0;
    var obas = 0;
    var ohrt = 0;
    var ocla1 = 0;
    var omed = 0;
    var ospl1 = 0;
    var ogros = 0;
    var normal = 0;
    var spl = 0;
    var adj = 0;
    var pro = 0;

    var loc = 0;
    var prov = 0;
    var dep = 0;
    var cost = 0;
    var ncost = 0;


    $('.clsincr').each(function () {
        incr += parseFloat($(this).text().trim());
    });
    $('.clsnbas').each(function () {
        nbas += parseFloat($(this).text().trim());
    });
    $('.clsnhrt').each(function () {
        nhrt += parseFloat($(this).text().trim());
    });
    $('.clsncla1').each(function () {
        ncla1 += parseFloat($(this).text().trim());
    });
    $('.clsnmed').each(function () {
        nmed += parseFloat($(this).text().trim());
    });
    $('.clsnspl1').each(function () {
        nspl1 += parseFloat($(this).text().trim());
    });
    $('.clsngros').each(function () {
        ngros += parseFloat($(this).text().trim());
    });
    $('.clsobas').each(function () {
        obas += parseFloat($(this).text().trim());
    });
    $('.clsohrt').each(function () {
        ohrt += parseFloat($(this).text().trim());
    });


    $('.clsocla1').each(function () {
        ocla1 += parseFloat($(this).text().trim());
    });

    $('.clsomed').each(function () {
        omed += parseFloat($(this).text().trim());
    });
    $('.clsospl1').each(function () {
        ospl1 += parseFloat($(this).text().trim());
    });
    $('.clsogros').each(function () {
        ogros += parseFloat($(this).text().trim());
    });
    $('.clsnormal').each(function () {
        normal += parseFloat($(this).text().trim());
    });
    $('.clsspl').each(function () {
        spl += parseFloat($(this).text().trim());
    });
    $('.clsadj').each(function () {
        adj += parseFloat($(this).text().trim());
    });
    $('.clspro').each(function () {
        pro += parseFloat($(this).text().trim());
    });

    $('.clsloc').each(function () {
        loc += parseFloat($(this).text().trim());
    });
    $('.clsprov').each(function () {
        prov += parseFloat($(this).text().trim());
    });
    $('.clsdep').each(function () {
        dep += parseFloat($(this).text().trim());
    });
    $('.clscost').each(function () {
        cost += parseFloat($(this).text().trim());
    });
    $('.clsncost').each(function () {
        ncost += parseFloat($(this).text().trim());
    });



    $('.tdincr').text(incr);
    $('.tdnbas').text(nbas);
    $('.tdnhrt').text(nhrt);
    $('.tdncla1').text(ncla1);
    $('.tdnmed').text(nmed);
    $('.tdnspl1').text(nspl1);
    $('.tdngros').text(ngros);
    $('.tdobas').text(obas);
    $('.tdohrt').text(ohrt);
    $('.tdocla1').text(ocla1);
    $('.tdomed').text(omed);
    $('.tdospl1').text(ospl1);
    $('.tdogros').text(ogros);
    $('.tdnormal').text(normal);
    $('.tdspl').text(spl);
    $('.tdadj').text(adj);
    $('.tdpro').text(pro);

    $('.tdloc').text(loc);
    $('.tdprov').text(prov);
    $('.tddep').text(dep);
    $('.tdcost').text(cost);
    $('.tdncost').text(ncost);


    var Prev = 0;
    var i = 0;
    var _Sum1 = 0;
    var _Sum2 = 0;
    var _Sum3 = 0;
    var _Sum4 = 0;
    var _Sum5 = 0;
    var _Sum6 = 0;
    var _Sum7 = 0;
    var _Sum8 = 0;
    var _Sum9 = 0;
    var _Sum10 = 0;
    var _Sum11 = 0;
    var _Sum12 = 0;
    var _Sum13 = 0;
    var _Sum14 = 0;
    var _Sum15 = 0;
    var _Sum16 = 0;
    var _Sum17 = 0;
    var _Sum18 = 0;
    var _Sum19 = 0;
    var _Sum20 = 0;
    var _Sum21 = 0;
    var _Sum22 = 0;

    var GroupBy = '.' + $('.ddlGroupBy').val();

    var ColSpan = $('.clsincr').index() - 0;

    var GroupByName = $(".ddlGroupBy option:selected").text();

    var GroupByValue = $('.ddlGroupBy').val();
    if (GroupByValue != 0) {
        $('.trList').each(function () {


            var CurrLocId = $(this).find('.ABC').find(GroupBy).val();

            if ($(this).is(':first-child')) {

                _Sum1 += parseFloat($(this).find('.clsincr').text());
                _Sum2 += parseFloat($(this).find('.clsnbas').text());
                _Sum3 += parseFloat($(this).find('.clsnhrt').text());
                _Sum4 += parseFloat($(this).find('.clsncla1').text());
                _Sum5 += parseFloat($(this).find('.clsnmed').text());
                _Sum6 += parseFloat($(this).find('.clsnspl1').text());
                _Sum7 += parseFloat($(this).find('.clsngros').text());
                _Sum8 += parseFloat($(this).find('.clsobas').text());
                _Sum9 += parseFloat($(this).find('.clsohrt').text());
                _Sum10 += parseFloat($(this).find('.clsocla1').text());
                _Sum11 += parseFloat($(this).find('.clsomed').text());
                _Sum12 += parseFloat($(this).find('.clsospl1').text());
                _Sum13 += parseFloat($(this).find('.clsogros').text());
                _Sum14 += parseFloat($(this).find('.clsnormal').text());
                _Sum15 += parseFloat($(this).find('.clsspl').text());
                _Sum16 += parseFloat($(this).find('.clsadj').text());
                _Sum17 += parseFloat($(this).find('.clspro').text());
                _Sum18 += parseFloat($(this).find('.clsloc').text());
                _Sum19 += parseFloat($(this).find('.clsprov').text());
                _Sum20 += parseFloat($(this).find('.clsdep').text());
                _Sum21 += parseFloat($(this).find('.clscost').text());
                _Sum22 += parseFloat($(this).find('.clsncost').text());



            }
            else {

                if (Prev == CurrLocId) {
                    _Sum1 += parseFloat($(this).find('.clsincr').text());
                    _Sum2 += parseFloat($(this).find('.clsnbas').text());
                    _Sum3 += parseFloat($(this).find('.clsnhrt').text());
                    _Sum4 += parseFloat($(this).find('.clsncla1').text());
                    _Sum5 += parseFloat($(this).find('.clsnmed').text());
                    _Sum6 += parseFloat($(this).find('.clsnspl1').text());
                    _Sum7 += parseFloat($(this).find('.clsngros').text());
                    _Sum8 += parseFloat($(this).find('.clsobas').text());
                    _Sum9 += parseFloat($(this).find('.clsohrt').text());
                    _Sum10 += parseFloat($(this).find('.clsocla1').text());
                    _Sum11 += parseFloat($(this).find('.clsomed').text());
                    _Sum12 += parseFloat($(this).find('.clsospl1').text());
                    _Sum13 += parseFloat($(this).find('.clsogros').text());
                    _Sum14 += parseFloat($(this).find('.clsnormal').text());
                    _Sum15 += parseFloat($(this).find('.clsspl').text());
                    _Sum16 += parseFloat($(this).find('.clsadj').text());
                    _Sum17 += parseFloat($(this).find('.clspro').text());
                    _Sum18 += parseFloat($(this).find('.clsloc').text());
                    _Sum19 += parseFloat($(this).find('.clsprov').text());
                    _Sum20 += parseFloat($(this).find('.clsdep').text());
                    _Sum21 += parseFloat($(this).find('.clscost').text());
                    _Sum22 += parseFloat($(this).find('.clsncost').text());

                }
                else {

                    var GroupByItem = $(this).prev().find('.ABC').find(GroupBy).val();

                    $("<tr class='success'><th colspan=" + ColSpan + " style='text-align:left'>" + GroupByName + " : " + GroupByItem + "</th><th style='text-align:right'>" + _Sum1 + "</th><th style='text-align:right'>" + _Sum2 + "</th><th style='text-align:right'>" + _Sum3 + "</th><th style='text-align:right'>" + _Sum4 + "</th><th style='text-align:right'>" + _Sum5 + "</th><th style='text-align:right'>" + _Sum6 + "</th><th style='text-align:right'>" + _Sum7 + "</th><th style='text-align:right'>" + _Sum8 + "</th><th style='text-align:right'>" + _Sum9 + "</th><th style='text-align:right'>" + _Sum10 + "</th><th style='text-align:right'>" + _Sum11 + "</th><th style='text-align:right'>" + _Sum12 + "</th><th style='text-align:right'>" + _Sum13 + "</th><th style='text-align:right'>" + _Sum14 + "</th><th style='text-align:right'>" + _Sum15 + "</th><th style='text-align:right'>" + _Sum16 + "</th><th style='text-align:right'>" + _Sum17 + "</th><th style='text-align:right'>" + _Sum18 + "</th><th style='text-align:right'>" + _Sum19 + "</th><th style='text-align:right'>" + _Sum20 + "</th><th style='text-align:right'>" + _Sum21 + "</th><th></th><th></th><th></th><th></th><th style='text-align:right'>" + _Sum22 + "</th></tr>").insertBefore($(this));
                    i = -1;
                    _Sum1 = parseFloat($(this).find('.clsincr').text());
                    _Sum2 = parseFloat($(this).find('.clsnbas').text());
                    _Sum3 = parseFloat($(this).find('.clsnhrt').text());
                    _Sum4 = parseFloat($(this).find('.clsncla1').text());
                    _Sum5 = parseFloat($(this).find('.clsnmed').text());
                    _Sum6 = parseFloat($(this).find('.clsnspl1').text());
                    _Sum7 = parseFloat($(this).find('.clsngros').text());
                    _Sum8 = parseFloat($(this).find('.clsobas').text());
                    _Sum9 = parseFloat($(this).find('.clsohrt').text());
                    _Sum10 = parseFloat($(this).find('.clsocla1').text());
                    _Sum11 = parseFloat($(this).find('.clsomed').text());
                    _Sum12 = parseFloat($(this).find('.clsospl1').text());
                    _Sum13 = parseFloat($(this).find('.clsogros').text());
                    _Sum14 = parseFloat($(this).find('.clsnormal').text());
                    _Sum15 = parseFloat($(this).find('.clsspl').text());
                    _Sum16 = parseFloat($(this).find('.clsadj').text());
                    _Sum17 = parseFloat($(this).find('.clspro').text());
                    _Sum18 = parseFloat($(this).find('.clsloc').text());
                    _Sum19 = parseFloat($(this).find('.clsprov').text());
                    _Sum20 = parseFloat($(this).find('.clsdep').text());
                    _Sum21 = parseFloat($(this).find('.clscost').text());
                    _Sum22 = parseFloat($(this).find('.clsncost').text());
                }

                if ($(this).is(':last-child')) {
                    var GroupByItem = $(this).find('.ABC').find(GroupBy).val();
                    $("<tr class='success'><th colspan=" + ColSpan + " style='text-align:left'>" + GroupByName + " : " + GroupByItem + "</th><th style='text-align:right'>" + _Sum1 + "</th><th style='text-align:right'>" + _Sum2 + "</th><th style='text-align:right'>" + _Sum3 + "</th><th style='text-align:right'>" + _Sum4 + "</th><th style='text-align:right'>" + _Sum5 + "</th><th style='text-align:right'>" + _Sum6 + "</th><th style='text-align:right'>" + _Sum7 + "</th><th style='text-align:right'>" + _Sum8 + "</th><th style='text-align:right'>" + _Sum9 + "</th><th style='text-align:right'>" + _Sum10 + "</th><th style='text-align:right'>" + _Sum11 + "</th><th style='text-align:right'>" + _Sum12 + "</th><th style='text-align:right'>" + _Sum13 + "</th><th style='text-align:right'>" + _Sum14 + "</th><th style='text-align:right'>" + _Sum15 + "</th><th style='text-align:right'>" + _Sum16 + "</th><th style='text-align:right'>" + _Sum17 + "</th><th style='text-align:right'>" + _Sum18 + "</th><th style='text-align:right'>" + _Sum19 + "</th><th style='text-align:right'>" + _Sum20 + "</th><th style='text-align:right'>" + _Sum21 + "</th><th></th><th></th><th></th><th></th><th style='text-align:right'>" + _Sum22 + "</th></tr>").insertAfter($(this));
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

                }
            }

            Prev = CurrLocId;
            i++;

        });
    }
    else {
        var divTbodyGoalFund = $('.tbodyEmployeeIncrementListListing').html('');
        $('#EmployeeIncrementListListing').tmpl(res).appendTo(divTbodyGoalFund);

    }



    var Para2 = '';


    SetReportHeader('Employee Increment List Report', 10, Para2);
    $('.div_reportbutton').show();
    addSerialNumber();
    ProgressBarHide();
}

function ClearReport() {
    $('.div_reportbutton').hide();
    $('.clsReportH').hide();
    $('.tbodyEmployeeIncrementListListing').html('');
    $('.incr').text('');
    $('.nbas').text('');
    $('.nhrt').text('');
    $('.ncla1').text('');
    $('.nmed').text('');
    $('.nspl1').text('');
    $('.ngros').text('');
    $('.obas').text('');
    $('.ohrt').text('');
    $('.ocla1').text('');
    $('.omed').text('');
    $('.ospl1').text('');
    $('.ogros').text('');
    $('.normal').text('');
    $('.spl').text('');
    $('.adj').text('');
    $('.pro').text('');

    $('.loc').text('');
    $('.prov').text('');
    $('.dep').text('');
    $('.cost').text('');
    $('.ncost').text('');
}

