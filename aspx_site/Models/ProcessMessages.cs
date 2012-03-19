using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using aspx_site.Models;

namespace aspx_site.Models
{
    public class ProcessMessages
    {
        novamainEntities _db;

        public ProcessMessages()
        {
            _db = new novamainEntities();
        }

        public List<@message> checkMessages(int appid, string clientid, DateTime lastupdated)
        {
            var selectedMessages = from m in _db.messages
                where (m.AppID == appid & m.MessageDate > lastupdated)
                select m;

            return selectedMessages.ToList();
        }

        public DateTime getLastMessageUpdate(int appid)
        {
            try
            {
            var selected_update = (from su in _db.lastupdates
                                   where (su.AppID == appid)
                                   select su).FirstOrDefault();

            return selected_update.LastMessageUpdate;
                        }
            catch (Exception e)
            {
                return new DateTime(1900, 1, 1);
            }

        }

        public int getMaxMessageID()
        {
            var selectedMessages = from e in _db.messages
                                 where e.AppID == 4
                                 select e.MessageID;
            return selectedMessages.Max();
        }

        public int addMessage(int appid, message newmessage)
        {
            newmessage.AppID = appid;
            try
            {
                objectmeta obj = new objectmeta();
                obj.TimesViewedApp = 0;
                obj.TimesViewedWeb = 0;
                obj.ObjectID = newmessage.MessageID;
                obj.ObjectType = 2;
                obj.FacebookImpressions = 0;
                obj.FacebookRSVPs = 0;

                _db.messages.AddObject(newmessage);
                _db.objectmetas.AddObject(obj);
                _db.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public int setLastMessageUpdate(int appid, DateTime newtime)
        {
            try
            {
                var selected_update = (from su in _db.lastupdates
                                       where (su.AppID == appid)
                                       select su).FirstOrDefault();
                selected_update.LastMessageUpdate = newtime;
                _db.lastupdates.ApplyCurrentValues(selected_update);
                _db.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                return -1;
            }

        }

        public List<message> getMessages(int appid, int count)
        {
            var selectedMessages = (from m in _db.messages
                                    where m.AppID == appid
                                    orderby m.MessageDate descending
                                    select m).Take(count).ToList();
            return selectedMessages;

        }

        public message getMessage(int messageid)
        {
            var selectedMessage = (from m in _db.messages
                                   where m.MessageID == messageid
                                   select m).First();
            return selectedMessage;
        }
        
    
    }




}