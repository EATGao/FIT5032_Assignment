using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HealingTalkNearestYou.CustomSecurity;
using HealingTalkNearestYou.Models;
using PagedList;

namespace HealingTalkNearestYou.Controllers
{
    [CustomAuthentication]
    [CustomAuthorization(UserType = "Psychologist")]
    public class PsychologistController : Controller
    {
        HTNYContainer1 htny_DB = new HTNYContainer1();

        // GET: Psychologist
        public ActionResult ManageCounselling(string search, int? pageNumber, string sort)
        {
            ViewBag.SortByTime = string.IsNullOrEmpty(sort) ? "ascending time" : "";
            ViewBag.SortByStatus = sort == "Status" ? "descending status" : "ascending status";

            var counsellings = htny_DB.CounsellingSet.AsQueryable();
            // add more search later
            counsellings = counsellings.Where(x => x.Patient.PatientFirstName.StartsWith(search) || search == null);
            switch (sort)
            {
                case "ascending time":
                    counsellings = counsellings.OrderBy(x => x.CDateTime);
                    break;

                case "descending status":
                    counsellings = counsellings.OrderByDescending(x => x.CStatus);
                    break;
                case "ascending status":
                    counsellings = counsellings.OrderBy(x => x.CStatus);
                    break;
                default:
                    counsellings = counsellings.OrderByDescending(x => x.CDateTime);
                    break;

            }

            return View(counsellings.ToPagedList(pageNumber ?? 1, 10));
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

        public ActionResult Edit(int id)
        { 
            Counselling counselling = htny_DB.CounsellingSet.Find(id);
            var selecStatusList = new List<SelectListItem>() {
                new SelectListItem() { Value = "Not Booked", Text = "Not Booked" },
                new SelectListItem() { Value = "Booked", Text = "Booked" },
                new SelectListItem() { Value = "Completed", Text = "Completed" }
            };
            ViewBag.StatusOptions = selecStatusList;
            return View(counselling);
        }

        [HttpPost]
        public ActionResult Edit(Counselling counselling)
        {
            try
            {
                htny_DB.Entry<Counselling>(counselling).State = System.Data.Entity.EntityState.Modified;
                htny_DB.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                Console.Write(dbEx);
            }
            return RedirectToAction("ManagePatient");
        }

        public ActionResult DeletePatient(int id)
        {
            Counselling counselling = htny_DB.CounsellingSet.Find(id);
            if (counselling == null)
            {
                return RedirectToAction("ManageCounselling");
            }
            else
            {
                htny_DB.CounsellingSet.Remove(counselling);
                htny_DB.SaveChanges();
            }
            return RedirectToAction("ManageCounselling");
        }

        public ActionResult Details(int id)
        {
            Counselling counselling = htny_DB.CounsellingSet.Find(id);
            return View(counselling);
        }


    }
}