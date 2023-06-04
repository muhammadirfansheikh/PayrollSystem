using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IO.Compression;

/// <summary>
/// Summary description for Base
/// </summary>
public class Base : System.Web.UI.Page
{
    public string GetSecurityKey()
    {
        string key = GetCookie("Payroll_sesk");
        if (key != null)
            return key;
        else
        {
            key = Guid.NewGuid().ToString();
            SaveCookie("Payroll_sesk", key);
            return key;
        }
    }
    public string Token { get; set; }
    public int UserId
    {
        get
        {
            if (string.IsNullOrEmpty(GetCookie("Payroll_UserId")))
                return 0;
            else
                return int.Parse(GetCookie("Payroll_UserId"));
        }
        set { SaveCookie("Payroll_UserId", value.ToString()); }
    }
    public string UserImage
    {
        get
        {
            if (string.IsNullOrEmpty(GetCookie("Payroll_UserImage")))
                return "";
            else
                return (GetCookie("Payroll_UserImage"));
        }
        set { SaveCookie("Payroll_UserImage", value.ToString()); }
    }
    public string Designation
    {
        get
        {
            if (string.IsNullOrEmpty(GetCookie("Payroll_Designation")))
                return "";
            else
                return (GetCookie("Payroll_Designation"));
        }
        set { SaveCookie("Payroll_Designation", value.ToString()); }
    }
    public string FullName
    {
        get { return GetCookie("Payroll_fullname"); }
        set { SaveCookie("Payroll_fullname", value); }
    }
    public string EmailAddress
    {
        get { return GetCookie("Payroll_emailaddress"); }
        set { SaveCookie("Payroll_emailaddress", value); }
    }
    public string RoleCode
    {
        get { return GetCookie("Payroll_rolecode"); }
        set { SaveCookie("Payroll_rolecode", value); }
    }
    public string RoleTats
    {
        get { return GetCookie("Payroll_roletats"); }
        set { SaveCookie("Payroll_roletats", value); }
    }
    public  int UserKey
    {
        get
        {
            if (string.IsNullOrEmpty(GetCookie("Payroll_userkey")))
                return 0;
            else
                return int.Parse(GetCookie("Payroll_userkey"));
        }
        set { SaveCookie("Payroll_userkey", value.ToString()); }
    }
    public string EmployeeCode
    {
        get { return GetCookie("Payroll_employeecode"); }
        set { SaveCookie("Payroll_employeecode", value); }
    }
    public int CompanyId
    {
        get
        {
            if (string.IsNullOrEmpty(GetCookie("Payroll_CompanyId")))
                return 0;
            else
                return int.Parse(GetCookie("Payroll_CompanyId"));
        }
        set { SaveCookie("Payroll_CompanyId", value.ToString()); }
    }
    public int DepatmentId
    {
        get
        {
            if (string.IsNullOrEmpty(GetCookie("Payroll_DepartmentId")))
                return 0;
            else
                return int.Parse(GetCookie("Payroll_DepartmentId"));
        }
        set { SaveCookie("Payroll_DepartmentId", value.ToString()); }
    }
    public bool IsSuperAdmin
    {
        get
        {
            if (string.IsNullOrEmpty(GetCookie("Payroll_issuperadmin")))
                return false;
            else
                return bool.Parse(GetCookie("Payroll_issuperadmin"));
        }
        set { SaveCookie("Payroll_issuperadmin", value.ToString()); }
    }
    public bool IsIncharge
    {
        get
        {
            if (string.IsNullOrEmpty(GetCookie("Payroll_isincharge")))
                return false;
            else
                return bool.Parse(GetCookie("Payroll_isincharge"));
        }
        set { SaveCookie("Payroll_isincharge", value.ToString()); }
    }
    public bool IsAdmin
    {
        get
        {
            if (string.IsNullOrEmpty(GetCookie("Payroll_isadmin")))
                return false;
            else
                return bool.Parse(GetCookie("Payroll_isadmin"));
        }
        set { SaveCookie("Payroll_isadmin", value.ToString()); }
    }
    public bool IsEmployee
    {
        get
        {
            if (string.IsNullOrEmpty(GetCookie("Payroll_IsEmployee")))
                return false;
            else
                return bool.Parse(GetCookie("Payroll_IsEmployee"));
        }
        set { SaveCookie("Payroll_IsEmployee", value.ToString()); }
    }
    public bool IsHcmSuperAdmin
    {
        get
        {
            if (string.IsNullOrEmpty(GetCookie("Payroll_IsHcmSuperAdmin")))
                return false;
            else
                return bool.Parse(GetCookie("Payroll_IsHcmSuperAdmin"));
        }
        set { SaveCookie("Payroll_IsHcmSuperAdmin", value.ToString()); }
    }
    public string UserIP
    {
        get { return Context.Request.UserHostAddress; }
    }
    public void LogError(Exception ex)
    {
        Base objBase = new Base();
        if (ex == null)
            ex = SetExceptionText();

        // Log In Database
        if (ValidateError(ex.Message))
        {
            Logging.WriteLog(LogType.Error, ex.Message);
        }

    }
    private bool ValidateError(string errorMessage)
    {
        // exclude common backend exceptions
        if (errorMessage == "File does not exist.")
            return false;
        else
            return true;
    }
    private Exception SetExceptionText()
    {
        Exception ex = HttpContext.Current.Server.GetLastError();
        if (ex != null)
        {
            if (ex.GetBaseException() != null)
            {
                ex = ex.GetBaseException();
                // lblError.Text = ex.Message;
            }
        }
        return ex;
    }
    public void SaveCookie(string strKey, string strValue)
    {
        if (HttpContext.Current.Request.Cookies[strKey] != null)
        {
            HttpContext.Current.Request.Cookies[strKey].Value = strValue;
        }
        else
        {
            HttpCookie cookie = new HttpCookie(strKey);
            cookie.Value = strValue;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
    public string GetCookie(string strKey)
    {
        if (HttpContext.Current.Request.Cookies[strKey] != null)
        {
            return HttpContext.Current.Request.Cookies[strKey].Value;
        }
        else
            return null;
    }
    public void ExpireCookie()
    {
        HttpRequest req = HttpContext.Current.Request;
        HttpResponse res = HttpContext.Current.Response;
        int count = req.Cookies.Count;
        for (int i = 0; i < count; i++)
        {
            HttpCookie cookie = new HttpCookie(req.Cookies[i].Name);
            cookie.Expires = DateTime.Now.AddDays(-1);
            res.Cookies.Add(cookie);
        }
    }
}

public static class UIExtensions
{
    public static void EnableCompression(this HttpResponse response)
    {
        response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
        response.AddHeader("Content-Encoding", "gzip");
    }
}