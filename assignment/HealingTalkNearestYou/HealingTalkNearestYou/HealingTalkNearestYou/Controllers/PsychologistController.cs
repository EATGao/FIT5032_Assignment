using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using HealingTalkNearestYou.Models;
using HealingTalkNearestYou.Util;
using PagedList;

namespace HealingTalkNearestYou.Controllers
{
    [Authorize(Roles = "Psychologist")]
    public class PsychologistController : Controller
    {
        ApplicationDbContext htny_DB = new ApplicationDbContext();
        CounsellingManager counsellingManager = new CounsellingManager();

        // GET: Psychologist
        public ActionResult ManageCounselling(string search, int? pageNumber, string sort)
        {
            counsellingManager.cleanPassedCounselling();
            ViewBag.SortByTime = string.IsNullOrEmpty(sort) ? "" : "ascending time";
            ViewBag.SortByStatus = sort == "Status" ? "descending status" : "ascending status";

            var counsellings = htny_DB.Counsellings.AsQueryable();
            counsellings = counsellings.Where(c => c.Psychologist.Email == User.Identity.Name);
            counsellings = counsellings.Where(c => c.Patient.Name.StartsWith(search) || search == null);
            switch (sort)
            {
                case "ascending time":
                    counsellings = counsellings.OrderBy(c => c.CDateTime);
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
        public ActionResult CreateCounselling(String dateTime)
        {
            if (ModelState.IsValid)
            {
                CultureInfo culture = CultureInfo.CurrentCulture;
                DateTime cDateTime = DateTime.Parse(dateTime, culture);
                Counselling counselling = new Counselling();
                counselling.CDateTime = cDateTime;
                counselling.CStatus = "Not Booked";
                var psys = htny_DB.Users.AsQueryable();
                List<ApplicationUser> psy = psys.Where(p => p.Email == User.Identity.Name).ToList();
                counselling.Psychologist = psy.FirstOrDefault();
                htny_DB.Counsellings.Add(counselling);
                htny_DB.SaveChanges();
            }
            
            return RedirectToAction("ManageCounselling");

        }

        public ActionResult Edit(int id)
        {
            Counselling counselling = htny_DB.Counsellings.Find(id);
            var selecStatusList = new List<SelectListItem>() {
                new SelectListItem() { Value = "Not Booked", Text = "Not Booked" },
                new SelectListItem() { Value = "Booked", Text = "Booked" },
            };
            
            return View(counselling);
        }

        [HttpPost]
        public ActionResult Edit(Counselling counselling)
        {
            try
            {
                htny_DB.Entry(counselling).State = EntityState.Modified;
                htny_DB.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                Console.Write(dbEx);
            }
            return RedirectToAction("ManageCounselling");
        }

        public ActionResult Delete(int id)
        {
            Counselling counselling = htny_DB.Counsellings.Find(id);
            if (counselling == null)
            {
                return RedirectToAction("ManageCounselling");
            }
            else
            {
                htny_DB.Counsellings.Remove(counselling);
                htny_DB.SaveChanges();
            }
            return RedirectToAction("ManageCounselling");
        }

        public ActionResult Details(int id)
        {
            Counselling counselling = htny_DB.Counsellings.Find(id);

            ViewBag.psyName = counselling.Psychologist.Name;
            if (counselling.Patient != null)
            {
                ViewBag.patientName = counselling.Patient.Name;
            }
            else 
            {
                ViewBag.patientName = "-1";
            }
            return View(counselling);
        }


    }
}