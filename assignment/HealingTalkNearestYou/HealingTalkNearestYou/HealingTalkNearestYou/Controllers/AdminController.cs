
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;
using HealingTalkNearestYou.Models;
using PagedList;


namespace HealingTalkNearestYou.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        ApplicationDbContext htny_DB = new ApplicationDbContext();
        // GET: Admin
        public ActionResult ManageUser(string search, int? pageNumber, string sort)
        {

            ViewBag.SortByName = string.IsNullOrEmpty(sort) ? "" : "descending name";
            ViewBag.SortByGender = sort == "Gender" ? "descending gender" : "ascending gender";
            ViewBag.SortByDOB = sort == "Date of Birth" ? "descending dob" : "ascending dob";
            var users = htny_DB.Users.AsQueryable();


            users = users.Where(p => p.Name.StartsWith(search) || search == null);


            switch (sort)
            {

                case "descending name":
                    users = users.OrderByDescending(p => p.Name);
                    break;

                case "ascending dob":
                    users = users.OrderBy(p => p.DOB);
                    break;

                case "descending gender":
                    users = users.OrderByDescending(p => p.Gender);
                    break;

                case "ascending gender":
                    users = users.OrderBy(p => p.Gender);
                    break;
                case "descending dob":
                    users = users.OrderByDescending(p => p.DOB);
                    break;

                default:
                    users = users.OrderBy(p => p.Name);
                    break;


            }


            return View(users.ToPagedList(pageNumber ?? 1, 10));
        }

        public ActionResult EditUser(string id)
        {
            ApplicationUser user = htny_DB.Users.Find(id);

            return View(user);
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

        public ActionResult DeleteUser(string id)
        {
            ApplicationUser user = htny_DB.Users.Find(id);
            if (user == null)
            {
                return RedirectToAction("ManageUser");
            }
            else
            {
                htny_DB.Users.Remove(user);
                htny_DB.SaveChanges();
            }
            return RedirectToAction("ManageUser");
        }

    }
}