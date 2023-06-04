﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="SetupLocation.aspx.cs" Inherits="Pages_HRMS_Setup_SetupLocation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

      <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Location</h2>
            <ol class="breadcrumb">
                  <li>
                    <a href="#">General Setup</a>
                </li>
                <li class="active">
                    <strong>Location</strong>
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
                                <label for="exampleInputEmail2">Location</label>
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>

                            </div>
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
                                Business Unit
                            </div>

                            <div class="panel-body">
                                <div class="row m-b-sm m-t-sm">
                                    <div class="col-md-12">
                                        <a href="#" class="btn btn-primary btnAdd pull-right">Add new</a>
                                        <input type="button" data-toggle="modal" data-target="#CreateProjectModal" class="openmodal" style="display: none;" />
                                    </div>
                                </div>
                                <div class="project-list">

                                    <table class="table table-hover">
                                        <thead>
                                            <tr>
                                                <th>Company</th>
                                                <th>Location</th>
                                                <th style="width: 170px; text-align: center;">Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rpt" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="project-title">
                                                            <a href="#"><%# Eval("CompanyName") %></a>
                                                            <br />
                                                            <%--<small>Created 14.08.2014</small>--%>
                                                            <input type="hidden" runat="server" id="hfId" class="hfId" value='<%# Eval("ID") %>' />
                                                        </td>
                                                        <td class="project-title">
                                                            <a href="#"><%# Eval("Title") %></a>
                                                            <br />
                                                            <%--<small>Created 14.08.2014</small>--%>
                                                        </td>
                                                        <td class="project-actions">
                                                            <%--<a href="#" class="btn btn-white btn-sm"><i class="fa fa-pencil"></i>Edit </a>--%>
                                                            <asp:LinkButton ID="lbEdit"
                                                                runat="server"
                                                                CssClass="btn btn-primary"
                                                                OnClick="lbEdit_Click"><span aria-hidden="true" class="fa fa-edit"></span>Edit
                                                            </asp:LinkButton>
                                                            <%--<asp:Button Text="Edit" ID="btnEdit" CssClass="fa fa-pencil" OnClick="btnEdit_Click" runat="server" />--%>
                                                            <%--<a href="#" class="btn btn-white btn-sm"><i class="fa fa-folder"></i>Delete </a>--%>
                                                            <asp:LinkButton ID="lbDelete"
                                                                runat="server"
                                                                CssClass="btn btn-danger"
                                                                OnClick="lbDelete_Click"><span aria-hidden="true" class="fa fa-trash"></span>Delete
                                                            </asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>

            <%-- Modal Start Here --%>
            <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <%-- Create project Modal Start--%>
        </ContentTemplate>
    </asp:UpdatePanel>

        <div class="modal inmodal" id="CreateProjectModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content animated flipInY">
                <div class="modal-header" style="padding-bottom: 9px; padding-top: 9px;">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">Add / Edit Location</h4>
                    <%--<small class="font-bold">Lorem Ipsum is simply dummy text of the printing and typesetting industry.</small>--%>
                </div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="modal-body" style="padding-bottom: 10px; border-bottom-width: 10px; padding-top: 10px; height: 197px;">
                            <input type="hidden" id="hfModalId" runat="server" class="hfModalId" />
                            <div id="div2" runat="server" visible="false" class="alert alert-warning">
                            </div>
                            <div class="form-group">
                                <div class="col-lg-12">
                                    <label for="exampleInputPassword2">Company</label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="Add" Text="*" ErrorMessage="Location" ForeColor="Red"
                                        Display="Dynamic" ControlToValidate="ddlCompanyAdd" InitialValue="0"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlCompanyAdd" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyAdd_SelectedIndexChanged" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                          
                                <label class="col-lg-12">Location:<em>*</em></label>
                                <div class="col-lg-12">
                                    <asp:TextBox runat="server" placeholder="Location" ID="txtNameAdd" class="form-control txtAdd" />
                                    <span class="help-block m-b-none"></span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Add"
                                        Display="Dynamic" ControlToValidate="txtNameAdd" CssClass="rfvbankdescription"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <label id="lbl" runat="server" class="label label-danger" visible="false"></label>
                        </div>
                        <div class="modal-footer">
                            <%--<asp:Button Text="Save" class="btn btn-primary" ID="btnAdd" OnClick="btnAdd_Click" runat="server" />--%>
                            <%--<asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" UseSubmitBehavior="false" ValidationGroup="AddEmployee" CssClass="btn btn-success pull-right" />--%>
                            <asp:Button Text="Save" class="btn btn-primary" ID="btnAdd" ValidationGroup="Add" OnClick="btnAdd_Click" runat="server" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

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


