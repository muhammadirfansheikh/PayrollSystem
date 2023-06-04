<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_SalaryRegister.aspx.cs" Inherits="Pages_HCM_Report_SalaryRegister" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
    <link href="/css/print.css" rel="stylesheet" />
    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
        <input type="hidden" runat="server" id="hf_IsSummary" class="hf_IsSummary" value="0" />
        <div class="row wrapper border-bottom white-bg page-heading">
            <div class="col-lg-10">
                <h2>
                    <asp:Label runat="server" ID="lbl1" Text="Salary Register & Summary Report" />
                </h2>
                <ol class="breadcrumb">
                  
                    <li>
                        <a href="#">HCM Reports</a>
                    </li>
                    <li class="active">
                        <strong>
                            <asp:Label runat="server" ID="lbl2" Text="Salary Register & Summary Report" />
                        </strong>
                    </li>
                </ol>
            </div>
        </div>

        <uc:EmployeeSearchFilters runat="server" ID="EmployeeSearchFilters" />

        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        Report Functions
                    </div>
                    <div class="panel-body">
                        <div class="col-lg-2 divMonthPayroll">
                            <label>Select Month</label>
                            <input type="text" class="form-control txtMonthOfPayroll DatePickerMonthComplete" />
                        </div>

                        <div class="col-lg-2 div_GroupBy">
                            <label>Select </label>
                            <select class="form-control ddlGroupBy"></select>
                        </div>

                        <div class="col-lg-8 div_reportbutton" style="margin-top: 23px; display: none;" runat="server" id="div_reportbutton">
                            <input type="button" class="btn btn-info pull-right btn_ExportRegister" value="Export Register" onclick="excelExportWithFileSaver('tableSalaryRegister','SalaryRegister')" />
                            <input type="button" class="btn btn-info pull-right m-r-sm btnExport" value="Export Summary" onclick="excelOutWithouHiddenFields('.divSummary')" />
                            <input type="button" class="btn btn-primary pull-right m-r-sm btn_PrintRegister" value="Print Register" onclick="sendPrint('.tableSalaryRegister')" />
                            <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print Summary" onclick="sendPrint('.divSummary')" />
                        </div>
                    </div>
                </div>

                <div class="panel panel-info SalaryRegister">
                    <div class="panel-heading">
                        Salary Register
                    </div>
                    <div class="table-responsive">
                        <div id="table-scroll">
                            <table class="table table-hover tableSalaryRegister" id="tableSalaryRegister">
                                <thead class="theadSalaryRegister">
                                    <uc:ReportHeader runat="server" ID="ReportHeader2" />
                                </thead>
                                <tbody class="tbodySalaryRegister">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>

                <div class="divSummary" style="display: none;">
                    <table class="table table-responsive">
                        <uc:ReportHeader runat="server" ID="ReportHeader1" />
                    </table>
                    <div class="col-lg-6">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4>Total Allowances</h4>
                            </div>

                            <div class="panel-body" style="min-height: 196px;">
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
                                        <tr class="info" style="display: none;">
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
                                <h4>Total Deductions</h4>
                            </div>
                            <div class="panel-body" style="min-height: 196px;">
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
            </div>
        </div>

        <script src="../../js/Page_JS/Report_SalaryRegister.js"></script>

        <script type="text/x-jQuery-tmpl" id="Allowances">
            <tr>
                <td class="project-title">${AllowanceName}</td>
                <td class="project-title AllowanceAmount">${AllowanceAmount}</td>
            </tr>
        </script>

        <script type="text/x-jQuery-tmpl" id="Deduction">
            <tr>
                <td class="project-title">${AllowanceName}</td>
                <td class="project-title DeductionAmount">${AllowanceAmount}</td>
            </tr>
        </script>

        <style>
            .hide {
                display: none;
            }
        </style>
</asp:Content>



