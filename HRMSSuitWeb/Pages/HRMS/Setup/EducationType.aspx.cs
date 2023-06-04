using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

public partial class Pages_HRMS_Setup_EducationType : Base
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
        BindRepeater();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ResetControls();
        BindRepeater();
    }
    protected void lbEdit_Click(object sender, EventArgs e)
    {
        LinkButton btnEdit = (LinkButton)sender;
        RepeaterItem rptItem = (RepeaterItem)btnEdit.NamingContainer;
        int typeId = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hftypeid")).Value);
        var edutype = context.HRMS_Setup_EducationType.Where(x => x.educationTypeId == typeId).FirstOrDefault();
        if (edutype != null)
        {
            hfTypeId.Value = typeId.ToString();
            txtTypeAdd.Text = edutype.educationType;
            txtThirPartyMappingId.Text = edutype.ThirdPartyMappingId;
            OpenPopup();
        }
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnDelete = (LinkButton)sender;
            RepeaterItem rptItem = (RepeaterItem)btnDelete.NamingContainer;

            int Id = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hftypeid")).Value);

            divError.Visible = false;
            HRMS_Setup_EducationType obj = context.HRMS_Setup_EducationType.FirstOrDefault(j => j.educationTypeId == Id);

            #region Audit Logs
            //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
            DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(Id), "HRMS_Setup_EducationType", 3);
            #endregion

            DateTime dt = DateTime.Now;
            obj.IsActive = false;
            obj.ModifiedBy = UserKey;
            obj.ModifiedDate = dt;
            context.SaveChanges();

            //MessageCtrl.showMessageBox("Education Type has been deleted successfully.", MessageType.Success);
            Success("Education Type has been deleted successfully.");
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
        if (CommonHelper.IsCheckRecordExistAccordingToSapID(Constant.Sap_staging_Table.HRMS_Setup_EducationType, txtThirPartyMappingId.Text.Trim(), hfTypeId.Value == string.Empty ? 0 : Convert.ToInt32(hfTypeId.Value),context))
        {
            if (hfTypeId.Value == string.Empty)
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
        try
        {
            DateTime dt = DateTime.Now;
            HRMS_Setup_EducationType obj = new HRMS_Setup_EducationType();
            obj.educationType = txtTypeAdd.Text.Trim();
            obj.CreatedBy = UserKey;
            obj.CreatedDate = dt;
            obj.IsActive = true;
            obj.ThirdPartyMappingId = txtThirPartyMappingId.Text;
            //obj.UserIP = UserIP;
            //obj.SiteId = Convert.ToInt16(ConfigurationManager.AppSettings["SiteId"]);
            bool checkIsExist = CheckAlreadyNameExists(txtTypeAdd.Text.Trim());

            if (!checkIsExist)
            {
                context.HRMS_Setup_EducationType.Add(obj);
                context.SaveChanges();
                //MessageCtrl.showMessageBox("Education Type has been added successfully.", MessageType.Success);
                Success("Education Type has been added successfully.");
               
            }
            else
            {
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert","Alert('Item Already Exist');",true);
                // ShowMessage("Education Type Already Exist");
                //MessageCtrl.showMessageBox("Education Type Already Exist.", MessageType.Validation);
                Error("Education Type Already Exist.");

            }



            BindRepeater();

            ResetControls();
            ClosePopup();
        }
        catch (Exception ex)
        {

            Error(ex.Message);
        }

      

    }
    private void Update()
    {
        DateTime dt = DateTime.Now;

        int Id = Convert.ToInt32(hfTypeId.Value);
        HRMS_Setup_EducationType obj = context.HRMS_Setup_EducationType.FirstOrDefault(j => j.educationTypeId == Id);
        obj.educationType = txtTypeAdd.Text.Trim(); 

        obj.ModifiedBy = UserKey;
        obj.ModifiedDate = dt;
        obj.IsActive = true;
        obj.ThirdPartyMappingId = txtThirPartyMappingId.Text;
        bool checkIsExist = CheckAlreadyNameExists(txtTypeAdd.Text.Trim());

        if (!checkIsExist)
        {
            #region Audit Logs
            //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
            DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(Id), "HRMS_Setup_EducationType", 2);
            #endregion

            context.SaveChanges();
            //MessageCtrl.showMessageBox("Education Type has been updated successfully.", MessageType.Success);
            Success("Education Type has been updated successfully."); 
        }
        else
        {
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert","Alert('Item Already Exist');",true);
            Error("Education Type Already Exist"); 
        }
        BindRepeater();
        ResetControls();
        ClosePopup();
    }

    public bool CheckAlreadyNameExists(string title)
    {
        int typeis = 0;
        //City ocity = context.Cities.FirstOrDefault(c => c.CityName == cityname);
        if (hfTypeId.Value != "")
        {
            typeis = Convert.ToInt32(hfTypeId.Value);
        }
        else
        {
            typeis = 0;
        }
        //City ocity = context.Cities.FirstOrDefault(c => c.CityName == cityname);

        HRMS_Setup_EducationType obj = context.HRMS_Setup_EducationType.FirstOrDefault(j => j.educationType == title && j.educationTypeId != typeis && j.IsActive == true);
        if (obj != null)
        {
            return true;
        }
        return false;
    }


    private void BindRepeater()
    {

        string type = txtTypeSearch.Text.Trim();
        string ThirdPartyMappingId = txtThirdPartyMappingIdSearch.Text.Trim();


        var List = context.HRMS_Setup_EducationType.Where(c => c.IsActive == true
                && (c.educationType.Contains(type) || type == string.Empty) && (c.ThirdPartyMappingId.Contains(ThirdPartyMappingId) || ThirdPartyMappingId == string.Empty)

            ).Select(c => new
            {
                _Id = c.educationTypeId,
                _Educationtype = c.educationType,
                _ThirdPartyMappingId = c.ThirdPartyMappingId


            }).ToList();
        rpt.DataSource = List;
        rpt.DataBind();
    }
    private void ResetControls()
    {
        txtTypeSearch.Text = string.Empty;
        txtTypeAdd.Text = string.Empty;
        txtThirdPartyMappingIdSearch.Text = string.Empty;
        txtThirPartyMappingId.Text = string.Empty;
        hfTypeId.Value = "";
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
    #endregion
}