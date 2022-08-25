using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealingTalkNearestYou.Models
{
    public class Counselling
    {
        [Key]
        public int CounsellingId { get; set; }

        [Required]
        public Psychologist ThePsychologist { get; set; }

        [Required]
        public DateTime CousellingDateTime { get; set; }

        [Required]
        public string Status { get; set; }

        public int Patientid { get; set; }
    }
}