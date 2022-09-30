using HealingTalkNearestYou.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HealingTalkNearestYou.Controllers
{
    public class PatientController : Controller
    {
        HTNYContainer1 htny_DB = new HTNYContainer1();
        // GET: Patient
        public ActionResult BookCounselling()
        {
            IEnumerable<Counselling> allCounsellings = htny_DB.CounsellingSet;

            //IEnumerable<Counselling> uncompleteCounsellings = new List<Counselling>();
            //foreach (Counselling counselling in allCounsellings)
            //{
            //    if (!counselling.CStatus.Equals("Completed"))
            //    {
            //        uncompleteCounsellings.Append(counselling);
            //    }
            //}
            //return View(uncompleteCounsellings);
            return View(allCounsellings);
        }

        public ActionResult Book(int id)
        {
            // change the status

            return RedirectToAction("BookCounselling");
        }

        public ActionResult History()
        {
            IEnumerable<Counselling> allCounsellings = htny_DB.CounsellingSet;
            //IEnumerable<Counselling> completeCounsellings = new List<Counselling>();
            //foreach (Counselling counselling in allCounsellings)
            //{
            //    if (counselling.CStatus.Equals("Completed"))
            //    {
            //        completeCounsellings.Append(counselling);
            //    }
            //}
            //return View(completeCounsellings);
            return View(allCounsellings);
        }

        public ActionResult Rate() 
        {
            return RedirectToAction("BookCounselling");
        }
    }
}