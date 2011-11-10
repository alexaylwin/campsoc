using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using aspx_site.Models;

namespace aspx_site.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/
        novamainEntities _db;
        ProcessEvents eventmodel;
        ProcessMessages messagesmodel;
        ProcessUsers usersmodel;

        public DashboardController()
        {
            _db = new novamainEntities();
            eventmodel = new ProcessEvents();
            ProcessMessages messagesmodel = new ProcessMessages();
            ProcessUsers usersmodel = new ProcessUsers();
        }

        public ActionResult Index()
        {
            return RedirectToAction("Home");
        }

        public ActionResult Home()
        {
            try
            {
                var selectedEvents = (from e in _db.novaevents
                                      where e.AppID == 4
                                      orderby e.EventStart descending
                                      select e);
                ViewData["eventlist"] = selectedEvents.Take(5).ToList();

                var selectedUsers = (from u in _db.appusers
                                      where u.AppID == 4
                                      orderby u.UserID descending
                                      select u);
                ViewData["userlist"] = selectedUsers.Take(20).ToList();

                var selectedMessages = (from m in _db.messages
                                        where m.AppID == 4
                                      orderby m.MessageDate descending
                                      select m);
                ViewData["messagelist"] = selectedMessages.Take(5).ToList();
            }
            catch
            {
            }
            //ViewData.Model = selectedEvents.ToList();
            return View();
        }

    }
}
