<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_SalarySlips.aspx.cs" Inherits="Pages_HCM_Report_SalarySlips" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <uc:EmployeeSearchFilters runat="server" ID="EmployeeSearchFilters" />

    <div class="panel panel-info">
        <div class="panel-heading">
            Report Functions
        </div>
        <div class="panel-body">
            <div class="col-lg-2 divMonthPayroll">
                <label>Select Month</label>
                <input type="text" class="form-control txtMonthOfPayroll DatePickerMonthComplete" />
            </div>
            <div class="col-lg-12">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelOutWithouHiddenFields('.SalarySlips')" />
                <input type="button" class="btn btn-primary pull-right" value="Print" onclick="sendPrint('.SalarySlips')" />
            </div>
        </div>
    </div>

    <div class="panel panel-info">
        <div class="panel-heading">
            Salary Slips
        </div>
        <div class="panel-body SalarySlips">
        </div>
    </div>

    <script type="text/x-jQuery-tmpl" id="SalarySlips">
        <div class="col-lg-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    Salary Slip
                </div>
                <div class="panel-body" style="background-color: white;">
                    <table class="table tableSalarySlip table-responsive table-hover">
                        <thead>

                            <tr class="success">
                                <th>Report Name:</th>
                                <th colspan="5">Salary Slip</th>
                            </tr>

                            <tr class="success">
                                <th>Company Name</th>
                                <th colspan="5">${CompanyName}</th>
                            </tr>

                            <tr class="success">
                                <th>For The Month</th>
                                <th colspan="5">${formatDate($('.txtMonthOfPayroll').val())}</th>
                            </tr>

                            <tr class="warning">
                                <th>Employee Code:</th>
                                <th>${EmployeeCode}</th>
                                <th>Employee Name:</th>
                                <th>${FirstName} ${LastName}</th>
                                <th>Designation:</th>
                                <th>${DesignationName}</th>
                            </tr>

                            <tr class="info">
                                <th colspan="2">Pay & Allowances</th>
                                <th colspan="2">Deductions</th>
                                <th colspan="2">Other Details</th>
                            </tr>
                        </thead>
                        <tbody>
                            {{html SlipData}}
                        </tbody>
                        <tfoot>
                            <tr class="info">
                                <th>Total Gross: </th>
                                <th class="tdTotalAllowances"></th>
                                <th>Total Deductions:</th>
                                <th class="tdTotalDeduction"></th>
                                <th>Net Salary:</th>
                                <th class="tdNetSalary"></th>
                            </tr>
                            <tr class="warning">
                                <th>Bank Name:</th>
                                <th>${BankName}</th>
                                <th>Branch Name:</th>
                                <th>${BankDescription}</th>
                                <th>Account Number:</th>
                                <th>${AccountNumber}</th>
                            </tr>
                            <tr>
                                <th colspan="6"></th>
                            </tr>
                            <tr>
                                <th colspan="6"></th>
                            </tr>
                        </tfoot>
                    </table>
                </div>
            </div>
        </div>
    </script>
    <script src="../../js/Page_JS/Report_SalarySlips.js"></script>
</asp:Content>

