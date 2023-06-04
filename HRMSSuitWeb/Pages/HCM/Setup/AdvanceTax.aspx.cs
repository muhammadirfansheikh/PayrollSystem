using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_HCM_Setup_AdvanceTax : Base
{
    Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropdownSearch();
            BindDropdownAdd();
            BindRepeater();

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ResetControls();
    }
    private void BindDropdownSearch()
    {
        var li = context.Setup_Group.Where(a => a.IsActive == true).ToList();
        CommonHelper.BindDropDown(ddlGroupSearch, li, "GroupName", "GroupId", li.Count == 1 ? false : true, false);
        ddlGroupSearch_SelectedIndexChanged(null, null);
       

        // ddlGroupAdd_SelectedIndexChanged(null, null);

    }

    private void BindDropdownAdd()
    {
        var li = context.Setup_Group.Where(a => a.IsActive == true).ToList();
        CommonHelper.BindDropDown(ddlgroupAdd, li, "GroupName", "GroupId", li.Count == 1 ? false : true, false);
        ddlgroupAdd_SelectedIndexChanged(null, null);
        ddlCompanyAdd_SelectedIndexChanged(null, null);

        // ddlGroupAdd_SelectedIndexChanged(null, null);

    }
    private void BindRepeater()
    {
        int? CompanyId = null;


        if (Convert.ToInt32(ddlCompanySearch.SelectedValue) > 0)
            CompanyId = Convert.ToInt32(ddlCompanySearch.SelectedValue);

       

        DataTable _Data = GetData(CompanyId);

        if (_Data.Rows.Count > 0)
        {
            rpt.DataSource = _Data;
            rpt.DataBind();
        }
        else
        {

            rpt.DataSource = null;
            rpt.DataBind();
        }



    }

    private DataTable GetData(int? companyId)
    {

        DataTable dt = new DataTable();
        string dbConnectionString = context.Database.Connection.ConnectionString;
        SqlConnection con = new SqlConnection(dbConnectionString);
        SqlDataAdapter da = new SqlDataAdapter("HCM_SP_GET_ADVANCE_TAX_LIST", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.CommandTimeout = Int32.MaxValue;

        da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = companyId;
      
        da.Fill(dt);


        return dt;
    }



    protected void ddlgroupAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        int GroupId = ddlgroupAdd.SelectedValue == "" ? 0 : Convert.ToInt32(ddlgroupAdd.SelectedValue);
        DataSet ds = CommonHelper.Get_Control_Data_EmployeeSearchFilter("OnChangeGroup", GroupId, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, UserKey);
        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables.Count == 1)
            {
                CommonHelper.BindDropDown(ddlCompanyAdd, ds.Tables[0], "Value", "Id", false, false);
            }
        }
    }

    protected void ddlCompanyAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        int CompanyId = 0;

        CompanyId = ddlCompanyAdd.SelectedValue == "" ? 0 : Convert.ToInt32(ddlCompanyAdd.SelectedValue);
        var _EmployeeListData = context.Setup_Employee.Where(x => x.CompanyId == CompanyId && x.IsActive == true).OrderBy(x => x.EmployeeId).Select(x => new { Id = x.EmployeeId, Value = x.EmployeeCode + "_" + x.FirstName + " " + x.MiddleName + " " + x.LastName }).ToList();
        CommonHelper.BindDropDown(ddlEmployeeAdd, _EmployeeListData, "Value", "Id", _EmployeeListData.Count == 1 ? false : true, false);




    }

    protected void ddlGroupSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        int GroupId = ddlGroupSearch.SelectedValue == "" ? 0 : Convert.ToInt32(ddlGroupSearch.SelectedValue);
        DataSet ds = CommonHelper.Get_Control_Data_EmployeeSearchFilter("OnChangeGroup", GroupId, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, UserKey);
        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables.Count == 1)
            {
                CommonHelper.BindDropDown(ddlCompanySearch, ds.Tables[0], "Value", "Id", false, false);
            }
        }
    }

   
    protected void btnCancelSearch_Click(object sender, EventArgs e)
    {
        ResetControls();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindRepeater();
    }

    private void ResetControls()
    {

        BindDropdownSearch();
        BindDropdownAdd();
        rpt.PageIndex = 0;
        BindRepeater();


        
        txtAdvanceTax.Text = "0";
    }
    protected void rpt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //check if the row is the header row
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //add the thead and tbody section programatically
            e.Row.TableSection = TableRowSection.TableHeader;
        }
    }
}