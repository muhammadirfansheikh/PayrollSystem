<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="AdvanceTax.aspx.cs" Inherits="Pages_HCM_Setup_AdvanceTax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
     <link type="text/css" rel="stylesheet" href="https://cdn.datatables.net/1.10.16/css/jquery.dataTables.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="Manage Advance Tax" />
            </h2>
            <ol class="breadcrumb">

                <li>
                    <a href="#">HCM </a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Manage Advance Tax" />
                    </strong>
                </li>
            </ol>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12" style="margin-top: 11px;">
                    <div class="form-group">
                        <div class="panel panel-info mainVehicleInformation">
                            <div class="panel-heading">
                                Manage
                            </div>
                            <div class="panel-body dvEntry" id="panelVehicleInformation">

                                <div class="form-group col-lg-3">
                                    <label for="exampleInputEmail2">Group</label>
                                    <asp:DropDownList ID="ddlgroupAdd" OnSelectedIndexChanged="ddlgroupAdd_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control select2apply">
                                    </asp:DropDownList>
                                </div>

                                <div class="form-group col-lg-3">
                                    <label for="exampleInputEmail2">Company</label>
                                    <asp:DropDownList ID="ddlCompanyAdd" runat="server" CssClass="form-control select2apply" AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyAdd_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>





                                <div class="form-group col-lg-3">
                                    <label for="exampleInputPassword2">Advance Tax</label>
                                    <asp:TextBox ID="txtAdvanceTax" CssClass="form-control" TextMode="Number" min="0" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group col-lg-12" runat="server">
                                    <label for="exampleInputEmail2">Employee</label>
                                    <asp:ListBox runat="server" ID="ddlEmployeeAdd" SelectionMode="multiple" CssClass="form-control select2applyEmployee"></asp:ListBox>
                                    <%--<asp:DropDownList ID="ddlEmployeeAdd" runat="server" CssClass="form-control select2applyEmployee">
                                    </asp:DropDownList>--%>
                                </div>
                            </div>
                            <div class="panel-footer">
                                <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Mark Advance Tax" CssClass="btn btn-success" />
                                <asp:Button ID="btnCancel" OnClick="btnCancel_Click" runat="server" Text="Cancel" CssClass="btn btn-danger" />
                            </div>
                        </div>

                        <div class="row" runat="server" id="DivSearchPanel">
                            <div class="col-lg-12" style="margin-top: 11px;">
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <h3 class="panel-title">Search</h3>
                                    </div>
                                    <div class="panel-body">
                                        <div class="form-group col-lg-3">
                                            <label for="exampleInputEmail2">Group</label>
                                            <asp:DropDownList ID="ddlGroupSearch" OnSelectedIndexChanged="ddlGroupSearch_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control select2apply">
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3">
                                            <label for="exampleInputEmail2">Company</label>
                                            <asp:DropDownList ID="ddlCompanySearch" runat="server" CssClass="form-control select2apply">
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-12">
                                            <asp:Button ID="btnCancelSearch" runat="server" Text="Cancel" CssClass="btn btn-default pull-right" OnClick="btnCancelSearch_Click" />
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

                                            <div class="project-list">

                                                <asp:GridView ID="rpt" runat="server" Width="100%" class="table table-striped table-bordered table-hover table-responsive datatable" OnRowDataBound="rpt_RowDataBound" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="FULLNAME" HeaderText="Employee Name" />
                                                        <asp:BoundField DataField="AdvanceTaxPercent" HeaderText="Advance Tax Perent" ItemStyle-HorizontalAlign="Right" />

                                                    </Columns>
                                                </asp:GridView>



                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>



            <%-- Modal End Here --%>
        </ContentTemplate>

    </asp:UpdatePanel>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.9/js/jquery.dataTables.min.js"></script>
   

    <script src="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js" type="text/javascript"></script>
    <script>
        function pageLoad() {
            $('.select2applyEmployee').select2();
            $('.select2apply').select2();
            
        }
    </script>

    <script type="text/javascript">

        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_endRequest(function () {
            createDataTable();
        });

        createDataTable();

        function createDataTable() {
            $('#<%= rpt.ClientID %>').DataTable();
        }
    </script>
</asp:Content>

