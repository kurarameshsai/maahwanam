﻿using MaaAahwanam.Models;
using MaaAahwanam.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaaAahwanam.Web.Controllers
{
    public class CateringFAQController : Controller
    {
        Vendormaster vendorMaster = new Vendormaster();
        VenorVenueSignUpService venorVenueSignUpService = new VenorVenueSignUpService();
        VendorCateringService vendorCateringService = new VendorCateringService();
        // GET: CateringFAQ
        public ActionResult Index(string id, string vid)
        {
            ViewBag.id = id;
            ViewBag.vid = vid;
            ViewBag.catering = vendorCateringService.GetVendorCatering(long.Parse(id), long.Parse(vid));
            return View();
        }

        [HttpPost]
        public ActionResult Index(string id, string vid, VendorsCatering vendorsCatering)
        {
            var data = vendorCateringService.GetVendorCatering(long.Parse(id), long.Parse(vid));
            vendorsCatering.UpdatedBy = vendorMaster.UpdatedBy = 2;
            vendorsCatering.CuisineType = data.CuisineType;
            vendorsCatering.discount = data.discount;
            vendorsCatering.Address = data.Address;
            vendorsCatering.City = data.City;
            vendorsCatering.State = data.State;
            vendorsCatering.Landmark = data.Landmark;
            vendorsCatering.ZipCode = data.ZipCode;
            vendorsCatering.name = data.name;
            vendorsCatering.Status = data.Status;
            //vendorsCatering.tier = data.tier;
            long masterid = vendorsCatering.VendorMasterId = vendorMaster.Id = long.Parse(id);
            vendorsCatering = venorVenueSignUpService.UpdateCatering(vendorsCatering, vendorMaster, masterid, long.Parse(vid));
            return Content("<script language='javascript' type='text/javascript'>alert('Details Saved Successfully');location.href='AvailableServices/Index?id=" + id + "&&vid=" + vid + "'</script>");
        }
    }
}