<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="UploadFile.aspx.cs" Inherits="Pages_HCM_UploadFile" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>File Upload </h2>
            <ol class="breadcrumb">

                <li>
                    <a href="#">HCM</a>
                </li>
                <li class="active">
                    <strong>File Upload</strong>
                </li>
            </ol>
        </div>
    </div>

    <div class="row">
        <div class="col-lg-12" style="margin-top: 11px;">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title">File Upload</h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-2">
                            <label>Group</label>
                            <select class="form-control ddlGroup" onchange="GetCompany(this)"></select>
                        </div>
                        <div class="col-lg-3">
                            <label>Company</label>
                            <select class="form-control ddlCompany" onchange="GetAllowances();"></select>
                        </div>
                        <div class="col-lg-2">
                            <label>Month</label>
                            <input type="text" class="form-control dtMonthOf DatePickerMonthComplete" />
                        </div>
                        <div class="col-lg-5">
                            <label>Select File</label>
                            <input type="file" class="form-control btn-primary" id="fileMedicalReinbursement" accept="application/vnd.ms-excel,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-2">
                            <label>Upload Type</label>
                            <select class="form-control ddlUploadType" onchange="UploadType();"></select>
                        </div>

                        <div class="col-lg-2 divAllowance">
                            <label>Allowance</label>
                            <select class="form-control ddlAllowance"></select>
                        </div>

                        <div class="col-lg-3 divddlyear" style="display: none">
                            <label>Year</label>
                            <select class="form-control ddlYear"></select>
                        </div>

                        <div class="col-lg-3 divddlgeneraldata" style="display: none">
                            <label>File Type</label>
                            <select class="form-control ddlgeneraldata"></select>
                        </div>

                        <div class="col-lg-3" id="divDownloadSamplFileFormat" style="display: none">
                            <label>Download File Format</label>
                            <br />
                            <a href="#" id="anchorfiledownload" download>
                                <img src="/Images/downld.gif" alt="W3Schools">
                            </a>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <input type="button" style="margin-top: 22px; float: right" class="btn btn-default pull-right aligncontrol" value="Cancel" onclick="Cancel();" />
                            <input type="button" style="margin-top: 22px; float: right" class="btn btn-success pull-right aligncontrol" value="Save" onclick="if(confirm('Are you sure you wants to upload and save?')){UploadFile();}" />
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <script src="../../js/Page_JS/Constant.js"></script>
    <script src="../../js/Page_JS/UploadFile.js"></script>

</asp:Content>

