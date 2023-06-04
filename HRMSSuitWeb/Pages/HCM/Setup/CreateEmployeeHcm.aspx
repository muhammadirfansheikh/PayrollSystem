<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="CreateEmployeeHcm.aspx.cs" Inherits="Pages_HCM_Setup_CreateEmployeeHcm" %>

<%@ Register Src="~/Controls/Shared/InProgress.ascx" TagName="InProgress" TagPrefix="uc2" %>
<%@ Register Src="~/Controls/Shared/PagingAndSorting.ascx" TagPrefix="up" TagName="PagingAndSorting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <uc2:InProgress ID="InProgress1" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Employee
            </h2>
            <ol class="breadcrumb">
                <li>
                    <a href="/Pages/Default.aspx">Dashboard</a>
                </li>
                <li>
                    <a href="#">HCM Setup</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Employee" />
                    </strong>
                </li>
            </ol>
        </div>
    </div>


    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="panel panel-danger" id="div1" runat="server" visible="false">
                <div class="panel-heading" id="Div2" runat="server"></div>
            </div>
            <div class="row" runat="server" id="DivSearchPanel">
                <div class="col-lg-12" style="margin-top: 11px;">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <h3 class="panel-title">Search</h3>
                        </div>
                        <div class="panel-body">
                            <div class="col-lg-12" style="padding-left: 0px; padding-right: 25px;">
                                <div class="form-group col-lg-2">
                                    <label for="exampleInputPassword2">Employee Code</label>
                                    <asp:TextBox ID="txtEmployeeCode" runat="server" CssClass="form-control numeric" AutoCompleteType="Disabled"></asp:TextBox>
                                </div>
                                <div class="form-group col-lg-2">
                                    <label for="exampleInputPassword2">CNIC</label>
                                    <asp:TextBox ID="txtCNIC" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                </div>
                                <div class="form-group col-lg-2">
                                    <label for="exampleInputPassword2">Joining Date</label>
                                    <asp:TextBox ID="txtDateOfJoin" runat="server" CssClass="form-control datetime" AutoCompleteType="Disabled"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-12" style="padding-left: 0px; padding-right: 25px;">
                                <div class="form-group col-lg-2">
                                    <label for="exampleInputEmail2">Group</label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidataor2" runat="server" ValidationGroup="Search" Text="*" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="ddlGroup" InitialValue="0"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ValidationGroup="Search" Text="*" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="ddlGroup" InitialValue=""></asp:RequiredFieldValidator>

                                    <asp:DropDownList ID="ddlGroup" runat="server" CssClass="form-control applyselect2" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>

                                </div>
                                <div class="form-group col-lg-2">
                                    <label for="exampleInputPassword2">Company</label>
                                <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ValidationGroup="Search" Text="*" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="ddlCompany" InitialValue="0"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ValidationGroup="Search" Text="*" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="ddlCompany" InitialValue=""></asp:RequiredFieldValidator>--%>
                                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control applyselect2" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-2">
                                    <label for="exampleInputPassword2">Location</label>
                                    <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control applyselect2">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ValidationGroup="Company" Text="*" ErrorMessage="Location" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="ddlLocation" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-2">
                                    <label for="exampleInputPassword2">Business Unit</label>
                                    <asp:DropDownList ID="ddlBusinessUnit" OnSelectedIndexChanged="ddlBusinessUnit_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control applyselect2"></asp:DropDownList></td>
                                </div>
                                <div class="form-group col-lg-2">
                                    <label for="exampleInputPassword2">Department</label>
                                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control applyselect2"></asp:DropDownList></td>
                                </div>
                                <div class="form-group col-lg-2">
                                    <label for="exampleInputPassword2">Cost Center</label>
                                    <asp:DropDownList ID="ddlCostCenter" runat="server" CssClass="form-control applyselect2"></asp:DropDownList></td>
                                </div>
                            </div>
                            <div class="col-lg-12" style="padding-left: 0px; padding-right: 25px;">
                                <div class="form-group col-lg-2">
                                    <label for="exampleInputPassword2">Job Category</label>
                                    <asp:DropDownList ID="ddlJobCategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlJobCategory_SelectedIndexChanged" CssClass="form-control applyselect2"></asp:DropDownList></td>
                                </div>
                                <div class="form-group col-lg-2">
                                    <label for="exampleInputPassword2">Designation</label>
                                    <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control applyselect2"></asp:DropDownList></td>
                                </div>
                                <div class="form-group col-lg-2">
                                    <label for="exampleInputPassword2">First Name</label>
                                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                </div>
                                <div class="form-group col-lg-2">
                                    <label for="exampleInputPassword2">Middle Name</label>
                                    <asp:TextBox ID="txtMiddleName" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                </div>
                                <div class="form-group col-lg-2">
                                    <label for="exampleInputPassword2">Last Name</label>
                                    <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                </div>

                                <div class="form-group col-lg-2">
                                    <label for="exampleInputPassword2">Is Active</label>
                                    <asp:CheckBox ID="ChbxIsActive" runat="server" Checked="true" CssClass="form-control i-checks" />
                                </div>
                            </div>



                            <div class="col-lg-12" style="padding-left: 0px; padding-right: 25px;">
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default pull-right" OnClick="btnCancel_Click" />
                                <asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="Search" CssClass="btn btn-primary pull-right btnSearch" OnClick="btnSearch_Click" />
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
                                Records
                            </div>
                            <div class="panel-body">
                                <div class="row m-b-sm m-t-sm">
                                    <div class="col-md-12">
                                        <%--<input type="button" class="btn btn-primary openmodal pull-right" data-toggle="modal" data-target="#CreateProjectModal" value="Add" />--%>
                                        <asp:Button ID="btn_Add" runat="server" CssClass="btn btn-primary pull-right" OnClick="btn_Add_Click" Text="Add"></asp:Button>

                                    </div>
                                </div>
                                <div class="project-list">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="project-list">
                                                <table class="table table-hover">
                                                    <thead>

                                                        <tr class="info" style="font-size: x-small;">
                                                            <th style="width: 125px">Company</th>
                                                            <th style="width: 80px">Employee Code</th>
                                                            <th>Name</th>
                                                            <th>Department</th>
                                                            <th>Designation</th>
                                                            <th>Joining Date</th>
                                                            <th style="text-align: center; width: 135px">Action</th>
                                                        </tr>

                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="RptEmployee" runat="server">
                                                            <ItemTemplate>
                                                                <tr style="font-size: x-small;">
                                                                    <td>
                                                                        <input type="hidden" value='<%# Eval("EmployeeId") %>' class="hfEmployeeIdRpt" runat="server" id="hfEmployeeIdRpt" />
                                                                        <strong><%# Eval("Company") %></strong>
                                                                    </td>
                                                                    <td>
                                                                        <strong><%# Eval("EmployeeCode") %></strong>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("Name") %>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("Department") %>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("Designation") %>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("JoiningDate")==null ? "":  Convert.ToDateTime(Eval("JoiningDate")).ToString(Constant.DateFormat) %>
                                                                    </td>
                                                                    <td style="text-align: center">
                                                                        <asp:Button ID="lbEdit" runat="server" CssClass="btn btn-primary" OnClick="btnEdit_Click" Text="Edit"></asp:Button>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                                <up:PagingAndSorting runat="server" ID="PagingAndSorting" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


    <div class="modal inmodal" id="CreateProjectModal" tabindex="-1" role="dialog" aria-hidden="true">

        <div class="modal-dialog" style="width: 95%;">
            <div class="modal-content animated ">

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="modal-header" style="padding-bottom: 9px; padding-top: 9px;">
                            <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                            <h4 class="modal-title">Add / Edit Employee</h4>

                            <input type="hidden" id="hfEmployeeId" runat="server" value="0" />
                        </div>
                        <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>--%>
                        <div class="modal-body" style="padding-bottom: 10px; border-bottom-width: 10px; padding-top: 10px;">
                            <div class="panel" style="margin-bottom: 0px;">
                                <div id="divError" runat="server" visible="false" class="alert alert-warning">
                                    <asp:Label ID="lblError" runat="server"></asp:Label>
                                </div>
                                <asp:Label ID="lblcode" runat="server"></asp:Label>
                                <div class="panel-body MainDiv">
                                    <fieldset>
                                        <legend><strong style="font-size: inherit;">Company Information </strong></legend>
                                        <div class="col-lg-12" style="padding-left: 0px; padding-right: 25px;">
                                            <div class="form-group col-lg-2">
                                                <label for="exampleInputEmail2">Employee Code</label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="Employee Code" ForeColor="Red"
                                                    Display="Dynamic" ControlToValidate="txtEmployeeCodeAdd"></asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtEmployeeCodeAdd" runat="server" CssClass="form-control numeric">
                                                </asp:TextBox>
                                            </div>
                                            <div class="form-group col-lg-2">
                                                <label for="exampleInputPassword2">Joining Date</label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="Joining Date" ForeColor="Red"
                                                    Display="Dynamic" ControlToValidate="txtDateOfJoiningHcm" InitialValue=""></asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtDateOfJoiningHcm" runat="server" CssClass="form-control datetime" />
                                            </div>
                                            <div class="form-group col-lg-2">
                                                <label for="exampleInputEmail2">Employee Type </label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="Employee Type" ForeColor="Red"
                                                    Display="Dynamic" ControlToValidate="ddlEmptype" InitialValue="0"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="Employee Type" ForeColor="Red"
                                                    Display="Dynamic" ControlToValidate="ddlEmptype" InitialValue=""></asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlEmptype" runat="server" CssClass="form-control applyselect2">
                                                </asp:DropDownList>
                                            </div>
                                        </div>


                                        <div class="col-lg-12" style="padding-left: 0px;">
                                            <div class="form-group col-lg-2">
                                                <label for="exampleInputEmail2">Group</label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="Group" ForeColor="Red"
                                                    Display="Dynamic" ControlToValidate="ddlGroupAdd" InitialValue="0"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="Group" ForeColor="Red"
                                                    Display="Dynamic" ControlToValidate="ddlGroupAdd" InitialValue=""></asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlGroupAdd" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlGroupAdd_SelectedIndexChanged" CssClass="form-control ddlAdd applyselect2">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-2">
                                                <label for="exampleInputPassword2">Company</label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="Company" ForeColor="Red"
                                                    Display="Dynamic" ControlToValidate="ddlCompanyAdd" InitialValue="0"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="Company" ForeColor="Red"
                                                    Display="Dynamic" ControlToValidate="ddlCompanyAdd" InitialValue=""></asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlCompanyAdd" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyAdd_SelectedIndexChanged" CssClass="form-control applyselect2 ddlAdd">
                                                </asp:DropDownList>

                                            </div>
                                            <div class="form-group col-lg-2">
                                                <label for="exampleInputPassword2">Location</label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="Location" ForeColor="Red"
                                                    Display="Dynamic" ControlToValidate="ddlLocationAdd" InitialValue="0"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="Location" ForeColor="Red"
                                                    Display="Dynamic" ControlToValidate="ddlLocationAdd" InitialValue=""></asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlLocationAdd" runat="server" CssClass="form-control applyselect2 ddlAdd">
                                                </asp:DropDownList>

                                            </div>
                                            <div class="form-group col-lg-2">
                                                <label for="exampleInputPassword2">Business Unit</label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="Business Unit" ForeColor="Red"
                                                    Display="Dynamic" ControlToValidate="ddlBusinessUnitAdd" InitialValue="0"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="Business Unit" ForeColor="Red"
                                                    Display="Dynamic" ControlToValidate="ddlBusinessUnitAdd" InitialValue=""></asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlBusinessUnitAdd" runat="server" CssClass="form-control applyselect2 ddlAdd" AutoPostBack="true" OnSelectedIndexChanged="ddlBusinessUnitAdd_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-2">
                                                <label for="exampleInputPassword2">Department</label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="Department" ForeColor="Red"
                                                    Display="Dynamic" ControlToValidate="ddlDepartmentAdd" InitialValue="0"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="Department" ForeColor="Red"
                                                    Display="Dynamic" ControlToValidate="ddlDepartmentAdd" InitialValue=""></asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlDepartmentAdd" runat="server" CssClass="form-control applyselect2 ddlAdd">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-2">
                                                <label for="exampleInputPassword2">Cost Center</label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="Cost Center" ForeColor="Red"
                                                    Display="Dynamic" ControlToValidate="ddlCostCenterAdd" InitialValue="0"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="Cost Center" ForeColor="Red"
                                                    Display="Dynamic" ControlToValidate="ddlCostCenterAdd" InitialValue=""></asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlCostCenterAdd" runat="server" CssClass="form-control applyselect2 ddlAdd">
                                                </asp:DropDownList>
                                            </div>

                                        </div>
                                        <div class="col-lg-12" style="padding-left: 0px;">
                                            <div class="form-group col-lg-2">
                                                <label for="exampleInputPassword2">Job Category </label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator128" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="Job Category" ForeColor="Red"
                                                    Display="Dynamic" ControlToValidate="ddlJobCategoryAdd" InitialValue="0"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="Job Category" ForeColor="Red"
                                                    Display="Dynamic" ControlToValidate="ddlJobCategoryAdd" InitialValue=""></asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlJobCategoryAdd" runat="server" CssClass="form-control applyselect2 ddlAdd" AutoPostBack="true" OnSelectedIndexChanged="ddlJobCategoryAdd_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-2">
                                                <label for="exampleInputPassword2">Designation</label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1s9" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="Designation" ForeColor="Red"
                                                    Display="Dynamic" ControlToValidate="ddlDesignationAdd" InitialValue="0"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="Designation" ForeColor="Red"
                                                    Display="Dynamic" ControlToValidate="ddlDesignationAdd" InitialValue=""></asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlDesignationAdd" runat="server" CssClass="form-control applyselect2 ddlAdd">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-2">
                                                <label for="exampleInputEmail2">Group Insurance</label>
                                                <asp:DropDownList ID="ddlGroupInsurance" runat="server" CssClass="form-control applyselect2">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-2">
                                                <label for="exampleInputEmail2">Employee Function</label>
                                                <asp:DropDownList ID="ddlEmpFunc" runat="server" CssClass="form-control applyselect2" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpFunc_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-2">
                                                <label for="exampleInputEmail2">Employee Sub Function</label>
                                                <asp:DropDownList ID="ddlEmployeeSubFunction" runat="server" CssClass="form-control applyselect2">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-2">
                                                <label for="exampleInputEmail2">Att. Allowance Status</label>
                                                <asp:CheckBox ID="chkAttendanceAllowanceStatus" runat="server" CssClass="form-control"></asp:CheckBox>
                                            </div>
                                        </div>

                                        <div class="col-lg-12" style="padding-left: 0px;">
                                            <div class="form-group col-lg-2">
                                                <label for="exampleInputEmail2">EOBI Status</label>
                                                <asp:CheckBox ID="chkEobiStatus" runat="server" CssClass="form-control"></asp:CheckBox>
                                            </div>
                                            <div class="form-group col-lg-2">
                                                <label for="exampleInputEmail2">EOBI No.</label>
                                                <asp:TextBox ID="txtEobiNo" placeholder="EOBI No" runat="server" CssClass="form-control ">
                                                </asp:TextBox>
                                            </div>
                                            <div class="form-group col-lg-2">
                                                <label for="exampleInputEmail2">EOBI Date </label>
                                                <asp:TextBox ID="txtEobiDate" runat="server" CssClass="form-control datetime " />
                                            </div>
                                            <div class="form-group col-lg-2">
                                                <label for="exampleInputEmail2">SESA No.</label>
                                                <asp:TextBox ID="txtSesaNo" placeholder="SESA No" runat="server" CssClass="form-control numeric">
                                                </asp:TextBox>
                                            </div>
                                            <div class="form-group col-lg-2">
                                                <label for="exampleInputEmail2">Allow Intrest</label>
                                                <asp:CheckBox ID="chkAllowIntrest" runat="server" CssClass="form-control"></asp:CheckBox>
                                            </div>
                                            <div class="form-group col-lg-2">
                                                <label for="exampleInputPassword2">SAP Cost Center</label>
                                                <asp:DropDownList ID="ddlSapCostCenter" runat="server" CssClass="form-control applyselect2">
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-lg-12" style="padding-left: 0px;">
                                            <div class="form-group col-lg-2">
                                                <label for="exampleInputPassword2">Confirmation Date </label>
                                                <asp:TextBox runat="server" ID="txtConfirmationdate" CssClass="form-control datetime" />
                                            </div>
                                            <div class="form-group col-lg-2">
                                                <label for="exampleInputEmail2">Contract Start Date </label>
                                                <asp:TextBox runat="server" ID="txtContractstartdate" CssClass="form-control datetime" />
                                            </div>
                                            <div class="form-group col-lg-2">
                                                <label for="exampleInputEmail2">Contract End Date</label>
                                                <asp:CompareValidator ID="CompareValidator2" runat="server"
                                                    ControlToCompare="txtContractstartdate" CultureInvariantValues="true"
                                                    Display="Dynamic" EnableClientScript="true"
                                                    ControlToValidate="txtContractenddate"
                                                    ErrorMessage="Contract Start date must be earlier than End date"
                                                    Type="Date" SetFocusOnError="true" Operator="GreaterThanEqual" ForeColor="Red"
                                                    Text="*"></asp:CompareValidator>
                                                <asp:TextBox runat="server" ID="txtContractenddate" CssClass="form-control txtProbationenddate datetime" />
                                            </div>
                                        </div>

                                    </fieldset>
                                    <fieldset>
                                        <legend><strong style="font-size: inherit;">Personal Information </strong></legend>
                                        <div class="form-group col-lg-2" id="divCNIC" runat="server">
                                            <label for="exampleInputPassword2">CNIC</label>
                                            <asp:RequiredFieldValidator ID="rfvtxtnic" runat="server" ValidationGroup="AddEmployee" Text="*"
                                                ErrorMessage="*" ForeColor="Red"
                                                Display="Dynamic" ControlToValidate="txtcnicadd" CssClass="rfv"></asp:RequiredFieldValidator>
                                            <asp:TextBox ID="txtcnicadd" runat="server" placeholder="CNIC" CssClass="form-control txtAdd numeric" MaxLength="13" />

                                        </div>

                                        <div class="form-group col-lg-2" id="divEmiratesID" runat="server" visible="false">
                                            <label for="exampleInputPassword2">Emirated ID</label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="AddEmployee" Text="*"
                                                ErrorMessage="*" ForeColor="Red"
                                                Display="Dynamic" ControlToValidate="txtEmiratesIDadd" CssClass="rfv"></asp:RequiredFieldValidator>
                                            <asp:TextBox ID="txtEmiratesIDadd" runat="server" placeholder="Emirates ID" CssClass="form-control" MaxLength="15" />
                                        </div>

                                        <div class="form-group col-lg-2">
                                            <label for="exampleInputEmail2">First Name</label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="AddEmployee" Text="*"
                                                ErrorMessage="*" ForeColor="Red"
                                                Display="Dynamic" ControlToValidate="txtFirstNameAdd" CssClass="rfv"></asp:RequiredFieldValidator>
                                            <asp:TextBox ID="txtFirstNameAdd" placeholder="First Name" pattern="[A-Za-z]{1,32}" runat="server" CssClass="form-control txtAdd">
                                            </asp:TextBox>


                                        </div>
                                        <div class="form-group col-lg-2">
                                            <label for="exampleInputPassword2">Middle Name</label>
                                            <asp:TextBox ID="txtMiddleNameAdd" placeholder="Middle Name" pattern="[A-Za-z]{1,32}" runat="server" CssClass="form-control txtAdd">
                                            </asp:TextBox>

                                        </div>
                                        <div class="form-group col-lg-2">
                                            <label for="exampleInputPassword2">Last Name</label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" pattern="[A-Za-z]{1,32}" runat="server" ValidationGroup="AddEmployee" Text="*"
                                                ErrorMessage="*" ForeColor="Red"
                                                Display="Dynamic" ControlToValidate="txtLastNameAdd" CssClass="rfv"></asp:RequiredFieldValidator>
                                            <asp:TextBox ID="txtLastNameAdd" placeholder="Last Name" runat="server" CssClass="form-control txtAdd">
                                            </asp:TextBox>

                                        </div>

                                        <div class="form-group col-lg-2">
                                            <label for="exampleInputPassword2">Date Of Birth</label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="AddEmployee" Text="*"
                                                ErrorMessage="*" ForeColor="Red"
                                                Display="Dynamic" ControlToValidate="txtDOB" CssClass="rfv"></asp:RequiredFieldValidator>
                                            <asp:TextBox runat="server" ID="txtDOB" CssClass="form-control txtAdd datetimedob datetime" />

                                        </div>
                                        <div class="form-group col-lg-2">
                                            <label for="exampleInputPassword2">Gender</label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ValidationGroup="AddEmployee" Text="*" ErrorMessage="Location" ForeColor="Red"
                                                Display="Dynamic" ControlToValidate="ddlGenderAdd" InitialValue="0"></asp:RequiredFieldValidator>
                                            <asp:DropDownList ID="ddlGenderAdd" runat="server" CssClass="form-control ddlAdd applyselect2">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-2">
                                            <label for="exampleInputPassword2">Personal Email</label>

                                            <asp:TextBox ID="txtPersonalEmailAdd" placeholder="Personal Email" runat="server" CssClass="form-control txtAdd" />

                                        </div>
                                        <div class="form-group col-lg-2">
                                            <label for="exampleInputPassword2">Father/Husband Name</label>
                                            <asp:TextBox ID="txtFatherHusbandNameAdd" placeholder="Father/Husband Name" pattern="[A-Za-z ]{1,32}" runat="server" CssClass="form-control txtAdd">
                                            </asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-2">
                                            <label for="exampleInputEmail2">Blood Group</label>
                                            <asp:DropDownList ID="ddlBloodGroup" runat="server" CssClass="form-control applyselect2">
                                            </asp:DropDownList>
                                        </div>


                                        <div class="form-group col-lg-2">
                                            <label for="exampleInputEmail2">Grade Code </label>

                                            <asp:DropDownList ID="ddlGrade" runat="server" CssClass="form-control applyselect2">
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-4">
                                            <label for="exampleInputEmail2">Address </label>

                                            <asp:TextBox ID="txtAddress" placeholder="Address" runat="server" CssClass="form-control txtAddress">
                                            </asp:TextBox>

                                        </div>

                                    </fieldset>
                                    <fieldset>
                                        <legend><strong style="font-size: inherit;">Bank Information </strong></legend>
                                        <div id="divBankForm" runat="server">
                                            <div class="form-group col-lg-2">
                                                <label for="exampleInputEmail2">Bank </label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="AddBank" Text="*" ErrorMessage="Bank" ForeColor="Red"
                                                    Display="Dynamic" ControlToValidate="ddlBank" InitialValue="0"></asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlBankMaster" runat="server" CssClass="form-control applyselect2" AutoPostBack="true" OnSelectedIndexChanged="ddlBankMaster_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group col-lg-2">
                                                <label for="exampleInputEmail2">Branch </label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator45" runat="server" ValidationGroup="AddBank" Text="*" ErrorMessage="Branch" ForeColor="Red"
                                                    Display="Dynamic" ControlToValidate="ddlBank" InitialValue="0"></asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlBank" runat="server" CssClass="form-control applyselect2">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-2">
                                                <label for="exampleInputEmail2">Account No  </label>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="AddBank" Text="*"
                                                    ErrorMessage="Name" ForeColor="Red" Display="Dynamic" ControlToValidate="txtAccountno" CssClass="rfv"></asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtAccountno" placeholder="Account No" runat="server" CssClass="form-control numeric">
                                                </asp:TextBox>
                                            </div>
                                            <div class="form-group col-lg-2">
                                                <label for="exampleInputEmail2">Default Bank  </label>
                                                <asp:CheckBox ID="chkDefault" runat="server" CssClass="form-control"></asp:CheckBox>
                                            </div>

                                            <div class="form-group col-lg-2">
                                                <label for="exampleInputEmail2">Payment Mode  </label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="AddBank" Text="*" ErrorMessage="Payment Mode" ForeColor="Red"
                                                    Display="Dynamic" ControlToValidate="ddlPaymentMode" InitialValue="0"></asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="form-control applyselect2">
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group col-lg-2">
                                                <label for="exampleInputEmail2">Account Type  </label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="AddBank" Text="*" ErrorMessage="Account Type" ForeColor="Red"
                                                    Display="Dynamic" ControlToValidate="ddlAccountType" InitialValue="0"></asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlAccountType" runat="server" CssClass="form-control applyselect2">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                            <label id="lbl" runat="server" class="label label-danger" visible="false"></label>
                        </div>
                        <div class="modal-footer">
                            <%--<asp:Button Text="Save" class="btn btn-primary" ID="btnAdd" OnClick="btnAdd_Click" runat="server" />--%>
                            <%--<asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" UseSubmitBehavior="false" ValidationGroup="AddEmployee" CssClass="btn btn-success pull-right" />--%>
                            <asp:Button Text="Save" class="btn btn-primary" ID="btnAdd" ValidationGroup="AddEmployee" OnClick="btnAdd_Click" runat="server" />
                        </div>

                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
                <%-- <asp:UpdateProgress ID="UpdateProgress11" runat="server">
                    <ProgressTemplate>
                        <uc2:InProgress ID="ucInprogress" runat="server" />
                    </ProgressTemplate>
                </asp:UpdateProgress>--%>
            </div>

        </div>
    </div>


    <script>

        function pageLoad() {


            $('.datetime').datepicker({
                todayBtn: "linked",
                keyboardNavigation: false,
                forceParse: false,
                calendarWeeks: true,
                autoclose: true,
                format: 'mm/dd/yyyy'
            });


            $('.datetime').keydown(function () {
                return false;
            });

            $(".btnAdd").click(function () {
                $('.openmodal').click();
            });
            $('.applyselect2').select2();
        }

        function ClearFields() {
            $(".MainDiv").find("input:text").val('');
            $(".hfEmployeeId").val('0');
        }




        function setEmployeeId(empId) {
            $(".hfEmployeeId").val(empId);
        }


        function ClosePopup() {
            $('.modal').hide();
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }
        function OpenPopup() {
            $('.openmodal').click();
        }
    </script>

</asp:Content>

