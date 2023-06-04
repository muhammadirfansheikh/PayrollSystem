<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_IntrestCalculation.aspx.cs" Inherits="Pages_HCM_Report_IntrestCalculation" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <uc:EmployeeSearchFilters runat="server" ID="EmployeeSearchFilters" />

    <div class="panel panel-info">
        <div class="panel-heading">
            Report Functions
        </div>
        <div class="panel-body">
            <div class="col-lg-2 divMonthPayroll">
                <label>Select Year</label>
                <input type="text" class="form-control txtYear DatePickerYear" />
            </div>

            <div class="col-lg-12">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelOutWithouHiddenFields('.table')" />
                <input type="button" class="btn btn-primary pull-right" value="Print" onclick="sendPrint('.table')" />
            </div>
        </div>
    </div>

    <div class="panel panel-info">
        <div class="panel-heading">
           Intrest Calculation
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover ">
                <thead class="thead">
                    <uc:ReportHeader runat="server" ID="ReportHeader" />
                </thead>
                <tbody class="tbody">
                </tbody>
            </table>
        </div>
    </div>

      <script src="../../js/Page_JS/Report_IntrestCalculation.js"></script>


</asp:Content>

