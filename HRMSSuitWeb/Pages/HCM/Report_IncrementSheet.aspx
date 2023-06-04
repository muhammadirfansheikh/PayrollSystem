<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_IncrementSheet.aspx.cs" Inherits="Pages_HCM_Report_IncrementSheet" %>

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
            <h2>Increment Sheet
            </h2>
            <ol class="breadcrumb">

                <li>
                    <a href="#">Other Reports </a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Increment Sheet" />
                    </strong>
                </li>
            </ol>
        </div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="panel panel-danger" id="divError" runat="server" visible="false">
                <div class="panel-heading" id="lblError" runat="server"></div>
            </div>
            <div class="row">
                <div class="col-lg-12" style="margin-top: 11px;">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <h3 class="panel-title">Search</h3>
                        </div>
                        <div class="panel-body">

                            <div class="row">
                                <div class="form-group col-lg-2">
                                    <label>Group</label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidataor2" runat="server" ValidationGroup="Search" Text="*" ForeColor="Red" Display="Dynamic" ControlToValidate="ddlGroup" InitialValue="0"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ValidationGroup="Search" Text="*" ForeColor="Red" Display="Dynamic" ControlToValidate="ddlGroup" InitialValue=""></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlGroup" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-4">
                                    <label>Company</label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ValidationGroup="Search" Text="*" ForeColor="Red" Display="Dynamic" ControlToValidate="ddlCompany" InitialValue="0"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ValidationGroup="Search" Text="*" ForeColor="Red" Display="Dynamic" ControlToValidate="ddlCompany" InitialValue=""></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-2 divToDate">
                                    <label>Sheet Password</label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Search" Text="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txtToDate" InitialValue=""></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtPassword" autocomplete="off" runat="server" CssClass="form-control" AutoCompleteType="Disabled" onpaste="return false">
                                    </asp:TextBox>
                                </div>

                                <div class="col-lg-2 divFromDate">
                                    <label>From Date</label>
                                    <asp:TextBox ID="txtFromDate" autocomplete="off" runat="server" CssClass="form-control DatePicker" AutoCompleteType="Disabled" onpaste="return false">
                                    </asp:TextBox>

                                </div>


                                <div class="col-lg-2 divToDate">
                                    <label>To Date</label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Search" Text="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txtToDate" InitialValue=""></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtToDate" autocomplete="off" runat="server" CssClass="form-control  DatePicker" AutoCompleteType="Disabled" onpaste="return false">
                                    </asp:TextBox>
                                </div>
                            </div>
                            <div class="row">


                                <div class="col-lg-2">
                                    <label>Year</label>
                                    <asp:TextBox ID="txtYear" autocomplete="off" runat="server" CssClass="form-control DatePickerYear" AutoCompleteType="Disabled" onpaste="return false">
                                    </asp:TextBox>

                                </div>


                                <div class="form-group col-lg-2">
                                    <label>Filter Type</label>
                                    
                                    <asp:DropDownList ID="ddlReportFilterType" runat="server" CssClass="form-control" >
                                        <asp:ListItem Value="0" Text="Select" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Department Wise"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Location Wise"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Cost Center Wise"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="Sap Cost Center Wise"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-6">
                                    <label></label>
                                    <asp:RadioButtonList ID="rdCheckStatus" runat="server" RepeatLayout="Flow">
                                        <asp:ListItem Selected="True" Value="1">Permanent</asp:ListItem>
                                        <asp:ListItem Value="0">Contractual</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <%--   <div class="col-lg-2">
                                <label>Month-Year</label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Search" Text="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txt_DatePicker" InitialValue=""></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txt_DatePicker" runat="server" CssClass="form-control DatePickerMonthComplete" AutoCompleteType="Disabled" onpaste="return false">
                                </asp:TextBox>
                            </div>--%>
                                <div class="form-group col-lg-2" style="margin-top: 23px; float: right">
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default pull-right" OnClick="btnCancel_Click" Style="margin-left: 5px;" />
                                    <asp:Button ID="btnExport" runat="server" Text="Export" ValidationGroup="Search" CssClass="btn btn-primary pull-right" OnClick="btnExport_Click" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-lg-12" runat="server">
                                    <label for="exampleInputEmail2">Employee</label>
                                    <asp:ListBox runat="server" ID="ddlEmployeeAdd" SelectionMode="multiple" CssClass="form-control select2applyEmployee"></asp:ListBox>
                                    <%--<asp:DropDownList ID="ddlEmployeeAdd" runat="server" CssClass="form-control select2applyEmployee">
                                    </asp:DropDownList>--%>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
    </asp:UpdatePanel>
    <script>

        function pageLoad() {
            $('.select2applyEmployee').select2();
            $('.DatePickerMonthComplete').datepicker({
                minViewMode: 1,
                keyboardNavigation: false,
                forceParse: false,
                autoclose: true,
                todayHighlight: true,
                format: "dd-M-yyyy",
            });
            $('.DatePickerMonthComplete').keydown(function () {
                return false;
            });


            //$('.datetime').datepicker({
            //    todayBtn: "linked",
            //    keyboardNavigation: false,
            //    forceParse: false,
            //    calendarWeeks: true,
            //    autoclose: true,
            //    format: 'mm/dd/yyyy'
            //});

            //$('.datetime').keydown(function () {
            //    return false;
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

