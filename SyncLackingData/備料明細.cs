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
    
    public partial class 備料明細
    {
        public string 訂單 { get; set; }
        public string 需求溯源 { get; set; }
        public string 物料 { get; set; }
        public string SLoc { get; set; }
        public string 生產儲位 { get; set; }
        public string 狀態 { get; set; }
        public Nullable<System.DateTime> 需求日期 { get; set; }
        public Nullable<double> 需求數量 { get; set; }
        public Nullable<double> 領料數量 { get; set; }
        public string 採購群組 { get; set; }
        public string 負責人 { get; set; }
        public string 工位 { get; set; }
        public Nullable<System.DateTime> 上傳日期 { get; set; }
        public Nullable<System.DateTime> 領料日期 { get; set; }
        public int id { get; set; }
        public string 儲格 { get; set; }
        public Nullable<double> 缺料數量 { get; set; }
        public string 儲格分類 { get; set; }
        public string 物管 { get; set; }
        public string UnitsInStock { get; set; }
        public Nullable<System.DateTime> 回覆日期 { get; set; }
        public string 領料人 { get; set; }
        public Nullable<System.DateTime> SAP_MdDate { get; set; }
        public string MAT_CAPTION_ORDER { get; set; }
    }
}
