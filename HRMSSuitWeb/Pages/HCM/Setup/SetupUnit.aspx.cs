using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Web.UI.HtmlControls;
using System.Data;

public partial class Pages_HCM_Setup_SetupUnit : Base
{
    Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        BindRepeater();
    }



    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (hfModalId.Value == string.Empty)
            AddNewUnit();
        else
            EditUnit();

    }


    private void BindRepeater()
    {
        var lstUnit = context.HCM_Setup_Detail.Where(x => x.IsActive == true && x.SetupMasterID == (int)Constant.HCMSetupMaster.Unit)
          .Select(x => new
          {
              Title = x.ColumnValue,
              ID = x.SetupDetailID
          }).ToList();
        rpt.DataSource = lstUnit;
        rpt.DataBind();
    }


    private void AddNewUnit()
    {


        if (CheckExist(txtNameAdd.Text.Trim()))
        {
            Error("Unit Already Been Added");
            return;
        }

        var Unit = new HCM_Setup_Detail();
        Unit.ColumnValue = txtNameAdd.Text.Trim();
        Unit.SetupMasterID = (int)Constant.HCMSetupMaster.Unit;
        Unit.IsActive = true;
        Unit.CreatedBy = UserKey;

        context.HCM_Setup_Detail.Add(Unit);
        context.SaveChanges();

        Success("Unit has been added successfully.");
        ClosePopup();
        BindRepeater();

    }

    private void EditUnit()
    {
        int ID = int.Parse(hfModalId.Value);
        var Unit = context.HCM_Setup_Detail.FirstOrDefault(x => x.IsActive == true && x.SetupDetailID == ID);
        Unit.ColumnValue = txtNameAdd.Text.Trim();
        Unit.SetupMasterID = (int)Constant.HCMSetupMaster.Unit;
        Unit.IsActive = true;
        Unit.CreatedBy = UserKey;

        if (CheckExist(txtNameAdd.Text.Trim()))
        {
            Error("Unit Already Exist");
        }
        else
        {
            #region Audit Logs
            //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
            DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(ID), "HCM_Setup_Detail", 2);
            #endregion

            context.SaveChanges();
            Success("Unit has been added successfully.");
            ClosePopup();
            BindRepeater();
        } 
    } 
    private bool CheckExist(string title)
    {
        var obj = context.HCM_Setup_Detail.FirstOrDefault(x => x.IsActive == true && x.SetupMasterID == (int)Constant.HCMSetupMaster.Unit && x.ColumnValue == title);

        if (obj != null)
            return true;
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




    protected void lbEdit_Click(object sender, EventArgs e)
    {
        LinkButton btnEdit = (LinkButton)sender;
        RepeaterItem rptItem = (RepeaterItem)btnEdit.NamingContainer;
        int ID = int.Parse(((HtmlInputHidden)rptItem.FindControl("hfId")).Value);
        string txtName = context.HCM_Setup_Detail.FirstOrDefault(x => x.IsActive == true && x.SetupDetailID == ID).ColumnValue;
        txtNameAdd.Text = txtName;
        hfModalId.Value = ID + "";
        OpenPopup();
    }

    private void Search()
    {
        var lstUnit = context.HCM_Setup_Detail
             .Where(x => x.IsActive == true && x.SetupMasterID == (int)Constant.HCMSetupMaster.Unit && x.ColumnValue.Contains(txtSearch.Text.Trim()))
             .Select(x => new
             {
                 Title = x.ColumnValue,
                 ID = x.SetupDetailID
             }).ToList();
        rpt.DataSource = lstUnit;
        rpt.DataBind();
    }

    private void ResetFields()
    {
        txtSearch.Text = string.Empty;
        txtNameAdd.Text = string.Empty;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ResetFields();
        BindRepeater();
    }



    protected void lbDelete_Click(object sender, EventArgs e)
    {
        LinkButton btnEdit = (LinkButton)sender;
        RepeaterItem rptItem = (RepeaterItem)btnEdit.NamingContainer;
        int ID = int.Parse(((HtmlInputHidden)rptItem.FindControl("hfId")).Value);

        #region Audit Logs
        //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
        DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(ID), "HCM_Setup_Detail", 3);
        #endregion

        var obj = context.HCM_Setup_Detail.FirstOrDefault(x => x.IsActive == true && x.SetupDetailID == ID);
        obj.IsActive = false;
        context.SaveChanges();
        Success("Unit Deleted Successfully");
        BindRepeater();
    }
}