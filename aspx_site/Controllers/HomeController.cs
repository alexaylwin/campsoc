using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using aspx_site.Models;

namespace aspx_site.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "";
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Version()
        {
            return View();
        }

        public ActionResult LandingPage()
        {
            return View();
        }
    }
}
