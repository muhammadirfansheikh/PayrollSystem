using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_HRMS_Setup_Group : Base
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
        txtSearch.Text = "";
        ResetControls();
        BindRepeater();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindRepeater();
    }
    protected void lbEdit_Click(object sender, EventArgs e)
    {
        LinkButton btnEdit = (LinkButton)sender;
        RepeaterItem rptItem = (RepeaterItem)btnEdit.NamingContainer;
        int ID = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfId")).Value);
        var lstEdit = context.Setup_Group.Where(x => x.GroupId == ID).FirstOrDefault();
        if (lstEdit != null)
        {
            hfModalId.Value = ID.ToString();
            txtNameAdd.Text = lstEdit.GroupName;
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
            Setup_Group obj = context.Setup_Group.FirstOrDefault(j => j.GroupId == Id);
            if (obj.Setup_Company.Count == 0)
            {
                #region Audit Logs
                //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
                DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(Id), "Setup_Group", 3);
                #endregion

                DateTime dt = DateTime.Now;
                obj.IsActive = false;
                obj.ModifiedBy = UserKey;
                obj.ModifiedDate = dt;
                obj.UserIP = UserIP;
                context.SaveChanges();
                Success("Deleted successfully.");
                BindRepeater();
            }
            else
            {
                Error("Unable to delete because company exist against this group");
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
            int ModalId = hfModalId.Value == "" ? 0 : Convert.ToInt32(hfModalId.Value);
            bool checkIsExist = CheckAlreadyNameExists(txtNameAdd.Text.Trim(), ModalId);
            if (checkIsExist == false)
            {
                if (hfModalId.Value == "")
                {
                    Add();
                }
                else
                {
                    Update();
                }
            }
            else
            {
                Error("Already Exist.");
            }
        }
        catch { }
    }
    private void Add()
    {
        DateTime dt = DateTime.Now;
        Setup_Group obj = new Setup_Group();
        obj.GroupName = txtNameAdd.Text.Trim();
        obj.CreatedBy = UserKey;
        obj.CreatedDate = dt;
        obj.IsActive = true;
        obj.UserIP = UserIP;
        context.Setup_Group.Add(obj);
        context.SaveChanges();
        if (obj.GroupId > 0)
        {
            Success("Added successfully.");
            ClosePopup();
            ResetControls();
            BindRepeater();
        }
    }
    private void Update()
    {
        DateTime dt = DateTime.Now;
        int Id = Convert.ToInt32(hfModalId.Value);
        Setup_Group obj = context.Setup_Group.FirstOrDefault(j => j.GroupId == Id);
        if (obj != null)
        {
            #region Audit Logs
            //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
            DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(Id), "Setup_Group", 2);
            #endregion

            obj.GroupName = txtNameAdd.Text.Trim();
            obj.ModifiedBy = UserKey;
            obj.ModifiedDate = dt;
            obj.UserIP = UserIP;
            obj.IsActive = true;
            context.SaveChanges();
            Success("Edit successfully.");
            ClosePopup();
            ResetControls();
            BindRepeater();
        }
    }
    private void BindRepeater()
    {
        var List = context.Setup_Group.Where(a => a.IsActive == true)
        .Where(a => txtSearch.Text.Trim() == string.Empty ? true : a.GroupName.Contains(txtSearch.Text.Trim()))
          .Select(a => new
          {
              ID = a.GroupId,
              Title = a.GroupName,
          })
          .ToList();
        rpt.DataSource = List;
        rpt.DataBind();
    }
    public bool CheckAlreadyNameExists(string title, int Id)
    {
        Setup_Group obj = context.Setup_Group.FirstOrDefault(j => j.GroupName == title && j.IsActive == true && j.GroupId != Id);
        if (obj != null)
        {
            return true;
        }
        return false;
    }
    private void ResetControls()
    {
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