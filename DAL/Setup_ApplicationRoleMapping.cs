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
    
    public partial class Setup_ApplicationRoleMapping
    {
        public int ApplicationRoleId { get; set; }
        public Nullable<int> ApplicationId { get; set; }
        public Nullable<int> User_Code { get; set; }
        public Nullable<int> Role_Code { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<bool> IsLocked { get; set; }
    
        public virtual Setup_Application Setup_Application { get; set; }
        public virtual Setup_Role Setup_Role { get; set; }
        public virtual Setup_User Setup_User { get; set; }
    }
}
