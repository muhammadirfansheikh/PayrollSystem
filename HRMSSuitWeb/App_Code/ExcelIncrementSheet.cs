using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public static class ExcelIncrementSheet
{


    private enum ExcelIncrementSheetColumnOrder
    {

        SrNo = 1,
        EmpCode = 2,
        NameDesignationDojQualification = 3,
        OtherBenefitsName = 4,
        OtherBenefitsValue = 5,
        Increment_Year = 6,
        Increment_Inflation = 7,
        Increment_Merit = 8,
        Increment_Adj = 9,
        Increment_Pro = 10,
        Increment_Total = 11,
        Increment_Percent = 12,
        Increment_Rating = 13,
        Additional_Incr = 14,
        Additional_WEF = 15,
        Present_Salary_Basic = 16,
        Present_Salary_Allow = 17,
        Present_Salary_Gross = 18,
        Performance = 19,
        PPRATING = 20,
        Proposed_Increment_Inflation_PF = 21,
        Proposed_Increment_Percent1 = 22,
        Proposed_Increment_Merit_Bonus = 23,
  
        Proposed_Increment_Percent2 = 24,
        Proposed_Increment_Adj_EOBI = 25,
        Proposed_Increment_Percent3 = 26,
        Proposed_Increment_Pro_SS = 27,
        Proposed_Increment_Percent4 = 28,
        Proposed_Increment_Total_Mobile = 29,
        Proposed_Increment_Pecent5_CarAllownce = 30,
        NewGrossed_RelocationHardshipAllownce = 31,
        NewGrossPackage = 32,
        GrossPackagePrevious = 33,
        Increse_Amount = 34,
        Increse_Perc_Age = 35,
        Remarks = 36

    }
    public static int SrNo
    {
        get
        {
            return Convert.ToInt32(ExcelIncrementSheetColumnOrder.SrNo);
        }

    }



    public static int EmpCode
    {
        get
        {


            return Convert.ToInt32(ExcelIncrementSheetColumnOrder.EmpCode);
        }

    }
    public static int NameDesignationDojQualification
    {
        get
        {


            return Convert.ToInt32(ExcelIncrementSheetColumnOrder.NameDesignationDojQualification);
        }

    }
    public static int OtherBenefitsName
    {
        get
        {


            return Convert.ToInt32(ExcelIncrementSheetColumnOrder.OtherBenefitsName);
        }

    }
    public static int OtherBenefitsValue
    {
        get
        {
            return Convert.ToInt32(ExcelIncrementSheetColumnOrder.OtherBenefitsValue);
        }

    }
    public static int Increment_Year
    {
        get
        {


            return Convert.ToInt32(ExcelIncrementSheetColumnOrder.Increment_Year);
        }

    }
    public static int Increment_Inflation
    {
        get
        {


            return Convert.ToInt32(ExcelIncrementSheetColumnOrder.Increment_Inflation);
        }

    }
    public static int Increment_Merit
    {
        get
        {


            return Convert.ToInt32(ExcelIncrementSheetColumnOrder.Increment_Merit);
        }

    }
    public static int Increment_Adj
    {
        get
        {
            return Convert.ToInt32(ExcelIncrementSheetColumnOrder.Increment_Adj);
        }

    }
    public static int Increment_Pro
    {
        get
        {
            return Convert.ToInt32(ExcelIncrementSheetColumnOrder.Increment_Pro);
        }

    }
    public static int Increment_Total
    {
        get
        {


            return Convert.ToInt32(ExcelIncrementSheetColumnOrder.Increment_Total);
        }

    }
    public static int Increment_Percent
    {
        get
        {


            return Convert.ToInt32(ExcelIncrementSheetColumnOrder.Increment_Percent);
        }

    }
    public static int Increment_Rating
    {
        get
        {


            return Convert.ToInt32(ExcelIncrementSheetColumnOrder.Increment_Rating);
        }

    }
    public static int Additional_Incr
    {
        get
        {


            return Convert.ToInt32(ExcelIncrementSheetColumnOrder.Additional_Incr);
        }

    }
    public static int Additional_WEF
    {
        get
        {


            return Convert.ToInt32(ExcelIncrementSheetColumnOrder.Additional_WEF);
        }

    }
    public static int Present_Salary_Basic
    {
        get
        {


            return Convert.ToInt32(ExcelIncrementSheetColumnOrder.Present_Salary_Basic);
        }

    }
    public static int Present_Salary_Allow
    {
        get
        {


            return Convert.ToInt32(ExcelIncrementSheetColumnOrder.Present_Salary_Allow);
        }

    }
    public static int Present_Salary_Gross
    {
        get
        {


            return Convert.ToInt32(ExcelIncrementSheetColumnOrder.Present_Salary_Gross);
        }

    }
    public static int Performance
    {
        get
        {


            return Convert.ToInt32(ExcelIncrementSheetColumnOrder.Performance);
        }

    }

    public static int PPRATING
    {
        get
        {


            return Convert.ToInt32(ExcelIncrementSheetColumnOrder.PPRATING);
        }

    }
    public static int Proposed_Increment_Inflation_PF
    {
        get
        {


            return Convert.ToInt32(ExcelIncrementSheetColumnOrder.Proposed_Increment_Inflation_PF);
        }

    }
    public static int Proposed_Increment_Percent1
    {
        get
        {


            return Convert.ToInt32(ExcelIncrementSheetColumnOrder.Proposed_Increment_Percent1);
        }

    }
    public static int Proposed_Increment_Merit_Bonus
    {
        get
        {


            return Convert.ToInt32(ExcelIncrementSheetColumnOrder.Proposed_Increment_Merit_Bonus);
        }

    }
    
    public static int Proposed_Increment_Percent2
    {
        get
        {


            return Convert.ToInt32(ExcelIncrementSheetColumnOrder.Proposed_Increment_Percent2);
        }

    }
    public static int Proposed_Increment_Adj_EOBI
    {
        get
        {


            return Convert.ToInt32(ExcelIncrementSheetColumnOrder.Proposed_Increment_Adj_EOBI);
        }

    }
    public static int Proposed_Increment_Percent3
    {
        get
        {


            return Convert.ToInt32(ExcelIncrementSheetColumnOrder.Proposed_Increment_Percent3);
        }

    }
    public static int Proposed_Increment_Pro_SS
    {
        get
        {


            return Convert.ToInt32(ExcelIncrementSheetColumnOrder.Proposed_Increment_Pro_SS);
        }

    }
    public static int Proposed_Increment_Percent4
    {
        get
        {


            return Convert.ToInt32(ExcelIncrementSheetColumnOrder.Proposed_Increment_Percent4);
        }

    }
    public static int Proposed_Increment_Total_Mobile
    {
        get
        {


            return Convert.ToInt32(ExcelIncrementSheetColumnOrder.Proposed_Increment_Total_Mobile);
        }

    }
    public static int Proposed_Increment_Pecent5_CarAllownce
    {
        get
        {


            return Convert.ToInt32(ExcelIncrementSheetColumnOrder.Proposed_Increment_Pecent5_CarAllownce);
        }

    }
    public static int NewGrossed_RelocationHardshipAllownce
    {
        get
        {


            return Convert.ToInt32(ExcelIncrementSheetColumnOrder.NewGrossed_RelocationHardshipAllownce);
        }

    }
    public static int NewGrossPackage
    {
        get
        {


            return Convert.ToInt32(ExcelIncrementSheetColumnOrder.NewGrossPackage);
        }

    }
    public static int GrossPackagePrevious
    {
        get
        {


            return Convert.ToInt32(ExcelIncrementSheetColumnOrder.GrossPackagePrevious);
        }

    }
    public static int Increse_Amount
    {
        get
        {


            return Convert.ToInt32(ExcelIncrementSheetColumnOrder.Increse_Amount);
        }

    }
    public static int Increse_Perc_Age
    {
        get
        {


            return Convert.ToInt32(ExcelIncrementSheetColumnOrder.Increse_Perc_Age);
        }

    }
    public static int Remarks
    {
        get
        {


            return Convert.ToInt32(ExcelIncrementSheetColumnOrder.Remarks);
        }

    }


}