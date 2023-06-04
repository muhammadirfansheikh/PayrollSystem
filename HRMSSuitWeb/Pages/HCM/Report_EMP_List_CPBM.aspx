<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_EMP_List_CPBM.aspx.cs" Inherits="Pages_HCM_Report_EMP_List_CPBM" %>
<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
     <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
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
                From Date
                <input type="text" class="form-control txtFromDate DatePickerComplete txtMonthOfPayroll" />
            </div>

            <div class="col-lg-3">
                 To Date
                <input type="text" class="form-control txtToDate DatePickerComplete txtMonthOfPayroll" />
            </div>
            <div class="col-lg-2">
            </div>
            <div class="col-lg-8 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelOutWithouHiddenFields('.tableEMPListListing')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tableEMPListListing')" />
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            Employee List
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tableEMPListListing">
                <thead class="theadEMPListlisting">
                    <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    <tr class="info">
                        <th>Sr No</th>
                        <th>Employee No</th>
                        <th>Employee Name</th>
                        <th>Father Name</th>
                        <th>Designation</th>
                        <th>Gender</th>
                        <th>Marital Status</th>
                        <th>Religion</th>
                        <th>Location</th>
                        <th>Dept Code</th>
                        <th>Department</th>
                        <th>Cost Code</th>
                        <th>Cost Center</th>
                        <th>Unit</th>
                        <th>Sap Cost Center Code</th>
                        <th>Sap Cost Center</th>
                        <th>Age</th>
                        <th>Service</th>
                        <th>Address</th>
                        <th>Blood Group</th>
                        <th>Phone</th>
                        <th>Office Email Address</th>
                        <th>NTN</th>
                        <th>EOBI Number</th>
                        <th>EOBI Date</th>
                        <th>SESA Number</th>
                        <th>Date Of Joining</th>
                        <th>Date Of Confirmation</th>
                        <th>Date Of Birth</th>
                        <th>CNIC</th>
                        <th>Bank</th>
                        <th>Branch</th>
                        <th>Account Number</th>
                        <th>Nap</th>
                        <th>Make</th>
                        <th>Registration Number</th>
                       
                        <th>Company Name</th>


                        <th>Basic</th>

                        <th>House Rent</th>
                        <th>Cola</th>
                        <th>Medical Allownce</th>
                        <th>Conveyance Allownce</th>
                        <th>PESA</th>
                        <th>Special Allownce</th>
                        <th>Special Pay</th>

                        <th>Bonus</th>
                        <%--<th>Allownces</th>
                        <th>Gross</th>
                        <th>Bonus</th>
                        <th>PF</th>
                        <th>EOBI</th>--%>
                        <th>SESSI</th>
                        <th>Car Allownce</th>
                        <th>Cell Allownce</th>
                        <th>Hardship</th>
                        <th>Fuel</th>
                        <th>Repair Maintainance</th>
                         <th>Fuel In Litres</th>
                        
                        
                       
                    </tr>
                </thead>
                <tbody class="tbodyEMPListListing">
                </tbody>
                <tfoot>
                    <tr class="info">
                        <td colspan="37"></td>
                        <td class="tdTotalBasic"></td>
                        <td class="tdHouseRent"></td>
                        <td class="tdCola"></td>
                        <td class="tdMedicalAllownce"></td>
                        <td class="tdConveyanceAllownce"></td>
                        <td class="tdPESA"></td>
                        <td class="tdSpecialAllownce"></td>
                        <td class="tdSpecialPay"></td>
                        
