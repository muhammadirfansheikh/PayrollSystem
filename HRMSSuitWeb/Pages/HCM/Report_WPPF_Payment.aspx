<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_WPPF_Payment.aspx.cs" Inherits="Pages_HCM_Report_WPPF_Payment" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <uc:EmployeeSearchFilters runat="server" ID="EmployeeSearchFilters" EmployeeCode="1" />

    <div class="panel panel-info">
        <div class="panel-heading">
            Report Functions
        </div>
        <div class="panel-body">

            <div class="row">
                   <div class="col-lg-2 divFromDate">
                <label>From Date</label>
                <input type="text" class="form-control txtFromDate DatePicker" />
            </div>


            <div class="col-lg-2 divToDate">
                <label>To Date</label>
                <input type="text" class="form-control txtToDate DatePicker" />
            </div>

           
            <div class="col-lg-2 ">
                <label>Unit Rate</label>
                <input type="number" min="0" id="txtUnitRate" class="txtUnitRate form-control" />
            </div>
            <div class="col-lg-2 ">
                <label>Interest Rate</label>
                <input type="number" min="0" id="txtInterestRate" class="txtInterestRate form-control" />
            </div>
            <div class="col-lg-2 ">
                <label>Minimum Wage</label>
                <input type="number" min="0" id="txtMinimumWage" class="txtMinimumWage form-control" />
            </div>
             
            <div class="col-lg-2 ">
                 <label>Resign Type</label>
                <select class="form-control  ddlIsResigned">
                    <option value="1">Resigned</option>
                    <option value="0">Not Resigned</option>
                </select>
            </div>
            </div>
         
            <div class="row">
                    <div class="col-lg-2 ">
                <label>Select </label>
                <select class="form-control  ddlGroupBy"></select>
            </div>
                 <div class="col-lg-10 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tablewppfpaymentListing','WPPF_Payment')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tablewppfpaymentListing')" />
            </div>
            </div>

           
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            WPPF Payment Report
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tablewppfpaymentListing" id="tablewppfpaymentListing">
                <thead class="theadwppfpaymentlisting">
                    <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    <tr class="info">
                        <th>SR. NO.</th>
                        <th>Employee Code</th>
                        <th>Name</th>
                        <th>Date Of Appointment</th>
                        <th>Group Name</th>
                        <th>Unit Rate</th>

                        <th>Interest Rate</th>
                        <th>Total Pay</th>

                        <th>Signature</th>
                      


                    </tr>
                </thead>
                <tbody class="tbodywppfpaymentListing">
                </tbody>
                <tfoot>
                    <tr class="info">
                        <td colspan="5" style="font-weight: bold">Total</td>
                        
                        <th class="tdUnitRate" style="text-align: right"></th>
                        <th class="tdInterestRate" style="text-align: right"></th>
                        <th class="tdTotalPay" style="text-align: right"></th>
                        <th class="tdSignature" ></th>
                       

                    </tr>
                </tfoot>
            </table>
        </div>
        <br />
    </div>
    <script type="text/x-jQuery-tmpl" id="budgetwppfpaymentListing">
        <tr class="trList" style="height:60px">
            <td class="project-title" style="height:60px"></td>

            <td class="project-title clsEmployeeCode ABC" style="height:60px">${EmployeeCode}

                <input class="clsSapCostCenter" type="hidden" value="${clsSapCostCenter}" />
                <input class="clsCostCenter" type="hidden" value="${clsCostCenter}" />
                <input class="clsLocation" type="hidden" value="${clsLocation}" />
                <input class="clsDepartment" type="hidden" value="${clsDepartment}" />
            </td>
            <td class="project-title clsName" style="height:60px">${Name}</td>
            <td class="project-title clsDateofAppointment" style="height:60px">${DateofAppointment}</td>
            <td class="project-title clsGroupName" style="height:60px">${GroupName}</td>
            <td class="project-title clsUnitRate" style="text-align: right;height:60px">${UnitRate}</td>
            <td class="project-title clsInterestRate" style="text-align: right;height:60px">${InterestRate}</td>
            <td class="project-title clsTotalPay" style="text-align: right;height:60px">${TotalPay}</td>



            <td class="project-title clsSignature" style="width: 140px;height:60px">${Signature}</td>
        

        </tr>
    </script>

    <script src="../../js/Page_JS/Report_WPPF_Payment.js"></script>
</asp:Content>

