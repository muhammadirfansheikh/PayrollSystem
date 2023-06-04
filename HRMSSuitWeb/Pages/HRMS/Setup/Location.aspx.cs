using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using DAL;
using System.Transactions;
using System.Data;
using System.Data.SqlClient;


public partial class Pages_HRMS_Setup_Location : Base
{
    int? NullableInt = null;

    Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetRoleFeature();
            BindDropdown();
            BindRepeater();
        }
    }
    private void BindDropdown()
    {
        var li = context.Setup_Group.Where(a => a.IsActive == true).ToList();
        CommonHelper.BindDropDown(ddlgroupSearch, li, "GroupName", "GroupId", li.Count == 1 ? false : true, false);
        CommonHelper.BindDropDown(ddlGroupAdd, li, "GroupName", "GroupId", li.Count == 1 ? false : true, false);
        ddlgroupSearch_SelectedIndexChanged(null, null);
        ddlGroupAdd_SelectedIndexChanged(null, null);
    }
    protected void ddlgroupSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        int GroupId = ddlgroupSearch.SelectedValue == "" ? 0 : Convert.ToInt32(ddlgroupSearch.SelectedValue);
        DataSet ds = CommonHelper.Get_Control_Data_EmployeeSearchFilter("OnChangeGroup", GroupId, 0, 0, 0, 0, 0,0, 0, 0, 0, 0, 0, UserKey);
        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables.Count == 1)
            {
                CommonHelper.BindDropDown(ddlcompanySearch, ds.Tables[0], "Value", "Id", true, false);
            }
        }
    }
    protected void ddlGroupAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        int GroupId = ddlGroupAdd.SelectedValue == "" ? 0 : Convert.ToInt32(ddlGroupAdd.SelectedValue);
        DataSet ds = CommonHelper.Get_Control_Data_EmployeeSearchFilter("OnChangeGroup", GroupId, 0, 0, 0, 0, 0,0, 0, 0, 0, 0, 0, UserKey);
        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables.Count == 1)
            {
                CommonHelper.BindDropDown(ddlCompanyAdd, ds.Tables[0], "Value", "Id", true, false);
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ResetControls();
        txtSearch.Text = "";
        ddlcompanySearch.SelectedValue = "0";
        BindRepeater();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindRepeater();
    }
    protected void lbEdit_Click(object sender, EventArgs e)
    {
        try
        {
            hfModalId.Value = "";
            LinkButton btnEdit = (LinkButton)sender;
            RepeaterItem rptItem = (RepeaterItem)btnEdit.NamingContainer;
            int ID = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfId")).Value);
            var lstEdit = context.Setup_Location.Where(x => x.LocationId == ID).FirstOrDefault();
            if (lstEdit != null)
            {
                if (lstEdit.CompanyId > 0)
                {
                    ddlGroupAdd.SelectedValue = lstEdit.Setup_Company.GroupId.ToString();
                    ddlGroupAdd_SelectedIndexChanged(null, null);
                    ddlCompanyAdd.SelectedValue = lstEdit.Setup_Company.CompanyId.ToString();
                }

                txtNameAdd.Text = lstEdit.LocationName;
                txtThirPartyMappingId.Text = lstEdit.ThirdPartyMappingId;
                hfModalId.Value = ID.ToString();
                Div_Save.Visible = true;
                OpenPopup();
            }
        }
        catch (Exception ex)
        {
            divError.Visible = true;
            lblError.InnerText = ex.Message;
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
            int Count = context.Setup_Employee.Where(a => a.LocationId == Id).Count();
            if (Count == 0)
            {
                Setup_Location obj = context.Setup_Location.FirstOrDefault(j => j.LocationId == Id);

                #region Audit Logs
                //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
                DataTable Datat1 = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(Id), "Setup_Location", 3);
                #endregion

                DateTime dt = DateTime.Now;
                obj.IsActive = false;
                obj.ModifiedBy = UserId;
                obj.ModifiedDate = dt;
                obj.UserIP = UserIP;
                context.SaveChanges();
                //context.INSERT_INTO_AuditLog(Convert.ToString(obj.LocationId), "Setup_Location", (int)Constant.OperationType.DELETE, UserId);
                Success("Deleted successfully.");
                BindRepeater();
            }
            else
            {
                Error("Unable to delete because employee exist against this location");
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
        try
        {
            if (CommonHelper.IsCheckRecordExistAccordingToSapID(Constant.Sap_staging_Table.Setup_Location, txtThirPartyMappingId.Text.Trim(), hfModalId.Value == string.Empty ? 0 : Convert.ToInt32(hfModalId.Value),context))
            {
                divError.Visible = false;
                string checkIsExist = CheckAlreadyNameExists(txtNameAdd.Text.Trim());
                if (checkIsExist == "")
                {
                    if (hfModalId.Value == string.Empty)
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
                    Error(checkIsExist);
                }
            }
            else
            {

                Error("Record Already Exist Against Third Party Id : " + txtThirPartyMappingId.Text.Trim() + "");

            }
            
        }
        catch (Exception ex)
        {
            divError.Visible = true;
            lblError.InnerText = ex.Message;
        }
    }
    private void Add()
    {
        bool Status = false;
        DateTime dt = DateTime.Now;

        Setup_Location obj = new Setup_Location();
        obj.LocationName = txtNameAdd.Text.Trim();
        obj.CompanyId = Convert.ToInt32(ddlCompanyAdd.SelectedValue);
        obj.CreatedBy = UserId;
        obj.CreatedDate = dt;
        obj.ThirdPartyMappingId = txtThirPartyMappingId.Text.Trim();
        obj.IsActive = true;
        obj.UserIP = UserIP;
        context.Setup_Location.Add(obj);
        context.SaveChanges();
        if (obj.LocationId > 0)
        {
            //context.INSERT_INTO_AuditLog(Convert.ToString(obj.LocationId), "Setup_Location", (int)Constant.OperationType.INSERT, UserId);
            Status = true;
        }

        if (Status == true)
        {
            Success("Added successfully.");
            ClosePopup();
            ResetControls();
            BindRepeater();
        }
    }
    private void Update()
    {
        bool Status = false;
        DateTime dt = DateTime.Now;
        int Id = Convert.ToInt32(hfModalId.Value);
        Setup_Location obj = context.Setup_Location.FirstOrDefault(j => j.LocationId == Id);

        #region Audit Logs
        //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
        DataTable Datat1 = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(Id), "Setup_Location", 2);
        #endregion

        obj.LocationName = txtNameAdd.Text.Trim();
        obj.CompanyId = Convert.ToInt32(ddlCompanyAdd.SelectedValue);
        obj.ModifiedBy = UserId;
        obj.ModifiedDate = dt;
        obj.IsActive = true;
        obj.ThirdPartyMappingId = txtThirPartyMappingId.Text.Trim();
        obj.UserIP = UserIP;
        context.SaveChanges();
        //context.INSERT_INTO_AuditLog(Convert.ToString(obj.LocationId), "Setup_Location", (int)Constant.OperationType.UPDATE, UserId);
        Status = true;
        if (Status == true)
        {
            Success("Edit successfully.");
            ClosePopup();
            ResetControls();
            BindRepeater();
        }
    }
    private void BindRepeater()
    {
        int CompanyId_ = ddlcompanySearch.SelectedValue == "" ? 0 : Convert.ToInt32(ddlcompanySearch.SelectedValue);
        List<int> CompanyIds = new List<int>();
        CompanyIds = CommonHelper.GetDropDownValuesArray(ddlcompanySearch);
        var List = context.Setup_Location.Where(a => a.IsActive == true &&
        a.Setup_Company.Setup_EmployeeCompanyMapping.Any(x => x.IsActive == true && x.EmployeeId == UserKey) &&
        (a.LocationName.Contains(txtSearch.Text.Trim()) || txtSearch.Text.Trim() == "") && (a.ThirdPartyMappingId.Contains(txtThirdPartyMappingIdSearch.Text.Trim()) || txtThirdPartyMappingIdSearch.Text.Trim() == "") &&
             (a.Setup_Company.CompanyId == CompanyId_ || CompanyIds.Contains(a.Setup_Company.CompanyId))
            )
          .Select(a => new
          {
              CompanyName = a.Setup_Company.CompanyName,
              ID = a.LocationId,
              Title = a.LocationName,
              ThirdPartyMappingId = a.ThirdPartyMappingId
          }).ToList().OrderBy(f => f.CompanyName).ThenBy(f => f.Title);
        rpt.DataSource = List;
        rpt.DataBind();

    }
    public string CheckAlreadyNameExists(string title)
    {
        string Msg = "";
        int ModalId = 0;
        if (hfModalId.Value != "")
        {
            ModalId = Convert.ToInt32(hfModalId.Value);
        }
        else
        {
            ModalId = 0;
        }
        int CompanyId = ddlCompanyAdd.SelectedValue == "" ? 0 : Convert.ToInt32(ddlCompanyAdd.SelectedValue);
        int Count = context.Setup_Location.Where(j => j.LocationName == title && j.CompanyId == CompanyId && j.IsActive == true && j.LocationId != ModalId).Count();
        if (Count > 0)
        {
            Msg = "Location Already Exist Against This Company";
        }

        return Msg;
    }
    private void ResetControls()
    {
        ddlCompanyAdd.SelectedValue = "0";
        txtNameAdd.Text = string.Empty;
        txtThirdPartyMappingIdSearch.Text = string.Empty;
        txtThirPartyMappingId.Text = string.Empty;
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
    private void SetRoleFeature()
    {
        //try
        //{

        //    string url = Request.Url.PathAndQuery;
        //    string list = menuRoleFeatureList.Replace("%0d%0a", "");
        //    var objRoleFeatureDTO = JsonConvert.DeserializeObject<List<RoleFeatureDTO>>(list);
        //    if (objRoleFeatureDTO != null && objRoleFeatureDTO.Count() > 0)
        //    {
        //        hfMenuItemId.Value = Convert.ToString(objRoleFeatureDTO[0].MenuItemId);
        //    }
        //    var Add_Feature = objRoleFeatureDTO.Where(a => a.IsActive == true && a.FeatureId == (int)Constant.Feature.Add && url.Contains(a.PageURL) == true);
        //    var View_Feature = objRoleFeatureDTO.Where(a => a.IsActive == true && a.FeatureId == (int)Constant.Feature.View && url.Contains(a.PageURL) == true);
        //    var Edit_Feature = objRoleFeatureDTO.Where(a => a.IsActive == true && a.FeatureId == (int)Constant.Feature.Edit && url.Contains(a.PageURL) == true);
        //    var Delete_Feature = objRoleFeatureDTO.Where(a => a.IsActive == true && a.FeatureId == (int)Constant.Feature.Delete && url.Contains(a.PageURL) == true);
        //    if (Add_Feature.Count() > 0)
        //    {
        //        IsAdd.Value = "1";
        //        Btn_Add.Visible = true;
        //    }
        //    else
        //    {
        //        IsAdd.Value = "0";
        //        Btn_Add.Visible = false;
        //    }
        //    if (View_Feature.Count() > 0)
        //    {
        //        IsView.Value = "1";
        //    }
        //    else
        //    {
        //        IsView.Value = "0";
        //    }
        //    if (Edit_Feature.Count() > 0)
        //    {
        //        IsEdit.Value = "1";
        //    }
        //    else
        //    {
        //        IsEdit.Value = "0";

        //    }
        //    if (Delete_Feature.Count() > 0)
        //    {
        //        IsDelete.Value = "1";
        //    }
        //    else
        //    {
        //        IsDelete.Value = "0";
        //    }
        //}
        //catch (Exception ex) { }
    }
    protected void rpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                LinkButton lblView = (LinkButton)e.Item.FindControl("lblView");
                LinkButton lbEdit = (LinkButton)e.Item.FindControl("lbEdit");
                LinkButton lbDelete = (LinkButton)e.Item.FindControl("lbDelete");
                if (IsView.Value == "1")
                {
                    lblView.Visible = true;
                }
                else
                {
                    lblView.Visible = false;
                }
                if (IsEdit.Value == "1")
                {
                    lbEdit.Visible = true;
                }
                else
                {
                    lbEdit.Visible = false;
                }
                if (IsDelete.Value == "1")
                {
                    lbDelete.Visible = true;
                }
                else
                {
                    lbDelete.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            divError.Visible = true;
            lblError.InnerText = ex.Message.ToString();

        }
    }
    protected void lblView_Click(object sender, EventArgs e)
    {
        try
        {
            hfModalId.Value = "";
            LinkButton btnEdit = (LinkButton)sender;
            RepeaterItem rptItem = (RepeaterItem)btnEdit.NamingContainer;
            int ID = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfId")).Value);
            var lstEdit = context.Setup_Location.Where(x => x.LocationId == ID).FirstOrDefault();
            if (lstEdit != null)
            {
                if (lstEdit.CompanyId > 0)
                {
                    ddlGroupAdd.SelectedValue = lstEdit.Setup_Company.GroupId.ToString();
                    ddlGroupAdd_SelectedIndexChanged(null, null);
                    ddlCompanyAdd.SelectedValue = lstEdit.Setup_Company.CompanyId.ToString();
                }

                txtNameAdd.Text = lstEdit.LocationName;
                txtThirPartyMappingId.Text = lstEdit.ThirdPartyMappingId;
                hfModalId.Value = ID.ToString();
                Div_Save.Visible = false;
                OpenPopup();
            }
        }
        catch (Exception ex)
        {
            divError.Visible = true;
            lblError.InnerText = ex.Message;
        }
    }
    protected void Btn_Add_Click(object sender, EventArgs e)
    {
        ResetControls();
        Div_Save.Visible = true;
        OpenPopup();
    }



}