using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;


/// <summary>
/// Summary description for UserAccess
/// </summary>
public class UserAccess
{
    public UserAccess()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string PasswordExpirationLimit = System.Configuration.ConfigurationManager.AppSettings["PasswordExpiration"];
    public static string TryLock = System.Configuration.ConfigurationManager.AppSettings["TryLock"];

    public void UpdateUserHistory(int UId, bool IsSuccess)
    {
        using (Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities())
        {

            UserLoginHistory obj = new UserLoginHistory();
            obj.CreatedBy = UId;
            obj.CreatedDate = DateTime.Now;
            obj.UserId = UId;
            obj.ApplicationId = (int)Constant.Application.HRMS;
            obj.IsSuccess = IsSuccess;
            obj.IsActive = true;
            obj.UserIP = HttpContext.Current.Request.UserHostAddress;
            context.UserLoginHistories.Add(obj);
            context.SaveChanges();
        }
    }



    public string ChangePassword(int UId, string OldPassword, string NewPassword)
    {
        using (Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities())
        {
            if (OldPassword == NewPassword)
            {
                throw new Exception("Already used password. Please try again.");
            }
            else
            {
                List<UserPasswordHistory> obj = context.UserPasswordHistories.Where(u => u.UserId == UId && u.Password == NewPassword).ToList();
                if (obj.Count > 0)
                {
                    throw new Exception("Already used password. Please try again.");
                }
                else
                {
                    string HashPasswordOld = CommonHelper.GetHash(OldPassword);
                    string HashPasswordNew = CommonHelper.GetHash(NewPassword);
                    var Employee = context.Setup_User.FirstOrDefault(p => p.User_Code == UId && p.Password == HashPasswordOld && p.Is_Active == true);
                    if (Employee != null)
                    {
                        Employee.Modified_By = UId;
                        Employee.Modified_Date = DateTime.Now;
                        Employee.Password = HashPasswordNew;

                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Incorrect Old Password");
                    }

                    var Emp = context.Setup_Employee.FirstOrDefault(p => p.EmployeeId == Employee.EmployeeId && p.IsActive == true);
                    if (Emp != null)
                    {
                        Emp.Password = NewPassword;
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Incorrect Old Password");
                    }

                    UserPasswordHistory uph = new UserPasswordHistory();
                    uph.CreatedDate = DateTime.Now;
                    uph.CreatedBy = UId;
                    uph.UserId = UId;
                    uph.Password = OldPassword;
                    uph.HashPassword = HashPasswordOld;
                    uph.IsActive = true;
                    context.UserPasswordHistories.Add(uph);
                    context.SaveChanges();

                    return "Password has been changed successfully.";
                }
            }
        }
    }

    public string CheckPasswordExpiration(int UId)
    {
        using (Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities())
        {
            var obj = context.UserPasswordHistories.Where(u => u.UserId == UId && u.IsActive == true).OrderByDescending(a => a.ID).ToList();
            if (obj != null && obj.Count > 0)
            {
                double PasswordLimit = Convert.ToDouble(PasswordExpirationLimit);
                //DateTime ExpiryDate = obj.ModifiedDate == null ? (DateTime)obj.CreatedDate : (DateTime)obj.ModifiedDate;
                DateTime ExpiryDate = obj[0].ModifiedDate == null ? (DateTime)obj[0].CreatedDate : (DateTime)obj[0].ModifiedDate;
                ExpiryDate = ExpiryDate.AddDays(PasswordLimit);
                if (ExpiryDate.Date <= DateTime.Today)
                {
                    if (!CheckHasPageAccess(false))
                        return string.Empty;
                    else
                        return "~/pages/changepassword.aspx";
                }
                else
                    return string.Empty;

            }
            else
                return string.Empty;

        }
    }

    public string IsFirstLogin(int UId)
    {
        using (Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities())
        {
            int Count = context.UserPasswordHistories.Where(a => a.IsActive == true && a.UserId == UId).Count();
            if (Count > 0)
            {
                List<UserLoginHistory> obj = context.UserLoginHistories.Where(u => u.UserId == UId && u.IsActive == true).ToList();
                int count = obj.Count();
                if (count == 0)
                {
                    string DefaultPage = string.Empty;
                    if (!CheckHasPageAccess(true))
                    {

                        DefaultPage = (from a in context.Setup_User
                                       join b in context.Setup_ApplicationRoleMapping on a.User_Code equals b.User_Code
                                       join c in context.Setup_Role on b.Role_Code equals c.Role_Code
                                       where a.Is_Active == true && b.IsActive == true && c.Is_Active == true
                                       && a.User_Code == UId && c.ApplicationId == (int)Constant.Application.HRMS
                                       select c).FirstOrDefault().DefaultPage;
                        return DefaultPage;
                    }
                    else
                    {
                        return "~/Pages/Default.aspx";
                    }
                }
                else
                    return string.Empty;
            }
            else
            {
                return "~/Pages/HRMS/ChangePassword.aspx";
            }
        }
    }

    public string GetDefaultPage(int UId, int ApplicationId)
    {
        //using (Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities())
        {


            string DefaultPage = "~/Pages/Default.aspx";
            //if (UId > 0)
            //{
            //    DefaultPage = (from a in context.Setup_User
            //                   join b in context.Setup_ApplicationRoleMapping on a.User_Code equals b.User_Code
            //                   join c in context.Setup_Role on b.Role_Code equals c.Role_Code
            //                   where a.Is_Active == true && b.IsActive == true && c.Is_Active == true
            //                   && b.ApplicationId == ApplicationId
            //                   && a.User_Code == UId
            //                   select c).FirstOrDefault().DefaultPage;
            //}
            return DefaultPage;
        }
    }

    public string IsAccountLocked(int UId, int ApplicationId)
    {
        using (Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities())
        {
            List<UserLoginHistory> LoginsList = context.UserLoginHistories.Where(u => u.UserId == UId).OrderByDescending(o => o.ID).Take(3).ToList();
            int Hour = DateTime.Now.Hour;
            int NoOfTry = Convert.ToInt32(TryLock);
            int count = LoginsList.Where(u => u.IsSuccess == false && u.CreatedDate.Hour == Hour).Count();
            if (count == NoOfTry)
            {
                Setup_ApplicationRoleMapping User = context.Setup_ApplicationRoleMapping.FirstOrDefault(u => u.User_Code == UId && u.ApplicationId == ApplicationId && u.IsActive == true);
                User.IsLocked = true;
                context.SaveChanges();
                return "Your account is locked. Please contact HR";
            }
            else
                return string.Empty;
        }
    }

    public bool CheckHasPageAccess(bool IsChangePassword)
    {
        string url = "";
        if (IsChangePassword)
            url = "/pages/changepassword.aspx";
        else
            url = HttpContext.Current.Request.Url.PathAndQuery;

        int roleCode = int.Parse(new Base().RoleCode);
        Setup_RoleAccess roleAccess = new Sybrid_DatabaseEntities().Setup_RoleAccess.Include("Setup_MenuItem")
            .FirstOrDefault(r => r.Role_Code == roleCode && url.Contains(r.Setup_MenuItem.Menu_URL) == true);
        if (roleAccess == null || roleAccess.Has_Access == false)
        {
            return false;
        }
        else
        {
            return true;
        }

    }

    public void LogOut()
    {
        // remove session before sending to login page.
        HttpContext.Current.Session.Abandon();
        new Base().ExpireCookie();
        HttpContext.Current.Response.Redirect("/Login.aspx", true);
    }
}