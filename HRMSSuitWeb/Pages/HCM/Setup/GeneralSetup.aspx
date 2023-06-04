<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="GeneralSetup.aspx.cs" Inherits="Pages_HCM_Setup_GeneralSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label ID="lblSetupName1" runat="server" Text=""></asp:Label>
            </h2>
            <ol class="breadcrumb">
               
                <li>
                    <a href="#">General Setup</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label ID="lblSetupName2" runat="server" Text=""></asp:Label>
                    </strong>
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
                                <label for="exampleInputEmail2">
                                    <asp:Label ID="lblSetupName3" runat="server" Text=""></asp:Label>

                                </label>
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>

                            </div>
                            <div class="form-group col-lg-12">
                                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" CssClass="btn btn-default pull-right" />
                                <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" CssClass="btn btn-primary pull-right" />
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
                                <asp:Label ID="lblSetupName4" runat="server" Text="Records"></asp:Label>
                            </div>
                            <div class="panel-body">
                                <div class="row m-b-sm m-t-sm">
                                    <div class="col-md-12">
                                        <a href="#" class="btn btn-primary btnAdd pull-right">Add</a>
                                        <input type="button" data-toggle="modal" data-target="#CreateProjectModal" class="openmodal" style="display: none;" />
                                    </div>
                                </div>
                                <div class="project-list">
                                    <table class="table table-hover">
                                        <thead>
                                            <tr>
                                                <th>
                                                    <asp:Label ID="lbl_TH" runat="server" Text="Name"></asp:Label>
                                                </th>
                                                <th style="width: 170px; text-align: center;">Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rpt" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <%# Eval("Title") %>
                                                            <input type="hidden" runat="server" id="hfId" class="hfId" value='<%# Eval("ID") %>' />
                                                        </td>
                                                        <td style="width: 170px; text-align: center;">
                                                            <asp:LinkButton ID="lbEdit"
                                                                runat="server"
                                                                CssClass="btn btn-primary"
                                                                OnClick="lbEdit_Click"><span aria-hidden="true" class="fa fa-edit"></span>Edit
                                                            </asp:LinkButton>
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
        </ContentTemplate>
    </asp:UpdatePanel>


    <div class="modal inmodal" id="CreateProjectModal" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="padding-bottom: 9px; padding-top: 9px;">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">Add / Edit   
                        <asp:Label ID="lblSetupName5" runat="server" Text=""></asp:Label></h4>
                </div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <input type="hidden" id="hfModalId" runat="server" class="hfModalId" />
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-lg-12">
                                        <label for="exampleInputPassword2">
                                            <asp:Label ID="lblSetupName6" runat="server" Text=""></asp:Label></label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Add" Display="Dynamic" ControlToValidate="txtNameAdd" CssClass="rfvbankdescription"></asp:RequiredFieldValidator>
                                        <asp:TextBox runat="server" ID="txtNameAdd" class="form-control txtAdd" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
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


