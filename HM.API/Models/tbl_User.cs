//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HM.API.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public Nullable<int> RoleId { get; set; }
        public Nullable<bool> Inactive { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
    }
}
