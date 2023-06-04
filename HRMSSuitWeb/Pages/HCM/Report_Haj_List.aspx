<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_Haj_List.aspx.cs" Inherits="Pages_HCM_Report_Haj_List" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <uc:EmployeeSearchFilters runat="server" ID="EmployeeSearchFilters" />

    <div class="panel panel-info">
        <div class="panel-heading">
            Report Functions
        </div>
        <div class="panel-body">
            
            <div class="col-lg-2 " style="display: none;">
                <label>Select </label>
                <select class="form-control  ddlGroupBy"></select>
            </div>

            <div class="col-lg-12">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelOutWithouHiddenFields('.tableStaffLoanListing')" />
                <input type="button" class="btn btn-primary pull-right" value="Print" onclick="sendPrint('.tableStaffLoanListing')" />
            </div>
        </div>
    </div>

    <div class="panel panel-info">
        <div class="panel-heading">
            Haj List
        </div>
        <div class="panel-body" style="overflow-x: scroll">

           <table class="table table-hover tableHR">
                <thead class="theadHR">
                    <uc:ReportHeader runat="server" ID="ReportHeader2" />
                </thead>
                <tbody class="tbodyHR">
                </tbody>
            </table>

        </div>
    </div>

    <script src="../../js/Page_JS/Report_HajList.js"></script>

</asp:Content>