<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_SessiContribution.aspx.cs" Inherits="Pages_HCM_Report_SessiContribution" %>

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
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelOutWithouHiddenFields('.tableSessiContributionListing')" />
                <input type="button" class="btn btn-primary pull-right" value="Print" onclick="sendPrint('.tableSessiContributionListing')" />
            </div>
        </div>
    </div>

    <div class="panel panel-info">
        <div class="panel-heading">
            Sessi Contribution 
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tableSessiContributionListing">
                <thead class="theadSessiContributionlisting">
                    <tr>
                        <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    </tr>
                    <tr class="info">

                        <th>S No</th>
                        <th>Employee Code</th>
                        <th>Name</th>
                        <th>Father Name</th>
                        <th>CNIC</th>
                        <th>Designation</th>
                        <th>Employee Type</th>
                        <th>Skill Leave</th>
                        <th>Gross Salary</th>
                        <th>Days/Per</th>
                        <th>Monthly Gross</th>

                    </tr>
                </thead>
                <tbody class="tbodySessiContributionListing">
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

    <script type="text/x-jQuery-tmpl" id="SessiContributionListing">
        <tr class="trList">
            <td class="project-title">${Sno}</td>
            <td class="project-title">${EmployeeCode}</td>
            <td class="project-title">${Name}</td>
            <td class="project-title">${FatherName}</td>
            <td class="project-title ">${CNIC}</td>
            <td class="project-title ">${DesignationName}</td>
            <td class="project-title ">${Emp_Type}</td>
            <td class="project-title ">${Skill_Leve}</td>
            <td class="project-title">${GrossSalary}</td>
            <td class="project-title">${DaysPerHours}</td>
            <td class="project-title">${Month_Gross}</td>

        </tr>
    </script>


    <script src="../../js/Page_JS/Report_SessiContribution.js"></script>


</asp:Content>

