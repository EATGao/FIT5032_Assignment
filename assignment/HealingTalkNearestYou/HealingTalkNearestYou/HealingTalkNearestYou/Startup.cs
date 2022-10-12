using HealingTalkNearestYou.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;
using System.Linq;

[assembly: OwinStartupAttribute(typeof(HealingTalkNearestYou.Startup))]
namespace HealingTalkNearestYou
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            PopulateRoles();
        }

        public void PopulateRoles()
        {
            var db = new ApplicationDbContext();
            if (!db.Roles.Any(x => x.Name == "Admin"))
            {
                db.Roles.Add(new IdentityRole("Admin"));
                db.SaveChanges();
            }
            if (!db.Roles.Any(x => x.Name == "Patient"))
            {
                db.Roles.Add(new IdentityRole("Patient"));
                db.SaveChanges();
            }
            if (!db.Roles.Any(x => x.Name == "Psychologist"))
            {
                db.Roles.Add(new IdentityRole("Psychologist"));
                db.SaveChanges();
            }
        }
    }
}
