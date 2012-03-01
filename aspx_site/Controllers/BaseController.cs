using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using aspx_site.Models;

namespace aspx_site.Controllers
{

    public class BaseController : Controller
    {
        public novamainEntities _db;
        public ProcessEvents eventmodel;
        public ProcessMessages messagesmodel;
        public ProcessUsers usersmodel;
        public ProcessFeedback feedbackmodel;

        public int defaultappid = 4;
        public Util utility;

        public BaseController()
        {
            _db = new novamainEntities();
            eventmodel = new ProcessEvents();
            messagesmodel = new ProcessMessages();
            usersmodel = new ProcessUsers();
            feedbackmodel = new ProcessFeedback();
            utility = new Util();
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            defaultappid = (int) requestContext.HttpContext.Session["appid"];
        }


    }
}
