
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HealingTalkNearestYou.Models;
using HealingTalkNearestYou.Util;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using SendGrid.Helpers.Mail;

namespace HealingTalkNearestYou.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext htny_DB = new ApplicationDbContext();
        // GET: Admin
        public ActionResult ManageUser(string search, int? pageNumber, string sort)
        {
            ViewBag.SortByEmail = string.IsNullOrEmpty(sort) ? "descending email" : "";
            ViewBag.SortByName = sort == "descending name" ? "ascending name" : "descending name";

            var users = htny_DB.Users.AsQueryable();

            users = users.Where(x => x.UserName != User.Identity.Name);
            if (search != null && Regex.IsMatch(search, "[A-Za-z0-9][@][A-Za-z0-9]+[.][A-Za-z0-9]"))
            {
                users = users.Where(x => x.Email.StartsWith(search) || search == null);
            }
            else 
            {
                users = users.Where(x => x.Name.StartsWith(search) || search == null);
            }


            switch (sort)
            {
                case "ascending name":
                    users = users.OrderBy(x => x.Name);
                    break;
                case "descending name":
                    users = users.OrderByDescending(x => x.Name);
                    break;
                case "descending email":
                    users = users.OrderByDescending(x => x.Email);
                    break;
                case "ascending email":
                    users = users.OrderBy(x => x.Email);
                    break;
                default:
                    users = users.OrderBy(x => x.Email);
                    break;
            }


            return View(users.ToPagedList(pageNumber ?? 1, 10));
        }

        public ActionResult EditUser(string id)
        {
            ApplicationUser user = htny_DB.Users.Find(id);
            ViewBag.dob = user.DOB;
            return View(user);
        }

        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(string email, string password, string gender, DateTime dOB, string name, string userType)
        {
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var user = new ApplicationUser { UserName = email, Email = email, Gender = gender, DOB = dOB, Name = name };
            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                _ = await userManager.AddToRoleAsync(user.Id, userType);
            }

            return RedirectToAction("ManageUser");
        }

        [HttpPost]
        public ActionResult EditUser(ApplicationUser user)
        {
            try
            {
                htny_DB.Entry(user).State = EntityState.Modified;
                htny_DB.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                Console.Write(dbEx);
            }
            return RedirectToAction("ManageUser");
        }

        public ActionResult DetailUser(string id)
        {
            ApplicationUser user = htny_DB.Users.Find(id);
            return View(user);
        }

        public ActionResult Delete(string id)
        {
            ApplicationUser user = htny_DB.Users.Find(id);
            if (user == null)
            {
                return RedirectToAction("ManageUser");
            }
            else
            {
                if (user.Counsellings.Count == 0)
                {
                    htny_DB.Users.Remove(user);
                    htny_DB.SaveChanges();
                }
            }
            return RedirectToAction("ManageUser");
        }

        public ActionResult SendAnnouncement()
        { 
            return View();
        }

        [HttpPost]
        public ActionResult SendAnnouncement(string content, HttpPostedFileBase postedFile = null)
        {
            EmailSender emailSender = new EmailSender();
            List<EmailAddress> tos = new List<EmailAddress>();
            if (postedFile != null)
            {
                TryValidateModel(postedFile);
                if (ModelState.IsValid)
                {
                    //change to base64
                    Stream str = postedFile.InputStream;
                    BinaryReader Br = new BinaryReader(str);
                    Byte[] bytes = Br.ReadBytes((Int32)str.Length);
                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);

                    // get all patient and psychologist email here
                    List<ApplicationUser> users = htny_DB.Users.ToList();
                    foreach (ApplicationUser u in users)
                    {
                        tos.Add(new EmailAddress(u.Email, ""));
                    }

                    emailSender.SendAnnouncementToAll(tos, "HTNY Announcement", content,
                        Path.GetFileName(postedFile.FileName), base64String);
                }
            }
            else 
            {
                // get all patient and psychologist email here
                tos.Add(new EmailAddress("ygao0096@student.monash.edu", ""));
                emailSender.SendAnnouncementToAll(tos, "HTNY Announcement", content);
            }
            return RedirectToAction("ManageUser");
        }

    }
}