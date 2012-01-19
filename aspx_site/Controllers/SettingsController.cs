using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Configuration;
using Twitterizer;
using aspx_site.Models;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;

namespace aspx_site.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        //
        // GET: /Settings/

        Util utility;
        int defaultappid = 4;

        public SettingsController()
        {
            utility = new Util();
            
        }

        public ActionResult Index()
        {
            return RedirectToAction("Home");
        }

        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Accounts()
        {
            appinfo app = utility.getAppInfo(defaultappid);
            WebClient wc = new WebClient();

            if (app != null)
            {
                if (app.TwitterAccessToken != null)
                {
                    ViewData["TwitterRegistered"] = true;

                    //use this to get the user name and return it as well
                    OAuthTokens tokens = new OAuthTokens();
                    tokens.AccessToken = app.TwitterAccessToken;
                    tokens.AccessTokenSecret = app.TwitterAccessTokenSecret;
                    tokens.ConsumerKey = ConfigurationManager.AppSettings["twitterConsumerKey"];
                    tokens.ConsumerSecret = ConfigurationManager.AppSettings["twitterConsumerSecret"];
                }
                else
                {
                    ViewData["TwitterRegistered"] = false;
                }

                if (app.FacebookAccessToken != null)
                {
                    ViewData["FacebookRegistered"] = true;

                    //get user name and accounts
                    string wcresponse;
                    List<FacebookPageModel> pagemodels = new List<FacebookPageModel>();
                    FacebookUserModel usermodel;

                    //create the user model
                    wcresponse = wc.DownloadString("https://graph.facebook.com/me?access_token=" + app.FacebookAccessToken);
                    usermodel = new FacebookUserModel(JObject.Parse(wcresponse));

                    //create the account models
                    wcresponse = wc.DownloadString("https://graph.facebook.com/me/accounts?access_token=" + app.FacebookAccessToken);
                    JArray accounts = (JArray)JObject.Parse(wcresponse)["data"];

                    for (int i = 0; i < accounts.Count; i++)
                    {

                        pagemodels.Add(new FacebookPageModel((JObject)accounts[i]));
                    }

                    string[] pagenames = new string[pagemodels.Count];

                    for (int i = 0; i < pagenames.Count(); i++)
                    {
                        pagenames[i] = pagemodels[i].Name;
                        if (app.FacebookPageId != null && app.FacebookPageId == pagemodels[i].Id)
                        {
                            ViewData["FacebookLinkedPageId"] = app.FacebookPageId;
                            ViewData["FacebookLinkedPageName"] = pagemodels[i].Name;
                        }
                    }

                    ViewData["FacebookUserName"] = usermodel.Name;
                    ViewData["FacebookUserId"] = usermodel.Id;
                    ViewData["FacebookPageNames"] = pagenames;

                }
                else
                {
                    ViewData["FacebookRegistered"] = false;
                }
            }

            //ViewData["FacebookReturnMessage"] = RouteData.Values["FacebookReturnMessage"];
            //ViewData["TwitterReturnMessage"] = RouteData.Values["TwitterReturnMessage"];
           
            return View();
        }

        /*
        [HttpGet]
        public ActionResult Accounts()
        {

            //if we're returning with a twitter registration, this will be set to one
            /*if (Request.QueryString["twitter"] == "1")
            {
                string consumersecret = ConfigurationManager.AppSettings["twitterConsumerSecret"];
                string consumerkey = ConfigurationManager.AppSettings["twitterConsumerKey"];
                string testAccessToken = "32297508-FkO7B8EgbPqykpwcFUe7k4zAOSt4n8OntwqZYsbpM";
                string testAccessTokenSecret = "NwBvoePNooAirhTFR5pEWVAGBkcNBIdKbVjYwrzE";
                OAuthTokens tokens = new OAuthTokens();
                tokens.ConsumerKey = consumerkey;
                tokens.ConsumerSecret = consumersecret;

                if (Request.QueryString["oauth_token"] != null && Request.QueryString["oauth_verifier"] != null)
                {
                    //this gets the access tokens after the user has allowed campsoc to access their account
                    OAuthTokenResponse accessTokens = OAuthUtility.GetAccessToken(consumerkey, consumersecret, Request.QueryString["oauth_token"], Request.QueryString["oauth_verifier"]);

                    //store the access tokens as user credentials with the appinfo
                    int result = utility.updateAppTwitterTokens(4, accessTokens.Token, accessTokens.TokenSecret);

                    if (result == 1)
                    {
                        ViewData["ReturnMessage"] = "Success! Twitter account now linked!";
                    }
                    else
                    {
                        ViewData["ReturnMessage"] = "There was an error linking your twitter account";
                    }

                    //TwitterResponse<TwitterStatus> twr = TwitterStatus.Update(tokens, "test 1 from twitterizer api!");
                    //string resp = "";
                    //resp = twr.Result.ToString();
                }
            }
            return View();
        }
        
        [HttpPost]
        public ActionResult Accounts(FormCollection collection)
        {
            
            string consumersecret = ConfigurationManager.AppSettings["twitterConsumerSecret"];
            string consumerkey = ConfigurationManager.AppSettings["twitterConsumerKey"];
            string testAccessToken = "32297508-FkO7B8EgbPqykpwcFUe7k4zAOSt4n8OntwqZYsbpM";
            string testAccessTokenSecret = "NwBvoePNooAirhTFR5pEWVAGBkcNBIdKbVjYwrzE";
            OAuthTokens tokens = new OAuthTokens();
            tokens.ConsumerKey = consumerkey;
            tokens.ConsumerSecret = consumersecret;

            //Twitter Access
            //This says to get the request token if we don't have one (should redirect to twitter.com)
            if (Request.QueryString["oauth_token"] == null)
            {
                OAuthTokenResponse requestToken = OAuthUtility.GetRequestToken(consumerkey, consumersecret, "http://localhost:2222/Settings/Accounts?twitter=1");

                Uri authenticationUri = OAuthUtility.BuildAuthorizationUri(requestToken.Token);
                Response.Redirect(authenticationUri.ToString());
            }

            return View();

        }
        */
        
        public ActionResult TwitterAccount(FormCollection collection)
        {
            string consumersecret = ConfigurationManager.AppSettings["twitterConsumerSecret"];
            string consumerkey = ConfigurationManager.AppSettings["twitterConsumerKey"];
            OAuthTokens tokens = new OAuthTokens();
            tokens.ConsumerKey = consumerkey;
            tokens.ConsumerSecret = consumersecret;
            appinfo app = utility.getAppInfo(defaultappid);
            string twitterReturnMessage = "";


            //Twitter Access
            if (app.TwitterAccessToken == null)
            {
                    //This says to get the request token if we don't have one (should redirect to twitter.com)
                    if (Request.QueryString["oauth_token"] == null)
                    {
                        OAuthTokenResponse requestToken = OAuthUtility.GetRequestToken(consumerkey, consumersecret, "http://campsoc.com/Settings/TwitterAccount");

                        Uri authenticationUri = OAuthUtility.BuildAuthorizationUri(requestToken.Token);
                        Response.Redirect(authenticationUri.ToString());
                    }
                    if (Request.QueryString["oauth_token"] != null && Request.QueryString["oauth_verifier"] != null)
                    {
                        //this gets the access tokens after the user has allowed campsoc to access their account
                        OAuthTokenResponse accessTokens = OAuthUtility.GetAccessToken(consumerkey, consumersecret, Request.QueryString["oauth_token"], Request.QueryString["oauth_verifier"]);

                        //store the access tokens as user credentials with the appinfo
                        int result = utility.updateAppTwitterTokens(defaultappid, accessTokens.Token, accessTokens.TokenSecret);

                        if (result == 1)
                        {
                            ViewData["ReturnMessageTwitter"] = "Success! Twitter account now linked!";
                        }
                        else
                        {
                            ViewData["ReturnMessageTwitter"] = "There was an error linking your twitter account";
                        }
                    }
            }
            else
            {
                //we have an access token, so this must be a request to unlink the account

                utility.updateAppTwitterTokens(defaultappid, null, null);
                twitterReturnMessage = "Your account has been unlinked. You may still need to visit <a href=\"https://twitter.com/settings/applications\">https://twitter.com/settings/applications</a> and revoke access for the 'CampSoc' application";
            }
            return RedirectToAction("Accounts");//, new {TwitterReturnMessage = twitterReturnMessage });
        }

        public ActionResult FacebookAccount(FormCollection collection)
        {
            string appId = ConfigurationManager.AppSettings["facebookAppId"];
            string appSecret = ConfigurationManager.AppSettings["facebookAppSecret"];
            string redirect_url;
            //required permissions:
            //create_event, offline_access, manage_pages
            appinfo app = utility.getAppInfo(defaultappid);
            WebClient wc = new WebClient();
            string facebookReturnMessage = "";

            if (app.FacebookAccessToken == null)
            {
                if (Request.QueryString["code"] == null)
                {
                    //first submit, this is if the user presses the 'link facebook account button'
                    //it redirects to facebooks auth dialog
                    redirect_url = "https://www.facebook.com/dialog/oauth?client_id=" + appId + "&redirect_uri=http://campsoc.com/settings/facebookaccount&scope=create_event,offline_access,manage_pages,publish_stream";
                    Response.Redirect(redirect_url);
                }
                else
                {
                    //if they accept, this is called with code set to an authorization code
                    redirect_url = "https://graph.facebook.com/oauth/access_token?";
                    redirect_url = redirect_url + "client_id=" + appId + "&redirect_uri=" + "http://campsoc.com/settings/facebookaccount" + "&";
                    redirect_url = redirect_url + "client_secret=" + appSecret + "&code=" + Request.QueryString["code"];

                    //make the request
                    string result = wc.DownloadString(redirect_url);

                    //parse the string and get the access_token
                    string access_token = "";
                    string temp;
                    int start = 0;
                    int end = 0;
                    bool gotit = false;
                    for (int i = 0; i < result.Length; i++)
                    {
                        temp = result.Substring(start, (i - start));
                        if (temp.Equals("access_token="))
                        {
                            gotit = true;
                        }
                        if (result.Substring(i, 1).Equals("&") && gotit)
                        {
                            gotit = false;
                        }
                        if (gotit)
                        {
                            access_token = access_token + result.Substring(i, 1);
                        }
                    }

                    //now get the userid
                    string wcresponse = wc.DownloadString("https://graph.facebook.com/me?access_token=" + access_token);
                    FacebookUserModel usermodel = new FacebookUserModel(JObject.Parse(wcresponse));

                    utility.updateAppFacebookUserTokens(defaultappid, access_token, usermodel.Id);


                }
            }
            else if (app.FacebookAccessToken != null && Request.QueryString["remove"] == null)
            {
                //we have a facebook access token, so this must be a request to unlink the account
                //Response.Redirect("https://www.facebook.com/settings?tab=applications");
                utility.updateAppFacebookUserTokens(defaultappid, null, null);
                utility.updateAppFacebookPageTokens(defaultappid, null, null);
                facebookReturnMessage = "Facebook account unlinked. You might still need to visit <a href=\"https://www.facebook.com/settings?tab=applications\">https://www.facebook.com/settings?tab=applications</a> and remove the 'Campus Social' application";
            }
            else if(app.FacebookAccessToken != null && Request.QueryString["remove"] == "1")
            {

            }


            return RedirectToAction("Accounts");//, new {FacebookReturnMessage = facebookReturnMessage });
        }

        public ActionResult FacebookAccountPage()
        {
            appinfo app = utility.getAppInfo(defaultappid);
            WebClient wc = new WebClient();

            if (Request.QueryString["pageName"] != null)
            {
                string url = "https://graph.facebook.com/me/accounts?access_token=" + app.FacebookAccessToken;
                string accountsresult = wc.DownloadString(url);
                JObject jtemp = JObject.Parse(accountsresult);
                JArray accounts = (JArray)jtemp["data"];
                FacebookPageModel fbpm = null;
                for (int i = 0; i < accounts.Count; i++)
                {
                    fbpm = new FacebookPageModel((JObject)accounts[i]);
                    if (fbpm.Name == Request.QueryString["pageName"])
                    {
                        break;
                    }
                }

                if (fbpm.AccessToken != null && fbpm.Id != null)
                {
                    utility.updateAppFacebookPageTokens(defaultappid, fbpm.Id, fbpm.AccessToken);
                }
            }

            return RedirectToAction("Accounts");
        }
    }
}
