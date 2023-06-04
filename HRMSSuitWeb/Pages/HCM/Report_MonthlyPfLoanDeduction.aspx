<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_MonthlyPfLoanDeduction.aspx.cs" Inherits="Pages_HCM_Report_MonthlyPfLoanDeduction" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <uc:EmployeeSearchFilters runat="server" ID="EmployeeSearchFilters" />

    <div class="panel panel-info">
        <div class="panel-heading">
            Report Functions
        </div>
        <div class="panel-body">
            <div class="col-lg-2 divMonthPayroll">
                <label>Select Year</label>
                <input type="text" class="form-control txtYear DatePickerYear" />
            </div>

            <div class="col-lg-12">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelOutWithouHiddenFields('.tableMonthlyPfDeduction')" />
                <input type="button" class="btn btn-primary pull-right" value="Print" onclick="sendPrint('.tableMonthlyPfDeduction')" />
            </div>
        </div>
    </div>

    <div class="panel panel-info">
        <div class="panel-heading">
            Monthly Pf Loan Deduction
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tableMonthlyPfDeduction">
                <thead class="theadMonthlyPfDeduction">
                    <uc:ReportHeader runat="server" ID="ReportHeader" />
                </thead>
                <tbody class="tbodyMonthlyPfDeduction">
                </tbody>
            </table>
        </div>
    </div>

    <div class="panel panel-info">
        <div class="panel-heading">
            Monthly Pf Deduction Loan Summary
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tableMonthlyPfDeductionSumm">
                <thead class="theadMonthlyPfDeductionSumm">
                    <uc:ReportHeader runat="server" ID="ReportHeader1" />
                </thead>
                <tbody class="tbodyMonthlyPfDeductionSumm">
                </tbody>
            </table>
        </div>
    </div>


    <script src="../../js/Page_JS/Report_MonthlyPfLoanDeduction.js"></script>


</asp:Content>



