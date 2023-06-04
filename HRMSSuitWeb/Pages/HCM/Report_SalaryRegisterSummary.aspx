<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_SalaryRegisterSummary.aspx.cs" Inherits="Pages_HCM_Report_SalaryRegisterSummary" %>


<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader"%>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <uc:EmployeeSearchFilters runat="server" ID="EmployeeSearchFilters"  EmployeeCode="1"/>

    <div class="panel panel-info">
        <div class="panel-heading">
            Report Functions
        </div>
        <div class="panel-body">
            <div class="col-lg-2 divMonthPayroll">
                <label>Select Month</label>
                <input type="text" class="form-control txtMonthOfPayroll DatePickerMonthComplete" />
            </div>
            <div class="col-lg-12">

                <input type="button" class="btn btn-info pull-right" value="Export Summary" onclick="excelOutWithouHiddenFields('.divSummary')" />

                <input type="button" class="btn btn-primary pull-right" value="Print Summary" onclick="sendPrint('.divSummary')" />
            </div>
        </div>
    </div>

    <div class="divSummary">
        <table class="table table-responsive">
            <uc:ReportHeader runat="server" ID="ReportHeader" />
        </table>
        <div class="col-lg-6">
            <div class="panel panel-default">

                <div class="panel-heading">
                    Total Allowances
                </div>
                <div class="panel-body">

                    <table class="table table-responsive">

                        <tbody class="tbodyAllowances">
                        </tbody>
                        <tfoot>
                            <tr class="info">
                                <td>Total</td>
                                <td class="tdTotal">0</td>
                            </tr>
                            <tr class="danger">
                                <td>Proportionate Recovery</td>
                                <td class="tdPropRecovery">0</td>
                            </tr>
                            <tr class="success">
                                <td>Total Master</td>
                                <td class="tdTotalMaster">0</td>
                            </tr>
                            <tr class="success">
                                <td>Basic Proportionate</td>
                                <td class="tdFlexiSum">0</td>
                            </tr>
                            <tr class="info" style="display:none;">
                                <td>E.O.B.I</td>
                                <td class="tdEOBI">0</td>
                            </tr>

                        </tfoot>
                    </table>
                </div>
            </div>
        </div>

        <div class="col-lg-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Total Deductions
                </div>
                <div class="panel-body">
                    <table class="table table-responsive">
                        <tbody class="tbodyDeduction">
                        </tbody>
                        <tfoot>
                            <tr class="info">
                                <td>Total</td>
                                <td class="tdTotalDeduction">0</td>
                            </tr>
                            <tr class="success">
                                <td>Net Salary</td>
                                <td class="tdNetSalary">0</td>
                            </tr>
                        </tfoot>
                    </table>
                </div>
            </div>
        </div>

    </div>


    <script src="../../js/Page_JS/Report_SalaryRegisterSummary.js"></script>

    <script type="text/x-jQuery-tmpl" id="Allowances">
        <tr>
            <td class="project-title">${AllowanceName}</td>
            <td class="project-title AllowanceAmount">${TotalAllowanceAmount} ${NoEmployees != null ? 'For Employees ( '+ NoEmployees +' )' : ''} </td>
        </tr>
    </script>

    <script type="text/x-jQuery-tmpl" id="Deduction">
        <tr>
            <td class="project-title">${AllowanceName}</td>
            <td class="project-title DeductionAmount">${TotalAllowanceAmount} ${NoEmployees != null ? 'For Employees ( '+ NoEmployees +' )' : ''}</td>
        </tr>
    </script>


</asp:Content>

