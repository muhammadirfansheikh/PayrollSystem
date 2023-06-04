<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="SetupTaxSlab.aspx.cs" Inherits="Pages_HCM_Setup_SetupTaxSlab" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Tax Slab</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="/Pages/Default.aspx">Dashboard</a>
                </li>
                <li>
                    <a href="#">Setup</a>
                </li>
                <li class="active">
                    <strong>Tax Slab</strong>
                </li>
            </ol>
        </div>
    </div>

    <div class="row" runat="server" id="DivSearchPanel">
        <div class="col-lg-12" style="margin-top: 11px;">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title">Tax Slab</h3>
                </div>
                <div class="panel-body">
                    <div class="dvEntry">
                        <input type="hidden" class="hfCompanyId" value="0" />
                        <input type="hidden" class="hfTaxId" value="0" />
                        <input type="hidden" class="hfTaxSlabId" value="0" />
                        <div class="col-lg-2 GroupDiv">
                            <label>Group</label>
                            <select onchange="GetCompany(this)" disabled="disabled" class="form-control ddlGroup">
                            </select>
                        </div>
                        <div class="col-lg-4">
                            <label for="exampleIsnputEmail2">Company</label>
                            <select onchange="GetTaxYear();" class="form-control ddlCompany"></select>
                        </div>
                        <div class="col-lg-2">
                            <label for="exampleIsnputEmail2">Tax</label>
                            <select class="form-control ddlTax"></select>
                        </div>
                        <div class="col-lg-4">
                            <label for="exampleIsnputEmail2">Tax Slab Name</label>
                            <input type="text" class="form-control txtTaxSlab" />
                        </div>
                        <div class="col-lg-2">
                            <label for="exampleIsnputEmail2">Salary Range From</label>
                            <input type="text" class="form-control txtSalaryRangeFrom numeric" value="0" />
                        </div>
                        <div class="col-lg-2">
                            <label for="exampleIsnputEmail2">Salary Range To</label>
                            <input type="text" class="form-control txtSalaryRangeTo numeric" value="0" />
                        </div>
                        <div class="col-lg-2">
                            <label for="exampleIsnputEmail2">Fixed Value</label>
                            <input type="text" class="form-control txtFixedValue numeric" />
                        </div>
                        <div class="col-lg-2">
                            <label for="exampleIsnputEmail2">Tax Percentage ( % )</label>
                            <input type="text" class="form-control txtTaxPercent numeric" />
                        </div>
                        <div class="col-lg-2">
                            <label for="exampleIsnputEmail2">Year</label>
                            <select class="form-control ddlTaxYear"></select>
                        </div>
                        <div class="col-lg-2" style="text-align: right;">
                            <input type="button" class="btn btn-primary btnSave" style="margin-top: 23px;" value="Save" onclick="Save();" />
                        </div>
                    </div>
                </div>
                <div class="panel-footer" style="text-align: right;">
                    <input type="button" class="btn btn-success btnSearch " value="Search" onclick="GetTaxSlabList();" />
                    <input type="button" class="btn btn-default " value="Cancel" onclick="ClearFields();" />
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-lg-12">
            <div class="wrapper wrapper-content animated fadeInUp">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        Records
                    </div>
                    <div class="panel-body">
                        <table class="table table-hover">
                            <thead>
                                <tr class="info">

                                    <th>Company</th>
                                    <th>Tax</th>
                                    <th>Tax Slab</th>
                                    <th>Salary Range Start</th>
                                    <th>Salary Range End</th>
                                    <th>Fixed Value</th>
                                    <th>Tax ( % )</th>
                                    <th>Tax Year</th>
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
    </div>

    <script src="../../../js/Page_JS/SetupTaxSlab.js"></script>

    <script type="text/x-jQuery-tmpl" id="TaxSlabListing">
        <tr>

            <td class="project-title" style="display: none;">${TaxSlabId}</td>
            <td class="project-title" style="display: none;">${CompanyId}</td>
            <td class="project-title" style="display: none;">${TaxId}</td>
            <td class="project-title" style="font-size: 10px;">${CompanyName}</td>
            <td class="project-title" style="font-size: 10px;">${Tax}</td>
            <td class="project-title" style="font-size: 10px;">${TaxSlab}</td>
            <td class="project-title" style="font-size: 10px;">${SalaryRangeStart}</td>
            <td class="project-title" style="font-size: 10px;">${SalaryRangeEnd}</td>
            <td class="project-title" style="font-size: 10px;">${FixedValue}</td>
            <td class="project-title" style="font-size: 10px;">${TaxPercent}</td>
            <td class="project-title" style="font-size: 10px;">${TaxYear}</td>
            <td class="project-title">
                <input type="button" class="btn btn-danger" value="Delete" onclick="if(confirm('Are you sure you wants to delete?')){deleteTaxSlab('${TaxSlabId}')}" />
                <input type="button" class="btn btn-primary" value="Edit" onclick="setTaxSlabFieldsOnEdit('${TaxSlabId}', '${CompanyId}', '${TaxId}', '${TaxSlab}', '${SalaryRangeStart}', '${SalaryRangeEnd}', '${FixedValue}', '${TaxPercent}', '${TaxYearId}');" />
            </td>
        </tr>
    </script>

</asp:Content>

