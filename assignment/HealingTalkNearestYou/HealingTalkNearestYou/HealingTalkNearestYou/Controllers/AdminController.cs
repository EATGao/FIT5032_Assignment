using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HealingTalkNearestYou.CustomSecurity;

namespace HealingTalkNearestYou.Controllers
{
    [CustomAuthentication]
    [CustomAuthorization(UserType = "Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult ManagePatient()
        {
            return View();
        }

        public ActionResult ManagePsychologist()
        {
            return View();
        }
    }
}