using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using aspx_site.Models;

namespace aspx_site.Models
{
    public class Util
    {
        novamainEntities _db;
        public Util()
        {
            _db = new novamainEntities();
        }

        public int getAppID(string appid)
        {
            var selected_app = (from a in _db.appinfoes
                               where (a.HashedAppID == appid)
                               select a).FirstOrDefault();

            if (selected_app == null)
            {
                return 0;
            }
            
            return selected_app.AppID;
        }

        public string getHomeText(int appid)
        {
            var selected_app = (from a in _db.appinfoes
                                where (a.AppID == appid)
                                select a).FirstOrDefault();

            if (selected_app == null)
            {
                return null;
            }

            return selected_app.HomeScreenText;
        }

        public int insertBetaEvent(string event_desc, string submitted_by, string col1, string col2, string col3, string col4, string col5)
        {
            try
            {
                beta_events eventToAdd = new beta_events();
                eventToAdd.event_desc = event_desc;
                eventToAdd.submitted_by = submitted_by;
                eventToAdd.timestamp = DateTime.Now;
                eventToAdd.col1 = col1;
                eventToAdd.col2 = col2;
                eventToAdd.col3 = col3;
                eventToAdd.col4 = col4;
                eventToAdd.col5 = col5;

                _db.beta_events.AddObject(eventToAdd);
                _db.SaveChanges();
            }
            catch
            {
                return 0;
            }

            return 1;
        }

        public int insertServerEvent(string event_desc, string col1 = "", string col2 = "", string col3 = "", string col4 = "", string col5 = "", string col6 = "", string col7 = "")
        {
            try
            {
                server_logs newentry = new server_logs();
                newentry.col1 = col1;
                newentry.col2 = col2;
                newentry.col3 = col3;
                newentry.col4 = col4;
                newentry.col5 = col5;
                newentry.event_desc = event_desc;
                newentry.timestamp = DateTime.Now;

                _db.server_logs.AddObject(newentry);
                _db.SaveChanges();
            }
            catch
            {
                return 0;
            }
            return 1;
        }

        public int updateAppTwitterTokens(int appid, string accessToken, string accessTokenSecret)
        {
            try
            {
                var selected_app = (from sa in _db.appinfoes
                                    where (sa.AppID == appid)
                                    select sa).First();

                selected_app.TwitterAccessToken = accessToken;
                selected_app.TwitterAccessTokenSecret = accessTokenSecret;
                _db.appinfoes.ApplyCurrentValues(selected_app);
                _db.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public appinfo getAppInfo(int appid)
        {
            try
            {
                var selected_app = (from sa in _db.appinfoes
                                    where (sa.AppID == appid)
                                    select sa).First();
                return selected_app;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public DateTime getHomeScreenUpdate(int appid)
        {
            try
            {
                var selected_update = (from su in _db.lastupdates
                                       where (su.AppID == appid)
                                       select su).FirstOrDefault();
                return selected_update.LastHomescreenUpdate;
            }
            catch (Exception e)
            {
                return new DateTime(1900, 1, 1);
            }
        }
    }
}