<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_StaffLoan.aspx.cs" Inherits="Pages_HCM_Report_StaffLoan" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="Staff Loan" />
            </h2>
            <ol class="breadcrumb">
                
                <li>
                    <a href="#">HCM Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Staff Loan" />
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
                <label> Month</label>
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

            <div class="col-lg-2 ">
                <label>Select </label>
                <select class="form-control  ddlGroupBy"></select>
            </div>

            <div class="col-lg-4 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tableStaffLoanListing','Staff_Loan')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tableStaffLoanListing')" />
            </div>
        </div>
    </div>

    <div class="panel panel-info">
        <div class="panel-heading">
            Staff Loan
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tableStaffLoanListing" id="tableStaffLoanListing">
                <thead class="theadStaffLoanlisting">
                    <tr>
                        <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    </tr>
                    <tr class="info">
                        <th>Employee Code</th>
                        <th>Location</th>
                        <th>Name</th>
                 
                        <th>Loan Type</th>
                        <th>Installment</th>
                        <th>Balance</th>
                     <%--   <th>Intrest Amount</th>--%>

                    </tr>
                </thead>
                <tbody class="tbodyStaffLoanListing">
                </tbody>
                <tfoot>
                    <tr class="info">
                        <td colspan="4" style="font-weight:bold"></td>
                        <th class="tdTotalInstallment" style="text-align:right"></th>
                        <th class="tdTotalBalance" style="text-align:right"></th>
              

                    </tr>
                </tfoot>
            </table>
        </div>
        <br />
    </div>

    <script type="text/x-jQuery-tmpl" id="StaffLoanListing">
        <tr class="trList">
            <td class="project-title">${EmployeeCode}</td>
            <td class="project-title clsLocation ABC">${clsLocation}
                  <input class="clsSapCostCenter" type="hidden" value="${clsSapCostCenter}" />
                <input class="clsCostCenter" type="hidden" value="${clsCostCenter}" />
                <input class="clsLocation" type="hidden" value="${clsLocation}" />
            </td>
            <td class="project-title">${Name}</td>
           
            <td class="project-title clsPaymentType">${PaymentType}</td>
            <td class="project-title clsInstallmentAmount" style="text-align:right">${InstallmentAmount}</td>
            <td class="project-title clsBalance" style="text-align:right">${Balance}</td>
         <%--   <td class="project-title clsInterestAmount">${InterestAmount}</td>--%>
            <%--  <td class="project-title">${TotalInstallment}</td>
            <td class="project-title">${TotalBalance}</td>--%>
        </tr>
    </script>

    <script src="../../js/Page_JS/Report_StaffLoan.js"></script>
</asp:Content>



