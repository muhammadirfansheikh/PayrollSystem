<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="SetupBonus.aspx.cs" Inherits="Pages_HCM_Setup_SetupBonus" %>

<%@ Register Src="~/Controls/Shared/InProgress.ascx" TagName="InProgress" TagPrefix="uc2" %>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../../../js/jquerynumeric.js"></script>
    <script src="../../../js/Page_JS/Constant.js"></script>
    <style>
        .clsabc {
            min-height: 500px;
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
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="Formula Definition" />
            </h2>
            <ol class="breadcrumb">
               
                <li>
                    <a href="#">HCM Setup</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Formula Definition" />
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
                                <label for="exampleInputEmail2">Group</label>
                                <asp:DropDownList runat="server" ID="ddlGroupSearch" class="form-control ddlGroupSearch" OnSelectedIndexChanged="ddlGroupSearch_SelectedIndexChanged" AutoPostBack="true" />
                            </div>
                            <div class="form-group col-lg-3">
                                <label for="exampleInputEmail2">Company</label>
                                <asp:DropDownList runat="server" ID="ddlCompanySearch" class="form-control ddlCompanySearch" />
                            </div>
                            <div class="form-group col-lg-3">
                                <label for="exampleInputEmail2">
                                    <asp:Label runat="server" ID="lbl3" Text="Allowance / Deduction" />
                                </label>
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="form-group col-lg-3" id="dvDeductionSearch" runat="server">
                                <label for="exampleInputEmail2">Deduction</label>
                                <asp:CheckBox ID="chkIsDeductionSearch" runat="server" CssClass="form-control" Text="" />
                            </div>
                            <div class="form-group col-lg-3">
                                <label for="exampleInputEmail2">Taxable Income</label>
                                <asp:CheckBox ID="chkTaxableIncomeSearch" runat="server" CssClass="form-control" Text="" />
                            </div>
                            <div class="form-group col-lg-9" style="margin-top: 20px;" runat="server" id="div_search">
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
                                <asp:Label runat="server" ID="lbl4" Text="Records" />
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
                                                <th>Company</th>
                                                <th> <asp:Label runat="server" ID="th_Headind" Text="Allowance"></asp:Label> </th>
                                                <th>Type</th>
                                                <th>Formula</th>
                                                <th>Taxable / Non Taxable Income </th>
                                                <th style="width: 170px; text-align: center;">Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rpt" runat="server">
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
                                                        <td>
                                                            <%# Eval("TaxableIncome") %>
                                                        </td>
                                                        <td class="project-actions">
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
    <%-- Create Project Modal End--%>
    <div class="modal inmodal" id="CreateProjectModal" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog" style="width: 60%;">
            <div class="modal-content">
                <div class="modal-header" style="padding-bottom: 9px; padding-top: 9px;">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">Add / Edit 
                          <asp:Label runat="server" ID="lbl5" Text="Formula" />
                    </h4>
                </div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="modal-body clsabc" style="padding-bottom: 10px; border-bottom-width: 10px; padding-top: 10px; overflow-y: scroll;">
                            <input type="hidden" id="hfModalId" runat="server" class="hfModalId" value="0" />
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="dvValidateAll">
                                        <div class="dvValidate">
                                            <div class="col-lg-6">
                                                <label for="exampleInputPassword2">Group</label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Save" Text="*" ForeColor="Red"
                                                    Display="Dynamic" ControlToValidate="ddlGroupAdd" InitialValue="0"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="Save" Text="*" ForeColor="Red"
                                                    Display="Dynamic" ControlToValidate="ddlGroupAdd" InitialValue=""></asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlGroupAdd" OnSelectedIndexChanged="ddlGroupAdd_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control ddlGroupAdd">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-lg-6">
                                                <label for="exampleInputEmail2">Company</label>
                                                <asp:DropDownList runat="server" ID="ddlCompany" class="form-control ddlCompany" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" />
                                            </div>
                                            <div class="col-lg-6">
                                                <label for="exampleInputEmail2">Formula</label>
                                                <asp:CheckBox ID="chkbxFormula" runat="server" CssClass="form-control chkbxFormula" />
                                            </div>
                                            <div class="col-lg-6">
                                                <label for="exampleInputEmail2">
                                                    <asp:Label runat="server" ID="lbl6" Text="Allowance / Deduction" />
                                                </label>
                                                <asp:TextBox runat="server" ID="txtAllowanceDeduction" class="form-control" />
                                            </div>
                                            <div class="col-lg-12">
                                                <label for="exampleInputEmail2">
                                                    <asp:Label runat="server" ID="Label2" Text="Type" />
                                                </label>
                                                <div class="checkbox checkbox-primary" id="dvDeduction" runat="server">
                                                    <input id="chkIsDeduction" type="checkbox" class="chkDeduction" runat="server" />
                                                    <label for="chkResSalary">Deduction</label>
                                                </div>
                                                <div class="checkbox checkbox-primary" id="dvTaxable" runat="server">
                                                    <input id="chkTaxableIncome" runat="server" type="checkbox" class="chkTaxable" />
                                                    <label for="chkResSalary">Taxable</label>
                                                </div>
                                                <div class="checkbox checkbox-primary" id="Div1" runat="server">
                                                    <input id="chkArrearAllowance" type="checkbox" class="chkDeduction" runat="server" />
                                                    <label for="chkResSalary">Arrear Allowance</label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <label for="exampleInputEmail2">
                                                <asp:Label runat="server" ID="Label4" Text="Sort Order" />
                                            </label>
                                            <asp:TextBox runat="server" ID="txtSortOrder" class="form-control numeric" Text="1" />
                                        </div>
                                        <div class="col-lg-4 dvValidate">
                                            <label for="exampleInputEmail2">
                                                <asp:Label runat="server" ID="Label6" Text="Payroll Placement" />
                                            </label>
                                            <asp:TextBox runat="server" ID="txtPayrollSheetPlacement" class="form-control numeric" Text="1" />
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <label for="exampleInputEmail2">
                                            <asp:Label runat="server" ID="Label5" Text="Special Type" />
                                        </label>
                                        <asp:DropDownList runat="server" ID="ddlSpecialType" class="form-control ddlSpecialType" />
                                    </div>
                                    <div id="dvFormula" runat="server" visible="true" style="margin-top: 7px;">
                                        <div id="dvBonus" runat="server" visible="false" class="dvValidateAll">
                                            <div class="col-lg-4">
                                                <label for="exampleInputEmail2">Max Joining Date</label>
                                                <asp:TextBox runat="server" ID="txtMaxJoining" onchange="changeDateFormat(this)" class="form-control DatePickerComplete" />
                                            </div>
                                            <div class="col-lg-4">
                                                <label for="exampleInputEmail2">Bonus Date</label>
                                                <asp:TextBox runat="server" ID="txtBonusDate" onchange="changeDateFormat(this)" class="form-control DatePickerComplete" />
                                            </div>
                                            <div class="col-lg-4">
                                                <label for="exampleInputEmail2">Release Date</label>
                                                <asp:TextBox runat="server" ID="txtReleaseDate" onchange="changeDateFormat(this)" class="form-control DatePickerComplete" />
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <asp:RadioButtonList runat="server" ID="rdbtnLstType" AutoPostBack="true" OnSelectedIndexChanged="rdbtnLstType_SelectedIndexChanged">
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="col-lg-6" style="max-height: 155px; overflow-y: scroll;">
                                            <label for="exampleInputEmail2">
                                                <asp:Label runat="server" ID="lblTypeDetail" Text=""></asp:Label>
                                            </label>
                                            <asp:RadioButtonList runat="server" ID="rdbtnLstTypeDetail">
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="col-lg-6" id="dvOperatorConstant" runat="server" visible="false">
                                            <asp:TextBox runat="server" placeholder="Constant" ID="txtOperatorConstant" CssClass="form-control numeric" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Add" Text="*" ForeColor="Red"
                                                Display="Dynamic" ControlToValidate="txtOperatorConstant"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-lg-12">
                                        <label for="exampleInputEmail2"></label>
                                        <asp:TextBox ID="lblFormula" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-12" style="margin-top: 5px;">
                                        <div class="panel panel-danger" id="divError_Add" runat="server" visible="false">
                                            <div class="panel-heading" id="lblError_Add" runat="server"></div>
                                        </div>
                                        <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-primary pull-right" OnClick="btnAdd_Click" ValidationGroup="Add" />
                                    </div>

                                    <div class="project-list">
                                        <table class="table table-hover">
                                            <thead>
                                                <tr>
                                                    <th>S No.</th>
                                                    <th>Type</th>
                                                    <th>Element</th>
                                                    <th>Element Detail</th>
                                                    <th style="width: 170px; text-align: center;">Action</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptAdd" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:Label runat="server" ID="Label3" Text='<%# Eval("ID") %>' />
                                                            </td>
                                                            <td class="project-title">
                                                                <input type="hidden" runat="server" id="hfId" class="hfId" value='<%# Eval("ID") %>' />
                                                                <input type="hidden" runat="server" id="hfElementId" class="hfId" value='<%# Eval("TypeId") %>' />
                                                                <input type="hidden" runat="server" id="hfFormulaElementDetailId" class="hfId" value='<%# Eval("FormulaElementDetailId") %>' />
                                                                <asp:Label runat="server" ID="lblType" Text='<%# Eval("Type") %>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="Label1" Text='<%# Eval("TitleActual") %>' />
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

                            <asp:CustomValidator ID="CustomValidator1" runat="server" ValidationGroup="Save" ClientValidationFunction="Validate"></asp:CustomValidator>
                            <asp:Button Text="Save" class="btn btn-primary" ID="btnSave" ValidationGroup="Save" OnClick="btnSave_Click" runat="server" />
                        </div>
                    </ContentTemplate>

                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <%-- Modal End Here --%>
    <script type="text/javascript">

        function funct() {

            if ($('#<%=chkbxFormula.ClientID %>').is(":checked")) {

                $('#<%=dvFormula.ClientID %>').slideDown();
            }
            else {
                $('#<%=dvFormula.ClientID %>').slideUp();
            }

        }

        function pageLoad() {

            funct();


            $(".btnAdd").click(function () {

                reset();
                $('.openmodal').click();
            });

            DatePickerComplete();


            function reset() {
                $(".txtAdd").val('');
                $(".hfModalId").val('0');
            }

            CheckBoxFunc();

            DatePicker();

            TaxableIncomeChk();
        }

        function TaxableIncomeChk() {
            //;
            $('.chkDeduction').change(function () {

                if ($('.chkDeduction').is(":checked")) {
                    //alert();

                    $('.chkTaxable').prop("checked", false);
                    $('.chkTaxable').attr("disabled", true);
                }
                else {
                    $('.chkTaxable').removeAttr("disabled");
                }
            });

        }

        function Validate(source, args) {

            var div;
            var chkbxFormula = $('.chkbxFormula').find('input:checked').size();

            if (chkbxFormula > 0) {
                div = '.dvValidateAll';
            }
            else {
                div = '.dvValidate';
            }

            var a = validateForm(div);
            args.IsValid = a;
            return;
        }


        function CheckBoxFunc() {

            $('.chkbxFormula').change(function () {
                $('.modal-body').toggleClass('clsabc');
                $('#<%=dvFormula.ClientID %>').slideToggle();

            });

        }


        function changeDateFormat(selector) {
            var dateVal = $(selector).val();
            $(selector).val(formatDate(dateVal));
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



