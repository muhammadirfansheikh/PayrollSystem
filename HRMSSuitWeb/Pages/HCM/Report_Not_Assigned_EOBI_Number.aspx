<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_Not_Assigned_EOBI_Number.aspx.cs" Inherits="Pages_HCM_Report_Not_Assigned_EOBI_Number" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
      <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="Not Assigned EOBI Number" />
            </h2>
            <ol class="breadcrumb">
          
                <li>
                    <a href="#">Other Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Not Assigned EOBI Number" />
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
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tableEOBIEmployeeListing','Not_Assigned_EOBI_Number')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tableEOBIEmployeeListing')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            Not Assigned EOBI Number
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tableEOBIEmployeeListing" id="tableEOBIEmployeeListing">
                <thead class="theadEOBIEmployeelisting">
                    <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    <tr class="info">
                        <th>Name</th>
                        <th>New NIC</th>
                        <th>Old Nic</th>
                        <th>Father Name</th>
                        <th>Relation CD</th>
                        <th>Gender</th>
                        <th>DOB</th>
                        <th>Joining Date</th>
                        <th>Post Address</th>
                        <th>City</th>
                        <th>Province</th>
                        <th>Phone</th>
                        <th>Email</th>
                        <th>Location</th>
                        
                        

                    </tr>
                </thead>
                <tbody class="tbodyEOBIEmployeeListing">
                </tbody>
                <tfoot>
                   <%-- <tr class="info">
                        <td colspan="14">Total</td>


                        <th class="tdTotalDays">Days</th>
                        <th class="tdTotalWadges">Wadges</th>
                        <th class="tdTotalEmployerCount">Employer Count</th>
                        <th class="tdEmployeeCount">Employee Count</th>
                        <th class="tdTotalCount">Total Count</th>

                        

                        <td></td>
                    </tr>--%>
                </tfoot>
            </table>
        </div>
        <br />
    </div>
    <script type="text/x-jQuery-tmpl" id="EOBIEmployeeListing">
        <tr class="trList">
      

            <td class="project-title">${Name}</td>
            <td class="project-title">${new_nic}</td>
            <td class="project-title">${old_nic}</td>

            <td class="project-title">${f_h_name}</td>
            <td class="project-title">${relationcd}</td>

            <td class="project-title">${Gender}</td>

            <td class="project-title">${dob}</td>
            <td class="project-title">${doj}</td>
            <td class="project-title">${postaddres}</td>
            <td class="project-title">${City}</td>
            <td class="project-title">${Province}</td>
            <td class="project-title">${Phone}</td>
            <td class="project-title">${Email}</td>
            <td class="project-title">${Location}</td>
            
        </tr>
    </script>

    <script src="../../js/Page_JS/Report_Not_Assigned_EOBI_Number.js"></script>
</asp:Content>

