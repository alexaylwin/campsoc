﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using aspx_site.Models;

namespace aspx_site.Models
{

    public class ProcessEvents
    {
        novamainEntities _db;

        public ProcessEvents()
        {
            _db = new novamainEntities();
        }

        public List<@novaevent> checkEvents(int appid, DateTime lastupdated)
        {
            var selectedEvents = from e in _db.novaevents 
                where (e.AppID == appid && e.LastUpdated > lastupdated)
                select e;

            return selectedEvents.ToList();
        }

        public int addEvent(int appid, novaevent newevent)
        {
            try
            {
                //create the meta object for this event
                objectmeta obj = new objectmeta();
                obj.TimesViewedApp = 0;
                obj.TimesViewedWeb = 0;
                obj.ObjectID = newevent.EventID;
                obj.ObjectType = 1;
                obj.FacebookImpressions = 0;
                obj.FacebookRSVPs = 0;

                _db.novaevents.AddObject(newevent);
                _db.objectmetas.AddObject(obj);

                _db.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public int addEvent(int appid, string desc, string name, DateTime start, DateTime end,
            int notattending, int attending, string location, int disabled, int survey)
        { return 0; }

        public int updateEvent(int appid, novaevent updatedevent)
        {
            try
            {
                var selected_event = (from e in _db.novaevents
                                      where (e.AppID == appid && e.EventID == updatedevent.EventID)
                                      select e).First();
                selected_event = updatedevent;
                _db.novaevents.ApplyCurrentValues(selected_event);
                _db.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public int getMaxEventID()
        {
            var selectedEvents = from e in _db.novaevents
                                  //where e.AppID == 4
                                  select e.EventID;
            return selectedEvents.Max();
        }

        public DateTime getLastEventUpdate(int appid)
        {
            try
            {

                var selected_update = (from su in _db.lastupdates
                                       where (su.AppID == appid)
                                       select su).FirstOrDefault();
                return selected_update.LastEventUpdate;
            }
            catch (Exception e)
            {
                return new DateTime(1900, 1, 1);
            }

        }

        public int setLastEventUpdate(int appid, DateTime newtime)
        {
            try
            {
                var selected_update = (from su in _db.lastupdates
                                       where (su.AppID == appid)
                                       select su).FirstOrDefault();
                selected_update.LastEventUpdate = newtime;
                _db.lastupdates.ApplyCurrentValues(selected_update);
                _db.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }

        }

        public int getEventID(string syncid)
        {
            
            var selectedEvents = (from e in _db.novaevents
                                 where (e.SyncID == syncid)
                                 select e).First();
            return selectedEvents.EventID;
        }

        public List<novaevent> getEvents(int appid, int count)
        {
            var selectedEvents = (from e in _db.novaevents
                                  where e.AppID == appid
                                  orderby e.EventStart descending
                                  select e).Take(count).ToList();
            return selectedEvents;
           
        }

        public novaevent getEvent(int eventid)
        {
            var selectedEvent = (from events in _db.novaevents
                                 where events.EventID == eventid
                                 select events).First();
            return selectedEvent;
        }
        
    }
}