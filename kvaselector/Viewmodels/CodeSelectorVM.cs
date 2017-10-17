using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kvaselector.Viewmodels
{
    public class CodeSelectorVM
    {
        public int NoOfDiagnosisCodes { get; set; }
        public int?[] SelectedDiagnosisId { get; set; }
        public IEnumerable<SelectListItem>[] Diagnosis { get; set; }

        public int NoOfTreatmentCodes { get; set; }
        public int?[] SelectedTypeId { get; set; }
        public IEnumerable<SelectListItem>[] Type { get; set; }

        public string[] TypeName { get; set; }
    }
}