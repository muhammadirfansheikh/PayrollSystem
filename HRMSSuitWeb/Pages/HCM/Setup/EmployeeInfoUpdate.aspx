<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="EmployeeInfoUpdate.aspx.cs" Inherits="Pages_HCM_Setup_EmployeeInfoUpdate" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../../../js/Control_JS/SearchEmployeeFilter.js"></script>
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" CssClass="lblHeading" Text="Employee Info Update"></asp:Label>
            </h2>
            <ol class="breadcrumb">
                <li>
                    <a href="/Pages/Default.aspx">Dashboard</a>
                </li>
                <li>
                    <a href="#">Setup</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" CssClass="lblHeading" Text="Employee Info Update"></asp:Label></strong>
                </li>
            </ol>
        </div>
    </div>

    <uc:EmployeeSearchFilters runat="server" ID="EmployeeSearchFilters" EmployeeCode="1" />
    <div class="row">
        <div class="col-lg-12">
            <div class="wrapper wrapper-content animated fadeInUp">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <h3 class="panel-title">Records</h3>
                    </div>
                    <div class="panel-body" style="overflow-x: scroll">
                        <table style="width: 2000px" class="table table-responsive table-bordered">
                            <thead>
                                <tr class="info">
                                    <th>
                                        <input type="checkbox" onclick="EnableAllRows(this)" class="chkEditAllInfo" />
                                        Edit
                                    </th>
                                    <th>EmployeeCode</th>
                                    <th>FirstName</th>
                                    <th>LastName</th>
                                    <th>BusinessUnit</th>
                                    <th>CostCentre</th>
                                    <th>Department</th>
                                    <th>Designation</th>
                                    <th>Location</th>
                                    <th>EmployeeGender</th>
                                    <th>EmployeeReligion</th>
                                    <th>MaritalStatus</th>
                                    <th>Phone</th>
                                    <th>Mobile</th>
                                    <th>GrossSalary</th>
                                </tr>
                            </thead>
                            <tbody class="EmployeeInfo">
                            </tbody>
                        </table>
                        <input type="button" class="btn btn-info btnRequestChanges" onclick="RequestChanges()" value="Request Changes" />
                        <input type="button" class="btn btn-info btnApproveRequest" onclick="ApproveAllRequest()" value="Approve Request" />
                        <input type="button" class="btn btn-danger btnRejectRequest" onclick="RejectAllRequest()" value="Reject Request" />
                    </div>
                </div>
            </div>
        </div>
    </div>


    <script type="text/x-jQuery-tmpl" id="EmployeeInfo">
        <tr>
            <input type="hidden" class="EmployeeId" value="${EmployeeId}" />
            <input type="hidden" class="CompanyId" value="${CompanyId}" />
            <input type="hidden" class="EmployeeChangeRequestId" value="${EmployeeChangeRequestId}" />
            <td class="project-title">

                <div class="col-lg-12">
                    <input type="checkbox" onclick="EnableCurrentRow(this)" class="chkEditInfo" />
                    <a href="#"><i class="fa fa-check AdminControl" onclick="ApproveRequest(this)"></i></a>
                    <a href="#"><i class="fa fa-times AdminControl" onclick="RejectRequest(this);"></i></a>
                </div>
                <div class="col-lg-4">
                </div>
                <div class="col-lg-4">
                </div>
            </td>
            <td class="project-title">
                <input class="form-control EmployeeCode" disabled="disabled" type="text" value="${EmployeeCode}" />
            </td>
            <td class="project-title">
                <input class="form-control FirstName" disabled="disabled" type="text" value="${FirstName}" />
            </td>
            <td class="project-title">
                <input class="form-control LastName" disabled="disabled" type="text" value="${LastName}" />
            </td>
            <td class="project-title">
                <select disabled="disabled" class="ddlChBU form-control"></select>
            </td>
            <td class="project-title">
                <select disabled="disabled" class="ddlChCostCentre form-control"></select>
            </td>
            <td class="project-title">
                <select disabled="disabled" class="ddlChDepartment form-control"></select>
            </td>
            <td class="project-title">
                <select disabled="disabled" class="ddlChDesignation form-control"></select>
            </td>
            <td class="project-title">
                <select disabled="disabled" class="ddlChLocation form-control"></select>
            </td>
            <td class="project-title">
                <select disabled="disabled" class="ddlChGender form-control"></select>
            </td>
            <td class="project-title">
                <select disabled="disabled" class="ddlChReligion form-control"></select>
            </td>
            <td class="project-title">
                <select disabled="disabled" class="ddlChMaritalStatus form-control"></select>
            </td>
            <td class="project-title">
                <input disabled="disabled" class="form-control PhoneNumber" type="text" value="${PhoneNumber}" /></td>
            <td class="project-title">
                <input disabled="disabled" class="form-control MobileNumber" type="text" value="${MobileNumber}" /></td>
            <td>
                <input disabled="disabled" class="form-control Salary" type="text" value="${Salary}" />
            </td>
        </tr>
    </script>

    <script src="../../../js/Page_JS/EmployeeInfoUpdate.js"></script>

</asp:Content>

