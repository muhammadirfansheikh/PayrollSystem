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
    
    public partial class HCM_Setup_Year
    {
        public HCM_Setup_Year()
        {
            this.HCM_EmployeeTaxForecast = new HashSet<HCM_EmployeeTaxForecast>();
            this.HCM_LeaveEncashment = new HashSet<HCM_LeaveEncashment>();
            this.HCM_Payroll_Log = new HashSet<HCM_Payroll_Log>();
            this.HCM_Setup_Tax_Slab = new HashSet<HCM_Setup_Tax_Slab>();
            this.HCM_Setup_TaxLaw = new HashSet<HCM_Setup_TaxLaw>();
            this.HCM_Setup_Wppf_Slab = new HashSet<HCM_Setup_Wppf_Slab>();
            this.HCM_WPPF = new HashSet<HCM_WPPF>();
            this.HCM_TaxComputation = new HashSet<HCM_TaxComputation>();
            this.HCM_WPPF1 = new HashSet<HCM_WPPF>();
        }
    
        public int YearId { get; set; }
        public Nullable<System.DateTime> YearFrom { get; set; }
        public Nullable<System.DateTime> YearTo { get; set; }
        public int CompanyId { get; set; }
        public bool IsCurrentActiveYear { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        public virtual ICollection<HCM_EmployeeTaxForecast> HCM_EmployeeTaxForecast { get; set; }
        public virtual ICollection<HCM_LeaveEncashment> HCM_LeaveEncashment { get; set; }
        public virtual ICollection<HCM_Payroll_Log> HCM_Payroll_Log { get; set; }
        public virtual ICollection<HCM_Setup_Tax_Slab> HCM_Setup_Tax_Slab { get; set; }
        public virtual ICollection<HCM_Setup_TaxLaw> HCM_Setup_TaxLaw { get; set; }
        public virtual ICollection<HCM_Setup_Wppf_Slab> HCM_Setup_Wppf_Slab { get; set; }
        public virtual ICollection<HCM_WPPF> HCM_WPPF { get; set; }
        public virtual Setup_Company Setup_Company { get; set; }
        public virtual ICollection<HCM_TaxComputation> HCM_TaxComputation { get; set; }
        public virtual ICollection<HCM_WPPF> HCM_WPPF1 { get; set; }
    }
}
