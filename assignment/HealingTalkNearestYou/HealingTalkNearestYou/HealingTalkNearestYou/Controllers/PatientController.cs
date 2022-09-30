using HealingTalkNearestYou.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace HealingTalkNearestYou.Controllers
{
    public class PatientController : Controller
    {
        HTNYContainer1 htny_DB = new HTNYContainer1();
        // GET: Patient
        public ActionResult BookCounselling(string search, int? pageNumber, string sort)
        {
            var counsellings = htny_DB.CounsellingSet.AsQueryable();
            ViewBag.SortByTime = string.IsNullOrEmpty(sort) ? "ascending time" : "";
            ViewBag.SortByPsyName = sort == "Psychologist Name" ? "descending name" : "ascending name";
            ViewBag.SortByStatus = sort == "Status" ? "descending status" : "ascending status";

            counsellings = counsellings.Where(c => c.Psychologist.PsyName.StartsWith(search) || search == null);

            switch (sort)
            {
                case "ascending time":
                    counsellings.OrderBy(c => c.CDateTime);
                    break;
                case "ascending name":
                    counsellings.OrderBy(c => c.Psychologist.PsyName);
                    break;
                case "descending name":
                    counsellings.OrderByDescending(c => c.Psychologist.PsyName);
                    break;
                case "descending status":
                    counsellings = counsellings.OrderByDescending(c => c.CStatus);
                    break;
                case "ascending status":
                    counsellings = counsellings.OrderBy(c => c.CStatus);
                    break;
                default:
                    counsellings.OrderByDescending(c => c.CDateTime);
                    break;
            }


            return View(counsellings.ToPagedList(pageNumber ?? 1, 10));
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