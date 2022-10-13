using HealingTalkNearestYou.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealingTalkNearestYou.Util
{
    public class CounsellingManager
    {
        ApplicationDbContext htny_DB = new ApplicationDbContext();

        public void cleanPassedCounselling()
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
    }
}