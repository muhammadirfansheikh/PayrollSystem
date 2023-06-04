using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_HRMS_Reports_HRReports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["reportname"])))
            {
                LoadReport(Convert.ToString(Request.QueryString["reportname"]));
            }

        }
    }
    private void LoadReport(string reportName)
    {
        try
        {
            string reportPathPrefix = System.Configuration.ConfigurationManager.AppSettings["ReportPathPrefix"].ToString();
            //string reportPath = "/" + reportPathPrefix + "/" + reportName;
            string reportPath = reportName;
            string reportMainPath = System.Configuration.ConfigurationManager.AppSettings["ReportPath"].ToString();
            ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            ReportViewer1.ServerReport.ReportServerUrl = new Uri(reportMainPath);

            ReportViewer1.ServerReport.ReportPath = reportPath;
            ReportViewer1.ServerReport.Refresh();
        }
        catch (Exception ex) { }
    }
}