<%--                        <td class="tdTotalAllownces"></td>
                        <td class="tdTotalGross"></td>--%>
                        <td class="tdTotalBonus"></td>
                        <%--<td class="tdTotalPF"></td>
                        <td class="tdTotalEOBI"></td>--%>
                        <td class="tdTotalSessi"></td>
                        <td class="tdTotalCarAllownce"></td>
                        <td class="tdTotalCellAllownces"></td>
                        <td class="tdTotalHardShip"></td>
                        <td class="tdTotalFuel"></td>
                        <td class="tdTotalRepairMaintainance"></td>
                        
                        <td class="tdTotalFuelInLitres"></td>
                        
                        <td></td>
                    </tr>
                </tfoot>
            </table>
        </div>
        <br />
    </div>
    <script type="text/x-jQuery-tmpl" id="EMPListListing">
        <tr class="trList">
            <td class="project-title ABC">${Sr_No}
                 <input class="clsSapCostCenter" type="hidden" value="${SapCostCenter}" />
                <input class="clsCostCenter" type="hidden" value="${CostCenter}" />
                <input class="clsLocation" type="hidden" value="${Location}" />
            </td>
           
            <td class="project-title clsEmpNo">${EmpNo}</td>
            <td class="project-title clsNameOfEmployees">${NameOfEmployees}</td>
            <td class="project-title clsFatherName">${FatherName}</td>
            <td class="project-title clsDesignation">${Designation}</td>
            <td class="project-title clsGender">${Gender}</td>
            <td class="project-title clsMaritalStatus">${MaritalStatus}</td>
            <td class="project-title clsReligion">${Religion}</td>
            <td class="project-title clsLocation">${Location}</td>
            <td class="project-title clsDeptCode">${DeptCode}</td>
            <td class="project-title clsDepartmnent">${Departmnent}</td>
            <td class="project-title clsCostCode">${CostCode}</td>
            <td class="project-title clsCostCenter">${CostCenter}</td>
            <td class="project-title clsUnit">${Unit}</td>
            <td class="project-title clsSapCostCenterCode">${SapCostCenterCode}</td>
            <td class="project-title clsSapCostCenter">${SapCostCenter}</td>
            <td class="project-title clsAge">${Age}</td>
            <td class="project-title clsService">${Service}</td>
            <td class="project-title clsAddress">${Address}</td>
            <td class="project-title clsBloodGroup">${BloodGroup}</td>
            <td class="project-title clsPhone">${Phone}</td>
            <td class="project-title clsOfficeEmailAddress">${OfficeEmailAddress}</td>
            <td class="project-title clsNTN">${NTN}</td>
            <td class="project-title clsEOBINumber">${EOBINumber}</td>
            <td class="project-title clsEOBIDate">${EOBIDate}</td>
            <td class="project-title clsSESANumber">${SESANumber}</td>
            <td class="project-title clsDateOfJoining">${DateOfJoining}</td>
            <td class="project-title clsDateOfConfirmation">${DateOfConfirmation}</td>
            <td class="project-title clsDateOfBirth">${DateOfBirth}</td>
            <td class="project-title clsCNIC">${CNIC}</td>
            <td class="project-title clsBank">${Bank}</td>
            <td class="project-title clsBranch">${Branch}</td>
            <td class="project-title clsAccountNumber">${AccountNumber}</td>
            <td class="project-title clsNap">${Nap}</td>
            <td class="project-title clsMake">${Make}</td>
            <td class="project-title clsRegistrationNumber">${RegistrationNumber}</td>
            <td class="project-title clsCompanyName">${CompanyName}</td>


            <td class="project-title clsBasic">${Basic}</td>
            <td class="project-title clsHouseRent">${HouseRent}</td>
            <td class="project-title clsCola">${Cola}</td>
            <td class="project-title clsMedicalAllowance">${MedicalAllowance}</td>
            <td class="project-title clsConveyanceAllowance">${ConveyanceAllowance}</td>
            <td class="project-title clsPESA">${PESA}</td>
            <td class="project-title clsSpecialAllowance">${SpecialAllowance}</td>
            <td class="project-title clsSpecialPay">${SpecialPay}</td>
            <%--<td class="project-title clsAllowances">${Allowances}</td>
            <td class="project-title clsGross">${Gross}</td>--%>
            <td class="project-title clsBonus">${Bonus}</td>
        <%--    <td class="project-title clsPF">${PF}</td>
            <td class="project-title clsEOBI">${EOBI}</td>--%>
            <td class="project-title clsSESSI">${SESSI}</td>
            <td class="project-title clsCarAllowance">${CarAllowance}</td>
            <td class="project-title clsCellAllowance">${CellAllowance}</td>
            <td class="project-title clsHardship">${Hardship}</td>
            <td class="project-title clsFuel">${Fuel}</td>
            <td class="project-title clsRepairMaintenance">${RepairMaintenance}</td>
            <td class="project-title clsFuelInLitres">${FuelInLitres}</td>
          
           
        </tr>
    </script>

    <script src="../../js/Page_JS/Report_EMP_List_CPBM.js"></script>
</asp:Content>

