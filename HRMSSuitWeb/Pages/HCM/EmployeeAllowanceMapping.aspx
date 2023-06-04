<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="EmployeeAllowanceMapping.aspx.cs" Inherits="Pages_HCM_EmployeeAllowanceMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
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
                        <label for="exampleInputEmail2">Select Desgination</label>
                        <asp:DropDownList ID="ddlSelectLevel" runat="server" OnSelectedIndexChanged="ddlSelectLevel_SelectedIndexChanged"></asp:DropDownList>

                    </div>
                    <div class="form-group col-lg-12">
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default pull-right"/>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-primary pull-right" />
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>

