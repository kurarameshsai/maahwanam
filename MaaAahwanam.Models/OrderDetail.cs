using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaaAahwanam.Models
{
    
    public class OrderDetail
    {
        [Key]
        public long OrderDetailId { get; set; }
        public long OrderId { get; set; }
        public long VendorId { get; set; }
        public string ServiceType { get; set; }
        public decimal ServicePrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal PerunitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal DiscountPrice { get; set; }
        public long OrderBy { get; set; }
        public long PaymentId { get; set; }
        public string Status { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long OrderNo { get; set; }
        public long subid { get; set; }
        public bool Isdeal { get; set; }
        public long DealId { get; set; }
        public string attribute { get; set; }
        public DateTime? BookedDate { get; set; }
        public string EventType { get; set; }
        public string ExtraDate1 { get; set; }
        public string ExtraDate2 { get; set; }
        public string ExtraDate3 { get; set; }
        public string ExtraDate4 { get; set; }
        public string ExtraDate5 { get; set; }
        public string ExtraDate6 { get; set; }
        public string type { get; set; }
        public string bookingtype { get; set; }
        public string DiscountType { get; set; }
        public string OrderType { get; set; }
    }
}
