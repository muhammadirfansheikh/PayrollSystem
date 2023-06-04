using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_HRMS_Setup_Gender : Base
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
        int GenderID = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfId")).Value);
        var Gender = context.HRMS_Setup_Gender.Where(x => x.GenderId == GenderID).FirstOrDefault();
        if (Gender != null)
        {
            hfModalId.Value = GenderID.ToString();
            txtGenderNameAdd.Text = Gender.GenderTitle;
            txtThirPartyMappingId.Text = Gender.ThirdPartyMappingId;

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
            HRMS_Setup_Gender obj = context.HRMS_Setup_Gender.FirstOrDefault(j => j.GenderId == Id);

            #region Audit Logs
            //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
            DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(Id), "HRMS_Setup_Gender", 3);
            #endregion

            DateTime dt = DateTime.Now;
            obj.IsActive = false;
            obj.ModifiedBy = UserKey;
            obj.ModifiedDate = dt;
            context.SaveChanges();
            //MessageCtrl.showMessageBox("Employee Type has been deleted successfully.", MessageType.Success);
            Success("Gender has been deleted successfully."); 
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
        if (CommonHelper.IsCheckRecordExistAccordingToSapID(Constant.Sap_staging_Table.HRMS_Setup_Gender, txtThirPartyMappingId.Text.Trim(), hfModalId.Value == string.Empty ? 0 : Convert.ToInt32(hfModalId.Value), context))
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
        HRMS_Setup_Gender obj = new HRMS_Setup_Gender();


        obj.GenderTitle = txtGenderNameAdd.Text.Trim();

        obj.CreatedBy = UserKey;
        obj.CreatedDate = dt;
        obj.ThirdPartyMappingId = txtThirPartyMappingId.Text.Trim();
        obj.IsActive = true;
        //obj.UserIP = UserIP;
        //obj.SiteId = Convert.ToInt16(ConfigurationManager.AppSettings["SiteId"]);
        bool checkIsExist = CheckAlreadyNameExists(txtGenderNameAdd.Text.Trim());

        if (!checkIsExist)
        {
            context.HRMS_Setup_Gender.Add(obj);
            context.SaveChanges();
            //MessageCtrl.showMessageBox("Employee Type has been added successfully.", MessageType.Success);
            Success("Gender has been added successfully.");
           
        }
        else
        {
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert","Alert('Item Already Exist');",true);
            // ShowMessage(");

            //MessageCtrl.showMessageBox("Employee Type Already Exist.", MessageType.Validation);
            Error("Gender Already Exist.");
           
        }




        BindRepeater();

        ResetControls();
        ClosePopup();
    }
    private void Update()
    {
        DateTime dt = DateTime.Now;

        int Id = Convert.ToInt32(hfModalId.Value);
        HRMS_Setup_Gender obj = context.HRMS_Setup_Gender.FirstOrDefault(j => j.GenderId == Id); 
        obj.GenderTitle = txtGenderNameAdd.Text.Trim();
        obj.ModifiedBy = UserKey;
        obj.ThirdPartyMappingId = txtThirPartyMappingId.Text.Trim();
        obj.ModifiedDate = dt;
        obj.IsActive = true;
        bool checkIsExist = CheckAlreadyNameExists(txtGenderNameAdd.Text.Trim());
        if (!checkIsExist)
        {
            #region Audit Logs
            //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
            DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(Id), "HRMS_Setup_Gender", 2);
            #endregion

            context.SaveChanges();
            //MessageCtrl.showMessageBox("Employee Type has been updated successfully.", MessageType.Success);
            Success("Gender has been updated successfully."); 
        }
        else
        {
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert","Alert('Item Already Exist');",true);
            //MessageCtrl.showMessageBox("Employee Type Already Exist.", MessageType.Validation);
            Error("Gender Already Exist."); 
        } 
        BindRepeater();
        ResetControls();
        ClosePopup();
    }

    private void BindRepeater()
    {
        var List = context.HRMS_Setup_Gender.Where(a => a.IsActive == true && (txtSearch.Text.Trim() == string.Empty ? true : a.GenderTitle.Contains(txtSearch.Text.Trim())) && (txtThirdPartyMappingIdSearch.Text.Trim() == string.Empty ? true : a.ThirdPartyMappingId.Contains(txtThirdPartyMappingIdSearch.Text.Trim())))
          .Select(a => new
          {
              ID = a.GenderId,
              Title = a.GenderTitle,

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



        HRMS_Setup_Gender obj = context.HRMS_Setup_Gender.FirstOrDefault(j => j.GenderTitle == title && j.IsActive == true && j.GenderId != ModalId);
        if (obj != null)
        {
            return true;
        }
        return false;
    }

    private void ResetControls()
    {
        txtSearch.Text = string.Empty;
        txtGenderNameAdd.Text = string.Empty;
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