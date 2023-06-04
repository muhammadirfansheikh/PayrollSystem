<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="Report_HR_HOD_List.aspx.cs" Inherits="Pages_HCM_Report_HR_HOD_List" %>


<%@ Register Src="~/Controls/HCM/EmployeeSearchFilters.ascx" TagPrefix="uc" TagName="EmployeeSearchFilters" %>
<%@ Register Src="~/Controls/HCM/ReportHeader.ascx" TagPrefix="uc" TagName="ReportHeader" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
      <script src="/js/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
       <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>
                <asp:Label runat="server" ID="lbl1" Text="HR HOD List" />
            </h2>
            <ol class="breadcrumb">
          
                <li>
                    <a href="#">Other Reports</a>
                </li>
                <li class="active">
                    <strong>
                        <asp:Label runat="server" ID="lbl2" Text="HR HOD List" />
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
            <div class="col-lg-2 divMonthPayroll">
                <label>Month</label>
                <input type="text" class="form-control txtMonthOfPayroll DatePickerMonthComplete" />
            </div>

             <div class="col-lg-2 ">
                <label>Select </label>
                <select class="form-control  ddlGroupBy"></select>
            </div>
            <div class="col-lg-4 div_reportbutton" style="margin-top: 23px; display: none;">
                <input type="button" class="btn btn-info pull-right" value="Export" onclick="excelExportWithFileSaver('tableHRHODListing','Report_HR_HOD_List')" />
                <input type="button" class="btn btn-primary pull-right m-r-sm" value="Print" onclick="sendPrint('.tableHRHODListing')" />
            </div>
        </div>
    </div>

    <div class="panel panel-info">
        <div class="panel-heading">
            HR HOD List
        </div>
        <div class="panel-body" style="overflow-x: scroll">
            <table class="table table-hover tableHRHODListing" id="tableHRHODListing">
                <thead class="theadHRHODlisting">
                    <tr>
                        <uc:ReportHeader runat="server" ID="ReportHeader2" />
                    </tr>
                    <tr class="info">
                        <th>Sr No</th>
                        <th>Employee No</th>
                        <th>Employee Name</th>
                        <th>Designation</th>
                        <th>Category</th>

                        <th>Grade</th>


                        <th>Nap</th>
						 <th>Function</th>
					<th>Sub Function</th>
                        <th>Department</th>
                        <th>Gender</th>
                        <th>Age</th>
                        <th>Service</th>
                        <th>Qualification 1</th>
                        <th>Qualification 2</th>

                        <th>Province</th>
                        <th>Location</th>


                        <th>Sap Cost Center Code</th>
                        <th>Sap Cost Center</th>

                        <th>Mobile</th>

                        <th>Group Code</th>


                        <th>Date Of Joining</th>
                        <th>Date Of Confirmation</th>
                        <th>Date Of Birth</th>
                        <th>Make</th>
                        <th>Registartion Number</th>



                        <th>Basic</th>
                        <th>Allownces</th>
                        <th>Gross</th>
                        <th>Bonus</th>
                        <th>PF</th>
                        <th>EOBI</th>
                        <th>Sessi</th>
                        
                        <th>Cell Allownce</th>
                        <th>Fuel</th>
                        <th>Repair Maintainance</th>
                        <th>Car Allownce</th>
                        <th>Hardship</th>
                        <th>Outstanding Allownce</th>
                        <th>Fuel In Litres</th>
                   
                        

                    </tr>
                </thead>
                <tbody class="tbodyHRHODListing">
                </tbody>
                <tfoot>
                    <tr class="info">
                        <td colspan="26" style="font-weight:bold">Total</td>
                        <th class="tdTotalBasic" style="text-align:right"></th>
                         <th class="tdTotalAllownces" style="text-align:right"></th>
                        <th class="tdTotalGross" style="text-align:right"></th>
                        <th class="tdTotalBonus" style="text-align:right"></th>
                        <th class="tdTotalPF" style="text-align:right"></th>
                        <th class="tdTotalEOBI" style="text-align:right"></th>
                        <th class="tdTotalSessi" style="text-align:right"></th>
                        <th class="tdTotalCellAllownces" style="text-align:right"></th>
                        <th class="tdTotalFuel" style="text-align:right"></th>
                        <th class="tdTotalRepairMaintainance" style="text-align:right"></th>
                       
                        <th class="tdTotalCarAllownces" style="text-align:right"></th>
                        <th class="tdTotalHardShip" style="text-align:right"></th>
                        <th class="tdDisLocationAllowance" style="text-align:right"></th>
                        <th class="tdFuelInLitres" style="text-align:right"></th>
                        
                    </tr>
                </tfoot>
            </table>
        </div>
        <br />
    </div>

    <script type="text/x-jQuery-tmpl" id="HRHODListing">
        <tr class="trList">
            <td class="project-title">
                 

            </td>
            <td class="project-title clsEmpNo ABC">${EmpNo}
                <input class="clsSapCostCenter" type="hidden" value="${clsSapCostCenter}" />
                <input class="clsCostCenter" type="hidden" value="${clsCostCenter}" />
                <input class="clsLocation" type="hidden" value="${clsLocation}" />
                <input class="clsDepartment" type="hidden" value="${clsDepartment}" />
            </td>
            <td class="project-title">${NameOfEmployees}</td>
            <td class="project-title">${clsDesignation}</td>
            <td class="project-title clsPaymentType">${Category}</td>
            <td class="project-title clsGrade">${Grade}</td>
            <td class="project-title clsNap">${Nap}</td>
			<td class="project-title clsFunction">${Function}</td>
			<td class="project-title clsSubFunction">${SubFunction}</td>
            <td class="project-title clsDepartmnent">${clsDepartment}</td>
            <td class="project-title clsGender">${Gender}</td>
            <td class="project-title clsAge">${Age}</td>
            <td class="project-title clsService">${Service}</td>
            <td class="project-title clsQualification1">${Qualification1}</td>
            <td class="project-title clsQualification2">${Qualification2}</td>
            <td class="project-title clsProvince">${Province}</td>
            <td class="project-title clsLocation">${clsLocation}</td>

            <td class="project-title clsSapCostCenterCode">${SapCostCenterCode}</td>
            <td class="project-title clsSapCostCenter">${clsSapCostCenter}</td>
            <td class="project-title clsMobile">${Mobile}</td>
            <td class="project-title clsGroupCode">${GroupCode}</td>
            <td class="project-title clsDateOfJoining">${DateOfJoining}</td>
            <td class="project-title clsDateOfConfirmation">${DateOfConfirmation}</td>
            <td class="project-title clsDateOfBirth">${DateOfBirth}</td>
            <td class="project-title clsMake">${Make}</td>
            <td class="project-title clsRegistrationNumber">${RegistrationNumber}</td>


            <td class="project-title clsBasic" style="text-align:right">${Basic}</td>
            <td class="project-title clsAllowances" style="text-align:right">${Allowances}</td>
            <td class="project-title clsGross" style="text-align:right">${Gross}</td>
            <td class="project-title clsBonus" style="text-align:right">${Bonus}</td>
            <td class="project-title clsPF" style="text-align:right">${PF}</td>
            <td class="project-title clsEOBI" style="text-align:right">${EOBI}</td>
            <td class="project-title clsSESSI" style="text-align:right">${SESSI}</td>
            <td class="project-title clsCellAllowance" style="text-align:right">${CellAllowance}</td>
            <td class="project-title clsFuel" style="text-align:right">${Fuel}</td>
            <td class="project-title clsRepairMaintenance" style="text-align:right">${RepairMaintenance}</td>
            <td class="project-title clsCarAllowance" style="text-align:right">${CarAllowance}</td>
            <td class="project-title clsHardship" style="text-align:right">${Hardship}</td>
             <td class="project-title clsDisLocationAllowance" style="text-align:right">${DisLocationAllowance}</td>
            <td class="project-title clsFuelInLitres" style="text-align:right">${FuelInLitres}</td>
			
           
            
         
        </tr>
    </script>
    <script src="../../js/Page_JS/Rpt_HR_HOD_List.js"></script>
</asp:Content>

