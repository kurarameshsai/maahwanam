using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaaAahwanam.Models
{
    
    public class CommentDetail
    {
        [Key]
        public long CommentDetId { get; set; }
        public long CommentId { get; set; }
        public int UserLoginId { get; set; }
        public string CommentDetails { get; set; }
        public DateTime? CommentDate { get; set; }
        public string AttachFileName { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Status { get; set; }
    }
}
