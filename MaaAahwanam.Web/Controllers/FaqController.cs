using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MaaAahwanam.Models;
using MaaAahwanam.Utility;
//using MaaAahwanam.Repository;
using System.IO;

namespace MaaAahwanam.Web.Controllers
{
    public class FaqController : Controller
    {
        //
        // GET: /Faq/
        public ActionResult Index()
        {
            return View();
        }   
	}
}