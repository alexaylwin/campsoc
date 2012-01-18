using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using aspx_site.Models;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;
using System.Text;
using Twitterizer;
using System.Configuration;

namespace aspx_site.Controllers
{
    [Authorize]
    public class MessagesController : Controller
    {

        novamainEntities _db;
        ProcessMessages messagemodel;
        Util utility;
        int defaultappid = 4;

        public MessagesController()
        {
            _db = new novamainEntities();
            messagemodel = new ProcessMessages();
            utility = new Util();
        }

        public ActionResult Index()
        {
            return RedirectToAction("Home");
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
            WebClient wc = new WebClient();
            string facebookpostid = "";
            appinfo app = utility.getAppInfo(defaultappid);
            try
            {
                message newmessage = new message
                {
                    MessageContents = collection["MessageContents"],
                    MessageTitle = collection["MessageTitle"],
                    MessageDate = DateTime.Now,
                    MessageID = Convert.ToInt32(collection["MessageID"]),
                    AppID = defaultappid
                };
                int ret = messagemodel.addMessage(defaultappid, newmessage);
                if (ret != 1)
                {
                    return View();
                }
                ret = messagemodel.setLastMessageUpdate(defaultappid, DateTime.Now);

                //create the facebook wall post
                if (collection["PostToFacebook"].Contains("true"))
                {
                    NameValueCollection nvc = new NameValueCollection();

                    nvc.Add("access_token", app.FacebookPageAccessToken);
                    nvc.Add("message", collection["FacebookWallPost"]);

                    byte[] result = wc.UploadValues("https://graph.facebook.com/" + app.FacebookPageId + "/feed", "POST", nvc);


                    string strresult = Encoding.Default.GetString(result);
                    JObject wallresponse = JObject.Parse(strresult);
                    if (wallresponse["id"] != null)
                    {
                        facebookpostid = (string)wallresponse["id"];
                    }

                }
                //create the tweet
                if (collection["PostToTwitter"].Contains("true"))
                {
                    OAuthTokens at = new OAuthTokens();
                    at.AccessToken = app.TwitterAccessToken;
                    at.AccessTokenSecret = app.TwitterAccessTokenSecret;
                    at.ConsumerKey = ConfigurationManager.AppSettings["twitterConsumerKey"];
                    at.ConsumerSecret = ConfigurationManager.AppSettings["twitterConsumerSecret"];

                    string tweettext = "";
                    tweettext = collection["TweetText"];

                    if (collection["TwitterEventLink"].Contains("true") && facebookpostid != "")
                    {
                        tweettext = tweettext + " http://facebook.com/" + app.FacebookPageId;
                    }

                    TwitterResponse<TwitterStatus> resp = TwitterStatus.Update(at, tweettext);
                }
                return RedirectToAction("Home");

            }
            catch(Exception e)
            {
                return View();
            }
        }
    }
}
