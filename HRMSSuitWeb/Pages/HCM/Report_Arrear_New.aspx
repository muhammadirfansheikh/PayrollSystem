<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_Arrear_New.aspx.cs" Inherits="Pages_HCM_Report_Arrear_New" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <iframe id="txtArea1" style="display: none"></iframe>
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="Arrear List" />
            </h2>
            <ol class="breadcrumb">

                <li>
                    <a href="#">Other Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Arrear List" />
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



            <div class="col-lg-3">
                Month
                <input type="text" class="form-control txtFromDate DatePickerMonthComplete txtMonthOfPayroll" />
            </div>

            <div class="col-lg-2 ">
                <label>Select </label>
                <select class="form-control  ddlGroupBy"></select>
            </div>
            <div class="col-lg-8 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tableArrearListing','Report_Arrear_List')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tableArrearListing')" />
                
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            Provident Fund Resigned Employees
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tableArrearListing" id="tableArrearListing">
                <thead class="theadArrearListlisting">
              
                        <uc:ReportHeader runat="server" ID="ReportHeader2" />
                  
                   <%-- <tr class="info">
                        <th>Sr No</th>
                        <th>sacd</th>
                        <th>sco</th>
                        <th>Employee Code</th>


                        <th>smmyy</th>

                        <th>sbasic</th>
                        <th>shrt</th>
                        <th>smed</th>
                        <th>smpf</th>
                        <th>sgross</th>
                        <th>snet</th>

                        <th>syrmm</th>


                    </tr>--%>
                </thead>
                <tbody class="tbodyArrearListListing">
                </tbody>
               <%-- <tfoot>
                    <tr class="info">



                        <td colspan="5" style="font-weight: bold">Total</td>


                        <td class="tdsbasic" style="text-align: right"></td>
                        <td class="tdshrt" style="text-align: right"></td>
                        <td class="tdsmed" style="text-align: right"></td>
                        <td class="tdsmpf" style="text-align: right"></td>
                        <td class="tdsgross" style="text-align: right"></td>
                        <td class="tdsnet" style="text-align: right"></td>
                        <td class="tdsyrmm" style="text-align: right"></td>


                    </tr>
                </tfoot>--%>
            </table>
        </div>
        <br />
    </div>
    <script type="text/x-jQuery-tmpl" id="ArrearListListing">
        <tr class="trList">
            <td class="project-title"></td>

            <td class="project-title clssacd ABC">${sacd}
                  <input class="clsSapCostCenter" type="hidden" value="${clsSapCostCenter}" />
                <input class="clsCostCenter" type="hidden" value="${clsCostCenter}" />
                <input class="clsLocation" type="hidden" value="${clsLocation}" />
                <input class="clsDepartment" type="hidden" value="${clsDepartment}" />
            </td>
            <td class="project-title clssco">${sco}</td>
            <td class="project-title clsEmployeeCode">${EmployeeCode}</td>
            <td class="project-title clssmmyy" style="text-align: right">${smmyy}</td>
            <td class="project-title clssbasic" style="text-align: right">${sbasic}</td>
            <td class="project-title clsshrt" style="text-align: right">${shrt}</td>
            <td class="project-title clssmed" style="text-align: right">${smed}</td>
            <td class="project-title clssmpf" style="text-align: right">${smpf}</td>
            <td class="project-title clssgross" style="text-align: right">${sgross}</td>
            <td class="project-title clssnet" style="text-align: right">${snet}</td>
            <td class="project-title clssyrmm" style="text-align: right">${syrmm}</td>







        </tr>
    </script>

    <script src="../../js/Page_JS/Report_Arrear_New.js"></script>
</asp:Content>

