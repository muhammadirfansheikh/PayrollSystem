<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_CarDetails.aspx.cs" Inherits="Pages_Report_CarDetails" %>
<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
      <iframe id="txtArea1" style="display: none"></iframe>
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="Car Details List" />
            </h2>
            <ol class="breadcrumb">
               
                <li>
                    <a href="#">Other Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Car Details" />
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


            <%--<div class="col-lg-2 ">
                <label>Bank</label>
                <select class="form-control ddlBank" onchange="BindBankBranch();"></select>
            </div>

            <div class="col-lg-2 ">
                <label>Branch</label>
                <select class="form-control ddlBranch"></select>
            </div>--%>

<%--            <div class="col-lg-2 ">
                <label>Select </label>
                <select class="form-control  ddlGroupBy"></select>
            </div>--%>

            <div class="col-lg-3">
                Month
                <input type="text" class="form-control txtFromDate DatePickerMonthComplete txtMonthOfPayroll" />
            </div>

            <div class="col-lg-2">
            </div>
            <div class="col-lg-8 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tableCarDetailListListing','Report_Car_Details_List')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tableCarDetailListListing')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            Car Detail List
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tableCarDetailListListing" id="tableCarDetailListListing">
                <thead class="theadCarDetailListlisting">
                    <tr>
                        <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    </tr>
                    <tr class="info">
                        <th>Sr No</th>
                        <th>Employee Code</th>
                        <th>Name</th>
                        <th>Designation</th>
                        <th>Vehicle Type</th>
                        <th>Registration No</th>
                        <th>Make</th>
                        <th>Model</th>
                        <th>Chasis No</th>
                        <th>Engine No</th>
                        <th>CC</th>
                        <th>Sanction Date</th>
                        <th>Allowance Date</th>

                        <th>Entitled Cost</th>
                        <th>Diffrence</th>
                        <th>Upgraded Cost</th>
                        <th>Written Down Value</th>
                        <th>No Of Installments</th>
                        <th>Installment Amount</th>
                        <th>Installment Received</th>
                        <th>Installment Balance</th>
                        <th>Fuel In Litres</th>
                        <th>Fuel Amount</th>
                        <th>Repair And Maintenance</th>
                      

                    </tr>
                </thead>
                <tbody class="tbodyCarDetailListListing">
                </tbody>
                <tfoot>
                    <tr class="info">



                        <td colspan="13" style="font-weight:bold">Total</td>
                        <td class="tdEntitledCost" style="text-align:right"></td>
                        <td class="tdDiffrence" style="text-align:right"></td>
                        <td class="tdUpgradedCost" style="text-align:right"></td>
                        <td class="tdWrittenDownValue" style="text-align:right"></td>
                        <td class="tdNoOfInstallments" style="text-align:right"></td>
                        <td class="tdInstallmentAmount" style="text-align:right"></td>
                        <td class="tdInstallmentReceived" style="text-align:right"></td>
                        <td class="tdInstallmentBalnc" style="text-align:right"></td>
                        <td class="tdFuelLtrs" style="text-align:right"></td>
                        <td class="tdFuelAmount" style="text-align:right"></td>
                        <td class="tdRepairAndMaintenance" style="text-align:right"></td>
                       
                    </tr>
                </tfoot>
            </table>
        </div>
        <br />
    </div>
    <script type="text/x-jQuery-tmpl" id="CarDetailListListing">
        <tr class="trList">
            <td class="project-title">

            </td>

            <td class="project-title clsEmployeeCode ABC">${EmployeeCode}</td>
            <td class="project-title clsNameOfEmployees">${NameOfEmployees}</td>
            <td class="project-title clsDesignation">${Designation}</td>
            <td class="project-title clsVehicleType">${VehicleType}</td>
            <td class="project-title clsRegistrationNumber">${RegistrationNumber}</td>
            <td class="project-title clsMake">${Make}</td>
            <td class="project-title clsModel">${Model}</td>
            <td class="project-title clsChasisNumber">${ChasisNumber}</td>
            <td class="project-title clsEngineNumber">${EngineNumber}</td>
            <td class="project-title clsCubicCapacity(CC)">${CubicCapacity}</td>
            <td class="project-title clsSanctionDate">${SanctionDate}</td>
            <td class="project-title clsAlowanceDate">${AlowanceDate}</td>


            <td class="project-title clsEntitled" style="text-align:right">${Entitled}</td>
            <td class="project-title clsDifference" style="text-align:right">${Difference}</td>
            <td class="project-title clsUpgradedCost" style="text-align:right">${UpgradedCost}</td>
            <td class="project-title clsWrittenDownValue" style="text-align:right">${WrittenDownValue}</td>
            <td class="project-title clsNoOfInstallments" style="text-align:right">${NoOfInstallments}</td>
            <td class="project-title clsInstallmentAmount" style="text-align:right">${InstallmentAmount}</td>
            <td class="project-title clsInstallmentReceived" style="text-align:right">${InstallmentReceived}</td>
            <td class="project-title clsInstallmentBalance" style="text-align:right">${InstallmentBalance}</td>
            <td class="project-title clsFuelInLitres" style="text-align:right">${FuelInLitres}</td>
            <td class="project-title clsFuelAmount" style="text-align:right">${FuelAmount}</td>
            <td class="project-title clsRepairMaintenance" style="text-align:right">${RepairMaintenance}</td>


          
         

                       
                    
                        
                        
           

        </tr>
    </script>

    <script src="../../js/Page_JS/Report_CarDetail.js"></script>

</asp:Content>

