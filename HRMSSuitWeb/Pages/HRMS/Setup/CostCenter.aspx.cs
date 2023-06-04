using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Data;

public partial class Pages_HRMS_Setup_CostCenter : Base
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
        PagingHandler();
    }
    #region PAGING
    private void PagingHandler()
    {
        PagingAndSorting.ImgNext.Click += ImgNext_Click;
        PagingAndSorting.ImgPrevious.Click += ImgPrevious_Click;
        PagingAndSorting.DdlPage.SelectedIndexChanged += DdlPage_SelectedIndexChanged;
        PagingAndSorting.DdlPageSize.SelectedIndexChanged += DdlPageSize_SelectedIndexChanged;
    }

    void DdlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindRepeater();
    }
    void DdlPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindRepeater();
    }
    void ImgNext_Click(object sender, ImageClickEventArgs e)
    {
        BindRepeater();
    }
    void ImgPrevious_Click(object sender, ImageClickEventArgs e)
    {
        BindRepeater();
    }
    #endregion

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtSearch.Text = string.Empty;
        txtCostCenterCodeSearch.Text = string.Empty;
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
            int ID = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfId")).Value);
            var List = context.Setup_CostCenter.Where(x => x.CostCenterId == ID).FirstOrDefault();
            if (List != null)
            {
                ddlGroupAdd.SelectedValue = List.Setup_Company.GroupId.ToString();
                ddlGroupAdd_SelectedIndexChanged(null, null);
                ddlCompanyAdd.SelectedValue = List.CompanyId.ToString();
                txtNameAdd.Text = List.CostCenterName;
                txtCostCenterCode.Text = List.CostCenterCode;
                txtThirPartyMappingId.Text = List.ThirdPartyMappingId;
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
            ResetControls();
            LinkButton btnDelete = (LinkButton)sender;
            RepeaterItem rptItem = (RepeaterItem)btnDelete.NamingContainer;
            int Id = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfId")).Value);
            if (Id > 0)
            {
                string Msg = IsTransectionExist(Id);
                if (Msg == "")
                {
                    Setup_CostCenter obj = context.Setup_CostCenter.FirstOrDefault(j => j.CostCenterId == Id);
                    if (obj != null)
                    {
                        #region Audit Logs
                        //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
                        DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(Id), "Setup_CostCenter", 3);
                        #endregion

                        DateTime dt = DateTime.Now;
                        obj.IsActive = false;
                        obj.ModifiedBy = UserId;
                        obj.ModifiedDate = dt;
                        obj.UserIP = UserIP;
                        context.SaveChanges();
                        //context.INSERT_INTO_AuditLog(Convert.ToString(obj.CostCenterId), "Setup_CostCenter", (int)Constant.OperationType.DELETE, UserId);
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
    private string IsTransectionExist(int Id)
    {
        string Msg = "";
        int Count = context.Setup_Employee.Where(a => a.IsActive == true && a.CostCenterId == Id).Count();
        if (Count > 0)
        {
            Msg = "Unable to delete because employee exist against this cost center";
        }
        else
        {
        }

        return Msg;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (CommonHelper.IsCheckRecordExistAccordingToSapID(Constant.Sap_staging_Table.Setup_CostCenter, txtThirPartyMappingId.Text.Trim(), hfModalId.Value == string.Empty ? 0 : Convert.ToInt32(hfModalId.Value),context))
            {
                string Msg = CheckAlreadyNameExists(txtCostCenterCode.Text.Trim(), txtNameAdd.Text.Trim());
                if (Msg == "")
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
                    Error(Msg);
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
        Setup_CostCenter obj = new Setup_CostCenter();
        obj.CompanyId = Convert.ToInt32(ddlCompanyAdd.SelectedValue);
        obj.CostCenterCode = txtCostCenterCode.Text.Trim();
        obj.CostCenterName = txtNameAdd.Text.Trim();
        obj.CreatedBy = UserId;
        obj.CreatedDate = dt;
        obj.UserIP = UserIP;
        obj.ThirdPartyMappingId = txtThirPartyMappingId.Text.Trim();
        obj.IsActive = true;
        context.Setup_CostCenter.Add(obj);
        context.SaveChanges();
        if (obj.CostCenterId > 0)
        {
            //context.INSERT_INTO_AuditLog(Convert.ToString(obj.CostCenterId), "Setup_CostCenter", (int)Constant.OperationType.INSERT, UserId);
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

        #region Audit Logs
        //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
        DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(Id), "Setup_CostCenter", 2);
        #endregion

        Setup_CostCenter obj = context.Setup_CostCenter.FirstOrDefault(j => j.CostCenterId == Id);
        obj.CompanyId = Convert.ToInt32(ddlCompanyAdd.SelectedValue);
        obj.CostCenterName = txtNameAdd.Text.Trim();
        obj.CostCenterCode = txtCostCenterCode.Text.Trim();
        obj.ModifiedBy = UserId;
        obj.ModifiedDate = dt;
        obj.ThirdPartyMappingId = txtThirPartyMappingId.Text.Trim();
        obj.UserIP = UserIP;
        obj.IsActive = true;
        context.SaveChanges();
        //context.INSERT_INTO_AuditLog(Convert.ToString(obj.CostCenterId), "Setup_CostCenter", (int)Constant.OperationType.UPDATE, UserId);
        Success("Edit successfully.");
        ClosePopup();
        ResetControls();
        BindRepeater();

    }
    private void BindRepeater()
    {
       
        int pageSize = 50;
        int pageNumber = 1;
        if (PagingAndSorting.DdlPageSize.SelectedValue.toInt() > 0)
        {
            pageSize = PagingAndSorting.DdlPageSize.SelectedValue.toInt();
        }
        if (PagingAndSorting.DdlPage.Items.Count > 0)
        {
            pageNumber = PagingAndSorting.DdlPage.SelectedValue.toInt();
        }

        int skip = pageNumber * pageSize - pageSize;
        int GroupId = ddlgroupSearch.SelectedValue == "" ? 0 : Convert.ToInt32(ddlgroupSearch.SelectedValue);
        int CompanyId_ = ddlcompanySearch.SelectedValue == "" ? 0 : Convert.ToInt32(ddlcompanySearch.SelectedValue);
        List<int> CompanyIds = new List<int>();
        CompanyIds = CommonHelper.GetDropDownValuesArray(ddlcompanySearch);

        var List = context.Setup_CostCenter.Where(a => a.IsActive == true && a.Setup_Company.IsActive == true &&
          a.Setup_Company.Setup_EmployeeCompanyMapping.Any(x => x.IsActive == true && x.EmployeeId == UserKey) &&
            (a.CostCenterName.Contains(txtSearch.Text.Trim()) || txtSearch.Text.Trim() == "") &&
            (a.CostCenterCode.Contains(txtCostCenterCodeSearch.Text.Trim()) || txtCostCenterCodeSearch.Text.Trim() == "") &&
            (a.Setup_Company.GroupId == GroupId || GroupId == 0) &&
            (a.Setup_Company.CompanyId == CompanyId_ || CompanyIds.Contains(a.Setup_Company.CompanyId)) && (a.ThirdPartyMappingId.Contains(txtThirdPartyMappingIdSearch.Text.Trim()) || txtThirdPartyMappingIdSearch.Text.Trim() == string.Empty)
            )
          .Select(a => new
          {
              Group = a.Setup_Company.Setup_Group.GroupName,
              ID = a.CostCenterId,
              Title = a.CostCenterName,
              Company = a.Setup_Company.CompanyName,
              Code = a.CostCenterCode,
              ThirdPartyMappingId = a.ThirdPartyMappingId
          }).ToList();
        var List_ = List.OrderBy(a => a.Company).Skip(skip).Take(pageSize).ToList();
        rpt.DataSource = List_;
        rpt.DataBind();
        PagingAndSorting.setPagingOptions(List.Count());

    }
    public string CheckAlreadyNameExists(string code, string description)
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

        int Count = context.Setup_CostCenter.Where(j => j.CostCenterName == description && j.IsActive == true && j.CompanyId == CompanyId && j.CostCenterId != ModalId).Count();
        if (Count > 0)
        {
            Msg = "Cost center already exist against this company";
        }
        else
        {
            Count = context.Setup_CostCenter.Where(j => j.CostCenterCode == code && j.IsActive == true && j.CompanyId == CompanyId && j.CostCenterId != ModalId).Count();
            if (Count > 0)
            {
                Msg = "Code already exist against this company";
            }
        }
        return Msg;
    }
    private void ResetControls()
    {
        BindDropdown();
        divError.Visible = false;
        lblError.InnerText = "";
        txtNameAdd.Text = string.Empty;
        txtThirdPartyMappingIdSearch.Text = string.Empty;
        txtThirPartyMappingId.Text = string.Empty;
        txtCostCenterCode.Text = string.Empty;
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
            ResetControls();
            LinkButton btnEdit = (LinkButton)sender;
            RepeaterItem rptItem = (RepeaterItem)btnEdit.NamingContainer;
            int ID = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfId")).Value);
            var List = context.Setup_CostCenter.Where(x => x.CostCenterId == ID).FirstOrDefault();
            if (List != null)
            {
                ddlGroupAdd.SelectedValue = List.Setup_Company.GroupId.ToString();
                ddlGroupAdd_SelectedIndexChanged(null, null);
                ddlCompanyAdd.SelectedValue = List.CompanyId.ToString();
                txtNameAdd.Text = List.CostCenterName;
                txtCostCenterCode.Text = List.CostCenterCode;
                txtThirPartyMappingId.Text = List.ThirdPartyMappingId;
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