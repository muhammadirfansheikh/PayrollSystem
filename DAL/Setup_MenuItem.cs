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
    
    public partial class Setup_MenuItem
    {
        public Setup_MenuItem()
        {
            this.Setup_RoleAccess = new HashSet<Setup_RoleAccess>();
            this.Setup_RoleAccessFunction = new HashSet<Setup_RoleAccessFunction>();
            this.Setup_MenuItem1 = new HashSet<Setup_MenuItem>();
            this.Setup_RoleAccess1 = new HashSet<Setup_RoleAccess>();
            this.ThirdParty_Menu_User_Mapping = new HashSet<ThirdParty_Menu_User_Mapping>();
        }
    
        public int Menu_Item_Code { get; set; }
        public string Menu_Item_Name { get; set; }
        public string Menu_URL { get; set; }
        public Nullable<int> Parent_Menu_Item_Code { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public Nullable<bool> Is_Active { get; set; }
        public Nullable<bool> Is_Displayed_In_Menu { get; set; }
        public Nullable<int> User_Code { get; set; }
        public Nullable<System.DateTime> Modified_Date { get; set; }
        public string User_IP { get; set; }
        public Nullable<int> Company_Code { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public Nullable<int> ApplicationId { get; set; }
        public string Menu_Image { get; set; }
        public Nullable<bool> Show_NewTab { get; set; }
    
        public virtual Setup_Application Setup_Application { get; set; }
        public virtual ICollection<Setup_RoleAccess> Setup_RoleAccess { get; set; }
        public virtual ICollection<Setup_RoleAccessFunction> Setup_RoleAccessFunction { get; set; }
        public virtual ICollection<Setup_MenuItem> Setup_MenuItem1 { get; set; }
        public virtual Setup_MenuItem Setup_MenuItem2 { get; set; }
        public virtual Setup_MenuItem Setup_MenuItem11 { get; set; }
        public virtual Setup_MenuItem Setup_MenuItem3 { get; set; }
        public virtual ICollection<Setup_RoleAccess> Setup_RoleAccess1 { get; set; }
        public virtual ICollection<ThirdParty_Menu_User_Mapping> ThirdParty_Menu_User_Mapping { get; set; }
    }
}
