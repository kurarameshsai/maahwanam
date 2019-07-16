using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace MaaAahwanam.Utility
{
    public class UploadFile
    {
        public string Uploadfiles(HttpPostedFileBase file, string actionname)
        {
            string str = string.Empty;
            string fileName = string.Empty;
            if (file != null && file.ContentLength > 0)
                try
                {
                    string strExtensionName = System.IO.Path.GetExtension(file.FileName).ToString().ToLower();
                    if ((strExtensionName == ".png") || (strExtensionName == ".jpg") || (strExtensionName == ".bmp") || (strExtensionName == ".gif") || (strExtensionName == ".jpeg"))
                    {
                        string g = Guid.NewGuid().ToString();
                        if (actionname == "UserProfile")
                        {
                            fileName = System.IO.Path.Combine(HttpContext.Current.Server.MapPath(@"~/Content/AdminUserfiles/" + g + strExtensionName));
                        }
                        else if (actionname == "VendorPhotoDet")
                        {                            
                            fileName = System.IO.Path.Combine(HttpContext.Current.Server.MapPath(@"~/Content/VendorImages/" + g + strExtensionName));
                        }
                        else if (actionname == "InviDesigns")
                        {
                            fileName = System.IO.Path.Combine(HttpContext.Current.Server.MapPath(@"~/Content/InvitationDesignfiles/" + file.FileName));
                        }
                        else if (actionname == "Giftdesigns")
                        {
                            fileName = System.IO.Path.Combine(HttpContext.Current.Server.MapPath(@"~/Content/Giftdesignfiles/" + file.FileName));
                        }
                        else if (actionname == "Eventsandtips")
                        {
                            fileName = System.IO.Path.Combine(HttpContext.Current.Server.MapPath(@"~/Content/EventsandtipsFiles/" + file.FileName));
                        }
                        file.SaveAs(fileName);
                        str = g + strExtensionName + "|" + file.FileName + "|" + strExtensionName;
                        return (str);
                    }
                }
                catch (Exception ex)
                {
                }
            return (str);
        }

        public string Uploadfiles1(HttpPostedFileBase file1, string actionname)
        {
            string str = string.Empty;
            string fileName = string.Empty;
            if (file1 != null && file1.ContentLength > 0)
                try
                {
                    string strExtensionName = System.IO.Path.GetExtension(file1.FileName).ToString().ToLower();
                    if ((strExtensionName == ".png") || (strExtensionName == ".jpg") || (strExtensionName == ".bmp") || (strExtensionName == ".gif") || (strExtensionName == ".jpeg"))
                    {
                        string g = Guid.NewGuid().ToString();
                        if (actionname == "UserProfile")
                        {
                            fileName = System.IO.Path.Combine(HttpContext.Current.Server.MapPath(@"~/Content/AdminUserfiles/" + g + strExtensionName));
                        }
                        else if (actionname == "VendorPhotoDet")
                        {
                            fileName = System.IO.Path.Combine(HttpContext.Current.Server.MapPath(@"~/Content/VendorImages/" + g + strExtensionName));
                        }
                        else if (actionname == "InviDesigns")
                        {
                            fileName = System.IO.Path.Combine(HttpContext.Current.Server.MapPath(@"~/Content/InvitationDesignfiles/" + file1.FileName));
                        }
                        else if (actionname == "Giftdesigns")
                        {
                            fileName = System.IO.Path.Combine(HttpContext.Current.Server.MapPath(@"~/Content/Giftdesignfiles/" + file1.FileName));
                        }
                        else if (actionname == "Eventsandtips")
                        {
                            fileName = System.IO.Path.Combine(HttpContext.Current.Server.MapPath(@"~/Content/EventsandtipsFiles/" + file1.FileName));
                        }
                        file1.SaveAs(fileName);
                        str = g + strExtensionName + "|" + file1.FileName + "|" + strExtensionName;
                        return (str);
                    }
                }
                catch (Exception ex)
                {
                }
            return (str);
        }
   
    }
}
