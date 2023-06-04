<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="PayrollTransactional.aspx.cs" Inherits="Pages_HCM_PayrollTransactional" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="Generate Payroll" />
            </h2>
            <ol class="breadcrumb">
                
                <li>
                    <a href="#">HCM </a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Generate Payroll" />
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

                        <div class="form-group col-lg-4">
                            <label for="exampleInputPassword2">Company</label>
                            <select class="form-control ddlCompany">
                            </select>
                        </div>

                        <div class="form-group col-lg-2">
                            <label for="exampleInputPassword2">Select Month</label>
                            <input type="text" class="form-control txtMonth DatePickerMonthComplete" />
                            <%--onchange=" GetLockStatus(this);"--%>
                        </div>
                        <div class="form-group col-lg-4" style="margin-top: 23px">
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default pull-right" />
                            <input type="button" onclick="if(confirm('Are you sure you wants to generate ?')){HCM_Validate_PayrollMonth()}" class="btn btn-primary pull-right btnSearch m-r-sm" value="Generate" />
                        </div>
                    </div>
                    <div class="form-group col-lg-12">

                        <input type="hidden" class="hdnArrearRelease" data-toggle="modal" data-target="#ArrearReleaseModal" />
                        <div class="col-lg-5 aligncontrol">
                            <div class="checkbox checkbox-primary checkbox-inline">
                                <input class="resSalary form" id="chkResSalary" type="checkbox" value="Sal" onclick="CheckSettings(this)" />
                                <label for="chkResSalary">Salary</label>
                            </div>
                            <div style="display: none;">
                                <div class="checkbox checkbox-primary checkbox-inline">
                                    <input class="ChkLstUpgd" id="chkResInc" type="checkbox" value="Inc" onclick="CheckSettings(this)" />
                                    <label for="chkResInc">Increment</label>
                                </div>
                            </div>
                            <div style="display: none;">
                                <div class="checkbox checkbox-primary checkbox-inline">
                                    <input class="resArrear" id="chkResArrear" type="checkbox" value="Arr" onclick="CheckSettings(this)" />
                                    <label for="chkResArrear">Arrear</label>
                                </div>
                            </div>
                            <div style="display: none;">
                                <div class="checkbox checkbox-primary checkbox-inline">
                                    <input class="chkbxBonus" id="chkResBonus" type="checkbox" value="Bonus" onclick="CheckSettings(this); " />
                                    <label for="chkResBonus">Bonus</label>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <div class="panel panel-info" style="display: none;">
        <div class="panel-heading">
            Process Settings
        </div>
        <div class="panel-body">

            <div class="divInc">
                <%--  <div class="checkbox checkbox-success checkbox-inline">
                    <input type="checkbox" id="chk0"/>
                    <label for="chk0">All</label>
                </div>--%>
                <div class="divIncList">
                </div>
            </div>

            <div class="divArrears">
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
                                    <h2 class="panel-title" style="font-size: x-large; text-align: center;">Payroll Transactions Detail
                                    </h2>
                                </div>
                                <div class="alert alert-info alert-dismissable alertCount text-center">
                                    <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                    The payroll for the selected month is generated <span class="badge badge-info spanCount">0</span> times before completing the process.
                                </div>
                            </div>
                        </div>
                        <div class="project-list divPayroll" style="overflow-x: scroll">
                            <table class="table table-hover" id="tblPayrollGenerate">
                                <thead class="payrollHead">
                                </thead>
                                <tbody class="payrollBody">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="modal inmodal" id="CreateProjectModal">
        <div class="modal-dialog">
            <div class="modal-content animated fadeIn">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">Manage</h4>
                </div>
                <div class="modal-body">
                    <div class="widget style1 lazur-bg">
                        <div class="row vertical-align">
                            <div class="col-xs-2">
                                <i class="fa fa-check fa-3x"></i>
                            </div>
                            <div class="col-xs-10 text-right">
                                <h2 onclick="ManageCarInstallmentDeduction()"  class="font-bold"><a style="color: white">Manage Car Installment Deduction</a></h2>
                            </div>
                        </div>
                    </div>

                    <div class="widget style1 lazur-bg">
                        <div class="row vertical-align">
                            <div class="col-xs-2">
                                <i class="fa fa-check fa-3x"></i>
                            </div>
                            <div class="col-xs-10 text-right">
                                <h2 onclick="ManageTaxDeduction()"  class="font-bold"><a style="color: white">Manage Tax Deduction</a></h2>
                            </div>
                        </div>
                    </div>

                    <div class="widget style1 lazur-bg">
                        <div class="row vertical-align">
                            <div class="col-xs-2">
                                <i class="fa fa-check fa-3x"></i>
                            </div>
                            <div class="col-xs-10 text-right">
                               <%-- <h2 onclick="GetEmployeeSalaryAllowances()" data-toggle="modal" data-target="#ExtraAllowance" class="font-bold"><a style="color: white">Allowance/Deduction</a></h2>--%>

                                 <h2 onclick="GetEmployeeSalaryAllowances()"  class="font-bold"><a style="color: white">Allowance/Deduction</a></h2>
                            </div>
                        </div>
                    </div>

                    <div id="lblerror" class="col-lg-6 danger" visible="false"></div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal inmodal" id="TaxDeductionManagement">
        <div class="modal-dialog" style="width: 65%">
            <div class="modal-content animated fadeIn">
                <div class="modal-header">
                    <button type="button" class="close" onclick="ClosePopup('TaxDeductionManagement')"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">Tax Deduction</h4>

                </div>
                <div class="modal-body">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            Tax Deduction
                        </div>
                        <div class="panel-body slimScrollBar" id="Div2">
                            <div class="divTaxDeduction">
                                <div class="row">
                                    <div class="col-lg-4">
                                        <label for="exampleIsnputEmail2">Tax Balance</label>
                                        <input type="text" disabled="disabled" class="form-control decimals txtTaxBalance" />
                                    </div>
                                    <div class="col-lg-4">
                                        <label for="exampleIsnputEmail2">Current Month Deduction</label>
                                        <input type="text" disabled="disabled" class="form-control decimals txtTaxDeduction" />
                                    </div>
                                    <div class="col-lg-4">
                                        <label for="exampleIsnputEmail2">Current Month Deduction (New) </label>
                                        <input type="text" class="form-control decimals txtTaxDeductionNew" maxlength="10" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel-footer text-right">
                            <input type="button" onclick="SaveTaxDeduction()" class="btn btn-primary text-right" value="Add To Current Salary" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
            </div>
        </div>
    </div>

    <div class="modal inmodal" id="OverTimeManagement">
        <div class="modal-dialog" style="width: 50%">
            <div class="modal-content animated fadeIn">
                <div class="modal-header">
                    <button type="button" class="close" onclick="ClosePopup('OverTimeManagement')"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">Overtime Calculation</h4>

                </div>
                <div class="modal-body">

                    <div class="panel panel-info">
                        <div class="panel-heading">
                            Overtime
                        </div>
                        <div class="panel-body slimScrollBar" id="panelArrearInputs">

                            <div class="divOverTime">
                                <div class="row">
                                    <div class="col-lg-4">
                                        <label for="exampleIsnputEmail2">Over Time Hours</label>
                                        <input type="text" onkeyup="calculateOverTimeAmount()" class="form-control numeric txtHours" />
                                    </div>

                                    <div class="col-lg-4">
                                        <label for="exampleIsnputEmail2">Overtime Rate</label>
                                        <input type="text" disabled="disabled" class="form-control numeric txtOverTimeRate" />
                                    </div>


                                    <div class="col-lg-4">
                                        <label for="exampleIsnputEmail2">Overtime Amount</label>
                                        <input type="text" disabled="disabled" class="form-control txtAmountExpected" />
                                    </div>
                                </div>

                            </div>
                        </div>

                        <div class="panel-footer">
                            <input type="button" onclick="SaveOverTime()" class="btn btn-primary" value="Add To Current Salary" />
                        </div>

                    </div>

                </div>
            </div>
            <div class="modal-footer">
            </div>
        </div>
    </div>

    <div class="modal inmodal" id="ExtraAllowance">
        <div class="modal-dialog" style="width: 50%">
            <div class="modal-content animated fadeIn">
                <div class="modal-header">
                    <button type="button" class="close" onclick="ClosePopup('ExtraAllowance')"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">Allowance/Deduction Management</h4>

                </div>
                <div class="modal-body">

                    <div class="panel panel-info">
                        <div class="panel-heading">
                            Allowance/Deduction Management
                        </div>
                        <div class="panel-body slimScrollBar" style="max-height: 300px; overflow-y: scroll">
                            <table class="table table-hover">
                                <thead>
                                    <tr class="info">
                                        <th>Allowance/Deduction</th>
                                        <th>Amount</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody class="divExtraAllowance">
                                </tbody>
                            </table>
                        </div>

                        <div class="panel-footer">
                            <input type="button" onclick="SaveUpdatedAllowance()" class="btn btn-primary" value="Update Current Salary" />
                            <input type="button" onclick="AddNewAllowance()" class="btn btn-warning" value="Add More" />
                        </div>

                    </div>

                </div>
            </div>
       
        </div>
    </div>





    <div class="modal inmodal" id="ArrearReleaseModal">
        <div class="modal-dialog">
            <div class="modal-content animated fadeIn">
                <div class="modal-header">
                   <button type="button" class="close" onclick="ClosePopup('ArrearReleaseModal')"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">Arrear Management</h4>
                </div>
                <div class="modal-body">
                    <table class="table table-hover">
                        <thead>
                            <tr class="info">
                                <th>Allowance/Deduction</th>
                                <th>Action
                                    <div class="checkbox checkbox-success">

                                        <input id="chkArrearsList" onclick="onChangeAllCheckBoxArrears()" type="checkbox" checked="checked" />
                                        <label for="chkArrearsList"></label>
                                    </div>

                                </th>
                            </tr>
                        </thead>
                        <tbody class="divListingNonDispersedArrear">
                        </tbody>
                    </table>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-white btnCloseArrearlisting" data-dismiss="modal">Close</button>
                    <button type="button" onclick="onContinueArrearsOnly()" class="btn btn-primary ContinueSaveChangesArrear">Save changes</button>
                </div>
            </div>
        </div>
    </div>


    <div class="modal inmodal" id="ModalCarDeduction">
        <div class="modal-dialog" style="width: 50%">
            <div class="modal-content animated fadeIn">
                <div class="modal-header">
                   <button type="button" class="close" onclick="ClosePopup('ModalCarDeduction')"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">Car Deduction</h4>

                </div>
                <div class="modal-body">

                    <div class="panel panel-info">
                        <div class="panel-heading">
                            Car Deduction
                        </div>
                        <div class="panel-body slimScrollBar" id="divCarInstallmentDeduction">

                            <div class="divTaxDeduction1">
                                <div class="row">
                                    <div class="col-lg-3">
                                        <label for="exampleIsnputEmail2">Car Installment Deduction</label>
                                        <input type="text" disabled="disabled" class="form-control numeric txtCarInstallmentDeduction" />
                                    </div>

                                    <div class="col-lg-3">
                                        <label for="exampleIsnputEmail2">Car Deduction Balance</label>
                                        <input type="text" disabled="disabled" class="form-control numeric txtCarDeductionBalance" />
                                    </div>

                                    <div class="col-lg-3">
                                        <label for="exampleIsnputEmail2">Car Installment New</label>
                                        <input type="text" class="form-control numeric txtCarInstallmentDeductionNew" onkeyup="calculationCarDeduction(this);" />
                                    </div>

                                </div>

                            </div>
                        </div>

                        <div class="panel-footer">
                            <input type="button" onclick="SaveCarInstallmentDeduction()" class="btn btn-primary" value="Add To Current Salary" />
                        </div>

                    </div>

                </div>
            </div>
            <div class="modal-footer">
            </div>
        </div>
    </div>


    <script type="text/x-jQuery-tmpl" id="SalaryEmployeeAllowances">
        
        <tr class="trUpdatedAllowances">
            <td class="project-title AllownceName" >${AllowanceName}</td>
            <td class="project-title">
                <input type="text" value="${AllowanceAmount}" disabled="disabled" class="form-control txtAllowanceAmount numericOnly" />
                <input type="hidden" value="${AllowanceName}" class="hdAllowanceName" />
                <input type="hidden" value="${ID}" class="hdAllowanceID" />
                </td>
            <td class="project-title">
                <input onclick="enableFieldAllowanceAmount(this)" class="checkAllowanceAmount" type="checkbox" /></td>
        </tr>
           
    </script>

    <script type="text/x-jQuery-tmpl" id="NonDispersedArrearList">
        <div class="checkbox checkbox-success checkbox-inline">
            <input class="ArrearTypeId" id="CHKA${ArrearTypeId}" value="${ArrearTypeId}" type="checkbox" checked="checked" />
            <label for="CHKA${ArrearTypeId}">${ArrearName}</label>
        </div>
    </script>



    <script type="text/x-jQuery-tmpl" id="CheckListIncrement">
        <div class="checkbox checkbox-success checkbox-inline">
            <input class="chkIncrementList" type="checkbox" id="chk${Id}" value="${Id}" checked="checked" />
            <label for="chk${Id}">${Value}</label>
        </div>
    </script>


    <script src="../../js/Page_JS/PayrollTransactional.js"></script>
</asp:Content>

