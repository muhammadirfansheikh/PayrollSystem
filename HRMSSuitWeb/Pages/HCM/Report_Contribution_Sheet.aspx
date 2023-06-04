<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_Contribution_Sheet.aspx.cs" Inherits="Pages_HCM_Report_Contribution_Sheet" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="Contribution Sheet" />
            </h2>
            <ol class="breadcrumb">
                
                <li>
                    <a href="#">Other Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Contribution Sheet" />
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

            <%--<div class="col-lg-2 ">
                <label>Bank</label>
                <select class="form-control ddlBank" onchange="BindBankBranch();"></select>
            </div>

            <div class="col-lg-2 ">
                <label>Branch</label>
                <select class="form-control ddlBranch"></select>
            </div>--%>

            <%--<div class="col-lg-2 ">
                <label>Select </label>
                <select class="form-control  ddlGroupBy"></select>
            </div>--%>

            <div class="col-lg-8 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tableContributionSheetListing','Contribution_Sheet')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tableContributuinSheetListing')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            Contribution Sheet
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tableContributionSheetListing" id="tableContributionSheetListing">
                <thead class="theadContributionSheetlisting">
                    <tr>
                        <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    </tr>
                    <tr class="info">
                        <th>Sr No.</th>

                        <th>Employee Code</th>
                        <th>Employee Name</th>
                        <th>Opening Balance</th>
                        <th>January</th>
                        <th>February</th>
                        <th>March</th>
                        <th>April</th>
                        <th>May</th>
                        <th>June</th>
                        <th>July</th>
                        <th>August</th>
                        <th>September</th>
                        <th>October</th>
                        <th>November</th>
                        <th>December</th>
                        <th>Arrear</th>
                        <th>Withdrawal</th>
                        <th>Closing Balance</th>
                       
                        <th>Commul Income</th>


                    </tr>
                </thead>
                <tbody class="tbodyContributionSheetListing">
                </tbody>
                <tfoot>
                    <tr class="info">
                        <td colspan="3" style="font-weight:bold">Total</td>
                        <td class="tdTotalOpeningBalance" style="text-align:right"></td>
                        <td class="tdTotalJanuary" style="text-align:right"></td>
                        <td class="tdTotalFebruary" style="text-align:right"></td>
                        <td class="tdTotalMarch" style="text-align:right"></td>
                        <td class="tdTotalApril" style="text-align:right"></td>
                        <td class="tdTotalMay" style="text-align:right"></td>
                        <td class="tdTotalJune" style="text-align:right"></td>
                        <td class="tdTotalJuly" style="text-align:right"></td>
                        <td class="tdTotalAugust" style="text-align:right"></td>
                        <td class="tdTotalSeptember" style="text-align:right"></td>
                        <td class="tdTotalOctober" style="text-align:right"></td>
                        <td class="tdTotalNovember" style="text-align:right"></td>
                        <td class="tdTotalDecember" style="text-align:right"></td>
                        <td class="tdTotalArrear" style="text-align:right"></td>
                        <td class="tdTotalWithdrawal" style="text-align:right"></td>
                        <td class="tdTotalClosingBalance" style="text-align:right"></td>
                        <td class="tdTotalCommulIncome" style="text-align:right"></td>
                    </tr>
                </tfoot>
            </table>
        </div>
        <br />
    </div>
    <script type="text/x-jQuery-tmpl" id="ContributionSheetListing">
        <tr class="trList">
            <td class="project-title ">
               
            </td>

            <td class="project-title clsEmployeeCode ABC">${EmployeeCode}</td>
            <td class="project-title clsEmployeeCode">${EmployeeName}</td>
            <td class="project-title clsOpeningBalance" style="text-align:right">${OpeningBalance}</td>
            <td class="project-title clsJanuary" style="text-align:right">${January}</td>
            <td class="project-title clsFebruary" style="text-align:right">${February}</td>
            <td class="project-title clsMarch" style="text-align:right">${March}</td>
            <td class="project-title clsApril" style="text-align:right">${April}</td>
            <td class="project-title clsMay" style="text-align:right">${May}</td>
            <td class="project-title clsJune" style="text-align:right">${June}</td>
            <td class="project-title clsJuly" style="text-align:right">${July}</td>
            <td class="project-title clsAugust" style="text-align:right">${August}</td>
            <td class="project-title clsSeptember" style="text-align:right">${September}</td>
            <td class="project-title clsOctober" style="text-align:right">${October}</td>
            <td class="project-title clsNovember" style="text-align:right">${November}</td>
            <td class="project-title clsDecember" style="text-align:right">${December}</td>
            <td class="project-title clsArrear" style="text-align:right">${Arrear}</td>
            <td class="project-title clsWithdrawal" style="text-align:right">${Withdrawal}</td>
            <td class="project-title clsClosingBalance" style="text-align:right">${ClosingBalance}</td>

            <td class="project-title clsCommulIncome" style="text-align:right">${CommulIncome}</td>


            
        </tr>
    </script>

    <script src="../../js/Page_JS/Report_Contribution_Sheet.js"></script>
</asp:Content>

