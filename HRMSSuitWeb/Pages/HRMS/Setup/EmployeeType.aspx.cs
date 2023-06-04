using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_HRMS_Setup_EmployeeType : Base
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
        int scoreId = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfId")).Value);
        var eduType = context.Setup_EmployeeType.Where(x => x.EmployeeTypeId == scoreId).FirstOrDefault();
        if (eduType != null)
        {
            hfModalId.Value = scoreId.ToString();
            txtNameAdd.Text = eduType.TypeName;
            txtThirPartyMappingId.Text = eduType.ThirdPartyMappingId;
            ddlCompanyAdd.Text = eduType.CompanyId.ToString();
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
            Setup_EmployeeType obj = context.Setup_EmployeeType.FirstOrDefault(j => j.EmployeeTypeId == Id);
            if (obj.Setup_Employee.Count == 0)
            {
                #region Audit Logs
                //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
                DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(Id), "Setup_EmployeeType", 3);
                #endregion

                DateTime dt = DateTime.Now;
                obj.IsActive = false;
                obj.ModifiedBy = UserKey;
                obj.ModifiedDate = dt;
                context.SaveChanges();
                //MessageCtrl.showMessageBox("Employee Type has been deleted successfully.", MessageType.Success);
                Success("Employee Type has been deleted successfully.");

                BindRepeater();

            }
            else
            {
                Error("Education Type already Exist against Employee.");
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
        if (CommonHelper.IsCheckRecordExistAccordingToSapID(Constant.Sap_staging_Table.Setup_EmployeeType, txtThirPartyMappingId.Text.Trim(), hfModalId.Value == string.Empty ? 0 : Convert.ToInt32(hfModalId.Value),context))
        {
            if (hfModalId.Value == string.Empty)
                Add();
            else
                Update();

            ResetControls();
            BindRepeater();
            ClosePopup();

        }
        else
        {

            Error("Record Already Exist Against Third Party Id : " + txtThirPartyMappingId.Text.Trim() + "");

        }
       
        
    }
    #region Custom Methods
    private void Add()
    {
        DateTime dt = DateTime.Now;
        Setup_EmployeeType obj = new Setup_EmployeeType();


        obj.TypeName = txtNameAdd.Text.Trim();
        obj.CompanyId = Convert.ToInt32(ddlCompanyAdd.SelectedValue);
        obj.CreatedBy = UserKey;
        obj.CreatedDate = dt;
        obj.ThirdPartyMappingId = txtThirPartyMappingId.Text.Trim();
        obj.IsActive = true;
        //obj.UserIP = UserIP;
        //obj.SiteId = Convert.ToInt16(ConfigurationManager.AppSettings["SiteId"]);
        bool checkIsExist = CheckAlreadyNameExists(txtNameAdd.Text.Trim());

        if (!checkIsExist)
        {
            context.Setup_EmployeeType.Add(obj);
            context.SaveChanges();
            //MessageCtrl.showMessageBox("Employee Type has been added successfully.", MessageType.Success);
            Success("Employee Type has been added successfully.");
            ClosePopup();
        }
        else
        {
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert","Alert('Item Already Exist');",true);
            // ShowMessage(");

            //MessageCtrl.showMessageBox("Employee Type Already Exist.", MessageType.Validation);
            Error("Employee Type Already Exist.");
            ClosePopup();
        }




        BindRepeater();

        ResetControls();
    }
    private void Update()
    {
        DateTime dt = DateTime.Now;

        int Id = Convert.ToInt32(hfModalId.Value);
        Setup_EmployeeType obj = context.Setup_EmployeeType.FirstOrDefault(j => j.EmployeeTypeId == Id); 
        obj.TypeName = txtNameAdd.Text.Trim();
        obj.CompanyId = Convert.ToInt32(ddlCompanyAdd.SelectedValue);
        obj.ModifiedBy = UserKey;
        obj.ThirdPartyMappingId = txtThirPartyMappingId.Text.Trim();
        obj.ModifiedDate = dt;
        obj.IsActive = true;
        bool checkIsExist = CheckAlreadyNameExists(txtNameAdd.Text.Trim());
        if (!checkIsExist)
        {
            #region Audit Logs
            //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
            DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(Id), "Setup_EmployeeType", 2);
            #endregion

            context.SaveChanges();
            //MessageCtrl.showMessageBox("Employee Type has been updated successfully.", MessageType.Success);
            Success("Employee Type has been updated successfully.");
            ClosePopup();
        }
        else
        {
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert","Alert('Item Already Exist');",true);
            //MessageCtrl.showMessageBox("Employee Type Already Exist.", MessageType.Validation);
            Error("Employee Type Already Exist.");
            ClosePopup();
        } 

        BindRepeater();
        ResetControls();
    }
    private void BindDropdown()
    {
        var LstCompany = context.Setup_UserAccessLocation.Where(a => a.IsActive == true && a.EmployeeId==UserKey)
          .Select(a => new
          {
              CompanyId = a.Setup_Location.Setup_Company.CompanyId,
              CompanyName = a.Setup_Location.Setup_Company.CompanyName,

          })
          .ToList().Distinct();

        CommonHelper.BindDropDown(ddlCompanyAdd, LstCompany, "CompanyName", "CompanyId", true, false);
    }
    private void BindRepeater()
    {
        var ListEmployeeType = context.Setup_EmployeeType.Where(a => a.IsActive == true && (txtSearch.Text.Trim() == string.Empty ? true : a.TypeName.Contains(txtSearch.Text.Trim())) && (txtThirdPartyMappingIdSearch.Text.Trim() == string.Empty ? true : a.ThirdPartyMappingId.Contains(txtThirdPartyMappingIdSearch.Text.Trim())))
          .Select(a => new
          {
              ID = a.EmployeeTypeId,
              Title = a.TypeName,
              Company = a.Setup_Company.CompanyName,
              ThirdPartyMappingId = a.ThirdPartyMappingId

          })
          .ToList();
        rpt.DataSource = ListEmployeeType;
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

        int CompanyId = Convert.ToInt32(ddlCompanyAdd.SelectedValue);

        Setup_EmployeeType obj = context.Setup_EmployeeType.FirstOrDefault(j => j.TypeName == title && j.IsActive == true && j.EmployeeTypeId != ModalId && j.CompanyId == CompanyId);
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
        txtThirPartyMappingId.Text = string.Empty;
        txtThirdPartyMappingIdSearch.Text = string.Empty;
        ddlCompanyAdd.SelectedValue = "0";
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