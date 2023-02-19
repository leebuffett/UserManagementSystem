using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment.Models;
using System.Net;
using System.Web.Security;
using System.IO;
using System.Data.Entity;


namespace Assignment.Controllers
{
    public class LoginController : Controller
    {

        private Database1Entities2 db = new Database1Entities2();

        public ActionResult Index(bool? multipleSession)
        {
            if (multipleSession == true)
            {
                ViewBag.ErrorMessage = "Logged out due to multiple login detected";
            }
            return View();
        }

        [HttpPost]
        public JsonResult ValidateLogin(string username, string password)
        {
            string sessionId= System.Web.HttpContext.Current.Session.SessionID;
            string result = DBManage.GetLoginInfo(username, password, sessionId);

            return Json(new { result = result, url = Url.Action("Index", "User") });
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            DBManage.Logout(User.Identity.Name);

            return RedirectToAction("Index", "Login");
        }

        
    }
}