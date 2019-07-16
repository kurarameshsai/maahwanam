﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MaaAahwanam.Service;
using MaaAahwanam.Models;

namespace MaaAahwanam.Web.Controllers
{
    public class VendorSignUp2Controller : Controller
    {
        const string imagepath = @"/vendorimages/";
        VendorImageService vendorImageService = new VendorImageService();
        // GET: VendorSignUp2
        public ActionResult Index(string id, string vid)
        {
            ViewBag.id = id;
            ViewBag.vid = vid;
            ViewBag.images = vendorImageService.GetImages(long.Parse(id), long.Parse(vid));
            return View();
        }

        [HttpPost]
        public ActionResult Index(string id, string vid, HttpPostedFileBase file, string removedimages, string type, VendorImage vendorImage, string command)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                string fileName = string.Empty;
                string imagetype = vendorImage.ImageType;
                string imgdesc = vendorImage.Imagedescription;
                Vendormaster vendorMaster = new Vendormaster();
                vendorMaster.Id = long.Parse(id);
                vendorImage.VendorId = long.Parse(vid);
                if (command == "update")
                {
                    string status = "";
                    var images = vendorImageService.GetImages(long.Parse(id), long.Parse(vid));
                    if (images.Count != 0)
                    {
                        //Updating images info
                        for (int i = 0; i < images.Count; i++)
                        {
                            vendorImage.ImageType = imagetype.Split(',')[i];
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
                        if (status == "updated")
                            return Content("<script language='javascript' type='text/javascript'>alert('Photo info Updated');location.href='/AvailableServices/Index?id=" + id + "&&vid=" + vid + "'</script>");
                        else
                            return Content("<script language='javascript' type='text/javascript'>alert('Failed !!!');location.href='/AvailableServices/Index?id=" + id + "&&vid=" + vid + "'</script>");
                    }
                    else
                        return Content("<script language='javascript' type='text/javascript'>alert('Upload Image');location.href='/VendorSignUp2/Index?id=" + id + "&&vid=" + vid + "&&type=" + type + "'</script>");
                }
            }
            return RedirectToAction("Index", "HomePage");
        }

        [HttpPost]
        public JsonResult UploadImages(HttpPostedFileBase file, string id, string vid, string type)
        {
            VendorImage vendorImage = new VendorImage();
            Vendormaster vendorMaster = new Vendormaster();
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

        //public PartialViewResult UploadImage(string id, string vid)
        //{
        //    ViewBag.images = vendorImageService.GetImages(long.Parse(id), long.Parse(vid));
        //    return PartialView("UploadImage", ViewBag.images);
        //}

        public ActionResult RemoveAllImages(string id, string vid,string type)
        {
            string delete = vendorImageService.DeleteAllImages(long.Parse(id), long.Parse(vid));
            return Content("<script language='javascript' type='text/javascript'>alert('Removed All Images');location.href='/VendorSignUp2/Index?id=" + id + "&&vid=" + vid + "&&type=" + type + "'</script>");
        }


        //[HttpPost]
        //public ActionResult updaterecord(string id, string vid, HttpPostedFileBase file, string removedimages, string type, VendorImage vendorImage)
        //{
        //    if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
        //    {
        //        string fileName = string.Empty;
        //        string imagetype = vendorImage.ImageType;
        //        string imgdesc = vendorImage.Imagedescription;
        //        //VendorImage vendorImage = new VendorImage();
        //        Vendormaster vendorMaster = new Vendormaster();
        //        vendorMaster.Id = long.Parse(id);
        //        vendorImage.VendorId = long.Parse(vid);
        //        string status = "";
        //        var images = vendorImageService.GetImages(long.Parse(id), long.Parse(vid));
        //        if (images.Count != 0)
        //        {
        //            //Updating images info
        //            for (int i = 0; i < images.Count; i++)
        //            {
        //                vendorImage.ImageType = imagetype.Split(',')[i];
        //                vendorImage.Imagedescription = imgdesc.Split(',')[i];
        //                vendorImage.ImageLimit = "6";
        //                status = vendorImageService.UpdateVendorVenue(vendorImage);
        //            }
        //            if (status == "updated")
        //                return Content("<script language='javascript' type='text/javascript'>alert('Photo info Updated');location.href='/AvailableServices/Index?id=" + id + "&&vid=" + vid + "'</script>");
        //            else
        //                return Content("<script language='javascript' type='text/javascript'>alert('Failed !!!');location.href='/AvailableServices/Index?id=" + id + "&&vid=" + vid + "'</script>");
        //        }
        //        else
        //            return Content("<script language='javascript' type='text/javascript'>alert('Upload Image');location.href='/VendorSignUp2/Index?id=" + id + "&&vid=" + vid + "&&type=" + type + "'</script>");
        //    }
        //    return RedirectToAction("Index", "HomePage");
        //}

        public ActionResult Removeimage(string src, string id, string vid, string type)
        {
            string delete = "";
            var vendorImage = vendorImageService.GetImageId(src, long.Parse(vid));
            delete = vendorImageService.DeleteImage(vendorImage);
            if (delete == "success")
            {
                string fileName = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath(imagepath + src));
                System.IO.File.Delete(fileName);
                //return Content("<script language='javascript' type='text/javascript'>alert('Image deleted successfully!');location.href='" + @Url.Action("Index", "VendorSignUp2", new { id = id, vid = vid, type = type }) + "'</script>");
                //return Content("<script language='javascript' type='text/javascript'>alert('Image deleted successfully!');location.href='/VendorSignUp2/Index?id=" + id + "&&vid=" + vid + "&&type=" + type + "'</script>");
                return Json("success");
            }
            else
            {
                //return Content("<script language='javascript' type='text/javascript'>alert('Failed!');location.href='" + @Url.Action("Index", "VendorSignUp2", new { id = id, vid = vid, type = type }) + "'</script>");
                //return Content("<script language='javascript' type='text/javascript'>alert('Failed!');location.href='/VendorSignUp2/Index?id=" + id + "&&vid=" + vid + "&&type=" + type + "'</script>");
                return Json("Failed");
            }
        }


    }
}