<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_PF_Interest_Allocation_Sheet.aspx.cs" Inherits="Pages_HCM_Report_PF_Interest_Allocation_Sheet" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="PF Interest Allocation Sheet" />
            </h2>
            <ol class="breadcrumb">
                
                <li>
                    <a href="#">Other Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="PF Interest Allocation Sheet" />
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
                <label>Month</label>
                <input type="text" class="form-control txtMonthOfPayroll DatePickerMonthComplete" />
            </div>

            <%--<div class="col-lg-2 ">
                <label>Bank</label>
                <select class="form-control ddlBank" onchange="BindBankBranch();"></select>
            </div>

            <div class="col-lg-2 ">
                <label>Branch</label>
                <select class="form-control ddlBranch"></select>
            </div>--%>
            <div class="col-lg-2 ">
                <label>Rate </label>
                <input type="number" min="0" id="txtPremiumRate" class="txtPremiumRate form-control" />
            </div>
     <%--       <div class="col-lg-2 ">
                <label>Select </label>
                <select class="form-control  ddlGroupBy"></select>
            </div>--%>

            <div class="col-lg-6 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tablePFIASListing','PF_Interest_Allocation_Sheet')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tablePFIASListing')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            PF Interest Allocation Sheet
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tablePFIASListing" id="tablePFIASListing">
                <thead class="theadPFIASlisting">
                    <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    <tr class="info">
                        <th>SR. NO.</th>
                        <th>Employee Code</th>
                        <th>Name</th>
                        <th>Location</th>
                        <th>Department</th>
                        <th>Cost Center</th>


                        <th>Staff Balance</th>
                        <th>Company Balance</th>

                        <th>Previous Interest</th>
                        <th>Total</th>
                        <th>Current Interest</th>
                        <th>Cum Interest</th>



                    </tr>
                </thead>
                <tbody class="tbodyPFIASListing">
                </tbody>
                <tfoot>
                    <tr class="info">
                        <td colspan="3" style="font-weight:bold">Total</td>
                        <th class="tdLocation" style="text-align:right"></th>
                        <th class="tdDepartment" style="text-align:right"></th>
                        <th class="tdCostCenter" style="text-align:right"></th>
                        <th class="tdStaffBalance" style="text-align:right"></th>
                        <th class="tdCompanyBalance" style="text-align:right"></th>
                        <th class="tdPreviousInterest" style="text-align:right"></th>
                        <th class="tdTotal" style="text-align:right"></th>
                        <th class="tdCurrentInterest" style="text-align:right"></th>
                        <th class="tdCumInterest" style="text-align:right"></th>

                    </tr>
                </tfoot>
            </table>
        </div>
        <br />
    </div>
    <script type="text/x-jQuery-tmpl" id="PFIASListing">
        <tr class="trList">
            <td class="project-title">
                
            </td>

            <td class="project-title clsEmployeeCode ABC">${EmployeeCode}</td>
            <td class="project-title clsName">${Name}</td>
            <td class="project-title clsLocation" style="text-align:right">${Location}</td>

            <td class="project-title clsDepartment" style="text-align:right">${Department}</td>
            <td class="project-title clsCostCenter" style="text-align:right">${CostCenter}</td>
            <td class="project-title clsStaffBalance" style="text-align:right">${StaffBalance}</td>
            <td class="project-title clsCompanyBalance" style="text-align:right">${CompanyBalance}</td>
            <td class="project-title clsPreviousInterest" style="text-align:right">${PreviousInterest}</td>
            <td class="project-title clsTotal" style="text-align:right">${Total}</td>
            <td class="project-title clsCurrentInterest" style="text-align:right">${CurrentInterest}</td>
            <td class="project-title clsCumInterest" style="text-align:right">${CumInterest}</td>

            



       






        </tr>
    </script>

    <script src="../../js/Page_JS/Report_PF_Interest_Allocation_Sheet.js"></script>
</asp:Content>

