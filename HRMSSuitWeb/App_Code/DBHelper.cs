using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
//===============================================================================
// Fixed Assets Register Functions Class
//===============================================================================
// 
//===============================================================================

using System.Web;
using System.ComponentModel;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Net.Mail;
using System.Net.Configuration;
using System.Web.Configuration;

using System.Data.SqlClient;
using System.Data.Common;
using System.Globalization;


using ListItem = System.Web.UI.WebControls.ListItem;

/// <summary>
/// Summary description for FA
/// </summary>
public class DBHelper
{
    #region Variables
    static Database defaultDB = null;
    #endregion
    public DBHelper()
    {

        defaultDB = DatabaseFactory.CreateDatabase("TKTDatabase");//EnterpriseLibraryContainer.Current.GetInstance<Database>();
    }
    [Description("Return rows using a SQL statement with no parameters")]
    public static DataTable getDataTable(string query)
    {
        //defaultDB = EnterpriseLibraryContainer.Current.GetInstance<Database>();
        // Call the ExecuteReader method by specifying the command type
        // as a SQL statement, and passing in the SQL statement
        using (IDataReader reader = defaultDB.ExecuteReader(CommandType.Text,query))
        {
            DataTable dt = new DataTable();
            dt.Load(reader);
            return dt;
        }
    }
    public static DataSet getDataSet(string query)
    {
        // Call the ExecuteReader method by specifying the command type
        // as a SQL statement, and passing in the SQL statement
       // defaultDB = EnterpriseLibraryContainer.Current.GetInstance<Database>();
        DataSet ds = defaultDB.ExecuteDataSet(CommandType.Text,query);
        return ds;

    }
    public static DataSet getDataSet(SqlCommand command)
    {
        // Call the ExecuteReader method by specifying the command type
        // as a SQL statement, and passing in the SQL statement
       // defaultDB = EnterpriseLibraryContainer.Current.GetInstance<Database>();
        DataSet ds = defaultDB.ExecuteDataSet(command);
        return ds;
       
    }
    public static DataTable getDataTable(SqlCommand command)
    {
        // Call the ExecuteReader method by specifying the command type
        // as a SQL statement, and passing in the SQL statement
       // defaultDB = EnterpriseLibraryContainer.Current.GetInstance<Database>();
        using (IDataReader reader = defaultDB.ExecuteReader(command))
        {
            DataTable dt = new DataTable();
            dt.Load(reader);
            return dt;
        }
    }


    //Only use this method when you have no straight forward way to set Combo Values by Usama.

    
    public static object ReadScalarValue(string query)
    {
        //defaultDB = EnterpriseLibraryContainer.Current.GetInstance<Database>();
        // Create a suitable command type for a SQL statement
        using (DbCommand sqlCmd = defaultDB.GetSqlStringCommand(query))
        {
            // Call the ExecuteScalar method of the command
            object obj = defaultDB.ExecuteScalar(sqlCmd);
            return obj == null ? "" : obj.ToString();          //defaultDB.ExecuteScalar(sqlCmd).ToString();
        }
        // Create a suitable command type for a stored procedure

    }
    public static object ReadScalarValue(SqlCommand command)
    {
        command.CommandTimeout = 60;
        //using (DbCommand sprocCmd = defaultDB.GetStoredProcCommand(command.CommandText))
        //{
            // Call the ExecuteScalar method of the command

        //    return defaultDB.ExecuteScalar(sprocCmd);
        //}
        //defaultDB = EnterpriseLibraryContainer.Current.GetInstance<Database>();
        return defaultDB.ExecuteScalar (command);
    }
    public static int insertData(SqlCommand command)
    {
        // Call the ExecuteReader method by specifying the command type
        // as a SQL statement, and passing in the SQL statement
       // defaultDB = EnterpriseLibraryContainer.Current.GetInstance<Database>();
       return  defaultDB.ExecuteNonQuery(command);
    }
    public static int insertData(string query)
    {
    defaultDB = DatabaseFactory.CreateDatabase("TKTDatabase");
        // Call the ExecuteReader method by specifying the command type
        // as a SQL statement, and passing in the SQL statement
       // defaultDB = EnterpriseLibraryContainer.Current.GetInstance<Database>();
        return defaultDB.ExecuteNonQuery(CommandType.Text,query);
    }

    public static int insertData(string query, SqlTransaction T, SqlConnection sql)
    {
        // Call the ExecuteReader method by specifying the command type
        // as a SQL statement, and passing in the SQL statement
        SqlCommand cmd = new SqlCommand();
        int no_of_record =  new SqlCommand(query, sql, T).ExecuteNonQuery();
        return no_of_record;
        //return defaultDB.ExecuteNonQuery(T,CommandType.Text, query);
    }

    //public static int insertData(string query,bool commit)  
    //{
    //    SqlConnection sql = new SqlConnection();
    //    sql.ConnectionString = ConfigurationManager.ConnectionStrings["SIMDatabase"].ConnectionString;
    //    SqlTransaction trans = sql.BeginTransaction();
    //    // Call the ExecuteReader method by specifying the command type
    //    // as a SQL statement, and passing in the SQL statement
    //    try
    //    {
    //        defaultDB = EnterpriseLibraryContainer.Current.GetInstance<Database>();
    //      int no_of_record =  defaultDB.ExecuteNonQuery(CommandType.Text, query);
    //        trans.Commit();
    //        return no_of_record;
    //    }
    //    catch (Exception ex)
    //    {
    //        trans.Rollback();
    //        throw ex.GetBaseException();
    //    }
    //}


    public static int approveRequestForTransfer(int assetId)
    {
        return 1;
    }
    public static int rejectRequestForTransfer(int assetId)
    {
        return 1;
    }



    public static void fillComboSelect(DropDownList cbo)
    {
        cbo.Items.Clear();
        cbo.Items.Add(new ListItem("--Select--", "0"));
    }
  

    public static void FillCombo(DropDownList cbo, string dataValueField, string dataTextField, string spName,
        bool includeSelect)
    {
        //var dbHelper = new DBHelper();
        cbo.Items.Clear();
        if (includeSelect)
            cbo.Items.Add(new ListItem("--Select--", "0"));
        cbo.AppendDataBoundItems = true;

        var cmd = new SqlCommand { CommandType = CommandType.StoredProcedure, CommandText = spName };

        var dt =  getDataTable(cmd);
        cbo.DataSource = dt;
        cbo.DataTextField = dataTextField;
        cbo.DataValueField = dataValueField;
        cbo.DataBind();
    }

    public static void fillCombo(DropDownList cbo, string dataValueField, string dataTextField, string query)
    {
        cbo.Items.Clear();
        cbo.Items.Add(new ListItem("--Select--", "0"));
        cbo.AppendDataBoundItems = true;

        DataTable dtEmp = DBHelper.getDataTable(query);
        cbo.DataSource = dtEmp;
        cbo.DataTextField = dataTextField;
        cbo.DataValueField = dataValueField;
        cbo.DataBind();
    }
    public static void fillCombo(DropDownList cbo, string dataValueField, string dataTextField, SqlCommand command)
    {
        cbo.Items.Clear();
        cbo.Items.Add(new ListItem("--Select--", "0"));
        cbo.AppendDataBoundItems = true;
        DataTable dtEmp = DBHelper.getDataTable(command);
        cbo.DataSource = dtEmp;
        cbo.DataTextField = dataTextField;
        cbo.DataValueField = dataValueField;
        cbo.DataBind();
    }
    public static void fillCombo(DropDownList cbo, string dataValueField, string dataTextField, DataTable data)
    {
        cbo.Items.Clear();
        cbo.Items.Add(new ListItem("--Select--", "0"));
        cbo.AppendDataBoundItems = true;
        cbo.DataSource = data;
        cbo.DataTextField = dataTextField;
        cbo.DataValueField = dataValueField;
        cbo.DataBind();
    }
    public static void fillCombo(DropDownList cbo, string dataValueField, string dataTextField, string query, bool includeSelect)
    {
        cbo.Items.Clear();
        if (includeSelect)
            cbo.Items.Add(new ListItem("--Select--", "0"));
        cbo.AppendDataBoundItems = true;

        DataTable dtEmp = DBHelper.getDataTable(query);
        cbo.DataSource = dtEmp;
        cbo.DataTextField = dataTextField;
        cbo.DataValueField = dataValueField;
        cbo.DataBind();
    }
    public static void fillCombo(DropDownList cbo, string dataValueField, string dataTextField, SqlCommand command, bool includeSelect)
    {
        cbo.Items.Clear();
        if (includeSelect)
            cbo.Items.Add(new ListItem("--Select--", "0"));
        cbo.AppendDataBoundItems = true;
        DataTable dtEmp = DBHelper.getDataTable(command);
        cbo.DataSource = dtEmp;
        cbo.DataTextField = dataTextField;
        cbo.DataValueField = dataValueField;
        cbo.DataBind();
    }



    public static void fillListBox(ListBox lst, string dataValueField, string dataTextField, string query)
    {
        lst.Items.Clear();

        DataTable dtEmp = DBHelper.getDataTable(query);
        lst.DataSource = dtEmp;
        lst.DataTextField = dataTextField;
        lst.DataValueField = dataValueField;
        lst.DataBind();
        if (lst.Items.Count > 0)
            lst.SelectedIndex = 0;
    }

