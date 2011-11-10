using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using aspx_site.Models;

namespace aspx_site.Controllers
{
    [Authorize]
    public class MessagesController : Controller
    {

        novamainEntities _db;
        ProcessMessages messagemodel;

        public MessagesController()
        {
            _db = new novamainEntities();
            messagemodel = new ProcessMessages();
        }

        public ActionResult Index()
        {
            return View("Home");
        }

        public ActionResult Details(int id)
        {
            var selectedMessage = (from m in _db.messages
                                 where m.MessageID == id
                                 select m).First();
            ViewData.Model = selectedMessage;
            return View();
        }

        public ActionResult Create()
        {
            return View();
        } 

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                message newmessage = new message
                {
                    MessageContents = collection["MessageContents"],
                    MessageTitle = collection["MessageTitle"],
                    MessageDate = DateTime.Now,
                    MessageID = Convert.ToInt32(collection["MessageID"])
                };
                return RedirectToAction("Home");
            }
            catch
            {
                return View();
            }
        }
    }
}
