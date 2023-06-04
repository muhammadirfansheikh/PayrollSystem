<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_Quarterly.aspx.cs" Inherits="Pages_HCM_Report_Quarterly" %>
<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
  <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="Quarterly Report" />
            </h2>
            <ol class="breadcrumb">

                <li>
                    <a href="#">Other Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Quarterly Report" />
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

           
            <div class="col-lg-8 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tableQuarterlyListing','Quarterly_Report')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tableQuarterlyListing')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            EOBI Employee
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tableQuarterlyListing" id="tableQuarterlyListing">
                <thead class="theadQuarterlylisting">
                    <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    <tr class="info">
                       <th>Sr No</th>
                       <th>Employee Code</th>
                       <th>Employee Name</th>
                       <th>Designation Name</th>
                       <th>Basic</th>
                       <th>House Rent</th>
                       <th>Bonus</th>
                       <th>PF</th>
                       <th>XLPCarp</th>
                       <th>Utility Allowance</th>
                       <th>Dis Loc Allowance</th>
                       <th>Car Allowance</th>
                       <th>Cell Allowance</th>
                       <th>Incentive</th>
                       <th>COLA</th>
                       <th>Special Allowance</th>
                       <th>Medical Allowance</th>
                    </tr>
                </thead>
                <tbody class="tbodyQuarterlyListing">
                </tbody>
                <tfoot>
                    <tr class="info">
                        <td colspan="4" style="font-weight:bold">Total</td>


                        <th class="tdBasic" style="text-align:right"></th>
                        <th class="tdHouseRent" style="text-align:right"></th>
                        <th class="tdBonus" style="text-align:right"></th>
                        <th class="tdPF" style="text-align:right"></th>
                        <th class="tdxlprcarp" style="text-align:right"></th>
                        <th class="tdUtilityAllowance" style="text-align:right"></th>
                        <th class="tdDis_LocationAllowance" style="text-align:right"></th>
                        <th class="tdCarAllowance" style="text-align:right"></th>
                        <th class="tdCellAllowance" style="text-align:right"></th>
                        <th class="tdIncentive" style="text-align:right"></th>
                        <th class="tdCOLA" style="text-align:right"></th>
                        <th class="tdSpecialAllowance" style="text-align:right"></th>
                        <th class="tdMedicalAllowance" style="text-align:right"></th>




                        <td></td>
                    </tr>
                </tfoot>
            </table>
        </div>
        <br />
    </div>
    <script type="text/x-jQuery-tmpl" id="QuarterlyListing">
         <tr class="trList">
            <td class="project-title clsSNo">${SNo}</td>
            <td class="project-title clsEmployeeCode ABC">${EmployeeCode}
                
            </td>
            <td class="project-title clsEmployeeName" >${EmployeeName}</td>
            <td class="project-title clsDesignationName">${DesignationName}</td>
            <td class="project-title clsBasic" style="text-align:right">${Basic}</td>
            <td class="project-title clsHouseRent" style="text-align:right">${HouseRent}</td>
            
            <td class="project-title clsBonus" style="text-align:right">${Bonus}</td>
            <td class="project-title clsPF" style="text-align:right">${PF}</td>
            <td class="project-title clsxlprcarp" style="text-align:right">${xlprcarp}</td>
            <td class="project-title clsfontUtilityAllowance" style="text-align:right">${UtilityAllowance}</td>
            <td class="project-title clsfontDis_LocationAllowance" style="text-align:right">${Dis_LocationAllowance}</td>
            <td class="project-title clsCarAllowance" style="text-align:right"> ${CarAllowance}</td>
            <td class="project-title clsCellAllowance" style="text-align:right">${CellAllowance}</td>
            <td class="project-title clsIncentive" style="text-align:right">${Incentive}</td>
            <td class="project-title clsCOLA" style="text-align:right">${COLA}</td>
            <td class="project-title clsSpecialAllowance" style="text-align:right">${SpecialAllowance}</td>
            <td class="project-title clsMedicalAllowance" style="text-align:right">${MedicalAllowance}</td>
        </tr>
    </script>

    <script src="../../js/Page_JS/Report_Quarterly.js"></script>
</asp:Content>

