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
    
    public partial class ThirdParty_Menu_User_Mapping
    {
        public int ThirdPartyMenuUserMappingID { get; set; }
        public Nullable<int> MenuItemId { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDateTime { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDateTime { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
    
        public virtual Setup_MenuItem Setup_MenuItem { get; set; }
        public virtual Setup_User Setup_User { get; set; }
    }
}
