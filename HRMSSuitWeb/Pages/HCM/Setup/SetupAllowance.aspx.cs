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

public partial class Pages_HCM_Setup_SetupAllowance : Base
{
    public string Type
    {
        get
        {
            return Request.QueryString["type"];
        }
    }

    string type = "Bonus";
    Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();
    //DataTable dt = new DataTable();
    bool IsBonus = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Type == type)
            {
                dvDeductionSearch.Visible = false;
                dvDeduction.Visible = false;
                dvBonus.Visible = true;
                lbl1.Text = lbl2.Text = lbl3.Text = lbl4.Text = lbl5.Text = lbl6.Text = Type;
                chkbxFormula.Checked = true;
                chkbxFormula.Enabled = false;

            }

            //CreatedDataTable();
            BindDropDown();
            BindRepeater();
        }

        if (Type == type)
        {
            IsBonus = true;
        }
    }

    //private void CreatedDataTable()
    //{
    //    dt = new DataTable();
    //    dt.Columns.Clear();

    //    dt.Columns.Add("ID", typeof(Int32));
    //    dt.Columns.Add("FormulaElementDetailId");
    //    dt.Columns.Add("TypeId");
    //    dt.Columns.Add("Type");
    //    dt.Columns.Add("Title");
    //    dt.Columns.Add("TitleActual");

    //    ViewState["dt"] = dt;
    //}

    private void BindAllowanceDropdown()
    {
        int AllowanceIdEdit = Convert.ToInt32(hfModalId.Value);
        int CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        //int ElementId = Convert.ToInt32(ddlType.SelectedValue);
        int ElementId = Convert.ToInt32(rdbtnLstType.SelectedValue);
        bool IsDeduction = false;

        if (ElementId == (int)Constant.FormulaElement.Deduction)
        {
            IsDeduction = true;
        }

        var lst = context.HCM_Setup_Allowance.Where(a => a.IsActive == true && a.IsDeduction == IsDeduction && a.CompanyId == CompanyId)
            .Where(b => AllowanceIdEdit == 0 ? true : b.AllowanceID != AllowanceIdEdit)

             .Select(a => new
             {
                 Id = a.AllowanceID,
                 Value = a.AllowanceName,

             })
             .ToList();

        //CommonHelper.BindDropDown(ddlAllowanceDeduction, lst, "Value", "Id", true, false);
        CommonHelper.BindRadioButtonList(rdbtnLstTypeDetail, lst, "Value", "Id", false, false);

        if (lst.Count > 0)
        {
            rdbtnLstTypeDetail.SelectedIndex = 0;
        }
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

        //CommonHelper.BindDropDown(ddlAllowanceDeduction, lst, "Value", "Id", true, false);
        CommonHelper.BindRadioButtonList(rdbtnLstTypeDetail, lst, "Value", "Id", false, false);
        rdbtnLstTypeDetail.SelectedIndex = 0;
    }

    private void BindAbsentFlexis()
    {
        var lst = context.HCM_Setup_Detail.Where(a => a.IsActive == true && a.ParentId == (int)Constant.FormulaElement.AbsentFlexi)
             .Select(a => new
             {
                 Id = a.SetupDetailID,
                 Value = a.ColumnValue
             })
            .ToList();

        //CommonHelper.BindDropDown(ddlAllowanceDeduction, lst, "Value", "Id", true, false);
        CommonHelper.BindRadioButtonList(rdbtnLstTypeDetail, lst, "Value", "Id", false, false);
        rdbtnLstTypeDetail.SelectedIndex = 0;
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

        //CommonHelper.BindDropDown(ddlAllowanceDeduction, lst, "Value", "Id", true, false);
        CommonHelper.BindRadioButtonList(rdbtnLstTypeDetail, lst, "Value", "Id", false, false);
        rdbtnLstTypeDetail.SelectedIndex = 0;
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

        //CommonHelper.BindDropDown(ddlType, lst, "Value", "Id", true, false);
        CommonHelper.BindRadioButtonList(rdbtnLstType, lst, "Value", "Id", false, false);
        rdbtnLstType.SelectedIndex = 0;
        rdbtnLstType_SelectedIndexChanged(null, null);
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

        var lst_SpecialType = context.HCM_Setup_Detail.Where(a => a.IsActive == true && a.SetupMasterID == (int)Constant.HCMSetupMaster.SpecialType)
            .Select(a => new
            {

                Id = a.SetupDetailID,
                Value = a.ColumnValue,

            }).ToList();

        CommonHelper.BindDropDown(ddlSpecialType, lst_SpecialType, "Value", "Id", true, false);
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
            //DataTable dt = (DataTable)ViewState["dt"];

            int TypeId = Convert.ToInt32(rdbtnLstType.SelectedValue);
            double ConstantNumber = 0;
            int ConstantId = 0;
            int FormulaElementDetailId = 0;
            string Allowance = "", Actual = "";
            int ElementId = Convert.ToInt32(rdbtnLstType.SelectedValue);
            string Element = "";
            int OperatorId = 0;
            string Operator = "";
            int AbsentId = 0;
            string Absent = "";

            //int Count = 0;
            //int SortOrder = Convert.ToInt32(txtSortOrder.Text );

            int Count = Convert.ToInt32(txtSortOrder.Text);

            //if (dt.Rows.Count == 0)
            //{
            //    Count = 1;
            //}
            //else
            //{
            //    Count = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["Id"]);
            //    Count = Count + 1;
            //}

            if (Count > 0)
            {
                if (ElementId == (int)Constant.FormulaElement.Allowance)
                {
                    FormulaElementDetailId = Convert.ToInt32(rdbtnLstTypeDetail.SelectedValue);
                    Allowance = rdbtnLstTypeDetail.SelectedItem.Text;
                    Element = "Allowance";

                    Allowance = "[" + Allowance + "]";
                    Actual = Allowance;
                    string formula = GetFormulaById(FormulaElementDetailId);

                    if (formula != string.Empty)
                    {
                        formula = "(" + formula + ")";
                        Allowance = formula;
                    }

                    //dt.Rows.Add(Count, FormulaElementDetailId, ElementId, Element, Allowance, Actual);
                }
                else if (ElementId == (int)Constant.FormulaElement.Deduction)
                {
                    FormulaElementDetailId = Convert.ToInt32(rdbtnLstTypeDetail.SelectedValue);
                    Allowance = rdbtnLstTypeDetail.SelectedItem.Text;
                    Element = "Deduction";

                    Allowance = "[" + Allowance + "]";
                    Actual = Allowance;
                    string formula = GetFormulaById(FormulaElementDetailId);

                    if (formula != string.Empty)
                    {
                        formula = "(" + formula + ")";
                        Allowance = formula;
                    }

                    //dt.Rows.Add(Count, FormulaElementDetailId, ElementId, Element, Allowance, Actual);
                }
                else if (ElementId == (int)Constant.FormulaElement.Operator)
                {
                    OperatorId = Convert.ToInt32(rdbtnLstTypeDetail.SelectedValue);
                    Operator = rdbtnLstTypeDetail.SelectedItem.Text;
                    Element = "Operator";

                    //dt.Rows.Add(Count, OperatorId, ElementId, Element, Operator, Operator);
                }
                else if (ElementId == (int)Constant.FormulaElement.Constant)
                {
                    ConstantNumber = Convert.ToDouble(txtOperatorConstant.Text);
                    Element = "Constant";

                    //dt.Rows.Add(Count, ConstantId, ElementId, Element, ConstantNumber, ConstantNumber);
                }
                else if (ElementId == (int)Constant.FormulaElement.Salary)
                {
                    FormulaElementDetailId = Convert.ToInt32(rdbtnLstTypeDetail.SelectedValue);
                    Allowance = rdbtnLstTypeDetail.SelectedItem.Text;
                    Element = "Salary";

                    Allowance = "[" + Allowance + "]";
                    //dt.Rows.Add(Count, FormulaElementDetailId, ElementId, Element, Allowance, Allowance);
                }
                else if (ElementId == (int)Constant.FormulaElement.AbsentFlexi)
                {
                    AbsentId = Convert.ToInt32(rdbtnLstTypeDetail.SelectedValue);
                    Absent = rdbtnLstTypeDetail.SelectedItem.Text;
                    Element = "Absent/Flexi";

                    //dt.Rows.Add(Count, AbsentId, ElementId, Element, Absent, Absent);
                }

                //if (dt != null)
                //{
                //    txtSortOrder.Text = Convert.ToString(dt.Rows.Count + 1);
                //}

                txtOperatorConstant.Text = string.Empty;
                rdbtnLstType.SelectedIndex = 0;
                rdbtnLstType_SelectedIndexChanged(null, null);

                try
                {
                    rdbtnLstTypeDetail.SelectedIndex = 0;
                }
                catch
                { }

                //ViewState["dt"] = dt;

                BindPopupRepeater();
            }
            else
            {
                Error("Sort Order must be greater than zero");
            }
        }
        catch (Exception ex)
        {
            divError.Visible = true;
            lblError.InnerText = ex.Message;
        }
    }

    private string GetFormulaById(int? Allowance_DeductionId)
    {
        string formula = string.Empty;

        //formula = context.HCM_CompanyFormula.Where(a => a.IsActive == true && a.AllowanceID == Allowance_DeductionId).FirstOrDefault().Formula == null ? string.Empty : context.HCM_CompanyFormula.Where(a => a.IsActive == true && a.AllowanceID == Allowance_DeductionId).FirstOrDefault().Formula;

        var s = context.HCM_CompanyFormula.Where(a => a.IsActive == true && a.AllowanceID == Allowance_DeductionId).ToList();

        if (s != null && s.Count > 0)
        {
            formula = s[0].Formula;
        }

        return formula;
    }

    private void BindPopupRepeater()
    {
        string FormulaStr = string.Empty;

        DataTable dt = (DataTable)ViewState["dt"];

        if (dt != null && dt.Rows.Count > 0)
        {
            int CurrSortNo = Convert.ToInt32(Convert.ToString(dt.Rows[dt.Rows.Count - 1]["ID"]));
            int index = 0;

            if (dt.Rows.Count > 1)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (CurrSortNo == Convert.ToInt32(row["ID"]))
                    {
                        for (int i = index; i < dt.Rows.Count - 1; i++)
                        {
                            dt.Rows[i]["ID"] = Convert.ToInt32(Convert.ToString(dt.Rows[i]["ID"])) + 1;
                        }

                        break;
                    }

                    index++;
                }
            }

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    if (i != dt.Rows.Count - 1)
            //    {
            //        if (CurrSortNo == Convert.ToInt32(Convert.ToString(dt.Rows[i]["ID"])))
            //        {

            //        }
            //    }
            //}
        }
        DataView dv = dt.DefaultView;
        dv.Sort = "ID";
        DataTable sortedDT = dv.ToTable();

        ViewState["dt"] = sortedDT;

        //rptAdd.DataSource = sortedDT;
        //rptAdd.DataBind();

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            FormulaStr = FormulaStr + Convert.ToString(dt.Rows[i]["Title"]) + " ";
        }

        lblFormula.Text = FormulaStr;
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindFormulaType(Convert.ToInt32(ddlCompany.SelectedValue));
    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        /* try
         {
             int ElementId = Convert.ToInt32(rdbtnLstType.SelectedValue);

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
         }*/
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string StrElementId = "", StrFormulaElementDetailId = "", StrElement = "", StrTitle = "", StrTitleActual = "";
            DateTime dt = DateTime.Now;
            int? IntNull = null;
            int? SpecialTypeId = Convert.ToInt32(ddlSpecialType.SelectedValue) == 0 ? IntNull : Convert.ToInt32(ddlSpecialType.SelectedValue);

            bool checkIsExist = CheckAlreadyAllowanceExists(txtAllowanceDeduction.Text.Trim(), Convert.ToInt32(ddlCompany.SelectedValue));

            if (Convert.ToInt32(hfModalId.Value) > 0)
            {
                if (chkbxFormula.Checked == false)
                {
                    int AllowanceId = Convert.ToInt32(hfModalId.Value);
                    var lstAllowance = context.HCM_Setup_Allowance.Where(a => a.AllowanceID == AllowanceId && a.IsActive == true).FirstOrDefault();

                    if (lstAllowance != null)
                    {
                        #region Audit Logs
                        //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
                        DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(AllowanceId), "HCM_Setup_Allowance", 2);
                        #endregion

                        lstAllowance.IsTaxableIncome = chkTaxableIncome.Checked;
                        lstAllowance.AllowanceName = txtAllowanceDeduction.Text.Trim();
                        lstAllowance.IsDeduction = chkIsDeduction.Checked;
                        lstAllowance.ModifiedBy = UserKey;
                        lstAllowance.ModifiedDate = dt;
                        lstAllowance.IsActive = true;
                        lstAllowance.UserIP = UserIP;
                        lstAllowance.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                        lstAllowance.SpecialTypeId = SpecialTypeId;
                        lstAllowance.OrderNumber = Convert.ToInt32(txtPayrollSheetPlacement.Text);
                        lstAllowance.AccountCode = txtAccountCode.Text.Trim();

                        context.SaveChanges();

                        Success("Allowance has been updated successfully.");

                        ResetControls();
                    }
                }
                else if (chkbxFormula.Checked == true)
                {
                    string Formula = lblFormula.Text, Allowances = "", Deductions = "";
                    //if (rptAdd.Items.Count > 0)
                    {
                        Expression v = new Expression(lblFormula.Text);
                        if (v.HasErrors())
                        {
                            Error("Formula is Incorrect.");

                            return;
                        }
                    }

                    int AllowanceId = Convert.ToInt32(hfModalId.Value);
                    var lstAllowance = context.HCM_Setup_Allowance.Where(a => a.AllowanceID == AllowanceId && a.IsActive == true).FirstOrDefault();

                    if (lstAllowance != null)
                    {
                        #region Audit Logs
                        //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
                        DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(AllowanceId), "HCM_Setup_Allowance", 2);
                        #endregion

                        lstAllowance.IsTaxableIncome = chkTaxableIncome.Checked;
                        lstAllowance.AllowanceName = txtAllowanceDeduction.Text.Trim();
                        lstAllowance.IsDeduction = chkIsDeduction.Checked;
                        lstAllowance.ModifiedBy = UserKey;
                        lstAllowance.ModifiedDate = dt;
                        lstAllowance.IsActive = true;
                        lstAllowance.UserIP = UserIP;
                        lstAllowance.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                        lstAllowance.SpecialTypeId = SpecialTypeId;
                        lstAllowance.OrderNumber = Convert.ToInt32(txtPayrollSheetPlacement.Text);
                        lstAllowance.AccountCode = txtAccountCode.Text.Trim();

                        context.SaveChanges();
                    }

                    var lstCompanyFormula = context.HCM_CompanyFormula.Where(a => a.AllowanceID == AllowanceId && a.IsActive == true).FirstOrDefault();
                    //int CompanyFormulaId = lstCompanyFormula.CompanyFormulaID;
                    int? CompanyFormulaId = null;
                    if (lstCompanyFormula != null)
                    {
                        CompanyFormulaId = lstCompanyFormula.CompanyFormulaID;

                        #region Audit Logs
                        //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
                        DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(CompanyFormulaId), "HCM_CompanyFormula", 2);
                        #endregion

                        lstCompanyFormula.ModifiedBy = UserKey;
                        lstCompanyFormula.ModifiedDate = dt;
                        lstCompanyFormula.IsActive = false;
                        lstCompanyFormula.UserIP = UserIP;

                        context.SaveChanges();

                        var lstBonus = context.HCM_Bonus.Where(a => a.CompanyFormulaId == CompanyFormulaId && a.IsActive == true).FirstOrDefault();
                        if (lstBonus != null)
                        {
                            #region Audit Logs
                            //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
                            DataTable Datat1 = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(lstBonus.BonusId), "HCM_Bonus", 2);
                            #endregion

                            lstBonus.ModifiedBy = UserKey;
                            lstBonus.ModifiedDate = dt;
                            lstBonus.IsActive = false; 
                            context.SaveChanges();
                        } 
                        HCM_CompanyFormula obj = new HCM_CompanyFormula();

                        obj.AllowanceID = AllowanceId;
                        //obj.SetupDetailID = 0;
                        obj.CompanyID = Convert.ToInt32(ddlCompany.SelectedValue);
                        obj.Formula = Formula;
                        obj.Deductions = Deductions;
                        obj.Allowances = Allowances;
                        obj.CreatedBy = UserKey;
                        obj.CreatedDate = dt;
                        obj.IsActive = true;
                        obj.UserIP = UserIP;
                        obj.FormulaElementDetailId = StrFormulaElementDetailId;
                        obj.ElementId = StrElementId;
                        obj.Element = StrElement;
                        obj.Title = StrTitle;
                        obj.TitleActual = StrTitleActual;

                        context.HCM_CompanyFormula.Add(obj);
                        context.SaveChanges();

                        CompanyFormulaId = obj.CompanyFormulaID;
                    }
                    else
                    {
                        HCM_CompanyFormula obj = new HCM_CompanyFormula();

                        obj.AllowanceID = AllowanceId;
                        //obj.SetupDetailID = 0;
                        obj.CompanyID = Convert.ToInt32(ddlCompany.SelectedValue);
                        obj.Formula = Formula;
                        obj.Deductions = Deductions;
                        obj.Allowances = Allowances;
                        obj.CreatedBy = UserKey;
                        obj.CreatedDate = dt;
                        obj.IsActive = true;
                        obj.UserIP = UserIP;
                        obj.FormulaElementDetailId = StrFormulaElementDetailId;
                        obj.ElementId = StrElementId;
                        obj.Element = StrElement;
                        obj.Title = StrTitle;
                        obj.TitleActual = StrTitleActual;

                        context.HCM_CompanyFormula.Add(obj);
                        context.SaveChanges();

                        CompanyFormulaId = obj.CompanyFormulaID;
                    }

                    if (IsBonus)
                    {
                        HCM_Bonus objBonus = new HCM_Bonus();

                        objBonus.CompanyFormulaId = Convert.ToInt32(CompanyFormulaId);
                        objBonus.MaxJoining = Convert.ToDateTime(txtMaxJoining.Text);
                        objBonus.BonusDate = Convert.ToDateTime(txtBonusDate.Text);
                        objBonus.ReleaseDate = Convert.ToDateTime(txtReleaseDate.Text);

                        objBonus.CreatedBy = UserKey;
                        objBonus.CreatedDate = dt;
                        objBonus.IsActive = true;
                        //objBonus.UserIP = UserIP;

                        context.HCM_Bonus.Add(objBonus);
                        context.SaveChanges();

                        Success(type + " has been updated successfully.");
                        ResetControls();
                    }
                    else
                    {
                        Success("Allowance has been updated successfully.");
                        ResetControls();
                    }
                }
            }
            else
            {
                if (checkIsExist)
                {
                    Error("Already Exist.");
                    BindRepeater();
                    return;
                }
                else
                {
                    if (chkbxFormula.Checked == false)
                    {
                        HCM_Setup_Allowance objAll = new HCM_Setup_Allowance();
                        objAll.IsTaxableIncome = Convert.ToBoolean(chkTaxableIncome.Checked);
                        objAll.AllowanceName = txtAllowanceDeduction.Text.Trim();
                        objAll.IsDeduction = chkIsDeduction.Checked;
                        objAll.CreatedBy = UserKey;
                        objAll.CreatedDate = dt;
                        objAll.IsActive = true;
                        objAll.UserIP = UserIP;
                        objAll.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                        objAll.SpecialTypeId = SpecialTypeId;
                        objAll.OrderNumber = Convert.ToInt32(txtPayrollSheetPlacement.Text);
                        objAll.AccountCode = txtAccountCode.Text.Trim();

                        context.HCM_Setup_Allowance.Add(objAll);
                        context.SaveChanges();

                        Success("Allowance has been added successfully.");
                        ResetControls();
                    }
                    else if (chkbxFormula.Checked == true)
                    {
                        string Formula = "", Allowances = "", Deductions = "";
                        //if (rptAdd.Items.Count > 0)
                        {

                            Expression v = new Expression(lblFormula.Text);
                            if (v.HasErrors())
                            {
                                Error("Formula is Incorrect.");

                                return;
                            }
                        }

                        HCM_Setup_Allowance objAll = new HCM_Setup_Allowance();
                        objAll.IsTaxableIncome = Convert.ToBoolean(chkTaxableIncome.Checked);
                        objAll.AllowanceName = txtAllowanceDeduction.Text.Trim();
                        objAll.IsDeduction = chkIsDeduction.Checked;
                        objAll.CreatedBy = UserKey;
                        objAll.CreatedDate = dt;
                        objAll.IsActive = true;
                        objAll.UserIP = UserIP;
                        objAll.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                        objAll.SpecialTypeId = SpecialTypeId;
                        objAll.OrderNumber = Convert.ToInt32(txtPayrollSheetPlacement.Text);
                        objAll.AccountCode = txtAccountCode.Text.Trim();

                        context.HCM_Setup_Allowance.Add(objAll);
                        context.SaveChanges();

                        int CompanyFormulaId = 0;
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
                            obj.FormulaElementDetailId = StrFormulaElementDetailId;
                            obj.ElementId = StrElementId;
                            obj.Element = StrElement;
                            obj.Title = StrTitle;
                            obj.TitleActual = StrTitleActual;

                            context.HCM_CompanyFormula.Add(obj);
                            context.SaveChanges();

                            CompanyFormulaId = obj.CompanyFormulaID;
                        }


                        if (Type == type)
                        {
                            HCM_Bonus objBonus = new HCM_Bonus();

                            objBonus.CompanyFormulaId = CompanyFormulaId;
                            objBonus.MaxJoining = Convert.ToDateTime(txtMaxJoining.Text);
                            objBonus.BonusDate = Convert.ToDateTime(txtBonusDate.Text);
                            objBonus.ReleaseDate = Convert.ToDateTime(txtReleaseDate.Text);

                            objBonus.CreatedBy = UserKey;
                            objBonus.CreatedDate = dt;
                            objBonus.IsActive = true;
                            //objBonus.UserIP = UserIP;

                            context.HCM_Bonus.Add(objBonus);
                            context.SaveChanges();

                            Success(type + " has been added successfully.");
                            ResetControls();
                        }
                        else
                        {
                            Success("Allowance has been added successfully.");
                            ResetControls();
                        }
                    }
                }
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



        if (Type == type)
        {
            var lst = context.HCM_Setup_Allowance.Where(a => a.IsActive == true)
            .Where(a => a.CompanyId == CompanyId)
            .Where(a => txtSearch.Text.Trim() == string.Empty ? true : a.AllowanceName.Contains(txtSearch.Text.Trim()))
            .Where(a => a.HCM_CompanyFormula.FirstOrDefault().HCM_Bonus.Count > 0)
              .Select(a => new
              {
                  ID = a.AllowanceID,
                  Company = a.Setup_Company.CompanyName,
                  Allowance = a.AllowanceName,
                  Formula = a.HCM_CompanyFormula.Where(b => b.IsActive == true).FirstOrDefault().Formula == null ? "" : a.HCM_CompanyFormula.Where(b => b.IsActive == true).FirstOrDefault().Formula,
                  Type = a.IsDeduction == true ? "Deduction" : "Allowance",
                  FormulaId = a.HCM_CompanyFormula.Where(b => b.IsActive == true).FirstOrDefault() == null ? 0 : a.HCM_CompanyFormula.Where(b => b.IsActive == true).FirstOrDefault().CompanyFormulaID,
                  TaxableIncome = a.IsTaxableIncome == true ? "Taxable" : "Non Taxable",
                  AccountCode = a.AccountCode,
              })
              .ToList();
            rpt.DataSource = lst;
            rpt.DataBind();
        }
        else
        {
            var lst = context.HCM_Setup_Allowance.Where(a => a.IsActive == true && a.IsDeduction == IsDeduction)
            .Where(a => a.CompanyId == CompanyId)
            .Where(a => txtSearch.Text.Trim() == string.Empty ? true : a.AllowanceName.Contains(txtSearch.Text.Trim()))
             .Where(a => a.HCM_CompanyFormula.FirstOrDefault().HCM_Bonus.Count == 0)
              .Select(a => new
              {
                  ID = a.AllowanceID,
                  Company = a.Setup_Company.CompanyName,
                  Allowance = a.AllowanceName,
                  Formula = a.HCM_CompanyFormula.Where(b => b.IsActive == true).FirstOrDefault().Formula == null ? "" : a.HCM_CompanyFormula.Where(b => b.IsActive == true).FirstOrDefault().Formula,
                  Type = a.IsDeduction == true ? "Deduction" : "Allowance",
                  FormulaId = a.HCM_CompanyFormula.Where(b => b.IsActive == true).FirstOrDefault() == null ? 0 : a.HCM_CompanyFormula.Where(b => b.IsActive == true).FirstOrDefault().CompanyFormulaID,
                  TaxableIncome = a.IsTaxableIncome == true ? "Taxable" : "Non Taxable",
                  AccountCode = a.AccountCode,
              })
              .ToList();
            rpt.DataSource = lst;
            rpt.DataBind();
        }

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
            DateTime dt = DateTime.Now;

            HCM_Setup_Allowance objAll = context.HCM_Setup_Allowance.FirstOrDefault(j => j.AllowanceID == Id && j.IsActive == true);

            if (objAll != null)
            {
                HCM_CompanyFormula obj = context.HCM_CompanyFormula.FirstOrDefault(j => j.AllowanceID == Id && j.IsActive == true);
                FormulaId = obj.CompanyFormulaID;
                if (obj != null)
                {
                    #region Audit Logs
                    //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
                    DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(Id), "HCM_Setup_Allowance", 3);
                    #endregion

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

                    if (IsBonus)
                    {
                        var lstBonus = context.HCM_Bonus.Where(a => a.CompanyFormulaId == FormulaId && a.IsActive == true).FirstOrDefault();
                        if (lstBonus != null)
                        {
                            #region Audit Logs
                            //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
                            DataTable Datat2 = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(lstBonus.BonusId), "HCM_Bonus", 3);
                            #endregion

                            lstBonus.ModifiedBy = UserKey;
                            lstBonus.ModifiedDate = dt;
                            lstBonus.IsActive = false;
                            context.SaveChanges();
                        }
                    }

                    Success("Deleted successfully.");

                    BindRepeater();

                }
                else
                {
                    #region Audit Logs
                    //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
                    DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(Id), "HCM_Setup_Allowance", 3);
                    #endregion

                    objAll.IsActive = false;
                    objAll.ModifiedBy = UserKey;
                    objAll.ModifiedDate = dt;
                    objAll.UserIP = UserIP;
                    context.SaveChanges();

                    Success("Deleted successfully.");

                    BindRepeater();
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
        lblFormula.Text = "";
        //ddlFormulaTypeSearch.SelectedValue = "0";
        ddlSpecialType.SelectedValue = "0";
        ddlCompanySearch.SelectedValue = "0";
        txtSearch.Text = string.Empty;

        txtOperatorConstant.Text = string.Empty;
        ddlCompany.SelectedValue = "0";
        rdbtnLstTypeDetail.SelectedIndex = 0;
        txtAllowanceDeduction.Text = string.Empty;
        rdbtnLstTypeDetail.SelectedIndex = 0;
        txtReleaseDate.Text = string.Empty;
        txtBonusDate.Text = string.Empty;
        txtMaxJoining.Text = string.Empty;

        hfModalId.Value = "0";

        //CreatedDataTable();
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
        if (hfModalId.Value != "0")
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
        if (hfModalId.Value != "0")
        {
            ModalId = Convert.ToInt32(hfModalId.Value);
        }
        else
        {
            ModalId = 0;
        }
        //City ocity = context.Cities.FirstOrDefault(c => c.CityName == cityname);

        HCM_Setup_Allowance obj = context.HCM_Setup_Allowance.FirstOrDefault(j => j.AllowanceName == title && j.IsActive == true /*&& j.AllowanceID != ModalId*/ && j.CompanyId == CompanyId);
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



            #region Old Running Code

            /*
             * string index = "";
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
                           
                            index = index + Convert.ToString(dt.Rows[nxtIndex]["ID"]) + ",";
                        }

                        break;
                    }
                    else
                    {
                     
                        index = index + Convert.ToString(dt.Rows[i]["ID"]) + ",";

                        if (i == dt.Rows.Count - 1)
                        {
                            if (i > 0)
                            {
                                int nxtIndex = i - 1;

                               
                                    index = index + Convert.ToString(dt.Rows[nxtIndex]["ID"]) + ",";
                             
                            }


                            break;
                        }
                        else
                        {
                            int nxtIndex = i + 1;

                            if (dt.Rows.Count > nxtIndex)
                            {
                               
                                index = index + Convert.ToString(dt.Rows[nxtIndex]["ID"]) + ",";
                            }

                            break;
                        }

                    }

                    
                    //if (ElementId == (int)Constant.FormulaElement.Allowance)
                    //{

                    //}
                    //else if (ElementId == (int)Constant.FormulaElement.Deduction)
                    //{

                    //}
                    //else if (ElementId == (int)Constant.FormulaElement.Operator)
                    //{
                        
                    //    index = index + i + ",";
                    //    index = index + (i - 1) + ",";
                    //}
                    //else if (ElementId == (int)Constant.FormulaElement.Constant)
                    //{
                    //    index = index + i + ",";
                    //    index = index + (i - 1) + ",";
                    //}
                    //else if (ElementId == (int)Constant.FormulaElement.Salary)
                    //{
                    //    if ((int)Constant.FormulaElement.Operator == Convert.ToInt32(Convert.ToString(dt.Rows[i + 1]["TypeId"])))
                    //    {
                    //        index = index + i + ",";
                    //        index = index + (i + 1) + ",";
                    //    }
                    //}
               
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
        */
            #endregion

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Id == Convert.ToInt32(Convert.ToString(dt.Rows[i]["ID"])))
                {
                    dt.Rows.RemoveAt(i);
                    break;
                }
            }

            int inc = 0;
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        inc = i + 1;
                        dt.Rows[i]["ID"] = inc;
                    }
                }
            }

            txtSortOrder.Text = Convert.ToString(inc + 1);

            ViewState["dt"] = dt;

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
        try
        {
            lblFormula.Text = "";
            LinkButton btnEdit = (LinkButton)sender;
            RepeaterItem rptItem = (RepeaterItem)btnEdit.NamingContainer;
            int ID = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfId")).Value);

            //lblFormula.Text = "";

            //CreatedDataTable();
            //BindPopupRepeater();

            var lstEdit = context.HCM_Setup_Allowance.Where(x => x.AllowanceID == ID && x.IsActive == true).FirstOrDefault();
            if (lstEdit != null)
            {
                hfModalId.Value = ID.ToString();
                txtAllowanceDeduction.Text = lstEdit.AllowanceName;
                chkIsDeduction.Checked = lstEdit.IsDeduction;
                ddlCompany.SelectedValue = Convert.ToString(lstEdit.CompanyId);
                chkTaxableIncome.Checked = lstEdit.IsTaxableIncome;
                ddlSpecialType.SelectedValue = Convert.ToString(lstEdit.SpecialTypeId == null ? 0 : lstEdit.SpecialTypeId);
                txtPayrollSheetPlacement.Text = Convert.ToString(lstEdit.OrderNumber == null ? 0 : lstEdit.OrderNumber);
                txtAccountCode.Text = lstEdit.AccountCode;
                rdbtnLstType_SelectedIndexChanged(null, null);

                var lstFormula = context.HCM_CompanyFormula.Where(x => x.AllowanceID == ID && x.IsActive == true).FirstOrDefault();

                if (lstFormula != null)
                {
                    chkbxFormula.Checked = true;
                    lblFormula.Text = lstFormula.Formula;

                    if (Type == type)
                    {
                        var LstBonus = context.HCM_Bonus.Where(a => a.IsActive == true && a.CompanyFormulaId == lstFormula.CompanyFormulaID).FirstOrDefault();

                        if (LstBonus != null)
                        {
                            txtBonusDate.Text = LstBonus.BonusDate.ToString("yyyy-MM-dd");
                            txtMaxJoining.Text = LstBonus.MaxJoining.ToString("yyyy-MM-dd");
                            txtReleaseDate.Text = LstBonus.ReleaseDate.ToString("yyyy-MM-dd");
                        }
                    }

                }
                else
                {
                    chkbxFormula.Checked = false;
                }
                OpenPopup();
            }
        }
        catch
        { }
    }

    protected void rdbtnLstType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //int ElementId = Convert.ToInt32(ddlType.SelectedValue);
            int ElementId = Convert.ToInt32(rdbtnLstType.SelectedValue);
            lblTypeDetail.Text = rdbtnLstType.SelectedItem.Text;

            if (ElementId == (int)Constant.FormulaElement.Allowance || ElementId == (int)Constant.FormulaElement.Deduction)
            {
                //dvlblOperatorConstant.Visible = false;
                dvOperatorConstant.Visible = false;
                //dvAllowanceDeduction.Visible = true;
                //dvlblAllowanceDeduction.Visible = true;
                rdbtnLstTypeDetail.Visible = true;
                BindAllowanceDropdown();
            }
            else if (ElementId == (int)Constant.FormulaElement.Operator)
            {
                //dvlblOperatorConstant.Visible = false;
                dvOperatorConstant.Visible = false;
                //dvAllowanceDeduction.Visible = true;
                //dvlblAllowanceDeduction.Visible = true;
                rdbtnLstTypeDetail.Visible = true;
                BindOperator();
            }
            else if (ElementId == (int)Constant.FormulaElement.Constant)
            {
                //dvlblAllowanceDeduction.Visible = false;
                //dvAllowanceDeduction.Visible = false;
                dvOperatorConstant.Visible = true;
                //dvlblOperatorConstant.Visible = true;

                rdbtnLstTypeDetail.Visible = false;

            }
            else if (ElementId == (int)Constant.FormulaElement.Salary)
            {
                //dvlblOperatorConstant.Visible = false;
                dvOperatorConstant.Visible = false;
                //dvAllowanceDeduction.Visible = true;
                //dvlblAllowanceDeduction.Visible = true;
                rdbtnLstTypeDetail.Visible = true;
                BindSalary();
            }
            else if (ElementId == (int)Constant.FormulaElement.AbsentFlexi)
            {
                //dvlblOperatorConstant.Visible = false;
                dvOperatorConstant.Visible = false;
                //dvAllowanceDeduction.Visible = true;
                //dvlblAllowanceDeduction.Visible = true;
                rdbtnLstTypeDetail.Visible = true;
                BindAbsentFlexis();
            }
        }
        catch (Exception ex)
        {
            divError.Visible = true;
            lblError.InnerText = ex.Message;
        }
    }

    protected void ddlCompany_SelectedIndexChanged1(object sender, EventArgs e)
    {
        BindAllowanceDropdown();
    }
}