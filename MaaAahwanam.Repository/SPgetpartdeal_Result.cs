//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MaaAahwanam.Repository
{
    using System;
    
    public partial class SPgetpartdeal_Result
    {
        public long DealID { get; set; }
        public long VendorId { get; set; }
        public long VendorSubId { get; set; }
        public string VendorType { get; set; }
        public string VendorSubType { get; set; }
        public string Category { get; set; }
        public Nullable<System.DateTime> DealStartDate { get; set; }
        public Nullable<System.DateTime> DealEndDate { get; set; }
        public decimal DealPrice { get; set; }
        public decimal OriginalPrice { get; set; }
        public string DealDescription { get; set; }
        public string FoodType { get; set; }
        public string MinMemberCount { get; set; }
        public string MaxMemberCount { get; set; }
        public string TermsConditions { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string TimeSlot { get; set; }
        public string DealName { get; set; }
    }
}
