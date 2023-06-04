<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="SetupFormula.aspx.cs" Inherits="Pages_HCM_Setup_SetupFormula"
    ValidateRequest="true" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../../../js/jquerynumeric.js"></script>

    <style>
        .abc {
            max-height: 500px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Formula Definition</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="/Pages/Default.aspx">Dashboard</a>
                </li>
                <li>
                    <a href="#">Setup</a>
                </li>
                <li class="active">
                    <strong>Formula Definition</strong>
                </li>
            </ol>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
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
                                <asp:DropDownList runat="server" ID="ddlCompanySearch" class="form-control ddlCompany" OnSelectedIndexChanged="ddlCompanySearch_SelectedIndexChanged" AutoPostBack="true" />

                            </div>
                            
                            <div class="form-group col-lg-3">
                                <label for="exampleInputEmail2">Allowance / Deduction</label>
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>

                            </div>

                            <div class="col-lg-3">
                                  <label for="exampleInputEmail2">Deduction</label>
                                 <asp:CheckBox ID="chkIsDeductionSearch" runat="server" Text="" />
                            </div>

                            <%-- <div class="form-group col-lg-3">
                                <label for="exampleInputEmail2">Formula</label>
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>

                            </div>--%>
                            <div class="form-group col-lg-12">
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default pull-right" OnClick="btnCancel_Click" />
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary pull-right" OnClick="btnSearch_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-12">
                    <div class="wrapper wrapper-content animated fadeInUp">

                        <div class="panel panel-info">
                            <%--<div class="ibox-title">
                        <h5></h5>
                        <div class="ibox-tools">

                            <a href="" class="btn btn-primary btn-xs btnAdd" data-toggle="modal" data-target="#CreateProjectModal">Create new project</a>
                        </div>
                    </div>--%>
                            <div class="panel-heading">
                                Formula Type
                            </div>
                            <div class="panel-body">
                                <div class="row m-b-sm m-t-sm">
                                    <div class="col-md-12">
                                        <a href="#" class="btn btn-primary btnAdd pull-right">Add new </a>
                                        <input type="button" data-toggle="modal" data-target="#CreateProjectModal" class="openmodal" style="display: none;" />
                                    </div>
                                </div>
                                <div class="project-list">

                                    <table class="table table-hover">
                                        <thead>
                                            <tr>
                                                <th>Company</th>
                                                <th>Allowance</th>
                                                <th>Type</th>
                                                <th>Formula</th>

                                                <th style="width: 170px; text-align: center;">Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rpt" runat="server">

                                                <%-- <asp:DataList ID="rpt" runat="server" RepeatDirection="Horizontal" RepeatColumns="8" RepeatLayout="Flow">--%>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="project-title">

                                                            <input type="hidden" runat="server" id="hfId" class="hfId" value='<%# Eval("ID") %>' />
                                                              <input type="hidden" runat="server" id="hfFormulaId" class="hfId" value='<%# Eval("FormulaId") %>' />

                                                            <%# Eval("Company") %></a>

                                                        </td>

                                                        <td class="project-title">
                                                            <%# Eval("Allowance") %></a>
                                                           
                                                           
                                                        </td>

                                                        <td>


                                                            <%# Eval("Type") %>
                                                        
                                                        </td>

                                                        <td>


                                                            <%# Eval("Formula") %>
                                                        
                                                        </td>

                                                        <td class="project-actions">

                                                             <asp:LinkButton ID="lbEdit"
                                                                runat="server"
                                                                CssClass="btn btn-primary"
                                                                OnClick="lbEdit_Click"><span aria-hidden="true" class="fa fa-edit"></span>Edit
                                                            </asp:LinkButton>

                                                            <asp:LinkButton ID="lbDelete"
                                                                runat="server"
                                                                CssClass="btn btn-danger"
                                                                OnClick="lbDelete_Click"><span aria-hidden="true" class="fa fa-trash"></span>Delete
                                                            </asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>

                                                <%--  </asp:DataList>--%>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


        </ContentTemplate>

    </asp:UpdatePanel>
    <%-- Create Project Modal End--%>
    <div class="modal inmodal" id="CreateProjectModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content animated flipInY">
                <div class="modal-header" style="padding-bottom: 9px; padding-top: 9px;">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">Add New Formula</h4>
                    <%--<small class="font-bold">Lorem Ipsum is simply dummy text of the printing and typesetting industry.</small>--%>
                </div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="modal-body abc" style="padding-bottom: 10px; border-bottom-width: 10px; padding-top: 10px; overflow-y: scroll;">
                            <input type="hidden" id="hfModalId" runat="server" class="hfModalId" />
                            <div id="div2" runat="server" visible="false" class="alert alert-warning">
                            </div>
                            <div class="form-group">

                                <div class="form-group col-lg-6">
                                    <label for="exampleInputEmail2">Company</label>
                                    <asp:DropDownList runat="server" ID="ddlCompany" class="form-control ddlCompany" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" />

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Save" Text="*" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="ddlCompany" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-6">
                                    <label for="exampleInputEmail2">Formula</label>
                                    <asp:CheckBox ID="chkbxFormula" runat="server" CssClass="form-control chkbxFormula" Checked="true" />

                                </div>

                                <div class="form-group col-lg-6">
                                    <label for="exampleInputEmail2">Allowance / Deduction</label>
                                    <asp:TextBox runat="server" placeholder="Allowance / Deduction" ID="txtAllowanceDeduction" class="form-control" />

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="Save" Text="*" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="txtAllowanceDeduction"></asp:RequiredFieldValidator>

                                </div>

                                <div class="form-group col-lg-6">
                                    <label for="exampleInputEmail2">Deduction</label>
                                    <asp:CheckBox ID="chkIsDeduction" runat="server" CssClass="form-control" />

                                </div>



                                <%--<div class="form-group col-lg-6">
                                    <label for="exampleInputEmail2">Formula Type</label>
                                    <asp:DropDownList runat="server" ID="ddlFormulaType" class="form-control ddlCompany" />

                                </div>--%>

                                <div id="dvFormula" runat="server" visible="true">

                                    <div class="form-group col-lg-6">
                                        <label for="exampleInputEmail2">Type</label>
                                        <asp:DropDownList runat="server" ID="ddlType" class="form-control ddlCompany" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" />

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="Add" Text="*" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="ddlType" InitialValue="0"></asp:RequiredFieldValidator>

                                    </div>

                                    <div class="form-group col-lg-6" id="dvAllowanceDeduction" runat="server" visible="false">


                                        <div id="dvlblAllowanceDeduction" runat="server">
                                            <label for="exampleInputEmail2">Allowance / Deduction</label>
                                        </div>
                                        <asp:DropDownList runat="server" ID="ddlAllowanceDeduction" class="form-control" />

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Add" Text="*" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="ddlAllowanceDeduction" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-6" id="dvOperatorConstant" runat="server" visible="false">


                                        <div id="dvlblOperatorConstant" runat="server">
                                            <label for="exampleInputEmail2">Constant</label>
                                        </div>
                                        <asp:TextBox runat="server" placeholder="Constant" ID="txtOperatorConstant" CssClass="form-control numeric" />

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Add" Text="*" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtOperatorConstant"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-12">

                                        <asp:Label ID="lblFormula" runat="server" Text="" CssClass="label-danger"></asp:Label>

                                        <label for="exampleInputEmail2"></label>
                                        <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-primary pull-right" OnClick="btnAdd_Click" ValidationGroup="Add" />
                                    </div>

                                    <div class="project-list">

                                        <table class="table table-hover">
                                            <thead>
                                                <tr>
                                                    <th>Type</th>
                                                    <th>Name</th>

                                                    <th style="width: 170px; text-align: center;">Action</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptAdd" runat="server">

                                                    <%-- <asp:DataList ID="rpt" runat="server" RepeatDirection="Horizontal" RepeatColumns="8" RepeatLayout="Flow">--%>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="project-title">

                                                                <input type="hidden" runat="server" id="hfId" class="hfId" value='<%# Eval("ID") %>' />
                                                                <input type="hidden" runat="server" id="hfElementId" class="hfId" value='<%# Eval("TypeId") %>' />
                                                                <input type="hidden" runat="server" id="hfFormulaElementDetailId" class="hfId" value='<%# Eval("FormulaElementDetailId") %>' />

                                                                <asp:Label runat="server" ID="lblType" Text='<%# Eval("Type") %>' />

                                                            </td>

                                                            <td>



                                                                <asp:Label runat="server" ID="lblTitle" Text='<%# Eval("Title") %>' />
                                                            </td>

                                                            <td class="project-actions">

                                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                    <ContentTemplate>

                                                                        <asp:LinkButton ID="lbDeletePopup"
                                                                            runat="server"
                                                                            CssClass="btn btn-danger"
                                                                            OnClick="lbDeletePopup_Click"><span aria-hidden="true" class="fa fa-trash"></span>Delete
                                                                        </asp:LinkButton>

                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="lbDeletePopup" EventName="Click" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>

                                                    <%--  </asp:DataList>--%>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>

                                </div>
                            </div>
                            <label id="lbl" runat="server" class="label label-danger" visible="false"></label>
                        </div>
                        <div class="modal-footer">
                            <%--<asp:Button Text="Save" class="btn btn-primary" ID="btnAdd" OnClick="btnAdd_Click" runat="server" />--%>
                            <%--<asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" UseSubmitBehavior="false" ValidationGroup="AddEmployee" CssClass="btn btn-success pull-right" />--%>
                            <asp:Button Text="Save" class="btn btn-primary" ID="btnSave" ValidationGroup="Save" OnClick="btnSave_Click" runat="server" />
                        </div>
                    </ContentTemplate>

                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <%-- Modal End Here --%>
    <script type="text/javascript">
        function pageLoad() {
            $(".btnAdd").click(function () {
               
                reset();
                $('.openmodal').click();
            });




            function reset() {
                $(".txtAdd").val('');
                $(".hfModalId").val('');
            }

            CheckBoxFunc();
        }

        function CheckBoxFunc() {

            $('.chkbxFormula').change(function () {

                $('.modal-body').toggleClass('abc');
                $('#<%=dvFormula.ClientID %>').slideToggle();

            });

        }

        function AlertBox(title, Message, type) {
            swal(title, Message, type);
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

