using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaaAahwanam.Models
{
    
    public class AdminTestimonialPath
    {
        [Key]
        public long PathId { get; set; }
        public long Id { get; set; }
        public string ImagePath { get; set; }
        public string VideoPath { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Status { get; set; }
    }
}
