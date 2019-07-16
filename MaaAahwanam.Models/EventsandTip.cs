using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaaAahwanam.Models
{
    
    public class EventsandTip
    {
        [Key]
        public long EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Image { get; set; }
        public string Phone { get; set; }
        public decimal Price { get; set; }
        public string Person { get; set; }
        public string Type { get; set; }
        public string Link { get; set; }
        public DateTime? Eventstartdate { get; set; }
        public DateTime? Eventenddate { get; set; }
        public string Imageid { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Status { get; set; }
    }
}
