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
    
    public partial class HCM_Arrear_Amounts
    {
        public int ArrearId { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<int> AllowanceId { get; set; }
        public Nullable<double> Amount { get; set; }
        public Nullable<System.DateTime> MonthValue { get; set; }
        public Nullable<int> PayrollId { get; set; }
        public Nullable<System.DateTime> ReleaseDate { get; set; }
        public Nullable<bool> IsDeduction { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
    }
}
