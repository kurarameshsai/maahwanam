﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaaAahwanam.Models
{
    public class AvailableWhishLists
    {
        [Key]
        public int WhishListID { get; set; }
        public string VendorID { get; set; }
        public string VendorSubID { get; set; }
        public string BusinessName { get; set; }
        public string ServiceType { get; set; }
        public string UserID { get; set; }
        public DateTime WhishListedDate { get; set; }
        public string IPAddress { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public string Ceremony { get; set; }
        public string Budjet { get; set; }
        public DateTime? EventStartDate { get; set; }
        public DateTime? EventEndDate { get; set; }
        public long listId { get; set; }
        public long collabratorid { get; set; }
    }
}
