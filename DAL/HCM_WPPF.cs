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
    
    public partial class HCM_WPPF
    {
        public int WPPF_ID { get; set; }
        public int EmployeeId { get; set; }
        public int CompanyId { get; set; }
        public int YearId { get; set; }
        public int Slab_Id { get; set; }
        public Nullable<double> UnitRate { get; set; }
        public Nullable<double> MaxUnitRate { get; set; }
        public Nullable<double> InterestRate { get; set; }
        public Nullable<double> MaxInterestRate { get; set; }
        public Nullable<double> UnitRateAmount { get; set; }
        public Nullable<double> InterestRateAmount { get; set; }
        public Nullable<double> Total_WPPF { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        public virtual HCM_Setup_Wppf_Slab HCM_Setup_Wppf_Slab { get; set; }
        public virtual HCM_Setup_Year HCM_Setup_Year { get; set; }
        public virtual HCM_Setup_Year HCM_Setup_Year1 { get; set; }
        public virtual Setup_Company Setup_Company { get; set; }
        public virtual Setup_Company Setup_Company1 { get; set; }
        public virtual Setup_Employee Setup_Employee { get; set; }
    }
}