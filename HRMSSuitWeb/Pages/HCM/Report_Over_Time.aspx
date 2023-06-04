<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_Over_Time.aspx.cs" Inherits="Pages_HCM_Report_Over_Time" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
        <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="Over Time" />
            </h2>
            <ol class="breadcrumb">
                
                <li>
                    <a href="#">Other Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Over Time" />
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

            <div class="col-lg-2 ">
                <label>Select </label>
                <select class="form-control  ddlGroupBy"></select>
            </div>

            <div class="col-lg-8 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tableOverTimeListing','Over_Time')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tableOverTimeListing')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
           Over Time
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tableOverTimeListing" id="tableOverTimeListing">
                <thead class="theadOverTimelisting">
                    <tr>
                        <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    </tr>
                    <tr class="info">
                        <th>Sr No.</th>
                 
                        <th>Employee Code</th>
                        <th>Name</th>
                        <th>Designation</th>
                        <th>Location</th>
                        <th>Department</th>
                        <th>Cost Center</th>
                        <th>Sap Cost Center</th>
                        <th>Basic+CLA</th>
                        <th>Rate</th>
                        <th>Over Time Hours</th>
                        <th>Amount</th>
                       

                    </tr>
                </thead>
                <tbody class="tbodyOverTimeListing">
                </tbody>
                <tfoot>
                    <tr class="info">
                        <td colspan="8" style="font-weight:bold">Total</td>
                        <th class="tdTotalOverTimeBasic_CLA" style="text-align:right"></th>
                        <th class="tdTotalOverTimeRate" style="text-align:right"></th>
                         <th class="tdTotalOverTimeDays" style="text-align:right"></th>
                        <th class="tdTotalOverTimeAmount" style="text-align:right"></th>
                       
                    </tr>
                </tfoot>
            </table>
        </div>
        <br />
    </div>
    <script type="text/x-jQuery-tmpl" id="OverTimeListing">
        <tr class="trList">
          <td class="project-title">
                
            </td>
            


            <td class="project-title clsEmployeeCode ABC">${EmployeeCode}
                 <input class="clsSapCostCenter" type="hidden" value="${clsSapCostCenter}" />
                <input class="clsCostCenter" type="hidden" value="${clsCostCenter}" />
                <input class="clsLocation" type="hidden" value="${clsLocation}" />
                <input class="clsDepartment" type="hidden" value="${clsDepartment}" />
            </td>
         
            <td class="project-title clsName">${Name}</td>
            <td class="project-title clsDesignation">${clsDesignation}</td>
            <td class="project-title">${clsLocation}</td>
            <td class="project-title clsDepartment">${clsDepartment}</td>
            <td class="project-title">${clsCostCenter}</td>
            <td class="project-title">${clsSapCostCenter}</td>
            
            <td class="project-title clsBasic_CLA" style="text-align:right">${BasicCola}</td>
            <td class="project-title clsRate" style="text-align:right">${Rate}</td>
            <td class="project-title clsOvertimeDays" style="text-align:right">${OvertimeHours}</td>
            <td class="project-title clsAllowanceAmount" style="text-align:right">${Amount}</td>
        </tr>
    </script>

    <script src="../../js/Page_JS/Report_Over_Time.js"></script>
</asp:Content>

