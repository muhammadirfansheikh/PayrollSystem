<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_CarList.aspx.cs" Inherits="Pages_HCM_Report_CarList" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="Vehicle List" />
            </h2>
            <ol class="breadcrumb">
               
                <li>
                    <a href="#">HCM Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Vehicle List" />
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

            <div class="col-lg-2" >
                <label>Select </label>
                <select class="form-control ddlGroupBy"></select>
            </div>
          <div class="col-lg-10 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tableCarListing','Vehicle_List')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tableCarListing')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
          Vehicle List
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tableCarListing" id="tableCarListing">
                <thead class="theadCarlisting">
                    <tr>
                        <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    </tr>
                    <tr class="info">
                        <th>S No.</th>
                        <th>Code</th>
                        
                        <th>Location</th>
                        <th>Sap Cost Center</th>
                        <th>Cost Center</th>
                        <th>Employee Code</th>
                        <th>Name</th>
                        <th>Designation</th>
                        <th>Department</th>
                        
                        <th>Petrol Amount</th>
                        <th>Maintainance Amount</th>
                        <th>Installment Amount</th>
                        <th>Total Recovery</th>
                        <th>Balance</th>
                    </tr>
                </thead>
                <tbody class="tbodyCarListing">
                </tbody>
                <tfoot>
                    <tr class="info">
                        <td colspan="9" style="font-weight:bold">Total</td>
                        <th class="tdPetrolAmount" style="text-align:right"></th>
                        <th class="tdMaintainanceAmount" style="text-align:right"></th>
                        <th class="tdTotalInstallment" style="text-align:right"></th>
                        <th class="tdTotalRecovery" style="text-align:right"></th>
                        <th class="tdTotalBalance" style="text-align:right"></th>
                    </tr>
                </tfoot>
            </table>
        </div>
        <br />
    </div>
    <script type="text/x-jQuery-tmpl" id="CarListing">
        <tr class="trList">
            <%--  <td class="clsLocationId" style="display: none;">${LocationId}</td>--%>
            <td class="project-title  clsSNo">${Sno}
                   
            </td>
            <td class="project-title clsCode ABC">${Code}

                <input class="clsSapCostCenter" type="hidden" value="${clsSapCostCenter}" />
                <input class="clsCostCenter" type="hidden" value="${clsCostCenter}" />
                <input class="clsLocation" type="hidden" value="${clsLocation}" />
                <input class="clsDepartment" type="hidden" value="${clsDepartment}" />
            </td>
      
            <td class="project-title clsLocation">${clsLocation}</td>
            <td class="project-title clsSapCostCenter">${clsSapCostCenter}</td>
            <td class="project-title clsCostCenter">${clsCostCenter}</td>
            <td class="project-title">${EmployeeCode}</td>
            <td class="project-title">${Name}</td>
            <td class="project-title">${clsDesignation}</td>
            <td class="project-title">${clsDepartment}</td>
            
            <td class="project-title clsPetrolAmount" style="text-align:right">${PetrolAmount}</td>
            <td class="project-title clsMaintanenceAmount" style="text-align:right">${MaintanenceAmount}</td>
            <td class="project-title clsInstallmentAmount" style="text-align:right">${InstallmentAmount}</td>
            <td class="project-title clsTotalRecovery" style="text-align:right">${TotalRecovery}</td>
            <td class="project-title clsBalance" style="text-align:right">${Balance}</td>

        </tr>
    </script>

    <script src="../../js/Page_JS/Report_CarList.js"></script>
</asp:Content>





