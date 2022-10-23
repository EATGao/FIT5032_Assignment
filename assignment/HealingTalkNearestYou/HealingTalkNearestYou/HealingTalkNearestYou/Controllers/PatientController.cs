﻿using HealingTalkNearestYou.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using HealingTalkNearestYou.Util;
using System.Data.Entity;
using System;
using System.Web.Security;
using Microsoft.AspNet.Identity;

namespace HealingTalkNearestYou.Controllers
{
    [Authorize(Roles = "Patient")]
    public class PatientController : Controller
    {
        private ApplicationDbContext htny_DB = new ApplicationDbContext();
        private CounsellingManager counsellingManager = new CounsellingManager();
      
        
        // GET: Patient
        public ActionResult BookCounselling(string search, int? pageNumber, string sort)
        {
            counsellingManager.CleanPassedCounselling();
            var counsellings = htny_DB.Counsellings.AsQueryable();
            counsellings = counsellings.Where(c => c.CStatus != "Completed");
            ViewBag.SortByTime = string.IsNullOrEmpty(sort) ? "descending time" : "";
            ViewBag.SortByPsyName = sort == "ascending name" ? "descending name" : "ascending name";

            counsellings = counsellings.Where(c => c.Psychologist.Name.StartsWith(search) || search == null);

            switch (sort)
            {
                case "descending time":
                    counsellings = counsellings.OrderByDescending(c => c.CDateTime);
                    break;
                case "ascending name":
                    counsellings = counsellings.OrderBy(c => c.Psychologist.Name);
                    break;
                case "descending name":
                    counsellings = counsellings.OrderByDescending(c => c.Psychologist.Name);
                    break;
                case "descending status":
                    counsellings = counsellings.OrderByDescending(c => c.CStatus);
                    break;
                case "ascending status":
                    counsellings = counsellings.OrderBy(c => c.CStatus);
                    break;
                default:
                    counsellings = counsellings.OrderBy(c => c.CDateTime);
                    break;
            }


            return View(counsellings.ToPagedList(pageNumber ?? 1, 10));
        }

        [HttpPost]
        public ActionResult BookCounselling(int id)
        {
            // get counselling
            Counselling counselling = htny_DB.Counsellings.Find(id);
            Counselling notAvailable = counsellingManager.CheckAvailable(counselling.CDateTime, User.Identity.Name, "Patient");
            if (notAvailable == null)
            {
                // get patient user
                List<ApplicationUser> result = htny_DB.Users.Where(p => p.Email == User.Identity.Name).ToList();
                ApplicationUser patient = result.FirstOrDefault();
                // change counselling status and patient
                counselling.Patient = patient;
                counselling.CStatus = "Booked";
                patient.Counsellings.Add(counselling);
                // save changes in database
                htny_DB.Entry(counselling).State = EntityState.Modified;
                htny_DB.Entry(patient).State = EntityState.Modified;
                htny_DB.SaveChanges();

                //send Email to patient
                string content = "Hi, " + patient.Name + ". You have booked a counselling of at " + counselling.CDateTime + "\n";
                EmailSender emailSender = new EmailSender();
                emailSender.Send(patient.Email, "Your Booking Result", content);

                return Content("Successful!");
            }
            else
            { 
                return Content("Book fail. \nYou already have a counselling from " + notAvailable.CDateTime + " to " + notAvailable.CEndDateTime
                    + ". \nPlease choose another time.");
            }
        }

        public ActionResult CancelBooking(int id)
        {
            // get counselling
            Counselling counselling = htny_DB.Counsellings.Find(id);

            // change counselling status and patient
            counselling.Patient = null;
            counselling.CStatus = "Not Booked";
            List<ApplicationUser> result = htny_DB.Users.Where(p => p.Email == User.Identity.Name).ToList();
            ApplicationUser patient = result.FirstOrDefault();
            patient.Counsellings.Remove(counselling);
            // save changes in database
            htny_DB.Entry(counselling).State = EntityState.Modified;
            htny_DB.Entry(patient).State = EntityState.Modified;
            htny_DB.SaveChanges();

            return RedirectToAction("BookCounselling");
        }


        public ActionResult History(string search, int? pageNumber, string sort)
        {
            var counsellings = htny_DB.Counsellings.AsQueryable();
            counsellings = counsellings.Where(c => (c.CStatus == "Completed" || c.CStatus == "Booked") && c.Patient.Email == User.Identity.Name);
            ViewBag.SortByTime = string.IsNullOrEmpty(sort) ? "ascending time" : "";
            ViewBag.SortByPsyName = sort == "Psychologist Name" ? "descending name" : "ascending name";
            ViewBag.SortByStatus = sort == "Status" ? "descending status" : "ascending status";

            counsellings = counsellings.Where(c => c.Psychologist.Name.StartsWith(search) || search == null);

            switch (sort)
            {
                case "ascending time":
                    counsellings = counsellings.OrderBy(c => c.CDateTime);
                    break;
                case "ascending name":
                    counsellings = counsellings.OrderBy(c => c.Psychologist.Name);
                    break;
                case "descending name":
                    counsellings = counsellings.OrderByDescending(c => c.Psychologist.Name);
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
            Counselling counselling = htny_DB.Counsellings.Find(cid);
            counselling.CRate = scores;
            htny_DB.Entry<Counselling>(counselling).State = System.Data.Entity.EntityState.Modified;
            htny_DB.SaveChanges();

            return RedirectToAction("History");
        }

        [HttpGet]
        public FileResult CheckFeedback(int id)
        {
            UploadFile file = htny_DB.UploadFiles.Find(id);

            return File(file.FileContent, "application/pdf", file.FileName);
        }
    }
}