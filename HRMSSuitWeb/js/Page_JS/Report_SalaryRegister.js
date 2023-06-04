function TriggerPageLoads() {

    ////$(".btnExport").click(function (e) {
    ////    let file = new Blob([$('.divSummary').html()], { type: "application/vnd.ms-excel" });
    ////    let url = URL.createObjectURL(file);
    ////    let a = $("<a />", {
    ////        href: url,
    ////        download: "filename.xls"
    ////    }).appendTo("body").get(0).click();
    ////    e.preventDefault();
    ////});

    if ($('.hf_IsSummary').val() == "1") {
        $('.SalaryRegister').hide();
        $('.btn_ExportRegister').hide();
        $('.btn_PrintRegister').hide();
        $('.div_GroupBy').hide();
    }

    $(".btnCancelSearch").click(function () {
        $('.txtMonthOfPayroll').datepicker('setDate', null);
        $('.ddlGroupBy').val('0');
        ClearReport();
    });
    BindGroupByDDL();
    $('.ddlGroupBy option').filter('[value="clsCompany"],[value="clsDesignation"],[value="clsBankName"]').remove();
}

function GetEmployee() {
    ;
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
        var GroupBy = $('.ddlGroupBy').val();
        ClearReport();
        ProgressBarShow();
        var service = new HrmsSuiteHcmService.HcmService();
        service.report_SalaryRegister(EmployeeCode, GroupId, CompanyId, LocationId, BusinessUnitId, DepartmentId, CostCenterId, CategoryId, DesignationId, Firstname, Lastname, PayrollMonth, GroupBy, onGetReportSalaryRegister, null, null);
    }
    else {
        showError('Please select Company');
    }
}

function onGetReportSalaryRegister(result) {

    /*
    var res = jQuery.parseJSON(result[0]);
    var res2 = jQuery.parseJSON(result[1]);
    var res3 = jQuery.parseJSON(result[2]);
    var resMaster = jQuery.parseJSON(result[3]);

    var divTbodyGoalFund = $('.tbodyAllowances').html('');
    $('#Allowances').tmpl(res2).appendTo(divTbodyGoalFund);

    var divTbodyGoalFund = $('.tbodyDeduction').html('');
    $('#Deduction').tmpl(res3).appendTo(divTbodyGoalFund);

    var AllowanceTotal = 0;
    var DeductionTotal = 0;
    var MasterTotal = 0;

    $('.AllowanceAmount').each(function () {
        AllowanceTotal += parseFloat($(this).text().trim());
    });

    $('.DeductionAmount').each(function () {
        DeductionTotal += parseFloat($(this).text().trim());
    });

    var PropotionateCount = 0;
    $(resMaster).each(function (k, v) {
        MasterTotal += (v.DifferenceAmount);
        PropotionateCount++;
    });

    $('.tdTotal').text(AllowanceTotal);
    $('.tdTotalDeduction').text(DeductionTotal);
    //$('.tdPropRecovery').text(MasterTotal + ' For Employees ( ' + PropotionateCount + ' )');
    //$('.tdPropRecovery').text(MasterTotal);

    if (PropotionateCount > 0) {
        $('.tdPropRecovery').text((parseFloat(MasterTotal) - parseFloat(res2[0].FlexiSum)));
    }
    $('.tdTotalMaster').text(res2[0].MasterGross);
    $('.tdNetSalary').text(AllowanceTotal - DeductionTotal);
    //$('.tdFlexiSum').text(res2[0].FlexiSum + ' For Employees ( ' + res2[0].FlexiEmpCount + ' )')
    //;
    if (res2[0].FlexiSum == 'undefined') {
        $('.tdFlexiSum').text('0');
    }
    else {
        $('.tdFlexiSum').text(res2[0].FlexiSum);
    }

    $('.tdEOBI').text(res2[0].EOBI + ' For Employees ( ' + res2[0].EOBIEmployee + ' )');
    bindReport(res);
    */
    Bind_Report_SalaryRegister(result);
    ProgressBarHide();
}

