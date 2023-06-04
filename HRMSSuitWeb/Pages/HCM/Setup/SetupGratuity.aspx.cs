using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Data;

public partial class Pages_HCM_Setup_SetupGratuity : Base
{
    Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropdown();
        }
    }
    private void BindDropdown()
    {
        var li = context.Setup_Group.Where(a => a.IsActive == true).ToList();
        CommonHelper.BindDropDown(ddlgroupSearch, li, "GroupName", "GroupId", li.Count == 1 ? false : true, false);
        CommonHelper.BindDropDown(ddlGroupAdd, li, "GroupName", "GroupId", li.Count == 1 ? false : true, false);
        ddlgroupSearch_SelectedIndexChanged(null, null);
        ddlGroupAdd_SelectedIndexChanged(null, null);
    }
    protected void ddlgroupSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        int GroupId = ddlgroupSearch.SelectedValue == "" ? 0 : Convert.ToInt32(ddlgroupSearch.SelectedValue);
        DataSet ds = CommonHelper.Get_Control_Data_EmployeeSearchFilter("OnChangeGroup", GroupId, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, UserKey);
        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables.Count == 1)
            {
                CommonHelper.BindDropDown(ddlCompany, ds.Tables[0], "Value", "Id", true, false);
            }
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
                CommonHelper.BindDropDown(ddlCompanyAdd, ds.Tables[0], "Value", "Id", true, false);
                ddlCompanyAdd_SelectedIndexChanged(null, null);
            }
        }
    }
    protected void ddlCompanyAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        int CompanyId = Convert.ToInt32(ddlCompanyAdd.SelectedValue);
        var lstAllowance = context.HCM_Setup_Allowance.Where(a => a.IsActive == true && a.CompanyId == CompanyId)
           //.Select(a => new
           //{
           //    AllowanceID = a.AllowanceID,
           //    AllowanceName = a.AllowanceName
           //})
           .ToList();
        if (lstAllowance.Count > 0)
        {
            CommonHelper.BindDropDown(ddlAllowance, lstAllowance, "AllowanceName", "AllowanceID", true, false);
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ResetControls();
        rpt.DataSource = null;
        rpt.DataBind();
        ddlCompany.SelectedValue = "0";
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindRepeater();
    }
    private void BindRepeater()
    {
        int CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);

        var lst = context.HCM_Setup_Gratuity.Where(a => a.IsActive == true)
        .Where(a => a.CompanyId == CompanyId)
          .Select(a => new
          {
              ID = a.SetupGratuityId,
              CompanyId = a.CompanyId,
              AllowanceId = a.AllowanceId,
              Company = a.Setup_Company.CompanyName,
              MinYear = a.MinYear,
              MaxYear = a.MaxYear,
              Days = a.AmountPerDays,
              Factor = a.MultiplyFactor,
              Allowance = a.HCM_Setup_Allowance.AllowanceName,
          })
          .ToList();
        rpt.DataSource = lst;
        rpt.DataBind();
    }

    protected void lbEdit_Click(object sender, EventArgs e)
    {
        LinkButton btnEdit = (LinkButton)sender;
        RepeaterItem rptItem = (RepeaterItem)btnEdit.NamingContainer;
        int ID = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfId")).Value);
        var lstEdit = context.HCM_Setup_Gratuity.Where(x => x.SetupGratuityId == ID).FirstOrDefault();

        if (lstEdit != null)
        {
            //ResetControls();
            hfModalId.Value = ID.ToString();
            var lst = context.HCM_Setup_Gratuity.Where(a => a.SetupGratuityId == ID).FirstOrDefault();

            if (lst != null)
            {
                ddlCompanyAdd.SelectedValue = lst.CompanyId.ToString();
                ddlgroupSearch_SelectedIndexChanged(null, null);
                ddlGroupAdd_SelectedIndexChanged(null, null);  
                ddlAllowance.SelectedValue = lst.AllowanceId.ToString();
                txtDays.Text = Convert.ToString(lst.AmountPerDays);
                txtFactor.Text = Convert.ToString(lst.MultiplyFactor);
                txtMaxYear.Text = Convert.ToString(lst.MaxYear);
                txtMinYear.Text = Convert.ToString(lst.MinYear);
            }

            OpenPopup();

        }
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnDelete = (LinkButton)sender;
            RepeaterItem rptItem = (RepeaterItem)btnDelete.NamingContainer;

            int Id = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfId")).Value);

            divError.Visible = false;
            HCM_Setup_Gratuity obj = context.HCM_Setup_Gratuity.FirstOrDefault(j => j.SetupGratuityId == Id);
            if (obj != null)
            {
                #region Audit Logs
                //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
                DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(Id), "HCM_Setup_Gratuity", 3);
                #endregion

                DateTime dt = DateTime.Now;
                obj.IsActive = false;
                obj.ModifiedBy = UserKey;
                obj.ModifiedDate = dt;
                context.SaveChanges();
                //MessageCtrl.showMessageBox("Education Score has been deleted successfully.", MessageType.Success);
                Success("Deleted successfully.");

                BindRepeater();

            }
            else
            {
                Error("Already Exist against.");
            }
        }
        catch (Exception ex)
        {
            divError.Visible = true;
            lblError.InnerText = ex.Message;
        }
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
    private void ResetControls()
    {
        hfModalId.Value = "";
        ddlAllowance.SelectedValue = "0";
        ddlCompanyAdd.SelectedValue = "0";
        txtDays.Text = string.Empty;
        txtFactor.Text = string.Empty;
        txtMaxYear.Text = string.Empty;
        txtMinYear.Text = string.Empty;
    }
    private void Save()
    {
        if (!CheckAlreadyNameExists(Convert.ToInt32(ddlCompanyAdd.SelectedValue), Convert.ToInt32(ddlAllowance.SelectedValue), Convert.ToInt32(txtMinYear.Text)
            , Convert.ToInt32(txtMaxYear.Text), Convert.ToDouble(txtDays.Text), Convert.ToDouble(txtFactor.Text)))
        {
            HCM_Setup_Gratuity obj = new HCM_Setup_Gratuity();
            obj.CompanyId = Convert.ToInt32(ddlCompanyAdd.SelectedValue);
            obj.AllowanceId = Convert.ToInt32(ddlAllowance.SelectedValue);
            obj.MinYear = Convert.ToInt32(txtMinYear.Text);
            obj.MaxYear = Convert.ToInt32(txtMaxYear.Text);
            obj.AmountPerDays = Convert.ToDouble(txtDays.Text);
            obj.MultiplyFactor = Convert.ToDouble(txtFactor.Text);
            obj.IsActive = true;
            obj.CreatedBy = UserId;
            obj.CreatedDate = DateTime.Now;
            context.HCM_Setup_Gratuity.Add(obj);
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

        int SetupGratuityId = Convert.ToInt32(hfModalId.Value);

        if (!CheckAlreadyNameExists(Convert.ToInt32(ddlCompanyAdd.SelectedValue), Convert.ToInt32(ddlAllowance.SelectedValue), Convert.ToInt32(txtMinYear.Text)
            , Convert.ToInt32(txtMaxYear.Text), Convert.ToDouble(txtDays.Text), Convert.ToDouble(txtFactor.Text)))
        {
            HCM_Setup_Gratuity obj = context.HCM_Setup_Gratuity.Where(a => a.SetupGratuityId == SetupGratuityId).FirstOrDefault();


            #region Audit Logs
            //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
            DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(SetupGratuityId), "HCM_Setup_Gratuity", 2);
            #endregion


            obj.CompanyId = Convert.ToInt32(ddlCompanyAdd.SelectedValue);
            obj.AllowanceId = Convert.ToInt32(ddlAllowance.SelectedValue);
            obj.MinYear = Convert.ToInt32(txtMinYear.Text);
            obj.MaxYear = Convert.ToInt32(txtMaxYear.Text);
            obj.AmountPerDays = Convert.ToDouble(txtDays.Text);
            obj.MultiplyFactor = Convert.ToDouble(txtFactor.Text);
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
    public bool CheckAlreadyNameExists(int CompanyId, int AllowanceId, int MinYear, int MaxYear, double Days, double Factor)
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

        HCM_Setup_Gratuity obj = context.HCM_Setup_Gratuity.FirstOrDefault(j => j.CompanyId == CompanyId && j.AllowanceId == AllowanceId && j.MinYear == MinYear
            && j.MaxYear == MaxYear && j.AmountPerDays == Days && j.MultiplyFactor == Factor && j.IsActive == true && j.SetupGratuityId != ModalId);

        if (obj != null)
        {
            return true;
        }
        return false;
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


}