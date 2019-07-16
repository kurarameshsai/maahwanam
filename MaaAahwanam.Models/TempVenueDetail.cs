using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MaaAahwanam.Models
{
    public class TempVenueDetail
    {
        [Key]
        public int ID { get; set; }
        public string VenueType { get; set; }
        public string BestSuited { get; set; }
        public string VenueConfig { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public string Localities { get; set; }
        public string VenueArea { get; set; }
        public string DiningArea { get; set; }
        public string IndoorSpace { get; set; }
        public string OutdoorSpace { get; set; }
        public string RoomsAvailable { get; set; }
        public string Numofrooms { get; set; }
        public string EstablishedDate { get; set; }
        public string EventDescription { get; set; }
        public string EmailID { get; set; }
        public Nullable<decimal> BookingAdvanceAmount { get; set; }
        public Nullable<decimal> BookingTotalAmount { get; set; }
        public string PaymentMode { get; set; }
        public string ManagerAvl { get; set; }
        public string ManagerContactInfo { get; set; }
        public string Images { get; set; }
        public string TwoWheelerPlace { get; set; }
        public string FourWheelerPlace { get; set; }
        public string CateringContactInfo { get; set; }
        public string FoodType { get; set; }
        public string CateringIndoorCap { get; set; }
        public string CateringOutdoorCap { get; set; }
        public string Alchol { get; set; }
        public string Wifi { get; set; }
        public string Tables { get; set; }
        public string Stage { get; set; }
        public string Washrooms { get; set; }
        public string Openairspace { get; set; }
        public string Dj { get; set; }
        public string Elevator { get; set; }
        public string DecoratorContactInfo { get; set; }
        public string DecorationType { get; set; }
        public string PhotographerContactInfo { get; set; }
        public string Videographercontactinfo { get; set; }
        public string LCDProjector { get; set; }
        public string Overheadprojector { get; set; }
        public string FullAudioandVideo { get; set; }
        public string Builtinscreens { get; set; }
        public string MenBoutiqueContactInfo { get; set; }
        public string WomenBoutiqueContactInfo { get; set; }
        public string Halltype { get; set; }
        public string Venuename { get; set; }
        public string CancelPolicy { get; set; }
        public string AvgEvents { get; set; }
    }
}
