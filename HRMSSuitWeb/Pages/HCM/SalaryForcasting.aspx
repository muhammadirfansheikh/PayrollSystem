<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="SalaryForcasting.aspx.cs" Inherits="Pages_HCM_SalaryForcasting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <div class="form-group">
        <div class="panel panel-info mainVehicleInformation">
            <div class="panel-heading">
                Manage
            </div>
            <div class="panel-body dvEntry" id="panelVehicleInformation">

                <div class="form-group col-lg-2">
                    <label for="exampleIsnputEmail2">Group</label>
                    <select onchange="GetCompany(this)" class="form-control ddlGroup">
                    </select>
                </div>
                <div class="form-group col-lg-2">
                    <label for="exampleInputPassword2">Company</label>
                    <select onchange="GetIncrementedEmployees()" class="form-control ddlCompany">
                    </select>
                </div>
                <div class="form-group col-lg-2">
                    <label for="exampleInputPassword2">Employee</label>
                    <select class="form-control ddlEmployee">
                    </select>
                </div>

            </div>
            <div class="panel-footer">
                <input type="button" class="btn btn-default " value="Cancel" onclick="ClearFields();" />
                <input type="button" class="btn btn-primary btnSave" onclick="ExecuteSalaryForcast();" value="Execute Salary Forcast" />
              <%--  <input type="button" class="btn btn-success btnView" onclick="" value="View" />--%>
            </div>
        </div>
    </div>


    <script src="../../js/Control_JS/SearchEmployeeFilter.js"></script>
    <script src="../../js/Page_JS/SalaryForcasting.js"></script>

    <script>
        function pageLoad() {
            //alert();
            TriggerLoad();
            TriggerPageLoads();
        }
    </script>

</asp:Content>

