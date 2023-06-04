<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PaySlip.ascx.cs" Inherits="Controls_HCM_PaySlip" %>

<div class="panel panel-info">

    <div id="Export">
        <div class="panel-heading">
            Pay Slip
        </div>

        <div class="panel-body tablePrintData">

            <table class="table table-hover dataTables" id="dataTables">
                <thead>
                    <tr class="info">
                        <th class="clsCompanyName border" style="text-align: center; font-size: 25px;" colspan="4"></th>
                    </tr>
                </thead>
                <tbody>
                    <tr class="info">
                        <td>For The Month
                        </td>

                        <td class="clsMonth border"></td>

                        <td>Days Worked:
                        </td>

                        <td class="clsDaysWork border">
                        </td>

                    </tr>

                    <tr class="info">
                        <td>Employee Code:
                        </td>

                        <td class="clsEmployeeCode border"></td>

                        <td>Employee Name:
                        </td>

                        <td class="clsEmployeeName border"></td>

                    </tr>

                    <tr class="info">
                        <td>Designation:
                        </td>

                        <td class="clsDesignation border"></td>

                        <td>Department:
                        </td>

                        <td class="clsDepartment border"></td>

                    </tr>


                </tbody>
            </table>

            <div class="warning " style="text-align: center">
                <div class="col-lg-4 divAllowance ">
                    <table class="table table-hover tableAllowanceListing ">
                        <thead class="theadAllowanceListing">

                            <tr class="info">
                                <th class="allignLeft">Pay & Allowances</th>
                                <th class="allignRight">Amount</th>
                            </tr>
                        </thead>
                        <tbody class="tbodyAllowanceListing">
                        </tbody>
                        <tfoot>
                            <tr class="info">
                                <th>Total Gross</th>
                                <th class="tdAllowanceAmount allignRight">0</th>
                            </tr>
                            <tr class="info">
                                <th class="allignLeft">Total Net Salary</th>
                                <th class="allignRight clsNetSalary">0</th>
                            </tr>
                        </tfoot>
                    </table>
                </div>

                <div class="col-lg-4 divDeduction ">
                    <table class="table table-hover tableDeductionListing">
                        <thead class="theadDeductionListing">

                            <tr class="info">
                                <th class="allignLeft">Deductions</th>
                                <th class="allignRight">Amount</th>
                            </tr>
                        </thead>
                        <tbody class="tbodyDeductionListing">
                        </tbody>
                        <tfoot>
                            <tr class="info">
                                <th>Total Deduction</th>

                                <th class="tdDeductionAmount allignRight">0</th>



                            </tr>
                        </tfoot>
                    </table>

                </div>

                <div class="col-lg-4 divIncomeTax ">
                    <table class="table table-hover  ">
                        <thead>
                            <tr class="info">
                                <th class="allignRight">Income Tax Payable</th>
                                <th class="allignRight">Income Tax Paid</th>
                                <th class="allignRight">Current Month</th>
                            </tr>
                        </thead>
                        <tbody class="tbodyTaxListing">
                        </tbody>
                    </table>
                </div>

                <div class="col-lg-4 divPF ">
                    <table class="table table-hover  ">
                        <thead>
                            <tr class="info">
                                <th class="allignCenter" colspan="4">Provident Fund</th>
                            </tr>
                            <tr class="info">
                                <th class="allignRight"></th>
                                <th class="allignRight">Employee</th>
                                <th class="allignRight">Employer</th>
                                <th class="allignRight">Total</th>
                            </tr>
                        </thead>
                        <tbody class="tbodyPfListing">
                        </tbody>
                    </table>
                </div>

                <div class="col-lg-4 divVehicleDeduction">
                    <table class="table table-hover  ">
                        <thead>
                            <tr class="info">
                                <th class="allignCenter" colspan="4">Other Detail</th>
                            </tr>
                            <tr class="info">
                                <%--   <th class="allignRight"></th>--%>
                                <th class="allignRight">Current Installment</th>
                                <th class="allignRight">Installment To Date</th>
                                <th class="allignRight">Installment Paid To Date</th>
                            </tr>
                        </thead>
                        <tbody class="tbodyVehicleDeductionListing">
                        </tbody>
                    </table>
                </div>

                <div class="col-lg-4 divLoan ">
                    <table class="table table-hover  ">
                        <thead>
                            <tr class="info">
                                <th class="allignCenter" colspan="5">Loan</th>
                            </tr>
                            <tr class="info">
                                <th class="allignLeft"></th>
                                <th class="allignRight">Total Loan </th>
                                <th class="allignRight">Loan Paid</th>
                                <th class="allignRight">Current Month</th>
                                <th class="allignRight">Loan Payable</th>
                            </tr>
                        </thead>
                        <tbody class="tbodyLoanListing">
                        </tbody>
                    </table>
                </div>

            </div>

            <table class="table table-hover dataTables" id="Table1">
                <thead>
                    <tr class="info">
                        <td>Bank
                        </td>

                        <td class="clsBank border"></td>

                        <td>Branch:
                        </td>

                        <td class="clsBranch border"></td>


                        <td>Account Number:
                        </td>

                        <td class="clsAccountNumber border"></td>

                    </tr>
                </thead>
            </table>

        </div>
    </div>
