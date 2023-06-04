using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_HCM_TaxCertificate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CertificateData"] != null)
        {
            DataSet Ds = (DataSet)Session["CertificateData"];

            if (Ds.Tables.Count > 0)
            {
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    pDateOfIssue.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
                    pIncomeTax.InnerText = Convert.ToDecimal(Ds.Tables[0].Rows[0]["IncomeTax"]).ToString("#,##.00");
                    pIncomeTaxAlphabets.InnerText = Ds.Tables[0].Rows[0]["IncomeTaxInWords"].ToString();
                    pTaxAccountantName.InnerText = Ds.Tables[0].Rows[0]["Name"].ToString();
                    pDeductedAddress.InnerText = Ds.Tables[0].Rows[0]["Address"].ToString();
                    pHolderCINC.InnerText = Ds.Tables[0].Rows[0]["CNIC"].ToString();
                    pFromDate.InnerText = Ds.Tables[0].Rows[0]["From"].ToString();
                    pToDate.InnerText = Ds.Tables[0].Rows[0]["To"].ToString();
                    pUnderSection.InnerText = Ds.Tables[0].Rows[0]["undersection"].ToString();
                    pAmountInRupee.InnerText = Convert.ToDecimal(Ds.Tables[0].Rows[0]["AmountOfRupee"]).ToString("#,##.00");
                    phavingNationalTaxNo.InnerText = Ds.Tables[0].Rows[0]["NTN"].ToString();


                }

                if (Ds.Tables[1].Rows.Count > 0) 
                {
                    pCompanyName.InnerText = Ds.Tables[1].Rows[0]["CompanyName"].ToString();
                    pCompanyAddress.InnerText = Ds.Tables[1].Rows[0]["Address"].ToString();
                    pCustomerName.InnerText = Ds.Tables[1].Rows[0]["Name"].ToString();
                    pCompanyNTN.InnerText = Ds.Tables[1].Rows[0]["CompanyNTN"].ToString();
                    pDesignation.InnerText = Ds.Tables[1].Rows[0]["DesignationName"].ToString();
                    pDateOFIssueSecond.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
                }
               
            }
           
        }
    }
}