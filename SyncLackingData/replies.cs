//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SyncLackingData
{
    using System;
    using System.Collections.Generic;
    
    public partial class replies
    {
        public long id { get; set; }
        public long order_id { get; set; }
        public long team_id { get; set; }
        public string comment { get; set; }
        public string user_id { get; set; }
        public System.DateTime time { get; set; }
    
        public virtual orders orders { get; set; }
        public virtual users users { get; set; }
    }
}