    public static void fillListBox(ListBox lst, string dataValueField, string dataTextField, SqlCommand command)
    {
        lst.Items.Clear();

        DataTable dtEmp = DBHelper.getDataTable(command);
        lst.DataSource = dtEmp;
        lst.DataTextField = dataTextField;
        lst.DataValueField = dataValueField;
        lst.DataBind();
        if (lst.Items.Count > 0)
            lst.SelectedIndex = 0;
    }

    public static bool verifyDdlData(string tableName, string data, string columnName)
    {
        string query;
        if (data.ToString().Trim().Length == 0)
        {
            return true;
        }
        bool status = false;
            query = "select isActive from " + tableName + " where " + columnName + " = '" + data + "'";
            status =Convert.ToBoolean( DBHelper.ReadScalarValue(query));
            return status;
    }
    public static void fillMonth(DropDownList cbo)
    {
        cbo.Items.Clear();
        for (int i = 1; i <= 12; i++)
            cbo.Items.Add(new ListItem(new DateTime(DateTime.Now.Year, i, 1).ToString("MMMM"), i.ToString()));
        if (cbo.Items.FindByValue(DateTime.Now.Month.ToString()).Value.Length > 0)
            cbo.SelectedValue = cbo.Items.FindByValue(DateTime.Now.Month.ToString()).Value;
    }
    public static void fillYear(DropDownList cbo)
    {
        cbo.Items.Clear();
        for (int i = 2011; i <= DateTime.Now.Year + 10; i++)
            cbo.Items.Add(i.ToString());
        if (cbo.Items.FindByValue(DateTime.Now.Year.ToString()).Value.Length > 0)
            cbo.SelectedValue = cbo.Items.FindByValue(DateTime.Now.Year.ToString()).Value;
    }
    public static void fillStatus(DropDownList cbo)
    {

        cbo.Items.Insert(0, new ListItem("Active", "1"));
        cbo.Items.Insert(1, new ListItem("In Active", "0"));
        cbo.SelectedIndex = 0;
    }
    public static void fillDurationUnit(DropDownList cbo)
    {
        cbo.Items.Add("Hour(s)");
        cbo.Items.Add("Day(s)");
        cbo.Items.Add("Week(s)");
        cbo.Items.Add("Month(s)");
        cbo.Items.Add("Year(s)");
        cbo.SelectedIndex = 0;
    }

    public static bool sendMail(string to,string cc,string bcc,string subject,string message)
    {
        if (!Convert.ToBoolean(ConfigurationManager.AppSettings["OnUKServer"].ToString()))
        {
            bool bSend = false;
            Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
            SmtpClient Smtp = new SmtpClient();
            System.Net.NetworkCredential SmtpUser = new System.Net.NetworkCredential();
            if (!string.IsNullOrEmpty(settings.Smtp.Network.UserName))
                SmtpUser.UserName = settings.Smtp.Network.UserName;
            if (!string.IsNullOrEmpty(settings.Smtp.Network.Password))
                SmtpUser.Password = settings.Smtp.Network.Password;
            if (!string.IsNullOrEmpty(settings.Smtp.Network.Host))
                Smtp.Host = settings.Smtp.Network.Host;
            if (settings.Smtp.Network.Port > 0)
                Smtp.Port = settings.Smtp.Network.Port;
            MailMessage Mail = new MailMessage();
            MailAddress From = new MailAddress(settings.Smtp.From);
            //        MailAddress Cc = new MailAddress(cc);
            string MailBcc = "";
            if (bcc == "")
            {
                MailBcc = WebConfigurationManager.AppSettings["BCC"].ToString();
            }
            else
            {
                MailBcc = bcc;
            }

            MailAddress BCC = new MailAddress(MailBcc);
            Mail.From = From;
            Mail.To.Add(to);
            if (cc != "")
            {
                Mail.CC.Add(cc);
            }
            Mail.Bcc.Add(MailBcc);
            Mail.Subject = "SIM: " + subject;
            Mail.IsBodyHtml = true;
            Mail.Body = message;
            Smtp.Credentials = SmtpUser;
            Smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnabledSSL4Email"]); //false;     // true;
            try
            {
                //Debug.SetMailRecipients(Mail);
                Smtp.Send(Mail);
                bSend = true;
                return bSend;
            }
            catch (Exception ex)
            {
                return bSend;
                throw new Exception(ex.Message);
            }
        }

        return false;
    }
    public static bool sendMail(string to, string cc,string subject, string message)
    {
        if (!Convert.ToBoolean(ConfigurationManager.AppSettings["OnUKServer"].ToString()))
        {
            bool bSend = false;
            Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
            SmtpClient Smtp = new SmtpClient();
            System.Net.NetworkCredential SmtpUser = new System.Net.NetworkCredential();
            if (!string.IsNullOrEmpty(settings.Smtp.Network.UserName))
                SmtpUser.UserName = settings.Smtp.Network.UserName;
            if (!string.IsNullOrEmpty(settings.Smtp.Network.Password))
                SmtpUser.Password = settings.Smtp.Network.Password;
            if (!string.IsNullOrEmpty(settings.Smtp.Network.Host))
                Smtp.Host = settings.Smtp.Network.Host;
            if (settings.Smtp.Network.Port > 0)
                Smtp.Port = settings.Smtp.Network.Port;
            MailMessage Mail = new MailMessage();
            MailAddress From = new MailAddress(settings.Smtp.From);
            //        MailAddress Cc = new MailAddress(cc);
            string MailBcc = WebConfigurationManager.AppSettings["BCC"].ToString();
            MailAddress BCC = new MailAddress(MailBcc);
            Mail.From = From;
            Mail.To.Add(to);
            if (cc != "")
            {
                // here we have to use the string with all the addresses, 
                // if we create it with new MailAddress(cc) just gets the first one:
                Mail.CC.Add(cc);
            }
            Mail.Bcc.Add(MailBcc);
            Mail.Subject = "SIM: " + subject;
            Mail.IsBodyHtml = true;
            //Mail.Body = message;
            Smtp.Credentials = SmtpUser;

            //Embed upto 10 images in email
            List<LinkedResource> resourceList = new List<LinkedResource>();
            LinkedResource resource = null;

            string imgPath;

            for (int imgId = 1; imgId <= 10; imgId++)
            {
                if (message.Contains("#$" + imgId))
                {
                    imgPath = message.Substring(message.IndexOf("#$" + imgId), message.LastIndexOf("#$" + imgId) - message.IndexOf("#$" + imgId)).Trim();
                    imgPath = imgPath.Replace("#$" + imgId, "");

                    resource = new LinkedResource(HttpContext.Current.Server.MapPath(imgPath));
                    resource.ContentId = "mailimage" + imgId;
                    resourceList.Add(resource);
                    message = message.Replace("#$" + imgId + imgPath + "#$" + imgId, "cid:" + resource.ContentId);
                }
            }

            AlternateView view = AlternateView.CreateAlternateViewFromString(message, null, System.Net.Mime.MediaTypeNames.Text.Html);

            foreach (LinkedResource linkResource in resourceList)
            {
                view.LinkedResources.Add(linkResource);
            }

            Mail.AlternateViews.Add(view);
            //End

            Mail.Body = message;

            Smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnabledSSL4Email"]); //false;     // true;
            try
            {
                //Debug.SetMailRecipients(Mail);
                Smtp.Send(Mail);
                bSend = true;
                return bSend;
            }

            catch (Exception ex)
            {
                return bSend;
                throw new Exception(ex.Message);
            }
        }

        return false;
    }



    
    public static bool sendMail(string to, string subject, string message)
    {
        if(!Convert.ToBoolean(ConfigurationManager.AppSettings["OnUKServer"].ToString()))
        {
            bool bSend = false;
            //Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath.Replace("SIM","").ToString());
            Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            //Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);

            //MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
            //Configuration config = WebConfigurationManager.OpenWebConfiguration("");
            MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
            SmtpClient Smtp = new SmtpClient();
            System.Net.NetworkCredential SmtpUser = new System.Net.NetworkCredential();

            if (!string.IsNullOrEmpty(settings.Smtp.Network.UserName))
                SmtpUser.UserName = settings.Smtp.Network.UserName;
            if (!string.IsNullOrEmpty(settings.Smtp.Network.Password))
                SmtpUser.Password = settings.Smtp.Network.Password;
            if (!string.IsNullOrEmpty(settings.Smtp.Network.Host))
                Smtp.Host = settings.Smtp.Network.Host;
            if (settings.Smtp.Network.Port > 0)
                Smtp.Port = settings.Smtp.Network.Port;

            MailMessage Mail = new MailMessage();
            MailAddress From = new MailAddress(settings.Smtp.From);
            string MailBcc = WebConfigurationManager.AppSettings["BCC"].ToString();
            MailAddress BCC = new MailAddress(MailBcc);
            Mail.From = From;
            Mail.To.Add(to);
            Mail.Bcc.Add(MailBcc);
            Mail.Subject = "SIM: " + subject;
            //Mail.Body = message;
            Mail.IsBodyHtml = true;
            Smtp.Credentials = SmtpUser;

            if (message.Contains("#$"))
            {
                //Embed upto 10 images in email
                List<LinkedResource> resourceList = new List<LinkedResource>();
                LinkedResource resource = null;

                string imgPath;

                for (int imgId = 1; imgId <= 10; imgId++)
                {
                    if (message.Contains("#$" + imgId))
                    {
                        imgPath = message.Substring(message.IndexOf("#$" + imgId), message.LastIndexOf("#$" + imgId) - message.IndexOf("#$" + imgId)).Trim();
                        imgPath = imgPath.Replace("#$" + imgId, "");

                        resource = new LinkedResource(HttpContext.Current.Server.MapPath(imgPath));
                        resource.ContentId = "mailimage" + imgId;
                        resourceList.Add(resource);
                        message = message.Replace("#$" + imgId + imgPath + "#$" + imgId, "cid:" + resource.ContentId);
                    }
                }

                AlternateView view = AlternateView.CreateAlternateViewFromString(message, null, System.Net.Mime.MediaTypeNames.Text.Html);

                foreach (LinkedResource linkResource in resourceList)
                {
                    view.LinkedResources.Add(linkResource);
                }

                Mail.AlternateViews.Add(view);
                //End
            }
            
            Mail.Body = message;
          
            Smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnabledSSL4Email"]); //false;     // true;
            try
            {
                //Debug.SetMailRecipients(Mail);
                Smtp.Send(Mail);
                bSend = true;
                return bSend;
            }

            catch (Exception ex)
            {
               // DBHelper.logError("DBHelper.cs/sendMail", ex.Message.ToString());
                return bSend;
                throw new Exception(ex.Message);
            }
        }

        return false;
    }
    
