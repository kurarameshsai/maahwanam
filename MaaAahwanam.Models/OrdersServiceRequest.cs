using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaaAahwanam.Models
{
    
    public class OrdersServiceRequest
    {
        [Key]
        public long OrderId { get; set; }
        public long ResponseId { get; set; }
        public decimal TotalPrice { get; set; }
        public long PaymentId { get; set; }
        public string ServiceType { get; set; }
        public long OrderNumber { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Status { get; set; }
    }
}
