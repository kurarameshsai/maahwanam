﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaaAahwanam.Models
{
    public class Collabrator
    {
        [Key]
        public long Id { get; set; }
        public long UserId { get; set; }
        public long wishlistid { get; set; }
        public string Email { get; set; }
        public string collabratorname { get; set; }
        public string PhoneNo { get; set; }
        public DateTime UpdatedDate { get; set; }
       public string wishlistlink { get; set; }
    }
}
