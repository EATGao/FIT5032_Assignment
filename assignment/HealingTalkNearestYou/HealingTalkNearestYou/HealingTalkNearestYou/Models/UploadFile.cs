using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealingTalkNearestYou.Models
{
    public class UploadFile
    {
        [Key]
        [Required]
        [Display(Name = "ID")]
        public int FileId { get; set; }

        [Required]
        [Display(Name = "File Name")]
        public string FileName { get; set; }

        [Required]
        [Display(Name = "File Path")]
        public string FilePath { get; set; }

        [Required]
        [Display(Name = "File Content")]
        public byte[] FileContent { get; set; }

        [Required]
        public virtual Counselling Counselling { get; set; }

    }
}