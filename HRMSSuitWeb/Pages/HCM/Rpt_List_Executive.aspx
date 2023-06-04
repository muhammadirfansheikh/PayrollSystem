<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Rpt_List_Executive.aspx.cs" Inherits="Pages_HCM_Rpt_List_Executive" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="Executive List" />
            </h2>
            <ol class="breadcrumb">
               
                <li>
                    <a href="#">Other Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Executive List" />
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
            
             <div class="col-lg-10">
         
            </div>
            <div class="col-lg-2 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tableExecutiveListing','Executive_List')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tableExecutiveListing')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
           Executive List
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tableExecutiveListing" id="tableExecutiveListing">
                <thead class="theadExecutivelisting">
                    <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    <tr class="info">
                        <th>SR. NO.</th>
                        <th>Employee No</th>
                        <th>Employee Name</th>
                        <th>Designation</th>
                      

                    </tr>
                </thead>
                <tbody class="tbodyExecutiveListing">
                </tbody>
               
            </table>
        </div>
        <br />
    </div>
    <script type="text/x-jQuery-tmpl" id="ExecutiveListing">
        <tr class="trList">
            <td class="project-title"></td>
            <td class="project-title clsEmployeeCode ABC">${EmpNo}</td>
            <td class="project-title clsName">${NameOfEmployees}</td>
            <td class="project-title clsDesignation">${clsDesignation}</td>

           
        </tr>
    </script>

    <script src="../../js/Page_JS/Rpt_List_Executive.js"></script>
</asp:Content>

