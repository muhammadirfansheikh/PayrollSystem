using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

public partial class Pages_HRMS_Setup_SetupLocation : Base
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
        var LstCompany = context.Setup_UserAccessLocation.Where(a => a.IsActive == true && a.EmployeeId == UserKey)
      .Select(a => new
      {
          CompanyId = a.Setup_Location.Setup_Company.CompanyId,
          CompanyName = a.Setup_Location.Setup_Company.CompanyName,
      })
      .ToList().Distinct();

        CommonHelper.BindDropDown(ddlCompanyAdd, LstCompany, "CompanyName", "CompanyId", true, false);
        ddlCompanyAdd_SelectedIndexChanged(null, null);

    }
    private void BindRepeater()
    {
        var List = context.Setup_Location.Where(a => a.IsActive == true)
            .Where(a => txtSearch.Text.Trim() == string.Empty ? true : a.LocationName.Contains(txtSearch.Text.Trim()))
          .Select(a => new
          {
              ID = a.LocationId,
              Title = a.LocationName == null ? "" : a.LocationName,
              PhoneNum = a.PhoneNo == null ? "" : a.PhoneNo,
              FaxNum = a.FaxNo == null ? "" : a.FaxNo,
              CompanyId = a.CompanyId ,
              CompanyName = a.Setup_Company.CompanyName ,
              CityId = a.Setup_City.CityId == null ? 0 : a.Setup_City.CityId,
              CityName = a.Setup_City.CityName == null ? "" : a.Setup_City.CityName,

          })
          .ToList().OrderBy(b => b.CompanyName).ThenBy(c => c.Title);
        rpt.DataSource = List;
        rpt.DataBind();
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
        var lstEdit = context.Setup_Location.Where(x => x.LocationId == ID).FirstOrDefault();
        if (lstEdit != null)
        {
            hfModalId.Value = ID.ToString();
            txtNameAdd.Text = lstEdit.LocationName;
            ddlCompanyAdd.SelectedValue = lstEdit.CompanyId.ToString();
            ddlCompanyAdd_SelectedIndexChanged(null, null);


            OpenPopup();

        }

    }
    protected void ddlCompanyAdd_SelectedIndexChanged(object sender, EventArgs e)
    {

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

    private void ResetControls()
    {
        txtSearch.Text = string.Empty;
        txtNameAdd.Text = string.Empty;
        hfModalId.Value = "";
    }

    protected void lbDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnDelete = (LinkButton)sender;
            RepeaterItem rptItem = (RepeaterItem)btnDelete.NamingContainer;

            int Id = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfId")).Value);

            divError.Visible = false;
            Setup_Location obj = context.Setup_Location.FirstOrDefault(j => j.LocationId == Id);
            if (obj.Setup_Employee.Count == 0)
            {
                #region Audit Logs
                //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
                DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(Id), "Setup_Location", 3);
                #endregion

                DateTime dt = DateTime.Now;
                obj.IsActive = false;
                obj.ModifiedBy = UserKey;
                obj.ModifiedDate = dt;
                context.SaveChanges();
                //MessageCtrl.showMessageBox("Education Score has been deleted successfully.", MessageType.Success);
                Success("Location has been deleted successfully.");

                BindRepeater();

            }
            else
            {
                Error("Location already Exist against Employee.");
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

    private void Add()
    {
        DateTime dt = DateTime.Now;
        Setup_Location obj = new Setup_Location();
        obj.CompanyId = Convert.ToInt32(ddlCompanyAdd.SelectedValue);
        obj.LocationName = txtNameAdd.Text.Trim();

        obj.CreatedBy = UserKey;
        obj.CreatedDate = dt;
        obj.IsActive = true;

        bool checkIsExist = CheckAlreadyNameExists(txtNameAdd.Text.Trim());

        if (!checkIsExist)
        {
            context.Setup_Location.Add(obj);
            context.SaveChanges();
            //MessageCtrl.showMessageBox("Education Score has been added successfully.", MessageType.Success);
            Success("Location has been added successfully.");
            ClosePopup();
        }
        else
        {
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert","Alert('Item Already Exist');",true);
            // ShowMessage(");

            //MessageCtrl.showMessageBox("Education Score Already Exist.", MessageType.Validation);
            Error("Location Already Exist.");
            ClosePopup();
        }
        BindRepeater();

        ResetControls();
    }
    private void Update()
    {
        DateTime dt = DateTime.Now;

        int Id = Convert.ToInt32(hfModalId.Value);
        Setup_Location obj = context.Setup_Location.FirstOrDefault(j => j.LocationId == Id);
        //obj.CompanyName = txtNameAdd.Text.Trim();
        //obj.GroupId = Convert.ToInt32(ddlGroupAdd.SelectedValue);
        obj.CompanyId = Convert.ToInt32(ddlCompanyAdd.SelectedValue);

        obj.LocationName = txtNameAdd.Text.Trim();
        obj.ModifiedBy = UserKey;
        obj.ModifiedDate = dt;
        obj.IsActive = true;
        bool checkIsExist = CheckAlreadyNameExists(txtNameAdd.Text.Trim());
        if (!checkIsExist)
        {
            #region Audit Logs
            //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
            DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(Id), "Setup_Location", 2);
            #endregion

            context.SaveChanges();
            //MessageCtrl.showMessageBox("Education Score has been updated successfully.", MessageType.Success);
            Success("Location has been updated successfully.");
            ClosePopup();
        }
        else
        {
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert","Alert('Item Already Exist');",true);
            //MessageCtrl.showMessageBox("Education Score Already Exist.", MessageType.Validation);
            Error("Location Already Exist.");
            ClosePopup();
        }
        BindRepeater();
        ResetControls();

    }


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

        int CompanyId = Convert.ToInt32(ddlCompanyAdd.SelectedValue);

        Setup_Location obj = context.Setup_Location.FirstOrDefault(j => j.LocationName == title && j.IsActive == true && j.LocationId != ModalId && j.CompanyId == CompanyId);
        if (obj != null)
        {
            return true;
        }
        return false;
    }
}