using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace aspx_site.Models
{
    public class ProcessUsers
    {
        novamainEntities _db;

        public ProcessUsers()
        {
            _db = new novamainEntities();
        }

        public bool registerUser(string clientid, int appid, string handset_model, string handset_manufacturer, string handset_product)
        {

            var user = new appuser { AppID = appid, 
                HandsetID = clientid, 
                HandsetModel = handset_model, 
                HandsetManufacturer = handset_manufacturer, 
                HandsetProduct = handset_product,
                RegisterTimestamp = DateTime.Now
            };
            try
            {
                if (checkUserRegistration(clientid, appid))
                {
                    var c_user = (from u in _db.appusers
                              where (u.HandsetID == clientid && u.AppID == appid)
                              select u).FirstOrDefault();

                    var removersvps = from r in _db.rsvps
                        where (r.UserID == c_user.UserID)
                        select r;

                    foreach (var c_rsvp in removersvps)
                    {
                        _db.rsvps.DeleteObject(c_rsvp);
                    }
                    _db.appusers.DeleteObject(c_user);
                }
                _db.appusers.AddObject(user);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
               return false;
            }
            return true;
        }

        public bool checkUserRegistration(string clientid, int appid)
        {
            int n_appid;
            n_appid = appid;

            var selectedUser = from u in _db.appusers
                               where (u.AppID == n_appid & u.HandsetID == clientid)
                               select u;
            return selectedUser.Count<@appuser>() == 1;

        }

        public int getUserID(string clientid)
        {
            var selectedUser = (from u in _db.appusers
                               where (u.HandsetID == clientid)
                               select u).First();
            
            return selectedUser.UserID;
        }
    
        public List<appuser> getUsers(int appid, int count)
        {
            var selectedUsers = (from u in _db.appusers
                                 where u.AppID == appid
                                 orderby u.UserID descending
                                 select u).Take(count).ToList();
            return selectedUsers;

        }
    }
}