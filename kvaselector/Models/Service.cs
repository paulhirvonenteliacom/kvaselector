using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kvaselector.Models
{
    public class Service
    {
        public int Id { get; set; }
        public int ServiceType { get; set; }

        public string DiagnosisCodeRemark { get; set; }
        public int NoOfDiagnosisCodes { get; set; }
        //public int?[] SelectedDiagnosisId { get; set; }
        //public IEnumerable<SelectListItem>[] Diagnosis { get; set; }

        public string TreatmentCodeRemark { get; set; }
        public int NoOfTreatmentCodes { get; set; }
        //public int?[] SelectedTreatmentId { get; set; }
        //public IEnumerable<SelectListItem>[] Treatment { get; set; }

        //This list contains pointers to the treatment lists that are valid for the service. The same list can appear multiple times in the same service.
        public string TreatmentTypeId { get; set; }

        //This list is used for treatments that can be selected from checkboxes
        //public List<Treatment>[] CheckedTreatment { get; set; }
        public int NoOfCheckedTreatmentLists { get; set; }
        public string CheckedTreatmentTypeId { get; set; }
    }
}