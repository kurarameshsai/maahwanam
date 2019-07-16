using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaaAahwanam.Models
{
    public class UserAddBook
    {
        [Key]
        public long AddBookId { get; set; }
        public long UserLoginId { get; set; }
        public string PersonName { get; set; }
        public string PersonAddress { get; set; }
        public string PersonLocation { get; set; }
        public string PersonCity { get; set; }
        public string PersonPhone { get; set; }
        public string PersonEmail { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Status { get; set; }
    }
}
