<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="FinalSettlement.aspx.cs" Inherits="Pages_HCM_FinalSettlement" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
        <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="Final Sattlement" />
            </h2>
            <ol class="breadcrumb">
                
                <li>
                    <a href="#">HCM </a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Final Settlement" />
                    </strong>
                </li>
            </ol>
        </div>
    </div>
    <uc:EmployeeSearchFilters runat="server" ID="EmployeeSearchFilters" EmployeeCode="1"/>


<%--    <div class="form-group">
        <div class="panel panel-info mainVehicleInformation">
            <div class="panel-heading">
                Manage
            </div>
            <div class="panel-body" id="panelVehicleInformation">

                <div class="col-lg-3">
                    <div class="checkbox checkbox-primary">
                        <input runat="server" type="checkbox" class="chkIsSettled" onchange="" />
                        <label for="chkIsSettled">Is Settle</label>
                    </div>

                </div>

            </div>
        </div>
    </div>--%>


    <div class="row">
        <div class="col-lg-12">
            <div class="wrapper wrapper-content animated fadeInUp">
                <div class="ibox">

                    <div class="ibox-content">
                        <div class="row m-b-sm m-t-sm" style="margin: 0px;">
                            <div class="col-md-12 panel-default">
                                <div class="panel-heading" style="padding-top: 0px; padding-bottom: 0px;">
                                    <h2 class="panel-title" style="font-size: x-large; text-align: center;">Employee Final Settlement Detail
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
                                        <%--<th>Status</th>--%>
                                        <th>Joining Date</th>
                                        <th>Resign Date</th>
                                        <th>Final Settlement</th>
                                       <%-- <th>PF Balance</th>
                                        <th>Loan Balance</th>
                                        <th>Total Arrear Amount</th>--%>
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

    <div class="modal inmodal" id="ModalResign" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog" style="width: 35%">
            <div class="modal-content animated fadeIn">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title panelHeading">

                        <label id="lblResignPop"></label>

                    </h4>
                    <small class="font-bold panelSubHeading"></small>

                </div>
                <div class="modal-body">

                    <div class="form-group">

                        <div class="panel panel-info mainVehicleInformation">
                            <div class="panel-heading">
                                Resignation Info
                            </div>

                            <div class="panel-body" id="Div11">
                                <div class="form-group col-lg-12">

                                    <div class="col-lg-4">
                                        <label for="exampleIsnputEmail2">Stop Salary Date</label>
                                        <input type="text" class="form-control txtStopSalaryDatePop DatePickerComplete" />
                                    </div>

                                    <div class="col-lg-4">
                                        <label for="exampleIsnputEmail2">Resign Date</label>
                                        <input type="text" class="form-control txtResignedDatePop DatePickerComplete" />
                                    </div>

                                    <div class="col-lg-4">
                                        <label for="exampleIsnputEmail2">Last Working Date</label>
                                        <input type="text" class="form-control txtLastWorkingDatePop DatePickerComplete" />
                                    </div>

                                </div>


                            </div>

                            <div class="panel-footer">
                                <%--<input type="button" class="btn btn-danger btnReset" value="Reset" onclick="ClearFields();"/>--%>
                                <input type="button" onclick="if(confirm('Are you sure you wants to save?')){SaveDates();}" class="btn btn-primary btnSaveResign" value="Save" />
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
                    <small class="font-bold panelSubHeading"></small>

                </div>
                <div class="modal-body">

                    <div class="form-group">

                        <div class="panel panel-info mainVehicleInformation">
                            <div class="panel-heading">
                                Basic Information
                            </div>

                            <div class="dvEntry">
                                <div class="panel-body" id="Div3">

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Stop Salary Date</label>
                                        <input type="text" class="form-control txtStopSalaryDate DatePicker" />
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Resigned Date</label>
                                        <input type="text" class="form-control txtResignedDate DatePicker" />
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Last Day</label>
                                        <input type="text" class="form-control txtLastWorkingDate DatePicker" />
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Notice Period</label>
                                        <select class="form-control ddlNoticePeriodType"></select>
                                    </div>

                                    <div class="col-lg-2">
                                        <div class="checkbox checkbox-primary">
                                            <input id="Checkbox4" runat="server" type="checkbox" class="form-control chkIsTakeHome" onchange="CheckIsTakeHome();" value="Car Take Home" />
                                            <label for="chkResSalary">Car Take Home</label>
                                        </div>
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Car Market Value</label>
                                        <input type="text" class="form-control numeric txtCarMarketValue" value="0" onchange="CarTotalPayable();" disabled="disabled" />
                                    </div>

                                    <div class="col-lg-2">
                                        <input type="button" class="btn btn-primary btnGetData" value="Get Data" onclick="GetData();" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="panel panel-info mainVehicleInformation">
                            <div class="panel-heading">
                                Payable By Company
                            </div>

                            <div class="panel-body" id="Div4">

                                <div class="form-group col-lg-12">
                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">PF Amount</label>
                                        <input type="text" class="form-control txtPfAmount" disabled="disabled" value="0" />
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Gratuity</label>
                                        <input type="text" class="form-control txtGratuity" disabled="disabled" value="0" />
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Total Arrear Amount</label>
                                        <input type="text" class="form-control txtArrearAmount" disabled="disabled" value="0" />
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Total Leaves Remaining</label>
                                        <input type="hidden" class="form-control hfBasicSalary" value="0" disabled="disabled" />
                                        <input type="hidden" class="form-control hfSettlementId" value="0" disabled="disabled" />
                                        <input type="text" class="form-control numeric txtTotalLeavesRemaining" value="0" onkeyup="CalculateLeaveEncashment(this);" />
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Leave Encashment</label>
                                        <input type="text" class="form-control txtTotalLeaveEncashment" value="0" disabled="disabled" />
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Group Insurance Type</label>
                                        <select class="form-control ddlGroupInsType"></select>
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Group Insurance Amount</label>
                                        <input type="text" class="form-control numeric txtGroupInsAmount" value="0" onchange="CalculateTextBoxOnChange();" />
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Deducted Amount</label>
                                        <input type="text" class="form-control numeric txtDeductedAmount" value="0" onchange="CalculateTextBoxOnChange();" />
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Total Amount Paid</label>
                                        <input type="text" class="form-control txtTotalAmountPaid" disabled="disabled" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="panel panel-info mainVehicleInformation">
                            <div class="panel-heading">
                                Payable By Employee
                            </div>

                            <div class="panel-body" id="Div6">

                                <div class="form-group col-lg-12">
                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Loan Balance Amount</label>
                                        <input type="text" class="form-control txtLoanBalanceAmount" disabled="disabled" value="0" />
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Car Total Paid</label>
                                        <input type="text" class="form-control txtCarTotalPaid" value="0" disabled="disabled" />
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Car Total Payable</label>
                                        <input type="text" class="form-control txtCarTotalPayable" value="0" disabled="disabled" />
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Other Payable</label>
                                        <input type="text" class="form-control numeric txtOtherPayable" value="0" onchange="CalculateTextBoxOnChange();" />
                                    </div>

                                    <div class="col-lg-4">
                                        <label for="exampleIsnputEmail2">Other Description</label>
                                        <input type="text" class="form-control txtOtherDesc" value="" />
                                    </div>
                                </div>

                                <div class="form-group col-lg-12">
                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Bonus Recovery</label>
                                        <input type="text" class="form-control txtBonusRecovery" value="0" disabled="disabled" />
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Employee Payable Amount </label>
                                        <input type="text" class="form-control txtEmployeePayableAmount" value="0" disabled="disabled" />
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Employee Payable Cheque Number</label>
                                        <input type="text" class="form-control txtEmployeeChequeNumber" />
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Employee Payable Cheque Date</label>
                                        <input type="text" class="form-control txtEmployeeChequeDate DatePicker" />
                                    </div>
                                </div>
                            </div>

                        </div>

                        <div class="panel panel-info mainVehicleInformation">
                            <div class="panel-heading">
                                Final Settlement
                            </div>

                            <div class="panel-body" id="Div5">
                                <div class="form-group col-lg-12">
                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">PF Cheque Number</label>
                                        <input type="text" class="form-control txtPfChequeNumber" value="" />
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">PF Cheque Date</label>
                                        <input type="text" class="form-control txtPfChequeDate DatePicker" />
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Settlement Cheque Number</label>
                                        <input type="text" class="form-control txtSettlementChequeNumber" />
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Settlement Cheque Date</label>
                                        <input type="text" class="form-control txtSettlementChequeDate DatePicker" />
                                    </div>

                                    <div class="col-lg-2">
                                        <div class="checkbox checkbox-primary">
                                            <input id="chkIsSettledPopup" runat="server" type="checkbox" class="chkIsSettledPopup" value="Is Settled" />
                                            <label for="chkResSalary">Setteled</label>
                                        </div>
                                    </div>

                                    <div class="col-lg-2">
                                        <label for="exampleIsnputEmail2">Net Amount Paid </label>
                                        <input type="text" class="form-control txtNetAmountPaid" disabled="disabled" />
                                    </div>

                                </div>


                            </div>

                            <div class="panel-footer">
                                <input type="button" class="btn btn-danger btnReset" value="Reset" onclick="ClearFields();" />
                                <input type="button" onclick="Save()" class="btn btn-primary btnSave" value="Save" />
                            </div>
                        </div>

                    </div>

                </div>

            </div>
        </div>
    </div>

       
    <script type="text/x-jQuery-tmpl" id="EmployeeListing">
        <tr>
            <td class="project-title">${Company}</td>
            <td class="project-title">${EmployeeCode}</td>
            <td class="project-title tdEmployeeName">${FirstName} ${LastName}</td>
            <td class="project-title" style="font-size: 10px;">${Department}</td>
            <td class="project-title tdDesignation" style="font-size: 10px;">${Designation}</td>
            <%--<td class="project-title">${IsSettled == 2 ? 'Settled' : 'Un Settled'}</td>--%>
            <td class="project-title">${formatDate(JoiningDate) }</td>
            <td class="project-title">${ResignedDate != undefined || ResignedDate != null ? formatDate(ResignedDate) : '' }</td>
            <td class="project-title">  
                
                <input type="checkbox" ${chkdisable(formatDate(ResignedDate),IsFinalSettlement)} ${chkdisable(formatDate(ResignedDate),IsFinalSettlement)}  class="finalset" onclick="if(confirm('Are you sure?')){changefinalsettlement('${EmployeeCode}','${CompanyId}')}"> 
            </td>
           <%-- <td class="project-title">${OpeningBalance}</td>
            <td class="project-title">${LoanBalance}</td>
            <td class="project-title">${TotalArrearAmount}</td>--%>
            <td class="project-title">

                <%--<input type="button" data-toggle="modal" onclick="SetPopupHeading('${FirstName} ${LastName}', '${EmployeeId}');" data-target="#ModalResign" value="Resign" class="btn btn-danger openmodal btnManage" style="${(IsSettled == '1' || IsSettled == '2') ? 'display:none;': ''}" />

                <input type="button" data-toggle="modal" onclick="GetFinalSettlement('${EmployeeId}', '${IsSettled}', '${CompanyId}');" data-target="#CreateProjectModal"
                    value="${IsSettled == '2' ? 'View' : 'Manage'}" class="${IsSettled == '2' ? 'btn btn-default openmodal btnManage' : 'btn btn-success openmodal btnManage'}" 
                    style="${IsSettled == '0' ? 'display:none;' : ''}" />

                  <input type="button" onclick="if(confirm('Are you sure you wants to revoke resign?')){MarkRevokeFinalSettlement('${EmployeeId}');}" 
                    value="Revoke Resign" class="btn btn-danger" 
                    style="${IsSettled == '0' ? 'display:none;' : ''}" />--%>

                <input type="button" data-toggle="modal" onclick="SetPopupHeading('${FirstName} ${LastName}', '${EmployeeId}');" data-target="#ModalResign" value="Resign" class="btn btn-danger openmodal btnManage" style="${(ResignedDate != undefined || ResignedDate != null ) ? 'display:none;': 'display:block;'}" />

                <input type="button" data-toggle="modal" onclick="GetFinalSettlement('${EmployeeId}', '${IsSettled}', '${CompanyId}');" data-target="#CreateProjectModal"
                    value="${(ResignedDate != undefined || ResignedDate != null ) ? 'Manage' : 'View'}" class="${(ResignedDate != undefined || ResignedDate != null ) ? 'btn btn-success openmodal btnManage' : 'btn btn-default openmodal btnManage'}" 
                    style="${(ResignedDate != undefined || ResignedDate != null ) ? 'display:block;' : 'display:none;'}" />

                  <input type="button" onclick="if(confirm('Are you sure you wants to revoke resign?')){MarkRevokeFinalSettlement('${EmployeeId}');}" 
                    value="Revoke Resign" class="btn btn-danger" 
                    style="${(ResignedDate != undefined || ResignedDate != null ) ? 'display:block;' : 'display:none;'}" />

            </td>
        </tr>
    </script>


     <script src="../../js/Page_JS/FinalSettlement.js"></script>
    <script src="../../js/Page_JS/EmployeeSettlement.js"></script>
    <script type="text/javascript">
        function chkdisable(ResignedDate,IsFinalSettlement) { 

            if (IsFinalSettlement == true) {
                return 'checked="checked"';
            }
            else {
                if (ResignedDate == null)
                    return 'disabled="disabled"';
                else return '';
            }
        }
    </script>

</asp:Content>

