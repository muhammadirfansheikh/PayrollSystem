<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="PF_Withdraw.aspx.cs" Inherits="Pages_HCM_PF_Withdraw" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
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
                                <tbody class="tBodyDetail">
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
            <div class="modal-content animated fadeIn">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">Withdraw Provident Fund</h4>

                </div>
                <div class="modal-body">

                    <div id="div2" runat="server" visible="false" class="alert alert-warning">
                    </div>
                    <div class="form-group">
                        <div class="panel panel-info">
                            <div class="panel-heading">
                                Withdraw Amount
                            </div>
                            <div class="panel-body">
                                <label class="col-lg-2">Amount</label>
                                <div class="col-lg-6">
                                    <input required="" type="text" class="form-control numeric txtAmountWithdraw" />
                                </div>
                                <div class="col-lg-4">
                                    <input type="button" class="btn btn-primary" onclick="withdraw()" value="Save" />
                                </div>

                            </div>
                        </div>
                    </div>
                    <div id="lblerror" class="col-lg-6 danger" visible="false"></div>
                    <table class="table table-bordered tablewithdrawHistoryHead">
                        <thead>
                            <tr class="info">
                                <th>Code</th>
                                <th>Name</th>
                                <th>Withdraw Date</th>
                                <th>Withdraw Amount</th>
                            </tr>
                        </thead>
                        <tbody class="tablewithdrawHistory"></tbody>
                        <tfoot>
                            <tr  class="info">
                                <td class="project-title">Total</td>
                                <td class="project-title"></td>
                                <td class="project-title"></td>
                                <td class="project-title historyTotal"></td>
                            </tr>
                        </tfoot>
                    </table>
                </div>
            </div>
            <div class="modal-footer">
                <input type="button" class="btn btn-primary pull-right" />
             </div>
        </div>
    </div>


    <script type="text/x-jQuery-tmpl" id="wfForm">
        <tr>
            <td class="project-title">${Company}</td>
            <td class="project-title">${EmployeeCode}</td>
            <td class="project-title">${FirstName} ${LastName}</td>
            <td class="project-title" style="font-size: 10px;">${Department}</td>
            <td class="project-title" style="font-size: 10px;">${Designation}</td>
            <td class="project-title" style="font-size: 10px;">${Location}</td>
            <td class="project-title" style="font-size: 10px;">${OfficalEmail}</td>
            <td class="project-title" style="font-size: 10px;">${JoiningDate}</td>
            <%--<td class="project-title">
                <input type="button" onclick="BindWithdrawHistory(${EmployeeId})" data-toggle="modal" data-target="#CreateProjectModal" value="Withdraw" class="btn btn-success openmodal" />
            </td>--%>
        </tr>
    </script>


    <script type="text/x-jQuery-tmpl" id="bindWithdrawHistory">

        <tr>
            <td class="project-title">${EmployeeCode}</td>
            <td class="project-title">${FirstName} ${LastName}</td>
            <td class="project-title">${WithdrawDate}</td>
            <td class="project-title historyAmount">${WithdrawAmount}</td>
        </tr>

    </script>








    <script src="../../js/Page_JS/PF_Withdraw.js"></script>
    <script>
        function pageLoad() {
            TriggerLoad();
        }

    </script>

</asp:Content>

