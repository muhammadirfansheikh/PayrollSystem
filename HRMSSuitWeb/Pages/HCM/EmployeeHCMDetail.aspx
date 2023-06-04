<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="EmployeeHCMDetail.aspx.cs" Inherits="Pages_HCM_EmployeeHCMDetail" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>  

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
 
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="Employee HCM Detail" />
            </h2>
            <ol class="breadcrumb">

                <li>
                    <a href="#">HCM </a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Employee HCM Detail" />
                    </strong>
                </li>
            </ol>
        </div>
    </div>
    <uc:EmployeeSearchFilters runat="server" ID="EmployeeSearchFilters" EmployeeCode="1" />


    <div class="row">
        <div class="col-lg-12">
            <div class="wrapper wrapper-content animated fadeInUp">
                <div class="ibox">

                    <div class="ibox-content">
                        <div class="row m-b-sm m-t-sm" style="margin: 0px;">

                            <div class="col-md-12 panel-default">
                                <div class="panel-heading" style="padding-top: 0px; padding-bottom: 0px;">
                                    <h2 class="panel-title" style="font-size: x-large; text-align: center;">Employee HCM Detail
                                    </h2>
                                </div>
                            </div>
                        </div>
                        <div class="project-list">
                            <div class="table-responsive">
                                <table class="table table-hover tableEmployee">
                                    <thead>
                                        <tr class="info">
                                            <th>Company</th>
                                            <th>Employee Code</th>
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

                                    <tfoot></tfoot>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="modal inmodal" id="CreateProjectModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog" style="width: 90%">
            <div class="modal-content animated fadeIn">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title panelHeading"></h4>
                    <h5 class="modal-title panelHeading1"></h5>
                    <small class="font-bold panelSubHeading"></small>

                </div>
                <div class="modal-body">
                    <input type="hidden" class="Payroll_Lock_Count" value="0" />
                    <ul class="nav nav-tabs">
                        <%-- <li class="active"><a data-toggle="tab" href="#home">Home</a></li>--%>
                        <li class="Tab_ active Tab_Salary"><a data-toggle="tab" href="#Salary">Salary</a></li>
                        <li class="Tab_"><a data-toggle="tab" href="#Vehicle">Vehicle</a></li>
                        <li class="Tab_"><a data-toggle="tab" href="#Loan">Loan</a></li>
                        <li class="Tab_" style="display: none;"><a data-toggle="tab" href="#Arrear">Arrear</a></li>
                        <li class="Tab_"><a data-toggle="tab" href="#Allowances">Allowances</a></li>
                        <li class="Tab_"><a data-toggle="tab" href="#ProvidentFund">Provident Fund</a></li>
                        <li class="Tab_"><a data-toggle="tab" href="#Increment">Increment</a></li>
                        <li class="Tab_"><a data-toggle="tab" href="#TaxSettings">Tax Settings</a></li>
                    </ul>

                    <div class="tab-content">
                        <%--  <div id="home" class="tab-pane fade in active">
                            <h3></h3>
                            <p></p>
                        </div>--%>

                        <div id="Salary" class="Tab_ Tab_Salary tab-pane fades active">
                            <div class="panel panel-info">
                                <div class="panel-heading">Salary</div>
                                <div class="panel-body">
                                    <div class="divNormalSalaryForm">
                                        <div class="col-lg-2">
                                            <label for="exampleIsnputEmail2">Salary Standard</label>
                                            <input type="hidden" class="hdnStandard" />
                                            <input type="text" disabled="disabled" class="form-control txtSalaryStandard" />
                                        </div>
                                        <div class="col-lg-2">
                                            <label for="exampleIsnputEmail2">Employee Salary</label>
                                            <input type="text" onkeyup="GeneratePercentage()" class="form-control numeric txtSalary" />
                                        </div>
                                    </div>
                                </div>
                                <div class="panel-footer">
                                    <input type="button" class="btn btn-primary btn_SaveSalary" onclick="onSaveSalary(this, false)" value="Save" />
                                </div>
                            </div>
                            <div class="panel panel-info">
                                <div class="panel-heading">Salary Transactional History</div>
                                <div class="panel-body" style="overflow: scroll">
                                    <table class="table table-hover">
                                        <thead class="theadSalaryHistory"></thead>
                                        <tbody class="tbodySalaryHistory"></tbody>
                                    </table>
                                </div>
                            </div>
                            <div class="panel panel-info">
                                <div class="panel-heading">Salary History</div>
                                <div class="panel-body">
                                    <table class="table table-hover">
                                        <thead class="theadSalaryChangeHistory">
                                            <tr>
                                                <th>Activity Date</th>
                                                <th>Salary</th>
                                                <th>Activity Type</th>
                                            </tr>
                                        </thead>
                                        <tbody class="tbodySalaryChangeHistory"></tbody>
                                    </table>
                                </div>
                            </div>
                        </div>

                        <div id="Vehicle" class="Tab_ tab-pane fade">
                            <div class="form-group">
                                <div class="panel panel-info mainVehicleInformation">
                                    <div class="panel-heading">
                                        Vehicle Information
                                    </div>
                                    <div class="panel-body panelVehicleInformation" id="panelVehicleInformation">

                                        <div class="divControlsVehicle" id="divControlsVehicle">
                                            <div class="row">
                                                <div class="divControlsMandotory">
                                                    <div class="col-lg-2">
                                                        <label for="exampleIsnputEmail2">Category</label>
                                                        <select class="form-control ddlCategory" onchange="GetVehicleList()">
                                                            <option value="0">Select</option>
                                                            <option value="1">Eligible</option>
                                                            <option value="2">Upgrade</option>
                                                        </select>
                                                    </div>

                                                    <div class="col-lg-2">
                                                        <label for="exampleIsnputEmail2">Vehicle Select</label>
                                                        <select class="form-control ddlVehicle"></select>
                                                    </div>

                                                    <div class="col-lg-2">
                                                        <label for="exampleIsnputEmail2">Chasis Number</label>
                                                        <input type="text" class="form-control ChasisNo" />
                                                    </div>

                                                    <div class="col-lg-2">
                                                        <label for="exampleIsnputEmail2">Engine Number</label>
                                                        <input type="text" class="form-control EngineNo" />
                                                    </div>

                                                    <div class="col-lg-2">
                                                        <label for="exampleIsnputEmail2">Registration Number</label>
                                                        <input type="text" class="form-control RegNo" />
                                                    </div>

                                                    <div class="col-lg-2">
                                                        <label for="exampleIsnputEmail2">Installment Amount</label>
                                                        <input type="text" disabled="disabled" class="form-control numeric txtInstAmt" value="0" />
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="row">

                                                <div class="col-lg-2">
                                                    <label for="exampleIsnputEmail2">Purchase Date</label>
                                                    <%--<input type="text" class="form-control DatePicker PurchaseDate" />--%>
                                                    <input type="text" class="form-control DatePickerComplete PurchaseDate" />
                                                </div>

                                                <div class="col-lg-2">
                                                    <label for="exampleIsnputEmail2">Allowance Date</label>
                                                    <%--<input type="text" class="form-control DatePicker AllowanceDate" />--%>
                                                    <input type="text" class="form-control DatePickerComplete AllowanceDate" />
                                                </div>

                                                <div class="col-lg-2">
                                                    <label for="exampleIsnputEmail2">Pur Value (Elg)</label>
                                                    <input type="text" class="form-control numeric PurchaseAmount" />
                                                </div>

                                                <div class="col-lg-2">
                                                    <label for="exampleIsnputEmail2">Pur Value (Upg)</label>
                                                    <input type="text" class="form-control numeric PurchaseAmountUpgraded" />
                                                </div>

                                                <div class="col-lg-2">
                                                    <label for="exampleIsnputEmail2">Written Down Value</label>
                                                    <input type="text" class="form-control numeric BookValue" onkeyup="calculateInstallmentAmount()" />
                                                </div>

                                                <div class="col-lg-2">
                                                    <label for="exampleIsnputEmail2">Is Ownership Deduction</label>
                                                    <asp:CheckBox ClientIDMode="Static" ID="chkOwnerShip" CssClass="form-control OwnershipDeduction" onclick="toggle()" runat="server" />
                                                </div>

                                            </div>
                                            <div class="row">

                                                <div class="col-lg-2">
                                                    <label for="exampleIsnputEmail2">Balance</label>
                                                    <input disabled="disabled" type="text" class="form-control Balance" />
                                                </div>

                                                <div class="col-lg-2 divInstallment">
                                                    <label for="exampleIsnputEmail2" class="lblInstallmentAmount">Total Installments</label>
                                                    <input type="text" class="form-control numeric InstallmentAmount" onkeyup="calculateInstallmentAmount();" />
                                                </div>

                                                <div class="col-lg-2 divCheque">
                                                    <label for="exampleIsnputEmail2" class="lblUpgradeAmount">Cheque/Pay Order No.</label>
                                                    <input type="text" class="form-control ChqNo" />
                                                </div>

                                                <div class="col-lg-2" id="divCarSettlementDate" style="display: none">
                                                    <label for="exampleIsnputEmail2">Car Settlement Date</label>
                                                    <input type="text" class="form-control DatePickerComplete CarSettlementDate" />
                                                </div>


                                                <%--  <div class="col-lg-2 divVehicleDifference" style="visibility: hidden">
                                                    <label for="exampleIsnputEmail2">Difference Vehicle</label>
                                                    <select class="form-control ddlVehicleDifference" onchange="VehicleEligbleVehicleDiffChange()"></select>
                                                </div>--%>

                                                <%-- <div class="col-lg-2 divupgradeamount" style="visibility: hidden">
                                                    <label for="exampleisnputemail2" class="lblupgradeamount">upg diff amount</label>
                                                    <input type="text" class="form-control numeric upgradeamount" />
                                                </div>

                                                <div class="col-lg-2" style="visibility: hidden">
                                                    <label for="exampleIsnputEmail2">Is Vehicle Payment</label>
                                                    <asp:CheckBox ClientIDMode="Static" ID="chkVehiclePayment" CssClass="form-control chkVehiclePayment" onclick="" runat="server" />
                                                </div>--%>
                                            </div>
                                            <div class="hidden">
                                                <div class="col-lg-2">
                                                    <label for="exampleIsnputEmail2">Car Allowance</label>
                                                    <input disabled="disabled" type="text" class="form-control txtCarAllowance" />
                                                </div>
                                            </div>

                                        </div>
                                        <div class="hidden" >
                                            <div class="hidden">
                                                <label for="exampleIsnputEmail2">Adj Installment Amount</label>
                                                <input type="text" class="form-control numeric txtCurrMonthInstallment" disabled value="0" />
                                            </div>

                                            <div class="hidden">
                                                <label for="exampleIsnputEmail2">Ins Adj Till Date</label>
                                                <input type="text" class="form-control numeric txtInsAdjTillDate" disabled />
                                            </div>
                                            <div class="hidden">
                                                <label for="exampleIsnputEmail2">Comments</label>
                                                <input disabled="disabled" type="text" class="form-control Comments" />
                                            </div>
                                        </div>


                                    </div>
                                    <div class="panel-footer">
                                        <input type="button" onclick="ResetControls()" class="btn btn-danger" value="Reset" />
                                        <input type="button" onclick="InsertVehicleInformation()" class="btn btn-primary" value="Save" />
                                    </div>
                                </div>


                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        History
                                    </div>
                                    <div class="panel-body" style="max-height: 300px; overflow: scroll">
                                        <table class="table table-hover">
                                            <thead>
                                                <tr class="info">
                                                    <%--<th>Category</th>
                                                    <th>Vehicle</th>
                                                    <th>Reg. No.</th>
                                                    <th>Chasis No.</th>
                                                    <th>Engine No.</th>
                                                    <th>Purchase Value (Upgraded)</th>
                                                    <th>Purchase Value (Eligible)</th>
                                                    <th>Written Down Value</th>
                                                    <th>Recovered Amount</th>
                                                    <th>Remaining Installment</th>
                                                    <th>Installment Amount</th>
                                                    <th>Adjustment Installment Amount</th>
                                                    <th>Upgrade Amount</th>
                                                    <th>Purchase Date</th>
                                                    <th>Allowance Date</th> 
                                                    <th>Cheque/Pay Order</th>
                                                    <th>Is Vehicle Payment</th>
                                                    <th>Is Ownership Deduction</th>
                                                    <th>Action</th>--%>


                                                    <th>Category</th>
                                                    <th>Vehicle</th>
                                                    <th>Purchase Value</th>
                                                    <th>Payment Detail</th>
                                                    <th>Installment Amount</th>
                                                    <th>Dates</th>
                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody class="tbodyVehicleHistory">
                                            </tbody>
                                        </table>
                                    </div>
                                </div> 

                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        Transactional History
                                    </div>
                                    <div class="panel-body" style="max-height: 300px; overflow: scroll">
                                        <table class="table table-hover">
                                            <thead>
                                                <tr class="info">
                                                    <th>Vehicle</th>
                                                    <th>Installment Amount</th>
                                                    <th>Balance</th>
                                                    <th>Installment Date</th>
                                                </tr>
                                            </thead>
                                            <tbody class="tbodyVehicleTransactionalHistory">
                                            </tbody>
                                        </table>
                                    </div>
                                </div> 

                            </div>
                        </div>

                        <div id="Arrear" class="Tab_ tab-pane fade">
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
                                                    <label for="exampleIsnputEmail2">Dispersed Month</label>
                                                    <input type="text" class="form-control dateDispersedDate DatePickerMonthComplete" />
                                                </div>

                                                <button onclick="removeSelectedDiv(this,'.divArrearInputs')" class="btn btn-danger btn-circle" style="margin-top: 1.9%" type="button">
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
                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody class="tbodyArrearHistory">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div id="Allowances" class="Tab_ tab-pane fade">
                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    Manage Allowances
                                </div>
                                <div class="panel-body">
                                    <div class="frmControlsMappingAll">
                                        <div class="frmControlsMapping">
                                            <div class="col-lg-2 divAllowances">
                                                <label for="exampleIsnputEmail2">Allowance Name</label>
                                                <select class="form-control ddlAllowancesList" onchange="onAllownceSelect()"></select>
                                            </div>

                                            <div class="col-lg-2 divMeasure">
                                                <label for="exampleIsnputEmail2">Measure/Amount</label>
                                                <input type="text" class="form-control numeric txtMeasure" />
                                            </div>
                                        </div>
                                        <div class="col-lg-2 divCheckTaxable">
                                            <label for="exampleIsnputEmail2">Release Tax</label>
                                            <asp:CheckBox ClientIDMode="Static" ID="chkTaxable" CssClass="form-control Taxable" onclick="toggleReleaseTaxMonth(this)" runat="server" />
                                        </div>


                                        <div class="col-lg-2 divReleaseAtOnce">
                                            <label for="exampleIsnputEmail2">Release Month</label>
                                            <input type="text" class="form-control DatePickerMonth txtReleaseTaxMonth" />
                                        </div>
                                    </div>


                                </div>

                                <div class="panel-footer">
                                    <input type="button" class="btn btn-primary btnAdd" onclick="onMapAllowance()" value="Add and Forecast" />
                                </div>
                            </div>
                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    Mapped Allowances
                                </div>
                                <div class="panel-body" style="max-height: 300px; overflow-y: scroll">
                                    <table class="table table-hover">
                                        <thead>
                                            <tr class="info">
                                                <th>Allowance Name</th>
                                                <th>Measure</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody class="tbodyMappedAllowances">
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>

                        <div id="ProvidentFund" class="Tab_ tab-pane fades ProvidentFund">
                            <div class="panel panel-info">
                                <div class="panel-heading">Provident Fund</div>
                                <div class="panel-body">

                                    <div class="col-lg-2">
                                        <div class="col-lg-10">
                                            <label for="exampleIsnputEmail2">Provident Fund Opening</label>
                                            <input type="text" class="form-control numeric txtPFOpening" value="0" />
                                        </div>
                                        <div class="col-lg-2">
                                            <input type="button" value="Edit" class="btn btn-danger btnEditPFOpening" onclick="onEditPFOpening()" />
                                        </div>
                                    </div>


                                    <div class="col-lg-2 txtWithdrawAmount">
                                        <label for="exampleIsnputEmail2" class="txtWithdrawAmount">Employee Balance Withdraw</label>
                                        <input type="text" placeholder="Wthdraw Amount" class="form-control numeric txtEmpBalanceWithdraw" disabled="disabled" value="0" />
                                    </div>

                                    <div class="col-lg-2 txtWithdrawAmount">
                                        <label for="exampleIsnputEmail2" class="txtWithdrawAmount">Company Balance Withdraw</label>
                                        <input type="text" placeholder="Wthdraw Amount" class="form-control numeric txtCompBalanceWithdraw" disabled="disabled" value="0" />
                                    </div>

                                    <div class="col-lg-2 txtWithdrawAmount">
                                        <label for="exampleIsnputEmail2" class="txtWithdrawAmount">Provident Fund Balance Withdraw</label>
                                        <input type="text" placeholder="Wthdraw Amount" class="form-control numeric txtTotalBalanceWithdraw" onchange="onChangePFBalance(this);" value="0" />
                                    </div>

                                    <div class="col-lg-2 txtWithdrawAmount">
                                        <label for="exampleIsnputEmail2" class="txtWithdrawAmount">Interest Income Withdraw</label>
                                        <input type="text" placeholder="Wthdraw Amount" class="form-control numeric txtProfitWithdraw" onchange="onChangePFIntrest(this);" value="0" />
                                    </div>

                                    <div class="col-lg-2 txtWithdrawAmount">
                                        <label for="exampleIsnputEmail2" class="txtWithdrawAmount">Total Amount Withdraw</label>
                                        <input type="text" placeholder="Wthdraw Amount" style="margin-top: 18px;" class="form-control numeric txtTotalWithdraw" disabled="disabled" value="0" />
                                    </div>

                                    <div class="col-lg-12 txtWithdrawAmount" style="margin-top: 10px;">
                                        <div class="col-lg-2 txtWithdrawAmount">
                                            <label for="exampleIsnputEmail2">Withdraw Date</label>
                                            <input type="text" class="form-control dvEntry txtwithdrawdate DatePickerComplete" />
                                        </div>
                                        <div class="col-lg-2 pull-right" style="margin-top: 22px;">
                                            <input type="button" class="btn btn-warning btnWithdrawPF" onclick="if(confirm('Are you sure you wants to save?')){SaveWithdraw()}" value="Withdraw Provident Fund" />
                                        </div>
                                    </div>

                                </div>
                                <div class="panel-footer">
                                    <input type="button" class="btn btn-primary btnSavePFOpening" onclick="SaveProvidentHistory()" value="Save" />
                                </div>
                            </div>
                            <div class="panel panel-info">
                                <div class="panel-heading">Zakat Calculation</div>
                                <div class="panel-body" style="max-height: 300px; overflow-y: scroll">
                                    <div class="row">
                                        <div class="col-lg-2">
                                            <label for="exampleIsnputEmail2">Date</label>
                                            <input type="text" class="form-control dvEntry txtzakat DatePickerMonthComplete" />
                                        </div>
                                        <div class="col-lg-2">
                                            <label for="exampleIsnputEmail2" class="lblzakatempcode">Employee Code</label>
                                            <input type="text" placeholder="Employee Code" class="form-control numeric txtzakatempcode" disabled />
                                        </div>
                                        <div class="col-lg-2">
                                            <label for="exampleIsnputEmail2" class="lblzakatempcode">Calculated Zakat</label>
                                            <input type="text" class="form-control numeric txtCalculatedZakat" disabled />
                                        </div>
                                    </div>


                                    <div class="row">
                                        <div class="col-lg-12">
                                            <input type="button" class="btn btn-primary pull-right m-r-sm" value="search" onclick="getzakatcalculation();" />
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="panel panel-info">
                                <div class="panel-heading">Provident Fund History</div>
                                <div class="panel-body" style="max-height: 300px; overflow-y: scroll">
                                    <table class="table table-hover">
                                        <thead class="theadProvidentHistory">
                                            <tr class="info">
                                                <%--   <th>Activity Date</th>--%>
                                                <th>Employee Contribution</th>
                                                <th>Company Contribution</th>
                                                <th>Employee Balance</th>
                                                <th>Company Balance</th>
                                                <th>Interest Income</th>
                                                <th>Total Balance</th>
                                            </tr>
                                        </thead>
                                        <tbody class="tbodyProvidentHistory"></tbody>
                                    </table>

                                </div>
                            </div>
                            <div class="panel panel-info">
                                <div class="panel-heading">Fund Withdrawl History</div>
                                <div class="panel-body" style="max-height: 300px; overflow-y: scroll">

                                    <table class="table table-bordered tablewithdrawHistoryHead">
                                        <thead>
                                            <tr class="info">
                                                <th>Code</th>
                                                <th>Withdraw Date</th>
                                                <th>Withdraw Amount</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody class="tablewithdrawHistory"></tbody>
                                        <tfoot>
                                            <tr class="info">
                                                <td class="project-title">Total</td>
                                                <td class="project-title"></td>
                                                <td class="project-title historyTotal"></td>
                                                <td class="project-title"></td>
                                            </tr>
                                        </tfoot>
                                    </table>

                                </div>
                            </div>
                        </div>

                        <div id="Loan" class="Tab_ tab-pane fades">
                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    Loan Information
                                </div>
                                <div class="panel-body">

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Loan Type</label>
                                        <select class="form-control dvEntry ddlLoanType" onchange="GenerateDynamicControls();GetInterestRate();"></select>
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Loan Amount</label>
                                        <input type="text" class="form-control dvEntry txtLoanAmount decimals" onkeyup="GetTotalNoInstallments();" onchange="GetInterestRate();" />
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Total Balance</label>
                                        <input type="text" class="form-control txtTotalBalance decimals" disabled="disabled" value="0" />
                                    </div>
                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Total Installments </label>
                                        <input type="text" class="form-control dvEntry txtTotalInstallment integers" onkeyup="GetInstallmentAmountByTotalInstallment();GetInterestRate();">
                                    </div>


                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Loan Given Date</label>
                                        <input type="text" class="form-control dvEntry txtLoanGivenDate DatePickerComplete" />
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Sanction Date</label>
                                        <input type="text" class="form-control dvEntry txtSanctionDate DatePickerMonthComplete" onchange="GetSettlementMonth();GetInterestRate();SetLoanGivenDate();" />
                                    </div>
                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Installment Amount</label>
                                        <input type="text" class="form-control txtInstallmentAmount decimals" onkeyup="GetTotalNoInstallments();GetInterestRate();" disabled="disabled" />
                                    </div>
                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Settlement Date</label>
                                        <input type="text" class="form-control txtSettlementDate" disabled="disabled" />
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Interest Rate ( % )</label>
                                        <input type="text" class="form-control txtInterestRate decimals" disabled="disabled" value="0" />
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Interest Amount</label>
                                        <input type="text" class="form-control txtInterestAmount decimals" disabled="disabled" value="0" />
                                        <input type="hidden" class="hfInterestId" value="0" />
                                    </div>

                                    <div class="col-lg-4">
                                    </div>
                                </div>


                                <div class="panel-heading">
                                    Loan Details
                                </div>
                                <div class="panel-body" style="max-height: 250px; overflow: scroll">

                                    <%--  <div class="dvEntryDetail dvEntry" id="dvEntryDetail">
                                    </div>--%>

                                    <div class="col-lg-12">

                                        <div class="clsInput cls_0 col-lg-6 dvEntry">
                                            <label for="exampleIsnputEmail2">Reason</label>
                                            <textarea cols="40" rows="5" class="form-control txtReason" style="resize: none;" title="29"></textarea>

                                        </div>

                                        <div class="clsInput cls_0 col-lg-6 dvEntry">
                                            <label for="exampleIsnputEmail2">Comments</label>
                                            <textarea cols="40" rows="5" class="form-control txtComments" style="resize: none;" title="29"></textarea>

                                        </div>

                                    </div>

                                </div>

                                <%-- <div class="row" style="margin-right: 0px; margin-bottom: 10px;">
                                    <div class="col-lg-12">--%>
                                <div class="panel-footer">
                                    <input type="button" class="btn btn-default " value="Cancel" onclick="ClearFields(); GetLoanAllow();" />
                                    <input type="button" class="btn btn-primary btnSave btnSaveLoan" value="Save" />


                                </div>
                                <%--</div>
                                </div>--%>
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
                                                <th>Balance</th>
                                                <th>Sanction Date</th>
                                                <th>Loan Given Date</th>
                                                <th>Settlement Date</th>
                                                <th>Status</th>
                                                <th style="text-align: center;">Action</th>

                                            </tr>
                                        </thead>
                                        <tbody class="tbodyLoanListing">
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>

                        <div id="Increment" class="Tab_ tab-pane fades">
                            <div class="panel panel-info">
                                <div class="panel-heading">Increment</div>
                                <div class="panel-body">
                                    <div class="divIncrementSalaryForm">
                                        <div class="col-lg-2">
                                            <label for="exampleIsnputEmail2">Salary Standard</label>
                                            <input type="hidden" class="hdnStandard" />
                                            <input type="text" disabled="disabled" class="form-control txtSalaryStandard" />
                                        </div>
                                        <div class="col-lg-2">
                                            <label for="exampleIsnputEmail2">Employee Salary</label>
                                            <input type="text" onkeyup="GeneratePercentage()" class="form-control numeric txtSalaryInc" />
                                        </div>
                                        <div class="col-lg-2 divIncrementPercentage">
                                            <label for="exampleIsnputEmail2">Increment Percentage</label>
                                            <input type="text" onkeyup="GenerateSalaryChange()" value="0" maxlength="3" class="form-control integers txtPercentage" />
                                        </div>
                                        <div class="col-lg-2">
                                            <label for="exampleIsnputEmail2">Increment Type</label>
                                            <select class="form-control ddlIncrementType"></select>
                                        </div>
                                        <div class="col-lg-2 divIncrementMonth">
                                            <label for="exampleIsnputEmail2">Increment Start Date</label>
                                            <input type="text" class="form-control IncrementSalaryMonth DatePickerComplete" />
                                        </div>
                                    </div>
                                </div>
                                <div class="panel-footer">
                                    <input type="button" class="btn btn-primary btn_SaveIncrementSalary" onclick="onSaveSalary(this, true)" value="Save" />
                                </div>
                            </div>
                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    Increment Information
                                </div>
                                <div class="panel-body">
                                    <table class="table table-hover">
                                        <thead>
                                            <tr>
                                                <th>Activity Date</th>
                                                <th>Salary</th>
                                                <th>Increment Type</th>
                                                <th>Effected</th>
                                                <%-- <th>Initiated Date</th>--%>
                                                <th>Increment Start Date</th>
                                                <%--  <th>Effected Date</th>--%>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody class="tbodyIncrementChangeHistory"></tbody>
                                    </table>
                                </div>

                            </div>
                        </div>

                        <div id="TaxSettings" class="Tab_ tab-pane fades">
                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    Tax Information
                                </div>
                                <div class="panel-body">

                                    <div class="dvEntryTax">

                                        <div class="col-lg-2">
                                            <label for="exampleIsnputEmail2">Tax Law</label>
                                            <select onchange="activeDate(this)" class="form-control ddlTaxLaw"></select>
                                        </div>

                                        <div class="col-lg-2 divActivityDate">
                                            <label for="exampleIsnputEmail2">Activity Date</label>
                                            <input type="text" class="form-control DatePickerComplete txtActivityDate" />
                                        </div>

                                        <div class="col-lg-2">
                                            <label for="exampleIsnputEmail2">Amount</label>
                                            <input type="text" class="form-control numeric txtTaxLawAmount" />
                                        </div>

                                        <div class="col-lg-2">
                                            <label for="exampleIsnputEmail2">Tax Year</label>
                                            <select class="form-control ddlTaxYear"></select>
                                        </div>


                                    </div>

                                </div>
                                <div class="panel-footer">
                                    <input type="button" class="btn btn-default " value="Cancel" onclick="ClearFieldsTaxComputation();" />
                                    <input type="button" class="btn btn-primary btnSaveTaxComputation" onclick="SaveTaxComputation()" value="Save" />
                                </div>



                            </div>
                            <div class="panel panel-info">
                                <div class="panel-heading">Tax Law Setting</div>
                                <div class="panel-body" style="max-height: 300px; overflow-y: scroll">
                                    <table class="table table-hover">
                                        <thead class="theadTaxLaw">
                                            <tr class="info">
                                                <th>Tax Law</th>
                                                <th>Amount</th>
                                                <th>Tax Year</th>
                                                <th>Law Activity Date</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody class="tbodyTaxLaw"></tbody>
                                    </table>

                                </div>
                            </div>
                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    Tax Details
                                </div>
                                <div class="panel-body">
                                    <table class="table table-hover">
                                        <thead>
                                            <tr class="info">
                                                <th>Slab</th>
                                                <th>Tax Payable</th>
                                                <th>Tax Paid</th>
                                                <th>Tax Balance</th>
                                            </tr>
                                        </thead>
                                        <tbody class="tbodyTaxDetail">
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
            </div>
        </div>
    </div>


    <%--<div id="LoanPaymentModal" class="modal hide fade" tabindex="-1" >--%>
    <div class="modal inmodal" id="LoanPaymentModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog" style="width: 60%">
            <div class="modal-content animated fadeIn">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>

                    <small class="font-bold ">Payment Settlement</small>

                </div>
                <div class="modal-body">

                    <div class="panel panel-info">
                        <div class="panel-heading">
                            Loan Payment
                        </div>
                        <div class="panel-body">

                            <div class="dvEntryLoanPayment">
                                <div class="col-lg-3">
                                    <input type="hidden" value="0" class="hfSelectedLoanMasterId" />
                                    <input type="hidden" value="0" class="hfSettlementDetailId" />

                                    <label for="exampleIsnputEmail2">Settlement Type</label>
                                    <select class="form-control ddlSettlementType"></select>
                                </div>

                                <div class="col-lg-3">
                                    <label for="exampleIsnputEmail2">Payment Type</label>
                                    <select class="form-control ddlPaymentType"></select>
                                </div>

                                <div class="col-lg-3">
                                    <label for="exampleIsnputEmail2">Settlement Amount</label>
                                    <input type="text" class="form-control txtSettlementAmount" />
                                </div>

                                <div class="col-lg-3">
                                    <label for="exampleIsnputEmail2">Cheque Date</label>
                                    <input type="text" class="form-control txtChequeDate DatePickerComplete" />
                                </div>

                                <div class="col-lg-3">
                                    <label for="exampleIsnputEmail2">Cheque No.</label>
                                    <input type="text" class="form-control txtChequeNo numeric" />
                                </div>

                                <div class="col-lg-3">
                                    <label for="exampleIsnputEmail2">Bank</label>
                                    <input type="text" class="form-control txtBankDetail" />
                                </div>



                            </div>


                        </div>
                        <div class="panel-footer">
                            <input type="button" class="btn btn-default " value="Cancel" onclick="CancelLoanPayment();" />
                            <input type="button" class="btn btn-primary btnLoanPayment" value="Save" onclick="if(confirm('Are you sure you wants to save?')){SaveLoanPayment();}" />


                        </div>

                    </div>

                    <div class="panel panel-info">
                        <div class="panel-heading">
                            Loan Payment
                        </div>
                        <div class="panel-body">

                            <table class="table table-hover">
                                <thead>
                                    <tr class="info">
                                        <th>Settlement Type</th>
                                        <th>Payment Type</th>
                                        <th>Settlement Amount</th>
                                        <th>Bank</th>
                                        <th>Cheque No.</th>
                                        <th>Cheque Date</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody class="tbodyLoanPaymentDetail">
                                </tbody>
                            </table>

                        </div>
                        <div class="panel-footer">
                        </div>

                    </div>


                </div>
            </div>
        </div>
    </div>

    <script type="text/x-jQuery-tmpl" id="LoanPayment">
        <tr class="trLoanPayment">

            <input type="hidden" value="${SettlementDetailId}" class="_hfSettlementDetailId" />
            <input type="hidden" value="${SettlementTypeId}" class="_hfSettlementTypeId" />
            <input type="hidden" value="${PaymentTypeId}" class="_hfPaymentTypeId" />
            <input type="hidden" value="${Islock}" class="_Islock" />
            <%--  <input type="hidden" value="${LoanMasterId}" class="_hfLoanMasterId" />--%>

            <td class="project-title tdSettlementType" style="font-size: 10px;">${SettlementType}</td>
            <td class="project-title tdPaymentType" style="font-size: 10px;">${PaymentType}</td>
            <td class="project-title tdSettlementAmount" style="font-size: 10px;">${SettlementAmount}</td>
            <td class="project-title tdBank" style="font-size: 10px;">${Bank}</td>
            <td class="project-title tdChequeNo" style="font-size: 10px;">${ChequeNo}</td>
            <td class="project-title tdChequeDate" style="font-size: 10px;">${ChequeDate}</td>
            <td class="project-title td_Action"><%--style='${Islock == 1 ? 'display:none;': ''}'--%>
                <%-- <input type="button" class="btn btn-primary btn-xs" value="Edit" onclick="EditSettlement(this);" />--%>
                <input type="button" class="btn btn-danger btn-xs btnDeleteSettlement" onclick="if(confirm('Are you sure you wants to delete?')){DeleteSettlement(this)}" value="Delete" />
            </td>
        </tr>
    </script>

    <script type="text/x-jQuery-tmpl" id="TaxDetailInformation">
        <tr class="trTaxDetail">
            <td class="project-title" style="font-size: 10px;">${formatDate(YearFrom)} - ${formatDate(YearTo)}</td>
            <td class="project-title" style="font-size: 10px;">
                <label class="badge badge-info">${TotalTaxPayable.toFixed(2)}</label></td>
            <td class="project-title" style="font-size: 10px;">
                <label class="badge badge-info">${(TotalTaxPayable-Balance).toFixed(2)}</label></td>
            <td class="project-title" style="font-size: 10px;">
                <label class="badge badge-info">${Balance.toFixed(2)}</label></td>
        </tr>
    </script>

    <script type="text/x-jQuery-tmpl" id="TaxManagement">
        <tr class="trTaxComputation">

            <td class="project-title" style="font-size: 10px;">

                <input type="hidden" class="hfTaxComputationId" value="${TaxComputationId}" />
                <input type="hidden" class="hfTaxLawId" value="${TaxLawId}" />
                <input type="hidden" class="hfTaxAmount" value="${Amount}" />
                <input type="hidden" class="hfTaxYearId" value="${TaxYearId}" />
                <input type="hidden" class="hfLawActivityDate" value="${LawActivityDate}" />

                ${TaxLaw}</td>
            <td class="project-title" style="font-size: 10px;">${Amount}</td>
            <td class="project-title" style="font-size: 10px;">${TaxYear}</td>
            <td class="project-title" style="font-size: 10px;">${LawActivityDate == null ? '-' : LawActivityDate.substring(0,10)}</td>
            <td>


                <input type="button" class="btn btn-primary btn-xs" value="Edit" onclick="SetEdit(this);" />
                <input type="button" class="btn btn-danger btn-xs btnDeleteTaxComputation" onclick="DeleteTaxComputation(this);" value="Delete" />

            </td>
        </tr>
    </script>

    <script type="text/x-jQuery-tmpl" id="EmployeeListing">
        <tr>
            <input type="hidden" value="${InterestStandard}" class="hdnInterestStandard" />
            <%--<input type="hidden" value="${OpeningBalance}" class="hdnPFBalance" />--%>
            <input type="hidden" value="${Balance}" class="hdnPFBalance" />
            <td class="project-title">${Company}</td>
            <td class="project-title tdEmployeeCode">${EmployeeCode}</td>
            <td class="project-title tdEmployeeName">${FirstName} ${LastName}</td>
            <td class="project-title" style="font-size: 10px;">${Department}</td>
            <td class="project-title tdDesignation" style="font-size: 10px;">${Designation}</td>
            <td class="project-title" style="font-size: 10px;">${Location}</td>
            <td class="project-title" style="font-size: 10px;">${OfficalEmail}</td>
            <td class="project-title" style="font-size: 10px;">${JoiningDate}</td>
            <td class="project-title">
                <input type="button" data-toggle="modal" onclick="setEmployeeId('${EmployeeId}', '${CompanyId}', this)" data-target="#CreateProjectModal" value="Manage" class="btn btn-success openmodal" />
            </td>
        </tr>
    </script>

    <script type="text/x-jQuery-tmpl" id="VehicleHistory">
        <tr>
            <td class="project-title Category" style="font-size: 10px;">${IsUpgraded == '1' ? 'Eligible' : 'Upgraded'}</td>
            <td class="project-title" style="font-size: 10px;">
                <table>
                    <tr>
                        <td><strong>Name</strong> : </td>
                        <td class="VehicleName"> ${VehicleName}</td>
                    </tr>
                    <tr>
                        <td><strong>Reg#</strong> : </td>
                        <td class="RegistrationNumber"> ${RegistrationNumber}</td>
                    </tr>
                    <tr>
                        <td><strong>Chasis#</strong> : </td>
                        <td class="ChasisNumber"> ${ChasisNumber}</td>
                    </tr>
                    <tr>
                        <td><strong>Engine#</strong> : </td>
                        <td class="EngineNumber"> ${EngineNumber}</td>
                    </tr>
                </table>
            </td>
            <td class="project-title" style="font-size: 10px;">
                <table>
                    <tr>
                        <td><strong>Eligible</strong> : </td>
                        <td class="PurchaseAmount"> ${PurchaseAmount}</td>
                    </tr>
                    <tr>
                        <td><strong>Updraded</strong> : </td>
                        <td class="PurchaseAmountUpgraded"> ${PurchaseAmountUpgraded}</td>
                    </tr>
                </table>
            </td>
            <td class="project-title" style="font-size: 10px;">
                <table>
                    <tr>
                        <td><strong>W.D Value</strong> : </td>
                        <td class="tdBookValue"> ${BookValue}</td>
                    </tr>
                    <tr>
                        <td><strong>Paid</strong> : </td>
                        <td class="tdRecoveredInstallment"> ${BookValue - Balance}</td>
                    </tr>
                    <tr>
                        <td><strong>Balance</strong> : </td>
                        <td class="tdBalance"> ${Balance}</td>
                    </tr>
                </table>
            </td> 
            <td class="project-title" style="font-size: 10px;">
                <table>
                    <tr>
                        <td><strong>Orignal</strong> : </td>
                        <td class="tdInstallmentAmount"> ${InstallmentAmount}</td>
                    </tr>
                    <tr>
                        <td><strong>Current</strong> : </td>
                        <td class="tdCurrentMonthInstallment"> ${CurrentMonthInstallment}</td>
                    </tr>
                    <tr>
                        <td><strong>Till Date</strong> : </td>
                        <td class="CarSettlementDate"> ${CurrentMonthDeductionTillDate != 'undefined' ||  CurrentMonthDeductionTillDate != null ? formatDate(CurrentMonthDeductionTillDate) : 'N/A'}</td>
                    </tr>
                </table>
            </td> 
            <td class="project-title" style="font-size: 10px;">
                <table>
                    <tr>
                        <td><strong>Purchase</strong> : </td>
                        <td class="PurchaseDate"> ${formatDate(PurchaseDate)}</td>
                    </tr>
                    <tr>
                        <td><strong>Allowance</strong> : </td>
                        <td class="AlowanceDate"> ${formatDate(AlowanceDate)}</td>
                    </tr>
                </table>
            </td>

            <%--{{if Balance == 0}}
            <td class="hidden tdRecoveredInstallment" style="font-size: 10px;">${BookValue}</td>
            {{/if}}
            {{if Balance >  0}}
             <td class="hidden tdRecoveredInstallment" style="font-size: 10px;">${RecoveredInstallment}</td>
            {{/if}} 
            {{if Balance == 0}}
            <td class="hidden tdRecoveredInstallment" style="font-size: 10px;">0</td>
            {{/if}}
             {{if Balance >  0}}
             <td class="hidden tdRemainingInstallment" style="font-size: 10px;">${BookValue - RecoveredInstallment}</td>
            {{/if}} --%>
            <%--            <td class="hidden tdInstallmentAmount" style="font-size: 10px;">${InstallmentAmount}</td>--%>
            <td class="hidden tdCurrentMonthInstallment" style="font-size: 10px;">${CurrentMonthInstallment}</td>
            <td class="hidden UpgradedAmount" style="font-size: 10px;">${UpgradedAmount}</td>
            <td class="hidden PurchaseDate" style="font-size: 10px;">${PurchaseDate == 'undefined' ? '' :formatDate(PurchaseDate)}</td>
            <td class="hidden AlowanceDate" style="font-size: 10px;">${AlowanceDate == 'undefined' ? '' : formatDate(AlowanceDate)}</td>
            <td class="hidden ChequeNumber" style="font-size: 10px;">${ChequeNumber}</td>
            <td class="hidden IsVehiclePayment" style="font-size: 10px;">${IsVehiclePayment}</td>
            <td class="hidden IsOwnerShipDeduction" style="font-size: 10px;">${IsOwnerShipDeduction}</td>
            <td class="hidden tdCarSettlementDate" style="font-size: 10px;">${CarSettlementDate == 'undefined' ||  CarSettlementDate == '' ? '' : formatDate(CarSettlementDate)}</td>
            <td class="hidden Comments" style="font-size: 10px;">${Comments != 'undefined' ||  Comments != null ? Comments : 'N/A'}</td>

            <%--<td class="project-title"> --%>
             {{if IsActive == 1}}
            <td class="${IsActive == 1 ? 'show' : 'hidden'}">

                <input type="button" class="${IsHold == 1 ? 'btn btn-success btn-xs' : 'btn btn-normal btn-xs'}" onclick="if(confirm('Are you sure you wants to hold?')){onVehicleHold(this)}" value="${IsHold == 1 ? 'Un Hold' : 'Hold'}" />

                <input type="button" class="btn btn-xs btn-warning btnVehicleEdit" onclick="EnableAllControls();onVehicleEdit(this);" value="Edit" />

                <%-- <input type="button" class="btn btn-xs btn-danger btnVehicleDelete" onclick="if(confirm('Are you sure you wants to delete?')){onVehicleDelete(this)}" value="Delete" />--%>
                <input type="button" class="btn btn-xs btn-danger btnVehicleDelete" onclick="Open_Modal_VehicleDeleteRecord('${VehicleMasterId}')" value="Delete" />

                <%--<input type="button" class="btn btn-xs btn-primary btnVehicleInstallment" onclick="if(confirm('Are you sure you wants to change installment?')){Open_Modal_VehicleChangeInstallment('${VehicleMasterId}')}" value="Change Installment" /> --%>
                <input type="button" class="btn btn-xs btn-primary btnVehicleInstallment" onclick="Open_Modal_VehicleChangeInstallment('${VehicleMasterId}')" value="Change Installment" />
            </td>
            {{/if}} 
             {{if IsActive == 0}}
            <td>
                <table>
                    <tr>
                        <td><strong>Deleted Record</strong></td>
                    </tr>
                    <tr> 
                        <td class="Comments"><strong>Comments: </strong>
                            <p>${Comments != 'undefined' ||  Comments != null ? Comments : 'N/A'}</p>
                        </td>
                    </tr>
                </table>
            </td>
            {{/if}} 

            <input type="hidden" value="${IsUpgraded}" class="IsUpgraded" />
            <input type="hidden" value="${CurrentMonthDeductionTillDate}" class="currentTillDateInsAdj" />
            <input type="hidden" value="${VehicleMasterId}" class="parentId" />
            <input type="hidden" value="${VehicleId}" class="VehicleId" />
            <input type="hidden" value="${BookValue}" class="BookValue" />
            <input type="hidden" value="${IsHold}" class="IsHold" />
            <input type="hidden" value="${IsOwnerShipDeduction}" class="IsOwnerShipDeduction" />
        </tr>
    </script>

    <script type="text/x-jQuery-tmpl" id="VehicleHistoryTransactional">
        <tr>
            <td class="project-title">${VehicleName}
                   <input type="hidden" value="${IsLock}" class="hfIsLockAgainstVehicle" />
            </td>
            <td class="project-title">${InstallmentAmount}</td>
            <td class="project-title" style="font-size: 10px;">${Balance}</td>
            <td class="project-title" style="font-size: 10px;">${formatDate(CreatedDate)}</td>
        </tr>
    </script>

    <script type="text/x-jQuery-tmpl" id="ArrearHistory">
        <tr>
            <td class="project-title">${FirstName} ${LastName}</td>
            <td class="project-title" style="font-size: 10px;">${ArrearAmount}</td>
            <td class="project-title" style="font-size: 10px;">${ArrearDate}</td>
            <td class="project-title" style="font-size: 10px;">${DispersedDate}</td>
            <td class="project-title" style="font-size: 10px;">
                <input type="hidden" class="IsDispersed" value="${IsDispersed}" />
                <span class="${IsDispersed ? 'badge badge-primary' : 'badge badge-danger'}">${IsDispersed ? 'Yes' : 'No'}</span>
            </td>
            <td class="project-title">
                <input type="hidden" class="ArrearId" value="${ID}" />
                <input type="button" class="btn btn-sm btn-danger" value="X" onclick="onRemoveArrear(this)" /></td>
        </tr>
    </script>

    <script type="text/x-jQuery-tmpl" id="MappedAllowances">
        <tr>
            <td class="project-title">${AllowanceName}</td>
            <td class="project-title MeasureTD">${Measure != null ? Measure : 'Formula' }</td>
            <td class="project-title">
                <input type="button" class="btn btn-sm btn-danger" onclick="onDeleteAllowance(this)" value="Delete" />
                <input type="button" class="btn btn-sm btn-warning" onclick="onEditAllowances(this)" value="Edit" />
            </td>
            <input type="hidden" class="AllowanceID" value="${EmployeeAllowanceMappingID}" />
            <input type="hidden" class="SetupAllowanceID" value="${AllowanceID}" />
        </tr>
    </script>

    <script type="text/x-jQuery-tmpl" id="ProvidentHistory">
        <tr>
            <%--  <td class="project-title">${formatDate(CreatedDate)}</td>--%>
            <td class="project-title">${EmployeeContribution.toFixed(0)}</td>
            <td class="project-title">${CompanyContribution.toFixed(0)}</td>
            <td class="project-title tdEmployeeBalance">${EmployeeBalance.toFixed(0)}</td>
            <td class="project-title tdCompanyBalance">${CompanyBalance.toFixed(0)}</td>
            <td class="project-title tdInterestIncome">${InterestIncome.toFixed(0)}</td>
            <td class="project-title"><span class="label label-primary">${TotalBalance.toFixed(0)}</span></td>
        </tr>
    </script>

    <script type="text/x-jQuery-tmpl" id="bindWithdrawHistory">

        <tr>
            <td class="project-title">${EmployeeCode}</td>
            <td class="project-title">${formatDate(WithdrawDate)}</td>
            <td class="project-title historyAmount">${WithdrawAmount}</td>
            <td class="project-title">
                <input type="hidden" class="hfFundWithdrawId" value='${FundWithdrawId}' />
                <input type="button" class="btn btn-danger btn-xs" onclick="if(confirm('Are you sure you wants to delete?')){PfWithdrawDelete('${FundWithdrawId}')}" value="Delete" style="display: none;" />

                <input type="button" class="btn btn-info btn-xs" onclick="if(confirm('Are you sure you wants to reverse?')){ReversePF('${FundWithdrawId}')}" value="Reverse PF" />
            </td>
        </tr>

    </script>

    <script type="text/x-jQuery-tmpl" id="LoanListing">
        <tr class="tr_LoanListing">

            <td class="project-title" style="font-size: 10px;">${LoanType}</td>
            <td class="project-title" style="display: none;">${LoanMasterId}</td>
            <td class="project-title" style="font-size: 10px;">${LoanAmount}</td>
            <td class="project-title" style="font-size: 10px;">${InstallmentAmount}</td>
            <td class="project-title" style="font-size: 10px;">${Balance == -1 ? LoanAmount : Balance}</td>
            <td class="project-title" style="font-size: 10px;">${formatDate(SanctionDate)}</td>
            <td class="project-title" style="font-size: 10px;">${formatDate(LoanGivenDate)}</td>
            <td class="project-title" style="font-size: 10px;">${formatDate(SettlementDate)}</td>
            <td class="project-title" style="font-size: 10px;">
                <span class="badge badge-info">${IsHold == true ? 'On Hold' : 'In Process'}</span>
            </td>

            <td class="project-title" style="text-align: center;">
                <input class="hf_Has_IsLocked_Transection" type="hidden" value='${Has_IsLocked_Transection}' />
                <input class="hf_IsHold" type="hidden" value='${IsHold}' />
                <input class="hf_Balance" type="hidden" value='${Balance == -1 ? LoanAmount : Balance}' />
                <input type="button" class="btn btn-normal btn-xs btn_Loan_Hold" onclick="if(confirm('Are you sure you wants to hold?')){HoldLoan('${LoanMasterId}', '${IsHold}');}" value="Hold" />
                <input type="button" class="btn btn-primary btn-xs btn_Loan_Edit" onclick="setLoanMasterId('${LoanMasterId}', '${LoanTypeId}', '${LoanAmount}', '${InstallmentAmount}', '${SanctionDate}','${LoanGivenDate}', '${SettlementDate}', '${Balance == -1 ? LoanAmount : Balance}', '${Reason}', '${Comments}'); GetInterestRate();" value="Edit" />
                <input type="button" class="btn btn-danger btn-xs btn_Loan_Delete" onclick="onLoanDelete('${LoanMasterId}')" value="Delete" />

                <input type="button" class="btn btn-warning btn-xs btn_Loan_viewHistory" onclick="onViewHistory('${LoanMasterId}')" value="View History" />
                {{if (Balance == -1 ? LoanAmount : Balance) > 0}}
                    <button type="button" class="btn btn-success btn-xs" data-toggle="modal" data-target="#LoanPaymentModal" onclick="LoanPaymentLoad(${LoanMasterId});">Payment</button>
                {{/if}}
                <input type="button" style="display: none;" class="btn btn-success btn-xs btn_Loan_Change_Installment" onclick="Open_Modal_LoanChangeInstallment('${LoanMasterId}', '${Balance == -1 ? LoanAmount : Balance}')" value="Change Installment" />
            </td>
        </tr>
    </script>

    <script type="text/x-jQuery-tmpl" id="SalaryChangeHistory">
        <tr>
            <td class="project-title" style="font-size: 10px;">${formatDate(CreatedDate)}</td>
            <td class="project-title" style="font-size: 10px;">${removeBlankandNulls(GrossSalary)}</td>
            <td class="project-title" style="font-size: 10px;">General Update</td>
        </tr>
    </script>

    <script type="text/x-jQuery-tmpl" id="IncrementChangeHistory">
        <tr class="trIncRecord">
            <input type="hidden" class="hdIsGranted" value="${IsGranted}" />
            <input type="hidden" class="hdnSalaryInc" value="${removeBlankandNulls(GrossSalary)}" />
            <input type="hidden" class="hdnIncrementTypeId" value="${IncrementTypeId}" />
            <input type="hidden" class="hdnEmpSalaryId" value="${EmployeeSalaryID}" />

            <td class="project-title" style="font-size: 10px;">${formatDate(CreatedDate)}</td>
            <td class="project-title" style="font-size: 10px;">${removeBlankandNulls(GrossSalary)}</td>
            <td class="project-title" style="font-size: 10px;">${IncrementType}</td>
            <td class="project-title" style="font-size: 10px;">${IsGranted == true ? 'Yes' : 'No'}</td>
            <td class="project-title tdIncInitiatedDate" style="font-size: 10px;">${formatDate(IncrementProcessStartDate)}</td>
            <%--     <td class="project-title" style="font-size: 10px;">${formatDate(IncrementAppliedDate)}</td>--%>
            <td class="project-title" style="font-size: 10px;">
                <input type="button" class="btn btn-info btn-xs" value="Edit" onclick="EditIncrement(this)" />
                <input type="button" class="btn btn-danger btn-xs" value="Delete" onclick="DeleteIncrement(this)" />
            </td>
        </tr>
    </script>



    <%-- Loan Change Installment Modal Start--%>
    <input type="button" data-toggle="modal" data-target="#LoanChangeInstallmentModal" class="ChangeInstallmentModal" style="display: none;" />
    <div class="modal fade in inmodal " id="LoanChangeInstallmentModal" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog">
            <div class="modal-content animated">
                <div class="modal-header" style="padding-bottom: 9px; padding-top: 9px; text-align: center">
                    <h5 class="modal-title">Change Loan Installment</h5>
                </div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <input type="hidden" id="hfId" runat="server" class="hf_ChangeLoanMasterId" value="0" />
                            <input type="hidden" id="hfLoanCurrentBalance" runat="server" class="hfLoanCurrentBalance" value="0" />
                            <div class="row">
                                <div class="col-md-6 col-sm-6 col-lg-6">
                                    <div class="form-group">
                                        <label>Installment Amount</label>
                                        <input type="text" class="form-control txtCurrentMonthInstallmentAmount decimals" maxlength="9" />
                                    </div>
                                </div>
                                <div class="col-md-6 col-sm-6 col-lg-6">
                                    <div class="form-group">
                                        <label>Till Date</label>
                                        <input type="text" class="form-control txtCurrentMonthInstallmentTillDate DatePickerMonth_Year" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <input type="button" class="btn btn-primary" value="Save" onclick="if(confirm('Are you sure you wants to change installment?')){Change_LoanInstallment_Amount()}" />
                            <input type="button" class="btnCancelLoanChange btn btn-default" value="Cancel" data-dismiss="modal" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <%-- Modal End--%>


    <%-- Loan Change Installment For Vehicle Modal Start--%>
    <input type="button" data-toggle="modal" data-target="#VehicleChangeInstallmentModal" class="ChangeVehicleInstallmentModal" style="display: none;" />
    <div class="modal fade in inmodal " id="VehicleChangeInstallmentModal" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog">
            <div class="modal-content animated">
                <div class="modal-header" style="padding-bottom: 9px; padding-top: 9px; text-align: center">
                    <h5 class="modal-title">Change Vehicle Installment</h5>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <input type="hidden" id="Hidden1" runat="server" class="hf_ChangeVehicleMasterId" value="0" />

                            <div class="row">
                                <div class="col-md-6 col-sm-6 col-lg-6">
                                    <div class="form-group">
                                        <label>Installment Amount</label>
                                        <input type="text" class="form-control txtVehicleCurrentMonthInstallmentAmount decimals" maxlength="9" />
                                    </div>
                                </div>
                                <div class="col-md-6 col-sm-6 col-lg-6">
                                    <div class="form-group">
                                        <label>Till Date</label>
                                        <input type="text" class="form-control txtVehicleCurrentMonthInstallmentTillDate DatePickerMonth_Year" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <input type="button" class="btn btn-primary" value="Save" onclick="if(confirm('Are you sure you wants to change installment?')){Change_VehicleInstallment_Amount()}" />
                            <input type="button" class="btnCancelVehicleChange btn btn-default" value="Cancel" data-dismiss="modal" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <%-- Modal End--%>

    <%-- Delete Record For Vehicle Modal Start--%>
    <input type="button" data-toggle="modal" data-target="#VehicleDeleteRecordModal" class="ChangeVehicleDeleteRecordModal" style="display: none;" />
    <div class="modal fade in inmodal " id="VehicleDeleteRecordModal" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog">
            <div class="modal-content animated">
                <div class="modal-header" style="padding-bottom: 9px; padding-top: 9px; text-align: center">
                    <h5 class="modal-title">Delete Vehicle</h5>
                </div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <input type="hidden" id="Hidden2" runat="server" class="hf_ChangeVehicleMasterId" value="0" />

                            <div class="row">
                                <div class="col-md-6 col-sm-12 col-lg-12">
                                    <div class="form-group">
                                        <label>Comments</label>
                                        <input type="text" class="form-control txtVehicleDeleteRecordComments" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <input type="button" class="btn btn-primary" value="Save" onclick="onVehicleDelete(this)" />
                            <input type="button" class="btnCancelVehicleDelete btn btn-default" value="Cancel" data-dismiss="modal" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <%-- Modal End--%>

    <script src="../../js/Page_JS/PfOpening.js"></script>
    <script src="../../js/Page_JS/EmployeeHCMDetail.js"></script>
    <script src="../../js/Page_JS/EmployeeAllowances.js"></script>
    <script src="../../js/Page_JS/ArrearManagement.js"></script>
    <script src="../../js/Page_JS/VehicleManagement.js"></script>
    <script src="../../js/Page_JS/LoanManagement.js"></script>
    <script src="../../js/Page_JS/TaxManagement.js"></script>
</asp:Content>

