using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Data;
using System.Transactions;
using Newtonsoft.Json;
using System.Web;
using System.IO;

public partial class Pages_HCM_Setup_CreateEmployee : Base
{
    Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();
    Base baseclass = new Base();
    int? IntNull = null;

    string url = "/Pages/HCM/Setup/CreateEmployeeHcm.aspx";

    public int EmployeeId
    {
        get
        {
            if (string.IsNullOrEmpty(hfEmployeeId.Value))
                return 0;
            else
                return int.Parse(hfEmployeeId.Value);
        }
    }
    public void SetEmployeeId()
    {
        hfEmployeeId.Value = "0";
        if (!string.IsNullOrEmpty(Request.Url.Query.TrimStart('?')))
        {
            try
            {
                string EncryptedQueryString = Request.Url.Query.TrimStart('?');
                string Id = CommonHelper.DecryptById(EncryptedQueryString, "EmployeeId");
                if (!string.IsNullOrEmpty(Id) && Id != "0")
                {
                    hfEmployeeId.Value = Convert.ToString(Id);
                }
                else
                {
                    ErrorRedirect("Record not found", url);
                }
            }
            catch (Exception ex)
            {
                ErrorRedirect("Record not found", url);
            }
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                SetEmployeeId();
                BindDropDownofAdd();
                if (EmployeeId > 0)
                {
                    BindData();
                }


            }
        }
        catch { }
    }
    public void BindDropDownofAdd()
    {
        var li_Religion = context.HRMS_Setup_Religion.Where(a => a.IsActive == true).OrderBy(b => b.ReligionTitle).ToList();
        CommonHelper.BindDropDown(ddlReligion, li_Religion, "ReligionTitle", "ReligionId", true, false);

        var ListGrade = context.HRMS_Setup_Grade.Where(c => c.IsActive == true).OrderBy(c => c.Grade).ToList();
        CommonHelper.BindDropDown(ddlGrade, ListGrade, "Grade", "GradeId", true, false);

        var ListGroupInsuranceId = context.HRMS_Setup_GroupInsurance.Where(c => c.IsActive == true).OrderBy(c => c.GroupInsurance).ToList();
        CommonHelper.BindDropDown(ddlGroupInsurance, ListGroupInsuranceId, "GroupInsurance", "GroupInsuranceId", true, false);

        var ListEmpFunc = context.HRMS_Setup_EmployeeFunction.Where(c => c.IsActive == true).OrderBy(c => c.FunctionName).ToList();
        CommonHelper.BindDropDown(ddlEmpFunc, ListEmpFunc, "FunctionName", "EmployeeFunctionId", true, false);
        ddlEmpFunc_SelectedIndexChanged(null, null);

        var ListBloodGroup = context.HRMS_Setup_BloodGroup.Where(c => c.IsActive == true).OrderBy(c => c.BloodGroup).ToList();
        CommonHelper.BindDropDown(ddlBloodGroup, ListBloodGroup, "BloodGroup", "BloodGroupId", true, false);

        //var ListEmpType = context.Setup_EmployeeType.Where(c => c.IsActive == true).OrderBy(c => c.TypeName).OrderBy(b => b.TypeName).ToList();
        //CommonHelper.BindDropDown(ddlEmptype, ListEmpType, "TypeName", "EmployeeTypeId", true, false);


        var ListGender = context.HRMS_Setup_Gender.Where(c => c.IsActive == true).OrderBy(c => c.GenderTitle).ToList();
        CommonHelper.BindDropDown(ddlGenderAdd, ListGender, "GenderTitle", "GenderId", true, false);

        var ListBank = context.HRMS_Setup_BankMaster.Where(c => c.IsActive == true).OrderBy(c => c.BankName).ToList();
        CommonHelper.BindDropDown(ddlBankMaster, ListBank, "BankName", "BankMasterId", true, false);
        ddlBankMaster_SelectedIndexChanged(null, null);

        var ListPaymentMode = context.HRMS_Setup_PayMode.Where(c => c.IsActive == true).OrderBy(c => c.PayMode).ToList();
        CommonHelper.BindDropDown(ddlPaymentMode, ListPaymentMode, "PayMode", "PayModeId", true, false);

        var ListAccountType = context.HRMS_Setup_AccountType.Where(c => c.IsActive == true).OrderBy(c => c.AccountType).ToList();
        CommonHelper.BindDropDown(ddlAccountType, ListAccountType, "AccountType", "AccountTypeId", true, false);


        var ListMaritalStatus = context.HRMS_Setup_MartialStatus.Where(c => c.IsActive == true).OrderBy(c => c.MartialStatusTitle).ToList();
        CommonHelper.BindDropDown(ddlMaritalStatus, ListMaritalStatus, "MartialStatusTitle", "MartialStatusId", true, false);


        var ListEducationDegree = context.HRMS_Setup_EducationType.Where(c => c.IsActive == true).OrderBy(c => c.educationType).ToList();
        CommonHelper.BindDropDown(cmbEducationDegree, ListEducationDegree, "educationType", "educationTypeId", true, false);

        var ListCountry = context.Setup_Country.Where(c => c.IsActive == true).OrderBy(c => c.CountryName).ToList();
        CommonHelper.BindDropDown(ddlCountry, ListCountry, "CountryName", "CountryId", false, false);
        ddlCountry_SelectedIndexChanged(null, null);


        var LstGroup = context.Setup_Group.Where(x => x.IsActive == true).OrderBy(x => x.GroupName).Select(s => new
        {
            GroupName = s.GroupName,
            GroupId = s.GroupId
        }).OrderBy(b => b.GroupName).ToList();

        CommonHelper.BindDropDown(ddlGroupAdd, LstGroup, "GroupName", "GroupId", LstGroup.Count == 1 ? false : true, false);

        ddlGroupAdd.Enabled = false;
        ddlGroupAdd_SelectedIndexChanged(null, null);

    }
    protected void ddlGroupAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        int GroupId = Convert.ToInt32(ddlGroupAdd.SelectedValue);
        var LstCompany = context.Setup_EmployeeCompanyMapping.Where(a => a.IsActive == true && a.EmployeeId == UserKey && a.Setup_Company.IsActive == true && a.Setup_Company.GroupId == GroupId)
            .Select(a => new
            {
                CompanyName = a.Setup_Company.CompanyName,
                CompanyId = a.Setup_Company.CompanyId
            })
            .OrderBy(b => b.CompanyName).ToList();
        CommonHelper.BindDropDown(ddlCompanyAdd, LstCompany, "CompanyName", "CompanyId", true, false);
        ddlCompanyAdd_SelectedIndexChanged(null, null);
    }
    protected void ddlCompanyAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        int CompanyId_ = Convert.ToInt32(ddlCompanyAdd.SelectedValue);

        //var LstBusinessUnitCostCenterMapping = context.HCM_SETUP_SapCostCenter.Where(a => a.IsActitve == true && a.CompanyId == CompanyId_).OrderBy(b => b.SapCostCenter).ToList();
        //CommonHelper.BindDropDown(ddlSapCostCenter, LstBusinessUnitCostCenterMapping, "SapCostCenter", "SapCostId", true, false);

        var LstBusinessUnitCostCenter = context.HCM_SETUP_SapCostCenter.Where(a => a.IsActitve == true && a.CompanyId == CompanyId_).OrderBy(b => b.SapCostCenter).ToList();
        var LstBusinessUnitCostCenterMapping = LstBusinessUnitCostCenter.Select(s => new
        {
            SapCostCenter = (s.SapCostCenterCode + " - " + s.SapCostCenter).ToString(),
            SapCostId = s.SapCostId
        }).OrderBy(b => b.SapCostId).ToList();
        CommonHelper.BindDropDown(ddlSapCostCenter, LstBusinessUnitCostCenterMapping, "SapCostCenter", "SapCostId", true, false);


        var LstLocation = context.Setup_Location.Where(a => a.IsActive == true && a.CompanyId == CompanyId_).OrderBy(b => b.LocationName).ToList();
        CommonHelper.BindDropDown(ddlLocationAdd, LstLocation, "LocationName", "LocationId", LstLocation.Count == 1 ? false : true, false);

        var ListEmpType = context.Setup_EmployeeType.Where(c => c.IsActive == true && c.CompanyId == CompanyId_).OrderBy(c => c.TypeName).OrderBy(b => b.TypeName).ToList();
        CommonHelper.BindDropDown(ddlEmptype, ListEmpType, "TypeName", "EmployeeTypeId", true, false);

        var LstCostCenter = context.Setup_CostCenter.Where(a => a.IsActive == true && a.CompanyId == CompanyId_).OrderBy(b => b.CostCenterName).ToList();
        CommonHelper.BindDropDown(ddlCostCenterAdd, LstCostCenter, "CostCenterName", "CostCenterId", LstCostCenter.Count == 1 ? false : true, false);
        var listBusinessUnit = context.TS_Setup_BusinessUnit.Where(c => c.IsActive == true && c.CompanyId == CompanyId_).OrderBy(c => c.BusinessUnitName).ToList();
        CommonHelper.BindDropDown(ddlBusinessUnitAdd, listBusinessUnit, "BusinessUnitName", "BusinessUnitID", listBusinessUnit.Count == 1 ? false : true, false);

        var listcat = context.Setup_Category.Where(c => c.IsActive == true && c.CompanyId == CompanyId_).OrderBy(c => c.CategoryName).ToList();
        CommonHelper.BindDropDown(ddlJobCategoryAdd, listcat, "CategoryName", "CategoryId", listcat.Count == 1 ? false : true, false);

        var listCompanyCode = context.HCM_Setup_Detail.Where(c => c.IsActive == true && c.CompanyID == CompanyId_ && c.SetupMasterID == 43).OrderBy(c => c.ColumnValue).ToList();
        CommonHelper.BindDropDown(ddlCompanyCode, listCompanyCode, "ColumnValue", "SetupDetailID", listcat.Count == 1 ? false : true, false);

        ddlBusinessUnitAdd_SelectedIndexChanged(null, null);
        ddlJobCategoryAdd_SelectedIndexChanged(null, null);


        var listReportCostCenter = context.HRMS_Setup_ReportingCostCenter.Where(c => c.IsActive == true && c.CompanyId == CompanyId_).OrderBy(c => c.ReportingCostCenter).ToList();
        CommonHelper.BindDropDown(ddlReportingCostCenter, listReportCostCenter, "ReportingCostCenter", "ReportingCostCenterId", listcat.Count == 1 ? false : true, false);
    }
    protected void ddlBusinessUnitAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        int CompanyId_ = Convert.ToInt32(ddlCompanyAdd.SelectedValue);
        int BUID = ddlBusinessUnitAdd.SelectedValue == "" ? 0 : Convert.ToInt32(ddlBusinessUnitAdd.SelectedValue);
        var ListDepartment = context.Setup_Department.Where(c => c.IsActive == true && c.BusinessUnitId == BUID && c.TS_Setup_BusinessUnit.CompanyId == CompanyId_).OrderBy(c => c.DepartmentName).ToList();
        CommonHelper.BindDropDown(ddlDepartmentAdd, ListDepartment, "DepartmentName", "DepartmentId", ListDepartment.Count == 1 ? false : true, false);
    }
    protected void ddlJobCategoryAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        int CompanyId_ = Convert.ToInt32(ddlCompanyAdd.SelectedValue);
        int JobCategoryId_ = ddlJobCategoryAdd.SelectedValue == "" ? 0 : Convert.ToInt32(ddlJobCategoryAdd.SelectedValue);
        var ListDesignation = context.Setup_Designation.Where(c => c.IsActive == true && c.CategoryId == JobCategoryId_ && c.Setup_Category.CompanyId == CompanyId_).OrderBy(c => c.DesignationName).ToList();
        CommonHelper.BindDropDown(ddlDesignationAdd, ListDesignation, "DesignationName", "DesignationId", ListDesignation.Count == 1 ? false : true, false);

    }
    protected void ddlBankMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        int BankMasterId = ddlBankMaster.SelectedValue == "" ? 0 : Convert.ToInt32(ddlBankMaster.SelectedValue);
        var ListBank = context.HRMS_Setup_Bank.Where(c => c.IsActive == true && c.BankMasterId == BankMasterId)
            .Select(a => new
            {
                Id = a.BankId,
                Value = a.BankDescription + " - " + a.BranchCode
            })
            .OrderBy(c => c.Value).ToList();
        CommonHelper.BindDropDown(ddlBank, ListBank, "Value", "Id", true, false);
    }
    protected void ddlEmpFunc_SelectedIndexChanged(object sender, EventArgs e)
    {
        int FuncId = ddlEmpFunc.SelectedValue == "" ? 0 : Convert.ToInt32(ddlEmpFunc.SelectedValue);
        var ListEmpSubFunc = context.HRMS_Setup_EmployeeSubFunction.Where(c => c.IsActive == true && c.EmployeeFunctionId == FuncId).OrderBy(c => c.SubFunction).ToList();
        CommonHelper.BindDropDown(ddlEmployeeSubFunction, ListEmpSubFunc, "SubFunction", "EmployeeSubFunctionId", true, false);
    }
    private void BindData()
    {
        var li = context.Setup_Employee.FirstOrDefault(a => a.EmployeeId == EmployeeId);
        if (li != null)
        {
            ddlCompanyAdd.SelectedValue = Convert.ToString(li.CompanyId);
            ddlCompanyAdd_SelectedIndexChanged(null, null);
            txtEmployeeNo.Text = li.EmployeeCode;
            txtDOJadd.Text = li.JoiningDate == null ? "" : Convert.ToDateTime(Convert.ToString(li.JoiningDate).Trim()).ToString(Constant.DateFormat3);
            ddlEmptype.SelectedValue = li.EmployeeTypeId == null ? "0" : Convert.ToString(li.EmployeeTypeId);
            ddlLocationAdd.SelectedValue = Convert.ToString(li.LocationId);
            ddlBusinessUnitAdd.SelectedValue = li.DepartmentId > 0 ? Convert.ToString(li.Setup_Department.BusinessUnitId) : "0";
            ddlReportingCostCenter.SelectedValue = li.ReportingCostCenterId != null ? Convert.ToString(li.ReportingCostCenterId) : "0";
            ddlBusinessUnitAdd_SelectedIndexChanged(null, null);
            ddlDepartmentAdd.SelectedValue = li.DepartmentId == null ? "0" : Convert.ToString(li.DepartmentId);
            ddlJobCategoryAdd.SelectedValue = li.DesignationId > 0 ? Convert.ToString(li.Setup_Designation.CategoryId) : "0";
            ddlJobCategoryAdd_SelectedIndexChanged(null, null);
            ddlDesignationAdd.SelectedValue = li.DesignationId == null ? "0" : Convert.ToString(li.DesignationId);
            ddlCompanyCode.SelectedValue = li.CompanyCodeId == null ? "0" : Convert.ToString(li.CompanyCodeId);
            ddlCostCenterAdd.SelectedValue = Convert.ToString(li.CostCenterId);
            ddlGroupInsurance.SelectedValue = Convert.ToString(li.GroupInsuranceId == null ? 0 : li.GroupInsuranceId);
            ddlEmpFunc.SelectedValue = li.EmployeeSubFunctionId == null ? "0" : Convert.ToString(li.HRMS_Setup_EmployeeSubFunction.EmployeeFunctionId);
            ddlEmpFunc_SelectedIndexChanged(null, null);
            ddlEmployeeSubFunction.SelectedValue = li.EmployeeSubFunctionId == null ? "0" : Convert.ToString(li.EmployeeSubFunctionId);
            chkAttendanceAllowanceStatus.Checked = li.AttendanceAllowanceStatus == null ? false : Convert.ToBoolean(li.AttendanceAllowanceStatus);
            chkEobiStatus.Checked = li.EOBIStatus == null ? false : Convert.ToBoolean(li.EOBIStatus);
            chkAdvanceTaxDeduction.Checked = Convert.ToBoolean(li.IsAdvanceTaxDeduction);
            txtEobiNo.Text = li.EOBINumber;
            txtEobiDate.Text = li.EOBIDate == null ? "" : Convert.ToDateTime(Convert.ToString(li.EOBIDate).Trim()).ToString(Constant.DateFormat3);
            txtSesaNo.Text = li.SESANumber;
            chkAllowIntrest.Checked = li.IsAllowInterest;
            ddlSapCostCenter.SelectedValue = Convert.ToString(li.SapCostCenterId == null ? 0 : li.SapCostCenterId);
            txtConfirmationdate.Text = li.ConfirmationDate == null ? "" : Convert.ToDateTime(Convert.ToString(li.ConfirmationDate).Trim()).ToString(Constant.DateFormat3);
            txtContractstartdate.Text = li.ContractStartDate == null ? "" : Convert.ToDateTime(Convert.ToString(li.ContractStartDate).Trim()).ToString(Constant.DateFormat3);
            txtContractenddate.Text = li.ContractEndDate == null ? "" : Convert.ToDateTime(Convert.ToString(li.ContractEndDate).Trim()).ToString(Constant.DateFormat3);

            txtcnicaddNew.Text = li.CNIC == null ? "" : Convert.ToString(li.CNIC).Trim();
            txtFirstNameNew.Text = li.FirstName == null ? "" : Convert.ToString(li.FirstName).Trim();
            txtMiddleNameNew.Text = li.MiddleName == null ? "" : Convert.ToString(li.MiddleName).Trim();
            txtLastNameNew.Text = li.LastName == null ? "" : Convert.ToString(li.LastName).Trim();
            txtDOB.Text = li.DateOfBirth == null ? "" : Convert.ToDateTime(Convert.ToString(li.DateOfBirth).Trim()).ToString(Constant.DateFormat3);
            ddlGenderAdd.SelectedValue = li.GenderId == null ? "0" : Convert.ToString(li.GenderId);
            ddlMaritalStatus.SelectedValue = li.MatrialStatusId == null ? "0" : Convert.ToString(li.MatrialStatusId);
            ddlCountry.SelectedValue = li.CountryId == null ? "0" : Convert.ToString(li.CountryId);
            ddlCountry_SelectedIndexChanged(null, null);
            ddlProvince.SelectedValue = li.ProvinceId == null ? "0" : Convert.ToString(li.ProvinceId);
            ddlProvince_SelectedIndexChanged(null, null);
            ddlCity.SelectedValue = li.CityId == null ? "0" : Convert.ToString(li.CityId);
            txtPhoneNo.Text = li.Phone;
            txtIbanNo.Text = li.NTN;
            txtCellNo.Text = li.Cell;
            txtOfficialEmail.Text = li.OfficeEmailAddress;
            txtPersonalEmail.Text = li.PersonalEmailAddress;
            ddlBloodGroup.SelectedValue = li.BloodGroupId == null ? "0" : Convert.ToString(li.BloodGroupId);
            txtFatherName.Text = li.FatherName == null ? "" : Convert.ToString(li.FatherName).Trim();
            ddlReligion.SelectedValue = li.ReligionId == null ? "0" : Convert.ToString(li.ReligionId);
            ddlGrade.SelectedValue = Convert.ToString(li.GradeId == null ? 0 : li.GradeId);
            txtPermanentaddress.Text = li.Address;
            txtIbanNo.Text = li.IBANNo;
            txtTaxNTNNo.Text = li.NTN;
            //ddlBankMaster.SelectedValue = li.HRMS_EmployeeBank.FirstOrDefault(a => a.IsActive == true) == null ? "0" : Convert.ToString(li.HRMS_EmployeeBank.FirstOrDefault(a => a.IsActive == true).HRMS_Setup_Bank.BankMasterId);
            //ddlBankMaster_SelectedIndexChanged(null, null);
            //ddlBank.SelectedValue = li.HRMS_EmployeeBank.FirstOrDefault(a => a.IsActive == true) == null ? "0" : Convert.ToString(li.HRMS_EmployeeBank.FirstOrDefault(a => a.IsActive == true).HRMS_Setup_Bank.BankId);
            //txtAccountno.Text = li.HRMS_EmployeeBank.FirstOrDefault(a => a.IsActive == true) == null ? "0" : li.HRMS_EmployeeBank.FirstOrDefault(a => a.IsActive == true).AccountNumber;
            //ddlPaymentMode.SelectedValue = li.HRMS_EmployeeBank.FirstOrDefault(a => a.IsActive == true) == null ? "0" : Convert.ToString(li.HRMS_EmployeeBank.FirstOrDefault(a => a.IsActive == true).PayModeId);
            //ddlAccountType.SelectedValue = li.HRMS_EmployeeBank.FirstOrDefault(a => a.IsActive == true) == null ? "0" : Convert.ToString(li.HRMS_EmployeeBank.FirstOrDefault(a => a.IsActive == true).AccountTypeId);

            var _data = li.HRMS_EmployeeBank.Where(a => a.IsActive == true).FirstOrDefault();
            if (_data != null)
            {
                if (_data.HRMS_Setup_Bank != null)
                {
                    ddlBankMaster.SelectedValue = _data.HRMS_Setup_Bank.BankMasterId.ToString();
                    ddlBankMaster_SelectedIndexChanged(null, null);
                    ddlBank.SelectedValue = _data.HRMS_Setup_Bank.BankId.ToString();



                }
                txtAccountno.Text = _data.AccountNumber;
                ddlPaymentMode.SelectedValue = _data.PayModeId.ToString();
                ddlAccountType.SelectedValue = _data.AccountTypeId.ToString();
            }


            BindEducationDetailGrid();
        }
        else
        {
            Response.Redirect("/Pages/HCM/Setup/CreateEmployeeHcm.aspx");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
            divError.Visible = false;
            if (ValiationFunction() == true)
            {
                bool IsSave = true;
                Setup_Employee emp = new Setup_Employee();
                if (EmployeeId > 0)
                {
                    emp = context.Setup_Employee.FirstOrDefault(a => a.EmployeeId == EmployeeId);
                    if (emp == null)
                    {
                        ErrorRedirect("Employee not found to edit.", url);
                        IsSave = false;
                    }
                }
                if (IsSave == true)
                {
                    DateTime? DateNull = null;
                    bool IsScope_Complete = false;
                    int NewEmployeeID = 0;
                    DateTime dt = DateTime.Now;
                    int? IntNull = null;
                    using (TransactionScope scope = new TransactionScope())
                    { 
                        emp.CompanyId = Convert.ToInt32(ddlCompanyAdd.SelectedValue);
                        emp.EmployeeCode = txtEmployeeNo.Text.Trim();
                        emp.JoiningDate = Convert.ToDateTime(txtDOJadd.Text.Trim());
                        emp.EmployeeTypeId = ddlEmptype.SelectedValue == "0" ? IntNull : Convert.ToInt32(ddlEmptype.SelectedValue);
                        emp.LocationId = Convert.ToInt32(ddlLocationAdd.SelectedValue);
                        emp.DepartmentId = Convert.ToInt32(ddlDepartmentAdd.SelectedValue);
                        emp.DesignationId = Convert.ToInt32(ddlDesignationAdd.SelectedValue);
                        emp.CostCenterId = Convert.ToInt32(ddlCostCenterAdd.SelectedValue);
                        emp.GroupInsuranceId = ddlGroupInsurance.SelectedValue == "0" ? IntNull : Convert.ToInt32(ddlGroupInsurance.SelectedValue);
                        emp.GroupLifeInsuranceId = ddlGroupInsurance.SelectedValue == "0" ? IntNull : Convert.ToInt32(ddlGroupInsurance.SelectedValue);
                        emp.GroupPersonalAccidentInsuranceId = ddlGroupInsurance.SelectedValue == "0" ? IntNull : Convert.ToInt32(ddlGroupInsurance.SelectedValue);
                        emp.CompanyCodeId = ddlCompanyCode.SelectedValue == "0" ? IntNull : Convert.ToInt32(ddlCompanyCode.SelectedValue);
                        emp.EmployeeSubFunctionId = Convert.ToInt32(ddlEmployeeSubFunction.SelectedValue) == 0 ? IntNull : Convert.ToInt32(ddlEmployeeSubFunction.SelectedValue);
                        emp.AttendanceAllowanceStatus = chkAttendanceAllowanceStatus.Checked;
                        emp.EOBIStatus = chkEobiStatus.Checked;
                        emp.IsAdvanceTaxDeduction = chkAdvanceTaxDeduction.Checked;
                        emp.EOBINumber = txtEobiNo.Text == string.Empty ? "" : txtEobiNo.Text;
                        emp.ReportingCostCenterId = Convert.ToInt32(ddlReportingCostCenter.SelectedValue);
                        emp.EOBIDate = txtEobiDate.Text == string.Empty ? DateNull : Convert.ToDateTime(txtEobiDate.Text);
                        emp.SESANumber = txtSesaNo.Text == string.Empty ? "" : txtSesaNo.Text;
                        emp.IsAllowInterest = chkAllowIntrest.Checked;
                        emp.SapCostCenterId = Convert.ToInt32(ddlSapCostCenter.SelectedValue) == 0 ? IntNull : Convert.ToInt32(ddlSapCostCenter.SelectedValue);
                        emp.ConfirmationDate = txtConfirmationdate.Text == string.Empty ? DateNull : Convert.ToDateTime(txtConfirmationdate.Text);
                        emp.ContractStartDate = txtContractstartdate.Text == string.Empty ? DateNull : Convert.ToDateTime(txtContractstartdate.Text);
                        emp.ContractEndDate = txtContractenddate.Text == string.Empty ? DateNull : Convert.ToDateTime(txtContractenddate.Text);

                        emp.CNIC = txtcnicaddNew.Text.Trim();
                        emp.FirstName = txtFirstNameNew.Text.Trim();
                        emp.MiddleName = txtMiddleNameNew.Text.Trim();
                        emp.LastName = txtLastNameNew.Text.Trim();
                        emp.DateOfBirth = txtDOB.Text == string.Empty ? DateNull : Convert.ToDateTime(txtDOB.Text);
                        emp.GenderId = ddlGenderAdd.SelectedValue == "0" ? IntNull : Convert.ToInt32(ddlGenderAdd.SelectedValue);
                        emp.MatrialStatusId = ddlMaritalStatus.SelectedValue == "0" ? IntNull : Convert.ToInt32(ddlMaritalStatus.SelectedValue);
                        emp.CountryId = ddlCountry.SelectedValue == "0" ? IntNull : Convert.ToInt32(ddlCountry.SelectedValue);
                        emp.CityId = ddlCity.SelectedValue == "0" ? IntNull : Convert.ToInt32(ddlCity.SelectedValue);
                        emp.ProvinceId = ddlProvince.SelectedValue == "0" ? IntNull : Convert.ToInt32(ddlProvince.SelectedValue);
                        emp.Phone = txtPhoneNo.Text;
                        emp.Cell = txtCellNo.Text;
                        emp.OfficeEmailAddress = txtOfficialEmail.Text;
                        emp.PersonalEmailAddress = txtPersonalEmail.Text;
                        emp.BloodGroupId = ddlBloodGroup.SelectedValue == "0" ? IntNull : Convert.ToInt32(ddlBloodGroup.SelectedValue);
                        emp.FatherName = txtFatherName.Text.Trim().ToUpper();
                        emp.ReligionId = ddlReligion.SelectedValue == "0" ? IntNull : Convert.ToInt32(ddlReligion.SelectedValue);
                        emp.GradeId = Convert.ToInt32(ddlGrade.SelectedValue) == 0 ? IntNull : Convert.ToInt32(ddlGrade.SelectedValue);
                        emp.Address = txtPermanentaddress.Text.Trim();
                        emp.UserIP = baseclass.UserIP;
                        emp.NTN = txtTaxNTNNo.Text;
                        emp.IBANNo = txtIbanNo.Text;
                        if (EmployeeId > 0)
                        { 
                            emp.ModifiedBy = baseclass.UserKey;
                            emp.ModifiedDate = dt;
                        }
                        else
                        {
                            emp.CreatedBy = baseclass.UserKey;
                            emp.CreatedDate = dt;
                            emp.IsActive = true;
                            context.Setup_Employee.Add(emp);
                        }
                        context.SaveChanges();
                        if (emp.EmployeeId > 0)
                        {
                            NewEmployeeID = emp.EmployeeId;
                            if (EmployeeId == 0)
                            {
                                Insert_EmployeeAllowanceMapping(NewEmployeeID, Convert.ToInt32(ddlJobCategoryAdd.SelectedValue), dt);
                            }
                            HCM_PFLog(emp, dt);
                            BankDetail(NewEmployeeID, dt);

                        }

                        if (emp.EmployeeId > 0)
                        {
                            var a = context.SP_HCM_Mark_Employee_Education_Detail(emp.EmployeeId, UserKey);
                            DataTable _EducationDetail = (DataTable)ViewState["EmployeeEducation"];

                            if (_EducationDetail != null)
                            {
                                for (int i = 0; i < _EducationDetail.Rows.Count; i++)
                                {
                                    HCM_Employee_Education_Detail _obj = new HCM_Employee_Education_Detail();
                                    _obj.EmployeeId = emp.EmployeeId;
                                    _obj.EducationDegreeId = Convert.ToInt32(_EducationDetail.Rows[i]["EducationDegreeId"]);
                                    _obj.NameOfUniversity = _EducationDetail.Rows[i]["UniversityName"].ToString();
                                    _obj.YearOfDegree = Convert.ToInt32(_EducationDetail.Rows[i]["Year"].ToString());
                                    _obj.MajorEducation = _EducationDetail.Rows[i]["Major"].ToString();
                                    _obj.IsActive = true;
                                    _obj.CreatedBy = UserKey;
                                    _obj.CreatedDate = DateTime.Now;
                                    _obj.Company_id = Convert.ToInt32(ddlCompanyAdd.SelectedValue);
                                    context.HCM_Employee_Education_Detail.Add(_obj);
                                    context.SaveChanges();
                                }
                            }


                            ViewState["EmployeeEducation"] = null;

                            BindEducationGrid();

                            scope.Complete();
                            IsScope_Complete = true;
                        }


                    }
                    if (IsScope_Complete == true)
                    {
                        try
                        {
                            if (EmployeeId > 0)
                            {
                                // context.INSERT_INTO_AuditLog(EmployeeId.ToString(), "Setup_Employee", (int)Constant.OperationType.UPDATE, baseclass.UserKey);
                              
                            }
                            else
                            {
                                // context.INSERT_INTO_AuditLog(NewEmployeeID.ToString(), "Setup_Employee", (int)Constant.OperationType.INSERT, baseclass.UserKey);
                            }
                        }
                        catch { }
                        SuccessRedirect("Employee has been " + (EmployeeId > 0 ? "edit" : "added") + " successfully.<br />Employee Code : " + txtEmployeeNo.Text.Trim() + "", url);
                    }
                    else
                    {
                        Error("Add / Edit Employee request cannot be fulfilled because something went wrong...");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.ToString();
            divError.Visible = true;
        }
    }
    private void HCM_PFLog(Setup_Employee emp, DateTime dt)
    {
        if (emp.EmployeeTypeId != null)
        {
            int? EmployeeTypeId = emp.EmployeeTypeId;
            var lst = context.Setup_EmployeeType.FirstOrDefault(a => a.EmployeeTypeId == EmployeeTypeId);
            if (lst != null)
            {
                if (lst.IsPermenant)
                {
                    var HCM_PFLog = context.HCM_PFLog.FirstOrDefault(a => a.IsActive == true && a.EmployeeId == emp.EmployeeId);
                    if (HCM_PFLog == null)
                    {
                        HCM_PFLog objPfLog = new HCM_PFLog();
                        objPfLog.CreatedBy = UserKey;
                        objPfLog.CreatedDate = DateTime.Now;
                        objPfLog.EmployeeId = emp.EmployeeId;
                        objPfLog.IsActive = true;
                        objPfLog.OnHold = false;
                        context.HCM_PFLog.Add(objPfLog);
                        context.SaveChanges();
                    }
                }
                else
                {
                    var HCM_PFLog = context.HCM_PFLog.Where(a => a.IsActive == true && a.EmployeeId == emp.EmployeeId).ToList();
                    HCM_PFLog.ForEach(d => { d.IsActive = false; d.ModifiedDate = dt; d.ModifiedBy = UserKey; });
                    context.SaveChanges();
                }
            }
        }
    }
    private void BankDetail(int EmpID, DateTime dt)
    {
        var li_EmployeeBank = context.HRMS_EmployeeBank.Where(a => a.IsActive == true && a.EmployeeId == EmpID).ToList();
        li_EmployeeBank.ForEach(d => { d.IsActive = false; d.ModifiedDate = dt; d.ModifiedBy = UserKey; });
        context.SaveChanges();

        HRMS_EmployeeBank obj = new HRMS_EmployeeBank();
        obj.EmployeeId = Convert.ToInt32(EmpID);
        obj.BankId = ddlBank.SelectedValue == "0" ? IntNull : Convert.ToInt32(ddlBank.SelectedValue);
        obj.AccountNumber = txtAccountno.Text.Trim();
        obj.PayModeId = ddlPaymentMode.SelectedValue == "0" ? IntNull : Convert.ToInt32(ddlPaymentMode.SelectedValue);
        obj.AccountTypeId = ddlAccountType.SelectedValue == "0" ? IntNull : Convert.ToInt32(ddlAccountType.SelectedValue);
        obj.DefaultBank = true;
        obj.CreatedBy = UserKey;
        obj.CreatedDate = dt;
        obj.IsActive = true;
        obj.UserIP = UserIP;
        context.HRMS_EmployeeBank.Add(obj);
        context.SaveChanges();
    }
    public void Insert_EmployeeAllowanceMapping(int EmployeeId_, int CategoryId_, DateTime dt)
    {

        var li = context.HCM_Setup_CategoryAllowanceMapping.Where(a => a.IsActive == true && a.CategoryId == CategoryId_).ToList();
        if (li != null && li.Count > 0)
        {
            for (int j = 0; j < li.Count; j++)
            {
                HCM_EmployeeAllowanceMapping obj = new HCM_EmployeeAllowanceMapping();
                obj.EmployeeID = EmployeeId_;
                obj.AllowanceID = li[j].AllowanceID;
                obj.Measure = 0;
                obj.IsSpecialAllowance = false;
                obj.CreatedBy = UserKey;
                obj.CreatedDate = dt;
                obj.IsActive = true;
                context.HCM_EmployeeAllowanceMapping.Add(obj);
                context.SaveChanges();
            }
        }

    }

    public bool ValiationFunction()
    {
        bool Status = true;
        int CompanyId = Convert.ToInt32(ddlCompanyAdd.SelectedValue);
        var Count = context.Setup_Employee.Where(a => a.EmployeeCode == txtEmployeeNo.Text.Trim() && a.CompanyId == CompanyId && a.EmployeeId != EmployeeId).Count();
        if (Count > 0)
        {
            Status = false;
            Error("Employee Code Already Exist in " + ddlCompanyAdd.SelectedItem.Text);
        }
        if (txtDOJadd.Text == txtDOB.Text)
        {
            Status = false;
            Error("Date of Birth and Date of Joining Cannot be same");
        }
        if (EmployeeId > 0)
        {
            var Countcnic = context.Setup_Employee.Where(a => a.CompanyId == CompanyId && a.CNIC == txtcnicaddNew.Text && a.EmployeeCode == txtEmployeeNo.Text).Count();
            if (Countcnic > 0)
            {
                //Status = false;
                //Error("CNIC Already Exixts");
            }
            else
            {
                var Countcnic1 = context.Setup_Employee.Where(a => a.CompanyId == CompanyId && a.CNIC == txtcnicaddNew.Text && a.EmployeeCode != txtEmployeeNo.Text).Count();
                if (Countcnic1 > 0)
                {
                    Status = false;
                    Error("CNIC Already Exixts");
                }
            }
        }
        else
        {
            var Countcnic = context.Setup_Employee.Where(a => a.CompanyId == CompanyId && a.CNIC == txtcnicaddNew.Text).Count();
            if (Countcnic > 0)
            {
                Status = false;
                Error("CNIC Already Exixts");
            }
        }
        return Status;
    }
    public void Error(string message)
    {
        message = "AlertBox('Error!','" + message + "','error');";
        ScriptManager.RegisterStartupScript(this, GetType(), message, message, true);
    }
    public void SuccessRedirect(string message, string url)
    {
        message = "AlertBoxRedirect('Success!','" + message + "','success','" + url + "');";
        ScriptManager.RegisterStartupScript(this, GetType(), message, message, true);
    }
    public void ErrorRedirect(string message, string url)
    {
        message = "AlertBoxRedirect('Error!','" + message + "','error','" + url + "');";
        ScriptManager.RegisterStartupScript(this, GetType(), message, message, true);
    }
    protected void BtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(url);
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        int CoutryId = Convert.ToInt32(ddlCountry.SelectedValue);
        var LstProvince = context.Setup_Province.Where(a => a.IsActive == true && a.CountryId == CoutryId)
            .Select(a => new
            {
                ProvinceName = a.Province,
                ProvinceId = a.ProvinceId
            })
            .OrderBy(b => b.ProvinceName).ToList();
        CommonHelper.BindDropDown(ddlProvince, LstProvince, "ProvinceName", "ProvinceId", true, false);
        ddlProvince_SelectedIndexChanged(null, null);
    }
    protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
    {
        int ProviceId = Convert.ToInt32(ddlProvince.SelectedValue);
        var LstCity = context.Setup_City.Where(a => a.IsActive == true && a.ProvinceId == ProviceId)
            .Select(a => new
            {
                CityName = a.CityName,
                CityId = a.CityId
            })
            .OrderBy(b => b.CityName).ToList();
        CommonHelper.BindDropDown(ddlCity, LstCity, "CityName", "CityId", true, false);
    }
    private void AddItemIntoEducationGrid(int educationDegreeId, string major, string educationdegreename, string universityname, int year)
    {
        if (ViewState["EmployeeEducation"] == null)
            ViewState["EmployeeEducation"] = AddColumnToGrid();

        if (!CheckValueExistInEducationGrid(educationDegreeId))
        {
            DataTable _Dt = (DataTable)ViewState["EmployeeEducation"];
            DataRow _DR = _Dt.NewRow();
            _DR["EducationDegreeId"] = educationDegreeId;
            _DR["Major"] = major;
            _DR["EducationDegree"] = educationdegreename;
            _DR["UniversityName"] = universityname;
            _DR["Year"] = year;

            _Dt.Rows.Add(_DR);
            ViewState["EmployeeEducation"] = _Dt;
        }
        else
        {
            Error("Education Degree already added.");
        }



    }
    private DataTable AddColumnToGrid()
    {
        DataTable _DT = new DataTable();
        _DT.Columns.Add("EducationDegreeId");
        _DT.Columns.Add("Major");
        _DT.Columns.Add("EducationDegree");
        _DT.Columns.Add("UniversityName");
        _DT.Columns.Add("Year");

        return _DT;
    }
    private void BindEducationDetailGrid()
    {

        var _data = context.HCM_Employee_Education_Detail.Where(x => x.EmployeeId == EmployeeId && x.IsActive == true).Select(x => new
        {
            EducationDegreeId = x.EducationDegreeId,
            EducationDegree = x.HRMS_Setup_EducationType.educationType,
            UniversityName = x.NameOfUniversity,
            Year = x.YearOfDegree,
            Major = x.MajorEducation

        }).ToList();

        if (_data.Count > 0)
        {
            foreach (var item in _data)
            {
                AddItemIntoEducationGrid(item.EducationDegreeId, item.Major, item.EducationDegree, item.UniversityName, Convert.ToInt32(item.Year));
            }

            BindEducationGrid();
        }

    }
    private bool CheckValueExistInEducationGrid(int educationdegreeid)
    {
        bool exists = false;
        if (ViewState["EmployeeEducation"] != null)
        {
            DataTable _Dt = (DataTable)ViewState["EmployeeEducation"];

            exists = _Dt.AsEnumerable().Where(c => c.Field<string>("EducationDegreeId").Equals(educationdegreeid.ToString())).Count() > 0;
        }

        return exists;
    }
    private void BindEducationGrid()
    {
        if (ViewState["EmployeeEducation"] != null)
        {
            grdEducation.DataSource = (DataTable)ViewState["EmployeeEducation"];
            grdEducation.DataBind();
        }
        else
        {
            grdEducation.DataSource = null;
            grdEducation.DataBind();
        }

    }
    protected void btnAddEducationDetail_Click(object sender, EventArgs e)
    {
        AddItemIntoEducationGrid(Convert.ToInt32(cmbEducationDegree.SelectedValue), txtMajor.Text, cmbEducationDegree.SelectedItem.Text, txtEductionUniName.Text, Convert.ToInt32(txtEducationYear.Text));
        BindEducationGrid();
    }
    protected void gridService_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "RemoveRowEducationGird")
            {
                DataTable _Dt = (DataTable)ViewState["EmployeeEducation"];
                var Data = _Dt.AsEnumerable()
                         .Where(r => r.Field<string>("EducationDegreeId") != e.CommandArgument.ToString())
                         .ToList();

                if (Data.Count > 0)
                {
                    ViewState["EmployeeEducation"] = Data.CopyToDataTable();
                }
                else
                {
                    ViewState["EmployeeEducation"] = null;

                }

                BindEducationGrid();

            }
        }
        catch (Exception ex)
        {

            Error(ex.Message);
        }



    }
}
