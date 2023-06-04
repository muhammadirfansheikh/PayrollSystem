<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="ArrearSeparateRelease.aspx.cs" Inherits="Pages_HCM_ArrearSeparateRelease" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
      <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

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

                        <div class="form-group col-lg-2">
                            <label for="exampleInputPassword2">Select Month</label>
                            <input type="text" class="form-control txtMonth DatePickerMonthComplete" onchange="" />
                        </div>

                    </div>
                 <%--   <div class="form-group col-lg-3 divBonus">
                        <label for="exampleInputPassword2">Separate Bonus</label>
                        <select class="form-control ddlSeparateBonus">
                        </select>
                    </div>--%>

                    <div class="form-group col-lg-12">

                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default pull-right" />
                        <input type="button" onclick="" class="btn btn-primary pull-right btnSearch" value="Lock" />
                        <input type="button" onclick="" class="btn btn-info pull-right btnCalculateArrear" value="Calculate Arrear" />

                    </div>

                </div>
            </div>
        </div>
    </div>

    <div class="panel panel-info">
        <div class="panel-heading">
            Release Arrear 
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tableBonusListing">
                <thead class="theadBonuslisting">

                    <tr class="info">
                        <th>S No.</th>
                        <th>Employee Code</th>
                        <th>Employee Name</th>
                        <th>Bonus Name</th>
                        <th>Bonus Amount</th>
                        <th>Bonus Release Date</th>
                    </tr>
                </thead>
                <tbody class="tbodyBonusListing">
                </tbody>
                <tfoot>
                    <tr class="info">
                        <%--<td colspan="7"></td>

                        <th class="tdBonusAmount">0</th>

                        <td></td>--%>
                    </tr>
                </tfoot>
            </table>
        </div>
    </div>

    <script type="text/x-jQuery-tmpl" id="BonusListing">
        <tr class="trList">
            <td class="project-title">${Sno}</td>
            <td class="project-title">${EmployeeCode}</td>
            <td class="project-title">${NAME}</td>
            <td class="project-title">${BonusName}</td>
            <td class="project-title">${AllowanceAmount}</td>
            <td class="project-title">${BonusReleaseDate}</td>

        </tr>
    </script>

     <script src="../../js/Page_JS/MergeSalaryBonus.js"></script>

</asp:Content>

