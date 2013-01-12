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

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            ViewData["Message"] = "";
            Util utility = new Util();
            utility.insertBetaEvent("email submission", "-", collection["email"], "", "", "", "");

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

        public ActionResult PrivacyPolicy()
        {
            return View();
        }

        public ActionResult Terms()
        {
            return View();
        }

        public ActionResult Tour()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}
