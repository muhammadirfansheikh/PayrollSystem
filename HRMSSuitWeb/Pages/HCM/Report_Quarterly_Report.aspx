<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_Quarterly_Report.aspx.cs" Inherits="Pages_HCM_Report_Quarterly_Report" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
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



            <div class="col-lg-2 ">
                <label>Select </label>
                <select class="form-control  ddlGroupBy"></select>
            </div>
            <div class="col-lg-8 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tableQuarterlyReport','Report_Quarterly')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tableQuarterlyReport')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            Quarterly Report List
        </div>
        <div class="panel-body" style="overflow-x: scroll">

            <table class="table table-hover tableQuarterlyReport" id="tableQuarterlyReport">
                <thead class="theadQuarterlyReport">
                    <tr>
                        <uc:ReportHeader runat="server" ID="ReportHeader1" />
                    </tr>
                    <tr class="info">
                        <th>Sr No</th>
                        <th>xlmastbasi</th>
                        <th>EmployeeCode</th>

                        <th>EmployeeName</th>
                        <th>DesignationName</th>
                        <th>Basic</th>
                        <th>HouseRent</th>
                        <th>Bonus</th>
                        <th>PF</th>
                        <th>xlprcarp</th>
                        <th>UtilityAllowance</th>
                        <th>Dis_LocationAllowance</th>
                        <th>CarAllowance</th>
                        <th>CellAllowance</th>
                        <th>Incentive</th>
                        <th>COLA</th>
                        <th>SpecialAllowance</th>
                        <th>EOBICompany</th>
                        <th>MedicalAllowance</th>
                        <th>Total2</th>
                        <th>Gratuity</th>
                        <th>Total3</th>
                    </tr>
                </thead>
                <tbody class="tbodyQuarterlyReport">
                </tbody>
                <tfoot>
                    <tr class="info">
                        <td style="font-weight: bold">Total</td>
                        <td class="tdxlmastbasi" style="text-align: right"></td>
                        <td colspan="3"></td>
                        <th class="tdBasic" style="text-align: right">0</th>
                        <th class="tdHouseRent" style="text-align: right">0</th>
                        <th class="tdBonus" style="text-align: right">0</th>
                        <th class="tdPF" style="text-align: right">0</th>
                        <th class="tdxlprcarp" style="text-align: right">0</th>
                        <th class="tdUtilityAllowance" style="text-align: right">0</th>
                        <th class="tdDis_LocationAllowance" style="text-align: right">0</th>
                        <th class="tdCarAllowance" style="text-align: right">0</th>
                        <th class="tdCellAllowance" style="text-align: right">0</th>
                        <th class="tdIncentive" style="text-align: right">0</th>
                        <th class="tdCOLA" style="text-align: right">0</th>
                        <th class="tdSpecialAllowance" style="text-align: right">0</th>
                        <th class="tdEOBICompany" style="text-align: right">0</th>
                        <th class="tdMedicalAllowance" style="text-align: right">0</th>
                        <th class="tdTotal2" style="text-align: right">0</th>
                        <th class="tdGratuity" style="text-align: right">0</th>
                        <th class="tdTotal3" style="text-align: right">0</th>


                    </tr>
                </tfoot>
            </table>

        </div>
        <br />
    </div>
    <script type="text/x-jQuery-tmpl" id="QuarterlyReportListing">
        <tr class="trList">


            <td class="project-title clsSNo">${SNo}</td>
            <td class="project-title clsxlmastbasi ABC" style="text-align: right">
                <input class="clsSapCostCenter" type="hidden" value="${clsSapCostCenter}" />
                <input class="clsCostCenter" type="hidden" value="${clsCostCenter}" />
                <input class="clsLocation" type="hidden" value="${clsLocation}" />
                <input class="clsDepartment" type="hidden" value="${clsDepartment}" />
                ${xlmastbasi}</td>
            <td class="project-title clsEmployeeCode">${EmployeeCode}</td>
            <td class="project-title clsEmployeeName">${EmployeeName}</td>
            <td class="project-title clsDesignationName">${DesignationName}</td>
            <td class="project-title clsBasic" style="text-align: right">${Basic}</td>
            <td class="project-title clsHouseRent" style="text-align: right">${HouseRent}</td>
            <td class="project-title clsBonus" style="text-align: right">${Bonus}</td>
            <td class="project-title clsPF" style="text-align: right">${PF}</td>
            <td class="project-title clsxlprcarp" style="text-align: right">${xlprcarp}</td>
            <td class="project-title clsUtilityAllowance" style="text-align: right">${UtilityAllowance}</td>
            <td class="project-title clsDis_LocationAllowance" style="text-align: right">${Dis_LocationAllowance}</td>
            <td class="project-title clsCarAllowance" style="text-align: right">${CarAllowance}</td>
            <td class="project-title clsCellAllowance" style="text-align: right">${CellAllowance}</td>
            <td class="project-title clsIncentive" style="text-align: right">${Incentive}</td>
            <td class="project-title clsCOLA" style="text-align: right">${COLA}</td>
            <td class="project-title clsSpecialAllowance" style="text-align: right">${SpecialAllowance}</td>
            <td class="project-title clsEOBICompany" style="text-align: right">${EOBICompany}</td>
            <td class="project-title clsMedicalAllowance">${MedicalAllowance}</td>
            <td class="project-title clsTotal2" style="text-align: right">${Total2}</td>
            <td class="project-title clsGratuity" style="text-align: right">${Gratuity}</td>
            <td class="project-title clsTotal3" style="text-align: right">${Total3}</td>






        </tr>
    </script>


    <script src="../../js/Page_JS/Report_Quarterly_Report.js"></script>
</asp:Content>

