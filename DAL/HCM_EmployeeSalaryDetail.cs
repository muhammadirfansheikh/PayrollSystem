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
    
    public partial class HCM_EmployeeSalaryDetail
    {
        public int EmployeeSalaryDetailId { get; set; }
        public Nullable<int> EmployeeSalaryId { get; set; }
        public int EmployeeId { get; set; }
        public int AllowanceId { get; set; }
        public Nullable<double> Amount { get; set; }
        public bool IsBonus { get; set; }
        public bool IsTaxableIncome { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
    }
}
