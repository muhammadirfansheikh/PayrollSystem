using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

public partial class Pages_HRMS_Setup_EmailList : System.Web.UI.Page
{
    Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();
    Base baseclass = new Base();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDown();
            BindRepeater();
        }

    }


    private void BindRepeater()
    {
        //int? NullVar=null;
        //int? DepartmentId = ddlDepartmentSearch.SelectedValue == "0" ? NullVar : Convert.ToInt32(ddlDepartmentSearch.SelectedValue);
        //var ListRpt = context.GET_Email_toEmail().ToList();

        //rpt.DataSource = ListRpt;
        //rpt.DataBind();

    }

    private void BindDropDown()
    {
        var CompanyList = context.Setup_Company.Where(x => x.IsActive == true && (x.Setup_EmployeeCompanyMapping.Any(a => x.CompanyId == baseclass.CompanyId))).ToList();
        CommonHelper.BindDropDown(ddlCompanySearch, CompanyList, "CompanyName", "CompanyId", true, false);
        ddlCompanySearch_SelectedIndexChanged(null, null);
    }
    protected void ddlCompanySearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        int CompanyId_ = Convert.ToInt32(ddlCompanySearch.SelectedValue);
        var list = context.Setup_UserAccessLocation.Where(c => c.IsActive == true && c.EmployeeId == baseclass.UserKey
            && c.IsActive == true
            && (c.Setup_Location.CompanyId == CompanyId_)
            ).Select(c => new
            {
                LocationName = c.Setup_Location.LocationName,
                LocationId = c.Setup_Location.LocationId,
            }).OrderBy(a => a.LocationName).ToList();
        CommonHelper.BindDropDown(ddlLocationSearch, list, "LocationName", "LocationId", true, false);
        ddlLocationSearch_SelectedIndexChanged(null, null);

    }
    protected void ddlLocationSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        int LocationId = Convert.ToInt32(ddlLocationSearch.SelectedValue);
        var List = context.TS_Setup_BusinessUnit.Where(x => x.IsActive == true && x.CompanyId == baseclass.CompanyId).Select
        (a => new
        {
           BusninessUnitId=a.BusinessUnitId,
           BusinessUnitName=a.BusinessUnitName
        }).ToList();

        CommonHelper.BindDropDown(ddlBusinessUnitSearch, List, "BusinessUnitName", "BusninessUnitId", true, false);
        ddlBusinessUnitSearch_SelectedIndexChanged(null, null);

    }

    protected void ddlBusinessUnitSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        int BUID = Convert.ToInt32(ddlBusinessUnitSearch.SelectedValue);
        var ListDept = context.Setup_Department.Where(x => x.IsActive == true && x.BusinessUnitId == BUID).Select
            (a => new
            {
                DepartmentID=a.DepartmentId,
                DepartmentName=a.DepartmentName
            }).ToList();
        CommonHelper.BindDropDown(ddlDepartmentSearch, ListDept, "DepartmentName", "DepartmentID", true, false);

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }
}