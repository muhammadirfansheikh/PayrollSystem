<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Setup_Repair_Maintainance.aspx.cs" Inherits="Pages_HCM_Setup_Setup_Repair_Maintainance" %>

<%@ Register Src="~/Controls/Shared/InProgress.ascx" TagName="InProgress" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.9/js/jquery.dataTables.min.js"></script>
    <link type="text/css" rel="stylesheet" href="https://cdn.datatables.net/1.10.16/css/jquery.dataTables.min.css" />

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
            <h2>Repair Maintainance</h2>
            <ol class="breadcrumb">

                <li>
                    <a href="#">HCM Setup</a>
                </li>
                <li class="active">
                    <strong>Repair Maintainance</strong>
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
                                <label for="exampleInputEmail2">RM Type</label>
                                <asp:DropDownList ID="ddlRMTypeSearch" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRMTypeSearch_SelectedIndexChanged" CssClass="form-control select2apply">
                                    <asp:ListItem Value="0">Not Fixed</asp:ListItem>
                                    <asp:ListItem Value="1">Fixed</asp:ListItem>
                                    <asp:ListItem Value="2">Standard</asp:ListItem>
                                </asp:DropDownList>
                            </div>
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
                            <div class="form-group col-lg-3" runat="server" id="dvSearchForJobCategory">
                                <label for="exampleInputEmail2">Job Category</label>
                                <asp:DropDownList ID="ddlJobCategorySearch" runat="server" CssClass="form-control select2apply">
                                </asp:DropDownList>
                            </div>



                            <div class="form-group col-lg-3" runat="server" id="dvSearchForEmployee" visible="false">
                                <label for="exampleInputEmail2">Employee</label>
                                <asp:DropDownList ID="ddlEmployeeSearch" runat="server" CssClass="form-control select2apply">
                                </asp:DropDownList>
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
                                        <asp:Button ID="btnShowIncreasePopup" class="btn btn-danger btnAdd pull-right" OnClick="btnShowIncreasePopup_Click" runat="server" Text="Increase Percentage" />
                                        <input type="button" data-toggle="modal" data-target="#CreateProjectModal" class="openmodal" style="display: none;" />
                                        <input type="button" data-toggle="modal" data-target="#CreateProjectModalIncrease" class="openmodalIncrease" style="display: none;" />
                                    </div>
                                </div>
                                <div class="project-list">

                                    <asp:GridView ID="rpt" runat="server" Width="100%" class="table table-striped table-bordered table-hover table-responsive" OnRowCommand="rpt_RowCommand" OnRowDataBound="rpt_RowDataBound" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr No">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                                            <asp:BoundField DataField="CategoryName" HeaderText="Category Name" />
                                            <asp:BoundField DataField="RMType" HeaderText="RM Type" />
                                            <asp:BoundField DataField="FuelInLitres" HeaderText="Fuel In Litres" />
                                            <asp:BoundField DataField="RM_FirstYear" HeaderText="1st Year" />
                                            <asp:BoundField DataField="RM_SecondYear" HeaderText="2nd Year" />
                                            <asp:BoundField DataField="RM_ThirdYear" HeaderText="3rd Year" />
                                            <asp:BoundField DataField="RM_ForthYear" HeaderText="4th Year" />
                                            <asp:BoundField DataField="RM_FifthYear" HeaderText="5th Year" />
                                            <asp:BoundField DataField="IncreasePercentage" HeaderText="Incr Perc" />
                                            <asp:BoundField DataField="IncreaseDate" HeaderText="Incr Date" DataFormatString="{0: yyyy-MM-dd}" />
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:Button ID="lbEdit" runat="server" CssClass="btn btn-primary editopenmodal" CommandName="EditS" CommandArgument='<%# Eval("SetupRMId") %>' Text="Edit" />
                                                    <%--<asp:LinkButton ID="lbEdit" runat="server" CssClass="btn btn-primary editopenmodal" CommandName="Edit" CommandArgument='<%# Eval("AdditonalAllowynceID") %>'><span aria-hidden="true" class="fa fa-edit"></span>Edit</asp:LinkButton>--%>
                                                    <%--<asp:LinkButton ID="lbDelete" runat="server" CssClass="btn btn-danger" CommandName="Delete" CommandArgument='<%# Eval("AdditonalAllowynceID") %>'><span aria-hidden="true" class="fa fa-trash"></span>Delete </asp:LinkButton>--%>
                                                    <asp:Button ID="lbDelete" runat="server" CssClass="btn btn-danger" CommandName="DeleteS" OnClientClick="return confirm('Are you sure you wants to delete?')" CommandArgument='<%# Eval("SetupRMId") %>' Text="Delete" />
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



            <%-- Modal End Here --%>
        </ContentTemplate>

    </asp:UpdatePanel>


    <div class="modal inmodal" id="CreateProjectModal" role="dialog" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">Add / Edit Repair Maintainance</h4>
                </div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">

                    <ContentTemplate>
                        <div class="modal-body">
                            <div class="form-horizontal">
                                <input type="hidden" id="hfModalId" runat="server" class="hfModalId" />
                                <div class="form-group">

                                    <div class="col-lg-6">
                                        <label for="exampleInputPassword2">RM Type</label>

                                        <asp:DropDownList ID="ddlRMTypeAdd" OnSelectedIndexChanged="ddlRMTypeAdd_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control ddlGroupAdd select2apply">
                                            <asp:ListItem Value="0">Not Fixed</asp:ListItem>
                                            <asp:ListItem Value="1">Fixed</asp:ListItem>



                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-lg-6">
                                        <label for="exampleInputPassword2">Group</label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="Add" Text="*" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="ddlGroupAdd" InitialValue="0"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="Add" Text="*" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="ddlGroupAdd" InitialValue=""></asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlGroupAdd" OnSelectedIndexChanged="ddlGroupAdd_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control ddlGroupAdd select2apply">
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
                                    <div class="col-lg-6" runat="server" id="dvJobCatgeoryAdd">
                                        <label for="exampleInputPassword2">Job Category</label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="Add" Text="*" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="ddlJobCategoryAdd" InitialValue="0"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ValidationGroup="Add" Text="*" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="ddlJobCategoryAdd" InitialValue=""></asp:RequiredFieldValidator>
                                        <asp:DropDownList runat="server" ID="ddlJobCategoryAdd" class="form-control select2apply" />
                                    </div>
                                    <div class="col-lg-6" id="dvEmployeeAdd" runat="server" visible="false">
                                        <label for="exampleInputPassword2">Employee</label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Enabled="false" runat="server" ValidationGroup="Add" Text="*" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="ddlEmployeeAdd" InitialValue="0"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" Enabled="false" runat="server" ValidationGroup="Add" Text="*" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="ddlEmployeeAdd" InitialValue=""></asp:RequiredFieldValidator>
                                        <asp:DropDownList runat="server" ID="ddlEmployeeAdd" class="form-control select2apply" />
                                    </div>

                                    <div class="col-lg-6">
                                        <label for="exampleInputPassword2">Fuel In Litres</label>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Add" Text="*" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtFuelInLitresAdd" CssClass="rfvbankdescription"></asp:RequiredFieldValidator>
                                        <asp:TextBox runat="server" ID="txtFuelInLitresAdd" onkeypress="ForceNumericInput(this,0,false)" class="form-control numeric" placeholder="Amount" />

                                    </div>
                                    <div class="col-lg-6">
                                        <label for="exampleInputPassword2">RM_FirstYear</label>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="Add" Text="*" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtRM_FirstYearAdd" CssClass="rfvbankdescription"></asp:RequiredFieldValidator>
                                        <asp:TextBox runat="server" ID="txtRM_FirstYearAdd" onkeypress="ForceNumericInput(this,0,false)" class="form-control numeric" placeholder="Amount" />

                                    </div>
                                    <div class="col-lg-6">
                                        <label for="exampleInputPassword2">RM_SecondYear</label>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Add" Text="*" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtRM_SecondYearAdd" CssClass="rfvbankdescription"></asp:RequiredFieldValidator>
                                        <asp:TextBox runat="server" ID="txtRM_SecondYearAdd" onkeypress="ForceNumericInput(this,0,false)" class="form-control numeric" placeholder="Amount" />

                                    </div>
                                    <div class="col-lg-6">
                                        <label for="exampleInputPassword2">RM_ThirdYear</label>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="Add" Text="*" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtRM_ThirdYearAdd" CssClass="rfvbankdescription"></asp:RequiredFieldValidator>
                                        <asp:TextBox runat="server" ID="txtRM_ThirdYearAdd" onkeypress="ForceNumericInput(this,0,false)" class="form-control numeric" placeholder="Amount" />

                                    </div>
                                    <div class="col-lg-6">
                                        <label for="exampleInputPassword2">RM_ForthYear</label>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="Add" Text="*" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtRM_ForthYearAdd" CssClass="rfvbankdescription"></asp:RequiredFieldValidator>
                                        <asp:TextBox runat="server" ID="txtRM_ForthYearAdd" onkeypress="ForceNumericInput(this,0,false)" class="form-control numeric" placeholder="Amount" />

                                    </div>

                                    <div class="col-lg-6">
                                        <label for="exampleInputPassword2">RM_FifthYear</label>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="Add" Text="*" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtRM_ForthYearAdd" CssClass="rfvbankdescription"></asp:RequiredFieldValidator>
                                        <asp:TextBox runat="server" ID="txtRM_FifthYearAdd" onkeypress="ForceNumericInput(this,0,false)" class="form-control numeric" placeholder="Amount" />

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


    <div class="modal inmodal" id="CreateProjectModalIncrease" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">Increase Percentage</h4>
                </div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">

                    <ContentTemplate>
                        <div class="modal-body">
                            <div class="form-horizontal">

                                <div class="form-group">
                                     <div class="col-lg-6">
                                        <label for="exampleInputPassword2">RM Type</label>

                                        <asp:DropDownList ID="ddlRMTypeIncrease" runat="server" CssClass="form-control ddlGroupAdd">
                                            <asp:ListItem Value="0">Not Fixed</asp:ListItem>
                                            <asp:ListItem Value="1">Fixed</asp:ListItem>
                                            <asp:ListItem Value="2">Standard</asp:ListItem>



                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-6">
                                        <label for="exampleInputEmail2">Group</label>
                                        <asp:DropDownList ID="ddlGroupIncrease" OnSelectedIndexChanged="ddlGroupIncrease_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control select2apply">
                                        </asp:DropDownList>
                                    </div>
                                    </div>
                                    <div class="form-group">
                                    <div class="col-lg-6">
                                        <label for="exampleInputEmail2">Company</label>
                                        <asp:DropDownList ID="ddlCompanyIncrease" runat="server" CssClass="form-control select2apply" AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyIncrease_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-6" runat="server" id="Div1">
                                        <label for="exampleInputEmail2">Job Category</label>
                                        <asp:DropDownList ID="ddlJobCategoryIncrease" runat="server" CssClass="form-control select2apply">
                                        </asp:DropDownList>
                                    </div>

                                   </div>

                                <div class="form-group">

                                    <div class="col-lg-6">
                                        <label for="exampleInputPassword2">Increase Percentage</label>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ValidationGroup="AddIncrease" Text="*" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtIncreasePerc" CssClass="rfvbankdescription"></asp:RequiredFieldValidator>
                                        <asp:TextBox runat="server" ID="txtIncreasePerc" onkeypress="ForceNumericInput(this,0,false)" class="form-control numeric" placeholder="Percentage" />

                                    </div>
                                    <div class="col-lg-6">
                                        <label for="exampleInputPassword2">Increase Date</label>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ValidationGroup="AddIncrease" Text="*" ForeColor="Red"
                                            Display="Dynamic" ControlToValidate="txtIncreaseDate" CssClass="rfvbankdescription"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtIncreaseDate" runat="server" class="form-control" autocomplete="off"></asp:TextBox>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button Text="Save" class="btn btn-primary" ID="btnIncreasePercentage" ValidationGroup="AddIncrease" OnClick="btnIncreasePercentage_Click" runat="server" />
                        </div>
                    </ContentTemplate>

                </asp:UpdatePanel>
            </div>
        </div>
    </div>
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

            $("#MainContent_txtIncreaseDate").datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'yyyy-MM-dd',
                onClose: function (selectedDate) {
                    $("#MainContent_txtIncreaseDate").datepicker("option", "minDate", selectedDate);
                }
            });
            $('.select2apply').select2();


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
        function OpenPopupIncrease() {
            $('.openmodalIncrease').click();
        }
    </script>
</asp:Content>