    public static bool sendMail(string to, string bcc, string subject, string message,float dumy) // float is a dummy variable
    {
        if (!Convert.ToBoolean(ConfigurationManager.AppSettings["OnUKServer"].ToString()))
        {
            bool bSend = false;
            //Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath.Replace("SIM","").ToString());
            Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            //Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);

            //MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
            //Configuration config = WebConfigurationManager.OpenWebConfiguration("");
            MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
            SmtpClient Smtp = new SmtpClient();
            System.Net.NetworkCredential SmtpUser = new System.Net.NetworkCredential();

            if (!string.IsNullOrEmpty(settings.Smtp.Network.UserName))
                SmtpUser.UserName = settings.Smtp.Network.UserName;
            if (!string.IsNullOrEmpty(settings.Smtp.Network.Password))
                SmtpUser.Password = settings.Smtp.Network.Password;
            if (!string.IsNullOrEmpty(settings.Smtp.Network.Host))
                Smtp.Host = settings.Smtp.Network.Host;
            if (settings.Smtp.Network.Port > 0)
                Smtp.Port = settings.Smtp.Network.Port;
            MailMessage Mail = new MailMessage();
            MailAddress From = new MailAddress(settings.Smtp.From);

            string MailBcc = WebConfigurationManager.AppSettings["BCC"].ToString();

            MailAddress BCC = new MailAddress(MailBcc);

            Mail.From = From;
            Mail.To.Add(to);
            Mail.Bcc.Add(MailBcc);
            Mail.Bcc.Add(bcc);
            Mail.Subject = "SIM: " + subject;
            Mail.IsBodyHtml = true;
            //Embed upto 10 images in email
            List<LinkedResource> resourceList = new List<LinkedResource>();
            LinkedResource resource = null;

            string imgPath;

            for (int imgId = 1; imgId <= 10; imgId++)
            {
                if (message.Contains("#$" + imgId))
                {
                    imgPath = message.Substring(message.IndexOf("#$" + imgId), message.LastIndexOf("#$" + imgId) - message.IndexOf("#$" + imgId)).Trim();
                    imgPath = imgPath.Replace("#$" + imgId, "");

                    resource = new LinkedResource(HttpContext.Current.Server.MapPath(imgPath));
                    resource.ContentId = "mailimage" + imgId;
                    resourceList.Add(resource);
                    message = message.Replace("#$" + imgId + imgPath + "#$" + imgId, "cid:" + resource.ContentId);
                }
            }

            AlternateView view = AlternateView.CreateAlternateViewFromString(message, null, System.Net.Mime.MediaTypeNames.Text.Html);

            foreach (LinkedResource linkResource in resourceList)
            {
                view.LinkedResources.Add(linkResource);
            }

            Mail.AlternateViews.Add(view);
            Mail.Body = message;
            Smtp.Credentials = SmtpUser;
            Smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnabledSSL4Email"]); //false;     // true;
            try
            {
                //Debug.SetMailRecipients(Mail);
                Smtp.Send(Mail);
                bSend = true;
                return bSend;
            }

            catch (Exception ex)
            {
                return bSend;
                throw new Exception(ex.Message);
            }
        }

        return false;
    }
    public static bool sendMail(string to, string cc, string bcc, string subject, string message, float dumy) // float is a dummy variable
    {
        if (!Convert.ToBoolean(ConfigurationManager.AppSettings["OnUKServer"].ToString()))
        {
            bool bSend = false;
            //Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath.Replace("SIM","").ToString());
            Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            //Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);

            //MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
            //Configuration config = WebConfigurationManager.OpenWebConfiguration("");
            MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
            SmtpClient Smtp = new SmtpClient();
            System.Net.NetworkCredential SmtpUser = new System.Net.NetworkCredential();

            if (!string.IsNullOrEmpty(settings.Smtp.Network.UserName))
                SmtpUser.UserName = settings.Smtp.Network.UserName;
            if (!string.IsNullOrEmpty(settings.Smtp.Network.Password))
                SmtpUser.Password = settings.Smtp.Network.Password;
            if (!string.IsNullOrEmpty(settings.Smtp.Network.Host))
                Smtp.Host = settings.Smtp.Network.Host;
            if (settings.Smtp.Network.Port > 0)
                Smtp.Port = settings.Smtp.Network.Port;
            MailMessage Mail = new MailMessage();
            MailAddress From = new MailAddress(settings.Smtp.From);

            string MailBcc = WebConfigurationManager.AppSettings["BCC"].ToString();

            MailAddress BCC = new MailAddress(MailBcc);

            Mail.From = From;
            Mail.To.Add(to);
            Mail.Bcc.Add(MailBcc);
            Mail.Bcc.Add(bcc);
            Mail.CC.Add(cc);
            Mail.Subject = "SIM: " + subject;
            Mail.IsBodyHtml = true;
            //Embed upto 10 images in email
            List<LinkedResource> resourceList = new List<LinkedResource>();
            LinkedResource resource = null;

            string imgPath;

            for (int imgId = 1; imgId <= 10; imgId++)
            {
                if (message.Contains("#$" + imgId))
                {
                    imgPath = message.Substring(message.IndexOf("#$" + imgId), message.LastIndexOf("#$" + imgId) - message.IndexOf("#$" + imgId)).Trim();
                    imgPath = imgPath.Replace("#$" + imgId, "");

                    resource = new LinkedResource(HttpContext.Current.Server.MapPath(imgPath));
                    resource.ContentId = "mailimage" + imgId;
                    resourceList.Add(resource);
                    message = message.Replace("#$" + imgId + imgPath + "#$" + imgId, "cid:" + resource.ContentId);
                }
            }

            AlternateView view = AlternateView.CreateAlternateViewFromString(message, null, System.Net.Mime.MediaTypeNames.Text.Html);

            foreach (LinkedResource linkResource in resourceList)
            {
                view.LinkedResources.Add(linkResource);
            }

            Mail.AlternateViews.Add(view);
            Mail.Body = message;
            Smtp.Credentials = SmtpUser;
            Smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnabledSSL4Email"]); //false;     // true;
            try
            {
                //Debug.SetMailRecipients(Mail);
                Smtp.Send(Mail);
                bSend = true;
                return bSend;
            }

            catch (Exception ex)
            {
                return bSend;
                throw new Exception(ex.Message);
            }
        }

        return false;
    }
    public static bool sendMail(string to, string subject, string message, MailPriority priority)
    {
        if (!Convert.ToBoolean(ConfigurationManager.AppSettings["OnUKServer"].ToString()))
        {
            bool bSend = false;
            //Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath.Replace("SIM","").ToString());
            Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            //Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);

            //MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
            //Configuration config = WebConfigurationManager.OpenWebConfiguration("");
            MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
            SmtpClient Smtp = new SmtpClient();
            System.Net.NetworkCredential SmtpUser = new System.Net.NetworkCredential();

            if (!string.IsNullOrEmpty(settings.Smtp.Network.UserName))
                SmtpUser.UserName = settings.Smtp.Network.UserName;
            if (!string.IsNullOrEmpty(settings.Smtp.Network.Password))
                SmtpUser.Password = settings.Smtp.Network.Password;
            if (!string.IsNullOrEmpty(settings.Smtp.Network.Host))
                Smtp.Host = settings.Smtp.Network.Host;
            if (settings.Smtp.Network.Port > 0)
                Smtp.Port = settings.Smtp.Network.Port;
            MailMessage Mail = new MailMessage();
            MailAddress From = new MailAddress(settings.Smtp.From);
            string MailBcc = WebConfigurationManager.AppSettings["BCC"].ToString();
            MailAddress BCC = new MailAddress(MailBcc);
            Mail.From = From;
            Mail.To.Add(to);
            Mail.Bcc.Add(MailBcc);
            Mail.Bcc.Add("zeeshan.raees@naushtech.com");
            Mail.Subject = "SIM: " + subject;
            Mail.IsBodyHtml = true;
            Mail.Body = message;
            Mail.Priority = priority;
            Smtp.Credentials = SmtpUser;
            Smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnabledSSL4Email"]); //false;     // true;
            try
            {
                //Debug.SetMailRecipients(Mail);
                Smtp.Send(Mail);
                bSend = true;
                return bSend;
            }

            catch (Exception ex)
            {
                return bSend;
                throw new Exception(ex.Message);
            }
        }

        return false;
    }
    public static bool sendMail(string to,string bcc, string subject, string message, bool isAttachment, string attachmentPath)
    {
        if (!Convert.ToBoolean(ConfigurationManager.AppSettings["OnUKServer"].ToString()))
        {
            bool bSend = false;
            //Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath.Replace("SIM","").ToString());
            Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            //Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);

            //MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
            //Configuration config = WebConfigurationManager.OpenWebConfiguration("");
            MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
            SmtpClient Smtp = new SmtpClient();
            System.Net.NetworkCredential SmtpUser = new System.Net.NetworkCredential();

            if (!string.IsNullOrEmpty(settings.Smtp.Network.UserName))
                SmtpUser.UserName = settings.Smtp.Network.UserName;
            if (!string.IsNullOrEmpty(settings.Smtp.Network.Password))
                SmtpUser.Password = settings.Smtp.Network.Password;
            if (!string.IsNullOrEmpty(settings.Smtp.Network.Host))
                Smtp.Host = settings.Smtp.Network.Host;
            if (settings.Smtp.Network.Port > 0)
                Smtp.Port = settings.Smtp.Network.Port;
            MailMessage Mail = new MailMessage();
            MailAddress From = new MailAddress(settings.Smtp.From);
            string MailBcc = "";
            
            MailBcc = WebConfigurationManager.AppSettings["BCC"].ToString();
           

            MailAddress BCC = new MailAddress(MailBcc);
            Mail.From = From;
            Mail.To.Add(to);
            Mail.Bcc.Add(MailBcc);
            if (bcc != "")
            Mail.Bcc.Add(bcc);
            Mail.Subject = "SIM: " + subject;
            Mail.IsBodyHtml = true;
            Mail.Body = message;
            Mail.Attachments.Add(new Attachment(attachmentPath));
            Smtp.Credentials = SmtpUser;
            Smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnabledSSL4Email"]); //false;     // true;
            try
            {
                //Debug.SetMailRecipients(Mail);
                Smtp.Send(Mail);
                Mail.Attachments.Dispose();
                Mail.Dispose();
                bSend = true;
                return bSend;
            }

            catch (Exception ex)
            {
                return bSend;
                throw new Exception(ex.Message);
            }
        }

        return false;
    }

