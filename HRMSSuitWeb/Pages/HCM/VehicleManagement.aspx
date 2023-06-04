<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="VehicleManagement.aspx.cs" Inherits="Pages_HCM_VehicleManagement" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <uc:EmployeeSearchFilters runat="server" ID="EmployeeSearchFilters" />
    <div class="bd">
        
        <div class="row">
            <div class="col-lg-12">
                <div class="wrapper wrapper-content animated fadeInUp">
                    <div class="ibox">

                        <div class="ibox-content">
                            <div class="row m-b-sm m-t-sm" style="margin: 0px;">
                                <div class="col-md-12 panel-default">
                                    <div class="panel-heading" style="padding-top: 0px; padding-bottom: 0px;">
                                        <h2 class="panel-title" style="font-size: x-large; text-align: center;">Employee Detail
                                        </h2>
                                    </div>
                                </div>
                            </div>
                            <div class="project-list">
                                <table class="table table-hover tableEmployee">
                                    <thead>
                                        <tr class="info">

                                            <th>Company</th>

                                            <th>Code</th>
                                            <th>Name</th>
                                            <th>Department</th>
                                            <th>Designation</th>
                                            <th>Location</th>

                                            <th>Official Email</th>
                                            <th>Joining Date</th>

                                            <th>Action</th>
                                        </tr>
                                    </thead>
                                    <tbody class="tbodyEmployeeListing">
                                    </tbody>

                                    <tfoot></tfoot>
                                </table>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div class="modal inmodal" id="CreateProjectModal" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content animated fadeIn" style="width: 200%; margin-left: -50%">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <h4 class="modal-title">Vehicle Management</h4>

                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <div class="panel panel-info mainVehicleInformation">
                                <div class="panel-heading">
                                    Vehicle Information
                                </div>
                                <div class="panel-body" id="panelVehicleInformation">

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Vehicle Type</label>
                                        <select onchange="GetFromSetupDetail(this,HCM_SetupMaster.Manufacturer,'.ddlManufacturer')" class="form-control ddlVehicleType"></select>
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Manufacturer</label>
                                        <select onchange="GetFromSetupDetail(this,HCM_SetupMaster.Manufacturer,'.ddlVehicleName')" class="form-control ddlManufacturer"></select>
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Vehicle Name</label>
                                        <select class="form-control ddlVehicleName"></select>
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Model Year</label>
                                        <input min="0" type="number" class="form-control ModelYear" />
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Horse Power/CC</label>
                                        <input min="0" type="number" class="form-control HP" />
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Chasis Number</label>
                                        <input type="text" class="form-control ChasisNo" />
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Engine Number</label>
                                        <input type="text" class="form-control EngineNo" />
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Registration Number</label>
                                        <input type="text" class="form-control RegNo" />
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Purchase Date</label>
                                        <input type="date" class="form-control PurchaseDate" />
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Allowance Date</label>
                                        <input type="date" class="form-control AllowanceDate" />
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Purchase Amount</label>
                                        <input type="text" class="form-control numeric PurchaseAmount" />
                                    </div>
                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Book Value</label>
                                        <input type="text" class="form-control numeric BookValue" />
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Is Ownership Deduction</label>
                                        <asp:CheckBox ClientIDMode="Static" ID="chkOwnerShip" CssClass="form-control OwnershipDeduction" onclick="toggleDiv('.divInstallment')" runat="server" />
                                    </div>

                                    <div class="col-lg-2 divInstallment">
                                        <label for="exampleIsnputEmail2" class="lblInstallmentAmount">Installment Amount</label>
                                        <input type="text" class="form-control numeric InstallmentAmount" />
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Is Upgraded</label>
                                        <asp:CheckBox ClientIDMode="Static" ID="chkIsDeduction" CssClass="form-control Upgraded" onclick="toggleDiv('.divUpgradeAmount')" runat="server" />
                                    </div>

                                    <div class="col-lg-2 divUpgradeAmount">
                                        <label for="exampleIsnputEmail2" class="lblUpgradeAmount">Upgrade Amount</label>
                                        <input type="text" class="form-control numeric UpgradeAmount" />
                                    </div>

                                </div>
                                <div class="panel-footer">
                                    <input type="button" onclick="InsertVehicleInformation()" class="btn btn-primary" value="Save" />
                                </div>
                            </div>
                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    History
                                </div>
                                <div class="panel-body" style="max-height: 300px; overflow: scroll">
                                    <table class="table table-hover">
                                        <thead>
                                            <tr class="info">

                                                <th>Vehicle</th>

                                                <th>Model</th>
                                                <th>Reg. No.</th>
                                                <th>Chasis No.</th>
                                                <th>Engine No.</th>

                                                <th>Purchase Amount</th>
                                                <th>Installment Amount</th>

                                                <th>Upgrade Amount</th>
                                                <th>Purchase Date</th>
                                                <th>Activity Date</th>

                                            </tr>
                                        </thead>
                                        <tbody class="tbodyVehicleHistory">
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div id="lblerror" class="col-lg-6 danger" visible="false"></div>
                    </div>
                </div>
                <div class="modal-footer">
                </div>
            </div>
        </div>


    </div>



    <script type="text/x-jQuery-tmpl" id="EmployeeListing">
        <tr>
            <td class="project-title">${Company}</td>
            <td class="project-title">${EmployeeCode}</td>
            <td class="project-title">${FirstName} ${LastName}</td>
            <td class="project-title" style="font-size: 10px;">${Department}</td>
            <td class="project-title" style="font-size: 10px;">${Designation}</td>
            <td class="project-title" style="font-size: 10px;">${Location}</td>
            <td class="project-title" style="font-size: 10px;">${OfficalEmail}</td>
            <td class="project-title" style="font-size: 10px;">${JoiningDate}</td>
            <td class="project-title">
                <input type="button" data-toggle="modal" onclick="setEmployeeId('${EmployeeId}')" data-target="#CreateProjectModal" value="Allocate" class="btn btn-success openmodal" />
            </td>
        </tr>
    </script>



    <script type="text/x-jQuery-tmpl" id="VehicleHistory">
        <tr>
            <td class="project-title">${Vehicle}</td>
            <td class="project-title">${Model}</td>
            <td class="project-title">${RegNo}</td>
            <td class="project-title" style="font-size: 10px;">${ChasisNo}</td>
            <td class="project-title" style="font-size: 10px;">${EngineNo}</td>
            <td class="project-title" style="font-size: 10px;">${PurchaseAmount}</td>
            <td class="project-title" style="font-size: 10px;">${InstallmentAmount}</td>
            <td class="project-title" style="font-size: 10px;">${UpgradeAmount}</td>
            <td class="project-title" style="font-size: 10px;">${PurchaseDate}</td>
            <td class="project-title" style="font-size: 10px;">${ActivityDate}</td>
            <input type="hidden" value="${VehicleMasterId}" class="parentId" />
        </tr>
    </script>



    <script src="../../js/Page_JS/VehicleManagement.js"></script>

</asp:Content>

