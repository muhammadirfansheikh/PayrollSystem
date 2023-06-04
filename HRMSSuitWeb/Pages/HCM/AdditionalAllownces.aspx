<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="AdditionalAllownces.aspx.cs" Inherits="Pages_HCM_AdditionalAllownces" %>

<%@ Register Src="~/Controls/Shared/InProgress.ascx" TagName="InProgress" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.9/js/jquery.dataTables.min.js"></script>
    <link type="text/css" rel="stylesheet" href="https://cdn.datatables.net/1.10.16/css/jquery.dataTables.min.css" />

    <script>

</script>

   <style>
        .select2-close-mask {
            z-index: 2099;
        }

        .select2-dropdown {
            z-index: 3051;
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
            <h2>Additional Allownces</h2>
            <ol class="breadcrumb">

                <li>
                    <a href="#">HCM</a>
                </li>
                <li class="active">
                    <strong>Additional Allownces</strong>
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
                                <label for="exampleInputEmail2">Group</label>
                                <asp:DropDownList ID="ddlgroupSearch" OnSelectedIndexChanged="ddlgroupSearch_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control select2apply">
                                </asp:DropDownList>
                            </div>

                            <div class="form-group col-lg-3">
                                <label for="exampleInputEmail2">Company</label>
                                <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control select2apply" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>

                            <div class="form-group col-lg-3">
                                <label for="exampleInputEmail2">Employee</label>
                                <asp:DropDownList ID="ddlEmployeeSearch" runat="server" CssClass="form-control select2apply">
                                </asp:DropDownList>
                            </div>

                            <div class="form-group col-lg-3">
                                <label for="exampleInputEmail2">Allownce</label>
                                <asp:DropDownList ID="ddlAllownces" runat="server" CssClass="form-control select2apply">
                                </asp:DropDownList>
                            </div>

                            <div class="col-lg-2 divMonthPayroll">
                                <label>Date</label>

                                <asp:TextBox ID="txtMonthSearch" runat="server" class="form-control DatePickerMonth_Years" autocomplete="off"></asp:TextBox>

                            </div>




                            <%--  <div class="form-group col-lg-3">
                                <label for="exampleInputEmail2">Company</label>
                                <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>--%>
                            <div class="form-group col-lg-12">
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default pull-right" OnClick="btnCancel_Click1" />
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary pull-right" OnClick="btnSearch_Click1" />
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

                                        <asp:Button ID="btnAddNewRecord" class="btn btn-primary btnAdd pull-right" OnClick="btnAddNewRecord_Click" runat="server" Text="Add" />
                                        <input type="button" data-toggle="modal" data-target="#CreateProjectModal" class="openmodal" style="display: none;" />
                                    </div>
                                </div>
                                <div class="project-list">

                                    <asp:GridView ID="rpt" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="rpt_PageIndexChanging" class="table table-striped table-bordered table-hover table-responsive" OnRowCommand="rpt_RowCommand" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr No">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                                            <asp:BoundField DataField="AllowanceName" HeaderText="Allownce Name" />
                                            <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                            <asp:BoundField DataField="Month" HeaderText="Month" DataFormatString="{0: yyyy-MM-dd}" />
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:Button ID="lbEdit" runat="server" CssClass="btn btn-primary editopenmodal" CommandName="EditS" CommandArgument='<%# Eval("AdditonalAllowynceID") %>' Text="Edit" />
                                                    <%--<asp:LinkButton ID="lbEdit" runat="server" CssClass="btn btn-primary editopenmodal" CommandName="Edit" CommandArgument='<%# Eval("AdditonalAllowynceID") %>'><span aria-hidden="true" class="fa fa-edit"></span>Edit</asp:LinkButton>--%>
                                                    <%--<asp:LinkButton ID="lbDelete" runat="server" CssClass="btn btn-danger" CommandName="Delete" CommandArgument='<%# Eval("AdditonalAllowynceID") %>'><span aria-hidden="true" class="fa fa-trash"></span>Delete </asp:LinkButton>--%>
                                                    <asp:Button ID="lbDelete" runat="server" CssClass="btn btn-danger" CommandName="DeleteS" OnClientClick="return confirm('Are you sure you wants to delete?')" CommandArgument='<%# Eval("AdditonalAllowynceID") %>' Text="Delete" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>


                                    <%--   <table id="fcTRPTable" class="table table-striped table-bordered table-hover table-responsive">
                                        <thead>
                                            <tr>
                                                <th>Employee</th>
                                                <th>Allownce Name</th>
                                                <th>Amount</th>
                                                <th>Month</th>

                                                <th style="width: 170px; text-align: center;">Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rpt" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="project-title">
                                                            <%# Eval("EmployeeName") %>
                                                            <input type="hidden" runat="server" id="hfId" class="hfId" value='<%# Eval("AdditonalAllowynceID") %>' />

                                                        </td>

                                                        <td class="project-title">
                                                            <%# Eval("AllowanceName") %>
                                                        </td>

                                                        <td class="project-title">
                                                            <%# Eval("Amount") %>
                                                        </td>

                                                        <td class="project-title">
                                                            <%# Eval("Month") %>
                                                        </td>


                                                        <td class="project-actions">
                                                          
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div class="modal inmodal" id="CreateProjectModal" role="dialog" aria-hidden="true" data-backdrop="static">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <%--<button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>--%>
                            <h4 class="modal-title">Add / Edit Additional Allownce </h4>
                        </div>
                    <%--      <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            
                    <ContentTemplate>--%>
                        <div class="modal-body">
                            <div class="form-horizontal">
                                <input type="hidden" id="hfModalId" runat="server" class="hfModalId" />
                                <div class="form-group">
                                    <div class="col-lg-6">
                                        <label for="exampleInputPassword2">Group</label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="Add" Text="*" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="ddlGroupAdd" InitialValue="0"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="Add" Text="*" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="ddlGroupAdd" InitialValue=""></asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlGroupAdd" OnSelectedIndexChanged="ddlGroupAdd_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control select2apply ddlGroupAdd">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-lg-6">
                                        <label for="exampleInputPassword2">Company</label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="Add" Text="*" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="ddlCompanyAdd" InitialValue="0"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="Add" Text="*" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="ddlCompanyAdd" InitialValue=""></asp:RequiredFieldValidator>
                                        <asp:DropDownList runat="server" ID="ddlCompanyAdd" class="form-control select2apply" OnSelectedIndexChanged="ddlCompanyAdd_SelectedIndexChanged" AutoPostBack="true" />
                                    </div>
                                    <div class="col-lg-6">
                                        <label for="exampleInputPassword2">Employee</label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Add" InitialValue="0" Text="*" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="ddlEmployeeAdd" CssClass="rfvbankdescription"></asp:RequiredFieldValidator>
                                        <asp:DropDownList runat="server" ID="ddlEmployeeAdd" class="form-control select2apply" />
                                    </div>
                                    <div class="col-lg-6">
                                        <label for="exampleInputPassword2">Allowance</label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="Add" InitialValue="0" Text="*" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="ddlAllowance" CssClass="rfvbankdescription"></asp:RequiredFieldValidator>
                                        <asp:DropDownList runat="server" ID="ddlAllowance" class="form-control select2apply" />
                                    </div>
                                    <div class="col-lg-6">
                                        <label for="exampleInputPassword2">Amount</label>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Add" Text="*" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtAmountAdd" CssClass="rfvbankdescription"></asp:RequiredFieldValidator>
                                        <asp:TextBox runat="server" ID="txtAmountAdd" onkeypress="ForceNumericInput(this,0,true)" class="form-control numeric" placeholder="Amount" />

                                    </div>
                                    <div class="col-lg-6">
                                        <label for="exampleInputPassword2">Date</label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Add" Text="*" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtMonthAdd" CssClass="rfvbankdescription"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtMonthAdd" runat="server" class="form-control DatePickerMonth_Years" autocomplete="off"></asp:TextBox>

                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button Text="Save" class="btn btn-primary" ID="btnAdd" ValidationGroup="Add" OnClick="btnAdd_Click" runat="server" />
                            <asp:Button Text="Close" class="btn btn-danger" ID="btnCloseModal" OnClick="btnCloseModal_Click" runat="server" />
                        </div>
                   <%--            </ContentTemplate>
                   
                </asp:UpdatePanel>--%>
                    </div>
                </div>
            </div>
            <%-- Modal End Here --%>
        </ContentTemplate>

    </asp:UpdatePanel>
    <script src="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js" type="text/javascript"></script>

    <script type="text/javascript">
        function ForceNumericInput(This, AllowDot, AllowMinus) {
            if (arguments.length == 1) {
                var s = This.value;
                // if "-" exists then it better be the 1st character
                var i = s.lastIndexOf("-");
                if (i == -1)
                    return;
                if (i != 0)
                    This.value = s.substring(0, i) + s.substring(i + 1);
                return;
            }


            var code = event.keyCode;
            switch (code) {
                case 8:     // backspace
                case 37:    // left arrow
                case 39:    // right arrow
                    //case 46:    // delete
                    event.returnValue = true;
                    return;
            }
            if (code == 45)     // minus sign
            {
                if (AllowMinus == false) {
                    event.returnValue = false;
                    return;
                }




                // wait until the element has been updated to see if the minus is in the right spot
                var s = "ForceNumericInput(document.getElementById('" + This.id + "'))";
                setTimeout(s, 2);
                return;
            }
            if (AllowDot && code == 46) {
                if (This.value.indexOf(".") >= 0) {
                    // don't allow more than one dot
                    event.returnValue = false;
                    return;
                }
                event.returnValue = true;
                return;
            }
            // allow character of between 0 and 9
            if (code >= 48 && code <= 57) {
                event.returnValue = true;
                return;
            }
            event.returnValue = false;
        }

        function pageLoad() {

            $('.DatePickerMonth_Years').datepicker({
                minViewMode: "months",
                keyboardNavigation: false,
                forceParse: false,
                autoclose: true,
                todayHighlight: true,
                calendarWeeks: true,
                format: "M-yyyy",
                viewMode: "months"
            });
            $('.DatePickerMonth_Years').keydown(function () {
                return false;
            });

            function reset() {
                //$(".txtAdd").val('');
                //$(".hfModalId").val('');
            }

            $('.select2apply').select2();

            $('.txtMonthOfPayroll').datepicker('setDate', null);


            //var $j = jQuery.noConflict();
            // DataTable, save state
            //var adminUsersDT = $("#MainContent_rpt").DataTable({
            //    'bDestroy': true,
            //    "bStateSave": true,
            //    "orderable": false, "targets": [1, 2, 3, 4,5],
            //    dom: 'lfrtip',
            //    "fnStateSave": function (oSettings, oData) {
            //        localStorage.setItem('MainContent_rpt', JSON.stringify(oData));
            //    },
            //    "fnStateLoad": function (oSettings) {
            //        return JSON.parse(localStorage.getItem('MainContent_rpt'));
            //    }
            //});
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

