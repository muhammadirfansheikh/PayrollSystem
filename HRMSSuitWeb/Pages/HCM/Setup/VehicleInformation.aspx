<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="VehicleInformation.aspx.cs" Inherits="Pages_HCM_Setup_VehicleInformation" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Vehicle Information</h2>
            <ol class="breadcrumb">
                
                <li>
                    <a href="#">HCM Setup</a>
                </li>
                <li class="active">
                    <strong>Vehicle Information</strong>
                </li>
            </ol>
        </div>
    </div>
    <uc:EmployeeSearchFilters runat="server" ID="EmployeeSearchFilters" JobCategory="1"  />

    
    <div class="row">
        <div class="col-lg-12">
            <div class="wrapper wrapper-content animated fadeInUp">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        Records
                    </div>
                    <div class="panel-body">
                        <div class="row m-b-sm m-t-sm">
                            <div class="col-md-12">
                                <input type="button" onclick="VehiclePopup()" value="Vehicle Management" class="btn btn-primary pull-right" data-toggle="modal" data-target="#ManageAddVehicles" />
                            </div>
                        </div>
                        <div class="project-list">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="project-list">
                                        <table class="table table-hover tableEmployee">
                                            <thead>
                                                <tr class="info">
                                                    <th>Company</th>
                                                    <th>Job Category</th>
                                                    
                                                    <th>Vehicle Assigned</th>
                                                    <th>Max. Upgrade Vehicle</th>
                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody class="tbodyCarInformation">
                                            </tbody>
                                            <tfoot></tfoot>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



  
    <div class="modal inmodal" id="ModalDesignationMap" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog" style="width: 90%">
            <div class="modal-content animated fadeIn">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>

                    <h4 class="modal-title titleModalMapping"></h4>

                </div>
                <div class="modal-body" style="height: 400px">
                    <div class="col-xs-6">
                        <div class="panel panel-info">
                            <div class="panel-heading">
                                Eligible Vehicles
                            </div>
                            <div class="panel-body" style="height: 300px; overflow-y: scroll">

                                <div class="col-lg-4">
                                    <label for="exampleIsnputEmail2">Vehicle Type</label>
                                    <select onchange="GetVehicleInfoByVehicleType('ddlVehicleType_')" class="form-control ddlVehicleType_ ddlVehicleType"></select>
                                </div>

                                <div class="col-lg-4">
                                    <label for="exampleIsnputEmail2">Vehicle</label>
                                    <%-- <select onchange="GetVehListFromSetupDetail()" class="form-control ddlVehicle"></select>--%>
                                    <select onchange="" class="form-control ddlVehicleInfo"></select>
                                </div>

                                <div class="row">
                                    <div class="col-lg-12">
                                        <hr />

                                        <%--<input onclick="bindtoTable('.ChkLstElig', '.SelectedEligCarList')" type="button" class="btn btn-primary pull-right" value="Add" />--%>
                                        <input onclick="BindVehicleInGrid('.ddlVehicleInfo', '.SelectedEligCarListNew')" type="button" class="btn btn-primary pull-right" value="Add" />
                                        <div class="col-lg-12">
                                            <hr />
                                            <table class="table table-hover">
                                                <thead>
                                                    <tr class="info">
                                                        <th>Vehicle</th>
                                                        <th>Action</th>
                                                    </tr>
                                                </thead>
                                                <tbody class="SelectedEligCarListNew">
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>

                            </div>

                        </div>
                    </div>


                    <div class="col-xs-6">

                        <div class="panel panel-info">
                            <div class="panel-heading">
                                Max Upgrade Vehicles
                            </div>
                            <div class="panel-body" style="height: 300px; overflow-y: scroll">
                                <div class="col-lg-4">
                                    <label for="exampleIsnputEmail2">Vehicle Type</label>
                                    <select onchange="GetVehicleInfoByVehicleType('ddlVehicleTypeX_')" class="form-control ddlVehicleTypeX_ ddlVehicleTypeX"></select>
                                </div>

                                <div class="col-lg-4">
                                    <label for="exampleIsnputEmail2">Vehicle</label>
                                    <%-- <select onchange="GetVehListFromSetupDetail()" class="form-control ddlVehicle"></select>--%>
                                    <select onchange="" class="form-control ddlVehicleInfoX"></select>
                                </div>

                                <div class="row">
                                    <div class="col-lg-12">
                                        <hr />

                                        <%--<input onclick="bindtoTable('.ChkLstUpgd', '.SelectedUpgCarList')" type="button" class="btn btn-primary pull-right" value="Add" />--%>
                                        <input onclick="BindVehicleInGrid('.ddlVehicleInfoX', '.SelectedUpgCarListNew')" type="button" class="btn btn-primary pull-right" value="Add" />
                                        <div class="col-lg-12">
                                            <hr />
                                            <table class="table table-hover">
                                                <thead>
                                                    <tr class="info">
                                                        <th>Vehicle</th>
                                                        <th>Action</th>
                                                    </tr>
                                                </thead>
                                                <tbody class="SelectedUpgCarListNew">
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>

                    </div>


                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-white" data-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary" onclick="SaveVehicleCategoryMappingNew()">Save changes</button>
                </div>
            </div>
        </div>
    </div>


    <div class="modal inmodal" id="ManageAddVehicles" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog" style="width: 70%">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title titleModalMappingVehicleManagement">Vehicle Management</h4>
                </div>
                <div class="modal-body">
                    <ul class="nav nav-tabs">
                        <li class="active"><a data-toggle="tab" href="#Manufacturer">Manufacturer</a></li>
                        <li><a data-toggle="tab" href="#VehicleTab">Vehicle</a></li>
                        <li><a data-toggle="tab" href="#VehicleVariants">Vehicle Variants</a></li>
                        <li><a data-toggle="tab" href="#Info">Vehicle Information</a></li>
                    </ul>

                    <div class="tab-content">
                        <div id="Manufacturer" class="tab-pane fade in active">
                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    Manufacturer
                                </div>
                                <div class="panel-body">
                                    <div class="divManufacturer">
                                        <div class="col-lg-3">
                                            <label for="exampleIsnputEmail2">Vehicle Category</label>
                                            <select class="form-control ddlVehicleCategoryForManufacture"></select>
                                        </div>
                                        <div class="form-group col-lg-3 ">
                                            <label for="exampleIsnputEmail2">Manufacture Name</label>
                                            <input type="text" class="form-control txtManufacturerName" />
                                        </div>
                                    </div>
                                </div>
                                <div class="panel-footer" style="text-align: right;">
                                    <input type="button" onclick="onSaveManufacturer()" value="Save Changes" class="btn btn-primary" />
                                    <input type="button" onclick="clearManufacturerFields()" value="Reset" class="btn btn-danger" />
                                </div>
                            </div>

                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    Manufacturer
                                </div>
                                <div class="panel-body" style="height: 300px; overflow-y: scroll">
                                    <table class="table table-hover">
                                        <thead>
                                            <tr class="info">
                                                <th>Vehicle Category</th>
                                                <th>Manufacturer Name</th>
                                                
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody class="divManufacturerListing">
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>

                        <div id="VehicleTab" class="tab-pane fade in">
                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    Vehicle
                                </div>
                                <div class="panel-body">
                                    <div class="divVehicle">
                                        <div class="col-lg-3">
                                            <label for="exampleIsnputEmail2">Vehicle Category</label>
                                            <select class="form-control ddlVehicleCategoryForVehicle"></select>
                                        </div>

                                        <div class="form-group col-lg-3 ">
                                            <label for="exampleIsnputEmail2">Manufacture</label>
                                            <select class="form-control ddlManufacturerForVehicle"></select>
                                        </div>

                                        <div class="form-group col-lg-3 ">
                                            <label for="exampleIsnputEmail2">Vehicle Name</label>
                                            <input type="text" class="form-control txtVehicleName" />
                                        </div>

                                    </div>
                                </div>
                                <div class="panel-footer" style="text-align: right;">
                                    <input type="button" onclick="SaveVehicleTabInfo(this)" value="Save Changes" class="btn btn-primary" />
                                    <input type="button" onclick="clearVehicleTabFields()" value="Reset" class="btn btn-danger" />
                                </div>
                            </div>

                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    Vehicle
                                </div>
                                <div class="panel-body" style="height: 300px; overflow-y: scroll">
                                    <table class="table table-hover">
                                        <thead>
                                            <tr class="info">
                                                <th>Vehicle Category</th>
                                                <th>Manufacturer Name</th>
                                                <th>Vehicle Name</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody class="divVehicleListing">
                                        </tbody>
                                    </table>
                                </div>
                            </div>



                        </div>

                        <div id="VehicleVariants" class="tab-pane fade in">
                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    Vehicle Variants
                                </div>
                                <div class="panel-body">
                                    <div class="divVariant">
                                        <div class="col-lg-3 divVariantCategory">
                                            <label for="exampleIsnputEmail2">Vehicle Category</label>
                                            <select class="form-control ddlVehicleCategoryForVariant"></select>
                                        </div>

                                        <div class="form-group col-lg-3 divVariantManufacture">
                                            <label for="exampleIsnputEmail2">Manufacture</label>
                                            <%--<select class="form-control ddlManufacturer" onchange="GetVehicleListing(this)"></select>--%>
                                            <select class="form-control ddlManufacturerForVariant"></select>
                                        </div>

                                        <div class="form-group col-lg-3 ">
                                            <label for="exampleIsnputEmail2">Vehicle</label>
                                            <select class="form-control ddlVehicleForVariant"></select>
                                        </div>

                                        <div class="form-group col-lg-3 ">
                                            <label for="exampleIsnputEmail2">Vehicle Variant Name</label>
                                            <input type="text" class="form-control txtVariant" />
                                        </div>

                                    </div>
                                </div>
                                <div class="panel-footer" style="text-align: right;">

                                    <input type="button" onclick="SaveVariantTabInfo(this)" value="Save Changes" class="btn btn-primary" />
                                    <input type="button" onclick="clearVariantTabFields()" value="Reset" class="btn btn-danger" />

                                </div>
                            </div>

                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    Vehicle Variants
                                </div>
                                <div class="panel-body" style="height: 300px; overflow-y: scroll">
                                    <table class="table table-hover">
                                        <thead>
                                            <tr class="info">
                                                <th>Vehicle Category</th>
                                                <th>Manufacture</th>
                                                <th>Vehicle</th>
                                                <th>Variant Name</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody class="divVariantListing">
                                        </tbody>
                                    </table>
                                </div>
                            </div>

                        </div>

                        <div id="Info" class="tab-pane fade in">
                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    Vehicle Information
                                </div>
                                <div class="panel-body">

                                    <div class="formVehicleDetail">

                                        <div class="form-group col-lg-3">
                                            <label for="exampleIsnputEmail2">Vehicle Category</label>
                                            <select class="form-control ddlVehicleCategoryForVehicleInformation">
                                            </select>
                                        </div>

                                        <div class="form-group col-lg-3">
                                            <label for="exampleIsnputEmail2">Manufacturer</label>
                                            <select class="form-control ddlManufacturerForVehicleInformation" >
                                            </select>
                                        </div>

                                        <div class="form-group col-lg-3 ">
                                            <label for="exampleIsnputEmail2">Vehicle</label>
                                            <select class="form-control ddlVehicleForVehicleInformation" ></select>
                                        </div>

                                        <div class="form-group col-lg-3 ">
                                            <label for="exampleIsnputEmail2">Vehicle Variant</label>
                                            <select class="form-control ddlVariantForVehicleInformation" ></select>
                                        </div>

                                        <div class="form-group col-lg-3 ">
                                            <label for="exampleIsnputEmail2">Fuel Type</label>
                                            <select class="form-control ddlFuelType"></select>
                                        </div>

                                        <div class="form-group col-lg-3">
                                            <label for="exampleIsnputEmail2">Model Year</label>
                                            <input min="0" type="number" class="form-control ModelYear" />
                                        </div>

                                        <div class="form-group col-lg-3">
                                            <label for="exampleIsnputEmail2">Horse Power/CC</label>
                                            <input min="0" type="number" class="form-control HP" />
                                        </div>

                                        <%--<div class="form-group col-lg-3">
                                            <label for="exampleIsnputEmail2">Purchase Value</label>
                                            <input type="text" class="form-control numeric txtPurchaseAmnt" />
                                        </div>

                                        <div class="form-group col-lg-3">
                                            <label for="exampleIsnputEmail2">Book Value</label>
                                            <input type="text" class="form-control numeric txtBookValue" />
                                        </div>--%>
                                    </div>

                                </div>
                                <div class="panel-footer" style="text-align: right;">

                                    <input type="button" class="btn btn-primary" onclick="SaveVehicleInformation()" value="Save Changes" />
                                    <input type="button" class="btn btn-danger" onclick="Clear()" value="Reset" />
                                </div>
                            </div>

                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    Vehicle Information
                                </div>
                                <div class="panel-body" style="height: 300px; overflow-y: scroll">

                                    <table class="table table-hover">
                                        <thead>
                                            <tr class="info">
                                                <th>Vehicle Type</th>
                                                <th>Manufacturer</th>
                                                <th>Vehicle Name</th>
                                                <th>Model Year</th>
                                                <th>HorsePower/CC</th>
                                                <%--  <th>Purchase Amount</th>
                                                <th>Book Value</th>--%>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody class="VehicleListingTBody">
                                        </tbody>
                                    </table>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

 

    <script src="../../../js/Page_JS/VehicleInformation.js"></script>

    <%--<script src="../../../js/Page_JS/VehicleInfo.js"></script>--%>

    <script type="text/x-jQuery-tmpl" id="VehicleDetailListing">
        <tr>
            <td class="project-title">
                <%-- <input type="hidden" class="GroupId" value="${GroupId}" />
                <input type="hidden" class="CompanyId" value="${CompanyId}" />--%>
                <input type="hidden" class="CategoryId" value="${VehicleTypeId}" />
                <input type="hidden" class="ManufacturerId" value="${ManufacturerId}" />
                <input type="hidden" class="VehicleInfoId" value="${VehicleInformationId}" />
                <input type="hidden" class="VariantId" value="${VariantId}" />
                <input type="hidden" class="FuelTypeId" value="${FuelTypeId}" />
                <input type="hidden" class="VehicleId" value="${VehicleNameId}" />
                ${VehicleType}
            </td>

            <td class="project-title Manufacturer">${Manufacturer}</td>
            <td class="project-title Vehicle">${VehicleName} ${Variant} ${FuelType}</td>
            <td class="project-title Model">${ModelYear}</td>
            <td class="project-title HorsePower">${HorsePower}</td>

            <%-- <td class="project-title PurchaseAmount">${PurchaseAmount}</td>
            <td class="project-title BookValue">${BookValue}</td>--%>

            <td class="project-title">
                <input type="button" class="btn btn-primary btn-xs" onclick="onEdit(this)" value="Edit" />
                <input type="button" class="btn btn-danger btn-xs" onclick="if(confirm('Are you sure you wants to delete?')){onDeleteVehicle(this)}" value="Delete" /></td>
        </tr>
    </script>

    <script type="text/x-jQuery-tmpl" id="CarInformation">
        <tr>
            <td class="project-title">${CompanyName}</td>
            <td class="project-title tdCategoryName" style="font-size: 10px;">${CategoryName}</td>
            
            <td class="project-title" style="font-size: 10px;">${Vehicle}</td>
            <td class="project-title" style="font-size: 10px;">${MaxVehicle}</td>
            <td class="project-title">
                <%--<input type="button" data-toggle="modal" data-target="#CreateProjectModal" class="btn btn-sm btn-success" onclick="mangeVehicles(${DesignationId},this)" value="Manage" />--%>

                <input type="button" data-toggle="modal" data-target="#ModalDesignationMap" class="btn btn-sm btn-success" onclick="mangeVehiclesNew(${CategoryId},this)" value="Manage" />

            </td>
        </tr>
    </script>

    <script type="text/x-jQuery-tmpl" id="VehicleList">
        <div class="col-lg-2">
            <div class="checkbox checkbox-success">
                <input class="ChkLstElig" id="ch${Id}" value="${Id}" type="checkbox" />
                <label for="ch${Id}">${Value}</label>
            </div>
        </div>
    </script>

    <script type="text/x-jQuery-tmpl" id="VehicleListX">
        <div class="col-lg-2">
            <div class="checkbox checkbox-warning">
                <input class="ChkLstUpgd" id="xh${Id}" value="${Id}" type="checkbox" />
                <label for="xh${Id}">${Value}</label>
            </div>
        </div>
    </script>

    <script type="text/x-jQuery-tmpl" id="VehicleDesignationMappingElig">
        <tr>
            <td class="project-title">
                <input type="hidden" value="${VehicleId}" />${VehicleName} ${ColumnValue}</td>
            <td class="project-title">
                <button onclick="if(confirm('Are you sure you wants to delete?')){removeRow(this)}" class="btn btn-danger btn-sm" type="button"><i class="fa fa-times"></i></button>
            </td>
        </tr>
    </script>

    <script type="text/x-jQuery-tmpl" id="VehicleDesignationMappingUpg">
        <tr>
            <td class="project-title">
                <input type="hidden" value="${VehicleId}" />${VehicleName} ${ColumnValue}</td>
            <td class="project-title">
                <button onclick="if(confirm('Are you sure you wants to delete?')){removeRow(this)}" class="btn btn-danger btn-sm" type="button"><i class="fa fa-times"></i></button>
            </td>
        </tr>
    </script>

    <script type="text/x-jQuery-tmpl" id="ManufacturerListing">
        <tr>
            <td class="project-title">${ParentName}</td>
            <td class="project-title tdManufacturerName">${Value}</td>
            <td class="project-title">
                <input type="button" class="btn btn-primary btn-xs" onclick="onEditManufacturer(this)" value="Edit" />
                <input type="button" class="btn btn-danger btn-xs" onclick="if(confirm('Are you sure you wants to delete?')){onDeleteManufacturer(this)}" value="Delete" />
            </td>
            <input type="hidden" value="${Id}" class="Manufacturer_SetupDetailId" />
            <input type="hidden" value="${ParentId}" class="Manufacturer_ParentId" />
        </tr>
    </script>

    <script type="text/x-jQuery-tmpl" id="VehicleListing">
        <tr>
            <td class="project-title categorytext">${level1}</td>
            <td class="project-title tdManufacturerName">${ParentName}</td>
            <td class="project-title tdVehicleName">${Value}</td>
            <td class="project-title">
                <input type="button" class="btn btn-primary btn-xs" onclick="EditVehicleTabInfo(this)" value="Edit" />
                <input type="button" class="btn btn-danger btn-xs" onclick="if(confirm('Are you sure you wants to delete?')){DeleteVehicleTabInfo(this)}" value="Delete" />

                <input type="hidden" value="${Id}" class="Vehicle_SetupDetailId" />
                <input type="hidden" value="${ParentId}" class="Vehicle_ParentId" />
               
            </td>
            <%--<input type="hidden" value="${VehicleId}" class="Vehicle_SetupDetailId" />
           <input type="hidden" value="${ManufacturerId}" class="Vehicle_ManufacturerId" />
            <input type="hidden" value="${CategoryId}" class="Vehicle_CategoryId" />--%>
        </tr>
    </script>

    <script type="text/x-jQuery-tmpl" id="VariantListing">
        <tr>
            <td class="project-title tdlevel3">${level3}</td>
            <td class="project-title tdlevel1">${level1}</td>
            <td class="project-title">${ParentName}</td>
            <td class="project-title tdVariantName">${Value}</td>
            <td class="project-title">
                <input type="button" class="btn btn-primary btn-xs" onclick="onEditVariantInfo(this)" value="Edit" />
                <input type="button" class="btn btn-danger btn-xs" onclick="if(confirm('Are you sure you wants to delete?')){DeleteVariantInfo(this)}" value="Delete" />
                <input type="hidden" value="${Id}" class="Variant_SetupDetailId" />
                <input type="hidden" value="${ParentId}" class="Variant_ParentId" />
            </td>
            <%--<input type="hidden" value="${Id}" class="Variant_SetupDetailId" />
           <input type="hidden" value="${ParentId}" class="Variant_ParentId" />--%>
        </tr>
    </script>

    <script type="text/x-jQuery-tmpl" id="VehicleDesignationMappingEligNew">
        <tr>
            <td class="project-title">
                <input type="hidden" value="${VehicleInfoId}" />${Vehicle} </td>
            <td class="project-title">
                <button onclick="if(confirm('Are you sure you wants to delete?')){removeRow(this)}" class="btn btn-danger btn-sm" type="button"><i class="fa fa-times"></i></button>
            </td>
        </tr>
    </script>

    <script type="text/x-jQuery-tmpl" id="VehicleDesignationMappingUpgradeNew">
        <tr>
            <td class="project-title">
                <input type="hidden" value="${VehicleInfoId}" />${Vehicle}</td>
            <td class="project-title">
                <button onclick="if(confirm('Are you sure you wants to delete?')){removeRow(this)}" class="btn btn-danger btn-sm" type="button"><i class="fa fa-times"></i></button>
            </td>
        </tr>
    </script>

    <%--  <script>
        function pageLoad() {          
            //TriggerPageLoads();
        }
    </script>--%>

</asp:Content>

