﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MaaAahwanam.Utility;
using MaaAahwanam.Models;
using MaaAahwanam.Service;
using MaaAahwanam.Repository;
using MaaAahwanam.Web.Custom;
using System.Web.Security;
using System.Web.Mvc.Html;

namespace MaaAahwanam.Web.Controllers
{
    public class IndexController : Controller
    {
        EventsandtipsService eventsandtipsService = new EventsandtipsService();
        public ActionResult Index()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrincipal)System.Web.HttpContext.Current.User;
                if (user.UserType == "Vendor") return RedirectToAction("Index", "VendorDashBoard");//("VendorDashBoard","Index");
            }
            EventsService eventsService = new EventsService();
            ViewBag.EventsCount = eventsService.EventInformationCount();//Successful Events Count
            ticketsService ticketsService = new ticketsService();
            ViewBag.Ticketscount = ticketsService.TicketsCount();//Raised Tickets COunt
            TestmonialService testmonialService = new TestmonialService();
            ViewBag.Testimonials = testmonialService.TestmonialServiceList();//Testimonials List
            //Products List Index(4 Services Photography,Beautition,Decorators,Travels)
            
            var Events = eventsandtipsService.EventsandTipsListUser("Event",0).OrderByDescending(m=>m.EventId).Take(4);
            var Beautytips = eventsandtipsService.EventsandTipsListUser("Beauty Tips",0).OrderByDescending(m => m.EventId).Take(4);
            var Healthtips = eventsandtipsService.EventsandTipsListUser("Health Tips",0).OrderByDescending(m => m.EventId).Take(4);
            ViewBag.UpcomingEvents = Events;
            ViewBag.HealthTips = Healthtips;
            ViewBag.BeautyTips = Beautytips;
            ViewBag.EventLastRecord = Events.Last().EventId;
            ViewBag.BeautytipsLastRecord = Beautytips.Last().EventId;
            ViewBag.HealthtipsLastRecord = Healthtips.Last().EventId;
            ProductService productService = new ProductService();
            //List<SP_Deals_Result> Productlist_Photography = productService.GetSP_Deals_Result("Photography", 0, "%", "Hyderabad", "ASC");
            //List<SP_Deals_Result> Productlist_BeautyService = productService.GetSP_Deals_Result("BeautyService", 0, "%", "Hyderabad", "ASC");
            //List<SP_Deals_Result> Productlist_Decorator = productService.GetSP_Deals_Result("Decorator", 0, "%", "Hyderabad", "ASC");
            //List<SP_Deals_Result> Productlist_Travel = productService.GetSP_Deals_Result("Travel", 0, "%", "Hyderabad", "ASC");
            List<sp_indexdeals_Result> Productlist_Photography = productService.gettopdealsservice("Photography");
            List<sp_indexdeals_Result> Productlist_BeautyService = productService.gettopdealsservice("BeautyService");
            List<sp_indexdeals_Result> Productlist_Decorator = productService.gettopdealsservice("Decorator");
            List<sp_indexdeals_Result> Productlist_Travel = productService.gettopdealsservice("Travel");
            ViewBag.PhotographersDetails = Productlist_Photography;
            ViewBag.Beautician = Productlist_BeautyService;
            ViewBag.Decorators = Productlist_Decorator;
            ViewBag.ToursandTravels = Productlist_Travel;
            return View();
        }


        [ChildActionOnly]
        public PartialViewResult ItemsCartViewBindingLayout()
        {
            CartService cartService = new CartService();
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrincipal)System.Web.HttpContext.Current.User;
                if (user.UserType == "Admin")
                {
                    ViewBag.cartCount = cartService.CartItemsCount(0);
                    return PartialView("ItemsCartViewBindingLayout");
                }
                    ViewBag.cartCount = cartService.CartItemsCount((int)user.UserId);
                
                List<GetCartItems_Result> cartlist = cartService.CartItemsList(int.Parse(user.UserId.ToString()));
                //List<cartcount_Result> cartlist = cartService.cartcountservice(user.UserId);
                decimal total = cartlist.Sum(s => s.TotalPrice);
                ViewBag.cartitems = cartlist;
                ViewBag.Total = total;
                //ViewBag.cartcounttotal = cartService.cartcountservice(user.UserId).Count();
                //ViewBag.cartitems = cartService.cartcountservice(user.UserId);
            }
            else
            {
                ViewBag.cartCount = cartService.CartItemsCount(0);
            }
            return PartialView("ItemsCartViewBindingLayout");
        }

        [ChildActionOnly]
        public PartialViewResult TestimonialsBindingLayout(string view)
        {
            return PartialView("TestimonialsBindingLayout");
        }

        [HttpPost]
        public JsonResult SubmittingSubscriber(Subscription Subscription)
        {
            string message = string.Empty;
            try
            {
                SubscriptionService subscriptionService = new SubscriptionService();
                subscriptionService.addsubscription(Subscription);
                EmailSendingUtility EmailSend = new EmailSendingUtility();
                EmailSend.Email_maaaahwanam(Subscription.EmailId,"Thank you for subscribing to Maa-Aahwanam","Confirmation Subscription", null);
                message = "subscribed successfully";
            }
            catch
            {
                message = "subscription failed";
            }
            return Json(String.Format(message));
        }

        public JsonResult AutoCompleteCountry()
        {
            AllVendorsService allVendorsService = new AllVendorsService();
            var Listoflocations = allVendorsService.VendorsList().Distinct();
            var builder = new TagBuilder("<br/>");

            string[] ListofEvents = { "Wedding", "Reception", "Engagement", "Birthday", "Wedding Anniversary", "Get Together", "Kitty Party", "Cocktail Party" };
            return Json(new { Listoflocations, ListofEvents }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Loadmore(string lastrecord,string typ)
        {
            //if (typ == "Event")
            //{
            //    var Events = eventsandtipsService.EventsandTipsListUser("Event",int.Parse(lastrecord)).OrderByDescending(m => m.EventId).Take(4);
            //    //ViewBag.EventLastRecord = Events.Last().EventId;
            //    return Json(Events);
            //}
             if (typ == "Beauty")
            {
                var Beauty = eventsandtipsService.EventsandTipsListUser("Beauty Tips", int.Parse(lastrecord)).OrderByDescending(m => m.EventId).Take(4);
                //ViewBag.BeautytipsLastRecord = Beauty.Last().EventId;
                return Json(Beauty);
            }
            else if (typ == "Health")
            {
                var Health = eventsandtipsService.EventsandTipsListUser("Health Tips", int.Parse(lastrecord)).OrderByDescending(m => m.EventId).Take(4);
                //ViewBag.HealthtipsLastRecord = Health.Last().EventId;
                return Json(Health);
            }
            return Json(JsonRequestBehavior.AllowGet);
        }
    }
}