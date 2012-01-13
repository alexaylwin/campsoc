using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Twitterizer;
using aspx_site.Models;

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
        
        
        [HttpPost] [HttpGet]
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
                        OAuthTokenResponse requestToken = OAuthUtility.GetRequestToken(consumerkey, consumersecret, "http://localhost:2222/Settings/Accounts?twitter=1");

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



    }
}
