<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="CompanySettings.aspx.cs" Inherits="Pages_HCM_Setup_CompanySettings" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Company Settings</h2>
            <ol class="breadcrumb">
               
                <li>
                    <a href="#">HCM Setup</a>
                </li>
                <li class="active">
                    <strong>Company Settings</strong>
                </li>
            </ol>
        </div>
    </div>

    <uc:EmployeeSearchFilters runat="server" ID="EmployeeSearchFilters" />


    <div class="row">
        <div class="col-lg-12">
            <div class="wrapper wrapper-content animated fadeInUp">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        Records
                    </div>
                    <div class="panel-body divSettingControls">
                        <div class="project-list">
                        </div>
                    </div>
                    <div class="row Div_SaveButton" style="display: none;">
                        <div class="form-group col-lg-12">
                            <input type="button" class="btn btn-primary pull-right" style="margin-right: 20px;" onclick="onSaveChanges()" value="Save" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="divBasicInterestManupulation">
        <div class="alert alert-info alert-dismissable alertCount">
            <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
            <span class="badge badge-info">Note:</span>These settings are only applicable over  <span class="badge badge-info">Basic Interest</span> calculations.
        </div>


        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">Basic Interest Detail</h3>
            </div>
            <div class="panel-body">
                <input type="button" class="btn btn-primary pull-right" value="Add New" data-toggle="modal" data-target="#CreateProjectModal" />
                <table class="table table-hover">
                    <thead>
                        <tr class="info">
                            <th>Slab Year</th>
                            <th>Interest Rate</th>
                            <th>Action</th>
                        </tr>
                    </thead>
                    <tbody class="tbodyInterestSlabListing">
                    </tbody>
                </table>
            </div>
        </div>
    </div>



    <div class="modal inmodal" id="CreateProjectModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog" style="width: 50%;">
            <div class="modal-content animated fadeIn">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">Basic Interest</h4>
                </div>
                <div class="modal-body">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            Basic Interest
                        </div>
                        <div class="panel-body">
                            <div class="divInterestInfo">
                                <div class="col-lg-6">
                                    <label>Interest Rate</label>
                                    <input type="text" class="form-control txtInterestRate numeric" />
                                </div>
                                <div class="col-lg-6">
                                    <label>Slab Year</label>
                                    <select class="form-control ddlYearSlab" onchange="onChangeYearSlab()"></select>
                                </div>
                            </div>

                        </div>
                        <div class="panel-footer">
                            <input type="button" class="btn btn-danger" onclick="ResetFieldsInterest()" value="Reset" />
                            <input type="button" class="btn btn-info btnSaveChanges" onclick="saveInterestInfo()" value="Save Changes" />
                        </div>

                    </div>
                </div>
            </div>
            <div class="modal-footer">
            </div>
        </div>
    </div> 

    <script src="../../../js/Page_JS/CompanySettings.js"></script>

    <script type="text/x-jQuery-tmpl" id="SettingControls">
        {{if IsDisplayInMenu == true}}
         <div class="col-lg-3">
             <input type="hidden" class="IsRelational" value="false" />
             <input type="hidden" class="SetupMasterId" value="${SetupMasterID}" />
             <div style="top: -50%;" class="tooltip fade top in tooltipInfo">
                 <div class="tooltip-arrow" style="top: 80%;"></div>
                 <div class="tooltip-inner">${Definition}</div>
             </div>
             <label class="lblTooltip" for="exampleIsnputEmail2">${SetupName}</label>
             <input type="text" class="form-control txtValues numericOnly" value="${Value}" />
         </div>
        {{/if}}
    </script>

    <script type="text/x-jQuery-tmpl" id="InterestSlabListing">
        <tr>
            <input type="hidden" value="${InterestId}" class="hdInterestId" />
            <input type="hidden" value="${SlabYearId}" class="hdSlabYearId" />
            <input type="hidden" value="${InterestRate}" class="hdInterestRate" />
            <input type="hidden" value="${SlabYear}" class="hdSlabYear" />
            <td class="project-title">Year ${SlabYear}</td>
            <td class="project-title">${InterestRate}%</td>
            <td class="project-title">
                <input type="button" data-toggle="modal" data-target="#CreateProjectModal" class="btn btn-primary btn-xs" onclick="EditInterestSlabRate(this)" value="Edit" />
                <input type="button" class="btn btn-danger btn-xs" onclick="DeleteInterestSlabRate(this)" value="Delete" />
            </td>
        </tr>
    </script>


</asp:Content>

