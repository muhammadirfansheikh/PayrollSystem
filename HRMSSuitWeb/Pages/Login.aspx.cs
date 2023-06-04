using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

public partial class Pages_Login : Base
{
    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (!IsPostBack)
        {
            //Session.Abandon();
            //new Base().ExpireCookie();
            //Response.Redirect("/Login.aspx", true);
            if (UserId != 0)
            {
                //if (RoleCode == Convert.ToString((int)Constant.Role.JKLead) || RoleCode == Convert.ToString((int)Constant.Role.iCareVolunteer))
                //    Response.Redirect("/pages/case/viewcase.aspx", true);
                //else
                Response.Redirect("/Pages/Default.aspx", true);
            }
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            bool IsAuthenticated = false;
            string ErrorMessage = string.Empty;

            Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();
            string password = CommonHelper.GetHash(txtPassword.Text.Trim());
            int ApplicationId = (int)Constant.Application.HRMS; // SET APPLICATION ID HERE.
            Setup_User user = context.Setup_User.FirstOrDefault(u => u.Password == password && u.Login_ID == txtUserName.Text.Trim() && u.Is_Active == true);
            if (user == null)
            {
                IsAuthenticated = false;
                divform.Attributes.Add("class", "has-error");
                divError.Visible = true;
            }
            else
            {
                int UserCode = user.User_Code;
                Setup_ApplicationRoleMapping userRole = context.Setup_ApplicationRoleMapping.FirstOrDefault(u => u.ApplicationId == ApplicationId && u.User_Code == UserCode && u.IsActive == true);
                Base baseClass = new Base();
                baseClass.UserId = user.User_Code;
                baseClass.FullName = user.Full_Name;
                baseClass.UserKey = user.EmployeeId;
                Session["EmpId"] = user.EmployeeId;
                baseClass.RoleCode = userRole.Role_Code == null ? "" : userRole.Role_Code.ToString();
                Session["RoleCode"] = baseClass.RoleCode;
                baseClass.UserId = user.User_Code;
                baseClass.CompanyId = user.Setup_Employee != null ? Convert.ToInt32(user.Setup_Employee.CompanyId) : 0;
                baseClass.IsSuperAdmin = userRole.Role_Code == (int)Constant.Role.SuperAdmin ? true : false;
                baseClass.IsIncharge = userRole.Role_Code == (int)Constant.Role.Incharge ? true : false;
                baseClass.IsAdmin = userRole.Role_Code == (int)Constant.Role.Admin ? true : false;
                baseClass.IsEmployee = userRole.Role_Code == (int)Constant.Role.Employee ? true : false;
                baseClass.DepatmentId = user.Setup_Employee.DepartmentId != null ? Convert.ToInt32(user.Setup_Employee.DepartmentId) : 0;
                baseClass.UserImage = user.Setup_Employee.PictureName == null ? "noprofilepic.png" : user.Setup_Employee.PictureName;
                baseClass.Designation = user.Setup_Employee.Setup_Designation.DesignationName;
                baseClass.RoleTats = userRole.Setup_Role.TAT.ToString();
                baseClass.EmployeeCode = user.Setup_Employee.EmployeeCode.ToString();
                IsAuthenticated = true;
            }

            context = null;
            ErrorMessage = Logging(txtUserName.Text.Trim(), IsAuthenticated);
            if (ErrorMessage != string.Empty)
            {
                Base baseClass = new Base();
                baseClass.UserId = 0;
                baseClass.FullName = "";
                baseClass.UserKey = 0;
                Session["EmpId"] = 0;
                baseClass.RoleCode = "";
                Session["RoleCode"] = "";
                baseClass.CompanyId = 0;
                baseClass.IsSuperAdmin = false;
                baseClass.IsIncharge = false;
                baseClass.IsAdmin = false;
                baseClass.IsEmployee = false;
                baseClass.DepatmentId = 0;
                
                lblError.Text = ErrorMessage;
                lblError.Visible = true;
            }
            else
            {
                if (IsAuthenticated == true)
                {
                    if (Request["returnurl"] != null)
                    {
                        string url = Request["returnurl"].ToString();
                        Response.Redirect(url);
                    }
                    if (ErrorMessage == string.Empty)
                    {
                        Response.Redirect(new UserAccess().GetDefaultPage(new Base().UserId,
                            (int)Constant.Application.HRMS));
                    }
                }
            }

        }
    }
    #region UserAccess
    private string Logging(string username, bool IsAuthenticated)
    {

        using (Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities())
        {
            //Setup_User usr = context.Setup_User.FirstOrDefault(u => u.Login_ID == username && u.Is_Active == true)
            //    .Setup_ApplicationRoleMapping.Where(u => u.IsActive == true && u.IsLocked == false);

            Setup_User usr = (from a in context.Setup_User
                              join b in context.Setup_ApplicationRoleMapping on a.User_Code equals b.User_Code
                              where a.Is_Active == true && b.IsActive == true
                                  //&& (b.IsLocked == false || b.IsLocked == null)
                              && a.Login_ID == username
                              && b.ApplicationId == (int)Constant.Application.HRMS
                              select a).FirstOrDefault();



            if (usr != null)
            {

                string Result = string.Empty, IsAccountLocked = string.Empty;
                UserAccess ua = new UserAccess();
                if (IsAuthenticated)
                    Result = ua.IsFirstLogin(usr.User_Code);
                ua.UpdateUserHistory(usr.User_Code, IsAuthenticated);
                //IsAccountLocked = ua.IsAccountLocked(usr.User_Code, (int)Constant.Application.HRMS);

                if (Result != string.Empty)
                {
                    Response.Redirect(Result);
                }




                //if (IsAccountLocked != string.Empty)
                //{
                //    return IsAccountLocked;
                //}
                //else
                //{
                //    return string.Empty;
                //}
            }
            //else
            //{
            //    Setup_User LockedUser = (from a in context.Setup_User
            //                             join b in context.Setup_ApplicationRoleMapping on a.User_Code equals b.User_Code
            //                             where a.Is_Active == true && b.IsActive == true
            //                             && b.IsLocked == true && a.Login_ID == username
            //                             && b.ApplicationId == (int)Constant.Application.HRMS
            //                             select a).SingleOrDefault();
            //    //UserLogin LockedUser = context.SetUserLogins.FirstOrDefault(u => u.LoginId == username && u.IsActive == true && u.IsLocked == true);
            //    if (LockedUser != null)
            //        return "Your account is locked. Please contact HR";
            //    else
            //        return string.Empty;
            //}
        }

        return string.Empty;
    }
    #endregion
    protected void btnForgetPassword_Click(object sender, EventArgs e)
    {
        try
        {
            string User_Name = txtLoginId.Text.Trim();
            Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();
            var GetUser = context.Setup_User.First(p => p.Login_ID == User_Name && p.Is_Active == true);
            string msg = "";
            msg += "Dear " + GetUser.Setup_Employee.FirstName.Trim() + " " + GetUser.Setup_Employee.LastName.Trim() + ",<br/><br/>";
            msg += "Your Password Is : <b>" + GetUser.Setup_Employee.Password + "</b>";
            msg += "<br/><br/>Regards,<br/>TMS Support Team";

            if (GetUser.Setup_Employee.OfficeEmailAddress.Trim() != "")
            {
                Email.SendMail(GetUser.Setup_Employee.OfficeEmailAddress, "Forgot Password", msg, "");//GetUser.Login_ID
                Success("Password has been sent to email address : " + GetUser.Setup_Employee.OfficeEmailAddress);
            }
            else
            {
                Error("Your password cannot be emailed because your official email address is not exists in HRMS, kindly contact to HR.");
                //ChangePasswordStatus.Text = ;
            }


            //var EMP_REC = (from emp in context.Employees
            //               join usr in context.Users on emp.EmployeeId equals usr.EmployeeId
            //               where (usr.Login_ID == User_Name && usr.Is_Active == true && emp.IsActive == true)
            //               select new { emp, usr });

            ////  string password = Decrypt(GetUser.Password.ToString() , "md5");

            //string msg = "Your Password Is : " + EMP_REC.First().emp.Password;
            //Email.SendMail(EMP_REC.First().emp.EmailID, "Forgot Password", msg, "");//GetUser.Login_ID
            //ChangePasswordStatus.Text = "Password Has Been Sent To Your Email";
        }
        catch
        {
            Error("User Does Not Exist");
            //ChangePasswordStatus.Text = "User Does Not Exist";
        }
        /* if (GetUser.User_Code != 0)
         {
             ForgotPassword ForgotPass = new ForgotPassword();

             try
             {
                 ForgotPass = context.ForgotPasswords.First(p => p.User_Code == GetUser.User_Code && p.IsActive == true);
             }
             catch
             {

                 changePaswordCode = Guid.NewGuid();
                 //ChangePasswordStatus.Text = changePaswordCode.ToString();
                 ForgotPass.ChangePasswordCode = changePaswordCode.ToString();
                 ForgotPass.User_Code = GetUser.User_Code;
                 ForgotPass.IsActive = true;
                 context.ForgotPasswords.Add(ForgotPass);
                 context.SaveChanges();
             }
             try
             {
                 string msg = "To Change Password <a href='" + SiteUrl + "/SetNewPassword.aspx?key=" + ForgotPass.ChangePasswordCode + "'> Click Here</a> ";
                 

                 Email.SendMail(GetUser.Login_ID, "Forgot Password", msg, GetUser.Login_ID);
             }
             catch (Exception ex)
             {
             //    ChangePasswordStatus.Text = "Email Sending Fail.";
                 FailureText.Text = "Email Sending Fail.";
             }
         }
         else
         {

           //  ChangePasswordStatus.Text = "User Does Not Exist."; 
             FailureText.Text = "User Does Not Exist.";
         }*/
        ClosePopup();
    }
    public void ClosePopup()
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "ClosePopup()", "ClosePopup();", true);
    }
    public void Success(string message)
    {
        message = "AlertBox('Success!','" + message + "','success');";
        ScriptManager.RegisterStartupScript(this, GetType(), message, message, true);
    }
    public void Error(string message)
    {
        message = "AlertBox('Error!','" + message + "','error');";
        ScriptManager.RegisterStartupScript(this, GetType(), message, message, true);
    }
}