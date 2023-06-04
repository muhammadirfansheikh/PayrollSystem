<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_EOBI_No.aspx.cs" Inherits="Pages_HCM_Report_EOBI_No" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="EOBI No List" />
            </h2>
            <ol class="breadcrumb">

                <li>
                    <a href="#">Other Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="EOBI No List" />
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
                <label>From Date</label>
                <input type="text" class="form-control txtFromDate DatePickerComplete txtMonthOfPayroll" />
            </div>
            <div class="col-lg-2 divMonthPayroll">
                <label>To Date</label>
                <input type="text" class="form-control txtToDate DatePickerComplete txtMonthOfPayroll" />
            </div>

            <%--<div class="col-lg-2 ">
                <label>Bank</label>
                <select class="form-control ddlBank" onchange="BindBankBranch();"></select>
            </div>

            <div class="col-lg-2 ">
                <label>Branch</label>
                <select class="form-control ddlBranch"></select>
            </div>--%>

            <%--     <div class="col-lg-2 ">
                <label>Select </label>
                <select class="form-control  ddlGroupBy"></select>
            </div>--%>

            <div class="col-lg-8 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tableEOBIEmployeeListing','EOBI_No')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tableEOBIEmployeeListing')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            EOBI Employee
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tableEOBIEmployeeListing" id="tableEOBIEmployeeListing">
                <thead class="theadEOBIEmployeelisting">
                    <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    <tr class="info">
                        <th>Name</th>
                        <th>EOBI No</th>
                        <th>CNIC</th>
                        <th>NIC</th>
                        <th>DOB</th>
                        <th>Joining Date</th>
                        <th>Exit Date</th>
                        <th>Location</th>
                        <th>No Of Days</th>
                    </tr>
                </thead>
                <tbody class="tbodyEOBIEmployeeListing">
                </tbody>
                <tfoot>
                    <tr class="info">
                        <td colspan="7" style="font-weight:bold">Total</td>


                        <th class="tdno_of_days" style="text-align:right"></th>




                        <td></td>
                    </tr>
                </tfoot>
            </table>
        </div>
        <br />
    </div>
    <script type="text/x-jQuery-tmpl" id="EOBIEmployeeListing">
        <tr class="trList">


            <td class="project-title clcName">${Name}</td>
            <td class="project-title clseobi_no">${eobi_no}</td>

            <td class="project-title clscnic">${cnic}</td>


            <td class="project-title clsnic">${nic}</td>
            <td class="project-title clsdob">${dob}</td>
            <td class="project-title clsdoj">${doj}</td>
            <td class="project-title clsdoe">${doe}</td>

            <td class="project-title clsLocation">${Location}</td>
            <td class="project-title clsno_of_days" style="text-align:right">${no_of_days}</td>
        </tr>
    </script>

    <script src="../../js/Page_JS/Report_EOBI_No.js"></script>
</asp:Content>

