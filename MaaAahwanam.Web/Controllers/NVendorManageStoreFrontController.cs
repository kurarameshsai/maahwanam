﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MaaAahwanam.Web.Custom;
using MaaAahwanam.Service;
using MaaAahwanam.Models;
using MaaAahwanam.Utility;

namespace MaaAahwanam.Web.Controllers
{
    public class NVendorManageStoreFrontController : Controller
    {
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        const string imagepath = @"/vendorimages/";
        VendorImageService vendorImageService = new VendorImageService();
        VenorVenueSignUpService vendorVenueSignUpService = new VenorVenueSignUpService();
        VendorMasterService vendorMasterService = new VendorMasterService();
        // GET: NVendorManageStoreFront
        public ActionResult Index(string ks, string vid, string category, string subcategory)
        {
            try
            {
                string strReq = "";
                encptdecpt encript = new encptdecpt();
                strReq = encript.Decrypt(ks);
                //Parse the value... this is done is very raw format.. you can add loops or so to get the values out of the query string...
                string[] arrMsgs = strReq.Split('&');
                string[] arrIndMsg;
                string id = "";
                arrIndMsg = arrMsgs[0].Split('='); //Get the id
                id = arrIndMsg[1].ToString().Trim();
                ViewBag.id = ks;
                ViewBag.vid = vid;
            ViewBag.category = category;
            ViewBag.subcategory = subcategory;
            if (vid != null)
            ViewBag.images = vendorImageService.GetImages(long.Parse(id), long.Parse(vid));
            ViewBag.Vendor = vendorMasterService.GetVendor(long.Parse(id));
            ViewBag.display = "0";
            if (vid != "" && vid != null)
            {
                ViewBag.display = "1";
                if (category == "Venue")
                {
                    VendorVenueService vendorVenueService = new VendorVenueService();
                    ViewBag.service = vendorVenueService.GetVendorVenue(long.Parse(id), long.Parse(vid));
                    ViewBag.categorytype = ViewBag.service.VenueType;
                    return View();
                }
                if (category == "Catering")
                {
                    VendorCateringService vendorCateringService = new VendorCateringService();
                    ViewBag.service = vendorCateringService.GetVendorCatering(long.Parse(id), long.Parse(vid));
                    ViewBag.categorytype = ViewBag.service.CuisineType;
                    return View();
                }
                if (category == "Photography")
                {
                    VendorPhotographyService vendorPhotographyService = new VendorPhotographyService();
                    ViewBag.service = vendorPhotographyService.GetVendorPhotography(long.Parse(id), long.Parse(vid));
                    ViewBag.categorytype = ViewBag.service.PhotographyType;
                    return View();
                }
                if (category == "Decorator")
                {
                    VendorDecoratorService vendorDecoratorService = new VendorDecoratorService();
                    ViewBag.service = vendorDecoratorService.GetVendorDecorator(long.Parse(id), long.Parse(vid));
                    ViewBag.categorytype = ViewBag.service.DecorationType;
                    return View();
                }
                if (category == "Other")
                {
                    VendorOthersService vendorOthersService = new VendorOthersService();
                    ViewBag.service = vendorOthersService.GetVendorOther(long.Parse(id), long.Parse(vid));
                    ViewBag.categorytype = ViewBag.service.type;
                    return View();
                }
            }
            return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Nhomepage");
            }
        }

        [HttpPost]
        public ActionResult Index(string ks, string command, string serviceselection, string subcategory, string vid)
        {
            try
            {
                string strReq = "";
                encptdecpt encript = new encptdecpt();
                strReq = encript.Decrypt(ks);
                //Parse the value... this is done is very raw format.. you can add loops or so to get the values out of the query string...
                string[] arrMsgs = strReq.Split('&');
                string[] arrIndMsg;
                string id = "";
                arrIndMsg = arrMsgs[0].Split('='); //Get the id
                id = arrIndMsg[1].ToString().Trim();
                string msg = "";

                if (subcategory != "Select Sub-Category")
                {
                    long count = 0;
                    if (command == "add")
                        count = addservice(serviceselection, subcategory, long.Parse(id));
                    else if (command == "update")
                        count = updateservice(serviceselection, subcategory, long.Parse(id), long.Parse(vid));
                    if (count > 0) msg = "Service Added Successfully";
                    else if (serviceselection == "Select Service Type")
                    {
                        msg = "Failed To Add Sevice";

                        return Content("<script language='javascript' type='text/javascript'>alert('" + msg + "');location.href='/NVendorManageStoreFront/Index?ks=" + ks + "'</script>");
                    }


                    else
                        msg = "Failed To Add Sevice";

                    return Content("<script language='javascript' type='text/javascript'>alert('" + msg + "');location.href='/NVendorManageStoreFront/Index?ks=" + ks + "&&vid=" + count + "&&category=" + serviceselection + "&&subcategory=" + subcategory + "'</script>");

                }
            
                msg = "Failed To Add Sub Sevice";

                return Content("<script language='javascript' type='text/javascript'>alert('" + msg + "');location.href='/NVendorManageStoreFront/Index?ks=" + ks + "'</script>");
            
            }
                  

            catch (Exception)
            {
                return RedirectToAction("Index", "Nhomepage");
            }
        }

