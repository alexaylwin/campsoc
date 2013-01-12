using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using aspx_site.Models;
using System.Xml.Linq;
using MvcContrib.ActionResults;

namespace aspx_site.Controllers
{
    public class AppServiceController : Controller
    {

        novamainEntities _db;
        ProcessEvents eventmodel;
        ProcessMessages messagemodel;
        ProcessRSVPs rsvpmodel;
        ProcessUsers usermodel;
        Util utility_functions;
        ProcessComments commentmodel;
        ProcessObjectMeta metamodel;

        public AppServiceController()
        {
            _db = new novamainEntities();
            eventmodel = new ProcessEvents();
            messagemodel = new ProcessMessages();
            utility_functions = new Util();
            rsvpmodel = new ProcessRSVPs();
            usermodel = new ProcessUsers();
            commentmodel = new ProcessComments();
            metamodel = new ProcessObjectMeta();

        }

        //
        // GET: /AppService/Check

        public ActionResult CheckEvents(string app_id, string client_id, string last_updated)
        {
            List<@novaevent> eventlist = new List<@novaevent>();
            XElement root;
            DateTime dt = DateTime.Parse(last_updated);

            //Response.Write(utility_functions.getAppID(appid));

            int appid = utility_functions.getAppID(app_id);
            if(appid == 0){
                root = new XElement("error");
                root.SetValue("No such app");
                return new XmlResult(root);
            }

            eventlist = eventmodel.checkEvents(appid, dt);
            root = new XElement("events");
            XElement currevent;
            XElement curr_node;

            if (eventlist != null)
            {
                foreach (var e in eventlist)
                {
                    currevent = new XElement("event");

                    curr_node = new XElement("name");
                    curr_node.SetValue(e.EventName);
                    currevent.Add(curr_node);

                    curr_node = new XElement("eventstart");
                    curr_node.SetValue(e.EventStart.ToString("yyyy-MM-dd HH:mm:ss"));
                    currevent.Add(curr_node);

                    curr_node = new XElement("eventend");
                    curr_node.SetValue(e.EventEnd.ToString("yyyy-MM-dd HH:mm:ss"));
                    currevent.Add(curr_node);

                    curr_node = new XElement("desc");
                    curr_node.SetValue(e.EventDesc);
                    currevent.Add(curr_node);

                    curr_node = new XElement("attending");
                    curr_node.SetValue(e.Attending);
                    currevent.Add(curr_node);

                    curr_node = new XElement("notattending");
                    curr_node.SetValue(e.NotAttending);
                    currevent.Add(curr_node);

                    curr_node = new XElement("syncid");
                    curr_node.SetValue(e.SyncID);
                    currevent.Add(curr_node);

                    curr_node = new XElement("disabled");
                    curr_node.SetValue(e.Disabled);
                    currevent.Add(curr_node);

                    curr_node = new XElement("location");
                    if (e.Location == null)
                    {
                        curr_node.SetValue("");
                    }
                    else
                    {
                        curr_node.SetValue(e.Location);
                    }

                    currevent.Add(curr_node);

                    root.Add(currevent);
                }
            }
            if (eventlist != null)
            {
                foreach (var e in eventlist)
                {
                    metamodel.addObjectViewedApp(e.EventID, 1);
                }
            }
            utility_functions.insertServerEvent("events checked", app_id, client_id, last_updated, Request.RawUrl, Request.UserHostAddress);
            return new XmlResult(root);
        }

