using System;
using System.Collections.Generic;
using System.Data.Entity;
using HealingTalkNearestYou.Models;
using System.Linq;
using System.Web;

namespace HealingTalkNearestYou.DAL
{
    public class HealingTalkNearestYouContext : DbContext
    {
        public virtual DbSet<Psychologist> Psychologists { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Counselling> Counsellings { get; set; }
    }
}