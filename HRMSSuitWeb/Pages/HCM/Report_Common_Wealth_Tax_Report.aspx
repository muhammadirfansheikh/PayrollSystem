<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_Common_Wealth_Tax_Report.aspx.cs" Inherits="Pages_HCM_Report_Common_Wealth_Tax_Report" %>
<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
      <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="Common Wealth Tax Report" />
            </h2>
            <ol class="breadcrumb">
             
                <li>
                    <a href="#">Other Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Common Wealth Tax Report" />
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


          

            <div class="col-lg-8 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tblData','Report_Common_Wealth_Tax')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tableCWTListing')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            Common Wealth Tax 
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table id="tblData" class="table table-hover tablecwtListing"  style="width:100%">
                <thead class="theadcwtlisting">
                    <tr>
                        <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    </tr>
                    <tr>
                  
                                <tr class="info">
                                 
                                    <th>Employee Code</th>
                                    <th>Name</th>
                                    <th>CNIC</th>
                                    <th>NTN</th>
                                    <th>Designation</th>
                                    <th>Address</th>
                                    <th>Date Of Birth</th>
                                    <th>Taxable Gross</th>
                                    <th>Taxable Income</th>
                                    <th>Tax Deduction</th>
                                    <th>PF Loan</th>
                                    <th>Company Loan</th>
                                    <th>Loan Markup</th>
                                    <th>Car Installment Deduction</th>
                                    <th>Zakat</th>
                                    <th>Investment</th>
                                    <th>Donation61</th>
                                    <th>Donation</th>
                                    <th>Rebate</th>
                                    <th>Advance Tax</th>
                                    <th>Motor Tax</th>
                                    <th>Mobile Tax</th>
                                    <th>Property Tax</th>
                                    <th>PF Withdrawl</th>
                                    <th>PF Withdraw Date</th>
                                    
                                </tr>
                               
                           
                    </tr>
                   
                </thead>
                 <tbody class="tbodycwtListing">
                                </tbody>
                <tfoot>
                    <tr class="info">
                        <td style="font-weight:bold" colspan="7">Total</td>

                        <td class="tdTaxableGross" style="text-align:right"></td>
                        <td class="tdTaxableIncome" style="text-align:right"></td>
                        <td class="tdTaxDeduction" style="text-align:right"></td>
                        <td class="tdPFLoan" style="text-align:right"></td>
                        <td class="tdCompanyLoan" style="text-align:right"></td>
                        <td class="tdLoanMarkup" style="text-align:right"></td>
                        <td class="tdCarInstallmentDeduction" style="text-align:right"></td>
                        <td class="tdZakat" style="text-align:right"></td>
                        <td class="tdInvestment" style="text-align:right"></td>
                        <td class="tdDonation61" style="text-align:right"></td>
                        <td class="tdDonation" style="text-align:right"></td>
                        <td class="tdRebate" style="text-align:right"></td>
                        <td class="tdAdvanceTax" style="text-align:right"></td>
                        <td class="tdMotorTax" style="text-align:right"></td>
                        <td class="tdMobileTax" style="text-align:right"></td>
                        <td class="tdPropertyTax" style="text-align:right"></td>
                        <td class="tdPFWithdrawl" style="text-align:right"></td>
                        <td></td>

                    </tr>
                </tfoot>
            </table>
        </div>
        <br />
    </div>
    <script type="text/x-jQuery-tmpl" id="cwtListing">
        <tr class="trList">
            
            <td class="project-title clsEmployeeCode">${EmployeeCode}</td>
            <td class="project-title clsName">${Name}</td>
            <td class="project-title clsCNIC">${CNIC}</td>
            <td class="project-title clsNTN">${NTN}</td>
            <td class="project-title clsclsDesignation">${clsDesignation}</td>
            <td class="project-title clsAddress">${Address}</td>
            <td class="project-title clsDateOfBirth">${DateOfBirth}</td>

            <td class="project-title clsTaxableGross" style="text-align:right">${TaxableGross}</td>
            <td class="project-title clsTaxableIncome" style="text-align:right">${TaxableIncome}</td>
            <td class="project-title clsTaxDeduction" style="text-align:right">${TaxDeduction}</td>
            <td class="project-title clsPFLoan" style="text-align:right">${PFLoan}</td>
            <td class="project-title clsCompanyLoan" style="text-align:right">${CompanyLoan}</td>
            <td class="project-title clsLoanMarkup" style="text-align:right">${LoanMarkup}</td>
            <td class="project-title clsCarInstallmentDeduction" style="text-align:right">${CarInstallmentDeduction}</td>
            <td class="project-title clsZakat" style="text-align:right">${Zakat}</td>
            <td class="project-title clsInvestment" style="text-align:right">${Investment}</td>
            <td class="project-title clsDonation61" style="text-align:right">${Donation61}</td>
            <td class="project-title clsDonation" style="text-align:right">${Donation}</td>
            <td class="project-title clsRebate" style="text-align:right">${Rebate}</td>
            <td class="project-title clsAdvanceTax" style="text-align:right">${AdvanceTax}</td>
            <td class="project-title clsMotorTax" style="text-align:right">${MotorTax}</td>
            <td class="project-title clsMobileTax" style="text-align:right">${MobileTax}</td>
            <td class="project-title clsPropertyTax" style="text-align:right">${PropertyTax}</td>
            <td class="project-title clsPFWithdrawl" style="text-align:right">${PFWithdrawl}</td>
            <td class="project-title clsPFWithdrawDate" style="text-align:right">${PFWithdrawDate}</td>
           
        </tr>
    </script>
  
    
    <script src="../../js/Page_JS/Report_Common_Wealth_Tax.js"></script>
   
</asp:Content>

