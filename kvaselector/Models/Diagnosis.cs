using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kvaselector.Models
{
    public class Diagnosis
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Diagnoskod")]
        public string Code { get; set; }

        [Display(Name = "Beskrivning")]
        public string Description { get; set; }
    }
}