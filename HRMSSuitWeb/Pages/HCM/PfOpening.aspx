<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="PfOpening.aspx.cs" Inherits="Pages_HCM_PfOpening" %>


<%@ Register Src="~/Controls/Shared/InProgress.ascx" TagName="InProgress" TagPrefix="uc2" %>

<%@ Register Src="~/Controls/Shared/PagingAndSorting.ascx" TagPrefix="up" TagName="PagingAndSorting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style>
        h2:first-letter {
            font-size: 20pt;
        }

        h2 {
            font-size: 16pt;
            font-variant: small-caps;
        }

        tr:nth-child(even) {
            background-color: rgba(20, 134, 136, 0.07);
        }
    </style>

    <script src="../../js/Page_JS/PfOpeningController.js"></script>
    <script src="../../js/Page_JS/PfOpening.js"></script>
    <script src="../../js/jquery.tmpl.min.js"></script>

    <script src="../../js/jquerynumeric.js"></script>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">


    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10" style="height: 65px;">
            <h2>Provident Fund Opening Balance</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="/Pages/Default.aspx">Dashboard</a>
                </li>
                <li class="active">
                    <strong>Provident Fund Opening Balance</strong>
                </li>
            </ol>
        </div>

        <div class="row" runat="server" id="DivSearchPanel">
            <div class="col-lg-12" style="margin-top: 11px;">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <h3 class="panel-title">Search</h3>
                    </div>
                    <div class="panel-body">
                        <div class="col-lg-12">
                            <div class="form-group col-lg-2" style="padding-left: 0px; padding-right: 25px;">
                                <label for="exampleInputPassword2">Employee Code</label>
                                <asp:TextBox ID="txtEmployeeCode" runat="server" CssClass="form-control numeric txtEmployeeCode"></asp:TextBox>
                            </div>

                        </div>
                        <div class="form-group col-lg-2">
                            <label for="exampleInputEmail2">Group</label>
                            <%--<asp:DropDownList ID="ddlGroup" runat="server" CssClass="form-control">
                                    </asp:DropDownList>--%>

                            <select id="ddlGroup" class="form-control ddlGroup"></select>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Company" Text="*" ErrorMessage="Group" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="ddlGroup" InitialValue="0"></asp:RequiredFieldValidator>--%>
                        </div>
                        <div class="form-group col-lg-2">
                            <label for="exampleInputPassword2">Company</label>

                            <select id="ddlCompany" class="form-control ddlCompany"></select>
                            <%--<asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Company" Text="*" ErrorMessage="Company" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="ddlCompany" InitialValue="0"></asp:RequiredFieldValidator>--%>
                        </div>
                        <div class="form-group col-lg-2">
                            <label for="exampleInputPassword2">Location</label>

                            <select id="ddlLocation" class="form-control ddlLocation"></select>
                            <%--<asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="Company" Text="*" ErrorMessage="Location" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="ddlLocation" InitialValue="0"></asp:RequiredFieldValidator>--%>
                        </div>

                        <div class="form-group col-lg-2">
                            <label for="exampleInputPassword2">Business Unit</label>
                            <%--<asp:DropDownList ID="ddlBusinessUnit" runat="server" CssClass="form-control"></asp:DropDownList></td>--%>

                            <select id="ddlBusinessUnit" class="form-control ddlBusinessUnit"></select>
                        </div>
                        <div class="form-group col-lg-2">
                            <label for="exampleInputPassword2">Department</label>
                            <select id="ddlDepartment" class="form-control ddlDepartment"></select>
                            <%-- <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control"></asp:DropDownList></td>--%>
                        </div>
                        <div class="form-group col-lg-2">
                            <label for="exampleInputPassword2">Cost Center</label>
                            <%--<asp:DropDownList ID="ddlCostCenter" runat="server" CssClass="form-control"></asp:DropDownList></td>--%>
                            <select id="ddlCostCenter" class="form-control ddlCostCenter"></select>
                        </div>
                        <div class="form-group col-lg-2">
                            <label for="exampleInputPassword2">Designation</label>
                            <%-- <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control"></asp:DropDownList></td>--%>
                            <select id="ddlDesignation" class="form-control ddlDesignation"></select>
                        </div>
                        <div class="form-group col-lg-2">
                            <label for="exampleInputPassword2">First Name</label>
                            <asp:TextBox ID="txtFirstName" runat="server" pattern="[A-Za-z]{1,32}" CssClass="form-control Alpha txtFirstName"></asp:TextBox>
                        </div>
                        <div class="form-group col-lg-2">
                            <label for="exampleInputPassword2">Last Name</label>
                            <asp:TextBox ID="txtLastName" runat="server" pattern="[A-Za-z]{1,32}" CssClass="form-control Alpha txtLastName"></asp:TextBox>
                        </div>

                        <div class="form-group col-lg-12">
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default pull-right" />
                            <%-- <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary pull-right btnSearch" />--%>


                            <button id="btnSearch" type="button" class="btn btn-primary pull-right btnSearch">Search</button>
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


                                <table class="table table-hover">
                                    <thead>
                                        <tr>
                                            <th>Company</th>
                                            <th>Code</th>
                                            <th>Name</th>
                                            <th>Department</th>
                                            <th>Designation</th>
                                            <th>Location</th>
                                            <th>Opening Balance</th>
                                        </tr>
                                    </thead>

                                    <tbody class="wfForm"></tbody>
                                </table>



                                <%--<div class="wfForm col-lg-12">
                                </div>--%>

                                <button type="button" class="btn btn-primary pull-right btnSave">Save</button>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>



    <script type="text/x-jQuery-tmpl" id="wfForm">

        <tr class="tr">
            <td class='project-title'>${Company} </td>
            <td class='project-title'>${EmployeeCode} </td>

            <td class='project-title'>${FirstName} ${LastName}</td>
            <td class='project-title'>${Department} </td>

            <td class='project-title'>${Designation} </td>
            <td class='project-title'>${Location} </td>
            <td>
                <input type="text" class="form-control numeric col-lg-2 txtOpeningBalance" id="txtOpeningBalance" value="${OpeningBalance}" />
                <input type="hidden" class="hfEmployeeId" id="hfEmployeeId" value="${EmployeeId}" />
                <input type="hidden" class="hfIsEdit" id="hfIsEdit" value="0" />
            </td>
        </tr>



    </script>



    <script type="text/javascript">

        function AlertBox(title, Message, type) {
            swal(title, Message, type);
        }

    </script>

    <script type="text/javascript">

        function pageLoad() {

            SetTriggers();
        }

    </script>
</asp:Content>



