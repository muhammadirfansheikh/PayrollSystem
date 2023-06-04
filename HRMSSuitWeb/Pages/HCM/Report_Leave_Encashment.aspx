<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_Leave_Encashment.aspx.cs" Inherits="Pages_HCM_Report_Leave_Encashment" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="Leave Encashment List" />
            </h2>
            <ol class="breadcrumb">

                <li>
                    <a href="#">Other Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Leave Encashment List" />
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
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="ClickExport('tableLEListing','Report_Leave_Encashment')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tableLEListing')" />

                <input type="button" class="btn btn-info pull-right exportSummary" value="Export Summary" onclick="ClickExport('tableLESummary','Report_LE_Summary')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print Summary" onclick="sendPrint('.tableLESummary')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            Leave Encashment
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <div class="row">
                <div class="col-lg-12">
                    <table class="table table-hover tableLEListing" id="tableLEListing">
                        <thead class="theadLElisting">
                            <tr>
                                <uc:ReportHeader runat="server" ID="ReportHeader2" />
                            </tr>
                            <tr class="info">
                                <th>SR. NO.</th>
                                <th>Employee Code</th>
                                <th>Name</th>
                                <th>Designation Name</th>
                                <th>Basic</th>

                                <th>Days</th>
                                <th>Hours</th>
                                <th>Amount</th>




                            </tr>
                        </thead>
                        <tbody class="tbodyLEListing">
                        </tbody>
                        <tfoot>
                            <tr class="info">
                                <td colspan="4" style="font-weight: bold">Total</td>


                                <th class="tdBasic" style="text-align: right"></th>
                                <th class="tdDays" style="text-align: right"></th>
                                <th class="tdHours" style="text-align: right"></th>
                                <th class="tdTotalAmount" style="text-align: right"></th>

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
                                    <h4>Leave Encashment Summary</h4>
                                </div>

                                <div class="panel-body" style="min-height: 196px;">
                                    <table class="table table-responsive tableLESummary" id="tableLESummary">
                                        <thead class="theadLESummary">
                                            <tr>
                                                <uc:ReportHeader runat="server" ID="ReportHeader1" />
                                            </tr>
                                            <tr class="info">
                                                
                                                <th>Sap Cost Center</th>
                                                <th>Amount</th>





                                            </tr>
                                        </thead>
                                        <tbody class="tbodySummary">
                                        </tbody>
                                        <tfoot>
                                            <tr class="info">
                                                <td style="font-weight: bold">Total</td>


                                                <th class="tdAmount" style="text-align: right"></th>
                                                
                                            </tr>
                                        </tfoot>
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
    <script type="text/x-jQuery-tmpl" id="LEListing">
        <tr class="trList">
            <td class="project-title"></td>

            <td class="project-title clsEmployeeCode ABC">${EmployeeCode}</td>
            <td class="project-title clsName">${Name}</td>
            <td class="project-title clsDesignation">${Designation}</td>

            <td class="project-title clsBasic" style="text-align: right">${Basic}</td>
            <td class="project-title clsDays" style="text-align: right">${Days}</td>
            <td class="project-title clsHours" style="text-align: right">${Hours}</td>
            <td class="project-title clsTotalAmount" style="text-align: right">${Amount}</td>



        </tr>
    </script>
    <script type="text/x-jQuery-tmpl" id="DataSummary">
        <tr class="trList">
           
          

            <td class="project-title clsSapCostCenter ABC">${SapCostCenter}</td>
            <td class="project-title clsAmount" style="text-align: right">${Amount}</td>
           
         
        </tr>
    </script>
    <script src="../../js/Page_JS/Report_Leave_Encashment.js"></script>

    <script>
        function ClickExport(elementId, FileName) {

            excelExportWithFileSaver(elementId, FileName + "_" + $('#ddlEmployeeType option:selected').text())



        }
    </script>
</asp:Content>

