<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="SetupWppfSlab.aspx.cs" Inherits="Pages_HCM_Setup_SetupWppfSlab" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">


    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>WPPF Slabs</h2>
            <ol class="breadcrumb">

                <li>
                    <a href="#">HCM Setup</a>
                </li>
                <li class="active">
                    <strong>WPPF Slab</strong>
                </li>
            </ol>
        </div>
    </div>

    <div class="row" runat="server" id="DivSearchPanel">
        <div class="col-lg-12" style="margin-top: 11px;">

            <div class="panel panel-info">
                <div class="panel-heading">
                    Setup WPPF Slab 
                </div>
                <div class="panel-body">

                    <div class="dvEntry">

                        <input type="hidden" class="hfCompanyId" value="0" />
                        <%-- <input type="hidden" class="hfTaxId" value="0" />--%>
                        <input type="hidden" class="hfWppfSlabId" value="0" />

                        <div class="col-lg-2">
                            <label for="exampleIsnputEmail2">Company</label>
                            <select onchange="GetYear();" class="form-control ddlCompany"></select>

                        </div>

                        <%-- <div class="col-lg-2">
                    <label for="exampleIsnputEmail2">Tax</label>
                    <select class="form-control ddlTax"></select>
                </div>--%>

                        <div class="col-lg-3">
                            <label for="exampleIsnputEmail2">WPPF Category Name</label>
                            <input type="text" class="form-control txtWppfSlab" />
                        </div>

                        <div class="col-lg-2">
                            <label for="exampleIsnputEmail2">Salary Range From</label>
                            <input type="text" class="form-control txtSalaryRangeFrom numeric" value="0" />
                        </div>

                        <div class="col-lg-2">
                            <label for="exampleIsnputEmail2">Salary Range To</label>
                            <input type="text" class="form-control txtSalaryRangeTo numeric" value="0" />
                        </div>

                        <div class="col-lg-1">
                            <label for="exampleIsnputEmail2">Unit Value</label>
                            <input type="text" class="form-control txtUnitValue numeric" />
                        </div>

                        <%--<div class="col-lg-2">
                    <label for="exampleIsnputEmail2">Tax Percentage ( % )</label>
                    <input type="text" class="form-control txtTaxPercent numeric" />
                </div>--%>

                        <div class="col-lg-2">
                            <label for="exampleIsnputEmail2">Year</label>
                            <select class="form-control ddlYear"></select>
                        </div>

                        <%--<div class="col-lg-2">
                    <label for="exampleIsnputEmail2">Year From</label>
                    <input type="text" class="form-control txtYearFrom DatePickerMonth" />
                </div>

                <div class="col-lg-2">
                    <label for="exampleIsnputEmail2">Year To</label>
                    <input type="text" class="form-control txtYearTo DatePickerMonth" />
                </div>--%>
                    </div>

                </div>

                <div class="panel-footer">
                    <input type="button" class="btn btn-default " value="Cancel" onclick="ClearFields();" />
                    <input type="button" class="btn btn-primary btnSave " value="Save" onclick="Save();" />
                    <input type="button" class="btn btn-success btnSearch " value="Search" onclick="GetWppfSlabList();" />

                </div>

            </div>
        </div>
    </div>

    <div class="row" runat="server" id="Div1">
        <div class="col-lg-12" style="margin-top: 11px;">
            <div class="panel panel-info">
                <div class="panel-heading">
                    Wppf Slab Listing
                </div>
                <div class="panel-body">
                    <table class="table table-hover">
                        <thead>
                            <tr class="info">

                                <th>Company</th>
                                <%--<th>Tax</th>--%>
                                <th>Wppf Slab</th>
                                <th>Salary Range Start</th>
                                <th>Salary Range End</th>
                                <th>Unit Value</th>
                                <%--<th>Tax ( % )</th>--%>
                                <th>Year</th>
                                <th>Action</th>

                            </tr>
                        </thead>
                        <tbody class="tbodyTaxSlabListing">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <script src="../../../js/Page_JS/SetupWppfSlab.js"></script>

    <script type="text/x-jQuery-tmpl" id="TaxSlabListing">
        <tr>

            <td class="project-title" style="display: none;">${WppfSlabId}</td>
            <td class="project-title" style="display: none;">${CompanyId}</td>
            <%-- <td class="project-title" style="display: none;">${TaxId}</td>--%>
            <td class="project-title" style="font-size: 10px;">${CompanyName}</td>
            <%--     <td class="project-title" style="font-size: 10px;">${Tax}</td>--%>
            <td class="project-title" style="font-size: 10px;">${WppfSlab}</td>
            <td class="project-title" style="font-size: 10px;">${SalaryRangeStart}</td>
            <td class="project-title" style="font-size: 10px;">${SalaryRangeEnd}</td>
            <td class="project-title" style="font-size: 10px;">${UnitValue}</td>
            <%--<td class="project-title" style="font-size: 10px;">${TaxPercent}</td>--%>
            <td class="project-title" style="font-size: 10px;">${Year}</td>
            <td class="project-title">
                <input type="button" class="btn btn-danger" value="Delete" onclick="if(confirm('Are you sure you wants to delete?')){deleteWppfSlab('${WppfSlabId}')}" />
                <input type="button" class="btn btn-primary" value="Edit" onclick="setWppfSlabFieldsOnEdit('${WppfSlabId}', '${CompanyId}',  '${WppfSlab}', '${SalaryRangeStart}', '${SalaryRangeEnd}', '${UnitValue}', '${YearId}');" />
            </td>
        </tr>
    </script>

</asp:Content>
