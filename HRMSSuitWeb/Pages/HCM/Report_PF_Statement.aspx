<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_PF_Statement.aspx.cs" Inherits="Pages_HCM_Report_PF_Statement" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="PF Statement" />
            </h2>
            <ol class="breadcrumb">
                
                <li>
                    <a href="#">HCM Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="PF Statement" />
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
                <label>Month</label>
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
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tablePFStatementListing','PF_Statement')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tablePFStatementListing')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            PF Statement
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tablePFStatementListing" id="tablePFStatementListing">
                <thead class="theadPFStatementlisting">
                    <tr>
                        <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table class="table table-hover">
                                <tr class="info">
                                    <th>Employee Code</th>
                                    <th>Name</th>
                                    <th>Department Name</th>
                                    <th>Designation</th>
                                </tr>
                                <tbody class="tbodyPFStatementEmployeeListing">
                                </tbody>
                            </table>
                        </td>
                    </tr>
                    <tr class="info">


                        <th>YEAR MONTH</th>
                        <th>EMPLOYEES CONTRIBUTION</th>
                        <th>EMPLOYERS CONTRIBUTION</th>
                        <th>TOTAL CONTRIBUTION</th>



                    </tr>
                </thead>
                <tbody class="tbodyPFStatementListing">
                </tbody>
                <tfoot>
                    <tr class="info">
                        <td>Total</td>

                        <td class="tdEMPLOYEESCONTRIBUTION" style="text-align:right"></td>
                        <td class="tdEMPLOYERSCONTRIBUTION" style="text-align:right"></td>
                        <td class="tdTOTALCONTRIBUTION" style="text-align:right"></td>

                    </tr>
                </tfoot>
            </table>
        </div>
        <br />
    </div>
    <script type="text/x-jQuery-tmpl" id="PFStatementListing">
        <tr class="trList">


            <td class="project-title clsYEARMONTH">${YEARMONTH}</td>
            <td class="project-title clsEMPLOYEESCONTRIBUTION" style="text-align:right">${EMPLOYEES_CONTRIBUTION}</td>
            <td class="project-title clsEMPLOYERSCONTRIBUTION" style="text-align:right">${EMPLOYERS_CONTRIBUTION}</td>
            <td class="project-title clsTOTALCONTRIBUTION" style="text-align:right">${TOTAL_CONTRIBUTION}</td>





        </tr>
    </script>
    <script type="text/x-jQuery-tmpl" id="PFStatementEmployeeListing">
        <tr class="trList">


          
                <td class="project-title clsEmployeeCode">${EmployeeCode}</td>
                <td class="project-title clsName">${Name}</td>
                <td class="project-title clsEDepartmentName">${DepartmentName}</td>
                <td class="project-title clsDesignationName">${DesignationName}</td>




          




        </tr>
    </script>

    <script src="../../js/Page_JS/Report_PF_Statement.js"></script>
</asp:Content>

