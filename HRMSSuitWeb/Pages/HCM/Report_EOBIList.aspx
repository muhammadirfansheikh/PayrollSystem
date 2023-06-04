<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_EOBIList.aspx.cs" Inherits="Pages_HCM_Report_EOBIList" %>

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
            <div class="col-lg-2 divMonthPayroll">
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
            EOBI Employee Listing
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tableStaffLoanListing">
                <thead class="theadStaffLoanlisting">
                    <tr>
                        <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    </tr>
                    <tr class="info">
                        <th>S No</th>
                        <th>Employee Code</th>
                        <th>Name</th>
                        <th>EOBI No</th>
                        <th>CNIC</th>

                        <th>Date of Birth</th>
                        <th>Date of Joining</th>
                        <th>DOE</th>
                        <th>No of Days</th>
                        <th>Employer</th>
                        <th>Employee</th>
                        <th>Total</th>
                        <th>Location</th>
                    </tr>
                </thead>
                <tbody class="tbodyEOBIEmpListing">
                </tbody>
                <tfoot>
                    <%-- <tr class="info">
                        <td colspan="5"></td>

                        <th class="tdTotalInstallment">0</th>


                        <th class="tdTotalBalance">0</th>
                        <td></td>

                    </tr>--%>
                </tfoot>
            </table>
        </div>
    </div>

    <script type="text/x-jQuery-tmpl" id="EOBIEmpListing">
        <tr class="trList">
            <td class="project-title">${SNo}</td>
            <td class="project-title">${EmployeeCode}</td>
            <td class="project-title">${Name}</td>
            <td class="project-title">${EOBI_NO}</td>
            <td class="project-title ">${CNIC}</td>
            <%--  <td class="project-title ">${NIC}</td>--%>
            <td class="project-title ">${DOB}</td>
            <td class="project-title ">${DOJ}</td>
            <td class="project-title ">${DOE}</td>
            <td class="project-title">${No_Of_Days}</td>
            <td class="project-title">${Employer}</td>
            <td class="project-title">${Employee}</td>
            <td class="project-title">${Total}</td>
            <td class="project-title">${Location}</td>
        </tr>
    </script>


    <script src="../../js/Page_JS/Report_EOBIList.js"></script>

</asp:Content>

