<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_Tax_Return_2020.aspx.cs" Inherits="Pages_HCM_Report_Tax_Return_2020" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="Tax Return" />
            </h2>
            <ol class="breadcrumb">
                
                <li>
                    <a href="#">Other Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Tax Return" />
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

          
            <div class="col-lg-12 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tableTaxReturnListing','Tax_Return')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tableTaxReturnListing')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
           Tax Return
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tableTaxReturnListing" id="tableTaxReturnListing">
                <thead class="theadTaxReturnlisting">
                    <tr>
                        <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    </tr>
                    <tr class="info">
                        <th>Sr No.</th>
                 
                        <th>NTN</th>
                        <th>Employee Code</th>
                        <th>Name</th>
                        <th>Transaction Date</th>
                        <th>Code</th>
                        
                        <th>Exemp Code</th>
                       
                        <th>Valid Status</th>
                        <th>Charged Salary</th>
                        <th>Tax Deposit</th>
                        

                    </tr>
                </thead>
                <tbody class="tbodyTaxReturnListing">
                </tbody>
                <tfoot>
                    <tr class="info">
                        <td colspan="8" style="font-weight:bold">Total</td>
                        
                         <th class="tdChargedSalary" style="text-align:right"></th>
                        <th class="tdTaxDeposit" style="text-align:right"></th>
                       
                     
                      
                    </tr>
                </tfoot>
            </table>
        </div>
        <br />
    </div>
    <script type="text/x-jQuery-tmpl" id="TaxReturnListing">
        <tr class="trList">
          <td class="project-title">
                
            </td>
            


            <td class="project-title clsNTN ABC" style='mso-number-format:"\@";'>${NTN}</td>
            <td class="project-title clsEmployeeCode">${EmployeeCode}</td>
            <td class="project-title clsName">${Name}</td>
            <td class="project-title clsTransactionDate">${TransactionDate}</td>
            <td class="project-title clsCode">${Code}</td>
            <td class="project-title clsExemp_Code">${Exemp_Code}</td>
            <td class="project-title clsValidStatus">${ValidStatus}</td>
            <td class="project-title clsChargedSalary" style="text-align:right">${ChargedSalary}</td>
            <td class="project-title clsTaxDeposit" style="text-align:right">${TaxDeposit}</td>
        </tr>
    </script>

    <script src="../../js/Page_JS/Report_Tax_Return.js"></script>
</asp:Content>

