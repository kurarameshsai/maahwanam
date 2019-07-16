﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MaaAahwanam.Web.Custom;
using MaaAahwanam.Service;
using MaaAahwanam.Models;
using MaaAahwanam.Utility;
using MaaAahwanam.Repository;

namespace MaaAahwanam.Web.Controllers
{
    public class NHomePageController : Controller
    {
        // GET: NHomePage
        VendorProductsService vendorProductsService = new VendorProductsService();
        UserLoginDetailsService userLoginDetailsService = new UserLoginDetailsService();
        CartService cartService = new CartService();
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        public ActionResult Index()
        {
            try
            {
                //DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                //ViewBag.currenttme = indianTime;
                if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    var user = (CustomPrincipal)System.Web.HttpContext.Current.User;
                    var userdata = userLoginDetailsService.GetUser((int)user.UserId);
                    if (userdata.FirstName != "" && userdata.FirstName != null)
                        ViewBag.username = userdata.FirstName;
                    else if (userdata.FirstName != "" && userdata.FirstName != null && userdata.LastName != "" && userdata.LastName != null)
                        ViewBag.username = "" + userdata.FirstName + " " + userdata.LastName + "";
                    else
                        ViewBag.username = userdata.AlternativeEmailID;

                    if (user.UserType == "Admin")
                    {
                        ViewBag.cartCount = cartService.CartItemsCount(0);
                        return PartialView("ItemsCartViewBindingLayout");
                    }
                    ViewBag.cartCount = cartService.CartItemsCount1((int)user.UserId);
                   var cartlist = cartService.CartItemsList1(int.Parse(user.UserId.ToString()));
                    decimal total = cartlist.Sum(s => s.TotalPrice);
                    ViewBag.cartitems = cartlist;
                    ViewBag.Total = total;
                }
                else
                {
                    ViewBag.cartCount = cartService.CartItemsCount(0);
                }
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Nhomepage");
            }
        }

        public JsonResult AutoCompleteCountry()
        {
            VendorMasterService allVendorsService = new VendorMasterService();
            var Listoflocations = String.Join(",", allVendorsService.GetVendorCities().Distinct());
            return new JsonResult { Data = Listoflocations, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult SortVendorsBasedOnLocation(string search, string type, string location)
        {

            if (new string[] { "Wedding", "Party", "Corporate", "BabyFunction", "Birthday", "Engagement", "Venues", "FunctionHall", "BanquetHall", "Function Hall", "Banquet Hall" }.Contains(type))
            { type = "Venue"; }
            if (type == "Convention") type = "Convention Hall";
            type = (type == "BanquetHall") ? "Banquet Hall" : type;
            type = (type == "FunctionHall") ? "Function Hall" : type;
            if (type != null) if (type.Split(',').Count() > 1) type = "Venue";
            var value = (type == null || type == "Venues") ? "Venue" : type;
            var records = vendorProductsService.Getsearchvendorproducts_Result(search, value);
            if (type == "Hotel" || type == "Resort" || type == "Convention Hall")
                records = records.Where(m => m.subtype.Contains(type)).Take(6).ToList();
            else
                records = records.Take(6).ToList();
            ViewBag.records = (search == null) ? vendorProductsService.Getsearchvendorproducts_Result("V", value).Where(m => m.landmark == location).Take(6).ToList() : records;//vendorProductsService.Getsearchvendorproducts_Result(search, value).Where(m => m.subtype == type).Take(6).ToList();//vendorProductsService.Getsearchvendorproducts_Result(search, value).Take(6).ToList(); //.Where(m => m.landmark == location)
            return PartialView();


        }



        public ActionResult ItemsCartViewBindingLayout()
        {
            try
            {
                if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    var user = (CustomPrincipal)System.Web.HttpContext.Current.User;

                    if (user.UserType == "User")
                    {
                        var userdata = userLoginDetailsService.GetUser((int)user.UserId);
                        if (userdata.FirstName != "" && userdata.FirstName != null)
                            ViewBag.username = userdata.FirstName;
                        else if (userdata.FirstName != "" && userdata.FirstName != null && userdata.LastName != "" && userdata.LastName != null)
                            ViewBag.username = "" + userdata.FirstName + " " + userdata.LastName + "";
                        else
                            ViewBag.username = userdata.AlternativeEmailID;
                        if (user.UserType == "Admin")
                        {
                            ViewBag.cartCount = cartService.CartItemsCount(0);
                            return PartialView("ItemsCartViewBindingLayout");
                        }


                        ViewBag.cartCount = cartService.CartItemsCount1((int)user.UserId);
                        //   List<GetCartItems_Result> cartlist = cartService.CartItemsList(int.Parse(user.UserId.ToString()));
                        var cartlist = cartService.CartItemsList1(int.Parse(user.UserId.ToString()));
                        var cartlist1 = cartService.CartItemsList1(int.Parse(user.UserId.ToString()));

                        //  decimal total = (cartlist1.TotalPrice) .Sum(s => s.TotalPrice);
                        ViewBag.cartitems = cartlist;
                        // ViewBag.Total = total;
                        ViewBag.Total = "0";
                    }
                }
                else
                {
                    ViewBag.cartCount = cartService.CartItemsCount(0);
                }
                return PartialView("ItemsCartViewBindingLayout");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Nhomepage");
            }
        }


        public ActionResult ItemsCartdetails()
        {
            try
            {
                if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    var user = (CustomPrincipal)System.Web.HttpContext.Current.User;
                    if (user.UserType == "User")
                    {
                        var userdata = userLoginDetailsService.GetUser((int)user.UserId);
                        if (user.UserType == "Admin")
                        {
                            ViewBag.cartCount = cartService.CartItemsCount(0);
                            return PartialView("ItemsCartdetails");
                        }
                        ViewBag.cartCount = cartService.CartItemsCount((int)user.UserId);
                        var cartlist1 = cartService.CartItemsList1(int.Parse(user.UserId.ToString()));

                        // List<GetCartItems_Result> cartlist = cartService.CartItemsList(int.Parse(user.UserId.ToString()));
                        decimal total = cartlist1.Select(m => m.TotalPrice).Sum();
                        ViewBag.cartitems = cartlist1;
                         ViewBag.Total = total;
                      //  ViewBag.Total = "0";
                    }
                }
                else
                {
                    ViewBag.cartCount = cartService.CartItemsCount(0);
                }


                return PartialView("ItemsCartdetails");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Nhomepage");
            }
        }
    }
}