using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaaAahwanam.Models
{
    public class GetProducts_Results
    {
        public long Id { get; set; }
        public string BusinessName { get; set; }
        public string Description { get; set; }
        public string ServicType { get; set; }
        public decimal Cost { get; set; }
        public string image { get; set; }
    }
}
