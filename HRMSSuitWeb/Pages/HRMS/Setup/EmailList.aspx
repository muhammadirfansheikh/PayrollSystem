<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="EmailList.aspx.cs" Inherits="Pages_HRMS_Setup_EmailList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
        <%@ Register Src="~/Controls/Shared/InProgress.ascx" TagName="InProgress" TagPrefix="uc2" %>

<%@ Register Src="~/Controls/Shared/PagingAndSorting.ascx" TagPrefix="up" TagName="PagingAndSorting" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
       
       <div class="panel panel-info">
               <div class="panel-heading">
                   <h3>Email List</h3>
               </div>
        <div>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <uc2:InProgress ID="InProgress" runat="server" />
                </ProgressTemplate>
            </asp:UpdateProgress>
            </div>
               <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
           <div class="panel-body">
              
                           <div class="row">
                        <div class="col-lg-3">
                            <label class="control-label col-lg-3">
                                Company
                            </label>
                            <div class="col-lg-9">
                                   <asp:DropDownList ID="ddlCompanySearch" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCompanySearch_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <label class="control-label col-lg-3">
                                Location
                            </label>
                            <div class="col-lg-9">
                                   <asp:DropDownList ID="ddlLocationSearch" runat="server" OnSelectedIndexChanged="ddlLocationSearch_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <label class="control-label col-lg-3">
                                Business Unit
                            </label>
                            <div class="col-lg-9">
                                                                <asp:DropDownList ID="ddlBusinessUnitSearch" AutoPostBack="true" OnSelectedIndexChanged="ddlBusinessUnitSearch_SelectedIndexChanged" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                                   <div class="col-lg-3">
                            <label class="control-label col-lg-3">
                                Department
                            </label>
                            <div class="col-lg-9">
                                                                <asp:DropDownList ID="ddlDepartmentSearch" AutoPostBack="true" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                      
                      
                    </div>
                        
                    <br />


                    <div class="row">
                        <div class="col-lg-12">
                            <div class="pull-right">
                                
                               
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-warning right" OnClick="btnSearch_Click" />
                            </div>
                        </div>
                    </div>
    

           </div>
                                    </ContentTemplate>
</asp:UpdatePanel>
           </div>


    <div class="panel panel-info">
        <div class="panel-heading">
            <h3>Department Emails</h3>
        </div>
        <div class="panel-body">
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <asp:Repeater ID="rpt" runat="server">
                        <HeaderTemplate>
                            <table class="table table-hover">

                           <thead>
                               <th>
                                   Department Name
                               </th>
                               <th>
                                   Email To
                               </th>
                               <th>
                                   CC
                               </th>
                           </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
<%# Eval("DepartmentName") %>
                                </td>
                                <td>
<%# Eval("EmailTo") %>
                                </td>
                                <td>
<%# Eval("CCEMAIL") %>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                             </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

</asp:Content>

