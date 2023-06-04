using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_HCM_Setup_Setup_Repair_Maintainance : Base
{
    Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropdownSearch();
            BindDropdownAdd();
            BindDropdownIncrease();
            BindRepeater();

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ResetControls();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindRepeater();
    }

    protected void btnAddNewRecord_Click(object sender, EventArgs e)
    {
        OpenPopup();
        ResetControls();


    }

    protected void rpt_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void rpt_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "EditS")
            {

                DataTable lstEdit = GetData(null, null, null, null, ID);

                if (lstEdit.Rows.Count > 0)
                {


                    ResetControls();

                    hfModalId.Value = ID.ToString();


                    if (lstEdit.Rows[0]["EmployeeId"] != DBNull.Value)
                    {
                        ddlRMTypeAdd.SelectedValue = "1";
                        ddlEmployeeAdd.SelectedValue = lstEdit.Rows[0]["EmployeeId"].ToString();
                        dvJobCatgeoryAdd.Visible = false;
                        dvEmployeeAdd.Visible = true;

                        RequiredFieldValidator4.Enabled = false;
                        RequiredFieldValidator13.Enabled = false;

                        RequiredFieldValidator1.Enabled = true;
                        RequiredFieldValidator14.Enabled = true;

                    }
                    else
                    {
                        ddlRMTypeAdd.SelectedValue = "0";
                        ddlJobCategoryAdd.SelectedValue = lstEdit.Rows[0]["CategoryId"].ToString();

                        dvJobCatgeoryAdd.Visible = true;
                        dvEmployeeAdd.Visible = false;

                        RequiredFieldValidator4.Enabled = true;
                        RequiredFieldValidator13.Enabled = true;

                        RequiredFieldValidator1.Enabled = false;
                        RequiredFieldValidator14.Enabled = false;
                    } 
                    txtFuelInLitresAdd.Text = lstEdit.Rows[0]["FuelInLitres"].ToString();
                    txtRM_FirstYearAdd.Text = lstEdit.Rows[0]["RM_FirstYear"].ToString();
                    txtRM_SecondYearAdd.Text = lstEdit.Rows[0]["RM_SecondYear"].ToString();
                    txtRM_ThirdYearAdd.Text = lstEdit.Rows[0]["RM_ThirdYear"].ToString();
                    txtRM_ForthYearAdd.Text = lstEdit.Rows[0]["RM_ForthYear"].ToString();
                    txtRM_FifthYearAdd.Text = lstEdit.Rows[0]["RM_FifthYear"].ToString(); 
                    OpenPopup(); 
                }
                else
                {
                    Error("Record Can Not Edit.Beacuse Record Not Find.");
                }
            }
            else if (e.CommandName == "DeleteS")
            {
                var _Data = context.HCM_Setup_RM.FirstOrDefault(x => x.SetupRMId == ID);

                #region Audit Logs
                //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
                DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(ID), "HCM_Setup_RM", 3);
                #endregion

                _Data.IsActive = false;
                _Data.ModifiedBy = UserKey;
                _Data.ModifiedDate = DateTime.Now;
                context.SaveChanges();
                Success("Repair Maintainance Deleted Successfully.");
                ResetControls();

            }
        }
        catch (Exception ex)
        {

            Error(ex.Message);
        }

    }

    protected void ddlgroupSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        int GroupId = ddlgroupSearch.SelectedValue == "" ? 0 : Convert.ToInt32(ddlgroupSearch.SelectedValue);
        DataSet ds = CommonHelper.Get_Control_Data_EmployeeSearchFilter("OnChangeGroup", GroupId, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, UserKey);
        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables.Count == 1)
            {
                CommonHelper.BindDropDown(ddlCompany, ds.Tables[0], "Value", "Id", false, false);
            }
        }
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        int CompanyId = 0;
        if (ddlRMTypeSearch.SelectedValue == "0")
        {
            CompanyId = ddlCompany.SelectedValue == "" ? 0 : Convert.ToInt32(ddlCompany.SelectedValue);
            var _JobCategoryata = context.Setup_Category.Where(x => x.CompanyId == CompanyId && x.IsActive == true).OrderBy(x => x.CategoryName).Select(x => new { Id = x.CategoryId, Value = x.CategoryName }).ToList();
            CommonHelper.BindDropDown(ddlJobCategorySearch, _JobCategoryata, "Value", "Id", _JobCategoryata.Count == 1 ? false : true, false);
            CommonHelper.BindDropDown(ddlEmployeeSearch, null, "", "", true, false);
        }
        else if (ddlRMTypeSearch.SelectedValue == "1")
        {
            CompanyId = ddlCompany.SelectedValue == "" ? 0 : Convert.ToInt32(ddlCompany.SelectedValue);
            var _EmployeeListData = context.Setup_Employee.Where(x => x.CompanyId == CompanyId && x.IsActive == true).OrderBy(x => x.EmployeeId).Select(x => new { Id = x.EmployeeId, Value =x.EmployeeCode+"_"+ x.FirstName + " " + x.MiddleName + " " + x.LastName }).ToList();
            CommonHelper.BindDropDown(ddlEmployeeSearch, _EmployeeListData, "Value", "Id", _EmployeeListData.Count == 1 ? false : true, false);
            CommonHelper.BindDropDown(ddlJobCategorySearch, null, "", "", true, false);

        }
        else
        {
            CompanyId = ddlCompany.SelectedValue == "" ? 0 : Convert.ToInt32(ddlCompany.SelectedValue);
            var _JobCategoryata = context.Setup_Category.Where(x => x.CompanyId == CompanyId && x.IsActive == true).OrderBy(x => x.CategoryName).Select(x => new { Id = x.CategoryId, Value = x.CategoryName }).ToList();
            CommonHelper.BindDropDown(ddlJobCategorySearch, _JobCategoryata, "Value", "Id", _JobCategoryata.Count == 1 ? false : true, false);


            var _EmployeeListData = context.Setup_Employee.Where(x => x.CompanyId == CompanyId && x.IsActive == true).OrderBy(x => x.EmployeeId).Select(x => new { Id = x.EmployeeId, Value = x.FirstName + " " + x.MiddleName + " " + x.LastName }).ToList();
            CommonHelper.BindDropDown(ddlEmployeeSearch, _EmployeeListData, "Value", "Id", _EmployeeListData.Count == 1 ? false : true, false);

        }

    }

    protected void ddlGroupAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        int GroupId = ddlGroupAdd.SelectedValue == "" ? 0 : Convert.ToInt32(ddlGroupAdd.SelectedValue);
        DataSet ds = CommonHelper.Get_Control_Data_EmployeeSearchFilter("OnChangeGroup", GroupId, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, UserKey);
        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables.Count == 1)
            {
                CommonHelper.BindDropDown(ddlCompanyAdd, ds.Tables[0], "Value", "Id", false, false);
            }
        }
    }

    protected void ddlCompanyAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        int CompanyId = 0;
        if (ddlRMTypeAdd.SelectedValue == "0")
        {
            CompanyId = ddlCompanyAdd.SelectedValue == "" ? 0 : Convert.ToInt32(ddlCompanyAdd.SelectedValue);
            var _JobCategoryata = context.Setup_Category.Where(x => x.CompanyId == CompanyId && x.IsActive == true).OrderBy(x => x.CategoryName).Select(x => new { Id = x.CategoryId, Value = x.CategoryName }).ToList();
            CommonHelper.BindDropDown(ddlJobCategoryAdd, _JobCategoryata, "Value", "Id", _JobCategoryata.Count == 1 ? false : true, false);
            CommonHelper.BindDropDown(ddlEmployeeAdd, null, "", "", true, false);
        }
        else if (ddlRMTypeAdd.SelectedValue == "1")
        {
            CompanyId = ddlCompanyAdd.SelectedValue == "" ? 0 : Convert.ToInt32(ddlCompanyAdd.SelectedValue);
            var _EmployeeListData = context.Setup_Employee.Where(x => x.CompanyId == CompanyId && x.IsActive == true).OrderBy(x => x.EmployeeId).Select(x => new { Id = x.EmployeeId, Value = x.EmployeeCode+"_"+ x.FirstName + " " + x.MiddleName + " " + x.LastName }).ToList();
            CommonHelper.BindDropDown(ddlEmployeeAdd, _EmployeeListData, "Value", "Id", _EmployeeListData.Count == 1 ? false : true, false);
            CommonHelper.BindDropDown(ddlJobCategoryAdd, null, "", "", true, false);

        }



    }
    public bool CheckAlreadyNameExists(int? id, bool isFixed)
    {
        int ModalId = 0;
        HCM_Setup_RM obj = null;
        //City ocity = context.Cities.FirstOrDefault(c => c.CityName == cityname);
        if (hfModalId.Value != "")
        {
            ModalId = Convert.ToInt32(hfModalId.Value);
        }
        else
        {
            ModalId = 0;
        }
        //City ocity = context.Cities.FirstOrDefault(c => c.CityName == cityname);
        if (isFixed)
            obj = context.HCM_Setup_RM.FirstOrDefault(j => j.EmployeeId == id && j.IsActive == true && j.SetupRMId != ModalId);
        else if (!isFixed)
            obj = context.HCM_Setup_RM.FirstOrDefault(j => j.CategoryId == id && j.IsActive == true && j.SetupRMId != ModalId);

        if (obj != null)
        {
            return true;
        }
        return false;
    }
    private void Save()
    {
        int? _ID = null;

        if (ddlRMTypeAdd.SelectedValue == "0")
            _ID = Convert.ToInt32(ddlJobCategoryAdd.SelectedValue);
        else if (ddlRMTypeAdd.SelectedValue == "1")
            _ID = Convert.ToInt32(ddlEmployeeAdd.SelectedValue);


        if (!CheckAlreadyNameExists(_ID,ddlRMTypeAdd.SelectedValue == "0" ? false : true))
        {
            HCM_Setup_RM obj = new HCM_Setup_RM();
            if (ddlRMTypeAdd.SelectedValue == "0")
            { 
                obj.CategoryId = Convert.ToInt32(ddlJobCategoryAdd.SelectedValue); obj.IsFixed = false;
            }
            else if (ddlRMTypeAdd.SelectedValue == "1")
            { obj.EmployeeId = Convert.ToInt32(ddlEmployeeAdd.SelectedValue); obj.IsFixed = true; }
                


            obj.FuelInLitres = Convert.ToDouble(txtFuelInLitresAdd.Text);
            obj.RM_FirstYear = Convert.ToDouble(txtRM_FirstYearAdd.Text);
            obj.RM_SecondYear = Convert.ToDouble(txtRM_SecondYearAdd.Text);
            obj.RM_ThirdYear = Convert.ToDouble(txtRM_ThirdYearAdd.Text);
            obj.RM_ForthYear = Convert.ToDouble(txtRM_ForthYearAdd.Text);
            obj.RM_FifthYear = Convert.ToDouble(txtRM_FifthYearAdd.Text);

            obj.IsActive = true;
            obj.CreatedBy = UserId;
            obj.CreatedDate = DateTime.Now;
            context.HCM_Setup_RM.Add(obj);
            context.SaveChanges();
            Success("Saved Successfully");
            ClosePopup();
            BindRepeater();
            ResetControls();
        }
        else
        {
            Error("Already Exist");

        }
    }
    private void Update()
    {

        int _RMPrimaryID = Convert.ToInt32(hfModalId.Value);
        int? _ID = null;

        if (ddlRMTypeAdd.SelectedValue == "0")
            _ID = Convert.ToInt32(ddlJobCategoryAdd.SelectedValue);
        else if (ddlRMTypeAdd.SelectedValue == "1")
            _ID = Convert.ToInt32(ddlEmployeeAdd.SelectedValue);

        if (!CheckAlreadyNameExists(_ID, ddlRMTypeAdd.SelectedValue == "0" ? false : true))
        {
            HCM_Setup_RM obj = context.HCM_Setup_RM.Where(a => a.SetupRMId == _RMPrimaryID).FirstOrDefault();

            if (ddlRMTypeAdd.SelectedValue == "0")
            {
                obj.CategoryId = Convert.ToInt32(ddlJobCategoryAdd.SelectedValue);
                obj.EmployeeId = null;
                obj.IsFixed = false;
            }
            else if (ddlRMTypeAdd.SelectedValue == "1")
            { obj.EmployeeId = Convert.ToInt32(ddlEmployeeAdd.SelectedValue); obj.CategoryId = null; obj.IsFixed = true; }

            #region Audit Logs
            //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
            DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(_RMPrimaryID), "HCM_Setup_RM", 3);
            #endregion

            obj.FuelInLitres = Convert.ToDouble(txtFuelInLitresAdd.Text);
            obj.RM_FirstYear = Convert.ToDouble(txtRM_FirstYearAdd.Text);
            obj.RM_SecondYear = Convert.ToDouble(txtRM_SecondYearAdd.Text);
            obj.RM_ThirdYear = Convert.ToDouble(txtRM_ThirdYearAdd.Text);
            obj.RM_ForthYear = Convert.ToDouble(txtRM_ForthYearAdd.Text);
            obj.RM_FifthYear = Convert.ToDouble(txtRM_FifthYearAdd.Text);

            obj.IsActive = true;
            obj.ModifiedBy = UserId;
            obj.ModifiedDate = DateTime.Now;

            context.SaveChanges();

            Success("Updated Successfully");
            ClosePopup();
            BindRepeater();

            ResetControls();
        }
        else
        {
            Error("Already Exist");

        }
    }
    protected void ddlRMTypeAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRMTypeAdd.SelectedValue == "0")
        {
            dvJobCatgeoryAdd.Visible = true;
            dvEmployeeAdd.Visible = false;

            RequiredFieldValidator4.Enabled = true;
            RequiredFieldValidator13.Enabled = true;

            RequiredFieldValidator1.Enabled = false;
            RequiredFieldValidator14.Enabled = false;

        }
        else if (ddlRMTypeAdd.SelectedValue == "1")
        {
            dvJobCatgeoryAdd.Visible = false;
            dvEmployeeAdd.Visible = true;

            RequiredFieldValidator4.Enabled = false;
            RequiredFieldValidator13.Enabled = false;

            RequiredFieldValidator1.Enabled = true;
            RequiredFieldValidator14.Enabled = true;

        }

        BindDropdownAdd();


    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {

        try
        {
            //if (!CheckAlreadyNameExists(Convert.ToInt32(ddlCompanyAdd.SelectedValue), Convert.ToInt32(ddlAllowance.SelectedValue), Convert.ToInt32(txtMinYear.Text)
            //    , Convert.ToInt32(txtMaxYear.Text), Convert.ToDouble(txtDays.Text), Convert.ToDouble(txtFactor.Text)))
            if (hfModalId.Value == string.Empty)
            {
                Save();
            }
            else
            {
                Update();
            }
        }
        catch (Exception ex)
        {
            Error(ex.ToString());
        }

    }

    public void Success(string message)
    {
        message = "AlertBox('Success!','" + message + "','success');";
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
    public void OpenPopupIncrease()
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "OpenPopupIncrease()", "OpenPopupIncrease();", true);
    }
    protected void ddlRMTypeSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRMTypeSearch.SelectedValue == "0")
        {
            dvSearchForJobCategory.Visible = true;
            dvSearchForEmployee.Visible = false;
        }
        else if (ddlRMTypeSearch.SelectedValue == "1")
        {
            dvSearchForJobCategory.Visible = false;
            dvSearchForEmployee.Visible = true;
        }
        else
        {
            dvSearchForJobCategory.Visible = true;
            dvSearchForEmployee.Visible = true;
        }
        BindDropdownSearch();
    }


    private void BindDropdownSearch()
    {
        var li = context.Setup_Group.Where(a => a.IsActive == true).ToList();
        CommonHelper.BindDropDown(ddlgroupSearch, li, "GroupName", "GroupId", li.Count == 1 ? false : true, false);
        ddlgroupSearch_SelectedIndexChanged(null, null);
        ddlCompany_SelectedIndexChanged(null, null);


        // ddlGroupAdd_SelectedIndexChanged(null, null);

    }

    private void ResetControls()
    {

        BindDropdownSearch();
        BindDropdownAdd();
        rpt.PageIndex = 0;
        BindRepeater();


        hfModalId.Value = "";
        //ddlAllowance.SelectedValue = "0";
        //ddlCompanyAdd.SelectedValue = "0";
        txtFuelInLitresAdd.Text = "";
        txtRM_FirstYearAdd.Text = "";
        txtRM_SecondYearAdd.Text = "";
        txtRM_ThirdYearAdd.Text = "";
        txtRM_ForthYearAdd.Text = "";
        txtRM_FifthYearAdd.Text = "";
        txtIncreasePerc.Text = "";
        txtIncreaseDate.Text = "";
    }

    private void BindDropdownAdd()
    {
        var li = context.Setup_Group.Where(a => a.IsActive == true).ToList();
        CommonHelper.BindDropDown(ddlGroupAdd, li, "GroupName", "GroupId", li.Count == 1 ? false : true, false);
        ddlGroupAdd_SelectedIndexChanged(null, null);
        ddlCompanyAdd_SelectedIndexChanged(null, null);

        // ddlGroupAdd_SelectedIndexChanged(null, null);

    }

    private void BindDropdownIncrease()
    {
        var li = context.Setup_Group.Where(a => a.IsActive == true).ToList();
        CommonHelper.BindDropDown(ddlGroupIncrease, li, "GroupName", "GroupId", li.Count == 1 ? false : true, false);
        ddlGroupIncrease_SelectedIndexChanged(null, null);
        ddlCompanyIncrease_SelectedIndexChanged(null, null);

        // ddlGroupAdd_SelectedIndexChanged(null, null);

    }

    private void BindRepeater()
    {
        int? GroupId = null;
        int? CompanyId = null;
        int? EmployeeId = null;
        int? CategoryID = null;



        if (Convert.ToInt32(ddlEmployeeSearch.SelectedValue) > 0)
            EmployeeId = Convert.ToInt32(ddlEmployeeSearch.SelectedValue);


        if (Convert.ToInt32(ddlgroupSearch.SelectedValue) > 0)
            GroupId = Convert.ToInt32(ddlgroupSearch.SelectedValue);


        if (Convert.ToInt32(ddlCompany.SelectedValue) > 0)
            CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);

        if (Convert.ToInt32(ddlJobCategorySearch.SelectedValue) > 0)
            CategoryID = Convert.ToInt32(ddlJobCategorySearch.SelectedValue);


        DataTable _Data = GetData(CompanyId, EmployeeId, CategoryID, Convert.ToInt32(ddlRMTypeSearch.SelectedValue));

        if (_Data.Rows.Count > 0)
        {
            rpt.DataSource = _Data;
            rpt.DataBind();
        }
        else
        {

            rpt.DataSource = null;
            rpt.DataBind();
        }



    }



    private DataTable GetData(int? companyId, int? employeeId, int? categoryId, int? rmType, int rmSetupId = 0)
    {

        DataTable dt = new DataTable();
        string dbConnectionString = context.Database.Connection.ConnectionString;
        SqlConnection con = new SqlConnection(dbConnectionString);
        SqlDataAdapter da = new SqlDataAdapter("SP_HCM_Get_RM_Data", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.CommandTimeout = Int32.MaxValue;

        da.SelectCommand.Parameters.Add("@RMType", SqlDbType.Int).Value = rmType;
        da.SelectCommand.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = employeeId;
        da.SelectCommand.Parameters.Add("@CatgeoryId", SqlDbType.Int).Value = categoryId;
        da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyId;
        da.SelectCommand.Parameters.Add("@RMSetupID", SqlDbType.Int).Value = rmSetupId;


        da.Fill(dt);


        return dt;
    }


    protected void rpt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (ddlRMTypeSearch.SelectedValue == "0")
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = true;
        }
        else if (ddlRMTypeSearch.SelectedValue == "1")
        {
            e.Row.Cells[1].Visible = true;
            e.Row.Cells[2].Visible = false;
        }
        else
        {
            e.Row.Cells[1].Visible = true;
            e.Row.Cells[2].Visible = true;
        }
    }

    protected void btnIncreasePercentage_Click(object sender, EventArgs e)
    {
       
       
        try
        {
            int _CompanyId =Convert.ToInt32(ddlCompanyIncrease.SelectedValue);
            int _FixedID =Convert.ToInt32(ddlRMTypeIncrease.SelectedValue);
            int _CategoryID =Convert.ToInt32(ddlJobCategoryIncrease.SelectedValue);
            double _IncreasePercentage=Convert.ToDouble(txtIncreasePerc.Text);
            DateTime _IncreaseDate = Convert.ToDateTime(txtIncreaseDate.Text);
            int _UserID = UserId;

            if (_CompanyId > 0)
            {
                var _Data = context.HCM_IncreaseRM(_CompanyId, _FixedID, _CategoryID, _IncreasePercentage, _IncreaseDate, _UserID);

                Success("Repair Maintainance Updated Successfully");
            }
            else
            {
                Error("Select Company.");
            }
            //if (ddlRMTypeIncrease.SelectedValue == "0")
            //{
            //    var OBJ = context.HCM_Setup_RM.Where(f => f.IsFixed == false && f.IsActive == true).ToList();

            //    OBJ.ForEach(a => { a.IncreasePercentage = _IncreasePercentage; a.IncreaseDate = _IncreaseDate; });
            //    context.SaveChanges();

            //    Success("Mark Increase Percentage Against Not Fixed Rows");
            //}
            //else if (ddlRMTypeIncrease.SelectedValue == "1")
            //{
            //    var OBJ = context.HCM_Setup_RM.Where(f => f.IsFixed == true && f.IsActive == true).ToList();

            //    OBJ.ForEach(a => { a.IncreasePercentage = _IncreasePercentage; a.IncreaseDate = _IncreaseDate; });
            //    context.SaveChanges();

            //    Success("Mark Increase Percentage Against Fixed Rows");
            //}
            //else if (ddlRMTypeIncrease.SelectedValue == "2")
            //{
            //    var OBJ = context.HCM_Setup_RM.Where(x=>x.IsActive == true).ToList();

            //    OBJ.ForEach(a => { a.IncreasePercentage = _IncreasePercentage; a.IncreaseDate = _IncreaseDate; });
            //    context.SaveChanges();

            //    Success("Mark Increase Percentage Against Fixed And Not Fixed Rows");
            //}

            ResetControls();
        }
        catch (Exception ex)
        {

            Error(ex.Message);
        }
    }

    protected void btnShowIncreasePopup_Click(object sender, EventArgs e)
    {
        OpenPopupIncrease();
    }

    protected void ddlGroupIncrease_SelectedIndexChanged(object sender, EventArgs e)
    {
        int GroupId = ddlGroupIncrease.SelectedValue == "" ? 0 : Convert.ToInt32(ddlGroupIncrease.SelectedValue);
        DataSet ds = CommonHelper.Get_Control_Data_EmployeeSearchFilter("OnChangeGroup", GroupId, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, UserKey);
        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables.Count == 1)
            {
                CommonHelper.BindDropDown(ddlCompanyIncrease, ds.Tables[0], "Value", "Id", false, false);
            }
        }
    }

    protected void ddlCompanyIncrease_SelectedIndexChanged(object sender, EventArgs e)
    {
        int CompanyId = 0;
        if (ddlRMTypeIncrease.SelectedValue == "0")
        {
            CompanyId = ddlCompanyIncrease.SelectedValue == "" ? 0 : Convert.ToInt32(ddlCompanyIncrease.SelectedValue);
            var _JobCategoryata = context.Setup_Category.Where(x => x.CompanyId == CompanyId && x.IsActive == true).OrderBy(x => x.CategoryName).Select(x => new { Id = x.CategoryId, Value = x.CategoryName }).ToList();
            CommonHelper.BindDropDown(ddlJobCategoryIncrease, _JobCategoryata, "Value", "Id", _JobCategoryata.Count == 1 ? false : true, false);
           
        }
        else if (ddlRMTypeIncrease.SelectedValue == "1")
        {
            CompanyId = ddlCompanyIncrease.SelectedValue == "" ? 0 : Convert.ToInt32(ddlCompanyIncrease.SelectedValue);
            var _EmployeeListData = context.Setup_Employee.Where(x => x.CompanyId == CompanyId && x.IsActive == true).OrderBy(x => x.EmployeeId).Select(x => new { Id = x.EmployeeId, Value = x.FirstName + " " + x.MiddleName + " " + x.LastName }).ToList();
           
            CommonHelper.BindDropDown(ddlJobCategoryIncrease, null, "", "", true, false);

        }
        else
        {
            CompanyId = ddlCompanyIncrease.SelectedValue == "" ? 0 : Convert.ToInt32(ddlCompanyIncrease.SelectedValue);
            var _JobCategoryata = context.Setup_Category.Where(x => x.CompanyId == CompanyId && x.IsActive == true).OrderBy(x => x.CategoryName).Select(x => new { Id = x.CategoryId, Value = x.CategoryName }).ToList();
            CommonHelper.BindDropDown(ddlJobCategoryIncrease, _JobCategoryata, "Value", "Id", _JobCategoryata.Count == 1 ? false : true, false);


        }
    }
}