<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="ArrearManagement.aspx.cs" Inherits="Pages_HCM_ArrearManagement" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
    '
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <uc:EmployeeSearchFilters runat="server" ID="EmployeeSearchFilters" />

    <div class="row">
        <div class="col-lg-12">
            <div class="wrapper wrapper-content animated fadeInUp">
                <div class="ibox">

                    <div class="ibox-content">
                        <div class="row m-b-sm m-t-sm" style="margin: 0px;">
                            <div class="col-md-12 panel-default">
                                <div class="panel-heading" style="padding-top: 0px; padding-bottom: 0px;">
                                    <h2 class="panel-title" style="font-size: x-large; text-align: center;">Employee Detail
                                    </h2>
                                </div>
                            </div>
                        </div>
                        <div class="project-list">
                            <table class="table table-hover tableEmployee">
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

                                        <th colspan="2" align="center">Action</th>
                                    </tr>
                                </thead>
                                <tbody class="tbodyEmployeeListing">
                                </tbody>
                                <tfoot>
                                </tfoot>

                            </table>

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
                    <h4 class="modal-title">Arrear Management</h4>

                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <div class="panel panel-info mainVehicleInformation">
                            <div class="panel-heading">
                                Arrear Management
                            </div>
                            <div class="panel-body slimScrollBar" id="panelArrearInputs" style="max-height: 300px; overflow-y: scroll">

                                <div class="divArrearInputs">
                                    <div class="row">
                                        <div class="col-lg-4">
                                            <label for="exampleIsnputEmail2">Arrear Type</label>
                                            <select class="form-control ddlArrearType"></select>
                                        </div>

                                        <div class="col-lg-4">
                                            <label for="exampleIsnputEmail2">Amount</label>
                                            <input type="text" class="form-control numeric txtAmount" />
                                        </div>


                                        <div class="col-lg-3">
                                            <label for="exampleIsnputEmail2">Dispersed Date</label>
                                            <input type="text" class="form-control dateDispersedDate DatePicker" />
                                        </div>

                                        <button onclick="removeSelectedDiv(this,'.divArrearInputs')" class="btn btn-danger btn-circle" style="margin-top: 3.5%" type="button">
                                            <i class="fa fa-times"></i>
                                        </button>

                                    </div>
                                </div>
                            </div>
                            <div class="panel-footer">
                                <input type="button" onclick="cloneDiv('.divArrearInputs', '#panelArrearInputs')" class="btn btn-warning" value="Add More" />
                                <input type="button" onclick="GetArrearInformation()" class="btn btn-primary" value="Save" />

                            </div>
                        </div>

                        <div class="panel panel-info">
                            <div class="panel-heading">
                                History
                            </div>
                            <div class="panel-body" style="max-height: 300px; overflow-y: scroll">
                                <table class="table table-hover">
                                    <thead>
                                        <tr class="info">

                                            <th>Employee Name</th>

                                            <th>Arrear Amount</th>
                                            <th>Arrear Date</th>
                                            <th>Disbursement Date</th>
                                            <th>Is Disbursed</th>

                                        </tr>
                                    </thead>
                                    <tbody class="tbodyArrearHistory">
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div id="lblerror" class="col-lg-6 danger" visible="false"></div>
                </div>
            </div>
            <div class="modal-footer">
            </div>
        </div>
    </div>


    <script type="text/x-jQuery-tmpl" id="EmployeeListing">
        <tr>
            <td class="project-title">${Company}</td>
            <td class="project-title">${EmployeeCode}</td>
            <td class="project-title">${FirstName} ${LastName}</td>
            <td class="project-title" style="font-size: 10px;">${Department}</td>
            <td class="project-title" style="font-size: 10px;">${Designation}</td>
            <td class="project-title" style="font-size: 10px;">${Location}</td>
            <td class="project-title" style="font-size: 10px;">${OfficalEmail}</td>
            <td class="project-title" style="font-size: 10px;">${JoiningDate}</td>
            <td class="project-title">
                <input type="button" data-toggle="modal" onclick="setEmployeeId('${EmployeeId}')" data-target="#CreateProjectModal" value="Manage" class="btn btn-success openmodal" />
            </td>
        </tr>
    </script>


    <script type="text/x-jQuery-tmpl" id="ArrearHistory">
        <tr>
            <td class="project-title">${FirstName} ${LastName}</td>
            <td class="project-title" style="font-size: 10px;">${ArrearAmount}</td>
            <td class="project-title" style="font-size: 10px;">${ArrearDate}</td>
            <td class="project-title" style="font-size: 10px;">${DispersedDate}</td>
            <td class="project-title" style="font-size: 10px;">

                <span class="${IsDispersed ? 'badge badge-primary' : 'badge badge-danger'}">    ${IsDispersed ? 'Yes' : 'No'}</span>
            </td>
        </tr>
    </script>

    <script src="../../js/Page_JS/ArrearManagement.js"></script>

</asp:Content>

