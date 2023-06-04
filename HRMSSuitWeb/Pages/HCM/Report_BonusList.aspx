<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_BonusList.aspx.cs" Inherits="Pages_HCM_Report_BonusList" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="Bonus List" />
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
                        <asp:Label runat="server" ID="lbl2" Text="Bonus List" />
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
            <div class="col-lg-2" style="margin-top: 30px;">
                <div class="checkbox checkbox-primary checkbox-inline">
                    <input class="chkSepBonus" id="chkSepBonus" type="checkbox" value="SepBonus" />
                    <label for="chkSepBonus">Separate Bonus</label>
                </div>
            </div>

            <div class="col-lg-2 divMonthPayroll1">
                <label>Select Month</label>
                <input type="text" class="form-control txtMonthOfPayroll DatePickerMonthComplete" />
                <input type="text" class="form-control txtMonthOfPayroll_date DatePickerComplete" style="display: none;" />
            </div>

            <div class="col-lg-2 ">
                <label>Bonus </label>
                <select class="form-control  ddlBonus"></select>
            </div>

            <div class="col-lg-2 ">
                <label>Select </label>
                <select class="form-control  ddlGroupBy"></select>
            </div>

            <div class="col-lg-4 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelOutWithouHiddenFields('.tableBonusListing')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tableBonusListing')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            Bonus List
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tableBonusListing">
                <thead class="theadBonuslisting">
                    <tr>
                        <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    </tr>
                    <tr class="info">
                        <th>S No.</th>
                        <th>Company</th>
                        <th>Location</th>
                        <th>Employee Code</th>
                        <th>Name</th>
                        <th>Designation</th>
                        <th>Bonus Amount</th>
                        <th>Appointed Date</th>
                    </tr>
                </thead>
                <tbody class="tbodyBonusListing">
                </tbody>
                <tfoot>
                    <tr class="info">
                        <td colspan="6"></td>
                        <th class="tdBonusAmount"></th>
                        <td></td>
                    </tr>
                </tfoot>
            </table>
        </div>
        <br />
    </div>
    <script type="text/x-jQuery-tmpl" id="BonusListing">
        <tr class="trList">
            <%-- <td class="clsLocationId" style="display: none;">${LocationId}</td>--%>
            <td class="project-title ABC clsSNo">${Sno}
                <input class="clsSapCostCenter" type="hidden" value="${clsSapCostCenter}" />
                <input class="clsCostCenter" type="hidden" value="${clsCostCenter}" />
                <input class="clsLocation" type="hidden" value="${clsLocation}" />
            </td>
            <td class="project-title clsCompany">${CompanyName}</td>
            <td class="project-title clsLocation">${clsLocation}</td>
            <td class="project-title">${EmployeeCode}</td>
            <td class="project-title">${NAME}</td>
            <%--<td class="project-title">${LastName}</td>--%>
            <td class="project-title clsDesignation">${DesignationName}</td>
            <td class="project-title clsBonusAmount">${BonusAmount}</td>
            <td class="project-title">${formatDate(AppointedDate)}</td>

        </tr>
    </script>

    <script src="../../js/Page_JS/Report_BonusList.js"></script>
</asp:Content>




