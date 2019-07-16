﻿using MaaAahwanam.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaaAahwanam.Web.Areas.Admin.Controllers
{
    public class adbController : Controller
    {
        PartnerService partnerservice = new PartnerService();

        // GET: Admin/adb
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult AutoCompletevendor()
        {
            VendorMasterService allVendorsService = new VendorMasterService();
            var Listoflocations = String.Join(",", allVendorsService.GetVendorname().Distinct());
            //return Json(Listoflocations,JsonRequestBehavior.AllowGet);
            return new JsonResult { Data = Listoflocations, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult sidebar()
        {
            var allresellers = partnerservice.GetallPartners();
            var allresellerspack = partnerservice.getallPartnerPackage();
            ViewBag.allresellers = allresellers;
            return View();
        }
    }
    }