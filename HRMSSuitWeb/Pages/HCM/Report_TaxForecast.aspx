<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_TaxForecast.aspx.cs" Inherits="Pages_HCM_Report_TaxForecast" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="/css/print.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="Tax Forecast" />
            </h2>
            <ol class="breadcrumb">
               
                <li>
                    <a href="#">HCM Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Tax Forecast" />
                    </strong>
                </li>
            </ol>
        </div>
    </div>
    <uc:EmployeeSearchFilters runat="server" ID="EmployeeSearchFilters" EmployeeCode="0" />

    <div class="panel panel-info">
        <div class="panel-heading">
            Report Functions
        </div>
        <div class="panel-body">
            <div class="col-lg-2 divMonthPayroll">
                <label>Select Year</label>
                <select class="form-control ddlTaxYear">
                </select>
            </div>
            <div class="col-lg-4" style="margin-top: 20px;">
                <div class="checkbox checkbox-primary">
                    <input id="chkAll" runat="server" type="checkbox" class="form-control chkAll" value="Current Month Allowance Tax" />
                    <label for="chkResSalary">Current Month Allowance Tax</label>
                </div>
            </div>
            <div class="col-lg-6 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tabletaxforecast','Tax_Forecast')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tabletaxforecast')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            Tax Forecast
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tabletaxforecast" id="tabletaxforecast">
                <thead class="theadtaxforecast">
                    <uc:ReportHeader runat="server" ID="ReportHeader2" />
                </thead>
                <tbody class="tbodytaxforecast">
                </tbody>
            </table>
        </div>
        <br />
    </div>

    <script src="../../js/Page_JS/Report_TaxForecast.js"></script>

</asp:Content>

