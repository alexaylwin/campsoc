using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using aspx_site.Models;
using System.Web.Script.Serialization;


namespace aspx_site.Controllers
{
    class fullcalendar_event
    {
        public int id;
        public string title;
        public Boolean allDay;
        public string start;//DateTime start;
        public string end;// DateTime end;
        public string url;
        public string className;
        public Boolean editable;
        public string source;
        public string color;
        public string backgroundColor;
        public string borderColor;
        public string textColor;
        
        public fullcalendar_event()
        {
            id = 0;
            title = "";
            allDay = false;
            //start = "";
            //end = "";
            url = "";
            className = "";
            editable = false;
            source = "";
            color = "";
            backgroundColor = "";
            borderColor = "";
            textColor = "";
        }

    }
    [Authorize]
    public class DashboardController : BaseController
    {
        //
        // GET: /Dashboard/
        //novamainEntities _db;

        public DashboardController() : base()
        {

        }

        public ActionResult Index()
        {
            return RedirectToAction("Home");
        }

        public ActionResult Home()
        {
            try
            {
                ViewData["hashedappid"] = utility.getHashedAppID(defaultappid);
                var selectedEvents = (from e in _db.novaevents
                                      where e.AppID == defaultappid
                                      orderby e.EventStart descending
                                      select e);
                ViewData["eventlist"] = selectedEvents.Take(5).ToList();

                var selectedUsers = (from u in _db.appusers
                                      where u.AppID == defaultappid
                                      orderby u.UserID descending
                                      select u);
                ViewData["userlist"] = selectedUsers.Take(20).ToList();

                var selectedMessages = (from m in _db.messages
                                        where m.AppID == defaultappid
                                      orderby m.MessageDate descending
                                      select m);
                ViewData["messagelist"] = selectedMessages.Take(5).ToList();


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


                //get user feedback and survey results
              //  var selectedFeedback = (from f in _db.eventfeedbacks
              //                          orderby f.SubmitTime descending
              //                          select f).Take(5).ToList();
                novaevent currentEvent;

                var selectedFeedback = feedbackmodel.getFeedback(defaultappid, 5);

                string[] eventids = new string[5];
                string[] eventnames = new string[5];
                string[] feedbackids = new string[5];
                DateTime[] submittimes = new DateTime[5];


                int curreventid;
                for (int i = 0; i < selectedFeedback.Count; i++)
                {
                    curreventid = selectedFeedback[i].EventID;
                    currentEvent = (from e in _db.novaevents
                                    where e.EventID == curreventid
                                    select e).First();
                    eventids[i] = Convert.ToString(currentEvent.EventID);
                    eventnames[i] = currentEvent.EventName;
                    feedbackids[i] = Convert.ToString(selectedFeedback[i].FeedbackID);
                    submittimes[i] = selectedFeedback[i].SubmitTime.GetValueOrDefault();
                }
                ViewData["feedbackcount"] = selectedFeedback.Count;
                ViewData["feedbackeventids"] = eventids;
                ViewData["feedbackeventnames"] = eventnames;
                ViewData["feedbackids"] = feedbackids;
                ViewData["feedbacksubmittimes"] = submittimes;
                ViewData["feedback"] = selectedFeedback;
                
            }
            catch(Exception e)
            {
                return View();
            }
            //ViewData.Model = selectedEvents.ToList();
            return View();
        }

    }
}