    public static bool sendMail(string to, string cc, string bcc, string subject, string message, bool isAttachment, string attachmentPath)
    {
        if (!Convert.ToBoolean(ConfigurationManager.AppSettings["OnUKServer"].ToString()))
        {
            bool bSend = false;
            //Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath.Replace("SIM","").ToString());
            Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            //Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);

            //MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
            //Configuration config = WebConfigurationManager.OpenWebConfiguration("");
            MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
            SmtpClient Smtp = new SmtpClient();
            System.Net.NetworkCredential SmtpUser = new System.Net.NetworkCredential();

            if (!string.IsNullOrEmpty(settings.Smtp.Network.UserName))
                SmtpUser.UserName = settings.Smtp.Network.UserName;
            if (!string.IsNullOrEmpty(settings.Smtp.Network.Password))
                SmtpUser.Password = settings.Smtp.Network.Password;
            if (!string.IsNullOrEmpty(settings.Smtp.Network.Host))
                Smtp.Host = settings.Smtp.Network.Host;
            if (settings.Smtp.Network.Port > 0)
                Smtp.Port = settings.Smtp.Network.Port;
            MailMessage Mail = new MailMessage();
            MailAddress From = new MailAddress(settings.Smtp.From);
            string MailBcc = "";
            if (bcc == "")
                MailBcc = WebConfigurationManager.AppSettings["BCC"].ToString();
            else
                MailBcc = WebConfigurationManager.AppSettings["BCC"].ToString() + "," + bcc;

            MailAddress BCC = new MailAddress(MailBcc);
            Mail.From = From;
            Mail.To.Add(to);
            Mail.Bcc.Add(MailBcc);
            Mail.CC.Add(cc);
            Mail.Subject = "SIM: " + subject;
            Mail.IsBodyHtml = true;
            Mail.Body = message;
            Mail.Attachments.Add(new Attachment(attachmentPath));
            Smtp.Credentials = SmtpUser;
            Smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnabledSSL4Email"]); //false;     // true;
            try
            {
                //Debug.SetMailRecipients(Mail);
                Smtp.Send(Mail);
                Mail.Attachments.Dispose();
                Mail.Dispose();
                bSend = true;
                return bSend;
            }

            catch (Exception ex)
            {
                return bSend;
                throw new Exception(ex.Message);
            }
        }

        return false;
    }

    //Daily Production Mail (ALI)

    public static bool DailyProductionsendMail(string to, string subject, string message, bool isAttachment, string[] attachmentPaths)
    {
        if (!Convert.ToBoolean(ConfigurationManager.AppSettings["OnUKServer"].ToString()))
        {
            bool bSend = false;
            //Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath.Replace("SIM","").ToString());
            Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            //Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);

            //MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
            //Configuration config = WebConfigurationManager.OpenWebConfiguration("");
            MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
            SmtpClient Smtp = new SmtpClient();
            System.Net.NetworkCredential SmtpUser = new System.Net.NetworkCredential();

            if (!string.IsNullOrEmpty(settings.Smtp.Network.UserName))
                SmtpUser.UserName = settings.Smtp.Network.UserName;
            if (!string.IsNullOrEmpty(settings.Smtp.Network.Password))
                SmtpUser.Password = settings.Smtp.Network.Password;
            if (!string.IsNullOrEmpty(settings.Smtp.Network.Host))
                Smtp.Host = settings.Smtp.Network.Host;
            if (settings.Smtp.Network.Port > 0)
                Smtp.Port = settings.Smtp.Network.Port;
            MailMessage Mail = new MailMessage();
            MailAddress From = new MailAddress(settings.Smtp.From);
            string MailBcc = WebConfigurationManager.AppSettings["BCC"].ToString();
            MailAddress BCC = new MailAddress(MailBcc);
            Mail.From = From;
            Mail.To.Add(to);
            Mail.Bcc.Add(MailBcc);
            Mail.Subject = "SIM: " + subject;
            Mail.IsBodyHtml = true;



            //Embed upto 10 images in email
            List<LinkedResource> resourceList = new List<LinkedResource>();
            LinkedResource resource = null;

            string imgPath;

            for (int imgId = 1; imgId <= 10; imgId++)
            {
                if (message.Contains("#$" + imgId))
                {
                    imgPath = message.Substring(message.IndexOf("#$" + imgId), message.LastIndexOf("#$" + imgId) - message.IndexOf("#$" + imgId)).Trim();
                    imgPath = imgPath.Replace("#$" + imgId, "");

                    resource = new LinkedResource(HttpContext.Current.Server.MapPath(imgPath));
                    resource.ContentId = "mailimage" + imgId;
                    resourceList.Add(resource);
                    message = message.Replace("#$" + imgId + imgPath + "#$" + imgId, "cid:" + resource.ContentId);
                }
            }

            AlternateView view = AlternateView.CreateAlternateViewFromString(message, null, System.Net.Mime.MediaTypeNames.Text.Html);

            foreach (LinkedResource linkResource in resourceList)
            {
                view.LinkedResources.Add(linkResource);
            }

            Mail.AlternateViews.Add(view);
            //End



            Mail.Body = message;
            foreach (string attachment in attachmentPaths)
                Mail.Attachments.Add(new Attachment(attachment));
            Smtp.Credentials = SmtpUser;
            Smtp.Timeout = Int32.MaxValue;

            Smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnabledSSL4Email"]); //false;     // true;
            try
            {
                //Debug.SetMailRecipients(Mail);
                Smtp.Send(Mail);
                //Smtp.SendCompleted += new
                //SendCompletedEventHandler(SendCompletedCallback);
                //string userState = "message with attachment(s).";
                //Smtp.SendAsync(Mail, userState);
                Mail.Attachments.Dispose();
                Mail.Dispose();
                bSend = true;
                return bSend;
            }

            catch (Exception ex)
            {
               // logError("DBHelper.cs/sendMail with attachment Array.", ex.Message);
                return bSend;
            }
        }

        return false;
    }
    //Daily Production Mail (ALI)