        public JsonResult filtercategories(string type)
        {
            string venueservices = "Select Sub-Category,Convention Hall,Function Hall,Banquet Hall,Meeting Room,Open Lawn,Hotel,Resort"; //Roof Top,
            string cateringservices = "Select Sub-Category,Indian,Chinese,Mexican,South Indian,Continental,Multi Cuisine,Chaat,Fast Food,Others";
            string photographyservices = "Select Sub-Category,Wedding,Candid,Portfolio,Fashion,Toddler,Videography,Conventional,Cinematography,Others";
            //string eventservices = "Select Sub-Category,Corporate Events,Brand Promotion,Fashion Shows,Exhibition,Conference & Seminar,Wedding Management,Birthday Planning & Celebrations,Live Concerts,Musical Nights,Celebrity Shows";
            string decoratorservices = "Select Sub-Category,Florists,TentHouse Decorators,Others";
            string otherservices = "Select Sub-Category,Mehendi,Pandit";
            if (type == "Venue") return Json(venueservices, JsonRequestBehavior.AllowGet);
            else if (type == "Catering") return Json(cateringservices, JsonRequestBehavior.AllowGet);
            else if (type == "Photography") return Json(photographyservices, JsonRequestBehavior.AllowGet);
            else if (type == "Decorator") return Json(decoratorservices, JsonRequestBehavior.AllowGet);
            else if (type == "Other") return Json(otherservices, JsonRequestBehavior.AllowGet);
            return Json("Fail", JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateStoreFront(string command, string category, string subcategory, string id, string vid, Vendormaster vendormaster, VendorVenue vendorVenue)
        {

            string strReq = "";
            encptdecpt encript = new encptdecpt();
            strReq = encript.Decrypt(id);
            //Parse the value... this is done is very raw format.. you can add loops or so to get the values out of the query string...
            string[] arrMsgs = strReq.Split('&');
            string[] arrIndMsg;
            string id1 = "";
            arrIndMsg = arrMsgs[0].Split('='); //Get the id
            id1 = arrIndMsg[1].ToString().Trim();
            vendormaster.ServicType = category;
            if (command == "one")
            {
                vendormaster = vendorMasterService.UpdateVendorStorefront(vendormaster, long.Parse(id1)); //updating Vendor Master
                return Json("Basic Details Updated");
            }
            else if (command == "two")
            {
                UpdateAmenities(category, subcategory, vendorVenue.Distancefrommainplaceslike, id1, vid);
                return Json("Amenities Updated");
            }
            else if (command == "three")
            {
                if (category == "Venue")
                {
                    var venuedata = vendorVenue;
                    vendorVenue = vendorVenueSignUpService.GetParticularVendorVenue(long.Parse(id1), long.Parse(vid)); // Retrieving Particular Vendor Record
                    vendorVenue.Description = venuedata.Description;
                    vendorVenue.Dimentions = venuedata.Dimentions;
                    vendorVenue.Minimumseatingcapacity = venuedata.Minimumseatingcapacity;
                    vendorVenue.Maximumcapacity = venuedata.Maximumcapacity;
                    vendorVenue.name = venuedata.name;
                    vendorVenue = vendorVenueSignUpService.UpdateVenue(vendorVenue, vendormaster, long.Parse(id1), long.Parse(vid));
                    return Json("Hall Details Updated");
                }
            }
            else if (command == "four")
            {
                var venuedata = vendorVenue;
                if (category == "Venue")
                {
                    vendorVenue = vendorVenueSignUpService.GetParticularVendorVenue(long.Parse(id1), long.Parse(vid)); // Retrieving Particular Vendor Record
                    vendorVenue.Address = venuedata.Address;
                    vendorVenue.City = venuedata.City;
                    vendorVenue.State = venuedata.State;
                    vendorVenue.Landmark = venuedata.Landmark;
                    vendorVenue.ZipCode = venuedata.ZipCode;
                    vendorVenue.GeoLocation = venuedata.GeoLocation;
                    vendorVenue = vendorVenueSignUpService.UpdateVenue(vendorVenue, vendormaster, long.Parse(id1), long.Parse(vid));
                }
                else if (category == "Catering")
                {
                    VendorCateringService vendorCateringService = new VendorCateringService();
                    VendorsCatering vendorsCatering = vendorVenueSignUpService.GetParticularVendorCatering(long.Parse(id1), long.Parse(vid));
                    vendorsCatering.Address = vendorVenue.Address;
                    vendorsCatering.City = vendorVenue.City;
                    vendorsCatering.State = vendorVenue.State;
                    vendorsCatering.Landmark = vendorVenue.Landmark;
                    vendorsCatering.ZipCode = vendorVenue.ZipCode;
                    vendorsCatering = vendorVenueSignUpService.UpdateCatering(vendorsCatering, vendormaster, long.Parse(id1), long.Parse(vid));
                }
                else if (category == "Photography")
                {
                    VendorsPhotography vendorsPhotography = vendorVenueSignUpService.GetParticularVendorPhotography(long.Parse(id1), long.Parse(vid));
                    vendorsPhotography.Address = vendorVenue.Address;
                    vendorsPhotography.City = vendorVenue.City;
                    vendorsPhotography.State = vendorVenue.State;
                    vendorsPhotography.Landmark = vendorVenue.Landmark;
                    vendorsPhotography.ZipCode = vendorVenue.ZipCode;
                    vendorsPhotography = vendorVenueSignUpService.UpdatePhotography(vendorsPhotography, vendormaster, long.Parse(id1), long.Parse(vid));
                }
                else if (category == "Decorator")
                {
                    VendorsDecorator vendorsDecorator = vendorVenueSignUpService.GetParticularVendorDecorator(long.Parse(id1), long.Parse(vid));
                    vendorsDecorator.Address = vendorVenue.Address;
                    vendorsDecorator.City = vendorVenue.City;
                    vendorsDecorator.State = vendorVenue.State;
                    vendorsDecorator.Landmark = vendorVenue.Landmark;
                    vendorsDecorator.ZipCode = vendorVenue.ZipCode;
                    vendorsDecorator = vendorVenueSignUpService.UpdateDecorator(vendorsDecorator, vendormaster, long.Parse(id1), long.Parse(vid));
                }
                else if (category == "Other")
                {
                    VendorsOther vendorsOther = vendorVenueSignUpService.GetParticularVendorOther(long.Parse(id1), long.Parse(vid));
                    vendorsOther.Address = vendorVenue.Address;
                    vendorsOther.City = vendorVenue.City;
                    vendorsOther.State = vendorVenue.State;
                    vendorsOther.Landmark = vendorVenue.Landmark;
                    vendorsOther.ZipCode = vendorVenue.ZipCode;
                    vendorsOther = vendorVenueSignUpService.UpdateOther(vendorsOther, vendormaster, long.Parse(id1), long.Parse(vid));
                }
                return Json("Address Updated");
            }
            else if (command == "six")
            {
                vendormaster = vendorMasterService.UpdateVendorStorefront(vendormaster, long.Parse(id1)); //updating Vendor Master
                return Json("Your Address Updated");
            }
            else if (command == "seven")
            {
                var venuedata = vendorVenue;
                if (category == "Venue")
                {
                    vendorVenue = vendorVenueSignUpService.GetParticularVendorVenue(long.Parse(id1), long.Parse(vid)); // Retrieving Particular Vendor Record
                    vendorVenue.ServiceCost = venuedata.ServiceCost;
                    vendorVenue.VegLunchCost = venuedata.VegLunchCost;
                    vendorVenue.NonVegLunchCost = venuedata.NonVegLunchCost;
                    vendorVenue.VegDinnerCost = venuedata.VegDinnerCost;
                    vendorVenue.NonVegDinnerCost = venuedata.NonVegDinnerCost;
                    vendorVenue.MinOrder = venuedata.MinOrder;
                    vendorVenue.MaxOrder = venuedata.MaxOrder;
                    vendorVenue = vendorVenueSignUpService.UpdateVenue(vendorVenue, vendormaster, long.Parse(id1), long.Parse(vid));
                }
                else if (category == "Catering")
                {
                    VendorCateringService vendorCateringService = new VendorCateringService();
                    VendorsCatering vendorsCatering = vendorVenueSignUpService.GetParticularVendorCatering(long.Parse(id1), long.Parse(vid));
                    vendorsCatering.Veg = vendorVenue.VegLunchCost;
                    vendorsCatering.NonVeg = vendorVenue.NonVegLunchCost;
                    vendorsCatering.MinOrder = vendorVenue.MinOrder;
                    vendorsCatering.MaxOrder = vendorVenue.MaxOrder;
                    //vendorsCatering.ZipCode = vendorVenue.ZipCode;
                    vendorsCatering = vendorVenueSignUpService.UpdateCatering(vendorsCatering, vendormaster, long.Parse(id1), long.Parse(vid));
                }
                else if (category == "Photography")
                {
                    VendorsPhotography vendorsPhotography = vendorVenueSignUpService.GetParticularVendorPhotography(long.Parse(id1), long.Parse(vid));
                    vendorsPhotography.StartingPrice = vendorVenue.ServiceCost;
                    vendorsPhotography = vendorVenueSignUpService.UpdatePhotography(vendorsPhotography, vendormaster, long.Parse(id1), long.Parse(vid));
                }
                else if (category == "Decorator")
                {
                    VendorsDecorator vendorsDecorator = vendorVenueSignUpService.GetParticularVendorDecorator(long.Parse(id1), long.Parse(vid));
                    vendorsDecorator.StartingPrice = vendorVenue.ServiceCost;
                    vendorsDecorator = vendorVenueSignUpService.UpdateDecorator(vendorsDecorator, vendormaster, long.Parse(id1), long.Parse(vid));
                }
                else if (category == "Other")
                {
                    VendorsOther vendorsOther = vendorVenueSignUpService.GetParticularVendorOther(long.Parse(id1), long.Parse(vid));
                    if(vendorVenue.ServiceCost != 0)
                    vendorsOther.ItemCost = vendorVenue.ServiceCost;
                    vendorsOther.MinOrder = vendorVenue.MinOrder;
                    vendorsOther.MaxOrder = vendorVenue.MaxOrder;
                    vendorsOther = vendorVenueSignUpService.UpdateOther(vendorsOther, vendormaster, long.Parse(id1), long.Parse(vid));
                }
                return Json("Price Updated");
            }
            return Json(JsonRequestBehavior.AllowGet);
        }

        public long addservice(string category, string subcategory, long id)
        {
            long count = 0;
            if (category == "Venue")
            {
                VendorVenue vendorVenue = new VendorVenue();
                vendorVenue.VendorMasterId = id;
                vendorVenue.VenueType = subcategory;
                vendorVenue = vendorVenueSignUpService.AddVendorVenue(vendorVenue);
                if (vendorVenue.Id != 0) count = vendorVenue.Id;
            }
            else if (category == "Catering")
            {
                VendorsCatering vendorsCatering = new VendorsCatering();
                vendorsCatering.VendorMasterId = id;
                vendorsCatering.CuisineType = subcategory;
                vendorsCatering = vendorVenueSignUpService.AddVendorCatering(vendorsCatering);
                if (vendorsCatering.Id != 0) count = vendorsCatering.Id;
            }
            else if (category == "Photography")
            {
                VendorsPhotography vendorsPhotography = new VendorsPhotography();
                vendorsPhotography.VendorMasterId = id;
                vendorsPhotography.PhotographyType = subcategory;
                vendorsPhotography = vendorVenueSignUpService.AddVendorPhotography(vendorsPhotography);
                if (vendorsPhotography.Id != 0) count = vendorsPhotography.Id;
            }
            else if (category == "Decorator")
            {
                VendorsDecorator vendorsDecorator = new VendorsDecorator();
                vendorsDecorator.VendorMasterId = id;
                vendorsDecorator.DecorationType = subcategory;
                vendorsDecorator = vendorVenueSignUpService.AddVendorDecorator(vendorsDecorator);
                if (vendorsDecorator.Id != 0) count = vendorsDecorator.Id;
            }
            //if (category == "EventManagement")
            //{
            //    VendorsEventOrganiser vendorsEventOrganiser = new VendorsEventOrganiser();
            //    vendorsEventOrganiser.VendorMasterId = id;
            //    vendorsEventOrganiser = vendorVenueSignUpService.AddVendorEventOrganiser(vendorsEventOrganiser);
            //    if (vendorsEventOrganiser.Id != 0) count = vendorsEventOrganiser.Id;
            //}
            else if (category == "Other")
            {
                VendorsOther vendorsOther = new VendorsOther();
                vendorsOther.VendorMasterId = id;
                vendorsOther.MinOrder = "0";
                vendorsOther.MaxOrder = "0";
                vendorsOther.Status = "InActive";
                vendorsOther.UpdatedBy = 2;
                vendorsOther.UpdatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);//Convert.ToDateTime(DateTime.UtcNow.ToShortDateString());
                vendorsOther.type = subcategory;
                vendorsOther = vendorVenueSignUpService.AddVendorOther(vendorsOther);
                if (vendorsOther.Id != 0) count = vendorsOther.Id;
            }
            return count;
        }

        public long updateservice(string category, string subcategory, long id, long vid)
        {
            Vendormaster vendormaster = new Vendormaster();
            long count = 0;
            if (category == "Venue")
            {
                VendorVenue vendorVenue = new VendorVenue();
                vendorVenue.VendorMasterId = id;
                vendorVenue.VenueType = subcategory;
                vendorVenue = vendorVenueSignUpService.UpdateVenue(vendorVenue, vendormaster, id, vid);
                if (vendorVenue.Id != 0) count = vendorVenue.Id;
            }
            else if (category == "Catering")
            {
                VendorsCatering vendorsCatering = new VendorsCatering();
                vendorsCatering.VendorMasterId = id;
                vendorsCatering.CuisineType = subcategory;
                vendorsCatering = vendorVenueSignUpService.UpdateCatering(vendorsCatering, vendormaster, id, vid);
                if (vendorsCatering.Id != 0) count = vendorsCatering.Id;
            }
            else if (category == "Photography")
            {
                VendorsPhotography vendorsPhotography = new VendorsPhotography();
                vendorsPhotography.VendorMasterId = id;
                vendorsPhotography.PhotographyType = subcategory;
                vendorsPhotography = vendorVenueSignUpService.UpdatePhotography(vendorsPhotography, vendormaster, id, vid);
                if (vendorsPhotography.Id != 0) count = vendorsPhotography.Id;
            }
            else if (category == "Decorator")
            {
                VendorsDecorator vendorsDecorator = new VendorsDecorator();
                vendorsDecorator.VendorMasterId = id;
                vendorsDecorator.DecorationType = subcategory;
                vendorsDecorator = vendorVenueSignUpService.UpdateDecorator(vendorsDecorator, vendormaster, id, vid);
                if (vendorsDecorator.Id != 0) count = vendorsDecorator.Id;
            }
            //if (category == "EventManagement")
            //{
            //    VendorsEventOrganiser vendorsEventOrganiser = new VendorsEventOrganiser();
            //    vendorsEventOrganiser.VendorMasterId = id;
            //    vendorsEventOrganiser = vendorVenueSignUpService.AddVendorEventOrganiser(vendorsEventOrganiser);
            //    if (vendorsEventOrganiser.Id != 0) count = vendorsEventOrganiser.Id;
            //}
            else if (category == "Other")
            {
                VendorsOther vendorsOther = new VendorsOther();
                vendorsOther.VendorMasterId = id;
                vendorsOther.MinOrder = "0";
                vendorsOther.MaxOrder = "0";
                vendorsOther.Status = "InActive";
                vendorsOther.UpdatedBy = 2;
                vendorsOther.UpdatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);//Convert.ToDateTime(DateTime.UtcNow.ToShortDateString());
                vendorsOther.type = subcategory;
                vendorsOther = vendorVenueSignUpService.UpdateOther(vendorsOther, vendormaster, id, vid);
                if (vendorsOther.Id != 0) count = vendorsOther.Id;
            }
            return count;
        }

