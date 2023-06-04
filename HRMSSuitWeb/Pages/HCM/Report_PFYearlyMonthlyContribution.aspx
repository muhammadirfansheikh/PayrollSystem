<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_PFYearlyMonthlyContribution.aspx.cs" Inherits="Pages_HCM_Report_PFYearlyMonthlyContribution" %>


<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
     <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="PF Yearly & Monthly Closing" />
            </h2>
            <ol class="breadcrumb">
                <li>
                    <a href="/Pages/Default.aspx">Dashboard</a>
                </li>
                <li>
                    <a href="#">HCM Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="PF Yearly & Monthly Closing" />
                    </strong>
                </li>
            </ol>
        </div>
    </div>

    <uc:EmployeeSearchFilters runat="server" ID="EmployeeSearchFilters"/>

    <div class="panel panel-info">
        <div class="panel-heading">
            Report Functions
        </div>
        <div class="panel-body">
            <div class="col-lg-2 divMonthPayroll">
                <label>Select Month</label>
                <input type="text" class="form-control txtMonthOfPayroll DatePickerMonthComplete" />
            </div>
           <div class="col-lg-10 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelOutWithouHiddenFields('.tablePayData')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tablePayData')" />
            </div>
        </div>
    </div>

    <div class="panel panel-info">
        <div class="panel-heading">
        PF Yearly & Monthly Closing
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tablePayData">
                <thead class="theadPayData">
                    <uc:ReportHeader runat="server" ID="ReportHeader" />
                </thead>
                <tbody class="tbodyPayData">
                </tbody>
            </table>
        </div>
        <br />
    </div>

    <script src="../../js/Page_JS/ReportPFYearlyMonthlyContribution.js"></script>

</asp:Content>