    public static bool sendOrdersAndMemosReminderMail(string to, string subject, string message, MailPriority priority)
    {
        if (!Convert.ToBoolean(ConfigurationManager.AppSettings["OnUKServer"].ToString()))
        {
            bool bSend = false;
            //Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath.Replace("SIM","").ToString());
            Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            //Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);

            //MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
            //Configuration config = WebConfigurationManager.OpenWebConfiguration("");
            MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
            SmtpClient Smtp = new SmtpClient();
            System.Net.NetworkCredential SmtpUser = new System.Net.NetworkCredential();

            if (!string.IsNullOrEmpty(settings.Smtp.Network.UserName))
                SmtpUser.UserName = settings.Smtp.Network.UserName;
            if (!string.IsNullOrEmpty(settings.Smtp.Network.Password))
                SmtpUser.Password = settings.Smtp.Network.Password;
            if (!string.IsNullOrEmpty(settings.Smtp.Network.Host))
                Smtp.Host = settings.Smtp.Network.Host;
            if (settings.Smtp.Network.Port > 0)
                Smtp.Port = settings.Smtp.Network.Port;
            MailMessage Mail = new MailMessage();
            MailAddress From = new MailAddress(settings.Smtp.From);
            string MailBcc = WebConfigurationManager.AppSettings["BCC"].ToString();
            MailAddress BCC = new MailAddress(MailBcc);
            Mail.From = From;
            Mail.To.Add(to);
            Mail.Bcc.Add(MailBcc);
            Mail.Bcc.Add("zeeshan.raees@naushtech.com");
            Mail.Subject = "SIM: " + subject;
            Mail.IsBodyHtml = true;
            //Mail.Body = message;
            Mail.Priority = priority;
            Smtp.Credentials = SmtpUser;

            //Embed images in email
            List<LinkedResource> resourceList = new List<LinkedResource>();
            LinkedResource resource = null;

            string imgPath;

            for (int imgId = 11; imgId <= 99; imgId++)
            {
                if (message.Contains("#$" + imgId))
                {
                    imgPath = message.Substring(message.IndexOf("#$" + imgId), message.LastIndexOf("#$" + imgId) - message.IndexOf("#$" + imgId)).Trim();
                    imgPath = imgPath.Replace("#$" + imgId, "");

                    resource = new LinkedResource(HttpContext.Current.Server.MapPath(imgPath));
                    resource.ContentId = "mailimage" + imgId;
                    resourceList.Add(resource);
                    message = message.Replace("#$" + imgId + imgPath + "#$" + imgId, "cid:" + resource.ContentId);
                }
            }

            AlternateView view = AlternateView.CreateAlternateViewFromString(message, null, System.Net.Mime.MediaTypeNames.Text.Html);

            foreach (LinkedResource linkResource in resourceList)
            {
                view.LinkedResources.Add(linkResource);
            }

            Mail.AlternateViews.Add(view);
            //End

            Smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnabledSSL4Email"]); //false;     // true;
            try
            {
                //Debug.SetMailRecipients(Mail);
                Smtp.Send(Mail);
                bSend = true;
                return bSend;
            }

            catch (Exception ex)
            {
                return bSend;
                throw new Exception(ex.Message);
            }
        }

        return false;
    }


    public static bool sendMail(string to, string subject, string message, bool isAttachment, string[] attachmentPaths)
    {
        if (!Convert.ToBoolean(ConfigurationManager.AppSettings["OnUKServer"].ToString()))
        {
            bool bSend = false;
            //Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath.Replace("SIM","").ToString());
            Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            //Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);

            //MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
            //Configuration config = WebConfigurationManager.OpenWebConfiguration("");
            MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
            SmtpClient Smtp = new SmtpClient();
            System.Net.NetworkCredential SmtpUser = new System.Net.NetworkCredential();

            if (!string.IsNullOrEmpty(settings.Smtp.Network.UserName))
                SmtpUser.UserName = settings.Smtp.Network.UserName;
            if (!string.IsNullOrEmpty(settings.Smtp.Network.Password))
                SmtpUser.Password = settings.Smtp.Network.Password;
            if (!string.IsNullOrEmpty(settings.Smtp.Network.Host))
                Smtp.Host = settings.Smtp.Network.Host;
            if (settings.Smtp.Network.Port > 0)
                Smtp.Port = settings.Smtp.Network.Port;
            MailMessage Mail = new MailMessage();
            MailAddress From = new MailAddress(settings.Smtp.From);
            string MailBcc = WebConfigurationManager.AppSettings["BCC"].ToString();
            MailAddress BCC = new MailAddress(MailBcc);
            Mail.From = From;
            Mail.To.Add(to);
            Mail.Bcc.Add(MailBcc);
            Mail.Subject = "SIM: " + subject;
            Mail.IsBodyHtml = true;
            //Embed upto 10 images in email
            List<LinkedResource> resourceList = new List<LinkedResource>();
            LinkedResource resource = null;

            string imgPath;

            for (int imgId = 1; imgId <= 10; imgId++)
            {
                if (message.Contains("#$" + imgId))
                {
                    imgPath = message.Substring(message.IndexOf("#$" + imgId), message.LastIndexOf("#$" + imgId) - message.IndexOf("#$" + imgId)).Trim();
                    imgPath = imgPath.Replace("#$" + imgId, "");

                    resource = new LinkedResource(HttpContext.Current.Server.MapPath(imgPath));
                    resource.ContentId = "mailimage" + imgId;
                    resourceList.Add(resource);
                    message = message.Replace("#$" + imgId + imgPath + "#$" + imgId, "cid:" + resource.ContentId);
                }
            }

            AlternateView view = AlternateView.CreateAlternateViewFromString(message, null, System.Net.Mime.MediaTypeNames.Text.Html);

            foreach (LinkedResource linkResource in resourceList)
            {
                view.LinkedResources.Add(linkResource);
            }

            Mail.AlternateViews.Add(view);
            Mail.Body = message;
            foreach (string attachment in attachmentPaths)
                Mail.Attachments.Add(new Attachment(attachment));
            Smtp.Credentials = SmtpUser;
            Smtp.Timeout = Int32.MaxValue;
            Smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnabledSSL4Email"]); //false;     // true;
            try
            {
                //Debug.SetMailRecipients(Mail);
                Smtp.Send(Mail);
                //Smtp.SendCompleted += new
                //SendCompletedEventHandler(SendCompletedCallback);
                //string userState = "message with attachment(s).";
                //Smtp.SendAsync(Mail, userState);
                Mail.Attachments.Dispose();
                Mail.Dispose();
                bSend = true;
                return bSend;
            }

            catch (Exception ex)
            {
                //logError("DBHelper.cs/sendMail with attachment Array.", ex.Message);
                return bSend;
            }
        }

        return false;
    }

    public static bool sendMail(string to, string bcc, string subject, string message, bool isAttachment, string[] attachmentPaths)
    {
        if (!Convert.ToBoolean(ConfigurationManager.AppSettings["OnUKServer"].ToString()))
        {
            bool bSend = false;
            //Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath.Replace("SIM","").ToString());
            Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            //Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);

            //MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
            //Configuration config = WebConfigurationManager.OpenWebConfiguration("");
            MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
            SmtpClient Smtp = new SmtpClient();
            System.Net.NetworkCredential SmtpUser = new System.Net.NetworkCredential();

            if (!string.IsNullOrEmpty(settings.Smtp.Network.UserName))
                SmtpUser.UserName = settings.Smtp.Network.UserName;
            if (!string.IsNullOrEmpty(settings.Smtp.Network.Password))
                SmtpUser.Password = settings.Smtp.Network.Password;
            if (!string.IsNullOrEmpty(settings.Smtp.Network.Host))
                Smtp.Host = settings.Smtp.Network.Host;
            if (settings.Smtp.Network.Port > 0)
                Smtp.Port = settings.Smtp.Network.Port;
            MailMessage Mail = new MailMessage();
            MailAddress From = new MailAddress(settings.Smtp.From);

            string MailBcc = "";
            if (bcc == "")
                MailBcc = WebConfigurationManager.AppSettings["BCC"].ToString();
            else
                MailBcc = WebConfigurationManager.AppSettings["BCC"].ToString() + "," + bcc;

            MailAddress BCC = new MailAddress(MailBcc);
            Mail.From = From;
            Mail.To.Add(to);
            Mail.Bcc.Add(MailBcc);
            Mail.Subject = "SIM: " + subject;
            Mail.IsBodyHtml = true;
            Mail.Body = message;
            foreach (string attachment in attachmentPaths)
                Mail.Attachments.Add(new Attachment(attachment));
            Smtp.Credentials = SmtpUser;
            Smtp.Timeout = Int32.MaxValue;
            Smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnabledSSL4Email"]);
            try
            {
                //Debug.SetMailRecipients(Mail);
                Smtp.Send(Mail);
                //Smtp.SendCompleted += new
                //SendCompletedEventHandler(SendCompletedCallback);
                //string userState = "message with attachment(s).";
                //Smtp.SendAsync(Mail, userState);
                Mail.Attachments.Dispose();
                Mail.Dispose();
                bSend = true;
                return bSend;
            }

            catch (Exception ex)
            {
                //logError("DBHelper.cs/sendMail with attachment Array.", ex.Message);
                return bSend;
            }
        }

        return false;
    }

