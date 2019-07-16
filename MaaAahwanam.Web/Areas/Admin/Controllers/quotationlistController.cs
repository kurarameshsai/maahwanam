﻿using MaaAahwanam.Models;
using MaaAahwanam.Repository;
using MaaAahwanam.Service;
using MaaAahwanam.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MaaAahwanam.Web.Areas.Admin.Controllers
{

    public class quotationlistController : Controller
    {
        viewservicesservice viewservicesss = new viewservicesservice();
        newmanageuser newmanageuse = new newmanageuser();
        ProductInfoService productInfoService = new ProductInfoService();
        QuotationListsService quotationListsService = new QuotationListsService();
        VendorDatesService vendorDatesService = new VendorDatesService();
        OrderService orderService = new OrderService();
        VendorDashBoardService mnguserservice = new VendorDashBoardService();
        UserLoginDetailsService userLoginDetailsService = new UserLoginDetailsService();
        string name; string txtto;
        const string imagepath = @"/Quotefile/";
        string mail;
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        // GET: Admin/quotationlist
        public ActionResult Index()
        {
            var orderss = orderService.allorderslist1().Where(m => m.ordertype == "Quote").ToList();
            ViewBag.order = orderss.OrderByDescending(m => m.orderid);
            return View();
        }
        public ActionResult QuoteReply(string id)
        {
            var orderss = orderService.allorderslist1().Where(m => m.ordertype == "Quote").ToList();
            var orders = orderss.Where(m => m.orderid == long.Parse(id));
            ViewBag.orderdetails = orders;
            ViewBag.total = orders.FirstOrDefault();
            ViewBag.id = id;
            return View();
        }

        public ActionResult FilteredVendors(string type, string date, string id,string type1,string id1)
        {        



            if (type != null && date != null)
            { 
                ViewBag.id = id;
                string select = Convert.ToDateTime(date).ToString("dd-MM-yyyy");
                var data = vendorDatesService.GetVendorsByService().Where(m => m.ServiceType == type).ToList();
                ViewBag.display = "1";
                ViewBag.records = seperatedates(data, select, type);
            }
            else if (type == null && date == null)
            {
                ViewBag.id = id;
                ViewBag.novendor = "Select Service Type & Date To View Vendors";//"No Vendors Available On This Date";
            }
            return PartialView("FilteredVendors", "Quotations");
        }

        public string Capitalise(string str)
        {
            if (String.IsNullOrEmpty(str))
            return String.Empty;
            return Char.ToUpper(str[0]) + str.Substring(1).ToLower();
        }
        public ActionResult FilteredVendors1(string id, string type,string date)
        {
            var orderss = orderService.allorderslist1().Where(m => m.ordertype == "Quote").ToList();
            var orders = orderss.Where(m => m.orderid == long.Parse(id));
            ViewBag.orderdetails = orders;
            ViewBag.total = orders.FirstOrDefault();
            ViewBag.id = id;

            var sid = quotationListsService.GetAllQuotations().ToList(); //.Where(m => m.Status == "Active")
            ViewBag.quotations = sid.Where(m => m.OrderID == id);
            ViewBag.orderid = id;
            return View();
        }
        [HttpPost]
        public JsonResult UploadContractdoc(HttpPostedFileBase helpSectionImages, string command, string OrderID, string vid)
        {

            string fileName = string.Empty;
            string filename = string.Empty;
            //VendorImage vendorImage = new VendorImage();
            //Vendormaster vendorMaster = new Vendormaster();
            PartnerFile partnerFile = new PartnerFile();
            if (helpSectionImages != null)
            {
                string path = System.IO.Path.GetExtension(helpSectionImages.FileName);
                int imageno = 0;
                int imagecount = 2;
               // var list = partnerservice.GetFiles(vid, partid);
               // var resellers = partnerservice.GetPartners(vid);
               // var reellers1 = resellers.Where(m => m.PartnerID == long.Parse(partid)).FirstOrDefault();

              //  if (list.Count <= imagecount && Request.Files.Count <= imagecount - list.Count)
                //{
                    //getting max imageno
                  //  if (list.Count != 0)
                    //{
                      //  string lastimage = list.OrderByDescending(m => m.FileName).FirstOrDefault().FileName;
                       // var splitimage = lastimage.Split('_', '.');
                      //  imageno = int.Parse(splitimage[3]);
                   // }
                    //Uploading images in db & folder
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        int j = imageno + i + 1;
                        var file1 = Request.Files[i];
                        if (file1 != null && file1.ContentLength > 0)
                        {
                            filename = OrderID + "_" + vid + "_" + j + path;
                            fileName = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath(imagepath + filename));
                            file1.SaveAs(fileName);
                           // partnerFile.FileName = filename;
                            //vendorImage.ImageType = type;//"Slider";
                            //partnerFile.VendorID = vid;
                            //partnerFile.PartnerID = partid;
                            //partnerFile.UpdatedDate = DateTime.Now;
                            //partnerFile.RegisteredDate = DateTime.Now;

                            // vendorImage = vendorImageService.AddVendorImage(vendorImage, vendorMaster);
                       //     partnerFile = partnerservice.addPartnerfile(partnerFile);
                      //  }
                    }
                }
            }
            return Json(filename, JsonRequestBehavior.AllowGet);
        }

        public ActionResult replyquote1()
        {

            return PartialView("replyquote1", "quotationlist");
        }

        public ActionResult vendorreply(string orderid)
        {
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            var s1 = ViewBag.orderdetails = orderService.allorderslist1().ToList().Where(m => m.orderid == long.Parse(orderid)).FirstOrDefault();
            var userdata = userLoginDetailsService.GetUserId((int)s1.cutomerid);
            var userdetails = userLoginDetailsService.GetUser((int)s1.cutomerid);
            QuotationsList quotationsList = new QuotationsList();
            var s12 = ViewBag.orderdetails = orderService.allorderslist1().ToList().Where(m => m.orderid == long.Parse(orderid)).ToList();

            var vendordetails = newmanageuse.getvendor(Convert.ToInt32(s1.vid));
            List<SPGETNpkg_Result> package = new List<SPGETNpkg_Result>();
            List<SPGETNpkg_Result> package1 = new List<SPGETNpkg_Result>();
            List<SPGETNpkg_Result> package2 = new List<SPGETNpkg_Result>();
            package = viewservicesss.getvendorpkgs(Convert.ToString(s1.vid)); 

               

            txtto = vendordetails.EmailId;
            string vname = vendordetails.BusinessName;
         ViewBag.name= vendordetails.BusinessName;
            ViewBag.Email = vendordetails.EmailId;
            ViewBag.bookdate = s1.bookdate.ToString();
            ViewBag.orderid = orderid;
            return View();
        }

        public List<string[]> seperatedates(List<filtervendordates_Result> data, string date, string type)
        {
            List<string[]> betweendates = new List<string[]>();
            string dates = "";
            //var Gettotaldates = vendorDatesService.GetDates(long.Parse(id), long.Parse(vid));
            int recordcount = data.Count();
            foreach (var item in data)
            {
                var startdate = Convert.ToDateTime(item.StartDate);
                var enddate = Convert.ToDateTime(item.EndDate);
                if (startdate != enddate)
                {
                    string orderdates = productInfoService.disabledate(item.masterid, item.subid, type);
                    for (var dt = startdate; dt <= enddate; dt = dt.AddDays(1))
                    {
                        dates = (dates != "") ? dates + "," + dt.ToString("dd-MM-yyyy") : dt.ToString("dd-MM-yyyy");
                    }
                    if (dates.Split(',').Contains(date))
                    {
                        if (orderdates != "")
                            dates = String.Join(",", dates.Split(',').Where(i => !orderdates.Split(',').Any(e => i.Contains(e))));
                        string[] da = { item.masterid.ToString(), item.subid.ToString(), dates, item.BusinessName, item.ServiceType, item.VenueType, item.Type };
                        betweendates.Add(da);
                    }
                    dates = "";
                }
                else
                {
                    if (dates.Contains(date))
                    {
                        string[] da = { item.masterid.ToString(), item.subid.ToString(), startdate.ToString("dd-MM-yyyy"), item.BusinessName, item.ServiceType, item.VenueType, item.Type };
                        betweendates.Add(da);
                    }
                    dates = "";
                }
            }
            return betweendates;
        }

        public ActionResult email(string id,string type)
        {
           
            var orders = orderService.alluserOrderList(type).Where(m => m.ordertype == "Quote").ToList();
            var s12 = ViewBag.orderdetails = orders.Where(m => m.OrderId == long.Parse(id)).ToList();
            var s1 = ViewBag.orderdetails = orders.Where(m => m.OrderId == long.Parse(id)).FirstOrDefault();
            string txtto;
            if (type == "Vendor")
            {
                var userlogdetails = mnguserservice.getuserbyid(Convert.ToInt32(s1.userid));
                string txtt1 = userlogdetails.email;
                string name1 = userlogdetails.firstname;
                txtto = txtt1;
                name = name1;
                name = Capitalise(name);

            }
            else if (type == "User")
            {
                var userdata = userLoginDetailsService.GetUserId((int)s1.userid);
                var userdata1 = userLoginDetailsService.GetUser((int)s1.userid);
                string txtto2 = userdata.UserName;
                string name2 = userdata1.FirstName;
                txtto = txtto2;
                name = name2;
                name = Capitalise(name);

            }
           
            name = Capitalise(name);
            string OrderId = Convert.ToString(s1.OrderId);
            StringBuilder cds = new StringBuilder();
            cds.Append("<table style='width:70%;'><tbody>");
            cds.Append("<tr><td style = 'width:20%;'></td><td><strong> Details</strong> </td><td> <strong>Event Type</strong> </td><td><strong>Total Amount</strong></td></tr>");
            foreach (var item in s12)
            {
                string image;
                if (item.logo != null)
                {
                    image = "/vendorimages/" + item.logo.Trim(',') + "";
                }
                else { image = "/noimages.png"; }
                cds.Append("<tr><td style = 'width:20%;'>  <img src = " + image + " style='height: 182px;width: 132px;'/></td><td style = '' > <p>" + "<strong>Business Name: </strong>" + item.BusinessName + "</p><p>" + "<strong>Packgename: </strong>" + item.PackageName + "</p><p>" + "<strong>Price/person:</strong> " + '₹' + item.PerunitPrice.ToString().Replace(".00", "") + "</p><p>" + "<strong>No. of Guests:</strong> " + item.Quantity + "</p> </td><td > " + item.EventType + " </td><td style = ''><p>" + '₹' + item.TotalPrice.ToString().Replace(".00", "") + "</p> </td> </tr>");
            }
            cds.Append("</tbody></table>");
            string url = Request.Url.Scheme + "://" + Request.Url.Authority;
            //FileInfo File = new FileInfo(Server.MapPath("/mailtemplate/order.html"));
            FileInfo File = new FileInfo(Server.MapPath("/mailtemplate/kansiris.html"));
            string readFile = File.OpenText().ReadToEnd();
            readFile = readFile.Replace("[ActivationLink]", url);
            readFile = readFile.Replace("[name]", name);
            readFile = readFile.Replace("[orderid]", OrderId);
            readFile = readFile.Replace("[table]", cds.ToString());
            string txtmessage1 = readFile;
            string subj1 = "order has been placed";
                        string txtto1 = s1.username;
            EmailSendingUtility emailSendingUtility = new EmailSendingUtility();
            emailSendingUtility.Email_maaaahwanam(txtto1, txtmessage1, subj1, null);
            var msg = "email sent";
            return Json( msg,JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveInstallments(QuoteResponse quoteResponse)
        {
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            quoteResponse.UpdatedDate = indianTime;
            quoteResponse.Status = "Active";
            if (quoteResponse.SecondInstallment == ",")
            {
                quoteResponse.SecondInstallment = "0,0";
            }
            if (quoteResponse.ThirdInstallment == ",")
            {
                quoteResponse.ThirdInstallment = "0,0";
            }
            if (quoteResponse.FourthInstallment == ",")
            {
                quoteResponse.FourthInstallment = "0,0";
            }
            if (quoteResponse.FifthInstallment == ",")
            {
                quoteResponse.FifthInstallment = "0,0";
            }
            int count = quotationListsService.AddInstallments(quoteResponse);
            if (count > 0)
                return Json("Success");
            else
                return Json("Failed");
        }

        public JsonResult ParticularQuoteReply(QuotationsList quoteResponse, string id, HttpPostedFileBase data1,string message, string replyto,string messagetype,string ext)
        {
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            var s1 = orderService.allorderslist1().ToList().Where(m => m.orderid == long.Parse(quoteResponse.OrderID)).FirstOrDefault();
            var userdata = userLoginDetailsService.GetUserId((int)s1.cutomerid);
            var userdetails = userLoginDetailsService.GetUser((int)s1.cutomerid);
            QuotationsList quotationsList = new QuotationsList();
            var s12 = ViewBag.orderdetails = orderService.allorderslist1().ToList().Where(m => m.orderid == long.Parse(quoteResponse.OrderID)).ToList();
            HttpPostedFileBase image1;
            if (ext != null || ext != "" || ext != "undefined")
            {
                image1 = Request.Files["/Quotefile/" + ext + ""];
            }
            else { image1 = null; }
            if (replyto == "Vendor") {
                //  quotationsList.EmailId = userdata.UserName; quotationsList.Name = userdetails.FirstName;
                var vendordetails = newmanageuse.getvendor(Convert.ToInt32(s1.vid));

                string txtto1 = vendordetails.EmailId;
                string vname = vendordetails.BusinessName;
              //  vname = home.Capitalise(vname);
            }
            else if(replyto == "Customer") { quotationsList.EmailId = s1.username; quotationsList.Name = s1.fname; }
            quotationsList.ServiceType = s1.servicetype;
            quotationsList.PhoneNo = s1.customerphoneno;
            quotationsList.EventStartDate = Convert.ToDateTime(s1.bookdate);
            quotationsList.EventStartTime = DateTime.UtcNow;
            quotationsList.EventEnddate = DateTime.UtcNow;
            quotationsList.EventEndtime = DateTime.UtcNow;
            quotationsList.VendorId = ""; quotationsList.OrderID = quoteResponse.OrderID;

            quotationsList.VendorMasterId = s1.vid.ToString();
            quotationsList.Persons = s1.guestno.ToString();
            string ip = HttpContext.Request.UserHostAddress;
            quotationsList.IPaddress = ip;
            quotationsList.UpdatedTime = DateTime.UtcNow;
            quotationsList.Status = "Active";
            quotationsList.Reply_Type = messagetype;
            quotationsList.ReplyToType = replyto;
            quotationsList.Description = message;

            quotationListsService.AddQuotationList(quotationsList);
           
            if (replyto == "Vendor")
            {
                mail = "/mailtemplate/kansiris1.html";
            }
            else if (replyto == "Customer") { mail = "/mailtemplate/kansiris.html"; }
            FileInfo File = new FileInfo(Server.MapPath(mail));

            //  FileInfo File = new FileInfo(Server.MapPath("/mailtemplate/QuoteReply.html"));
            string readFile = File.OpenText().ReadToEnd();
            StringBuilder cds = new StringBuilder();
            StringBuilder cds1 = new StringBuilder();
            if (replyto == "Vendor")
            {
                var vendordetails = newmanageuse.getvendor(Convert.ToInt32(s1.vid));
                txtto = vendordetails.EmailId;
                string vname = vendordetails.BusinessName;
                readFile = readFile.Replace("[name]", vendordetails.BusinessName);
                readFile = readFile.Replace("[Email]", vendordetails.EmailId);
               // readFile = readFile.Replace("[bookdate]", s1.bookdate.ToString());
                cds1.Append("<table style='width: 70 %;border: 1px solid #fffff;'><tbody style='width: 70 %;border: 1px solid #fffff;'>");
               // cds1.Append("<tr><td style='width: 70 %;border: 1px solid #fffff;'></td><td style='width: 70 %;border: 1px solid #fffff;'><strong> Event Type</strong> </td></tr>");
                cds1.Append("<tr><td style='width: 70 %;border: 1px solid #fffff;'>  <p>" + "<strong>No. of Guests:</strong> " + s1.guestno + "</p> <p><strong>Event Type:</strong> " +  s1.eventtype + "</p> <p><strong>Event Date:</strong> " + s1.bookdate + " </td> </tr>");            
                cds1.Append("</tbody></table>");
                cds.Append("<table class='example - table'><tbody>");
               // cds.Append("<tr><td ><strong>Packgename</strong></td><td ><strong> Ahwanam Price</strong> </td><td ><strong> Market Price </strong> </td><td><strong>Availablity(Yes/No)</strong> </td></tr>");
                foreach (var item in s12)
                {
                    string image;
                    if (item.logo != null)
                    {
                        image = "/vendorimages/" + item.logo.Trim(',') + "";
                    }
                    else { image = "/noimages.png"; }
                    cds.Append("<tr><td style='width: 70 %;border: 1px solid #000;'>  <p>" + "<strong>Packgename:</strong>" + item.packagename + "</p><strong><p>Please provide the information for booking :</p><p>Date Available: Y / N(if no pls give alternate available date)</p><p>Package Price for Customer incl GST:</p><p>Package Price for Ahwanam incl GST:</p> </strong> </td></tr></tr>");
                }
                cds.Append("</tbody></table>");
            }
            else if (replyto == "Customer")
            {
                readFile = readFile.Replace("[name]", s1.fname);
                readFile = readFile.Replace("[Email]", s1.username);
                txtto = s1.username;
                cds.Append("<table style='width:70%;'><tbody>");
                cds.Append("<tr><td style = 'width:20%;'></td><td><strong> Details</strong> </td><td> <strong>Event Type</strong> </td><td><strong>Total Amount</strong></td></tr>");
                foreach (var item in s12)
                {
                    string image;
                    if (item.logo != null)
                    {
                        image = "/vendorimages/" + item.logo.Trim(',') + "";
                    }
                    else { image = "/noimages.png"; }
                    cds.Append("<tr><td style = 'width:20%;'>  <img src = " + image + " style='height: 182px;width: 132px;'/></td><td style = '' > <p>" + "<strong>Business Name: </strong>" + item.BusinessName + "</p><p>" + "<strong>Packgename: </strong>" + item.packagename + "</p><p>" + "<strong>Price/person:</strong> " + '₹' + item.perunitprice.ToString().Replace(".00", "") + "</p><p>" + "<strong>No. of Guests:</strong> " + item.guestno + "</p> </td><td > " + item.eventtype + " </td><td style = ''><p>" + '₹' + item.totalprice.ToString().Replace(".00", "") + "</p> </td> </tr>");
                }
                cds.Append("</tbody></table>");
            }
            if (message == "")
            { message = null; }
            readFile = readFile.Replace("[message]", message);
            string url = Request.Url.Scheme + "://" + Request.Url.Authority;
            readFile = readFile.Replace("[ActivationLink]", url);
            readFile = readFile.Replace("[name]", name);
            readFile = readFile.Replace("[orderid]", quoteResponse.OrderID);
            readFile = readFile.Replace("[table]", cds.ToString());
            readFile = readFile.Replace("[table1]", cds1.ToString());
            string txtmessage = readFile;
            string subj = "Response to your Quote #" + s1.orderid + "";
            EmailSendingUtility emailSendingUtility = new EmailSendingUtility();
            emailSendingUtility.Email_maaaahwanam(txtto, txtmessage, subj, image1);
            return Json("Success");
        }



        //public JsonResult ParticularQuoteReply(string QuoteID, string id)
        //{
        //    DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        //    var s1 = orderService.allorderslist1().ToList().Where(m => m.orderid == long.Parse(QuoteID)).FirstOrDefault(); 
        //    QuotationsList quotationsList = new QuotationsList();
        //    quotationsList.Name = s1.fname;
        //    quotationsList.EmailId = s1.username;
        //    quotationsList.ServiceType = s1.servicetype;
        //    quotationsList.PhoneNo = s1.customerphoneno;
        //    quotationsList.EventStartDate = Convert.ToDateTime(s1.bookdate);
        //    quotationsList.EventStartTime = DateTime.UtcNow;
        //    quotationsList.EventEnddate = DateTime.UtcNow;
        //    quotationsList.EventEndtime = DateTime.UtcNow;
        //    quotationsList.VendorId = "";
        //    quotationsList.VendorMasterId = s1.vid.ToString();
        //    quotationsList.Persons = s1.guestno.ToString();
        //    string ip = HttpContext.Request.UserHostAddress;
        //    quotationsList.IPaddress = ip;
        //    quotationsList.UpdatedTime = DateTime.UtcNow;
        //    quotationsList.Status = "Active";
        //    quotationsList.OrderID = s1.orderid.ToString();
        //    quotationListsService.AddQuotationList(quotationsList);
        //    FileInfo File = new FileInfo(Server.MapPath("/mailtemplate/QuoteReply.html"));
        //    string readFile = File.OpenText().ReadToEnd();
        //    readFile = readFile.Replace("[name]", s1.fname);
        //    readFile = readFile.Replace("[Email]", s1.username);
        //    string txtto = s1.username;
        //    string txtmessage = readFile;
        //    string subj = "Response to your Quote #" + s1.orderid + "";
        //    EmailSendingUtility emailSendingUtility = new EmailSendingUtility();
        //    emailSendingUtility.Email_maaaahwanam(txtto, txtmessage, subj);
        //    return Json("sucess");
        //}
    }
}
