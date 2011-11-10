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

    }
}