<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="LoanDetailHistory.aspx.cs" Inherits="Pages_HCM_LoanDetailHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
   
            <div class="row">
                <div class="col-sm-12 col-md-12 col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading"><b>Loan Detail</b></div>
                        <div class="panel-body">

                            <div class="row">
                                <div class="col-lg-3">
                                    Employee Name
                                    <asp:Label ID="lblEmpName" CssClass="form-control" runat="server"></asp:Label>
                                </div>
                                <div class="col-lg-3">
                                    Loan Type
                                    <asp:Label ID="lblLoanType" CssClass="form-control" runat="server"></asp:Label>
                                </div>
                                <div class="col-lg-3">
                                    Loan Amount
                                    <asp:Label ID="lblLoanAmount" CssClass="form-control" runat="server"></asp:Label>
                                </div>
                                <div class="col-lg-3">
                                    Loan With Interest
                                    <asp:Label ID="lblLoanWithInt" CssClass="form-control" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-3">
                                    Installment Amount
                                    <asp:Label ID="lblInstallmentAmt" CssClass="form-control" runat="server"></asp:Label>
                                </div>
                                <div class="col-lg-3">
                                    Loan Balance
                                    <asp:Label ID="lblLoanBlnc" CssClass="form-control" runat="server"></asp:Label>
                                </div>
                                <div class="col-lg-3">
                                    Loan Apply Date
                                    <asp:Label ID="lblLoanApplyDate" CssClass="form-control" runat="server"></asp:Label>
                                </div>
                                <div class="col-lg-3">
                                    Sanction Date
                                    <asp:Label ID="lblSanctionDate" CssClass="form-control" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-3">
                                    Settlement Date
                                    <asp:Label ID="lblSettlementDate" CssClass="form-control" runat="server"></asp:Label>
                                </div>

                                <div class="col-lg-3">
                                    Curr Ins Till Date
                                    <asp:Label ID="lblCurrentMonthInsTillDate" CssClass="form-control" runat="server"></asp:Label>
                                </div>
                                <div class="col-lg-3">
                                    Current Month Installment
                                    <asp:Label ID="lblCurrentMonthInst" CssClass="form-control" runat="server"></asp:Label>
                                </div>
                                <div class="col-lg-3">
                                    Is Hold
                                    <asp:Label ID="lblIsHold" CssClass="form-control" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">

                                <div class="col-lg-3">
                                    Is Settled
                                    <asp:Label ID="lblIsSettled" CssClass="form-control" runat="server"></asp:Label>
                                </div>
                                <div class="col-lg-3">
                                    Interest Rate
                                    <asp:Label ID="lblInterestRate" CssClass="form-control" runat="server"></asp:Label>
                                </div>
                                <div class="col-lg-3">
                                    Interest Amount
                                    <asp:Label ID="lblInterestAmount" CssClass="form-control" runat="server"></asp:Label>
                                </div>

                                   <div class="col-lg-3">
                                    Loan Given Date
                                    <asp:Label ID="lblLoanGivenDate" CssClass="form-control" runat="server"></asp:Label>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    Reason
                                    <asp:Label ID="lblReason" CssClass="form-control" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    Comments
                                    <asp:Label ID="lblComments" CssClass="form-control" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


            </div>

            <div class="row">
                <div class="col-sm-12 col-md-12 col-lg-12">
                    <div class="panel panel-success">
                        <div class="panel-heading"><b>Loan Installment History</b></div>
                        <div class="panel-body">
                            <asp:GridView ID="grdLoanHistory" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive table-hover table-bordered">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr No">
                                        <ItemTemplate>
                                            <%--<asp:HiddenField ID="hfLoanDetailId" runat="server" Value="<%# Eval("LoadDetailId").ToString() %>" />
                                            <asp:HiddenField ID="hfLoanMasterId" runat="server" Value="<%# Eval("LoanMasterId").ToString() %>" />
                                            <asp:HiddenField ID="hfPayRollLogid" runat="server" Value="<%# Eval("PayrollLogId").ToString() %>" />
                                            <asp:HiddenField ID="hfInterestId" runat="server" Value="<%# Eval("InterestRateId").ToString() %>" />--%>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="PayrollDate" HeaderText="Pay Roll Date" />
                                    <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                                    <asp:BoundField DataField="InstallmentAmount" HeaderText="Inst Amount" />
                                   <%-- <asp:BoundField DataField="InterestAmount" HeaderText="Interest Amount" />
                                    <asp:BoundField DataField="TotalAllowances" HeaderText="Total Allow" />
                                    <asp:BoundField DataField="TotalDeduction" HeaderText="Total Deduction" />
                                    <asp:BoundField DataField="TotalMaster" HeaderText="Total Master" />--%>
                                    <asp:BoundField DataField="Balance" HeaderText="Balance" />
                             <%--       <asp:BoundField DataField="ArrearRelease" HeaderText="Arrear Release" />
                                    <asp:BoundField DataField="BonusRelease" HeaderText="Bonus Release" />
                                    <asp:BoundField DataField="YearFrom" HeaderText="From Year" />
                                    <asp:BoundField DataField="YearTo" HeaderText="To Year" />--%>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>


            </div>


            <div class="row">
                <div class="col-sm-12 col-md-12 col-lg-12">
                    <div class="panel panel-info">
                        <div class="panel-heading"><b>Loan Settlement History</b></div>
                        <div class="panel-body">
                            <asp:GridView ID="grdLoanSettlement" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive table-hover table-bordered">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr No">
                                        <ItemTemplate>
                                          <%--  <asp:HiddenField ID="hfSettlementDetailId" runat="server" Value="<%# Eval("SettlementDetailId").ToString() %>" />
                                            <asp:HiddenField ID="hfSettlement_typeId" runat="server" Value="<%# Eval("Settlement_typeId").ToString() %>" />
                                            <asp:HiddenField ID="hfPay_TypeId" runat="server" Value="<%# Eval("Pay_TypeId").ToString() %>" />
                                            <asp:HiddenField ID="hfEmployeeId" runat="server" Value="<%# Eval("EmployeeId").ToString() %>" />--%>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="SettlementName" HeaderText="Settlement Name" />
                                    <asp:BoundField DataField="SettlementAmount" HeaderText="Settlement Amount" />
                                    <asp:BoundField DataField="SattlementDate" HeaderText="Settlement Date" />
                                    <asp:BoundField DataField="Pay_method" HeaderText="Pay Method" />
                                    <asp:BoundField DataField="cheque_Date" HeaderText="Cheque Date" />
                                    <asp:BoundField DataField="cheque_No" HeaderText="Cheque No" />
                                    <asp:BoundField DataField="Bank_Detail" HeaderText="Bank Detail" />




                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>


            </div>

</asp:Content>

