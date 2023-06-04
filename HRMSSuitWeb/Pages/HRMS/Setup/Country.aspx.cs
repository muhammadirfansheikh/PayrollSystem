using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_HRMS_Setup_Country : Base
{
    Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindRepeater();
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ResetControls();
        BindRepeater();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindRepeater();
    }
    protected void lbEdit_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnEdit = (LinkButton)sender;
            RepeaterItem rptItem = (RepeaterItem)btnEdit.NamingContainer;
            int ID = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfId")).Value);
            var lstEdit = context.Setup_Country.Where(x => x.CountryId == ID).FirstOrDefault();
            if (lstEdit != null)
            {
                hfModalId.Value = ID.ToString();
                txtNameAdd.Text = lstEdit.CountryName;
                OpenPopup();
            }
        }
        catch { }
    }

    protected void lbDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnDelete = (LinkButton)sender;
            RepeaterItem rptItem = (RepeaterItem)btnDelete.NamingContainer;
            int Id = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfId")).Value);
            divError.Visible = false;
            int Count = context.Setup_City.Where(a => a.IsActive == true && a.CountryId == Id).Count();
            if (Count == 0)
            {

                Count = context.Setup_Employee.Where(a => a.IsActive == true && a.CountryId == Id).Count();
                if (Count == 0)
                {
                    #region Audit Logs
                    //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
                    DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(Id), "Setup_Country", 3);
                    #endregion

                    Setup_Country obj = context.Setup_Country.FirstOrDefault(j => j.CountryId == Id);
                    DateTime dt = DateTime.Now;
                    obj.IsActive = false;
                    obj.ModifiedBy = UserKey;
                    obj.ModifiedDate = dt.ToString();
                    context.SaveChanges();
                    Success("Deleted successfully.");
                    BindRepeater();
                }
                else
                {
                    Error("Unable to delete because employee exist against this country");
                }
            }
            else
            {
                Error("Unable to delete because city exist against this country");
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
        bool checkIsExist = CheckAlreadyNameExists(txtNameAdd.Text.Trim());
        if (!checkIsExist)
        {
            if (hfModalId.Value == string.Empty)
                Add();
            else
                Update();
        }
        else
        {
            Error("Already Exist.");
        }
    }

    private void Add()
    {
        DateTime dt = DateTime.Now;
        Setup_Country obj = new Setup_Country();
        obj.CountryName = txtNameAdd.Text.Trim();
        obj.CreatedBy = UserKey;
        obj.CreatedDate = dt;
        obj.IsActive = true;
        context.Setup_Country.Add(obj);
        context.SaveChanges();
        Success("Added successfully.");
        ResetControls();
        ClosePopup();
        BindRepeater();
    }
    private void Update()
    {
        DateTime dt = DateTime.Now;
        int Id = Convert.ToInt32(hfModalId.Value);

        #region Audit Logs
        //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
        DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(Id), "Setup_Country", 2);
        #endregion

        Setup_Country obj = context.Setup_Country.FirstOrDefault(j => j.CountryId == Id);
        obj.CountryName = txtNameAdd.Text.Trim();
        obj.ModifiedBy = UserKey;
        obj.ModifiedDate = dt.ToString();
        obj.IsActive = true;
        context.SaveChanges();
        Success("Updated successfully");
        ResetControls();
        ClosePopup();
        BindRepeater();
    }
    private void BindRepeater()
    {
        var ListEmployeeType = context.Setup_Country.Where(a => a.IsActive == true)
        .Where(a => txtSearch.Text.Trim() == string.Empty ? true : a.CountryName.Contains(txtSearch.Text.Trim()))
          .Select(a => new
          {
              ID = a.CountryId,
              Title = a.CountryName,
          })
          .ToList();
        rpt.DataSource = ListEmployeeType;
        rpt.DataBind();
    }

    public bool CheckAlreadyNameExists(string title)
    {
        int ModalId = 0;
        if (hfModalId.Value != "")
        {
            ModalId = Convert.ToInt32(hfModalId.Value);
        }
        else
        {
            ModalId = 0;
        }
        Setup_Country obj = context.Setup_Country.FirstOrDefault(j => j.CountryName == title && j.IsActive == true && j.CountryId != ModalId);
        if (obj != null)
        {
            return true;
        }
        return false;
    }

    private void ResetControls()
    {
        txtSearch.Text = string.Empty;
        txtNameAdd.Text = string.Empty;
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
    public void ClosePopup()
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "ClosePopup()", "ClosePopup();", true);
    }
    public void OpenPopup()
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "OpenPopup()", "OpenPopup();", true);
    }
}