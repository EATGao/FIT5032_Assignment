using HealingTalkNearestYou.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealingTalkNearestYou.Util
{
    public class CounsellingManager
    {
        private ApplicationDbContext htny_DB = new ApplicationDbContext();

        public void CleanPassedCounselling()
        {
            List<Counselling> counsellings = htny_DB.Counsellings.ToList();

            foreach (Counselling c in counsellings)
            {
                if (c.CStatus.Equals("Not Booked") && (DateTime.Compare(c.CDateTime, DateTime.Now) < 0))
                {
                    htny_DB.Counsellings.Remove(c);
                    htny_DB.SaveChanges();
                }
            }
        }

        public Counselling CheckAvailable(DateTime datetime, string userName, string userType)
        {
            var counsellings = htny_DB.Counsellings.AsQueryable();
            if (userType == "Psychologist")
            {
                counsellings = counsellings.Where(c => c.Psychologist.Email == userName);
            }
            else if (userType == "Patient")
            {
                counsellings = counsellings.Where(c => c.Patient.Email == userName && c.CStatus == "Booked");
            }

            foreach (Counselling c in counsellings)
            {
                if (datetime >= c.CDateTime && datetime < c.CEndDateTime)
                {
                    return c;
                }
            }

            return null;
        }
    }
}