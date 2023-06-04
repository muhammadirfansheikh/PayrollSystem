<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_IncomeTax.aspx.cs" Inherits="Pages_HCM_Report_IncomeTax" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="Income Tax" />
            </h2>
            <ol class="breadcrumb">
           
                <li>
                    <a href="#">HCM Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Income Tax" />
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

            <%--<div class="col-lg-2 ">
                <label>Bank</label>
                <select class="form-control ddlBank" onchange="BindBankBranch();"></select>
            </div>

            <div class="col-lg-2 ">
                <label>Branch</label>
                <select class="form-control ddlBranch"></select>
            </div>--%>

            <div class="col-lg-2 ">
                <label>Select </label>
                <select class="form-control  ddlGroupBy"></select>
            </div>

            <div class="col-lg-8 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tableIncomeTaxListing','Income_Tax')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tableIncomeTaxListing')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            Income Tax
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tableIncomeTaxListing" id="tableIncomeTaxListing">
                <thead class="theadIncomeTaxlisting">
                    <tr>
                        <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    </tr>
                    <tr class="info">
                        <th>Employee Code</th>
                        <th>Name</th>
                  
                        <th>Designation</th>
                        <th>NTN No.</th>
                        <th>Tax Paid Date</th>
                        <th>Taxable Amount</th>

                        <th>Tax Paid</th>
                        <th>Accumulated Tax</th>
                        

                    </tr>
                </thead>
                <tbody class="tbodyIncomeTaxListing">
                </tbody>
                <tfoot>
                    <tr class="info">
                        <td colspan="5" style="font-weight:bold">Total</td>
                        <th class="tdTotalTaxableIncome" style="text-align:right"></th>
                        
                        <th class="tdTotalTaxPaid" style="text-align:right" ></th>
                        <th class="tdTotalAccTax" style="text-align:right"></th>
                       
                    </tr>
                </tfoot>
            </table>
        </div>
        <br />
    </div>
    <script type="text/x-jQuery-tmpl" id="IncomeTaxListing">
        <tr class="trList">
            <td class="project-title ABC">${EmployeeCode}
                 <input class="clsSapCostCenter" type="hidden" value="${clsSapCostCenter}" />
                <input class="clsCostCenter" type="hidden" value="${clsCostCenter}" />
                <input class="clsLocation" type="hidden" value="${clsLocation}" />
            </td>
            <td class="project-title">${Name}</td>

            <td class="project-title clsDesignation">${DesignationName}</td>
            <td class="project-title">${NTN}</td>
            <td class="project-title">${TaxPaidDate}</td>


            <td class="project-title clsTotalTaxableIncome" style="text-align:right">${TaxableIncome}</td>
        

            <td class="project-title clsTotalTaxPaid" style="text-align:right">${TaxPaid}</td>
            <td class="project-title clsTotalAccumulatedTax" style="text-align:right">${AccumulatedTax}</td>

            
        </tr>
    </script>

    <script src="../../js/Page_JS/Report_IncomeTax.js"></script>
   
</asp:Content>

