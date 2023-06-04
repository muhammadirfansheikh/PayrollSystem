<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_Sessi.aspx.cs" Inherits="Pages_HCM_Report_Sessi" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="Sessi" />
            </h2>
            <ol class="breadcrumb">
               
                <li>
                    <a href="#">HCM Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Sessi" />
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
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tableSessiListing','SESSI_Report')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tableSessiListing')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            Sessi
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tableSessiListing" id="tableSessiListing">
                <thead class="theadSessilisting">
                    <tr>
                        <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    </tr>
                    <tr class="info">

                        <th>S No.</th>
                        <%--    <th>Company</th>--%>
                        <th>Location</th>
                        <th>Employee Code</th>
                        <th>Name</th>
                        
                        <th>Department</th>
                        <th>Designation</th>
                        <th>Sesa Number</th>
                        <th>Days / Hour</th>
                        <th style="text-align: center;">Wages Paid</th>
                        <th style="text-align: center;">Employee Contribution</th>
                        <th style="text-align: center;">SESA Contribution</th>
                        <th style="text-align: center;">Total</th>
                    </tr>
                </thead>
                <tbody class="tbodySessiListing">
                </tbody>
                <tfoot>
                    <tr class="info">
                        <td colspan="8" style="font-weight:bold" ></td>
                        <th class="tdTotalWages" style="text-align: right;"></th>
                        <th class="tdTotalEmpCont" style="text-align: right;"></th>
                        <th class="tdTotalSesaCont" style="text-align: right;"></th>
                        <th class="tdTotalWagesCont" style="text-align: right;"></th>
                    </tr>
                </tfoot>
            </table>
        </div>
        <br />
    </div>
    <script type="text/x-jQuery-tmpl" id="SessiListing">
        <tr class="trList">
            <%--<td class="clsLocationId" style="display: none;">${LocationId}</td>--%>


            <td class="project-title clsSNo ABC">${Sno} 
                 <input class="clsSapCostCenter" type="hidden" value="${clsSapCostCenter}" />
                <input class="clsCostCenter" type="hidden" value="${clsCostCenter}" />
                <input class="clsLocation" type="hidden" value="${clsLocation}" />
                <input class="clsDepartment" type="hidden" value="${clsDepartment}" />
                <input type="hidden" value="${Wages}" class="clsWages" />
            </td>
            <%-- <td class="project-title clsCompany">${CompanyName}</td>--%>
            <td class="project-title clsLocation">${clsLocation}</td>
            <td class="project-title">${EmployeeCode}</td>
            <td class="project-title">${Name}</td>
          
            <td class="project-title clsDepartment">${clsDepartment}</td>
            <td class="project-title clsDesignation">${clsDesignation}</td>
            <td class="project-title clsSesaNumber">${SessiNumber}</td>
            <td class="project-title">${DaysPerHours}</td>


            <td class="project-title clsWagesPaid" style="text-align: right;">${WagesPaid}</td>
            <td class="project-title clsEmpCont" style="text-align: right;">${EmpContribution}</td>
            <td class="project-title clsSesaCont" style="text-align: right;">${SESAContribution}</td>
            <td class="project-title clsWagesCont" style="text-align: right;">${Total}</td>
        </tr>
    </script>

    <script src="../../js/Page_JS/Report_Sessi.js"></script>
</asp:Content>



