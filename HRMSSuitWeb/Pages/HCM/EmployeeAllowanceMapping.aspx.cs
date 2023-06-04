using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

public partial class Pages_HCM_EmployeeAllowanceMapping : System.Web.UI.Page
{
    Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        var lstLevels = context.Setup_Designation.Where(x => x.IsActive == true).ToList();
        ddlSelectLevel.DataSource = lstLevels;
        ddlSelectLevel.DataValueField = "DesignationId";
        ddlSelectLevel.DataTextField = "DesignationName";
        ddlSelectLevel.DataBind();
    }



    protected void ddlSelectLevel_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }
}