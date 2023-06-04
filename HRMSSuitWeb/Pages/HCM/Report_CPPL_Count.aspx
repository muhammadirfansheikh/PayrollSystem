<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_CPPL_Count.aspx.cs" Inherits="Pages_HCM_Report_CPPL_Count" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="CPPL Count Report" />
            </h2>
            <ol class="breadcrumb">
                
                <li>
                    <a href="#">Other Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="CPPL Count" />
                    </strong>
                </li>
            </ol>
        </div>
    </div>

    <uc:EmployeeSearchFilters runat="server" ID="EmployeeSearchFilters" EmployeeCode="0" />

    <div class="panel panel-info">
        <div class="panel-heading">
            Report Functions
        </div>
        <div class="panel-body">
            <div class="col-lg-2 divMonthPayroll">
                <label>Select Month</label>
                <input type="text" class="form-control txtMonthOfPayroll DatePickerMonth_Year" />
            </div>


            <div class="col-lg-12 div_reportbutton" style="margin-top: 23px; display: none">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExport('tableMainCPPLCountListing','CPPL_Report')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tableMainCPPLCountListing')" />
            </div>
        </div>

        <div class="panel panel-info">
            <div class="panel-heading">
                Tax Return
            </div>
            <div class="panel-body" style="overflow-x: scroll">
                <table class="tableMainCPPLCountListing" id="tableMainCPPLCountListing" style="width: 100%">
                    <thead class="theadTaxReturnlisting">

                        <tr>
                            <td id="trCompanyTitle" colspan="2"></td>

                        </tr>
                        <tr>
                            <td id="trHeadOfficTitle" <%--colspan="2"--%>></td>
                        </tr>
                        <tr>
                            <td id="trHeadCountTitleWithDate">Head Count Report – July, 2021</td>
                            <td>
                                <div style="float: right">
                                    <p>P - Permanent Staff</p>
                                    <p>T - Casual/Temporary Staff</p>
                                    <p>C - Contract Staff</p>
                                    <p>DW - Daily Wages</p>
                                </div>

                            </td>
                        </tr>


                        <tr>
                            <td colspan="2">
                                <table border="1" class="table table-hover tableReportFirstListing" style="width: 100%">
                                    <thead class="theadReportFirstlisting">
                                        <tr>

                                            <td></td>
                                            <td></td>
                                            <td colspan="4" style="text-align: center" id="MonthOf"></td>
                                            <td colspan="12" style="text-align: center">Changes</td>
                                            <td style="text-align: center">Remarks</td>

                                        </tr>

                                        <tr>
                                            <td></td>
                                            <td>Unit / Department / Section</td>
                                            <td colspan="3" style="text-align: center">Staff</td>

                                            <td style="text-align: center">Month</td>
                                            <td colspan="4" style="text-align: center">Appointments</td>
                                            <td colspan="4" style="text-align: center">Transfers</td>
                                            <td colspan="4" style="text-align: center">Resignation</td>
                                            <td style="text-align: center"></td>

                                        </tr>
                                        <tr>
                                            <td>Sr No</td>
                                            <td></td>
                                            <td style="text-align: center">P</td>
                                            <td style="text-align: center">T</td>
                                            <td style="text-align: center">C</td>
                                            <td style="text-align: center">Total</td>

                                            <td style="text-align: center">P</td>
                                            <td style="text-align: center">T</td>
                                            <td style="text-align: center">C</td>
                                            <td style="text-align: center">DW</td>
                                            <td style="text-align: center">P</td>
                                            <td style="text-align: center">T</td>
                                            <td style="text-align: center">C</td>
                                            <td style="text-align: center">DW</td>
                                            <td style="text-align: center">P</td>
                                            <td style="text-align: center">T</td>
                                            <td style="text-align: center">C</td>
                                            <td style="text-align: center">DW</td>
                                            <td></td>



                                        </tr>


                                    </thead>
                                    <tbody class="tbodyReportFirstListing">
                                    </tbody>

                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <h3 style="text-decoration: underline">New Appointments</h3>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table border="1" class="table table-hover tableReportSecondListing">
                                    <thead class="theadReportSecondlisting">
                                        <tr>
                                            <td style="text-align: center">Sr No</td>
                                            <td style="text-align: center">Employee Code</td>
                                            <td style="text-align: center">Employee Name</td>
                                            <td style="text-align: center">Duration</td>
                                            <td style="text-align: center">Joining Date</td>
                                            <td style="text-align: center">Sap Cost Center</td>
                                        </tr>
                                    </thead>
                                    <tbody class="tbodyReportSecondListing">
                                    </tbody>

                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <h3 style="text-decoration: underline">Resignations</h3>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table border="1" class="table table-hover tableReportThirdListing" style="width: 100%">
                                    <thead class="theadReportThirdlisting">
                                        <tr>
                                            <td style="text-align: center">Sr No</td>
                                            <td style="text-align: center">Employee Code</td>
                                            <td style="text-align: center">Employee Name</td>

                                            <td style="text-align: center">Resigned Date</td>
                                            <td style="text-align: center">Sap Cost Center</td>
                                        </tr>
                                    </thead>
                                    <tbody class="tbodyReportThirdListing">
                                    </tbody>

                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <h3 style="text-decoration: underline">Change Sap Cost Center</h3>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table border="1" class="table table-hover tableReportFourthListing" style="width: 100%">
                                    <thead class="theadReportFourthlisting">
                                        <tr>
                                            <td style="text-align: center">Sr No</td>
                                            <td style="text-align: center">Employee Code</td>
                                            <td style="text-align: center">Employee Name</td>

                                            <td style="text-align: center">Old Sap Cost Center</td>
                                            <td style="text-align: center">New Sap Cost Center</td>
                                        </tr>
                                    </thead>
                                    <tbody class="tbodyReportFourthListing">
                                    </tbody>

                                </table>
                            </td>
                        </tr>

                    </thead>
                    <tbody class="tbodyMainCPPLCountListing">
                    </tbody>
                    <tfoot>
                    </tfoot>
                </table>
            </div>
            <br />
        </div>
    </div>

    <script type="text/x-jQuery-tmpl" id="ReportFirstListing">
        <tr class="trList">
            <td class="project-title ABC">${SrNo}
                
            </td>

            <td class="project-title clsDescription">${SapCostCenter}</td>
            <td class="project-title clsStaffP" style="text-align: center;">${Permanent}</td>
            <td class="project-title clsStaffT" style="text-align: center;">0</td>
            <td class="project-title clsStaffC" style="text-align: center;">${Contractual}</td>
            <td class="project-title clsStaffTotal" style="text-align: center;">${StaffTotal}</td>
            <td class="project-title clsAppointmentP" style="text-align: center;">${ApopointmentPermanent}</td>
            <td class="project-title clsAppointmentT" style="text-align: center;">0</td>
            <td class="project-title clsAppointmentC" style="text-align: center;">${AppointmentContractual}</td>
            <td class="project-title clsAppointmentDW" style="text-align: center;">0</td>
            <td class="project-title clsTransferP" style="text-align: center;">${TransferPermanent}</td>
            <td class="project-title clsTransferT" style="text-align: center;">0</td>
            <td class="project-title clsTransferC" style="text-align: center;">${TransferContractual}</td>
            <td class="project-title clsTransferDW" style="text-align: center;">0</td>
            <td class="project-title clsResignedP" style="text-align: center;">${ResignedPermanent}</td>
            <td class="project-title clsResignedT" style="text-align: center;">0</td>
            <td class="project-title clsResignedC" style="text-align: center;">${ResignedContractual}</td>
            <td class="project-title clsResignedDW" style="text-align: center;">0</td>
            <td class="project-title clsRemarks" style="text-align: center;"></td>

        </tr>
    </script>


    <script type="text/x-jQuery-tmpl" id="ReportSecondListing">
        <tr class="trList">
            <td class="project-title ABC">${SrNo}
                
            </td>

            <td class="project-title clsEmployeeCode">${EmployeeCode}</td>
            <td class="project-title clsNameOfEmployees" style="text-align: center;">${NameOfEmployees}</td>
            <td class="project-title clsDuration" style="text-align: center;">${Duration}</td>
            <td class="project-title clsJoiningDate" style="text-align: center;">${JoiningDate}</td>
            <td class="project-title clsSapCostCenter" style="text-align: center;">${SapCostCenter}</td>

        </tr>
    </script>


    <script type="text/x-jQuery-tmpl" id="ReportThirdListing">
        <tr class="trList">
            <td class="project-title ABC">${SrNo}
                
            </td>

            <td class="project-title clsEmployeeCode">${EmployeeCode}</td>
            <td class="project-title clsNameOfEmployees" style="text-align: center;">${NameOfEmployees}</td>

            <td class="project-title clsJoiningDate" style="text-align: center;">${ResignedDate}</td>
            <td class="project-title clsSapCostCenter" style="text-align: center;">${SapCostCenter}</td>

        </tr>
    </script>

    <script type="text/x-jQuery-tmpl" id="ReportFourthListing">
        <tr class="trList">
            <td class="project-title ABC">${SrNo}
                
            </td>

            <td class="project-title clsEmployeeCode">${EmployeeCode}</td>
            <td class="project-title clsNameOfEmployees" style="text-align: center;">${NameOfEmployees}</td>

            <td class="project-title clsJoiningDate" style="text-align: center;">${OldSapCostCenter}</td>
            <td class="project-title clsSapCostCenter" style="text-align: center;">${NewSapCostCenter}</td>

        </tr>
    </script>

    <script src="../../js/Page_JS/CPPL_COUNT_Report.js"></script>
   

  
</asp:Content>

