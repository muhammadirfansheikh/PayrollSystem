﻿using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_HRMS_Setup_HiringType : Base
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
        LinkButton btnEdit = (LinkButton)sender;
        RepeaterItem rptItem = (RepeaterItem)btnEdit.NamingContainer;
        int ID = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfId")).Value);
        var lstEdit = context.HRMS_Setup_HiringType.Where(x => x.HiringTypeId == ID).FirstOrDefault();
        if (lstEdit != null)
        {
            hfModalId.Value = ID.ToString();
            txtNameAdd.Text = lstEdit.HiringType;
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
            HRMS_Setup_HiringType obj = context.HRMS_Setup_HiringType.FirstOrDefault(j => j.HiringTypeId == Id);
            if (obj.Setup_Employee.Count == 0)
            {
                #region Audit Logs
                //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
                DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(Id), "HRMS_Setup_HiringType", 3);
                #endregion

                DateTime dt = DateTime.Now;
                obj.IsActive = false;
                obj.ModifiedBy = UserKey;
                obj.ModifiedDate = dt;
                context.SaveChanges();
                //MessageCtrl.showMessageBox("Education Score has been deleted successfully.", MessageType.Success);
                Success("Hiring Type has been deleted successfully.");
                BindRepeater();
            }
            else
            {
                Error("Hiring Type already Exist against Employee..");
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
        HRMS_Setup_HiringType obj = new HRMS_Setup_HiringType();
        obj.HiringType = txtNameAdd.Text.Trim();
        obj.CreatedBy = UserKey;
        obj.CreatedDate = dt;
        obj.IsActive = true;
        //obj.UserIP = UserIP;
        //obj.SiteId = Convert.ToInt16(ConfigurationManager.AppSettings["SiteId"]);
        bool checkIsExist = CheckAlreadyNameExists(txtNameAdd.Text.Trim());

        if (!checkIsExist)
        {
            context.HRMS_Setup_HiringType.Add(obj);
            context.SaveChanges();
            //MessageCtrl.showMessageBox("Education Score has been added successfully.", MessageType.Success);
            Success("Hiring Type has been added successfully.");
            ClosePopup();
        }
        else
        {
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert","Alert('Item Already Exist');",true);
            // ShowMessage(");

            //MessageCtrl.showMessageBox("Education Score Already Exist.", MessageType.Validation);
            Error("Hiring Type Already Exist.");
            ClosePopup();
        }
        BindRepeater();

        ResetControls();
    }
    private void Update()
    {
        DateTime dt = DateTime.Now;

        int Id = Convert.ToInt32(hfModalId.Value);
        HRMS_Setup_HiringType obj = context.HRMS_Setup_HiringType.FirstOrDefault(j => j.HiringTypeId == Id); 
        obj.HiringType = txtNameAdd.Text.Trim();
        obj.ModifiedBy = UserKey;
        obj.ModifiedDate = dt;
        obj.IsActive = true;
        bool checkIsExist = CheckAlreadyNameExists(txtNameAdd.Text.Trim());
        if (!checkIsExist)
        {
            #region Audit Logs
            //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
            DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(Id), "HRMS_Setup_HiringType", 2);
            #endregion

            context.SaveChanges();
            //MessageCtrl.showMessageBox("Education Score has been updated successfully.", MessageType.Success);
            Success("Hiring Type has been updated successfully.");
            ClosePopup();
        }
        else
        {
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert","Alert('Item Already Exist');",true);
            //MessageCtrl.showMessageBox("Education Score Already Exist.", MessageType.Validation);
            Error("Hiring Type Already Exist.");
            ClosePopup();
        }
        BindRepeater();
        ResetControls();
    }
    private void BindRepeater()
    {
        var List = context.HRMS_Setup_HiringType.Where(a => a.IsActive == true)
        .Where(a => txtSearch.Text.Trim() == string.Empty ? true : a.HiringType.Contains(txtSearch.Text.Trim()))
          .Select(a => new
          {
              ID = a.HiringTypeId,
              Title = a.HiringType,
          })
          .ToList();
        rpt.DataSource = List;
        rpt.DataBind();
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

        HRMS_Setup_HiringType obj = context.HRMS_Setup_HiringType.FirstOrDefault(j => j.HiringType == title && j.IsActive == true && j.HiringTypeId != ModalId);
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