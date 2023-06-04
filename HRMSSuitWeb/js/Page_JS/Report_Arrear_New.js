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
            service.report_Arrear_New(EmployeeCode, CompanyId, fromDate, $(".ddlGroupBy").val(), onreport_Emp_List, null, null);
        }

    } else {
        showError('Please select Company');
    }
}

function replaceZeroAndNull(val) {
    if (val == "0" || val == null)
        return "-";
    else {
        //if ($.isNumeric(val))
        //    return parseInt(val);
        //else
        return val;
    }
}
function onreport_Emp_List(result) {

    ;
    if (result != "") {
        
        var res1 = JSON.parse(result);
      
        
        $('.theadArrearListlisting').html('');
            var headers = '<tr class="info HeaderColoumn">';
            var data = '';
           
            $(res1).each(function (i, val) {
                var tr = '';
                var IsFooter = false;
              
                var NoOfColoumn_td = 0;

                $.each(val, function (k, v) {
                    var values = v;
                   
                    if (i == 0) {
                        
                        headers += '<th>' + k + '</th>';
                        
                    }

                    

                    if (tr == '') {
                        if (values.includes('..') == true) {
                            tr = '<tr style="font-weight: bold;">';
                            values = values.replace('..', '');
                            IsFooter = true;
                        } else {
                            tr = '<tr>';
                        }
                        data += tr;
                    }


                    if (IsFooter == true) {
                        data += '<th>' + replaceZeroAndNull(values) + '</th>';
                    }
                    else {
                        data += '<td>' + replaceZeroAndNull(values) + '</td>';
                    }
                  
                });
                data += '</tr>';
            });
            headers += '</tr>';
            $('.theadArrearListlisting').append(headers);
            $('.tbodyArrearListListing').append(data);
       
       
        
       
        
    
    }



    // var GroupByValue = $('.ddlGroupBy').val();

    //var res = JSON.parse(result);
    //;
    //if (GroupByValue != 0) {
    //    res = res.sort(sortByProperty(GroupByValue));
    //}


    //var divTbodyGoalFund = $('.tbodyArrearListListing').html('');
    //$('#ArrearListListing').tmpl(res).appendTo(divTbodyGoalFund);


    //var sbasic = 0;
    //var shrt = 0;
    //var smed = 0;
    //var smpf = 0;
    //var sgross = 0;
    //var snet = 0;


    //$('.clssbasic').each(function () {
    //    sbasic += parseFloat($(this).text().trim());
    //});
    //$('.clsshrt').each(function () {
    //    shrt += parseFloat($(this).text().trim());
    //});
    //$('.clssmed').each(function () {
    //    smed += parseFloat($(this).text().trim());
    //});

    //$('.clssmpf').each(function () {
    //    smpf += parseFloat($(this).text().trim());
    //});
    //$('.clssgross').each(function () {
    //    sgross += parseFloat($(this).text().trim());
    //});
    //$('.clssnet').each(function () {
    //    snet += parseFloat($(this).text().trim());
    //});
  
    


    //$('.tdsbasic').text(sbasic);
    //$('.tdshrt').text(shrt);
    //$('.tdsmed').text(smed);
    //$('.tdsmpf').text(smpf);
    //$('.tdsgross').text(sgross);
    //$('.tdsnet').text(snet);
  



    //var Prev = 0;
    //var i = 0;
    //var _Sum1 = 0;
    //var _Sum2 = 0;
    //var _Sum3 = 0;
    //var _Sum4 = 0;
    //var _Sum5 = 0;
    //var _Sum6 = 0;
   

    //var GroupBy = '.' + $('.ddlGroupBy').val();

    //var ColSpan = $('.clssbasic').index() - 0;

    //var GroupByName = $(".ddlGroupBy option:selected").text();

    //var GroupByValue = $('.ddlGroupBy').val();

    //if (GroupByValue != 0) {
    //    $('.trList').each(function () {


    //        var CurrLocId = $(this).find('.ABC').find(GroupBy).val();

    //        if ($(this).is(':first-child')) {

    //            _Sum1 += parseFloat($(this).find('.clssbasic').text());
    //            _Sum2 += parseFloat($(this).find('.clsshrt').text());
    //            _Sum3 += parseFloat($(this).find('.clssmed').text());
    //            _Sum4 += parseFloat($(this).find('.clssmpf').text());
    //            _Sum5 += parseFloat($(this).find('.clssgross').text());
    //            _Sum6 += parseFloat($(this).find('.clssnet').text());
               
    //        }
    //        else {

    //            if (Prev == CurrLocId) {
    //                _Sum1 += parseFloat($(this).find('.clssbasic').text());
    //                _Sum2 += parseFloat($(this).find('.clsshrt').text());
    //                _Sum3 += parseFloat($(this).find('.clssmed').text());
    //                _Sum4 += parseFloat($(this).find('.clssmpf').text());
    //                _Sum5 += parseFloat($(this).find('.clssgross').text());
    //                _Sum6 += parseFloat($(this).find('.clssnet').text());
                    
    //            }
    //            else {

    //                var GroupByItem = $(this).prev().find('.ABC').find(GroupBy).val();

    //                $("<tr class='success'><th colspan=" + ColSpan + " style='text-align:left'>" + GroupByName + " : " + GroupByItem + "</th><th style='text-align:right'>" + _Sum1 + "</th><th style='text-align:right'>" + _Sum2 + "</th><th style='text-align:right'>" + _Sum3 + "</th><th style='text-align:right'>" + _Sum4 + "</th><th style='text-align:right'>" + _Sum5 + "</th><th style='text-align:right'>" + _Sum6 + "</th><th style='text-align:right'></th></tr>").insertBefore($(this));
    //                i = -1;
    //                _Sum1 = parseFloat($(this).find('.clssbasic').text());
    //                _Sum2 = parseFloat($(this).find('.clsshrt').text());
    //                _Sum3 = parseFloat($(this).find('.clssmed').text());
    //                _Sum4 = parseFloat($(this).find('.clssmpf').text());
    //                _Sum5 = parseFloat($(this).find('.clssgross').text());
    //                _Sum6 = parseFloat($(this).find('.clssnet').text());
                  
    //            }

    //            if ($(this).is(':last-child')) {
    //                var GroupByItem = $(this).find('.ABC').find(GroupBy).val();
    //                $("<tr class='success'><th colspan=" + ColSpan + " style='text-align:left'>" + GroupByName + " : " + GroupByItem + "</th><th style='text-align:right'>" + _Sum1 + "</th><th style='text-align:right'>" + _Sum2 + "</th><th style='text-align:right'>" + _Sum3 + "</th><th style='text-align:right'>" + _Sum4 + "</th><th style='text-align:right'>" + _Sum5 + "</th><th style='text-align:right'>" + _Sum6 + "</th><th style='text-align:right'></th></tr>").insertAfter($(this));
    //                i = -1;
    //                _Sum1 = 0;
    //                _Sum2 = 0;
    //                _Sum3 = 0;
    //                _Sum4 = 0;
    //                _Sum5 = 0;
    //                _Sum6 = 0;
                    
    //            }
    //        }

    //        Prev = CurrLocId;
    //        i++;

    //    });
    //}
    //else {
    //    var divTbodyGoalFund = $('.tbodyArrearListListing').html('');
    //    $('#ArrearListListing').tmpl(res).appendTo(divTbodyGoalFund);

    //}




    var Para2 = '';


    SetReportHeader('Arrear List Report', 10, Para2);
    $('.div_reportbutton').show();
    addSerialNumber();
    ProgressBarHide();
}

function ClearReport() {
    $('.div_reportbutton').hide();
    $('.clsReportH').hide();
    $('.tbodyArrearListListing').html('');
    $('.tdsmmyy').text('');
    $('.tdsbasic').text('');
    $('.tdshrt').text('');
    $('.tdsmed').text('');
    $('.tdsmpf').text('');
    $('.tdsgross').text('');
    $('.tdsnet').text('');
    $('.tdsyrmm').text('');
}

