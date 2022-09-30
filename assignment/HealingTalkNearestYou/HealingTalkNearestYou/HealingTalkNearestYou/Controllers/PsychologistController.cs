using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HealingTalkNearestYou.CustomSecurity;
using HealingTalkNearestYou.Models;

namespace HealingTalkNearestYou.Controllers
{
    [CustomAuthentication]
    [CustomAuthorization(UserType = "Psychologist")]
    public class PsychologistController : Controller
    {
        HTNYContainer1 htny_DB = new HTNYContainer1();

        // GET: Psychologist
        public ActionResult ManageCounselling()
        {
            IEnumerable<Counselling> counsellings = htny_DB.CounsellingSet;
            return View(counsellings);
        }

        public ActionResult CreateCounselling()
        {
            var selecStatusList = new List<SelectListItem>() {
                new SelectListItem() { Value = "Not Booked", Text = "Not Booked" },
                new SelectListItem() { Value = "Booked", Text = "Booked" },
                new SelectListItem() { Value = "Completed", Text = "Completed" }
            };
            ViewBag.StatusOptions = selecStatusList;
            return View();
        }

        [HttpPost]
        public ActionResult CreateCounselling(Counselling counselling)
        {
            if (ModelState.IsValid)
            {
                htny_DB.CounsellingSet.Add(counselling);
                htny_DB.SaveChanges();
            }
            return RedirectToAction("ManageCounselling");
        }


    }
}