<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_Pessi_Upload.aspx.cs" Inherits="Pages_HCM_Report_Pessi_Upload" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <iframe id="txtArea1" style="display: none"></iframe>
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="Pessi Upload" />
            </h2>
            <ol class="breadcrumb">

                <li>
                    <a href="#">Other Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Pessi Upload" />
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




            <div class="col-lg-3">
                Month
                <input type="text" class="form-control txtFromDate DatePickerMonthComplete txtMonthOfPayroll" />
            </div>

            <div class="col-lg-2">
            </div>
            <div class="col-lg-8 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tablePessiReportListing','Pessi_Report')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tablePessiReportListing')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            Pessi Upload List
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tablePessiReportListing" id="tablePessiReportListing">
                <thead class="theadPessiReportlisting">
                    <tr>
                        <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    </tr>
                    <tr class="info">
                        <th>Sr No</th>
                        <th>Employee Code</th>
                        <th>Employee Name</th>
                        <th>Father Name</th>
                        <th>Pessi No</th>

                        <th>Designation</th>
                        <th>Employee Type</th>
                        <th>Category</th>

                        <th>Days</th>
                        <th>Hours</th>

                        <th>Month Wages</th>
                        <th>Wages Paid</th>
                        <th>PESSI Cont</th>
                        <th>Location</th>
                       

                    </tr>
                </thead>
                <tbody class="tbodyPessiReportListing">
                </tbody>
                <tfoot>
                    <tr class="info">



                        <td colspan="8" style="font-weight: bold">Total</td>
                         <td class="tdDays" style="text-align: right"></td>
                        <td class="tdHours" style="text-align: right"></td>
                       
                        <td class="tdMonthWedges" style="text-align: right"></td>
                        <td class="tdWagesPaid" style="text-align: right"></td>
                        <td class="tdPessiCont" style="text-align: right"></td>
                        <td  style="text-align: right"></td>


                    </tr>
                </tfoot>
            </table>
        </div>
        <br />
    </div>
    <script type="text/x-jQuery-tmpl" id="PessiReportListing">
        <tr class="trList">
            <td class="project-title"></td>



            <td class="project-title clsEmployeeCode ABC">${EmployeeCode}
                
            </td>

            <td class="project-title clsEmployeeName">${EmployeeName}</td>
            <td class="project-title clsFatherName">${FatherName}</td>
            <td class="project-title clspessi_no">${pessi_no}</td>
            <td class="project-title clsDesignation">${Designation}</td>
            <td class="project-title clsEmployeeType">${EmployeeType}</td>
            <td class="project-title clsCategory">${Category}</td>

            <td class="project-title clsDays" style="text-align: right">${Days}</td>
            <td class="project-title clsHours" style="text-align: right">${Hours}</td>
            <td class="project-title clsMonthWages" style="text-align: right">${MonthWages}</td>
            <td class="project-title clsWagesPaid">${WagesPaid}</td>
            <td class="project-title clsPESSICont">${PESSICont}</td>
            <td class="project-title clsProvinceCode">${Location}</td>
           
        </tr>
    </script>

    <script src="../../js/Page_JS/Rpt_Pessi_Report.js"></script>

</asp:Content>

