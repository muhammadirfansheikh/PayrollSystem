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
    
    public partial class HCM_Payroll_Master_20210709
    {
        public int PayrollMasterId { get; set; }
        public int EmployeeId { get; set; }
        public int PayrollLogId { get; set; }
        public Nullable<double> StandardGrossSalary { get; set; }
        public Nullable<double> GrossSalary { get; set; }
        public double TotalSalary { get; set; }
        public double TotalDeduction { get; set; }
        public double TotalAllowances { get; set; }
        public Nullable<double> TotalTaxableAmount { get; set; }
        public Nullable<double> OtherDeduction { get; set; }
        public bool IsDispersed { get; set; }
        public Nullable<System.DateTime> DispersedDate { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