    public static bool sendMailImage(string to, string subject, string message, string FileName)
    {
        if (!Convert.ToBoolean(ConfigurationManager.AppSettings["OnUKServer"].ToString()))
        {
            bool bSend = false;
            //Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath.Replace("SIM","").ToString());
            Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            //Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);

            //MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
            //Configuration config = WebConfigurationManager.OpenWebConfiguration("");
            MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
            SmtpClient Smtp = new SmtpClient();
            System.Net.NetworkCredential SmtpUser = new System.Net.NetworkCredential();

            if (!string.IsNullOrEmpty(settings.Smtp.Network.UserName))
                SmtpUser.UserName = settings.Smtp.Network.UserName;
            if (!string.IsNullOrEmpty(settings.Smtp.Network.Password))
                SmtpUser.Password = settings.Smtp.Network.Password;
            if (!string.IsNullOrEmpty(settings.Smtp.Network.Host))
                Smtp.Host = settings.Smtp.Network.Host;
            if (settings.Smtp.Network.Port > 0)
                Smtp.Port = settings.Smtp.Network.Port;
            MailMessage Mail = new MailMessage();
            MailAddress From = new MailAddress(settings.Smtp.From);
            string MailBcc = WebConfigurationManager.AppSettings["BCC"].ToString();
            MailAddress BCC = new MailAddress(MailBcc);
            Mail.From = From;
            Mail.To.Add(to);
            Mail.Bcc.Add(MailBcc);
            Mail.Subject = "SIM: " + subject;
            Mail.IsBodyHtml = true;
            Mail.Body = message;
            LinkedResource myimage = new LinkedResource(FileName);
            // Create HTML view
            AlternateView htmlMail = AlternateView.CreateAlternateViewFromString(Mail.Body, null, "text/html");
            // Set ContentId property. Value of ContentId property must be the same as
            // the src attribute of image tag in email body. 
            myimage.ContentId = "companylogo";
            htmlMail.LinkedResources.Add(myimage);
            Mail.AlternateViews.Add(htmlMail);

            Smtp.Credentials = SmtpUser;
            Smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnabledSSL4Email"]); //false;     // true;
            try
            {
                //Debug.SetMailRecipients(Mail);
                Smtp.Send(Mail);
                bSend = true;
                return bSend;
            }

            catch (Exception ex)
            {
                return bSend;
                throw new Exception(ex.Message);
            }
        }

        return false;
    }

    public static bool sendMail(string to, string bcc, string subject, string message, bool isAttachment, string attachmentPath, string FileName)
    {
        if (!Convert.ToBoolean(ConfigurationManager.AppSettings["OnUKServer"].ToString()))
        {
            bool bSend = false;
            //Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath.Replace("SIM","").ToString());
            Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            //Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);

            //MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
            //Configuration config = WebConfigurationManager.OpenWebConfiguration("");
            MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
            SmtpClient Smtp = new SmtpClient();
            System.Net.NetworkCredential SmtpUser = new System.Net.NetworkCredential();

            if (!string.IsNullOrEmpty(settings.Smtp.Network.UserName))
                SmtpUser.UserName = settings.Smtp.Network.UserName;
            if (!string.IsNullOrEmpty(settings.Smtp.Network.Password))
                SmtpUser.Password = settings.Smtp.Network.Password;
            if (!string.IsNullOrEmpty(settings.Smtp.Network.Host))
                Smtp.Host = settings.Smtp.Network.Host;
            if (settings.Smtp.Network.Port > 0)
                Smtp.Port = settings.Smtp.Network.Port;
            MailMessage Mail = new MailMessage();
            MailAddress From = new MailAddress(settings.Smtp.From);
            string MailBcc = "";
            if (bcc == "")
                MailBcc = WebConfigurationManager.AppSettings["BCC"].ToString();
            else
                MailBcc = WebConfigurationManager.AppSettings["BCC"].ToString() + "," + bcc;

            MailAddress BCC = new MailAddress(MailBcc);
            Mail.From = From;
            Mail.To.Add(to);
            Mail.Bcc.Add(MailBcc);
            Mail.Subject = "SIM: " + subject;
            Mail.IsBodyHtml = true;
            Mail.Body = message;
            Mail.Attachments.Add(new Attachment(attachmentPath));
            LinkedResource myimage = new LinkedResource(FileName);
            // Create HTML view
            AlternateView htmlMail = AlternateView.CreateAlternateViewFromString(Mail.Body, null, "text/html");
            // Set ContentId property. Value of ContentId property must be the same as
            // the src attribute of image tag in email body. 
            myimage.ContentId = "companylogo";
            htmlMail.LinkedResources.Add(myimage);
            Mail.AlternateViews.Add(htmlMail);
            Smtp.Credentials = SmtpUser;
            Smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnabledSSL4Email"]); //false;     // true;
            try
            {
                //Debug.SetMailRecipients(Mail);
                Smtp.Send(Mail);
                Mail.Attachments.Dispose();
                Mail.Dispose();
                bSend = true;
                return bSend;
            }

            catch (Exception ex)
            {
                return bSend;
                throw new Exception(ex.Message);
            }
        }

        return false;
    
    }

    //private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
    //{
    //    // Get the unique identifier for this asynchronous operation.
    //    String token = (string)e.UserState;

    //    //if (e.Cancelled)
    //    //{
    //    //    Console.WriteLine("[{0}] Send canceled.", token);
    //    //}
    //    //if (e.Error != null)
    //    //{
    //    //    Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
    //    //}
    //    //else
    //    //{
    //    //    Console.WriteLine("Message sent.");
    //    //}
    //    // mailSent = true;
    //}

  


    //Code Added by Shahnila
    ////public static void sendApprovalEmail(string empid, string masterTable, string masterColumn, string masterId, string wfTableName, string primaryColumn, string emailMessage, string emailSubject)
    ////{
    ////    string selectQuery = "";
    ////    try
    ////    {


    ////        //TNAME, P.PROFESSION ,DEPT_HEAD_ID,DEPT_HEAD_EMAIL,SEQUENCE FROM " + wfTableName + " D ";
    ////        selectQuery = "SELECT top 1 " + primaryColumn + ",  " + masterColumn + ",D.EMP_ID, E.FIRSTNAME, E.LASTNAME, P.PROFESSION ,DEPT_HEAD_ID,DEPT_HEAD_EMAIL,SEQUENCE FROM " + wfTableName + " D ";
    ////        selectQuery += "Left JOIN HR_TBLEMPLOYEE E ON E.EMP_ID = D.EMP_ID ";
    ////        selectQuery += "LEFT JOIN SETUP_PROFESSION P ON E.PROFESSIONID = P.PROFESSIONID ";
    ////        selectQuery += " WHERE " + masterColumn + "=" + masterId + " AND d.EMP_id = " + empid + " and EMAILSENT =0 ";
    ////        selectQuery += " ORDER BY SEQUENCE DESC";
    ////        DataTable WFInfo = getDataTable(selectQuery);
    ////        if (WFInfo.Rows.Count > 0)
    ////        {
    ////            // we got the first email that is not sent in the work flow sequence 
    ////            // so, we send the email 
    ////            emailMessage = emailMessage.Replace("XXX", WFInfo.Rows[0][primaryColumn].ToString());
    ////            sendMail(WFInfo.Rows[0]["dept_head_email"].ToString(), emailSubject, emailMessage);
    ////            string updateQuery = "update " + wfTableName + " set emailsent = 1 , ModifiedOn = getdate() where " + primaryColumn + " =" + WFInfo.Rows[0][primaryColumn].ToString() + " AND " + masterColumn + "=" + masterId + " AND emp_id = " + empid;
    ////            insertData(updateQuery);
    ////        }
    ////        else
    ////        {
    ////            // if all the emails in the work flow have been sent
    ////            // then the request is approved and we update it
    ////            string updateMasterQuery = "update " + masterTable + " set Status = 'Approved', ModifiedBy=" + SessionHelper.EmpId + ", ModifiedOn=getdate() where " + masterColumn + " = " + masterId;
    ////            insertData(updateMasterQuery);
    ////        }
    ////    }
    ////    catch (Exception ex)
    ////    {
    ////        System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);

    ////        DBHelper.logError("HRExpatTS.aspx/sendApprovalEmail()", ex.Message + selectQuery);

    ////    }
    ////}
    //End of Code

