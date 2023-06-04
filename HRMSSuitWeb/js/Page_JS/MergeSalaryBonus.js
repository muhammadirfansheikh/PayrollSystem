
TriggerPageLoads();

function TriggerPageLoads() {

    ;
    GetGroup();

   
}

function GetGroup() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getGroup(onGetGroup, null, null);
}

function onGetGroup(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlGroup", res);
    $(".ddlGroup").change();

    BindGroupByDDL();
    $('.ddlGroupBy option').filter('[value="clsCompany"],[value="clsDesignation"],[value="clsDepartment"],[value="clsBankName"]').remove();
}

function GetCompany(Group) {
    var service = new HrmsSuiteHcmService.HcmService();
    service.getCompanyByGroupId($(Group).val(), onGetCompany, null, null);
}

function onGetCompany(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlCompany", res);
    $(".ddlCompany").change();
}

function GetSalaryMonth() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.GetSalaryMonth($('.ddlCompany').val(), onGetSalaryMonth, null, null);
}

function onGetSalaryMonth(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlSalaryMonth", res);

}

function GetBonus() {
    var service = new HrmsSuiteHcmService.HcmService();
    service.GetBonusTransactionMaster($('.ddlCompany').val(), onGetBonus, null, null);
}

function onGetBonus(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlSeparateBonus", res);
    //$(".ddlCompany").change();
}

function ReleaseBonus() {
    if (!validateForm('.divPayrollForm'))
        return;

    //var Company = $(".ddlCompany").val();
    //var DateOfPayroll = formatDate($('.txtMonth').val());

    //ProgressBarShow();
    //var service = new HrmsSuiteHcmService.HcmService();
    //service.payrollBonusRelease(Company, DateOfPayroll, false, onGetBonusRelease, null, null);

    var Company = $(".ddlCompany").val();
    var BonusId = $(".ddlSeparateBonus").val();


    ProgressBarShow();
    var service = new HrmsSuiteHcmService.HcmService();
    service.payrollBonusRelease(Company, BonusId, true, onGetBonusRelease, null, null);
   
}

function onGetBonusRelease(result) {
    ;
    try {

        //if (result == '0') {
        //    showError("Bonus Informations are not yet updated!");
        //    ProgressBarHide();
        //    return;
        //}


       

        var res = jQuery.parseJSON(result);
        if (res.length > 0) {
            var GroupByValue = $('.ddlGroupBy').val();

            if (GroupByValue != 0) {
                res = res.sort(sortByProperty(GroupByValue));
            }
            var divTbodyGoalFund = $('.tbodyBonusListing').html('');
            $('#BonusListing').tmpl(res).appendTo(divTbodyGoalFund);

            var TotalBonus = 0;
         


            $('.clsBonusAmount').each(function () {
                TotalBonus += parseFloat($(this).text().trim());
            });

           




            $('.tdBonusAmount').text(TotalBonus);
           


            var Prev = 0;
            var i = 0;
            var _Sum1 = 0;
           

            var GroupBy = '.' + $('.ddlGroupBy').val();

            var ColSpan = $('.clsBonusAmount').index() - 0;

            var GroupByName = $(".ddlGroupBy option:selected").text();

            var GroupByValue = $('.ddlGroupBy').val();
            if (GroupByValue != 0) {
                $('.trList').each(function () {


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

                            $("<tr class='success'><th colspan=" + ColSpan + " style='text-align:left'>" + GroupByName + " : " + GroupByItem + "</th><th style='text-align:right'>" + _Sum1 + "</th><th></th><th></th></tr>").insertBefore($(this));
                            i = -1;
                            _Sum1 = parseFloat($(this).find('.clsBonusAmount').text());
                           
                        }

                        if ($(this).is(':last-child')) {
                            var GroupByItem = $(this).find('.ABC').find(GroupBy).val();
                            $("<tr class='success'><th colspan=" + ColSpan + " style='text-align:left'>" + GroupByName + " : " + GroupByItem + "</th><th style='text-align:right'>" + _Sum1 + "</th><th></th><th></th></tr>").insertAfter($(this));
                            i = -1;
                            _Sum1 = 0;
                           


                        }
                    }

                    Prev = CurrLocId;
                    i++;

                });
            }
            else {
                var divTbodyGoalFund = $('.tbodyBonusListing').html('');
                $('#BonusListing').tmpl(res).appendTo(divTbodyGoalFund);

            }


            
            var _Counter = 0;
            $('.clsSNo').each(function (_Counter) {
                _Counter = _Counter + 1;
                $(this).text(_Counter);
            });
            
            GetBonus();
            ProgressBarHide();
         
        }
     




       


    }
    catch (e) {
        showError("Bonus Informations are not yet updated!");
        ProgressBarHide();
    }
}

function MergeSalaryBonus() {

    if (!validateForm('.divPayrollForm'))
        return;

    if (!validateForm('.divBonus'))
        return;

    var Company = $(".ddlCompany").val();
    var DateOfPayroll = formatDate($('.txtMonth').val());

    var service = new HrmsSuiteHcmService.HcmService();
    service.MergeSalaryBonus($('.ddlCompany').val(), DateOfPayroll, onMergeSalaryBonus, null, null);
}

function onMergeSalaryBonus(result) {
    try {

    }
    catch (e) {
        showError("Bonus Informations are not yet merge!");
        ProgressBarHide();
    }
}

function ViewBonus() {
    if (!validateForm('.divPayrollForm'))
        return;

    //var Company = $(".ddlCompany").val();
    //var DateOfPayroll = formatDate($('.txtMonth').val());

    //ProgressBarShow();
    //var service = new HrmsSuiteHcmService.HcmService();
    //service.payrollBonusRelease(Company, DateOfPayroll, true, onGetBonusRelease, null, null);

    var Company = $(".ddlCompany").val();
    var BonusId = $(".ddlSeparateBonus").val();
   

    ProgressBarShow();
    var service = new HrmsSuiteHcmService.HcmService();
    service.payrollBonusRelease(Company, BonusId, false, onGetBonusRelease, null, null);
}
