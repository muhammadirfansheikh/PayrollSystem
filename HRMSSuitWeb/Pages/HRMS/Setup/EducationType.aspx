<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EducationType.aspx.cs" Inherits="Pages_HRMS_Setup_EducationType" MasterPageFile="~/MasterPage/AdminMaster.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Education Type</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="#">General Setup</a>
                </li>
                <li class="active">
                    <strong>Education Type</strong>
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
                                <label for="exampleInputEmail2">Education Type</label>
                                <asp:TextBox ID="txtTypeSearch" runat="server" CssClass="form-control"></asp:TextBox>

                            </div>
                            <div class="form-group col-lg-3">
                                <label for="exampleInputEmail2">Third Party Id</label>
                                <asp:TextBox ID="txtThirdPartyMappingIdSearch" runat="server" CssClass="form-control"></asp:TextBox>

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
                                Education Type Detail
                            </div>

                            <div class="panel-body">
                                <div class="row m-b-sm m-t-sm">
                                    <div class="col-md-12">
                                        <h3 class="panel-title"></h3>
                                        <a href="#" class="btn btn-primary btnAdd pull-right">Add new Type</a>
                                        <input type="button" data-toggle="modal" data-target="#CreateProjectModal" class="openmodal" style="display: none;" />
                                    </div>
                                </div>
                                <div class="project-list">

                                    <table class="table table-hover">
                                        <thead>
                                            <tr>
                                                <th>Education Type Title</th>
                                                <th>Third Party Id</th>
                                                <th style="width: 170px; text-align: center;">Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rpt" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="project-title">
                                                            <a href="#"><%# Eval("_Educationtype") %></a>
                                                            <br />
                                                            <%--<small>Created 14.08.2014</small>--%>
                                                            <input type="hidden" runat="server" id="hftypeid" class="hftypeid" value='<%# Eval("_Id") %>' />
                                                        </td>
                                                        <td class="project-title">
                                                            <%# Eval("_ThirdPartyMappingId") %>
                                                          
                                                        
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
                                                                OnClientClick="return confirm('Are you sure you wants to delete?')"
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
    <%-- Create Project Modal End--%>
    <div class="modal inmodal" id="CreateProjectModal" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">Add New Type</h4>
                    <%--<small class="font-bold">Lorem Ipsum is simply dummy text of the printing and typesetting industry.</small>--%>
                </div>

                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <input type="hidden" id="hfTypeId" runat="server" class="hfTypeId" />
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-lg-12">Education Type Title:<em>*</em></label>

                                    <div class="col-lg-12">
                                        <asp:TextBox runat="server" placeholder="Education Type Title" ID="txtTypeAdd" class="form-control txtAdd" />
                                        <span class="help-block m-b-none"></span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Add"
                                            Display="Dynamic" ControlToValidate="txtTypeAdd" CssClass="rfvbankdescription"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-lg-12">Third Party Id</label>

                                    <div class="col-lg-12">
                                        <asp:TextBox runat="server" MaxLength="50" ID="txtThirPartyMappingId" class="form-control txtAdd" />

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <%--<asp:Button Text="Close" ID="btnClose" OnClick="btnClose_Click" CssClass="btn btn-white" runat="server" />--%>
                            <%--<button type="button" class="btn btn-white" data-dismiss="modal">Close</button>--%>
                            <asp:Button Text="Save" class="btn btn-primary" ID="btnAdd" ValidationGroup="Add" OnClick="btnAdd_Click" runat="server" />
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
                $(".hfTypeId").val('');
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
