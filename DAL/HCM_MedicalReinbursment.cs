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
    
    public partial class HCM_MedicalReinbursment
    {
        public int ReinbursmentId { get; set; }
        public int CompanyId { get; set; }
        public int EmployeeId { get; set; }
        public int EmployeeCode { get; set; }
        public System.DateTime MonthOfReInbursement { get; set; }
        public double PayAmount { get; set; }
        public Nullable<int> PayrollLogId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }
    
        public virtual Setup_Company Setup_Company { get; set; }
        public virtual Setup_Employee Setup_Employee { get; set; }
    }
}