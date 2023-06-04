<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_Zakat_Calculate.aspx.cs" Inherits="Pages_HCM_Report_Zakat_Calculate" %>
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
                <asp:Label runat="server" ID="lbl1" Text="Zakat Calculation List" />
            </h2>
            <ol class="breadcrumb">
               
                <li>
                    <a href="#">Other Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Zakat Calculation" />
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


            <%--<div class="col-lg-2 ">
                <label>Bank</label>
                <select class="form-control ddlBank" onchange="BindBankBranch();"></select>
            </div>

            <div class="col-lg-2 ">
                <label>Branch</label>
                <select class="form-control ddlBranch"></select>
            </div>--%>

<%--            <div class="col-lg-2 ">
                <label>Select </label>
                <select class="form-control  ddlGroupBy"></select>
            </div>--%>

            <div class="col-lg-3">
                Month
                <input type="text" class="form-control txtFromDate DatePickerMonthComplete txtMonthOfPayroll" />
            </div>

            <div class="col-lg-2">
            </div>
            <div class="col-lg-8 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tablezakatcalculationListing','Report_Zakat_Calculation_List')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tablezakatcalculationListing')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            Zakat Calculate
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tablezakatcalculationListing" id="tablezakatcalculationListing">
                <thead class="theadCarDetailListlisting">
                    <tr>
                        <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    </tr>
                    <tr class="info">
                        <th>Sr No</th>
                        <th>Employee Code</th>
                        <th>Name</th>
                        <th>Employee Balance</th>
                        <th>Company Balance</th>
                        <th>Interest Income</th>
                        <th>Total</th>
                        <th>Zakat</th>
                        <th>Zakat Till Date</th>
                        
                    </tr>
                </thead>
                <tbody class="tbodyzakatcalculationListing">
                </tbody>
                <tfoot>
                    <tr class="info">



                        <td colspan="3" style="font-weight:bold">Total</td>
                        <td class="tdEmployeeBalance" style="text-align:right"></td>
                        <td class="tdCompanyBalance" style="text-align:right"></td>
                        <td class="tdInterestIncome" style="text-align:right"></td>
                        <td class="tdTotal" style="text-align:right"></td>
                        <td class="tdZakat" style="text-align:right"></td>
                        <td ></td>
                      
                       
                    </tr>
                </tfoot>
            </table>
        </div>
        <br />
    </div>
    <script type="text/x-jQuery-tmpl" id="zakatcalculationListing">
        <tr class="trList">
            <td class="project-title">

            </td>

            <td class="project-title clsEmployeeCode ABC">${EmployeeCode}</td>
            <td class="project-title clsName">${Name}</td>
            <td class="project-title clsEmployeeBalance" style="text-align:right">${EmployeeBalance}</td>
            <td class="project-title clsCompanyBalance" style="text-align:right">${CompanyBalance}</td>
            <td class="project-title clsInterestIncome" style="text-align:right">${InterestIncome}</td>
            <td class="project-title clsTotal" style="text-align:right">${Total}</td>
            <td class="project-title clsZakat" style="text-align:right">${Zakat}</td>
            <td class="project-title clsZakatTillDate">${ZakatTillDate}</td>



            
           

        </tr>
    </script>

    <script src="../../js/Page_JS/Report_zakat_calculation.js"></script>

</asp:Content>

