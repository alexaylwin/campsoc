using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using aspx_site.Models;

namespace aspx_site.Models
{
    public class ProcessSurveys
    {
        novamainEntities _db;

        public ProcessSurveys()
        {
            _db = new novamainEntities();
        }

        public int addSurvey(eventsurvey newsurvey)
        {
            try
            {
                _db.eventsurveys.AddObject(newsurvey);
                _db.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public int getMaxSurveyID()
        {
            var selectedSurveys = from s in _db.eventsurveys
                                 select s.SurveyID;
            if(selectedSurveys.Max() == null)
            {
                return 0;
            }
            return selectedSurveys.Max();
        }

        public int getAttachedSurvey(int eventid)
        {
            var selectedEvents = (from e in _db.novaevents
                                  where (e.EventID == eventid)
                                  select e).First();
            return (int)selectedEvents.Survey;
        }

    }
}