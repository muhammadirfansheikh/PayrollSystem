<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="LockPayroll.aspx.cs" Inherits="Pages_HCM_LockPayroll" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="Lock Payroll" />
            </h2>
            <ol class="breadcrumb">
                
                <li>
                    <a href="#">HCM </a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Lock Payroll" />
                    </strong>
                </li>
            </ol>
        </div>
    </div>
    <div class="row" runat="server" id="DivSearchPanel">
        <div class="col-lg-12" style="margin-top: 11px;">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title">Search</h3>
                </div>
                <div class="panel-body">

                    <div class="divPayrollForm">
                        <div class="form-group col-lg-2">
                            <label for="exampleIsnputEmail2">Group</label>
                            <select onchange="GetCompany(this)" class="form-control ddlGroup">
                            </select>
                        </div>

                        <div class="form-group col-lg-2">
                            <label for="exampleInputPassword2">Company</label>
                            <select class="form-control ddlCompany">
                            </select>
                        </div>


                    </div>

                    <div class="form-group col-lg-12">

                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default pull-right" />

                        
                        <input type="button" onclick="GetPayrollLogData()" class="btn btn-primary pull-right btnSearch" value="Search" />

                    </div>

                </div>
            </div>
        </div>
    </div>

    <div class="panel panel-info">
        <div class="panel-heading">
            Payroll Listing
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tablePayroll">
                <thead class="theadPayroll">

                    <tr class="info">
                        <th>Payroll Month</th>
                        <th>Status</th>
                        <th>Action </th>
                    </tr>
                </thead>
                <tbody class="tbodyPayroll">
                </tbody>
            </table>
        </div>
    </div>
    <script type="text/x-jQuery-tmpl" id="Payroll">
        <tr>
            <td class="project-title">${PayrollDate}</td>
            <td class="project-title">${PayrollStatus}</td>
            <td class="project-title">
                <input type="hidden" class="hfPayrollLogId" value="${PayrollLogId}" />
                <input type="button" onclick="if(confirm('Are you sure you wants to lock?')){PayrollLockSeparate('${PayrollLogId}')}" class="btn btn-danger btnLock" value="Lock" style='${IsLocked == 1 ? "Display:none;" : ""}'/>
            </td>

        </tr>
    </script>

    <script src="../../js/Page_JS/LockPayroll.js"></script>


</asp:Content>

