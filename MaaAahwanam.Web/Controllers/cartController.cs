﻿using MaaAahwanam.Models;
using MaaAahwanam.Repository;
using MaaAahwanam.Service;
using MaaAahwanam.Utility;
using MaaAahwanam.Web.Custom;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace MaaAahwanam.Web.Controllers
{
    public class cartController : Controller
    {
        cartservices cartserve = new cartservices();
        UserLoginDetailsService userLoginDetailsService = new UserLoginDetailsService();
        decimal totalp, discount, servcharge, gst, nettotal, totalp2;
        CartService cartService = new CartService();
        QuotationListsService quotationListsService = new QuotationListsService();

        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");


        // GET: cart
        public ActionResult Index()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrincipal)System.Web.HttpContext.Current.User;
                if (user.UserType == "User")
                {
                    var userdata = userLoginDetailsService.GetUser((int)user.UserId);
                    if (user.UserType == "Admin")
                    {
                        ViewBag.cartCount = cartserve.CartItemsCount(0);
                        return PartialView("ItemsCartdetails");
                    }
                    ViewBag.cartCount = cartserve.CartItemsCount((int)user.UserId);
                    List<GetCartItems_Result> cartlist = cartserve.CartItemsList(int.Parse(user.UserId.ToString()));
                    decimal total = cartlist.Where(m => m.Status == "Active").Sum(s => s.TotalPrice);
                    ViewBag.cartitems = cartlist.OrderByDescending(m => m.UpdatedDate).Where(m => m.Status == "Active");
                    ViewBag.Total = total;
                }
            }
            else
            {
                ViewBag.cartCount = cartserve.CartItemsCount(0);
            }
            return View();
        }

        public JsonResult DeletecartItem(long cartId)
        {
            var message = cartserve.Deletecartitem(cartId);
            return Json(message);
        }

        public ActionResult billing(string cartid)
        {
            //if (cartid == null)
            //{
            //    ViewBag.tamount = "000";
            //    ViewBag.discount = "0";
            //    ViewBag.service = "0";
            //    ViewBag.Gst = "0";
            //    ViewBag.netamount = "0";
            //}
            //else {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrincipal)System.Web.HttpContext.Current.User;
                if (user.UserType == "User")
                {
                    List<GetCartItems_Result> cartlist = cartserve.CartItemsList(int.Parse(user.UserId.ToString())).ToList();

                    if (cartlist.Count == 0)
                    {
                        ViewBag.tamount = "000";
                        ViewBag.discount = "0";
                        ViewBag.service = "0";
                        ViewBag.Gst = "0";
                        ViewBag.netamount = "0";
                        return PartialView("billing");
                    }
                    if (cartid != null)
                        cartid = cartid.Trim(',');
                    else
                        cartid = string.Join(",", cartlist.Select(m => m.CartId));
                    var cartid1 = cartid.Split(',');
                    for (int i = 0; i < cartid1.Count(); i++)
                    {
                        if (cartid1[i] == "" || cartid1[i] == null)
                        {
                            totalp = 0;
                        }
                        else
                        {
                            var cartdetails = cartlist.Where(m => m.CartId == Convert.ToInt64(cartid1[i])).FirstOrDefault();
                            totalp = cartdetails.TotalPrice;
                        }
                        totalp2 = totalp2 + totalp;
                        discount = 0;
                        servcharge = 0;
                        gst = 0;
                        nettotal = nettotal + totalp - Convert.ToDecimal(discount) + Convert.ToDecimal(servcharge) + Convert.ToDecimal(gst);
                    }
                    var totalp1 = totalp2;
                    var discount1 = "0";
                    var servcharge1 = servcharge;
                    var gst1 = gst;
                    var nettotal1 = Convert.ToString(nettotal);
                    ViewBag.tamount = totalp1;
                    ViewBag.discount = discount1;
                    ViewBag.service = servcharge1;
                    ViewBag.Gst = gst1;
                    ViewBag.netamount = nettotal1;
                }
            }
            //}
            return PartialView("billing");
        }

        public JsonResult email(string selcartid, string searchedcontent)
        {
            selcartid = selcartid.Trim(',');
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrincipal)System.Web.HttpContext.Current.User;
                if (user.UserType == "User")
                {
                    var userlogin = userLoginDetailsService.GetUserId(int.Parse(user.UserId.ToString()));
                    string Email = userlogin.UserName;
                    string txtto = "sireesh.k@xsilica.com,rameshsai@xsilica.com,seema@xsilica.com,amit.saxena@ahwanam.com,jm@dsc-usa.com";
                    int id = Convert.ToInt32(user.UserId);
                    var userdetails = userLoginDetailsService.GetUser(id);
                    string ipaddress = HttpContext.Request.UserHostAddress;
                    string username = userdetails.FirstName;
                    string phoneno = userdetails.UserPhone;
                    HomeController home = new HomeController();
                    username = home.Capitalise(username) + " " + home.Capitalise(userdetails.LastName);
                    List<GetCartItems_Result> cartlist = cartserve.CartItemsList(int.Parse(user.UserId.ToString()));
                    var cartid1 = selcartid.Split(',');
                    QuotationsList quotationsList = new QuotationsList();




                    //for (int i = 0; i < cartid1.Count(); i++)
                    //{
                    //    var cartdetails = cartlist.Where(m => m.CartId == Convert.ToInt64(cartid1[i])).FirstOrDefault();
                    //    var cartname1 = cartdetails.BusinessName;
                    //    cartname = cartname1 + ',' + cartname1;
                    //}
                    List<GetCartItems_Result> cartdetails = new List<GetCartItems_Result>();
                    for (int i = 0; i < cartid1.Count(); i++)
                    {
                        var qcart = cartlist.Where(m => m.CartId == Convert.ToInt64(cartid1[i])).FirstOrDefault();
                        cartdetails.AddRange(cartlist.Where(m => m.CartId == Convert.ToInt64(cartid1[i])).ToList());
                        quotationsList.Name = userdetails.FirstName;
                        quotationsList.EmailId = userlogin.UserName;
                        quotationsList.ServiceType = qcart.ServiceType;
                        quotationsList.PhoneNo = userdetails.UserPhone;
                        quotationsList.EventStartDate = DateTime.UtcNow;
                        quotationsList.EventStartTime = DateTime.UtcNow;
                        quotationsList.EventEnddate = DateTime.UtcNow;
                        quotationsList.EventEndtime = DateTime.UtcNow;
                        quotationsList.VendorId = qcart.subid.ToString();
                        quotationsList.VendorMasterId = qcart.Id.ToString();
                        quotationsList.Persons = qcart.Quantity.ToString();
                        string ip = HttpContext.Request.UserHostAddress;
                        //string hostName = Dns.GetHostName();
                        quotationsList.IPaddress = ip;//Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();
                        quotationsList.UpdatedTime = DateTime.UtcNow;
                        quotationsList.Status = "Active";
                        quotationListsService.AddQuotationList(quotationsList);
                    }
                    ViewBag.cartdetails = cartdetails;
                    StringBuilder cds = new StringBuilder();
                    cds.Append("<table style='border:1px black solid;'><tbody>");
                    cds.Append("<tr><td> Selected Business </td><td> Guests Count </td><td> Amount </td><td> Selected Event </td></tr>");
                    foreach (var item in ViewBag.cartdetails)
                    {
                        cds.Append("<tr><td style = 'width: 75px;border: 1px black solid;'> " + item.BusinessName + "</td><td style = 'width: 75px;border: 1px black solid;' > " + item.Quantity + " </td><td style = 'width: 75px;border: 1px black solid;'> " + item.TotalPrice + " </td><td style = 'width: 50px;border: 1px black solid;'> " + item.EventType + " </td></tr>");  //<td style = 'width: 50px;border: 2px black solid;'> " + item.eventstartdate + " </td><td> date </td>
                    }
                    cds.Append("</tbody></table>");
                    FileInfo File = new FileInfo(Server.MapPath("/mailtemplate/getassistance.html"));
                    string readFile = File.OpenText().ReadToEnd();
                    readFile = readFile.Replace("[carttable]", cds.ToString());
                    readFile = readFile.Replace("[name]", username);
                    readFile = readFile.Replace("[Ipaddress]", ipaddress);
                    readFile = readFile.Replace("[email]", Email);
                    readFile = readFile.Replace("[phoneno]", phoneno);
                    readFile = readFile.Replace("[usersearch]", searchedcontent.Replace(",", "<br/>"));
                    string txtmessage = readFile;//readFile + body;
                    string subj = "Get Assistance/Quote From Cart Page";
                    EmailSendingUtility emailSendingUtility = new EmailSendingUtility();
                    emailSendingUtility.Email_maaaahwanam(txtto, txtmessage, subj, null);
                }
            }
            var message = "success";
            return Json(message);
        }




        class payees
        {
            public string id { get; set; }
            public string entity { get; set; }
            public string amount { get; set; }
            public string currency { get; set; }
            public string status { get; set; }
            public string order_id { get; set; }
            public string invoice_id { get; set; }
            public string international { get; set; }
            public string method { get; set; }
            public string amount_refunded { get; set; }
            public string refund_status { get; set; }
            public string captured { get; set; }
            public string description { get; set; }
            public string card_id { get; set; }
            public string bank { get; set; }
            public string wallet { get; set; }
            public string vpa { get; set; }
            public string email { get; set; }
            public List<ResponseData> notes { get; set; }

            public string contact { get; set; }
            public string fee { get; set; }
            public string tax { get; set; }
            public string error_code { get; set; }
            public string error_description { get; set; }
            public string created_at { get; set; }

        }
        public class ResponseData
        {
            public string address { get; set; }
            public string order_id { get; set; }
        }
        //public ActionResult booknow(string cartnos, string paymentid,string amountpaid)
        //{
        //    if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
        //    {
        //        var user = (CustomPrincipal)System.Web.HttpContext.Current.User;
        //        if (user.UserType == "User")
        //        {
        //            var userdata = userLoginDetailsService.GetUser((int)user.UserId);
        //            ViewBag.cartCount = cartService.CartItemsCount((int)user.UserId);
        //            var cartlist = cartService.CartItemsList(int.Parse(user.UserId.ToString()));
        //            var cartnos1 = cartnos.Split(',');

        //             //Payment Section
        //                RazorpayClient client = new RazorpayClient("rzp_test_3OHEkrM9aPMz5u", "WUA3WciyAExRDwRwxMIqU5Yb");
        //                Payment payme1 = client.Payment.Fetch(paymentid);


        //                Dictionary<string, object> options = new Dictionary<string, object>();
        //                options.Add("amount", Convert.ToInt32(amountpaid));

        //                Payment paymentCaptured = payme1.Capture(options);
        //                Payment payme2 = client.Payment.Fetch(paymentid);
        //                var ks = JsonConvert.SerializeObject(payme2.Attributes);

        //                payees paymentArray = JsonConvert.DeserializeObject<payees>(ks);
        //            for (int i = 0; i < cartnos1.Count(); i++)
        //            {
        //                string cartno2 = cartnos1[i];
        //                string totalprice = "";
        //                var cartdetails = cartlist.Where(m => m.CartId == Convert.ToInt64(cartno2)).FirstOrDefault();
        //                string type = cartdetails.ServicType;
        //                string guest = Convert.ToString(cartdetails.Quantity);
        //                string price = Convert.ToString(cartdetails.Perunitprice);
        //                string id = Convert.ToString(cartdetails.Id);
        //                string did = Convert.ToString(cartdetails.DealId);
        //                string timeslot = cartdetails.attribute;
        //                string etype1 = cartdetails.EventType;
        //                string vid = Convert.ToString(cartdetails.subid);
        //                DateTime date = Convert.ToDateTime(cartdetails.eventstartdate);
        //                if (type == "Photography" || type == "Decorator" || type == "Other")
        //                {
        //                    totalprice = price;
        //                    guest = "0";
        //                }
        //                else
        //                {
        //                    totalprice = Convert.ToString(cartdetails.TotalPrice);
        //                }
        //                DateTime updateddate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        //                int userid = Convert.ToInt32(user.UserId);
        //                //Saving Record in order Table
        //                OrderService orderService = new OrderService();
        //                MaaAahwanam.Models.Order order = new MaaAahwanam.Models.Order();
        //                order.TotalPrice = Convert.ToDecimal(totalprice);
        //                order.OrderDate = Convert.ToDateTime(updateddate); //Convert.ToDateTime(bookeddate);
        //                order.UpdatedBy = (Int64)user.UserId;
        //                order.OrderedBy = (Int64)user.UserId;
        //                order.UpdatedDate = Convert.ToDateTime(updateddate);
        //                order.Status = "Pending";
        //                order = orderService.SaveOrder(order);


        //                //Payment Section


        //                Payment_orderServices payment_orderServices = new Payment_orderServices();
        //                Payment_Orders payment_Orders = new Payment_Orders();

        //                payment_Orders.cardnumber = paymentArray.card_id;
        //                payment_Orders.CVV = "razorpay";
        //                payment_Orders.paidamount = Convert.ToDecimal(Convert.ToDouble(paymentArray.amount)* 0.01);
        //                payment_Orders.Gateway_ID = paymentArray.id;
        //                payment_Orders.Paiddate = Convert.ToDateTime(updateddate);
        //                payment_Orders.OrderID = order.OrderId;
        //                payment_Orders.Amount = totalprice;
        //                payment_Orders.Bank = paymentArray.bank;
        //                payment_Orders.Card_ID = paymentArray.card_id;
        //                payment_Orders.Currency = paymentArray.currency;
        //                payment_Orders.Customer_Email = paymentArray.email;
        //                payment_Orders.Customer_Contact = paymentArray.contact;
        //                payment_Orders.Error_Code = paymentArray.error_code;
        //                payment_Orders.Error_Description = paymentArray.error_description;
        //                payment_Orders.Fee = Convert.ToString(Convert.ToDouble(paymentArray.fee) *0.01);
        //                payment_Orders.Tax = Convert.ToString(Convert.ToDouble(paymentArray.tax) * 0.01);
        //                payment_Orders.Payment_Captured = paymentArray.captured;
        //                payment_Orders.Payment_Status = paymentArray.status;
        //                payment_Orders.Refunded_Amount = paymentArray.amount_refunded;
        //                payment_Orders.Refund_Status = paymentArray.refund_status;
        //                payment_Orders.Wallet = paymentArray.wallet;
        //                payment_Orders.International = paymentArray.international;
        //                payment_Orders.Payment_Method = paymentArray.method;

        //                payment_Orders = payment_orderServices.SavePayment_Orders(payment_Orders);


        //                //Saving Order Details
        //                OrderdetailsServices orderdetailsServices = new OrderdetailsServices();
        //                OrderDetail orderDetail = new OrderDetail();
        //                orderDetail.OrderId = order.OrderId;
        //                orderDetail.OrderBy = user.UserId;
        //                orderDetail.PaymentId = payment_Orders.PaymentID;
        //                orderDetail.ServiceType = type;
        //                orderDetail.ServicePrice = decimal.Parse(price);
        //                orderDetail.attribute = timeslot;
        //                orderDetail.TotalPrice = decimal.Parse(totalprice);
        //                orderDetail.PerunitPrice = decimal.Parse(price);
        //                orderDetail.Quantity = int.Parse(guest);
        //                orderDetail.OrderId = order.OrderId;
        //                orderDetail.VendorId = long.Parse(id);
        //                orderDetail.Status = "Pending";
        //                orderDetail.UpdatedDate = Convert.ToDateTime(updateddate);
        //                orderDetail.UpdatedBy = user.UserId;
        //                orderDetail.subid = long.Parse(vid);
        //                orderDetail.BookedDate = date;
        //                orderDetail.EventType = etype1;
        //                orderDetail.DealId = long.Parse(did);
        //                orderDetail.ExtraDate1 = cartdetails.c1date;
        //                orderDetail.ExtraDate2 = cartdetails.c2date;
        //                orderDetail.ExtraDate3 = cartdetails.c3date;

        //                orderdetailsServices.SaveOrderDetail(orderDetail);

        //                var userlogdetails = userLoginDetailsService.GetUserId(userid);

        //                string txtto = userlogdetails.UserName;
        //                var userdetails = userLoginDetailsService.GetUser(userid);
        //                string name = userdetails.FirstName;
        //                name = Capitalise(name);
        //                string OrderId = Convert.ToString(order.OrderId);
        //                string url = Request.Url.Scheme + "://" + Request.Url.Authority;
        //                FileInfo File = new FileInfo(Server.MapPath("/mailtemplate/order.html"));
        //                string readFile = File.OpenText().ReadToEnd();
        //                readFile = readFile.Replace("[ActivationLink]", url);
        //                readFile = readFile.Replace("[name]", name);
        //                readFile = readFile.Replace("[orderid]", OrderId);
        //                string txtmessage = readFile;//readFile + body;
        //                string subj = "Thanks for your order";
        //                EmailSendingUtility emailSendingUtility = new EmailSendingUtility();
        //                emailSendingUtility.Email_maaaahwanam(txtto, txtmessage, subj);

        //                var vendordetails = userLoginDetailsService.getvendor(Convert.ToInt32(id));

        //                string txtto1 = vendordetails.EmailId;
        //                string vname = vendordetails.BusinessName;
        //                vname = Capitalise(vname);

        //                string url1 = Request.Url.Scheme + "://" + Request.Url.Authority;
        //                FileInfo file1 = new FileInfo(Server.MapPath("/mailtemplate/vorder.html"));
        //                string readfile1 = file1.OpenText().ReadToEnd();
        //                readfile1 = readfile1.Replace("[ActivationLink]", url1);
        //                readfile1 = readfile1.Replace("[name]", name);
        //                readfile1 = readfile1.Replace("[vname]", vname);
        //                readfile1 = readfile1.Replace("[orderid]", OrderId);
        //                string txtmessage1 = readfile1;
        //                string subj1 = "order has been placed";
        //                emailSendingUtility.Email_maaaahwanam(txtto1, txtmessage1, subj1);
        //                var message = cartService.Deletecartitem(long.Parse(cartno2));
        //            }
        //        }
        //        return Json("Success", JsonRequestBehavior.AllowGet);
        //    }
        //    return Json(JsonRequestBehavior.AllowGet);
        //}

        public ActionResult booknow(string selcartid, string searchedcontent, string total, string booktype)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var msg = "";
                var user = (CustomPrincipal)System.Web.HttpContext.Current.User;
                if (user.UserType == "User")
                {
                    selcartid = selcartid.Trim(',');
                    HomeController home = new HomeController();
                    var userdata = userLoginDetailsService.GetUser((int)user.UserId);
                    ViewBag.cartCount = cartService.CartItemsCount((int)user.UserId);
                    var cartlist = cartService.CartItemsList(int.Parse(user.UserId.ToString()));
                    var cartnos1 = selcartid.Split(',');
                    DateTime updateddate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                    //Saving Record in order Table
                    OrderService orderService = new OrderService();
                    MaaAahwanam.Models.Order order = new MaaAahwanam.Models.Order();
                    order.TotalPrice = Convert.ToDecimal(total);
                    order.OrderDate = Convert.ToDateTime(updateddate);
                    order.UpdatedBy = (Int64)user.UserId;
                    order.OrderedBy = (Int64)user.UserId;
                    order.UpdatedDate = Convert.ToDateTime(updateddate);
                    order.Status = "Pending";
                    if (booktype == "Quote") { order.type = "Quote"; }
                    else
                    {
                        order.type = "Order";
                    }
                    order.bookingtype = "User";
                    order = orderService.SaveOrder(order);

                    //Payment Section
                    for (int i = 0; i < cartnos1.Count(); i++)
                    {
                        string cartno2 = cartnos1[i];
                        string totalprice = "";
                        var cartdetails = cartlist.Where(m => m.CartId == Convert.ToInt64(cartno2)).FirstOrDefault();
                        string type = cartdetails.ServicType;
                        string guest = Convert.ToString(cartdetails.Quantity);
                        string price = Convert.ToString(cartdetails.Perunitprice);
                        string id = Convert.ToString(cartdetails.Id);
                        string did = Convert.ToString(cartdetails.DealId);
                        string timeslot = cartdetails.attribute;
                        string etype1 = cartdetails.EventType;
                        string vid = Convert.ToString(cartdetails.subid);
                        DateTime date = Convert.ToDateTime(cartdetails.eventstartdate);
                        if (type == "Photography" || type == "Decorator" || type == "Other")
                        {
                            totalprice = price;
                            guest = "0";
                        }
                        else
                        {
                            totalprice = Convert.ToString(cartdetails.TotalPrice);
                        }
                        int userid = Convert.ToInt32(user.UserId);
                        //Saving Order Details
                        OrderdetailsServices orderdetailsServices = new OrderdetailsServices();
                        OrderDetail orderDetail = new OrderDetail();
                        orderDetail.OrderId = order.OrderId;
                        orderDetail.OrderBy = user.UserId;
                        orderDetail.PaymentId = '1';
                        orderDetail.ServiceType = type;
                        orderDetail.ServicePrice = decimal.Parse(price);
                        orderDetail.attribute = timeslot;
                        orderDetail.TotalPrice = decimal.Parse(totalprice);
                        orderDetail.PerunitPrice = decimal.Parse(price);
                        orderDetail.Quantity = int.Parse(guest);
                        orderDetail.OrderId = order.OrderId;
                        orderDetail.VendorId = long.Parse(id);
                        orderDetail.Status = "Pending";
                        orderDetail.UpdatedDate = Convert.ToDateTime(updateddate);
                        orderDetail.UpdatedBy = user.UserId;
                        orderDetail.subid = long.Parse(vid);
                        orderDetail.BookedDate = date;
                        orderDetail.EventType = etype1;
                        orderDetail.DealId = long.Parse(did);
                        orderDetail.ExtraDate1 = cartdetails.c1date;
                        orderDetail.ExtraDate2 = cartdetails.c2date;
                        orderDetail.ExtraDate3 = cartdetails.c3date;
                        if (booktype == "Quote") { orderDetail.type = "Quote"; }
                        else
                        {
                            orderDetail.type = "Order";
                        }
                        orderDetail.bookingtype = "User";
                        orderdetailsServices.SaveOrderDetail(orderDetail);
                        var userlogdetails = userLoginDetailsService.GetUserId(userid);
                        string txtto = userlogdetails.UserName;
                        var userdetails = userLoginDetailsService.GetUser(userid);
                        string name = userdetails.FirstName;
                        StringBuilder cds = new StringBuilder();
                        cds.Append("<table style='border:1px black solid;'><tbody>");
                        cds.Append("<tr><td>Order Id</td><td>Order Date</td><td> Event Type </td><td> Quantity</td><td>Perunit Price</td><td>Total Price</td></tr>");
                        cds.Append("<tr><td style = 'width: 75px;border: 1px black solid;'> " + order.OrderId + "</td><td style = 'width: 75px;border: 1px black solid;' > " + orderDetail.BookedDate + " </td><td style = 'width: 75px;border: 1px black solid;'> " + orderDetail.EventType + " </td><td style = 'width: 50px;border: 1px black solid;'> " + orderDetail.Quantity + " </td> <td style = 'width: 50px;border: 1px black solid;'> " + orderDetail.PerunitPrice + " </td><td style = 'width: 50px;border: 1px black solid;'> " + orderDetail.TotalPrice + " </td></tr>");  //<td style = 'width: 50px;border: 2px black solid;'> " + item.eventstartdate + " </td><td> date </td>
                        cds.Append("</tbody></table>");
                        name = home.Capitalise(name);
                        string OrderId = Convert.ToString(order.OrderId);
                        string url = Request.Url.Scheme + "://" + Request.Url.Authority;
                        FileInfo File = new FileInfo(Server.MapPath("/mailtemplate/order.html"));
                        string readFile = File.OpenText().ReadToEnd();
                        readFile = readFile.Replace("[ActivationLink]", url);
                        readFile = readFile.Replace("[name]", name);
                        readFile = readFile.Replace("[orderid]", OrderId);
                        readFile = readFile.Replace("[table]", cds.ToString());

                        string txtmessage = readFile;//readFile + body;
                        string subj = "Thanks for your order";
                        EmailSendingUtility emailSendingUtility = new EmailSendingUtility();
                        emailSendingUtility.Email_maaaahwanam(txtto, txtmessage, subj, null);
                        emailSendingUtility.Email_maaaahwanam("seema@xsilica.com ", txtmessage, subj, null);
                        var vendordetails = userLoginDetailsService.getvendor(Convert.ToInt32(id));
                        string txtto1 = vendordetails.EmailId;
                        string vname = vendordetails.BusinessName;
                        vname = home.Capitalise(vname);
                        string url1 = Request.Url.Scheme + "://" + Request.Url.Authority;
                        FileInfo file1 = new FileInfo(Server.MapPath("/mailtemplate/vorder.html"));
                        string readfile1 = file1.OpenText().ReadToEnd();
                        readfile1 = readfile1.Replace("[ActivationLink]", url1);
                        readfile1 = readfile1.Replace("[name]", name);
                        readfile1 = readfile1.Replace("[vname]", vname);
                        readfile1 = readfile1.Replace("[orderid]", OrderId);
                        readFile = readFile.Replace("[table]", cds.ToString());
                        string txtmessage1 = readfile1;
                        string subj1 = "order has been placed";
                        emailSendingUtility.Email_maaaahwanam(txtto1, txtmessage1, subj1, null);
                        if (booktype == "Quote") { }
                        else if (booktype == "booknow") { var message = cartService.Deletecartitem(long.Parse(cartno2)); }
                    }
                }
                if (booktype == "Quote") { msg = "Quotation sent"; }
                else if (booktype == "booknow") { msg = "Order Successfully placed"; }
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            return Json(JsonRequestBehavior.AllowGet);
        }
    }
}

