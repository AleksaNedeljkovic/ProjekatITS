//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Projektni_centar_proba.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SiteUser
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Nullable<bool> IsValid { get; set; }
        public string TipKorisnika { get; set; }
    }
}
