<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="RepairMaintenance.aspx.cs" Inherits="Pages_HCM_RepairMaintenance" %>

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

                    <div class="row">
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

                    </div>

                    <div class="row">

                        <div class="form-group col-lg-2">
                            <label>Fuel (In Litres)</label>
                            <input type="text" class="form-control numeric txtFuel" />
                        </div>

                    </div>

                    <div class="row">

                        <div class="form-group col-lg-2">
                            <label for="exampleInputPassword2">1st Year</label>
                            <input type="text" class="form-control numeric txtFirstYear" />
                        </div>

                        <div class="form-group col-lg-2">
                            <label for="exampleInputPassword2">2nd Year</label>
                            <input type="text" class="form-control numeric txtSecondYear" />
                        </div>

                        <div class="form-group col-lg-2">
                            <label for="exampleInputPassword2">3rd Year</label>
                            <input type="text" class="form-control numeric txtThirdYear" />
                        </div>

                        <div class="form-group col-lg-2">
                            <label for="exampleInputPassword2">4th Year</label>
                            <input type="text" class="form-control numeric txtFourthYear" />
                        </div>

                        <div class="form-group col-lg-2">
                            <label for="exampleInputPassword2">5th Year</label>
                            <input type="text" class="form-control numeric txtFifthYear" />
                        </div>

                        <div class="form-group col-lg-2">
                            <div class="checkbox checkbox-primary">
                                <input id="chkbxOnActual" runat="server" type="checkbox" class="form-control chkbxOnActual" onchange="SetZeroForActual();" value="On Actual" />
                                <label for="chkResSalary">On Actual</label>
                            </div>
                        </div>

                    </div>

                    <div class="form-group col-lg-12">
                        <input type="hidden" data-toggle="modal" data-target="#ManageAddVehicles" class="modalOpenManageVehicle" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default pull-right" />
                        <input type="button" onclick="GetDesignationList()" class="btn btn-primary pull-right btnSearch" value="Search">
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
</asp:Content>

