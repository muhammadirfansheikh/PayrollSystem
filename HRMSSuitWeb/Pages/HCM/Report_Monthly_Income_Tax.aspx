<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_Monthly_Income_Tax.aspx.cs" Inherits="Pages_HCM_Report_Monthly_Income_Tax" %>

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
                <asp:Label runat="server" ID="lbl1" Text="Monthly Income Tax Upload" />
            </h2>
            <ol class="breadcrumb">
               
                <li>
                    <a href="#">Other Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Monthly Income Tax" />
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
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tableMonthlyIncomeTaxListing','Monthly_Income_Tax')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tableMonthlyIncomeTaxListing')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            Car Detail List
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tableMonthlyIncomeTaxListing" id="tableMonthlyIncomeTaxListing">
                <thead class="theadCarDetailListlisting">
                    <tr>
                        <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    </tr>
                    <tr class="info">
                        <%--<th>Sr No</th>--%>
                        <th>Pay Sec</th>
                        <th>NTN</th>
                        <th>CNIC</th>
                        <th>Name</th>
                        <th>City</th>
                        <th>Adderss</th>
                        <th>Status</th>
                        <th>Business</th>
                        <th>Tax Gross</th>
                        <th>Tax Month</th>
                       
                    </tr>
                </thead>
                <tbody class="tbodyMonthlyIncomeTaxListing">
                </tbody>
                <tfoot>
                    <tr class="info">



                        <td colspan="8" style="font-weight:bold">Total</td>
                        <td class="tdTaxGross" style="text-align:right"></td>
                        <td class="tdTaxMonth" style="text-align:right"></td>
                        
                       
                    </tr>
                </tfoot>
            </table>
        </div>
        <br />
    </div>
    <script type="text/x-jQuery-tmpl" id="MonthlyIncomeTaxListing">
        <tr class="trList">
            <%--<td class="project-title">

            </td>--%>

            <td class="project-title clspay_sec ABC">${pay_sec}</td>
            <td class="project-title clsNTN" style='mso-number-format:"\@";'>${NTN}</td>
            <td class="project-title clsCNIC" style='mso-number-format:"\@";'>${CNIC}</td>
            <td class="project-title clsName">${Name}</td>
            <td class="project-title clsCity">${City}</td>
            <td class="project-title clsAddress">${Address}</td>
            <td class="project-title clsStatus">${Status}</td>
            <td class="project-title clsStatus">${Business}</td>
            <td class="project-title clsTaxGross" style="text-align:right">${TaxGross}</td>
            <td class="project-title clsTaxMonth" style="text-align:right">${TaxMonth}</td>
           
        </tr>
    </script>

    <script src="../../js/Page_JS/Report_Monthly_Income_Tax.js"></script>

</asp:Content>

