using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CatService.Models
{
    public class DocumentBindingModel
    {
        [Required]
        [Display(Name = "DocumentName")]
        public string DocumentName { get; set; }

        [Required]
        [Display(Name = "MimeType")]
        public string MimeType { get; set; }
    }
}