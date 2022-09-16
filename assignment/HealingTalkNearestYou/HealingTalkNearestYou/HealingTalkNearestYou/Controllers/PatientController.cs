using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HealingTalkNearestYou.Controllers
{
    public class PatientController : Controller
    {
        // GET: Patient
        public ActionResult BookCounselling()
        {
            return View();
        }
    }
}