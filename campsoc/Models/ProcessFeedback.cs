using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace aspx_site.Models
{
    public class ProcessFeedback
    {
        novamainEntities _db;
        ProcessSurveys surveymodel;

        public ProcessFeedback()
        {
            _db = new novamainEntities();
            surveymodel = new ProcessSurveys();
        }

        public List<eventfeedback> getFeedback(int appid, int count)
        {
            var selectedFeedback = (from f in _db.eventfeedbacks
                                    from s in _db.eventsurveys
                                    where f.SurveyID == s.SurveyID
                                        && s.AppID == appid
                                    orderby f.SubmitTime descending
                                    select f).Take(5).ToList();
            return selectedFeedback;
        }

        public int addFeedback(int new_AppUserID, int new_EventID, string new_QuestionOne, string new_QuestionTwo, string new_QuestionThree)
        {
            eventfeedback feedback = new eventfeedback();
            feedback.AppUserID = new_AppUserID;
            feedback.EventID = new_EventID;
            feedback.QuestionOne = new_QuestionOne;
            feedback.QuestionTwo = new_QuestionTwo;
            feedback.QuestionThree = new_QuestionThree;
            feedback.SubmitTime = DateTime.Now;
            feedback.SurveyID = surveymodel.getAttachedSurvey(new_EventID);

            try
            {
                _db.eventfeedbacks.AddObject(feedback);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
            }


            return 0;
        }

    }
}