function Bind_Report_SalaryRegister(result_) {
    ;
    if (result_ != "") {
        var result = result_.split("#SPLIT#");
        var res1 = jQuery.parseJSON(result[0]);
        var res2 = jQuery.parseJSON(result[1]);
        if ($('.hf_IsSummary').val() == "0") {
            var headers = '<tr class="info HeaderColoumn">';
            var data = '';
            var Total_NoOfColoumn_PerCell = 3;
            $(res1).each(function (i, val) {
                var tr = '';
                var IsFooter = false;
                var NoOfColoumn_th = 0;
                var NoOfColoumn_td = 0;

                $.each(val, function (k, v) {
                    var values = v;
                    if (NoOfColoumn_th > Total_NoOfColoumn_PerCell) {
                        NoOfColoumn_th = 0;
                    }
                    if (i == 0) {
                        //headers += '<th>' + k + '</th>';
                        if (k == 'Emp #') {
                            headers += '<th>' + k + '</th>';
                        }
                        else if (k == 'NetSalary') {
                            headers += '<th>' + k + '</th>';
                        }
                        else {
                            if (NoOfColoumn_th == 0) {
                                headers += '<th>' + k + ',<br/>';
                            }
                            else if (NoOfColoumn_th == 1 || NoOfColoumn_th == 2) {
                                headers += k + ',<br/>';
                            }
                            else if (NoOfColoumn_th == 3) {
                                headers += k + '</th>';
                            }
                            NoOfColoumn_th++;
                        }
                    }

                    if (NoOfColoumn_td > Total_NoOfColoumn_PerCell) {
                        NoOfColoumn_td = 0;
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

                    //if (IsFooter == true) {
                    //    if ($.isNumeric(values)) {
                    //        data += '<th style="text-align:right;">' + replaceZeroAndNull(values) + '</th>';
                    //    } else {
                    //        data += '<th style="text-align:left;">' + replaceZeroAndNull(values) + '</th>';
                    //    }
                    //} else {
                    //    data += '<td>' + replaceZeroAndNull(values) + '</td>';
                    //}

                    if (k == 'Emp #') {
                        if (IsFooter == true) {
                            data += '<th>' + replaceZeroAndNull(values) + '</th>';
                        }
                        else {
                            data += '<td>' + replaceZeroAndNull(values) + '</td>';
                        }
                    } else if (k == 'NetSalary') {
                        if (IsFooter == true) {
                            data += '<th>' + replaceZeroAndNull(values) + '</th>';
                        }
                        else {
                            data += '<td>' + replaceZeroAndNull(values) + '</td>';
                        }
                    }
                    else {
                        if (NoOfColoumn_td == 0) {
                            if (IsFooter == true) {
                                data += '<th>' + replaceZeroAndNull(values) + '<br/>';
                            }
                            else {
                                data += '<td>' + replaceZeroAndNull(values) + '<br/>';
                            }
                        }
                        else if (NoOfColoumn_td == 1 || NoOfColoumn_td == 2) {
                            data += replaceZeroAndNull(values) + '<br/>';
                        }
                        else if (NoOfColoumn_td == 3) {
                            if (IsFooter == true) {
                                data += replaceZeroAndNull(values) + '</th>';
                            }
                            else {
                                data += replaceZeroAndNull(values) + '</td>';
                            }
                        }
                        NoOfColoumn_td++;
                    }
                });
                data += '</tr>';
            });
            headers += '</tr>';
            $('.theadSalaryRegister').append(headers);
            $('.tbodySalaryRegister').append(data);
        }
        var Allowances = [];
        var Deductions = [];
        $(res2).each(function () {
            var Info = {
                AllowanceName: this.AllowanceName,
                AllowanceAmount: this.AllowanceAmount,
            }
            if (this.IsDeduction == false) {
                Allowances.push(Info);
            }
            else {
                Deductions.push(Info);
            }
        })
        var tbodyAllowances = $('.tbodyAllowances').html('');
        $('#Allowances').tmpl(Allowances).appendTo(tbodyAllowances);
        var tbodyDeduction = $('.tbodyDeduction').html('');
        $('#Deduction').tmpl(Deductions).appendTo(tbodyDeduction);
        $('.tdTotal').text(res2[0].TotalAllowance);
        $('.tdPropRecovery').text(res2[0].ProportionateRecovery);
        $('.tdTotalMaster').text(res2[0].TotalMaster);
        $('.tdFlexiSum').text(res2[0].BasicProportionate);
        $('.tdTotalDeduction').text(res2[0].TotalDeduction);
        $('.tdNetSalary').text(res2[0].NetSalary);
        $('.div_reportbutton').show();
        $('.divSummary').show();

        if ($('.hf_IsSummary').val() == "0") {
            SetReportHeader('Salary Register Report', 15, '');
        }
        else {
            $('.btn_ExportRegister').hide();
            $('.btn_PrintRegister').hide();
            SetReportHeader('Salary Register Summary Report', 15, '');
        }
    }
}

/*
function Bind_Report_SalaryRegister(result_) {
    if (result_ != "") {
        var result = result_.split("#SPLIT#");
        var res1 = jQuery.parseJSON(result[0]);
        var res2 = jQuery.parseJSON(result[1]);
        if ($('.hf_IsSummary').val() == "0") {
            var headers = '<tr class="info HeaderColoumn">';
            var data = '';
            $(res1).each(function (i, val) {
                var tr = '';
                var IsFooter = false;
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
                        if ($.isNumeric(values)) {
                            data += '<th style="text-align:right;">' + replaceZeroAndNull(values) + '</th>';
                        } else {
                            data += '<th style="text-align:left;">' + replaceZeroAndNull(values) + '</th>';
                        }
                    } else {

                        data += '<td>' + replaceZeroAndNull(values) + '</td>';
                    }
                });
                data += '</tr>';
            });
            headers += '</tr>';
            $('.theadSalaryRegister').append(headers);
            $('.tbodySalaryRegister').append(data);

        }
        var Allowances = [];
        var Deductions = [];
        $(res2).each(function () {
            var Info = {
                AllowanceName: this.AllowanceName,
                AllowanceAmount: this.AllowanceAmount,
            }
            if (this.IsDeduction == false) {
                Allowances.push(Info);
            }
            else {
                Deductions.push(Info);
            }
        })
        var tbodyAllowances = $('.tbodyAllowances').html('');
        $('#Allowances').tmpl(Allowances).appendTo(tbodyAllowances);
        var tbodyDeduction = $('.tbodyDeduction').html('');
        $('#Deduction').tmpl(Deductions).appendTo(tbodyDeduction);
        $('.tdTotal').text(res2[0].TotalAllowance);
        $('.tdPropRecovery').text(res2[0].ProportionateRecovery);
        $('.tdTotalMaster').text(res2[0].TotalMaster);
        $('.tdFlexiSum').text(res2[0].BasicProportionate);
        $('.tdTotalDeduction').text(res2[0].TotalDeduction);
        $('.tdNetSalary').text(res2[0].NetSalary);
        $('.div_reportbutton').show();
        $('.divSummary').show();

        if ($('.hf_IsSummary').val() == "0") {
            SetReportHeader('Salary Register Report', 15, '');
        }
        else {
            $('.btn_ExportRegister').hide();
            $('.btn_PrintRegister').hide();
            SetReportHeader('Salary Register Summary Report', 15, '');
        }
    }
}

*/

function ClearReport() {
    $('.clsReportH').hide();
    $('.HeaderColoumn').remove();
    $('.tbodySalaryRegister').html('');
    $('.tbodyAllowances').html('');
    $('.tbodyDeduction').html('');
    $('.div_reportbutton').hide();
    $('.divSummary').hide();
    $('.tdTotal').text('0');
    $('.tdPropRecovery').text('0');
    $('.tdTotalMaster').text('0');
    $('.tdFlexiSum').text('0');
    $('.tdTotalDeduction').text('0');
    $('.tdNetSalary').text('0');
}

function bindReport1(res) {
    var headers = '';
    var data = '';
    var _Counter = 0;
    var _Counter1 = 0;

    var GroupByValue = $('.ddlGroupBy').val();

    $(res[0]).each(function (i, val) {
        headers += '<tr class="info">';
        $.each(val, function (k, v) {

            headers += '<th>' + k + '</th>';

            //if (_Counter == 1) 
            //{
            //    headers += '<th>Location</th>';
            //}
            //else if (_Counter == 2) {
            //    headers += '<th>Cost Center</th>';
            //}
            //else if(_Counter == 3) {
            //    headers += '<th>Sap Cost Center</th>';
            //}
            //else {
            //    headers += '<th>' + k + '</th>';
            //}

            _Counter++;
        });
        headers += '</tr>';

    });

    if (GroupByValue != 0) {
        res = res.sort(sortByProperty(GroupByValue));

        var grouped = _.groupBy(res, function (car) {

            if (GroupByValue == 'clsLocation') {
                return car.clsLocation;
            }
            else if (GroupByValue == 'clsDepartment') {
                return car.clsCostCenter;
            }
            else if (GroupByValue == 'clsCostCenter') {
                return car.clsCostCenter;
            }
            else if (GroupByValue == 'clsSapCostCenter') {
                return car.clsSapCostCenter;
            }
        }); 

        $(grouped).each(function (i, val) {
            alert();
            $(val).each(function (ii, vval) {



            });

            //data += '<tr class=trList>';

            //$.each(val, function (k, v) {

            //    data += '<td>' + replaceZeroAndNull(v) + '</td>';
            //});

            //data += '</tr>';
        });
    }
    else {
        $(res).each(function (i, val) {

            data += '<tr class=trList>';

            $.each(val, function (k, v) {

                data += '<td>' + replaceZeroAndNull(v) + '</td>';
            });

            data += '</tr>';

            _Counter1++;
        });


    }

    $('.theadSalaryRegister').children().not('.clsReportH').hide();
    //$('.theadSalaryRegister').html('');
    $('.tbodySalaryRegister').html('');

    $('.theadSalaryRegister').append(headers);
    $('.tbodySalaryRegister').append(data);

    SetReportHeader('Salary Register Report', 15, '');
    SetReportHeader('Salary Register Report', 15, '');

    ProgressBarHide();
}

function bindReport2(res) {

    var GroupByValue = $('.ddlGroupBy').val();

    if (GroupByValue != 0) {
        res = res.sort(sortByProperty(GroupByValue));
    }

    var headers = '';
    var data = '';
    $(res[0]).each(function (i, val) {
        headers += '<tr class="info">';
        $.each(val, function (k, v) {
            headers += '<th>' + k + '</th>';
        });
        headers += '</tr>';
    });

    $(res).each(function (i, val) {

        data += '<tr>';

        $.each(val, function (k, v) {

            var HF = '';
            if (k == 1) {
                HF = HF + '<input class="clsLocation" type="hidden" value=' + v + ' />';
            }
            else if (k == 2) {
                '<input class="clsDepartment" type="hidden" value=' + v + ' />';
            }
            else if (k == 3) {
                '<input class="clsCostCenter" type="hidden" value=' + v + ' />';
            }
            else if (k == 4) {
                '<input class="clsSapCostCenter" type="hidden" value=' + v + ' />';
            }
            data += '<td>' + replaceZeroAndNull(v) + '</td>';
        });

        data += '</tr>';
    });

    $('.theadSalaryRegister').children().not('.clsReportH').hide();
    //$('.theadSalaryRegister').html('');
    $('.tbodySalaryRegister').html('');

    $('.theadSalaryRegister').append(headers);
    $('.tbodySalaryRegister').append(data);

    SetReportHeader('Salary Register Report', 15, '');
    SetReportHeader('Salary Register Report', 15, '');

    var Prev = 0;
    var i = 0;
    var _Sum1 = 0, _Sum2 = 0, _Sum3 = 0, _Sum4 = 0, _Sum5 = 0, _Sum6 = 0, _Sum7 = 0;
    var GroupBy = '.' + $('.ddlGroupBy').val();
    var GroupByName = $(".ddlGroupBy option:selected").text();
    var ColSpan = $('.clsMonthlyCont').index();

    $('.trList').each(function () {

        if (GroupByValue != 0) {

            //var CurrLocId = $(this).closest('td').find(GroupBy).text();

            var CurrLocId = $(this).find('.ABC').find(GroupBy).val();

            if ($(this).is(':first-child')) {

            }
            else {

                if (Prev == CurrLocId) {

                }
                else {

                    var GroupByItem = $(this).prev().find('.ABC').find(GroupBy).val();

                    $("<tr class='success XYZ'><th colspan=" + ColSpan + ">" + GroupByName + " : " + GroupByItem + "</th></tr>").insertBefore($(this));
                    i = -1;

                }

                if ($(this).is(':last-child')) {

                    var GroupByItem = $(this).find('.ABC').find(GroupBy).val();

                    $("<tr class='success XYZ'><th colspan=" + ColSpan + ">" + GroupByName + " : " + GroupByItem + "</th></tr>").insertAfter($(this));
                    i = -1;
                    _Sum1 = 0;
                }
            }

            Prev = CurrLocId;
            i++;
        }
        else {
            var divTbodyGoalFund = $('.ProvidentFundSummary').html('');
            $('#ProvidentFundSummary').tmpl(res).appendTo(divTbodyGoalFund);
        }

    });


    ProgressBarHide();
}

function bindReport(res) {
    ;
    var headers = '';
    var data = '';
    $(res[0]).each(function (i, val) {
        headers += '<tr class="info">';
        var incH = 0;
        $.each(val, function (k, v) {
            if (incH > 3) {
                incH = 0;
            }

            if (k == 'SerialNo' || k == 'clsLocation' || k == 'clsDepartment' || k == 'clsCostCenter' || k == 'clsSapCostCenter' || k == 'EmployeeCode' || k == 'EmployeeName') {
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
            //;
            //headers += '<th>' + k + '</th>';
        });
        headers += '</tr>';
    });

    $(res).each(function (i, val) {

        data += '<tr>';

        var inc = 0;
        $.each(val, function (k, v) {


            if (inc > 3) {
                inc = 0;
            }

            if (k == 'SerialNo' || k == 'clsLocation' || k == 'clsDepartment' || k == 'clsCostCenter' || k == 'clsSapCostCenter' || k == 'EmployeeCode' || k == 'EmployeeName') {
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
        });

        data += '</tr>';
    });

    $('.theadSalaryRegister').children().not('.clsReportH').hide();
    $('.tbodySalaryRegister').html('');
    $('.theadSalaryRegister').append(headers);
    $('.tbodySalaryRegister').append(data);
    SetReportHeader('Salary Register Report', 15, '');
}

function bindReport3(res) {
    var headers = '';
    var data = '';
    $(res[0]).each(function (i, val) {
        headers += '<tr class="info">';

        var incH = 0;
        $.each(val, function (k, v) {

            if (incH > 3) {
                incH = 0;
            }

            if (k == 'SerialNo' || k == 'clsLocation' || k == 'clsDepartment' || k == 'clsCostCenter' || k == 'clsSapCostCenter' || k == 'EmployeeCode' || k == 'EmployeeName') {
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
            //;
            //headers += '<th>' + k + '</th>';
        });
        headers += '</tr>';
    });

    $(res).each(function (i, val) {

        data += '<tr>';

        var inc = 0;
        $.each(val, function (k, v) {


            if (inc > 3) {
                inc = 0;
            }

            if (k == 'SerialNo' || k == 'clsLocation' || k == 'clsDepartment' || k == 'clsCostCenter' || k == 'clsSapCostCenter' || k == 'EmployeeCode' || k == 'EmployeeName') {
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
        });

        data += '</tr>';
    });

    $('.theadSalaryRegister').children().not('.clsReportH').hide();
    //$('.theadSalaryRegister').html('');
    $('.tbodySalaryRegister').html('');

    $('.theadSalaryRegister').append(headers);
    $('.tbodySalaryRegister').append(data);

    //SetReportHeader('Salary Register Report', 15, '');
    //SetReportHeader('Salary Register Report', 15, '');


    SetReportHeader('Salary Register Report', 15, '');
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



