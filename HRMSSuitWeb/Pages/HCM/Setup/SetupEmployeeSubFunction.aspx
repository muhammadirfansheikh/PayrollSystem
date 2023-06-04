<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="SetupEmployeeSubFunction.aspx.cs" Inherits="Pages_HCM_Setup_SetupEmployeeSubFunction" %>

<%@ Register Src="~/Controls/Shared/InProgress.ascx" TagName="InProgress" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.9/js/jquery.dataTables.min.js"></script>
    <link type="text/css" rel="stylesheet" href="https://cdn.datatables.net/1.10.16/css/jquery.dataTables.min.css" />

    <script>

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <uc2:InProgress ID="InProgress1" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Sub Function</h2>
            <ol class="breadcrumb">

                <li>
                    <a href="#">HCM Setup</a>
                </li>
                <li class="active">
                    <strong>Sub Function</strong>
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
                                <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control select2apply">
                                </asp:DropDownList>
                            </div>

                            <div class="form-group col-lg-2">
                                <label for="exampleInputEmail2">Function</label>
                                <asp:DropDownList ID="cmbFunction" runat="server" CssClass="form-control select2apply">
                                </asp:DropDownList>
                            </div>

                            <div class="col-lg-2 divMonthPayroll">
                                <label>Sub Function Code</label>

                                <asp:TextBox ID="txtSubFunctionCode" runat="server" class="form-control" autocomplete="off"></asp:TextBox>

                            </div>


                            <div class="col-lg-2 divMonthPayroll">
                                <label>Sub Function Name</label>

                                <asp:TextBox ID="txtSubFunctionName" runat="server" class="form-control" autocomplete="off"></asp:TextBox>

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
                                            <asp:BoundField DataField="FunctionName" HeaderText="Function Name" />
                                            <asp:BoundField DataField="SubFunctionCode" HeaderText="Sub Function Code" />
                                            <asp:BoundField DataField="SubFunctionName" HeaderText="Sub Function Name" />


                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:Button ID="lbEdit" runat="server" CssClass="btn btn-primary editopenmodal" CommandName="EditS" CommandArgument='<%# Eval("SubFunctionId") %>' Text="Edit" />

                                                    <asp:Button ID="lbDelete" runat="server" CssClass="btn btn-danger" CommandName="DeleteS" OnClientClick="return confirm('Are you sure you wants to delete?')" CommandArgument='<%# Eval("SubFunctionId") %>' Text="Delete" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>



                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal inmodal" id="CreateProjectModal" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                            <h4 class="modal-title">Add / Edit Additional Allownce </h4>
                        </div>
                        <%--  <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            
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
                                        <asp:DropDownList ID="ddlGroupAdd" OnSelectedIndexChanged="ddlGroupAdd_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control ddlGroupAdd">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-lg-6">
                                        <label for="exampleInputPassword2">Company</label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="Add" Text="*" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="ddlCompanyAdd" InitialValue="0"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="Add" Text="*" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="ddlCompanyAdd" InitialValue=""></asp:RequiredFieldValidator>
                                        <asp:DropDownList runat="server" ID="ddlCompanyAdd" class="form-control " />
                                    </div>

                                    <div class="col-lg-6">
                                        <label for="exampleInputPassword2">Function</label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="Add" InitialValue="0" Text="*" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="cmbFunctionAdd" CssClass="rfvbankdescription"></asp:RequiredFieldValidator>
                                        <asp:DropDownList runat="server" ID="cmbFunctionAdd" class="form-control " />
                                    </div>

                                    <div class="col-lg-6 divMonthPayroll">
                                        <label>Sub Function Code</label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Add" InitialValue="0" Text="*" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtFunctionCodeAdd" CssClass="rfvbankdescription"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtFunctionCodeAdd" runat="server" class="form-control" autocomplete="off"></asp:TextBox>

                                    </div>


                                    <div class="col-lg-6 divMonthPayroll">
                                        <label>Sub Function Name</label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Add" InitialValue="0" Text="*" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtSubFunctionNameAdd" CssClass="rfvbankdescription"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtSubFunctionNameAdd" runat="server" class="form-control" autocomplete="off"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button Text="Save" class="btn btn-primary" ID="btnAdd" ValidationGroup="Add" OnClick="btnAdd_Click" runat="server" />
                        </div>
                        <%--       </ContentTemplate>
                   
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