</div>

<script type="text/x-jQuery-tmpl" id="AllowanceListing">
    <tr class=${AllowanceAmount} == 0 ? "trList rowHeight hide" :"trList rowHeight">

        <td class="project-title allignLeft">${AllowanceName}</td>
        <td class="project-title allignRight clsAllowanceAmount">${AllowanceAmount}</td>

    </tr>
</script>

<script type="text/x-jQuery-tmpl" id="DeductionListing">
    <tr class="trList rowHeight">

        <td class="project-title allignLeft">${AllowanceName}</td>
        <td class="project-title allignRight clsDeductionAmount">${AllowanceAmount}</td>

    </tr>
</script>

<script type="text/x-jQuery-tmpl" id="TaxListing">
    <tr class="trList rowHeight">

        <td class="project-title allignRight">${TotalPayable}</td>
        <td class="project-title allignRight">${TaxPaidAmount}</td>
        <td class="project-title allignRight">${CurrentMonth}</td>

    </tr>
</script>

<script type="text/x-jQuery-tmpl" id="LoanListing">
    <tr class="trList rowHeight">

        <td class="project-title allignLeft">${LoanType}</td>
        <td class="project-title allignRight">${TotalLoan}</td>
        <%--        <td class="project-title allignRight">${parseFloat(LoanPaid) - parseFloat(CurrentMonth)}</td>--%>
        <td class="project-title allignRight">${parseFloat(LoanPaid) }</td>
        <td class="project-title allignRight">${CurrentMonth}</td>
        <td class="project-title allignRight">${BalancePayable}</td>

    </tr>
</script>

<script type="text/x-jQuery-tmpl" id="PfListing">

    <%-- <td class="project-title allignLeft">${LoanType}</td>
        <td class="project-title allignRight">${TotalLoan}</td>
        <td class="project-title allignRight"> ${parseFloat(LoanPaid) - parseFloat(CurrentMonth)}</td>
        <td class="project-title allignRight">${CurrentMonth}</td>
        <td class="project-title allignRight">${BalancePayable}</td>--%>

    <tr class="trList rowHeight">
        <td class="project-title allignRight">Opening Balance</td>
        <td class="project-title allignLeft">${EmployeeClosing}</td>
        <td class="project-title allignLeft">${CompanyClosing}</td>
        <td class="project-title allignLeft">${TotalClosing}</td>

    </tr>

    <tr class="trList rowHeight">
        <td class="project-title allignRight">Contribution</td>
        <td class="project-title allignLeft">${EmployeeContribution}</td>
        <td class="project-title allignLeft">${CompanyContribution}</td>
        <td class="project-title allignLeft">${CurrentContribution}</td>

    </tr>

    <tr class="trList rowHeight">
        <td class="project-title allignRight">PF Balance</td>
        <td class="project-title allignLeft" colspan="3">${TotalBalance}</td>
    </tr>

    <%--<tr class="trList rowHeight">
        <td class="project-title allignRight">PF Net Balance</td>
        <td class="project-title allignLeft" colspan="3">0</td>
    </tr>--%>
</script>

<script type="text/x-jQuery-tmpl" id="VehicleListing">
    <tr class="trList rowHeight">

        <td class="project-title allignLeft">${CurrentMonthInstallment}</td>
        <td class="project-title allignRight">${CarInstallmentPaidToDate}</td>
        <td class="project-title allignRight">${CarInstallmentBalance}</td>

    </tr>
</script>

<script src="../../js/Control_JS/PaySlip.js"></script>

<style>
    .border {
        /*border: groove;*/
        font-weight: bold;
    }

    .allignLeft {
        text-align: left;
    }

    .allignRight {
        text-align: right;
    }

    .allignCenter {
        text-align: center;
    }

    .rowHeight {
        height: 35px;
    }

    .hide {
        display: none;
    }
</style>
