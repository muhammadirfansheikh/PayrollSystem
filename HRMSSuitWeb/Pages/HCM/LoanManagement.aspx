<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="LoanManagement.aspx.cs" Inherits="Pages_HCM_LoanManagement" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
    <script src="../../js/Page_JS/LoanManagement.js"></script>

    <%--<style>
        .dvDetailHide {
            display: none;
        }
    </style>--%>
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


    <div class="modal inmodal" id="CreateProjectModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content animated fadeIn" style="width: 200%; margin-left: -50%">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">Loan Management</h4>

                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <div class="panel panel-info mainVehicleInformation">
                            <div class="panel-heading">
                                Loan Information
                            </div>
                            <div class="panel-body" id="panelVehicleInformation">

                                <div class="dvEntry">
                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Loan Type</label>
                                        <select class="form-control ddlLoanType" onchange="GenerateDynamicControls();"></select>
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Loan Amount</label>
                                        <input type="text" class="form-control txtLoanAmount" onkeyup="GetTotalNoInstallments();" />
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Installment Amount</label>
                                        <input type="text" class="form-control txtInstallmentAmount" onkeyup="GetTotalNoInstallments();" />
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Total Installments</label>
                                        <input type="text" class="form-control txtTotalInstallment" disabled="disabled" />
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Sanction Date</label>
                                        <input type="text" class="form-control txtSanctionDate DatePicker" onchange="GetSettlementMonth();" onclick="GetSettlementMonth();"/>
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Settlement Date</label>
                                        <input type="text" class="form-control txtSettlementDate DatePicker" />
                                    </div>
                                </div>

                            </div>


                            <div class="panel-heading">
                                Loan Details
                            </div>
                            <div class="panel-body" style="max-height: 250px; overflow: scroll">
                                <div class="dvEntryDetail dvEntry" id="dvEntryDetail">
                                </div>

                            </div>

                            <div class="row" style="margin-right:0px;margin-bottom:10px;">
                                <div class="col-lg-12" >

                                    <input type="button" class="btn btn-default pull-right" value="Cancel" onclick="ClearFields();" />
                                    <input type="button" class="btn btn-primary btnSave pull-right" value="Save" />
                                </div>
                            </div>
                        </div>



                        <div class="panel panel-info">
                            <div class="panel-heading">
                                History
                            </div>
                            <div class="panel-body" style="max-height: 250px; overflow: scroll">
                                <table class="table table-hover">
                                    <thead>
                                        <tr class="info">

                                            <th>Loan Type</th>

                                            <th>Loan Amount</th>
                                            <th>Installment Amount</th>
                                            <th>Sanction Date</th>
                                            <th>Settlement Date</th>
                                            <th>Action</th>

                                        </tr>
                                    </thead>
                                    <tbody class="tbodyLoanListing">
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
                <input type="button" data-toggle="modal" onclick="setEmployeeId('${EmployeeId}')" data-target="#CreateProjectModal" value="Allocate" class="btn btn-success openmodal" />
            </td>
        </tr>
    </script>

    <script type="text/x-jQuery-tmpl" id="LoanListing">
        <tr>

            <td class="project-title" style="font-size: 10px;">${LoanType}</td>

            <td class="project-title" style="display: none;">${LoanMasterId}</td>
            <td class="project-title" style="font-size: 10px;">${LoanAmount}</td>
            <td class="project-title" style="font-size: 10px;">${InstallmentAmount}</td>
            <td class="project-title" style="font-size: 10px;">${SanctionDate}</td>
            <td class="project-title" style="font-size: 10px;">${SettlementDate}</td>
            <td class="project-title">
                <input type="button" class="btn btn-primary" onclick="setLoanMasterId('${LoanMasterId}', '${LoanTypeId}', '${LoanAmount}', '${InstallmentAmount}', '${SanctionDate}', '${SettlementDate}')" value="Edit" />
            </td>
        </tr>
    </script>

</asp:Content>

