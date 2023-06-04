using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : Base
{
    Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();
    string DefaultPage = "~/Pages/Default.aspx";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblYear.Text = DateTime.Now.Year - 1 + "-" + DateTime.Now.Year;
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        //DataTable dtLogin = UserLogin();
        //for (int i = 0; i < dtLogin.Rows.Count; i++)
        //{
        //    int EmployeeId = Convert.ToInt32(dtLogin.Rows[i]["EmployeeId"]);
        //    Setup_User user = context.Setup_User.FirstOrDefault(x => x.Is_Active == true && x.EmployeeId == EmployeeId);
        //    if(EmployeeId == user.EmployeeId)
        //    {
        //        string password = Security.Generate_Password(dtLogin.Rows[i]["Password"].ToString());
        //        user.Password = password;
        //        context.SaveChanges();
        //    }
        //} 
        try
        {
            if (IsValid)
            {
                int User_Code = 0;
                bool IsAuthenticated = false;
                string ErrorMessage = string.Empty;
                DataTable dtLogin = UserLogin();
                if (dtLogin != null && dtLogin.Rows.Count > 0)
                {
                    Base baseClass = new Base();
                    baseClass.CompanyId = Convert.ToInt32(Convert.ToString(dtLogin.Rows[0]["CompanyId"]));
                    User_Code = Convert.ToInt32(Convert.ToString(dtLogin.Rows[0]["User_Code"]));
                    baseClass.UserId = Convert.ToInt32(Convert.ToString(dtLogin.Rows[0]["User_Code"]));
                    baseClass.UserKey = Convert.ToInt32(Convert.ToString(dtLogin.Rows[0]["EmployeeId"]));
                    baseClass.RoleCode = Convert.ToString(dtLogin.Rows[0]["Role_Code"]);
                    baseClass.DepatmentId = Convert.ToString(dtLogin.Rows[0]["DepartmentId"]) != null ? Convert.ToInt32(Convert.ToString(dtLogin.Rows[0]["DepartmentId"])) : 0;
                    baseClass.FullName = Convert.ToString(dtLogin.Rows[0]["Full_Name"]);
                    baseClass.Designation = Convert.ToString(dtLogin.Rows[0]["DesignationName"]);
                    baseClass.UserImage = Convert.ToString(dtLogin.Rows[0]["PictureName"]) == "" ? "noprofilepic.png" : Convert.ToString(dtLogin.Rows[0]["PictureName"]);
                    baseClass.IsSuperAdmin = Convert.ToInt32(Convert.ToString(dtLogin.Rows[0]["Role_Code"])) == (int)Constant.Role.SuperAdmin ? true : false;
                    baseClass.IsHcmSuperAdmin = Convert.ToInt32(Convert.ToString(dtLogin.Rows[0]["Role_Code"])) == (int)Constant.Role.HCMSuperAdmin ? true : false;
                    baseClass.IsAdmin = Convert.ToInt32(Convert.ToString(dtLogin.Rows[0]["Role_Code"])) == (int)Constant.Role.Admin ? true : false;
                    baseClass.IsIncharge = Convert.ToInt32(Convert.ToString(dtLogin.Rows[0]["Role_Code"])) == (int)Constant.Role.Incharge ? true : false;
                    baseClass.IsEmployee = Convert.ToInt32(Convert.ToString(dtLogin.Rows[0]["Role_Code"])) == (int)Constant.Role.Employee ? true : false;
                    Session["EmpId"] = Convert.ToInt32(Convert.ToString(dtLogin.Rows[0]["EmployeeId"]));
                    Session["RoleCode"] = Convert.ToString(dtLogin.Rows[0]["Role_Code"]);
                    IsAuthenticated = true;
                }
                else
                {
                    IsAuthenticated = false;
                    divform.Attributes.Add("class", "has-error");
                    divError.Visible = true;
                }
                UserLoginHistory(User_Code, IsAuthenticated);
                if (IsAuthenticated == true)
                {
                    Response.Redirect(DefaultPage);
                }
            }
        }
        catch
        { }
    }

    private void UserLoginHistory(int User_Code, bool Authenticated)
    {
        try
        {
            if (User_Code > 0)
            {
                UserAccess ua = new UserAccess();
                ua.UpdateUserHistory(User_Code, Authenticated);
            }
        }
        catch
        {

        }
    }

    private DataTable UserLogin()
    {
        try
        {
            string UserName = txtUserName.Text.Trim();
            //string password = CommonHelper.GetHash(txtPassword.Text.Trim());
            string password = Security.Generate_Password(txtPassword.Text.Trim());
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("uspLogin", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = UserName;
            da.SelectCommand.Parameters.Add("@Password", SqlDbType.NVarChar).Value = password;
            da.Fill(dt);
            return dt;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    protected void btnForgetPassword_Click(object sender, EventArgs e)
    {
        try
        {
            
            string User_Name = txtLoginId.Text.Trim();
            Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();
            var GetUser = context.Setup_User.Where(p => p.Login_ID == User_Name && p.Is_Active == true).ToList();
            if (GetUser.Count > 0)
            {
                DataTable dt = Security.ResetPasswordEmailLink(txtLoginId.Text.Trim());
                if (dt != null && dt.Rows.Count > 0)
                {
                    string Message = dt.Rows[0]["Message"].ToString();
                    if (dt.Rows[0]["Status"].ToString() == "1")
                    {
                        ClosePopup();
                        Success(Message);
                    }
                    else
                    {
                        Error(Message);
                    }
                }
                else
                {
                    Error("Invalid Login ID");
                }
            }
            else
            {
                Error("Invalid Login ID");
            }
        }
        catch (Exception ex)
        {
            Error(ex.Message);
        }
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