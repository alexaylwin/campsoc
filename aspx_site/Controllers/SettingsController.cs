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

namespace aspx_site.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        //
        // GET: /Settings/

        Util utility;

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
            appinfo app = utility.getAppInfo(4);
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
            }
           
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
        
        
        /*[HttpPost] [HttpGet]*/
        public ActionResult TwitterAccount(FormCollection collection)
        {
            string consumersecret = ConfigurationManager.AppSettings["twitterConsumerSecret"];
            string consumerkey = ConfigurationManager.AppSettings["twitterConsumerKey"];
            //string testAccessToken = "32297508-FkO7B8EgbPqykpwcFUe7k4zAOSt4n8OntwqZYsbpM";
            //string testAccessTokenSecret = "NwBvoePNooAirhTFR5pEWVAGBkcNBIdKbVjYwrzE";
            OAuthTokens tokens = new OAuthTokens();
            tokens.ConsumerKey = consumerkey;
            tokens.ConsumerSecret = consumersecret;
            appinfo app = utility.getAppInfo(4);


            //Twitter Access
            //This says to get the request token if we don't have one (should redirect to twitter.com)
            if (app.TwitterAccessToken != null)
            {
                if (Request.QueryString["twitter"] != "1")
                {
                    if (Request.QueryString["oauth_token"] == null)
                    {
                        OAuthTokenResponse requestToken = OAuthUtility.GetRequestToken(consumerkey, consumersecret, "http://localhost:2222/Settings/TwitterAccount");

                        Uri authenticationUri = OAuthUtility.BuildAuthorizationUri(requestToken.Token);
                        Response.Redirect(authenticationUri.ToString());
                    }
                }
                else
                {
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
            }
            return RedirectToAction("Accounts");
        }

        /*[HttpPost][HttpGet]*/
        public ActionResult FacebookAccount(FormCollection collection)
        {
            string appId = ConfigurationManager.AppSettings["facebookAppId"];
            string appSecret = ConfigurationManager.AppSettings["facebookAppSecret"];
            string redirect_url;
            //required permissions:
            //create_event, offline_access, manage_pages

            //https://www.facebook.com/dialog/oauth?client_id=YOUR_APP_ID&redirect_uri=YOUR_URL&scope=create_event,offline_access,manage_pages

            if (Request.QueryString["code"] == null)
            {
                //first submit, this is if the user presses the 'link facebook account button'
                //it redirects to facebooks auth dialog
                redirect_url = "https://www.facebook.com/dialog/oauth?client_id=" + appId + "&redirect_uri=http://localhost:2222/settings/facebookaccount&scope=create_event,offline_access,manage_pages";
                Response.Redirect(redirect_url);
            }
            else
            {
                //if they accept, this is called with code set to an authorization code
                redirect_url = "https://graph.facebook.com/oauth/access_token?";
                redirect_url = redirect_url + "client_id=" + appId + "&redirect_uri=" + "http://localhost:2222/settings/facebookaccount" + "&";
                redirect_url = redirect_url + "client_secret=" + appSecret + "&code=" + Request.QueryString["code"];

                //make the request
                HttpWebRequest req = (HttpWebRequest) WebRequest.Create(redirect_url);
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                
                //read the result into a string
                Stream recievestream = resp.GetResponseStream();
                StreamReader readstream = new StreamReader(recievestream, Encoding.UTF8);
                string result = readstream.ReadToEnd();

                //parse the string and get the access_token
                string access_token = "";
                string temp;
                int start = 0;
                int end = 0;
                bool gotit = false;
                for(int i = 0; i < result.Length; i++)
                {
                    temp = result.Substring(start, (i-start));
                    if(temp.Equals("access_token="))
                    {
                        gotit = true;
                    }
                    if(result.Substring(i, 1).Equals("&") && gotit)
                    {
                        gotit = false;
                    }
                    if(gotit)
                    {
                        access_token = access_token + result.Substring(i, 1);
                    }
                }

                utility.updateAppFacebookTokens(4, access_token);


            }

            return RedirectToAction("Accounts");
        }

    }
}
