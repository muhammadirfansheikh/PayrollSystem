<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_VehicleDetail.aspx.cs" Inherits="Pages_HCM_Report_VehicleDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-info">
        <div class="panel-heading">
            Vehicle Information
        </div>
        <div class="panel-body">

            <div class="col-lg-2">
                <label>Select Group</label>
                <select class="form-control ddlGroup" onchange="GetCompany(this);"></select>
            </div>

            <div class="col-lg-2">
                <label>Select Company</label>
                <select class="form-control ddlCompany"></select>
            </div>

            <div class="col-lg-12">
                <input type="button" onclick="VehicleDetailReport()" class="btn btn-primary pull-right" value="Search" />
            </div>

        </div>
    </div>

    <div class="panel panel-info">
        <div class="panel-heading">
            Vehicle Detail Report
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover">
                <thead>
                    <tr class="info">
                        <th class="project-title" style="font-size: 10px;">Employee Code</th>
                        <th class="project-title" style="font-size: 10px;">First Name</th>
                        <th class="project-title" style="font-size: 10px;">Last Name</th>
                        <th class="project-title" style="font-size: 10px;">Manufacturer</th>
                        <th class="project-title" style="font-size: 10px;">Vehicle Name</th>
                        <th class="project-title" style="font-size: 10px;">Fuel</th>
                        <th class="project-title" style="font-size: 10px;">Model Year</th>
                        <th class="project-title" style="font-size: 10px;">Horse Power / CC</th>
                        <th class="project-title" style="font-size: 10px;">Purchase Date</th>
                        <th class="project-title" style="font-size: 10px;">Allowance Date</th>
                        <th class="project-title" style="font-size: 10px;">Purchase Amount</th>
                        <th class="project-title" style="font-size: 10px;">Installment Amount</th>
                        <th class="project-title" style="font-size: 10px;">Balance</th>
                        <th class="project-title" style="font-size: 10px;">Written Down Value</th>
                        <th class="project-title" style="font-size: 10px;">Current Status</th>
                        <th class="project-title" style="font-size: 10px;">Ownership Detection</th>
                        <th class="project-title" style="font-size: 10px;">Is Upgraded</th>
                        <th class="project-title" style="font-size: 10px;">Difference Manufacturer</th>
                        <th class="project-title" style="font-size: 10px;">Difference Vehicle</th>
                        <th class="project-title" style="font-size: 10px;">Difference Fuel</th>
                        <th class="project-title" style="font-size: 10px;">Difference ModelYear</th>
                        <th class="project-title" style="font-size: 10px;">Difference HorsePower</th>
                        <th class="project-title" style="font-size: 10px;">Difference VehicleAmount</th>
                        <th class="project-title" style="font-size: 10px;">Difference WrittenDownValue</th>
                    </tr>
                </thead>
                <tbody class="VehicleDetailReport">
                </tbody>
            </table>
        </div>
    </div>



    <script type="text/x-jQuery-tmpl" id="VehicleDetailReport">
        <tr >
            <th class="project-title" style="font-size: 10px;">${EmployeeCode}</th>
            <th class="project-title" style="font-size: 10px;">${FirstName}</th>
            <th class="project-title" style="font-size: 10px;">${LastName}</th>
            <th class="project-title" style="font-size: 10px;">${Manufacturer}</th>
            <th class="project-title" style="font-size: 10px;">${Vehicle}</th>
            <th class="project-title" style="font-size: 10px;">${Fuel}</th>
            <th class="project-title" style="font-size: 10px;">${ModelYear}</th>
            <th class="project-title" style="font-size: 10px;">${HoursePower}</th>
            <th class="project-title" style="font-size: 10px;">${formatDate(PurchaseDate)}</th>
            <th class="project-title" style="font-size: 10px;">${formatDate(AllowanceDate)}</th>
            <th class="project-title" style="font-size: 10px;">${PurchaseAmount}</th>
            <th class="project-title" style="font-size: 10px;">${InstallmentAmount}</th>
            <th class="project-title" style="font-size: 10px;">${Balance}</th>
            <th class="project-title" style="font-size: 10px;">${WrittenDownValue}</th>
            <th class="project-title" style="font-size: 10px;">${CurrentStatus}</th>
            <th class="project-title" style="font-size: 10px;">${OwnershipDetection}</th>
            <th class="project-title upgradecols" style="font-size: 10px;">${IsUpgraded}</th>
            <th class="project-title upgradecols" style="font-size: 10px;">${DifferenceManufacturer}</th>
            <th class="project-title upgradecols" style="font-size: 10px;">${DifferenceVehicle}</th>
            <th class="project-title upgradecols" style="font-size: 10px;">${DifferenceFuel}</th>
            <th class="project-title upgradecols" style="font-size: 10px;">${DifferenceModelYear}</th>
            <th class="project-title upgradecols" style="font-size: 10px;">${DifferenceHorsePower}</th>
            <th class="project-title upgradecols" style="font-size: 10px;">${DifferenceVehicleAmount}</th>
            <th class="project-title upgradecols" style="font-size: 10px;">${DifferenceWrittenDownValue}</th>
        </tr>
    </script>

    <script src="../../js/Page_JS/Report_VehicleDeduction.js"></script>


    <script>
        function pageLoad() {
            TriggerLoad();
        }
    </script>


    <style>
        .upgradecols{
            background-color:#efefef !important;
        }
    </style>

</asp:Content>



