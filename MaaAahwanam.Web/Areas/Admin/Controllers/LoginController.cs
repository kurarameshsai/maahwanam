﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MaaAahwanam.Models;
using MaaAahwanam.Utility;
using MaaAahwanam.Service;
using System.Configuration;
using System.Web.Security;
using Newtonsoft.Json;
using MaaAahwanam.Web.Custom;

namespace MaaAahwanam.Web.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        UserLoginDetailsService userLoginDetailsService = new UserLoginDetailsService();
        public ActionResult Index()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrincipal)System.Web.HttpContext.Current.User;
                var userResponse = userLoginDetailsService.GetUser((int)user.UserId);
                if (user.UserType == "Admin")
                {
                    return RedirectToAction("dashboard", "dashboard", new { id = userResponse.UserLoginId });
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index([Bind(Prefix = "Item1")] UserLogin userLogin, [Bind(Prefix = "Item2")] UserDetail userDetails, string command)
        {
            if (command == "Register")
            {
                
                userLogin.UserType = "Admin";
                var response = userLoginDetailsService.AddUserDetails(userLogin, userDetails);
                if (response == "sucess")
                {
                    return Content("<script language='javascript' type='text/javascript'>alert('Registered Successfully');location.href='" + @Url.Action("Index", "Login") + "'</script>");
                }
                else
                {
                    return Content("<script language='javascript' type='text/javascript'>alert('Registration Failed');location.href='" + @Url.Action("Index", "Login") + "'</script>");
                }
            }
            if (command == "Authenticate")
            {
                UserLoginDetailsService userLoginDetailsService = new UserLoginDetailsService();
                userLogin.UserType = "Admin";
                var userResponse = userLoginDetailsService.AuthenticateUser(userLogin);
                if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    var user = (CustomPrincipal)System.Web.HttpContext.Current.User;
                    if (user.UserType == "Admin")
                    {
                        return RedirectToAction("dashboard", "dashboard", new { id = userResponse.UserLoginId });
                    }
                }
                if (userResponse.UserLoginId != 0)
                {
                    
                        userResponse.UserType = "Admin";
                        string userData = JsonConvert.SerializeObject(userResponse);
                        ValidUserUtility.SetAuthCookie(userData, userResponse.UserLoginId.ToString());
                    
                    return RedirectToAction("dashboard", "dashboard", new { id = userResponse.UserLoginId });
                    
                }
                else
                {
                    return Content("<script language='javascript' type='text/javascript'>alert('Wrong Credentials,Check Username and password');location.href='" + @Url.Action("Index", "Login") + "'</script>");
                }
            }
            return View();
        }
        public JsonResult RegularExpressionPattern_Password()
        {
            // Password Reguler Expression Pattern
            return Json(ValidationsUtility.PatternforPassword(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult SignOut()
        {
            //logs.LogTimings("Out");
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}