        public ActionResult CheckMessages(string app_id, string client_id, string last_updated)
        {
            List<@message> messagelist = new List<@message>();
            XElement root;
            DateTime dt = DateTime.Parse(last_updated);

            int appid = utility_functions.getAppID(app_id);
            if (appid == 0)
            {
                root = new XElement("error");
                root.SetValue("No such app");
                return new XmlResult(root);
            }

            messagelist = messagemodel.checkMessages(appid, client_id, dt);
            root = new XElement("messages");
            XElement currmessage;
            XElement curr_node;

            if (messagelist != null)
            {
                foreach (var e in messagelist)
                {
                    currmessage = new XElement("message");

                    curr_node = new XElement("title");
                    curr_node.SetValue(e.MessageTitle);
                    currmessage.Add(curr_node);

                    curr_node = new XElement("date");
                    curr_node.SetValue(e.MessageDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    currmessage.Add(curr_node);

                    curr_node = new XElement("contents");
                    curr_node.SetValue(e.MessageContents);
                    currmessage.Add(curr_node);

                    root.Add(currmessage);
                }
            }

            if (messagelist != null)
            {
                foreach (var e in messagelist)
                {
                    metamodel.addObjectViewedApp(e.MessageID, 2);
                }
            }
            utility_functions.insertServerEvent("messages checked", app_id, client_id, last_updated, Request.RawUrl, Request.UserHostAddress);
            return new XmlResult(root);
        }

        public ActionResult CheckDate(string app_id, string client_id)
        {
            XElement root;
            int appid = utility_functions.getAppID(app_id);

            if (appid == 0)
            {
                root = new XElement("error");
                root.SetValue("No such app - " + app_id);
                return new XmlResult(root);
            }

            DateTime eventdate = eventmodel.getLastEventUpdate(appid);
            DateTime messagedate = messagemodel.getLastMessageUpdate(appid);
            DateTime homescreendate = utility_functions.getHomeScreenUpdate(appid);
            root = new XElement("items");
            XElement messagenode = new XElement("messages");
            XElement eventnode = new XElement("events");
            XElement homenode = new XElement("hometext");

            messagenode.SetValue(messagedate.ToString("yyyy-MM-dd HH:mm:ss"));
            eventnode.SetValue(eventdate.ToString("yyyy-MM-dd HH:mm:ss"));
            homenode.SetValue(homescreendate.ToString("yyyy-MM-dd HH:mm:ss"));
            root.Add(eventnode);
            root.Add(messagenode);
            root.Add(homenode);

            utility_functions.insertServerEvent("date checked", app_id, client_id, Request.RawUrl, Request.UserHostAddress);

            return new XmlResult(root);
        }

        public ActionResult CheckHomeText(string app_id, string client_id)
        {
            XElement root;
            int appid = utility_functions.getAppID(app_id);

            if (appid == 0)
            {
                root = new XElement("error");
                root.SetValue("No such app - " + app_id);
                return new XmlResult(root);
            }

            string hometext = utility_functions.getHomeText(appid);

            root = new XElement("items");
            XElement hometextnode = new XElement("hometext");
            hometextnode.SetValue(hometext);
            root.Add(hometextnode);

            utility_functions.insertServerEvent("homescreen checked", app_id, client_id, Request.RawUrl, Request.UserHostAddress);

            return new XmlResult(root);
        }

        public ActionResult CheckUserRegistration(string app_id, string client_id)
        {
            XElement root;
            int appid = utility_functions.getAppID(app_id);

            if (appid == 0)
            {
                root = new XElement("error");
                root.SetValue("No such app - " + app_id);
                return new XmlResult(root);
            }

            bool userregistered = usermodel.checkUserRegistration(client_id, appid);

            root = new XElement("registered");
            if (userregistered)
            {
                root.SetValue("1");
            }
            else
            {
                root.SetValue("0");
            }

            return new XmlResult(root);
        }

        public ActionResult SendRSVP(string app_id, string client_id, string response, string syncid)
        {
            XElement root = new XElement("response");
            try
            {
                rsvpmodel.insertRSVP(usermodel.getUserID(client_id), eventmodel.getEventID(syncid), response);
                root.SetValue("1");
                utility_functions.insertServerEvent("rsvp recieved", app_id, client_id, response, syncid, "success", Request.RawUrl, Request.UserHostAddress);
                return new XmlResult(root);
            }
            catch (Exception e)
            {
                root.SetValue("0");
                utility_functions.insertServerEvent("rsvp recieved", app_id, client_id, response, syncid, "failure", Request.RawUrl, Request.UserHostAddress);
                return new XmlResult(root);
            }
        }

        public ActionResult SendComment(string app_id, string client_id, string comment, string syncid)
        {
            XElement root = new XElement("response");
            try
            {
                commentmodel.insertComment(usermodel.getUserID(client_id), eventmodel.getEventID(syncid), comment);
                root.SetValue("1");
                utility_functions.insertServerEvent("comment recieved", app_id, client_id, comment, syncid, "success", Request.RawUrl, Request.UserHostAddress);
                return new XmlResult(root);
            }
            catch (Exception e)
            {
                root.SetValue("0");
                utility_functions.insertServerEvent("comment recieved", app_id, client_id, comment, syncid, "failure", Request.RawUrl, Request.UserHostAddress);
                return new XmlResult(root);
            }
        }

        public ActionResult RegisterUser(string app_id, string client_id, string handset_model, string handset_manufacturer, string handset_product)
        {
            XElement root = new XElement("response");
            if (usermodel.registerUser(client_id, utility_functions.getAppID(app_id), handset_model, handset_manufacturer, handset_product))
            {
                root.SetValue("1");
                utility_functions.insertServerEvent("user registering", app_id, client_id, "success", Request.RawUrl, Request.UserHostAddress);
            }
            else
            {
                root.SetValue("0");
                utility_functions.insertServerEvent("user registering", app_id, client_id, "failure", Request.RawUrl, Request.UserHostAddress);
            }
            
            return new XmlResult(root);
        }

        public ActionResult SendBetaEvent(string event_desc, string submitted_by, string col1, string col2, string col3, string col4, string col5)
        {

            int ret = utility_functions.insertBetaEvent(event_desc, submitted_by, col1, col2, col3, col4, col5);

            XElement root = new XElement("response");
            if (ret == 1)
            {
                root.SetValue("1");
            }
            else
            {
                root.SetValue("0");
            }
            return new XmlResult(root);
        }

        public ActionResult testDatabase()
        {
            XElement root = new XElement("root");

            var selected_app = (from a in _db.appinfoes
                                where (a.HashedAppID == "6f67ddf73de6079afb3531a120944abfcf642b78")
                                select a).FirstOrDefault();
            root.SetValue(selected_app.AppID);
            return new XmlResult(root);
        }
        



        public ActionResult Index()
        {
            return View();
        }
    
    }

}
