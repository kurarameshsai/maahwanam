﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaaAahwanam.Models
{
    public class UserDetail
    {
        [Key]
        public long UserDetailId { get; set; }
        public long UserLoginId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AlternativeEmailID { get; set; }
        public string Country { get; set; }
        public string Gender { get; set; }
        public string Landmark { get; set; }
        public string UserPhone { get; set; }
        public string Url { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string UserImgId { get; set; }
        public string UserImgName { get; set; }
        public string Status { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string name { get; set; }
        
    }
}
