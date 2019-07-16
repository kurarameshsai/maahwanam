using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaaAahwanam.Models
{
    public class Enquirycomment
    {
        [Key]
        public long commenId { get; set; }
        public string comment { get; set; }
        public DateTime commentedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public long enquiryid { get; set; }
        public long userid { get; set; }
        public string IpAddress { get; set; }
    }
}
