using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HealingTalkNearestYou.Models;
using HealingTalkNearestYou.DAL;
using System.Web.Security;

namespace HealingTalkNearestYou.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            var selectUserTypeList = new List<SelectListItem>() {
                new SelectListItem() { Value = "1", Text = "Admin" },
                new SelectListItem() { Value = "2", Text = "Patient" },
                new SelectListItem() { Value = "3", Text = "Pychologist" }
            };

            ViewBag.UserTypeOptions = selectUserTypeList;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserRepository userRepository = new UserRepository();
                if (model.UserType == "1")
                {
                    AdminViewModel adminViewModel = userRepository.AdminValidate(model);
                    if (adminViewModel == null)
                    {
                        ModelState.AddModelError("", "Invalid Email/Password.");
                    }
                    else
                    {
                        // string adminData = string.Format(adminViewModel.AdminEmail);
                        string adminData = string.Format("{0}|{1}", adminViewModel.AdminEmail, "Admin");
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, adminViewModel.AdminEmail, DateTime.Now, DateTime.Now.AddDays(1),false, adminData);
                        string encryptTicket = FormsAuthentication.Encrypt(ticket);
                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptTicket);
                        Response.Cookies.Add(cookie);

                        return RedirectToAction("Index", "Home");
                    }
                }
                else if (model.UserType == "2")
                {
                    PatientViewModel patientViewModel = userRepository.PatientValidate(model);
                    if (patientViewModel == null)
                    {
                        ModelState.AddModelError("", "Invalid Email/Password.");
                    }
                    else
                    {
                        //string patientData = string.Format("{0}|{1}|{2}|{3}", patientViewModel.PatientEmail, patientViewModel.PatientFirstName, patientViewModel.PatientGender, patientViewModel.PatientDOB);
                        string patientData = string.Format("{0}|{1}", patientViewModel.PatientEmail, "Patient");
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, patientViewModel.PatientEmail, DateTime.Now, DateTime.Now.AddDays(1), false, patientData);
                        string encryptTicket = FormsAuthentication.Encrypt(ticket);
                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptTicket);
                        Response.Cookies.Add(cookie);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else if (model.UserType == "3")
                {
                    PsychologistViewModel psychologistViewModel = userRepository.PsychologistValidate(model);
                    if (psychologistViewModel == null)
                    {
                        ModelState.AddModelError("", "Invalid Email/Password.");
                    }
                    else
                    {
                        //string psyData = string.Format("{0}|{1}|{2}|{3}|{4}", psychologistViewModel.PsyEmail, psychologistViewModel.PsyFirstName, psychologistViewModel.PsyGender, psychologistViewModel.PsyDOB, psychologistViewModel.PsyDescription);
                        string psyData = string.Format("{0}|{1}", psychologistViewModel.PsyEmail, "Psychologist");
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, psychologistViewModel.PsyEmail, DateTime.Now, DateTime.Now.AddDays(1), false, psyData);
                        string encryptTicket = FormsAuthentication.Encrypt(ticket);
                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptTicket);
                        Response.Cookies.Add(cookie);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid Data.");
            }
            return View();
        }

        public ActionResult RegisterNormal()
        {
            var selectUserTypeList = new List<SelectListItem>() {
                //new SelectListItem() { Value = "1", Text = "Admin" },
                new SelectListItem() { Value = "1", Text = "Patient" },
                new SelectListItem() { Value = "2", Text = "Pychologist" }
            };

            var selectGenderTypeList = new List<SelectListItem>() {
                //new SelectListItem() { Value = "1", Text = "Admin" },
                new SelectListItem() { Value = "Female", Text = "Female" },
                new SelectListItem() { Value = "Male", Text = "Male" }
            };

            ViewBag.UserTypeOptions = selectUserTypeList;
            ViewBag.GenderOptions = selectGenderTypeList;

            return View();
        }

        [HttpPost]
        public ActionResult RegisterNormal(RegisterNormalViewModel model)
        {
            HTNYContainer1 htny_DB = new HTNYContainer1();
            UserRepository userRepository = new UserRepository();

            if (ModelState.IsValid)
            {
                if (model.UserType.Equals("1"))
                {
                    Patient newPatient = userRepository.PatientRegister(model);
                    htny_DB.PatientSet.Add(newPatient);
                    htny_DB.SaveChangesAsync();

                }
                else if (model.UserType.Equals("2"))
                {
                    Psychologist newPsychologist = userRepository.PsychologistRegister(model);
                    htny_DB.PsychologistSet.Add(newPsychologist);
                    htny_DB.SaveChangesAsync();
                    
                }
                return RedirectToAction("Login", "Account");

            }
            else
            {
                ModelState.AddModelError("", "Invalid Data.");
            }
            return View();
        }

        public ActionResult RegisterAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterAdmin(RegisterAdminViewModel model)
        {
            HTNYContainer1 htny_DB = new HTNYContainer1();
            UserRepository userRepository = new UserRepository();

            if (ModelState.IsValid)
            {
                Admin newAdmin = userRepository.AdminRegister(model);
                htny_DB.AdminSet.Add(newAdmin);
                htny_DB.SaveChangesAsync();
                return RedirectToAction("Login", "Account");
            }
            else
            { 
                ModelState.AddModelError("", "Invalid Data."); 
            }
            return View();
        }

        public RedirectToRouteResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult UserProfile()
        {
            return View();
        }

    }
}