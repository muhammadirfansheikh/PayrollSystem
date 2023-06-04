<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_ProvidentFundStatement.aspx.cs" Inherits="Pages_HCM_Report_ProvidentFundStatement" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>

    <style>
        .clsBold {
            font-weight: bold;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="PF Statement" />
            </h2>
            <ol class="breadcrumb">
                <li>
                    <a href="/Pages/Default.aspx">Dashboard</a>
                </li>
                <li>
                    <a href="#">HCM Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="PF Statement" />
                    </strong>
                </li>
            </ol>
        </div>
    </div>

    <uc:EmployeeSearchFilters runat="server" ID="EmployeeSearchFilters" EmployeeCode="1" />

    <div class="panel panel-info">
        <div class="panel-heading">
            Report Functions
        </div>
        <div class="panel-body">
            <div class="col-lg-2 divMonthPayroll">
                <label>As On Month</label>
                <input type="text" class="form-control txtMonthOfPayroll DatePickerMonthComplete" />
            </div>
            <div class="col-lg-10 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelOutWithouHiddenFields('.SalarySlips')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.SalarySlips')" />
            </div>
        </div>
    </div>

    <div class="panel panel-info">
        <div class="panel-heading">
            PF Statement
        </div>
        <div class="panel-body PFStatements">
        </div>
    </div>

    <script type="text/x-jQuery-tmpl" id="PFStatements">
        <div class="col-lg-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    PF Statement
                </div>
                <div class="panel-body" style="background-color: white;">
                    <table class="table tableSalarySlip table-responsive table-hover">
                        <thead>
                            <tr class="success">
                                <th>Company Name</th>
                                <th>${CompanyName} - ${LOCATION}</th>
                                <th colspan="6"></th>
                            </tr>

                            <tr class="success">
                                <th>Report Name:</th>
                                <th>Employees Contributory Provident Fund</th>
                                <th colspan="6"></th>
                            </tr>

                            <tr class="success">
                                <th>For The Month</th>
                                <th>${formatDate($('.txtMonthOfPayroll').val())}</th>
                                <th colspan="6"></th>
                            </tr>

                            <tr class="warning">
                                <th>Employee Code:</th>
                                <th>${EmployeeCode}</th>
                                <th>Employee Name:</th>
                                <th>${NAME}</th>
                                <th>Designation:</th>
                                <th>${Designation}</th>
                                <th colspan="2"></th>
                            </tr>

                            <tr class="info">
                                <th colspan="2">Month</th>
                                <th colspan="2">Employees Contribution</th>
                                <th colspan="2">Company Contribution</th>
                                <th colspan="2">Total Contribution</th>
                            </tr>

                            <%--<tr class="success">
                                <th>Balance CF</th>
                                <th colspan="5">${CF}</th>
                            </tr>--%>
                        </thead>


                        <tbody>

                            <tr class="clsBold">
                                <td class="Balance_CF" colspan="6">Balance CF   
                                </td>
                                <td>${CF}
                                </td>
                            </tr>
                            {{html SlipData}}

                        </tbody>

                        <tfoot>
                            <%--  <tr class="info">
                                <th>Total Gross: </th>
                                <th class="tdTotalAllowances"></th>
                                <th>Total Deductions:</th>
                                <th class="tdTotalDeduction"></th>
                                <th>Net Salary:</th>
                                <th class="tdNetSalary"></th>
                            </tr>
                            <tr class="warning">
                                <th>Bank Name:</th>
                                <th>${BankName}</th>
                                <th>Branch Name:</th>
                                <th>${BankDescription}</th>
                                <th>Account Number:</th>
                                <th>${AccountNumber}</th>
                            </tr>
                            <tr>
                                <th colspan="6"></th>
                            </tr>
                            <tr>
                                <th colspan="6"></th>
                            </tr>--%>
                        </tfoot>
                    </table>
                </div>
            </div>
        </div>
    </script>
    <script src="../../js/Page_JS/Report_ProvidentFundStatement.js"></script>

</asp:Content>

