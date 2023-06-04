<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_ResignedStaff.aspx.cs" Inherits="Pages_HCM_Report_ResignedStaff" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="Resigned Staff" />
            </h2>
            <ol class="breadcrumb">
                
                <li>
                    <a href="#">Other Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Resigned Staff" />
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
            <%--     <div class="col-lg-2 divMonthPayroll">
                <label>Select Month</label>
                <input type="text" class="form-control txtMonthOfPayroll DatePickerMonthComplete" />
            </div>--%>

            <div class="col-lg-2 divFromDate">
                <label>From Date</label>
                <input type="text" class="form-control txtFromDate DatePicker" />
            </div>


            <div class="col-lg-2 divToDate">
                <label>To Date</label>
                <input type="text" class="form-control txtToDate DatePicker" />
            </div>


            <%--<div class="col-lg-2 ">
                <label>Bank</label>
                <select class="form-control ddlBank" onchange="BindBankBranch();"></select>
            </div>

            <div class="col-lg-2 ">
                <label>Branch</label>
                <select class="form-control ddlBranch"></select>
            </div>--%>

            <div class="col-lg-2 ">
                <label>Select </label>
                <select class="form-control  ddlGroupBy"></select>
            </div>

            <div class="col-lg-8 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tableResignedStaffListing','Resigned_Staff')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tableResignedStaffListing')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            Resigned Staff
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tableResignedStaffListing" id="tableResignedStaffListing">
                <thead class="theadResignedStafflisting">
                    <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    <tr class="info">
                        <th>SR. NO.</th>
                        <th>Employee Code</th>
                        <th>Name</th>
                        <th>Designation</th>
                        <th>CNIC No</th>
                        <th>D.O Joining</th>
                        <th>Last Working Day</th>
                        <th>Employee Status</th>
                        <th>Location</th>
                        <th>Department</th>
                        <th>Cost Centre</th>
                        <th>SAP Cost Center Code</th>
                        <th>SAP Cost</th>
                        <th>Function</th>
                        <th>Subfunction</th>
                        <th>Gross Salary</th>
                        <th>Other Benefits</th>

                    </tr>
                </thead>
                <tbody class="tbodyResignedStaffListing">
                </tbody>
                <tfoot>
                    <tr class="info">
                        <td colspan="15" style="text-align:left">Total</td>
                        <th class="tdTotalGrossSalary" style="text-align:right"></th>
                        <th class="tdTotalBenefits" style="text-align:right"></th>

                        
                    </tr>
                </tfoot>
            </table>
        </div>
        <br />
    </div>
    <script type="text/x-jQuery-tmpl" id="ResignedStaffListing">
        <tr class="trList">
            <td class="project-title "></td>

            <td class="project-title clsEmployeeCode ABC">${EmployeeCode}

                <input class="clsSapCostCenter" type="hidden" value="${clsSapCostCenter}" />
                <input class="clsCostCenter" type="hidden" value="${clsCostCenter}" />
                <input class="clsLocation" type="hidden" value="${clsLocation}" />
                <input class="clsDepartment" type="hidden" value="${clsDepartment}" />
            </td>
            <td class="project-title clsName">${Name}</td>
            <td class="project-title clsDesignation">${clsDesignation}</td>

            <td class="project-title clcCNICNo">${CNIC}</td>
            <td class="project-title clcDOJoining">${JoiningDate}</td>

            <td class="project-title clsLastWorkingDay">${LastworkingDate}</td>
            <td class="project-title clsEmployeeStatus">${EmployeeStatus}</td>

            <td class="project-title">${clsLocation}</td>
            <td class="project-title clsDepartment">${clsDepartment}</td>
            <td class="project-title">${clsCostCenter}</td>
            <td class="project-title">${SapCostCenterCode}</td>
            <td class="project-title">${clsSapCostCenter}</td>
            <td class="project-title">${Function}</td>
            <td class="project-title">${SubFunction}</td>

            <td class="project-title clsGrossSalary" style="text-align:right">${GrossSalary}</td>
            <td class="project-title clsOtherBenefits" style="text-align:right">${OtherBenefits}</td>
        </tr>
    </script>

    <script src="../../js/Page_JS/Report_ResignedStaff.js"></script>
</asp:Content>

