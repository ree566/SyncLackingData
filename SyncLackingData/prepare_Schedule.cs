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
    
    public partial class prepare_Schedule
    {
        public int prepareID { get; set; }
        public Nullable<System.DateTime> ondatetime { get; set; }
        public string PO { get; set; }
        public string Model_name { get; set; }
        public string CLASS { get; set; }
        public string line_name { get; set; }
        public string process_category { get; set; }
        public int prepare_stateID { get; set; }
        public Nullable<System.DateTime> input_time { get; set; }
        public Nullable<System.DateTime> receive_time { get; set; }
        public string prepare_Stime { get; set; }
        public string prepare_Ftime { get; set; }
        public Nullable<System.DateTime> output_time { get; set; }
        public Nullable<System.DateTime> PC_Schedule { get; set; }
        public Nullable<decimal> ORDERS { get; set; }
        public string Reason { get; set; }
    }
}
