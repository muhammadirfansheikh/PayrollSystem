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
    
    public partial class Setup_RoleAccessFunction
    {
        public int Role_Access_Function_Code { get; set; }
        public Nullable<int> Access_Function_Code { get; set; }
        public Nullable<int> Role_Code { get; set; }
        public Nullable<int> Menu_Item_Code { get; set; }
    
        public virtual Setup_MenuItem Setup_MenuItem { get; set; }
        public virtual TMS_AccessFunction TMS_AccessFunction { get; set; }
    }
}
