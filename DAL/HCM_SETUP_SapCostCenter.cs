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
    
    public partial class HCM_SETUP_SapCostCenter
    {
        public int SapCostId { get; set; }
        public string SapCostCenterCode { get; set; }
        public string SapCostCenter { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public bool IsActitve { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public string ThirdPartyMappingId { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string UserIP { get; set; }
    
        public virtual Setup_Company Setup_Company { get; set; }
    }
}