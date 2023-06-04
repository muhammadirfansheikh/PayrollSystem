<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="FuelRateMapping.aspx.cs" Inherits="Pages_HCM_FuelRateMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row" runat="server" id="DivSearchPanel">
        <div class="col-lg-12" style="margin-top: 11px;">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title">Search</h3>
                </div>
                <div class="panel-body">

                    <div class="divGroupAndCompany">
                        <div class="form-group col-lg-2">
                            <label for="exampleIsnputEmail2">Group</label>
                            <select onchange="GetCompany(this)" class="form-control ddlGroup">
                            </select>
                        </div>

                        <div class="form-group col-lg-2">
                            <label for="exampleInputPassword2">Company</label>
                            <select onchange="GetCategory(this)" class="form-control ddlCompany">
                            </select>
                        </div>
                    </div>

                    <div class="form-group col-lg-2">
                        <label for="exampleInputPassword2">Category</label>
                        <select onchange="GetDesignation(this)" class="form-control ddlCategory">
                        </select>
                    </div>


                    <div class="form-group col-lg-2">
                        <label for="exampleInputPassword2">Designation</label>
                        <select class="form-control ddlDesignation">
                        </select>
                    </div>

                    <div class="form-group col-lg-2">
                        <label for="exampleInputPassword2">Designation Name</label>
                        <input type="text" class="form-control Alpha  txtDesignationName" />
                    </div>



                    <div class="form-group col-lg-12">
                        <input type="hidden" data-toggle="modal" data-target="#ManageAddVehicles" class="modalOpenManageVehicle" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default pull-right" />
                        <input type="button" onclick="GetDesignationList()" class="btn btn-primary pull-right btnSearch" value="Search">

                        <input type="button" data-toggle="modal" data-target="#divIncrease" class="btn btn-success pull-right btnIncrease" value="Increase">
                    </div>



                </div>
            </div>
        </div>
    </div>




    <div class="row">
        <div class="col-lg-12">
            <div class="wrapper wrapper-content animated fadeInUp">
                <div class="ibox">

                    <div class="ibox-content">
                        <%--  <div class="row m-b-sm m-t-sm" style="margin: 0px;">
                            <div class="col-md-12 panel-default">
                                <div class="panel-heading" style="padding-top: 0px; padding-bottom: 0px;">
                                    <h2 class="panel-title" style="font-size: x-large; text-align: center;">Employee Detail
                                    </h2>
                                </div>
                            </div>
                        </div>--%>
                        <div class="project-list">
                            <table class="table table-hover tableEmployee">
                                <thead>
                                    <tr class="info">

                                        <th>Company</th>
                                        <th>Category</th>
                                        <th>Designation</th>
                                        <th>Fuel (In Litres)</th>
                                        <th>Repair & Maintainance 1st Year</th>
                                        <th>Repair & Maintainance 2nd Year</th>
                                        <th>Repair & Maintainance 3rd Year</th>
                                        <th>Repair & Maintainance 4th Year</th>
                                        <th>Repair & Maintainance 5th Year</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody class="tbodyFuelInformation">
                                </tbody>
                                <tfoot>
                                </tfoot>

                            </table>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Fuel Update - 
                        <label id="lblDesignation"></label>

                    </h4>
                    <small class="font-bold panelSubHeading"></small>

                </div>
                <div class="modal-body">
                    <div class="divFuel">
                        <label>Fuel (In Litres)</label>
                        <input type="text" class="form-control numeric txtFuel" />
                        <input type="hidden" class="form-control numeric hfFuel" />

                        <label>1st Year</label>
                        <input type="text" class="form-control numeric txtFirstYear" />
                        <input type="hidden" class="form-control numeric hfFirstYear" />

                        <label>2nd Year</label>
                        <input type="text" class="form-control numeric txtSecondYear" />
                        <input type="hidden" class="form-control numeric hfSecondYear" />

                        <label>3rd Year</label>
                        <input type="text" class="form-control numeric txtThirdYear" />
                        <input type="hidden" class="form-control numeric hfThirdYear" />

                        <label>4th Year</label>
                        <input type="text" class="form-control numeric txtFourthYear" />
                        <input type="hidden" class="form-control numeric hfFourthYear" />

                        <label>5th Year</label>
                        <input type="text" class="form-control numeric txtFifthYear" />
                        <input type="hidden" class="form-control numeric hfFifthYear" />
                    </div>

                    <div class="checkbox checkbox-primary">
                        <input id="chkbxOnActual" runat="server" type="checkbox" class="form-control chkbxOnActual" onchange="SetZeroForActual();" value="On Actual" />
                        <label for="chkResSalary">On Actual</label>
                    </div>

                </div>
                <div class="modal-footer">
                    <div class="form-group">
                        <input type="button" class="btn btn-primary pull-right" onclick="if(confirm('Are you sure you wants to save?')){saveFuelChanges()}" value="Save Changes" />
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <div id="divIncrease" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Increase Repair Maintenance  
                        <label id="Label1"></label>

                    </h4>
                    <small class="font-bold panelSubHeading"></small>

                </div>
                <div class="modal-body">

                    <div class="row">
                        <div class="form-group col-lg-3">
                            <label for="exampleInputPassword2">Percentage</label>
                            <input type="text" class="form-control numeric txtPercentage" />
                        </div>
                    </div>

                </div>

                <div class="modal-footer">
                    <div class="form-group">
                        <input type="button" class="btn btn-primary pull-right" onclick="if(confirm('Are you sure you wants to save?')){IncreasePercentage()}" value="Save Changes" />
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>




    <script src="../../js/Page_JS/FuelRateMapping.js"></script>
    <script>
        function pageLoad() {
            TriggerLoad()
        }
    </script>
    <script type="text/x-jQuery-tmpl" id="FuelInformation">
        <tr>

            <td class="project-title">${CompanyName}
                <input type="hidden" class="hdnDesignationId" value="${DesignationId}" />
            </td>
            <td class="project-title tdCategoryName" style="font-size: 10px;">${CategoryName}</td>
            <td class="project-title tdDesignationName" style="font-size: 10px;">${DesignationName}</td>

            <td class="project-title FuelProvidedInLitres" style="font-size: 10px;">${FuelInLitres}</td>
            <td class="project-title First" style="font-size: 10px;">${First}</td>
            <td class="project-title Second" style="font-size: 10px;">${Second}</td>
            <td class="project-title Third" style="font-size: 10px;">${Third}</td>
            <td class="project-title Fourth" style="font-size: 10px;">${Fourth}</td>
            <td class="project-title Fifth" style="font-size: 10px;">${Fifth}</td>

            <td class="project-title">
                <input type="button" data-toggle="modal" data-target="#myModal" class="btn btn-sm btn-success" onclick="SetSavedValues(this, '${DesignationName}')" value="Manage" /></td>
        </tr>
    </script>
</asp:Content>

