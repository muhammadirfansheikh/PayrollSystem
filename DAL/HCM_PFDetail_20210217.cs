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
    
    public partial class HCM_PFDetail_20210217
    {
        public int PFDetailId { get; set; }
        public int PFMasterId { get; set; }
        public double EmployeeContribution { get; set; }
        public double CompanyContribution { get; set; }
        public double EmployeeBalance { get; set; }
        public double CompanyBalance { get; set; }
        public double InterestIncome { get; set; }
        public Nullable<int> InterestIncomeId { get; set; }
        public Nullable<int> PayrollLogId { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string UserIP { get; set; }
    }
}
