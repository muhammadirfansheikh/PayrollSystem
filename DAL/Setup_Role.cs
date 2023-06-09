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
    
    public partial class Setup_Role
    {
        public Setup_Role()
        {
            this.Setup_ApplicationRoleMapping = new HashSet<Setup_ApplicationRoleMapping>();
            this.Setup_RoleAccess = new HashSet<Setup_RoleAccess>();
        }
    
        public int Role_Code { get; set; }
        public string Role_Name { get; set; }
        public Nullable<int> ApplicationId { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public Nullable<bool> Is_Active { get; set; }
        public Nullable<int> User_Code { get; set; }
        public Nullable<System.DateTime> Modified_Date { get; set; }
        public string User_IP { get; set; }
        public Nullable<int> Company_Code { get; set; }
        public Nullable<int> TAT { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string DefaultPage { get; set; }
    
        public virtual Setup_Application Setup_Application { get; set; }
        public virtual ICollection<Setup_ApplicationRoleMapping> Setup_ApplicationRoleMapping { get; set; }
        public virtual ICollection<Setup_RoleAccess> Setup_RoleAccess { get; set; }
    }
}
