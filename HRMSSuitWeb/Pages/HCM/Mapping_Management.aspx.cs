using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_HCM_Mapping_Management : Base
{
    Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region For Allownce Mapping
            ResetControl();
            #endregion


        }
    }

    #region For Allownce Mapping
    private void BindDropdown()
    {
        var li = context.Setup_Group.Where(a => a.IsActive == true).ToList();
        CommonHelper.BindDropDown(ddlgroupSearch, li, "GroupName", "GroupId", li.Count == 1 ? false : true, false);

        ddlgroupSearch_SelectedIndexChanged(null, null);

        ddlCompany_SelectedIndexChanged(null, null);

        ddlAllownces_SelectedIndexChanged(null, null);
        // ddlGroupAdd_SelectedIndexChanged(null, null);

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string RollBackEmloyee = "Transaction Rollback Against These Emoloyees : ";
            bool Flag = false;
            for (int i = 0; i < lstEmployees.Items.Count; i++)
            {
                if (lstEmployees.Items[i].Selected)
                {
                    int EmployeeID = Convert.ToInt32(lstEmployees.Items[i].Value);
                    int CompanyId = context.Setup_Employee.FirstOrDefault(x => x.IsActive == true && x.EmployeeId == EmployeeID).CompanyId;
                    int AllowncID = Convert.ToInt32(ddlAllownces.SelectedValue);
                    int Measure = txtMeasure.Text == "" ? 0 : Convert.ToInt32(txtMeasure.Text);

                    var _Result = context.SP_HCM_Mark_Employee_Allownce_With_For_Cast(AllowncID, CompanyId, EmployeeID, Measure, UserKey).FirstOrDefault();

                    if (_Result.MsgType != 1)
                    { Flag = true; RollBackEmloyee += lstEmployees.Items[i].Text; }

                }

            }

            if (Flag)
            {
                Error(RollBackEmloyee);
            }
            else
            {
                ResetControl();
                Success("Allownce Assigned To Selected Employees");
            }

        }
        catch (Exception ex)
        {

            Error(ex.Message);
        }
    }

    protected void ddlgroupSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        int GroupId = ddlgroupSearch.SelectedValue == "" ? 0 : Convert.ToInt32(ddlgroupSearch.SelectedValue);
        DataSet ds = CommonHelper.Get_Control_Data_EmployeeSearchFilter("OnChangeGroup", GroupId, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, UserKey);
        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables.Count == 1)
            {
                CommonHelper.BindDropDown(ddlCompany, ds.Tables[0], "Value", "Id", false, false);
            }
        }
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        int CompanyId = ddlCompany.SelectedValue == "" ? 0 : Convert.ToInt32(ddlCompany.SelectedValue);
        var _EmployeeListData = context.Setup_Employee.Where(x => x.CompanyId == CompanyId && x.IsActive == true).OrderBy(x => x.EmployeeId).Select(x => new { Id = x.EmployeeId, Value = x.EmployeeCode + "_" + x.FirstName + " " + x.MiddleName + " " + x.LastName }).Where(x => x.Value != null).OrderBy(x => x.Value).ToList();
        CommonHelper.BindDropDown(lstEmployees, _EmployeeListData, "Value", "Id", _EmployeeListData.Count == 1 ? false : false, false);


        var _AllownceData = context.HCM_Setup_Allowance.Where(x => x.CompanyId == CompanyId && x.IsActive == true).OrderBy(x => x.AllowanceName).Select(x => new { Id = x.AllowanceID, Value = x.AllowanceName }).OrderBy(x => x.Value).ToList();
        CommonHelper.BindDropDown(ddlAllownces, _AllownceData, "Value", "Id", _AllownceData.Count == 1 ? false : true, false);
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
    protected void ddlAllownces_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            int _AllownceID = Convert.ToInt32(ddlAllownces.SelectedValue);
            bool? _CheckIsFormulaExist = null;


            var _RecordCount = context.HCM_Setup_Allowance.Where(a => a.IsActive == true && a.AllowanceID == _AllownceID).ToList();

            if (_RecordCount.Count > 0)
            {
                _CheckIsFormulaExist = context.HCM_Setup_Allowance.Where(a => a.IsActive == true && a.AllowanceID == _AllownceID)
.Select(a => new
{
    IsFormulaExist = context.HCM_CompanyFormula.FirstOrDefault(x => x.IsActive == true && x.AllowanceID == a.AllowanceID).Formula == null || context.HCM_CompanyFormula.FirstOrDefault(x => x.IsActive == true && x.AllowanceID == a.AllowanceID).Formula == "" ? false : true,

}
).FirstOrDefault().IsFormulaExist;

            }


            if (_CheckIsFormulaExist != null)
            {
                if (Convert.ToBoolean(_CheckIsFormulaExist))
                {
                    txtMeasure.Attributes.Add("disabled", "true");
                    txtMeasure.Text = "";
                    rqMeasure.Enabled = false;
                }
                else
                {
                    txtMeasure.Attributes.Remove("disabled");
                    rqMeasure.Enabled = true;
                }

            }
            else
            {
                txtMeasure.Attributes.Add("disabled", "true");
                txtMeasure.Text = "";
                rqMeasure.Enabled = false;
            }




        }
        catch (Exception ex)
        {

            Error(ex.Message);
        }
    }

    private void ResetControl()
    {
        BindDropdown();

        txtMeasure.Attributes.Add("disabled", "true");
        rqMeasure.Enabled = false;
        txtMeasure.Text = "";
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ResetControl();
    }

    #endregion
}