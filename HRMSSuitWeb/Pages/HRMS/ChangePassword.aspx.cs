using System;
using System.Web.UI;
using DAL;
using System.Data;

public partial class Pages_ChangePassword : System.Web.UI.Page
{
    Sybrid_DatabaseEntities entities = new Sybrid_DatabaseEntities();
    Base baseClass = new Base();

    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    setcontrol();
    //    if (!IsPostBack)
    //    {
    //        CurrentPassword.Text = "";
    //    }
    //}

    //protected void ChangePasswordPushButton_Click(object sender, EventArgs e)
    //{

    //    setcontrol();

    //    string old_Password = CurrentPassword.Text;

    //    string New_Password = NewPassword.Text;


    //    int EmployeeId = new Base().UserId;
    //    int EmployeeId_ = Convert.ToInt32(new Base().EmployeeCode);

    //    try
    //    {

    //        //var Employee = context.UserLogins.FirstOrDefault(p => p.UserId == EmployeeId && p.UserPassword == old_Password && p.IsActive == true);

    //        //Employee.ModifiedBy = EmployeeId;
    //        //Employee.ModifiedDate = DateTime.Now;
    //        //Employee.UserPassword = New_Password;

    //        //context.SaveChanges();
    //        #region UserAccess

    //        string Result = string.Empty;
    //        UserAccess ua = new UserAccess();
    //        Result = ua.ChangePassword(EmployeeId, old_Password, New_Password);
    //        if (Result != string.Empty)
    //        {
    //            string message = "";
    //            message = "AlertBox('Success!',' Updated Successfully','success');";
    //    ScriptManager.RegisterStartupScript(this, GetType(), message, message, true);
    //        }

    //           // MessageCtrl.showMessageBox(Result, MessageType.Success);
    //           // MessageCtrl.showMessageBox("Password has been changed successfully.", MessageType.Success);
    //        //var TMSEntry = entities.UPDATE_TMS_PASSWORD(EmployeeId_);
    //        ua.UpdateUserHistory(EmployeeId, true);

    //        #endregion
    //    }
    //    catch (Exception ex)
    //    {

    //        LB_Error.Visible = true;
    //        LB_Error.Text = ex.Message != null? ex.Message: "Old Password is Wrong ";
    //        RightContentError.Attributes.Add("class", "validationSummary");
    //        RightContentError.Visible = true;

    //    }



    //}

    //public void setcontrol()
    //{
    //    LB_Error.Visible = false;
    //    RightContentError.Visible = false;
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CurrentPassword.Text = "";
        }
    }
    protected void ChangePasswordPushButton_Click(object sender, EventArgs e)
    {
        try
        {
            string old_Password = CurrentPassword.Text.Trim();
            if (old_Password != "")
            {

                string url = "/Pages/Default.aspx";// "/Pages/HRMS/EmployeeDetail.aspx?id=" + baseClass.UserKey;
                string CurrentPassword_GetHash = Security.Generate_Password(old_Password);
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
                                    string NewPassword_GetHash = Security.Generate_Password(New_Password);
                                    DataTable dt1 = CommonHelper.usp_UserLogin_UpdatePassword(baseClass.UserId, CurrentPassword_GetHash, NewPassword_GetHash, false, baseClass.UserIP, false, baseClass.UserId);
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
                                            Success("Password has been changed successfully.");
                                        }
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
                Error("Please enter old password");
            }
        }
        catch (Exception ex)
        {
            string mesg = ex.Message != null ? ex.Message : "Old Password is Wrong ";
            Error(mesg);
        }
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
    public void SuccessRedirect(string message, string url)
    {
        message = "AlertBoxRedirect('Success!','" + message + "','success','" + url + "');";
        ScriptManager.RegisterStartupScript(this, GetType(), message, message, true);
    }
}