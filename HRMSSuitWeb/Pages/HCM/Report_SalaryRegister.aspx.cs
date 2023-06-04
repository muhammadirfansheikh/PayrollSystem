using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_HCM_Report_SalaryRegister : System.Web.UI.Page
{
    public string IsSummary
    {
        get
        {
            string IsSummary = "0";
            bool Check_id = Request.QueryString.ToString().Contains("IsSummary");
            if (Check_id == true)
            {
                try
                {
                    if (Request.QueryString["IsSummary"] == "1")
                    {
                        IsSummary = Request.QueryString["IsSummary"];
                        div_reportbutton.Attributes.Add("class", "col-lg-10 div_reportbutton");
                    }
                }
                catch
                {
                    IsSummary = "0";
                }
            }

            return IsSummary;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (IsSummary == "1")
            {
                hf_IsSummary.Value = "1";
                lbl1.Text = lbl2.Text = "Salary Register Summary Report";
            }
        }
    }
}