<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_ProptionateList.aspx.cs" Inherits="Pages_HCM_Report_ProptionateList" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="Propotionate List" />
            </h2>
            <ol class="breadcrumb">
           
                <li>
                    <a href="#">HCM Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Propotionate List" />
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
            <div class="col-lg-10 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tablePropotionateListing','ProptionateList')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tablePropotionateListing')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            Propotionate List
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tablePropotionateListing" id="tablePropotionateListing">
                <thead class="theadpropotionatelisting">
                    <tr>
                        <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    </tr>
                    <tr class="info">
                        <th>Employee Code</th>
                        <th>Name</th>
                        <th>Total Master Salary</th>
                        <th>Without Pay</th>
                        <th>Flexi Days</th>
                        <th>Total Days In Month</th>
                        <th>Total Gross</th>
                        <th>Difference Amount</th>
                        <th>Remarks</th>
                    </tr>
                </thead>
                <tbody class="tbodypropotionatelisting">
                </tbody>
                <tfoot>
                    <tr class="info">
                        <th>Total Gross Salaries</th>
                        <td class="tdGrossSalary">0.0</td>
                    </tr>
                    <tr class="warning">
                        <th>Proportionate Recovery</th>
                        <td class="tdProportionateRecovery">0.0</td>
                    </tr>
                    <tr class="success">
                        <th>Net Payable</th>
                        <td class="tdNetPayable">0.0</td>
                    </tr>
                    <tr class="success">
                        <th>Total No. Of Employees</th>
                        <td class="tdTotalNumEmployees">0</td>
                    </tr>
                </tfoot>
            </table>
        </div>
        <br />
    </div>

    <script type="text/x-jQuery-tmpl" id="ProportionateListing">
        <tr>
            <td class="project-title">${EmployeeCode}</td>
            <td class="project-title">${Name}</td>
            <td class="project-title tdMasterSalary">${TotalMasterSalary}</td>
            <td class="project-title">${WithoutPay}</td>
            <td class="project-title">${Flexi}</td>
            <td class="project-title">${TotalDaysInMonth}</td>
            <td class="project-title tdGS">${TotalGross}</td>
            <td class="project-title tdDifferenceAmount">${DifferenceAmount}</td>
            <td class="project-title">${Remarks}</td>
        </tr>
    </script>

    <script src="../../js/Page_JS/Report_ProptionateList.js"></script>


</asp:Content>