    public static void sendRejectionEmail(string empid,string wfId, string masterTable, string masterColumn, string masterId, string wfTableName, string primaryColumn, string emailMessage, string emailSubject)
    {
        string selectQuery = "SELECT " + primaryColumn + ",  " + masterColumn + ",D.EMP_ID, E.FIRSTNAME, E.LASTNAME, P.PROFESSION ,DEPT_HEAD_ID,DEPT_HEAD_EMAIL,SEQUENCE FROM " + wfTableName + " D ";
        selectQuery += "Left JOIN HR_TBLEMPLOYEE E ON E.EMP_ID = D.EMP_ID ";
        selectQuery += "LEFT JOIN SETUP_PROFESSION P ON E.PROFESSIONID = P.PROFESSIONID ";
        selectQuery += " WHERE " + masterColumn + "=" + masterId + " AND d.EMP_id = " + empid;
        selectQuery += " and sequence>(select sequence from "+wfTableName+" where "+primaryColumn+"=" + wfId + ")";
        selectQuery += " ORDER BY SEQUENCE DESC";
        DataTable WFInfo = getDataTable(selectQuery);
        if (WFInfo.Rows.Count > 0)
        {
            //emailMessage = emailMessage.Replace("XXX", WFInfo.Rows[0][primaryColumn].ToString());
            for(int icount=0; icount<WFInfo.Rows.Count; icount++)
                sendMail(WFInfo.Rows[icount]["dept_head_email"].ToString(), emailSubject, emailMessage);
        }
        
    }
    public static void sendRejectionEmail(string empid, string masterTable, string masterColumn, string masterId, string wfTableName, string primaryColumn, string emailMessage, string emailSubject)
    {
        string selectQuery = "SELECT " + primaryColumn + ",  " + masterColumn + ",D.EMP_ID, E.FIRSTNAME, E.LASTNAME, P.PROFESSION ,DEPT_HEAD_ID,DEPT_HEAD_EMAIL,SEQUENCE FROM " + wfTableName + " D ";
        selectQuery += "Left JOIN HR_TBLEMPLOYEE E ON E.EMP_ID = D.EMP_ID ";
        selectQuery += "LEFT JOIN SETUP_PROFESSION P ON E.PROFESSIONID = P.PROFESSIONID ";
        selectQuery += " WHERE " + masterColumn + "=" + masterId + " AND d.EMP_id = " + empid;
        selectQuery += " ORDER BY SEQUENCE DESC";
        DataTable WFInfo = getDataTable(selectQuery);
        if (WFInfo.Rows.Count > 0)
        {
            //emailMessage = emailMessage.Replace("XXX", WFInfo.Rows[0][primaryColumn].ToString());
            for (int icount = 0; icount < WFInfo.Rows.Count; icount++)
                sendMail(WFInfo.Rows[icount]["dept_head_email"].ToString(), emailSubject, emailMessage);
        }

    }


    public static void addRowToDataTable(DataTable dt, int rowPosition, string textFieldName, string valueFieldName, string text, string value)
    {
        // in case there are fields that don't accept nulls
        foreach (DataColumn col in dt.Columns)
            col.AllowDBNull = true;

        DataRow dr = dt.NewRow();
        dr[valueFieldName] = value;
        dr[textFieldName] = text;
        dt.Rows.InsertAt(dr, rowPosition);
    }


    public static string returnDept(DataTable dept,string s, ref string p)
    {
        //string newP = "";
        DataRow [] dr= dept.Select("Dept_Id=" + s);
        if (dr.Length > 0)
        {
            p += dr[0]["ParentDeptId"].ToString()+",";
            return  returnDept(dept, dr[0]["ParentDeptId"].ToString(),ref p);
        }
        return "";
    }
    public static DataTable getDeptChildren(int deptId)
    {
        DataTable dept = DBHelper.getDataTable("Select * from HR_tblDept");
        string p = "(" + deptId + ",";
       returnParent(dept, deptId.ToString(), ref p);
        p = p.Substring(0, p.Length - 1);
        p += ")";
        DataTable newdept = DBHelper.getDataTable("Select * from HR_tblDept where dept_id in " + p);
        return newdept;
    }
    public static void returnParent(DataTable dept, string s, ref string p)
    {
        //string newP = "";
        DataRow[] dr = dept.Select("ParentDeptid=" + s);
        //if (dr.Length > 0)
        {
            for (int i = 0; i < dr.Length; i++)
            {
                p += dr[i]["Dept_Id"].ToString() + ",";
                 returnParent(dept, dr[i]["Dept_Id"].ToString(), ref p);
            }
        }
        //return "";
    }

    public static string getQRTable(DataRowCollection dr, string DesignationColumnName, string QRPathColumnName)
    {
        string tblStart = "<table cellspacing='5' style='border:solid 0px #eee; margin:0px;'><tr>";
        string QRPath = "";
        int QRsShowing = 0;
        for (int i = 0; i < dr.Count; i++)
        {
            if (dr[i]["QRPath"].ToString() != "")
            {
                //maximum 5 columns in each row:
                if ((QRsShowing != 0) && (QRsShowing % 5 == 0)) QRPath += "</tr><tr>";
                QRsShowing++;

                QRPath += "<td align='center' valign='bottom' style='border:solid 1px #eee;padding:7px; width:130px;' >" + dr[i][DesignationColumnName] + "<hr/></br><img id='imgQR" + i + "' src='Barcodes/" + dr[i][QRPathColumnName] + "' style='height:120px;width:120px;border-width:0px;' /></td>";
            }
        }
        if (QRPath.Length > 0)
            QRPath = tblStart + QRPath + "</tr></table>";
        return QRPath;
    }
    public static string getQRTable(DataRow[] dr, string DesignationColumnName, string QRPathColumnName)
    {
        string tblStart = "<table cellspacing='5' style='border:solid 0px #eee; margin:0px;'><tr>";
        string QRPath = "";
        for (int i = 0; i < dr.Length; i++)
        {
            if (dr[i]["QRPath"].ToString() != "")
            {
                QRPath += "<td align='center' valign='bottom' style='border:solid 1px #eee;padding:7px; width:130px;' >" + dr[i][DesignationColumnName] + "<hr/></br><img id='imgQR" + i + "' src='Barcodes/" + dr[i][QRPathColumnName] + "' style='height:120px;width:120px;border-width:0px;' /></td>";
            }
        }
        if (QRPath.Length > 0)
            QRPath = tblStart + QRPath + "</tr></table>";
        return QRPath;
    }

    public static string getQRTable(DataRowCollection dr, string DesignationColumnName, string QRPathColumnName,int pageSize)
    {
        string tblStart = "<table cellspacing='5' style='border:solid 0px #eee; margin:0px;'><tr>";
        string QRPath = "";
        int idx = 0;
        for (int i = 0; i < dr.Count; i++)
        {
            
            if (dr[i]["QRPath"].ToString() != "")
            {
                idx++;
                QRPath += "<td align='center' valign='bottom' style='border:solid 1px #eee;padding:7px; width:130px;' >" + dr[i][DesignationColumnName] + "<hr/></br><img id='imgQR" + i + "' src='Barcodes/" + dr[i][QRPathColumnName] + "' style='height:120px;width:120px;border-width:0px;' /></td>";
            }
            if ((pageSize ) == idx)
            {
                QRPath += "</tr><tr>";
            }
        }
        if (QRPath.Length > 0)
            QRPath = tblStart + QRPath + "</tr></table>";
        return QRPath;
    }
    public static string getQRTable(DataRow[] dr, string DesignationColumnName, string QRPathColumnName, int pageSize)
    {
        string tblStart = "<table cellspacing='5' style='border:solid 0px #eee; margin:0px;'><tr>";
        string QRPath = "";
        int idx = 0;
        for (int i = 0; i < dr.Length; i++)
        {

            if (dr[i]["QRPath"].ToString() != "")
            {
                idx++;
                QRPath += "<td align='center' valign='bottom' style='border:solid 1px #eee;padding:7px; width:130px;' >" + dr[i][DesignationColumnName] + "<hr/></br><img id='imgQR" + i + "' src='Barcodes/" + dr[i][QRPathColumnName] + "' style='height:120px;width:120px;border-width:0px;' /></td>";
            }
            if ((pageSize) == idx)
            {
                QRPath += "</tr><tr>";
            }
        }
        if (QRPath.Length > 0)
            QRPath = tblStart + QRPath + "</tr></table>";
        return QRPath;
    }

