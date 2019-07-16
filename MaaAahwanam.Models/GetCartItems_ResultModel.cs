using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaaAahwanam.Models
{
    public class GetCartItems_ResultModel
    {
        public long Id { get; set; }
        public string BusinessName { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Landmark { get; set; }
        public string ContactNumber { get; set; }
        public string ServicType { get; set; }
        public string image { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
