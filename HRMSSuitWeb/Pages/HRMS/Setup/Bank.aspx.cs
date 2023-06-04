using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

public partial class Pages_HRMS_Setup_Bank : Base
{
    Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDown();
            BindRepeater();
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
        BindRepeater();
    }
    void DdlPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindRepeater();
    }
    void ImgNext_Click(object sender, ImageClickEventArgs e)
    {
        BindRepeater();
    }
    void ImgPrevious_Click(object sender, ImageClickEventArgs e)
    {
        BindRepeater();
    }
    #endregion

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
            string msg = CheckAlreadyNameExists();
            if (msg == "")
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
                Error(msg);
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
            int bankid = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfbankid")).Value); 

            HRMS_Setup_Bank company = context.HRMS_Setup_Bank.FirstOrDefault(j => j.BankId == bankid);
            if (company != null)
            {
                #region Audit Logs
                //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
                DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(bankid), "HRMS_Setup_Bank", 3);
                #endregion

                DateTime dt = DateTime.Now;
                company.IsActive = false;
                company.ModifiedBy = UserKey;
                company.ModifiedDate = dt;
                context.SaveChanges();
                BindRepeater();
                Success("Deleted successfully.");
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
        int bankid = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfbankid")).Value);
        var bank = context.HRMS_Setup_Bank.Where(x => x.BankId == bankid).FirstOrDefault();
        if (bank != null)
        {
            hfBankID.Value = bankid.ToString();
            txtPrefix.Text = bank.BranchCode;
            txtDiscription.Text = bank.BankDescription;
            ddlBankAdd.SelectedValue = Convert.ToString(bank.BankMasterId == null ? 0 : bank.BankMasterId);
            OpenPopup();

        }
    }

    private void BindRepeater()
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

        string bankcode = txtbancodesearch.Text.Trim();
        string bankdescription = txtdescriptionsearch.Text.Trim();
        int BankMasterId = Convert.ToInt32(ddlBank.SelectedValue);


        var List_ = context.HRMS_Setup_Bank.Where(c => c.IsActive == true
                && (c.BranchCode.Contains(bankcode) || bankcode == string.Empty)
                 && (c.BankDescription.Contains(bankdescription) || bankdescription == string.Empty)
                 && (c.HRMS_Setup_BankMaster.BankMasterId == BankMasterId || BankMasterId == 0)
            ).Select(c => new
            {
                _Id = c.BankId,
                _BankMasterId = c.BankMasterId,
                _BankMaster = c.HRMS_Setup_BankMaster.BankName,
                _bankcode = c.BranchCode,
                _bankdescription = c.BankDescription

            }).ToList().OrderBy(d => d._BankMaster).ThenBy(g => g._bankdescription).ToList();

        var List = List_.OrderBy(b => b._BankMaster).ThenBy(b => b._bankdescription).Skip(skip).Take(pageSize).ToList();
        rpt.DataSource = List;
        rpt.DataBind();
        PagingAndSorting.setPagingOptions(List_.Count());

    }
    private void BindDropDown()
    {
        var lstBank = context.HRMS_Setup_BankMaster.Where(a => a.IsActive == true).ToList();
        CommonHelper.BindDropDown(ddlBankAdd, lstBank, "BankName", "BankMasterId", true, false);
        CommonHelper.BindDropDown(ddlBank, lstBank, "BankName", "BankMasterId", true, false);
    }

    private void Update()
    {
        DateTime dt = DateTime.Now;
        int Id = Convert.ToInt32(hfBankID.Value); 
        HRMS_Setup_Bank obj = context.HRMS_Setup_Bank.FirstOrDefault(j => j.BankId == Id);
        if (obj != null)
        {
            #region Audit Logs
            //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
            DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(Id), "HRMS_Setup_Bank", 2);
            #endregion

            obj.BankMasterId = Convert.ToInt32(ddlBankAdd.SelectedValue);
            obj.BranchCode = txtPrefix.Text.Trim();
            obj.BankDescription = txtDiscription.Text.Trim();
            obj.ModifiedBy = UserKey;
            obj.ModifiedDate = dt;
            obj.IsActive = true;
            context.SaveChanges();
            Success("Edit successfully.");
            BindRepeater();
            ResetControls();
            ClosePopup();
        }
    }
    private void ResetControls()
    {
        txtbancodesearch.Text = "";
        txtdescriptionsearch.Text = "";
        txtDiscription.Text = "";
        txtPrefix.Text = "";
        ddlBank.SelectedValue = "0";
        ddlBankAdd.SelectedValue = "0";
    }
    private void Add()
    {
        DateTime dt = DateTime.Now;
        HRMS_Setup_Bank obj = new HRMS_Setup_Bank();
        obj.BankMasterId = Convert.ToInt32(ddlBankAdd.SelectedValue);
        obj.BranchCode = txtPrefix.Text.Trim();
        obj.BankDescription = txtDiscription.Text.Trim();
        obj.CreatedBy = UserKey;
        obj.CreatedDate = dt;
        obj.IsActive = true;
        context.HRMS_Setup_Bank.Add(obj);
        context.SaveChanges();
        Success("Added successfully.");
        ClosePopup();
        BindRepeater();
    }


    public string CheckAlreadyNameExists()
    {
        string msg = "";
        int bankid = hfBankID.Value == "" ? 0 : Convert.ToInt32(hfBankID.Value);
        int MasterId = ddlBankAdd.SelectedValue == "" ? 0 : Convert.ToInt32(ddlBankAdd.SelectedValue);
        string BranchCode = txtPrefix.Text.Trim();
        string BankDescription = txtDiscription.Text.Trim();
        var obj = context.HRMS_Setup_Bank.Where(j => j.IsActive == true && j.BankId != bankid && j.BankMasterId == MasterId && j.BankDescription == BankDescription).Count();
        if (obj > 0)
        {
            msg = "Branch already exist against this Bank";
        }
        else
        {
            obj = context.HRMS_Setup_Bank.Where(j => j.IsActive == true && j.BankId != bankid && j.BankMasterId == MasterId && j.BranchCode == BranchCode).Count();
            if (obj > 0)
            {
                msg = "Branch code already exist against this Bank";
            }
        }
        return msg;
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