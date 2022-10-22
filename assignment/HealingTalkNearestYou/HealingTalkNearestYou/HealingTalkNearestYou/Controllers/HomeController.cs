using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HealingTalkNearestYou.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Healing Talk Nearest You";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Healing Talk Nearest You";

            return View();
        }

        public ActionResult Map()
        {
            return View();
        }
    }
}