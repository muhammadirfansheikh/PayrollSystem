using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Data.SqlClient;
using OfficeOpenXml.DataValidation;
using System.IO;
using Ionic.Zip;

public partial class Pages_HCM_Report_IncrementSheet : Base
{
    Sybrid_DatabaseEntities context = new Sybrid_DatabaseEntities();
    Base baseclass = new Base();
    string LastCaption = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDown();
        }
    }
    private void BindDropDown()
    {
        ddlGroup.Enabled = false;
        var LstGroup = context.Setup_Group.Where(x => x.IsActive == true).OrderBy(x => x.GroupName).Select(s => new
        {
            GroupName = s.GroupName,
            GroupId = s.GroupId
        }).ToList();
        CommonHelper.BindDropDown(ddlGroup, LstGroup, "GroupName", "GroupId", LstGroup.Count == 1 ? false : true, false);
        ddlGroup_SelectedIndexChanged(null, null);
        ddlCompany_SelectedIndexChanged(null,null);
    }
    protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        int GroupId = Convert.ToInt32(ddlGroup.SelectedValue);
        var LstCompany = context.Setup_EmployeeCompanyMapping.Where(a => a.IsActive == true && a.EmployeeId == UserKey && a.Setup_Company.IsActive == true && a.Setup_Company.GroupId == GroupId)
            .Select(a => new
            {
                CompanyName = a.Setup_Company.CompanyName,
                CompanyId = a.Setup_Company.CompanyId
            }).OrderBy(b => b.CompanyName).ToList();

        CommonHelper.BindDropDown(ddlCompany, LstCompany, "CompanyName", "CompanyId", false, false);
        
        /// btnExport_Click(null, null);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtFromDate.Text = "";
        txtToDate.Text = "";
        ddlGroup.SelectedIndex = 0;
        ddlGroup_SelectedIndexChanged(null, null);
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {

           string _Employees = GetSelectedEmployees();
            DateTime _ToDate = Convert.ToDateTime(txtToDate.Text);
            DateTime _txtFromDate = Convert.ToDateTime(txtFromDate.Text);

            int _FromYear = !String.IsNullOrEmpty(txtFromDate.Text) ? Convert.ToDateTime(txtFromDate.Text).Year : 0;
            int _ToYear = !String.IsNullOrEmpty(txtToDate.Text) ? Convert.ToDateTime(txtToDate.Text).Year : 0;
            int _SelectedYear = !String.IsNullOrEmpty(txtYear.Text) ? Convert.ToInt32(txtYear.Text) : 0; ;
            int _FilterTypeId = Convert.ToInt32(ddlReportFilterType.SelectedValue);

            string _GetChecking = CheckYearValidation(_ToYear, _FromYear, _SelectedYear);

            if (String.IsNullOrEmpty(_GetChecking))
            {
                int _IsPermanent = 1;

                if (rdCheckStatus.SelectedValue == "0")
                    _IsPermanent = 0;


                int _CompnayId = ddlCompany.SelectedValue == "" ? 0 : Convert.ToInt32(ddlCompany.SelectedValue);
                if (_CompnayId > 0 && _ToDate != null)
                {

                    Sybrid_DatabaseEntities context_ = new Sybrid_DatabaseEntities();
                    string dbConnectionString = context_.Database.Connection.ConnectionString;
                    SqlConnection con = new SqlConnection(dbConnectionString);
                    SqlDataAdapter da = new SqlDataAdapter("HCM_RPT_Increment_Sheet", con);
                    da.SelectCommand.CommandTimeout = 360000;
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("CompanyId", SqlDbType.Int).Value = _CompnayId;

                    if (!string.IsNullOrEmpty(txtFromDate.Text))
                        da.SelectCommand.Parameters.Add("FromDate", SqlDbType.Date).Value = _txtFromDate;
                    else
                        da.SelectCommand.Parameters.Add("FromDate", SqlDbType.Date).Value = null;

                    da.SelectCommand.Parameters.Add("ToDate", SqlDbType.Date).Value = _ToDate;
                    da.SelectCommand.Parameters.Add("IsPermanent", SqlDbType.Int).Value = _IsPermanent;

                    da.SelectCommand.Parameters.Add("Year", SqlDbType.Int).Value = _SelectedYear;
                    da.SelectCommand.Parameters.Add("FilterByID", SqlDbType.Int).Value = _FilterTypeId;
                    da.SelectCommand.Parameters.Add("EmployeeIds", SqlDbType.NVarChar).Value = _Employees;
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables.Count == 2)
                    {
                        DataSet dsFianl = new DataSet();
                        DataTable dtMain = ds.Tables[0];
                        DataTable dt_AdditionalIncrement = ds.Tables[1];
                        string _PFFormula = dtMain.Rows[0]["Formula_PF"].ToString();
                        string _BonusFormula = dtMain.Rows[0]["Formula_Bonus"].ToString();

                        DataTable dtReporting_CostCenter = new DataTable();
                        dtReporting_CostCenter = dtMain.DefaultView.ToTable(true, "Reporting_CostCenter_Id");
                        for (int j = 0; j < dtReporting_CostCenter.Rows.Count; j++)
                        {
                            int Reporting_CostCenter_Id = Convert.ToInt32(dtReporting_CostCenter.Rows[j]["Reporting_CostCenter_Id"]);
                            DataView dvv = dtMain.DefaultView;
                            dvv.RowFilter = "Reporting_CostCenter_Id = '" + Reporting_CostCenter_Id + "'";
                            DataTable dt_Employee = dvv.ToTable();
                            if (dt_Employee != null && dt_Employee.Rows.Count > 0)
                            {
                                #region Creating Table and Header
                                DataTable dt_table = new DataTable();
                                dt_table.Columns.Add("C0");
                                dt_table.Columns.Add("C1");
                                dt_table.Columns.Add("C2");
                                dt_table.Columns.Add("C3");
                                dt_table.Columns.Add("C4");
                                dt_table.Columns.Add("C5");
                                dt_table.Columns.Add("C6");
                                dt_table.Columns.Add("C7");
                                dt_table.Columns.Add("C8");
                                dt_table.Columns.Add("C9");
                                dt_table.Columns.Add("C10");
                                dt_table.Columns.Add("C11");
                                dt_table.Columns.Add("C12");
                                dt_table.Columns.Add("C13");
                                dt_table.Columns.Add("C14");
                                dt_table.Columns.Add("C15");
                                dt_table.Columns.Add("C16");
                                dt_table.Columns.Add("C17");
                                dt_table.Columns.Add("C18");
                                dt_table.Columns.Add("C19");
                                dt_table.Columns.Add("C20");
                                dt_table.Columns.Add("C21");
                                dt_table.Columns.Add("C22");
                                dt_table.Columns.Add("C23");
                                dt_table.Columns.Add("C24");
                                dt_table.Columns.Add("C25");
                                dt_table.Columns.Add("C26");
                                dt_table.Columns.Add("C27");
                                dt_table.Columns.Add("C28");
                                dt_table.Columns.Add("C29");
                                dt_table.Columns.Add("C30");
                                dt_table.Columns.Add("C31");
                                dt_table.Columns.Add("C32");
                                dt_table.Columns.Add("C33");
                                dt_table.Columns.Add("C34");
                                dt_table.Columns.Add("C35");


                                int Last_RowNumber = 0;

                                DataRow dr = dt_table.NewRow();
                                dr[0] = ddlCompany.SelectedItem.Text;
                                dt_table.Rows.InsertAt(dr, Last_RowNumber);

                                dr = dt_table.NewRow();

                                string _FromDate = "Not Provided";

                                if (!String.IsNullOrEmpty(txtFromDate.Text))
                                    _FromDate = Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd");
                               
                                if(_FilterTypeId == 1)
                                    dr[0] = "DEPARTMENT WISE INCREMENT PROPOSAL REPORT YEAR : " +txtYear.Text; //Need To Change Year.
                                else if (_FilterTypeId == 2)
                                    dr[0] = "LOCATION WISE INCREMENT PROPOSAL REPORT YEAR : " + txtYear.Text; //Need To Change Year.
                                else if (_FilterTypeId == 3)
                                    dr[0] = "COST CENTER WISE INCREMENT PROPOSAL REPORT YEAR : " + txtYear.Text; //Need To Change Year.
                                else if (_FilterTypeId == 4)
                                    dr[0] = "SAP COST CENTER WISE INCREMENT PROPOSAL REPORT YEAR : " + txtYear.Text; //Need To Change Year.
                                else
                                    dr[0] = "REPORTING COSTCENTER WISE INCREMENT PROPOSAL REPORT YEAR : " + txtYear.Text;

                                dt_table.Rows.InsertAt(dr, ++Last_RowNumber);

                                dr = dt_table.NewRow();
                                dr[0] = "";
                                dt_table.Rows.InsertAt(dr, ++Last_RowNumber);

                                dr = dt_table.NewRow();
                                dr[0] = "";
                                dt_table.Rows.InsertAt(dr, ++Last_RowNumber);

                                dr = dt_table.NewRow();
                                dr[0] = "Sr.#";
                                dr[1] = "EMP.#";
                                dr[2] = "NAME/DESIGNATION/D.O.J/QUALIFICATION";
                                dr[3] = "OTHER BENEFITS";
                                dr[4] = "";
                                dr[5] = "INCREMENT";
                                dr[6] = "";
                                dr[7] = "";
                                dr[8] = "";
                                dr[9] = "";
                                dr[10] = "";
                                dr[11] = "";
                                dr[12] = "";
                                dr[13] = "ADDITIONAL";
                                dr[14] = "";
                                dr[15] = "PRESENT SALARY";
                                dr[16] = "";
                                dr[17] = "";
                                dr[18] = "PERFORMANCE";
                                dr[19] = "PP RATING";
                                dr[20] = "PROPOSED INCREMENT";
                                dr[21] = "";
                                dr[22] = "";
                                dr[23] = "";
                                dr[24] = "";
                                dr[25] = "";
                                dr[26] = "";
                                dr[27] = "";
                                dr[28] = "";
                                dr[29] = "";
                                dr[30] = "NEW GROSS";
                                dr[31] = "NEW GROSS PACKAGE";
                                dr[32] = "GROSS PACKAGE PREVIOUS";
                                dr[33] = "INCREASE";
                                dr[34] = "";
                                dr[35] = "REMARKS";
                                dt_table.Rows.InsertAt(dr, ++Last_RowNumber);



                                dr = dt_table.NewRow();
                                dr[0] = "";
                                dr[1] = "";
                                dr[2] = "";
                                dr[3] = "";
                                dr[4] = "";
                                dr[5] = " YEAR";
                                dr[6] = "INFLATION";
                                dr[7] = "MERIT";
                                dr[8] = "ADJ.";
                                dr[9] = "PRO.";
                                dr[10] = "TOTAL";
                                dr[11] = "%";
                                dr[12] = "Rating";
                                dr[13] = "INCR.";
                                dr[14] = "W.E.F.";
                                dr[15] = "BASIC";
                                dr[16] = "ALLOW.";
                                dr[17] = "GROSS";
                                dr[18] = "";
                                dr[19] = "";
                                dr[20] = "INFLATION";
                                dr[21] = "%";
                                dr[22] = "MERIT";
                                dr[23] = "%";
                                dr[24] = "ADJ.";
                                dr[25] = "%";
                                dr[26] = "PRO.";
                                dr[27] = "%";
                                dr[28] = "TOTAL";
                                dr[29] = "%";
                                dr[30] = "Relocation/ Hardship Allw";
                                dr[31] = "";
                                dr[32] = "";
                                dr[33] = "";
                                dr[34] = "";
                                dr[35] = "";
                                dt_table.Rows.InsertAt(dr, ++Last_RowNumber);

                                dr = dt_table.NewRow();
                                dr[0] = "";
                                dr[1] = "";
                                dr[2] = "";
                                dr[3] = "";
                                dr[4] = "";
                                dr[5] = "";
                                dr[6] = "";
                                dr[7] = "";
                                dr[8] = "";
                                dr[9] = "";
                                dr[10] = "";
                                dr[11] = "";
                                dr[12] = "";
                                dr[13] = "";
                                dr[14] = "";
                                dr[15] = "";
                                dr[16] = "";
                                dr[17] = "";
                                dr[18] = "";
                                dr[19] = "";
                                dr[20] = "P.F.";
                                dr[21] = "";
                                dr[22] = "BONUS";
                                dr[23] = "";
                                dr[24] = "EOBI";
                                dr[25] = "";
                                dr[26] = "S.S";
                                dr[27] = "";
                                dr[28] = "MOBILE";
                                dr[29] = "CAR ALLOW.";
                                dr[30] = "";
                                dr[31] = "";
                                dr[32] = "";
                                dr[33] = "Amount";
                                dr[34] = "Age%";
                                dr[35] = "";
                                dt_table.Rows.InsertAt(dr, ++Last_RowNumber);
                                #endregion
                                for (int i = 0; i < dt_Employee.Rows.Count; i++)
                                {
                                    LastCaption = "";
                                    int EmployeeId = Convert.ToInt32(dt_Employee.Rows[i]["EmployeeId"]);
                                    bool HAS_ADDTIONAL_INCREMENT = Convert.ToBoolean(dt_Employee.Rows[i]["EmployeeId"]);
                                    DataView dv = dt_AdditionalIncrement.DefaultView;
                                    dv.RowFilter = "EmployeeId = '" + EmployeeId + "'";
                                    DataTable dt_EmployeeAdditionalIncrement = dv.ToTable();

                                    #region Employee Row 1
                                    string BONUS_PAID_DURING_THE_YEAR = dt_Employee.Rows[i]["BONUS_PAID_DURING_THE_YEAR"].ToString();
                                    string PERFORMANCE_BONUS_PAID_DURING_THE_YEAR = dt_Employee.Rows[i]["PERFORMANCE_BONUS_PAID_DURING_THE_YEAR"].ToString();
                                    string WPPF_PAID_DURING_THE_YEAR = dt_Employee.Rows[i]["WPPF_PAID_DURING_THE_YEAR"].ToString();
                                    string COMPANY_LOAN_GRANTED = dt_Employee.Rows[i]["COMPANY_LOAN_GRANTED"].ToString();
                                    string LOAN_BALANCE_AT_YEAR_END = dt_Employee.Rows[i]["LOAN_BALANCE_AT_YEAR_END"].ToString();
                                    string OVERTIME_PAID_DURING_THE_YEAR = dt_Employee.Rows[i]["OVERTIME_PAID_DURING_THE_YEAR"].ToString();
                                    string INCENTIVE_PAID_DURING_THE_YEAR = dt_Employee.Rows[i]["INCENTIVE_PAID_DURING_THE_YEAR"].ToString();
                                    dr = dt_table.NewRow();
                                    dr[0] = dt_Employee.Rows[i]["SERIAL_NO"].ToString();
                                    dr[1] = dt_Employee.Rows[i]["EMPLOYEE_CODE"].ToString();
                                    dr[2] = dt_Employee.Rows[i]["EMPLOYEE_NAME"].ToString();
                                    List<string> lstBONUS_PAID_DURING_THE_YEAR = GetDataWithOutZero("BONUS_PAID_DURING_THE_YEAR", BONUS_PAID_DURING_THE_YEAR, PERFORMANCE_BONUS_PAID_DURING_THE_YEAR
                                      , WPPF_PAID_DURING_THE_YEAR, COMPANY_LOAN_GRANTED, LOAN_BALANCE_AT_YEAR_END, OVERTIME_PAID_DURING_THE_YEAR
                                      , INCENTIVE_PAID_DURING_THE_YEAR);
                                    dr[3] = lstBONUS_PAID_DURING_THE_YEAR[0];
                                    dr[4] = lstBONUS_PAID_DURING_THE_YEAR[1];
                                    //dr[3] = dt_Employee.Rows[i]["BONUS_PAID_DURING_THE_YEAR_CAPTION"].ToString();
                                    //dr[4] = dt_Employee.Rows[i]["BONUS_PAID_DURING_THE_YEAR"].ToString();
                                    dr[5] = dt_Employee.Rows[i]["INCREMENT_YEAR_1"].ToString();
                                    dr[6] = dt_Employee.Rows[i]["INCREMENT_INFLATION_1"].ToString();
                                    dr[7] = dt_Employee.Rows[i]["INCREMENT_MERIT_1"].ToString();
                                    dr[8] = dt_Employee.Rows[i]["INCREMENT_ADJ_1"].ToString();
                                    dr[9] = dt_Employee.Rows[i]["INCREMENT_PRO_1"].ToString();
                                    dr[10] =dt_Employee.Rows[i]["INCREMENT_TOTAL_1"].ToString();
                                    dr[11] =dt_Employee.Rows[i]["INCREMENT_PERCENTAGE_1"].ToString();
                                    dr[12] =dt_Employee.Rows[i]["INCREMENT_RATING_1"].ToString();

                                    if (HAS_ADDTIONAL_INCREMENT == true)
                                    {
                                        if (dt_EmployeeAdditionalIncrement != null && dt_EmployeeAdditionalIncrement.Rows.Count > 0)
                                        {
                                            DataView dv_Filter = dt_EmployeeAdditionalIncrement.DefaultView;
                                            dv_Filter.RowFilter = "RowNum = '1'";
                                            DataTable dt_Filtered_AdditionalIncrement = dv_Filter.ToTable();
                                            if (dt_Filtered_AdditionalIncrement != null && dt_Filtered_AdditionalIncrement.Rows.Count > 0)
                                            {
                                                dr[13] = dt_Filtered_AdditionalIncrement.Rows[0]["ADDITIONAL_INCR"].ToString();
                                                dr[14] = dt_Filtered_AdditionalIncrement.Rows[0]["ADDITIONAL_INCR_WEF"].ToString();
                                            }
                                        }
                                    }

                                    dr[15] = "BASICSALARY_"+dt_Employee.Rows[i]["PRESENT_SALARY_BASIC_1"].ToString();
                                    dr[16] =  dt_Employee.Rows[i]["PRESENT_SALARY_ALLOW_1"].ToString();
                                    dr[17] = dt_Employee.Rows[i]["PRESENT_SALARY_GROSS_1"].ToString();
                                    dr[18] = "--- Select Rating ---";
                                    dr[19] = "";
                                    dr[20] = "";
                                    dr[21] = "";
                                    dr[22] = "";
                                    dr[23] = "";
                                    dr[24] = "";
                                    dr[25] = "";
                                    dr[26] = "";
                                    dr[27] = "";
                                    dr[28] = "";
                                    dr[29] = "";
                                    dr[30] = "";
                                    dr[31] = "";
                                    dr[32] = "";
                                    dr[33] = "";
                                    dr[34] = "";
                                    dr[35] = "";
                                    dt_table.Rows.InsertAt(dr, ++Last_RowNumber);
                                    #endregion

                                    #region Employee Row 2
                                   
                                    dr = dt_table.NewRow();
                                    dr[0] = "";
                                    dr[1] = "";
                                    dr[2] = dt_Employee.Rows[i]["DESIGNATION"].ToString();
                                    List<string> lstPERFORMANCE_BONUS_PAID_DURING_THE_YEAR = GetDataWithOutZero("PERFORMANCE_BONUS_PAID_DURING_THE_YEAR", BONUS_PAID_DURING_THE_YEAR, PERFORMANCE_BONUS_PAID_DURING_THE_YEAR
                                     , WPPF_PAID_DURING_THE_YEAR, COMPANY_LOAN_GRANTED, LOAN_BALANCE_AT_YEAR_END, OVERTIME_PAID_DURING_THE_YEAR
                                     , INCENTIVE_PAID_DURING_THE_YEAR);
                                    dr[3] = lstPERFORMANCE_BONUS_PAID_DURING_THE_YEAR[0];
                                    dr[4] = lstPERFORMANCE_BONUS_PAID_DURING_THE_YEAR[1];
                                    //dr[3] = dt_Employee.Rows[i]["PERFORMANCE_BONUS_PAID_DURING_THE_YEAR_CAPTION"].ToString();
                                    //dr[4] = dt_Employee.Rows[i]["PERFORMANCE_BONUS_PAID_DURING_THE_YEAR"].ToString();
                                    dr[5] = dt_Employee.Rows[i]["INCREMENT_YEAR_2"].ToString();
                                    dr[6] = dt_Employee.Rows[i]["INCREMENT_INFLATION_2"].ToString();
                                    dr[7] = dt_Employee.Rows[i]["INCREMENT_MERIT_2"].ToString();
                                    dr[8] = dt_Employee.Rows[i]["INCREMENT_ADJ_2"].ToString();
                                    dr[9] = dt_Employee.Rows[i]["INCREMENT_PRO_2"].ToString();
                                    dr[10] = dt_Employee.Rows[i]["INCREMENT_TOTAL_2"].ToString();
                                    dr[11] = dt_Employee.Rows[i]["INCREMENT_PERCENTAGE_2"].ToString();
                                    dr[12] = dt_Employee.Rows[i]["INCREMENT_RATING_2"].ToString();

                                    if (HAS_ADDTIONAL_INCREMENT == true && dt_EmployeeAdditionalIncrement != null && dt_EmployeeAdditionalIncrement.Rows.Count > 0)
                                    {
                                        DataView dv_Filter = dt_EmployeeAdditionalIncrement.DefaultView;
                                        dv_Filter.RowFilter = "RowNum = '2'";
                                        DataTable dt_Filtered_AdditionalIncrement = dv_Filter.ToTable();
                                        if (dt_Filtered_AdditionalIncrement != null && dt_Filtered_AdditionalIncrement.Rows.Count > 0)
                                        {
                                            dr[13] = dt_Filtered_AdditionalIncrement.Rows[0]["ADDITIONAL_INCR"].ToString();
                                            dr[14] = dt_Filtered_AdditionalIncrement.Rows[0]["ADDITIONAL_INCR_WEF"].ToString();
                                        }
                                    }


                                    dr[15] = dt_Employee.Rows[i]["PRESENT_SALARY_BASIC_2"].ToString();
                                    dr[16] = dt_Employee.Rows[i]["PRESENT_SALARY_ALLOW_2"].ToString();
                                    dr[17] = dt_Employee.Rows[i]["PRESENT_SALARY_GROSS_2"].ToString();
                                    dr[18] = "";
                                    dr[19] = dt_Employee.Rows[i]["PP_RATING_YEAR"].ToString();
                                    dr[20] = "-";
                                    dr[21] = "PerValue";
                                    dr[22] = "-";
                                    dr[23] = "PerValue";
                                    dr[24] = "-";
                                    dr[25] = "PerValue";
                                    dr[26] = "-";
                                    dr[27] = "PerValue";
                                    dr[28] = "-";
                                    dr[29] = "";
                                    dr[30] = dt_Employee.Rows[i]["NEW_GROSS_RELOCATION_HARDSHIP_ALLOW"].ToString();
                                    dr[31] = "";
                                    dr[32] = "";
                                    dr[33] = "";
                                    dr[34] = "";
                                    dr[35] = "";

                                    dt_table.Rows.InsertAt(dr, ++Last_RowNumber);

                                    #endregion

                                    #region Employee Row 3
                                    dr = dt_table.NewRow();
                                    dr[0] = "";
                                    dr[1] = "";
                                    dr[2] = dt_Employee.Rows[i]["JOINING_DATE"].ToString();
                                    List<string> lstWPPF_PAID_DURING_THE_YEAR = GetDataWithOutZero("WPPF_PAID_DURING_THE_YEAR", BONUS_PAID_DURING_THE_YEAR, PERFORMANCE_BONUS_PAID_DURING_THE_YEAR
                                     , WPPF_PAID_DURING_THE_YEAR, COMPANY_LOAN_GRANTED, LOAN_BALANCE_AT_YEAR_END, OVERTIME_PAID_DURING_THE_YEAR
                                     , INCENTIVE_PAID_DURING_THE_YEAR);
                                    dr[3] = lstWPPF_PAID_DURING_THE_YEAR[0];
                                    dr[4] = lstWPPF_PAID_DURING_THE_YEAR[1];
                                    //dr[3] = dt_Employee.Rows[i]["WPPF_PAID_DURING_THE_YEAR_CAPTION"].ToString();
                                    //dr[4] = dt_Employee.Rows[i]["WPPF_PAID_DURING_THE_YEAR"].ToString();
                                    dr[5] = dt_Employee.Rows[i]["INCREMENT_YEAR_3"].ToString();
                                    dr[6] = dt_Employee.Rows[i]["INCREMENT_INFLATION_3"].ToString();
                                    dr[7] = dt_Employee.Rows[i]["INCREMENT_MERIT_3"].ToString();
                                    dr[8] = dt_Employee.Rows[i]["INCREMENT_ADJ_3"].ToString();
                                    dr[9] = dt_Employee.Rows[i]["INCREMENT_PRO_3"].ToString();
                                    dr[10] = dt_Employee.Rows[i]["INCREMENT_TOTAL_3"].ToString();
                                    dr[11] = dt_Employee.Rows[i]["INCREMENT_PERCENTAGE_3"].ToString();
                                    dr[12] = dt_Employee.Rows[i]["INCREMENT_RATING_3"].ToString();

                                    if (HAS_ADDTIONAL_INCREMENT == true && dt_EmployeeAdditionalIncrement != null && dt_EmployeeAdditionalIncrement.Rows.Count > 0)
                                    {
                                        DataView dv_Filter = dt_EmployeeAdditionalIncrement.DefaultView;
                                        dv_Filter.RowFilter = "RowNum = '3'";
                                        DataTable dt_Filtered_AdditionalIncrement = dv_Filter.ToTable();
                                        if (dt_Filtered_AdditionalIncrement != null && dt_Filtered_AdditionalIncrement.Rows.Count > 0)
                                        {
                                            dr[13] = dt_Filtered_AdditionalIncrement.Rows[0]["ADDITIONAL_INCR"].ToString();
                                            dr[14] = dt_Filtered_AdditionalIncrement.Rows[0]["ADDITIONAL_INCR_WEF"].ToString();
                                        }
                                    }

                                    dr[15] = dt_Employee.Rows[i]["PRESENT_SALARY_BASIC_3"].ToString();
                                    dr[16] = dt_Employee.Rows[i]["PRESENT_SALARY_ALLOW_3"].ToString();
                                    dr[17] = dt_Employee.Rows[i]["PRESENT_SALARY_GROSS_3"].ToString();
                                    dr[18] = "";
                                    dr[19] = "";
                                    dr[20] = "CalculatePF";
                                    dr[21] = "";
                                    dr[22] = "CalculateBonus";
                                    dr[23] = "";
                                    dr[24] = dt_Employee.Rows[i]["PROPOSED_INCREMENT_ADJ_EOBI"].ToString();
                                    dr[25] = "";
                                    dr[26] = dt_Employee.Rows[i]["PROPOSED_INCREMENT_SESSI_ALLOW"].ToString();
                                    dr[27] = "";
                                    dr[28] = dt_Employee.Rows[i]["PROPOSED_INCREMENT_CELL_ALLOW"].ToString();
                                    dr[29] = dt_Employee.Rows[i]["PROPOSED_INCREMENT_CAR_ALLOW"].ToString();
                                    dr[30] = dt_Employee.Rows[i]["NEW_GROSS_RELOCATION_HARDSHIP_ALLOW"].ToString();
                                    dr[31] = dt_Employee.Rows[i]["NEW_GROSS_PACKAGE"].ToString() + "_NewGrossPKG_" + dt_Employee.Rows[i]["Fixed_WPPF_OT"].ToString();
                                    dr[32] = dt_Employee.Rows[i]["GROSS_PACKAGE_PREVIOUS"].ToString();
                                    dr[33] = "0_IncreaseDifferenceAmount";
                                    dr[34] = "0_IncreaseDifferenceAgePercent";
                                    dr[35] = "";

                                    dt_table.Rows.InsertAt(dr, ++Last_RowNumber);

                                    #endregion

                                    #region Employee Row 4
                                   
                                    dr = dt_table.NewRow();
                                    dr[0] = "";
                                    dr[1] = "";
                                    //dr[2] = dt_Employee.Rows[i]["YEAR_OF_SERVICE"].ToString();
                                    dr[2] = rdCheckStatus.SelectedValue=="0"?dt_Employee.Rows[i]["CONTRACT_START_DATE"].ToString(): dt_Employee.Rows[i]["YEAR_OF_SERVICE"].ToString();
                                    //dr[3] =!string.IsNullOrEmpty(COMPANY_LOAN_GRANTED) && COMPANY_LOAN_GRANTED!="0"? dt_Employee.Rows[i]["COMPANY_LOAN_GRANTED_CAPTION"].ToString():"";
                                    //dr[4] = !string.IsNullOrEmpty(COMPANY_LOAN_GRANTED) && COMPANY_LOAN_GRANTED != "0" ? dt_Employee.Rows[i]["COMPANY_LOAN_GRANTED"].ToString():"";
                                    List<string> lstCOMPANY_LOAN_GRANTED = GetDataWithOutZero("COMPANY_LOAN_GRANTED", BONUS_PAID_DURING_THE_YEAR, PERFORMANCE_BONUS_PAID_DURING_THE_YEAR
                                    , WPPF_PAID_DURING_THE_YEAR, COMPANY_LOAN_GRANTED, LOAN_BALANCE_AT_YEAR_END, OVERTIME_PAID_DURING_THE_YEAR
                                    , INCENTIVE_PAID_DURING_THE_YEAR);
                                    dr[3] = lstCOMPANY_LOAN_GRANTED[0];
                                    dr[4] = lstCOMPANY_LOAN_GRANTED[1];
                                    dr[5] = "";
                                    dr[6] = "";
                                    dr[7] = "";
                                    dr[8] = "";
                                    dr[9] = "";
                                    dr[10] = "";
                                    dr[11] = "";
                                    dr[12] = "";

                                    if (HAS_ADDTIONAL_INCREMENT == true && dt_EmployeeAdditionalIncrement != null && dt_EmployeeAdditionalIncrement.Rows.Count > 0)
                                    {
                                        DataView dv_Filter = dt_EmployeeAdditionalIncrement.DefaultView;
                                        dv_Filter.RowFilter = "RowNum = '4'";
                                        DataTable dt_Filtered_AdditionalIncrement = dv_Filter.ToTable();
                                        if (dt_Filtered_AdditionalIncrement != null && dt_Filtered_AdditionalIncrement.Rows.Count > 0)
                                        {
                                            dr[13] = dt_Filtered_AdditionalIncrement.Rows[0]["ADDITIONAL_INCR"].ToString();
                                            dr[14] = dt_Filtered_AdditionalIncrement.Rows[0]["ADDITIONAL_INCR_WEF"].ToString();
                                        }
                                    }

                                    dr[15] = "";
                                    dr[16] = "";
                                    dr[17] = "";
                                    dr[18] = "";
                                    dr[19] = "";
                                    dr[20] = "";
                                    dr[21] = "";
                                    dr[22] = "";
                                    dr[23] = "";
                                    dr[24] = "";
                                    dr[25] = "";
                                    dr[26] = "";
                                    dr[27] = "";
                                    dr[28] = "";
                                    dr[29] = "";
                                    dr[30] = "";
                                    dr[31] = "";
                                    dr[32] = "";
                                    dr[33] = "";
                                    dr[34] = "";
                                    dr[35] = "";

                                    dt_table.Rows.InsertAt(dr, ++Last_RowNumber);

                                    #endregion
                                    #region Employee Row 5
                                    //string LOAN_BALANCE_AT_YEAR_END= dt_Employee.Rows[i]["LOAN_BALANCE_AT_YEAR_END"].ToString();
                                    dr = dt_table.NewRow();
                                    dr[0] = "";
                                    dr[1] = "";
                                    //dr[2] = dt_Employee.Rows[i]["AGE"].ToString();
                                    dr[2] = rdCheckStatus.SelectedValue == "0" ? dt_Employee.Rows[i]["CONTRACT_END_DATE"].ToString(): dt_Employee.Rows[i]["AGE"].ToString();
                                    List<string> lstLOAN_BALANCE_AT_YEAR_END = GetDataWithOutZero("LOAN_BALANCE_AT_YEAR_END", BONUS_PAID_DURING_THE_YEAR, PERFORMANCE_BONUS_PAID_DURING_THE_YEAR
                                   , WPPF_PAID_DURING_THE_YEAR, COMPANY_LOAN_GRANTED, LOAN_BALANCE_AT_YEAR_END, OVERTIME_PAID_DURING_THE_YEAR
                                   , INCENTIVE_PAID_DURING_THE_YEAR);
                                    dr[3] = lstLOAN_BALANCE_AT_YEAR_END[0];
                                    dr[4] = lstLOAN_BALANCE_AT_YEAR_END[1];

                                    //if (!string.IsNullOrEmpty(COMPANY_LOAN_GRANTED) && COMPANY_LOAN_GRANTED != "0" && !string.IsNullOrEmpty(LOAN_BALANCE_AT_YEAR_END) && LOAN_BALANCE_AT_YEAR_END != "0")
                                    //{
                                    //    dr[3] = dt_Employee.Rows[i]["LOAN_BALANCE_AT_YEAR_END_CAPTION"].ToString();
                                    //    dr[4] = dt_Employee.Rows[i]["LOAN_BALANCE_AT_YEAR_END"].ToString();
                                    //}
                                    //else if ((string.IsNullOrEmpty(COMPANY_LOAN_GRANTED) || COMPANY_LOAN_GRANTED == "0")
                                    //    && !string.IsNullOrEmpty(LOAN_BALANCE_AT_YEAR_END) && LOAN_BALANCE_AT_YEAR_END != "0"
                                    //    && !string.IsNullOrEmpty(OVERTIME_PAID_DURING_THE_YEAR) && OVERTIME_PAID_DURING_THE_YEAR != "0")
                                    //{
                                    //    dr[3] = dt_Employee.Rows[i]["OVERTIME_PAID_DURING_THE_YEAR_CAPTION"].ToString();
                                    //    dr[4] = dt_Employee.Rows[i]["OVERTIME_PAID_DURING_THE_YEAR"].ToString();
                                    //}
                                    //else if ((string.IsNullOrEmpty(COMPANY_LOAN_GRANTED) || COMPANY_LOAN_GRANTED == "0")
                                    //    && !string.IsNullOrEmpty(LOAN_BALANCE_AT_YEAR_END) && LOAN_BALANCE_AT_YEAR_END != "0"
                                    //    && !string.IsNullOrEmpty(INCENTIVE_PAID_DURING_THE_YEAR) && INCENTIVE_PAID_DURING_THE_YEAR != "0")
                                    //{
                                    //    dr[3] = dt_Employee.Rows[i]["INCENTIVE_PAID_DURING_THE_YEAR_CAPTION"].ToString();
                                    //    dr[4] = dt_Employee.Rows[i]["INCENTIVE_PAID_DURING_THE_YEAR"].ToString();
                                    //}
                                    //else
                                    //{
                                    //    dr[3] = "";
                                    //    dr[4] = "";
                                    //}
                                    dr[5] = "";
                                    dr[6] = "";
                                    dr[7] = "";
                                    dr[8] = "";
                                    dr[9] = "";
                                    dr[10] = "";
                                    dr[11] = "";
                                    dr[12] = "";

                                    if (HAS_ADDTIONAL_INCREMENT == true && dt_EmployeeAdditionalIncrement != null && dt_EmployeeAdditionalIncrement.Rows.Count > 0)
                                    {
                                        DataView dv_Filter = dt_EmployeeAdditionalIncrement.DefaultView;
                                        dv_Filter.RowFilter = "RowNum = '5'";
                                        DataTable dt_Filtered_AdditionalIncrement = dv_Filter.ToTable();
                                        if (dt_Filtered_AdditionalIncrement != null && dt_Filtered_AdditionalIncrement.Rows.Count > 0)
                                        {
                                            dr[13] = dt_Filtered_AdditionalIncrement.Rows[0]["ADDITIONAL_INCR"].ToString();
                                            dr[14] = dt_Filtered_AdditionalIncrement.Rows[0]["ADDITIONAL_INCR_WEF"].ToString();
                                        }
                                    }

                                    dr[15] = "";
                                    dr[16] = "";
                                    dr[17] = "";
                                    dr[18] = "";
                                    dr[19] = "";
                                    dr[20] = "";
                                    dr[21] = "";
                                    dr[22] = "";
                                    dr[23] = "";
                                    dr[24] = "";
                                    dr[25] = "";
                                    dr[26] = "";
                                    dr[27] = "";
                                    dr[28] = "";
                                    dr[29] = "";
                                    dr[30] = "";
                                    dr[31] = "";
                                    dr[32] = "";
                                    dr[33] = "";
                                    dr[34] = "";
                                    dr[35] = "";

                                    dt_table.Rows.InsertAt(dr, ++Last_RowNumber);

                                    #endregion


                                    #region Employee Row 6
                                  //  string OVERTIME_PAID_DURING_THE_YEAR= dt_Employee.Rows[i]["OVERTIME_PAID_DURING_THE_YEAR"].ToString();
                                    dr = dt_table.NewRow();
                                    dr[0] = "";
                                    dr[1] = "";
                                    //dr[2] = "";
                                    dr[2] = rdCheckStatus.SelectedValue == "0" ? dt_Employee.Rows[i]["YEAR_OF_SERVICE"].ToString():"";

                                    List<string> lstOVERTIME_PAID_DURING_THE_YEAR = GetDataWithOutZero("OVERTIME_PAID_DURING_THE_YEAR", BONUS_PAID_DURING_THE_YEAR, PERFORMANCE_BONUS_PAID_DURING_THE_YEAR
                                   , WPPF_PAID_DURING_THE_YEAR, COMPANY_LOAN_GRANTED, LOAN_BALANCE_AT_YEAR_END, OVERTIME_PAID_DURING_THE_YEAR
                                   , INCENTIVE_PAID_DURING_THE_YEAR);
                                    dr[3] = lstOVERTIME_PAID_DURING_THE_YEAR[0];
                                    dr[4] = lstOVERTIME_PAID_DURING_THE_YEAR[1];
                                    //if (!string.IsNullOrEmpty(COMPANY_LOAN_GRANTED) && COMPANY_LOAN_GRANTED != "0" 
                                    //    && !string.IsNullOrEmpty(LOAN_BALANCE_AT_YEAR_END) && LOAN_BALANCE_AT_YEAR_END != "0"
                                    //    && !string.IsNullOrEmpty(OVERTIME_PAID_DURING_THE_YEAR) && OVERTIME_PAID_DURING_THE_YEAR != "0")
                                    //{
                                    //    dr[3] = dt_Employee.Rows[i]["OVERTIME_PAID_DURING_THE_YEAR_CAPTION"].ToString();
                                    //    dr[4] = dt_Employee.Rows[i]["OVERTIME_PAID_DURING_THE_YEAR"].ToString();
                                    //}

                                    //else if (!string.IsNullOrEmpty(COMPANY_LOAN_GRANTED) && COMPANY_LOAN_GRANTED != "0"
                                    //    && !string.IsNullOrEmpty(LOAN_BALANCE_AT_YEAR_END) && LOAN_BALANCE_AT_YEAR_END != "0"
                                    //    && (string.IsNullOrEmpty(OVERTIME_PAID_DURING_THE_YEAR) || OVERTIME_PAID_DURING_THE_YEAR == "0")
                                    //    && !string.IsNullOrEmpty(INCENTIVE_PAID_DURING_THE_YEAR) && INCENTIVE_PAID_DURING_THE_YEAR != "0")
                                    //{
                                    //    dr[3] = dt_Employee.Rows[i]["INCENTIVE_PAID_DURING_THE_YEAR_CAPTION"].ToString();
                                    //    dr[4] = dt_Employee.Rows[i]["INCENTIVE_PAID_DURING_THE_YEAR"].ToString();
                                    //}
                                    //else
                                    //{
                                    //    dr[3] = "";
                                    //    dr[4] = "";
                                    //}


                                    dr[5] = "";
                                    dr[6] = "";
                                    dr[7] = "";
                                    dr[8] = "";
                                    dr[9] = "";
                                    dr[10] = "";
                                    dr[11] = "";
                                    dr[12] = "";

                                    //if (HAS_ADDTIONAL_INCREMENT == true && dt_EmployeeAdditionalIncrement != null && dt_EmployeeAdditionalIncrement.Rows.Count > 0)
                                    //{
                                    //    DataView dv_Filter = dt_EmployeeAdditionalIncrement.DefaultView;
                                    //    dv_Filter.RowFilter = "RowNum = '5'";
                                    //    DataTable dt_Filtered_AdditionalIncrement = dv_Filter.ToTable();
                                    //    if (dt_Filtered_AdditionalIncrement != null && dt_Filtered_AdditionalIncrement.Rows.Count > 0)
                                    //    {
                                    //        dr[13] = dt_Filtered_AdditionalIncrement.Rows[0]["ADDITIONAL_INCR"].ToString();
                                    //        dr[14] = dt_Filtered_AdditionalIncrement.Rows[0]["ADDITIONAL_INCR_WEF"].ToString();
                                    //    }
                                    //}

                                    dr[13] = "";
                                    dr[14] = "";

                                    dr[15] = "";
                                    dr[16] = "";
                                    dr[17] = "";
                                    dr[18] = "";
                                    dr[19] = "";
                                    dr[20] = "HOLIDAYS";
                                    dr[21] = "";
                                    dr[22] = "LV/ABS";
                                    dr[23] = "";
                                    dr[24] = "LWP";
                                    dr[25] = "";
                                    dr[26] = "TOT-PRS";
                                    dr[27] = "";
                                    dr[28] = "REQ-HRS";
                                    dr[29] = "WRKD-HRS";
                                    dr[30] = "SHRT-HRS";
                                    dr[31] = "EXCS-HRS";
                                    dr[32] = "";
                                    dr[33] = "";
                                    dr[34] = "";
                                    dr[35] = "";

                                    dt_table.Rows.InsertAt(dr, ++Last_RowNumber);

                                    #endregion


                                    #region Employee Row 7
                                 //   string INCENTIVE_PAID_DURING_THE_YEAR= dt_Employee.Rows[i]["INCENTIVE_PAID_DURING_THE_YEAR"].ToString();
                                    dr = dt_table.NewRow();
                                    dr[0] = "";
                                    dr[1] = "";
                                    //dr[2] = "";
                                    dr[2] = rdCheckStatus.SelectedValue == "0" ? dt_Employee.Rows[i]["AGE"].ToString():"";
                                    List<string> lstINCENTIVE_PAID_DURING_THE_YEAR = GetDataWithOutZero("INCENTIVE_PAID_DURING_THE_YEAR", BONUS_PAID_DURING_THE_YEAR, PERFORMANCE_BONUS_PAID_DURING_THE_YEAR
                                   , WPPF_PAID_DURING_THE_YEAR, COMPANY_LOAN_GRANTED, LOAN_BALANCE_AT_YEAR_END, OVERTIME_PAID_DURING_THE_YEAR
                                   , INCENTIVE_PAID_DURING_THE_YEAR);
                                    dr[3] = lstINCENTIVE_PAID_DURING_THE_YEAR[0];
                                    dr[4] = lstINCENTIVE_PAID_DURING_THE_YEAR[1];
                                    //if (!string.IsNullOrEmpty(COMPANY_LOAN_GRANTED) && COMPANY_LOAN_GRANTED != "0"
                                    //    && !string.IsNullOrEmpty(LOAN_BALANCE_AT_YEAR_END) && LOAN_BALANCE_AT_YEAR_END != "0"
                                    //    && !string.IsNullOrEmpty(OVERTIME_PAID_DURING_THE_YEAR) && OVERTIME_PAID_DURING_THE_YEAR != "0"
                                    //    && !string.IsNullOrEmpty(INCENTIVE_PAID_DURING_THE_YEAR) && INCENTIVE_PAID_DURING_THE_YEAR != "0")
                                    //{
                                    //    dr[3] = dt_Employee.Rows[i]["INCENTIVE_PAID_DURING_THE_YEAR_CAPTION"].ToString();
                                    //    dr[4] = dt_Employee.Rows[i]["INCENTIVE_PAID_DURING_THE_YEAR"].ToString();
                                    //}
                                    //else
                                    //{
                                    //    dr[3] = "";
                                    //    dr[4] = "";
                                    //}
                                    dr[5] = "";
                                    dr[6] = "";
                                    dr[7] = "";
                                    dr[8] = "";
                                    dr[9] = "";
                                    dr[10] = "";
                                    dr[11] = "";
                                    dr[12] = "";

                                    if (HAS_ADDTIONAL_INCREMENT == true && dt_EmployeeAdditionalIncrement != null && dt_EmployeeAdditionalIncrement.Rows.Count > 0)
                                    {
                                        DataView dv_Filter = dt_EmployeeAdditionalIncrement.DefaultView;
                                        dv_Filter.RowFilter = "RowNum = '6'";
                                        DataTable dt_Filtered_AdditionalIncrement = dv_Filter.ToTable();
                                        if (dt_Filtered_AdditionalIncrement != null && dt_Filtered_AdditionalIncrement.Rows.Count > 0)
                                        {
                                            dr[13] = dt_Filtered_AdditionalIncrement.Rows[0]["ADDITIONAL_INCR"].ToString();
                                            dr[14] = dt_Filtered_AdditionalIncrement.Rows[0]["ADDITIONAL_INCR_WEF"].ToString();
                                        }
                                    }

                                    dr[15] = "";
                                    dr[16] = "";
                                    dr[17] = "";
                                    dr[18] = "";
                                    dr[19] = "";
                                    dr[20] = dt_Employee.Rows[i]["HOLIDAYS"].ToString();
                                    dr[21] = "";
                                    dr[22] = dt_Employee.Rows[i]["LV_ABS"].ToString();
                                    dr[23] = "";
                                    dr[24] = dt_Employee.Rows[i]["LWP"].ToString();
                                    dr[25] = "";
                                    dr[26] = dt_Employee.Rows[i]["TOT_PRS"].ToString();
                                    dr[27] = "";
                                    dr[28] = dt_Employee.Rows[i]["REQ_HRS"].ToString();
                                    dr[29] = dt_Employee.Rows[i]["WRKD_HRS"].ToString();
                                    dr[30] = dt_Employee.Rows[i]["SHRT_HRS"].ToString();
                                    dr[31] = dt_Employee.Rows[i]["EXCS_HRS"].ToString();
                                    dr[32] = "";
                                    dr[33] = "";
                                    dr[34] = "";
                                    dr[35] = "";

                                    dt_table.Rows.InsertAt(dr, ++Last_RowNumber);

                                    #endregion

                                    #region Employee Row 8
                                    dr = dt_table.NewRow();
                                    dr[0] = "";
                                    dr[1] = "";
                                    
                                    dr[2] = rdCheckStatus.SelectedValue == "1"? dt_Employee.Rows[i]["EDUCATION_1"].ToString():"";
                                    dr[3] = "";
                                    dr[4] = "";
                                    dr[5] = "";
                                    dr[6] = "";
                                    dr[7] = "";
                                    dr[8] = "";
                                    dr[9] = "";
                                    dr[10] = "";
                                    dr[11] = "";
                                    dr[12] = "";

                                    if (HAS_ADDTIONAL_INCREMENT == true && dt_EmployeeAdditionalIncrement != null && dt_EmployeeAdditionalIncrement.Rows.Count > 0)
                                    {
                                        DataView dv_Filter = dt_EmployeeAdditionalIncrement.DefaultView;
                                        dv_Filter.RowFilter = "RowNum = '7'";
                                        DataTable dt_Filtered_AdditionalIncrement = dv_Filter.ToTable();
                                        if (dt_Filtered_AdditionalIncrement != null && dt_Filtered_AdditionalIncrement.Rows.Count > 0)
                                        {
                                            dr[13] = dt_Filtered_AdditionalIncrement.Rows[0]["ADDITIONAL_INCR"].ToString();
                                            dr[14] = dt_Filtered_AdditionalIncrement.Rows[0]["ADDITIONAL_INCR_WEF"].ToString();
                                        }
                                    }

                                    dr[15] = "";
                                    dr[16] = "";
                                    dr[17] = "";
                                    dr[18] = "";
                                    dr[19] = "";
                                    dr[20] = "";
                                    dr[21] = "";
                                    dr[22] = "";
                                    dr[23] = "";
                                    dr[24] = "";
                                    dr[25] = "";
                                    dr[26] = "";
                                    dr[27] = "";
                                    dr[28] = "";
                                    dr[29] = "";
                                    dr[30] = "";
                                    dr[31] = "";
                                    dr[32] = "";
                                    dr[33] = "";
                                    dr[34] = "";
                                    dr[35] = "";

                                    dt_table.Rows.InsertAt(dr, ++Last_RowNumber);

                                    #endregion

                                    #region Employee Row 9
                                    dr = dt_table.NewRow();
                                    dr[0] = "";
                                    dr[1] = "";
                                    //dr[2] = dt_Employee.Rows[i]["EDUCATION_2"].ToString();
                                    dr[2] = rdCheckStatus.SelectedValue == "1" ? dt_Employee.Rows[i]["EDUCATION_2"].ToString() : dt_Employee.Rows[i]["EDUCATION_1"].ToString();
                                    dr[3] = "";
                                    dr[4] = "";
                                    dr[5] = "";
                                    dr[6] = "";
                                    dr[7] = "";
                                    dr[8] = "";
                                    dr[9] = "";
                                    dr[10] = "";
                                    dr[11] = "";
                                    dr[12] = "";

                                    if (HAS_ADDTIONAL_INCREMENT == true && dt_EmployeeAdditionalIncrement != null && dt_EmployeeAdditionalIncrement.Rows.Count > 0)
                                    {
                                        DataView dv_Filter = dt_EmployeeAdditionalIncrement.DefaultView;
                                        dv_Filter.RowFilter = "RowNum = '8'";
                                        DataTable dt_Filtered_AdditionalIncrement = dv_Filter.ToTable();
                                        if (dt_Filtered_AdditionalIncrement != null && dt_Filtered_AdditionalIncrement.Rows.Count > 0)
                                        {
                                            dr[13] = dt_Filtered_AdditionalIncrement.Rows[0]["ADDITIONAL_INCR"].ToString();
                                            dr[14] = dt_Filtered_AdditionalIncrement.Rows[0]["ADDITIONAL_INCR_WEF"].ToString();
                                        }
                                    }

                                    dr[15] = "";
                                    dr[16] = "";
                                    dr[17] = "";
                                    dr[18] = "";
                                    dr[19] = "";
                                    dr[20] = "";
                                    dr[21] = "";
                                    dr[22] = "";
                                    dr[23] = "";
                                    dr[24] = "";
                                    dr[25] = "";
                                    dr[26] = "";
                                    dr[27] = "";
                                    dr[28] = "";
                                    dr[29] = "";
                                    dr[30] = "";
                                    dr[31] = "";
                                    dr[32] = "";
                                    dr[33] = "";
                                    dr[34] = "";
                                    dr[35] = "";

                                    dt_table.Rows.InsertAt(dr, ++Last_RowNumber);

                                    #endregion

                                    #region Employee Row 10
                                    dr = dt_table.NewRow();
                                    dr[0] = "";
                                    dr[1] = "";
                                    dr[2] = rdCheckStatus.SelectedValue == "1" ? "" : dt_Employee.Rows[i]["EDUCATION_2"].ToString();
                                    dr[3] = "";
                                    dr[4] = "";
                                    dr[5] = "";
                                    dr[6] = "";
                                    dr[7] = "";
                                    dr[8] = "";
                                    dr[9] = "";
                                    dr[10] = "";
                                    dr[11] = "";
                                    dr[12] = "";

                                    if (HAS_ADDTIONAL_INCREMENT == true && dt_EmployeeAdditionalIncrement != null && dt_EmployeeAdditionalIncrement.Rows.Count > 0)
                                    {
                                        DataView dv_Filter = dt_EmployeeAdditionalIncrement.DefaultView;
                                        dv_Filter.RowFilter = "RowNum = '7'";
                                        DataTable dt_Filtered_AdditionalIncrement = dv_Filter.ToTable();
                                        if (dt_Filtered_AdditionalIncrement != null && dt_Filtered_AdditionalIncrement.Rows.Count > 0)
                                        {
                                            dr[13] = dt_Filtered_AdditionalIncrement.Rows[0]["ADDITIONAL_INCR"].ToString();
                                            dr[14] = dt_Filtered_AdditionalIncrement.Rows[0]["ADDITIONAL_INCR_WEF"].ToString();
                                        }
                                    }

                                    dr[15] = "";
                                    dr[16] = "";
                                    dr[17] = "";
                                    dr[18] = "";
                                    dr[19] = "";
                                    dr[20] = "";
                                    dr[21] = "";
                                    dr[22] = "";
                                    dr[23] = "";
                                    dr[24] = "";
                                    dr[25] = "";
                                    dr[26] = "";
                                    dr[27] = "";
                                    dr[28] = "";
                                    dr[29] = "";
                                    dr[30] = "";
                                    dr[31] = "";
                                    dr[32] = "";
                                    dr[33] = "";
                                    dr[34] = "";
                                    dr[35] = "";

                                    dt_table.Rows.InsertAt(dr, ++Last_RowNumber);

                                    #endregion

                                    #region Employee Row 11
                                    dr = dt_table.NewRow();
                                    dr[0] = "";
                                    dr[1] = "";
                                    dr[2] = "";// dt_Employee.Rows[i]["EDUCATION_2"].ToString();
                                    dr[3] = "";
                                    dr[4] = "";
                                    dr[5] = "";
                                    dr[6] = "";
                                    dr[7] = "";
                                    dr[8] = "";
                                    dr[9] = "";
                                    dr[10] = "";
                                    dr[11] = "";
                                    dr[12] = "";

                                    if (HAS_ADDTIONAL_INCREMENT == true && dt_EmployeeAdditionalIncrement != null && dt_EmployeeAdditionalIncrement.Rows.Count > 0)
                                    {
                                        DataView dv_Filter = dt_EmployeeAdditionalIncrement.DefaultView;
                                        dv_Filter.RowFilter = "RowNum = '8'";
                                        DataTable dt_Filtered_AdditionalIncrement = dv_Filter.ToTable();
                                        if (dt_Filtered_AdditionalIncrement != null && dt_Filtered_AdditionalIncrement.Rows.Count > 0)
                                        {
                                            dr[13] = dt_Filtered_AdditionalIncrement.Rows[0]["ADDITIONAL_INCR"].ToString();
                                            dr[14] = dt_Filtered_AdditionalIncrement.Rows[0]["ADDITIONAL_INCR_WEF"].ToString();
                                        }
                                    }

                                    dr[15] = "";
                                    dr[16] = "";
                                    dr[17] = "";
                                    dr[18] = "";
                                    dr[19] = "";
                                    dr[20] = "";
                                    dr[21] = "";
                                    dr[22] = "";
                                    dr[23] = "";
                                    dr[24] = "";
                                    dr[25] = "";
                                    dr[26] = "";
                                    dr[27] = "";
                                    dr[28] = "";
                                    dr[29] = "";
                                    dr[30] = "";
                                    dr[31] = "";
                                    dr[32] = "";
                                    dr[33] = "";
                                    dr[34] = "";
                                    dr[35] = "";

                                    dt_table.Rows.InsertAt(dr, ++Last_RowNumber);

                                    #endregion

                                    //#region Employee Row 10
                                    //dr = dt_table.NewRow();
                                    //dr[0] = "";
                                    //dr[1] = "";
                                    //dr[2] = dt_Employee.Rows[i]["EDUCATION_2"].ToString();
                                    //dr[3] = "";
                                    //dr[4] = "";
                                    //dr[5] = "";
                                    //dr[6] = "";
                                    //dr[7] = "";
                                    //dr[8] = "";
                                    //dr[9] = "";
                                    //dr[10] = "";
                                    //dr[11] = "";
                                    //dr[12] = "";

                                    //if (HAS_ADDTIONAL_INCREMENT == true && dt_EmployeeAdditionalIncrement != null && dt_EmployeeAdditionalIncrement.Rows.Count > 0)
                                    //{
                                    //    DataView dv_Filter = dt_EmployeeAdditionalIncrement.DefaultView;
                                    //    dv_Filter.RowFilter = "RowNum = '8'";
                                    //    DataTable dt_Filtered_AdditionalIncrement = dv_Filter.ToTable();
                                    //    if (dt_Filtered_AdditionalIncrement != null && dt_Filtered_AdditionalIncrement.Rows.Count > 0)
                                    //    {
                                    //        dr[13] = dt_Filtered_AdditionalIncrement.Rows[0]["ADDITIONAL_INCR"].ToString();
                                    //        dr[14] = dt_Filtered_AdditionalIncrement.Rows[0]["ADDITIONAL_INCR_WEF"].ToString();
                                    //    }
                                    //}

                                    //dr[15] = "";
                                    //dr[16] = "";
                                    //dr[17] = "";
                                    //dr[18] = "";
                                    //dr[19] = "";
                                    //dr[20] = "";
                                    //dr[21] = "";
                                    //dr[22] = "";
                                    //dr[23] = "";
                                    //dr[24] = "";
                                    //dr[25] = "";
                                    //dr[26] = "";
                                    //dr[27] = "";
                                    //dr[28] = "";
                                    //dr[29] = "";
                                    //dr[30] = "";
                                    //dr[31] = "";
                                    //dr[32] = "";
                                    //dr[33] = "";
                                    //dr[34] = "";
                                    //dr[35] = "";

                                    //dt_table.Rows.InsertAt(dr, ++Last_RowNumber);

                                    //#endregion




                                    #region Employee Row 12,13 & 14
                                    dr = dt_table.NewRow();
                                    dt_table.Rows.InsertAt(dr, ++Last_RowNumber);
                                    dr = dt_table.NewRow();
                                    dt_table.Rows.InsertAt(dr, ++Last_RowNumber);

                                    dr = dt_table.NewRow();
                                    dr[0] = "LastRow";
                                    dt_table.Rows.InsertAt(dr, ++Last_RowNumber);




                                    #endregion
                                }

                                string Reporting_CostCenter = dt_Employee.Rows[0]["Reporting_CostCenter"].ToString();
                                if (dsFianl.Tables.Contains(Reporting_CostCenter))
                                {
                                    Reporting_CostCenter = Reporting_CostCenter + ".";
                                }
                                dt_table.TableName = Reporting_CostCenter;
                                dt_table.AcceptChanges();
                                if (dt_table != null && dt_table.Rows.Count > 0)
                                {
                                    dsFianl.Tables.Add(dt_table);
                                }
                            }
                        }
                        dsFianl.AcceptChanges();
                        if (dsFianl != null && dsFianl.Tables.Count > 0)
                        {
                            Generate_Excel(dsFianl, _PFFormula, _BonusFormula, txtPassword.Text);
                        }
                    }
                }
                else
                {
                    Error("Select To Date");
                }
            }
            else
            {
                Error(_GetChecking);
            }

           
        }
        catch (Exception ex)
        {
            divError.Visible = true;
            lblError.InnerText = ex.Message;
        }
        finally
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(System.Web.Hosting.HostingEnvironment.MapPath("~/Uploads/HCM_INCREMENT_FILES_SAVE/"));
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
        }

    }


    private string GetSelectedEmployees() {
        string _Values = "0";

        foreach (ListItem item in ddlEmployeeAdd.Items)
        {
            if(item.Selected && item.Value.ToString() != "0")
                _Values += item.Value.ToString()+",";
        }
      return  _Values.TrimEnd(',');
    }
    public static void Generate_Excel(DataSet ds, string pfFormula, string bonusFormula, string sheet_password)
    {
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                List<ListItem> filePath = new List<ListItem>();

                for (int i = 0; i < ds.Tables.Count; ++i)
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (ExcelPackage pck = new ExcelPackage())
                    {
                        double _TryParseOutVar = 0;
                        Color colFromHex;
                        DataTable dt = ds.Tables[i];

                        ExcelWorksheet ws1 = pck.Workbook.Worksheets.Add(dt.TableName);

                        ws1.PrinterSettings.Orientation = eOrientation.Landscape;

                        ws1.Cells["A1"].LoadFromDataTable(ds.Tables[i], false);
                        ws1.Cells.Style.Font.Size = 11;
                        ws1.Cells.Style.Font.Name = "Calibri";
                        ws1.View.FreezePanes(ExcelIncrementSheet.SrNo, ExcelIncrementSheet.OtherBenefitsName);


                        ws1.Protection.IsProtected = true;
                        ws1.Protection.SetPassword(sheet_password);
                        pck.Workbook.Calculate();

                        ws1.Cells[1, ExcelIncrementSheet.SrNo, 1, dt.Columns.Count].Merge = true;
                        ws1.Cells[1, ExcelIncrementSheet.SrNo, 1, dt.Columns.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        ws1.Cells[1, ExcelIncrementSheet.SrNo, 1, dt.Columns.Count].Style.VerticalAlignment = ExcelVerticalAlignment.Top;


                        ws1.Cells[2, ExcelIncrementSheet.SrNo, 2, dt.Columns.Count].Merge = true;
                        ws1.Cells[2, ExcelIncrementSheet.SrNo, 2, dt.Columns.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        ws1.Cells[2, ExcelIncrementSheet.SrNo, 2, dt.Columns.Count].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                        ws1.Cells[1, ExcelIncrementSheet.SrNo, 2, dt.Columns.Count].Style.Font.Size = 14;
                        ws1.Cells[1, ExcelIncrementSheet.SrNo, 2, dt.Columns.Count].Style.Font.Bold = true;



                        ws1.Cells[5, ExcelIncrementSheet.SrNo, 7, dt.Columns.Count].Style.Font.Bold = true;
                        ws1.Cells[5, ExcelIncrementSheet.SrNo, 7, dt.Columns.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws1.Cells[5, ExcelIncrementSheet.SrNo, 7, dt.Columns.Count].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        ws1.Cells[5, ExcelIncrementSheet.SrNo, 7, ExcelIncrementSheet.SrNo].Merge = true;
                        ws1.Cells[5, ExcelIncrementSheet.EmpCode, 7, ExcelIncrementSheet.EmpCode].Merge = true;
                        ws1.Cells[5, ExcelIncrementSheet.NameDesignationDojQualification, 7, ExcelIncrementSheet.NameDesignationDojQualification].Merge = true;

                        ws1.Cells[5, ExcelIncrementSheet.NewGrossPackage, 7, ExcelIncrementSheet.NewGrossPackage].Merge = true;
                        ws1.Cells[6, ExcelIncrementSheet.NewGrossed_RelocationHardshipAllownce, 7, ExcelIncrementSheet.NewGrossed_RelocationHardshipAllownce].Merge = true;

                        ws1.Cells[5, ExcelIncrementSheet.GrossPackagePrevious, 7, ExcelIncrementSheet.GrossPackagePrevious].Merge = true;

                        ws1.Cells[5, ExcelIncrementSheet.Increse_Amount, 6, ExcelIncrementSheet.Increse_Perc_Age].Merge = true;

                        
                        ws1.Cells[5, ExcelIncrementSheet.Remarks, 7, ExcelIncrementSheet.Remarks].Merge = true;
                        ws1.Cells[5, ExcelIncrementSheet.OtherBenefitsName, 7, ExcelIncrementSheet.OtherBenefitsValue].Merge = true;
                        ws1.Cells[5, ExcelIncrementSheet.Increment_Year, 5, ExcelIncrementSheet.Increment_Rating].Merge = true;
                        ws1.Cells[5, ExcelIncrementSheet.Additional_Incr, 5, ExcelIncrementSheet.Additional_WEF].Merge = true;

                        ws1.Cells[6, ExcelIncrementSheet.Increment_Year, 7, ExcelIncrementSheet.Increment_Year].Merge = true;
                        ws1.Cells[6, ExcelIncrementSheet.Increment_Inflation, 7, ExcelIncrementSheet.Increment_Inflation].Merge = true;
                        ws1.Cells[6, ExcelIncrementSheet.Increment_Merit, 7, ExcelIncrementSheet.Increment_Merit].Merge = true;
                        ws1.Cells[6, ExcelIncrementSheet.Increment_Adj, 7, ExcelIncrementSheet.Increment_Adj].Merge = true;
                        ws1.Cells[6, ExcelIncrementSheet.Increment_Pro, 7, ExcelIncrementSheet.Increment_Pro].Merge = true;
                        ws1.Cells[6, ExcelIncrementSheet.Increment_Total, 7, ExcelIncrementSheet.Increment_Total].Merge = true;
                        ws1.Cells[6, ExcelIncrementSheet.Increment_Percent, 7, ExcelIncrementSheet.Increment_Percent].Merge = true;
                        ws1.Cells[6, ExcelIncrementSheet.Increment_Rating, 7, ExcelIncrementSheet.Increment_Rating].Merge = true;
                        ws1.Cells[6, ExcelIncrementSheet.Additional_Incr, 7, ExcelIncrementSheet.Additional_Incr].Merge = true;
                        ws1.Cells[6, ExcelIncrementSheet.Additional_WEF, 7, ExcelIncrementSheet.Additional_WEF].Merge = true;
                        ws1.Cells[6, ExcelIncrementSheet.Present_Salary_Basic, 7, ExcelIncrementSheet.Present_Salary_Basic].Merge = true;
                        ws1.Cells[6, ExcelIncrementSheet.Present_Salary_Allow, 7, ExcelIncrementSheet.Present_Salary_Allow].Merge = true;
                        ws1.Cells[6, ExcelIncrementSheet.Present_Salary_Gross, 7, ExcelIncrementSheet.Present_Salary_Gross].Merge = true;
                        //ws1.Cells[6, ExcelIncrementSheet.NewGrossed_RelocationHardshipAllownce, 7, ExcelIncrementSheet.NewGrossed_RelocationHardshipAllownce].Merge = true;
                        ws1.Cells[5, ExcelIncrementSheet.Performance, 7, ExcelIncrementSheet.Performance].Merge = true;
                        ws1.Cells[5, ExcelIncrementSheet.PPRATING, 7, ExcelIncrementSheet.PPRATING].Merge = true;


                        ws1.Cells[5, ExcelIncrementSheet.Present_Salary_Basic, 5, ExcelIncrementSheet.Present_Salary_Gross].Merge = true;

                        ws1.Cells[5, ExcelIncrementSheet.Proposed_Increment_Inflation_PF, 5, ExcelIncrementSheet.Proposed_Increment_Pecent5_CarAllownce].Merge = true;

                        ws1.Column(ExcelIncrementSheet.NameDesignationDojQualification).Style.WrapText = true;
                        ws1.Column(ExcelIncrementSheet.NameDesignationDojQualification).Width = 50;
                        ws1.Column(ExcelIncrementSheet.Additional_WEF).Width = 30;
                        ws1.Column(ExcelIncrementSheet.GrossPackagePrevious).Width = 30;
                        ws1.Column(ExcelIncrementSheet.OtherBenefitsName).Width = 60;
                        ws1.Column(ExcelIncrementSheet.OtherBenefitsValue).Width = 10;
                        ws1.Column(ExcelIncrementSheet.Proposed_Increment_Inflation_PF).Width = 15;
                        ws1.Column(ExcelIncrementSheet.Proposed_Increment_Merit_Bonus).Width = 15;
                        ws1.Column(ExcelIncrementSheet.Proposed_Increment_Adj_EOBI).Width = 15;
                        ws1.Column(ExcelIncrementSheet.Proposed_Increment_Pro_SS).Width = 15;
                        ws1.Column(ExcelIncrementSheet.Proposed_Increment_Total_Mobile).Width = 20;
                        ws1.Column(ExcelIncrementSheet.Increment_Inflation).Width = 10;
                        ws1.Column(ExcelIncrementSheet.Performance).Width = 20;
                        ws1.Column(ExcelIncrementSheet.PPRATING).Width = 15;
                        ws1.Column(ExcelIncrementSheet.Proposed_Increment_Inflation_PF).Width = 10;
                        ws1.Column(ExcelIncrementSheet.Proposed_Increment_Pecent5_CarAllownce).Width = 30;
                        ws1.Column(ExcelIncrementSheet.NewGrossed_RelocationHardshipAllownce).Width = 30;
                        ws1.Column(ExcelIncrementSheet.NewGrossPackage).Width = 30;
                        ws1.Column(ExcelIncrementSheet.Remarks).Width = 50;





                        ws1.Cells[5, ExcelIncrementSheet.SrNo, 7, dt.Columns.Count].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        ws1.Cells[5, ExcelIncrementSheet.SrNo, 7, dt.Columns.Count].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        ws1.Cells[5, ExcelIncrementSheet.SrNo, 7, dt.Columns.Count].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        ws1.Cells[5, ExcelIncrementSheet.SrNo, 7, dt.Columns.Count].Style.Border.Right.Style = ExcelBorderStyle.Thin;


                        int rows = ws1.Dimension.End.Row;
                        int columns = ws1.Dimension.End.Column;
                        int _TotalRows = 0;
                        string _BasicSalaryRows = "";
                        string _BasicSalarySecondRows = "";
                        string _BasicSalaryThirdRows = "";
                        for (int row = 0; row < rows; row++)
                        {
                            for (int column = 1; column <= columns; column++)
                            {


                                if (ws1.Cells[row + 2, column].Value != null)
                                {
                                    if (double.TryParse(Convert.ToString(ws1.Cells[row + 2, column].Value), out _TryParseOutVar))
                                    {
                                        ws1.Cells[row + 2, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                        ws1.Cells[row + 2, column].Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                                        ws1.Cells[row + 2, column].Style.Numberformat.Format = "#,##";
                                        //if (Convert.ToString(ws1.Cells[row + 2, column].Value).Contains("."))
                                        //{
                                        //    ws1.Cells[row + 2, column].Value = Convert.ToDouble(ws1.Cells[row + 2, column].Value);
                                        //    ws1.Cells[row + 2, column].Style.Numberformat.Format = "#,##0.00";
                                        //}
                                        //else
                                        //{
                                        //    ws1.Cells[row + 2, column].Value = Convert.ToDouble(ws1.Cells[row + 2, column].Value);
                                        //    if (row >= 6 && (column == 5 || column==14 || (column>=7 && column<=11) || (column >= 16 && column <= 18) || (column >= 21 && column <= 34)))
                                        //    {
                                        //        ws1.Cells[row + 2, column].Style.Numberformat.Format = "#,##";
                                        //    }
                                        //}

                                    }




                                    if (Convert.ToString(ws1.Cells[row + 2, column].Value) == "HOLIDAYS")
                                    {
                                        // Set dynamic Field Color.
                                        colFromHex = System.Drawing.ColorTranslator.FromHtml("#f7f700");
                                        ws1.Cells[row + 2, column, row + 2, dt.Columns.Count - 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        ws1.Cells[row + 2, column, row + 2, dt.Columns.Count - 1].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                        ws1.Cells[row + 3, column, row + 3, dt.Columns.Count - 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        ws1.Cells[row + 3, column, row + 3, dt.Columns.Count - 1].Style.Fill.BackgroundColor.SetColor(colFromHex);

                                    }

                                    else if (Convert.ToString(ws1.Cells[row + 2, column].Value).Contains("PerValue"))
                                    {
                                        // Set dynamic Field Color.

                                        if (Convert.ToString(ws1.Cells[row + 2, column].Value).Contains("_PerValue"))
                                        {
                                            ws1.Cells[row + 2, column].Value =Convert.ToDouble(Convert.ToString(ws1.Cells[row + 2, column].Value).Split('_')[0]);
                                        }
                                        else
                                        {
                                            ws1.Cells[row + 2, column].Value = Convert.ToDouble("0");
                                        }
                                        colFromHex = System.Drawing.ColorTranslator.FromHtml("#FCE4D6");
                                        ws1.Cells[row + 2, column, row + 2, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        ws1.Cells[row + 2, column, row + 2, column].Style.Fill.BackgroundColor.SetColor(colFromHex);

                                        ///Set Formula
                                        ws1.Cells[row + 2, column - 1].Formula = "=ROUND(" + ws1.Cells[(row + 2) - 1, ExcelIncrementSheet.Present_Salary_Gross] + "*" + ws1.Cells[row + 2, column] + "/100,0)";
                                        ws1.Cells[row + 2, ExcelIncrementSheet.Proposed_Increment_Total_Mobile].Formula = "=ROUND(" + ws1.Cells[row + 2, ExcelIncrementSheet.Proposed_Increment_Inflation_PF] + "+" + ws1.Cells[row + 2, ExcelIncrementSheet.Proposed_Increment_Merit_Bonus] + "+" + ws1.Cells[row + 2, ExcelIncrementSheet.Proposed_Increment_Adj_EOBI] + "+" + ws1.Cells[row + 2, ExcelIncrementSheet.Proposed_Increment_Pro_SS] + ",0)";
                                        ws1.Cells[row + 2, ExcelIncrementSheet.Proposed_Increment_Pecent5_CarAllownce].Formula = "=ROUND(" + ws1.Cells[row + 2, ExcelIncrementSheet.Proposed_Increment_Percent1] + "+" + ws1.Cells[row + 2, ExcelIncrementSheet.Proposed_Increment_Percent2] + "+" + ws1.Cells[row + 2, ExcelIncrementSheet.Proposed_Increment_Percent3] + "+" + ws1.Cells[row + 2, ExcelIncrementSheet.Proposed_Increment_Percent4] + ",0)";
                                        ws1.Cells[row + 2, ExcelIncrementSheet.NewGrossed_RelocationHardshipAllownce].Formula = "=ROUND(" + ws1.Cells[(row + 2) - 1, ExcelIncrementSheet.Present_Salary_Gross] + "+" + ws1.Cells[row + 2, ExcelIncrementSheet.Proposed_Increment_Total_Mobile] + ",0)";
                                        ws1.Cells[row + 2, ExcelIncrementSheet.NewGrossed_RelocationHardshipAllownce].Style.Numberformat.Format = "#,##";
                                        ws1.Cells[row + 2, column].Style.Locked = false;
                                        
                                    }



                                    else if (Convert.ToString(ws1.Cells[row + 2, column].Value) == "--- Select Rating ---")
                                    {
                                        var dd = ws1.Cells[row + 2, column, row + 2, column].DataValidation.AddListDataValidation() as ExcelDataValidationList;
                                        dd.AllowBlank = true;
                                        dd.Formula.Values.Add("EX");
                                        dd.Formula.Values.Add("EE");
                                        dd.Formula.Values.Add("ME");
                                        dd.Formula.Values.Add("BE");
                                        dd.Formula.Values.Add("UA");

                                        ws1.Cells[row + 3, ExcelIncrementSheet.Performance, row + 15, ExcelIncrementSheet.Performance].Merge = true;
                                        ws1.Cells[row + 2, column].Style.Locked = false;
                                    }



                                

                                    else if (Convert.ToString(ws1.Cells[row + 2, column].Value) == "LastRow")
                                    {
                                        ws1.Cells[row + 2, ExcelIncrementSheet.SrNo, row + 2, dt.Columns.Count].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                        ws1.Cells[(row + 2) - 11, ExcelIncrementSheet.Remarks, row + 2, ExcelIncrementSheet.Remarks].Merge = true;

                                        ws1.Cells[(row + 2) - 11, ExcelIncrementSheet.Remarks, row + 2, ExcelIncrementSheet.Remarks].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        ws1.Cells[(row + 2) - 11, ExcelIncrementSheet.Remarks, row + 2, ExcelIncrementSheet.Remarks].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        ws1.Cells[(row + 2) - 11, ExcelIncrementSheet.Remarks, row + 2, ExcelIncrementSheet.Remarks].Style.Locked = false;

                                        ws1.Cells[row + 2, column].Value = "";

                                    }

                                    else if (Convert.ToString(ws1.Cells[row + 2, column].Value).Contains("BASICSALARY_"))
                                    {


                                        ws1.Cells[row + 2, column].Value =Convert.ToInt32(Convert.ToString(ws1.Cells[row + 2, column].Value).Split('_')[1]);

                                        _BasicSalaryRows += (row + 2).ToString() + ",";
                                        _BasicSalarySecondRows += ((row + 2)+1).ToString() + ",";
                                        _BasicSalaryThirdRows += ((row + 2)+2).ToString() + ",";
                                        ws1.Cells[row + 2, column].Style.Numberformat.Format = "#,##";
                                    }
                                    else if (Convert.ToString(ws1.Cells[row + 2, column].Value).Contains("CalculatePF"))
                                    {
                                        ws1.Cells[row + 2, column].Value = "0";
                                        ws1.Cells[row + 2, column].Formula = "=ROUND(" + pfFormula.Replace("[Gross Salary]", "" + ws1.Cells[(row + 2) - 1, ExcelIncrementSheet.NewGrossed_RelocationHardshipAllownce] + "") + ",0)";
                                        ws1.Cells[row + 2, column].Style.Numberformat.Format = "#,##";
                                        
                                    }
                                    else if (Convert.ToString(ws1.Cells[row + 2, column].Value).Contains("_NewGrossPKG"))
                                    {
                                        decimal _Fixed_WPPF_OverTime = Convert.ToDecimal(Convert.ToString(ws1.Cells[row + 2, column].Value).Split('_')[2]);

                                        ws1.Cells[row + 2, column].Value = Convert.ToDouble(Convert.ToString(ws1.Cells[row + 2, column].Value).Split('_')[0]);

                                        ws1.Cells[row + 2, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                        ws1.Cells[row + 2, column].Style.VerticalAlignment = ExcelVerticalAlignment.Top;


                                        ws1.Cells[row + 2, column].Formula = "=ROUND(" + ws1.Cells[row + 2, ExcelIncrementSheet.Proposed_Increment_Inflation_PF] + "+" + ws1.Cells[row + 2, ExcelIncrementSheet.Proposed_Increment_Merit_Bonus] + "+" + ws1.Cells[row + 2, ExcelIncrementSheet.Proposed_Increment_Adj_EOBI] + "+" + ws1.Cells[row + 2, ExcelIncrementSheet.Proposed_Increment_Pro_SS] + "+" + ws1.Cells[row + 2, ExcelIncrementSheet.Proposed_Increment_Total_Mobile] + "+" + ws1.Cells[row + 2, ExcelIncrementSheet.Proposed_Increment_Pecent5_CarAllownce] + "+" + ws1.Cells[row + 2, ExcelIncrementSheet.NewGrossed_RelocationHardshipAllownce] + "+" + ws1.Cells[(row + 2) - 1, ExcelIncrementSheet.NewGrossed_RelocationHardshipAllownce] + "+" + _Fixed_WPPF_OverTime + ",0)";
                                        ws1.Cells[row + 2, column].Style.Numberformat.Format = "#,##";
                                    }

                                    else if (Convert.ToString(ws1.Cells[row + 2, column].Value).Contains("CalculateBonus"))
                                    {
                                        ws1.Cells[row + 2, column].Value = Convert.ToDouble("0");
                                        ws1.Cells[row + 2, column].Formula = "=ROUND(" + bonusFormula.Replace("[Gross Salary]", "" + ws1.Cells[(row + 2) - 1, ExcelIncrementSheet.NewGrossed_RelocationHardshipAllownce] + "") + ",0)";
                                        ws1.Cells[row + 2, column].Style.Numberformat.Format = "#,##";

                                    }

                                    else if (Convert.ToString(ws1.Cells[row + 2, column].Value).Contains("_IncreaseDifferenceAmount"))
                                    {
                                        ws1.Cells[row + 2, column].Value = Convert.ToDouble(Convert.ToString(ws1.Cells[row + 2, column].Value).Split('_')[0]);
                                        ws1.Cells[row + 2, column].Formula = "=ROUND(" + ws1.Cells[row + 2, ExcelIncrementSheet.NewGrossPackage] + "-" + ws1.Cells[row + 2, ExcelIncrementSheet.GrossPackagePrevious] + ",0)";
                                        ws1.Cells[row + 2, column].Style.Numberformat.Format = "#,##";
                                    }


                                    else if (Convert.ToString(ws1.Cells[row + 2, column].Value).Contains("_IncreaseDifferenceAgePercent"))
                                    {
                                        ws1.Cells[row + 2, column].Value = Convert.ToDouble(Convert.ToString(ws1.Cells[row + 2, column].Value).Split('_')[0]);
                                        ws1.Cells[row + 2, column].Formula = "=ROUND((" + ws1.Cells[row + 2, ExcelIncrementSheet.NewGrossPackage] + "-" + ws1.Cells[row + 2, ExcelIncrementSheet.GrossPackagePrevious] + ")/" + ws1.Cells[row + 2, ExcelIncrementSheet.GrossPackagePrevious] + "*100,2)";
                                        ws1.Cells[row + 2, column].Style.Numberformat.Format = "#,##";
                                    }

                                }
                            }

                            _TotalRows += 1;
                        }
                        colFromHex = System.Drawing.ColorTranslator.FromHtml("#ACB9CA");
                        ws1.Cells[6, ExcelIncrementSheet.Proposed_Increment_Inflation_PF, 6, ExcelIncrementSheet.Proposed_Increment_Total_Mobile].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws1.Cells[6, ExcelIncrementSheet.Proposed_Increment_Inflation_PF, 6, ExcelIncrementSheet.Proposed_Increment_Total_Mobile].Style.Fill.BackgroundColor.SetColor(colFromHex);


                        colFromHex = System.Drawing.ColorTranslator.FromHtml("#ACB9CA");
                        ws1.Cells[6, ExcelIncrementSheet.Proposed_Increment_Pecent5_CarAllownce, 6, ExcelIncrementSheet.Proposed_Increment_Pecent5_CarAllownce].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws1.Cells[6, ExcelIncrementSheet.Proposed_Increment_Pecent5_CarAllownce, 6, ExcelIncrementSheet.Proposed_Increment_Pecent5_CarAllownce].Style.Fill.BackgroundColor.SetColor(colFromHex);


                        colFromHex = System.Drawing.ColorTranslator.FromHtml("#A9D08E");
                        ws1.Cells[6, ExcelIncrementSheet.NewGrossed_RelocationHardshipAllownce, 7, ExcelIncrementSheet.NewGrossed_RelocationHardshipAllownce].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws1.Cells[6, ExcelIncrementSheet.NewGrossed_RelocationHardshipAllownce, 7, ExcelIncrementSheet.NewGrossed_RelocationHardshipAllownce].Style.Fill.BackgroundColor.SetColor(colFromHex);



                        colFromHex = System.Drawing.ColorTranslator.FromHtml("#A9D08E");
                        ws1.Cells[7, ExcelIncrementSheet.Proposed_Increment_Inflation_PF, 7, ExcelIncrementSheet.NewGrossed_RelocationHardshipAllownce].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws1.Cells[7, ExcelIncrementSheet.Proposed_Increment_Inflation_PF, 7, ExcelIncrementSheet.NewGrossed_RelocationHardshipAllownce].Style.Fill.BackgroundColor.SetColor(colFromHex);


                        colFromHex = System.Drawing.ColorTranslator.FromHtml("#ACB9CA");
                        ws1.Cells[5, ExcelIncrementSheet.Proposed_Increment_Pecent5_CarAllownce, 5, ExcelIncrementSheet.Proposed_Increment_Pecent5_CarAllownce].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws1.Cells[5, ExcelIncrementSheet.Proposed_Increment_Pecent5_CarAllownce, 5, ExcelIncrementSheet.Proposed_Increment_Pecent5_CarAllownce].Style.Fill.BackgroundColor.SetColor(colFromHex);

                        colFromHex = System.Drawing.ColorTranslator.FromHtml("#ACB9CA");
                        ws1.Cells[5, ExcelIncrementSheet.NewGrossed_RelocationHardshipAllownce, 5, ExcelIncrementSheet.NewGrossed_RelocationHardshipAllownce].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws1.Cells[5, ExcelIncrementSheet.NewGrossed_RelocationHardshipAllownce, 5, ExcelIncrementSheet.NewGrossed_RelocationHardshipAllownce].Style.Fill.BackgroundColor.SetColor(colFromHex);


                        colFromHex = System.Drawing.ColorTranslator.FromHtml("#C9C9C9");
                        ws1.Cells[5, ExcelIncrementSheet.Performance, 7, ExcelIncrementSheet.Performance].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws1.Cells[5, ExcelIncrementSheet.Performance, 7, ExcelIncrementSheet.Performance].Style.Fill.BackgroundColor.SetColor(colFromHex);


                        colFromHex = System.Drawing.ColorTranslator.FromHtml("#C9C9C9");
                        ws1.Cells[5, ExcelIncrementSheet.PPRATING, 7, ExcelIncrementSheet.PPRATING].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws1.Cells[5, ExcelIncrementSheet.PPRATING, 7, ExcelIncrementSheet.PPRATING].Style.Fill.BackgroundColor.SetColor(colFromHex);

                        ////Grand Total First Row
                        
                        ws1.Cells[_TotalRows + 1, ExcelIncrementSheet.Present_Salary_Basic].Formula = GetTotalRowFormula(_BasicSalaryRows, ExcelIncrementSheet.Present_Salary_Basic.ToString(), "sum", ws1);
                        ws1.Cells[_TotalRows + 1, ExcelIncrementSheet.Present_Salary_Basic].Style.Font.Bold = true;

                        ws1.Cells[_TotalRows + 1, ExcelIncrementSheet.Present_Salary_Allow].Formula = GetTotalRowFormula(_BasicSalaryRows, ExcelIncrementSheet.Present_Salary_Allow.ToString(), "sum", ws1);
                        ws1.Cells[_TotalRows + 1, ExcelIncrementSheet.Present_Salary_Allow].Style.Font.Bold = true;

                        ws1.Cells[_TotalRows + 1, ExcelIncrementSheet.Present_Salary_Gross].Formula = GetTotalRowFormula(_BasicSalaryRows, ExcelIncrementSheet.Present_Salary_Gross.ToString(), "sum", ws1);
                        ws1.Cells[_TotalRows + 1, ExcelIncrementSheet.Present_Salary_Gross].Style.Font.Bold = true;

                        ///End Grand Total First Row
                        ///


                        ////Grand Total Second Row

                        ws1.Cells[_TotalRows + 2, ExcelIncrementSheet.Proposed_Increment_Inflation_PF].Formula = GetTotalRowFormula(_BasicSalarySecondRows, ExcelIncrementSheet.Proposed_Increment_Inflation_PF.ToString(), "sum", ws1);
                        ws1.Cells[_TotalRows + 2, ExcelIncrementSheet.Proposed_Increment_Inflation_PF].Style.Font.Bold = true;

                        ws1.Cells[_TotalRows + 2, ExcelIncrementSheet.Proposed_Increment_Percent1].Formula = GetTotalRowFormula(_BasicSalarySecondRows, ExcelIncrementSheet.Proposed_Increment_Percent1.ToString(), "average", ws1);
                        ws1.Cells[_TotalRows + 2, ExcelIncrementSheet.Proposed_Increment_Percent1].Style.Font.Bold = true;

                        ws1.Cells[_TotalRows + 2, ExcelIncrementSheet.Proposed_Increment_Merit_Bonus].Formula = GetTotalRowFormula(_BasicSalarySecondRows, ExcelIncrementSheet.Proposed_Increment_Merit_Bonus.ToString(), "sum", ws1);
                        ws1.Cells[_TotalRows + 2, ExcelIncrementSheet.Proposed_Increment_Merit_Bonus].Style.Font.Bold = true;

                        ws1.Cells[_TotalRows + 2, ExcelIncrementSheet.Proposed_Increment_Percent2].Formula = GetTotalRowFormula(_BasicSalarySecondRows, ExcelIncrementSheet.Proposed_Increment_Percent2.ToString(), "average", ws1);
                        ws1.Cells[_TotalRows + 2, ExcelIncrementSheet.Proposed_Increment_Percent2].Style.Font.Bold = true;



                        ws1.Cells[_TotalRows + 2, ExcelIncrementSheet.Proposed_Increment_Adj_EOBI].Formula = GetTotalRowFormula(_BasicSalarySecondRows, ExcelIncrementSheet.Proposed_Increment_Adj_EOBI.ToString(), "sum", ws1);
                        ws1.Cells[_TotalRows + 2, ExcelIncrementSheet.Proposed_Increment_Adj_EOBI].Style.Font.Bold = true;

                        ws1.Cells[_TotalRows + 2, ExcelIncrementSheet.Proposed_Increment_Percent3].Formula = GetTotalRowFormula(_BasicSalarySecondRows, ExcelIncrementSheet.Proposed_Increment_Percent3.ToString(), "average", ws1);
                        ws1.Cells[_TotalRows + 2, ExcelIncrementSheet.Proposed_Increment_Percent3].Style.Font.Bold = true;


                        ws1.Cells[_TotalRows + 2, ExcelIncrementSheet.Proposed_Increment_Pro_SS].Formula = GetTotalRowFormula(_BasicSalarySecondRows, ExcelIncrementSheet.Proposed_Increment_Pro_SS.ToString(), "sum", ws1);
                        ws1.Cells[_TotalRows + 2, ExcelIncrementSheet.Proposed_Increment_Pro_SS].Style.Font.Bold = true;

                        ws1.Cells[_TotalRows + 2, ExcelIncrementSheet.Proposed_Increment_Percent4].Formula = GetTotalRowFormula(_BasicSalarySecondRows, ExcelIncrementSheet.Proposed_Increment_Percent4.ToString(), "average", ws1);
                        ws1.Cells[_TotalRows + 2, ExcelIncrementSheet.Proposed_Increment_Percent4].Style.Font.Bold = true;


                        ws1.Cells[_TotalRows + 2, ExcelIncrementSheet.Proposed_Increment_Total_Mobile].Formula = GetTotalRowFormula(_BasicSalarySecondRows, ExcelIncrementSheet.Proposed_Increment_Total_Mobile.ToString(), "sum", ws1);
                        ws1.Cells[_TotalRows + 2, ExcelIncrementSheet.Proposed_Increment_Total_Mobile].Style.Font.Bold = true;



                        ws1.Cells[_TotalRows + 2, ExcelIncrementSheet.Proposed_Increment_Pecent5_CarAllownce].Formula = GetTotalRowFormula(_BasicSalarySecondRows, ExcelIncrementSheet.Proposed_Increment_Pecent5_CarAllownce.ToString(), "average", ws1);
                        ws1.Cells[_TotalRows + 2, ExcelIncrementSheet.Proposed_Increment_Pecent5_CarAllownce].Style.Font.Bold = true;


                        ws1.Cells[_TotalRows + 2, ExcelIncrementSheet.NewGrossed_RelocationHardshipAllownce].Formula = GetTotalRowFormula(_BasicSalarySecondRows, ExcelIncrementSheet.NewGrossed_RelocationHardshipAllownce.ToString(), "sum", ws1);
                        ws1.Cells[_TotalRows + 2, ExcelIncrementSheet.NewGrossed_RelocationHardshipAllownce].Style.Font.Bold = true;
                        ///End Grand Total Second Row
                        ///





                        ////Grand Total Third  Row

                        ws1.Cells[_TotalRows + 3, ExcelIncrementSheet.Proposed_Increment_Inflation_PF].Formula = GetTotalRowFormula(_BasicSalaryThirdRows, ExcelIncrementSheet.Proposed_Increment_Inflation_PF.ToString(), "sum", ws1);
                        ws1.Cells[_TotalRows + 3, ExcelIncrementSheet.Proposed_Increment_Inflation_PF].Style.Font.Bold = true;


                        ws1.Cells[_TotalRows + 3, ExcelIncrementSheet.Proposed_Increment_Merit_Bonus].Formula = GetTotalRowFormula(_BasicSalaryThirdRows, ExcelIncrementSheet.Proposed_Increment_Merit_Bonus.ToString(), "sum", ws1);
                        ws1.Cells[_TotalRows + 3, ExcelIncrementSheet.Proposed_Increment_Merit_Bonus].Style.Font.Bold = true;

                    
                        ws1.Cells[_TotalRows + 3, ExcelIncrementSheet.Proposed_Increment_Adj_EOBI].Formula = GetTotalRowFormula(_BasicSalaryThirdRows, ExcelIncrementSheet.Proposed_Increment_Adj_EOBI.ToString(), "sum", ws1);
                        ws1.Cells[_TotalRows + 3, ExcelIncrementSheet.Proposed_Increment_Adj_EOBI].Style.Font.Bold = true;




                        ws1.Cells[_TotalRows + 3, ExcelIncrementSheet.Proposed_Increment_Pro_SS].Formula = GetTotalRowFormula(_BasicSalaryThirdRows, ExcelIncrementSheet.Proposed_Increment_Pro_SS.ToString(), "sum", ws1);
                        ws1.Cells[_TotalRows + 3, ExcelIncrementSheet.Proposed_Increment_Pro_SS].Style.Font.Bold = true;

                        ws1.Cells[_TotalRows + 3, ExcelIncrementSheet.Proposed_Increment_Total_Mobile].Formula = GetTotalRowFormula(_BasicSalaryThirdRows, ExcelIncrementSheet.Proposed_Increment_Total_Mobile.ToString(), "sum", ws1);
                        ws1.Cells[_TotalRows + 3, ExcelIncrementSheet.Proposed_Increment_Total_Mobile].Style.Font.Bold = true;



                        ws1.Cells[_TotalRows + 3, ExcelIncrementSheet.Proposed_Increment_Pecent5_CarAllownce].Formula = GetTotalRowFormula(_BasicSalaryThirdRows, ExcelIncrementSheet.Proposed_Increment_Pecent5_CarAllownce.ToString(), "sum", ws1);
                        ws1.Cells[_TotalRows + 3, ExcelIncrementSheet.Proposed_Increment_Pecent5_CarAllownce].Style.Font.Bold = true;


                        ws1.Cells[_TotalRows + 3, ExcelIncrementSheet.NewGrossed_RelocationHardshipAllownce].Formula = GetTotalRowFormula(_BasicSalaryThirdRows, ExcelIncrementSheet.NewGrossed_RelocationHardshipAllownce.ToString(), "sum", ws1);
                        ws1.Cells[_TotalRows + 3, ExcelIncrementSheet.NewGrossed_RelocationHardshipAllownce].Style.Font.Bold = true;


                        ws1.Cells[_TotalRows + 3, ExcelIncrementSheet.NewGrossPackage].Formula = GetTotalRowFormula(_BasicSalaryThirdRows, ExcelIncrementSheet.NewGrossPackage.ToString(), "sum", ws1);
                        ws1.Cells[_TotalRows + 3, ExcelIncrementSheet.NewGrossPackage].Style.Font.Bold = true;


                        ws1.Cells[_TotalRows + 3, ExcelIncrementSheet.GrossPackagePrevious].Formula = GetTotalRowFormula(_BasicSalaryThirdRows, ExcelIncrementSheet.GrossPackagePrevious.ToString(), "sum", ws1);
                        ws1.Cells[_TotalRows + 3, ExcelIncrementSheet.GrossPackagePrevious].Style.Font.Bold = true;



                        ws1.Cells[_TotalRows + 3, ExcelIncrementSheet.Increse_Amount].Formula = GetTotalRowFormula(_BasicSalaryThirdRows, ExcelIncrementSheet.Increse_Amount.ToString(), "sum", ws1);
                        ws1.Cells[_TotalRows + 3, ExcelIncrementSheet.Increse_Amount].Style.Font.Bold = true;


                        ws1.Cells[_TotalRows + 3, ExcelIncrementSheet.Increse_Perc_Age].Formula = GetTotalRowFormula(_BasicSalaryThirdRows, ExcelIncrementSheet.Increse_Perc_Age.ToString(), "average", ws1);
                        ws1.Cells[_TotalRows + 3, ExcelIncrementSheet.Increse_Perc_Age].Style.Font.Bold = true;
                        ///End Grand Total Third Row

                        string _FileNAme = dt.TableName.Replace(@"/", ""); ;

                        pck.SaveAs(new FileInfo(System.Web.Hosting.HostingEnvironment.MapPath("~/Uploads/HCM_INCREMENT_FILES_SAVE/") + _FileNAme + ".xlsx"));
                        filePath.Add(new ListItem(_FileNAme+ ".xlsx", System.Web.Hosting.HostingEnvironment.MapPath("~/Uploads/HCM_INCREMENT_FILES_SAVE/") + _FileNAme + ".xlsx"));
                    }
                }
                using (ZipFile zip = new ZipFile())
                {
                    zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                    zip.AddDirectoryByName("Files");
                    foreach (ListItem item in filePath)
                    {
                        zip.AddFile(item.Value, "Files");
                    }
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.BufferOutput = false;
                    string zipName = String.Format("IncrementList_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                    HttpContext.Current.Response.ContentType = "application/zip";
                    HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                    zip.Save(HttpContext.Current.Response.OutputStream);
                    HttpContext.Current.Response.End();
                }



                //if (pck.Workbook.Worksheets.Count > 0)
                //{
                //    //Read the Excel file in a byte array  
                //    Byte[] fileBytes = pck.GetAsByteArray();
                //    HttpContext.Current.Response.ClearContent();
                //    // Add the content disposition (file name to be customizable) to be exported.  
                //    HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=INCREMENT PROPOSAL SHEET.xlsx");
                //    // add the required content type  
                //    HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //    // write the bytes to the file and end the response  
                //    HttpContext.Current.Response.BinaryWrite(fileBytes);
                //    HttpContext.Current.Response.End();
                //    pck.Save();
                //}

            }
        }
    }

    private static  string GetTotalRowFormula(string rows, string columns,string formula, ExcelWorksheet ws1) {
        string _Formula = "";
        string _RowsAddition = "";
        string[] _IDs = rows.TrimEnd(',').Split(',');

        foreach (var item in _IDs)
        {
            _RowsAddition += ws1.Cells[Convert.ToInt32(item),Convert.ToInt32(columns)]+",";
        }

        string _TrimValue = _RowsAddition.TrimEnd(',');
        if(formula.ToLower() == "sum")
            _Formula = "=SUM("+ _TrimValue + ")";
        else if(formula.ToLower() == "average")
            _Formula = "=AVERAGE(" + _TrimValue + ")";
        return _Formula;
    
    }
    private string CheckYearValidation(int to_year, int from_year, int selected_year)
    {

        string _ReturnMsg = "";

        if (selected_year != 0)
        {
            if (from_year == 0)
            {
                if (selected_year < to_year)
                {
                    _ReturnMsg = "Selected Year Must Be Next Year Of Selected To Date Or Equal ";
                }
                else if (((to_year - selected_year) > 0) )
                {
                    _ReturnMsg = "Selected Year Must Be Select Only Next Year Of To Date.";
                }
            }
            else if (from_year > 0)
            {
                if (!(selected_year >= from_year && selected_year <= to_year))
                {
                    _ReturnMsg = "Selected Year Must Be Between Of Selected From And To Dates.";
                }
                //else if ((to_year - selected_year) != -1)
                //{
                //    _ReturnMsg = "Selected Year Must Be Select Only Next Year Of To Date.";
                //}
            }
            else
            {
                _ReturnMsg = "";
            }

        }
        else
        {
            _ReturnMsg = "Select Year.";
        }

        return _ReturnMsg;

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
    public void ClosePopup()
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "ClosePopup()", "ClosePopup();", true);
    }
    public void OpenPopup()
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "OpenPopup()", "OpenPopup();", true);
    }

    private void BindEmployee() {
        int CompanyId = 0;

        CompanyId = ddlCompany.SelectedValue == "" ? 0 : Convert.ToInt32(ddlCompany.SelectedValue);
        var _EmployeeListData = context.Setup_Employee.Where(x => x.CompanyId == CompanyId && x.IsActive == true).OrderBy(x => x.EmployeeId).Select(x => new { Id = x.EmployeeId, Value = x.EmployeeCode + "_" + x.FirstName + " " + x.MiddleName + " " + x.LastName }).ToList();
        CommonHelper.BindDropDown(ddlEmployeeAdd, _EmployeeListData, "Value", "Id", _EmployeeListData.Count == 1 ? false : true, false);


    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployee();
    }

    public List<string> GetDataWithOutZero(string CaseName, string BONUS_PAID_DURING_THE_YEAR, string PERFORMANCE_BONUS_PAID_DURING_THE_YEAR
     , string WPPF_PAID_DURING_THE_YEAR, string COMPANY_LOAN_GRANTED, string LOAN_BALANCE_AT_YEAR_END, string OVERTIME_PAID_DURING_THE_YEAR
     , string INCENTIVE_PAID_DURING_THE_YEAR)
    {
        List<string> lst = new List<string>();

        if (CaseName == "BONUS_PAID_DURING_THE_YEAR")
        {
            if (BONUS_PAID_DURING_THE_YEAR != "0")
            {
                lst.Add("BONUS_PAID_DURING_THE_YEAR");
                lst.Add(BONUS_PAID_DURING_THE_YEAR);
            }

            else
            {

                return GetDataWithOutZero("PERFORMANCE_BONUS_PAID_DURING_THE_YEAR", BONUS_PAID_DURING_THE_YEAR, PERFORMANCE_BONUS_PAID_DURING_THE_YEAR
                , WPPF_PAID_DURING_THE_YEAR, COMPANY_LOAN_GRANTED, LOAN_BALANCE_AT_YEAR_END, OVERTIME_PAID_DURING_THE_YEAR
                , INCENTIVE_PAID_DURING_THE_YEAR);
            }
        }

        if (CaseName == "PERFORMANCE_BONUS_PAID_DURING_THE_YEAR")
        {
            if (LastCaption != CaseName && PERFORMANCE_BONUS_PAID_DURING_THE_YEAR != "0")
            {
                LastCaption = "PERFORMANCE_BONUS_PAID_DURING_THE_YEAR";
                lst.Add("PERFORMANCE_BONUS_PAID_DURING_THE_YEAR");
                lst.Add(PERFORMANCE_BONUS_PAID_DURING_THE_YEAR);
            }
            else
            {
                return GetDataWithOutZero("WPPF_PAID_DURING_THE_YEAR", BONUS_PAID_DURING_THE_YEAR, PERFORMANCE_BONUS_PAID_DURING_THE_YEAR
                , WPPF_PAID_DURING_THE_YEAR, COMPANY_LOAN_GRANTED, LOAN_BALANCE_AT_YEAR_END, OVERTIME_PAID_DURING_THE_YEAR
                , INCENTIVE_PAID_DURING_THE_YEAR);
            }
        }

        if (CaseName == "WPPF_PAID_DURING_THE_YEAR")
        {
            if (LastCaption != CaseName && WPPF_PAID_DURING_THE_YEAR != "0")
            {
                LastCaption = "WPPF_PAID_DURING_THE_YEAR";
                lst.Add("WPPF_PAID_DURING_THE_YEAR");
                lst.Add(WPPF_PAID_DURING_THE_YEAR);
            }
            else
            {
                return GetDataWithOutZero("COMPANY_LOAN_GRANTED", BONUS_PAID_DURING_THE_YEAR, PERFORMANCE_BONUS_PAID_DURING_THE_YEAR
                , WPPF_PAID_DURING_THE_YEAR, COMPANY_LOAN_GRANTED, LOAN_BALANCE_AT_YEAR_END, OVERTIME_PAID_DURING_THE_YEAR
                , INCENTIVE_PAID_DURING_THE_YEAR);
            }
        }

        if (CaseName == "COMPANY_LOAN_GRANTED")
        {
            if (LastCaption != CaseName && COMPANY_LOAN_GRANTED != "0")
            {
                LastCaption = "COMPANY_LOAN_GRANTED";
                lst.Add("COMPANY_LOAN_GRANTED");
                lst.Add(COMPANY_LOAN_GRANTED);
            }

            else
            {
                return GetDataWithOutZero("LOAN_BALANCE_AT_YEAR_END", BONUS_PAID_DURING_THE_YEAR, PERFORMANCE_BONUS_PAID_DURING_THE_YEAR
                , WPPF_PAID_DURING_THE_YEAR, COMPANY_LOAN_GRANTED, LOAN_BALANCE_AT_YEAR_END, OVERTIME_PAID_DURING_THE_YEAR
                , INCENTIVE_PAID_DURING_THE_YEAR);
            }
        }

        if (CaseName == "LOAN_BALANCE_AT_YEAR_END")
        {
            if (LastCaption != CaseName && LOAN_BALANCE_AT_YEAR_END != "0")
            {
                LastCaption = "LOAN_BALANCE_AT_YEAR_END";
                lst.Add("LOAN_BALANCE_AT_YEAR_END");
                lst.Add(LOAN_BALANCE_AT_YEAR_END);
            }
            else
            {
                return GetDataWithOutZero("OVERTIME_PAID_DURING_THE_YEAR", BONUS_PAID_DURING_THE_YEAR, PERFORMANCE_BONUS_PAID_DURING_THE_YEAR
               , WPPF_PAID_DURING_THE_YEAR, COMPANY_LOAN_GRANTED, LOAN_BALANCE_AT_YEAR_END, OVERTIME_PAID_DURING_THE_YEAR
               , INCENTIVE_PAID_DURING_THE_YEAR);
            }
        }

        if (CaseName == "OVERTIME_PAID_DURING_THE_YEAR")
        {
            if (LastCaption != CaseName && OVERTIME_PAID_DURING_THE_YEAR != "0")
            {
                LastCaption = "OVERTIME_PAID_DURING_THE_YEAR";
                lst.Add("OVERTIME_PAID_DURING_THE_YEAR");
                lst.Add(OVERTIME_PAID_DURING_THE_YEAR);
            }
            else
            {
                return GetDataWithOutZero("INCENTIVE_PAID_DURING_THE_YEAR", BONUS_PAID_DURING_THE_YEAR, PERFORMANCE_BONUS_PAID_DURING_THE_YEAR
              , WPPF_PAID_DURING_THE_YEAR, COMPANY_LOAN_GRANTED, LOAN_BALANCE_AT_YEAR_END, OVERTIME_PAID_DURING_THE_YEAR
              , INCENTIVE_PAID_DURING_THE_YEAR);
            }
        }

        if (CaseName == "INCENTIVE_PAID_DURING_THE_YEAR")
        {
            if (LastCaption != CaseName && INCENTIVE_PAID_DURING_THE_YEAR != "0")
            {
                LastCaption = "INCENTIVE_PAID_DURING_THE_YEAR";
                lst.Add("INCENTIVE_PAID_DURING_THE_YEAR");
                lst.Add(INCENTIVE_PAID_DURING_THE_YEAR);
            }
            else
            {
                lst.Add("");
                lst.Add("");
            }
        }

        return lst;

    }

    //private List<string> GetDataWithOutZero(string CaseName,string BONUS_PAID_DURING_THE_YEAR, string PERFORMANCE_BONUS_PAID_DURING_THE_YEAR
    //    , string WPPF_PAID_DURING_THE_YEAR, string COMPANY_LOAN_GRANTED, string LOAN_BALANCE_AT_YEAR_END, string OVERTIME_PAID_DURING_THE_YEAR
    //    ,string INCENTIVE_PAID_DURING_THE_YEAR)
    //{
    //    List<string> lst = new List<string>();

    //    if(CaseName == "BONUS_PAID_DURING_THE_YEAR")
    //    {
    //        if(BONUS_PAID_DURING_THE_YEAR!="0")
    //        {
    //            lst.Add("BONUS_PAID_DURING_THE_YEAR_CAPTION");
    //            lst.Add(BONUS_PAID_DURING_THE_YEAR);
    //        }
    //        else if (PERFORMANCE_BONUS_PAID_DURING_THE_YEAR != "0")
    //        {
    //            lst.Add("PERFORMANCE_BONUS_PAID_DURING_THE_YEAR_CAPTION");
    //            lst.Add(PERFORMANCE_BONUS_PAID_DURING_THE_YEAR);
    //        }
    //        else if (WPPF_PAID_DURING_THE_YEAR != "0")
    //        {
    //            lst.Add("WPPF_PAID_DURING_THE_YEAR_CAPTION");
    //            lst.Add(WPPF_PAID_DURING_THE_YEAR);
    //        }
    //        else if (COMPANY_LOAN_GRANTED != "0")
    //        {
    //            lst.Add("COMPANY_LOAN_GRANTED_CAPTION");
    //            lst.Add(COMPANY_LOAN_GRANTED);
    //        }
    //        else if (LOAN_BALANCE_AT_YEAR_END != "0")
    //        {
    //            lst.Add("LOAN_BALANCE_AT_YEAR_END_CAPTION");
    //            lst.Add(LOAN_BALANCE_AT_YEAR_END);
    //        } 
    //        else if (OVERTIME_PAID_DURING_THE_YEAR != "0")
    //        {
    //            lst.Add("OVERTIME_PAID_DURING_THE_YEAR_CAPTION");
    //            lst.Add(OVERTIME_PAID_DURING_THE_YEAR);
    //        } 
    //        else if (INCENTIVE_PAID_DURING_THE_YEAR != "0")
    //        {
    //            lst.Add("INCENTIVE_PAID_DURING_THE_YEAR_CAPTION");
    //            lst.Add(INCENTIVE_PAID_DURING_THE_YEAR);
    //        }
    //        else
    //        {
    //            lst.Add("");
    //            lst.Add("");
    //        }
    //    }

    //    if (CaseName == "PERFORMANCE_BONUS_PAID_DURING_THE_YEAR")
    //    {
    //         if (BONUS_PAID_DURING_THE_YEAR!="0" && PERFORMANCE_BONUS_PAID_DURING_THE_YEAR != "0")
    //        {
    //            lst.Add("PERFORMANCE_BONUS_PAID_DURING_THE_YEAR_CAPTION");
    //            lst.Add(PERFORMANCE_BONUS_PAID_DURING_THE_YEAR);
    //        }
    //        else if (BONUS_PAID_DURING_THE_YEAR != "0" && PERFORMANCE_BONUS_PAID_DURING_THE_YEAR == "0" && WPPF_PAID_DURING_THE_YEAR != "0")
    //        {
    //            lst.Add("WPPF_PAID_DURING_THE_YEAR_CAPTION");
    //            lst.Add(WPPF_PAID_DURING_THE_YEAR);
    //        }
    //        else if (BONUS_PAID_DURING_THE_YEAR != "0" && PERFORMANCE_BONUS_PAID_DURING_THE_YEAR == "0" && COMPANY_LOAN_GRANTED != "0")
    //        {
    //            lst.Add("COMPANY_LOAN_GRANTED_CAPTION");
    //            lst.Add(COMPANY_LOAN_GRANTED);
    //        }
    //        else if (BONUS_PAID_DURING_THE_YEAR != "0" && PERFORMANCE_BONUS_PAID_DURING_THE_YEAR == "0" && LOAN_BALANCE_AT_YEAR_END != "0")
    //        {
    //            lst.Add("LOAN_BALANCE_AT_YEAR_END_CAPTION");
    //            lst.Add(LOAN_BALANCE_AT_YEAR_END);
    //        }
    //        else if (BONUS_PAID_DURING_THE_YEAR != "0" && PERFORMANCE_BONUS_PAID_DURING_THE_YEAR == "0" && OVERTIME_PAID_DURING_THE_YEAR != "0")
    //        {
    //            lst.Add("OVERTIME_PAID_DURING_THE_YEAR_CAPTION");
    //            lst.Add(OVERTIME_PAID_DURING_THE_YEAR);
    //        }
    //        else if (BONUS_PAID_DURING_THE_YEAR != "0" && PERFORMANCE_BONUS_PAID_DURING_THE_YEAR == "0" && INCENTIVE_PAID_DURING_THE_YEAR != "0")
    //        {
    //            lst.Add("INCENTIVE_PAID_DURING_THE_YEAR_CAPTION");
    //            lst.Add(INCENTIVE_PAID_DURING_THE_YEAR);
    //        }
    //        else
    //        {
    //            lst.Add("");
    //            lst.Add("");
    //        }
    //    }

    //    if(CaseName== "WPPF_PAID_DURING_THE_YEAR")
    //    {
    //        if (BONUS_PAID_DURING_THE_YEAR != "0" && PERFORMANCE_BONUS_PAID_DURING_THE_YEAR != "0" && WPPF_PAID_DURING_THE_YEAR != "0")
    //        {
    //            lst.Add("WPPF_PAID_DURING_THE_YEAR_CAPTION");
    //            lst.Add(WPPF_PAID_DURING_THE_YEAR);
    //        }
    //        else if (BONUS_PAID_DURING_THE_YEAR != "0" && PERFORMANCE_BONUS_PAID_DURING_THE_YEAR != "0" && WPPF_PAID_DURING_THE_YEAR == "0" && COMPANY_LOAN_GRANTED != "0")
    //        {
    //            lst.Add("COMPANY_LOAN_GRANTED_CAPTION");
    //            lst.Add(COMPANY_LOAN_GRANTED);
    //        }
    //        else if (BONUS_PAID_DURING_THE_YEAR != "0" && PERFORMANCE_BONUS_PAID_DURING_THE_YEAR == "0" && WPPF_PAID_DURING_THE_YEAR == "0" && LOAN_BALANCE_AT_YEAR_END != "0")
    //        {
    //            lst.Add("LOAN_BALANCE_AT_YEAR_END_CAPTION");
    //            lst.Add(LOAN_BALANCE_AT_YEAR_END);
    //        }
    //        else if (BONUS_PAID_DURING_THE_YEAR != "0" && PERFORMANCE_BONUS_PAID_DURING_THE_YEAR == "0" && WPPF_PAID_DURING_THE_YEAR == "0" && OVERTIME_PAID_DURING_THE_YEAR != "0")
    //        {
    //            lst.Add("OVERTIME_PAID_DURING_THE_YEAR_CAPTION");
    //            lst.Add(OVERTIME_PAID_DURING_THE_YEAR);
    //        }
    //        else if (BONUS_PAID_DURING_THE_YEAR != "0" && PERFORMANCE_BONUS_PAID_DURING_THE_YEAR == "0" && WPPF_PAID_DURING_THE_YEAR == "0" && INCENTIVE_PAID_DURING_THE_YEAR != "0")
    //        {
    //            lst.Add("INCENTIVE_PAID_DURING_THE_YEAR_CAPTION");
    //            lst.Add(INCENTIVE_PAID_DURING_THE_YEAR);
    //        }
    //        else
    //        {
    //            lst.Add("");
    //            lst.Add("");
    //        }
    //    }
    //    return lst;

    //}
}