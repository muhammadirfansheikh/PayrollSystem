using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_HCM_LoanDetailHistory : System.Web.UI.Page
{
    DAL.Sybrid_DatabaseEntities context = new DAL.Sybrid_DatabaseEntities();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["LoanMasterID"] != null)
        {
            DataSet _DataSet = GetLoanData(Convert.ToInt32(Request.QueryString["LoanMasterID"]));

            BindData(_DataSet);
        }
        else
        {
            Error("Loan Master ID Is Null.");
        }


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


    private void BindData(DataSet ds)
    {
        if (ds.Tables.Count > 0)
        {
            BindLoanMaster(ds.Tables[0]);
            BindLoanInstallmentData(ds.Tables[1]);
            BindLoanSettlementData(ds.Tables[2]);
        }
        else
        {
            Error("Data Not Found");
        }

    }

    private void BindLoanInstallmentData(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            grdLoanHistory.DataSource = dt;
            grdLoanHistory.DataBind();
        }
        else
        {
            grdLoanHistory.DataSource = null;
            grdLoanHistory.DataBind();
        }
    }

    private void BindLoanSettlementData(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            grdLoanSettlement.DataSource = dt;
            grdLoanSettlement.DataBind();
        }
        else
        {
            grdLoanSettlement.DataSource = null;
            grdLoanSettlement.DataBind();
        }
    }
    private void BindLoanMaster(DataTable dt)
    {

        if (dt.Rows.Count > 0)
        {
            lblEmpName.Text = dt.Rows[0]["EmployeeName"].ToString();
            lblLoanType.Text = dt.Rows[0]["ColumnValue"].ToString();
            lblLoanAmount.Text = dt.Rows[0]["LoanAmount"] != DBNull.Value ? Convert.ToDecimal(dt.Rows[0]["LoanAmount"]).ToString("#,##") : "0";
            lblLoanWithInt.Text = dt.Rows[0]["LoanAmountWithInterest"] != DBNull.Value ? Convert.ToDecimal(dt.Rows[0]["LoanAmountWithInterest"]).ToString("#,##") : "0";
            lblInstallmentAmt.Text = dt.Rows[0]["InstallmentAmount"] != DBNull.Value ? Convert.ToDecimal(dt.Rows[0]["InstallmentAmount"]).ToString("#,##") : "0";
            lblLoanBlnc.Text = dt.Rows[0]["LoanBalance"] != DBNull.Value ? Convert.ToDecimal(dt.Rows[0]["LoanBalance"]).ToString("#,##") : "0";
            lblLoanApplyDate.Text = Convert.ToDateTime(dt.Rows[0]["CreatedDate"]).ToString("yyyy-MM-dd");
            lblSanctionDate.Text = dt.Rows[0]["SanctionDate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["SanctionDate"]).ToString("yyyy-MM-dd") : "";
            lblLoanGivenDate.Text = dt.Rows[0]["LoanGivenDate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["LoanGivenDate"]).ToString("yyyy-MM-dd") : "";
            lblSettlementDate.Text = dt.Rows[0]["SettlementDate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["SettlementDate"]).ToString("yyyy-MM-dd") : "";
            lblCurrentMonthInsTillDate.Text = dt.Rows[0]["CurrentMonthInstallmentTillDate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["CurrentMonthInstallmentTillDate"]).ToString("yyyy-MM-dd") : "";
            lblCurrentMonthInst.Text = dt.Rows[0]["CurrentMonthInstallment"] != DBNull.Value ? Convert.ToDecimal(dt.Rows[0]["CurrentMonthInstallment"]).ToString("#,##") : "0";
            lblIsHold.Text = dt.Rows[0]["IsHold"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["IsHold"].ToString()) == true ? "Yes" : "No" : "No";
            lblIsSettled.Text = dt.Rows[0]["IsSettled"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["IsSettled"].ToString()) == true ? "Yes" : "No" : "No";
            lblInterestRate.Text = dt.Rows[0]["InterestRate"] != DBNull.Value ? Convert.ToDecimal(dt.Rows[0]["InterestRate"]).ToString("#,##") : "0";
            lblInterestAmount.Text = dt.Rows[0]["InterestAmount"] != DBNull.Value ? Convert.ToDecimal(dt.Rows[0]["InterestAmount"]).ToString("#,##") : "0";
            lblReason.Text = dt.Rows[0]["Reason"].ToString();
            lblComments.Text = dt.Rows[0]["Comments"].ToString();
        }
        else
        {
            lblEmpName.Text = "";
            lblLoanType.Text = "";
            lblLoanAmount.Text = "";
            lblLoanWithInt.Text = "";
            lblInstallmentAmt.Text = "";
            lblLoanBlnc.Text = "";
            lblLoanApplyDate.Text = "";
            lblSanctionDate.Text = "";
            lblSettlementDate.Text = "";
            lblCurrentMonthInsTillDate.Text = "";
            lblCurrentMonthInst.Text = "";
            lblIsHold.Text = "";
            lblIsSettled.Text = "";
            lblInterestRate.Text = "";
            lblInterestAmount.Text = "";
            lblReason.Text = "";
            lblComments.Text = "";
        }

    }
    private DataSet GetLoanData(int loanMasterId)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string dbConnectionString = context.Database.Connection.ConnectionString;
        SqlConnection con = new SqlConnection(dbConnectionString);
        SqlDataAdapter da = new SqlDataAdapter("SP_HCM_Get_Loan_AND_PaymentHistory", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.CommandTimeout = Int32.MaxValue;

        da.SelectCommand.Parameters.Add("@LoanMasterId", SqlDbType.Int).Value = loanMasterId;




        da.Fill(ds);


        return ds;
    }
}