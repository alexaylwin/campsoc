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
            return RedirectToAction("Home");//View("Home");
        }

        public ActionResult Home()
        {
            var selectedMessages = from messages in _db.messages
                                   orderby messages.MessageDate descending
                                   select messages;
            ViewData.Model = selectedMessages.ToList();
            return View();
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
            ViewData["MessageID"] = (messagemodel.getMaxMessageID() + 1);
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
                    MessageID = Convert.ToInt32(collection["MessageID"]),
                    AppID = 4
                };
                int ret = messagemodel.addMessage(4, newmessage);
                if (ret != 1)
                {
                    return View();
                }
                ret = messagemodel.setLastMessageUpdate(4, DateTime.Now);
                return View("Home");
            }
            catch
            {
                return View();
            }
        }
    }
}
