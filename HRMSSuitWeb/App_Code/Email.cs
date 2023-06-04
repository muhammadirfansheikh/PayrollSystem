using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.IO;
using System.Collections.Generic;
using DAL;
using System.Linq;
using System.Threading;

/// <summary>
/// Summary description for Email
/// </summary>
public class Email
{
    public static Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();
    public static string SenderEmailAddress = System.Configuration.ConfigurationManager.AppSettings["SenderEmailAddress"];
    public static string SenderEmailPassword = System.Configuration.ConfigurationManager.AppSettings["SenderEmailPassword"];
    public static string SenderSMTPServer = System.Configuration.ConfigurationManager.AppSettings["SenderSMTPServer"];

    public static string RootInSybrid = System.Configuration.ConfigurationManager.AppSettings["Root"];
    public static string Port = System.Configuration.ConfigurationManager.AppSettings["Port"];
    public static string clientEnableSsl = System.Configuration.ConfigurationManager.AppSettings["clientEnableSsl"];
    public static string UseDefaultCredentials = System.Configuration.ConfigurationManager.AppSettings["UseDefaultCredentials"];
    public static string BCC = "";
    public static string ATSSenderSMTPServer = System.Configuration.ConfigurationManager.AppSettings["ATSSenderSMTPServer"];
    public static string ATSSenderEmailAddress = System.Configuration.ConfigurationManager.AppSettings["ATSSenderEmailAddress"];
    public static string ATSSenderEmailPassword = System.Configuration.ConfigurationManager.AppSettings["ATSSenderEmailPassword"];


    //public static void SendMail(string to, string subject, string msg, string cc)
    //{
    //    try
    //    {
    //        //to = "ammar.khan@sybrid.com;";
    //        string displayName = System.Configuration.ConfigurationManager.AppSettings["Displayname"];
    //        string BCC = System.Configuration.ConfigurationManager.AppSettings["BCC"];
    //        int port = 587;
    //        MailMessage Mail = new MailMessage();

    //        string[] addresses = to.Split(';');
    //        foreach (string address in addresses)
    //        {
    //            Mail.To.Add(new MailAddress(address));
    //        }

    //        if (string.IsNullOrEmpty(cc) == false)
    //            Mail.CC.Add(new MailAddress(cc));

    //        Mail.From = new MailAddress(SenderEmailAddress, displayName);

    //        Mail.Bcc.Add(new MailAddress(BCC));
    //        Mail.Subject = subject;
    //        Mail.IsBodyHtml = true;

    //        List<LinkedResource> resourceList = new List<LinkedResource>();
    //        LinkedResource resource = null;
    //        string imgPath;

    //        for (int imgId = 1; imgId <= 4; imgId++)
    //        {
    //            if (msg.Contains("#$" + imgId))
    //            {
    //                imgPath = msg.Substring(msg.IndexOf("#$" + imgId), msg.LastIndexOf("#$" + imgId) - msg.IndexOf("#$" + imgId)).Trim();
    //                imgPath = imgPath.Replace("#$" + imgId, "");

    //                resource = new LinkedResource(HttpContext.Current.Server.MapPath(imgPath));
    //                resource.ContentId = "mailimage" + imgId;
    //                resourceList.Add(resource);
    //                msg = msg.Replace("#$" + imgId + imgPath + "#$" + imgId, "cid:" + resource.ContentId);
    //            }
    //        }
    //        AlternateView view = AlternateView.CreateAlternateViewFromString(msg, null, System.Net.Mime.MediaTypeNames.Text.Html);

    //        foreach (LinkedResource linkResource in resourceList)
    //        {
    //            view.LinkedResources.Add(linkResource);
    //        }

    //        Mail.AlternateViews.Add(view);
    //        //End

    //        Mail.Body = msg;

    //        SmtpClient client = new SmtpClient(SenderSMTPServer);
    //        //smtpClient.Send(message);
    //        client.Port = port;
    //        client.Host = SenderSMTPServer;
    //        System.Net.NetworkCredential nc = new System.Net.NetworkCredential(SenderEmailAddress, SenderEmailPassword);
    //        client.EnableSsl = true;
    //        client.UseDefaultCredentials = false;
    //        client.Credentials = nc;
    //        client.Send(Mail);
    //    }
    //    catch (Exception ex)
    //    {
    //        // do logging
    //        return;
    //    }
    //}

