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
    
    public partial class HRMS_ApplicantApplication
    {
        public HRMS_ApplicantApplication()
        {
            this.HRMS_CandidateInfo = new HashSet<HRMS_CandidateInfo>();
        }
    
        public int ApplicantApplicationId { get; set; }
        public int ApplicantId { get; set; }
        public Nullable<int> RequisitionId { get; set; }
        public string Filename { get; set; }
        public string Filecomments { get; set; }
        public string Filetype { get; set; }
        public string FileOriginalName { get; set; }
        public int FileStatusId { get; set; }
        public Nullable<double> LastDrawnSalary { get; set; }
        public Nullable<double> ExpectedSalary { get; set; }
        public Nullable<System.DateTime> ExpectedJoiningDate { get; set; }
        public Nullable<int> NoticePeriodDays { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsWebSitePath { get; set; }
    
        public virtual HRMS_Requisition HRMS_Requisition { get; set; }
        public virtual HRMS_Setup_Applicant HRMS_Setup_Applicant { get; set; }
        public virtual HRMS_SetupDetail HRMS_SetupDetail { get; set; }
        public virtual ICollection<HRMS_CandidateInfo> HRMS_CandidateInfo { get; set; }
    }
}