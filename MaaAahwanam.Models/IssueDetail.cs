using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaaAahwanam.Models
{
    
    public class IssueDetail
    {
        [Key]
        public long TicketCommuId { get; set; }
        public long TicketId { get; set; }
        public string Msg { get; set; }
        public long RepliedBy { get; set; }
        public DateTime? ReplayedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Status { get; set; }
    }
}
