﻿using MaaAahwanam.Repository;
using MaaAahwanam.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MaaAahwanam.Models;
using MaaAahwanam.Utility;
using MaaAahwanam.Web.Custom;

namespace MaaAahwanam.Web.Controllers
{
    public class ProductInfoController : Controller
    {
        ReviewService reviewService = new ReviewService();
        UserLoginDetailsService userLoginDetailsService = new UserLoginDetailsService();
        AvailabledatesService availabledatesService = new AvailabledatesService();
        //
        // GET: /CardInfo/
        public ActionResult Index()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrincipal)System.Web.HttpContext.Current.User;
                ViewBag.Userloginstatus = user.UserId;
                ViewBag.usertype = user.UserType;
            }
            else
            {
                ViewBag.Userloginstatus = 0;
            }
            ProductInfoService productInfoService = new ProductInfoService();
            VendorVenueService vendorVenueService = new VendorVenueService();
            Review review = new Review();
            string Servicetype = Request.QueryString["par"];
            if(Servicetype== "Travel&Accomadation" || Servicetype == "Travel&Accommodation")
            {
                Servicetype = "Travel";
            }
            int vid = Convert.ToInt32(Request.QueryString["VID"]);
            int Svid = Convert.ToInt32(Request.QueryString["subvid"]);
            ViewBag.Subvid = Svid;
            GetProductsInfo_Result Productinfo = productInfoService.getProductsInfo_Result(vid, Servicetype, Svid);
            List<SP_Amenities_Result> Amenities = productInfoService.GetAmenities(Svid, Servicetype);
            ViewBag.Amenities = Amenities; string type = Request.QueryString["par"];
            //Vendor Available Dates
            if (type == "Travel")
            {
                type = "Travel&Accommodation";
            }
            var vendorid = userLoginDetailsService.GetLoginDetailsByEmail(Productinfo.EmailId);
            if (vendorid != 0)
            {
                var today = DateTime.UtcNow;
                var first = new DateTime(today.Year, today.Month, 1);
                var vendordates = availabledatesService.GetCurrentMonthDates(vendorid).Select(m => m.availabledate.ToShortDateString()).ToArray();
                var bookeddates = productInfoService.GetCount(vid, Svid, type).Where(m => m.BookedDate > first).Select(m => m.BookedDate.Value.ToShortDateString()).Distinct().ToArray();
                //var bookeddates = productInfoService.disabledate(vid, Svid, type).Split(',');
                var finalbookeddates = bookeddates.Except(vendordates);
                var finalvendordates = vendordates.Except(bookeddates);
                //var finalbookeddates1 = bookeddates;
                //var finalvendordates1 = vendordates;
                if (finalbookeddates.Count() != 0)
                    ViewBag.vendoravailabledates = string.Join(",", finalvendordates) +  string.Join(",", finalbookeddates);
                else
                    ViewBag.vendoravailabledates = string.Join(",", finalvendordates);
            }
            //List<DateTime> availabledates = availabledatesService.GetCurrentMonthDates(vendorid).Select(m=>m.a);
            if (Productinfo != null)
            {
                if (Productinfo.image != null)
                {
                    string[] imagenameslist = Productinfo.image.Replace(" ", "").Split(',');
                    ViewBag.Imagelist = imagenameslist;
                }
                
            }
            ViewBag.servicetype = Servicetype;
            ViewBag.Reviewlist = reviewService.GetReview(vid);
            
            //Ratings count & avg rating 
            ViewBag.ratingscount = productInfoService.GetCount(vid,Svid, type).Count();
            
            ViewBag.rating = productInfoService.ratingscount(vid, Svid, type);
            if (Productinfo.ServicType == "Venue")
            {
                var list = vendorVenueService.GetVendorVenue(vid, Svid);
                ViewBag.venuetype = list.VenueType;
                ViewBag.servicecost = list.ServiceCost;
            }
            //var tupleModel = new Tuple<GetProductsInfo_Result, Review,SP_Amenities_Result>(Productinfo, review,Amenities);
            var tupleModel = new Tuple<GetProductsInfo_Result, Review>(Productinfo, review);
            return View(tupleModel);
        }
        public ActionResult WriteaRiview([Bind(Prefix = "Item2")] Review review)
        {
            var user = (CustomPrincipal)System.Web.HttpContext.Current.User;
            string updateddate = DateTime.UtcNow.ToShortDateString();
            review.UpdatedBy = (int)user.UserId;
            review.Status = "Active";
            review.UpdatedDate = Convert.ToDateTime(updateddate);
            reviewService.InsertReview(review);
            return RedirectToAction("Index", new { par = review.Service, VID = review.ServiceId , subvid =review.Sid});

            //return RedirectToAction("Index", "Signin");
        }
        public JsonResult Addtocart(OrderRequest orderRequest, string bookeddate)
        {
            var user = (CustomPrincipal)System.Web.HttpContext.Current.User;
            string updateddate = DateTime.UtcNow.ToShortDateString();
            CartItem cartItem = new CartItem();
            cartItem.VendorId = orderRequest.VendorId;
            cartItem.ServiceType = orderRequest.ServiceType;
            cartItem.Perunitprice = orderRequest.Perunitprice;
            cartItem.TotalPrice = orderRequest.TotalPrice;
            cartItem.Orderedby = user.UserId;
            cartItem.Quantity = orderRequest.Quantity;
            cartItem.UpdatedDate = Convert.ToDateTime(updateddate);
            cartItem.subid = orderRequest.subid;
            cartItem.attribute = orderRequest.attribute;
            CartService cartService = new CartService();
            cartItem = cartService.AddCartItem(cartItem);
            EventInformation eventInformation = new EventInformation();
            eventInformation.EventName = orderRequest.EventName;
            eventInformation.Email = orderRequest.Email;
            eventInformation.Address = orderRequest.Address;
            eventInformation.Location = orderRequest.Location;
            eventInformation.Phone = orderRequest.Phone;
            eventInformation.PostalCode = orderRequest.PostalCode;
            eventInformation.State = orderRequest.State;
            eventInformation.City = orderRequest.City;
            eventInformation.vendorid = orderRequest.VendorId;
            eventInformation.subid = orderRequest.subid;
            eventInformation.CartId = cartItem.CartId;

            EventsService eventsService = new EventsService();
            eventInformation = eventsService.SaveEventinformation(eventInformation);

            EventDate eventDate = new EventDate();
            foreach (var item in orderRequest.EventDates)
            {
                eventDate.StartDate = item.StartDate;
                eventDate.StartTime = item.StartTime;
                eventDate.EndDate = item.EndDate;
                eventDate.EndTime = item.EndTime;
                eventDate.vendorid = orderRequest.VendorId;
                eventDate.EventId = eventInformation.EventId;
            }


            EventDatesServices eventDatesServices = new EventDatesServices();
            string message3 = eventDatesServices.SaveEventDates(eventDate);
            return Json(message3);
        }
        [Authorize]
        public JsonResult Buynow(OrderRequest orderRequest,string bookeddate)
        {
            var user = (CustomPrincipal)System.Web.HttpContext.Current.User;
            string updateddate = DateTime.UtcNow.ToShortDateString();
            OrderService orderService = new OrderService();
            Order order = new Order();
            order.TotalPrice = Convert.ToDecimal(orderRequest.TotalPrice);
            order.OrderDate = Convert.ToDateTime(updateddate); //Convert.ToDateTime(bookeddate);
            order.UpdatedBy = (Int64)user.UserId;
            order.OrderedBy = (Int64)user.UserId;
            order.UpdatedDate = Convert.ToDateTime(updateddate);
            order.Status = "Active";
            order = orderService.SaveOrder(order);

            Payment_orderServices payment_orderServices = new Payment_orderServices();
            Payment_Orders payment_Orders = new Payment_Orders();
            payment_Orders.cardnumber = orderRequest.cardnumber;
            payment_Orders.CVV = orderRequest.CVV;
            payment_Orders.paidamount = orderRequest.TotalPrice;
            payment_Orders.PaymentID = orderRequest.PaymentId;
            payment_Orders.Paiddate = orderRequest.Paiddate;
            payment_Orders.OrderID = order.OrderId;
            payment_Orders = payment_orderServices.SavePayment_Orders(payment_Orders);

            OrderdetailsServices orderdetailsServices = new OrderdetailsServices();
            OrderDetail orderDetail = new OrderDetail();
            orderDetail.OrderId = order.OrderId;
            orderDetail.OrderBy = user.UserId;
            orderDetail.PaymentId = payment_Orders.PaymentID;
            orderDetail.ServiceType = orderRequest.ServiceType;
            orderDetail.ServicePrice = orderRequest.ServicePrice;
            orderDetail.TotalPrice = Convert.ToDecimal(orderRequest.TotalPrice);
            orderDetail.PerunitPrice = orderRequest.TotalPrice;
            orderDetail.Quantity = orderRequest.Quantity;
            orderDetail.OrderId = order.OrderId;
            orderDetail.VendorId = orderRequest.VendorId;
            orderDetail.Status = "Active";
            orderDetail.UpdatedDate = Convert.ToDateTime(updateddate);
            orderDetail.UpdatedBy = user.UserId;
            orderDetail.subid = orderRequest.subid;
            orderDetail.BookedDate = Convert.ToDateTime(bookeddate);
            orderdetailsServices.SaveOrderDetail(orderDetail);


            EventInformation eventInformation = new EventInformation();
            eventInformation.EventName = orderRequest.EventName;
            eventInformation.Email = orderRequest.Email;
            eventInformation.Address = orderRequest.Address;
            eventInformation.Location = orderRequest.Location;
            eventInformation.Phone = orderRequest.Phone;
            eventInformation.PostalCode = orderRequest.PostalCode;
            eventInformation.State = orderRequest.State;
            eventInformation.City = orderRequest.City;
            eventInformation.OrderId = order.OrderId;
            eventInformation.OrderDetailsid = orderDetail.OrderDetailId;

            EventsService eventsService = new EventsService();
            eventInformation = eventsService.SaveEventinformation(eventInformation);

            EventDatesServices eventDatesServices = new EventDatesServices();
            EventDate eventDate = new EventDate();
            foreach (var item in orderRequest.EventDates)
            {
                eventDate.StartDate = item.StartDate;
                eventDate.StartTime = item.StartTime;
                eventDate.EndDate = item.EndDate;
                eventDate.EndTime = item.EndTime;
                eventDate.EventId = eventInformation.EventId;
                string message3 = eventDatesServices.SaveEventDates(eventDate);
            }

            return Json(orderDetail.OrderId);
        }
        [Authorize]
        public JsonResult getproductfromcart(string cid)
        {

            CartService cartService = new CartService();
            var user = (CustomPrincipal)System.Web.HttpContext.Current.User;
            GetCartItems_Result cartlist = cartService.editcartitem(int.Parse(user.UserId.ToString()), int.Parse(cid));
            return Json(cartlist);
        }
        [Authorize]
        public JsonResult Updatecartitem(OrderRequest orderRequest)
        {

            var user = (CustomPrincipal)System.Web.HttpContext.Current.User;
            string updateddate = DateTime.UtcNow.ToShortDateString();
            CartItem cartItem = new CartItem();
            cartItem.VendorId = orderRequest.VendorId;
            cartItem.ServiceType = orderRequest.ServiceType;
            cartItem.Perunitprice = orderRequest.Perunitprice;
            cartItem.TotalPrice = orderRequest.TotalPrice;
            cartItem.Orderedby = user.UserId;
            cartItem.Quantity = orderRequest.Quantity;
            cartItem.UpdatedDate = Convert.ToDateTime(updateddate);
            cartItem.attribute = orderRequest.attribute;
            cartItem.CartId = orderRequest.cid;

            CartService cartService = new CartService();
            string mesaage = cartService.Updatecartitem(cartItem);
            return Json(mesaage);
        }
    }
}