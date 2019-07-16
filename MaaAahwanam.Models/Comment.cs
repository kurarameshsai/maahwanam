using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaaAahwanam.Models
{
    public class Comment
    {
        [Key]
        public long CommentId { get; set; }
        public string ServiceId { get; set; }
        public string ServiceType { get; set; }
        public string Status { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
