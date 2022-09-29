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
    [CustomAuthorization(UserType = "Admin")]
    public class AdminController : Controller
    {
        HTNYContainer1 htny_DB = new HTNYContainer1();
        // GET: Admin
        public ActionResult ManagePatient()
        {
            IEnumerable<Patient> allPatient = htny_DB.PatientSet;
          

            return View(allPatient);
        }

        public ActionResult EditPatient(int id)
        {
            Patient patient = htny_DB.PatientSet.Find(id);

            return View(patient);
        }

        [HttpPost]
        public ActionResult EditPatient(Patient patient)
        {
            htny_DB.Entry<Patient>(patient).State = System.Data.Entity.EntityState.Modified;
            htny_DB.SaveChanges();
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
            return View();
        }

        public ActionResult ManagePsychologist()
        {
            IEnumerable<Psychologist> allPsychologist = htny_DB.PsychologistSet;


            return View(allPsychologist);
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

            return View();
        }
    }
}