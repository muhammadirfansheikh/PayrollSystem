using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

public partial class Pages_HRMS_Setup_EmployeeCompanyMapping : Base
{
    Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropdown();
            //BindRepeater();
            BindRepeater();
        }
    }

    private void BindDropdown()
    {
        var LstGroup = context.Setup_UserAccessLocation.Where(a => a.IsActive == true && a.EmployeeId == UserKey)
                        .Select(a => new
                        {
                            GroupId = a.Setup_Location.Setup_Company.Setup_Group.GroupId,
                            GroupName = a.Setup_Location.Setup_Company.Setup_Group.GroupName,
                        })
                        .ToList().Distinct();
        CommonHelper.BindDropDown(ddlGroup, LstGroup, "GroupName", "GroupId", false, false);
        CommonHelper.BindDropDown(ddlGroupAdd, LstGroup, "GroupName", "GroupId", false, false);

        ddlGroupAdd_SelectedIndexChanged(null, null);
        ddlGroup_SelectedIndexChanged(null, null);
    }
    protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        int GroupId = Convert.ToInt32(ddlGroup.SelectedValue);
        var LstCompany = context.Setup_UserAccessLocation.Where(a => a.IsActive == true && a.EmployeeId == UserKey && a.Setup_Location.Setup_Company.GroupId == GroupId)
            .Select(a => new
            {
                CompanyId = a.Setup_Location.Setup_Company.CompanyId,
                CompanyName = a.Setup_Location.Setup_Company.CompanyName,

            })
            .ToList().Distinct();

        CommonHelper.BindDropDown(ddlCompany, LstCompany, "CompanyName", "CompanyId", true, false);
    }
    protected void ddlGroupAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        int GroupId = Convert.ToInt32(ddlGroupAdd.SelectedValue);
        var LstCompany = context.Setup_UserAccessLocation.Where(a => a.IsActive == true && a.EmployeeId == UserKey && a.Setup_Location.Setup_Company.GroupId == GroupId)
            .Select(a => new
            {
                CompanyId = a.Setup_Location.Setup_Company.CompanyId,
                CompanyName = a.Setup_Location.Setup_Company.CompanyName,

            })
            .ToList().Distinct();
        CommonHelper.BindCheckBoxList(cbCompanyAdd, LstCompany, "CompanyName", "CompanyId", LstCompany.Count() > 1 ? true : false, false);

        //CommonHelper.BindDropDown(ddlCompanyAdd, LstCompany, "CompanyName", "CompanyId", true, false);
        //ddlCompanyAdd_SelectedIndexChanged(null, null);
    }
    //protected void ddlCompanyAdd_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    int CompanyId = Convert.ToInt32(ddlCompanyAdd.SelectedValue);
    //    var LstLocation = context.Setup_UserAccessLocation.Where(a => a.IsActive == true && a.EmployeeId == UserKey && a.Setup_Location.CompanyId == CompanyId)
    //       .Select(a => new
    //       {
    //           LocationId = a.Setup_Location.LocationId,
    //           LocationName = a.Setup_Location.LocationName,

    //       })
    //       .ToList();
    //    CommonHelper.BindCheckBoxList(cbLocationsAdd, LstLocation, "LocationName", "LocationId", LstLocation.Count > 1 ? true : false, false);

    //    //CommonHelper.BindDropDown(ddlLocation, LstLocation, "LocationName", "LocationId", true, false);

    //    var ListDepartment = context.Setup_Department.Where(c => c.IsActive == true && c.Setup_Company.CompanyId == CompanyId).OrderBy(c => c.DepartmentName).ToList();
    //    CommonHelper.BindDropDown(ddlDeptAdd, ListDepartment, "DepartmentName", "DepartmentId", true, false);
    //    ddlDeptAdd_SelectedIndexChanged(null, null);
    //}
    //protected void ddlDeptAdd_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    int departmentid = Convert.ToInt32(ddlDeptAdd.SelectedValue);
    //    var listEmp = context.Setup_Employee.Where(a => a.IsActive == true && a.DepartmentId == departmentid)
    //         .Select(a => new
    //         {
    //             EmployeeName = a.EmployeeCode + " - " + a.FirstName == "" ? null : a.EmployeeCode + " - " + a.FirstName + " " + a.LastName,
    //             EmployeeId = a.EmployeeId,
    //         }
    //       ).ToList();
    //    //CommonHelper.BindDropDown(ddlUserAdd, listEmp, "EmployeeName", "EmployeeId", true, false);
    //}
    //protected void ddlUserAdd_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    int empID = Convert.ToInt32(ddlUserAdd.SelectedValue);
    //    //   var listLocation = context.Setup_UserAccessLocation.Where(c => c.IsActive == true && (c.EmployeeId == empID))
    //    //.Select(a => new
    //    //{
    //    //    LocationId = a.LocationId,
    //    //    LocationName = a.Setup_Location.LocationName,
    //    //}
    //    //).ToList();
    //    //var departmentList = context.Setup_UserAccessLocation.Where(c => c.IsActive == true && c.EmployeeId == empID).ToList();
    //    cbLocationsAdd.ClearSelection();
    //    var LstLocation = context.Setup_UserAccessLocation.Where(a => a.IsActive == true && a.EmployeeId == empID && a.Setup_Location.CompanyId == CompanyId)
    //       .ToList();
    //    for (int i = 0; i < LstLocation.Count; i++)
    //    {
    //        cbLocationsAdd.Items.FindByValue(LstLocation[i].LocationId.ToString()).Selected = true;
    //    }

    //    cbLocationsAdd.Items[0].Selected = cbLocationsAdd.Items.Count - 1 == LstLocation.Count;

    //    //   CommonHelper.BindDropDown(ddlLocation, listLocation, "LocationName", "LocationId", true, false);
    //    //   //CommonHelper.BindDropDown(ddlUserAdd, listEmp, "EmployeeName", "EmployeeId", true, false);

    //}
    //protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    int departmentid = Convert.ToInt32(ddlDepartment.SelectedValue);
    //    var listEmp = context.Setup_Employee.Where(a => a.IsActive == true && a.DepartmentId == departmentid)
    //         .Select(a => new
    //         {
    //             EmployeeName = a.EmployeeCode + " - " + a.FirstName == "" ? null : a.EmployeeCode + " - " + a.FirstName + " " + a.LastName,
    //             EmployeeId = a.EmployeeId,
    //         }
    //       ).ToList();
    //    CommonHelper.BindDropDown(ddlUser, listEmp, "EmployeeName", "EmployeeId", true, false);

    //}
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        BindRepeater();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindRepeater();
    }
    protected void lbEdit_Click(object sender, EventArgs e)
    {

    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {

    }
    private void BindRepeater()
    {
        int CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        var list = context.Setup_Employee.Where(x => x.IsActive == true)
            .Where(x => x.Setup_EmployeeCompanyMapping.Count > 0 ? x.Setup_EmployeeCompanyMapping.Any(y => y.IsActive == true && y.EmployeeId == x.EmployeeId && (CompanyId == 0 || x.CompanyId == CompanyId)) : true)
            .ToList()
            .Select(x => new
            {
                EmployeeID = x.EmployeeId,
                EmployeeName = x.FirstName + " " + x.LastName,
                DepartmentName = x.Setup_Department.DepartmentName,
                Companies = String.Join(", ", x.Setup_EmployeeCompanyMapping.Where(aa => aa.IsActive == true).Select(t => t.Setup_Company.CompanyName)),

            });
        rpt.DataSource = list;
        rpt.DataBind();
    }
}