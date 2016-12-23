using LMT_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LMT_MVC.Controllers
{
    
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.CurrentUserName = CommonMethods.CurrentUserName;
            ViewBag.CurrentUserType = CommonMethods.CurrentUserType;
            return View();
        }
        
        public ActionResult Calendar()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login(string email, string password)
        {
            DCDataContext DC = new DCDataContext();
            var user = (from a in DC.Innovation_Management_Users where a.user_email == email && a.user_password == password && a.user_isdeleted == false select a).FirstOrDefault();
            if (user != null)
            {
                int usertype = 0;
                if ((bool)user.user_isadmin)
                {
                    usertype = 1;
                }
                FormsAuthentication.RedirectFromLoginPage(user.user_name + "|" + user.pk_user_id + "|" + usertype + "|" + user.fk_assigned_country, false);
            }
            return View();
        }
        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            FormsAuthentication.RedirectToLoginPage();
            return View("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}