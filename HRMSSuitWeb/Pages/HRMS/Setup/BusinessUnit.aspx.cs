using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using Newtonsoft.Json;
using System.Data;

public partial class Pages_HRMS_Setup_BusinessUnit : Base
{
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtSearch.Text = string.Empty;
        ResetControls();
        BindDropdown();
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
            int BusinessUnitId = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfId")).Value);
            var List = context.TS_Setup_BusinessUnit.Where(x => x.BusinessUnitId == BusinessUnitId).FirstOrDefault();
            if (List != null)
            {
                ddlGroupAdd.SelectedValue = List.Setup_Company.GroupId.ToString();
                ddlGroupAdd_SelectedIndexChanged(null, null);
                ddlCompanyAdd.SelectedValue = List.CompanyId.ToString();
                txtThirPartyMappingId.Text = List.ThirdPartyMappingId;
                txtNameAdd.Text = List.BusinessUnitName;
                hfModalId.Value = BusinessUnitId.ToString();
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
            ResetControls();
            LinkButton btnDelete = (LinkButton)sender;
            RepeaterItem rptItem = (RepeaterItem)btnDelete.NamingContainer;
            int Id = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfId")).Value);

            if (Id > 0)
            { 
                string Msg = IsTransectionExist(Id);
                if (Msg == "")
                {
                    TS_Setup_BusinessUnit obj = context.TS_Setup_BusinessUnit.FirstOrDefault(j => j.BusinessUnitId == Id);
                    if (obj != null)
                    {
                        #region Audit Logs
                        //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
                        DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(Id), "TS_Setup_BusinessUnit", 3);
                        #endregion

                        DateTime dt = DateTime.Now;
                        obj.IsActive = false;
                        obj.ModifiedBy = UserId;
                        obj.ModifiedDate = dt;
                        obj.UserIP = UserIP;
                        context.SaveChanges();
                        //context.INSERT_INTO_AuditLog(Convert.ToString(obj.BusinessUnitId), "TS_Setup_BusinessUnit", (int)Constant.OperationType.DELETE, UserId);
                        Success("Deleted successfully.");
                        BindRepeater();
                    }
                }
                else
                {
                    Error(Msg);
                }
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

            if (CommonHelper.IsCheckRecordExistAccordingToSapID(Constant.Sap_staging_Table.TS_Setup_BusinessUnit, txtThirPartyMappingId.Text.Trim(), hfModalId.Value == string.Empty ? 0 : Convert.ToInt32(hfModalId.Value),context))
            {
                int ModalId = hfModalId.Value == "" ? 0 : Convert.ToInt32(hfModalId.Value);
                bool checkIsExist = CheckAlreadyNameExists(txtNameAdd.Text.Trim(), ModalId);
                if (checkIsExist == false)
                {
                    if (hfModalId.Value == "")
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
                    Error("Business unit already exist against this company.");
                }
            }
            else
            {

                Error("Record Already Exist Against Third Party Id : " + txtThirPartyMappingId.Text.Trim() + "");

            }
            
        }
        catch { }
    }
    private void Add()
    {
        DateTime dt = DateTime.Now;
        TS_Setup_BusinessUnit obj = new TS_Setup_BusinessUnit();
        obj.BusinessUnitName = txtNameAdd.Text.Trim();
        obj.CompanyId = Convert.ToInt32(ddlCompanyAdd.SelectedValue);
        obj.CreatedBy = UserId;
        obj.CreatedDate = dt;
        obj.IsActive = true;
        obj.UserIP = UserIP;
        obj.ThirdPartyMappingId = txtThirPartyMappingId.Text;
        context.TS_Setup_BusinessUnit.Add(obj);
        context.SaveChanges();
        if (obj.BusinessUnitId > 0)
        {
            //context.INSERT_INTO_AuditLog(Convert.ToString(obj.BusinessUnitId), "TS_Setup_BusinessUnit", (int)Constant.OperationType.INSERT, UserId);
            Success("Added successfully.");
            ClosePopup();
            ResetControls();
            BindRepeater();
        }
    }
    private void Update()
    {
        DateTime dt = DateTime.Now;
        int Id = Convert.ToInt32(hfModalId.Value); 
        TS_Setup_BusinessUnit obj = context.TS_Setup_BusinessUnit.FirstOrDefault(j => j.BusinessUnitId == Id);
        if (obj != null)
        {
            #region Audit Logs
            //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
            DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(Id), "TS_Setup_BusinessUnit", 2);
            #endregion

            obj.BusinessUnitName = txtNameAdd.Text.Trim();
            obj.CompanyId = Convert.ToInt32(ddlCompanyAdd.SelectedValue);
            obj.ModifiedBy = UserId;
            obj.ModifiedDate = dt;
            obj.UserIP = UserIP;
            obj.IsActive = true;
            obj.ThirdPartyMappingId = txtThirPartyMappingId.Text;
            context.SaveChanges();
            //context.INSERT_INTO_AuditLog(Convert.ToString(obj.BusinessUnitId), "TS_Setup_BusinessUnit", (int)Constant.OperationType.UPDATE, UserId);
            Success("Edit successfully.");
            ClosePopup();
            ResetControls();
            BindRepeater();
        }
    }
    private void BindRepeater()
    {
        int GroupId = ddlgroupSearch.SelectedValue == "" ? 0 : Convert.ToInt32(ddlgroupSearch.SelectedValue);
        int CompanyId_ = ddlcompanySearch.SelectedValue == "" ? 0 : Convert.ToInt32(ddlcompanySearch.SelectedValue);
        List<int> CompanyIds = new List<int>();
        CompanyIds = CommonHelper.GetDropDownValuesArray(ddlcompanySearch);

        var List = context.TS_Setup_BusinessUnit.Where(a => a.IsActive == true &&
            (a.BusinessUnitName.Contains(txtSearch.Text.Trim()) || txtSearch.Text.Trim() == "") &&
            (a.Setup_Company.GroupId == GroupId || GroupId == 0) &&

            (a.Setup_Company.CompanyId == CompanyId_ || CompanyIds.Contains(a.Setup_Company.CompanyId)) && (a.ThirdPartyMappingId.Contains(txtThirdPartyMappingIdSearch.Text.Trim()) || txtThirdPartyMappingIdSearch.Text.Trim() == string.Empty) 
            &&
            a.Setup_Company.Setup_EmployeeCompanyMapping.Any(x => x.IsActive == true && x.EmployeeId == UserKey))
          .Select(a => new
          {
              Group = a.Setup_Company.Setup_Group.GroupName,
              ID = a.BusinessUnitId,
              Title = a.BusinessUnitName,
              Company = a.Setup_Company.CompanyName,
              ThirdPartyMappingId = a.ThirdPartyMappingId

          }).OrderBy(g => g.Company).ThenBy(f => f.Title).ToList();
        rpt.DataSource = List;
        rpt.DataBind();
    }
    public bool CheckAlreadyNameExists(string title, int Id)
    {
        int CompanyId = ddlCompanyAdd.SelectedValue == "" ? 0 : Convert.ToInt32(ddlCompanyAdd.SelectedValue);
        TS_Setup_BusinessUnit obj = context.TS_Setup_BusinessUnit.FirstOrDefault(j => j.IsActive == true && j.BusinessUnitName == title && j.BusinessUnitId != Id && j.CompanyId == CompanyId);
        if (obj != null)
        {
            return true;
        }
        return false;
    }
    private void ResetControls()
    {
        BindDropdown();
        divError.Visible = false;
        lblError.InnerText = "";
        txtNameAdd.Text = string.Empty;
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
    private string IsTransectionExist(int Id)
    {
        string Msg = "";
        int Count = context.Setup_Department.Where(a => a.IsActive == true && a.BusinessUnitId == Id).Count();
        if (Count > 0)
        {
            Msg = "Unable to delete because department exist against this business unit";
        }
        return Msg;
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
        DataSet ds = CommonHelper.Get_Control_Data_EmployeeSearchFilter("OnChangeGroup", GroupId, 0, 0, 0, 0, 0, 0, 0,0, 0, 0, 0, UserKey);
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
        DataSet ds = CommonHelper.Get_Control_Data_EmployeeSearchFilter("OnChangeGroup", GroupId, 0, 0, 0, 0, 0, 0,0, 0, 0, 0, 0, UserKey);
        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables.Count == 1)
            {
                CommonHelper.BindDropDown(ddlCompanyAdd, ds.Tables[0], "Value", "Id", true, false);
            }
        }
    }



    private void SetRoleFeature()
    {
        //try
        //{

        //    string url = Request.Url.PathAndQuery;
        //    string list = menuRoleFeatureList.Replace("%0d%0a", "");
        //    var objRoleFeatureDTO = JsonConvert.DeserializeObject<List<RoleFeatureDTO>>(list);
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
            int BusinessUnitId = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfId")).Value);
            var List = context.TS_Setup_BusinessUnit.Where(x => x.BusinessUnitId == BusinessUnitId).FirstOrDefault();
            if (List != null)
            {
                ddlGroupAdd.SelectedValue = List.Setup_Company.GroupId.ToString();
                ddlGroupAdd_SelectedIndexChanged(null, null);
                ddlCompanyAdd.SelectedValue = List.CompanyId.ToString();
                txtNameAdd.Text = List.BusinessUnitName;
                hfModalId.Value = BusinessUnitId.ToString();
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

