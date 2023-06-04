<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_Tax_Certificate.aspx.cs" Inherits="Pages_HCM_Report_Tax_Certificate" %>
<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
         <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="Tax Certificate" />
            </h2>
            <ol class="breadcrumb">
                
                <li>
                    <a href="#">Other Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Tax Certificate" />
                    </strong>
                </li>
            </ol>
        </div>
    </div>

    <uc:EmployeeSearchFilters runat="server" ID="EmployeeSearchFilters" EmployeeCode="1" />

    <div class="panel panel-info">
        <div class="panel-heading">
            Report Functions
        </div>
        <div class="panel-body">
            <div class="col-lg-2 divMonthPayroll">
                <label>From Date</label>
                <input type="text" class="form-control txtFromDate DatePickerComplete txtMonthOfPayroll" />
            </div>
              <div class="col-lg-2 divMonthPayroll">
                <label>To Date</label>
                <input type="text" class="form-control txtToDate DatePickerComplete txtMonthOfPayroll" />
            </div>

          
<%--            <div class="col-lg-12 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelOutWithouHiddenFields('.tableCertificateListing')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tableCertificateListing')" />
            </div>--%>
        </div>
    </div>
   
   

    <script src="../../js/Page_JS/Report_Tax_Certificate.js"></script>


   
</asp:Content>

