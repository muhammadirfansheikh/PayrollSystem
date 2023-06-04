<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="MedicalReinbursement.aspx.cs" Inherits="Pages_HCM_MedicalReinbursement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <div class="panel panel-info">
        <div class="panel-heading">
            Medical Reinbursement
        </div>
        <div class="panel-body">
            <div class="viewReinbursement">
                <div class="col-lg-3">
                    <label>Select Group</label>
                    <select class="form-control ddlGroup" onchange="GetCompany(this)"></select>
                </div>
                <div class="col-lg-3">
                    <label>Select Company</label>
                    <select class="form-control ddlCompany"></select>
                </div>
                <div class="col-lg-3">
                    <label>Select Month</label>
                    <input type="text" class="form-control dtMonthOf DatePickerMonthComplete" />
                </div>
            </div>
            <div class="col-lg-3">
                <label>Select File</label>
                <input type="file" class="form-control btn-primary" id="fileMedicalReinbursement" accept="application/vnd.ms-excel" />
            </div>
            <div class="col-lg-12">
                <input type="button" class="btn btn-info pull-right aligncontrol" value="Save" onclick="Medical_UploadReinbursementFile()" />
                <input type="button" class="btn btn-warning pull-right aligncontrol" onclick="Medical_GetReInbursment()" value="View" />
            </div>
        </div>
    </div>

    <div class="panel panel-info">
        <div class="panel-heading">
            Medical Reinbursement
        </div>
        <div class="panel-body">
            <table class="table table-hover">
                <thead>
                    <tr class="info">
                        <th>Reinbursement Month</th>
                        <th>Employee Code</th>
                        <th>Employee Name</th>
                        <th>Amount Pay</th>
                    </tr>
                </thead>
                <tbody class="tbodyMedicalReinbursement">
                </tbody>
            </table>
        </div>
    </div>

    <script src="../../js/Page_JS/MedicalReinbursement.js"></script>

    <script type="text/x-jQuery-tmpl" id="MedicalReinbursement">
        <tr>
            <td class="project-title" style="font-size: 10px;">${formatDate(MonthOfReInbursement)}</td>
            <td class="project-title" style="font-size: 10px;">${EmployeeCode}</td>
            <td class="project-title" style="font-size: 10px;">${FirstName} ${LastName}</td>
            <td class="project-title" style="font-size: 10px;"><label class="label label-danger">${PayAmount}</label></td>
        </tr>
    </script>

</asp:Content>

