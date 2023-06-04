<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="GenerateSalary.aspx.cs" Inherits="Pages_HCM_GenerateSalary" %>

<%@ Register Src="~/Controls/Shared/InProgress.ascx" TagPrefix="uc1" TagName="InProgress" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <script src="../../js/Page_JS/GenerateSalary.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Generate Salary</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="/Pages/Default.aspx">Dashboard</a>
                </li>

                <li class="active">
                    <strong>Generate Salary</strong>
                </li>
            </ol>
        </div>
    </div>

    <div class="panel panel-danger" id="divError" runat="server" visible="false">
        <div class="panel-heading" id="lblError" runat="server"></div>
    </div>
    <div class="row" runat="server" id="DivSearchPanel">
        <div class="col-lg-12" style="margin-top: 11px;">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title">Search</h3>
                </div>
                <div class="panel-body">

                    <div class="form-group col-lg-3">
                        <label for="exampleInputEmail2">Company</label>
                        <asp:DropDownList runat="server" ID="ddlCompany" class="form-control ddlCompany" />

                    </div>

                    <div class="form-group col-lg-3">
                        <label for="exampleInputEmail2">Business Unit</label>
                        <asp:DropDownList runat="server" ID="ddlBusinessUnit" class="form-control ddlBusinessUnit" />

                    </div>

                    <div class="form-group col-lg-3">
                        <label for="exampleInputEmail2">Department</label>
                        <asp:DropDownList runat="server" ID="ddlDepartment" class="form-control ddlDepartment" />

                    </div>

                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-lg-12">
            <div class="wrapper wrapper-content animated fadeInUp">

                <div class="panel panel-info">

                    <div class="panel-heading">
                        Employees
                    </div>
                    <div class="panel-body">

                        <div class="project-list">

                            <table class="table table-hover gvEmployees">
                                <thead>
                                    <tr>
                                        <th>Value</th>
                                        <th>Id</th>

                                    </tr>
                                </thead>
                                <tbody>

                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">

        function pageLoad() {

            SetTriggers();
        }

    </script>
</asp:Content>

