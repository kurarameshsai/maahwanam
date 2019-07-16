using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
//using MaaAahwanam.Repository;

namespace MaaAahwanam.Web.Controllers
{
    public class LogsController : Controller
    {
        //
        // GET: /Logs/


        public ActionResult Index()
        {
            return View();
        }
    }
}