    public static bool SendMail(string to, string subject, string msg, string cc)
    {
        bool Return_Value = false;
        string EmailsAddress = "";
        if (to != "" && SenderEmailAddress != "" && SenderEmailPassword != "" && SenderSMTPServer != "" && Port != "" && clientEnableSsl != "" && UseDefaultCredentials != "")
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            if (!subject.Contains("Reset Password Request"))
            {
                //if (!cc.Contains("hr@cyber.net.pk"))
                //{
                //    cc = cc + ";hr@cyber.net.pk";
                //}
            }
            else
            {
                cc = "";
                BCC = "";
            } 
            BCC = "";
            string displayName = System.Configuration.ConfigurationManager.AppSettings["Displayname"];
            EmailsAddress = subject + " : " + to + (cc != "" ? (";" + cc + "") : "") + (BCC != "" ? (";" + BCC + "") : "");
            MailMessage Mail = new MailMessage();

            string[] addresses = to.Split(';');
            foreach (string address in addresses)
            {
                if (address.Trim() != "")
                {
                    Mail.To.Add(new MailAddress(address.Trim()));
                }
            }

            if (string.IsNullOrEmpty(cc) == false)
            {
                if (cc.Contains(";"))
                {
                    string[] cc_addresses = cc.Split(';');
                    foreach (string address in cc_addresses)
                    {
                        if (address.Trim() != "")
                        {
                            Mail.CC.Add(new MailAddress(address.Trim()));
                        }
                    }
                }
                else
                {
                    Mail.CC.Add(new MailAddress(cc.Trim()));
                }
            }

            Mail.From = new MailAddress(SenderEmailAddress, displayName);
            if (string.IsNullOrEmpty(BCC) == false)
            {
                if (BCC.Contains(";"))
                {
                    string[] BCC_addresses = BCC.Split(';');
                    foreach (string address in BCC_addresses)
                    {
                        if (address.Trim() != "")
                        {
                            Mail.Bcc.Add(new MailAddress(address.Trim()));
                        }
                    }
                }
                else
                {
                    Mail.Bcc.Add(new MailAddress(BCC.Trim()));
                }
            }
            Mail.Subject = subject;
            Mail.IsBodyHtml = true;

            List<LinkedResource> resourceList = new List<LinkedResource>();
            LinkedResource resource = null;
            string imgPath;

            try
            {
                for (int imgId = 1; imgId <= 4; imgId++)
                {
                    if (msg.Contains("#$" + imgId))
                    {
                        imgPath = msg.Substring(msg.IndexOf("#$" + imgId), msg.LastIndexOf("#$" + imgId) - msg.IndexOf("#$" + imgId)).Trim();
                        imgPath = imgPath.Replace("#$" + imgId, "");

                        resource = new LinkedResource(HttpContext.Current.Server.MapPath(imgPath));
                        resource.ContentId = "mailimage" + imgId;
                        resourceList.Add(resource);
                        msg = msg.Replace("#$" + imgId + imgPath + "#$" + imgId, "cid:" + resource.ContentId);
                    }
                }
            }
            catch
            { }

            AlternateView view = AlternateView.CreateAlternateViewFromString(msg, null, System.Net.Mime.MediaTypeNames.Text.Html);

            foreach (LinkedResource linkResource in resourceList)
            {
                view.LinkedResources.Add(linkResource);
            } 
            Mail.AlternateViews.Add(view);
            Mail.Body = msg;
            SmtpClient client = new SmtpClient(SenderSMTPServer);
            client.Port = Convert.ToInt32(Port);
            client.Host = SenderSMTPServer;
            System.Net.NetworkCredential nc = new System.Net.NetworkCredential(SenderEmailAddress, SenderEmailPassword);
            client.EnableSsl = Convert.ToBoolean(clientEnableSsl);
            client.UseDefaultCredentials = Convert.ToBoolean(UseDefaultCredentials);
            client.Credentials = nc;
            try
            {
                client.Send(Mail);
                Return_Value = true;
                WriteFile("Success - ", msg, EmailsAddress);
            }
            catch (Exception ex)
            {
                Return_Value = false;
                WriteFile("Error   - ", ex.ToString(), EmailsAddress);
            }
        }
        else
        {
            Return_Value = false;
        }
        return Return_Value;
    }

    public static void WriteFile(string Type, string error, string EmailsAddress)
    {
        try
        {
            DateTime dateTime = DateTime.Now;
            string FolderName = "EmailLog";
            string EmailLog = AppDomain.CurrentDomain.BaseDirectory + FolderName;
            if (!Directory.Exists(EmailLog))
                Directory.CreateDirectory(EmailLog);
            string Year = EmailLog + "/" + dateTime.ToString("yyyy");
            if (!Directory.Exists(Year))
                Directory.CreateDirectory(Year);
            string Month = Year + "/" + dateTime.ToString("MMM");
            if (!Directory.Exists(Month))
                Directory.CreateDirectory(Month);
            string Date = dateTime.ToString("dd-MMM-yyyy");
            string LogFileName = Month + "/" + Date + ".txt";

            if (!System.IO.File.Exists(LogFileName))
            {
                // Create a file to write to. 
                using (System.IO.StreamWriter sw = System.IO.File.CreateText(LogFileName))
                {

                }
            }
            // This text is always added, making the file longer over time 
            // if it is not deleted. 
            using (System.IO.StreamWriter sw = System.IO.File.AppendText(LogFileName))
            {
                sw.WriteLine(Type + DateTime.Now.ToString() + " < " + EmailsAddress + " > " + " : " + error);
            }
        }
        catch (Exception ex)
        {

        }
    }

    public static void SendBulkMail(List<string> toBCC, string subject, string msg, string cc)
    {
        try
        {
            string displayName = "TMS";
            int port = 587;
            MailMessage message = new MailMessage();

            foreach (string address in toBCC)
            {
                message.Bcc.Add(new MailAddress(address));
            }

            if (string.IsNullOrEmpty(cc) == false)
                message.CC.Add(new MailAddress(cc));

            message.From = new MailAddress(SenderEmailAddress, displayName);
            message.Subject = subject;
            message.Body = msg;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient();
            client.Port = port;
            client.Host = SenderSMTPServer;
            System.Net.NetworkCredential nc = new System.Net.NetworkCredential(SenderEmailAddress, SenderEmailPassword);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = nc;
            client.Send(message);
        }
        catch (Exception ex)
        {
            // do logging
            return;
        }
    }

    public static string GetTemplateString(int templateCode)
    {
        StreamReader objStreamReader;
        string emailText = "";
        switch (templateCode)
        {
            case (int)Constant.EmailTemplates.ForgotPassword:
                objStreamReader = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + @"Templates\ForgotPasswordTemplate.htm");
                emailText = objStreamReader.ReadToEnd();
                objStreamReader.Close();
                objStreamReader = null;
                break;
            case (int)Constant.EmailTemplates.NewRegistration:
                objStreamReader = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + @"Templates\NewRegistration.htm");
                emailText = objStreamReader.ReadToEnd();
                objStreamReader.Close();
                objStreamReader = null;
                break;

            case (int)Constant.EmailTemplates.EmployeeProfileLink:
                objStreamReader = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + @"Templates\Email\EmployeeProfileEmail.htm");
                emailText = objStreamReader.ReadToEnd();
                objStreamReader.Close();
                objStreamReader = null;
                break;
        }
        return emailText;
        //if (templateCode == (int)Constant.EmailTemplates.ForgotPassword)
        //{
        //    objStreamReader = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + @"Templates\ForgotPasswordTemplate.htm");
        //    string emailText = objStreamReader.ReadToEnd();
        //    objStreamReader.Close();
        //    objStreamReader = null;
        //    return emailText;
        //}
        //else
        //{
        //    objStreamReader = null;
        //    return string.Empty;
        //}
    }

    public static string ReplaceEmailBodySection(string emailTemplate, string heading,
     string requesterInfoHtml, string requestDetailHtml, string linksHtml)
    {
        string emailBody = emailTemplate;
        emailBody = emailBody.Replace("[Heading]", heading);

        emailBody = emailBody.Replace("[RequesterInfo]", requesterInfoHtml);
        emailBody = emailBody.Replace("[RequestDetail]", requestDetailHtml);
        emailBody = emailBody.Replace("[Links]", linksHtml);


        return emailBody;
    }

    public static DataTable GetRequesterInfo(int RequesterId)
    {


        DataTable Employees = context.Setup_Employee.Where(c => c.EmployeeId == RequesterId

            ).Select(c => new
            {

                requesterName = c.FirstName + " " + c.LastName,
                RequesterDesignation = c.Setup_Designation == null ? "" : c.Setup_Designation.DesignationName,
                requesterDeptName = c.Setup_Department == null ? "" : c.Setup_Department.DepartmentName,



            }).ToList().ToDataTable();
        return Employees;
    }

    public static DataTable GetEmployeeInfo(int Employee)
    {


        DataTable Employees = context.Setup_Employee.Where(c => c.EmployeeId == Employee

            ).Select(c => new
            {

                Name = c.FirstName + " " + c.LastName,
                Designation = c.Setup_Designation == null ? "" : c.Setup_Designation.DesignationName,
                Department = c.Setup_Department == null ? "" : c.Setup_Department.DepartmentName,
                Email = c.OfficeEmailAddress,
                UserName = c.FirstName.Trim().Replace(" ", "") + c.EmployeeCode + "@sybrid.com",
                password = c.EmployeeCode
            }).ToList().ToDataTable();
        return Employees;
    }


    public static string GetRequesterInfoHtml(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            string html =

    @"<td style='color: #eeeeee; font-family: Calibri; font-size: 12px;'>
            &nbsp; Requester Name:
        </td>
        <td  style='color: #ffffff; font-family: Calibri; font-size: 12px;'>
            " + Convert.ToString(dt.Rows[0]["requesterName"]) + "" +
    @"</td>
        <td style='color: #eeeeee; font-family: Calibri; font-size: 12px;' align='center'>
            Designation:
        </td>
        <td  style='color: #ffffff; font-family: Calibri; font-size: 12px;' align='left'>
            " + Convert.ToString(dt.Rows[0]["RequesterDesignation"]) + "" +
    @"</td>
        <td  style='color: #eeeeee; font-family: Calibri; font-size: 12px;' align='center'>
            Department:
        </td>
        <td style='color: #ffffff; font-family: Calibri; font-size: 12px;'>
            &nbsp; " + Convert.ToString(dt.Rows[0]["requesterDeptName"]) + "" +
    @"</td>";

            return html;
        }
        return "";
    }
    public static string GetRequestDetailHtml(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            string detailHTML = "<table border='0' bordercolor='#CCCCCC' cellpadding='0' cellspacing='2' align='center' width='620px' style='color:#0d6e8c; font-family:Calibri; font-size:12px;border-top:thick #FFFFFF solid;'>";
            detailHTML += "<tr valign='bottom'>";
            int columnCount = 0;

            foreach (DataColumn dc in dt.Columns)
            {
                detailHTML += "<td width='100' style='color:#222222;' bgcolor='#dfebf4'>&nbsp;" + dc.ColumnName.Replace("?", "") + "</td>";
                detailHTML += "<td width='190' colspan='" + (dc.ColumnName.EndsWith("?") ? "3" : "1") + "'style='color:#0d6e8c;' bgcolor='#dfebf4'>&nbsp;" + dt.Rows[0][dc.ColumnName] + "</td>";

                columnCount++;

                if (columnCount % 2 == 0)
                {
                    detailHTML += "</tr><tr>";
                }
            }

            detailHTML += "</tr></table>";
            return detailHTML;
        }
        return "";
    }
    public static string ComposeEmailLinks(string pageURL, Dictionary<string, string> urlParameter,
    bool includeWFParameter, int workflowDataId = 0)
    {
        // the DBHelper send approval email changes this XXX for the next WFId
        string WFId = workflowDataId.ToString();

        string queryString = ""; //"?" + ParameterName + "=" + ParameterValue;

        if (urlParameter.Count > 0)
        {
            queryString += "?";
            foreach (KeyValuePair<string, string> keyValuePair in urlParameter)
            {
                queryString += keyValuePair.Key + "=" + keyValuePair.Value + "&";
            }

            queryString = queryString.Substring(0, queryString.Length - 1);
        }

        if (includeWFParameter)
            queryString += "&WF=" + WFId;


        string getLinksHTML =
     "<table height='62' border='0' bgcolor='#f6fbff' cellpadding='0' cellspacing='0' align='left' width='360px' style='color:#0d6e8c; font-family:Calibri; font-size:12px; margin-left:5px; border:#CCCCCC dotted thin;'>"
   + "<tr>"
   + "<td width='100' style='color:#222222;'>&nbsp;I am in Sybrid</td>"
   + "<td width='75' style='color:#0d6e8c; text-align:right'><img src='#$4/Images/arrow1.png#$4'/>&nbsp;</td>"
   + "<td width='75' style='color:#222222; text-align:center'><a href='#1' style='color:#000000; text-decoration:none'>Click Here</a>&nbsp;</td>"
   + "</tr>"

   + "</table>";

        getLinksHTML = getLinksHTML.Replace("#1", Constant.Urls.RootInSybrid + pageURL + queryString);


        return getLinksHTML;








    }

    public string GetWFStatusHtml(DataTable dt)
    {
        int step = 1;
        string html = "<table border='0' bordercolor='#CCCCCC' cellpadding='0' cellspacing='2' align='center' width='570px' style='color:#0d6e8c; font-family:Calibri; font-size:12px;border-top:thick #FFFFFF solid;'>" +
            "<tbody><tr valign='bottom'>" +
                "<td colspan='8' style='color:#585858; font-weight:bold;' bgcolor='#efefef'>Request Workflow Status</td>" +
    "</tr>" +
     "<tr>" +
                      "<td width='15' style='color:#222222; font-size:smaller;' bgcolor='#efefef'>Steps</td>" +
                      "<td width='80' style='color:#222222; font-size:smaller;' bgcolor='#efefef'>Designation</td>" +
                      "<td width='80' style='color:#222222 ;font-size:smaller;' bgcolor='#efefef'>Department</td>" +
                      "<td width='100' style='color:#222222 ;font-size:smaller;' bgcolor='#efefef'>Email</td>" +
                      "<td width='70' style='color:#222222; font-size:smaller;' bgcolor='#efefef'>Remarks</td>" +
                      "<td width='100' style='color:#222222; font-size:smaller;' bgcolor='#efefef'>Actor Type</td>" +
                      "<td width='100' style='color:#222222; font-size:smaller;' bgcolor='#efefef'>Date</td>" +
                      "<td width='60' style='color:#222222; font-size:smaller;' bgcolor='#efefef'>Status</td>" +
                      "</tr>";
        foreach (DataRow dr in dt.Rows)
        {
            html += "<tr>" +
                    "<td style='color:#0d6e8c; font-size:smaller; text-align:center;' bgcolor='#dfebf4'>" + step.ToString() + "</td>" +
                    "<td style='color:#0d6e8c; font-size:smaller;' bgcolor='#dfebf4'>" + Convert.ToString(dr["head_designation"]) + "</td>" +
                    "<td style='color:#0d6e8c; font-size:smaller;' bgcolor='#dfebf4'>" + Convert.ToString(dr["Dept_Name"]) + "</td>" +
                    "<td style='color:#0d6e8c; font-size:smaller;' bgcolor='#dfebf4'>" + Convert.ToString(dr["Dept_Head_Email"]) + "</td>" +
                    "<td style='color:#0d6e8c; font-size:smaller;' bgcolor='#dfebf4'>" + Convert.ToString(dr["Remarks"]) + "</td>" +
                    "<td style='color:#0d6e8c; font-size:smaller;' bgcolor='#dfebf4'>" + Convert.ToString(dr["stepActor"]) + "</td>" +
                    "<td style='color:#0d6e8c; font-size:smaller;' bgcolor='#dfebf4'>" + Convert.ToString(dr["CreatedOn"]) + "</td>" +
                    "<td style='color:#0d6e8c; font-size:smaller;' bgcolor='#dfebf4'>" + Convert.ToString(dr["Status"]) + "</td>" +
                    "</tr>";
            step++;
        }
        html += "</tbody></table>";
        return html;
    }

    public static void SendEmail(string to, string subject, string msg, string cc)
    {
        Thread email = new Thread(delegate ()
        {
            SendMail(to, subject, msg, cc, "");
        });

        email.IsBackground = true;
        email.Start();
        //ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Email sent.');", true);
    }

    private static void SendMail(string to, string subject, string msg, string cc, string var)
    {
        try
        {
            //to = "ammar.khan@sybrid.com;";
            string displayName = "HRMS";
            int port = 587;
            MailMessage message = new MailMessage();

            if (to.Contains(","))
            {
                to = to.Replace(',', ';');
            }
            string[] addresses = to.Split(';');
            foreach (string address in addresses)
            {
                if (address != "")
                {
                    message.To.Add(new MailAddress(address));
                }
            }


            if (cc.Contains(","))
            {
                cc = cc.Replace(',', ';');
            }
            string[] cc_address = cc.Split(';');
            foreach (string address in cc_address)
            {
                if (address != "")
                {
                    message.CC.Add(new MailAddress(address));
                }
            }

            //message.Bcc.Add(new MailAddress("ammar.khan@sybrid.com"));
            //message.CC.Add(new MailAddress("ameer.ali@sybrid.com"));


            message.From = new MailAddress(SenderEmailAddress, displayName);
            message.Subject = subject;
            message.Body = msg;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient();
            client.Port = port;
            client.Host = SenderSMTPServer;
            System.Net.NetworkCredential nc = new System.Net.NetworkCredential(SenderEmailAddress, SenderEmailPassword);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = nc;
            client.Send(message);
        }
        catch (Exception ex)
        {
            // do logging
            return;
        }
    }
}
