using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MaaAahwanam.Models;
using MaaAahwanam.Utility;
//using MaaAahwanam.Repository;

namespace MaaAahwanam.Web.Controllers
{
    public class CommentsController : Controller
    {
        // GET: /Comments/
        public ActionResult Index(string Id)
        {
            return View();
        }

        public ActionResult Update()
        {
            return View();
        }
    }
}