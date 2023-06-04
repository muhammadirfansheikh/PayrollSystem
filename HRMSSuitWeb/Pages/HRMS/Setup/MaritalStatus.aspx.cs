using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_HRMS_Setup_MaritalStatus : Base
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
        int MaritalStatusId = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfId")).Value);
        var Marital = context.HRMS_Setup_MartialStatus.Where(x => x.MartialStatusId == MaritalStatusId).FirstOrDefault();
        if (Marital != null)
        {
            hfModalId.Value = MaritalStatusId.ToString();
            txtMaritalStatusNameAdd.Text = Marital.MartialStatusTitle;
            txtThirPartyMappingId.Text = Marital.ThirdPartyMappingId;

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
            HRMS_Setup_MartialStatus obj = context.HRMS_Setup_MartialStatus.FirstOrDefault(j => j.MartialStatusId == Id);

            #region Audit Logs
            //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
            DataTable Datat1 = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(Id), "HRMS_Setup_MartialStatus", 3);
            #endregion

            DateTime dt = DateTime.Now;
            obj.IsActive = false;
            obj.ModifiedBy = UserKey;
            obj.ModifiedDate = dt;
            context.SaveChanges();
            //MessageCtrl.showMessageBox("Employee Type has been deleted successfully.", MessageType.Success);
            Success("Marital Status has been deleted successfully."); 
            BindRepeater();
        }
        catch (Exception ex)
        {
            divError.Visible = true;
            lblError.InnerText = ex.Message;
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (CommonHelper.IsCheckRecordExistAccordingToSapID(Constant.Sap_staging_Table.HRMS_Setup_MartialStatus, txtThirPartyMappingId.Text.Trim(), hfModalId.Value == string.Empty ? 0 : Convert.ToInt32(hfModalId.Value), context))
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
        HRMS_Setup_MartialStatus obj = new HRMS_Setup_MartialStatus();


        obj.MartialStatusTitle = txtMaritalStatusNameAdd.Text.Trim();

        obj.CreatedBy = UserKey;
        obj.CreatedDate = dt;
        obj.ThirdPartyMappingId = txtThirPartyMappingId.Text.Trim();
        obj.IsActive = true;
        //obj.UserIP = UserIP;
        //obj.SiteId = Convert.ToInt16(ConfigurationManager.AppSettings["SiteId"]);
        bool checkIsExist = CheckAlreadyNameExists(txtMaritalStatusNameAdd.Text.Trim());

        if (!checkIsExist)
        {
            context.HRMS_Setup_MartialStatus.Add(obj);
            context.SaveChanges();
            //MessageCtrl.showMessageBox("Employee Type has been added successfully.", MessageType.Success);
            Success("Marital Status has been added successfully.");
          
        }
        else
        {
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert","Alert('Item Already Exist');",true);
            // ShowMessage(");

            //MessageCtrl.showMessageBox("Employee Type Already Exist.", MessageType.Validation);
            Error("Marital Status Already Exist.");
           
        }




        BindRepeater();

        ResetControls();
        ClosePopup();
    }
    private void Update()
    {
        DateTime dt = DateTime.Now;

        int Id = Convert.ToInt32(hfModalId.Value);
        HRMS_Setup_MartialStatus obj = context.HRMS_Setup_MartialStatus.FirstOrDefault(j => j.MartialStatusId == Id); 
        obj.MartialStatusTitle = txtMaritalStatusNameAdd.Text.Trim(); 
        obj.ModifiedBy = UserKey;
        obj.ThirdPartyMappingId = txtThirPartyMappingId.Text.Trim();
        obj.ModifiedDate = dt;
        obj.IsActive = true;
        bool checkIsExist = CheckAlreadyNameExists(txtMaritalStatusNameAdd.Text.Trim());
        if (!checkIsExist)
        {
            #region Audit Logs
            //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
            DataTable Datat1 = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(Id), "HRMS_Setup_MartialStatus", 2);
            #endregion

            context.SaveChanges();
            //MessageCtrl.showMessageBox("Employee Type has been updated successfully.", MessageType.Success);
            Success("Marital Status has been updated successfully."); 
        }
        else
        {
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert","Alert('Item Already Exist');",true);
            //MessageCtrl.showMessageBox("Employee Type Already Exist.", MessageType.Validation);
            Error("Marital Status Already Exist."); 
        } 
        BindRepeater();
        ResetControls();
        ClosePopup();
    }

    private void BindRepeater()
    {
        var List = context.HRMS_Setup_MartialStatus.Where(a => a.IsActive == true && (txtSearch.Text.Trim() == string.Empty ? true : a.MartialStatusTitle.Contains(txtSearch.Text.Trim())) && (txtThirdPartyMappingIdSearch.Text.Trim() == string.Empty ? true : a.ThirdPartyMappingId.Contains(txtThirdPartyMappingIdSearch.Text.Trim())))
          .Select(a => new
          {
              ID = a.MartialStatusId,
              Title = a.MartialStatusTitle,

              ThirdPartyMappingId = a.ThirdPartyMappingId

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



        HRMS_Setup_MartialStatus obj = context.HRMS_Setup_MartialStatus.FirstOrDefault(j => j.MartialStatusTitle == title && j.IsActive == true && j.MartialStatusId != ModalId);
        if (obj != null)
        {
            return true;
        }
        return false;
    }

    private void ResetControls()
    {
        txtSearch.Text = string.Empty;
        txtMaritalStatusNameAdd.Text = string.Empty;
        txtThirPartyMappingId.Text = string.Empty;
        txtThirdPartyMappingIdSearch.Text = string.Empty;

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