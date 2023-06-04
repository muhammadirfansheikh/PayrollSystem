<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_Hajj_List.aspx.cs" Inherits="Pages_HCM_Report_Hajj_List" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="Hajj List" />
            </h2>
            <ol class="breadcrumb">
                
                <li>
                    <a href="#">Other Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Hajj List" />
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

            <div class="col-lg-3">
                Month
                <input type="text" class="form-control txtFromDate DatePickerMonthComplete txtMonthOfPayroll" />
            </div>

           
            <div class="col-lg-4">
            </div>
            <div class="col-lg-2 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tableHajjListing','Hajj_List')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tableHajjListing')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            Executive List
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tableHajjListing" id="tableHajjListing">
                <thead class="theadHajjlisting">
                    <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    <tr class="info">
                        <th>SR. NO.</th>
                        <th>Employee No</th>
                        <th>Employee Name</th>
                        <th>Designation</th>
                        <th>Date Of Joining</th>
                        <th>Religion</th>
                        <%--<th>Cost Center</th>--%>
                        <th>Location</th>
                        <th>Nap</th>


                    </tr>
                </thead>
                <tbody class="tbodyHajjListing">
                </tbody>

            </table>
        </div>
        <br />
    </div>
    <script type="text/x-jQuery-tmpl" id="HajjListing">
        <tr class="trList">
            <td class="project-title"></td>
            <td class="project-title clsEmployeeCode ABC">${EmpNo}</td>
            <td class="project-title clsName">${EmployeeName}</td>
            <td class="project-title clsDesignation">${clsDesignation}</td>
            <td class="project-title clsDateOfJoining">${DateOfJoining}</td>
            <td class="project-title clsReligion">${Religion}</td>
            <%--<td class="project-title clsDesignation">${CostCenter}</td>--%>
            <td class="project-title clsLocation">${clsLocation}</td>
            <td class="project-title clsNap">${Nap}</td>


        </tr>
    </script>
    <script src="../../js/Page_JS/Rpt_Hajj_List.js"></script>
</asp:Content>

