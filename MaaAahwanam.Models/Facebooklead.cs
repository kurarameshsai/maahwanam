using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaaAahwanam.Models
{
    public class Facebooklead
    {
        [Key]
        public long facebookLeadId { get; set; }
        public string Name { get; set; }
        public string email { get; set; }
        public string phoneno { get; set; }
        public DateTime? EventDate { get; set; }
        public string city { get; set; }
        public string EnquiryRegarding { get; set; }
        public string Event { get; set; }
        public string status { get; set; }
        public string leadsource { get; set; }
        public DateTime? EnquiryDate { get; set; }
        public string campaign { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string description { get; set; }
       public string firstcontact { get; set; }
    }
}
