using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaaAahwanam.Models
{
    
    public class ServiceResponse
    {
        [Key]
        public long ResponseId { get; set; }
        public long RequestId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public long ResponseBy { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Status { get; set; }
        public string VendorType { get; set; }
    }
}
