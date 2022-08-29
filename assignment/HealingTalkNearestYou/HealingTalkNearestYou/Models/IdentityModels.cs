using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HealingTalkNearestYou.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    // the class where we will add other properties
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here

            return userIdentity;
        }
    }

    //used for connection to database as base constructor having
    //“DefaultConnection” name which is the same as we have in our Web.Config file
    public class HealingTalkNearestYouDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Psychologist> Psychologists { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Counselling> Counsellings { get; set; }

        public HealingTalkNearestYouDbContext()
            : base("HealingTalkNearestYouDb", throwIfV1Schema: false)
        {
        }

        public static HealingTalkNearestYouDbContext Create()
        {
            return new HealingTalkNearestYouDbContext();
        }
    }
}