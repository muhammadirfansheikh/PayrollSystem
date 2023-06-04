<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_Tax_Statement.aspx.cs" Inherits="Pages_HCM_Report_Tax_Statement" %>


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
                <asp:Label runat="server" ID="lbl1" Text="Tax Statement List" />
            </h2>
            <ol class="breadcrumb">
               
                <li>
                    <a href="#">Other Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Tax Statement" />
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
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tableTaxStatementListing','Tax_Statement')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tableTaxStatementListing')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            Car Detail List
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tableTaxStatementListing" id="tableTaxStatementListing">
                <thead class="theadTaxStatementlisting">
                    <tr>
                        <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    </tr>
                    <tr class="info">
                        <th>Sr No</th>
                        <th>id_type</th>
                        <th>NTN</th>
                        <th>CNIC</th>
                        <th>Name</th>
                        <th>Address</th>
                        <th>Pay Nature</th>
                        <th>Pay Sec</th>
                        <th>Ded Date</th>
                        <th>Tax Gross</th>
                        <th>Tax Rate</th>
                        <th>Tax Month</th>
                        <th>Tax Dedyn</th>
                        <th>Net Tax</th>
                       
                    </tr>
                </thead>
                <tbody class="tbodyTaxStatementListing">
                </tbody>
                <tfoot>
                    <tr class="info">



                        <td colspan="9" style="font-weight:bold">Total</td>
                        <td class="tdTaxGross" style="text-align:right"></td>
                        <td class="tdTaxRate" style="text-align:right"></td>
                        <td class="tdTaxMonth" style="text-align:right"></td>
                        <td class="tdTaxDedyn" style="text-align:right"></td>
                        <td class="tdNetTax" style="text-align:right"></td>
                        
                       
                    </tr>
                </tfoot>
            </table>
        </div>
        <br />
    </div>
    <script type="text/x-jQuery-tmpl" id="TaxStatementListing">
        <tr class="trList">
            <td class="project-title">

            </td>

            <td class="project-title clspay_sec ABC">${id_type}</td>
            <td class="project-title clsNTN" style='mso-number-format:"\@";'>${NTN}</td>
            <td class="project-title clsCNIC" style='mso-number-format:"\@";'>${CNIC}</td>
            <td class="project-title clsName">${Name}</td>
   
            <td class="project-title clsAddress">${Address}</td>
            <td class="project-title clspay_nature">${pay_nature}</td>
            <td class="project-title clspay_sec" style="text-align:right">${pay_sec}</td>
            <td class="project-title clsdeddate" >${deddate}</td>
            <td class="project-title clsTaxGross" style="text-align:right">${TaxGross}</td>
            <td class="project-title clstaxrate" style="text-align:right">${taxrate}</td>
            <td class="project-title clsTaxMonth" style="text-align:right">${TaxMonth}</td>
            <td class="project-title clstaxdedyn" style="text-align:right">${taxdedyn}</td>
            <td class="project-title clsNetTax" style="text-align:right">${NetTax}</td>
           
        </tr>
    </script>

    <script src="../../js/Page_JS/Report_Tax_Statement.js"></script>

</asp:Content>

