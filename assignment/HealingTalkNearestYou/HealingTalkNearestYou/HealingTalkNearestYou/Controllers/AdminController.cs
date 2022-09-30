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
    [CustomAuthorization(UserType = "Admin")]
    public class AdminController : Controller
    {
        HTNYContainer1 htny_DB = new HTNYContainer1();
        // GET: Admin
        public ActionResult ManagePatient(string option, string search, int? pageNumber, string sort)
        {
            ViewBag.SortByID = string.IsNullOrEmpty(sort) ? "descending ID" : "";
            ViewBag.SortByName = string.IsNullOrEmpty(sort) ? "descending name" : "ascending name";
            ViewBag.SortByGender = sort == "Gender" ? "descending gender" : "ascending gender";
            ViewBag.SortByDOB = sort == "Date of Birth" ? "descending dob" : "ascending dob";
            var patients = htny_DB.PatientSet.AsQueryable();
            int searchID = -1;
            if (option == "ID" && Int32.TryParse(search, out searchID))
            {
                patients = patients.Where(p => p.PatientId == searchID || search == null);
            }
            else
            {
                patients = patients.Where(p => p.PatientName.StartsWith(search) || search == null);
            }

            switch (sort)
            {

                case "descending name":
                    patients = patients.OrderByDescending(p => p.PatientName);
                    break;

                case "ascending name":
                    patients = patients.OrderBy(p => p.PatientName);
                    break;

                case "descending gender":
                    patients = patients.OrderByDescending(p => p.PatientName);
                    break;

                case "ascending gender":
                    patients = patients.OrderBy(p => p.PatientGender);
                    break;
                case "descending dob":
                    patients = patients.OrderByDescending(p => p.PatientDOB);
                    break;

                case "ascending dob":
                    patients = patients.OrderBy(p => p.PatientDOB);
                    break;
                case "descending ID":
                    patients = patients.OrderByDescending(p => p.PatientId);
                    break;
                default:
                    patients = patients.OrderBy(p => p.PatientId);
                    break;

            }


            return View(patients.ToPagedList(pageNumber ?? 1, 10));
        }

        public ActionResult EditPatient(int id)
        {
            Patient patient = htny_DB.PatientSet.Find(id);

            return View(patient);
        }

        [HttpPost]
        public ActionResult EditPatient(Patient patient)
        {
            try
            {
                htny_DB.Entry<Patient>(patient).State = System.Data.Entity.EntityState.Modified;
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
            Patient patient = htny_DB.PatientSet.Find(id);
            if (patient == null)
            {
                return RedirectToAction("ManagePatient");
            }
            else
            {
                htny_DB.PatientSet.Remove(patient);
                htny_DB.SaveChanges();
            }
            return RedirectToAction("ManagePatient");
        }

        public ActionResult ManagePsychologist(string option, string search, int? pageNumber, string sort)
        {
            ViewBag.SortByID = string.IsNullOrEmpty(sort) ? "descending ID" : "";
            ViewBag.SortByName = string.IsNullOrEmpty(sort) ? "descending name" : "ascending name";

            var psychologists = htny_DB.PsychologistSet.AsQueryable();
            int searchID = -1;
            if (option == "ID" && Int32.TryParse(search, out searchID))
            {
                psychologists = psychologists.Where(p => p.PsyId == searchID || search == null);
            }
            else
            {
                psychologists = psychologists.Where(p => p.PsyName.StartsWith(search) || search == null);
            }

            switch (sort)
            {

                case "descending name":
                    psychologists = psychologists.OrderByDescending(p => p.PsyName);
                    break;

                case "ascending name":
                    psychologists = psychologists.OrderBy(p => p.PsyName);
                    break;

                case "descending ID":
                    psychologists = psychologists.OrderByDescending(p => p.PsyId);
                    break;

                default:
                    psychologists = psychologists.OrderBy(p => p.PsyId);
                    break;

            }


            return View(psychologists.ToPagedList(pageNumber ?? 1, 10));
        }
    
        

        public ActionResult EditPsy(int id)
        {
            Psychologist psy = htny_DB.PsychologistSet.Find(id);
            return View(psy);
        }

        [HttpPost]
        public ActionResult EditPsy(Psychologist psy)
        {

                htny_DB.Entry<Psychologist>(psy).State = System.Data.Entity.EntityState.Modified;
                htny_DB.SaveChanges();
            
            return RedirectToAction("ManagePsychologist");
        }

        public ActionResult DetailPsy(int id)
        {
            Psychologist psy = htny_DB.PsychologistSet.Find(id);
            return View(psy);
        }

        public ActionResult DeletePsy(int id)
        {
            Psychologist psy = htny_DB.PsychologistSet.Find(id);
            if (psy == null)
            {
                return RedirectToAction("ManagePsychologist");
            }
            else
            {
                htny_DB.PsychologistSet.Remove(psy);
                htny_DB.SaveChanges();
            }

            return RedirectToAction("ManagePsychologist");
        }
    }
}