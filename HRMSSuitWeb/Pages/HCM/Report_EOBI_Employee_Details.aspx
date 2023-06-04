<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_EOBI_Employee_Details.aspx.cs" Inherits="Pages_HCM_Report_EOBI_Employee_Details" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="EOBI Employee Detail List" />
            </h2>
            <ol class="breadcrumb">

                <li>
                    <a href="#">Other Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="EOBI Employee Detail List" />
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

            <%--     <div class="col-lg-2 ">
                <label>Select </label>
                <select class="form-control  ddlGroupBy"></select>
            </div>--%>

            <div class="col-lg-8 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tableEOBIEmployeeListing','EOBI_Employee_Detail')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tableEOBIEmployeeListing')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            EOBI Employee Detail
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


                        <th>No Of Days</th>
                        <th>Employer Cont</th>
                        <th>Employee Cont</th>
                        <th>Total Cont</th>

                        <th>Location</th>
                        <th>Cost Center</th>





                    </tr>
                </thead>
                <tbody class="tbodyEOBIEmployeeListing">
                </tbody>
                <tfoot>
                    <tr class="info">
                        <td colspan="7" style="font-weight: bold">Total</td>


                        <th class="tdno_of_days" style="text-align: right"></th>
                        <th class="tdemployer" style="text-align: right"></th>
                        <th class="tdemployee" style="text-align: right"></th>
                        <th class="tdtotal_cont" style="text-align: right"></th>
                        <th colspan="2"></th>


                    </tr>
                </tfoot>
            </table>
        </div>
        <br />
    </div>
    <script type="text/x-jQuery-tmpl" id="EOBIEmployeeListing">
        <tr class="trList">


            <td class="project-title clcName">${Name}</td>
            <td class="project-title clcf_h_name">${eobi_no}</td>

            <td class="project-title clscnic_no">${cnic}</td>
            <td class="project-title clscnic_no">${nic}</td>

            <td class="project-title clsgender">${dob}</td>
            <td class="project-title clsdt_birth">${doj}</td>
            <td class="project-title clsdt_entry">${doe}</td>
            <td class="project-title clsno_of_days" style="text-align: right">${no_of_days}

            </td>
            <td class="project-title clsemployer" style="text-align: right">${employer}</td>
            <td class="project-title clsemployee" style="text-align: right">${employee}</td>
            <td class="project-title clstotal_cont" style="text-align: right">${total_cont}</td>
            <td class="project-title clsDays">${location}</td>
            <td class="project-title clswages">${costcentre}</td>




        </tr>
    </script>

    <script src="../../js/Page_JS/Report_EOBI_Employee_Detail.js"></script>
</asp:Content>

