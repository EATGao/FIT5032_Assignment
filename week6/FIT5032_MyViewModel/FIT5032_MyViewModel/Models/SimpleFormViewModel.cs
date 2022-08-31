using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FIT5032_MyViewModel.Models
{
    public class SimpleFormViewModel
    {
    }

    public class FormOneViewModel
    {
        [Required] // 1. add error message here
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}