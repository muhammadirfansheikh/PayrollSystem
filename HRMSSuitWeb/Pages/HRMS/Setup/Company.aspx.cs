
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Newtonsoft.Json;
using System.Transactions;
using DAL;
public partial class Pages_HRMS_Setup_Company : Base
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
        txtSearch.Text = "";
        ResetControls();
        BindRepeater();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindRepeater();
    }
    private void BindRepeater()
    {
        int GroupId = ddlgroupSearch.SelectedValue == "" ? 0 : Convert.ToInt32(ddlgroupSearch.SelectedValue);
        var List = context.Setup_Company.Where(a => a.IsActive == true && a.GroupId == GroupId &&
          a.Setup_EmployeeCompanyMapping.Any(x => x.IsActive == true && x.EmployeeId == UserKey) &&
            (a.CompanyName.Contains(txtSearch.Text.Trim()) || txtSearch.Text.Trim() == ""))
          .Select(a => new
          {
              Id = a.CompanyId,
              Value = a.CompanyName,
              GroupName = a.Setup_Group.GroupName,
          })
          .OrderBy(b => b.GroupName).ThenBy(b => b.Value)
          .ToList();
        rpt.DataSource = List;
        rpt.DataBind();
    }
    protected void lbEdit_Click(object sender, EventArgs e)
    {
        ResetControls();
        LinkButton btnEdit = (LinkButton)sender;
        RepeaterItem rptItem = (RepeaterItem)btnEdit.NamingContainer;
        int ID = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfId")).Value);
        var lstEdit = context.Setup_Company.Where(x => x.CompanyId == ID).FirstOrDefault();
        if (lstEdit != null)
        {

            ddlGroupAdd.SelectedValue = lstEdit.GroupId.ToString();
            txtNameAdd.Text = lstEdit.CompanyName;
            hfModalId.Value = ID.ToString();
            Div_Save.Visible = true;
            OpenPopup();
        }
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        try
        {
            divError.Visible = false;
            LinkButton btnDelete = (LinkButton)sender;
            RepeaterItem rptItem = (RepeaterItem)btnDelete.NamingContainer;
            int Id = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfId")).Value);
            if (Id > 0)
            {
                string Msg = IsTransectionExist(Id);
                if (Msg == "")
                {
                    Setup_Company obj = context.Setup_Company.FirstOrDefault(j => j.CompanyId == Id);
                    if (obj != null)
                    {
                        #region Audit Logs
                        //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
                        DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(Id), "Setup_Company", 3);
                        #endregion

                        DateTime dt = DateTime.Now;
                        obj.IsActive = false;
                        obj.ModifiedBy = UserId;
                        obj.ModifiedDate = dt;
                        obj.UserIP = UserIP;
                        context.SaveChanges();
                        //context.INSERT_INTO_AuditLog(Convert.ToString(obj.CompanyId), "Setup_Company", (int)Constant.OperationType.DELETE, UserId);
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
        int Count = context.Setup_CostCenter.Where(a => a.IsActive == true && a.CompanyId == Id).Count();
        if (Count > 0)
        {
            Msg = "Unable to delete because cost center exist against this company";
        }
        else
        {
            Count = context.Setup_Category.Where(a => a.IsActive == true && a.CompanyId == Id).Count();
            if (Count > 0)
            {
                Msg = "Unable to delete because job category exist against this company";
            }
            else
            {
                Count = context.TS_Setup_BusinessUnit.Where(a => a.IsActive == true && a.CompanyId == Id).Count();
                if (Count > 0)
                {
                    Msg = "Unable to delete because business unit exist against this company";
                }
                else
                {
                    Count = context.Setup_Employee.Where(a => a.IsActive == true && a.CompanyId == Id).Count();
                    if (Count > 0)
                    {
                        Msg = "Unable to delete because employee exist against this company";
                    }
                }
            }
        }
        return Msg;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
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
                Error("Company already exist against this group.");
            }
        }
        catch { }
    }
    private void Add()
    {
        bool Status = false;
        DateTime dt = DateTime.Now;
        using (TransactionScope scope = new TransactionScope())
        {
            Setup_Company obj = new Setup_Company();
            obj.GroupId = Convert.ToInt32(ddlGroupAdd.SelectedValue);
            obj.CompanyName = txtNameAdd.Text.Trim();
            obj.CreatedBy = UserId;
            obj.CreatedDate = dt;
            obj.IsActive = true;
            obj.UserIP = UserIP;
            context.Setup_Company.Add(obj);
            context.SaveChanges();
            if (obj.CompanyId > 0)
            {
                //context.INSERT_INTO_AuditLog(Convert.ToString(obj.CompanyId), "Setup_Company", (int)Constant.OperationType.INSERT, UserId);
                InsertCompanyDefaultSettings(obj.CompanyId, dt);
                scope.Complete();
                Status = true;
            }
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
        Setup_Company obj = context.Setup_Company.FirstOrDefault(j => j.CompanyId == Id);
        if (obj != null)
        {
            #region Audit Logs
            //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
            DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(Id), "Setup_Company", 2);
            #endregion

            using (TransactionScope scope = new TransactionScope())
            {
                obj.CompanyName = txtNameAdd.Text.Trim();
                obj.GroupId = Convert.ToInt32(ddlGroupAdd.SelectedValue);
                obj.ModifiedBy = UserId;
                obj.ModifiedDate = dt;
                obj.IsActive = true;
                obj.UserIP = UserIP;
                context.SaveChanges();
                //context.INSERT_INTO_AuditLog(Convert.ToString(obj.CompanyId), "Setup_Company", (int)Constant.OperationType.UPDATE, UserId);
                scope.Complete();
                Status = true;
            }

            if (Status == true)
            {
                Success("Edit successfully.");
                ClosePopup();
                ResetControls();
                BindRepeater();
            }

        }
    }
    public bool CheckAlreadyNameExists(string title, int Id)
    {
        Setup_Company obj = context.Setup_Company.FirstOrDefault(j => j.CompanyName == title && j.IsActive == true && j.CompanyId != Id);
        if (obj != null)
        {
            return true;
        }
        return false;
    }
    private void ResetControls()
    {
        txtNameAdd.Text = string.Empty;
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
        //        //Div_Save.Visible = true;
        //    }
        //    else
        //    {
        //        IsAdd.Value = "0";
        //        Btn_Add.Visible = false;
        //        //Div_Save.Visible = false;
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
            var lstEdit = context.Setup_Company.Where(x => x.CompanyId == ID).FirstOrDefault();
            if (lstEdit != null)
            {
                ddlGroupAdd.SelectedValue = lstEdit.GroupId.ToString();
                txtNameAdd.Text = lstEdit.CompanyName;
                hfModalId.Value = ID.ToString();
                Div_Save.Visible = false;
                OpenPopup();
            }
        }
        catch (Exception ex)
        {
            divError.Visible = true;
            lblError.InnerText = ex.Message.ToString();
        }
    }
    protected void Btn_Add_Click(object sender, EventArgs e)
    {
        ResetControls();
        Div_Save.Visible = true;
        OpenPopup();
    }
    public void InsertCompanyDefaultSettings(int CompanyId_, DateTime dt)
    {
        /*

         #region LeaveSandwich
         var obj_LeaveSandwich = context.Setup_Settings.FirstOrDefault(a => a.IsActive == true && a.CompanyId == CompanyId_ && a.SettingTypeId == (int)Constant.SettingsTypeId.LeaveSandwich);
         if (obj_LeaveSandwich == null)
         {
             Setup_Settings objSetup_Settings = new Setup_Settings();
             objSetup_Settings.CompanyId = CompanyId_;
             objSetup_Settings.SettingTypeId = (int)Constant.SettingsTypeId.LeaveSandwich;
             objSetup_Settings.Value = "True";
             objSetup_Settings.CreatedBy = UserId;
             objSetup_Settings.CreatedDate = dt;
             objSetup_Settings.IsActive = true;
             objSetup_Settings.UserIP = UserIP;
             context.Setup_Settings.Add(objSetup_Settings);
             context.SaveChanges();
             context.INSERT_INTO_AuditLog(Convert.ToString(objSetup_Settings.SettingId), "Setup_Settings", (int)Constant.OperationType.INSERT, UserId);
         }


         #endregion

         #region NoOfDaysForFlexileaveDetuction
         var obj_NoOfDaysForFlexileaveDetuction = context.Setup_Settings.FirstOrDefault(a => a.IsActive == true && a.CompanyId == CompanyId_ && a.SettingTypeId == (int)Constant.SettingsTypeId.NoOfDaysForFlexileaveDetuction);
         if (obj_NoOfDaysForFlexileaveDetuction == null)
         {
             Setup_Settings objSetup_Settings = new Setup_Settings();
             objSetup_Settings.CompanyId = CompanyId_;
             objSetup_Settings.SettingTypeId = (int)Constant.SettingsTypeId.NoOfDaysForFlexileaveDetuction;
             objSetup_Settings.Value = "3";
             objSetup_Settings.CreatedBy = UserId;
             objSetup_Settings.CreatedDate = dt;
             objSetup_Settings.IsActive = true;
             objSetup_Settings.UserIP = UserIP;
             context.Setup_Settings.Add(objSetup_Settings);
             context.SaveChanges();
             context.INSERT_INTO_AuditLog(Convert.ToString(objSetup_Settings.SettingId), "Setup_Settings", (int)Constant.OperationType.INSERT, UserId);
         }

         #endregion

         #region OverTimePerHourinMinutes
         var obj_OverTimePerHourinMinutes = context.Setup_Settings.FirstOrDefault(a => a.IsActive == true && a.CompanyId == CompanyId_ && a.SettingTypeId == (int)Constant.SettingsTypeId.OverTimePerHourinMinutes);
         if (obj_OverTimePerHourinMinutes == null)
         {
             Setup_Settings objSetup_Settings = new Setup_Settings();
             objSetup_Settings.CompanyId = CompanyId_;
             objSetup_Settings.SettingTypeId = (int)Constant.SettingsTypeId.OverTimePerHourinMinutes;
             objSetup_Settings.Value = "30";
             objSetup_Settings.CreatedBy = UserId;
             objSetup_Settings.CreatedDate = dt;
             objSetup_Settings.IsActive = true;
             objSetup_Settings.UserIP = UserIP;
             context.Setup_Settings.Add(objSetup_Settings);
             context.SaveChanges();
             context.INSERT_INTO_AuditLog(Convert.ToString(objSetup_Settings.SettingId), "Setup_Settings", (int)Constant.OperationType.INSERT, UserId);
         }

         #endregion

         #region SickLeaveDocumentRequiredAfter
         var obj_SickLeaveDocumentRequiredAfter = context.Setup_Settings.FirstOrDefault(a => a.IsActive == true && a.CompanyId == CompanyId_ && a.SettingTypeId == (int)Constant.SettingsTypeId.SickLeaveDocumentRequiredAfter);
         if (obj_SickLeaveDocumentRequiredAfter == null)
         {
             Setup_Settings objSetup_Settings = new Setup_Settings();
             objSetup_Settings.CompanyId = CompanyId_;
             objSetup_Settings.SettingTypeId = (int)Constant.SettingsTypeId.SickLeaveDocumentRequiredAfter;
             objSetup_Settings.Value = "3";
             objSetup_Settings.Limit = 3;
             objSetup_Settings.CreatedBy = UserId;
             objSetup_Settings.CreatedDate = dt;
             objSetup_Settings.IsActive = true;
             objSetup_Settings.UserIP = UserIP;
             context.Setup_Settings.Add(objSetup_Settings);
             context.SaveChanges();
             context.INSERT_INTO_AuditLog(Convert.ToString(objSetup_Settings.SettingId), "Setup_Settings", (int)Constant.OperationType.INSERT, UserId);
         }

         #endregion

         #region CasualLeaveApplyLimit
         var obj_CasualLeaveApplyLimit = context.Setup_Settings.FirstOrDefault(a => a.IsActive == true && a.CompanyId == CompanyId_ && a.SettingTypeId == (int)Constant.SettingsTypeId.CasualLeaveApplyLimit);
         if (obj_CasualLeaveApplyLimit == null)
         {
             Setup_Settings objSetup_Settings = new Setup_Settings();
             objSetup_Settings.CompanyId = CompanyId_;
             objSetup_Settings.SettingTypeId = (int)Constant.SettingsTypeId.CasualLeaveApplyLimit;
             objSetup_Settings.Value = "3";
             objSetup_Settings.Limit = 3;
             objSetup_Settings.CreatedBy = UserId;
             objSetup_Settings.CreatedDate = dt;
             objSetup_Settings.IsActive = true;
             objSetup_Settings.UserIP = UserIP;
             context.Setup_Settings.Add(objSetup_Settings);
             context.SaveChanges();
             context.INSERT_INTO_AuditLog(Convert.ToString(objSetup_Settings.SettingId), "Setup_Settings", (int)Constant.OperationType.INSERT, UserId);
         }

         #endregion

         string Casual = Convert.ToString((int)Constant.TMSLeaveTypes.Casual);
         string Annual = Convert.ToString((int)Constant.TMSLeaveTypes.Annual);
         string Sick = Convert.ToString((int)Constant.TMSLeaveTypes.Sick);
         string WithoutPay = Convert.ToString((int)Constant.TMSLeaveTypes.WithoutPay);

         #region LeaveDeductionHierarchyForNonContractualEmployees_1_Casual
         var LeaveDeductionHierarchyForNonContractualEmployees_1_Casual = context.Setup_Settings.FirstOrDefault(a => a.IsActive == true && a.CompanyId == CompanyId_ && a.SettingTypeId == (int)Constant.SettingsTypeId.LeaveDeductionHierarchy && a.Value == Casual && a.IsContractual == false);
         if (LeaveDeductionHierarchyForNonContractualEmployees_1_Casual == null)
         {
             Setup_Settings objSetup_Settings = new Setup_Settings();
             objSetup_Settings.CompanyId = CompanyId_;
             objSetup_Settings.SettingTypeId = (int)Constant.SettingsTypeId.LeaveDeductionHierarchy;
             objSetup_Settings.Value = Casual;
             objSetup_Settings.Limit = 3;
             objSetup_Settings.SortOrder = 1;
             objSetup_Settings.IsActive = true;
             objSetup_Settings.CreatedBy = UserId;
             objSetup_Settings.CreatedDate = dt;
             objSetup_Settings.UserIP = UserIP;
             context.Setup_Settings.Add(objSetup_Settings);
             context.SaveChanges();
             context.INSERT_INTO_AuditLog(Convert.ToString(objSetup_Settings.SettingId), "Setup_Settings", (int)Constant.OperationType.INSERT, UserId);
         }

         #endregion
         #region LeaveDeductionHierarchyForNonContractualEmployees_2_Sick
         var LeaveDeductionHierarchyForNonContractualEmployees_2_Sick = context.Setup_Settings.FirstOrDefault(a => a.IsActive == true && a.CompanyId == CompanyId_ && a.SettingTypeId == (int)Constant.SettingsTypeId.LeaveDeductionHierarchy && a.Value == Sick && a.IsContractual == false);
         if (LeaveDeductionHierarchyForNonContractualEmployees_2_Sick == null)
         {
             Setup_Settings objSetup_Settings = new Setup_Settings();
             objSetup_Settings.CompanyId = CompanyId_;
             objSetup_Settings.SettingTypeId = (int)Constant.SettingsTypeId.LeaveDeductionHierarchy;
             objSetup_Settings.Value = Sick;
             objSetup_Settings.Limit = 3;
             objSetup_Settings.SortOrder = 2;
             objSetup_Settings.IsActive = true;
             objSetup_Settings.CreatedBy = UserId;
             objSetup_Settings.CreatedDate = dt;
             objSetup_Settings.UserIP = UserIP;
             context.Setup_Settings.Add(objSetup_Settings);
             context.SaveChanges();
             context.INSERT_INTO_AuditLog(Convert.ToString(objSetup_Settings.SettingId), "Setup_Settings", (int)Constant.OperationType.INSERT, UserId);
         }

         #endregion
         #region LeaveDeductionHierarchyForNonContractualEmployees_3_Annual
         var LeaveDeductionHierarchyForNonContractualEmployees_3_Annual = context.Setup_Settings.FirstOrDefault(a => a.IsActive == true && a.CompanyId == CompanyId_ && a.SettingTypeId == (int)Constant.SettingsTypeId.LeaveDeductionHierarchy && a.Value == Annual && a.IsContractual == false);
         if (LeaveDeductionHierarchyForNonContractualEmployees_3_Annual == null)
         {
             Setup_Settings objSetup_Settings = new Setup_Settings();
             objSetup_Settings.CompanyId = CompanyId_;
             objSetup_Settings.SettingTypeId = (int)Constant.SettingsTypeId.LeaveDeductionHierarchy;
             objSetup_Settings.Value = Annual;
             objSetup_Settings.Limit = 1000;
             objSetup_Settings.SortOrder = 3;
             objSetup_Settings.IsActive = true;
             objSetup_Settings.CreatedBy = UserId;
             objSetup_Settings.CreatedDate = dt;
             objSetup_Settings.UserIP = UserIP;
             context.Setup_Settings.Add(objSetup_Settings);
             context.SaveChanges();
             context.INSERT_INTO_AuditLog(Convert.ToString(objSetup_Settings.SettingId), "Setup_Settings", (int)Constant.OperationType.INSERT, UserId);
         }

         #endregion
         #region LeaveDeductionHierarchyForContractualEmployees_1_Casual
         var LeaveDeductionHierarchyForContractualEmployees_1_Casual = context.Setup_Settings.FirstOrDefault(a => a.IsActive == true && a.CompanyId == CompanyId_ && a.SettingTypeId == (int)Constant.SettingsTypeId.LeaveDeductionHierarchy && a.Value == Casual && a.IsContractual == true);
         if (LeaveDeductionHierarchyForContractualEmployees_1_Casual == null)
         {
             Setup_Settings objSetup_Settings = new Setup_Settings();
             objSetup_Settings.CompanyId = CompanyId_;
             objSetup_Settings.SettingTypeId = (int)Constant.SettingsTypeId.LeaveDeductionHierarchy;
             objSetup_Settings.Value = Casual;
             objSetup_Settings.Limit = 1000;
             objSetup_Settings.IsContractual = true;
             objSetup_Settings.SortOrder = 1;
             objSetup_Settings.IsActive = true;
             objSetup_Settings.CreatedBy = UserId;
             objSetup_Settings.CreatedDate = dt;
             objSetup_Settings.UserIP = UserIP;
             context.Setup_Settings.Add(objSetup_Settings);
             context.SaveChanges();
             context.INSERT_INTO_AuditLog(Convert.ToString(objSetup_Settings.SettingId), "Setup_Settings", (int)Constant.OperationType.INSERT, UserId);
         }

         #endregion
         #region LeaveDeductionHierarchyForContractualEmployees_2_Sick
         var LeaveDeductionHierarchyForContractualEmployees_2_Sick = context.Setup_Settings.FirstOrDefault(a => a.IsActive == true && a.CompanyId == CompanyId_ && a.SettingTypeId == (int)Constant.SettingsTypeId.LeaveDeductionHierarchy && a.Value == Sick && a.IsContractual == true);
         if (LeaveDeductionHierarchyForContractualEmployees_2_Sick == null)
         {
             Setup_Settings objSetup_Settings = new Setup_Settings();
             objSetup_Settings.CompanyId = CompanyId_;
             objSetup_Settings.SettingTypeId = (int)Constant.SettingsTypeId.LeaveDeductionHierarchy;
             objSetup_Settings.Value = Sick;
             objSetup_Settings.Limit = 1000;
             objSetup_Settings.SortOrder = 2;
             objSetup_Settings.IsContractual = true;
             objSetup_Settings.CreatedBy = UserId;
             objSetup_Settings.CreatedDate = dt;
             objSetup_Settings.IsActive = true;
             objSetup_Settings.UserIP = UserIP;
             context.Setup_Settings.Add(objSetup_Settings);
             context.SaveChanges();
             context.INSERT_INTO_AuditLog(Convert.ToString(objSetup_Settings.SettingId), "Setup_Settings", (int)Constant.OperationType.INSERT, UserId);
         }

         #endregion
         #region LeaveDeductionHierarchyForContractualEmployees_3_Annual
         var LeaveDeductionHierarchyForContractualEmployees_3_Annual = context.Setup_Settings.FirstOrDefault(a => a.IsActive == true && a.CompanyId == CompanyId_ && a.SettingTypeId == (int)Constant.SettingsTypeId.LeaveDeductionHierarchy && a.Value == Annual && a.IsContractual == true);
         if (LeaveDeductionHierarchyForContractualEmployees_3_Annual == null)
         {
             Setup_Settings objSetup_Settings = new Setup_Settings();
             objSetup_Settings.CompanyId = CompanyId_;
             objSetup_Settings.SettingTypeId = (int)Constant.SettingsTypeId.LeaveDeductionHierarchy;
             objSetup_Settings.Value = Annual;
             objSetup_Settings.Limit = 0;
             objSetup_Settings.SortOrder = 3;
             objSetup_Settings.IsContractual = true;
             objSetup_Settings.IsActive = true;
             objSetup_Settings.CreatedBy = UserId;
             objSetup_Settings.CreatedDate = dt;
             objSetup_Settings.UserIP = UserIP;
             context.Setup_Settings.Add(objSetup_Settings);
             context.SaveChanges();
             context.INSERT_INTO_AuditLog(Convert.ToString(objSetup_Settings.SettingId), "Setup_Settings", (int)Constant.OperationType.INSERT, UserId);
         }

         #endregion



         #region FlexyDeductionHierarchyForNonContractualEmployees_1_Annual
         var FlexyDeductionHierarchyForNonContractualEmployees_1_Annual = context.Setup_Settings.FirstOrDefault(a => a.IsActive == true && a.CompanyId == CompanyId_ && a.SettingTypeId == (int)Constant.SettingsTypeId.FlexiDeductionHierarchy && a.Value == Annual && a.IsContractual == false);
         if (FlexyDeductionHierarchyForNonContractualEmployees_1_Annual == null)
         {
             Setup_Settings objSetup_Settings = new Setup_Settings();
             objSetup_Settings.CompanyId = CompanyId_;
             objSetup_Settings.SettingTypeId = (int)Constant.SettingsTypeId.FlexiDeductionHierarchy;
             objSetup_Settings.Value = Annual;
             objSetup_Settings.Limit = 1000;
             objSetup_Settings.SortOrder = 1;
             objSetup_Settings.IsActive = true;
             objSetup_Settings.CreatedBy = UserId;
             objSetup_Settings.CreatedDate = dt;
             objSetup_Settings.UserIP = UserIP;
             context.Setup_Settings.Add(objSetup_Settings);
             context.SaveChanges();
             context.INSERT_INTO_AuditLog(Convert.ToString(objSetup_Settings.SettingId), "Setup_Settings", (int)Constant.OperationType.INSERT, UserId);
         }

         #endregion
         #region FlexyDeductionHierarchyForNonContractualEmployees_2_Sick
         var FlexyDeductionHierarchyForNonContractualEmployees_2_Sick = context.Setup_Settings.FirstOrDefault(a => a.IsActive == true && a.CompanyId == CompanyId_ && a.SettingTypeId == (int)Constant.SettingsTypeId.FlexiDeductionHierarchy && a.Value == Sick && a.IsContractual == false);
         if (FlexyDeductionHierarchyForNonContractualEmployees_2_Sick == null)
         {
             Setup_Settings objSetup_Settings = new Setup_Settings();
             objSetup_Settings.CompanyId = CompanyId_;
             objSetup_Settings.SettingTypeId = (int)Constant.SettingsTypeId.FlexiDeductionHierarchy;
             objSetup_Settings.Value = Sick;
             objSetup_Settings.Limit = 0;
             objSetup_Settings.SortOrder = 2;
             objSetup_Settings.IsActive = true;
             objSetup_Settings.CreatedBy = UserId;
             objSetup_Settings.CreatedDate = dt;
             objSetup_Settings.UserIP = UserIP;
             context.Setup_Settings.Add(objSetup_Settings);
             context.SaveChanges();
             context.INSERT_INTO_AuditLog(Convert.ToString(objSetup_Settings.SettingId), "Setup_Settings", (int)Constant.OperationType.INSERT, UserId);
         }
         #endregion
         #region FlexyDeductionHierarchyForNonContractualEmployees_3_Casual
         var FlexyDeductionHierarchyForNonContractualEmployees_3_Casual = context.Setup_Settings.FirstOrDefault(a => a.IsActive == true && a.CompanyId == CompanyId_ && a.SettingTypeId == (int)Constant.SettingsTypeId.FlexiDeductionHierarchy && a.Value == Casual && a.IsContractual == false);
         if (FlexyDeductionHierarchyForNonContractualEmployees_3_Casual == null)
         {
             Setup_Settings objSetup_Settings = new Setup_Settings();
             objSetup_Settings.CompanyId = CompanyId_;
             objSetup_Settings.SettingTypeId = (int)Constant.SettingsTypeId.FlexiDeductionHierarchy;
             objSetup_Settings.Value = Casual;
             objSetup_Settings.Limit = 0;
             objSetup_Settings.SortOrder = 3;
             objSetup_Settings.IsActive = true;
             objSetup_Settings.CreatedBy = UserId;
             objSetup_Settings.CreatedDate = dt;
             objSetup_Settings.UserIP = UserIP;
             context.Setup_Settings.Add(objSetup_Settings);
             context.SaveChanges();
             context.INSERT_INTO_AuditLog(Convert.ToString(objSetup_Settings.SettingId), "Setup_Settings", (int)Constant.OperationType.INSERT, UserId);
         }

         #endregion
         #region FlexyDeductionHierarchyForContractualEmployees_1_Casual
         var FlexyDeductionHierarchyForContractualEmployees_1_Casual = context.Setup_Settings.FirstOrDefault(a => a.IsActive == true && a.CompanyId == CompanyId_ && a.SettingTypeId == (int)Constant.SettingsTypeId.FlexiDeductionHierarchy && a.Value == Casual && a.IsContractual == true);
         if (FlexyDeductionHierarchyForContractualEmployees_1_Casual == null)
         {
             Setup_Settings objSetup_Settings = new Setup_Settings();
             objSetup_Settings.CompanyId = CompanyId_;
             objSetup_Settings.SettingTypeId = (int)Constant.SettingsTypeId.FlexiDeductionHierarchy;
             objSetup_Settings.Value = Casual;
             objSetup_Settings.Limit = 1000;
             objSetup_Settings.SortOrder = 1;
             objSetup_Settings.IsContractual = true;
             objSetup_Settings.IsActive = true;
             objSetup_Settings.CreatedBy = UserId;
             objSetup_Settings.CreatedDate = dt;
             objSetup_Settings.UserIP = UserIP;
             context.Setup_Settings.Add(objSetup_Settings);
             context.SaveChanges();
             context.INSERT_INTO_AuditLog(Convert.ToString(objSetup_Settings.SettingId), "Setup_Settings", (int)Constant.OperationType.INSERT, UserId);
         }

         #endregion
         #region FlexyDeductionHierarchyForContractualEmployees_2_Sick
         var FlexyDeductionHierarchyForContractualEmployees_2_Sick = context.Setup_Settings.FirstOrDefault(a => a.IsActive == true && a.CompanyId == CompanyId_ && a.SettingTypeId == (int)Constant.SettingsTypeId.FlexiDeductionHierarchy && a.Value == Sick && a.IsContractual == true);
         if (FlexyDeductionHierarchyForContractualEmployees_2_Sick == null)
         {
             Setup_Settings objSetup_Settings = new Setup_Settings();
             objSetup_Settings.CompanyId = CompanyId_;
             objSetup_Settings.SettingTypeId = (int)Constant.SettingsTypeId.FlexiDeductionHierarchy;
             objSetup_Settings.Value = Sick;
             objSetup_Settings.Limit = 1000;
             objSetup_Settings.SortOrder = 2;
             objSetup_Settings.IsContractual = true;
             objSetup_Settings.IsActive = true;
             objSetup_Settings.CreatedBy = UserId;
             objSetup_Settings.CreatedDate = dt;
             objSetup_Settings.UserIP = UserIP;
             context.Setup_Settings.Add(objSetup_Settings);
             context.SaveChanges();
             context.INSERT_INTO_AuditLog(Convert.ToString(objSetup_Settings.SettingId), "Setup_Settings", (int)Constant.OperationType.INSERT, UserId);
         }
         #endregion
         #region FlexyDeductionHierarchyForContractualEmployees_3_Annual
         var FlexyDeductionHierarchyForContractualEmployees_3_Annual = context.Setup_Settings.FirstOrDefault(a => a.IsActive == true && a.CompanyId == CompanyId_ && a.SettingTypeId == (int)Constant.SettingsTypeId.FlexiDeductionHierarchy && a.Value == Annual && a.IsContractual == true);
         if (FlexyDeductionHierarchyForContractualEmployees_3_Annual == null)
         {
             Setup_Settings objSetup_Settings = new Setup_Settings();
             objSetup_Settings.CompanyId = CompanyId_;
             objSetup_Settings.SettingTypeId = (int)Constant.SettingsTypeId.FlexiDeductionHierarchy;
             objSetup_Settings.Value = Annual;
             objSetup_Settings.Limit = 0;
             objSetup_Settings.SortOrder = 3;
             objSetup_Settings.IsContractual = true;
             objSetup_Settings.IsActive = true;
             objSetup_Settings.CreatedBy = UserId;
             objSetup_Settings.CreatedDate = dt;
             objSetup_Settings.UserIP = UserIP;
             context.Setup_Settings.Add(objSetup_Settings);
             context.SaveChanges();
             context.INSERT_INTO_AuditLog(Convert.ToString(objSetup_Settings.SettingId), "Setup_Settings", (int)Constant.OperationType.INSERT, UserId);
         }

         #endregion

         #region HalfDayDeductionHierarchyForNonContractualEmployees_1_Casual
         var HalfDayDeductionHierarchyForNonContractualEmployees_1_Casual = context.Setup_Settings.FirstOrDefault(a => a.IsActive == true && a.CompanyId == CompanyId_ && a.SettingTypeId == (int)Constant.SettingsTypeId.HalfdayLeaveDeductionHierarchy && a.Value == Casual && a.IsContractual == false);
         if (HalfDayDeductionHierarchyForNonContractualEmployees_1_Casual == null)
         {
             Setup_Settings objSetup_Settings = new Setup_Settings();
             objSetup_Settings.CompanyId = CompanyId_;
             objSetup_Settings.SettingTypeId = (int)Constant.SettingsTypeId.HalfdayLeaveDeductionHierarchy;
             objSetup_Settings.Value = Casual;
             objSetup_Settings.Limit = 1000;
             objSetup_Settings.SortOrder = 1;
             objSetup_Settings.CreatedBy = UserId;
             objSetup_Settings.CreatedDate = dt;
             objSetup_Settings.IsActive = true;
             objSetup_Settings.UserIP = UserIP;
             context.Setup_Settings.Add(objSetup_Settings);
             context.SaveChanges();
             context.INSERT_INTO_AuditLog(Convert.ToString(objSetup_Settings.SettingId), "Setup_Settings", (int)Constant.OperationType.INSERT, UserId);
         }

         #endregion
         #region HalfDayDeductionHierarchyForNonContractualEmployees_2_Sick
         var HalfDayDeductionHierarchyForNonContractualEmployees_2_Sick = context.Setup_Settings.FirstOrDefault(a => a.IsActive == true && a.CompanyId == CompanyId_ && a.SettingTypeId == (int)Constant.SettingsTypeId.HalfdayLeaveDeductionHierarchy && a.Value == Sick && a.IsContractual == false);
         if (HalfDayDeductionHierarchyForNonContractualEmployees_2_Sick == null)
         {
             Setup_Settings objSetup_Settings = new Setup_Settings();
             objSetup_Settings.CompanyId = CompanyId_;
             objSetup_Settings.SettingTypeId = (int)Constant.SettingsTypeId.HalfdayLeaveDeductionHierarchy;
             objSetup_Settings.Value = Sick;
             objSetup_Settings.SortOrder = 2;
             objSetup_Settings.Limit = 1000;
             objSetup_Settings.CreatedBy = UserId;
             objSetup_Settings.CreatedDate = dt;
             objSetup_Settings.IsActive = true;
             objSetup_Settings.UserIP = UserIP;
             context.Setup_Settings.Add(objSetup_Settings);
             context.SaveChanges();
             context.INSERT_INTO_AuditLog(Convert.ToString(objSetup_Settings.SettingId), "Setup_Settings", (int)Constant.OperationType.INSERT, UserId);
         }

         #endregion
         #region HalfDayDeductionHierarchyForNonContractualEmployees_3_Annual
         var HalfDayDeductionHierarchyForNonContractualEmployees_3_Annual = context.Setup_Settings.FirstOrDefault(a => a.IsActive == true && a.CompanyId == CompanyId_ && a.SettingTypeId == (int)Constant.SettingsTypeId.HalfdayLeaveDeductionHierarchy && a.Value == Annual && a.IsContractual == false);
         if (HalfDayDeductionHierarchyForNonContractualEmployees_3_Annual == null)
         {
             Setup_Settings objSetup_Settings = new Setup_Settings();
             objSetup_Settings.CompanyId = CompanyId_;
             objSetup_Settings.SettingTypeId = (int)Constant.SettingsTypeId.HalfdayLeaveDeductionHierarchy;
             objSetup_Settings.Value = Annual;
             objSetup_Settings.SortOrder = 3;
             objSetup_Settings.Limit = 1000;
             objSetup_Settings.CreatedBy = UserId;
             objSetup_Settings.CreatedDate = dt;
             objSetup_Settings.IsActive = true;
             objSetup_Settings.UserIP = UserIP;
             context.Setup_Settings.Add(objSetup_Settings);
             context.SaveChanges();
             context.INSERT_INTO_AuditLog(Convert.ToString(objSetup_Settings.SettingId), "Setup_Settings", (int)Constant.OperationType.INSERT, UserId);
         }

         #endregion

         #region HalfdayDeductionHierarchyForContractualEmployees_1_Casual
         var HalfdayDeductionHierarchyForContractualEmployees_1_Casual = context.Setup_Settings.FirstOrDefault(a => a.IsActive == true && a.CompanyId == CompanyId_ && a.SettingTypeId == (int)Constant.SettingsTypeId.HalfdayLeaveDeductionHierarchy && a.Value == Casual && a.IsContractual == true);
         if (HalfdayDeductionHierarchyForContractualEmployees_1_Casual == null)
         {
             Setup_Settings objSetup_Settings = new Setup_Settings();
             objSetup_Settings.CompanyId = CompanyId_;
             objSetup_Settings.SettingTypeId = (int)Constant.SettingsTypeId.HalfdayLeaveDeductionHierarchy;
             objSetup_Settings.Value = Casual;
             objSetup_Settings.IsContractual = true;
             objSetup_Settings.Limit = 1000;
             objSetup_Settings.SortOrder = 1;
             objSetup_Settings.CreatedBy = UserId;
             objSetup_Settings.CreatedDate = dt;
             objSetup_Settings.IsActive = true;
             objSetup_Settings.UserIP = UserIP;
             context.Setup_Settings.Add(objSetup_Settings);
             context.SaveChanges();
             context.INSERT_INTO_AuditLog(Convert.ToString(objSetup_Settings.SettingId), "Setup_Settings", (int)Constant.OperationType.INSERT, UserId);
         }

         #endregion
         #region HalfdayDeductionHierarchyForContractualEmployees_2_Sick
         var HalfdayDeductionHierarchyForContractualEmployees_2_Sick = context.Setup_Settings.FirstOrDefault(a => a.IsActive == true && a.CompanyId == CompanyId_ && a.SettingTypeId == (int)Constant.SettingsTypeId.HalfdayLeaveDeductionHierarchy && a.Value == Sick && a.IsContractual == true);
         if (HalfdayDeductionHierarchyForContractualEmployees_2_Sick == null)
         {
             Setup_Settings objSetup_Settings = new Setup_Settings();
             objSetup_Settings.CompanyId = CompanyId_;
             objSetup_Settings.SettingTypeId = (int)Constant.SettingsTypeId.HalfdayLeaveDeductionHierarchy;
             objSetup_Settings.Value = Sick;
             objSetup_Settings.SortOrder = 2;
             objSetup_Settings.Limit = 1000;
             objSetup_Settings.IsContractual = true;
             objSetup_Settings.CreatedBy = UserId;
             objSetup_Settings.CreatedDate = dt;
             objSetup_Settings.IsActive = true;
             objSetup_Settings.UserIP = UserIP;
             context.Setup_Settings.Add(objSetup_Settings);
             context.SaveChanges();
             context.INSERT_INTO_AuditLog(Convert.ToString(objSetup_Settings.SettingId), "Setup_Settings", (int)Constant.OperationType.INSERT, UserId);
         }

         #endregion
         #region HalfdayDeductionHierarchyForContractualEmployees_3_WithoutPay
         var HalfdayDeductionHierarchyForContractualEmployees_3_WithoutPay = context.Setup_Settings.FirstOrDefault(a => a.IsActive == true && a.CompanyId == CompanyId_ && a.SettingTypeId == (int)Constant.SettingsTypeId.HalfdayLeaveDeductionHierarchy && a.Value == WithoutPay && a.IsContractual == true);
         if (HalfdayDeductionHierarchyForContractualEmployees_3_WithoutPay == null)
         {
             Setup_Settings objSetup_Settings = new Setup_Settings();
             objSetup_Settings.CompanyId = CompanyId_;
             objSetup_Settings.SettingTypeId = (int)Constant.SettingsTypeId.HalfdayLeaveDeductionHierarchy;
             objSetup_Settings.Value = WithoutPay;
             objSetup_Settings.SortOrder = 3;
             objSetup_Settings.Limit = 1000;
             objSetup_Settings.IsContractual = true;
             objSetup_Settings.CreatedBy = UserId;
             objSetup_Settings.CreatedDate = dt;
             objSetup_Settings.IsActive = true;
             objSetup_Settings.UserIP = UserIP;
             context.Setup_Settings.Add(objSetup_Settings);
             context.SaveChanges();
             context.INSERT_INTO_AuditLog(Convert.ToString(objSetup_Settings.SettingId), "Setup_Settings", (int)Constant.OperationType.INSERT, UserId);
         }

         #endregion
         */
    }

}