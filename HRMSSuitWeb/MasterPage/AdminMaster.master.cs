using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

public partial class MasterPage_AdminMaster : System.Web.UI.MasterPage
{
    Sybrid_DatabaseEntities entities = new Sybrid_DatabaseEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblYear.Text = DateTime.Now.Year - 1 + "-" + DateTime.Now.Year;
            Base objBase = new Base();
            if (objBase.UserId != 0)
            {
                lblName.Text = objBase.FullName;
                imgUser.ImageUrl = objBase.UserImage == "/Picture/noprofilepic.png" ? "" : "/Picture/" + objBase.UserImage;
                lblDesignation.Text = objBase.Designation == "" ? "" : objBase.Designation;
            }
            else
            {
                Response.Redirect("/Login.aspx", true);
            }
        }
    }
    protected void lbLogout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        new Base().ExpireCookie();
        Response.Redirect("/Login.aspx", true);
    }
}
