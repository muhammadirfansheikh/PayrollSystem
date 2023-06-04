<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_PfLoanClosingBalance.aspx.cs" Inherits="Pages_HCM_Report_PfLoanClosingBalance" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="PF Loan Closing" />
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
                        <asp:Label runat="server" ID="lbl2" Text="PF Loan Closing" />
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
                <label>Select Month</label>
                <input type="text" class="form-control txtMonthOfPayroll DatePickerMonthComplete" />
            </div>
            <div class="col-lg-10 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelOutWithouHiddenFields('.tablePfLoanClosingBalanceListing')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tablePfLoanClosingBalanceListing')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            PF Loan Closing
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tablePfLoanClosingBalanceListing">
                <thead class="theadpropotionatelisting">
                    <tr>
                        <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    </tr>
                    <tr class="info">
                        <th>Sr No.</th>
                        <th>Employee Code</th>
                        <th>Name</th>
                        <th>Closing Balance</th>
                    </tr>
                </thead>
                <tbody class="tbodyPfLoanClosingBalancelisting">
                </tbody>
                <tfoot>
                    <tr class="success">
                        <th colspan="3">Total</th>
                        <td class="tdTotal"></td>
                    </tr>
                </tfoot>
            </table>
        </div>
        <br />
    </div>

    <script type="text/x-jQuery-tmpl" id="PfLoanClosingBalanceListing">
        <tr>
            <td class="project-title">${Sno}</td>
            <td class="project-title">${EmployeeCode}</td>
            <td class="project-title">${NAME}</td>
            <td class="project-title tdMasterSalary">${Balance}</td>
        </tr>
    </script>

    <script src="../../js/Page_JS/Report_PfLoanClosingReport.js"></script>


</asp:Content>


