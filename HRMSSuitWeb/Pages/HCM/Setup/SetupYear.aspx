<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="SetupYear.aspx.cs" Inherits="Pages_HCM_Setup_SetupYear" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Year Settings</h2>
            <ol class="breadcrumb">
               
                <li>
                    <a href="#">HCM Setup</a>
                </li>
                <li class="active">
                    <strong>Year Settings</strong>
                </li>
            </ol>
        </div>
    </div>
    <div class="row" runat="server" id="DivSearchPanel">
        <div class="col-lg-12" style="margin-top: 11px;">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title">Year Settings</h3>
                </div>
                <div class="panel-body">
                    <div class="frmSlabYear">
                        <div class="col-lg-2">
                            <label for="exampleIsnputEmail2">Group</label>
                            <select onchange="GetCompany(this)" disabled="disabled" class="form-control ddlGroup">
                            </select>
                        </div>
                        <div class="col-lg-4">
                            <label for="exampleIsnputEmail2">Company</label>
                            <select class="form-control ddlCompany">
                            </select>
                        </div>
                        <div class="col-lg-2">
                            <label for="exampleIsnputEmail2">Year From</label>
                            <input type="text" class="form-control DatePicker txtYearFrom" />
                        </div>
                        <div class="col-lg-2">
                            <label for="exampleIsnputEmail2">Year To</label>
                            <input type="text" class="form-control DatePicker txtYearTo" />
                        </div>
                        <div class="col-lg-2">
                            <label for="exampleIsnputEmail2">Active Year</label>
                            <span class="form-control">
                                <input type="checkbox" id="chkActiveYear" value="1" class="chkActiveYear" />
                            </span>
                        </div>
                        <div class="col-lg-12">
                            <input type="button" onclick="SaveTaxYear()" class="btn btn-primary pull-right" style="margin-top: 23px;" value="Save" />
                        </div>
                    </div>
                </div>

                <div class="panel-footer" style="text-align: right;">
                    <input type="button" class="btn btn-success btnSearch " value="Search" onclick="GetTaxYear();" />
                    <input type="button" class="btn btn-default btnCancel" value="Cancel" onclick="ClearFields();" />
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
                                    <th class="project-title">Year Slab</th>
                                    <th class="project-title">Active Year</th>
                                    <th class="project-title">Action</th>
                                </tr>
                            </thead>
                            <tbody class="tbodyYearSlabListing">
                            </tbody>
                        </table>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <script src="../../../js/Page_JS/SetupYear.js"></script>

    <script type="text/x-jQuery-tmpl" id="YearSlabListing">
        <tr>
            <input type="hidden" class="hdnYearId" value="${YearId}" />
            <input type="hidden" class="hdnGroupId" value="${GroupId}" />
            <input type="hidden" class="hdnCompanyId" value="${CompanyId}" />
            <input type="hidden" class="hdnHasTransaction" value="${HasTransactions}" />
            <input type="hidden" class="hdnYearFrom" value="${formatDate(YearFrom)}" />
            <input type="hidden" class="hdnYearTo" value="${formatDate(YearTo)}" />
            <input type="hidden" class="hdnIsCurrentActiveYear" value="${IsCurrentActiveYear}" />
            <td class="project-title">
                <label class="badge badge-info">${formatDate(YearFrom)}</label>
                <label class="badge badge-warning">TO</label>
                <label class="badge badge-info">${formatDate(YearTo)}</label></td>
            <td class="project-title">
                <label class="badge badge-info">${IsCurrentActiveYear?'Yes':'No'}</label></td>
            <td class="project-title">{{if !IsCurrentActiveYear}}
                <input type="button" onclick="if(confirm('Are you sure you wants to Re Active?')){ReActiveYearSlab(this)}" class="btn btn-xs btn-info" value="Mark Active" />
                {{/if}}
                {{if HasTransactions == 0}}
                <input type="button" class="btn btn-xs btn-primary" onclick="EditYearSlab(this)" value="Edit" />
                <input type="button" class="btn btn-xs btn-danger" onclick="if(confirm('Are you sure you wants to delete?')){DeleteYearSlab(this)}" value="Delete" /></td>
            {{/if}}
        </tr>
    </script>

</asp:Content>

