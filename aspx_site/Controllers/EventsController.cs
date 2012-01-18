﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using System.Xml.Linq;
using MvcContrib.ActionResults;
using aspx_site.Models;
using System.Text;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using System.Globalization;
using Twitterizer;
using System.Configuration;
using System.Web.Script.Serialization;

namespace aspx_site.Controllers
{

    [Authorize]
    public class EventsController : Controller
    {
        //
        // GET: /Events/

        novamainEntities _db;
        ProcessEvents eventmodel;
        Util utility;
        int defaultappid = 4;

        public EventsController()
        {
            _db = new novamainEntities();
            eventmodel = new ProcessEvents();
            utility = new Util();
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

            //create the json list with fullcalendar event properties
            List<fullcalendar_event> jsonlist = new List<fullcalendar_event>();
            fullcalendar_event fc_event;
            foreach (novaevent ne in selectedEvents.Take(5).ToList())
            {
                fc_event = new fullcalendar_event();
                fc_event.allDay = false;
                fc_event.title = ne.EventName;
                fc_event.start = ne.EventStart.ToString("yyyy-MM-dd") + " " + ne.EventStart.ToString("HH:mm");//ne.EventStart;//
                fc_event.end = ne.EventEnd.ToString("yyyy-MM-dd") + " " + ne.EventEnd.ToString("HH:mm");//ne.EventEnd;//
                fc_event.url = "../Events/Details/" + ne.EventID;
                fc_event.id = ne.EventID;
                jsonlist.Add(fc_event);
            }

            JsonResult eventjson = this.Json(jsonlist);
            Object jsondata = eventjson.Data;
            string serialized = new JavaScriptSerializer().Serialize(jsondata);
            string replaced = serialized.Replace("\"\\/Date(", "Date(").Replace(")\\/\"", ")");

            ViewData["eventlistjson"] = replaced;

            return View();
        }

