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
    
    public partial class HCM_Salary_OverTimeHistory
    {
        public int OvertimeId { get; set; }
        public double OverTimeHours { get; set; }
        public int OverTimeRateId { get; set; }
        public double OverTimeAmount { get; set; }
        public System.DateTime ReleaseDate { get; set; }
        public int EmployeeAllowanceId { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        public virtual HCM_Company_Settings HCM_Company_Settings { get; set; }
        public virtual HCM_Payroll_Detail HCM_Payroll_Detail { get; set; }
    }
}
