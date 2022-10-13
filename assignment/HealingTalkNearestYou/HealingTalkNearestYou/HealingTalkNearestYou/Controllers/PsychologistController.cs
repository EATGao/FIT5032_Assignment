using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HealingTalkNearestYou.Models;
using HealingTalkNearestYou.Util;
using PagedList;
using UploadFile = HealingTalkNearestYou.Models.UploadFile;

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
            ViewBag.SortByTime = string.IsNullOrEmpty(sort) ? "descending time" : "";

            var counsellings = htny_DB.Counsellings.AsQueryable();
            counsellings = counsellings.Where(c => c.Psychologist.Email == User.Identity.Name && c.CStatus == "Not Booked");
            counsellings = counsellings.Where(c => c.Patient.Name.StartsWith(search) || search == null);
            if (sort == null)
            {
                counsellings = counsellings.OrderBy(c => c.CDateTime);
            }
            else
            {
                counsellings = counsellings.OrderByDescending(c => c.CDateTime);
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
                counselling.CEndDateTime = cDateTime.AddHours(1);
                counselling.CStatus = "Not Booked";
                var psys = htny_DB.Users.AsQueryable();
                List<ApplicationUser> psy = psys.Where(p => p.Email == User.Identity.Name).ToList();
                counselling.Psychologist = psy.FirstOrDefault();
                htny_DB.Counsellings.Add(counselling);
                htny_DB.SaveChanges();
            }
            
            return RedirectToAction("ManageCounselling");

        }

        public ActionResult EditUnbookedCounselling(int id)
        {
            Counselling counselling = htny_DB.Counsellings.Find(id);
            ViewBag.id = id;
            ViewBag.dateAndTime = counselling.CDateTime;
            
            return View(counselling);
        }

        [HttpPost]
        public ActionResult EditUnbookedCounselling(int id, String datetime)
        {
            Counselling counselling = htny_DB.Counsellings.Find(id);
            counselling.CDateTime = DateTime.Parse(datetime);
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

        public ActionResult EditBookedCounselling(int id)
        {
            Counselling counselling = htny_DB.Counsellings.Find(id);
            ViewBag.id = id;
            ViewBag.dateAndTime = counselling.CDateTime;

            return View(counselling);
        }

        [HttpPost]
        public ActionResult EditBookedCounselling(int id, String status)
        {
            Counselling counselling = htny_DB.Counsellings.Find(id);
            counselling.CStatus = status;
            try
            {
                htny_DB.Entry(counselling).State = EntityState.Modified;
                htny_DB.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                Console.Write(dbEx);
            }
            return RedirectToAction("BookedCounselling");
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

        public ActionResult BookedCounselling(string search, int? pageNumber, string sort)
        {
            ViewBag.SortByTime = string.IsNullOrEmpty(sort) ? "descending time" : "";
            ViewBag.SortByStatus = sort == "Status" ? "descending status" : "ascending status";

            var counsellings = htny_DB.Counsellings.AsQueryable();
            counsellings = counsellings.Where(c => c.Psychologist.Email == User.Identity.Name);
            counsellings = counsellings.Where(c => c.CStatus.Equals("Booked") || c.CStatus.Equals("Completed"));
            counsellings = counsellings.Where(c => c.Patient.Name.StartsWith(search) || search == null);

            switch (sort)
            {
                case "descending time":
                    counsellings = counsellings.OrderByDescending(c => c.CDateTime);
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

        public ActionResult UploadFeedback(int id)
        {
            ViewBag.cid = id;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadFeedback(HttpPostedFileBase postedFile, int cid, Boolean send)
        {
            TryValidateModel(postedFile);
            if (ModelState.IsValid)
            {
                //save file
                string fileName = Path.GetFileName(postedFile.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
                postedFile.SaveAs(filePath);

                //save into database
                UploadFile file = new UploadFile();
                Counselling counselling = htny_DB.Counsellings.Find(cid);
                file.Counselling = counselling;
                file.FileName = fileName;
                file.FilePath = filePath;
                Stream str = postedFile.InputStream;
                BinaryReader Br = new BinaryReader(str);
                Byte[] bytes = Br.ReadBytes((Int32)str.Length);
                file.FileContent = bytes;
                counselling.FeedbackFile = file;
                htny_DB.UploadFiles.Add(file);
                htny_DB.Entry(counselling).State = EntityState.Modified;
                htny_DB.SaveChanges();

                //send to patient
                if (send)
                {
                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                    string content = "Hi, I am your psychologist " + counselling.Psychologist.Name + ".\nHere is the feedback for your counselling.";
                    EmailSender emailSender = new EmailSender();
                    emailSender.Send("ygao0096@student.monash.edu", "Your Feedback", content, fileName, base64String);
                }

                return RedirectToAction("BookedCounselling");
            }

            return RedirectToAction("ManageCounselling");
        }


        [HttpGet] 
        public FileResult CheckFeedback(int id)
        {
            UploadFile file = htny_DB.UploadFiles.Find(id);

            return File(file.FileContent, "application/pdf", file.FileName);  
        }


    }
}