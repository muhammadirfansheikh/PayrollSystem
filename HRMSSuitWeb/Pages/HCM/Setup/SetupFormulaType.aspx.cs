﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
public partial class Pages_HCM_Setup_SetupFormulaType : Base
{
    Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDown();
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
        LinkButton btnEdit = (LinkButton)sender;
        RepeaterItem rptItem = (RepeaterItem)btnEdit.NamingContainer;
        int ID = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfId")).Value);
        var lstEdit = context.HCM_Setup_FomulaType.Where(x => x.FormulaTypeID == ID).FirstOrDefault();
        if (lstEdit != null)
        {
            hfModalId.Value = ID.ToString();
            txtNameAdd.Text = lstEdit.FomulaName;
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
            HCM_Setup_FomulaType obj = context.HCM_Setup_FomulaType.FirstOrDefault(j => j.FormulaTypeID == Id);
            if (obj != null)
            {
                #region Audit Logs
                //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
                DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(Id), "HCM_Setup_FomulaType", 3);
                #endregion

                DateTime dt = DateTime.Now;
                obj.IsActive = false;
                obj.ModifiedBy = UserKey;
                obj.ModifiedDate = dt;
                obj.UserIP = UserIP;
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
        if (hfModalId.Value == string.Empty)
            Add();
        else
            Update();
    }
    #region Custom Methods
    private void Add()
    {
        DateTime dt = DateTime.Now;
        HCM_Setup_FomulaType obj = new HCM_Setup_FomulaType();
        obj.FomulaName = txtNameAdd.Text.Trim();
        obj.CreatedBy = UserKey;
        obj.CreatedDate = dt;
        obj.IsActive = true;
        obj.UserIP = UserIP;
        //obj.SiteId = Convert.ToInt16(ConfigurationManager.AppSettings["SiteId"]);
        bool checkIsExist = CheckAlreadyNameExists(txtNameAdd.Text.Trim());

        if (!checkIsExist)
        {
            context.HCM_Setup_FomulaType.Add(obj);
            context.SaveChanges();
            //MessageCtrl.showMessageBox("Education Score has been added successfully.", MessageType.Success);
            Success("Formula Type has been added successfully.");
            ClosePopup();
        }
        else
        {
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert","Alert('Item Already Exist');",true);
            // ShowMessage(");

            //MessageCtrl.showMessageBox("Education Score Already Exist.", MessageType.Validation);
            Error("Formula Type Already Exist.");
            ClosePopup();
        }
        BindRepeater();

        ResetControls();
    }
    private void Update()
    {
        DateTime dt = DateTime.Now;

        int Id = Convert.ToInt32(hfModalId.Value);
        HCM_Setup_FomulaType obj = context.HCM_Setup_FomulaType.FirstOrDefault(j => j.FormulaTypeID == Id);

        obj.FomulaName = txtNameAdd.Text.Trim();
        obj.ModifiedBy = UserKey;
        obj.ModifiedDate = dt;
        obj.IsActive = true;
        obj.UserIP = UserIP;
        bool checkIsExist = CheckAlreadyNameExists(txtNameAdd.Text.Trim());
        if (!checkIsExist)
        {
            #region Audit Logs
            //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
            DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(Id), "HCM_Setup_FomulaType", 2);
            #endregion

            context.SaveChanges();
            //MessageCtrl.showMessageBox("Education Score has been updated successfully.", MessageType.Success);
            Success("Formula Type has been updated successfully.");
            ClosePopup();
        }
        else
        {
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert","Alert('Item Already Exist');",true);
            //MessageCtrl.showMessageBox("Education Score Already Exist.", MessageType.Validation);
            Error("Formula Type Already Exist.");
            ClosePopup();
        }
        BindRepeater();
        ResetControls();
    }
    private void BindRepeater()
    {
        var lst = context.HCM_Setup_FomulaType.Where(a => a.IsActive == true)
        .Where(a => txtSearch.Text.Trim() == string.Empty ? true : a.FomulaName.Contains(txtSearch.Text.Trim()))
          .Select(a => new
          {
              ID = a.FormulaTypeID,
              Title = a.FomulaName,

          })
          .ToList();
        rpt.DataSource = lst;
        rpt.DataBind();
    }
    private void BindDropDown()
    {
        var lst = context.Setup_Company.Where(a => a.IsActive == true)
            .Select(a => new
            {
                Id = a.CompanyId,
                Value = a.CompanyName
            })
            .ToList();
        CommonHelper.BindDropDown(ddlCompany, lst, "Value", "Id", true, false);
    }
    #endregion
    public bool CheckAlreadyNameExists(string title)
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

        HCM_Setup_FomulaType obj = context.HCM_Setup_FomulaType.FirstOrDefault(j => j.FomulaName == title && j.IsActive == true && j.FormulaTypeID != ModalId);
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