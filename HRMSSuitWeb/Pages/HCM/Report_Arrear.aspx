<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_Arrear.aspx.cs" Inherits="Pages_HCM_Report_Arrear" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <uc:EmployeeSearchFilters runat="server" ID="EmployeeSearchFilters" EmployeeCode="1"/>

    <div class="panel panel-info">
        <div class="panel-heading">
            Report Functions
        </div>
        <div class="panel-body">
            <div class="col-lg-2 divMonthPayroll">
                <label>Select Month</label>
                <input type="text" class="form-control txtMonthOfPayroll DatePickerMonthComplete" />
            </div>



            <div class="col-lg-12">

               
                <input type="button" class="btn btn-info pull-right" value="Export " onclick="excelOutWithouHiddenFields('.divSummary')" />

                <input type="button" class="btn btn-primary pull-right" value="Print " onclick="sendPrint('.tableInc')" />
                

            </div>
        </div>
    </div>

    <div class="panel panel-info">
        <div class="panel-heading">
            Arrear 
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tableArr">
                <thead class="theadArr">
                    <uc:ReportHeader runat="server" ID="ReportHeader" />
                </thead>
                <tbody class="tbodyArr">
                </tbody>
            </table>
        </div>
    </div>



    <script src="../../js/Page_JS/Report_Arrear.js"></script>


</asp:Content>