        public ActionResult Create()
        {
            ViewData["EventID"] = (eventmodel.getMaxEventID() + 1);
            appinfo app = utility.getAppInfo(defaultappid);

            if (app.TwitterAccessToken != null)
            {
                ViewData["twitterRegistered"] = true;
            }
            else
            {
                ViewData["twitterRegistered"] = false;
            }

            if (app.FacebookAccessToken != null)
            {
                //We have a facebook account linked, so get the facebook user's name, page name and links
                ViewData["facebookRegistered"] = true;
                WebClient wc = new WebClient();
                string wcresponse;
                wcresponse = wc.DownloadString("https://graph.facebook.com/" + app.FacebookUserId);
                JObject userinfo = JObject.Parse(wcresponse);
                ViewData["facebookUserName"] = (string)userinfo["name"];
                ViewData["facebookUserId"] = (string)userinfo["id"];

                if (app.FacebookPageId != null)
                {
                    wcresponse = wc.DownloadString("https://graph.facebook.com/" + app.FacebookPageId + "?access_token=" + app.FacebookAccessToken);
                    JObject pageinfo = JObject.Parse(wcresponse);
                    ViewData["facebookPageName"] = (string)pageinfo["name"];
                    ViewData["facebookPageId"] = (string)pageinfo["id"];
                }
            }
            else
            {
                ViewData["facebookRegistered"] = false;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                appinfo app = utility.getAppInfo(defaultappid);
                WebClient wc = new WebClient();
                string url_request;
                string facebookeventid = "";

                //Create the app event
                novaevent eventToAdd = new novaevent();
                eventToAdd.EventID = Convert.ToInt32(collection["EventID"]);
                eventToAdd.EventDesc = collection["EventDescription"];

                //todo: session app id, not 4;
                eventToAdd.AppID = defaultappid;
                eventToAdd.EventName = collection["EventName"];

                string eventstart;
                eventstart = collection["EventStartDate"] + " " + collection["EventStartTime"] + collection["EventStartTimeAMPM"];
                eventToAdd.EventStart = Convert.ToDateTime(eventstart);

                string eventend;
                if (!collection["NoEndDate"].Contains("true"))
                {
                    eventend = collection["EventEndDate"] + " " + collection["EventEndTime"] + collection["EventEndTimeAMPM"];
                    eventToAdd.EventEnd = Convert.ToDateTime(eventend);
                }
                else
                {
                    eventToAdd.EventEnd = eventToAdd.EventStart;
                }

                int outnum;

                int.TryParse(collection["NotAttending"], out outnum);
                eventToAdd.NotAttending = outnum;

                int.TryParse(collection["Attending"], out outnum);
                eventToAdd.Attending = outnum;

                eventToAdd.LastUpdated = DateTime.Now;
                if(collection["Disabled"].Contains("true")){
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
                int ret = eventmodel.addEvent(defaultappid, eventToAdd);
                if (ret != 1)
                {
                    ViewData["ReturnMessage"] = "Error, could not add event.";
                    return View();
                }
                ret = eventmodel.setLastEventUpdate(defaultappid, DateTime.Now);
                if (ret != 1)
                {
                    ViewData["ReturnMessage"] = "Error, could not update last event date.";
                    return View();
                }

                //Create the Facebook event
                if (collection["PostToFacebook"].Contains("true"))
                {
                    NameValueCollection nvc = new NameValueCollection();
                    DateTime tempdate;
                    TimeSpan tempspan;

                    tempdate = Convert.ToDateTime(collection["FacebookEventStartDate"] + " " + collection["FacebookEventStartTime"] + collection["FacebookEventStartTimeAMPM"]);
                    //TODO:
                    //Awkward hack to fix facebook times on events, ideally this would use the current timezone
                    tempdate = tempdate.AddHours(8);
                    tempspan = (tempdate - new DateTime(1970, 1, 1));
                    nvc.Add("start_time", Convert.ToString(tempspan.TotalSeconds));

                    if (!collection["NoEndDate"].Contains("true"))
                    {
                        tempdate = Convert.ToDateTime(collection["FacebookEventEndDate"] + " " + collection["FacebookEventEndTime"] + collection["FacebookEventEndTimeAMPM"]);
                        tempdate = tempdate.AddHours(8);
                        tempspan = (tempdate - new DateTime(1970, 1, 1));
                        nvc.Add("end_time", Convert.ToString(tempspan.TotalSeconds));
                    }

                    nvc.Add("access_token", app.FacebookPageAccessToken);
                    nvc.Add("name", collection["FacebookEventName"]);
                    nvc.Add("description", collection["FacebookEventDescription"]);
                    nvc.Add("location", collection["FacebookEventLocation"]);

                    byte[] result = wc.UploadValues("https://graph.facebook.com/" + app.FacebookPageId + "/events", "POST", nvc);
                    
                    
                    string strresult = Encoding.Default.GetString(result);
                    JObject eventresponse = JObject.Parse(strresult);
                    if(eventresponse["id"] != null){
                       facebookeventid = (string)eventresponse["id"];
                    }

                }

                //Create the twitter message
                if(collection["PostToTwitter"].Contains("true"))
                {
                    OAuthTokens at = new OAuthTokens();
                    at.AccessToken = app.TwitterAccessToken;
                    at.AccessTokenSecret = app.TwitterAccessTokenSecret;
                    at.ConsumerKey = ConfigurationManager.AppSettings["twitterConsumerKey"];
                    at.ConsumerSecret= ConfigurationManager.AppSettings["twitterConsumerSecret"];

                    string tweettext = "";
                    tweettext = collection["TweetText"];

                    if (collection["TwitterEventLink"].Contains("true") && facebookeventid != "")
                    {
                        tweettext = tweettext + " http://facebook.com/events/" + facebookeventid;
                    }
                
                    TwitterResponse<TwitterStatus> resp = TwitterStatus.Update(at, tweettext);
                }


                return RedirectToAction("Home");
            }
            catch(Exception e)
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
            ViewData["EventStartDate"] = selectedEvent.EventStart.ToString("dd/M/yyyy");
            ViewData["EventStartTime"] = selectedEvent.EventStart.ToString("h:mm");
            ViewData["EventStartAMPM"] = selectedEvent.EventStart.ToString("tt");
            ViewData["EventEndDate"] = selectedEvent.EventEnd.ToString("dd/M/yyyy");
            ViewData["EventEndTime"] = selectedEvent.EventEnd.ToString("h:mm");
            ViewData["EventEndAMPM"] = selectedEvent.EventEnd.ToString("tt");
            ViewData["EventName"] = selectedEvent.EventName;
            ViewData["EventDescription"] = selectedEvent.EventDesc;
            ViewData["EventLocation"] = selectedEvent.Location;
            ViewData["Attending"] = selectedEvent.Attending;
            ViewData["NotAttending"] = selectedEvent.NotAttending;
            ViewData["EventDisabled"] = selectedEvent.Disabled;
            if (selectedEvent.EventStart == selectedEvent.EventEnd)
            {
                ViewData["NoEndDate"] = 1;
            }
            else
            {
                ViewData["NoEndDate"] = 0;
            }
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
                //updatedEvent.AppID = defaultappid;
                updatedEvent.EventName = collection["EventName"];
                updatedEvent.EventStart = Convert.ToDateTime(collection["EventStart"]);
                updatedEvent.EventEnd = Convert.ToDateTime(collection["EventEnd"]);
                updatedEvent.NotAttending = Convert.ToInt32(collection["NotAttending"]);
                updatedEvent.Attending = Convert.ToInt32(collection["Attending"]);
                updatedEvent.LastUpdated = DateTime.Now;
                //updatedEvent.Disabled = 0;
                updatedEvent.Location = collection["EventLocation"];

                //add the new object to the database
                int ret = eventmodel.updateEvent(defaultappid, updatedEvent);
                if (ret != 1)
                {
                    ViewData["ReturnMessage"] = "Error, could not add event.";
                    return View();
                }
                ret = eventmodel.setLastEventUpdate(defaultappid, DateTime.Now);
                if (ret != 1)
                {
                    ViewData["ReturnMessage"] = "Error, could not update last event date.";
                    return View();
                }
                return Redirect("../Details/" + id);
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
