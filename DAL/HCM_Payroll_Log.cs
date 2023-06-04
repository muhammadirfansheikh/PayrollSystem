//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class HCM_Payroll_Log
    {
        public HCM_Payroll_Log()
        {
            this.HCM_EmployeeSESSI_Details = new HashSet<HCM_EmployeeSESSI_Details>();
            this.HCM_EmployeeTaxDetails = new HashSet<HCM_EmployeeTaxDetails>();
            this.HCM_EmployeeTaxDirectDeduction = new HashSet<HCM_EmployeeTaxDirectDeduction>();
            this.HCM_Loan_Detail = new HashSet<HCM_Loan_Detail>();
            this.HCM_PayrollDisplay = new HashSet<HCM_PayrollDisplay>();
            this.HCM_ProvidentFund = new HashSet<HCM_ProvidentFund>();
            this.HCM_Payroll_Master = new HashSet<HCM_Payroll_Master>();
            this.HCM_Settlement_Detail = new HashSet<HCM_Settlement_Detail>();
            this.HCM_Vehicle_Detail = new HashSet<HCM_Vehicle_Detail>();
        }
    
        public int PayrollLogId { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public System.DateTime PayrollDate { get; set; }
        public bool IsLocked { get; set; }
        public Nullable<double> TotalAllowances { get; set; }
        public Nullable<double> TotalDeduction { get; set; }
        public Nullable<double> TotalMaster { get; set; }
        public int Createdby { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> Modifiedby { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsArrearRelease { get; set; }
        public bool IsBonusRelease { get; set; }
        public Nullable<int> YearId { get; set; }
    
        public virtual ICollection<HCM_EmployeeSESSI_Details> HCM_EmployeeSESSI_Details { get; set; }
        public virtual ICollection<HCM_EmployeeTaxDetails> HCM_EmployeeTaxDetails { get; set; }
        public virtual ICollection<HCM_EmployeeTaxDirectDeduction> HCM_EmployeeTaxDirectDeduction { get; set; }
        public virtual ICollection<HCM_Loan_Detail> HCM_Loan_Detail { get; set; }
        public virtual HCM_Setup_Year HCM_Setup_Year { get; set; }
        public virtual ICollection<HCM_PayrollDisplay> HCM_PayrollDisplay { get; set; }
        public virtual ICollection<HCM_ProvidentFund> HCM_ProvidentFund { get; set; }
        public virtual ICollection<HCM_Payroll_Master> HCM_Payroll_Master { get; set; }
        public virtual ICollection<HCM_Settlement_Detail> HCM_Settlement_Detail { get; set; }
        public virtual ICollection<HCM_Vehicle_Detail> HCM_Vehicle_Detail { get; set; }
    }
}
