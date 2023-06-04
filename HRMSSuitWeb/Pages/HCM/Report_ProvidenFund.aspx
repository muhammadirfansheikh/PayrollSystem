<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_ProvidenFund.aspx.cs" Inherits="Pages_HCM_Report_ProvidenFund" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="Providend Fund" />
            </h2>
            <ol class="breadcrumb">
             
                <li>
                    <a href="#">HCM Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Providend Fund" />
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

            <div class="col-lg-2 ">
                <label>Select </label>
                <select class="form-control  ddlGroupBy"></select>
            </div>

            <div class="col-lg-8 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tablePropotionateListing','Report_Provident_Fund')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tablePropotionateListing')" />
            </div>
        </div>
    </div>

    <div class="panel panel-info">
        <div class="panel-heading">
            Provident Fund 
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tablePropotionateListing" id="tablePropotionateListing">
                <thead class="theadpropotionatelisting">
                    <tr>
                        <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    </tr>
                    <tr class="info">
                        <th>Location </th>
                        <th>Cost Center</th>
                        <th>Employee Code</th>
                        <th>Employee Name</th>
                        <th>Monthly P.F Contribution</th>
                        <th>P.F Balance</th>
                        <th>Interest Income</th>
                        <th>P.F Intrest Type</th>
                        <th>Loan Installment</th>
                        <th>Loan Balance</th>
                        <%--<th>Loan Interest</th>--%>
                        <th>Loan Int. Installment</th>
                    </tr>
                </thead>
                <tbody class="ProvidentFundSummary">
                </tbody>
                <tfoot>
                    <tr class="info">
                        <th colspan="4" style="font-weight:bold">Total : </th>
                        <th class="tdMonthlyCont" style="text-align:right"></th>
                        <th class="tdEmpBalance" style="text-align:right"></th>
                        <th class="tdInterestIncome" style="text-align:right"></th>
                        <th></th>
                        <th class="tdLoanInst" style="text-align:right"></th>
                        <th class="tdBalance" style="text-align:right"></th>
                       <%-- <th class="tdIntrestAmount" style="text-align:right"></th>--%>
                        <th class="tdLoanInstAmount" style="text-align:right"></th>

                    </tr>
                </tfoot>
            </table>
        </div>
        <br />
    </div>

    <script type="text/x-jQuery-tmpl" id="ProvidentFundSummary">
        <tr class="trList">

            <td class="project-title clsLocation ABC">${clsLocation}
                 <input class="clsSapCostCenter" type="hidden" value="${clsSapCostCenter}" />
                <input class="clsCostCenter" type="hidden" value="${clsCostCenter}" />
                <input class="clsLocation" type="hidden" value="${clsLocation}" />
            </td>
            <td class="project-title clsCostCenter ">${clsCostCenter}
               
            </td>
            <td class="project-title">${EmployeeCode}</td>
            <td class="project-title">${FirstName} ${LastName}</td>

            <td class="project-title clsMonthlyCont" style="text-align:right">${MonthlyContribution}</td>
            <td class="project-title clsEmpBalance" style="text-align:right">${EmployeeBalance}</td>
            <td class="project-title clsInterestIncome" style="text-align:right">${IsAllowInterest == 0 ? 0 : InterestIncome}</td>
            <td class="project-title">${IsAllowInterest == 0 ? 'IF' : ''}</td>
            <td class="project-title clsLoanInst" style="text-align:right">${LoanInstallmentAmount}</td>
            <td class="project-title clsBalance" style="text-align:right">${Balance}</td>
            <%--<td class="project-title clsIntrestAmount">${InterestAmount}</td>--%>
            <%--<td class="project-title clsIntrestAmount">0</td>--%>
            <%--   <td class="project-title clsLoanInstAmount">${InterestInstallmentAmount}</td>--%>
            <td class="project-title clsLoanInstAmount" style="text-align:right">${IsAllowInterest == 0 ? 0 : InterestInstallmentAmount}</td>
        </tr>
    </script>

    <script src="../../js/Page_JS/Report_ProvidentFund.js"></script>

</asp:Content>

