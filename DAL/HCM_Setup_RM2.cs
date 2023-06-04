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
    
    public partial class HCM_Setup_RM2
    {
        public int SetupRMId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> DesignationId { get; set; }
        public Nullable<double> FuelInLitres { get; set; }
        public Nullable<double> RM_FirstYear { get; set; }
        public Nullable<double> RM_SecondYear { get; set; }
        public Nullable<double> RM_ThirdYear { get; set; }
        public Nullable<double> RM_ForthYear { get; set; }
        public Nullable<double> RM_FifthYear { get; set; }
        public Nullable<bool> IsOnActual { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<double> IncreasePercentage { get; set; }
        public Nullable<System.DateTime> IncreaseDate { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public bool IsFixed { get; set; }
    
        public virtual Setup_Category Setup_Category { get; set; }
        public virtual Setup_Designation Setup_Designation { get; set; }
    }
}
