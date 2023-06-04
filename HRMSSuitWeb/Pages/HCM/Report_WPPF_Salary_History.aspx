<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_WPPF_Salary_History.aspx.cs" Inherits="Pages_HCM_Report_WPPF_Salary_History" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="WPPF Salary History List" />
            </h2>
            <ol class="breadcrumb">

                <li>
                    <a href="#">Other Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="WPPF Salary History List" />
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
            <div class="col-lg-2">
                <label>Employee Type</label>
                <select class="form-control" id="ddlEmployeeType">
                    <option value="-1">Select</option>
                    <option value="1">Resigned</option>
                    <option value="0">Un-Resigned</option>

                </select>
            </div>

            <div class="col-lg-6 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="ClickExport('tableEOBIEmployeeListing','Report_WPPF_Salary_History')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tableEOBIEmployeeListing')" />

                <input type="button" class="btn btn-info pull-right exportSummary" value="Export Summary" onclick="ClickExport('tablewppfSummary','Report_WPPF_Salary_History_Summary')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print Summary" onclick="sendPrint('.tablewppfSummary')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            WPPF Salary History
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <div class="row">
                <div class="col-lg-12">
                    <table class="table table-hover tableEOBIEmployeeListing" id="tableEOBIEmployeeListing">
                        <thead class="theadEOBIEmployeelisting">
                            <tr>
                                <uc:ReportHeader runat="server" ID="ReportHeader2" />
                            </tr>
                            <tr class="info">
                                <th>SR. NO.</th>
                                <th>Employee Code</th>
                                <th>Name</th>
                                <th>Designation Name</th>
                                <th>Appointment Date</th>

                                <th>Remarks</th>
                                <th>Status</th>
                                <th>July</th>
                                <th>August</th>
                                <th>September</th>
                                <th>October</th>
                                <th>November</th>
                                <th>December</th>
                                <th>January</th>
                                <th>February</th>
                                <th>March</th>
                                <th>April</th>
                                <th>May</th>
                                <th>June</th>
                                <th>Total</th>
                                <th>Periods</th>
                                <%--<th>Working Hours</th>--%>
                                <th>PM</th>
                                <th>Group</th>
                                <th>Resigned Date</th>



                            </tr>
                        </thead>
                        <tbody class="tbodyEOBIEmployeeListing">
                        </tbody>
                        <tfoot>
                            <tr class="info">
                                <td colspan="7" style="font-weight: bold">Total</td>


                                <th class="tdJuly" style="text-align: right"></th>
                                <th class="tdAugust" style="text-align: right"></th>
                                <th class="tdSeptember" style="text-align: right"></th>
                                <th class="tdOctober" style="text-align: right"></th>
                                <th class="tdNovember" style="text-align: right"></th>
                                <th class="tdDecember" style="text-align: right"></th>
                                <th class="tdJanuary" style="text-align: right"></th>
                                <th class="tdFebruary" style="text-align: right"></th>
                                <th class="tdMarch" style="text-align: right"></th>
                                <th class="tdApril" style="text-align: right"></th>
                                <th class="tdMay" style="text-align: right"></th>
                                <th class="tdJune" style="text-align: right"></th>
                                <th class="tdTotal" style="text-align: right"></th>
                                <th class="tdPeriod" style="text-align: right"></th>
                                <%-- <th class="tdWorkingDays" style="text-align:right"></th>--%>
                                <th class="tdPM" style="text-align: right"></th>
                                <th class="" style="text-align: right"></th>
                                <th class="" style="text-align: right"></th>
                            </tr>
                        </tfoot>
                    </table>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-12">
                    <div class="divSummary" style="display: none;">

                        <div class="col-lg-6">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4>WPPF Salary Summary</h4>
                                </div>

                                <div class="panel-body" style="min-height: 196px;">
                                    <table class="table table-responsive tablewppfSummary" id="tablewppfSummary">
                                        <thead class="theadwppfSummary">
                                            <tr>
                                                <uc:ReportHeader runat="server" ID="ReportHeader1" />
                                            </tr>

                                        </thead>
                                        <tbody class="tbodySummary">
                                        </tbody>
                                     
                                    </table>
                                </div>
                            </div>
                        </div>


                    </div>

                </div>
            </div>

        </div>
        <br />
    </div>
    <script type="text/x-jQuery-tmpl" id="EOBIEmployeeListing">
        <tr class="trList">
            <td class="project-title"></td>

            <td class="project-title clsEmployeeCode ABC">${EmployeeCode}</td>
            <td class="project-title clsName">${Name}</td>
            <td class="project-title clsDesignationName">${clsDesignation}</td>

            <td class="project-title clsDateofAppointment">${DateofAppointment}</td>

            <td class="project-title clsRemark">${Remark}</td>
            <td class="project-title clsStatus">${Status}</td>


            <td class="project-title clsJuly" style="text-align: right">${July}</td>
            <td class="project-title clsAugust" style="text-align: right">${August}</td>
            <td class="project-title clsSeptember" style="text-align: right">${September}</td>
            <td class="project-title clsOctober" style="text-align: right">${October}</td>
            <td class="project-title clsNovember" style="text-align: right">${November}</td>
            <td class="project-title clsDecember" style="text-align: right">${December}</td>
            <td class="project-title clsJanuary" style="text-align: right">${January}</td>
            <td class="project-title clsFebruary" style="text-align: right">${February}</td>
            <td class="project-title clsMarch" style="text-align: right">${March}</td>
            <td class="project-title clsApril" style="text-align: right">${April}</td>
            <td class="project-title clsMay" style="text-align: right">${May}</td>
            <td class="project-title clsJune" style="text-align: right">${June}</td>
            <td class="project-title clsTotal" style="text-align: right">${Total}</td>
            <td class="project-title clsPeriod" style="text-align: right">${Period}</td>
            <%-- <td class="project-title clsWorkingDays" style="text-align:right">${WorkingDays}</td>--%>
            <td class="project-title clsPM" style="text-align: right">${PM}</td>
            <td class="project-title clsGroup">${Group}</td>
            <td class="project-title clsDateofResigned">${DateofResigned}</td>

        </tr>
    </script>
    <script type="text/x-jQuery-tmpl" id="DataSummary">
        <tr class="trList">
            {{if IsBold == 1}}
            <td class="project-title clsDescription" style="font-weight:bold">${Description}</td>
            <td class="project-title clsCount" style="font-weight:bold">${Count}</td>
            {{else IsBold == 0}}
             <td class="project-title clsDescription">${Description}</td>
            <td class="project-title clsCount">${Count}</td>
            {{/if}}

        </tr>
    </script>
    <script src="../../js/Page_JS/Report_WPPF_Salary_History.js"></script>

    <script>
        function ClickExport(elementId,FileName) {

            excelExportWithFileSaver(elementId, FileName + "_"+$('#ddlEmployeeType option:selected').text())


            
        }
    </script>
</asp:Content>

