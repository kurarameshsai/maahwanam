﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaaAahwanam.Models
{
    public class OrderRequest
    {
        public string EventId { get; set; }
        public long OrderId { get; set; }
        public string EventName { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Status { get; set; }
        public List<EventDate> EventDates { get; set; }
        public List<EventInformation> Eventinformation { get; set; }

        public List<OrderDetail> OrderDetail { get; set; }
        public long OrderedBy { get; set; }
        public long OrderNumber { get; set; }
        public long PaymentId { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal TotalPrice { get; set; }

        public long VendorId { get; set; }
        public string ServiceType { get; set; }
        public decimal ServicePrice { get; set; }
        public decimal Perunitprice { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal DiscountPrice { get; set; }
        public string attribute { get; set; }
        public long cid { get; set; }
        public long subid { get; set; }
        public bool Isdeal { get; set; }
        public List<CartItem> Cartitems { get; set; }

        //public long PaymentID { get; set; }
        //public long OrderID { get; set; }
        public decimal paidamount { get; set; }
        public string cardnumber { get; set; }
        public string CVV { get; set; }
        public DateTime Paiddate { get; set; }
        public long RequestID { get; set; }
    }
}
