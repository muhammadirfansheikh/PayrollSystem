using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
public partial class Pages_HCM_MedicalReinbursement : System.Web.UI.Page
{
    Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();
    protected void Page_Load(object sender, EventArgs e)
    { 
    }

    protected void txtDateMonth_TextChanged(object sender, EventArgs e)
    {
       // Response.Write("<script>alert('');</script>");
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}