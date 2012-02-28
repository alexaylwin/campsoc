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
            //Display event info

            var selectedEvent = (from events in _db.novaevents
                                 where events.EventID == id && events.Disabled == 0
                                 select events).First();
            ViewData.Model = selectedEvent;

            ViewData["EventID"] = selectedEvent.EventID;

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


            //Handle user survey
            if (selectedEvent.Survey != null && selectedEvent.Survey > 0)
            {
                ViewData["survey"] = 1;
                var selectedSurvey = (from surveys in _db.eventsurveys
                                      where surveys.SurveyID == selectedEvent.Survey
                                      select surveys).First();

                ViewData["survey_Q1"] = selectedSurvey.QuestionOne;
                ViewData["survey_Q1MC"] = 0;
                if (selectedSurvey.QuestionOneMC1 != null)
                {
                    ViewData["survey_Q1MC"] = 1;
                    ViewData["survey_Q1C1"] = selectedSurvey.QuestionOneMC1;
                }
                if(selectedSurvey.QuestionOneMC2 != null)
                {
                    ViewData["survey_Q1MC"] = 1;
                    ViewData["survey_Q1C2"] = selectedSurvey.QuestionOneMC2;
                }
                if(selectedSurvey.QuestionOneMC3 != null)
                {
                    ViewData["survey_Q1MC"] = 1;
                    ViewData["survey_Q1C3"] = selectedSurvey.QuestionOneMC3;
                }


                ViewData["survey_Q2"] = selectedSurvey.QuestionTwo;
                ViewData["survey_Q2MC"] = 0;
                if (selectedSurvey.QuestionTwoMC1 != null)
                {
                    ViewData["survey_Q2MC"] = 1;
                    ViewData["survey_Q2C1"] = selectedSurvey.QuestionTwoMC1;
                }
                if (selectedSurvey.QuestionTwoMC2 != null)
                {
                    ViewData["survey_Q2MC"] = 1;
                    ViewData["survey_Q2C2"] = selectedSurvey.QuestionTwoMC2;
                }
                if(selectedSurvey.QuestionTwoMC3 != null)
                {
                    ViewData["survey_Q2MC"] = 1;
                    ViewData["survey_Q2C3"] = selectedSurvey.QuestionTwoMC3;
                }
                

                ViewData["survey_Q3"] = selectedSurvey.QuestionThree;
                ViewData["survey_Q3MC"] = 0;
                if (selectedSurvey.QuestionThreeMC1 != null)
                {
                    ViewData["survey_Q3MC"] = 1;
                    ViewData["survey_Q3C1"] = selectedSurvey.QuestionThreeMC1;
                }
                if (selectedSurvey.QuestionThreeMC2 != null)
                {
                    ViewData["survey_Q3MC"] = 1;
                    ViewData["survey_Q3C2"] = selectedSurvey.QuestionThreeMC2;
                }
                if(selectedSurvey.QuestionThreeMC3 != null)
                {
                    ViewData["survey_Q3MC"] = 1;
                    ViewData["survey_Q3C3"] = selectedSurvey.QuestionThreeMC3;
                }
            }

            return View();
        }

        //handle a event survey submission
        [HttpPost]
        public ActionResult EventDetails(FormCollection collection)
        {
            eventfeedback feedback = new eventfeedback();
            feedback.AppUserID = 0;
            feedback.SurveyID = 1;
            feedback.QuestionOne = collection["Q1_R"];
            feedback.QuestionTwo = collection["Q2_R"];
            feedback.QuestionThree = collection["Q3_R"];
            feedback.EventID = Convert.ToInt32(collection["EventID"]);
            feedback.SubmitTime = DateTime.Now;

            try
            {
                _db.eventfeedbacks.AddObject(feedback);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
            }



            return RedirectToAction("Events");
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
