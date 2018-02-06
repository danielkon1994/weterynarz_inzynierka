using System;
using System.Collections.Generic;
using System.Text;

namespace Weterynarz.Domain.ViewModels.SummaryVisit
{
    public class SummaryVisitIndexViewModel
    {
        public DateTime VisitDate { get; set; }
        public string Owner { get; set; }
        public string Animal { get; set; }
        public IEnumerable<string> Diseases { get; set; }
        public IEnumerable<string> MedicalExaminations { get; set; }
        public string Drugs { get; set; }
        public string Description { get; set; }
    }
}
