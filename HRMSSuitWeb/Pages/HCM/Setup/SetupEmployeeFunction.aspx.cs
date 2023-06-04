using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_HCM_Setup_SetupEmployeeFunction : Base
{
    Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            BindRepeater();
        }
    }

    private void BindRepeater()
    {
        string _FunctionCode = txtFunctionCode.Text;
        string _FunctionName = txtFunctionName.Text;

        var _Data = context.HRMS_Setup_EmployeeFunction.Where(x => x.IsActive == true && (x.FunctionCode.Contains(_FunctionCode) || _FunctionCode == "") && (x.FunctionName.Contains(_FunctionName) || _FunctionName == "") ).ToList();

        if (_Data.Count > 0)
        {
            rpt.DataSource = _Data;
            rpt.DataBind();

            rpt.UseAccessibleHeader = true;
            
            rpt.HeaderRow.TableSection =
            TableRowSection.TableHeader;
        }
        else
        {

            rpt.DataSource = null;
            rpt.DataBind();
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ResetControls();
    }
    private void ResetControls()
    {

        
        rpt.PageIndex = 0;
        BindRepeater();


        hfModalId.Value = "";
        
        txtFunctionCode.Text = "";
        txtFunctionName.Text = "";
       
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindRepeater();
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
    protected void btnAddNewRecord_Click(object sender, EventArgs e)
    {
        ResetControls();
        OpenPopup();

        hfModalId.Value = "";
        txtEmployeeFunctionCodeAdd.Text = "";
        txtFunctionNameAdd.Text = "";
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

                var lstEdit = context.HRMS_Setup_EmployeeFunction.Where(x => x.EmployeeFunctionId == ID).FirstOrDefault();

                if (lstEdit != null)
                {


                    ResetControls();

                    hfModalId.Value = ID.ToString();

                    txtEmployeeFunctionCodeAdd.Text = lstEdit.FunctionCode;
                    txtFunctionNameAdd.Text = lstEdit.FunctionName;

                    OpenPopup();

                }
                else
                {
                    Error("Record Can Not Edit.Beacuse Record Not Find.");
                }
            }
            else if (e.CommandName == "DeleteS")
            {
                var _Data = context.HRMS_Setup_EmployeeFunction.FirstOrDefault(x => x.EmployeeFunctionId == ID);

                #region Audit Logs
                //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
                DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(ID), "HRMS_Setup_EmployeeFunction", 3);
                #endregion

                _Data.IsActive = false;
                _Data.ModifiedBy = UserKey;
                _Data.ModifiedDate = DateTime.Now;
                context.SaveChanges();
                Success("Employee Function Deleted Successfully.");
                ResetControls();

            }
        }
        catch (Exception ex)
        {

            Error(ex.Message);
        }

    }
    public bool CheckAlreadyNameExists(string functioncode, string functionnme)
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
        //City ocity = context.Cities.FirstOrDefault(c => c.CityName == cityname);

        HRMS_Setup_EmployeeFunction obj = context.HRMS_Setup_EmployeeFunction.FirstOrDefault(j => (j.FunctionCode == functioncode || j.FunctionName == functionnme && j.EmployeeFunctionId != ModalId) && j.IsActive == true && j.EmployeeFunctionId != ModalId);

        if (obj != null)
        {
            return true;
        }
        return false;
    }
    private void Save()
    {
        if (!CheckAlreadyNameExists(txtEmployeeFunctionCodeAdd.Text, txtFunctionNameAdd.Text))
        {
            HRMS_Setup_EmployeeFunction obj = new HRMS_Setup_EmployeeFunction();
            obj.FunctionCode =txtEmployeeFunctionCodeAdd.Text;
            obj.FunctionName = txtFunctionNameAdd.Text;
            obj.IsActive = true;
           
            obj.CreatedBy = UserId;
            obj.CreatedDate = DateTime.Now;
            context.HRMS_Setup_EmployeeFunction.Add(obj);
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

        int FunctionId = Convert.ToInt32(hfModalId.Value);

        if (!CheckAlreadyNameExists(txtEmployeeFunctionCodeAdd.Text, txtFunctionNameAdd.Text))
        {
            HRMS_Setup_EmployeeFunction obj = context.HRMS_Setup_EmployeeFunction.Where(a => a.EmployeeFunctionId == FunctionId).FirstOrDefault();

            #region Audit Logs
            //INSERT_INTO_AuditLog(int ParentKey, string Primarykey, string TableName, int OperationTypeId, int UserId, string UserIP);
            DataTable Datat = CommonHelper.INSERT_INTO_AuditLog(null, Convert.ToString(FunctionId), "HRMS_Setup_EmployeeFunction", 2);
            #endregion

            obj.FunctionCode = txtEmployeeFunctionCodeAdd.Text;
            obj.FunctionName = txtFunctionNameAdd.Text;
          
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
          
            if (hfModalId.Value == string.Empty)
            {
                Save();
            }
            else
            {
                Update();
            }

            ClosePopup();
        }
        catch (Exception ex)
        {
            Error(ex.ToString());
        }
    }
}