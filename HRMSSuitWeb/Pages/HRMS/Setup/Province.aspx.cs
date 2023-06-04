using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_HRMS_Setup_Province : Base
{
    Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtSocialSecurityLimit.Attributes.Add("onkeypress", "return numeralsOnly(event)");
            txtMaxAmount.Attributes.Add("onkeypress", "return numeralsOnly(event)");
            txtEOBILimit.Attributes.Add("onkeypress", "return numeralsOnly(event)");
            txtContrubution.Attributes.Add("onkeypress", "return numeralsOnly(event)");
            SetRoleFeature();
            BindDropdown();
            BindRepeater();
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        BindDropdown();

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
        var lstEdit = context.Setup_Province.Where(x => x.ProvinceId == ID).FirstOrDefault();
        if (lstEdit != null)
        {
            txtProvinceAdd.Text = lstEdit.Province;
            txtThirPartyMappingId.Text = lstEdit.ThirdPartyMappingId;
            txtSocialSecurityLimit.Text = lstEdit.SocialSecurityLimit == null ? "" : lstEdit.SocialSecurityLimit.ToString();
            txtMaxAmount.Text = lstEdit.MaxAmount == null ? "" : lstEdit.MaxAmount.ToString();
            txtContrubution.Text = lstEdit.Contribution == null ? "" : lstEdit.Contribution.ToString();
            txtEOBILimit.Text = lstEdit.EOBILimit == null ? "" : lstEdit.EOBILimit.ToString();
            ddlCountryAdd.SelectedValue = lstEdit.CountryId.ToString();
            hfModalId.Value = ID.ToString();
            Div_Save.Visible = true;
            OpenPopup();
        }
    }
    protected void lblView_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnEdit = (LinkButton)sender;
            RepeaterItem rptItem = (RepeaterItem)btnEdit.NamingContainer;
            int ID = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfId")).Value);
            var lstEdit = context.Setup_Province.Where(x => x.ProvinceId == ID).FirstOrDefault();
            if (lstEdit != null)
            {
                txtProvinceAdd.Text = lstEdit.Province;
                txtThirPartyMappingId.Text = lstEdit.ThirdPartyMappingId;
                txtSocialSecurityLimit.Text = lstEdit.SocialSecurityLimit == null ? "" : lstEdit.SocialSecurityLimit.ToString();
                txtMaxAmount.Text = lstEdit.MaxAmount == null ? "" : lstEdit.MaxAmount.ToString();
                txtContrubution.Text = lstEdit.Contribution == null ? "" : lstEdit.Contribution.ToString();
                txtEOBILimit.Text = lstEdit.EOBILimit == null ? "" : lstEdit.EOBILimit.ToString();
                ddlCountryAdd.SelectedValue = lstEdit.CountryId.ToString();
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
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnDelete = (LinkButton)sender;
            RepeaterItem rptItem = (RepeaterItem)btnDelete.NamingContainer;
            int Id = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfId")).Value);
            divError.Visible = false;

            Setup_Province obj = context.Setup_Province.FirstOrDefault(j => j.ProvinceId == Id);

            #region Audit Logs
            //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
            DataTable Datat1 = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(Id), "Setup_Province", 3);
            #endregion

            DateTime dt = DateTime.Now;
            obj.IsActive = false;
            obj.ModifiedBy = UserKey;
            obj.ModifiedDate = dt;
           
            context.SaveChanges();
            Success("Deleted successfully.");
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
        try
        {
            if (CommonHelper.IsCheckRecordExistAccordingToSapID(Constant.Sap_staging_Table.Setup_Province, txtThirPartyMappingId.Text.Trim(), hfModalId.Value == string.Empty ? 0 : Convert.ToInt32(hfModalId.Value), context))
            {
               
                    if (hfModalId.Value == string.Empty || hfModalId.Value == "0")
                        Add();
                    else
                        Update();
               
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
        DateTime dt = DateTime.Now;
        Setup_Province obj = new Setup_Province();
        obj.CountryId = Convert.ToInt32(ddlCountryAdd.SelectedValue);
        obj.Province = txtProvinceAdd.Text.Trim();
        obj.CreatedBy = UserKey;
        obj.CreatedDate = dt;
        obj.IsActive = true;
        obj.SocialSecurityLimit = String.IsNullOrEmpty(txtSocialSecurityLimit.Text) ? 0 : Convert.ToInt32(txtSocialSecurityLimit.Text);
        obj.MaxAmount = String.IsNullOrEmpty(txtMaxAmount.Text) ? 0 : Convert.ToInt32(txtMaxAmount.Text);
        obj.Contribution = String.IsNullOrEmpty(txtContrubution.Text) ? 0 : Convert.ToInt32(txtContrubution.Text);
        obj.EOBILimit = String.IsNullOrEmpty(txtEOBILimit.Text) ? 0 : Convert.ToInt32(txtEOBILimit.Text);
        obj.ThirdPartyMappingId = txtThirPartyMappingId.Text.Trim();
        context.Setup_Province.Add(obj);
        context.SaveChanges();
        Success("Added successfully.");
        ResetControls();
        ClosePopup();
        BindRepeater();
    }
    private void Update()
    {
        DateTime dt = DateTime.Now;

        int Id = Convert.ToInt32(hfModalId.Value);
        Setup_Province obj = context.Setup_Province.FirstOrDefault(j => j.ProvinceId == Id);

        #region Audit Logs
        //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
        DataTable Datat1 = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(Id), "Setup_Province", 2);
        #endregion

        obj.CountryId = Convert.ToInt32(ddlCountryAdd.SelectedValue);
        obj.Province = txtProvinceAdd.Text.Trim();
        obj.ModifiedBy = UserKey;
        obj.SocialSecurityLimit = String.IsNullOrEmpty(txtSocialSecurityLimit.Text) ? 0 : Convert.ToInt32(txtSocialSecurityLimit.Text);
        obj.MaxAmount = String.IsNullOrEmpty(txtMaxAmount.Text) ? 0 : Convert.ToInt32(txtMaxAmount.Text);
        obj.Contribution = String.IsNullOrEmpty(txtContrubution.Text) ? 0 : Convert.ToInt32(txtContrubution.Text);
        obj.EOBILimit = String.IsNullOrEmpty(txtEOBILimit.Text) ? 0 : Convert.ToInt32(txtEOBILimit.Text);
        obj.ModifiedDate = dt;
        obj.ThirdPartyMappingId = txtThirPartyMappingId.Text.Trim();
        obj.IsActive = true;
        context.SaveChanges();
        Success("Edit successfully.");
        ResetControls();
        ClosePopup();
        BindRepeater();
    }
    private void BindRepeater()
    {
        int CountryId = ddlCountrySearch.SelectedValue == "" ? 0 : Convert.ToInt32(ddlCountrySearch.SelectedValue);
        var List = context.Setup_Province.Where(a => a.IsActive == true && a.Setup_Country.IsActive == true &&
            (a.Province.Contains(txtProvinceSearch.Text.Trim()) || txtProvinceSearch.Text.Trim() == "") &&
            (a.CountryId == CountryId || CountryId == 0) && (a.ThirdPartyMappingId.Contains(txtThirdPartyMappingIdSearch.Text.Trim()) || txtThirdPartyMappingIdSearch.Text.Trim() == string.Empty))
          .Select(a => new
          {
              ID = a.ProvinceId,
              Title = a.Province,
              CountryName = a.Setup_Country.CountryName,
              ThirdPartyMappingId = a.ThirdPartyMappingId,
              SocialSecurityLimit = a.SocialSecurityLimit,
              Contribution = a.Contribution,
              EOBILimit = a.EOBILimit,
              MaxAmount = a.MaxAmount,
          }).ToList().OrderBy(f => f.CountryName).ThenBy(g => g.Title).ToList();
        rpt.DataSource = List;
        rpt.DataBind();
    }
    public bool CheckAlreadyNameExists(string title)
    {
        int ModalId = 0;
        if (hfModalId.Value != "")
        {
            ModalId = Convert.ToInt32(hfModalId.Value);
        }
        else
        {
            ModalId = 0;
        }
        int CategoryId = ddlCountryAdd.SelectedValue == "" ? 0 : Convert.ToInt32(ddlCountryAdd.SelectedValue);
        Setup_Province obj = context.Setup_Province.FirstOrDefault(j => j.Province == title && j.IsActive == true && j.CountryId == CategoryId && j.ProvinceId != ModalId);
        if (obj != null)
        {
            return true;
        }
        return false;
    }
    private void ResetControls()
    {
        txtProvinceSearch.Text = string.Empty;
        txtProvinceAdd.Text = string.Empty;
        txtSocialSecurityLimit.Text = "0";
        txtMaxAmount.Text = "0";
        txtContrubution.Text = "0";
        txtEOBILimit.Text = "0";
        txtThirdPartyMappingIdSearch.Text = string.Empty;
        txtThirPartyMappingId.Text = string.Empty;
        hfModalId.Value = "0";
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
        var LstCountry = context.Setup_Country.Where(a => a.IsActive == true)
          .Select(a => new
          {
              Id = a.CountryId,
              Value = a.CountryName,
          })
          .ToList();

        CommonHelper.BindDropDown(ddlCountryAdd, LstCountry, "Value", "Id", true, false);
        CommonHelper.BindDropDown(ddlCountrySearch, LstCountry, "Value", "Id", true, false);
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

    private void SetRoleFeature()
    {
        try
        {

            //string url = Request.Url.PathAndQuery;
            //string list = menuRoleFeatureList.Replace("%0d%0a", "");
            //var objRoleFeatureDTO = JsonConvert.DeserializeObject<List<RoleFeatureDTO>>(list);
            //var Add_Feature = objRoleFeatureDTO.Where(a => a.IsActive == true && a.FeatureId == (int)Constant.Feature.Add && url.Contains(a.PageURL) == true);
            //var View_Feature = objRoleFeatureDTO.Where(a => a.IsActive == true && a.FeatureId == (int)Constant.Feature.View && url.Contains(a.PageURL) == true);
            //var Edit_Feature = objRoleFeatureDTO.Where(a => a.IsActive == true && a.FeatureId == (int)Constant.Feature.Edit && url.Contains(a.PageURL) == true);
            //var Delete_Feature = objRoleFeatureDTO.Where(a => a.IsActive == true && a.FeatureId == (int)Constant.Feature.Delete && url.Contains(a.PageURL) == true);
            //if (Add_Feature.Count() > 0)
            //{
            //    IsAdd.Value = "1";
            //    Btn_Add.Visible = true;
            //}
            //else
            //{
            //    IsAdd.Value = "0";
            //    Btn_Add.Visible = false;
            //}
            //if (View_Feature.Count() > 0)
            //{
            //    IsView.Value = "1";
            //}
            //else
            //{
            //    IsView.Value = "0";
            //}
            //if (Edit_Feature.Count() > 0)
            //{
            //    IsEdit.Value = "1";
            //}
            //else
            //{
            //    IsEdit.Value = "0";

            //}
            //if (Delete_Feature.Count() > 0)
            //{
            //    IsDelete.Value = "1";
            //}
            //else
            //{
            //    IsDelete.Value = "0";
            //}
        }
        catch (Exception ex) { }
    }



    protected void Btn_Add_Click(object sender, EventArgs e)
    {
        ResetControls();
        Div_Save.Visible = true;
        OpenPopup();
    }
}