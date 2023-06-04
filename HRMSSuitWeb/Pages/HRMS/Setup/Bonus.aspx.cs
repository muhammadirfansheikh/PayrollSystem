using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public partial class Pages_HRMS_Setup_Bonus : Base
{
    Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();
    int ConnectionTimeout = Constant.ConnectionTimeout;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGroupDropdown();
        }
    }

    #region Search Panel 

    protected void ddlgroupSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int GroupId = ddlgroupSearch.SelectedValue == "" ? 0 : Convert.ToInt32(ddlgroupSearch.SelectedValue);
            DataSet ds = CommonHelper.Get_Control_Data_EmployeeSearchFilter("OnChangeGroup", GroupId, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, UserKey);
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables.Count == 1)
                {
                    CommonHelper.BindDropDown(ddlcompanySearch, ds.Tables[0], "Value", "Id", true, false);
                }
            }
        }
        catch (Exception ex)
        {
            divError.Visible = true;
            lblError.InnerText = ex.Message.ToString();
        }
    }
    protected void ddlcompanySearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = sp_setup_bonus(Convert.ToInt32(ddlgroupSearch.SelectedValue), Convert.ToInt32(ddlcompanySearch.SelectedValue), 0, null, null, null, 0, 5);
            if (dt.Rows.Count > 0)
            {
                CommonHelper.BindDropDown(ddlBonus, dt, dt.Columns["AllowanceName"].ToString(), dt.Columns["AllowanceID"].ToString(), true, false);
            }
        }
        catch (Exception ex)
        {
            divError.Visible = true;
            lblError.InnerText = ex.Message.ToString();
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ResetControls();
        BindGroupDropdown();
        BindRepeater();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (ddlcompanySearch.SelectedValue == "0")
        {
            divError.Visible = true;
            lblError.InnerText = "Select Company";
            return;
        }
        BindRepeater();
    }

    #endregion

    #region Add 
    protected void Btn_Add_Click(object sender, EventArgs e)
    {
        try
        {
            IsAdd.Value = Convert.ToString(1);
            IsEdit.Value = Convert.ToString(0);
            ResetControls();
            Div_Save.Visible = true;
            OpenPopup();
        }
        catch (Exception ex)
        {
            divError.Visible = true;
            lblError.InnerText = ex.Message.ToString();
        }
    }
    #endregion

    #region Repeater Items
    protected void lblView_Click(object sender, EventArgs e)
    {
        try
        {
        }
        catch (Exception ex)
        {
            divError.Visible = true;
            lblError.InnerText = ex.Message.ToString();
        }
    }
    protected void lbEdit_Click(object sender, EventArgs e)
    {
        try
        {
            IsAdd.Value = Convert.ToString(0);
            IsEdit.Value = Convert.ToString(1);
            BindGroupDropdown();
            LinkButton btnEdit = (LinkButton)sender;
            RepeaterItem rptItem = (RepeaterItem)btnEdit.NamingContainer;
            int BonusId = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfBonusId")).Value);
            HFbonusid.Value = BonusId.ToString();
            int CompanyFormulaId = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfCompanyFormulaId")).Value);
            HFcompanyformulaid.Value = CompanyFormulaId.ToString();
            DataTable dt = new DataTable();
            int companyid = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfCompanyId")).Value);
            dt = sp_setup_bonus(0, companyid, 0, null, null, null, 0, 4, BonusId, CompanyFormulaId);
            if (dt.Rows.Count > 0)
            {
                ddlCompanyAdd.SelectedValue = dt.Rows[0]["CompanyId"].ToString();
                ddlCompanyAdd_SelectedIndexChanged(null, null);
                ddlBonusAdd.SelectedValue = dt.Rows[0]["AllowanceID"].ToString();
                ddlYearAdd.SelectedValue = dt.Rows[0]["YearId"].ToString();
                txtMaxJoiningDate.Text = string.IsNullOrEmpty(dt.Rows[0]["MaxJoining"].ToString()) ? "" : Convert.ToDateTime(dt.Rows[0]["MaxJoining"]).ToString("MM/dd/yyyy");
                txtBonusDate.Text = string.IsNullOrEmpty(dt.Rows[0]["BonusDate"].ToString()) ? "" : Convert.ToDateTime(dt.Rows[0]["BonusDate"]).ToString("MM/dd/yyyy");
                txtReleaseDate.Text = string.IsNullOrEmpty(dt.Rows[0]["ReleaseDate"].ToString()) ? "" : Convert.ToDateTime(dt.Rows[0]["ReleaseDate"]).ToString("MM/dd/yyyy");

                OpenPopup();
            }
            else { Error("No Record Found"); }
        }
        catch (Exception ex)
        {
            divError.Visible = true;
            lblError.InnerText = ex.Message;
        }
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnDelete = (LinkButton)sender;
            RepeaterItem rptItem = (RepeaterItem)btnDelete.NamingContainer;
            int hfBonusId = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfBonusId")).Value);

            DataTable dt = new DataTable();
            dt = sp_setup_bonus(0, 0, 0, null, null, null, 0, 3, hfBonusId, 0);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["ErrorCode"].ToString() == "1")
                { Success(dt.Rows[0]["ErrorMessage"].ToString()); }
                else { Error(dt.Rows[0]["ErrorMessage"].ToString()); }
                BindRepeater();
            }
        }
        catch (Exception ex)
        {
            divError.Visible = true;
            lblError.InnerText = ex.Message.ToString();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        try
        {
            string dtMaxJoiningDate = txtMaxJoiningDate.Text;
            string dtBonusDate = txtBonusDate.Text;
            string dtReleaseDate = txtReleaseDate.Text;

            if (IsAdd.Value == Convert.ToString(1))
            {
                dt = sp_setup_bonus(Convert.ToInt32(ddlGroupAdd.SelectedValue), Convert.ToInt32(ddlCompanyAdd.SelectedValue), Convert.ToInt32(ddlBonusAdd.SelectedValue)
                    , dtMaxJoiningDate, dtBonusDate, dtReleaseDate, Convert.ToInt32(ddlYearAdd.SelectedValue), 1, 0, 0);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["ErrorCode"].ToString() == "1")
                    { Success(dt.Rows[0]["ErrorMessage"].ToString()); }
                    else { Error(dt.Rows[0]["ErrorMessage"].ToString()); }
                }
            }
            if (IsEdit.Value == Convert.ToString(1))
            {
                dt = sp_setup_bonus(Convert.ToInt32(ddlGroupAdd.SelectedValue), Convert.ToInt32(ddlCompanyAdd.SelectedValue), Convert.ToInt32(ddlBonusAdd.SelectedValue), dtMaxJoiningDate, dtBonusDate, dtReleaseDate, Convert.ToInt32(ddlYearAdd.SelectedValue), 2, Convert.ToInt32(HFbonusid.Value), Convert.ToInt32(HFcompanyformulaid.Value));
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["ErrorCode"].ToString() == "1")
                    { Success(dt.Rows[0]["ErrorMessage"].ToString()); }
                    else { Error(dt.Rows[0]["ErrorMessage"].ToString()); }
                }
            }
            ClosePopup();
            BindRepeater();
        }
        catch (Exception ex)
        {
            divError.Visible = true;
            lblError.InnerText = ex.Message.ToString();
        }
    }
    protected void ddlGroupAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int GroupId = ddlGroupAdd.SelectedValue == "" ? 0 : Convert.ToInt32(ddlGroupAdd.SelectedValue);
            DataSet ds = CommonHelper.Get_Control_Data_EmployeeSearchFilter("OnChangeGroup", GroupId, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, UserKey);
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables.Count == 1)
                {
                    CommonHelper.BindDropDown(ddlCompanyAdd, ds.Tables[0], "Value", "Id", true, false);
                }
            }
        }
        catch (Exception ex)
        {
            divError.Visible = true;
            lblError.InnerText = ex.Message.ToString();
        }
    }
    protected void ddlCompanyAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = sp_setup_bonus(Convert.ToInt32(ddlGroupAdd.SelectedValue), Convert.ToInt32(ddlCompanyAdd.SelectedValue), 0, null, null, null, 0, 5);
            if (dt.Rows.Count > 0)
            {
                CommonHelper.BindDropDown(ddlBonusAdd, dt, dt.Columns["AllowanceName"].ToString(), dt.Columns["AllowanceID"].ToString(), true, false);

                DataTable dt1 = sp_setup_bonus(Convert.ToInt32(ddlGroupAdd.SelectedValue), Convert.ToInt32(ddlCompanyAdd.SelectedValue), 0, null, null, null, 0, 7, 0, Convert.ToInt32(dt.Rows[0]["CompanyFormulaID"].ToString()));
                if (dt1.Rows.Count > 0)
                {
                    CommonHelper.BindDropDown(ddlYearAdd, dt1, dt1.Columns["Year"].ToString(), dt1.Columns["YearId"].ToString(), true, false);
                }
            }
        }
        catch (Exception ex)
        {
            divError.Visible = true;
            lblError.InnerText = ex.Message.ToString();
        }
    }
    protected void rpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        string message = "";
        try
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                LinkButton lbEdit = (LinkButton)e.Item.FindControl("lbEdit");
                LinkButton lbDelete = (LinkButton)e.Item.FindControl("lbDelete");
                HiddenField hfIsRelease = (HiddenField)e.Item.FindControl("hfIsRelease");
                if (hfIsRelease.Value.ToString() != "Yes")
                {
                    lbEdit.Enabled = true;
                    //lbEdit.CssClass = lbEdit.CssClass.Replace("btn btn-primary", "");  
                    //lbDelete.CssClass = lbDelete.CssClass.Replace("btn btn-danger", ""); 
                    lbDelete.Enabled = true;
                }
                else
                {
                    lbEdit.CssClass = "btn btn-default";
                    lbEdit.Attributes.Add("color", "#000000");
                    lbDelete.CssClass = "btn btn-default";
                    lbDelete.Attributes.Add("color", "#000000");
                    lbEdit.Enabled = false;
                    lbDelete.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            divError.Visible = true;
            lblError.InnerText = ex.Message.ToString();
        }
    }
    private void BindRepeater()
    {
        DataTable dt = new DataTable();
        try
        {
            dt = sp_setup_bonus(Convert.ToInt32(ddlgroupSearch.SelectedValue), Convert.ToInt32(ddlcompanySearch.SelectedValue), Convert.ToInt32(ddlBonus.SelectedValue), null, null, null, 0, 6);
            if (dt.Rows.Count > 0)
            {
                rpt.DataSource = dt;
                rpt.DataBind();
            }
            else
            {
                rpt.DataSource = null;
                rpt.DataBind();
            }
        }
        catch (Exception ex)
        {
            divError.Visible = true;
            lblError.InnerText = ex.Message.ToString();
        }
    }
    #endregion

    public DataTable sp_setup_bonus(int GroupId, int CompanyId, int? AllowenceId, string MaxJoiningDate, string BonusDate, string ReleaseDate, int YearId, int OperationId, int BonusId = 0, int CompanyFormulaId = 0)
    {
        DataTable dt = new DataTable();
        try
        {
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("HCM_Setup_Bonus", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = ConnectionTimeout;
            da.SelectCommand.Parameters.Add("@GroupId", SqlDbType.Int).Value = GroupId;
            da.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            da.SelectCommand.Parameters.Add("@AllowenceId", SqlDbType.Int).Value = AllowenceId == 0 ? null : AllowenceId;
            da.SelectCommand.Parameters.Add("@MaxJoiningDate", SqlDbType.Date).Value = MaxJoiningDate == "" ? null : MaxJoiningDate;
            da.SelectCommand.Parameters.Add("@BonusDate", SqlDbType.Date).Value = BonusDate == "" ? null : BonusDate;
            da.SelectCommand.Parameters.Add("@ReleaseDate", SqlDbType.Date).Value = ReleaseDate == "" ? null : ReleaseDate;
            da.SelectCommand.Parameters.Add("@YearId", SqlDbType.Int).Value = YearId;
            da.SelectCommand.Parameters.Add("@OperationId", SqlDbType.Int).Value = OperationId;
            da.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = UserKey;
            da.SelectCommand.Parameters.Add("@BonusId", SqlDbType.Int).Value = BonusId;
            da.SelectCommand.Parameters.Add("@CompanyFormulaId", SqlDbType.Int).Value = CompanyFormulaId;
            da.Fill(dt);
        }
        catch (Exception ex)
        {
            Error(ex.Message.ToString());
        }

        return dt;
    }
    private void BindGroupDropdown()
    {
        try
        {
            var li = context.Setup_Group.Where(a => a.IsActive == true).ToList();
            CommonHelper.BindDropDown(ddlgroupSearch, li, "GroupName", "GroupId", li.Count == 1 ? false : true, false);
            CommonHelper.BindDropDown(ddlGroupAdd, li, "GroupName", "GroupId", li.Count == 1 ? false : true, false);
            ddlgroupSearch_SelectedIndexChanged(null, null);
            ddlGroupAdd_SelectedIndexChanged(null, null);
        }
        catch (Exception ex)
        {
            divError.Visible = true;
            lblError.InnerText = ex.Message.ToString();
        }

    }
    public void OpenPopup()
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "OpenPopup()", "OpenPopup();", true);
    }

    public void ClosePopup()
    {
        ResetControls();
        ScriptManager.RegisterStartupScript(this, GetType(), "ClosePopup()", "ClosePopup();", true);
        BindRepeater();
    }

    private void ResetControls()
    {
        divError.Visible = false;
        lblError.InnerText = "";
        txtMaxJoiningDate.Text = string.Empty;
        txtBonusDate.Text = string.Empty;
        txtReleaseDate.Text = string.Empty;
        hfModalId.Value = "";
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
}