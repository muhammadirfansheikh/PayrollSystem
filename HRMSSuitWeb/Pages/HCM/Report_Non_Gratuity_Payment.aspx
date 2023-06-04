﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_Non_Gratuity_Payment.aspx.cs" Inherits="Pages_HCM_Report_Non_Gratuity_Payment" %>
<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
      <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
        <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="Gratuity" />
            </h2>
            <ol class="breadcrumb">

                <li>
                    <a href="#">Other Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Gratuity" />
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

           
            <div class="col-lg-8 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tableGListing','Gratuity_Report')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tableGListing')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            Gratuity
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tableGListing" id="tableGListing">
                <thead class="theadGlisting">
                    <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    <tr class="info">
                       <th>Sr No</th>
                        <th>Employee Name</th>
                       <th>Employee Code</th>
                       <th>Designation</th>
                       <th>Joining Date</th>

                       <th>Date Of Birth</th>
                       <th>Basic Salary</th>
                       <th>Year</th>
                       <th>Month</th>
                       <th>Day</th>
                       <th>Sap Code</th>
                       <th>SAP Cost Center Name</th>
                       
                    </tr>
                </thead>
                <tbody class="tbodyGListing">
                </tbody>
                <tfoot>
                    <tr class="info">
                        <td colspan="6" style="font-weight:bold">Total</td>


                        <th class="tdBasicSalary" style="text-align:right"></th>
                        <th class="tdYear" style="text-align:right"></th>
                        <th class="tdMonth" style="text-align:right"></th>
                        <th class="tdDay" style="text-align:right"></th>
                       




                        <td></td>
                        <td></td>
                    </tr>
                </tfoot>
            </table>
        </div>
        <br />
    </div>
    <script type="text/x-jQuery-tmpl" id="GListing">
         <tr class="trList">
            <td class="project-title clsSNo"></td>
            <td class="project-title clsEmployeeCode ABC">${EmployeeCode}
                
            </td>
            <td class="project-title clsNameofEmployee" >${NameofEmployee}</td>
            <td class="project-title clsDesignation">${Designation}</td>
            <td class="project-title clsJoiningDate">${JoiningDate}</td>
            <td class="project-title clsDateOfBirth">${DateOfBirth}</td>
            <td class="project-title clsBasicSalary" style="text-align:right">${BasicSalary}</td>
            
            <td class="project-title clsYear" style="text-align:right">${Year}</td>
            <td class="project-title clsMonth" style="text-align:right">${Month}</td>
            <td class="project-title clsDay" style="text-align:right">${Day}</td>
            <td class="project-title clsSapCode" >${SapCode}</td>
            <td class="project-title clsSAPCostCenterName" >${SAPCostCenterName}</td>
           
        </tr>
    </script>

    <script src="../../js/Page_JS/Non_Gratuity_Payment.js"></script>
</asp:Content>

