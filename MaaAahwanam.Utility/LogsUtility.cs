using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaaAahwanam.Dal;
using MaaAahwanam.Models;
using System.Net;
using System.Web;

namespace MaaAahwanam.Utility
{
    public class LogsUtility
    {
        Maa_AhwaanamBase _Repositories = new Maa_AhwaanamBase();
        public void LogTheExceptions(Exception exception, string URL)
        {
            MaaAahwanam.Dal.MA_ExceptionLogs ExceptionLogs = new MA_ExceptionLogs();
            ExceptionLogs.ExceptionType = exception.GetType().FullName;
            ExceptionLogs.ErrorMessage = exception.Message;
            ExceptionLogs.UrlPattern = URL;
            ExceptionLogs.Date = DateTime.Now;
            ExceptionLogs.LogType = "ExceptionLog";
            ExceptionLogs.UserID = ValidUserUtility.ValidUser();
            _Repositories.ExceptionLogs.Add(ExceptionLogs);
            Repositories.SaveChanges();
        }
        public void LogTimings(string Type)
        {
            MaaAahwanam.Dal.MA_UserLogInTimings UserLogInTimings = new MA_UserLogInTimings();
            UserLogInTimings.UserID = ValidUserUtility.ValidUser();
            UserLogInTimings.Type = Type;
            UserLogInTimings.Date = DateTime.Now;
            _Repositories.UserLogInTimings.Add(UserLogInTimings);
            _Repositories.SaveChanges();
        }
        private string GetComputer_LanIP()
        {
            string strHostName = System.Net.Dns.GetHostName();

            IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);

            foreach (IPAddress ipAddress in ipEntry.AddressList)
            {
                if (ipAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    return ipAddress.ToString();
                }
            }

            return "";
        }

        public void LogEvents(string Description, string URL)
        {
            MaaAahwanam.Dal.MA_ExceptionLogs ExceptionLogs = new MA_ExceptionLogs();
            ExceptionLogs.ErrorMessage = Description;
            ExceptionLogs.UrlPattern = URL;
            ExceptionLogs.Date = DateTime.Now;
            ExceptionLogs.LogType = "EventLog";
            ExceptionLogs.UserID = ValidUserUtility.ValidUser();
            ExceptionLogs.IPAddress = GetComputer_LanIP();
            ExceptionLogs.Browsertype = HttpContext.Current.Request.Browser.Type;
            ExceptionLogs.URL = HttpContext.Current.Request.Url.OriginalString;
            ExceptionLogs.usertype = ValidUserUtility.UserType();
            _Repositories.ExceptionLogs.Add(ExceptionLogs);
            _Repositories.SaveChanges();
        }
        public void LogEvents(string Description, string URL, int UserID)
        {
            MaaAahwanam.Dal.MA_ExceptionLogs ExceptionLogs = new MA_ExceptionLogs();
            ExceptionLogs.ErrorMessage = Description;
            ExceptionLogs.UrlPattern = URL;
            ExceptionLogs.Date = DateTime.Now;
            ExceptionLogs.IPAddress = GetComputer_LanIP();
            ExceptionLogs.Browsertype = HttpContext.Current.Request.Browser.Type;
            ExceptionLogs.URL = HttpContext.Current.Request.Url.OriginalString;
            ExceptionLogs.usertype = "";
            ExceptionLogs.LogType = "EventLog";
            ExceptionLogs.UserID = UserID;
            _Repositories.ExceptionLogs.Add(ExceptionLogs);
            _Repositories.SaveChanges();
        }
    }
}
