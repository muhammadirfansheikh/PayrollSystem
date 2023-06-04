<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_GroupLifeInsuranceAboveSixtyFive.aspx.cs" Inherits="Pages_HCM_Report_GroupLifeInsuranceAboveSixtyFive" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="Group Life Insurance Above Sixty Five" />
            </h2>
            <ol class="breadcrumb">
                
                <li>
                    <a href="#">Other Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Group Life Insurance Type 2" />
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
                <label>Month</label>
                <input type="text" class="form-control txtMonthOfPayroll DatePickerMonthComplete" />
            </div>

            <%--<div class="col-lg-2 ">
                <label>Bank</label>
                <select class="form-control ddlBank" onchange="BindBankBranch();"></select>
            </div>

            <div class="col-lg-2 ">
                <label>Branch</label>
                <select class="form-control ddlBranch"></select>
            </div>--%>
            <div class="col-lg-2 ">
                <label>Premium Rate </label>
                <input type="number" min="0" id="txtPremiumRate" class="txtPremiumRate form-control" />
            </div>
            <div class="col-lg-2 ">
                <label>Select </label>
                <select class="form-control  ddlGroupBy"></select>
            </div>

            <div class="col-lg-6 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tableGLIAboveListing','Group_Life_Insurance_Type_2')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tableGLIAboveListing')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            Group Life Insurance Above Sixty Five
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tableGLIAboveListing" id="tableGLIAboveListing">
                <thead class="theadGLIAbovelisting">
                    <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    <tr class="info">
                        <th>SR. NO.</th>
                        <th>Employee Code</th>
                        <th>Name</th>
                        <th>Designation</th>
                        <th>CNIC No</th>
                        <th>D.O Joining</th>
                        <th>D.O Birth</th>
                        <th>Department</th>
                        <th>Location</th>
                        <th>Cost Center</th>
                        <th>SAP Cost Center</th>
                        <th>Sum Insured</th>
                        <th>Premium Rate</th>
                        <th>Premium Amount</th>


                    </tr>
                </thead>
                <tbody class="tbodyGLIAboveListing">
                </tbody>
                <tfoot>
                    <tr class="info">
                        <td colspan="11" style="font-weight:bold">Total</td>
                        <th class="tdTotalSumInsured" style="text-align:right"></th>
                        <th class="tdTotalSumPremiumRate" style="text-align:right"></th>
                        <th class="tdTotalSumPremiumAmount" style="text-align:right"></th>

                       
                    </tr>
                </tfoot>
            </table>
        </div>
        <br />
    </div>
    <script type="text/x-jQuery-tmpl" id="GLIAboveListing">
        <tr class="trList">
            <td class="project-title"></td>

            <td class="project-title clsEmployeeCode ABC">${EmployeeCode}


                 <input class="clsSapCostCenter" type="hidden" value="${clsSapCostCenter}" />
                <input class="clsCostCenter" type="hidden" value="${clsCostCenter}" />
                <input class="clsLocation" type="hidden" value="${clsLocation}" />
                <input class="clsDepartment" type="hidden" value="${clsDepartment}" />
            </td>
            <td class="project-title clsName">${Name}</td>
            <td class="project-title clsDesignation">${clsDesignation}</td>

            <td class="project-title clcCNICNo">${CNIC}</td>
            <td class="project-title clcDOJoining">${JoiningDate}</td>
            <td class="project-title clcDOJoining">${DateOfBirth}</td>
            <td class="project-title clsDepartment">${clsDepartment}</td>
            <td class="project-title">${clsLocation}</td>

            <td class="project-title">${clsCostCenter}</td>
            <td class="project-title">${clsSapCostCenter}</td>
            <td class="project-title clsSumInsured" style="text-align:right">${SumInsured}</td>
            <td class="project-title clsPremiumRate" style="text-align:right">${PremiumRate}</td>
            <td class="project-title clsPremiumAmount" style="text-align:right">${PremiumAmount}</td>



        </tr>
    </script>

    <script src="../../js/Page_JS/Report_GroupLifInsuraneAboveSixtyFive.js"></script>
</asp:Content>

