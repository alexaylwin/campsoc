using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using aspx_site.Models;

namespace aspx_site.Models
{
    public class ProcessRSVPs
    {
        novamainEntities _db;

        public ProcessRSVPs()
        {
            _db = new novamainEntities();
        }

        public bool insertRSVP(int userid, int eventid, string rsvp)
        {
            int n_userid = userid;
            int n_eventid = eventid;
            var selected_r = (from r in _db.rsvps
                     where r.UserID == n_userid && r.EventID == n_eventid
                     select r).FirstOrDefault();
            try
            {
                if (selected_r == null)
                {
                    rsvp new_r = new rsvp { UserID = n_userid, EventID = n_eventid, Rsvp1 = rsvp };
                    _db.rsvps.AddObject(new_r);
                }
                else
                {
                    selected_r.Rsvp1 = rsvp;
                }
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}