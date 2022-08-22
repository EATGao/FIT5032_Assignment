using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealingTalkNearestYou.Models
{
    public class Psychologist
    {
        [Key]
        public int PyschologistId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        public string SelfDescription { get; set; }
    }
}