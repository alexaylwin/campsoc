using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using aspx_site.Models;

namespace aspx_site.Controllers
{
    public class MobileController : Controller
    {
        //
        // GET: /Mobile/
        Util utility;
        novamainEntities _db;

        public MobileController()
        {
            utility = new Util();
            _db = new novamainEntities();
        }

        public ActionResult Index()
        {
            return RedirectToAction("Home");
        }

        public ActionResult Home()
        {
            int appid = 0;
            if (Request.Cookies["appid"] == null)
            {
                if (Request.QueryString["appid"] != null)
                {
                    appid = utility.getAppID(Request.QueryString["appid"]);
                    HttpCookie mobilecookie = new HttpCookie("appid", Request.QueryString["appid"]);
                    Response.SetCookie(mobilecookie);
                }
                else
                {
                    appid = 0;
                }
            }
            else
            {
                appid = utility.getAppID(Request.Cookies["appid"].Value);
            }

            ViewData["HomeText"] = utility.getHomeText(appid);

            return View();
        }
        public ActionResult Events()
        {
            int appid = 0;
            if (Request.Cookies["appid"] == null)
            {
                if (Request.QueryString["appid"] != null)
                {
                    appid = utility.getAppID(Request.QueryString["appid"]);
                    HttpCookie mobilecookie = new HttpCookie("appid", Request.QueryString["appid"]);
                    Response.SetCookie(mobilecookie);
                }
                else
                {
                    appid = 0;
                }
            }
            else
            {
                appid = utility.getAppID(Request.Cookies["appid"].Value);
            }

            //get all the events
            //DateTime compareDate = DateTime.Now.Subtract(TimeSpan.FromDays(30));
            var selectedEvents = from events in _db.novaevents
                                 where events.AppID == appid //&& events.EventStart > compareDate
                                 orderby events.EventStart descending
                                 select events;
            
            ViewData.Model = selectedEvents.Take(10).ToList();

            return View();
        }
        public ActionResult Messages()
        {
            int appid = 0;
            if (Request.Cookies["appid"] == null)
            {
                if (Request.QueryString["appid"] != null)
                {
                    appid = utility.getAppID(Request.QueryString["appid"]);
                    HttpCookie mobilecookie = new HttpCookie("appid", Request.QueryString["appid"]);
                    Response.SetCookie(mobilecookie);
                }
                else
                {
                    appid = 0;
                }
            }
            else
            {
                appid = utility.getAppID(Request.Cookies["appid"].Value);
            }

            //get all the events
            //DateTime compareDate = DateTime.Now.Subtract(TimeSpan.FromDays(30));
            var selectedMessages = from messages in _db.messages
                                   where messages.AppID == appid //&& messages.MessageDate > compareDate
                                   orderby messages.MessageDate descending
                                   select messages;

            ViewData.Model = selectedMessages.Take(10).ToList();

            return View();
        }

        public ActionResult EventDetails(int id)
        {
            var selectedEvent = (from events in _db.novaevents
                                 where events.EventID == id
                                 select events).First();
            ViewData.Model = selectedEvent;

            ViewData["EventName"] = selectedEvent.EventName;
            if (selectedEvent.EventStart == selectedEvent.EventEnd)
            {
                ViewData["EventDate"] = selectedEvent.EventStart.ToShortDateString() + " " + selectedEvent.EventStart.ToShortTimeString();

            }
            else
            {
                ViewData["EventDate"] = selectedEvent.EventStart.ToShortDateString() + " " + selectedEvent.EventStart.ToShortTimeString();
                ViewData["EventDate"] = ViewData["EventDate"] + " to " + selectedEvent.EventEnd.ToShortDateString() + " " + selectedEvent.EventEnd.ToShortTimeString();
            }

            ViewData["EventDescription"] = selectedEvent.EventDesc;
            ViewData["EventLocation"] = selectedEvent.Location;

            return View();
        }

        public ActionResult MessageDetails(int id)
        {
            var selectedMessage = (from messages in _db.messages
                                 where messages.MessageID == id
                                 select messages).First();

            ViewData["MessageTitle"] = selectedMessage.MessageTitle;
            ViewData["MessageContents"] = selectedMessage.MessageContents;
            ViewData["MessageDate"] = selectedMessage.MessageDate.ToShortDateString() + " " + selectedMessage.MessageDate.ToShortTimeString();

            return View();
        }


    }
}
