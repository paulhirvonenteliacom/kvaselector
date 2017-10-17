using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kvaselector.Viewmodels
{
    public class CodeSelectorIIVM
    {
        public string PageHeader { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public int SelectedServiceTypeId { get; set; }

        public string FavoriteHeading { get; set; }
        public int? SelectedFavoriteId { get; set; }
        public IEnumerable<SelectListItem> Favorite { get; set; }

        public string DiagnosisCodeRemark { get; set; }
        public int NoOfDiagnosisCodes { get; set; }
        public string[] DiagnosisCodeHeading { get; set; }
        public int?[] SelectedDiagnosisId { get; set; }
        public IEnumerable<SelectListItem>[] Diagnosis { get; set; }
        public int?[] PreSelectedDiagnosisId { get; set; }

        public string TreatmentCodeRemark { get; set; }
        public int NoOfTreatmentCodes { get; set; }
        public string[] TreatmentCodeHeading { get; set; }
        public int?[] SelectedTreatmentId { get; set; }
        public IEnumerable<SelectListItem>[] Treatment { get; set; }
        public int?[] PreSelectedTreatmentId { get; set; }

        public int NoOfCheckedTreatmentLists { get; set; }
        public string[] CheckedTreatmentHeading { get; set; }
        public List<TreatmentCheckBox>[] CheckedTreatment { get; set; }
    }

    public class TreatmentCheckBox
    {
        public string CheckboxTreatment { get; set; }
        public bool Checked { get; set; }
    }
}