using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using aspx_site.Models;

using System.Security.Cryptography;
using System.Text;

namespace aspx_site.Controllers
{
    public class WordpressController : Controller
    {
        //
        // GET: /Wordpress/
        Util utility;

        public WordpressController()
        {
            utility = new Util();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PostEvent(FormCollection collection)
        {
            utility.insertServerEvent("wordpress event post", collection["postID"], collection["user"], collection["password"]);

            ViewData["message"] = collection["postID"];
            return View("Return");
        }

        /*[HttpPost]
        public ActionResult AuthenticateUser(FormCollection collection)
        {
            string username = collection["username"];
            string password = collection["password"];

            byte[] keyBytes = Encoding.ASCII.GetBytes("alexrocks_21");
            byte[] initVectorBytes = Encoding.ASCII.GetBytes("0000000000000000");
            byte[] saltValueBytes = Encoding.ASCII.GetBytes("");
            byte[] ciphertextBytes = Encoding.ASCII.GetBytes(password);
            int keysize = 128;

            PasswordDeriveBytes pass = new PasswordDeriveBytes(

            RijndaelManaged rm = new RijndaelManaged();
            rm.Mode = CipherMode.CBC;



        }*/

    }
}
