using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace knowledgeBase.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            string username = User.Identity.Name;
            if (username.IndexOf('\\') > 0)
            {
                username = username.Substring(username.IndexOf('\\') + 1);
            }
            //return username?.ToLower();

            return View();
        }
    }
}
