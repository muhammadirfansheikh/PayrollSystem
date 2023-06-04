<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_LoanJV.aspx.cs" Inherits="Pages_HCM_Report_LoanJV" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <uc:EmployeeSearchFilters runat="server" ID="EmployeeSearchFilters" EmployeeCode="1"/>

    <div class="panel panel-info">
        <div class="panel-heading">
            Report Functions
        </div>
        <div class="panel-body">
            <div class="col-lg-2 divMonthPayroll" >
                <label>Select Month</label>
                <input type="text" class="form-control txtMonthOfPayroll DatePickerMonthComplete" />
            </div>

            <div class="col-lg-2 " style="display:none;">
                <label>Select </label>
                <select class="form-control  ddlGroupBy"></select>
            </div>

            <div class="col-lg-12">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelOutWithouHiddenFields('.tableStaffLoanListing')" />
                <input type="button" class="btn btn-primary pull-right" value="Print" onclick="sendPrint('.tableStaffLoanListing')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            JV Loan Listing
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tableStaffLoanListing">
                <thead class="theadStaffLoanlisting">
                    <tr>
                        <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    </tr>
                    <tr class="info">
                        <th>Employee Code</th>
                        <th>Sap Code</th>
                        <th>Name</th>
                        <th>Vendor Code</th>
                        <th>Amount</th>
                        <th>Advance</th>
                    </tr>
                </thead>
                <tbody class="tbodyStaffLoanListing">
                </tbody>
                <tfoot>
                    <tr class="info">
                        <td colspan="4"></td>
                        
                        <th class="tdAmount">0</th>

                        
                        <th class="tdAdvance">0</th>
                      

                    </tr>
                </tfoot>
            </table>
        </div>
    </div>
    <script type="text/x-jQuery-tmpl" id="StaffLoanListing">
        <tr class="trList">
            <td class="project-title">${EmployeeCode}</td>
            <td class="project-title clsSapCostCenter ABC">${SapcostCenterCode}
                 <%-- <input class="clsSapCostCenter" type="hidden" value="${clsSapCostCenter}" />
                <input class="clsCostCenter" type="hidden" value="${clsCostCenter}" />
                <input class="clsLocation" type="hidden" value="${clsLocation}" />--%>
            </td>
            <td class="project-title">${NAME}</td>
            <td class="project-title">${Vendr_Cod}</td>
            <td class="project-title clsCompanyLoan">${CompanyLoan}</td>
            <td class="project-title clsOtherLoan">${OtherLoan}</td>
          
        </tr>
    </script>

    <script src="../../js/Page_JS/Report_LoanJV.js"></script>
</asp:Content>
