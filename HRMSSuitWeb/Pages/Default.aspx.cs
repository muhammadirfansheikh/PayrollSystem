using System;
using DAL;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Pages_Default : Base
{
    Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        var Emp = context.Setup_Employee.Where(x => x.EmployeeId == UserKey).FirstOrDefault();
        if (Emp != null)
        {
            lblEmployeeName.Text = Emp.FirstName + " " + Emp.MiddleName + " " + Emp.LastName;
            lblEmpCode.Text = Emp.EmployeeCode;
            lblDesignation.Text = Emp.Setup_Designation.DesignationName;
            lblOfficialEmail.Text = Emp.OfficeEmailAddress;
            lblDepartment.Text = Emp.Setup_Department.DepartmentName;
            if (Emp.PictureName != "" && Emp.PictureName != null)
            {
                imgEmployeeImage.ImageUrl = Convert.ToString(ConfigurationManager.AppSettings["EmployeeImagePath"]) + Emp.PictureName;
            }
        }
    }

}