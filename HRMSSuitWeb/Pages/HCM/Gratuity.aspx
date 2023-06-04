<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Gratuity.aspx.cs" Inherits="Pages_HCM_Gratuity" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <uc:EmployeeSearchFilters runat="server" ID="EmployeeSearchFilters" EmployeeCode="1"/>

    <div class="panel panel-info">
        <div class="panel-heading">
            Functions
        </div>
        <div class="panel-body">
            <div class="col-lg-12">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelOutWithouHiddenFields('.tableMasterSalary')" />
                <input type="button" class="btn btn-primary pull-right" value="Print" onclick="sendPrint('.tableMasterSalary')" />
            </div>
        </div>
    </div>

    <div class="panel panel-info">
        <div class="panel-heading">
            Gratuity
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tableGratuity">
                <thead class="theadGratuity">
                    <uc:ReportHeader runat="server" ID="ReportHeader2" />
                </thead>
                <tbody class="tbodyGratuity">
                </tbody>
            </table>
        </div>
    </div>

     <script src="../../js/Page_JS/Grtuity.js"></script>

</asp:Content>

