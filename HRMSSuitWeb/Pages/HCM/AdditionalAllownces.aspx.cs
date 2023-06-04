using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_HCM_AdditionalAllownces : Base
{
    Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropdown();
            BindRepeater();

          
           


        }
    }
    private void BindDropdown()
    {
        var li = context.Setup_Group.Where(a => a.IsActive == true).ToList();
        CommonHelper.BindDropDown(ddlgroupSearch, li, "GroupName", "GroupId", li.Count == 1 ? false : true, false);
        CommonHelper.BindDropDown(ddlGroupAdd, li, "GroupName", "GroupId", li.Count == 1 ? false : true, false);
        ddlgroupSearch_SelectedIndexChanged(null, null);
        ddlGroupAdd_SelectedIndexChanged(null, null);
        ddlCompanyAdd_SelectedIndexChanged(null, null);
        ddlCompany_SelectedIndexChanged(null, null);
        // ddlGroupAdd_SelectedIndexChanged(null, null);

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
        int CompanyId = ddlCompany.SelectedValue == "" ? 0 : Convert.ToInt32(ddlCompany.SelectedValue);
        var _EmployeeListData = context.Setup_Employee.Where(x => x.CompanyId == CompanyId && x.IsActive == true).OrderBy(x => x.EmployeeId).Select(x => new { Id = x.EmployeeId, Value = x.EmployeeCode+"_"+x.FirstName + " " + x.MiddleName + " " + x.LastName }).ToList();
        CommonHelper.BindDropDown(ddlEmployeeSearch, _EmployeeListData, "Value", "Id", _EmployeeListData.Count == 1 ? false : true, false);


        var _AllownceData = context.HCM_Setup_Allowance.Where(x => x.CompanyId == CompanyId && x.IsActive == true).OrderBy(x => x.AllowanceName).Select(x => new { Id = x.AllowanceID, Value = x.AllowanceName }).ToList();
        CommonHelper.BindDropDown(ddlAllownces, _AllownceData, "Value", "Id", _AllownceData.Count == 1 ? false : true, false);
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
        int CompanyId = ddlCompanyAdd.SelectedValue == "" ? 0 : Convert.ToInt32(ddlCompanyAdd.SelectedValue);
        var _EmployeeListData = context.Setup_Employee.Where(x => x.CompanyId == CompanyId && x.IsActive == true).OrderBy(x => x.EmployeeId).Select(x => new { Id = x.EmployeeId, Value = x.EmployeeCode+"_"+x.FirstName + " " + x.MiddleName + " " + x.LastName }).ToList();
        CommonHelper.BindDropDown(ddlEmployeeAdd, _EmployeeListData, "Value", "Id", _EmployeeListData.Count == 1 ? false : true, false);


        var _AllownceData = context.HCM_Setup_Allowance.Where(x => x.CompanyId == CompanyId && x.IsActive == true).OrderBy(x => x.AllowanceName).Select(x => new { Id = x.AllowanceID, Value = x.AllowanceName }).ToList();
        CommonHelper.BindDropDown(ddlAllowance, _AllownceData, "Value", "Id", _AllownceData.Count == 1 ? false : true, false);
        OpenPopup();
    }

    public bool CheckAlreadyNameExists(int employeeId, int AllowanceId, int month, int year)
    {
        int ModalId = 0;
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

        HCM_Additional_Allowances obj = context.HCM_Additional_Allowances.FirstOrDefault(j => j.EmployeeId == employeeId && j.AllowanceId == AllowanceId && j.IsActive == true && j.AdditonalAllowynceID != ModalId && j.Month.Value.Month == month && j.Month.Value.Year == year);

        if (obj != null)
        {
            return true;
        }
        return false;
    }
    private void Save()
    {
        if (!CheckAlreadyNameExists(Convert.ToInt32(ddlEmployeeAdd.SelectedValue), Convert.ToInt32(ddlAllowance.SelectedValue), Convert.ToDateTime(txtMonthAdd.Text).Month, Convert.ToDateTime(txtMonthAdd.Text).Year))
        { 
            HCM_Additional_Allowances obj = new HCM_Additional_Allowances();
            obj.EmployeeId = Convert.ToInt32(ddlEmployeeAdd.SelectedValue);
            obj.AllowanceId = Convert.ToInt32(ddlAllowance.SelectedValue);
            obj.Amount = Convert.ToDouble(txtAmountAdd.Text);
            obj.Month = Convert.ToDateTime(txtMonthAdd.Text);

            obj.IsActive = true;
            obj.CreatedBy = UserId;
            obj.CreatedDate = DateTime.Now;
            context.HCM_Additional_Allowances.Add(obj);
            context.SaveChanges();

            #region Audit Logs
            //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
            DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(obj.AdditonalAllowynceID), "HCM_Additional_Allowances", 1);
            #endregion

            Success("Saved Successfully");
            ClosePopup();
            BindRepeater();
            hfModalId.Value = "";
            txtAmountAdd.Text = "";
            //ResetControls();
            OpenPopup();
            
        }
        else
        {
            Error("Already Exist");

        }
    }
    private void Update()
    {

        int AdditionalAllownceId = Convert.ToInt32(hfModalId.Value);

        if (!CheckAlreadyNameExists(Convert.ToInt32(ddlEmployeeAdd.SelectedValue), Convert.ToInt32(ddlAllowance.SelectedValue), Convert.ToDateTime(txtMonthAdd.Text).Month, Convert.ToDateTime(txtMonthAdd.Text).Year))
        {
            HCM_Additional_Allowances obj = context.HCM_Additional_Allowances.Where(a => a.AdditonalAllowynceID == AdditionalAllownceId).FirstOrDefault();

            #region Audit Logs
            //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
            DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(AdditionalAllownceId), "HCM_Additional_Allowances", 2);
            #endregion

            obj.EmployeeId = Convert.ToInt32(ddlEmployeeAdd.SelectedValue);
            obj.AllowanceId = Convert.ToInt32(ddlAllowance.SelectedValue);
            obj.Amount = Convert.ToDouble(txtAmountAdd.Text);
            obj.Month = Convert.ToDateTime(txtMonthAdd.Text);
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
    private void BindRepeater()
    {
        int? GroupId = null;
        int? CompanyId = null;
        int? EmployeeId = null;
        int? AllownceId = null;
        DateTime? Month = null;

        if (Convert.ToInt32(ddlEmployeeSearch.SelectedValue) > 0)
            EmployeeId = Convert.ToInt32(ddlEmployeeSearch.SelectedValue);


        if (Convert.ToInt32(ddlgroupSearch.SelectedValue) > 0)
            GroupId = Convert.ToInt32(ddlgroupSearch.SelectedValue);


        if (Convert.ToInt32(ddlCompany.SelectedValue) > 0)
            CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);

        if (Convert.ToInt32(ddlAllownces.SelectedValue) > 0)
            AllownceId = Convert.ToInt32(ddlAllownces.SelectedValue);

        if (!String.IsNullOrEmpty(txtMonthSearch.Text))
            Month = Convert.ToDateTime(txtMonthSearch.Text);


        DataTable _Data = GetData(GroupId, CompanyId, EmployeeId, AllownceId, Month, null);

        if (_Data.Rows.Count > 0)
        {
            rpt.DataSource = _Data;
            rpt.DataBind();

            rpt.UseAccessibleHeader = true;
            //adds <thead> and <tbody> elements
            rpt.HeaderRow.TableSection =
            TableRowSection.TableHeader;
        }
        else
        { 
            rpt.DataSource = null;
            rpt.DataBind();
        } 
    }


    private DataTable GetData(int? groupId, int? companyId, int? employeeId, int? allownceid, DateTime? month, int? additionalAllownceId)
    {

        DataTable dt = new DataTable();
        string dbConnectionString = context.Database.Connection.ConnectionString;
        SqlConnection con = new SqlConnection(dbConnectionString);
        SqlDataAdapter da = new SqlDataAdapter("Sp_HCM_Get_Additional_Allownces", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.CommandTimeout = Int32.MaxValue;

        da.SelectCommand.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = employeeId;
        da.SelectCommand.Parameters.Add("@GroupId", SqlDbType.Int).Value = groupId;
        da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyId;
        da.SelectCommand.Parameters.Add("@AllownceId", SqlDbType.Int).Value = allownceid;
        da.SelectCommand.Parameters.Add("@Month", SqlDbType.DateTime).Value = month;
        da.SelectCommand.Parameters.Add("@AdditionalAllownceId", SqlDbType.Int).Value = additionalAllownceId;




        da.Fill(dt);


        return dt;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        
        ResetControls();
       
       
    }


    private void ResetControls()
    {

        BindDropdown();
        rpt.PageIndex = 0;
        BindRepeater();


        hfModalId.Value = "";
        //ddlAllowance.SelectedValue = "0";
        //ddlCompanyAdd.SelectedValue = "0";
        txtAmountAdd.Text = "";
        txtMonthAdd.Text = "";
        txtMonthSearch.Text = "";
    }



    protected void btnCancel_Click1(object sender, EventArgs e)
    {
        ResetControls();
    }

    protected void btnSearch_Click1(object sender, EventArgs e)
    {

        BindRepeater();


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

    protected void btnAddNewRecord_Click(object sender, EventArgs e)
    {

        ResetControls();
        OpenPopup();

        hfModalId.Value = "";
        ddlAllowance.SelectedValue = "0";
        //ddlCompanyAdd.SelectedValue = "0";
        ddlEmployeeAdd.SelectedValue = "0";
        txtAmountAdd.Text = "";
        txtMonthAdd.Text = "";


    }

    protected void rpt_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        rpt.PageIndex = e.NewPageIndex;
        BindRepeater();
    }


    protected void rpt_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "EditS")
            {

                DataTable lstEdit = GetData(null, null, null, null, null, ID);

                if (lstEdit.Rows.Count > 0)
                {
                    ResetControls();
                    hfModalId.Value = ID.ToString();
                    ddlGroupAdd.SelectedValue = lstEdit.Rows[0]["GroupId"].ToString();
                    //ddlGroupAdd_SelectedIndexChanged(null,null);
                    ddlCompanyAdd.SelectedValue = lstEdit.Rows[0]["CompanyId"].ToString();
                    ///ddlCompanyAdd_SelectedIndexChanged(null,null);
                    ddlEmployeeAdd.SelectedValue = lstEdit.Rows[0]["EmployeeId"].ToString();
                    ddlAllowance.SelectedValue = lstEdit.Rows[0]["AllowanceId"].ToString();
                    txtAmountAdd.Text = lstEdit.Rows[0]["Amount"].ToString();
                    txtMonthAdd.Text = Convert.ToDateTime(lstEdit.Rows[0]["Month"]).ToString("MMM-yyyy");
                    OpenPopup();

                }
                else
                {
                    Error("Record Can Not Edit.Beacuse Record Not Find.");
                }
            }
            else if (e.CommandName == "DeleteS")
            {
                var _Data = context.HCM_Additional_Allowances.FirstOrDefault(x=>x.AdditonalAllowynceID == ID);

                #region Audit Logs
                //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
                DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(ID), "HCM_Additional_Allowances", 3);
                #endregion

                _Data.IsActive = false;
                _Data.ModifiedBy = UserKey;
                _Data.ModifiedDate = DateTime.Now;
                context.SaveChanges();
                Success("Additional Allownce Deleted Successfully.");
                ResetControls();

            }
        }
        catch (Exception ex)
        {

            Error(ex.Message);
        }

    }



    protected void lbEdit_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        DataTable lstEdit = GetData(null, null, null, null, null, Convert.ToInt32(btn.CommandArgument));

        if (lstEdit.Rows.Count > 0)
        {
            ResetControls();
            hfModalId.Value = ID.ToString();
            ddlGroupAdd.SelectedValue = lstEdit.Rows[0]["GroupId"].ToString();
            //ddlGroupAdd_SelectedIndexChanged(null,null);
            ddlCompanyAdd.SelectedValue = lstEdit.Rows[0]["CompanyId"].ToString();
            ///ddlCompanyAdd_SelectedIndexChanged(null,null);
            ddlEmployeeAdd.SelectedValue = lstEdit.Rows[0]["EmployeeId"].ToString();
            ddlAllowance.SelectedValue = lstEdit.Rows[0]["AllowanceId"].ToString();
            txtMonthSearch.Text = lstEdit.Rows[0]["Amount"].ToString();
            txtMonthAdd.Text = Convert.ToDateTime(lstEdit.Rows[0]["Month"]).ToString("yyyy/MM/dd");
        }
    }



    protected void btnCloseModal_Click(object sender, EventArgs e)
    {
        ClosePopup();
    }
}