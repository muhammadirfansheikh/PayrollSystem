<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Rpt_Sessi_Report.aspx.cs" Inherits="Pages_Rpt_Sessi_Report" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
     <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <iframe id="txtArea1" style="display: none"></iframe>
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="Sessi Upload" />
            </h2>
            <ol class="breadcrumb">
               
                <li>
                    <a href="#">Other Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Sessi Upload" />
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
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tableSessiReportListing','Sessi_Report')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tableSessiReportListing')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            Sessi Upload List
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tableSessiReportListing" id="tableSessiReportListing">
                <thead class="theadSessiReportlisting">
                    <tr>
                        <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    </tr>
                    <tr class="info">
                        <th>Sr No</th>
                        <th>Employee Code</th>
                        <th>Employee Name</th>
                        <th>Father Name</th>
                        <th>CNIC</th>
                        <th>Designation</th>
                        <th>Employee Type</th>
                        <th>skill leve</th>
                        <th>Gross</th>
                        <th>Worked Days</th>
                        <th>Month Gross</th>
                        <th>Is Employee Separated</th>
                        <th>Location</th>
                        <th>Province Code</th>
                        <th>Mobile</th>
                        <th>Gender</th>
                       
                    </tr>
                </thead>
                <tbody class="tbodySessiReportListing">
                </tbody>
                <tfoot>
                    <tr class="info">



                        <td colspan="8" style="font-weight:bold">Total</td>
                        <td class="tdGross" style="text-align:right"></td>
                        <td class="tdWorkedDays" style="text-align:right"></td>
                        <td class="tdMonthGross" style="text-align:right"></td>
                        <td colspan="5" style="text-align:right"></td>
                        
                       
                    </tr>
                </tfoot>
            </table>
        </div>
        <br />
    </div>
    <script type="text/x-jQuery-tmpl" id="SessiReportListing">
         <tr class="trList">
          <td class="project-title">
                
            </td>
            


            <td class="project-title clsEmployeeCode ABC">${EmployeeCode}
                
            </td>
         
            <td class="project-title clsEmployeeName">${EmployeeName}</td>
            <td class="project-title clsFatherName">${FatherName}</td>
            <td class="project-title clsCNIC" style='mso-number-format:"\@";'>${CNIC}</td>
            <td class="project-title clsDesignation">${Designation}</td>
            <td class="project-title clsEmployeeType">${EmployeeType}</td>
            <td class="project-title clsskill_leve">${skill_leve}</td>
            
            <td class="project-title clsGross" style="text-align:right">${Gross}</td>
            <td class="project-title clsWorkedDays" style="text-align:right">${WorkedDays}</td>
            <td class="project-title clsMonthGross" style="text-align:right">${MonthGross}</td>
            <td class="project-title clsIsEmployeeSeparated"> ${IsEmployeeSeparated}</td>
            <td class="project-title clsLocation">${Location}</td>
            <td class="project-title clsProvinceCode" >${ProvinceCode}</td>
            <td class="project-title clsMobile">${Mobile}</td>
            <td class="project-title clsGender" >${Gender}</td>
        </tr>
    </script>

    <script src="../../js/Page_JS/Rpt_Sessi_Report.js"></script>

</asp:Content>

