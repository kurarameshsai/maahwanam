﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MaaAahwanam.Models
{
    public class OrderTransactionLog
    {
        [Key]
        public long TransactionLogID { get; set; }
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
    }
}
