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
    
    public partial class HRMS_ResignationDetail
    {
        public int ResignationDetailId { get; set; }
        public int EmployeeId { get; set; }
        public Nullable<int> WorkflowTaskMasterId { get; set; }
        public string ResignationDetail { get; set; }
        public System.DateTime LastWorkingDate { get; set; }
        public Nullable<int> HODRCAReasonId { get; set; }
        public string HODComments { get; set; }
        public Nullable<int> HRRCAReasonId { get; set; }
        public string HRComments { get; set; }
        public Nullable<bool> IsResignTakeBack { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string UserIP { get; set; }
        public string Comment { get; set; }
    
        public virtual Setup_Employee Setup_Employee { get; set; }
        public virtual HRMS_Setup_RCAReason HRMS_Setup_RCAReason { get; set; }
        public virtual HRMS_Setup_RCAReason HRMS_Setup_RCAReason1 { get; set; }
        public virtual HRMS_WorkFlowTaskMaster HRMS_WorkFlowTaskMaster { get; set; }
    }
}
