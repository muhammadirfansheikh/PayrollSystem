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
    
    public partial class HCM_VehicleDesignationMapping
    {
        public int VehicleDesignatiionMappingId { get; set; }
        public int DesignationId { get; set; }
        public int VehicleId { get; set; }
        public Nullable<bool> IsUpgradeVehicle { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> VehicleInformationId { get; set; }
    
        public virtual HCM_Setup_Detail HCM_Setup_Detail { get; set; }
        public virtual HCM_VehicleInformation HCM_VehicleInformation { get; set; }
    }
}
