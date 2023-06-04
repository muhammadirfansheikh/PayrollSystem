<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="YearEndClosing.aspx.cs" Inherits="Pages_HCM_YearEndClosing" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    
    <div class="row" runat="server" id="DivSearchPanel">
        <div class="col-lg-12" style="margin-top: 11px;">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title">Search</h3>
                </div>
                <div class="panel-body">

                    <div class="divYearEnd">
                        <div class="form-group col-lg-2">
                            <label for="exampleIsnputEmail2">Group</label>
                            <select onchange="GetCompany(this)" class="form-control ddlGroup">
                            </select>
                        </div>

                        <div class="form-group col-lg-2">
                            <label for="exampleInputPassword2">Company</label>
                            <select class="form-control ddlCompany" onchange="GetYear();">
                            </select>
                        </div>

                        <div class="form-group col-lg-2">
                            <label for="exampleInputPassword2">Current Year</label>
                            
                             <select class="form-control ddlYear" onchange="GetYear(); " disabled="disabled">
                            </select>
                        </div>

                       
                    </div>

                    <div class="form-group col-lg-12">

                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default pull-right" />

                        <input type="button" onclick="" class="btn btn-danger pull-right btnYearEnd" value="Execute Year End" />

                    </div>

                </div>
            </div>
        </div>
    </div>

    <script src="../../js/Page_JS/YearEndClosing.js"></script>
</asp:Content>

