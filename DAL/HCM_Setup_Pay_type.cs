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
    
    public partial class HCM_Setup_Pay_type
    {
        public HCM_Setup_Pay_type()
        {
            this.HCM_Loan_Detail = new HashSet<HCM_Loan_Detail>();
            this.HCM_Settlement_Detail = new HashSet<HCM_Settlement_Detail>();
        }
    
        public int Pay_typeID { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public string Pay_method { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public string UserIP { get; set; }
    
        public virtual ICollection<HCM_Loan_Detail> HCM_Loan_Detail { get; set; }
        public virtual ICollection<HCM_Settlement_Detail> HCM_Settlement_Detail { get; set; }
        public virtual Setup_Company Setup_Company { get; set; }
    }
}