        [HttpPost]
        public JsonResult UploadImages(HttpPostedFileBase file, string ks, string vid, string type)
        {
            VendorImage vendorImage = new VendorImage();
            Vendormaster vendorMaster = new Vendormaster();
            string strReq = "";
            encptdecpt encript = new encptdecpt();
            strReq = encript.Decrypt(ks);
            //Parse the value... this is done is very raw format.. you can add loops or so to get the values out of the query string...
            string[] arrMsgs = strReq.Split('&');
            string[] arrIndMsg;
            string id = "";
            arrIndMsg = arrMsgs[0].Split('='); //Get the id
            id = arrIndMsg[1].ToString().Trim();
            ViewBag.id = ks;
            ViewBag.vid = vid;
            vendorMaster.Id = long.Parse(id);
            vendorImage.VendorId = long.Parse(vid);
            string fileName = string.Empty;
            if (file != null)
            {
                string path = System.IO.Path.GetExtension(file.FileName);
                //if (path.ToLower() != ".jpg" && path.ToLower() != ".jpeg" && path.ToLower() != ".png")
                //    return Json("File");
                int imageno = 0;
                int imagecount = 8;
                var list = vendorImageService.GetImages(long.Parse(id), long.Parse(vid));
                if (list.Count <= imagecount && Request.Files.Count <= imagecount - list.Count)
                {
                    //getting max imageno
                    for (int i = 0; i < list.Count; i++)
                    {
                        string x = list[i].ImageName.ToString();
                        string[] y = x.Split('_', '.');
                        if (y[3] == "jpg")
                        {
                            imageno = int.Parse(y[2]);
                        }
                        else
                        {
                            imageno = int.Parse(y[3]);
                        }
                    }

                    //Uploading images in db & folder
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        int j = imageno + i + 1;
                        var file1 = Request.Files[i];
                        if (file1 != null && file1.ContentLength > 0)
                        {
                            var filename = type + "_" + id + "_" + vid + "_" + j + path;
                            fileName = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath(imagepath + filename));
                            file1.SaveAs(fileName);
                            vendorImage.ImageName = filename;
                            vendorImage = vendorImageService.AddVendorImage(vendorImage, vendorMaster);
                        }
                    }
                }
            }
            return Json("success");
        }

        public ActionResult Removeimage(string src, string id, string vid, string type)
        {
            try { 
            string delete = "";
            var vendorImage = vendorImageService.GetImageId(src, long.Parse(vid));
            delete = vendorImageService.DeleteImage(vendorImage);
            if (delete == "success")
            {
                string fileName = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath(imagepath + src));
                System.IO.File.Delete(fileName);
                return Json("success");
            }
            else
            {
                return Json("Failed");
            }
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Nhomepage");
            }
        }

        public JsonResult UpdateImageInfo(string ks, string vid, string description)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                VendorImage vendorImage = new VendorImage();
                string fileName = string.Empty;
                string imgdesc = description;
                Vendormaster vendorMaster = new Vendormaster();
                string strReq = "";
                encptdecpt encript = new encptdecpt();
                strReq = encript.Decrypt(ks);
                //Parse the value... this is done is very raw format.. you can add loops or so to get the values out of the query string...
                string[] arrMsgs = strReq.Split('&');
                string[] arrIndMsg;
                string id = "";
                arrIndMsg = arrMsgs[0].Split('='); //Get the id
                id = arrIndMsg[1].ToString().Trim();
                ViewBag.id = ks;
                vendorMaster.Id = long.Parse(id);
                vendorImage.VendorId = long.Parse(vid);
                string status = "";
                var images = vendorImageService.GetImages(long.Parse(id), long.Parse(vid));
                if (images.Count != 0)
                {
                    //Updating images info
                    for (int i = 0; i < images.Count; i++)
                    {
                        vendorImage.ImageType = images[i].ImageType;
                        vendorImage.Imagedescription = imgdesc.Split(',')[i];
                        vendorImage.ImageName = images[i].ImageName;
                        vendorImage.ImageId = images[i].ImageId;
                        vendorImage.VendorId = images[i].VendorId;
                        vendorImage.VendorMasterId = images[i].VendorMasterId;
                        vendorImage.UpdatedBy = images[i].UpdatedBy;
                        vendorImage.UpdatedDate = images[i].UpdatedDate;
                        vendorImage.Status = images[i].Status;
                        vendorImage.ImageLimit = "6";
                        status = vendorImageService.UpdateVendorImage(vendorImage, long.Parse(id), long.Parse(vid));
                    }
                }
                return Json(JsonRequestBehavior.AllowGet);
            }
            return Json(JsonRequestBehavior.AllowGet);
        }

        public JsonResult UploadProfilePic(HttpPostedFileBase helpSectionImages, string email)
        {
            string fileName = string.Empty;
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrincipal)System.Web.HttpContext.Current.User;
                UserLoginDetailsService userLoginDetailsService = new UserLoginDetailsService();
                string path = System.IO.Path.GetExtension(helpSectionImages.FileName);
                var filename = email + path;
                fileName = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"/ProfilePictures/" + filename));
                helpSectionImages.SaveAs(fileName);
                userLoginDetailsService.ChangeDP(int.Parse(user.UserId.ToString()), filename);
            }
            return Json(JsonRequestBehavior.AllowGet);
        }

        public void UpdateAmenities(string category, string subcategory, string selectedamenities,string id,string vid)
        {
            //long count = 0;
            Vendormaster vendormaster = new Vendormaster();
            vendormaster.ServicType = category;
            string[] selectedamenitieslist = selectedamenities.Split(',');
            if (category == "Venue")
            {
                VendorVenue vendorVenue = vendorVenueSignUpService.GetParticularVendorVenue(long.Parse(id), long.Parse(vid)); // Retrieving Particular Vendor Record
                if (selectedamenitieslist.Contains("CockTails")) vendorVenue.CockTails = "Yes"; else vendorVenue.CockTails = "No";
                if (selectedamenitieslist.Contains("Rooms")) vendorVenue.Rooms = "Yes"; else vendorVenue.Rooms = "No";
                if (selectedamenitieslist.Contains("Wifi")) vendorVenue.Wifi = "Yes"; else vendorVenue.Wifi = "No";
                if (selectedamenitieslist.Contains("Live Cooking Station")) vendorVenue.LiveCookingStation = "Yes"; else vendorVenue.LiveCookingStation = "No";
                if (selectedamenitieslist.Contains("Decoration Allowed")) vendorVenue.DecorationAllowed = "Yes"; else vendorVenue.DecorationAllowed = "No";
                if (selectedamenitieslist.Contains("Sufficient Washroom")) vendorVenue.Sufficient_Washroom = "Yes"; else vendorVenue.Sufficient_Washroom = "No";
                if (selectedamenitieslist.Contains("Sufficient Room Size")) vendorVenue.Sufficient_Room_Size = "Yes"; else vendorVenue.Sufficient_Room_Size = "No";
                if (selectedamenitieslist.Contains("Intercom")) vendorVenue.Intercom = "Yes"; else vendorVenue.Intercom = "No";
                if (selectedamenitieslist.Contains("Single Bed")) vendorVenue.Single_Bed = "Yes"; else vendorVenue.Single_Bed = "No";
                if (selectedamenitieslist.Contains("Queen Bed")) vendorVenue.Queen_Bed = "Yes"; else vendorVenue.Queen_Bed = "No";
                if (selectedamenitieslist.Contains("King Bed")) vendorVenue.King_Bed = "Yes"; else vendorVenue.King_Bed = "No";
                if (selectedamenitieslist.Contains("Balcony")) vendorVenue.Balcony = "Yes"; else vendorVenue.Balcony = "No";
                if (selectedamenitieslist.Contains("Full Length Mirrror")) vendorVenue.Full_Length_Mirrror = "Yes"; else vendorVenue.Full_Length_Mirrror = "No";
                if (selectedamenitieslist.Contains("Jacuzzi")) vendorVenue.Jacuzzi = "Yes"; else vendorVenue.Jacuzzi = "No";
                if (selectedamenitieslist.Contains("Sofa Set")) vendorVenue.Sofa_Set = "Yes"; else vendorVenue.Sofa_Set = "No";
                if (selectedamenitieslist.Contains("Coffee Tea Maker")) vendorVenue.Coffee_Tea_Maker = "Yes"; else vendorVenue.Coffee_Tea_Maker = "No";
                if (selectedamenitieslist.Contains("Kindle")) vendorVenue.Kindle = "Yes"; else vendorVenue.Kindle = "No";
                if (selectedamenitieslist.Contains("Netflix")) vendorVenue.Netflix = "Yes"; else vendorVenue.Netflix = "No";
                if (selectedamenitieslist.Contains("Kitchen")) vendorVenue.Kitchen = "Yes"; else vendorVenue.Kitchen = "No";
                if (selectedamenitieslist.Contains("Bath Tub")) vendorVenue.Bath_Tub = "Yes"; else vendorVenue.Bath_Tub = "No";
                if (selectedamenitieslist.Contains("Electricity")) vendorVenue.Electricity = "Yes"; else vendorVenue.AC = "No";
                if (selectedamenitieslist.Contains("Wellness Center")) vendorVenue.Wellness_Center = "Yes"; else vendorVenue.Wellness_Center = "No";
                if (selectedamenitieslist.Contains("Spa")) vendorVenue.Spa = "Yes"; else vendorVenue.Spa = "No";
                if (selectedamenitieslist.Contains("HDTV")) vendorVenue.HDTV = "Yes"; else vendorVenue.HDTV = "No";
                if (selectedamenitieslist.Contains("Pet Friendly")) vendorVenue.Pet_Friendly = "Yes"; else vendorVenue.Pet_Friendly = "No";
                if (selectedamenitieslist.Contains("Gym")) vendorVenue.Gym = "Yes"; else vendorVenue.Gym = "No";
                if (selectedamenitieslist.Contains("In-house Restaurant")) vendorVenue.In_house_Restaurant = "Yes"; else vendorVenue.In_house_Restaurant = "No";
                if (selectedamenitieslist.Contains("Hair Dryer")) vendorVenue.Hair_Dryer= "Yes"; else vendorVenue.Hair_Dryer = "No";
                if (selectedamenitieslist.Contains("Mini Fridge")) vendorVenue.Mini_Fridge = "Yes"; else vendorVenue.Mini_Fridge = "No";
                if (selectedamenitieslist.Contains("In-Room Safe")) vendorVenue.In_Room_Safe = "Yes"; else vendorVenue.In_Room_Safe = "No";
                if (selectedamenitieslist.Contains("Room Heater")) vendorVenue.Room_Heater = "Yes"; else vendorVenue.Room_Heater = "No";
                if (selectedamenitieslist.Contains("Wheelchair Accessible")) vendorVenue.Wheelchair_Accessible = "Yes"; else vendorVenue.Wheelchair_Accessible = "No";
                if (selectedamenitieslist.Contains("Power Backup")) vendorVenue.Power_Backup = "Yes"; else vendorVenue.Power_Backup = "No";
                if (selectedamenitieslist.Contains("Dining Area")) vendorVenue.Dining_Area = "Yes"; else vendorVenue.Dining_Area = "No";
                if (selectedamenitieslist.Contains("Bar")) vendorVenue.Bar = "Yes"; else vendorVenue.Bar = "No";
                if (selectedamenitieslist.Contains("Conference Room")) vendorVenue.Conference_Room = "Yes"; else vendorVenue.Conference_Room = "No";
                if (selectedamenitieslist.Contains("Swimming Pool")) vendorVenue.Swimming_Pool = "Yes"; else vendorVenue.AC = "No";
                if (selectedamenitieslist.Contains("CCTV Cameras")) vendorVenue.CCTV_Cameras = "Yes"; else vendorVenue.CCTV_Cameras = "No";
                if (selectedamenitieslist.Contains("Laundry")) vendorVenue.Laundry = "Yes"; else vendorVenue.Laundry = "No";
                if (selectedamenitieslist.Contains("Banquet Hall")) vendorVenue.Banquet_Hall = "Yes"; else vendorVenue.Banquet_Hall = "No";
                if (selectedamenitieslist.Contains("Lift/Elevator")) vendorVenue.Lift_or_Elevator = "Yes"; else vendorVenue.Lift_or_Elevator = "No";
                if (selectedamenitieslist.Contains("Card Payment")) vendorVenue.Card_Payment = "Yes"; else vendorVenue.Card_Payment = "No";
                if (selectedamenitieslist.Contains("Parking Facility")) vendorVenue.Parking_Facility = "Yes"; else vendorVenue.Parking_Facility = "No";
                if (selectedamenitieslist.Contains("Geyser")) vendorVenue.Geyser = "Yes"; else vendorVenue.AC = "No";
                if (selectedamenitieslist.Contains("Complimentary Breakfast")) vendorVenue.Complimentary_Breakfast = "Yes"; else vendorVenue.Complimentary_Breakfast = "No";
                if (selectedamenitieslist.Contains("TV")) vendorVenue.TV = "Yes"; else vendorVenue.TV = "No";
                if (selectedamenitieslist.Contains("AC")) vendorVenue.AC = "Yes"; else vendorVenue.AC = "No";

                vendorVenue = vendorVenueSignUpService.UpdateVenue(vendorVenue, vendormaster, long.Parse(id), long.Parse(vid));
                //if (vendorVenue.Id != 0) count = vendorVenue.Id;
            }
            else if (category == "Catering")
            {
                VendorsCatering vendorsCatering = vendorVenueSignUpService.GetParticularVendorCatering(long.Parse(id), long.Parse(vid));
                if (selectedamenitieslist.Contains("Mineral Water Included")) vendorsCatering.MineralWaterIncluded = "Yes";
                if (selectedamenitieslist.Contains("Transport Included")) vendorsCatering.TransportIncluded = "Yes";
                if (selectedamenitieslist.Contains("Live Cooking Station")) vendorsCatering.LiveCookingStation = "Yes";
                vendorsCatering = vendorVenueSignUpService.UpdateCatering(vendorsCatering, vendormaster, long.Parse(id), long.Parse(vid));
                //if (vendorsCatering.Id != 0) count = vendorsCatering.Id;
            }
            else if (category == "Photography")
            {
                VendorsPhotography vendorsPhotography = vendorVenueSignUpService.GetParticularVendorPhotography(long.Parse(id), long.Parse(vid));
                if (selectedamenitieslist.Contains("Pre Wedding Shoot")) vendorsPhotography.PreWeddingShoot = "Yes";
                if (selectedamenitieslist.Contains("Destination Photography")) vendorsPhotography.DestinationPhotography = "Yes";
                vendorsPhotography = vendorVenueSignUpService.UpdatePhotography(vendorsPhotography, vendormaster, long.Parse(id), long.Parse(vid));
                //if (vendorsPhotography.Id != 0) count = vendorsPhotography.Id;
            }
            else if (category == "Decorator")
            {
                VendorsDecorator vendorsDecorator = vendorVenueSignUpService.GetParticularVendorDecorator(long.Parse(id), long.Parse(vid));
                if (selectedamenitieslist.Contains("Archway")) vendorsDecorator.Archway = "Yes";
                if (selectedamenitieslist.Contains("Altar arrangements")) vendorsDecorator.Altararrangements = "Yes";
                if (selectedamenitieslist.Contains("Pew bows")) vendorsDecorator.Pewbows = "Yes";
                if (selectedamenitieslist.Contains("Aisle runner")) vendorsDecorator.Aislerunner = "Yes";
                if (selectedamenitieslist.Contains("Head pieces")) vendorsDecorator.Headpieces = "Yes";
                if (selectedamenitieslist.Contains("Center pieces")) vendorsDecorator.Centerpieces = "Yes";
                if (selectedamenitieslist.Contains("Chair covers")) vendorsDecorator.Chaircovers = "Yes";
                if (selectedamenitieslist.Contains("Head table decor")) vendorsDecorator.Headtabledecor = "Yes";
                if (selectedamenitieslist.Contains("Backdrops")) vendorsDecorator.Backdrops = "Yes";
                if (selectedamenitieslist.Contains("Ceiling canopies")) vendorsDecorator.Ceilingcanopies = "Yes";
                if (selectedamenitieslist.Contains("Mandaps")) vendorsDecorator.Mandaps = "Yes";
                if (selectedamenitieslist.Contains("Mehendi")) vendorsDecorator.Mehendi = "Yes";
                if (selectedamenitieslist.Contains("Sangeet")) vendorsDecorator.Sangeet = "Yes";
                if (selectedamenitieslist.Contains("Chuppas")) vendorsDecorator.Chuppas = "Yes";
                if (selectedamenitieslist.Contains("Lighting")) vendorsDecorator.Lighting = "Yes";
                if (selectedamenitieslist.Contains("Gifts for guests")) vendorsDecorator.Giftsforguests = "Yes";
                if (selectedamenitieslist.Contains("Gift table")) vendorsDecorator.Gifttable = "Yes";
                if (selectedamenitieslist.Contains("Basket or Box for gifts")) vendorsDecorator.BasketorBoxforgifts = "Yes";
                if (selectedamenitieslist.Contains("Place or seating cards")) vendorsDecorator.Placeorseatingcards = "Yes";
                if (selectedamenitieslist.Contains("Car decoration")) vendorsDecorator.Cardecoration = "Yes";
                if (selectedamenitieslist.Contains("Brides bouquet")) vendorsDecorator.Bridesbouquet = "Yes";
                if (selectedamenitieslist.Contains("Bridesmaids bouquets")) vendorsDecorator.Bridesmaidsbouquets = "Yes";
                if (selectedamenitieslist.Contains("Maid of honor bouquet")) vendorsDecorator.Maidofhonorbouquet = "Yes";
                if (selectedamenitieslist.Contains("Throw away bouquet")) vendorsDecorator.Throwawaybouquet = "Yes";
                if (selectedamenitieslist.Contains("Corsages")) vendorsDecorator.Corsages = "Yes";
                if (selectedamenitieslist.Contains("Boutonnieres (for groom, fathers, grandfathers, best man, groom’s men)")) vendorsDecorator.Boutonnieres = "Yes";
                if (selectedamenitieslist.Contains("Decora")) vendorsDecorator.Decora = "Yes";
                if (selectedamenitieslist.Contains("Just married clings")) vendorsDecorator.Justmarriedclings = "Yes";

                vendorsDecorator = vendorVenueSignUpService.UpdateDecorator(vendorsDecorator, vendormaster, long.Parse(id), long.Parse(vid));
                //if (vendorsDecorator.Id != 0) count = vendorsDecorator.Id;
            }
            //return count;
        }
    }
}