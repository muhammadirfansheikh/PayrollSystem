<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_Tax_Certificate_New.aspx.cs" Inherits="Pages_HCM_Report_Tax_Certificate_New" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>
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
            <h2>Tax Certificate Generate
            </h2>
            <ol class="breadcrumb">

                <li>
                    <a href="#">Other Reports </a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Tax Certificate Generate" />
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
                                <div class="form-group col-lg-3">
                                    <label>Group</label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidataor2" runat="server" ValidationGroup="Search" Text="*" ForeColor="Red" Display="Dynamic" ControlToValidate="ddlGroup" InitialValue="0"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ValidationGroup="Search" Text="*" ForeColor="Red" Display="Dynamic" ControlToValidate="ddlGroup" InitialValue=""></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlGroup" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3">
                                    <label>Company</label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ValidationGroup="Search" Text="*" ForeColor="Red" Display="Dynamic" ControlToValidate="ddlCompany" InitialValue="0"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ValidationGroup="Search" Text="*" ForeColor="Red" Display="Dynamic" ControlToValidate="ddlCompany" InitialValue=""></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>


                                <div class="col-lg-2">
                                    <label>Employee Code</label>
                                    <asp:TextBox ID="txtEmployeeCode" autocomplete="off" runat="server" CssClass="form-control " AutoCompleteType="Disabled">
                                    </asp:TextBox>

                                </div>


                                <div class="col-lg-2 divFromDate">
                                    <label>From Date</label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Search" Text="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txtFromDate" InitialValue="0"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Search" Text="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txtFromDate" InitialValue=""></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtFromDate" autocomplete="off" runat="server" CssClass="form-control txtFromDate DatePickerComplete txtMonthOfPayroll" AutoCompleteType="Disabled" onpaste="return false">
                                    </asp:TextBox>

                                </div>
                                
                                <div class="col-lg-2 divToDate">
                                    <label>From Date</label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="Search" Text="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txtToDate" InitialValue="0"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="Search" Text="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txtToDate" InitialValue=""></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtToDate" autocomplete="off" runat="server" CssClass="form-control txtFromDate DatePickerComplete txtMonthOfPayroll" AutoCompleteType="Disabled" onpaste="return false">
                                    </asp:TextBox>

                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-2 divDateOfIssue">
                                    <label>Date Of Issue</label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Search" Text="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txtIssueDate" InitialValue="0"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="Search" Text="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txtIssueDate" InitialValue=""></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtIssueDate" autocomplete="off" runat="server" CssClass="form-control txtFromDate DatePickerComplete txtMonthOfPayroll" AutoCompleteType="Disabled" onpaste="return false">
                                    </asp:TextBox>

                                </div>

                                
                                <div class="col-lg-4">
                                    <label>Issued By Name</label>
                                    <asp:TextBox ID="txtIssuedBy" autocomplete="off" runat="server" CssClass="form-control " AutoCompleteType="Disabled">
                                    </asp:TextBox>

                                </div>

                                <div class="col-lg-4">
                                    <label>Designation Name</label>
                                    <asp:TextBox ID="txtDesignation" autocomplete="off" runat="server" CssClass="form-control " AutoCompleteType="Disabled">
                                    </asp:TextBox>

                                </div>
                            </div>

                     
                            <div class="row">


                                <div class="form-group col-lg-2" style="margin-top: 23px; float: right">
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default pull-right" OnClick="btnCancel_Click" Style="margin-left: 5px;" />
                                    <asp:Button ID="btnExport" runat="server" Text="Export" ValidationGroup="Search" CssClass="btn btn-primary pull-right" OnClick="btnExport_Click" />
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


        function AlertBox(title, Message, type) {
            swal(title, Message, type);
        }

    </script>
</asp:Content>

