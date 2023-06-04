<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Bonus_Bank_Adhvise.aspx.cs" Inherits="Pages_HCM_Bonus_Bank_Adhvise" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="Bonus Bank Advise" />
            </h2>
            <ol class="breadcrumb">

                <li>
                    <a href="#">HCM Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Bonus Bank Advise" />
                    </strong>
                </li>
            </ol>
        </div>
    </div>
    <uc:EmployeeSearchFilters runat="server" ID="EmployeeSearchFilters" EmployeeCode="1" />
     <div class="row" runat="server" id="DivSearchPanel">
        <div class="col-lg-12" style="margin-top: 11px;">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title">Search Separate Bonus</h3>
                </div>
                <div class="panel-body">

                   
                    <div class="form-group col-lg-3 divBonus">
                        <label for="exampleInputPassword2">Separate Bonus</label>
                        <select class="form-control ddlSeparateBonus">
                        </select>
                    </div>

                

                </div>
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            Report Functions
        </div>
        <div class="panel-body">
      

            <div class="col-lg-2 ">
                <label>Bank</label>
                <select class="form-control ddlBank" onchange="BindBankBranch();"></select>
            </div>

            <div class="col-lg-2 ">
                <label>Branch</label>
                <select class="form-control ddlBranch"></select>
            </div>

            <div class="col-lg-2">
                <label>Select </label>
                <select class="form-control  ddlGroupBy"></select>
            </div>

        


            <div class="col-lg-4 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="exportBankAdhvise('#tableBankAdviseListing','Bonus_Bank_Adhvise')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tableBankAdviseListing')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            Bank Advise
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tableBankAdviseListing" id="tableBankAdviseListing">
                <thead class="theadBankAdviselisting">
                    <tr>
                        <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    </tr>
                    <tr class="info">
                        <th>S No.</th>
                        <th>Employee Code</th>
                        <th>Name</th>
                        <th>Bank</th>
                        <th>Branch Description</th>
                        <th>Branch</th>

                        <th>Account Number</th>
                        <th>Bonus Amount</th>
                    </tr>
                </thead>
                <tbody class="tbodyBankAdviseListing">
                </tbody>
                <tfoot>
                    <tr class="info">

                        <th colspan="7" class="tfootColsSpan" style="font-weight: bold">Total</th>


                        <th class="tdTotalPay" style="text-align: right"></th>

                    </tr>
                </tfoot>
            </table>
        </div>
        <br />

    </div>
    <script type="text/x-jQuery-tmpl" id="BankAdviseListing">
        <tr class="trList">
            <td class="project-title ABC clsSNo">${Sno}
                   <input class="clsSapCostCenter" type="hidden" value="${clsSapCostCenter}" />
                <input class="clsCostCenter" type="hidden" value="${clsCostCenter}" />
                <input class="clsLocation" type="hidden" value="${clsLocation}" />
                <input class="clsBankName" type="hidden" value="${clsBankName}" />
            </td>
            <td class="project-title">${EmployeeCode}</td>
            <td class="project-title">${Name}</td>
            <td class="project-title">${clsBankName}</td>
            <td class="project-title clsBankDesc">${clsBankDescription}</td>
            <td class="project-title">${Branch}</td>

            <td class="project-title" style='mso-number-format: "\@";'>${AccountNumber}</td>
            <td class="project-title clsTotalPay" style="text-align: right">${BonusAmount}</td>
        </tr>
    </script>

    <script src="../../js/Page_JS/Report_Bonus_Bank_Adhvise.js"></script>
</asp:Content>

