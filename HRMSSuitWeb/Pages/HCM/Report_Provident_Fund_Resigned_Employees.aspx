<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_Provident_Fund_Resigned_Employees.aspx.cs" Inherits="Pages_HCM_Report_Provident_Fund_Resigned_Employees" %>
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
                <asp:Label runat="server" ID="lbl1" Text="Provident Fund Resigned Employee List" />
            </h2>
            <ol class="breadcrumb">
               
                <li>
                    <a href="#">Other Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Provident Fund Resigned Employee List" />
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
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tableProvidentResignedEmployeeListing','Report_Provident_FundResigned_Employee_List')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tableProvidentResignedEmployeeListing')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            Provident Fund Resigned Employees
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tableProvidentResignedEmployeeListing" id="tableProvidentResignedEmployeeListing">
                <thead class="theadCarDetailListlisting">
                    <tr>
                        <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    </tr>
                    <tr class="info">
                        <th>Sr No</th>
                        <th>Code</th>
                        <th>Location</th>
                        <th>Employee Code</th>
                        <th>Employee Name</th>

                        <th>PF Opening</th>
                        <th>Monthly Contribution</th>
                        <th>PF Arrear</th>
                        <th>PF Closing</th>
                        <th>Current PF Interest</th>
     
                        <th>Cummulative PF Interest</th>
                        <th>PF Loan Balance</th>
                        <th>PF Loan Interest</th>
                        <th>Company Loan Balance</th>
                        <th>Company Loan Interest</th>
                        <th>Other Loan Balance</th>
                        <th>Other Loan Interest</th>
                        

                    </tr>
                </thead>
                <tbody class="tbodyProvidentFundResignedListListing">
                </tbody>
                <tfoot>
                    <tr class="info">



                        <td colspan="5" style="font-weight:bold">Total</td>

                        <td class="tdPFOpening" style="text-align:right"></td>
                        <td class="tdMonthlyContribution" style="text-align:right"></td>
                        <td class="tdPFArrear" style="text-align:right"></td>
                        <td class="tdPFClosing" style="text-align:right"></td>
                        <td class="tdCurrentPFInterest" style="text-align:right"></td>
                        <td class="tdCummulativePFInterest" style="text-align:right"></td>
                        <td class="tdPFLoanBalance" style="text-align:right"></td>
                        <td class="tdPFLoanInterest" style="text-align:right"></td>
                        <td class="tdCompanyLoanBalance" style="text-align:right"></td>
                        <td class="tdCompanyLoanInterest" style="text-align:right"></td>
                        <td class="tdOtherLoanBalance" style="text-align:right"></td>
                        <td class="tdOtherLoanInterest" style="text-align:right"></td>
                       
                    </tr>
                </tfoot>
            </table>
        </div>
        <br />
    </div>
    <script type="text/x-jQuery-tmpl" id="ProvidentFundResignedListListing">
        <tr class="trList">
            <td class="project-title">

            </td>

            <td class="project-title clsCode ABC">${Code}</td>
            <td class="project-title clsLocation">${Location}</td>
            <td class="project-title clsEmployeeCode">${EmployeeCode}</td>
            <td class="project-title clsEmployeeName">${EmployeeName}</td>
            <td class="project-title clsPFOpening" style="text-align:right">${PFOpening}</td>
            <td class="project-title clsMonthlyContribution" style="text-align:right">${MonthlyContribution}</td>
            <td class="project-title clsPFArrear" style="text-align:right">${PFArrear}</td>
            <td class="project-title clsPFClosing" style="text-align:right">${PFClosing}</td>
            <td class="project-title clsCurrentPFInterest" style="text-align:right">${CurrentPFInterest}</td>
            <td class="project-title clsCummulativePFInterest" style="text-align:right">${CummulativePFInterest}</td>
            <td class="project-title clsPFLoanBalance" style="text-align:right">${PFLoanBalance}</td>
            <td class="project-title clsPFLoanInterest" style="text-align:right">${PFLoanInterest}</td>
            <td class="project-title clsCompanyLoanBalance" style="text-align:right">${CompanyLoanBalance}</td>
            <td class="project-title clsCompanyLoanInterest" style="text-align:right">${CompanyLoanInterest}</td>
            <td class="project-title clsOtherLoanBalance" style="text-align:right">${OtherLoanBalance}</td>
            <td class="project-title clsOtherLoanInterest" style="text-align:right">${OtherLoanInterest}</td>
            
                    
                        
                        
           

        </tr>
    </script>

    <script src="../../js/Page_JS/Report_Provident_Fund_Resigned_Employee.js"></script>
</asp:Content>

