using HealingTalkNearestYou.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealingTalkNearestYou.Util
{
    public class CounsellingManager
    {
        HTNYContainer1 htny_DB = new HTNYContainer1();

        public void cleanPassedCounselling()
        {
            List<Counselling> counsellings = htny_DB.CounsellingSet.ToList();

            foreach (Counselling c in counsellings)
            {
                if (c.CStatus == "Not Booked" && (DateTime.Compare(c.CDateTime, DateTime.Now) < 0))
                {
                    htny_DB.CounsellingSet.Remove(c);
                    htny_DB.SaveChanges();
                }
            }
        }
    }
}