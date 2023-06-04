<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_EOBI.aspx.cs" Inherits="Pages_HCM_Report_EOBI" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
       <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="EOBI Listing" />
            </h2>
            <ol class="breadcrumb">
              
                <li>
                    <a href="#">HCM Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="EOBI Listing" />
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

            <div class="col-lg-2 ">
                <label>Select </label>
                <select class="form-control  ddlGroupBy"></select>
            </div>

          <div class="col-lg-8 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tablePropotionateListing','EOBI_Listing')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tablePropotionateListing')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            EOBI Listing
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tablePropotionateListing" id="tablePropotionateListing">
                <thead class="theadEOBIlisting">
                    <tr>
                        <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    </tr>
                    <tr class="info">
                        <th>S No. </th>
                        <th>Location </th>
                        <th>EOBI No. </th>
                        <th>Employee Code</th>
                        <th>CNIC</th>
                        <th>Name</th>
                      
                        <th>Father Name</th>
                        <th>Date Of Birth</th>
                        <th>Appointment Date</th>
                        <th>Resigned Date</th>
                        <th>Days Worked</th>
                        <th>Wages Paid</th>
                        <th>Company Contribution</th>
                        <th>Employee Contribution</th>
                        <th>Arrears</th>
                        <th>Total</th>
                    </tr>
                </thead>
                <tbody class="tbodyEOBIListing">
                </tbody>
                <tfoot>
                    <tr class="info">
                        <th colspan="12" style="font-weight:bold">Total</th>
                        <%--<th class="tdWagesPaid">0</th>--%>
                        <th class="tdCompanyContribution" style="text-align:right">0</th>
                        <th class="tdEmployeeContribution" style="text-align:right">0</th>
                        <th class="tdArrear" style="text-align:right">0</th>
                        <th class="tdTotal" style="text-align:right">0</th>
                    </tr>
                </tfoot>
            </table>
        </div>
        <br />
    </div>
    <script type="text/x-jQuery-tmpl" id="EOBIListing">
        <tr class="trList">
            <td class="project-title clsSNo">${SNo}</td>
            <td class="project-title clsLocation ABC">${clsLocation}
                  <input class="clsSapCostCenter" type="hidden" value="${clsSapCostCenter}" />
                <input class="clsCostCenter" type="hidden" value="${clsCostCenter}" />
                <input class="clsLocation" type="hidden" value="${clsLocation}" />
                <input class="clsWagesPaid" type="hidden" value="${WagesPaid}" />
            </td>
            <td class="project-title">${EOBINumber}</td>
            <td class="project-title">${EmployeeCode}</td>
            <td class="project-title">${CNIC}</td>
            <td class="project-title">${Name}</td>
            
            <td class="project-title">${FatherName}</td>
            <td class="project-title">${formatDate(DateOfBirth)     == undefined ? '--' : formatDate(DateOfBirth)}</td>
            <td class="project-title">${formatDate(AppointmentDate) == undefined ? '--' : formatDate(AppointmentDate)}</td>
            <td class="project-title">${formatDate(ResignedDate)    == undefined ? '--' : formatDate(ResignedDate)}</td>
            <td class="project-title">${DaysWorked}</td>
            <td class="project-title clsWagesPaid"> ${WagesPaid}</td>
            <td class="project-title clsCompanyCont" style="text-align:right">${EOBICompany}</td>
            <td class="project-title clsEmployeeCont" style="text-align:right">${EOBIEmployee}</td>
            <td class="project-title clsArrear" style="text-align:right">${Arears}</td>
            <td class="project-title clsTotal" style="text-align:right">${EOBICompany + EOBIEmployee}</td>
        </tr>
    </script>

    <script src="../../js/Page_JS/Report_EOBI.js"></script>
</asp:Content>

