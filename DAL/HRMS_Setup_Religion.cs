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
    
    public partial class HRMS_Setup_Religion
    {
        public HRMS_Setup_Religion()
        {
            this.Setup_Employee = new HashSet<Setup_Employee>();
        }
    
        public int ReligionId { get; set; }
        public string ReligionTitle { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string UserIP { get; set; }
    
        public virtual ICollection<Setup_Employee> Setup_Employee { get; set; }
    }
}
