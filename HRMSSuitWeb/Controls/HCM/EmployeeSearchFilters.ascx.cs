using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_HCM_EmployeeSearchFilters : System.Web.UI.UserControl
{
    public int EmployeeCode
    {
        get { return Convert.ToInt32(HasEmployeeCode.Value == "" ? "0" : HasEmployeeCode.Value); }
        set { HasEmployeeCode.Value = value.ToString(); }
    }
    public int EmployeeType
    {
        get { return Convert.ToInt32(HasEmployeeType.Value == "" ? "0" : HasEmployeeType.Value); }
        set { HasEmployeeType.Value = value.ToString(); }
    }
    public int Location
    {
        get { return Convert.ToInt32(HasLocation.Value == "" ? "0" : HasLocation.Value); }
        set { HasLocation.Value = value.ToString(); }
    }
    public int BusinessUnit
    {
        get { return Convert.ToInt32(HasBusinessUnit.Value == "" ? "0" : HasBusinessUnit.Value); }
        set { HasBusinessUnit.Value = value.ToString(); }
    }
    public int Department
    {
        get { return Convert.ToInt32(HasDepartment.Value == "" ? "0" : HasDepartment.Value); }
        set { HasDepartment.Value = value.ToString(); }
    }
    public int CostCenter
    {
        get { return Convert.ToInt32(HasCostCenter.Value == "" ? "0" : HasCostCenter.Value); }
        set { HasCostCenter.Value = value.ToString(); }
    }

    public int Sap_CostCenter
    {
        get { return Convert.ToInt32(HasSapCostCenter.Value == "" ? "0" : HasSapCostCenter.Value); }
        set { HasSapCostCenter.Value = value.ToString(); }
    }
    public int JobCategory
    {
        get { return Convert.ToInt32(HasJobCategory.Value == "" ? "0" : HasJobCategory.Value); }
        set { HasJobCategory.Value = value.ToString(); }
    }
    public int Designation
    {
        get { return Convert.ToInt32(HasDesignation.Value == "" ? "0" : HasDesignation.Value); }
        set { HasDesignation.Value = value.ToString(); }
    }
    public int FirstName
    {
        get { return Convert.ToInt32(HasFirstName.Value == "" ? "0" : HasFirstName.Value); }
        set { HasFirstName.Value = value.ToString(); }
    }
    public int LastName
    {
        get { return Convert.ToInt32(HasLastName.Value == "" ? "0" : HasLastName.Value); }
        set { HasLastName.Value = value.ToString(); }
    }


    protected void Page_Load(object sender, EventArgs e)
    {

    }
}