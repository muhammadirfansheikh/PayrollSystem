using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_HCM_GenerateSalary : Base
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //hf_UserId.Value = Convert.ToString(UserId);

                //DAL.Sybrid_DatabaseEntities context = new DAL.Sybrid_DatabaseEntities();

                //var List = context.Setup_Company.Where(x => 1 == 2).OrderBy(x => x.CompanyName).Select(s => new
                //{
                //    Value = s.CompanyName,
                //    Id = s.CompanyId
                //}).ToList();

                //gvEmployees.DataSource = List;
                //gvEmployees.DataBind();

            }
        }
        catch (Exception ex)
        {

        }
    }
}