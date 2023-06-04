<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_BudgetSummaryWithIncrease.aspx.cs" Inherits="Pages_HCM_Report_BudgetSummaryWithIncrease" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="Budget Summary Report With Increase" />
            </h2>
            <ol class="breadcrumb">

                <li>
                    <a href="#">HCM Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Budget Summary Increase" />
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
            <div class="col-lg-2 ">
                <label>Increase Rate </label>
                <input type="number" min="0" id="txtIncreaseRate" class="txtIncreaseRate form-control" />
            </div>
            <div class="col-lg-2 ">
                <label>Premium Rate GLI</label>
                <input type="number" min="0" id="txtPremiumRate" class="txtPremiumRate form-control" />
            </div>
            <div class="col-lg-2 ">
                <label>Premium Rate GPA</label>
                <input type="number" min="0" id="txtPremiumRateGPA" class="txtPremiumRateGPA form-control" />
            </div>
            <div class="col-lg-2 divMonthPayroll">
                <label>Select </label>
                <select class="form-control  ddlGroupBy"></select>
            </div>

            <div class="col-lg-2 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tablebudgetsummarywithincreaseListing','Budget_Summary_Report_With_Increase')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tablebudgetsummarywithincreaseListing')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            Budget Detail Report With Increase
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tablebudgetsummarywithincreaseListing" id="tablebudgetsummarywithincreaseListing">
                <thead class="theadbudgetsummarywithincreaselisting">
                    <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    <tr class="info">
                        <th>SR. NO.</th>

                        <%--<th>Designation</th>
                        <th>Location</th>
                        <th>Department</th> 
                        <th>Cost Center</th>
                        <th>SAP Cost Center</th>--%>
                        <th>
                            <label class="Headercode"></label>
                        </th>
                        <th>
                            <label class="HeaderName"></label>
                        </th>

                        <th>Gross Salary And Other Benefits</th>
                        <th>PF Count</th>
                        <th>Bounus 10C</th>
                        <th>SP Bounus</th>
                        <th>Gratuity</th>
                        <th>EOBI Count</th>
                        <th>SESSI Count</th>
                        <th>Veh Fuel</th>
                        <th>Veh R&M</th>
                        <th>GLI</th>
                        <th>GPA</th>
                        <th>Medical Insurance</th>

                    </tr>
                </thead>
                <tbody class="tbodybudgetsummarywithincreaseListing">
                </tbody>
                <tfoot>
                    <tr class="info">
                        <td colspan="3" style="font-weight: bold">Total</td>
                        <th class="tdGSAndOtherBenefit" style="text-align: right"></th>
                        <th class="tdPFCount" style="text-align: right"></th>
                        <th class="tdBonus10C" style="text-align: right"></th>
                        <th class="tdSPBonus" style="text-align: right"></th>
                        <th class="tdGratuity" style="text-align: right"></th>
                        <th class="tdEOBICount" style="text-align: right"></th>
                        <th class="tdSessiCount" style="text-align: right"></th>
                        <th class="tdVehFuel" style="text-align: right"></th>
                        <th class="tdVehRANDM" style="text-align: right"></th>
                        <th class="tdGLI" style="text-align: right"></th>
                        <th class="tdGPA" style="text-align: right"></th>
                        <th class="tdMedicalInsurance" style="text-align: right"></th>

                    </tr>
                </tfoot>
            </table>
        </div>
        <br />
    </div>
    <script type="text/x-jQuery-tmpl" id="budgetsummarywithincreaseListing">
        <tr class="trList">
            <td class="project-title"></td>

            <%-- <td class="project-title clsDesignation ABC">${clsDesignation}--%>
            <input class="clsSapCostCenter" type="hidden" value="${clsSapCostCenter}" />
            <input class="clsCostCenter" type="hidden" value="${clsCostCenter}" />
            <input class="clsLocation" type="hidden" value="${clsLocation}" />
            <input class="clsDepartment" type="hidden" value="${clsDepartment}" />
            <%-- </td>--%>
            <%-- 
            <td class="project-title">${clsLocation}</td>
            <td class="project-title clsDepartment">${clsDepartment}</td>
            <td class="project-title">${clsCostCenter}</td>
            <td class="project-title">${clsSapCostCenter}</td>
            --%>
            {{if FilterId == 1 }}
            <td class="project-title" id="DepartmentCode">${DepartmentCode}</td>
            <td class="project-title" id="clsDepartment">${clsDepartment}</td>
            {{/if}} 
            {{if FilterId == 2 }}
            <td class="project-title" id="tdLocationCode">${LocationCode}</td>
            <td class="project-title" id="tdclsLocation">${clsLocation}</td>
            {{/if}} 

              {{if FilterId == 3 }}
            <td class="project-title" id="CostCenterCode">${CostCenterCode}</td>
            <td class="project-title" id="clsCostCenter">${clsCostCenter}</td>
            {{/if}} 

            {{if FilterId == 4 }}
            <td class="project-title" id="SapCostCenterCode">${SapCostCenterCode}</td>
            <td class="project-title" id="clsSapCostCenter">${clsSapCostCenter}</td>
            {{/if}} 

            <td class="project-title ClsGrossSalaryANDOtherBenefits" style="text-align: right">${GrossSalaryAndOtherBenefits}</td>
            <td class="project-title ClsPFCont" style="text-align: right">${PFCont}</td>
            <td class="project-title ClsBonus10C" style="text-align: right">${Bonus10C}</td>
            <td class="project-title ClsSPBonus" style="text-align: right">${SPBonus}</td>
            <td class="project-title ClsGratuity" style="text-align: right">${Gratuity}</td>
            <td class="project-title ClsEOBICont" style="text-align: right">${EOBICont}</td>
            <td class="project-title ClsSESSICont" style="text-align: right">${SESSICont}</td>
            <td class="project-title ClsVehFuel" style="text-align: right">${VehFuel}</td>
            <td class="project-title ClsVehRANDM" style="text-align: right">${VehRAndM}</td>
            <td class="project-title ClsGLI" style="text-align: right">${GLI}</td>
            <td class="project-title ClsGPA" style="text-align: right">${GPA}</td>
            <td class="project-title ClsMedicalInsurance" style="text-align: right">${MedicalInsurance}</td> 

        </tr>
    </script>

    <script src="../../js/Page_JS/Report_BudgetSummaryWithIncrease.js"></script>
</asp:Content>

