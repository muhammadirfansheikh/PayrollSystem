<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="MergeSalaryBonus.aspx.cs" Inherits="Pages_HCM_MergeSalaryBonus" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="Merge Salary Bonus" />
            </h2>
            <ol class="breadcrumb">

                <li>
                    <a href="#">HCM</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Merge Salary Bonus" />
                    </strong>
                </li>
            </ol>
        </div>
    </div>

    <div class="row" runat="server" id="DivSearchPanel">
        <div class="col-lg-12" style="margin-top: 11px;">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title">Search</h3>
                </div>
                <div class="panel-body">

                    <div class="divPayrollForm">
                        <div class="form-group col-lg-3">
                            <label for="exampleIsnputEmail2">Group</label>
                            <select onchange="GetCompany(this)" class="form-control ddlGroup">
                            </select>
                        </div>

                        <div class="form-group col-lg-3">
                            <label for="exampleInputPassword2">Company</label>
                            <select class="form-control ddlCompany" onchange="GetSalaryMonth();GetBonus();">
                            </select>
                        </div>

                        <%--<div class="form-group col-lg-3">
                            <label for="exampleInputPassword2">Salary Month</label>
                            <select class="form-control ddlSalaryMonth">
                            </select>
                        </div>--%>

                        <%--<div class="form-group col-lg-2">
                            <label for="exampleInputPassword2">Select Month</label>
                            <input type="text" class="form-control txtMonth DatePickerComplete" onchange="" />
                        </div>--%>
                    </div>
                    <div class="form-group col-lg-3 divBonus">
                        <label for="exampleInputPassword2">Separate Bonus</label>
                        <select class="form-control ddlSeparateBonus">
                        </select>
                    </div>

                    <div class="form-group col-lg-12">

                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default pull-right" />
                        <input type="button" onclick="if(confirm('Are you sure you wants to merge?')){MergeSalaryBonus()}" class="btn btn-primary pull-right btnSearch" style="display: none" value="Merge" />
                        <input type="button" onclick="if(confirm('Are you sure you wants to release bonus?')){ReleaseBonus()}" class="btn btn-info pull-right btnReleaseBonus" value="Release Bonus" />
                        <input type="button" onclick="ViewBonus()" class="btn btn-success pull-right btnReleaseBonus" value="View Bonus" />
                        <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tableBonusListing','Merger_Salary')" />
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
                <label>Select </label>
                <select class="form-control  ddlGroupBy"></select>
            </div>

          <div class="col-lg-8 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tablePropotionateListing','EOBI_Listing')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tablePropotionateListing')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            Release Bonus 
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tableBonusListing" id="tableBonusListing">
                <thead class="theadBonuslisting">

                    <tr class="info">
                        <th>S No.</th>
                        <th>Employee Code</th>
                        <th>Employee Name</th>
                        <th>Bonus Name</th>
                        <th>Bonus Amount</th>
                        <th>Joining Date</th>
                        <th>Bonus Release Date</th>


                    </tr>
                </thead>
                <tbody class="tbodyBonusListing">
                </tbody>
                <tfoot>
                    <tr class="info">
                        <td colspan="4">Total</td>

                        <th class="tdBonusAmount" style="text-align:right">0</th>

                        <td></td>
                        <td></td>
                    </tr>
                </tfoot>
            </table>
        </div>
    </div>

    <script type="text/x-jQuery-tmpl" id="BonusListing">
        <tr class="trList">
            <td class="project-title clsSNo">${Sno}</td>
            <td class="project-title clsEmployeeCode ABC">${EmployeeCode}
                 <input class="clsSapCostCenter" type="hidden" value="${clsSapCostCenter}" />
                <input class="clsCostCenter" type="hidden" value="${clsCostCenter}" />
                <input class="clsLocation" type="hidden" value="${clsLocation}" />
                <input class="clsWagesPaid" type="hidden" value="${WagesPaid}" />
            </td>
            <td class="project-title clsEmployeeName">${EmployeeName}</td>
            <td class="project-title clsBonusName">${BonusName}</td>
            <td class="project-title clsBonusAmount" style="text-align:right">${BonusAmount}</td>
            <td class="project-title clsJoiningDate">${JoiningDate}</td>
            <td class="project-title clsBonusReleaseDate">${BonusReleaseDate}</td>


        </tr>
    </script>

    <script src="../../js/Page_JS/MergeSalaryBonus.js"></script>

</asp:Content>

