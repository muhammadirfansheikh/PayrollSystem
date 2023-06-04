using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

public partial class Pages_HRMS_Setup_BankMaster : Base
{
    Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindRepeater();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsValid)
            {
                BindRepeater();
            }
        }


        catch (Exception ex)
        {

            //throw;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ResetControls();
        BindRepeater();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            int Id = hfBankID.Value == "" ? 0 : Convert.ToInt32(hfBankID.Value);
            if (CheckAlreadyNameExists(txtPrefix.Text.Trim(), txtBank.Text.Trim()) == false)
            {
                if (Id == 0)
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
        catch (Exception ex)
        {
        }
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnDelete = (LinkButton)sender;
            RepeaterItem rptItem = (RepeaterItem)btnDelete.NamingContainer;
            int bankid = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfBankMasterId")).Value); 

            HRMS_Setup_BankMaster company = context.HRMS_Setup_BankMaster.FirstOrDefault(j => j.BankMasterId == bankid);
            if (company != null)
            {
                #region Audit Logs
                //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
                DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(bankid), "HRMS_Setup_BankMaster", 3);
                #endregion

                DateTime dt = DateTime.Now;
                company.IsActive = false;
                company.ModifiedBy = UserKey;
                company.ModifiedDate = dt;
                context.SaveChanges();
                Success("Deleted successfully.");
                BindRepeater();
            }

        }
        catch (Exception ex)
        {
            divError.Visible = true;
            lblError.InnerText = ex.Message;
        }
    }
    protected void lbEdit_Click(object sender, EventArgs e)
    {
        LinkButton btnEdit = (LinkButton)sender;
        RepeaterItem rptItem = (RepeaterItem)btnEdit.NamingContainer;
        int bankid = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfBankMasterId")).Value);
        var bank = context.HRMS_Setup_BankMaster.Where(x => x.BankMasterId == bankid).FirstOrDefault();
        if (bank != null)
        {
            hfBankID.Value = bank.BankMasterId.ToString();
            txtPrefix.Text = bank.BankPrefix;
            txtBank.Text = bank.BankName;
            OpenPopup();

        }
    }
    private void BindRepeater()
    {

        string bankcode = txtBankPrefixSearch.Text.Trim();
        string bankdescription = txtBankSearch.Text.Trim();

        var List = context.HRMS_Setup_BankMaster.Where(c => c.IsActive == true
                && (c.BankPrefix.Contains(bankcode) || bankcode == string.Empty)
                 && (c.BankName.Contains(bankdescription) || bankdescription == string.Empty)

            ).Select(c => new
            {

                _BankMasterId = c.BankMasterId,
                _Bank = c.BankName,
                _BankPrefix = c.BankPrefix,

            }).OrderBy(d => d._Bank).ToList();
        rpt.DataSource = List;
        rpt.DataBind();
    }
    private void Update()
    {
        DateTime dt = DateTime.Now;
        int Id = hfBankID.Value == "" ? 0 : Convert.ToInt32(hfBankID.Value); 

        HRMS_Setup_BankMaster obj = context.HRMS_Setup_BankMaster.FirstOrDefault(j => j.BankMasterId == Id);
        if (obj != null)
        {
            #region Audit Logs
            //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
            DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(Id), "HRMS_Setup_BankMaster", 2);
            #endregion

            obj.BankPrefix = txtPrefix.Text.Trim();
            obj.BankName = txtBank.Text.Trim();
            obj.ModifiedBy = UserKey;
            obj.ModifiedDate = dt;
            obj.IsActive = true;
            context.SaveChanges();
            Success("Edit successfully.");
            ResetControls();
            BindRepeater();
            ClosePopup();
        }
    }
    private void ResetControls()
    {
        txtBankPrefixSearch.Text = "";
        txtBankSearch.Text = "";
        txtBank.Text = "";
        txtPrefix.Text = "";
        hfBankID.Value = "0";
    }
    private void Add()
    {
        DateTime dt = DateTime.Now;
        HRMS_Setup_BankMaster obj = new HRMS_Setup_BankMaster();
        obj.BankPrefix = txtPrefix.Text.Trim();
        obj.BankName = txtBank.Text.Trim();
        obj.CreatedBy = UserKey;
        obj.CreatedDate = dt;
        obj.IsActive = true;
        context.HRMS_Setup_BankMaster.Add(obj);
        context.SaveChanges();
        Success("Added successfully.");
        ClosePopup();
        BindRepeater();
    }
    public bool CheckAlreadyNameExists(string code, string description)
    {
        int bankid = hfBankID.Value == "" ? 0 : Convert.ToInt32(hfBankID.Value);
        var obj = context.HRMS_Setup_BankMaster.Where(j => j.BankName == description && j.IsActive == true && j.BankMasterId != bankid).ToList();
        if (obj != null && obj.Count > 0)
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
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ClosePopup();
    }
    protected void Btn_Add_Click(object sender, EventArgs e)
    {
        ResetControls();
        OpenPopup();

    }
}