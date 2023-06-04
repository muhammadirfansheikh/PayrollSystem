<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JobCategory.aspx.cs" MasterPageFile="~/MasterPage/AdminMaster.master" Inherits="Pages_HRMS_Setup_JobCategory" %>

<%@ Register Src="~/Controls/Shared/InProgress.ascx" TagName="InProgress" TagPrefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style>
        .chkChoice label {
            margin-left: 10px;
        }

        .chkChoice input {
            margin-left: 10px;
        }

        .chkChoice td {
            padding-left: 10px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <uc2:InProgress ID="InProgress1" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Job Category</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="#">General Setup</a>
                </li>
                <li class="active">
                    <strong>Job Category</strong>
                </li>
            </ol>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <input type="hidden" runat="server" id="IsAdd" value="1" />
            <input type="hidden" runat="server" id="IsView" value="0" />
            <input type="hidden" runat="server" id="IsEdit" value="1" />
            <input type="hidden" runat="server" id="IsDelete" value="1" />


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
                                <label for="exampleInputEmail2">Group</label>
                                <asp:DropDownList ID="ddlgroupSearch" OnSelectedIndexChanged="ddlgroupSearch_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>

                            <div class="form-group col-lg-3">
                                <label for="exampleInputEmail2">Company</label>
                                <asp:DropDownList ID="ddlcompanySearch" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>

                            <div class="form-group col-lg-3">
                                <label for="exampleInputEmail2">Job Category</label>
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
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
                            <div class="panel-heading">
                                Records
                            </div>
                            <div class="panel-body">
                                <%-- <a href="#" class="btn btn-primary btnAdd pull-right">Add</a>--%>
                                <asp:Button Text="Add" class="btn btn-primary pull-right" ID="Btn_Add" OnClick="Btn_Add_Click" runat="server" />
                                <input type="button" data-toggle="modal" data-target="#CreateProjectModal" class="openmodal" style="display: none;" />
                                <div class="project-list">

                                    <table class="table table-hover">
                                        <thead>
                                            <tr>
                                                <th>S.No</th>
                                                <th>Group</th>
                                                <th>Company</th>
                                                <th>Job Category</th>
                                                <th>Third Party Id</th>
                                                <th>Allowance</th>
                                                <th style="width: 250px; text-align: center;">Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rpt" runat="server" OnItemDataBound="rpt_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <%#(((RepeaterItem)Container).ItemIndex+1).ToString()%>
                                                        </td>
                                                        <td class="project-title">
                                                            <%# Eval("Group") %>
                                                        </td>
                                                        <td class="project-title">
                                                            <%# Eval("Company") %>
                                                        </td>
                                                        <td class="project-title">
                                                            <%# Eval("Title") %>
                                                            <asp:HiddenField runat="server" ID="hf_Id" Value='<%# Eval("ID") %>' />
                                                            <input type="hidden" runat="server" id="hfId" class="hfId" value='<%# Eval("ID") %>' />
                                                        </td>
                                                        <td class="project-title">
                                                            <%# Eval("ThirdPartyMappingId") %>
                                                        </td>
                                                        <td>
                                                            <div style="overflow-y: scroll; max-height: 400px;">
                                                                <asp:Literal ID="litAllowance" runat="server"></asp:Literal>
                                                            </div>
                                                        </td>
                                                        <td class="project-actions">
                                                            <asp:LinkButton ID="lblView"
                                                                runat="server"
                                                                CssClass="btn btn-info"
                                                                OnClick="lblView_Click" Visible="false"><span aria-hidden="true" class="fa fa-eye"></span>View
                                                            </asp:LinkButton>
                                                            <asp:LinkButton ID="lbEdit"
                                                                runat="server"
                                                                CssClass="btn btn-primary"
                                                                OnClick="lbEdit_Click"><span aria-hidden="true" class="fa fa-edit"></span>Edit
                                                            </asp:LinkButton>
                                                            <asp:LinkButton ID="lbDelete"
                                                                runat="server"
                                                                CssClass="btn btn-danger" OnClientClick="return confirm('Are you sure you want to delete?')"
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

    <%-- Modal Start Here --%>
    <%-- Create project Modal Start--%>
    <div class="modal inmodal" id="CreateProjectModal" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog" style="width: 60%;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">Add / Edit Job Category</h4>
                </div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <input type="hidden" id="hfModalId" runat="server" class="hfModalId" />
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-lg-6">
                                        <label for="exampleInputPassword2">Group</label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Add" Text="*" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="ddlGroupAdd" InitialValue="0"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="Add" Text="*" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="ddlGroupAdd" InitialValue=""></asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlGroupAdd" OnSelectedIndexChanged="ddlGroupAdd_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control ddlGroupAdd">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-lg-6">
                                        <label for="exampleInputPassword2">Company</label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="Add" Text="*" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="ddlCompanyAdd" InitialValue="0"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Add" Text="*" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="ddlCompanyAdd" InitialValue=""></asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlCompanyAdd" runat="server" CssClass="form-control ddlCompanyAdd" AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyAdd_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-lg-6">
                                        <label for="exampleInputPassword2">Job Category</label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Add" Text="*" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtNameAdd" CssClass="rfvbankdescription"></asp:RequiredFieldValidator>
                                        <asp:TextBox runat="server" placeholder="Job Category" ID="txtNameAdd" class="form-control txtAdd" />
                                    </div>

                                    <div class="col-lg-6">
                                        <label for="exampleInputPassword2">Third Party Id</label>
                                        <asp:TextBox runat="server" MaxLength="50"  ID="txtThirPartyMappingId" class="form-control txtAdd" />
                                    </div>

                                    <div class="col-lg-12">
                                        <label>Allowance</label>
                                        <div style="overflow-x: scroll">
                                            <asp:CheckBoxList runat="server" ID="chk_Allowance" RepeatColumns="3" CssClass="cblCheckAll chkChoice" RepeatDirection="Vertical"></asp:CheckBoxList>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="modal-footer" runat="server" id="Div_Save">
                            <asp:Button Text="Save" class="btn btn-primary" ID="btnAdd" ValidationGroup="Add" OnClick="btnAdd_Click" runat="server" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <%-- Create Project Modal End--%>
    <%-- Modal End Here --%>
    <script type="text/javascript">
        function pageLoad() {


            $('.i-checks').iCheck({
                checkboxClass: 'icheckbox_square-green',
                radioClass: 'iradio_square-green'
            });
            $(".btnAdd").click(function () {
                reset();
                $('.openmodal').click();
            });

            $('.cblCheckAll input').change(function () {
                var currChk = $(this);

                if ($(this).val() == "0") {
                    $(this).closest('table').find('input:checkbox').prop('checked', $(currChk).is(':checked'));
                }
                else {
                    var allCheckboxCount = $(this).closest('table').find('input:checkbox').size();
                    var allCheckedCount = $(this).closest('table').find('input:checkbox:checked').not('input:checkbox[value=0]').size();
                    var isChecked = false;
                    if (allCheckedCount >= allCheckboxCount - 1) {
                        isChecked = true;
                    }
                    $(this).closest('table').find('input:checkbox[value=0]').prop('checked', isChecked);
                }
            });
        }

        function reset() {
            $(".ddlCompanyAdd").val('0');
            $(".txtAdd").val('');
            $(".hfModalId").val('');
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
