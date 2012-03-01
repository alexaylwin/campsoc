using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using aspx_site.Models;

namespace aspx_site.Controllers
{
    public class SurveysController : BaseController
    {

        // GET: /Survey/

        //novamainEntities _db;
        //int defaultappid = 4;
        //Util utility;
        ProcessSurveys surveymodel;

        public SurveysController() :base()
        {
            //_db = new novamainEntities();
            //utility = new Util();
            surveymodel = new ProcessSurveys();
        }
 
        public ActionResult Index()
        {
            return RedirectToAction("Home");
        }

        public ActionResult Home()
        {
            return View();
        }
        public ActionResult Create()
        {
            var selectedEvents = (from e in _db.novaevents
                                  where e.AppID == defaultappid
                                  orderby e.EventStart descending
                                  select e);
            ViewData["eventlist"] = selectedEvents.Take(10).ToList();

            return View();
        }


        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            appinfo app = utility.getAppInfo(defaultappid);
            eventsurvey newsurvey = new eventsurvey();

            if (collection["QuestionInclude1"].Contains("true") && collection["QuestionText1"].Length > 0)
            {
                newsurvey.QuestionOne = collection["QuestionText1"];
                if (collection["QuestionMC1"].Contains("true"))
                {
                    if (collection["QuestionOptionInclude1_1"].Contains("true"))
                    {
                        newsurvey.QuestionOneMC1 = collection["QuestionOptionText1_1"];
                    }
                    if (collection["QuestionOptionInclude1_2"].Contains("true"))
                    {
                        newsurvey.QuestionOneMC2 = collection["QuestionOptionText1_2"];
                    }
                    if (collection["QuestionOptionInclude1_3"].Contains("true"))
                    {
                        newsurvey.QuestionOneMC3 = collection["QuestionOptionText1_3"];
                    }
                }
            }
            if (collection["QuestionInclude2"].Contains("true") && collection["QuestionText2"].Length > 0)
            {
                newsurvey.QuestionTwo = collection["QuestionText2"];
                if (collection["QuestionMC2"].Contains("true"))
                {
                    if (collection["QuestionOptionInclude2_1"].Contains("true"))
                    {
                        newsurvey.QuestionTwoMC1 = collection["QuestionOptionText2_1"];
                    }
                    if (collection["QuestionOptionInclude2_2"].Contains("true"))
                    {
                        newsurvey.QuestionTwoMC2 = collection["QuestionOptionText2_2"];
                    }
                    if (collection["QuestionOptionInclude2_3"].Contains("true"))
                    {
                        newsurvey.QuestionTwoMC3 = collection["QuestionOptionText2_3"];
                    }
                }
            }
            if (collection["QuestionInclude3"].Contains("true") && collection["QuestionText3"].Length > 0)
            {
                newsurvey.QuestionThree = collection["QuestionText3"];
                if (collection["QuestionMC3"].Contains("true"))
                {
                    if (collection["QuestionOptionInclude3_1"].Contains("true"))
                    {
                        newsurvey.QuestionThreeMC1 = collection["QuestionOptionText3_1"];
                    }
                    if (collection["QuestionOptionInclude3_2"].Contains("true"))
                    {
                        newsurvey.QuestionThreeMC2 = collection["QuestionOptionText3_2"];
                    }
                    if (collection["QuestionOptionInclude3_3"].Contains("true"))
                    {
                        newsurvey.QuestionThreeMC3 = collection["QuestionOptionText3_3"];
                    }
                }
            }

            newsurvey.SurveyID = surveymodel.getMaxSurveyID() + 1;
            newsurvey.AppID = app.AppID;
            if (collection["SurveyName"] != null)
            {
                newsurvey.SurveyName = collection["SurveyName"];
            }
            else
            {
                newsurvey.SurveyName = Convert.ToString(newsurvey.SurveyID);
            }
            surveymodel.addSurvey(newsurvey);

            return RedirectToAction("Home");


        }

    }
}
