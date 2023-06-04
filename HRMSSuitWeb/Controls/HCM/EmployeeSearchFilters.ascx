<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmployeeSearchFilters.ascx.cs" Inherits="Controls_HCM_EmployeeSearchFilters" %>

<div class="row" runat="server" id="DivSearchPanel">
    <div class="col-lg-12" style="margin-top: 11px;">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">Search</h3>
            </div>
            <div class="panel-body">
                <input type="hidden" runat="server" id="HasEmployeeCode" class="HasEmployeeCode" value="0" />
                <input type="hidden" runat="server" id="HasEmployeeType" class="HasEmployeeType" value="0" />
                <input type="hidden" runat="server" id="HasLocation" class="HasLocation" value="0" />
                <input type="hidden" runat="server" id="HasCostCenter" class="HasCostCenter" value="0" />
                <input type="hidden" runat="server" id="HasSapCostCenter" class="HasSapCostCenter" value="0" />
                <input type="hidden" runat="server" id="HasBusinessUnit" class="HasBusinessUnit" value="0" />
                <input type="hidden" runat="server" id="HasDepartment" class="HasDepartment" value="0" />
                <input type="hidden" runat="server" id="HasJobCategory" class="HasJobCategory" value="0" />
                <input type="hidden" runat="server" id="HasDesignation" class="HasDesignation" value="0" />
                <input type="hidden" runat="server" id="HasFirstName" class="HasFirstName" value="0" />
                <input type="hidden" runat="server" id="HasLastName" class="HasLastName" value="0" />


                <div class="form-group col-lg-2 GroupDiv">
                    <label>Group</label>
                    <select onchange="Get_Control_Data_EmployeeSearchFilter('OnChangeGroup')" class="form-control ddlGroup">
                    </select>
                </div>
                <div class="form-group col-lg-4 CompanyDiv">
                    <label>Company</label>
                    <select onchange="Get_Control_Data_EmployeeSearchFilter('OnChangeCompany')" class="form-control ddlCompany">
                    </select>
                </div>

                <div class="form-group col-lg-2 EmployeeTypeDiv" style="display: none;">
                    <label>Employee Type</label>
                    <select class="form-control ddlEmployeeType">
                    </select>
                </div>

                <div class="form-group col-lg-2 LocationDiv" style="display: none;">
                    <label>Location</label>
                    <select class="form-control ddlLocation">
                    </select>
                </div>
                <div class="form-group col-lg-2 CostCenterDiv" style="display: none;">
                    <label>Cost Center</label>
                    <select class="form-control ddlCostCenter">
                    </select>
                </div>
                <div class="form-group col-lg-2 SapCostCenterDiv" style="display: none;">
                    <label>Sap Cost</label>
                    <select class="form-control ddlSapCostCenter">
                    </select>
                </div>
                <div class="form-group col-lg-2 BusinessUnitDiv" style="display: none;">
                    <label>Business Unit</label>
                    <select onchange="Get_Control_Data_EmployeeSearchFilter('OnChangeBusinessUnit')" class="form-control ddlBU">
                    </select>
                </div>

                <div class="form-group col-lg-2 DepartmentDiv" style="display: none;">
                    <label>Department</label>
                    <select class="form-control ddlDepartment">
                    </select>
                </div>
                <div class="form-group col-lg-2 CategoryDiv" style="display: none;">
                    <label>Job Category</label>
                    <select class="form-control ddlCategoryC" onchange="Get_Control_Data_EmployeeSearchFilter('OnChangeJobCategory')">
                    </select>
                </div>
                <div class="form-group col-lg-2 DesignationDiv" style="display: none;">
                    <label>Designation</label>
                    <select class="form-control ddlDesignation">
                    </select>
                </div>
                <div class="form-group col-lg-2 EmployeeCodeDiv" style="display: none;">
                    <label>Employee Code</label>
                    <input type="text" class="form-control Alpha numeric txtEmployeeCode" />
                </div>
                <div class="form-group col-lg-2 EmployeeNamediv" style="display: none">
                    <label>Employee Name</label>
                    <input type="text" class="form-control Alpha txtEmployeeName" pattern="[A-Za-z]{1,32}" />
                </div>
                <div class="form-group col-lg-2 FirstNameDiv" style="display: none;">
                    <label>First Name</label>
                    <input type="text" class="form-control Alpha txtFirstName" pattern="[A-Za-z]{1,32}" />
                </div>

                <div class="form-group col-lg-2 LastNameDiv" style="display: none;">
                    <label>Last Name</label>
                    <input type="text" class="form-control Alpha txtLastName" pattern="[A-Za-z]{1,32}" />
                </div>

                <div class="form-group col-lg-12">
                    <input type="button" class="btn btn-default pull-right btnCancelSearch" value="Cancel" onclick="Get_Control_Data_EmployeeSearchFilter('Onload');">
                    <input type="button" onclick="GetEmployee()" class="btn btn-primary pull-right btnSearch m-r-sm" value="Search">
                </div>
            </div>
        </div>
    </div>
</div>
  
<%--<script src="/../js/Control_JS/SearchEmployeeFilter.js"></script>--%> 
<script src="/../js/Control_JS/SearchEmployeeFilter.js"></script> 
<%--<script src="../../js/Control_JS/SearchEmployeeFilter.js"></script> --%>

<script>
    function pageLoad() { 
        TriggerLoad();
        TriggerPageLoads();
    }
</script>
