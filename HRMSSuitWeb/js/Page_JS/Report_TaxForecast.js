function TriggerPageLoads() {
    $(".ddlCompany").change(function () {
        GetTaxYear();
    });

    $(".btnCancelSearch").click(function () {
        $(".chkAll").prop("checked", false);
        FillDropDownByReference(".ddlTaxYear", null);
        ClearReport();
    });
}

function GetTaxYear() {
    ProgressBarShow();
    var service = new HrmsSuiteHcmService.HcmService();
    service.getTaxYear($('.ddlCompany').val(), onGetTaxYear, null, null);
}

function onGetTaxYear(result) {
    var res = jQuery.parseJSON(result);
    FillDropDownByReference(".ddlTaxYear", res);
    if (res.length > 0) {
        $(".ddlTaxYear").val(res[0].Id);
    }
    ProgressBarHide();
}

function GetEmployee() {

    var CompanyId = $('.ddlCompany').val();
    if (CompanyId > 0) {
        if (!validateForm('.divMonthPayroll'))
            return;
        var CompanyDiv = $('.ddlCompany').closest('div');
        if (!validateForm(CompanyDiv))
            return;

        var YearId = $('.ddlTaxYear').val();

        /*var EmployeeCode = $('.txtEmployeeCode').val() == '' ? 0 : $('.txtEmployeeCode').val();*/
        var GroupId = $('.ddlGroup').val();

        var LocationId = $('.ddlLocation').val();
        var BusinessUnitId = $('.ddlBU').val();
        var DepartmentId = $('.ddlDepartment').val();
        var CostCenterId = $('.ddlCostCenter').val();
        var CategoryId = $('.ddlCategoryC').val();
        var DesignationId = $('.ddlDesignation').val();
        var Firstname = $('.txtFirstName').val();
        var Lastname = $('.txtLastName').val();
        var IsAll = $(".chkAll").prop('checked');

        ProgressBarShow();
        ClearReport();
        var service = new HrmsSuiteHcmService.HcmService();
        service.report_TaxForecast( GroupId, CompanyId, LocationId, BusinessUnitId, DepartmentId, CostCenterId, CategoryId, DesignationId, Firstname, Lastname, YearId, IsAll, onGetTaxForecast, null, null);
    } else {
        showError('Please select Company');
    }
}

function onGetTaxForecast(result) {
    var res = jQuery.parseJSON(result);
    var headers = '';
    var data = '';
    var maxInc = 3;
    $(res[0]).each(function (i, val) {
        headers += '<tr class="info">';
        var incH = 0;
        $.each(val, function (k, v) {
            if (k.includes('_ColumnHide') == false) {
                if (incH > maxInc) {
                    incH = 0;
                }
                if (k == 'Sno' || k == 'LocationName' || k == 'EmployeeCode' || k == 'FirstName' || k == 'LastName' || k == 'DesignationName') {
                    headers += '<th>' + k + '</th>';

                }
                else {
                    if (incH == 0) {
                        headers += '<th>' + k + ' ,<br/>';
                    }
                    else if (incH == 1) {
                        headers += k + ' ,<br/>';
                    }
                    else if (incH == 2) {

                        headers += k + ' ,<br/>';

                    } else if (incH == 3) {

                        headers += k + '</th>';

                    }
                    incH++;
                }
            }
        });
        headers += '</tr>';
    });
    $(res).each(function (i, val) {

        data += '<tr>';

        var inc = 0;
        $.each(val, function (k, v) {
            if (k.includes('_ColumnHide') == false) {
                if (inc > maxInc) {
                    inc = 0;
                }
                if (k == 'Sno' || k == 'LocationName' || k == 'EmployeeCode' || k == 'FirstName' || k == 'LastName' || k == 'DesignationName') {
                    data += '<td>' + replaceZeroAndNull(v) + '</td>';
                }
                else {

                    if (inc == 0) {
                        data += '<td>' + replaceZeroAndNull(v) + ' <br/>';
                    }
                    else if (inc == 1) {
                        data += replaceZeroAndNull(v) + ' <br/>';
                    }
                    else if (inc == 2) {

                        data += replaceZeroAndNull(v) + ' <br/>';

                    } else if (inc == 3) {

                        data += replaceZeroAndNull(v) + '</td>';

                    }
                    inc++;
                }
            }
        });

        data += '</tr>';
    });
    $('.theadtaxforecast').children().not('.clsReportH').hide();
    $('.tbodytaxforecast').html('');
    $(".clsDateH").remove();
    SetReportHeader('Tax Forecast Report', 15, 'Year : ' + $('.ddlTaxYear option:selected').text());
    $('.theadtaxforecast').append(headers);
    $('.tbodytaxforecast').append(data);
    $('.div_reportbutton').show();
    ProgressBarHide();
}

function replaceZeroAndNull(val) {
    if (val == "0" || val == null)
        return "-";
    else {
        if ($.isNumeric(val))
            return parseInt(val);
        else
            return val;
    }
}

function ClearReport() {
    $('.clsReportH').hide();
    $('.tbodytaxforecast').html('');
    $('.div_reportbutton').hide();
}
