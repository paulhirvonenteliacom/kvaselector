using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kvaselector.Viewmodels
{
    public class ConfirmCodesVM
    {
        public string PageHeader { get; set; }

        public int TypeOfServiceId { get; set; }

        public int NoOfDiagnosisCodes { get; set; }
        public string DiagnosisHeader { get; set; }
        public List<string> SelectedDiagnosisCodes { get; set; }
        public List<string> SelectedDiagnosisDescriptions { get; set; }

        public int NoOfTreatmentCodes { get; set; }
        public string TreatmentHeader { get; set; }
        public List<string> SelectedTreatmentCodes { get; set; }
        public List<string> SelectedTreatmentDescriptions { get; set; }

        public int noOfDiaCodePositionsInOutput { get; set; }
        public int noOfTreCodePositionsInOutput { get; set; }

        public int?[] SelectedDiagnosisId { get; set; }
        public IEnumerable<SelectListItem>[] Diagnosis { get; set; }

        public int?[] SelectedTypeId { get; set; }
        public IEnumerable<SelectListItem>[] Type { get; set; }

        public string[] CheckedTreatment { get; set; }

        public int NoOfRadioButtons { get; set; }
        public FavoriteRadioButton[] FavoriteChoice { get; set; }

        public bool[] SelectedFavorite { get; set; }
        public FavoriteRadioButtonII[] FavoriteChoices { get; set; }

        public class FavoriteRadioButtonII
        {
            public int Id { get; set; }
            public string FavoriteName { get; set; }
        }

        public class FavoriteRadioButton
        {
            public string FavoriteName { get; set; }
            public bool Selected { get; set; }
        }

        public ConfirmCodesVM()
        {
            CheckedTreatment = new string[4];
            NoOfRadioButtons = 2;
        }
    }
}