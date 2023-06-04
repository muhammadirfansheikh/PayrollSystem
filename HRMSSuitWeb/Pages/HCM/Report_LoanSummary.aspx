<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_LoanSummary.aspx.cs" Inherits="Pages_HCM_Report_LoanSummary" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <uc:EmployeeSearchFilters runat="server" ID="EmployeeSearchFilters" />

    <div class="panel panel-info">
        <div class="panel-heading">
            Report Functions
        </div>
        <div class="panel-body">
            <div class="col-lg-2 divMonthPayroll">
                <label>Select Month</label>
                <input type="text" class="form-control txtMonthOfPayroll DatePickerMonthComplete" />
            </div>

            <div class="col-lg-2 ">
                <label>Select </label>
               <select class="form-control  ddlGroupBy"></select>
            </div>

            <div class="col-lg-12">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelOutWithouHiddenFields('.tableLoanSummary')" />
                <input type="button" class="btn btn-primary pull-right" value="Print" onclick="sendPrint('.tableLoanSummary')" />
            </div>
        </div>
    </div>

    <div class="panel panel-info">
        <div class="panel-heading">
            Loan Summary
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tableLoanSummary">
                <thead class="theadLoanSummary">
                    <tr>
                         <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    </tr>
                    <tr class="info">
                        <th>Employee Code</th>
                        <th>Loan Type</th>
                        <th>Installment Amount</th>
                        <th>Balance</th>
                        <th>Interest Amount</th>
                        <th>Interest Inst. Amount</th>
                    </tr>
                </thead>
                <tbody class="tbodyLoanSummary">
                </tbody>
                <tfoot>
                    <tr class="info">
                        <td colspan="2"></td>

                        <th class="tdInstallmentAmount">0</th>
                        <th class="tdBalance">0</th>
                        <th class="tdInterestAmount">0</th>
                        <th class="tdInterestInstallmentAmount">0</th>

                    
                    </tr>
                </tfoot>
            </table>
        </div>
    </div>

    <script type="text/x-jQuery-tmpl" id="LoanSummary">
        <tr>
            <td class="project-title">${EmployeeCode}</td>
            <td class="project-title clsLoanType">${LoanType}</td>
            <td class="project-title clsInstallmentAmount">${InstallmentAmount}</td>
            <td class="project-title clsBalance">${Balance}</td>
            <td class="project-title clsInterestAmount">${InterestAmount}</td>
            <td class="project-title clsInterestInstallmentAmount">${InterestInstallmentAmount}</td>
        </tr>
    </script>

    <script src="../../js/Page_JS/Report_LoanSummary.js"></script>

</asp:Content>

