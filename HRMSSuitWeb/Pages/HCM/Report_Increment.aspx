<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_Increment.aspx.cs" Inherits="Pages_HCM_Report_Increment" %>

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

                <input type="button" class="btn btn-info pull-right" value="Export Detail" onclick="excelOutWithouHiddenFields('.tableInc')" />
                <input type="button" class="btn btn-info pull-right" value="Export Summary" onclick="excelOutWithouHiddenFields('.divSummary')" />

                <input type="button" class="btn btn-primary pull-right" value="Print Detail" onclick="sendPrint('.tableInc')" />
                <input type="button" class="btn btn-primary pull-right" value="Print Summary" onclick="sendPrint('.divSummary')" />

            </div>
        </div>
    </div>

    <div class="panel panel-info">
        <div class="panel-heading">
            Increment 
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tableInc">
                <thead class="theadInc">
                    <uc:ReportHeader runat="server" ID="ReportHeader" />
                </thead>
                <tbody class="tbodyInc">
                </tbody>
            </table>
        </div>
    </div>


    <div class="divSummary">
        <table class="table table-responsive">
            <uc:ReportHeader runat="server" ID="ReportHeader1" />
        </table>
        <div class="col-lg-12">
            <div class="panel panel-default">

                <div class="panel-heading">
                    Increment Summary
                </div>
                <div class="panel-body">

                    <table class="table table-responsive">

                        <tbody class="tbodyIncSum">
                        </tbody>
                        <tfoot>
                        </tfoot>
                    </table>
                </div>
            </div>
        </div>


    </div>

    <script src="../../js/Page_JS/Report_Increment.js"></script>


</asp:Content>

