using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaaAahwanam.Models
{
    
    public class IssueTicket
    {
        [Key]
        public long TicketId { get; set; }
        public long UserLoginId { get; set; }
        public long OrderId { get; set; }
        public string IssueType { get; set; }
        public string Subject { get; set; }
        public string Severity { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public DateTime? ClosedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
