using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kvaselector.Models
{
    public class FavoriteService
    {
        public int Id { get; set; }

        public int FavoriteNameId { get; set; }

        public int ServiceTypeId { get; set; }

        public string SelectedDiagnosisCodeId { get; set; }

        public string SelectedTreatmentCodeId { get; set; }

        public string SelectedCheckedTreatmentCodeId { get; set; }
    }
}