﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MaaAahwanam.Models
{
    public class PartnerPackage
    {
        [Key]
        public long ID { get; set; }
        public long PartnerID { get; set; }
        public long VendorId { get; set; }
        public long VendorSubId { get; set; }
        public string VendorType { get; set; }
        public string VendorSubType { get; set; }
        public string packageid { get; set; }
        public string packagename { get; set; }
        // Sale Price to Partner & Sale price Type
        public string normaldayssaletype { get; set; }
        public string salenormaldays { get; set; }
        public string peakdaysaletype { get; set; }
        public string salepeakdays { get; set; }
        public string holidaysssaletype { get; set; }
        public string saleholidays { get; set; }
        public string choicedayssaletype { get; set; }
        public string salechoicedays { get; set; }
        // Current Selling Price
        public string spchoicedays { get; set; }
        public string spnormaldays { get; set; }
        public string spholidays { get; set; }
        public string sppeakdays { get; set; }
        // Margin
        public string Mchoicedays { get; set; }
        public string Mnormaldays { get; set; }
        public string Mholidays { get; set; }
        public string Mpeakdays { get; set; }
        //Final Sale Price
        public string fschoicedays { get; set; }
        public string fsnormaldays { get; set; }
        public string fsholidays { get; set; }
        public string fspeakdays { get; set; }
        //Ahwanam Prices
        public string Achoicedays { get; set; }
        public string Anormaldays { get; set; }
        public string Aholidays { get; set; }
        public string Apeakdays { get; set; }

        public string expiry { get; set; }
        public string Status { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? RegisteredDate { get; set; }
    }
}