    public static string getQRTable(DataRowCollection dr, string DesignationColumnName, string QRPathColumnName,string RemarksColumnName)
    {
        string tblStart = "<link href='../tooltip/theme/style.css' rel='stylesheet' type='text/css' />";
        tblStart +="<script src='../tooltip/js/jquery.betterTooltip.js' type='text/javascript'></script>";
        tblStart += "<script type='text/javascript'>$(document).ready(function () {$('.tTip').betterTooltip({ speed: 150, delay: 200 });});</script>";
        tblStart += "<table cellspacing='5' style='border:solid 0px #eee; margin:0px;'><tr>";
        string QRPath = "";
        string strDesignation;
        string strQRPath;
        string strRemarks;
        //CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
        //TextInfo textInfo = cultureInfo.TextInfo;
        for (int i = 0; i < dr.Count; i++)
        {
            if (dr[i]["QRPath"].ToString() != "")
            {
                try
                {
                    strDesignation = dr[i][DesignationColumnName].ToString();
                }
                catch {
                    strDesignation = DesignationColumnName;
                }
                strQRPath = dr[i][QRPathColumnName].ToString();
                //strRemarks = textInfo.ToTitleCase(dr[i][RemarksColumnName].ToString().Replace("'","''"));
                strRemarks = dr[i][RemarksColumnName].ToString().Replace("'", "`");
                QRPath += "<td align='center' valign='bottom' style='border:solid 1px #eee;padding:7px; width:130px;' ><div class='tTip' id='tip" + i.ToString() + "' title='<div style=\"text-align:left; font-size:9px; \"><span style=\"font-weight:bold; \">Comments:</span><hr/><div style=\"text-align:justify;\"><b></b> " + strRemarks + "</div></div>' >" + strDesignation + "<hr/></br><img id='imgQR" + i + "' src='Barcodes/" + strQRPath + "' style='height:120px;width:120px;border-width:0px;' /></div></td>";
            }
        }
        if (QRPath.Length > 0)
            QRPath = tblStart + QRPath + "</tr></table>";
        return QRPath;
    }
    public static string getQRTable(DataRow[] dr, string DesignationColumnName, string QRPathColumnName, string RemarksColumnName)
    {
        string tblStart = "<link href='../tooltip/theme/style.css' rel='stylesheet' type='text/css' />";
        tblStart += "<script src='../tooltip/js/jquery.betterTooltip.js' type='text/javascript'></script>";
        tblStart += "<script type='text/javascript'>$(document).ready(function () {$('.tTip').betterTooltip({ speed: 150, delay: 200 });});</script>";
        tblStart += "<table cellspacing='5' style='border:solid 0px #eee; margin:0px;'><tr>";
        string QRPath = "";
        string strDesignation;
        string strQRPath;
        string strRemarks;
        //CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
        //TextInfo textInfo = cultureInfo.TextInfo;
        for (int i = 0; i < dr.Length; i++)
        {
            if (dr[i]["QRPath"].ToString() != "")
            {
                strDesignation = dr[i][DesignationColumnName].ToString();
                strQRPath = dr[i][QRPathColumnName].ToString();
                //strRemarks = textInfo.ToTitleCase(dr[i][RemarksColumnName].ToString().Replace("'","''"));
                strRemarks = dr[i][RemarksColumnName].ToString().Replace("'", "`");
                QRPath += "<td align='center' valign='bottom' style='border:solid 1px #eee;padding:7px; width:130px;' ><div class='tTip' id='tip" + i.ToString() + "' title='<div style=\"text-align:left; font-size:9px; \"><span style=\"font-weight:bold; \">Comments:</span><hr/><div style=\"text-align:justify;\"><b></b> " + strRemarks + "</div></div>' >" + strDesignation + "<hr/></br><img id='imgQR" + i + "' src='Barcodes/" + strQRPath + "' style='height:120px;width:120px;border-width:0px;' /></div></td>";
            }
        }
        if (QRPath.Length > 0)
            QRPath = tblStart + QRPath + "</tr></table>";
        return QRPath;
    }

    public static string getQRTable(DataRowCollection dr, string DesignationColumnName, string QRPathColumnName, string RemarksColumnName, int pageSize)
    {
        string tblStart = "<link href='../tooltip/theme/style.css' rel='stylesheet' type='text/css' />";
        tblStart += "<script src='../tooltip/js/jquery.betterTooltip.js' type='text/javascript'></script>";
        tblStart += "<script type='text/javascript'>$(document).ready(function () {$('.tTip').betterTooltip({ speed: 150, delay: 200 });});</script>";
        tblStart += "<table cellspacing='5' style='border:solid 0px #eee; margin:0px;'><tr>";
        string QRPath = "";
        string strDesignation;
        string strQRPath;
        string strRemarks;
        int idx = 0;
        //CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
        //TextInfo textInfo = cultureInfo.TextInfo;
        for (int i = 0; i < dr.Count; i++)
        {
            if (dr[i]["QRPath"].ToString() != "")
            {
                idx++;
                strDesignation = dr[i][DesignationColumnName].ToString();
                strQRPath = dr[i][QRPathColumnName].ToString();
                //strRemarks = textInfo.ToTitleCase(dr[i][RemarksColumnName].ToString());
                strRemarks = dr[i][RemarksColumnName].ToString().Replace("'", "`");
                QRPath += "<td align='center' valign='bottom' style='border:solid 1px #eee;padding:7px; width:130px;' ><div class='tTip' id='tip" + i.ToString() + "' title='<div style=\"text-align:left; font-size:9px; \"><span style=\"font-weight:bold; \">Comments:</span><hr/><div style=\"text-align:justify;\"><b></b> " + strRemarks + "</div></div>' >" + strDesignation + "<hr/></br><img id='imgQR" + i + "' src='Barcodes/" + strQRPath + "' style='height:120px;width:120px;border-width:0px;' /></div></td>";
            }
            if ((pageSize ) == idx)
            {
                QRPath += "</tr><tr>";
            }
        }
        if (QRPath.Length > 0)
            QRPath = tblStart + QRPath + "</tr></table>";
        return QRPath;
    }
    public static string getQRTable(DataRow[] dr, string DesignationColumnName, string QRPathColumnName, string RemarksColumnName, int pageSize)
    {
        string tblStart = "<link href='../tooltip/theme/style.css' rel='stylesheet' type='text/css' />";
        tblStart += "<script src='../tooltip/js/jquery.betterTooltip.js' type='text/javascript'></script>";
        tblStart += "<script type='text/javascript'>$(document).ready(function () {$('.tTip').betterTooltip({ speed: 150, delay: 200 });});</script>";
        tblStart += "<table cellspacing='5' style='border:solid 0px #eee; margin:0px;'><tr>";
        string QRPath = "";
        string strDesignation;
        string strQRPath;
        string strRemarks;
        int idx = 0;
        //CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
        //TextInfo textInfo = cultureInfo.TextInfo;
        for (int i = 0; i < dr.Length; i++)
        {
            if (dr[i]["QRPath"].ToString() != "")
            {
                idx++;
                strDesignation = dr[i][DesignationColumnName].ToString();
                strQRPath = dr[i][QRPathColumnName].ToString();
                //strRemarks = textInfo.ToTitleCase(dr[i][RemarksColumnName].ToString());
                strRemarks = dr[i][RemarksColumnName].ToString().Replace("'", "`");
                QRPath += "<td align='center' valign='bottom' style='border:solid 1px #eee;padding:7px; width:130px;' ><div class='tTip' id='tip" + i.ToString() + "' title='<div style=\"text-align:left; font-size:9px; \"><span style=\"font-weight:bold; \">Comments:</span><hr/><div style=\"text-align:justify;\"><b></b> " + strRemarks + "</div></div>' >" + strDesignation + "<hr/></br><img id='imgQR" + i + "' src='Barcodes/" + strQRPath + "' style='height:120px;width:120px;border-width:0px;' /></div></td>";
            }
            if ((pageSize) == idx)
            {
                QRPath += "</tr><tr>";
            }
        }
        if (QRPath.Length > 0)
            QRPath = tblStart + QRPath + "</tr></table>";
        return QRPath;
    }
    #region Validation
    public static bool validateDate(object obj)
    {
        try
        {
            DateTime dt = Convert.ToDateTime(obj);
            return true;
        }
        catch
        {
            return false;
        }

    }
    public static bool validateInterger(object obj)
    {
        try
        {
            int dt = Convert.ToInt32(obj);
            return true;
        }
        catch
        {
            return false;
        }

    }
    public static int getInteger(object obj)
    {
        return validateInterger(obj) ? Convert.ToInt32(obj) : 0;
    }
    public static bool validateDecimal(object obj)
    {
        try
        {
            decimal dt = Convert.ToDecimal(obj);
            return true;
        }
        catch
        {
            return false;
        }

    }
    public static decimal getDecimal(object obj)
    {
        return validateDecimal(obj) ? Convert.ToDecimal(obj) : 0;
    }
    public static decimal getDecimal(object obj, int roundAt)
    {
        return validateDecimal(obj) ? Math.Round(Convert.ToDecimal(obj), roundAt): 0;
    }

    public static string CurrencyFormat(string currency, int roundAt)
    {
        string salary = Math.Round(DBHelper.getDecimal(currency), roundAt).ToString("n"+roundAt);
        return salary;
    }

    #endregion
 

    public DataSet GetApprovedRequestsDataSet(int? EmployeeID, DateTime FromDate, DateTime ToDate)
    {
        DataSet dsApprovedRequests = new DataSet();
        try
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SIM_spGetApprovedRequestsReport";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@emp_id", SqlDbType.Int).Value = EmployeeID;
            command.Parameters.Add("@Fromdate", SqlDbType.DateTime).Value = FromDate;
            command.Parameters.Add("@Todate", SqlDbType.DateTime).Value = ToDate;
            dsApprovedRequests = DBHelper.getDataSet(command);

        }
        catch (Exception ex)
        {

        }
        return dsApprovedRequests;
    }

    #region region added to send the approval final email to the requester as well
    
    #endregion


    public static int Delete(SqlCommand oSqlCommand)
    {
        // Call the ExecuteReader method by specifying the command type
        // as a SQL statement, and passing in the SQL statement
        defaultDB = EnterpriseLibraryContainer.Current.GetInstance<Database>();
        return defaultDB.ExecuteNonQuery(oSqlCommand);
    }


    public static void logError(string pageName, string message)
    {
        Base obj = new Base();
        string strFileName = HttpContext.Current.Server.MapPath("~/Logs/" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt");
        System.IO.StreamWriter streamW = new System.IO.StreamWriter(strFileName, true);
        string strCompleteMsg = DateTime.Now.ToString() + " " + obj.FullName + " (" + obj.EmployeeCode + ") " + pageName + "  " + message;
        streamW.WriteLine(strCompleteMsg);
        streamW.Close();
    }
    public static void logError(Control control, string message)
    {
        // I put everything inisde try catchs in case something is null...
        string pageName = "";
        try { pageName += control.Page.Request.RawUrl; }
        catch (Exception err) { }
        try { pageName += " " + control.NamingContainer; }
        catch (Exception err) { }

        logError(pageName, message);
    }

}
