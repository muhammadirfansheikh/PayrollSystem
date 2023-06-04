<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_EOBI_Employee_List.aspx.cs" Inherits="Pages_HCM_Report_EOBI_Employee_List" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="EOBI Employee List" />
            </h2>
            <ol class="breadcrumb">
            
                <li>
                    <a href="#">Other Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="EOBI Employee List" />
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

                <div class="col-lg-2 ">
                <label>Select </label>
                <select class="form-control  ddlGroupBy"></select>
            </div>
            <div class="col-lg-2 divMonthPayroll">
                <label>Select Month</label>
                <input type="text" class="form-control txtMonthOfPayroll DatePickerMonthComplete" />
            </div>
        <%--   <div class="col-lg-2 divMonthPayroll">
                <label>From Date</label>
                <input type="text" class="form-control txtFromDate DatePickerComplete txtMonthOfPayroll" />
            </div>
              <div class="col-lg-2 divMonthPayroll">
                <label>To Date</label>
                <input type="text" class="form-control txtToDate DatePickerComplete txtMonthOfPayroll" />
            </div>--%>

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
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tableEOBIEmployeeListing','EOBI_Employee_List')" />
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
                        <th>SR. NO.</th>
                        <th>IPS Reg No</th>
                        <th>Token No</th>
                        <th>REL CODE</th>
                        <th>Name</th>
                        <th>Father Name</th>
                        <th>CNIC No </th>
                   
                        <th>Gender</th>
                        <th>D.O.B</th>
                        <th>Entry Date</th>
                        <th>Exit Date</th>
                        <th>Remarks</th>
                        <th>Location</th>
                        <th>Department</th>
                        <th>Cost Center</th>
                        <th>Sap Cost Center</th>
                        <th>Days</th>
                        <th>Wadges</th>
                        <th>Employer Count</th>
                        <th>Employee Count</th>
                        <th>Total Count</th>
                        

                    </tr>
                </thead>
                <tbody class="tbodyEOBIEmployeeListing">
                </tbody>
                <tfoot>
                    <tr class="info">
                        <td colspan="16" style="font-weight:bold">Total</td>


                        <th class="tdTotalDays" style="text-align:right"></th>
                        <th class="tdTotalWadges" style="text-align:right"></th>
                        <th class="tdTotalEmployerCount" style="text-align:right"></th>
                        <th class="tdEmployeeCount" style="text-align:right"></th>
                        <th class="tdTotalCount" style="text-align:right"></th>

                        

                        
                    </tr>
                </tfoot>
            </table>
        </div>
        <br />
    </div>
    <script type="text/x-jQuery-tmpl" id="EOBIEmployeeListing">
        <tr class="trList">
            <td class="project-title">
                 
            </td>

            <td class="project-title clsips_reg_no ABC">${ips_reg_no}
                 <input class="clsSapCostCenter" type="hidden" value="${clsSapCostCenter}" />
                <input class="clsCostCenter" type="hidden" value="${clsCostCenter}" />
                <input class="clsLocation" type="hidden" value="${clsLocation}" />
                <input class="clsDepartment" type="hidden" value="${clsDepartment}" />
            </td>
            <td class="project-title clstoken_no">${token_no}</td>
            <td class="project-title clsrel_code">${rel_code}</td>

            <td class="project-title clcName">${Name}</td>
            <td class="project-title clcf_h_name">${f_h_name}</td>

            <td class="project-title clscnic_no">${cnic_no}</td>

 
            <td class="project-title clsgender">${gender}</td>
            <td class="project-title clsdt_birth">${dt_birth}</td>
            <td class="project-title clsdt_entry">${dt_entry}</td>
            <td class="project-title clsdt_exit">${dt_exit}</td>
            <td class="project-title clsremarks">${remarks}</td>
            <td class="project-title clsLocation">${clsLocation}</td>
            <td class="project-title clsDepartment">${clsDepartment}</td>
            <td class="project-title clsCostCenter">${clsCostCenter}</td>
            <td class="project-title clsSapCostCenter">${clsSapCostCenter}</td>

            <td class="project-title clsDays" style="text-align:right">${Days}</td>
            <td class="project-title clswages" style="text-align:right">${wages}</td>
            <td class="project-title clsemplyr_con" style="text-align:right">${emplyr_con}</td>
            <td class="project-title clsemp_cont" style="text-align:right">${emp_cont}</td>
            <td class="project-title clstotal_cont" style="text-align:right">${total_cont}</td>
        </tr>
    </script>

    <script src="../../js/Page_JS/Report_EOBI_Employee.js"></script>
</asp:Content>

