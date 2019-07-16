﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MaaAahwanam.Models;
using MaaAahwanam.Utility;
using System.Configuration;
using System.Web.Security;
using MaaAahwanam.Service;
using MaaAahwanam.Web.Custom;

namespace MaaAahwanam.Web.Controllers
{
    public class ChangePasswordController : Controller
    {
        //
        // GET: /ChangePassword/
        [Authorize]
        public ActionResult Index()
        {
            var user = (CustomPrincipal)System.Web.HttpContext.Current.User;
            ViewBag.Type = user.UserType;
            return View();
        }
        [HttpPost]
        public ActionResult Index(UserLogin userLogin,string OldPassword)
        {
            var user = (CustomPrincipal)System.Web.HttpContext.Current.User;
            UserLoginDetailsService userLoginDetailsService = new UserLoginDetailsService();
            string oldpwd = userLoginDetailsService.Getpassword(user.UserId);
            if (oldpwd == OldPassword)
            {
                userLoginDetailsService.changepassword(userLogin, (int)user.UserId);
                return Content("<script language='javascript' type='text/javascript'>alert('Password Updated Successfully');location.href='" + @Url.Action("Index", "ChangePassword") + "'</script>");
            }
            return Content("<script language='javascript' type='text/javascript'>alert('Old Password Wrongly Entered');location.href='" + @Url.Action("Index", "ChangePassword") + "'</script>");
        }
    }
}