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
        [Required]
        [Display(Name = "ID")]
        public int CId { get; set; }

        [Required]
        [Display(Name = "Start At")]
        public DateTime CDateTime { get; set; }

        [Required]
        [Display(Name = "End At")]
        public DateTime CEndDateTime { get; set; }

        [Display(Name = "Rate")]
        public int? CRate { get; set; }

        [Required]
        [Display(Name = "Status")]
        public string CStatus { get; set; }

        [Required]
        public virtual ApplicationUser Psychologist { get; set; }
        public virtual ApplicationUser Patient { get; set; }
        public virtual UploadFile FeedbackFile { get; set; }
    }
}