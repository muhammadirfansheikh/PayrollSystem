function TriggerPageLoads() {
    $(".btnCancelSearch").click(function () {

        ClearReport();
    });

    //BindGroupByDDL();
    //$('.ddlGroupBy option').filter('[value="clsCompany"],[value="clsDesignation"],[value="clsDepartment"]').remove();
}

function GetEmployee() {
    var CompanyId = $('.ddlCompany').val();
    if (CompanyId > 0) {
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
        var GroupBy = "0";//$('.ddlGroupBy').val();
        ProgressBarShow();
        ClearReport();
        var service = new HrmsSuiteHcmService.HcmService();
        service.report_MasterSalary(EmployeeCode, GroupId, CompanyId, LocationId, BusinessUnitId, DepartmentId, CostCenterId, CategoryId, DesignationId, Firstname, Lastname, GroupBy, onget_report_MasterSalary, null, null);
    } else {
        showError('Please select Company');
    }
}

function onget_report_MasterSalary(result) {
    var res = jQuery.parseJSON(result);
    if (res.length > 0) {
        var header_count = 0;
        var headers = '<tr>';
        var data = '';
        var Total_NoOfColoumn_PerCell = 2;

        $(res).each(function (i, val) {
            var NoOfColoumn_th = 0;
            var NoOfColoumn_td = 0;
            data += '<tr>';
            $.each(val, function (k, v) {
                if (k.includes('_ColumnHide') == false) {
                    if (NoOfColoumn_th > Total_NoOfColoumn_PerCell) {
                        NoOfColoumn_th = 0;
                    }

                    if (i == 0) {
                        header_count = header_count + 1;
                        if (k == 'S.No' || k == 'Employee Code') {
                            headers += '<th style="text-align:left !important;">' + k + '</th>';
                        }
                        else {
                            if (NoOfColoumn_th == 0) {
                                headers += '<th style="text-align:left !important;">' + k + ',<br/>';
                            }
                            else if (NoOfColoumn_th == 1) {
                                headers += k + ',<br/>';
                            }
                            else if (NoOfColoumn_th == 2) {
                                headers += k + '</th>';
                            }
                            NoOfColoumn_th++;
                        }
                    }

                    if (NoOfColoumn_td > Total_NoOfColoumn_PerCell) {
                        NoOfColoumn_td = 0;
                    }
                    if (k == 'S.No' || k == 'Employee Code') {
                        data += '<td>' + replaceZeroAndNull(v) + '</td>';
                    }
                    else {
                        if (NoOfColoumn_td == 0) {
                            data += '<td>' + replaceZeroAndNull(v) + '<br/>';
                        }
                        else if (NoOfColoumn_td == 1) {
                            data += replaceZeroAndNull(v) + '<br/>';
                        }
                        else if (NoOfColoumn_td == 2) {
                            data += replaceZeroAndNull(v) + '</td>';
                        }
                        NoOfColoumn_td++;
                    }
                }
            });
            data += '</tr>';
        });
        headers += '</tr>';

        $('.theadMasterSalary').children().not('.clsReportH').hide();
        $('.tbodyMasterSalary').html('');
        $('.theadMasterSalary').append(headers);
        $('.tbodyMasterSalary').append(data);
        SetReportHeader('Master Salary', header_count, '');
        $('.div_reportbutton').show();
    }
    ProgressBarHide();
    //bindReport(res);
}

/*
function onget_report_MasterSalary(result) {
    var res = jQuery.parseJSON(result);
    if (res.length > 0) {
        var header_count = 0;
        var headers = '<tr>';
        var data = '';
        $(res).each(function (i, val) {
            data += '<tr>';
            $.each(val, function (k, v) {
                if (k.includes('_ColumnHide') == false) {
                    if (i == 0) {
                        header_count = header_count + 1;
                        headers += '<th>' + k + '</th>';
                    }
                    data += '<td>' + replaceZeroAndNull(v) + '</td>';
                }
            });
            data += '</tr>';
        });
        headers += '</tr>';
        $('.theadMasterSalary').children().not('.clsReportH').hide();
        $('.tbodyMasterSalary').html('');
        $('.theadMasterSalary').append(headers);
        $('.tbodyMasterSalary').append(data);
        SetReportHeader('Master Salary', header_count, '');
        $('.div_reportbutton').show();
    }
    ProgressBarHide();
    //bindReport(res);
}
*/
function ClearReport() {
    $('.clsReportH').hide();
    $('.HeaderColoumn').remove();
    $('.tbodyMasterSalary').html('');
    $('.div_reportbutton').hide();
    $('.clsDateH').hide();
    $('.clsDateH').remove();
}

function bindReport(res) {


    var headers = '';
    var data = '';
    var maxInc = 2;
    $(res[0]).each(function (i, val) {
        headers += '<tr class="info">';

        var incH = 0;
        $.each(val, function (k, v) {

            if (incH > maxInc) {
                incH = 0;
            }
            //headers += '<th>' + k + '</th>';

            if (k == 'SerialNo' || k == 'LocationName' || k == 'CostCenterName' || k == 'SapCostCenter' || k == 'DepartmentName' || k == 'EmployeeCode' || k == 'FirstName' || k == 'LastName' || k == 'DesignationName') {
                headers += '<th>' + k + '</th>';

            }
            else {
                if (incH == 0) {
                    headers += '<th>' + k + ' ,<br/>';
                }
                    //else if (incH == 1) {
                    //    headers += k + ' ,<br/>';
                    //}
                else if (incH == 1) {

                    headers += k + ' ,<br/>';

                } else if (incH == 2) {

                    headers += k + '</th>';

                }
                incH++;
            }

        });
        headers += '</tr>';
    });

    $(res).each(function (i, val) {

        data += '<tr>';

        var inc = 0;
        $.each(val, function (k, v) {

            if (inc > maxInc) {
                inc = 0;
            }

            //data += '<td>' + replaceZeroAndNull(v) + '</td>';

            if (k == 'SerialNo' || k == 'LocationName' || k == 'CostCenterName' || k == 'SapCostCenter' || k == 'DepartmentName' || k == 'EmployeeCode' || k == 'FirstName' || k == 'LastName' || k == 'DesignationName') {
                data += '<td>' + replaceZeroAndNull(v) + '</td>';
            }
            else {

                if (inc == 0) {
                    data += '<td>' + replaceZeroAndNull(v) + ' <br/>';
                }
                    //else if (inc == 1) {
                    //    data += replaceZeroAndNull(v) + ' <br/>';
                    //}
                else if (inc == 1) {

                    data += replaceZeroAndNull(v) + ' <br/>';

                } else if (inc == 2) {

                    data += replaceZeroAndNull(v) + '</td>';

                }
                inc++;
            }
        });

        data += '</tr>';
    });


    $('.theadMasterSalary').children().not('.clsReportH').hide();
    $('.tbodyMasterSalary').html('');

    $('.theadMasterSalary').append(headers);
    $('.tbodyMasterSalary').append(data);

    ProgressBarHide();

    SetReportHeader('Master Salary', 15, '');
    $('.clsDateH').hide();
    $('.clsDateH').remove();
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

