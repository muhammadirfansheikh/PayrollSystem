using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Data;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Transactions;
using NCalc;

public partial class Pages_HCM_Setup_SetupFormula : Base
{
    Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();
    DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CreatedDataTable();
            BindDropDown();
            BindRepeater();
        }
    }

    private void CreatedDataTable()
    {
        dt = new DataTable();
        dt.Columns.Clear();

        dt.Columns.Add("ID");
        dt.Columns.Add("FormulaElementDetailId");
        dt.Columns.Add("TypeId");
        dt.Columns.Add("Type");
        dt.Columns.Add("Title");
        //dt.Columns.Add("ParentId");

        ViewState["dt"] = dt;
    }

    private void BindAllowanceDropdown()
    {
        int CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        int ElementId = Convert.ToInt32(ddlType.SelectedValue);
        bool IsDeduction = false;

        if (ElementId == (int)Constant.FormulaElement.Deduction)
        {
            IsDeduction = true;
        }

        var lst = context.HCM_Setup_Allowance.Where(a => a.IsActive == true && a.IsDeduction == IsDeduction)

             .Select(a => new
             {
                 Id = a.AllowanceID,
                 Value = a.AllowanceName,

             })
             .ToList();

        CommonHelper.BindDropDown(ddlAllowanceDeduction, lst, "Value", "Id", true, false);
    }

    private void BindOperator()
    {
        var lst = context.HCM_Setup_Detail.Where(a => a.IsActive == true && a.ParentId == (int)Constant.FormulaElement.Operator)
             .Select(a => new
             {
                 Id = a.SetupDetailID,
                 Value = a.ColumnValue
             })
            .ToList();

        CommonHelper.BindDropDown(ddlAllowanceDeduction, lst, "Value", "Id", true, false);
    }

    private void BindSalary()
    {
        var lst = context.HCM_Setup_Detail.Where(a => a.IsActive == true && a.ParentId == (int)Constant.FormulaElement.Salary)
             .Select(a => new
             {
                 Id = a.SetupDetailID,
                 Value = a.ColumnValue
             })
            .ToList();

        CommonHelper.BindDropDown(ddlAllowanceDeduction, lst, "Value", "Id", true, false);
    }

    //private void BindFormulaCategory()
    //{
    //    var lst = context.HCM_Setup_Detail.Where(a => a.IsActive == true && a.HCM_Setup_Master.SetupMasterID == (int)Constant .HCMSetupMaster.FormulaCategory)
    //            .Select(a => new
    //            {
    //                Id = a.SetupDetailID,
    //                Value = a.ColumnValue
    //            })
    //            .ToList();
    //    CommonHelper.BindDropDown(ddlCategory, lst, "Value", "Id", true, false);

    //    if (lst != null && lst.Count > 0)
    //    {
    //        ddlCategory.SelectedValue = lst[0].Id.ToString();
    //    }
    //}

    private void BindFormulaElement()
    {
        int? IntNull = null;
        var lst = context.HCM_Setup_Detail.Where(a => a.IsActive == true && a.SetupMasterID == (int)Constant.HCMSetupMaster.FormulaElement)
            .Where(b => b.ParentId == null)
            .Select(a => new
            {
                Id = a.SetupDetailID,
                Value = a.ColumnValue
            })
            .ToList();

        CommonHelper.BindDropDown(ddlType, lst, "Value", "Id", true, false);
    }

    private void BindDropDown()
    {
        var lst = context.Setup_Company.Where(a => a.IsActive == true)
            .Select(a => new
            {
                Id = a.CompanyId,
                Value = a.CompanyName
            })
            .ToList();
        CommonHelper.BindDropDown(ddlCompany, lst, "Value", "Id", true, false);
        CommonHelper.BindDropDown(ddlCompanySearch, lst, "Value", "Id", true, false);

        BindFormulaTypeSearch(Convert.ToInt32(ddlCompanySearch.SelectedValue));
        //BindFormulaType(Convert.ToInt32(ddlCompany.SelectedValue));

        BindFormulaElement();
        //BindFormulaCategory();

        ddlCategory_SelectedIndexChanged(null, null);
    }

    private void BindFormulaType(int CompanyId)
    {
        //int? CategoryId = Convert.ToInt32(ddlCategory.SelectedValue);
        ////int? CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);

        //if (CategoryId == (int)Constant.FormulaCategory.Allowance)
        //{
        //    var lst = context.HCM_Setup_Allowance.Where(a => a.IsActive == true && a.CompanyId == CompanyId)
        //           .Select(a => new
        //           {
        //               Id = a.AllowanceID,
        //               Value = a.AllowanceName
        //           })
        //           .ToList();

        //    CommonHelper.BindDropDown(ddlFormulaType, lst, "Value", "Id", true, false);
        //}
        //else if (CategoryId == (int)Constant.FormulaCategory.Salary)
        //{
        //    var lst = context.HCM_Setup_Detail.Where(a => a.IsActive == true && a.ParentId == (int)Constant.FormulaElement.Salary)
        //     .Select(a => new
        //     {
        //         Id = a.SetupDetailID,
        //         Value = a.ColumnValue
        //     })
        //    .ToList();

        //    CommonHelper.BindDropDown(ddlFormulaType, lst, "Value", "Id", true, false);
        //}

    }

    private void BindFormulaTypeSearch(int CompanyId)
    {
        //var lst = context.HCM_Setup_FomulaType.Where(a => a.IsActive == true && a.CompanyID == CompanyId)
        //        .Select(a => new
        //        {
        //            Id = a.FormulaTypeID,
        //            Value = a.FomulaName
        //        })
        //        .ToList();

        //var lst = context.HCM_Setup_Allowance.Where(a => a.IsActive == true && a.CompanyId == CompanyId)
        //      .Select(a => new
        //      {
        //          Id = a.AllowanceID,
        //          Value = a.AllowanceName
        //      })
        //      .ToList();
        //CommonHelper.BindDropDown(ddlFormulaTypeSearch, lst, "Value", "Id", true, false);
    }

    private bool CheckDuplicateElement(int ElementId, DataTable dt)
    {
        //DataTable dt = (DataTable)ViewState["dt"];
        bool isExist = false;

        if (dt.Rows.Count == 0)
        {
            isExist = true;
        }
        else
        {
            if (Convert.ToInt32(Convert.ToString(dt.Rows[dt.Rows.Count - 1]["TypeId"])) == (int)Constant.FormulaElement.Operator)
            {
                if (ElementId != (int)Constant.FormulaElement.Operator)
                {
                    isExist = true;
                }
                else
                {
                    isExist = false;
                }
            }
            else
            {
                if (ElementId == (int)Constant.FormulaElement.Operator)
                {
                    isExist = true;
                }
                else
                {
                    isExist = false;
                }
            }
        }

        return isExist;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = (DataTable)ViewState["dt"];

            int TypeId = Convert.ToInt32(ddlType.SelectedValue);
            double ConstantNumber = 0;
            int ConstantId = 0;
            int FormulaElementDetailId = 0;
            string Allowance = "";
            int ElementId = Convert.ToInt32(ddlType.SelectedValue);
            string Element = "";
            int OperatorId = 0;
            string Operator = "";
            int Count = 0;

            //if (CheckDuplicateElement(ElementId, dt))
            {
                if (dt.Rows.Count == 0)
                {
                    Count = 1;
                }
                else
                {
                    Count = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["Id"]);
                    Count = Count + 1;
                }

                if (ElementId == (int)Constant.FormulaElement.Allowance)
                {
                    FormulaElementDetailId = Convert.ToInt32(ddlAllowanceDeduction.SelectedValue);
                    Allowance = ddlAllowanceDeduction.SelectedItem.Text;
                    Element = "Allowance";

                    dt.Rows.Add(Count, FormulaElementDetailId, ElementId, Element, Allowance);
                }
                else if (ElementId == (int)Constant.FormulaElement.Deduction)
                {
                    FormulaElementDetailId = Convert.ToInt32(ddlAllowanceDeduction.SelectedValue);
                    Allowance = ddlAllowanceDeduction.SelectedItem.Text;
                    Element = "Deduction";

                    dt.Rows.Add(Count, FormulaElementDetailId, ElementId, Element, Allowance);
                }
                else if (ElementId == (int)Constant.FormulaElement.Operator)
                {
                    OperatorId = Convert.ToInt32(ddlAllowanceDeduction.SelectedValue);
                    Operator = ddlAllowanceDeduction.SelectedItem.Text;
                    Element = "Operator";

                    dt.Rows.Add(Count, OperatorId, ElementId, Element, Operator);
                }
                else if (ElementId == (int)Constant.FormulaElement.Constant)
                {
                    ConstantNumber = Convert.ToDouble(txtOperatorConstant.Text);
                    Element = "Constant";

                    dt.Rows.Add(Count, ConstantId, ElementId, Element, ConstantNumber);
                }
                else if (ElementId == (int)Constant.FormulaElement.Salary)
                {
                    FormulaElementDetailId = Convert.ToInt32(ddlAllowanceDeduction.SelectedValue);
                    Allowance = ddlAllowanceDeduction.SelectedItem.Text;
                    Element = "Salary";

                    dt.Rows.Add(Count, FormulaElementDetailId, ElementId, Element, Allowance);
                }

                txtOperatorConstant.Text = string.Empty;
                ddlAllowanceDeduction.SelectedIndex = 0;
                ddlType.SelectedIndex = 0;


                ViewState["dt"] = dt;

                BindPopupRepeater();
            }
            //else
            //{ 

            //}


        }
        catch (Exception ex)
        {
            divError.Visible = true;
            lblError.InnerText = ex.Message;
        }
    }

    private void BindPopupRepeater()
    {
        DataTable dt = (DataTable)ViewState["dt"];

        rptAdd.DataSource = dt;
        rptAdd.DataBind();

        string FormulaStr = string.Empty;

        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                FormulaStr = FormulaStr + Convert.ToString(dt.Rows[i]["Title"]) + " ";
            }
        }
        lblFormula.Text = FormulaStr;
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindFormulaType(Convert.ToInt32(ddlCompany.SelectedValue));
    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int ElementId = Convert.ToInt32(ddlType.SelectedValue);

            if (ElementId == (int)Constant.FormulaElement.Allowance || ElementId == (int)Constant.FormulaElement.Deduction)
            {
                dvlblOperatorConstant.Visible = false;
                dvOperatorConstant.Visible = false;
                dvAllowanceDeduction.Visible = true;
                dvlblAllowanceDeduction.Visible = true;

                BindAllowanceDropdown();
            }
            else if (ElementId == (int)Constant.FormulaElement.Operator)
            {
                dvlblOperatorConstant.Visible = false;
                dvOperatorConstant.Visible = false;
                dvAllowanceDeduction.Visible = true;
                dvlblAllowanceDeduction.Visible = true;

                BindOperator();
            }
            else if (ElementId == (int)Constant.FormulaElement.Constant)
            {
                dvlblAllowanceDeduction.Visible = false;
                dvAllowanceDeduction.Visible = false;
                dvOperatorConstant.Visible = true;
                dvlblOperatorConstant.Visible = true;
            }
            else if (ElementId == (int)Constant.FormulaElement.Salary)
            {
                dvlblOperatorConstant.Visible = false;
                dvOperatorConstant.Visible = false;
                dvAllowanceDeduction.Visible = true;
                dvlblAllowanceDeduction.Visible = true;

                BindSalary();
            }
        }
        catch (Exception ex)
        {
            divError.Visible = true;
            lblError.InnerText = ex.Message;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        { 
            int? IntNull = null;
            bool checkIsExist = CheckAlreadyAllowanceExists(txtAllowanceDeduction.Text.Trim(), Convert.ToInt32(ddlCompany.SelectedValue));

            if (!checkIsExist)
            {
                if (rptAdd.Items.Count > 0)
                {
                    string Formula = "", Allowances = "", Deductions = "";
                    for (int i = 0; i < rptAdd.Items.Count; i++)
                    {
                        HtmlInputHidden hfId = (HtmlInputHidden)rptAdd.Items[i].FindControl("hfId");
                        HtmlInputHidden hfElementId = (HtmlInputHidden)rptAdd.Items[i].FindControl("hfElementId");
                        HtmlInputHidden hfFormulaElementDetailId = (HtmlInputHidden)rptAdd.Items[i].FindControl("hfFormulaElementDetailId");

                        Label lblType = (Label)rptAdd.Items[i].FindControl("lblType");
                        Label lblTitle = (Label)rptAdd.Items[i].FindControl("lblTitle");
                        string Title = "";

                        if (Convert.ToInt32(hfElementId.Value) == (int)Constant.FormulaElement.Allowance)
                        {
                            Title = "[" + lblTitle.Text + "]";
                            Allowances = Allowances + hfFormulaElementDetailId.Value + ",";
                        }
                        else if (Convert.ToInt32(hfElementId.Value) == (int)Constant.FormulaElement.Deduction)
                        {
                            Title = "[" + lblTitle.Text + "]";
                            Deductions = Deductions + hfFormulaElementDetailId.Value + ",";
                        }
                        else if (Convert.ToInt32(hfElementId.Value) == (int)Constant.FormulaElement.Salary)
                        {
                            Title = "[" + lblTitle.Text + "]";
                        }
                        else
                        {
                            Title = lblTitle.Text;
                        }

                        Formula = Formula + Title;
                    }

                    Expression v = new Expression(Formula);
                    if (v.HasErrors())
                    {
                        Error("Formula is Incorrect.");

                        return;
                    }

                    DateTime dt = DateTime.Now;

                    HCM_Setup_Allowance objAll = new HCM_Setup_Allowance();
                    objAll.AllowanceName = txtAllowanceDeduction.Text.Trim();
                    objAll.IsDeduction = chkIsDeduction.Checked;
                    objAll.CreatedBy = UserKey;
                    objAll.CreatedDate = dt;
                    objAll.IsActive = true;
                    objAll.UserIP = UserIP;
                    objAll.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);

                    context.HCM_Setup_Allowance.Add(objAll);
                    context.SaveChanges();

                    if (chkbxFormula.Checked)
                    {
                        if (objAll.AllowanceID > 0)
                        {
                            HCM_CompanyFormula obj = new HCM_CompanyFormula();

                            obj.AllowanceID = objAll.AllowanceID;
                            //obj.SetupDetailID = 0;
                            obj.CompanyID = Convert.ToInt32(ddlCompany.SelectedValue);
                            obj.Formula = Formula;
                            obj.Deductions = Deductions;
                            obj.Allowances = Allowances;
                            obj.CreatedBy = UserKey;
                            obj.CreatedDate = dt;
                            obj.IsActive = true;
                            obj.UserIP = UserIP;

                            context.HCM_CompanyFormula.Add(obj);
                            context.SaveChanges();

                            Success("Formula has been added successfully.");

                            ResetControls();

                        }
                    }
                }
            }
            else
            {
                Error("Formula Type Already Exist.");
                //ClosePopup();
            }

            BindRepeater();


        }
        catch (Exception ex)
        {
            lbl.Visible = true;
            lbl.InnerText = ex.Message;
        }
    }

    private void BindRepeater()
    {
        int? CompanyId = Convert.ToInt32(ddlCompanySearch.SelectedValue);
        bool IsDeduction = Convert.ToBoolean(chkIsDeductionSearch.Checked);

        //var lst = context.HCM_CompanyFormula.Where(a => a.IsActive == true)
        //.Where(a => CompanyId == 0 ? true : a.CompanyID == CompanyId)
        //  .Where(a => a.HCM_Setup_Allowance.IsDeduction == IsDeduction)
        //.Where(a => txtSearch.Text.Trim() == string.Empty ? true : a.HCM_Setup_Allowance.AllowanceName.Contains(txtSearch.Text.Trim()))
        //  .Select(a => new
        //  {
        //      ID = a.CompanyFormulaID,
        //      Company = a.Setup_Company.CompanyName,
        //      Allowance = a.HCM_Setup_Allowance.AllowanceName,
        //      Formula = a.Formula,
        //      Type = a.HCM_Setup_Allowance.IsDeduction == true ? "Deduction" : "Allowance",
        //      AllowanceId = a.AllowanceID,
        //  })
        //  .ToList();

        var lst = context.HCM_Setup_Allowance.Where(a => a.IsActive == true && a.IsDeduction == IsDeduction)
        .Where(a => CompanyId == 0 ? true : a.CompanyId == CompanyId)
        //.Where(a => a.HCM_Setup_Allowance.IsDeduction == IsDeduction)
        .Where(a => txtSearch.Text.Trim() == string.Empty ? true : a.AllowanceName.Contains(txtSearch.Text.Trim()))
          .Select(a => new
          {
              ID = a.AllowanceID,
              Company = a.Setup_Company.CompanyName,
              Allowance = a.AllowanceName,
              Formula = a.HCM_CompanyFormula.Where(b => b.IsActive == true).FirstOrDefault().Formula == null ? "" : a.HCM_CompanyFormula.Where(b => b.IsActive == true).FirstOrDefault().Formula,
              Type = a.IsDeduction == true ? "Deduction" : "Allowance",
              FormulaId = a.HCM_CompanyFormula.Where(b => b.IsActive == true).FirstOrDefault().CompanyFormulaID == null ? 0 : a.HCM_CompanyFormula.Where(b => b.IsActive == true).FirstOrDefault().CompanyFormulaID,
          })
          .ToList();

        rpt.DataSource = lst;
        rpt.DataBind();
    }

    protected void ddlCompanySearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindFormulaTypeSearch(Convert.ToInt32(ddlCompanySearch.SelectedValue));
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

    protected void lbDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnDelete = (LinkButton)sender;
            RepeaterItem rptItem = (RepeaterItem)btnDelete.NamingContainer;
            divError.Visible = false;

            int Id = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfId")).Value);
            int FormulaId = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfFormulaId")).Value);

            HCM_Setup_Allowance objAll = context.HCM_Setup_Allowance.FirstOrDefault(j => j.AllowanceID == Id);

            if (objAll != null)
            {
                HCM_CompanyFormula obj = context.HCM_CompanyFormula.FirstOrDefault(j => j.CompanyFormulaID == FormulaId);
                if (obj != null)
                {

                    #region Audit Logs
                    //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
                    DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(Id), "HCM_Setup_Allowance", 3);
                    #endregion

                    DateTime dt = DateTime.Now;

                    objAll.IsActive = false;
                    objAll.ModifiedBy = UserKey;
                    objAll.ModifiedDate = dt;
                    objAll.UserIP = UserIP;
                    context.SaveChanges();

                    #region Audit Logs
                    //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
                    DataTable Datat1 = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(FormulaId), "HCM_CompanyFormula", 3);
                    #endregion

                    obj.IsActive = false;
                    obj.ModifiedBy = UserKey;
                    obj.ModifiedDate = dt;
                    obj.UserIP = UserIP;
                    context.SaveChanges();
                    //MessageCtrl.showMessageBox("Education Score has been deleted successfully.", MessageType.Success);
                    Success("Deleted successfully.");

                    BindRepeater();

                }
                else
                {
                    Error("Already Exist against.");
                }
            }
        }
        catch (Exception ex)
        {
            divError.Visible = true;
            lblError.InnerText = ex.Message;
        }
    }

    private void ResetControls()
    {
        //ddlFormulaTypeSearch.SelectedValue = "0";
        ddlCompanySearch.SelectedValue = "0";
        txtOperatorConstant.Text = string.Empty;
        ddlCompany.SelectedValue = "0";
        ddlAllowanceDeduction.SelectedValue = "0";
        txtAllowanceDeduction.Text = string.Empty;
        ddlType.SelectedValue = "0";


        hfModalId.Value = "";

        CreatedDataTable();
        BindPopupRepeater();
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

    public bool CheckAlreadyNameExists(int? FormulaTypeId)
    {
        int CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
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

        HCM_CompanyFormula obj = context.HCM_CompanyFormula.FirstOrDefault(j => j.AllowanceID == FormulaTypeId && j.IsActive == true /*&& j.FormulaTypeID != ModalId*/ && j.CompanyID == CompanyId);
        if (obj != null)
        {
            return true;
        }
        return false;
    }

    public bool CheckAlreadyAllowanceExists(string title, int CompanyId)
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

        HCM_Setup_Allowance obj = context.HCM_Setup_Allowance.FirstOrDefault(j => j.AllowanceName == title && j.IsActive == true && j.AllowanceID != ModalId && j.CompanyId == CompanyId);
        if (obj != null)
        {
            return true;
        }
        return false;
    }

    protected void lbDeletePopup_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnDelete = (LinkButton)sender;
            RepeaterItem rptItem = (RepeaterItem)btnDelete.NamingContainer;

            int Id = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfId")).Value);
            int ElementId = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfElementId")).Value);

            lbl.Visible = false;

            DataTable dt = (DataTable)ViewState["dt"];

            string index = "";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Id == Convert.ToInt32(Convert.ToString(dt.Rows[i]["ID"])))
                {
                    if (ElementId == (int)Constant.FormulaElement.Operator)
                    {
                        index = index + Convert.ToString(dt.Rows[i]["ID"]) + ",";

                        int nxtIndex = i + 1;

                        if (dt.Rows.Count > nxtIndex)
                        {
                            //index = index + (i + 1) + ",";
                            index = index + Convert.ToString(dt.Rows[nxtIndex]["ID"]) + ",";
                        }

                        break;
                    }
                    else
                    {
                        //index = index + i + ",";
                        index = index + Convert.ToString(dt.Rows[i]["ID"]) + ",";

                        if (i == dt.Rows.Count - 1)
                        {
                            int nxtIndex = i - 1;

                            //if (dt.Rows.Count < nxtIndex)
                            {
                                index = index + Convert.ToString(dt.Rows[nxtIndex]["ID"]) + ",";
                            }

                            break;
                        }
                        else
                        {
                            int nxtIndex = i + 1;

                            if (dt.Rows.Count > nxtIndex)
                            {
                                //index = index + (i + 1) + ",";
                                index = index + Convert.ToString(dt.Rows[nxtIndex]["ID"]) + ",";
                            }

                            break;
                        }

                    }

                    /*//dt.Rows.RemoveAt(i);

                    if (ElementId == (int)Constant.FormulaElement.Allowance)
                    {

                    }
                    else if (ElementId == (int)Constant.FormulaElement.Deduction)
                    {

                    }
                    else if (ElementId == (int)Constant.FormulaElement.Operator)
                    {
                        
                        index = index + i + ",";
                        index = index + (i - 1) + ",";
                    }
                    else if (ElementId == (int)Constant.FormulaElement.Constant)
                    {
                        index = index + i + ",";
                        index = index + (i - 1) + ",";
                    }
                    else if (ElementId == (int)Constant.FormulaElement.Salary)
                    {
                        if ((int)Constant.FormulaElement.Operator == Convert.ToInt32(Convert.ToString(dt.Rows[i + 1]["TypeId"])))
                        {
                            index = index + i + ",";
                            index = index + (i + 1) + ",";
                        }
                    }*/
                }
            }

            string[] arr = index.Split(',');

            if (arr.Length - 1 > 0)
            {
                for (int i = 0; i < arr.Length - 1; i++)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (Convert.ToInt32(arr[i]) == Convert.ToInt32(Convert.ToString(dt.Rows[j]["ID"])))
                        {
                            dt.Rows.RemoveAt(j);
                        }
                    }
                }
            }

            BindPopupRepeater();
        }
        catch (Exception ex)
        {
            lbl.Visible = true;
            lbl.InnerText = ex.Message;
        }
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindFormulaType(Convert.ToInt32(ddlCompany.SelectedValue));
    }

    protected void lbEdit_Click(object sender, EventArgs e)
    {
        LinkButton btnEdit = (LinkButton)sender;
        RepeaterItem rptItem = (RepeaterItem)btnEdit.NamingContainer;
        int ID = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfId")).Value);
        var lstEdit = context.HCM_Setup_Allowance.Where(x => x.AllowanceID == ID).FirstOrDefault();
        if (lstEdit != null)
        {
            hfModalId.Value = ID.ToString();
            txtAllowanceDeduction.Text = lstEdit.AllowanceName;
            chkIsDeduction.Checked = lstEdit.IsDeduction;
            ddlCompany.SelectedValue = Convert.ToString(lstEdit.CompanyId);

            var lstFormula = context.HCM_CompanyFormula.Where(x => x.AllowanceID == ID).FirstOrDefault();
            if (lstFormula != null)
            {
                chkbxFormula.Checked = true;
                lblFormula.Text = lstFormula.Formula;
            }
            else
            {
                chkbxFormula.Checked = false;
            }
            OpenPopup();

        }
    }
}