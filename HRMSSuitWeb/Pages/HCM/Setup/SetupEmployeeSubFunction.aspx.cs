using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_HCM_Setup_SetupEmployeeSubFunction : Base
{
    Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropdown();
            BindRepeater(); 
        }
    }
    private void BindRepeater()
    {
        int? GroupId = null;
        int? CompanyId = null;
        int? EmployeeId = null;
        int? FunctionId = null;
        string _SubFunctionCode = txtSubFunctionCode.Text;
        string _SubFunctionName = txtSubFunctionName.Text;

       
        if (Convert.ToInt32(ddlgroupSearch.SelectedValue) > 0)
            GroupId = Convert.ToInt32(ddlgroupSearch.SelectedValue);


        if (Convert.ToInt32(ddlCompany.SelectedValue) > 0)
            CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        if (Convert.ToInt32(cmbFunction.SelectedValue) > 0)
            FunctionId = Convert.ToInt32(cmbFunction.SelectedValue);

        var _Data = context.HRMS_Setup_EmployeeSubFunction.Where(x => (x.EmployeeFunctionId == FunctionId || FunctionId == null)&&(x.CompanyId == CompanyId || CompanyId == null) && (x.SubFunctionCode.Contains(_SubFunctionCode) || _SubFunctionCode == "") && (x.SubFunction.Contains(_SubFunctionName) || _SubFunctionName == "") && x.IsActive == true).Select(x=> new {

            FunctionName = x.HRMS_Setup_EmployeeFunction.FunctionName,
            SubFunctionCode = x.SubFunctionCode,
            SubFunctionName = x.SubFunction,
            SubFunctionId = x.EmployeeSubFunctionId
        }).ToList();


        if (_Data.Count > 0)
        {
            rpt.DataSource = _Data;
            rpt.DataBind();

            rpt.UseAccessibleHeader = true;
            //adds <thead> and <tbody> elements
            rpt.HeaderRow.TableSection =
            TableRowSection.TableHeader;
        }
        else
        {

            rpt.DataSource = null;
            rpt.DataBind();
        }



    }
    private void BindDropdown()
    {
        var li = context.Setup_Group.Where(a => a.IsActive == true).ToList();
        CommonHelper.BindDropDown(ddlgroupSearch, li, "GroupName", "GroupId", li.Count == 1 ? false : true, false);
        CommonHelper.BindDropDown(ddlGroupAdd, li, "GroupName", "GroupId", li.Count == 1 ? false : true, false);
        ddlgroupSearch_SelectedIndexChanged(null, null);
       ddlGroupAdd_SelectedIndexChanged(null, null);
       var liFunction = context.HRMS_Setup_EmployeeFunction.Where(a => a.IsActive == true).ToList();
        CommonHelper.BindDropDown(cmbFunction, liFunction, "FunctionName", "EmployeeFunctionId", liFunction.Count == 1 ? false : true, false);
        
        CommonHelper.BindDropDown(cmbFunctionAdd, liFunction, "FunctionName", "EmployeeFunctionId", liFunction.Count == 1 ? false : true, false);


    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ResetControls();
    }
    private void ResetControls()
    {

        BindDropdown();
        rpt.PageIndex = 0;
        BindRepeater();


        //hfModalId.Value = "";
        txtSubFunctionNameAdd.Text = "";
        txtSubFunctionCode.Text = "";
        txtSubFunctionCode.Text = "";
        txtSubFunctionName.Text = "";
        
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindRepeater();
    }

    protected void btnAddNewRecord_Click(object sender, EventArgs e)
    {
        ResetControls();
        OpenPopup();

        hfModalId.Value = "";

        txtFunctionCodeAdd.Text = "";
        txtSubFunctionNameAdd.Text = "";
    }

    protected void rpt_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        rpt.PageIndex = e.NewPageIndex;
        BindRepeater();
    }

    protected void rpt_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "EditS")
            {

                var lstEdit = context.HRMS_Setup_EmployeeSubFunction.Where(x=>x.EmployeeSubFunctionId == ID).FirstOrDefault();

                if (lstEdit != null)
                { 
                    ResetControls();

                    hfModalId.Value = ID.ToString();


                    ddlGroupAdd.SelectedValue =  context.Setup_Company.Where(x=>x.CompanyId == lstEdit.CompanyId).FirstOrDefault().GroupId.ToString();
                    ddlGroupAdd_SelectedIndexChanged(null,null);
                    ddlCompanyAdd.SelectedValue = lstEdit.CompanyId.ToString();
                    cmbFunctionAdd.SelectedValue = lstEdit.EmployeeFunctionId.ToString();

                    txtFunctionCodeAdd.Text = lstEdit.SubFunctionCode;
                    txtSubFunctionNameAdd.Text = lstEdit.SubFunction; 
                    OpenPopup();

                }
                else
                {
                    Error("Record Can Not Edit.Beacuse Record Not Find.");
                }
            }
            else if (e.CommandName == "DeleteS")
            {
                var _Data = context.HRMS_Setup_EmployeeSubFunction.FirstOrDefault(x => x.EmployeeSubFunctionId == ID);

                #region Audit Logs
                //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
                DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(ID), "HRMS_Setup_EmployeeSubFunction", 3);
                #endregion

                _Data.IsActive = false;
                _Data.ModifiedBy = UserKey;
                _Data.ModifiedDate = DateTime.Now;
                context.SaveChanges();
                Success("Sub Function Deleted Successfully.");
                ResetControls();

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

    protected void ddlGroupAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        int GroupId = ddlGroupAdd.SelectedValue == "" ? 0 : Convert.ToInt32(ddlGroupAdd.SelectedValue);
        DataSet ds = CommonHelper.Get_Control_Data_EmployeeSearchFilter("OnChangeGroup", GroupId, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, UserKey);
        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables.Count == 1)
            {
                CommonHelper.BindDropDown(ddlCompanyAdd, ds.Tables[0], "Value", "Id", false, false);
            }
        }
    }
    public bool CheckAlreadyNameExists(int companyid, int functionid, string functioncode, string functionnme)
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

        HRMS_Setup_EmployeeSubFunction obj = context.HRMS_Setup_EmployeeSubFunction.FirstOrDefault(j => (j.SubFunctionCode == functioncode || j.SubFunction == functionnme && j.EmployeeFunctionId != ModalId) && j.CompanyId == CompanyId && j.EmployeeFunctionId == functionid && j.IsActive == true && j.EmployeeFunctionId != ModalId);

        if (obj != null)
        {
            return true;
        }
        return false;
    }
    private void Save()
    {
        if (!CheckAlreadyNameExists(Convert.ToInt32(ddlCompanyAdd.SelectedValue),Convert.ToInt32(cmbFunctionAdd.SelectedValue),txtFunctionCodeAdd.Text,txtSubFunctionNameAdd.Text))
        {
            HRMS_Setup_EmployeeSubFunction obj = new HRMS_Setup_EmployeeSubFunction();
            obj.CompanyId = Convert.ToInt32(ddlCompanyAdd.SelectedValue);
            obj.EmployeeFunctionId = Convert.ToInt32(cmbFunctionAdd.SelectedValue);
            obj.SubFunctionCode = txtFunctionCodeAdd.Text;
            obj.SubFunction = txtSubFunctionNameAdd.Text;

            obj.IsActive = true;
            obj.CreatedBy = UserId;
            obj.CreatedDate = DateTime.Now;
            context.HRMS_Setup_EmployeeSubFunction.Add(obj);
            context.SaveChanges();
            Success("Saved Successfully");
            ClosePopup();
            BindRepeater();
            ResetControls();
        }
        else
        {
            Error("Already Exist");

        }
    }
    private void Update()
    {

        int SubFunctionId = Convert.ToInt32(hfModalId.Value);

        if (!CheckAlreadyNameExists(Convert.ToInt32(ddlCompanyAdd.SelectedValue), Convert.ToInt32(cmbFunctionAdd.SelectedValue), txtFunctionCodeAdd.Text, txtSubFunctionNameAdd.Text))
        {
            #region Audit Logs
            //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
            DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(SubFunctionId), "HRMS_Setup_EmployeeSubFunction", 2);
            #endregion

            HRMS_Setup_EmployeeSubFunction obj = context.HRMS_Setup_EmployeeSubFunction.Where(a => a.EmployeeSubFunctionId == SubFunctionId).FirstOrDefault();

            obj.CompanyId = Convert.ToInt32(ddlCompanyAdd.SelectedValue);
            obj.EmployeeFunctionId = Convert.ToInt32(cmbFunctionAdd.SelectedValue);
            obj.SubFunction = txtSubFunctionNameAdd.Text;
            obj.SubFunctionCode = txtFunctionCodeAdd.Text;
           
            obj.IsActive = true;
            obj.ModifiedBy = UserId;
            obj.ModifiedDate = DateTime.Now;

            context.SaveChanges();

            Success("Updated Successfully");
            ClosePopup();
            BindRepeater();

            ResetControls();
        }
        else
        {
            Error("Already Exist");

        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //if (!CheckAlreadyNameExists(Convert.ToInt32(ddlCompanyAdd.SelectedValue), Convert.ToInt32(ddlAllowance.SelectedValue), Convert.ToInt32(txtMinYear.Text)
            //    , Convert.ToInt32(txtMaxYear.Text), Convert.ToDouble(txtDays.Text), Convert.ToDouble(txtFactor.Text)))
            if (hfModalId.Value == string.Empty)
            {
                Save();
            }
            else
            {
                Update();
            }
        }
        catch (Exception ex)
        {
            Error(ex.ToString());
        }
    }
}