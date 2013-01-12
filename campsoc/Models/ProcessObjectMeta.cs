using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using aspx_site.Models;

namespace aspx_site.Models
{
    public class ProcessObjectMeta
    {
       novamainEntities _db;

        public ProcessObjectMeta()
        {
            _db = new novamainEntities();
        }

        public objectmeta getObjectMeta(int objectid, int objecttype)
        {
            objectmeta obj = (from o in _db.objectmetas
                              where o.ObjectID == objectid && o.ObjectType == objecttype
                              select o).FirstOrDefault();

            return obj;

        }

        public int addObjectViewedApp(int objectid, int objecttype)
        {
            objectmeta obj = getObjectMeta(objectid, objecttype);
            obj.TimesViewedApp = obj.TimesViewedApp + 1;

            try
            {
                _db.objectmetas.ApplyCurrentValues(obj);
                _db.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                return -1;
            }
            
        }

        public int addObjectViewedWeb(int objectid, int objecttype)
        {
            objectmeta obj = getObjectMeta(objectid, objecttype);
            obj.TimesViewedWeb = obj.TimesViewedWeb + 1;
            try
            {
                _db.objectmetas.ApplyCurrentValues(obj);
                _db.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                return -1;
            }

        }
    }
}