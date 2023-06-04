<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Bonus_Report_Pay_Order.aspx.cs" Inherits="Pages_HCM_Bonus_Report_Pay_Order" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
     <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
  <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="Bonus Pay Order" />
            </h2>
            <ol class="breadcrumb">
                
                <li>
                    <a href="#">HCM Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Bonus Pay Order" />
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

            <div class="col-lg-6 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tablePayOrderListing','Bonus_Pay_Order')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tablePayOrderListing')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
           Bonus  Pay Order 
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tablePayOrderListing" id="tablePayOrderListing">
                <thead class="theadPayOrderlisting">
                    <tr>
                        <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    </tr>
                    <tr class="info">
                        <th>Employee Code</th>
                        <th>Name</th>
                        <th>Bank </th>
                        <th>Bank Description</th>
                        <th>Bonus Amount</th>
                    </tr>
                </thead>
                <tbody class="tbodyPayOrderListing">
                </tbody>
                  <tfoot>
                    <tr class="info">
                        <td colspan="4" style="font-weight:bold">Total</td>


                        <th class="tdTotal" style="text-align:right"></th>




                    </tr>
                </tfoot>
            </table>
        </div>
        <br />
    </div>
    <script type="text/x-jQuery-tmpl" id="PayOrderListing">
        <tr>
            <td class="project-title clsEmployeeCode">${EmployeeCode}</td>
            <td class="project-title clsName">${Name}</td>
            <td class="project-title clsBankName">${clsBankName}</td>
            <td class="project-title clsBankDescription">${clsBankDescription}</td>
            <td class="project-title clsTotalPay" style="text-align:right">${BonusAmount}</td>
        </tr>
    </script>

    <script src="../../js/Page_JS/Report_Bonus_Pay_Order.js"></script>
    
</asp:Content>

