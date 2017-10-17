using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kvaselector.Models
{
    public class Treatment
    {
        [Key]
        public int Id { get; set; }

        public int TreatmentTypeId { get; set; }

        [Display(Name = "Åtgärdskod")]
        public string Code { get; set; }

        [Display(Name = "Åtgärd")]
        public string Name { get; set; }

        [Display(Name = "Beskrivning")]
        public string Description { get; set; }

        public virtual TreatmentType TreatmentType { get; set; }
    }
}