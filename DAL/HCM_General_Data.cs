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
    
    public partial class HCM_General_Data
    {
        public int GeneralDataId { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<double> Amount { get; set; }
        public Nullable<int> TypeId { get; set; }
        public Nullable<int> YearId { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string UserIP { get; set; }
    
        public virtual HCM_Setup_Detail HCM_Setup_Detail { get; set; }
        public virtual HCM_Setup_Year HCM_Setup_Year { get; set; }
        public virtual Setup_Employee Setup_Employee { get; set; }
    }
}
