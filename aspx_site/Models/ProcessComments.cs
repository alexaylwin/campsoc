using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using aspx_site.Models;

namespace aspx_site.Models
{
    public class ProcessComments
    {
        novamainEntities _db;

        public ProcessComments()
        {
            _db = new novamainEntities();
        }

        public bool insertComment(int userid, int eventid, string comment)
        {
            comment new_c = new comment { UserID = userid, EventID = eventid, Comment1 = comment, timestamp = DateTime.Now};
            _db.comments.AddObject(new_c);
            _db.SaveChanges();
            return true;

        }
    }
}