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
    
    public partial class HCM_Employee_Education_Detail_20230109
    {
        public int EmployeeEducationDetailId { get; set; }
        public int EmployeeId { get; set; }
        public int EducationDegreeId { get; set; }
        public string NameOfUniversity { get; set; }
        public Nullable<int> YearOfDegree { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string UserIP { get; set; }
        public int Company_id { get; set; }
        public string MajorEducation { get; set; }
    }
}
