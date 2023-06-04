<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="TaxForcasting.aspx.cs" Inherits="Pages_HCM_TaxForcasting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="Tax Forcasting" />
            </h2>
            <ol class="breadcrumb">
                
                <li>
                    <a href="#">HCM </a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Tax Forcasting" />
                    </strong>
                </li>
            </ol>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12" style="margin-top: 11px;">
    <div class="form-group">
        <div class="panel panel-info mainVehicleInformation">
            <div class="panel-heading">
                Manage
            </div>
            <div class="panel-body dvEntry" id="panelVehicleInformation">

                <div class="form-group col-lg-2">
                    <label for="exampleIsnputEmail2">Group</label>
                    <select onchange="GetCompany(this)" class="form-control ddlGroup">
                    </select>
                </div>
                <div class="form-group col-lg-2">
                    <label for="exampleInputPassword2">Company</label>
                    <select onchange="GetTaxYear()" class="form-control ddlCompany">
                    </select>
                </div>
                <div class="form-group col-lg-2">
                    <label for="exampleInputPassword2">Tax Year</label>
                    <select disabled="disabled" class="form-control ddlTaxYear">
                    </select>
                </div>

                <div class="form-group col-lg-2">
                    <label for="exampleInputPassword2">Advance Tax %</label>
                    <input type="number" min="0" id="txtAdvanceTaxc" class="txtAdvanceTaxc form-control"/>
                </div>
                <div class="col-lg-4">
                    <div class="checkbox checkbox-primary">
                        <input id="chkAll" runat="server" type="checkbox" class="form-control chkAll"  value="Current Month Allowance Tax" />
                        <label for="chkResSalary">Current Month Allowance Tax</label>
                    </div>
                </div>

            </div>
            <div class="panel-footer">
                <input type="button" class="btn btn-default " value="Cancel" onclick="ClearFields();" />
                <input type="button" class="btn btn-primary btnSave" onclick="SaveTaxForcast();" value="Save & View" />
                <input type="button" class="btn btn-success btnView" onclick="GetTaxForcasting();" value="View" />
            </div>
        </div>
    </div>

</div>
        </div>


    <div class="row">
        <div class="col-lg-12">
            <div class="wrapper wrapper-content animated fadeInUp">
                <div class="ibox">

                    <div class="ibox-content">
                        <div class="row m-b-sm m-t-sm" style="margin: 0px;">
                            <div class="col-md-12 panel-default">
                                <div class="panel-heading" style="padding-top: 0px; padding-bottom: 0px;">
                                    <h2 class="panel-title" style="font-size: x-large; text-align: center;">Tax Forcasting Details
                                    </h2>
                                </div>
                            </div>
                        </div>
                        <div class="project-list">
                            <input type="hidden" class="hdnOpenModal" data-toggle="modal" data-target="#myModal" />
                            <table class="table table-hover tableEmployee">
                                <thead>
                                    <tr class="info">

                                        <th>Employee Code</th>
                                        <th>Name</th>
                                        <th>Department</th>
                                        <th>Tax Slab</th>
                                        <th>Taxable Income</th>
                                        <th>Tax Notional Adition</th>
                                        <th>Taxable Reduction</th>
                                        <th>Total Taxable Income</th>
                                        <th>Tax Payable</th>
                                        <th>Tax Credit</th>
                                        <th>Total Tax Payable</th>
                                    </tr>

                                </thead>
                                <tbody class="tbodyTaxForcastListing">
                                </tbody>

                                <tfoot></tfoot>
                            </table>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog" style="width: 70%">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Tax Summary</h4>
                </div>
                <div class="modal-body">
                    <table class="table table-hover">
                        <thead>
                            <tr class="info">
                                <td>Taxable Allowance Name
                                </td>
                                <td>Amount (Yearly)
                                </td>
                            </tr>
                        </thead>
                        <tbody class="tbodyTaxableAllowances">
                        </tbody>
                        <tfoot>
                            <tr class="info">
                                <td>Total</td>
                                <td class="tdYearlyTotal">0.0</td>
                            </tr>
                        </tfoot>
                    </table>

                    <table class="table table-hover">
                        <thead>
                            <tr class="info">
                                <td>Tax Law Description
                                </td>
                                <td>Amount
                                </td>
                            </tr>
                        </thead>
                        <tbody class="ForecastedDetails">
                        </tbody>
                    </table>


                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>


    <script type="text/x-jQuery-tmpl" id="TaxableAllowances">
        <tr>
            <td class="project-title" style="font-size: 10px;">${AllowanceName}</td>
            <td class="project-title tdYearlyAmount" style="font-size: 10px;">${Amount}</td>
        </tr>
    </script>


    <script type="text/x-jQuery-tmpl" id="ForecastedDetails">
        <tr class="${RowType == 'T' ? 'info' : ''}">
            <td class="project-title" style="font-size: 10px;">${AllowanceName}</td>
            <td class="project-title" style="font-size: 10px;">${Amount}</td>
        </tr>
    </script>

    <script type="text/x-jQuery-tmpl" id="TaxForcastListing">
        <tr onclick="openModal(this)">
            <input type="hidden" class="hdnEmployeeId" value="${EmployeeId}" />
            <td class="project-title" style="font-size: 10px;">${EmployeeCode}</td>
            <td class="project-title" style="font-size: 10px;">${FirstName}  ${MiddleName} ${LastName}</td>

            <td class="project-title" style="font-size: 10px;">${DepartmentName}</td>
            <td class="project-title" style="font-size: 10px;">${TaxSlab}</td>
            <td class="project-title" style="font-size: 10px;">${TaxableIncome.toFixed(0)}</td>

            <td class="project-title" style="font-size: 10px;">${TaxNotionalAdition.toFixed(0)}</td>
            <td class="project-title" style="font-size: 10px;">${TaxableReduction.toFixed(0)}</td>
            <td class="project-title" style="font-size: 10px;">${TotalTaxableIncome.toFixed(0)}</td>

            <td class="project-title" style="font-size: 10px;">${TaxPayable.toFixed(0)}</td>
            <td class="project-title" style="font-size: 10px;">${TaxCredit.toFixed(0)}</td>
            <td class="project-title" style="font-size: 10px;">${TotalTaxPayable.toFixed(0)}</td>

        </tr>
    </script>

    <script src="../../js/Control_JS/SearchEmployeeFilter.js"></script>
    <script src="../../js/Page_JS/TaxForcasting.js"></script>

    <script>
        function pageLoad() {
            //alert();
            TriggerLoad();
            TriggerPageLoads();
        }
    </script>

</asp:Content>

