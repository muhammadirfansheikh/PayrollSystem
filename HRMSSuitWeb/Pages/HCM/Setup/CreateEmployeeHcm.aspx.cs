using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DAL;
using System.Web.UI.WebControls;

public partial class Pages_HCM_Setup_CreateEmployeeHcm : Base
{
    Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();
    Base baseclass = new Base();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDown();
        }
        PagingHandler();
    }

    #region PAGING
    private void PagingHandler()
    {
        PagingAndSorting.ImgNext.Click += ImgNext_Click;
        PagingAndSorting.ImgPrevious.Click += ImgPrevious_Click;
        PagingAndSorting.DdlPage.SelectedIndexChanged += DdlPage_SelectedIndexChanged;
        PagingAndSorting.DdlPageSize.SelectedIndexChanged += DdlPageSize_SelectedIndexChanged;
    }

    void DdlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindRepeaterEmployee();
    }
    void DdlPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindRepeaterEmployee();
    }
    void ImgNext_Click(object sender, ImageClickEventArgs e)
    {
        BindRepeaterEmployee();
    }
    void ImgPrevious_Click(object sender, ImageClickEventArgs e)
    {
        BindRepeaterEmployee();
    }
    #endregion

    private void BindDropDown()
    {
        ddlGroup.Enabled = false;
        ddlGroupAdd.Enabled = false;


        //var ListGrade = context.HRMS_Setup_Grade.Where(c => c.IsActive == true).OrderBy(c => c.Grade).ToList();
        //CommonHelper.BindDropDown(ddlGrade, ListGrade, "Grade", "GradeId", true, false);

        //var ListGroupInsuranceId = context.HRMS_Setup_GroupInsurance.Where(c => c.IsActive == true).OrderBy(c => c.GroupInsurance).ToList();
        //CommonHelper.BindDropDown(ddlGroupInsurance, ListGroupInsuranceId, "GroupInsurance", "GroupInsuranceId", true, false);

        //var ListEmpFunc = context.HRMS_Setup_EmployeeFunction.Where(c => c.IsActive == true).OrderBy(c => c.FunctionName).ToList();
        //CommonHelper.BindDropDown(ddlEmpFunc, ListEmpFunc, "FunctionName", "EmployeeFunctionId", true, false);
        //ddlEmpFunc_SelectedIndexChanged(null, null);

        //var ListBloodGroup = context.HRMS_Setup_BloodGroup.Where(c => c.IsActive == true).OrderBy(c => c.BloodGroup).ToList();
        //CommonHelper.BindDropDown(ddlBloodGroup, ListBloodGroup, "BloodGroup", "BloodGroupId", true, false);

        //var ListEmpType = context.Setup_EmployeeType.Where(c => c.IsActive == true).OrderBy(c => c.TypeName).ToList();
        //CommonHelper.BindDropDown(ddlEmptype, ListEmpType, "TypeName", "EmployeeTypeId", true, false);


        //var ListGender = context.HRMS_Setup_Gender.Where(c => c.IsActive == true).OrderBy(c => c.GenderTitle).ToList();
        //CommonHelper.BindDropDown(ddlGenderAdd, ListGender, "GenderTitle", "GenderId", true, false);

        //var ListBank = context.HRMS_Setup_BankMaster.Where(c => c.IsActive == true).OrderBy(c => c.BankName).ToList();
        //CommonHelper.BindDropDown(ddlBankMaster, ListBank, "BankName", "BankMasterId", true, false);
        //ddlBankMaster_SelectedIndexChanged(null, null);

        //var ListPaymentMode = context.HRMS_Setup_PayMode.Where(c => c.IsActive == true).OrderBy(c => c.PayMode).ToList();
        //CommonHelper.BindDropDown(ddlPaymentMode, ListPaymentMode, "PayMode", "PayModeId", true, false);

        //var ListAccountType = context.HRMS_Setup_AccountType.Where(c => c.IsActive == true).OrderBy(c => c.AccountType).ToList();
        //CommonHelper.BindDropDown(ddlAccountType, ListAccountType, "AccountType", "AccountTypeId", true, false);

        //var LstBusinessUnitCostCenterMapping = context.HCM_SETUP_SapCostCenter.Where(a => a.IsActitve == true).ToList();
        //CommonHelper.BindDropDown(ddlSapCostCenter, LstBusinessUnitCostCenterMapping, "SapCostCenter", "SapCostId", true, false);


        var LstGroup = context.Setup_Group.Where(x => x.IsActive == true).OrderBy(x => x.GroupName).Select(s => new
        {
            GroupName = s.GroupName,
            GroupId = s.GroupId
        }).ToList();

        CommonHelper.BindDropDown(ddlGroup, LstGroup, "GroupName", "GroupId", LstGroup.Count == 1 ? false : true, false);
        //CommonHelper.BindDropDown(ddlGroupAdd, LstGroup, "GroupName", "GroupId", LstGroup.Count == 1 ? false : true, false);
        ddlGroup_SelectedIndexChanged(null, null);
        //ddlGroupAdd_SelectedIndexChanged(null, null);
    }
    protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        int GroupId = Convert.ToInt32(ddlGroup.SelectedValue);
        var LstCompany = context.Setup_EmployeeCompanyMapping.Where(a => a.IsActive == true && a.EmployeeId == UserKey && a.Setup_Company.IsActive == true && a.Setup_Company.GroupId == GroupId)
            .Select(a => new
            {
                CompanyName = a.Setup_Company.CompanyName,
                CompanyId = a.Setup_Company.CompanyId
            }).OrderBy(b => b.CompanyName).ToList();

        CommonHelper.BindDropDown(ddlCompany, LstCompany, "CompanyName", "CompanyId", true, false);
        ddlCompany_SelectedIndexChanged(null, null);
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        int CompanyId_ = Convert.ToInt32(ddlCompany.SelectedValue);
        var LstLocation = context.Setup_Location.Where(a => a.IsActive == true && a.CompanyId == CompanyId_).OrderBy(b => b.LocationName).ToList();
        CommonHelper.BindDropDown(ddlLocation, LstLocation, "LocationName", "LocationId", true, false);
        var LstCostCenter = context.Setup_CostCenter.Where(a => a.IsActive == true && a.CompanyId == CompanyId_).OrderBy(b => b.CostCenterName).ToList();
        CommonHelper.BindDropDown(ddlCostCenter, LstCostCenter, "CostCenterName", "CostCenterId", true, false);
        var listBusinessUnit = context.TS_Setup_BusinessUnit.Where(c => c.IsActive == true && c.CompanyId == CompanyId_).OrderBy(c => c.BusinessUnitName).ToList();
        CommonHelper.BindDropDown(ddlBusinessUnit, listBusinessUnit, "BusinessUnitName", "BusinessUnitID", true, false);
        var listcat = context.Setup_Category.Where(c => c.IsActive == true && c.CompanyId == CompanyId_).OrderBy(c => c.CategoryName).ToList();
        CommonHelper.BindDropDown(ddlJobCategory, listcat, "CategoryName", "CategoryId", true, false);
        ddlBusinessUnit_SelectedIndexChanged(null, null);
        ddlJobCategory_SelectedIndexChanged(null, null);
    }
    protected void ddlBusinessUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        int CompanyId_ = Convert.ToInt32(ddlCompany.SelectedValue);
        int BUID = ddlBusinessUnit.SelectedValue == "" ? 0 : Convert.ToInt32(ddlBusinessUnit.SelectedValue);
        var ListDepartment = context.Setup_Department.Where(c => c.IsActive == true && c.BusinessUnitId == BUID && c.TS_Setup_BusinessUnit.CompanyId == CompanyId_).OrderBy(c => c.DepartmentName).ToList();
        CommonHelper.BindDropDown(ddlDepartment, ListDepartment, "DepartmentName", "DepartmentId", true, false);
    }
    protected void ddlJobCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        int CompanyId_ = Convert.ToInt32(ddlCompany.SelectedValue);
        int JobCategoryId_ = ddlJobCategory.SelectedValue == "" ? 0 : Convert.ToInt32(ddlJobCategory.SelectedValue);
        var ListDesignation = context.Setup_Designation.Where(c => c.IsActive == true && c.CategoryId == JobCategoryId_ && c.Setup_Category.CompanyId == CompanyId_).OrderBy(c => c.DesignationName).ToList();
        CommonHelper.BindDropDown(ddlDesignation, ListDesignation, "DesignationName", "DesignationId", true, false);
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
        var LstLocation = context.Setup_Location.Where(a => a.IsActive == true && a.CompanyId == CompanyId_).OrderBy(b => b.LocationName).ToList();
        CommonHelper.BindDropDown(ddlLocationAdd, LstLocation, "LocationName", "LocationId", true, false);
        var LstCostCenter = context.Setup_CostCenter.Where(a => a.IsActive == true && a.CompanyId == CompanyId_).OrderBy(b => b.CostCenterName).ToList();
        CommonHelper.BindDropDown(ddlCostCenterAdd, LstCostCenter, "CostCenterName", "CostCenterId", true, false);
        var listBusinessUnit = context.TS_Setup_BusinessUnit.Where(c => c.IsActive == true && c.CompanyId == CompanyId_).OrderBy(c => c.BusinessUnitName).ToList();
        CommonHelper.BindDropDown(ddlBusinessUnitAdd, listBusinessUnit, "BusinessUnitName", "BusinessUnitID", true, false);
        var listcat = context.Setup_Category.Where(c => c.IsActive == true && c.CompanyId == CompanyId_).OrderBy(c => c.CategoryName).ToList();
        CommonHelper.BindDropDown(ddlJobCategoryAdd, listcat, "CategoryName", "CategoryId", true, false);
        ddlBusinessUnitAdd_SelectedIndexChanged(null, null);
        ddlJobCategoryAdd_SelectedIndexChanged(null, null);
    }
    protected void ddlBusinessUnitAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        int CompanyId_ = Convert.ToInt32(ddlCompanyAdd.SelectedValue);
        int BUID = ddlBusinessUnitAdd.SelectedValue == "" ? 0 : Convert.ToInt32(ddlBusinessUnitAdd.SelectedValue);
        var ListDepartment = context.Setup_Department.Where(c => c.IsActive == true && c.BusinessUnitId == BUID && c.TS_Setup_BusinessUnit.CompanyId == CompanyId_).OrderBy(c => c.DepartmentName).ToList();
        CommonHelper.BindDropDown(ddlDepartmentAdd, ListDepartment, "DepartmentName", "DepartmentId", true, false);
    }
    protected void ddlJobCategoryAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        int CompanyId_ = Convert.ToInt32(ddlCompanyAdd.SelectedValue);
        int JobCategoryId_ = ddlJobCategoryAdd.SelectedValue == "" ? 0 : Convert.ToInt32(ddlJobCategoryAdd.SelectedValue);
        var ListDesignation = context.Setup_Designation.Where(c => c.IsActive == true && c.CategoryId == JobCategoryId_ && c.Setup_Category.CompanyId == CompanyId_).OrderBy(c => c.DesignationName).ToList();
        CommonHelper.BindDropDown(ddlDesignationAdd, ListDesignation, "DesignationName", "DesignationId", true, false);

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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindRepeaterEmployee();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtEmployeeCode.Text = "";
        txtCNIC.Text = "";
        txtDateOfJoin.Text = "";
        txtFirstName.Text = "";
        txtMiddleName.Text = "";
        txtLastName.Text = "";
        PagingAndSorting.setPagingOptions(0);
        RptEmployee.DataSource = null;
        RptEmployee.DataBind();
        BindDropDown();
    }
    private void BindRepeaterEmployee()
    {

        int pageSize = 50;
        int pageNumber = 1;
        if (PagingAndSorting.DdlPageSize.SelectedValue.toInt() > 0)
        {
            pageSize = PagingAndSorting.DdlPageSize.SelectedValue.toInt();
        }
        if (PagingAndSorting.DdlPage.Items.Count > 0)
        {
            pageNumber = PagingAndSorting.DdlPage.SelectedValue.toInt();
        }

        int skip = pageNumber * pageSize - pageSize;
        DateTime? DateTimeNull = null;
        int? IntNull = null;
        string EmployeeCode = txtEmployeeCode.Text.Trim();
        string CNIC = txtCNIC.Text.Trim();
        DateTime? DateOfJoin = txtDateOfJoin.Text.Trim() == "" ? DateTimeNull : Convert.ToDateTime(txtDateOfJoin.Text.Trim());
        int GroupId_ = Convert.ToInt32(ddlGroup.SelectedValue);
        int? CompanyId_ = ddlCompany.SelectedValue == "0" ? IntNull : Convert.ToInt32(ddlCompany.SelectedValue);
        int? LocationId_ = ddlLocation.SelectedValue == "0" ? IntNull : Convert.ToInt32(ddlLocation.SelectedValue);
        int? BusinessUnit_ = ddlBusinessUnit.SelectedValue == "0" ? IntNull : Convert.ToInt32(ddlBusinessUnit.SelectedValue);
        int? DepartmentId_ = ddlDepartment.SelectedValue == "0" ? IntNull : Convert.ToInt32(ddlDepartment.SelectedValue);
        int? CostCenterId_ = ddlCostCenter.SelectedValue == "0" ? IntNull : Convert.ToInt32(ddlCostCenter.SelectedValue);
        int? JobCategoryId_ = ddlJobCategory.SelectedValue == "0" ? IntNull : Convert.ToInt32(ddlJobCategory.SelectedValue);
        int? DesignationId_ = ddlDesignation.SelectedValue == "0" ? IntNull : Convert.ToInt32(ddlDesignation.SelectedValue);
        string FirstName = txtFirstName.Text.Trim();
        string MiddleName = txtMiddleName.Text.Trim();
        string LastName = txtLastName.Text.Trim();
        bool IsActive = ChbxIsActive.Checked;

        var Employees = context.Setup_Employee.Where(c => c.IsActive == IsActive
            && c.Setup_Company.Setup_EmployeeCompanyMapping.Any(x => x.IsActive == true && x.EmployeeId == UserKey && c.Setup_Company.IsActive == true && c.Setup_Company.GroupId == GroupId_)
            && (c.CompanyId == CompanyId_ || CompanyId_ == null)
            && (c.EmployeeCode == EmployeeCode || EmployeeCode == string.Empty)
            && (c.CNIC == CNIC || CNIC == string.Empty)
            && (c.JoiningDate == DateOfJoin || DateOfJoin == null)
            && (c.LocationId == LocationId_ || LocationId_ == null)
            && (c.Setup_Department.BusinessUnitId == BusinessUnit_ || BusinessUnit_ == null)
            && (c.DepartmentId == DepartmentId_ || DepartmentId_ == null)
            && (c.CostCenterId == CostCenterId_ || CostCenterId_ == null)
            && (c.Setup_Designation.CategoryId == JobCategoryId_ || JobCategoryId_ == null)
            && (c.DesignationId == DesignationId_ || DesignationId_ == null)
            && (c.FirstName.Contains(FirstName) || FirstName == string.Empty)
            && (c.MiddleName.Contains(MiddleName) || MiddleName == string.Empty)
            && (c.LastName.Contains(LastName) || LastName == string.Empty)).Select(c => new
            {
                Name = c.FirstName + ((c.MiddleName == "" || c.MiddleName == null) ? "" : " " + c.MiddleName) + ((c.LastName == "" || c.LastName == null) ? "" : " " + c.LastName),
                EmployeeId = c.EmployeeId,
                EmployeeCode = c.EmployeeCode,
                FirstName = c.FirstName,
                LastName = c.LastName,
                CNIC = c.CNIC,
                Designation = c.Setup_Designation.DesignationName,
                Department = c.Setup_Department.DepartmentName,
                DateOfBirth = c.DateOfBirth,
                JoiningDate = c.JoiningDate,
                LastworkingDate = c.LastworkingDate,
                ReisgnedDate = c.ResignedDate,
                PersonalEmail = c.PersonalEmailAddress,
                OfficalEmail = c.OfficeEmailAddress,
                Gender = c.HRMS_Setup_Gender.GenderTitle,
                EmployeeType = c.Setup_EmployeeType.TypeName,
                IsActive = c.IsActive,
                Company = c.Setup_Company.CompanyName,
                Location = c.Setup_Location.LocationName,
                LocationId = c.LocationId,
                EmployeeImage = c.PictureName,
                Extension = c.Extension,
            }).ToList().OrderBy(d => d.Company).ThenBy(a => Convert.ToInt32(a.EmployeeCode)).ToList();
        var List = Employees.ToList().OrderBy(d => d.Company).ThenBy(a => Convert.ToInt32(a.EmployeeCode)).ToList().Skip(skip).Take(pageSize).ToList();
        RptEmployee.DataSource = List;
        RptEmployee.DataBind();
        PagingAndSorting.setPagingOptions(Employees.Count());
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Button btnEdit = (Button)sender;
        RepeaterItem rptItem = (RepeaterItem)btnEdit.NamingContainer;
        int empIdRpt = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfEmployeeIdRpt")).Value);
        if (empIdRpt > 0)
        {
            string Encrypted = "EmployeeId=" + empIdRpt;
            Response.Redirect("/Pages/HCM/Setup/CreateEmployee.aspx?" + CommonHelper.Encrypt_New(Encrypted));
        } 
    } 
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            int? IntNull = null;
            DateTime? DateNull = null;
            int SelectedEmployeeId = Convert.ToInt32(hfEmployeeId.Value);
            bool IsEmpCodeExist = false;
            string _EmployeeCode = txtEmployeeCodeAdd.Text;
            int _CompanyId = Convert.ToInt32(ddlCompanyAdd.SelectedValue);

            if (SelectedEmployeeId > 0)
            {
                var emp = context.Setup_Employee.FirstOrDefault(j => j.EmployeeId == SelectedEmployeeId);
                //Setup_Employee emp = new Setup_Employee();

                emp.EmployeeCode = txtEmployeeCodeAdd.Text;
                emp.CompanyId = Convert.ToInt32(ddlCompanyAdd.SelectedValue);
                emp.LocationId = Convert.ToInt32(ddlLocationAdd.SelectedValue);
                emp.DepartmentId = Convert.ToInt32(ddlDepartmentAdd.SelectedValue);
                if (Convert.ToInt32(ddlDesignationAdd.SelectedValue) > 0)
                {
                    emp.DesignationId = Convert.ToInt32(ddlDesignationAdd.SelectedValue);
                }

                emp.CostCenterId = Convert.ToInt32(ddlCostCenterAdd.SelectedValue);
                emp.GroupInsuranceId = Convert.ToInt32(ddlGroupInsurance.SelectedValue) == 0 ? IntNull : Convert.ToInt32(ddlGroupAdd.SelectedValue);
                emp.GroupLifeInsuranceId = Convert.ToInt32(ddlGroupInsurance.SelectedValue) == 0 ? IntNull : Convert.ToInt32(ddlGroupAdd.SelectedValue);
                emp.GroupPersonalAccidentInsuranceId = Convert.ToInt32(ddlGroupInsurance.SelectedValue) == 0 ? IntNull : Convert.ToInt32(ddlGroupAdd.SelectedValue);
                emp.EmployeeSubFunctionId = Convert.ToInt32(ddlEmployeeSubFunction.SelectedValue) == 0 ? IntNull : Convert.ToInt32(ddlEmployeeSubFunction.SelectedValue);
                emp.AttendanceAllowanceStatus = chkAttendanceAllowanceStatus.Checked;
                emp.EOBIStatus = chkEobiStatus.Checked;
                emp.EOBINumber = txtEobiNo.Text == string.Empty ? "" : txtEobiNo.Text;
                emp.EOBIDate = txtEobiDate.Text == string.Empty ? DateNull : Convert.ToDateTime(txtEobiDate.Text);
                emp.SESANumber = txtSesaNo.Text == string.Empty ? "" : txtSesaNo.Text;
                emp.IsAllowInterest = chkAllowIntrest.Checked;
                emp.JoiningDate = txtDateOfJoiningHcm.Text == string.Empty ? DateNull : Convert.ToDateTime(txtDateOfJoiningHcm.Text);
                emp.SapCostCenterId = Convert.ToInt32(ddlSapCostCenter.SelectedValue) == 0 ? IntNull : Convert.ToInt32(ddlSapCostCenter.SelectedValue);
                emp.EmployeeTypeId = Convert.ToInt32(ddlEmptype.SelectedValue) == 0 ? IntNull : Convert.ToInt32(ddlEmptype.SelectedValue);
                emp.ConfirmationDate = txtConfirmationdate.Text == string.Empty ? DateNull : Convert.ToDateTime(txtConfirmationdate.Text);
                emp.ContractStartDate = txtContractstartdate.Text == string.Empty ? DateNull : Convert.ToDateTime(txtContractstartdate.Text);
                emp.ContractEndDate = txtContractenddate.Text == string.Empty ? DateNull : Convert.ToDateTime(txtContractenddate.Text);
                emp.CNIC = txtcnicadd.Text == string.Empty ? "" : txtcnicadd.Text;
                emp.FirstName = txtFirstNameAdd.Text == string.Empty ? "" : txtFirstNameAdd.Text;
                emp.MiddleName = txtMiddleNameAdd.Text == string.Empty ? "" : txtMiddleNameAdd.Text;
                emp.LastName = txtLastNameAdd.Text == string.Empty ? "" : txtLastNameAdd.Text;
                emp.DateOfBirth = txtDOB.Text == string.Empty ? DateNull : Convert.ToDateTime(txtDOB.Text);
                emp.GenderId = Convert.ToInt32(ddlGenderAdd.SelectedValue) == 0 ? IntNull : Convert.ToInt32(ddlGenderAdd.SelectedValue);
                emp.PersonalEmailAddress = txtPersonalEmailAdd.Text == string.Empty ? "" : txtPersonalEmailAdd.Text;
                emp.FatherName = txtFatherHusbandNameAdd.Text == string.Empty ? "" : txtFatherHusbandNameAdd.Text;
                emp.BloodGroupId = Convert.ToInt32(ddlBloodGroup.SelectedValue) == 0 ? IntNull : Convert.ToInt32(ddlBloodGroup.SelectedValue);
                emp.GradeId = Convert.ToInt32(ddlGrade.SelectedValue) == 0 ? IntNull : Convert.ToInt32(ddlGrade.SelectedValue);
                emp.ModifiedDate = DateTime.Now;
                emp.ModifiedBy = UserKey;
                emp.Address = txtAddress.Text;

                context.SaveChanges();

                PfLog(emp);

                BankDetail(SelectedEmployeeId);

                Success("Employee has been updated successfully.Employee Code: " + emp.EmployeeCode + "");

                ClearFields();
            }
            else
            {
                var LstEmpCodeExist = context.Setup_Employee.FirstOrDefault(a => a.EmployeeCode == _EmployeeCode && a.CompanyId == _CompanyId);

                if (LstEmpCodeExist != null)
                {
                    IsEmpCodeExist = true;
                }
                else
                {
                    IsEmpCodeExist = false;
                }

                if (!IsEmpCodeExist)
                {
                    Setup_Employee emp = new Setup_Employee();

                    emp.EmployeeCode = txtEmployeeCodeAdd.Text;
                    emp.CompanyId = Convert.ToInt32(ddlCompanyAdd.SelectedValue);
                    emp.LocationId = Convert.ToInt32(ddlLocationAdd.SelectedValue);
                    emp.DepartmentId = Convert.ToInt32(ddlDepartmentAdd.SelectedValue);
                    if (Convert.ToInt32(ddlDesignationAdd.SelectedValue) > 0)
                    {
                        emp.DesignationId = Convert.ToInt32(ddlDesignationAdd.SelectedValue);
                    }
                    emp.CostCenterId = Convert.ToInt32(ddlCostCenterAdd.SelectedValue);
                    emp.GroupInsuranceId = Convert.ToInt32(ddlGroupInsurance.SelectedValue) == 0 ? IntNull : Convert.ToInt32(ddlGroupInsurance.SelectedValue);
                    emp.GroupLifeInsuranceId = Convert.ToInt32(ddlGroupInsurance.SelectedValue) == 0 ? IntNull : Convert.ToInt32(ddlGroupInsurance.SelectedValue);
                    emp.GroupPersonalAccidentInsuranceId = Convert.ToInt32(ddlGroupInsurance.SelectedValue) == 0 ? IntNull : Convert.ToInt32(ddlGroupInsurance.SelectedValue);
                    emp.EmployeeSubFunctionId = Convert.ToInt32(ddlEmployeeSubFunction.SelectedValue) == 0 ? IntNull : Convert.ToInt32(ddlEmployeeSubFunction.SelectedValue);
                    emp.AttendanceAllowanceStatus = chkAttendanceAllowanceStatus.Checked;
                    emp.EOBIStatus = chkEobiStatus.Checked;
                    emp.EOBINumber = txtEobiNo.Text == string.Empty ? "" : txtEobiNo.Text;
                    emp.EOBIDate = txtEobiDate.Text == string.Empty ? DateNull : Convert.ToDateTime(txtEobiDate.Text);
                    emp.SESANumber = txtSesaNo.Text == string.Empty ? "" : txtSesaNo.Text;
                    emp.IsAllowInterest = chkAllowIntrest.Checked;
                    emp.JoiningDate = txtDateOfJoiningHcm.Text == string.Empty ? DateNull : Convert.ToDateTime(txtDateOfJoiningHcm.Text);
                    emp.SapCostCenterId = Convert.ToInt32(ddlSapCostCenter.SelectedValue) == 0 ? IntNull : Convert.ToInt32(ddlSapCostCenter.SelectedValue);
                    emp.EmployeeTypeId = Convert.ToInt32(ddlEmptype.SelectedValue) == 0 ? IntNull : Convert.ToInt32(ddlEmptype.SelectedValue);
                    emp.ConfirmationDate = txtConfirmationdate.Text == string.Empty ? DateNull : Convert.ToDateTime(txtConfirmationdate.Text);
                    emp.ContractStartDate = txtContractstartdate.Text == string.Empty ? DateNull : Convert.ToDateTime(txtContractstartdate.Text);
                    emp.ContractEndDate = txtContractenddate.Text == string.Empty ? DateNull : Convert.ToDateTime(txtContractenddate.Text);
                    emp.CNIC = txtcnicadd.Text == string.Empty ? "" : txtcnicadd.Text;
                    emp.FirstName = txtFirstNameAdd.Text == string.Empty ? "" : txtFirstNameAdd.Text;
                    emp.MiddleName = txtMiddleNameAdd.Text == string.Empty ? "" : txtMiddleNameAdd.Text;
                    emp.LastName = txtLastNameAdd.Text == string.Empty ? "" : txtLastNameAdd.Text;
                    emp.DateOfBirth = txtDOB.Text == string.Empty ? DateNull : Convert.ToDateTime(txtDOB.Text);
                    emp.GenderId = Convert.ToInt32(ddlGenderAdd.SelectedValue) == 0 ? IntNull : Convert.ToInt32(ddlGenderAdd.SelectedValue);
                    emp.PersonalEmailAddress = txtPersonalEmailAdd.Text == string.Empty ? "" : txtPersonalEmailAdd.Text;
                    emp.FatherName = txtFatherHusbandNameAdd.Text == string.Empty ? "" : txtFatherHusbandNameAdd.Text;
                    emp.BloodGroupId = Convert.ToInt32(ddlBloodGroup.SelectedValue) == 0 ? IntNull : Convert.ToInt32(ddlBloodGroup.SelectedValue);
                    emp.GradeId = Convert.ToInt32(ddlGrade.SelectedValue) == 0 ? IntNull : Convert.ToInt32(ddlGrade.SelectedValue);
                    emp.IsActive = true;
                    emp.CreatedDate = DateTime.Now;
                    emp.CreatedBy = UserKey;
                    emp.Address = txtAddress.Text;

                    context.Setup_Employee.Add(emp);
                    context.SaveChanges();

                    PfLog(emp);

                    BankDetail(emp.EmployeeId);

                    Success("Employee has been added successfully.Employee Code: " + emp.EmployeeCode + "");

                    ClearFields();
                }
                else
                {
                    Error("Employee Code Already Exist in " + ddlCompanyAdd.SelectedItem.Text);
                }
            }
        }
        catch
        {

        }
    }
    private void PfLog(DAL.Setup_Employee emp)
    {
        if (emp.EmployeeTypeId != null)
        {
            int COId = emp.CompanyId;
            int? EMPTId = emp.EmployeeTypeId;

            var lstIsPermenant = context.Setup_EmployeeType.FirstOrDefault(a => a.IsActive == true && a.CompanyId == COId && a.EmployeeTypeId == EMPTId);

            if (lstIsPermenant != null)
            {
                bool IsPermenant = lstIsPermenant.IsPermenant == null ? false : lstIsPermenant.IsPermenant;

                if (IsPermenant)
                {
                    var HCM_PFLog = context.HCM_PFLog.FirstOrDefault(a => a.IsActive == true && a.EmployeeId == emp.EmployeeId);

                    if (HCM_PFLog != null)
                    {
                        //HCM_PFLog.IsActive = false;
                        //HCM_PFLog.ModifiedBy = UserKey;
                        //HCM_PFLog.ModifiedDate = DateTime.Now;

                        //context.SaveChanges();
                    }
                    else
                    {

                        HCM_PFLog objPfLog = new HCM_PFLog();

                        objPfLog.CreatedBy = UserKey;
                        objPfLog.CreatedDate = DateTime.Now;
                        objPfLog.EmployeeId = emp.EmployeeId;
                        objPfLog.IsActive = true;
                        objPfLog.OnHold = HCM_PFLog != null ? HCM_PFLog.OnHold : false;

                        context.HCM_PFLog.Add(objPfLog);
                        context.SaveChanges();
                    }
                }
            }
        }
    }
    private void BankDetail(int EmpID)
    {
        var emp = context.HRMS_EmployeeBank.FirstOrDefault(j => j.EmployeeId == EmpID && j.IsActive == true);

        if (emp != null)
        {
            emp.ModifiedBy = UserKey;
            emp.ModifiedDate = DateTime.Now;
            emp.IsActive = false;

            context.SaveChanges();
        }

        HRMS_EmployeeBank obj = new HRMS_EmployeeBank();
        obj.EmployeeId = Convert.ToInt32(EmpID);
        obj.BankId = Convert.ToInt32(ddlBank.SelectedValue);
        obj.AccountNumber = txtAccountno.Text.Trim();
        obj.PayModeId = Convert.ToInt32(ddlPaymentMode.SelectedValue);
        obj.AccountTypeId = Convert.ToInt32(ddlAccountType.SelectedValue);

        obj.CreatedBy = UserKey;
        obj.CreatedDate = DateTime.Now;
        obj.IsActive = true;
        obj.UserIP = UserIP;
        obj.DefaultBank = chkDefault.Checked;
        context.HRMS_EmployeeBank.Add(obj);
        context.SaveChanges();
    }
    private void ClearFields()
    {
        hfEmployeeId.Value = "0";
        txtEmployeeCodeAdd.Text = "";
        chkAllowIntrest.Checked = false;
        chkAttendanceAllowanceStatus.Checked = false;
        chkEobiStatus.Checked = false;
        txtEobiNo.Text = "";
        txtEobiDate.Text = "";
        txtSesaNo.Text = "";
        txtDateOfJoin.Text = "";
        txtDateOfJoiningHcm.Text = "";
        txtConfirmationdate.Text = "";
        txtContractenddate.Text = "";
        txtContractstartdate.Text = "";
        txtcnicadd.Text = "";
        txtFirstNameAdd.Text = "";
        txtMiddleNameAdd.Text = "";
        txtLastNameAdd.Text = "";
        txtDOB.Text = "";
        txtPersonalEmailAdd.Text = "";
        txtFatherHusbandNameAdd.Text = "";
        txtAccountno.Text = "";
        chkDefault.Checked = false;

        ddlGroupAdd.SelectedValue = "0";
        ddlCompanyAdd.SelectedValue = "0";
        ddlCompanyAdd_SelectedIndexChanged(null, null);
        ddlLocationAdd.SelectedValue = "0";
        ddlBusinessUnitAdd.SelectedValue = "0";
        ddlDepartmentAdd.SelectedValue = "0";
        ddlJobCategoryAdd.SelectedValue = "0";
        ddlDesignationAdd.SelectedValue = "0";
        ddlCostCenterAdd.SelectedValue = "0";
        ddlGroupInsurance.SelectedValue = "0";
        ddlEmpFunc.SelectedValue = "0";
        ddlEmployeeSubFunction.SelectedValue = "0";
        ddlSapCostCenter.SelectedValue = "0";
        ddlEmptype.SelectedValue = "0";
        ddlGenderAdd.SelectedValue = "0";
        ddlBloodGroup.SelectedValue = "0";
        ddlGrade.SelectedValue = "0";
        ddlBankMaster.SelectedValue = "0";
        ddlBank.SelectedValue = "0";
        ddlAccountType.SelectedValue = "0";
        ddlPaymentMode.SelectedValue = "0";
    }
    public void Success(string message)
    {
        message = "AlertBox('Success!','" + message + "','success');ClearFields();";
        ScriptManager.RegisterStartupScript(this, GetType(), message, message, true);
    }
    public void Error(string message)
    {
        message = "AlertBox('Error!','" + message + "','error');";
        ScriptManager.RegisterStartupScript(this, GetType(), message, message, true);
    }
    public void ClosePopup()
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "ClosePopup()", "ClosePopup();", true);
    }
    public void OpenPopup()
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "OpenPopup()", "OpenPopup();", true);
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Pages/HCM/Setup/CreateEmployee.aspx");
    }
}