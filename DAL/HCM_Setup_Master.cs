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
    
    public partial class HCM_Setup_Master
    {
        public HCM_Setup_Master()
        {
            this.HCM_Setup_Definitions = new HashSet<HCM_Setup_Definitions>();
        }
    
        public int SetupMasterID { get; set; }
        public string SetupName { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public string UserIP { get; set; }
        public bool IsDisplayInMenu { get; set; }
        public bool IsSetting { get; set; }
    
        public virtual ICollection<HCM_Setup_Definitions> HCM_Setup_Definitions { get; set; }
    }
}