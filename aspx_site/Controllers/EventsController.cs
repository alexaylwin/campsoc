using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using MvcContrib.ActionResults;
using aspx_site.Models;
using System.Text;
using System.Security.Cryptography;

namespace aspx_site.Controllers
{

    [Authorize]
    public class EventsController : Controller
    {
        //
        // GET: /Events/

        novamainEntities _db;
        ProcessEvents eventmodel;

        public EventsController()
        {
            _db = new novamainEntities();
            eventmodel = new ProcessEvents();
        }

        public ActionResult Index()
        {
            return RedirectToAction("Home");
        }

        public ActionResult Home()
        {
            //ViewData.Model = _db.events.ToList();
            var selectedEvents = from events in _db.novaevents 
                                 orderby events.EventStart descending
                                 select events;// where events.EventID == 6 select events;
            ViewData.Model = selectedEvents.ToList();
            return View();
        }

        public ActionResult Create()
        {
            ViewData["EventID"] = (eventmodel.getMaxEventID() + 1);
            ViewData["EventStart"] = Convert.ToString(DateTime.Now);
            ViewData["EventEnd"] = Convert.ToString(DateTime.Now);
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                //get the form data from the create event form, and apply it to the new event
                novaevent eventToAdd = new novaevent();
                eventToAdd.EventID = Convert.ToInt32(collection["EventID"]);
                eventToAdd.EventDesc = collection["EventDescription"];
                eventToAdd.AppID = 4;
                eventToAdd.EventName = collection["EventName"];

                string eventstart;
                eventstart = collection["EventStartDate"] + " " + collection["EventStartTime"] + collection["EventStartTimeAMPM"];
                eventToAdd.EventStart = Convert.ToDateTime(eventstart);

                string eventend;
                eventend = collection["EventEndDate"] + " " + collection["EventEndTime"] + collection["EventEndTimeAMPM"];
                eventToAdd.EventEnd = Convert.ToDateTime(eventend);
                
                eventToAdd.NotAttending = Convert.ToInt32(collection["NotAttending"]);
                eventToAdd.Attending = Convert.ToInt32(collection["Attending"]);
                eventToAdd.LastUpdated = DateTime.Now;
                if(collection["Disabled"] == "true"){
                    eventToAdd.Disabled = 1;
                } else {
                    eventToAdd.Disabled = 0;
                }
                eventToAdd.Location = collection["EventLocation"];

                //Compute SHA1 hash for the syncid, and encode it in 32bit
                SHA1 hash = SHA1.Create();
                ASCIIEncoding encoder = new ASCIIEncoding();
                byte[] bytearray = encoder.GetBytes(collection["EventID"] + "alexrocks_21");  
                hash.ComputeHash(bytearray);
                string syncid = "";
                for (int i = 0; i < hash.Hash.Length; i++)
                {
                    syncid = syncid + hash.Hash[i].ToString("x2");
                }
                eventToAdd.SyncID = syncid;

                //add the new object to the database
                int ret = eventmodel.addEvent(4, eventToAdd);
                if (ret != 1)
                {
                    ViewData["ReturnMessage"] = "Error, could not add event.";
                    return View();
                }
                ret = eventmodel.setLastEventUpdate(4, DateTime.Now);
                if (ret != 1)
                {
                    ViewData["ReturnMessage"] = "Error, could not update last event date.";
                    return View();
                }
                return RedirectToAction("Home");
            }
            catch
            {
                ViewData["ReturnMessage"] = "Error, could not create event from form data";
                return View();
           }
            
        }

        public ActionResult Details(int id)
        {
            var selectedEvent = (from events in _db.novaevents 
                                 where events.EventID == id 
                                 select events).First();
            ViewData.Model = selectedEvent;
            return View();
        }


        public ActionResult Edit(int id)
        {
            var selectedEvent = (from events in _db.novaevents 
                                 where events.EventID == id 
                                 select events).First();

            ViewData.Model = selectedEvent;
            ViewData["EventID"] = selectedEvent.EventID;

            //ViewData["EventStart"] = selectedEvent.EventStart;
            //ViewData["EventEnd"] = selectedEvent.EventEnd;
            ViewData["EventStartDate"] = selectedEvent.EventStart.ToString("dd/M/yyyy");
            ViewData["EventStartTime"] = selectedEvent.EventStart.ToString("h:mm");// + ":" + Convert.ToString(selectedEvent.EventStart.Minute);
            ViewData["EventStartAMPM"] = selectedEvent.EventStart.ToString("tt");
            ViewData["EventEndDate"] = selectedEvent.EventEnd.ToString("dd/M/yyyy");
            ViewData["EventEndTime"] = selectedEvent.EventEnd.ToString("h:mm");// + ":" + Convert.ToString(selectedEvent.EventStart.Minute);
            ViewData["EventEndAMPM"] = selectedEvent.EventEnd.ToString("tt");
            ViewData["EventName"] = selectedEvent.EventName;
            ViewData["EventDescription"] = selectedEvent.EventDesc;
            ViewData["EventLocation"] = selectedEvent.Location;
            ViewData["Attending"] = selectedEvent.Attending;
            ViewData["NotAttending"] = selectedEvent.NotAttending;
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                //get the form data from the create event form, and apply it to the new event
                var selectedEvent = (from events in _db.novaevents
                                     where events.EventID == id
                                     select events).First();
                novaevent updatedEvent = new novaevent();
                updatedEvent = selectedEvent;
                //eventToAdd.EventID = Convert.ToInt32(collection["EventID"]);
                updatedEvent.EventDesc = collection["EventDescription"];
                //updatedEvent.AppID = 4;
                updatedEvent.EventName = collection["EventName"];
                updatedEvent.EventStart = Convert.ToDateTime(collection["EventStart"]);
                updatedEvent.EventEnd = Convert.ToDateTime(collection["EventEnd"]);
                updatedEvent.NotAttending = Convert.ToInt32(collection["NotAttending"]);
                updatedEvent.Attending = Convert.ToInt32(collection["Attending"]);
                updatedEvent.LastUpdated = DateTime.Now;
                //updatedEvent.Disabled = 0;
                updatedEvent.Location = collection["EventLocation"];

                //add the new object to the database
                int ret = eventmodel.updateEvent(4, updatedEvent);
                if (ret != 1)
                {
                    ViewData["ReturnMessage"] = "Error, could not add event.";
                    return View();
                }
                ret = eventmodel.setLastEventUpdate(4, DateTime.Now);
                if (ret != 1)
                {
                    ViewData["ReturnMessage"] = "Error, could not update last event date.";
                    return View();
                }
                return View();
            }
            catch
            {
                ViewData["ReturnMessage"] = "Error, could not create event from form data";
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
               
                return RedirectToAction("Home");
            }
            catch
            {
                return View();
            }
        }
    }
}
