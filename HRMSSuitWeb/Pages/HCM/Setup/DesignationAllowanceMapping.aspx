<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="DesignationAllowanceMapping.aspx.cs" Inherits="Pages_HCM_Setup_DesignationAllowanceMapping" %>


<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
    <script src="../../../js/Control_JS/SearchEmployeeFilter.js"></script>
    <script src="../../../js/Page_JS/Constant.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="Allowance Mapping" />
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
                        <asp:Label runat="server" ID="lbl2" Text="Allowance Mapping" />
                    </strong>
                </li>
            </ol>
        </div>
    </div>

    <div class="row">


        <div class="panel-body">
            <uc:EmployeeSearchFilters runat="server" ID="EmployeeSearchFilters2" />
            <input type="button" data-toggle="modal" data-target="#MultiEmpMapAllowanceModal" value="Multi Allowance Mapping" class="btn btn-success pull-right " onclick="OpenPopupMulti();" />
            <div class="row">
                <div class="col-lg-12">
                    <div class="wrapper wrapper-content animated fadeInUp">
                        <div class="ibox">

                            <div class="ibox-content">
                                <div class="project-list">
                                    <table class="table table-hover">
                                        <thead>
                                            <tr class="info">
                                                <th>Company</th>
                                                <th>Code</th>
                                                <th>Name</th>
                                                <th>Department</th>
                                                <th>Designation</th>
                                                <th>Location</th>
                                                <th>Official Email</th>
                                                <th>Joining Date</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody class="tbodyEmployeeListing">
                                        </tbody>
                                    </table>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


    </div>


    <div class="modal inmodal" id="CreateProjectModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog" style="width: 50%;">
            <div class="modal-content animated fadeIn">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">Designation Allowance Mapping</h4>

                </div>
            </div>
            <div class="modal-footer">
            </div>
        </div>
    </div>


    <div class="modal inmodal clsModal" id="MultiEmpMapAllowanceModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog" style="width: 99%;">
            <div class="modal-content animated fadeIn">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" onclick="SetPopupOpen(false);"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">Designation Allowance Mapping</h4>

                </div>

                <div class="slimScrollBar clsPopupSearchFilter" style="max-height: 300px; overflow-y: scroll">
                    <uc:EmployeeSearchFilters runat="server" ID="EmployeeSearchFilters1" />
                </div>

                <div class="modal-body">
                    <div class="form-group">
                        <div class="panel panel-info mainVehicleInformation">
                            <div class="panel-heading">
                                Manage
                            </div>
                            <div class="panel-body slimScrollBar" id="Div2" style="max-height: 300px; overflow-y: scroll">

                                <div class="row">
                                    <div class="dvSelect">
                                        <div class="col-lg-3">
                                            <select class="form-control multiddlSearch" multiple="multiple" id="multiddlSearch">
                                            </select>
                                        </div>

                                        <div class="col-lg-1">
                                            <input type="button" onclick="DestToSrc()" class="btn btn-success btn-xs" value="<<" />
                                            <input type="button" onclick="SrcToDest()" class="btn btn-success btn-xs" value=">>" />

                                        </div>
                                    </div>
                                    <div class="col-lg-3">

                                        <select class="form-control multiddlDest " multiple="multiple" id="multiddlDest">
                                        </select>
                                    </div>

                                    <div class="col-lg-5">

                                        <div class="project-list slimScrollBar" style="max-height: 300px; overflow-y: scroll">
                                            <table class="table table-hover">
                                                <thead>
                                                    <tr class="info">

                                                        <th>
                                                            <input type="checkbox" class="chkAll" onchange="ChkAll();" /></th>
                                                        <th>Allowance</th>
                                                        <th>Measure</th>

                                                    </tr>
                                                </thead>
                                                <tbody class="tbodyAllowanceListing">
                                                </tbody>
                                            </table>

                                        </div>

                                    </div>

                                </div>

                            </div>

                        </div>

                    </div>


                </div>
                <div class="modal-footer">

                    <input type="button" class="btn btn-default pull-right btnCancel" value="Cancel" onclick="ClearFields();" style="margin-right: 5px;" />
                     <input type="button" onclick="RemoveMultipleAllowances()" class="btn btn-success " value="Remove Allowances" />
                    <input type="button" onclick="SaveMultipleAllowances()" class="btn btn-primary " value="Save" />

                   

                </div>
            </div>

        </div>
    </div>


    <script type="text/x-jQuery-tmpl" id="EmployeeListing">
        <tr>
            <td class="project-title">${Company}
                   <input type="hidden" class="hfEmployeeId" value="${EmployeeId}" />
            </td>
            <td class="project-title">${EmployeeCode}</td>
            <td class="project-title">${FirstName} ${LastName}</td>
            <td class="project-title" style="font-size: 10px;">${Department}</td>
            <td class="project-title" style="font-size: 10px;">${Designation}</td>
            <td class="project-title" style="font-size: 10px;">${Location}</td>
            <td class="project-title" style="font-size: 10px;">${OfficalEmail}</td>
            <td class="project-title" style="font-size: 10px;">${JoiningDate}</td>
            <td class="project-title">
                <input type="button" data-toggle="modal"
                    onclick="OpenPopupSingle('${EmployeeId}', '${FirstName}', '${LastName}', '${CompanyId}'); " data-target="#MultiEmpMapAllowanceModal" value="Allocate" class="btn btn-success openmodal" />

            </td>
        </tr>
    </script>

    <script type="text/x-jQuery-tmpl" id="DesignationListing">
        <tr>
            <td class="project-title">${Company}</td>
            <td class="project-title" style="font-size: 10px;">${Designation}</td>

            <td class="project-title">
                <input type="button" data-toggle="modal" onclick="setDesignationId('${DesignationId}')" data-target="#CreateProjectModal" value="Allocate" class="btn btn-success openmodal" />
            </td>
        </tr>
    </script>

    <script type="text/x-jQuery-tmpl" id="AllowanceMapListing">
        <tr class="trInputs">
            <td class="project-title">
                <input type="checkbox" class="chkSelect chkSelectAllowance" onchange="ChkSelect(this)" />
                <input type="hidden" class="hfAllowanceId" value="${Id}" />
                <input type="hidden" class="hfIsFormulaExist" value="${IsFormulaExist}" />

            </td>
            <td class="project-title clsValue">${Value}</td>
            <td class="project-title tdMajor">
                <input type="text" class="form-control numeric txtMajor" value="${Measure}" style="${Formula ? 'display:none;width:110px;': 'width:110px;'}" />
            </td>
        </tr>
    </script>

    <script src="../../../js/Page_JS/DesignationAllowanceMapping.js"></script>



</asp:Content>

