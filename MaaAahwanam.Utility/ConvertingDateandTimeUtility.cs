using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace MaaAahwanam.Utility
{
    public class ConvertingDateandTimeUtility
    {
        public static string DateFormat_asper_system()
        {
            string DateFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
            return DateFormat;
        }
        public string DateConvertion_to_String(string datestring)
        {
            IFormatProvider culture = new CultureInfo("en-Us", true);
            DateTime MyDateTime = new DateTime();
            MyDateTime = DateTime.ParseExact(datestring, "dd/MM/yyyy", culture);
            string MyString_new = MyDateTime.ToString("yyyy-MM-dd");
            return MyString_new;
        }
        
        public DateTime  DateConvertion(string datestring)
        {
            return DateTime.Parse(datestring);
        }
        public TimeSpan DateConvertion_to_timeformate(string timestring)
        {
            TimeSpan MyTime = TimeSpan.Parse(timestring);
            return MyTime;
        }

    }
}
