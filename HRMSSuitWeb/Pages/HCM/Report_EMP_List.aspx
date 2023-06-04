<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_EMP_List.aspx.cs" Inherits="Pages_HCM_Report_EMP_List" %>

<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <iframe id="txtArea1" style="display: none"></iframe>
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="Employee List" />
            </h2>
            <ol class="breadcrumb">
                
                <li>
                    <a href="#">Other Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="Employee List" />
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


            <%--<div class="col-lg-2 ">
                <label>Bank</label>
                <select class="form-control ddlBank" onchange="BindBankBranch();"></select>
            </div>

            <div class="col-lg-2 ">
                <label>Branch</label>
                <select class="form-control ddlBranch"></select>
            </div>--%>

            <div class="col-lg-2 ">
                <label>Select </label>
                <select class="form-control  ddlGroupBy"></select>
            </div>

            <div class="col-lg-3">
                Month
                <input type="text" class="form-control txtFromDate DatePickerMonthComplete txtMonthOfPayroll" />
            </div>

            <div class="col-lg-2">
            </div>
            <div class="col-lg-8 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tableEMPListListing','Report_Employee_List')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tableEMPListListing')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            Employee List
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tableEMPListListing" id="tableEMPListListing">
                <thead class="theadEMPListlisting">
                    <tr>
                        <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    </tr>
                    <tr class="info">
                        <th>Sr No</th>
                        <th>Employee No</th>
                        <th>Employee Name</th>
                        <th>Father Name</th>
                        <th>Designation</th>
                        <th>Gender</th>
                        <th>Marital Status</th>
                        <th>Religion</th>
                        <th>Qualification 1</th>
                        <th>Qualification 2</th>
                        <th>Province</th>
                        <th>Location Code</th>
                        <th>Location</th>
                        <th>Dept Code</th>
                        <th>Department</th>
                        <th>Cost Code</th>
                        <th>Cost Center</th>
                        <th>Unit Code</th>
                        <th>Unit</th>
                        <th>Sap Cost Center Code</th>
                        <th>Sap Cost Center</th>
                        <th>Reporting Cost Center Code</th>
                        <th>Reporting Cost Center</th>
                        <th>Age</th>
                        <th>Service</th>
                        <th>Address</th>
                        <th>City</th>
                        <th>Blood Group</th>
                        <th>Phone</th>
                        <th>Mobile</th>
                        <th>Official Email Address</th>
                        <th>Personal Email Address</th>
                        <th>NTN</th>
                        <%--<th>SESSI Code</th>--%>
                        <th>SESSI Code</th>
                        <th>EOBI Number</th>
                        <th>EOBI Date</th>
                        <th>SESA Number</th>
                        <th>Group Code</th>
                        <th>Date Of Joining</th>
                        <th>Date Of Confirmation</th>
                        <th>Date Of Birth</th>
                        <th>NIC NO</th>
                        <th>CNIC</th>
                        <th>Bank</th>
                        <th>Branch</th>
                        <th>Account Number</th>
                        <th>Nap</th>
                        <th>Make</th>
                        <th>Registration Number</th>
                        <th>Company Name</th>
                        <th>H List</th>
                        <th>Company</th>





                        <th>Basic</th>
                        <th>House Rent</th>
                        <th>Cola</th>
                        <th>Medical Allownce</th>
                        <th>Special Allownce</th>
                        <th>Gross</th>
                        <th>Other Allownces</th>
                        <th>Bonus</th>
                        <th>Performance Bonus</th>
                        <th>PF</th>
                        <th>EOBI</th>
                        <th>SESSI</th>
                        
                        <th>Car Allownce</th>
                        <th>Cell Allownce</th>
                        <th>Conveyance ReImburstment</th>
                        <th>Hardship</th>
                        <th>Cordination Allowance</th>
                        <th>Fuel</th>
                        <th>Repair Maintainance</th>
                        <th>Cell Limit</th>
                        
                        <th>Fuel In Litres</th>
                        <th>Installment Amount</th>
                        <th>Out Standing Allownce</th>

                    </tr>
                </thead>
                <tbody class="tbodyEMPListListing">
                </tbody>
                <tfoot>
                    <tr class="info">
                        <td colspan="52" style="font-weight:bold">Total</td>
                        <td class="tdTotalBasic" style="text-align:right"></td>
                        <td class="tdHouseRent" style="text-align:right"></td>
                        <td class="tdCola" style="text-align:right"></td>
                        <td class="tdMedicalAlownce" style="text-align:right"></td>
                        <td class="tdSpecialAlownce" style="text-align:right"></td>
                        <td class="tdTotalGross" style="text-align:right"></td>
                        <td class="tdTotalOtherAllownces" style="text-align:right"></td>
                        <td class="tdTotalBonus" style="text-align:right"></td>
                        <td class="tdTotalPerformanceBonus" style="text-align:right"></td>
                        <td class="tdTotalPF" style="text-align:right"></td>
                        <td class="tdTotalEOBI" style="text-align:right"></td>
                        <td class="tdTotalSessi" style="text-align:right"></td>
                      
                        <td class="tdTotalCarAllownce" style="text-align:right"></td>
                        <td class="tdTotalCellAllownces" style="text-align:right"></td>
                        <td class="tdTotalConveyanceReImburstment" style="text-align:right"></td>
                        
                        <td class="tdTotalHardShip" style="text-align:right"></td>
                        <td class="tdTotalCordinationAllowance" style="text-align:right"></td>
                        <td class="tdTotalFuel" style="text-align:right"></td>
                        <td class="tdTotalRepairMaintainance" style="text-align:right"></td>
                        <td class="tdTotalCellLimit" style="text-align:right"></td>

                        <td class="tdTotalFuelInLitres" style="text-align:right"></td>
                        <td class="tdInstallmentAllownce" style="text-align:right"></td>
                        <td class="tdOutStandingAllownce" style="text-align:right"></td>

                    </tr>
                </tfoot>
            </table>
        </div>
        <br />
    </div>
    <script type="text/x-jQuery-tmpl" id="EMPListListing">
        <tr class="trList">
            <td class="project-title ">
                 
            </td>

            <td class="project-title clsEmpNo ABC">${EmpNo}
                <input class="clsSapCostCenter" type="hidden" value="${clsSapCostCenter}" />
                <input class="clsCostCenter" type="hidden" value="${clsCostCenter}" />
                <input class="clsLocation" type="hidden" value="${clsLocation}" />
                <input class="clsDepartment" type="hidden" value="${clsDepartment}" />
            </td>
            <td class="project-title clsNameOfEmployees">${NameOfEmployees}</td>
            <td class="project-title clsFatherName">${FatherName}</td>
            <td class="project-title clsDesignation">${clsDesignation}</td>
            <td class="project-title clsGender">${Gender}</td>
            <td class="project-title clsMaritalStatus">${MaritalStatus}</td>
            <td class="project-title clsReligion">${Religion}</td>
            <td class="project-title clsEducation1">${Qualification1}</td>
            <td class="project-title clsEducation2">${Qualification2}</td>
            <td class="project-title clsProvince">${Province}</td>
            <td class="project-title clsLocationCode">${LocationCode}</td>
            <td class="project-title clsLocation">${clsLocation}</td>
            <td class="project-title clsDepartmentCode">${DepartmentCode}</td>
            <td class="project-title clsDepartment">${clsDepartment}</td>
            <td class="project-title clsCostCode">${CostCode}</td>
            <td class="project-title clsCostCenter">${clsCostCenter}</td>
            <td class="project-title clsUnit">${UnitCode}</td>
            <td class="project-title clsUnit">${Unit}</td>
            <td class="project-title clsSapCostCenterCode">${SapCostCenterCode}</td>
            <td class="project-title clsSapCostCenter">${clsSapCostCenter}</td>
            <td class="project-title clsReportingCostCenterCode">${ReportingCostCenterCode}</td>
            <td class="project-title clsReportingCostCenter">${ReportingCostCenter}</td>
            <td class="project-title clsAge">${Age}</td>
            <td class="project-title clsService">${Service}</td>
            <td class="project-title clsAddress">${Address}</td>
            <td class="project-title clsCity">${City}</td>
            <td class="project-title clsBloodGroup">${BloodGroup}</td>
            <td class="project-title clsPhone">${Phone}</td>
            <td class="project-title clsMobile">${Mobile}</td>
            <td class="project-title clsOfficeEmailAddress">${OfficialEmailAddress}</td>
            <td class="project-title clsPersonalEmailAddress">${PersonalEmailAddress}</td>
            <td class="project-title clsNTN">${NTN}</td>
            <%--<td class="project-title clsSESSICode">${SESSICode}</td>--%>
            <td class="project-title clsSessiCode">${SESSICODE}</td>
            <td class="project-title clsEOBINumber">${EOBINumber}</td>
            <td class="project-title clsEOBIDate">${EOBIDate}</td>
            <td class="project-title clsSESANumber">${SESANumber}</td>
            <td class="project-title clsGroupCode">${GroupCode}</td>
            <td class="project-title clsDateOfJoining">${DateOfJoining}</td>
            <td class="project-title clsDateOfConfirmation">${DateOfConfirmation}</td>
            <td class="project-title clsDateOfBirth">${DateOfBirth}</td>
            <td class="project-title clsNICNo">${NICNo}</td>
            <td class="project-title clsCNIC">${CNIC}</td>
            <td class="project-title clsBank">${Bank}</td>
            <td class="project-title clsBranch">${Branch}</td>
            <td class="project-title clsAccountNumber" style='mso-number-format:"\@";'>${AccountNumber}</td>
            <td class="project-title clsNap">${Nap}</td>
            <td class="project-title clsMake">${Make}</td>
            <td class="project-title clsRegistrationNumber">${RegistrationNumber}</td>
            <td class="project-title clsCompanyName">${CompanyName}</td>
            <td class="project-title clsHList">${HList}</td>
            <td class="project-title clsCompany">${Company}</td>


            <td class="project-title clsBasic" style="text-align:right">${Basic}</td>
            <td class="project-title clsHouseRent" style="text-align:right">${HouseRent}</td>
            <td class="project-title clsCola" style="text-align:right">${Cola}</td>
            <td class="project-title clsMedicalAllowance" style="text-align:right">${MedicalAllowance}</td>
            <td class="project-title clsSpecialAllowance" style="text-align:right">${SpecialAllowance}</td>
            <td class="project-title clsGross" style="text-align:right">${Gross}</td>
            <td class="project-title clsOtherAllownces" style="text-align:right">${OtherAllowances}</td>
            <td class="project-title clsBonus" style="text-align:right">${Bonus}</td>
            <td class="project-title clsPerformanceBonus" style="text-align:right">${PerformanceBonus}</td>
            <td class="project-title clsPF" style="text-align:right">${PF}</td>
            <td class="project-title clsEOBI" style="text-align:right">${EOBI}</td>
            <td class="project-title clsSESSI" style="text-align:right">${SESSI}</td>
            
            <td class="project-title clsCarAllowance" style="text-align:right">${CarAllowance}</td>
            
            <td class="project-title clsCellAllowance" style="text-align:right">${CellAllowance}</td>
            <td class="project-title clsConveyanceReImburstment" style="text-align:right">${ConveyanceReimbursement}</td>
            <td class="project-title clsHardship" style="text-align:right">${Hardship}</td>
            <td class="project-title clsCordinationAllowance" style="text-align:right">${CoordinationAllowance}</td>
            <td class="project-title clsFuel" style="text-align:right">${Fuel}</td>
            <td class="project-title clsRepairMaintenance" style="text-align:right">${RepairMaintenance}</td>
            <td class="project-title clsCellLimit" style="text-align:right">${CellLimit}</td>
            <td class="project-title clsFuelInLitres" style="text-align:right">${FuelInLitres}</td>
            <td class="project-title clsInstallmentAmount" style="text-align:right">${InstallmentAmount}</td>
            <td class="project-title clsDis_LocationAllowance" style="text-align:right">${Dis_LocationAllowance}</td>


        </tr>
    </script>

    <script src="../../js/Page_JS/Report_EMP_List.js"></script>


</asp:Content>

