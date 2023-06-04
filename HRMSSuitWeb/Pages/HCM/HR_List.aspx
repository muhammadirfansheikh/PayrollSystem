<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="HR_List.aspx.cs" Inherits="Pages_HCM_HR_List" %>
<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
      <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="HR List" />
            </h2>
            <ol class="breadcrumb">
              
                <li>
                    <a href="#">Other Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="HR List" />
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

            <div class="col-lg-3">
                Month
                <input type="text" class="form-control txtFromDate DatePickerMonthComplete txtMonthOfPayroll" />
            </div>

            
            <div class="col-lg-4">
            </div>
            <div class="col-lg-2 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tableHRListing','Report_HR_List')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tableHRListing')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            Executive List
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tableHRListing" id="tableHRListing">
                <thead class="theadHRlisting">
                    <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    <tr class="info">
                        <th>SR. NO.</th>
                        <th>Employee No</th>
                        <th>Employee Name</th>
                        <th>Father Name</th>
                        <th>Designation</th>
                        <th>Sap Cost Center Code</th>
                        <th>Sap Cost Center</th>
                        <th>Location</th>
                        <th>Department</th>
                        <th>Unit Code</th>
                        <th>Unit</th>
                        <th>Age</th>
                        <th>Address</th>
                        <th>NTN</th>
                        <th>Group Code</th>
                        <th>Date Of Joining</th>
                        <th>Date Of Confirmation</th>
                        <th>Date Of Birth</th>
                        <th>CNIC</th>
                        <th>Nap</th>
                        <th>Gender</th>
                        <th>Marital Status</th>
                        <th>Qualification 1</th>
                        <th>Qualification 2</th>
                        <th>degin</th>
                        <th>degyr</th>
                        <th>degfrom</th>
                        <th>Province</th>
                        <th>Blood Group</th>
                        <th>Phone</th>
                        <th>Cell</th>
                        <th>Official Email Address</th>
                        <th>Personal Email Address</th>


                    </tr>
                </thead>
                <tbody class="tbodyHRListing">
                </tbody>

            </table>
        </div>
        <br />
    </div>
    <script type="text/x-jQuery-tmpl" id="HRListing">
        <tr class="trList">
            <td class="project-title"></td>
            <td class="project-title clsEmployeeCode ABC">${EmpNo}</td>
            <td class="project-title clsName">${EmployeeName}</td>
            <td class="project-title clsDesignation">${FatherName}</td>
            <td class="project-title clsDesignation">${clsDesignation}</td>
            <td class="project-title clsDesignation">${SapCostCenterCode}</td>
            <td class="project-title clsSapCostCenter">${clsSapCostCenter}</td>
            <td class="project-title clsDesignation">${clsLocation}</td>
            <td class="project-title clsDesignation">${clsDepartment}</td>
            <td class="project-title clsUnitCode">${UnitCode}</td>
            <td class="project-title clsDesignation">${Unit}</td>
            <td class="project-title clsDesignation">${Age}</td>
            <td class="project-title clsDesignation">${Address}</td>
            <td class="project-title clsDesignation">${NTN}</td>
            <td class="project-title clsGroupCode">${GroupCode}</td>
            <td class="project-title clsDesignation">${DateOfJoining}</td>
            <td class="project-title clsDesignation">${DateOfConfirmation}</td>
            <td class="project-title clsDesignation">${DateOfBirth}</td>
            <td class="project-title clsDesignation">${CNIC}</td>
            <td class="project-title clsDesignation">${Nap}</td>
            <td class="project-title clsDesignation">${Gender}</td>
            <td class="project-title clsDesignation">${MaritalStatus}</td>
            <td class="project-title clsQualification1">${Qualification1}</td>
            <td class="project-title clsQualification2">${Qualification2}</td>
            <td class="project-title clsdegin">${degin}</td>
            <td class="project-title clsdegyr">${degyr}</td>
            <td class="project-title clsdegfrom">${degfrom}</td>


            <td class="project-title clsProvince">${Province}</td>
            <td class="project-title clsDesignation">${BloodGroup}</td>
            <td class="project-title clsDesignation">${Phone}</td>
            <td class="project-title clsDesignation">${Cell}</td>
             <td class="project-title clsOfficeEmailAddress">${OfficialEmailAddress}</td>
            <td class="project-title clsPersonalEmailAddress">${PersonalEmailAddress}</td>
   
        </tr>
    </script>
    <script src="../../js/Page_JS/Rpt_HR_List.js"></script>
</asp:Content>

