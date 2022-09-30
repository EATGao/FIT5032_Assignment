using HealingTalkNearestYou.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using HealingTalkNearestYou.CustomSecurity;
using HealingTalkNearestYou.Util;


namespace HealingTalkNearestYou.Controllers
{
    [CustomAuthentication]
    [CustomAuthorization(UserType = "Patient")]
    public class PatientController : Controller
    {
        HTNYContainer1 htny_DB = new HTNYContainer1();
        // GET: Patient
        public ActionResult BookCounselling(string search, int? pageNumber, string sort)
        {
            var counsellings = htny_DB.CounsellingSet.AsQueryable();
            counsellings = counsellings.Where(c => c.CStatus != "Completed");
            ViewBag.SortByTime = string.IsNullOrEmpty(sort) ? "ascending time" : "";
            ViewBag.SortByPsyName = sort == "Psychologist Name" ? "descending name" : "ascending name";
            ViewBag.SortByStatus = sort == "Status" ? "descending status" : "ascending status";
            
            counsellings = counsellings.Where(c => c.Psychologist.PsyName.StartsWith(search) || search == null);

            switch (sort)
            {
                case "ascending time":
                    counsellings = counsellings.OrderBy(c => c.CDateTime);
                    break;
                case "ascending name":
                    counsellings = counsellings.OrderBy(c => c.Psychologist.PsyName);
                    break;
                case "descending name":
                    counsellings = counsellings.OrderByDescending(c => c.Psychologist.PsyName);
                    break;
                case "descending status":
                    counsellings = counsellings.OrderByDescending(c => c.CStatus);
                    break;
                case "ascending status":
                    counsellings = counsellings.OrderBy(c => c.CStatus);
                    break;
                default:
                    counsellings = counsellings.OrderByDescending(c => c.CDateTime);
                    break;
            }


            return View(counsellings.ToPagedList(pageNumber ?? 1, 10));
        }

        public ActionResult Book(int id)
        {
            // get counselling
            Counselling counselling = htny_DB.CounsellingSet.Find(id);
            // get patient user
            List<Patient> result = htny_DB.PatientSet.Where(p => p.PatientEmail == User.Identity.Name).ToList();
            Patient patient = result.FirstOrDefault();
            // change counselling status and patient
            counselling.Patient = patient;
            counselling.CStatus = "Booked";
            // save changes in database
            htny_DB.Entry<Counselling>(counselling).State = System.Data.Entity.EntityState.Modified;
            htny_DB.SaveChanges();

            //send Email to patient
            string content = "Hi, You have booked a counselling of Psychologist " + counselling.Psychologist.PsyName + " at " + counselling.CDateTime + "\n";
            EmailSender emailSender = new EmailSender();
            emailSender.Send("ygao0096@student.monash.edu", "Your Booking Result", content);

            return RedirectToAction("BookCounselling");
        }

        public ActionResult History(string search, int? pageNumber, string sort)
        {
            var counsellings = htny_DB.CounsellingSet.AsQueryable();
            counsellings = counsellings.Where(c => (c.CStatus == "Completed" || c.CStatus == "Booked") && c.Patient.PatientEmail == User.Identity.Name);
            ViewBag.SortByTime = string.IsNullOrEmpty(sort) ? "ascending time" : "";
            ViewBag.SortByPsyName = sort == "Psychologist Name" ? "descending name" : "ascending name";
            ViewBag.SortByStatus = sort == "Status" ? "descending status" : "ascending status";

            counsellings = counsellings.Where(c => c.Psychologist.PsyName.StartsWith(search) || search == null);

            switch (sort)
            {
                case "ascending time":
                    counsellings = counsellings.OrderBy(c => c.CDateTime);
                    break;
                case "ascending name":
                    counsellings = counsellings.OrderBy(c => c.Psychologist.PsyName);
                    break;
                case "descending name":
                    counsellings = counsellings.OrderByDescending(c => c.Psychologist.PsyName);
                    break;
                case "descending status":
                    counsellings = counsellings.OrderByDescending(c => c.CStatus);
                    break;
                case "ascending status":
                    counsellings = counsellings.OrderBy(c => c.CStatus);
                    break;
                default:
                    counsellings = counsellings.OrderByDescending(c => c.CDateTime);
                    break;
            }


            return View(counsellings.ToPagedList(pageNumber ?? 1, 10));
        }

        public ActionResult Rate(int id) 
        {

            ViewBag.cousellingId = id;
            return View();
        }

        [HttpPost]
        public ActionResult Rate(int cid, int scores)
        {
            Counselling counselling = htny_DB.CounsellingSet.Find(cid);
            counselling.CRate = scores;
            htny_DB.Entry<Counselling>(counselling).State = System.Data.Entity.EntityState.Modified;
            htny_DB.SaveChanges();

            return RedirectToAction("History");
        }
    }
}