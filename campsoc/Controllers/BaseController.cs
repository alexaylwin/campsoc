using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using aspx_site.Models;

namespace aspx_site.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public novamainEntities _db;
        public ProcessEvents eventmodel;
        public ProcessMessages messagesmodel;
        public ProcessUsers usersmodel;
        public ProcessFeedback feedbackmodel;
        public ProcessObjectMeta metamodel;

        public int defaultappid = 5;
        public Util utility;
        
        
        public BaseController()
        {
            _db = new novamainEntities();
            eventmodel = new ProcessEvents();
            messagesmodel = new ProcessMessages();
            usersmodel = new ProcessUsers();
            feedbackmodel = new ProcessFeedback();
            metamodel = new ProcessObjectMeta();
            utility = new Util();
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            //requestContext.HttpContext.Session.
            defaultappid = utility.getCustomerAppID(this.User.Identity.Name);
            if (defaultappid == 5)
            {
                HttpCookie tourcookie = new HttpCookie("ontour", "1");
                Response.SetCookie(tourcookie);
            }
            else
            {
                if (Request.Cookies["ontour"] != null)
                {
                    HttpCookie tourcookie = new HttpCookie("ontour");
                    tourcookie.Expires = DateTime.Now.AddDays(-1d);
                    Response.Cookies.Add(tourcookie);
                }
            }
        }


    }
}
