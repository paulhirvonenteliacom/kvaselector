using kvaselector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kvaselector.Viewmodels
{
    public class TreatmentsOverviewVM
    {
        public string Type1Name { get; set; }
        public List<Treatment> Type1 { get; set; }

        public string Type2Name { get; set; }
        public List<Treatment> Type2 { get; set; }

        public string Type3Name { get; set; }
        public List<Treatment> Type3 { get; set; }

        public string Type4Name { get; set; }
        public List<Treatment> Type4 { get; set; }
    }
}