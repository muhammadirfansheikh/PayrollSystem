using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;


public partial class ResetPassword : Base
{
    Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();

    public void SetToken()
    {
        try
        {
            hfToken.Value = "";
            string EncryptedQueryString = Request.Url.Query.TrimStart('?');
            Token = Security.DecryptById(EncryptedQueryString, "Token");
            if (Token != "")
            {
                var li = context.Setup_User.FirstOrDefault(a => a.ForgetPassword_Token == Token);
                if (li != null)
                {
                    if (li.ForgetPassword_Token_CreatedDatetime != null)
                    {
                        DateTime ForgetPassword_Token_CreatedDatetime = Convert.ToDateTime(li.ForgetPassword_Token_CreatedDatetime);
                        ForgetPassword_Token_CreatedDatetime = ForgetPassword_Token_CreatedDatetime.AddMinutes(Security.ResetPasswordEmailLink_Expiry_Mints);
                        if (DateTime.Now <= ForgetPassword_Token_CreatedDatetime)
                        {
                            hfToken.Value = Token;
                            Div_Main.Visible = true;
                        }
                        else
                        {
                            AlertBoxRedirect_Error("This link has been expired. Please generate a new request.");
                        }
                    }
                    else
                    {
                        AlertBoxRedirect_Error("This link has been expired. Please generate a new request.");
                    }
                }
                else
                {
                    AlertBoxRedirect_Error("This link has been expired. Please generate a new request.");
                }
            }
            else
            {
                AlertBoxRedirect_Error("Invalid request");
            }
        }
        catch (Exception ex)
        {
            AlertBoxRedirect_Error("Invalid request");
        }
    }
    public string GetToken()
    {
        return hfToken.Value;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfAppURL.Value = System.Configuration.ConfigurationManager.AppSettings["Root"];
            NewPassword.Text = "";
            ConfirmNewPassword.Text = "";
            SetToken();
        }
    }
    protected void btn_ChangePassword_Click(object sender, EventArgs e)
    {
        try
        {
            string Token = GetToken();
            if (Token != "")
            {
                string New_Password = NewPassword.Text.Trim();
                if (New_Password != "")
                {
                    if (Security.Validate_Password(New_Password) == true)
                    {
                        string ConfirmNewPassword_ = ConfirmNewPassword.Text.Trim();
                        if (ConfirmNewPassword_ != "")
                        {
                            if (Security.Validate_Password(ConfirmNewPassword_) == true)
                            {
                                if (New_Password == ConfirmNewPassword_)
                                {
                                    var User_list = context.Setup_User.FirstOrDefault(a => a.Is_Active == true && a.ForgetPassword_Token == Token);
                                    if (User_list != null)
                                    {
                                        if (User_list.ForgetPassword_Token_CreatedDatetime != null)
                                        {
                                            DateTime ForgetPassword_Token_CreatedDatetime = Convert.ToDateTime(User_list.ForgetPassword_Token_CreatedDatetime);
                                            ForgetPassword_Token_CreatedDatetime = ForgetPassword_Token_CreatedDatetime.AddMinutes(Security.ResetPasswordEmailLink_Expiry_Mints);
                                            if (DateTime.Now <= ForgetPassword_Token_CreatedDatetime)
                                            {
                                                string NewPassword_GetHash = Security.Generate_Password(New_Password);
                                                DataTable dt1 = CommonHelper.usp_UserLogin_UpdatePassword(User_list.User_Code, "", NewPassword_GetHash, true, UserIP, false, User_list.User_Code);
                                                if (dt1 != null && dt1.Rows.Count > 0)
                                                {
                                                    string Status = Convert.ToString(dt1.Rows[0]["Status"]);
                                                    string Message = Convert.ToString(dt1.Rows[0]["Msg"]);
                                                    if (Status == "0")
                                                    {
                                                        Error(Message);
                                                    }
                                                    else if (Status == "1")
                                                    {
                                                        ////////Security.SendEmailOnPasswordChange(User_list.User_Code, true);
                                                        AlertBoxRedirect(Message);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                Error("This link has been expired. Please generate a new request.");
                                            }
                                        }
                                        else
                                        {
                                            Error("This link has been expired. Please generate a new request.");
                                        }
                                    }
                                    else
                                    {
                                        Error("This link has been expired. Please generate a new request.");
                                    }
                                }
                                else
                                {
                                    Error("The confirm new password must match the new password entry");
                                }
                            }
                            else
                            {
                                Error("Confirm new password does not meet complexity requirements");
                            }
                        }
                        else
                        {
                            Error("Please enter confirm new password");
                        }
                    }
                    else
                    {
                        Error("New password does not meet complexity requirements");
                    }
                }
                else
                {
                    Error("Please enter new password");
                }
            }
            else
            {
                Error("Invalid request");
            }

        }
        catch (Exception ex)
        {
            Error("Something went wrong. Please contact to administrator.");
        }
    }
    public void AlertBoxRedirect(string message)
    {
        message = "AlertBoxRedirect('Success!','" + message + "','success');";
        ScriptManager.RegisterStartupScript(this, GetType(), message, message, true);
    }
    public void Error(string message)
    {
        message = "AlertBox('Error!','" + message + "','error');";
        ScriptManager.RegisterStartupScript(this, GetType(), message, message, true);
    }
    public void Success(string message)
    {
        message = "AlertBox('Success!','" + message + "','success');";
        ScriptManager.RegisterStartupScript(this, GetType(), message, message, true);
    }
    public void AlertBoxRedirect_Error(string message)
    {
        message = "AlertBoxRedirect_Error('Error!','" + message + "','error','/Login.aspx');";
        ScriptManager.RegisterStartupScript(this, GetType(), message, message, true);
    }
}