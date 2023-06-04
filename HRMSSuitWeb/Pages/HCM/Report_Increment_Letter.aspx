<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_Increment_Letter.aspx.cs" Inherits="Pages_HCM_Report_Increment_Letter" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="Increment Letter" />
            </h2>
            <ol class="breadcrumb">

                <li>
                    <a href="#">Other Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Increment Letter" />
                    </strong>
                </li>
            </ol>
        </div>
    </div>

    <uc:EmployeeSearchFilters runat="server" ID="EmployeeSearchFilters" EmployeeCode="1" CostCenter="1" />

    <div class="panel panel-info">
        <div class="panel-heading">
            Report Functions
        </div>
        <div class="panel-body">
            <div class="col-lg-3">
                Month
                <input type="text" class="form-control txtFromDate DatePickerMonthComplete txtMonthOfPayroll" />
            </div>




            <div class="col-lg-8 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tblReportIncrementLetter','Report_Increment_Letter')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tblReportIncrementLetter')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            Increment Letter
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table id="tblReportIncrementLetter" class="table table-hover tblReportIncrementLetter" style="width: 100%">
                <thead class="tblReportIncrementLetter">
                    <tr>
                        <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table class="table table-hover">

                                <tbody class="tbodyIncrementLetter">
                                </tbody>
                            </table>
                        </td>
                    </tr>
                    <tr class="info">


                        <th>Allowances</th>
                        <th style="text-align: right">New Salary Rupees</th>
                        <th style="text-align: right">Current Salary Rupees</th>
                        <th style="text-align: right">Change Rupees</th>



                    </tr>
                </thead>
                <tbody class="tbodyIncrementLetterAllownces">
                </tbody>
            <%--    <tfoot>
                    <tr class="info">
                        <td style="font-weight: bold">Total</td>

                        <td class="tdNewSalaryRupees" style="text-align: right"></td>
                        <td class="tdCurrentSalaryRupees" style="text-align: right"></td>
                        <td class="tdChangeRupees" style="text-align: right"></td>


                    </tr>
                </tfoot>--%>
            </table>
        </div>
        <br />
    </div>
    <script type="text/x-jQuery-tmpl" id="IncrementLetterEmployeeDetail">
        <tr class="trList">



            <td class="project-title clsName" style="font-weight: bold">${Name}</td>
            <td class="project-title clsEmployeeCode" style="font-weight: bold">${EmployeeCode}</td>

        </tr>
        <tr class="trList">


            <td class="project-title" style="font-weight: bold">Designation</td>
            <td class="project-title clsDesignationName">${DesignationName}</td>

        </tr>

        <tr class="trList">


            <td class="project-title" style="font-weight: bold">Location</td>
            <td class="project-title clsLocationName">${LocationName}</td>

        </tr>
        <tr class="trList">


            <td class="project-title" style="font-weight: bold">Sap Cost Center</td>
            <td class="project-title clsSapCostCenter">${SapCostCenter}</td>

        </tr>
        <tr class="trList">


            <td class="project-title" style="font-weight: bold">Date Of Joining</td>
            <td class="project-title clsDateOfJoining">${DateOfJoining}</td>

        </tr>
        <tr class="trList">


            <td class="project-title" style="font-weight: bold">Date Of Increment</td>
            <td class="project-title clsDateOfNewIncrement">${DateOfNewIncrement}</td>

        </tr>
        <tr class="trList">


            <td class="project-title" colspan="2">
                <p>
                    The Management is pleased to increase your Gross emoluments
          to Rs.**101,418/= per month w.e.f. APRIL 01, 2021. The revised
          Gross increase will be as under :

                </p>
            </td>


        </tr>
    </script>
    <script type="text/x-jQuery-tmpl" id="IncrementLetterAllownces">
        <tr class="trList">



            <td class="project-title clsAllowances">${Allowances}</td>
            <td class="project-title clsNewSalaryRupees" style="text-align: right">${NewSalaryRupees}</td>
            <td class="project-title clsCurrentSalaryRupees" style="text-align: right">${CurrentSalaryRupees}</td>
            <td class="project-title clsChangeRupees" style="text-align: right">${ChangeRupees}</td>
        </tr>
        <tr class="trList">



          

        </tr>

    </script>

    <script type="text/x-jQuery-tmpl" id="IncrementLetterAllowncesThree">
        <tr class="trList">
          <td class="project-title" colspan="4">The above Gross increment includes Rs.1,000/= as Normal 
          Increment Rs.500/= as Special increment and Rs.500/= 
          as Adjustment/Performance increment and Rs.634/= as 
          Promotional increment.

            </td>
            </tr>
        <tr class="trList">



            <td class="project-title" colspan="4">
                <p>
                    The above Gross increment includes Rs.1,000/= as Normal 
          Increment Rs.500/= as Special increment and Rs.500/= 
          as Adjustment/Performance increment and Rs.634/= as 
          Promotional increment.

          You have been Promoted as “ DEPUTY MANAGER – PLANT HR “
          w.e.f. 
                </p>
                ${DateOfNewIncrement}</td>

        </tr>
        <tr class="trList">



            <td class="project-title">KARACHI:
            </td>
            <td class="project-title"></td>
            <td class="project-title"></td>
            <td class="project-title">SYED VIQAR ALI
            </td>
        </tr>


           <tr class="trList">



            <td class="project-title">Dated  :  ${CurrentDate}
            </td>
            <td class="project-title"></td>
               <td class="project-title"></td>
            <td class="project-title">GENERAL MANAGER HUMAN RESOURCE
            </td>
        </tr>
    </script>

    <script src="../../js/Page_JS/Report_Increment_Letter.js"></script>
</asp:Content>

