using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HealingTalkNearestYou.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string UserEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string UserPassword { get; set; }

        [Required]
        [Display(Name = "Login as ")]
        public string UserType { get; set; }
    }

    public class RegisterAdminViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string UserType = "Admin";
    }

    public class RegisterNormalViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }


        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "Date of birth")]
        public DateTime DOB { get; set; }

        [Required]
        [Display(Name = "Register as ")]
        public string UserType { get; set; }
    }

    public class AdminViewModel 
    {
        public string AdminEmail { get; set; }
        public string AdminPassword { get; set; }
    }

    public class PatientViewModel
    {
        public string PatientEmail;
        public string PatientName;
        public string PatientGender;
        public DateTime PatientDOB;
    }

    public class PsychologistViewModel
    {
        public string PsyEmail;
        public string PsyName;
        public string PsyGender;
        public DateTime PsyDOB;
        public string PsyDescription;
    }
}