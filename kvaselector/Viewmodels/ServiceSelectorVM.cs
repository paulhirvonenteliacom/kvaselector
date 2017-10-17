using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kvaselector.Viewmodels
{
    public class ServiceSelectorVM
    {
        public string ApplicationPurpose { get; set; }

        public string SourceDocumentTitleLine1 { get; set; }
        public string SourceDocumentTitleLine2 { get; set; }
        public string SourceDocumentTitleLine3 { get; set; }

        public string SourceValidFrom { get; set; }
        public string SourceComplianceStatement { get; set; }

        public int? SelectedServiceTypeId { get; set; }
        public IEnumerable<SelectListItem> ServiceType { get; set; }
    }
}