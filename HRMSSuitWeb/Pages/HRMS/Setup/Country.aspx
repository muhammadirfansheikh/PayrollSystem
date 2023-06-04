<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/AdminMaster.master" CodeFile="Country.aspx.cs" Inherits="Pages_HRMS_Setup_Country" %>

<%@ Register Src="~/Controls/Shared/InProgress.ascx" TagName="InProgress" TagPrefix="uc2" %>


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
            <h2>Country</h2>
            <ol class="breadcrumb">
                 <li>
                    <a href="#">General Setup</a>
                </li>
                <li class="active">
                    <strong>Country</strong>
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
                                <label for="exampleInputEmail2">Country</label>
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
                            <div class="panel-heading">
                                Records
                            </div>
                            <div class="panel-body">
                                <div class="row m-b-sm m-t-sm">
                                    <div class="col-md-12">
                                        <a href="#" class="btn btn-primary btnAdd pull-right">Add  </a>
                                        <input type="button" data-toggle="modal" data-target="#CreateProjectModal" class="openmodal" style="display: none;" />
                                    </div>
                                </div>
                                <div class="project-list">

                                    <table class="table table-hover">
                                        <thead>
                                            <tr>
                                                <th>S.No</th>
                                                <th>Country</th>
                                                <th style="width: 170px; text-align: center;">Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rpt" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <%#(((RepeaterItem)Container).ItemIndex+1).ToString()%>
                                                        </td>
                                                        <td class="project-title">
                                                            <%# Eval("Title") %>

                                                            <input type="hidden" runat="server" id="hfId" class="hfId" value='<%# Eval("ID") %>' />
                                                        </td>
                                                        <td class="project-actions">
                                                            <asp:LinkButton ID="lbEdit"
                                                                runat="server"
                                                                CssClass="btn btn-primary"
                                                                OnClick="lbEdit_Click"><span aria-hidden="true" class="fa fa-edit"></span>Edit
                                                            </asp:LinkButton>

                                                            <asp:LinkButton ID="lbDelete"
                                                                OnClientClick="return confirm('Are you sure you wants to delete?')"
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


        </ContentTemplate>
    </asp:UpdatePanel>
    <%-- Create project Modal Start--%>

    <div class="modal inmodal" id="CreateProjectModal" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="padding-bottom: 9px; padding-top: 9px;">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">Add / Edit Country</h4>
                </div>

                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <input type="hidden" id="hfModalId" runat="server" class="hfModalId" />
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-lg-12">
                                        <label for="exampleInputPassword2">Country</label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Add" Text="*" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtNameAdd" CssClass="rfvbankdescription"></asp:RequiredFieldValidator>
                                        <asp:TextBox runat="server" placeholder="Country" ID="txtNameAdd" class="form-control txtAdd" />
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
    <%-- Create Project Modal End--%>
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
